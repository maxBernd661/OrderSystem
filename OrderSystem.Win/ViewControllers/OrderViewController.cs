using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.Services;
using OrderSystem.Win.View;

namespace OrderSystem.Win.ViewControllers
{
    public class OrderViewController(IServiceScopeFactory scopeFactory, ViewBase view) : ViewController<Order>(view)
    {
        private ToolStrip? toolStrip;
        private ListView<OrderItem>? listView;
        private ToolStripButton? addItemButton;
        private ToolStripButton? deleteItemButton;
        private readonly IServiceScopeFactory scopeFactory = scopeFactory;

        protected override void ViewOnLoad(object? sender, EventArgs e)
        {
            if (View is DetailView<Order>)
            {
                listView = View.GetControls().OfType<ListView<OrderItem>>().FirstOrDefault();
                if (listView is null)
                {
                    return;
                }

                listView.Grid.SelectionChanged += GridOnSelectionChanged;
                listView.OnCustomOpenDetailView += ListViewOnOnCustomOpenDetailView;

                if (toolStrip is null)
                {
                    toolStrip = new ToolStrip();

                    toolStrip.Dock = DockStyle.Top;

                    addItemButton = new ToolStripButton()
                    {
                        Image = projectResources.newItem,
                        Text = @"Add Item",
                        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                    };
                    addItemButton.Click += AddItemButtonOnClick;

                    toolStrip.Items.Add(addItemButton);

                    deleteItemButton = new ToolStripButton()
                    {
                        Image = projectResources.delete,
                        Text = @"Delete Item",
                        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    };
                    deleteItemButton.Click += DeleteItemButtonOnClick;
                    toolStrip.Items.Add(deleteItemButton);

                    toolStrip.GripStyle = ToolStripGripStyle.Hidden;
                    toolStrip.Stretch = true;
                    toolStrip.ImageScalingSize = new Size(16, 16);

                    listView.AddControl(toolStrip);
                }
            }
        }

        private void ListViewOnOnCustomOpenDetailView(object? sender, CustomOpenEventArgs e)
        {
        }

        private async void AddItemButtonOnClick(object? sender, EventArgs e)
        {
            if (View.Holder is null)
            {
                return;
            }

            using IServiceScope scope = scopeFactory.CreateScope();
            ViewManager viewManager = scope.ServiceProvider.GetRequiredService<ViewManager>();
            PopupView popup = scope.ServiceProvider.GetRequiredService<PopupView>();
            ViewHolder holder = viewManager.AddDetailView<OrderItem>();

            if (popup.ShowView(holder) && popup.ReturnedItem is OrderItem orderItem)
            {
                Order order = (Order)((IDetailView)View).ReadData();
                order.AddItem(orderItem);
                ((IDetailView)View).LoadData(order, scope.ServiceProvider);
            }
        }

        private void DeleteItemButtonOnClick(object? sender, EventArgs e)
        {
        }

        private void GridOnSelectionChanged(object? sender, EventArgs e)
        {
        }

        public override void Dispose()
        {
            if (listView != null)
            {
                listView.Grid.SelectionChanged -= GridOnSelectionChanged;
            }

            if (addItemButton != null)
            {
                addItemButton.Click -= AddItemButtonOnClick;
                addItemButton.Dispose();
            }

            if (deleteItemButton != null)
            {
                deleteItemButton.Click -= DeleteItemButtonOnClick;
                deleteItemButton.Dispose();
            }

            base.Dispose();
        }
    }
}