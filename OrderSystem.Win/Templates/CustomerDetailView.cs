using OrderSystem.Core.Entities;
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
    }
}