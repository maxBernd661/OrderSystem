using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Win.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OrderSystem.Win.Forms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider serviceProvider;

        public MainForm(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InitializeComponent();

            buttonCloseTab.Visible = false;
            seperatorCloseTab.Visible = false;
        }

        #region Panels

        private void navProducts_MouseEnter(object sender, EventArgs e)
        {
            panelProduct.BackColor = Color.LightBlue;
        }

        private void navProducts_MouseLeave(object sender, EventArgs e)
        {
            panelProduct.BackColor = Color.White;
        }

        private void navOrder_MouseEnter(object sender, EventArgs e)
        {
            panelOrder.BackColor = Color.LightBlue;
        }

        private void navOrder_MouseLeave(object sender, EventArgs e)
        {
            panelOrder.BackColor = Color.White;
        }

        private void navCustomer_MouseEnter(object sender, EventArgs e)
        {
            panelCustomer.BackColor = Color.LightBlue;
        }

        private void navCustomer_MouseLeave(object sender, EventArgs e)
        {
            panelCustomer.BackColor = Color.White;
        }

        #endregion Panels

        private void toggleSidebarButton_Click(object sender, EventArgs e)
        {
            if (!mainContainer.Panel1Collapsed)
            {
                toggleSidebarButton.Image = resources.right;
                mainContainer.Panel1Collapsed = true;
            }
            else
            {
                toggleSidebarButton.Image = resources.left;
                mainContainer.Panel1Collapsed = false;
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void navProducts_Click(object sender, EventArgs e)
        {
            ProductListView view = serviceProvider.GetRequiredService<ProductListView>();
            TabPage page = new("Products");
            page.Controls.Add(view);
            view.Dock = DockStyle.Fill;

            mainTabControl.TabPages.Add(page);

            ToggleCloseVisibility();
        }

        private void ToggleCloseVisibility()
        {
            if (mainTabControl.TabPages.Count > 0)
            {
                buttonCloseTab.Visible = true;
                seperatorCloseTab.Visible = true;
            }
            else
            {
                buttonCloseTab.Visible = false;
                seperatorCloseTab.Visible = false;
            }
        }

        private void navCustomers_Click(object sender, EventArgs e)
        {
        }

        private void navOrders_Click(object sender, EventArgs e)
        {
        }

        private void allTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in mainTabControl.TabPages)
            {
                mainTabControl.TabPages.Remove(tab);
            }
        }

        private void otherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage? selected = mainTabControl.SelectedTab;

            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                if (mainTabControl.TabPages[i] != selected)
                { mainTabControl.TabPages.Remove(mainTabControl.TabPages[i]); }
            }
        }

        private void buttonCloseTab_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is { } tab)
            {
                mainTabControl.TabPages.Remove(tab);
            }
        }
    }
}