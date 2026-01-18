using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Bestellung
    /// </summary>
    public class Order : PersistentEntityBase
    {
        public Order()
        {
            Status = OrderStatus.Draft;
            history.Add(OrderStatusHistory.Created(this));
        }

        #region Properties

        [HideInListView]
        [Required]
        public Guid CustomerId { get; private set; }

        [HideInListView]
        public Customer Customer { get; private set; } = null!;

        [ColumnName("Displayname")]
        [Identifier]
        public string DisplayName
        {
            get { return $"{(Customer != null ? Customer.Name : "Unknown Customer")} : {Status}, {CreatedAt:D}"; }
        }

        private readonly List<OrderItem> items = [];

        [HideInListView]
        [AtLeastOne]
        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }

        private readonly List<OrderStatusHistory> history = [];

        [HideInListView]
        public IReadOnlyCollection<OrderStatusHistory> History
        {
            get { return history; }
        }

        public OrderStatus Status { get; set; }

        #endregion Properties

        public static Order Create(Guid customerId)
        {
            Order order = new()
            {
                Status = OrderStatus.Draft,
                CustomerId = customerId
            };

            return order;
        }

        public Result Confirm()
        {
            switch (Status)
            {
                case OrderStatus.Confirmed:
                    return Result.Fail("Order has already been confirmed");

                case OrderStatus.Shipped:
                    return Result.Fail("Order has already been shipped");

                case OrderStatus.Cancelled:
                    return Result.Fail("Order has already been cancelled");
            }

            history.Add(OrderStatusHistory.Changed(this, Status, OrderStatus.Confirmed));
            Status = OrderStatus.Confirmed;

            return Result.Ok();
        }

        public Result Ship()
        {
            switch (Status)
            {
                case OrderStatus.Cancelled:
                    return Result.Fail("Cannot ship cancelled order.");

                case OrderStatus.Shipped:
                    return Result.Fail("Order has already been shipped.");

                case OrderStatus.Draft:
                    return Result.Fail("Order has to be confirmed before shipping.");
            }

            history.Add(OrderStatusHistory.Changed(this, Status, OrderStatus.Shipped));
            Status = OrderStatus.Shipped;
            return Result.Ok();
        }

        public Result Cancel()
        {
            if (Status is OrderStatus.Cancelled)
            {
                return Result.Fail("Order is already cancelled.");
            }

            if (Status is OrderStatus.Shipped)
            {
                return Result.Fail("Shipped orders cannot be cancelled.");
            }

            history.Add(OrderStatusHistory.Changed(this, Status, OrderStatus.Cancelled));
            Status = OrderStatus.Cancelled;

            return Result.Ok();
        }

        public Result AddItem(OrderItem item)
        {
            if (Status > OrderStatus.Draft)
            {
                return Result.Fail("Can only add items to drafted orders.");
            }

            OrderItem? existingItem = items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem is null)
            {
                items.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }

            return Result.Ok();
        }

        public Result DeleteItem(Guid itemId)
        {
            if (Status > OrderStatus.Draft)
            {
                return Result.Fail("Can only remove items from drafted orders");
            }

            if (Items.Count == 1)
            {
                return Result.Fail("Cannot remove the last item from order");
            }

            if (items.FirstOrDefault(x => x.Id == itemId) is { } item)
            {
                items.Remove(item);
            }

            return Result.Ok();
        }

        public Result SetCustomer(Customer customer)
        {
            if (Status > OrderStatus.Draft)
            {
                return Result.Fail("Can only change customer while order is drafted");
            }

            Customer = customer;
            CustomerId = customer.Id;

            return Result.Ok();
        }
    }

    public enum OrderStatus
    {
        None,
        Draft,
        Confirmed,
        Shipped,
        Cancelled
    }

    public sealed record Result(bool IsSuccess, string? Error)
    {
        public static Result Ok() => new(true, null);
        public static Result Fail(string error) => new(false, error);
    }

    public sealed class OrderQueryProfile : IQueryProfile<Order>
    {
        public IQueryable<Order> Apply(IQueryable<Order> query)
        {
            return query.Include(x => x.Customer).Include(x => x.Items).ThenInclude(x => x.Product).AsSplitQuery();
        }
    }

    public sealed class OrderGraphMerger : IGraphMerger<Order>
    {
        public async Task<Result> Merge(OrderContext context, Order tracked, Order incoming, CancellationToken ct = default)
        {
            if (tracked.CustomerId != incoming.CustomerId)
            {
                Customer? customer = await context.Set<Customer>().FindAsync([incoming.CustomerId], ct);
                if (customer is null)
                {
                    return Result.Fail($"Customer {incoming.CustomerId} not found.");
                }

                Result result = tracked.SetCustomer(customer);
                if (!result.IsSuccess)
                {
                    return result;
                }
            }

            List<OrderItem> incomingItems = incoming.Items.ToList();
            List<OrderItem> trackedItems = tracked.Items.ToList();

            List<OrderItem> toRemove = trackedItems.Where(x => incomingItems.All(y => y.Id != x.Id)).ToList();

            foreach (OrderItem rem in toRemove)
            {
                Result result = tracked.DeleteItem(rem.Id);
                if (!result.IsSuccess)
                {
                    return result;
                }

                context.Remove(rem);
            }

            foreach (OrderItem item in incomingItems)
            {
                //item is new
                if (item.CreatedAt == DateTime.MinValue &&
                    item.UpdatedAt == DateTime.MinValue)
                {
                    item.OrderId = tracked.Id;
                    item.Order = tracked;

                    Product? product = context.Set<Product>().Local.FirstOrDefault(p => p.Id == item.ProductId) ??
                                    await context.Set<Product>().FindAsync([item.ProductId], ct);

                    if (product is null)
                    {
                        return Result.Fail($"Product {item.ProductId} not found.");
                    }

                    Result addResult = tracked.AddItem(item);
                    if (!addResult.IsSuccess)
                    {
                        return addResult;
                    }

                    context.Entry(item).State = EntityState.Added;
                    continue;
                }

                OrderItem? toUpdate = trackedItems.FirstOrDefault(x => x.Id == item.Id);
                if (toUpdate == null)
                {
                    continue;
                }

                context.Entry(toUpdate).CurrentValues.SetValues(item);

                if (toUpdate.ProductId != item.ProductId)
                {
                    Product? product = context.Set<Product>().Local.FirstOrDefault(p => p.Id == item.ProductId) ??
                                       await context.Set<Product>().FindAsync([item.ProductId], ct);

                    toUpdate.Product = product;
                    toUpdate.ProductId = item.ProductId;
                }
            }
            return Result.Ok();
        }
    }
}