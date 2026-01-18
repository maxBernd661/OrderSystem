using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public class DataManipulationService<TEntity>(IServiceProvider sp, OrderContext context) : IDataManipulationService where TEntity : PersistentEntityBase
    {
        public async Task<PersistentEntityBase> SaveAsync(IManagedView managedView, CancellationToken ct = default)
        {
            if (managedView.Holder.View is not IDetailView dv)
            {
                return new Product();
            }

            if (managedView.ManagedEntity is null ||
                managedView.ManagedEntity.CreatedAt == DateTime.MinValue)
            {
                return await SaveNew(dv, ct);
            }

            return await UpdateExisting(managedView, dv, ct);
        }

        private async Task<PersistentEntityBase> UpdateExisting(IManagedView managedView, IDetailView dv, CancellationToken ct)
        {
            TEntity incomingEntity = (TEntity)dv.Template.ReadData();
            TEntity trackedEntity = await LoadForUpdate(managedView.ManagedEntity!.Id, ct);

            context.Entry(trackedEntity).CurrentValues.SetValues(incomingEntity);

            IGraphMerger<TEntity>? merger = sp.GetService<IGraphMerger<TEntity>>();
            if (merger != null)
            {
                await merger.Merge(context, trackedEntity, incomingEntity, ct);
            }

            await context.SaveChangesAsync(ct);
            return trackedEntity;
        }

        private async Task<PersistentEntityBase> SaveNew(IDetailView dv, CancellationToken ct = default)
        {
            TEntity incomingEntity = (TEntity)dv.Template.ReadData();
            incomingEntity = await SetNavigations(incomingEntity, ct);
            TrackGraphForInsert(incomingEntity);

            await context.SaveChangesAsync(ct);
            return incomingEntity;
        }

        private async Task<TEntity> LoadForUpdate(Guid id, CancellationToken ct = default)
        {
            IQueryable<TEntity> q = context.Set<TEntity>();

            IQueryProfile<TEntity> queryProfile = sp.GetRequiredService<IQueryProfile<TEntity>>();
            q = queryProfile.Apply(q);

            return await q.SingleAsync(x => x.Id == id, ct);
        }

        private async Task<TEntity> SetNavigations(TEntity savingEntity, CancellationToken ct = default)
        {
            if (savingEntity is Order order)
            {
                Customer? customer = await context.Set<Customer>().FirstOrDefaultAsync(x => x.Id == order.CustomerId, ct);
                if (customer != null)
                {
                    order.SetCustomer(customer);
                }
            }

            return savingEntity;
        }

        private void TrackGraphForInsert(TEntity rootEntity)
        {
            context.ChangeTracker.TrackGraph(rootEntity, node =>
            {
                PersistentEntityBase entity = (PersistentEntityBase)node.Entry.Entity;
                node.Entry.State = entity.CreatedAt == DateTime.MinValue ? EntityState.Added : EntityState.Unchanged;
            });
        }

        public async Task<Result> DeleteAsync(IManagedView managedView, CancellationToken ct = default)
        {
            TEntity? entity = null;

            if (managedView.ManagedEntity is TEntity dvEntity)
            {
                entity = dvEntity;
            }
            else if (managedView.Holder.View is IListView lv && lv.GetSelectedItem() is TEntity selectedEntity)
            {
                entity = selectedEntity;
            }

            if (entity is null)
            {
                return Result.Fail("Nothing to delete");
            }

            TEntity? trackedEntity = await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id, ct);
            if (trackedEntity is null)
            {
                return Result.Fail("Entity not saved");
            }

            context.Remove(trackedEntity);
            await context.SaveChangesAsync(ct);
            return Result.Ok();
        }
    }

    public interface IDataManipulationService
    {
        Task<PersistentEntityBase> SaveAsync(IManagedView managedView, CancellationToken ct = default);

        Task<Result> DeleteAsync(IManagedView managedView, CancellationToken ct = default);
    }
}