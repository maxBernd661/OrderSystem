using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
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
            return currentData.SoftValidate();
        }

        public override object ReadData()
        {
            Product output = productControl1.GetData();
            PersistentEntityBase baseData = persistentEntityBaseControl1.GetData();
            
            output.Id = baseData.Id;
            output.CreatedAt = baseData.CreatedAt;
            output.UpdatedAt = baseData.UpdatedAt;
            output.IsDeleted = baseData.IsDeleted;

            return output;
        }
    }
}