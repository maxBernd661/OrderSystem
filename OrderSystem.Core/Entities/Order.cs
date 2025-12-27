namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Bestellung
    /// </summary>
    public class Order : PersistentEntityBase
    {
        //für EF
        private Order()
        {
        }

        #region Properties

        public Guid CustomerId { get; private set; }

        public Customer Customer { get; private set; } = null!;

        private readonly List<OrderItem> items = [];

        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }

        private readonly List<OrderStatusHistory> history = [];

        public IReadOnlyCollection<OrderStatusHistory> History
        {
            get { return history; }
        }

        public OrderStatus Status { get; private set; }

        #endregion Properties

        public static Order Create(Customer customer)
        {
            Order order = new()
            {
                Status = OrderStatus.Draft,
                Customer = customer,
                CustomerId = customer.Id
            };

            order.history.Add(OrderStatusHistory.Created(order));
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

        public Result AddItem(Product product, int quantity)
        {
            if (Status > OrderStatus.Draft)
            {
                return Result.Fail("Can only add items to drafted orders.");
            }

            items.Add(new OrderItem
            {
                Product = product,
                ProductId = product.Id,
                Quantity = quantity
            });

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
}