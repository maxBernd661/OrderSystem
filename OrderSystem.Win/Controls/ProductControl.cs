using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class ProductControl : UserControl, IDataControl<Product>
    {
        public ProductControl()
        {
            InitializeComponent();
        }

        public void LoadData(object entity)
        {
            LoadData((Product)entity);
        }

        public Product GetData()
        {
            return new Product()
            {
                Name = textBoxName.Text,
                UnitPrice = numericUpDownPrice.Value,
                Weight = (float)numericUpDownWeight.Value,
                IsAvailable = checkBoxAvailable.Checked
            };
        }

        public void LoadData(Product entity)
        {
            textBoxName.Text = entity.Name;
            numericUpDownPrice.Value = entity.UnitPrice;
            numericUpDownWeight.Value = (decimal)entity.Weight;
            checkBoxAvailable.Checked = entity.IsAvailable;
        }

        object IDataControl.GetData()
        {
            return GetData();
        }
    }
}