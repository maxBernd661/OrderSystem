using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderSystem.Core.Entities;

namespace OrderSystem.Core
{
    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> Items { get; set; }

        public DbSet<OrderStatusHistory> History { get; set; }

        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Automatically excludes all entities flagged with <c>IsDeleted</c>.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<OrderStatusHistory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }

        public override int SaveChanges()
        {
            ValidatePending();
            HandleDeletion();
            UpdateTimestamp();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            ValidatePending();
            HandleDeletion();
            UpdateTimestamp();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Attempts to validate all tracked entities
        /// </summary>
        /// <exception cref="ValidationException{TEntity}">Thrown, when an entity could not be validated</exception>
        private void ValidatePending()
        {
            IEnumerable<PersistentEntityBase> entities = ChangeTracker.Entries<PersistentEntityBase>()
                                                                      .Where(x => x.State is EntityState.Added or EntityState.Modified)
                                                                      .Select(x => x.Entity);

            foreach (PersistentEntityBase entity in entities)
            {
                entity.ValidateOrThrow();
            }
        }

        /// <summary>
        /// Iterates over tracked entities, setting the <seealso cref="PersistentEntityBase.CreatedAt"/> and <seealso cref="PersistentEntityBase.UpdatedAt"/> timestamps
        /// </summary>
        private void UpdateTimestamp()
        {
            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry<PersistentEntityBase> entry in ChangeTracker.Entries<PersistentEntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.UpdatedAt = now;
                        break;

                    case EntityState.Deleted:
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }
        }

        /// <summary>
        /// Iterates over tracked entities, sets <seealso cref="PersistentEntityBase.IsDeleted"/> for soft-deletion
        /// </summary>
        private void HandleDeletion()
        {
            foreach (EntityEntry<PersistentEntityBase> entry in ChangeTracker.Entries<PersistentEntityBase>())
            {
                if (entry.State is EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
        }
    }

    /// <summary>
    /// Thrown when <see cref="PersistentEntityBase.ValidateOrThrow"/> finds one or more problems during Validation
    /// </summary>
    public class ValidationException<TEntity>(TEntity entity, string error) : Exception(error) where TEntity : PersistentEntityBase
    {
        public TEntity Entity { get; } = entity;
    }
}