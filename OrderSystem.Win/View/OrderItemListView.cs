using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class OrderItemListView : ListView
    {
        public OrderItemListView()
        {
            InitializeComponent();
            InitializeView<OrderItem>(dataGrid, bindingSource);
        }

        [ActivatorUtilitiesConstructor]
        public OrderItemListView(OrderContext context) : base(context)
        {
            InitializeComponent();
            InitializeView<OrderItem>(dataGrid, bindingSource);
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