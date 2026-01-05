using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class OrderControl : UserControl, IComplexDataControl<Order>, IRequireLookup<CustomerLookups>
    {
        public OrderControl()
        {
            InitializeComponent();
        }

        private List<CustomerLookup> customers = [];

        public void SetLookup(CustomerLookups lookup)
        {
            customers = lookup.Lookups;
            comboBoxCustomer.BeginUpdate();
            try
            {
                comboBoxCustomer.Items.Clear();
                foreach (CustomerLookup customer in customers)
                {
                    comboBoxCustomer.Items.Add(customer);
                }

                comboBoxCustomer.DisplayMember = nameof(CustomerLookup.Name);
            }
            finally
            {
                comboBoxCustomer.EndUpdate();
            }
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

                CustomerLookup? selectedCustomer = customers.FirstOrDefault(x => x.Id == entity.CustomerId);
                if (selectedCustomer != null)
                {
                    comboBoxCustomer.SelectedItem = selectedCustomer;
                }
            }
            else
            {
                savedOrder = null;
                orderItems = [];
                textBoxStatus.Text = nameof(OrderStatus.Draft);
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
                order.AddItem(item.Product, item.Quantity);
            }

            return order;
        }
    }
}