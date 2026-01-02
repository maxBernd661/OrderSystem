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
            return base.Evaluate();
        }
    }
}