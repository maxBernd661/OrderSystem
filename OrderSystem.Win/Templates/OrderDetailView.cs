using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Templates
{
    [DetailView(typeof(Order))]
    public partial class OrderDetailView : DetailViewDummy
    {
        public OrderDetailView()
        {
            InitializeComponent();
        }

        public override object ReadData()
        {
            Order output = orderControl1.GetData();
            PersistentEntityBase baseData = persistentEntityBaseControl1.GetData();

            output.Id = baseData.Id;
            output.CreatedAt = baseData.CreatedAt;
            output.UpdatedAt = baseData.UpdatedAt;
            output.IsDeleted = baseData.IsDeleted;

            return output;
        }

        public override Result Evaluate()
        {
            Order currentData = (Order)ReadData();
            return currentData.SoftValidate();
        }
    }
}