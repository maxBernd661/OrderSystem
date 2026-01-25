using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Forms;
using OrderSystem.Win.View;

namespace OrderSystem.Win.ViewControllers
{
    public class OrderListViewController(IServiceScopeFactory scopeFactory, ViewBase view) : ViewController<Order>(view)
    {
        private ToolStripButton? confirmButton;
        private ToolStripButton? shipButton;
        private ToolStripButton? cancelButton;

        private Order? selectedItem;

        protected override void ViewOnLoad(object? sender, EventArgs e)
        {
            if (View is not ListView<Order>)
            {
                return;
            }
            AddButtons();
        }

        protected override void ViewOnSelectionChanged(object? sender, SelectionChangedArgs<PersistentEntityBase> e)
        {
            if (e.Data is not Order order)
            {
                return;
            }

            selectedItem = order;

            switch (order.Status)
            {
                case OrderStatus.Draft:
                    ShowNone();
                    ShowConfirm();
                    ShowCancel();

                    break;

                case OrderStatus.Confirmed:
                    ShowNone();
                    ShowShip();
                    ShowCancel();
                    break;

                case OrderStatus.Shipped:
                case OrderStatus.Cancelled:
                case OrderStatus.None:
                default:
                    ShowNone();
                    break;
            }
        }

        private void AddButtons()
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            MainForm form = scope.ServiceProvider.GetRequiredService<MainForm>();

            confirmButton = new ToolStripButton()
            {
                Image = projectResources.confirm,
                Text = @"Confirm Order",
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            };
            confirmButton.Click += ConfirmButtonOnClick;
            confirmButton.Visible = false;
            confirmButton.Enabled = false;

            form.ToolStrip.Items.Add(confirmButton);

            shipButton = new ToolStripButton()
            {
                Image = projectResources.order,
                Text = @"Ship Order",
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            };
            shipButton.Click += ShipButtonOnClick;
            shipButton.Visible = false;
            shipButton.Enabled = false;

            form.ToolStrip.Items.Add(shipButton);

            cancelButton = new ToolStripButton()
            {
                Image = projectResources.cancel,
                Text = @"Cancel Order",
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            };
            cancelButton.Click += CancelButtonOnClick;
            cancelButton.Visible = false;
            cancelButton.Enabled = false;

            form.ToolStrip.Items.Add(cancelButton);
        }

        private void ShowNone()
        {
            if (confirmButton != null)
            {
                confirmButton.Visible = false;
                confirmButton.Enabled = false;
            }

            if (shipButton != null)
            {
                shipButton.Visible = false;
                shipButton.Enabled = false;
            }

            if (cancelButton != null)
            {
                cancelButton.Visible = false;
                cancelButton.Enabled = false;
            }
        }

        private void ShowConfirm()
        {
            if (confirmButton != null)
            {
                confirmButton.Enabled = true;
                confirmButton.Visible = true;
            }
        }

        private void ShowShip()
        {
            if (shipButton != null)
            {
                shipButton.Enabled = true;
                shipButton.Visible = true;
            }
        }

        private void ShowCancel()
        {
            if (cancelButton != null)
            {
                cancelButton.Enabled = true;
                cancelButton.Visible = true;
            }
        }

        private void CancelButtonOnClick(object? sender, EventArgs e)
        {
            if (selectedItem is null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Really cancel this order?",
                $"Cancel {selectedItem.DisplayName}", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }
        }

        private void ShipButtonOnClick(object? sender, EventArgs e)
        {
            if (selectedItem is null)
            {
                return;
            }
            DialogResult result = MessageBox.Show("A shipped order can no longer be cancelled.",
                $"Ship {selectedItem.DisplayName}", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
        }

        private void ConfirmButtonOnClick(object? sender, EventArgs e)
        {
            if (selectedItem is null)
            {
                return;
            }
            DialogResult result = MessageBox.Show("A confirmed order can no longer be changed.",
                $"Confirm {selectedItem.DisplayName}", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
        }

        public override void Dispose()
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            MainForm form = scope.ServiceProvider.GetRequiredService<MainForm>();

            if (confirmButton != null)
            {
                form.ToolStrip.Items.Remove(confirmButton);
                confirmButton.Click -= ConfirmButtonOnClick;
                confirmButton.Dispose();
            }

            if (shipButton != null)
            {
                form.ToolStrip.Items.Remove(shipButton);
                shipButton.Click -= ShipButtonOnClick;
                shipButton.Dispose();
            }

            if (cancelButton != null)
            {
                form.ToolStrip.Items.Remove(cancelButton);
                cancelButton.Click -= CancelButtonOnClick;
                cancelButton.Dispose();
            }

            base.Dispose();
        }
    }
}