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

        public MainForm(IServiceProvider serviceProvider)
        {
            factory = serviceProvider.GetRequiredService<ViewFactory>();
            viewManager = serviceProvider.GetRequiredService<ViewManager>();
            InitializeComponent();

            ToggleButtons();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        #region Views

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewManager.AddDetailView<Product>();
        }

        private void sidebarButtonProduct_Click(object sender, EventArgs e)
        {
            viewManager.AddListView<Product>();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewManager.AddDetailView<Customer>();
        }

        private void sidebarButtonCustomer_Click(object sender, EventArgs e)
        {
            viewManager.AddListView<Customer>();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewManager.AddDetailView<Order>();
        }

        private void sidebarButtonOrder_Click(object sender, EventArgs e)
        {
            viewManager.AddListView<Order>();
        }

        public void ShowView(ViewHolder holder)
        {
            if (!mainTabControl.TabPages.Contains(holder))
            {
                mainTabControl.TabPages.Add(holder);
            }

            mainTabControl.SelectedTab = holder;

            ToggleButtons();
        }

        private void CloseView(ViewHolder holder)
        {
            if (mainTabControl.TabPages.Contains(holder) && viewManager.DisposeView(holder))
            {
                mainTabControl.TabPages.Remove(holder);
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

            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                if (mainTabControl.TabPages[i] != selected && mainTabControl.TabPages[i] is ViewHolder holder)
                {
                    CloseView(holder);
                }
            }
        }

        private void buttonCloseTab_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is ViewHolder holder)
            {
                CloseView(holder);
            }
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is ViewHolder holder)
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
                if (mainTabControl.SelectedTab is ViewHolder holder)
                {
                    CloseView(holder);
                    MethodInfo? addMethod = typeof(ViewManager).GetMethod(nameof(ViewManager.AddDetailView))?
                                                              .MakeGenericMethod(holder.View.EntityType);

                    if (addMethod != null)
                    {
                        addMethod.Invoke(viewManager, [null]);
                    }
                }
            }
        }

        private async void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Result saveSuccess = await TrySave();
            if (saveSuccess.IsSuccess)
            {
                if (mainTabControl.SelectedTab is ViewHolder holder)
                {
                    CloseView(holder);
                }
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab is ViewHolder holder)
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
            if (mainTabControl.SelectedTab is ViewHolder { View: IDetailView dv } holder)
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

            if (mainTabControl.SelectedTab is not ViewHolder holder)
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
            foreach (ToolStripItem control in mainToolStrip.Items)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }

        #endregion Toolstrip
    }

    public enum ViewKind
    {
        DetailView,
        ListView
    }
}