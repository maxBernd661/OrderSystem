namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Bestellungsposition
    /// </summary>
    public class OrderItem : PersistentEntityBase
    {
        [HideInListView]
        public Guid OrderId { get; set; }

        [HideInListView]
        public Order Order { get; set; }

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

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}