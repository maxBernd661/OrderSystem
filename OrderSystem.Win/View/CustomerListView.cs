using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class CustomerListView : ListView
    {
        public CustomerListView()
        {
            InitializeComponent();
            InitializeView<Customer>(dataGrid, bindingSource);
        }

        [ActivatorUtilitiesConstructor]
        public CustomerListView(OrderContext context) : base(context)
        {
            InitializeComponent();
            InitializeView<Customer>(dataGrid, bindingSource);
        }

        public override async Task LoadData(Guid? id)
        {
            bindingSource.Clear();
            List<Customer> customers = await context.Customers
                                                    .AsNoTracking()
                                                    .Where(x => !x.IsDeleted)
                                                    .Include(x => x.Orders)
                                                    .ToListAsync();

            foreach (Customer customer in customers)
            {
                CustomerDTO dto = new()
                {
                    Id = customer.Id,
                    CreatedAt = customer.CreatedAt,
                    UpdatedAt = customer.UpdatedAt,
                    Name = customer.Name,
                    Email = customer.Email,
                    IsActive = customer.IsActive,
                    OpenOrders = customer.Orders.Count(x => x.Status != OrderStatus.Shipped)
                };
                bindingSource.Add(dto);
            }
        }

        public override Task SaveData()
        {
            throw new NotImplementedException();
        }
    }
}