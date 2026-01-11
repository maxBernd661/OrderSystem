using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public interface IRequireLookup<in TLookup> where TLookup : IEntityLookup
    {
        void SetLookup(TLookup lookup);
    }

    public interface IEntityLookup;

    public sealed class ProductLookups : IEntityLookup
    {
        public List<ProductLookup> Lookups { get; init; }
    }

    public sealed record ProductLookup(Guid Id, string Name, decimal Price, double Weight, bool IsAvailable);

    public sealed class CustomerLookups : IEntityLookup
    {
        public List<CustomerLookup> Lookups { get; init; }
    }

    public sealed record CustomerLookup(Guid Id, string Name);

    public interface IProviderBase
    {
        public Task<IEntityLookup> GetLookups(CancellationToken ct = default);
    }

    public interface IProductLookupProvider : IProviderBase
    {
        public new Task<ProductLookups> GetLookups(CancellationToken ct = default);
    }

    public sealed class ProductLookupProvider(IDbContextFactory<OrderContext> dbFactory) : IProductLookupProvider
    {
        private readonly IDbContextFactory<OrderContext> dbFactory = dbFactory;

        public async Task<ProductLookups> GetLookups(CancellationToken ct = default)
        {
            await using OrderContext db = await dbFactory.CreateDbContextAsync(ct);

            List<ProductLookup> products = await db.Set<Product>()
                                                    .Where(x => x.IsAvailable)
                                                    .OrderBy(x => x.Name)
                                                    .Select(x => new ProductLookup(x.Id, x.Name, x.UnitPrice, x.Weight, x.IsAvailable))
                                                    .AsNoTracking()
                                                    .ToListAsync(ct);

            return new ProductLookups { Lookups = products };
        }

        async Task<IEntityLookup> IProviderBase.GetLookups(CancellationToken ct)
        {
            return await GetLookups(ct);
        }
    }

    public interface ICustomerLookupProvider : IProviderBase
    {
        new Task<CustomerLookups> GetLookups(CancellationToken ct = default);
    }

    public sealed class CustomerLookupProvider(IDbContextFactory<OrderContext> dbFactory) : ICustomerLookupProvider
    {
        private readonly IDbContextFactory<OrderContext> dbFactory = dbFactory;

        public async Task<CustomerLookups> GetLookups(CancellationToken ct = default)
        {
            await using OrderContext db = await dbFactory.CreateDbContextAsync(ct);

            List<CustomerLookup> customers = await db.Set<Customer>()
                                                     .Where(x => x.IsActive)
                                                     .OrderBy(x => x.Name)
                                                     .Select(x => new CustomerLookup(x.Id, x.Name))
                                                     .AsNoTracking()
                                                     .ToListAsync(ct);

            return new CustomerLookups() { Lookups = customers };
        }

        async Task<IEntityLookup> IProviderBase.GetLookups(CancellationToken ct)
        {
            return await GetLookups(ct);
        }
    }
}