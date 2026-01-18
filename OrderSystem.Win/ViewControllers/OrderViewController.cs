using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
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

        protected override void ViewOnLoad(object? sender, EventArgs e)
        {
            if (View is DetailView<Order>)
            {
                listView = View.GetControls().OfType<ListView<OrderItem>>().FirstOrDefault();
                if (listView is null)
                {
                    return;
                }

                listView.OnCustomOpenDetailView += ListViewOnOnCustomOpenDetailView;

                if (toolStrip is not null)
                {
                    return;
                }

                toolStrip = new ToolStrip();
                toolStrip.Dock = DockStyle.Top;
                toolStrip.GripStyle = ToolStripGripStyle.Hidden;
                toolStrip.Stretch = true;
                toolStrip.ImageScalingSize = new Size(16, 16);

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

                Order order = (Order)((IDetailView)View).ReadData();
                if (order.Status != OrderStatus.Draft)
                {
                    addItemButton.Enabled = false;
                    deleteItemButton.Enabled = false;
                }

                listView.AddControl(toolStrip);
            }
        }

        private async void ListViewOnOnCustomOpenDetailView(object? sender, CustomOpenEventArgs<OrderItem> e)
        {
            OrderItem toOpen = e.Data;
            await AddOrUpdate(toOpen);
        }

        private async void AddItemButtonOnClick(object? sender, EventArgs e)
        {
            if (View.Holder is null)
            {
                return;
            }

            await AddOrUpdate();
        }

        private async Task AddOrUpdate(OrderItem? item = null)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            ViewManager viewManager = scope.ServiceProvider.GetRequiredService<ViewManager>();
            PopupView popup = scope.ServiceProvider.GetRequiredService<PopupView>();
            ViewHolder holder = await viewManager.AddDetailView(item);

            if (popup.ShowView(holder) && popup.ReturnedItem is OrderItem orderItem)
            {
                Order order = (Order)((IDetailView)View).ReadData();
                Product? product = await scope.ServiceProvider.GetRequiredService<OrderContext>()
                                              .Set<Product>()
                                              .FirstOrDefaultAsync(x => x.Id == orderItem.ProductId);

                if (product == null)
                {
                    return;
                }

                orderItem.Product = product;

                if (item != null)
                {
                    order.DeleteItem(item.Id);
                }

                order.AddItem(orderItem);
                await ((IDetailView)View).LoadData(order, scope.ServiceProvider);
                SetViewChanged();
            }
        }

        private async void DeleteItemButtonOnClick(object? sender, EventArgs e)
        {
            if (listView?.HasData == false)
            {
                return;
            }

            Order order = (Order)((IDetailView)View).ReadData();
            OrderItem item = (OrderItem)listView!.GetSelectedItem()!;
            using IServiceScope scope = scopeFactory.CreateScope();
            order.DeleteItem(item.Id);
            await ((IDetailView)View).LoadData(order, scope.ServiceProvider);
            SetViewChanged();
        }

        public override void Dispose()
        {
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