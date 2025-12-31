using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class OrderStatusHistoryListView : ListView
    {
        public OrderStatusHistoryListView()
        {
            InitializeComponent();
            InitializeView<OrderStatusHistory>(dataGrid, bindingSource);
        }

        [ActivatorUtilitiesConstructor]
        public OrderStatusHistoryListView(OrderContext context) : base(context)
        {
            InitializeComponent();
            InitializeView<OrderStatusHistory>(dataGrid, bindingSource);
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