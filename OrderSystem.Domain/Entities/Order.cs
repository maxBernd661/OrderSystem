namespace OrderSystem.Domain.Entities
{
    /// <summary>
    /// Bestellung
    /// </summary>
    public class Order
    {
        public Guid CustomerId { get; private set; }

        public Customer? Customer { get; private set; } = null!;

        private readonly List<OrderItem> items = [];

        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }
    }

    public enum OrderStatus
    {
        Draft,
        Submitted,
        Confirmed,
        Shipped,
        Cancelled
    }
}