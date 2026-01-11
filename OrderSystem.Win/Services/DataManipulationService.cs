using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public class DataManipulationService<TEntity>(OrderContext context) : IDataManipulationService
        where TEntity : PersistentEntityBase
    {
        public async Task<PersistentEntityBase> SaveAsync(IManagedView managedView, CancellationToken ct = default)
        {
            if (managedView.Holder.View is not IDetailView dv)
            {
                return new Product();
            }

            TEntity savingEntity;

            DbSet<TEntity> set = context.Set<TEntity>();

            if (managedView.ManagedEntity is null ||
                managedView.ManagedEntity.CreatedAt == DateTime.MinValue)
            {
                savingEntity = (TEntity)dv.Template.ReadData();
                savingEntity = await SetNavigations(savingEntity, ct);
                TrackGraphForInsert(savingEntity);
            }
            else
            {
                savingEntity = (TEntity)dv.Template.ReadData();
                TEntity existingItem = await set.SingleAsync(x => x.Id == managedView.ManagedEntity.Id, ct);
                savingEntity = SetBaseValues(savingEntity, existingItem);

                context.Entry(existingItem).CurrentValues.SetValues(savingEntity);
            }

            await context.SaveChangesAsync(ct);
            return savingEntity;
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

        private TEntity SetBaseValues(TEntity entity, TEntity existingEntity)
        {
            entity.Id = existingEntity.Id;
            entity.CreatedAt = existingEntity.CreatedAt;
            entity.UpdatedAt = existingEntity.UpdatedAt;
            entity.IsDeleted = existingEntity.IsDeleted;
            return entity;
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