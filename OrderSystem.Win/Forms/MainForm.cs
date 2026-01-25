using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Services;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Forms
{
    public partial class MainForm : Form
    {
        private readonly ViewFactory factory;
        private readonly ViewManager viewManager;

        private List<ToolStripItem> ownItems;

        public MainForm(IServiceProvider serviceProvider)
        {
            factory = serviceProvider.GetRequiredService<ViewFactory>();
            viewManager = serviceProvider.GetRequiredService<ViewManager>();
            InitializeComponent();

            ownItems =
            [
                buttonNewOrder,
                buttonNewCustomer,
                buttonNewProduct,
                buttonNewGeneric,
                buttonSave,
                buttonDelete,
                buttonCloseTab,
                buttonRefresh,
                seperatorSave,
                seperatorCloseTab,
                seperatorDelete,
                saveAndExitToolStripMenuItem,
                saveAndNewToolStripMenuItem
            ];

            ToggleButtons();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        #region Views

        private async void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHolder view = await viewManager.AddDetailView<Product>();
            ShowView(view);
        }

        private void sidebarButtonProduct_Click(object sender, EventArgs e)
        {
            ViewHolder view = viewManager.AddListView<Product>();
            ShowView(view);
        }

        private async void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHolder view = await viewManager.AddDetailView<Customer>();
            ShowView(view);
        }

        private void sidebarButtonCustomer_Click(object sender, EventArgs e)
        {
            ViewHolder view = viewManager.AddListView<Customer>();
            ShowView(view);
        }

        private async void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHolder view = await viewManager.AddDetailView<Order>();
            ShowView(view);
        }

        private void sidebarButtonOrder_Click(object sender, EventArgs e)
        {
            ViewHolder view = viewManager.AddListView<Order>();
            ShowView(view);
        }

        public void ShowView(ViewHolder holder)
        {
            TabPageHost? host = FindHost(holder);
            if (host is null)
            {
                host = new TabPageHost(holder.Name, holder);
                mainTabControl.TabPages.Add(host);
            }

            mainTabControl.SelectedTab = host;

            ToggleButtons();
        }

        private void CloseView(ViewHolder holder)
        {
            TabPageHost? host = FindHost(holder);
            if (host is null)
            {
                return;
            }

            if (viewManager.DisposeView(holder))
            {
                mainTabControl.TabPages.Remove(host);
                host.Dispose();
            }

            ToggleButtons();
        }

        #endregion Views

        #region Buttons

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

        private void allTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ViewHolder view in mainTabControl.TabPages.OfType<ViewHolder>())
            {
                CloseView(view);
            }
        }

        private void otherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage? selected = mainTabControl.SelectedTab;

            foreach (TabPageHost host in mainTabControl.TabPages.OfType<TabPageHost>().ToList())
            {
                if (!ReferenceEquals(host, selected))
                    CloseView(host.Holder);
            }
        }

        private void buttonCloseTab_Click(object sender, EventArgs e)
        {
            ViewHolder? holder = GetCurrentHolder();
            if (holder is not null)
            {
                CloseView(holder);
            }
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            ViewHolder? holder = GetCurrentHolder();
            if (holder is not null)
            {
                await viewManager.ReloadView(holder);
            }
        }

        private async void buttonSave_ButtonClick(object sender, EventArgs e)
        {
            await TrySave();
        }

        private async void saveAndNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Result saveSuccess = await TrySave();
            if (saveSuccess.IsSuccess)
            {
                ViewHolder? holder = GetCurrentHolder();
                if (holder is not null)
                {
                    CloseView(holder);
                    MethodInfo? addMethod = typeof(ViewManager).GetMethod(nameof(ViewManager.AddAndShowDetailView))?
                                                              .MakeGenericMethod(holder.View.EntityType);

                    if (addMethod != null)
                    {
                        addMethod.Invoke(viewManager, new object?[1]);
                    }
                }
            }
        }

        private async void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Result saveSuccess = await TrySave();
            if (saveSuccess.IsSuccess)
            {
                ViewHolder? holder = GetCurrentHolder();
                if (holder is not null)
                {
                    CloseView(holder);
                }
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            ViewHolder? holder = GetCurrentHolder();
            if (holder is not null)
            {
                if (MessageBox.Show(@$"Really delete this {holder.View.EntityType.Name}", @"Delete Entity", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Result deleteResult = await viewManager.DeleteAsync(holder);
                if (deleteResult.IsSuccess)
                {
                    if (holder.View.Kind == ViewKind.DetailView)
                    {
                        CloseView(holder);
                    }
                    else
                    {
                        await viewManager.ReloadView(holder);
                    }
                }
            }
        }

        private async Task<Result> TrySave()
        {
            ViewHolder? holder = GetCurrentHolder();
            if (holder is { View: IDetailView dv })
            {
                Result evalResult = dv.Template.Evaluate();
                if (!evalResult.IsSuccess)
                {
                    labelStatus.Text = @$"Could not Save Object: {evalResult.Error}";
                    return evalResult;
                }

                await viewManager.SaveAsync(holder);
                return Result.Ok();
            }

            return Result.Fail(string.Empty);
        }

        #endregion Buttons

        #region Toolstrip

        public ToolStrip ToolStrip
        {
            get { return mainToolStrip; }
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleButtons();
        }

        private void ShowNewType(Type type)
        {
            buttonNewGeneric.Visible = type == typeof(PersistentEntityBase);
            buttonNewCustomer.Visible = type == typeof(Customer);
            buttonNewOrder.Visible = type == typeof(Order);
            buttonNewProduct.Visible = type == typeof(Product);
        }

        private void ShowSave(bool visible, bool enabled)
        {
            seperatorSave.Visible = visible;
            buttonSave.Enabled = enabled;
            buttonSave.Visible = visible;
        }

        private void ShowDelete(bool visible, bool enabled)
        {
            seperatorDelete.Visible = visible;
            buttonDelete.Enabled = enabled;
            buttonDelete.Visible = visible;
        }

        private void ShowClose(bool visible)
        {
            seperatorCloseTab.Visible = visible;
            buttonCloseTab.Visible = visible;
        }

        public void ToggleButtons()
        {
            ResetToolStrip();

            labelStatus.Text = string.Empty;

            if (mainTabControl.TabPages.Count == 0)
            {
                ShowNewType(typeof(PersistentEntityBase));
                ShowClose(false);
                ShowDelete(false, false);
                ShowSave(false, false);
                buttonRefresh.Visible = false;
                return;
            }

            ViewHolder? holder = GetCurrentHolder();
            if (holder is null)
            {
                return;
            }

            buttonRefresh.Visible = true;
            ShowClose(true);
            ShowNewType(holder.View.EntityType);

            if (holder.View is IListView lv)
            {
                ShowSave(false, false);
                ShowDelete(true, lv.HasData);
            }
            else
            {
                ShowSave(true, holder.ViewIsChanged);
                ShowDelete(true, true);
            }
        }

        private void ResetToolStrip()
        {
            foreach (ToolStripItem control in ownItems)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }

        #endregion Toolstrip

        private ViewHolder? GetCurrentHolder()
        {
            return (mainTabControl.SelectedTab as TabPageHost)?.Holder;
        }

        private TabPageHost? FindHost(ViewHolder holder)
        {
            return mainTabControl.TabPages.OfType<TabPageHost>().FirstOrDefault(x => ReferenceEquals(x.Holder, holder));
        }
    }

    public enum ViewKind
    {
        DetailView,
        ListView
    }
}