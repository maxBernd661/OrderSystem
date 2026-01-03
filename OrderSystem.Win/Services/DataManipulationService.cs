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

            if (managedView.ManagedEntity is null)
            {
                savingEntity = (TEntity)dv.Template.ReadData();
                set.Add(savingEntity);
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
            if (managedView.ManagedEntity is TEntity entity)
            {
                entity.Delete();
            }
            else if (managedView.Holder.View is IListView lv && lv.GetData() is { } id)
            {
                TEntity? foundItem = await context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
                foundItem?.Delete();
            }
            else
            {
                return Result.Fail("Nothing to delete");
            }

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