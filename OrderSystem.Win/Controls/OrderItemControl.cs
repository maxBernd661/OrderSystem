using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class OrderItemControl : UserControl, IComplexDataControl<OrderItem>
    {
        public OrderItemControl()
        {
            InitializeComponent();
        }

        private Order? order;

        public event EventHandler<EventArgs>? Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(OrderItem? entity, IServiceProvider serviceProvider)
        {
            order = entity?.Order;

            if (entity != null)
            {
                numericUpDownQuantity.Value = entity.Quantity;
            }

            comboBoxProduct.Items.Clear();
            IQueryable<Product> products = serviceProvider.GetRequiredService<OrderContext>().Set<Product>().Where(x => x.IsAvailable);

            foreach (Product product in products)
            {
                comboBoxProduct.Items.Add(product);
            }
        }

        public void LoadData(object? entity, IServiceProvider serviceProvider)
        {
            LoadData((OrderItem)entity, serviceProvider);
        }

        public void LoadData(OrderItem? entity)
        {
            LoadData(entity, null);
        }

        public void LoadData(object? entity)
        {
            LoadData((OrderItem)entity, null);
        }

        public OrderItem GetData()
        {
            Product currentProduct = null;
            if (comboBoxProduct.SelectedItem is Product p)
            {
                currentProduct = p;
            }

            return new OrderItem()
            {
                Quantity = (int)numericUpDownQuantity.Value,
                Product = currentProduct,
                Order = order
            };
        }

        object IDataControl.GetData()
        {
            return GetData();
        }
    }
}