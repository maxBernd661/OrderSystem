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
            return base.ReadData();
        }

        public override Result Evaluate()
        {
            return base.Evaluate();
        }
    }
}