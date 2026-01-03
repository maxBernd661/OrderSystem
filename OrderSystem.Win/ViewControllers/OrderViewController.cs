using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.ViewControllers
{
    public class OrderViewController(ViewBase view) : ViewController<Order>(view)
    {
        private ToolStrip? toolStrip;
        private IListView? listView;
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

                listView.Grid.SelectionChanged += GridOnSelectionChanged;

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
                    toolStrip.Items.Add(addItemButton);

                    deleteItemButton = new ToolStripButton()
                    {
                        Image = projectResources.delete,
                        Text = @"Delete Item",
                        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    };
                    toolStrip.Items.Add(deleteItemButton);

                    toolStrip.GripStyle = ToolStripGripStyle.Hidden;
                    toolStrip.Stretch = true;
                    toolStrip.ImageScalingSize = new Size(16, 16);

                    listView.AddControl(toolStrip);
                }
            }
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

            addItemButton?.Dispose();
            deleteItemButton?.Dispose();

            base.Dispose();
        }
    }
}