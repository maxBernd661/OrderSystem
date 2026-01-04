using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Templates
{
    [DetailView(typeof(OrderItem))]
    public partial class OrderItemDetailView : DetailViewDummy
    {
        public OrderItemDetailView()
        {
            InitializeComponent();
        }

        public override object ReadData()
        {
            OrderItem output = orderItemControl1.GetData();
            PersistentEntityBase baseData = persistentEntityBaseControl1.GetData();

            output.Id = baseData.Id;
            output.CreatedAt = baseData.CreatedAt;
            output.UpdatedAt = baseData.UpdatedAt;
            output.IsDeleted = baseData.IsDeleted;

            return output;
        }

        public override Result Evaluate()
        {
            OrderItem currentData = (OrderItem)ReadData();
            return currentData.SoftValidate();
        }
    }
}