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

        object IDataControl.GetData()
        {
            return GetData();
        }

        public event EventHandler<EventArgs>? Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(Order? entity, IServiceProvider serviceProvider)
        {
            Customer? currentCustomer = null;

            if (entity != null)
            {
                textBoxStatus.Text = entity.Status.ToString();
                currentCustomer = entity.Customer;
            }
            comboBoxCustomer.Items.Clear();
            IQueryable<Customer> customers = serviceProvider.GetRequiredService<OrderContext>().Set<Customer>().Where(x => x.IsActive);
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

        public void LoadData(Order? entity)
        {
            LoadData(entity, null);
        }

        public void LoadData(object entity)
        {
            LoadData((Order)entity, null);
        }

        public void LoadData(object entity, IServiceProvider serviceProvider)
        {
            LoadData((Order)entity, serviceProvider);
        }

        public Order GetData()
        {
            Customer? currentCustomer = new();
            if (comboBoxCustomer.SelectedItem is Customer c)
            {
                currentCustomer = c;
            }
            Order order = Order.Create(currentCustomer);
            return order;
        }
    }
}