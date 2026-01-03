using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public class SavingService<TEntity>(OrderContext context) : ISavingService
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

            await context.SaveData(ct);
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
    }

    public interface ISavingService
    {
        Task<PersistentEntityBase> SaveAsync(IManagedView managedView, CancellationToken ct = default);
    }
}