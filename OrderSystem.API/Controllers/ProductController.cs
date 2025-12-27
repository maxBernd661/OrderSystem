using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.API.Controllers
{
    [ApiController]
    [Route("/product")]
    public class ProductController(OrderContext context) : ControllerBase
    {
        private readonly OrderContext context = context;

        [HttpGet]
        public async Task<ActionResult<List<ProductLite>>> GetAll(CancellationToken ct)
        {
            List<Product> products = await context.Products
                                                   .AsNoTracking()
                                                   .Where(x => !x.IsDeleted)
                                                   .ToListAsync(cancellationToken: ct);
            List<ProductLite> output = [];
            output.AddRange(products.Select(product => new ProductLite(product.Id, product.Name, product.UnitPrice)));
            return output;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductLite>> GetById(Guid id, CancellationToken ct)
        {
            Product? existing = await context.Products
                                             .AsNoTracking()
                                             .Where(x => !x.IsDeleted && x.Id == id)
                                             .SingleOrDefaultAsync(cancellationToken: ct);
            if (existing is null)
            {
                return NotFound();
            }

            return new ProductLite(existing.Id, existing.Name, existing.UnitPrice);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProductRequest request, CancellationToken ct)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UnitPrice = request.UnitPrice,
            };

            context.Products.Add(product);
            await context.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, UpdateProductRequest request, CancellationToken ct)
        {
            Product? existing = await context.Products
                                              .Where(x => !x.IsDeleted && x.Id == id)
                                              .SingleOrDefaultAsync(cancellationToken: ct);

            if (existing is null)
            {
                return NotFound();
            }

            existing.Name = request.Name;
            existing.UnitPrice = request.UnitPrice;

            await context.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            Product? existing = await context.Products
                                              .Where(x => !x.IsDeleted && x.Id == id)
                                              .SingleOrDefaultAsync(cancellationToken: ct);

            if (existing is null)
            {
                return NotFound();
            }

            existing.Delete();
            await context.SaveChangesAsync(ct);

            return Ok();
        }
    }

    public sealed record ProductLite(Guid Id, string Name, decimal UnitPrice);

    public sealed record CreateProductRequest(string Name, decimal UnitPrice);

    public sealed record UpdateProductRequest(string Name, decimal UnitPrice);
}