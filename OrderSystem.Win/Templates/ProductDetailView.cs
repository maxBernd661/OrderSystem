using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Templates
{
    [DetailView(typeof(Product))]
    public partial class ProductDetailViewDummy : DetailViewDummy
    {
        public ProductDetailViewDummy()
        {
            InitializeComponent();
        }

        public override Result Evaluate()
        {
            Product currentData = (Product)ReadData();
            return base.Evaluate();
        }

        public override object ReadData()
        {
            return productControl1.GetData();
        }
    }
}