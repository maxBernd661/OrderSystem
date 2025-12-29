namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Bestellungsposition
    /// </summary>
    public class OrderItem : PersistentEntityBase
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

    public class OrderItemDTO : BaseDTO
    {
        public OrderDTO Order { get; set; }

        public ProductDTO Product { get; set; }

        public int Quantity { get; set; }
    }
}