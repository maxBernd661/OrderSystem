using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Forms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IReadOnlyList<IViewDescriptor> viewDescriptors;

        public MainForm(IServiceProvider serviceProvider, IEnumerable<IViewDescriptor> viewDescriptors)
        {
            this.serviceProvider = serviceProvider;
            this.viewDescriptors = viewDescriptors.ToList();
            InitializeComponent();

            buttonCloseTab.Visible = false;
            seperatorCloseTab.Visible = false;

            buttonSave.Visible = false;
            seperatorSave.Visible = false;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            UpdateStyles();
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

        private void toggleSidebarButton_Click(object sender, EventArgs e)
        {
            if (!mainContainer.Panel1Collapsed)
            {
                toggleSidebarButton.Image = projectResources.right;
                mainContainer.Panel1Collapsed = true;
            }
            else
            {
                toggleSidebarButton.Image = projectResources.left;
                mainContainer.Panel1Collapsed = false;
            }
        }

        #endregion Panels

        #region Views

        private async void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AddDetailView<Product>();
        }

        private void navProducts_Click(object sender, EventArgs e)
        {
            AddListView<Product>();
        }

        private async void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDetailView<Customer>();
        }

        private void navCustomers_Click(object sender, EventArgs e)
        {
            AddListView<Customer>();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void navOrders_Click(object sender, EventArgs e)
        {
        }

        #endregion Views

        private void ToggleButtonVisibility()
        {
            if (mainTabControl.SelectedTab is ViewHolder holder)
            {
                ShowSave(holder.View.Kind == ViewKind.DetailView);
            }

            ShowClose(mainTabControl.TabPages.Count > 0);
        }

        private void allTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in mainTabControl.TabPages)
            {
                mainTabControl.TabPages.Remove(tab);
            }

            ToggleButtonVisibility();
        }

        private void otherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage? selected = mainTabControl.SelectedTab;

            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                if (mainTabControl.TabPages[i] != selected)
                { mainTabControl.TabPages.Remove(mainTabControl.TabPages[i]); }
            }

            ToggleButtonVisibility();
        }

        private void buttonCloseTab_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is { } tab)
            {
                mainTabControl.TabPages.Remove(tab);
            }

            ToggleButtonVisibility();
        }

        private void buttonSave_ButtonClick(object sender, EventArgs e)
        {
        }

        private void saveAndNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleButtonVisibility();
        }

        private async Task AddView<TView>() where TView : ViewBase
        {
            ViewBase view = (ViewBase)serviceProvider.GetRequiredService(typeof(TView));
            IViewDescriptor descriptor = viewDescriptors.Single(x => x.ViewType == typeof(TView));

            ViewHolder holder = new(descriptor.Title, view);

            mainTabControl.TabPages.Add(holder);

            ToggleButtonVisibility();
        }

        private void AddDetailView<TEntity>(Guid? id = null) where TEntity : PersistentEntityBase
        {
        }

        private void AddListView<TEntity>() where TEntity : PersistentEntityBase
        {
            ListView<TEntity> listview = serviceProvider.GetRequiredService<ListView<TEntity>>();
            ViewHolder holder = new($"All {typeof(TEntity).Name}s", listview);
            mainTabControl.TabPages.Add(holder);
            ToggleButtonVisibility();
        }

        private void ShowSave(bool visible)
        {
            buttonSave.Visible = visible;
            seperatorSave.Visible = visible;
        }

        private void ShowClose(bool visible)
        {
            buttonCloseTab.Visible = visible;
            seperatorCloseTab.Visible = visible;
        }
    }

    public interface IView
    {
        public ViewHolder Holder { get; }

        public void SetHolder(ViewHolder holder);

        public ViewKind Kind { get; }

        public Task LoadData(Guid? id = null);

        public Task SaveData();
    }
}