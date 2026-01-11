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

        public OrderStatus Status { get; private set; }

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

            items.Add(item);

            return Result.Ok();
        }

        public Result DeleteItem(Guid itemId)
        {
            if (Status > OrderStatus.Draft)
            {
                return Result.Fail("Can only remove items from drafted orders");
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
}