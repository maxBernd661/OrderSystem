using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Kunde
    /// </summary>
    public class Customer : PersistentEntityBase
    {
        [Required]
        [ClampLength(5, 50)]
        [Identifier]
        public string Name { get; set; }

        [ClampLength(5, 50)]
        public string Email { get; set; }

        [ColumnName("Is Active")]
        public bool IsActive { get; set; }

        private readonly List<Order> orders = [];

        [HideInListView]
        public IReadOnlyCollection<Order> Orders
        {
            get { return orders; }
        }

        [ColumnName("Unshipped Orders")]
        public int OpenOrders
        {
            get { return Orders.Count(x => x.Status != OrderStatus.Shipped); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class CustomerQueryProfile : IQueryProfile<Customer>
    {
        public IQueryable<Customer> Apply(IQueryable<Customer> query)
        {
            //potentially too much
            return query.Include(x => x.Orders).AsSplitQuery();
        }
    }
}