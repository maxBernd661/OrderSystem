using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.API.Controllers
{
    [ApiController]
    [Route("/customer")]
    public class CustomerController(OrderContext context) : ControllerBase
    {
        private readonly OrderContext context = context;

        [HttpGet]
        public async Task<ActionResult<List<CustomerLite>>> GetAll(CancellationToken ct)
        {
            List<Customer> customers = await context.Customers
                                                   .AsNoTracking()
                                                   .Where(x => !x.IsDeleted)
                                                   .ToListAsync(cancellationToken: ct);
            List<CustomerLite> output = [];
            output.AddRange(customers.Select(customer => new CustomerLite(customer.Id, customer.Name, customer.Email, customer.IsActive)));
            return output;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerLite>> GetById(Guid id, CancellationToken ct)
        {
            Customer? existing = await context.Customers
                                              .AsNoTracking()
                                              .Where(x => !x.IsDeleted && x.Id == id)
                                              .SingleOrDefaultAsync(cancellationToken: ct);
            if (existing is null)
            {
                return NotFound();
            }

            return new CustomerLite(existing.Id, existing.Name, existing.Email, existing.IsActive);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCustomerRequest request, CancellationToken ct)
        {
            Customer customer = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
            };

            context.Customers.Add(customer);
            await context.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, UpdateCustomerRequest request, CancellationToken ct)
        {
            Customer? existing = await context.Customers
                                              .Where(x => !x.IsDeleted && x.Id == id)
                                              .SingleOrDefaultAsync(cancellationToken: ct);

            if (existing is null)
            {
                return NotFound();
            }

            if (request.IsActive != null)
            {
                existing.IsActive = request.IsActive.Value;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                existing.Email = request.Email;
            }

            existing.Name = request.Name;

            await context.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            Customer? existing = await context.Customers
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

    public sealed record CustomerLite(Guid Id, string Name, string Email, bool IsActive)
    {
        public static CustomerLite Empty()
        {
            return new CustomerLite(Guid.Empty, string.Empty, string.Empty, false);
        }
    }

    public sealed record CreateCustomerRequest(string Name, string Email);

    public sealed record UpdateCustomerRequest(string Name, string? Email, bool? IsActive);

    public sealed record DeleteCustomerRequest(Guid Id);

    public sealed record CreateOrderRequest(Guid CustomerId);
}