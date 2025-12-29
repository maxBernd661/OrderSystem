namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Kunde
    /// </summary>
    public class Customer : PersistentEntityBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [ColumnName("Is Active")]
        public bool IsActive { get; set; }

        private readonly List<Order> orders = [];

        public IReadOnlyCollection<Order> Orders
        {
            get { return orders; }
        }
    }

    public class CustomerDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public List<OrderDTO> Orders { get; set; } = [];

        public int OpenOrders { get; set; }
    }
}