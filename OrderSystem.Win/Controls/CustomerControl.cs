using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class CustomerControl : UserControl, IDataControl<Customer>
    {
        public CustomerControl()
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

        public void LoadData(object? entity)
        {
            LoadData((Customer)entity!);
        }

        public Customer GetData()
        {
            return new Customer()
            {
                Name = textBoxName.Text,
                Email = textBoxMail.Text,
                IsActive = checkBoxActive.Checked
            };
        }

        public void LoadData(Customer? entity)
        {
            if (entity != null)
            {
                textBoxName.Text = entity.Name;
                textBoxMail.Text = entity.Email;
                checkBoxActive.Checked = entity.IsActive;
            }
        }
    }
}