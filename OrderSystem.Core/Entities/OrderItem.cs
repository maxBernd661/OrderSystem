using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Bestellungsposition
    /// </summary>
    public class OrderItem : PersistentEntityBase
    {
        [Required]
        [HideInListView]
        public Guid OrderId { get; set; }

        [HideInListView]
        public Order? Order { get; set; }

        [Required]
        [HideInListView]
        public Guid ProductId { get; set; }

        [Identifier]
        [ColumnName("Item")]
        public string DisplayName
        {
            get
            {
                return $"{Quantity} x {(Product != null ? Product.Name : string.Empty)}";
            }
        }

        public Product? Product { get; set; }

        [ClampValue(1, 9999)]
        public int Quantity { get; set; }
    }

    public class OrderItemQueryProfile : IQueryProfile<OrderItem>
    {
        public IQueryable<OrderItem> Apply(IQueryable<OrderItem> query)
        {
            return query.Include(x => x.Product);
        }
    }
}