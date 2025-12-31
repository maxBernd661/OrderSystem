using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class OrderListView : ListView
    {
        public OrderListView()
        {
            InitializeComponent();
            InitializeView<Order>(dataGrid, bindingSource);
        }

        [ActivatorUtilitiesConstructor]
        public OrderListView(OrderContext context) : base(context)
        {
            InitializeComponent();
            InitializeView<Order>(dataGrid, bindingSource);
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