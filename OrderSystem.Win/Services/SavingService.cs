using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public class SavingService<TEntity>(OrderContext context) : ISavingService
        where TEntity : PersistentEntityBase
    {
        public async Task SaveAsync(IManagedView managedView, CancellationToken ct = default)
        {
            if (managedView.Holder.View is not IDetailView dv)
            {
                return;
            }

            DbSet<TEntity> set = context.Set<TEntity>();

            if (managedView.ManagedEntity is null)
            {
                TEntity newItem = (TEntity)dv.Template.ReadData();
                set.Add(newItem);
            }
            else
            {
                TEntity newItem = (TEntity)dv.Template.ReadData();
                TEntity existingItem = await set.SingleAsync(x => x.Id == managedView.ManagedEntity.Id, ct);
                newItem = SetBaseValues(newItem, existingItem);

                context.Entry(existingItem).CurrentValues.SetValues(newItem);
            }

            await context.SaveChangesAsync(ct);
        }

        private TEntity SetBaseValues(TEntity entity, TEntity existingEntity)
        {
            entity.Id = existingEntity.Id;
            entity.CreatedAt = existingEntity.CreatedAt;
            entity.UpdatedAt = existingEntity.UpdatedAt;
            entity.IsDeleted = existingEntity.IsDeleted;
            return entity;
        }
    }

    public interface ISavingService
    {
        Task SaveAsync(IManagedView managedView, CancellationToken ct = default);
    }
}