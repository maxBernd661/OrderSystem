using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Services;
using OrderSystem.Win.View;
using System.Reflection;

namespace OrderSystem.Win.Forms
{
    public partial class MainForm : Form
    {
        private readonly ViewFactory factory;
        private readonly IServiceProvider serviceProvider;

        private readonly List<OpenView> openViews = [];

        public MainForm(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            factory = serviceProvider.GetRequiredService<ViewFactory>();
            InitializeComponent();

            ShowDelete(false);
            ShowSave(false);
            ShowClose(false);

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

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDetailView<Product>();
        }

        private void navProducts_Click(object sender, EventArgs e)
        {
            AddListView<Product>();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
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
            AddListView<Order>();
        }

        #endregion Views

        #region Buttons

        private void allTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ViewHolder view in mainTabControl.TabPages.OfType<ViewHolder>())
            {
                RemoveView(view);
            }
        }

        private void otherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage? selected = mainTabControl.SelectedTab;

            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                if (mainTabControl.TabPages[i] != selected && mainTabControl.TabPages[i] is ViewHolder holder)
                {
                    RemoveView(holder);
                }
            }
        }

        private void buttonCloseTab_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is ViewHolder holder)
            {
                RemoveView(holder);
            }
        }

        private void buttonSave_ButtonClick(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is ViewHolder { View: IDetailView dv})
            {
                Result evalResult = dv.Template.Evaluate();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
        }

        private void saveAndNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion Buttons

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleButtonVisibility();
        }

        public void AddDetailView<TEntity>(Guid? id = null) where TEntity : PersistentEntityBase
        {
            DetailView<TEntity> view = factory.CreateDetailView<TEntity>();

            TEntity? existingItem = null;
            string viewName = $"New {typeof(TEntity).Name}";
            if (id != null)
            {
                existingItem = serviceProvider.GetRequiredService<OrderContext>()
                                                      .Set<TEntity>()
                                                      .AsNoTracking()
                                                      .Single(x => x.Id == id);

                PropertyInfo? identProp = serviceProvider.GetRequiredService<ViewFactory>().GetIdentifier(typeof(TEntity));
                if (identProp != null)
                {
                    object? identvalue = identProp.GetValue(existingItem);
                    viewName = identvalue is string s ? s : string.Empty;
                }

                view.Template.LoadData(existingItem);
            }

            ViewHolder holder = new(viewName, view, this);
            AddView<TEntity>(holder, ViewKind.DetailView, existingItem);
        }

        private void AddListView<TEntity>() where TEntity : PersistentEntityBase
        {
            ListView<TEntity> listview = serviceProvider.GetRequiredService<ListView<TEntity>>();
            ViewHolder holder = new($"All {typeof(TEntity).Name}s", listview, this);
            AddView<TEntity>(holder, ViewKind.ListView);
        }

        private void ToggleButtonVisibility()
        {
            if (mainTabControl.TabPages.Count == 0)
            {
                ShowSave(false);
                ShowDelete(false);
                ShowClose(false);
                return;
            }

            if (mainTabControl.SelectedTab is ViewHolder holder)
            {
                ShowSave(holder.View.Kind == ViewKind.DetailView);
                ShowDelete(holder.View.Kind == ViewKind.DetailView);
            }

            ShowClose(true);
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

        private void ShowDelete(bool visible)
        {
            buttonDelete.Visible = visible;
            seperatorDelete.Visible = visible;
        }

        private void AddView<TEntity>(ViewHolder holder, ViewKind kind, PersistentEntityBase? loadedEntity = null)
        {
            //dont open the same unfiltered listview twice
            if (kind == ViewKind.ListView)
            {
                OpenView? openListView = openViews.FirstOrDefault(x => x.EntityType == typeof(TEntity));
                if (openListView != null)
                {
                    mainTabControl.SelectedTab = openListView.Holder;
                    return;
                }
            }

            //if this object is already showing in a detailview, dont show again
            if (loadedEntity != null)
            {
                OpenView? existingView = openViews.FirstOrDefault(x => x.Kind == ViewKind.DetailView && x.Entity?.Id == loadedEntity.Id);
                if (existingView != null)
                {
                    mainTabControl.SelectedTab = existingView.Holder;
                    return;
                }
            }

            openViews.Add(new OpenView(holder, kind, typeof(TEntity), loadedEntity));
            mainTabControl.TabPages.Add(holder);
            mainTabControl.SelectedTab = holder;
            ToggleButtonVisibility();
        }

        //properly dispose view
        private void RemoveView(ViewHolder holder)
        {
            OpenView? existingView = openViews.FirstOrDefault(x => x.Holder == holder);
            if (existingView != null)
            {
                mainTabControl.TabPages.Remove(holder);
                openViews.Remove(existingView);
                existingView.Holder.Dispose();
            }

            ToggleButtonVisibility();
        }
    }

    public record OpenView(ViewHolder Holder, ViewKind Kind, Type EntityType, PersistentEntityBase? Entity);

    public enum ViewKind
    {
        DetailView,
        ListView
    }
}