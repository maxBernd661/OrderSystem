using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class OrderControl : UserControl, IDataControl<Order>, IRequireLookup<CustomerLookups>
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

        public void LoadData(Order? entity)
        {
            if (entity != null)
            {
                textBoxStatus.Text = entity.Status.ToString();

                CustomerLookup? selectedCustomer = customers.FirstOrDefault(x => x.Id == entity.CustomerId);
                if (selectedCustomer != null)
                {
                    comboBoxCustomer.SelectedItem = selectedCustomer;
                }
            }
            else
            {
                textBoxStatus.Text = nameof(OrderStatus.Draft);
            }
        }

        public void LoadData(object entity)
        {
            LoadData((Order)entity);
        }

        public Order GetData()
        {
            Guid id = Guid.Empty;
            if (comboBoxCustomer.SelectedItem is CustomerLookup c)
            {
                id = c.Id;
            }

            Order order = Order.Create(id);
            return order;
        }
    }
}