namespace OrderSystem.Domain.Entities
{
    /// <summary>
    /// Kunde
    /// </summary>
    public class Customer : PersistentEntityBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        private readonly List<Order> orders = [];

        public IReadOnlyCollection<Order> Orders
        {
            get { return orders; }
        }
    }
}