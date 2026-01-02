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

        public event EventHandler<EventArgs>? Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(object entity)
        {
            throw new NotImplementedException();
        }

        public Customer GetData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(Customer entity)
        {
            LoadData(entity);
        }

        object IDataControl.GetData()
        {
            return GetData();
        }
    }
}