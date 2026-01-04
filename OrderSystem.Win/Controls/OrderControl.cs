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

        private List<OrderItem> orderItems;
        private Order? savedOrder;

        public void LoadData(Order? entity, IServiceProvider serviceProvider)
        {
            if (entity != null)
            {
                savedOrder = entity;
                orderItems = entity.Items.ToList();
                textBoxStatus.Text = entity.Status.ToString();
            }
            else
            {
                savedOrder = null;
                orderItems = [];
                textBoxStatus.Text = nameof(OrderStatus.Draft);
            }

            comboBoxCustomer.Items.Clear();
            IQueryable<Customer> customers = serviceProvider.GetRequiredService<OrderContext>().Set<Customer>().Where(x => x.IsActive);

            foreach (Customer customer in customers)
            {
                comboBoxCustomer.Items.Add(customer);

                if (entity?.CustomerId == customer.Id)
                {
                    comboBoxCustomer.SelectedItem = customer;
                }
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
            if (savedOrder != null)
            {
                return savedOrder;
            }
            Customer? currentCustomer = new();
            if (comboBoxCustomer.SelectedItem is Customer c)
            {
                currentCustomer = c;
            }
            Order order = Order.Create(currentCustomer);
            foreach (OrderItem item in orderItems)
            {
                order.AddItem(item);
            }

            return order;
        }
    }
}