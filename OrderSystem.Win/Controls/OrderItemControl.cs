using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class OrderItemControl : UserControl, IDataControl<OrderItem>, IRequireLookup<ProductLookups>
    {
        public OrderItemControl()
        {
            InitializeComponent();
        }

        private Order? order;
        private List<ProductLookup> products = [];

        public event EventHandler<EventArgs>? Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void SetLookup(ProductLookups lookup)
        {
            products = lookup.Lookups;
            comboBoxProduct.BeginUpdate();
            try
            {
                comboBoxProduct.Items.Clear();
                foreach (ProductLookup product in products)
                {
                    comboBoxProduct.Items.Add(product);
                }

                comboBoxProduct.DisplayMember = nameof(ProductLookup.Name);
            }
            finally
            {
                comboBoxProduct.EndUpdate();
            }
        }

        public void LoadData(OrderItem? entity)
        {
            order = entity?.Order;

            if (entity == null)
            {
                return;
            }

            ProductLookup? selectedProduct = products.FirstOrDefault(x => x.Id == entity.ProductId);
            if (selectedProduct != null)
            {
                comboBoxProduct.SelectedItem = selectedProduct;
            }
            numericUpDownQuantity.Value = entity.Quantity;
        }

        public void LoadData(object? entity)
        {
            LoadData((OrderItem)entity);
        }

        public OrderItem GetData()
        {
            Guid id = Guid.Empty;
            if (comboBoxProduct.SelectedItem is ProductLookup p)
            {
                id = p.Id;
            }

            return new OrderItem()
            {
                Quantity = (int)numericUpDownQuantity.Value,
                ProductId = id,
                Order = order
            };
        }

        object IDataControl.GetData()
        {
            return GetData();
        }
    }
}