using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.API.Controllers
{
    [ApiController]
    [Route("/order")]
    public class OrderController(OrderContext context) : ControllerBase
    {
        private readonly OrderContext context = context;

        [HttpGet]
        public async Task<ActionResult<List<OrderLite>>> GetAll(CancellationToken ct)
        {
            List<Order> orders = await context.Orders
                                              .AsNoTracking()
                                              .Where(x => !x.IsDeleted)
                                              .Include(order => order.Items).ThenInclude(x => x.Product)
                                              .Include(x => x.Customer)
                                              .Include(x => x.History)
                                              .ToListAsync(cancellationToken: ct);
            List<OrderLite> output = [];
            output.AddRange(orders.Select(BuildLite));

            return output;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderLite>> GetById(Guid id, CancellationToken ct)
        {
            Order? existing = await context.Orders
                                           .AsNoTracking()
                                           .Where(x => !x.IsDeleted && x.Id == id)
                                           .Include(order => order.Items).ThenInclude(x => x.Product)
                                           .Include(x => x.Customer)
                                           .Include(x => x.History)
                                           .SingleOrDefaultAsync(cancellationToken: ct);
            if (existing is null)
            {
                return NotFound();
            }

            return BuildLite(existing);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            Order? existing = await context.Orders
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

        private OrderLite BuildLite(Order order)
        {
            List<OrderItemLite> items = [];
            items.AddRange(order.Items.Select(item => new OrderItemLite(new ProductLite(item.Product.Id, item.Product.Name, item.Product.UnitPrice), item.Quantity)));

            List<OrderStatusHistoryLite> history = [];
            history.AddRange(order.History.Select(x => new OrderStatusHistoryLite(x.CreatedAt, x.ChangedTo, x.ChangedFrom ?? OrderStatus.None)));
            CustomerLite customerLite = new(order.Customer.Id, order.Customer.Name, order.Customer.Email, order.Customer.IsActive);

            return new OrderLite(order.Id, order.Status, customerLite, items, history);
        }
    }

    public sealed record OrderLite(Guid Id, OrderStatus Status, CustomerLite Customer, List<OrderItemLite> Items, List<OrderStatusHistoryLite> StatusHistory);

    public sealed record OrderItemLite(ProductLite Product, int Quanitity);

    public sealed record OrderStatusHistoryLite(DateTime ChangedAt, OrderStatus ChangedTo, OrderStatus ChangedFrom);
}