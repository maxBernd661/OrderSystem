using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class OrderControl : UserControl, IComplexDataControl<Order>
    {
        public OrderControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public object GetData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(Order? entity, IServiceProvider serviceProvider)
        {
            Customer? currentCustomer = null;

            if (entity != null)
            {
                textBoxStatus.Text = entity.Status.ToString();
                currentCustomer = entity.Customer;
            }

            DbSet<Customer> customers = serviceProvider.GetRequiredService<OrderContext>().Set<Customer>();
            currentCustomer ??= customers.FirstOrDefault();

            foreach (Customer customer in customers)
            {
                comboBoxCustomer.Items.Add(customer);
            }

            if (currentCustomer != null)
            {
                comboBoxCustomer.SelectedItem = currentCustomer;
            }
        }

        public void LoadData(object entity)
        {
            LoadData((Order)entity, null);
        }

        public void LoadData(object entity, IServiceProvider serviceProvider)
        {
            LoadData((Order)entity, serviceProvider);
        }
    }
}