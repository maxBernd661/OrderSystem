using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class CustomerDetailView : DetailView
    {
        public CustomerDetailView()
        {
            InitializeComponent();
        }

        [ActivatorUtilitiesConstructor]
        public CustomerDetailView(OrderContext context) : base(context)
        {
            InitializeComponent();
        }

        public override Task LoadData(Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public override Task SaveData()
        {
            throw new NotImplementedException();
        }
    }
}