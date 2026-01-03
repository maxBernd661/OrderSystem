using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Templates
{
    [DetailView(typeof(Customer))]
    public partial class CustomerDetailViewDummy : DetailViewDummy
    {
        public CustomerDetailViewDummy()
        {
            InitializeComponent();
        }

        public override Result Evaluate()
        {
            Customer currentData = (Customer)ReadData();
            return currentData.SoftValidate();
        }

        public override object ReadData()
        {
            Customer output = customerControl.GetData();
            PersistentEntityBase baseData = persistentEntityBaseControl1.GetData();

            output.Id = baseData.Id;
            output.CreatedAt = baseData.CreatedAt;
            output.UpdatedAt = baseData.UpdatedAt;
            output.IsDeleted = baseData.IsDeleted;

            return output;
        }
    }
}