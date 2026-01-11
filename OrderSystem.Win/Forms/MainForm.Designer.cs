namespace OrderSystem.Win.Forms
{
    partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainToolStrip = new ToolStrip();
            buttonNewOrder = new ToolStripButton();
            buttonNewCustomer = new ToolStripButton();
            buttonNewProduct = new ToolStripButton();
            buttonNewGeneric = new ToolStripDropDownButton();
            productToolStripMenuItem = new ToolStripMenuItem();
            customerToolStripMenuItem = new ToolStripMenuItem();
            orderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonSave = new ToolStripSplitButton();
            saveAndNewToolStripMenuItem = new ToolStripMenuItem();
            saveAndExitToolStripMenuItem = new ToolStripMenuItem();
            seperatorSave = new ToolStripSeparator();
            buttonDelete = new ToolStripButton();
            seperatorDelete = new ToolStripSeparator();
            buttonCloseTab = new ToolStripSplitButton();
            allTabsToolStripMenuItem = new ToolStripMenuItem();
            otherTabsToolStripMenuItem = new ToolStripMenuItem();
            seperatorCloseTab = new ToolStripSeparator();
            buttonRefresh = new ToolStripButton();
            mainContainer = new SplitContainer();
            splitContainer1 = new SplitContainer();
            sidebarLayout = new TableLayoutPanel();
            sidebarButtonOrder = new OrderSystem.Win.Controls.SidebarButton();
            sidebarButtonCustomer = new OrderSystem.Win.Controls.SidebarButton();
            sidebarButtonProduct = new OrderSystem.Win.Controls.SidebarButton();
            mainTabControl = new TabControl();
            toolStrip2 = new ToolStrip();
            toggleSidebarButton = new ToolStripButton();
            labelStatus = new ToolStripLabel();
            mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainContainer).BeginInit();
            mainContainer.Panel1.SuspendLayout();
            mainContainer.Panel2.SuspendLayout();
            mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            sidebarLayout.SuspendLayout();
            toolStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // mainToolStrip
            // 
            mainToolStrip.GripMargin = new Padding(0);
            mainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            mainToolStrip.ImageScalingSize = new Size(32, 32);
            mainToolStrip.Items.AddRange(new ToolStripItem[] { buttonNewOrder, buttonNewCustomer, buttonNewProduct, buttonNewGeneric, toolStripSeparator1, buttonSave, seperatorSave, buttonDelete, seperatorDelete, buttonCloseTab, seperatorCloseTab, buttonRefresh });
            mainToolStrip.Location = new Point(0, 0);
            mainToolStrip.Margin = new Padding(5);
            mainToolStrip.MinimumSize = new Size(0, 54);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Padding = new Padding(5);
            mainToolStrip.Size = new Size(1235, 54);
            mainToolStrip.Stretch = true;
            mainToolStrip.TabIndex = 0;
            mainToolStrip.Text = "mainToolStrip";
            // 
            // buttonNewOrder
            // 
            buttonNewOrder.Image = projectResources.newItem;
            buttonNewOrder.ImageTransparentColor = Color.Magenta;
            buttonNewOrder.Margin = new Padding(2);
            buttonNewOrder.Name = "buttonNewOrder";
            buttonNewOrder.Padding = new Padding(2);
            buttonNewOrder.Size = new Size(113, 40);
            buttonNewOrder.Text = "New Order";
            buttonNewOrder.Click += orderToolStripMenuItem_Click;
            // 
            // buttonNewCustomer
            // 
            buttonNewCustomer.Image = projectResources.newItem;
            buttonNewCustomer.ImageTransparentColor = Color.Magenta;
            buttonNewCustomer.Margin = new Padding(2);
            buttonNewCustomer.Name = "buttonNewCustomer";
            buttonNewCustomer.Padding = new Padding(2);
            buttonNewCustomer.Size = new Size(134, 40);
            buttonNewCustomer.Text = "New Customer";
            buttonNewCustomer.Click += customerToolStripMenuItem_Click;
            // 
            // buttonNewProduct
            // 
            buttonNewProduct.Image = projectResources.newItem;
            buttonNewProduct.ImageTransparentColor = Color.Magenta;
            buttonNewProduct.Margin = new Padding(2);
            buttonNewProduct.Name = "buttonNewProduct";
            buttonNewProduct.Padding = new Padding(2);
            buttonNewProduct.Size = new Size(123, 40);
            buttonNewProduct.Text = "New Product";
            buttonNewProduct.Click += productToolStripMenuItem_Click;
            // 
            // buttonNewGeneric
            // 
            buttonNewGeneric.DropDownItems.AddRange(new ToolStripItem[] { productToolStripMenuItem, customerToolStripMenuItem, orderToolStripMenuItem });
            buttonNewGeneric.Image = projectResources.newItem;
            buttonNewGeneric.ImageTransparentColor = Color.Magenta;
            buttonNewGeneric.Margin = new Padding(2);
            buttonNewGeneric.Name = "buttonNewGeneric";
            buttonNewGeneric.Padding = new Padding(2);
            buttonNewGeneric.Size = new Size(83, 40);
            buttonNewGeneric.Text = "New";
            // 
            // productToolStripMenuItem
            // 
            productToolStripMenuItem.Name = "productToolStripMenuItem";
            productToolStripMenuItem.Size = new Size(132, 22);
            productToolStripMenuItem.Text = "Product";
            productToolStripMenuItem.Click += productToolStripMenuItem_Click;
            // 
            // customerToolStripMenuItem
            // 
            customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            customerToolStripMenuItem.Size = new Size(132, 22);
            customerToolStripMenuItem.Text = "Customer";
            customerToolStripMenuItem.Click += customerToolStripMenuItem_Click;
            // 
            // orderToolStripMenuItem
            // 
            orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            orderToolStripMenuItem.Size = new Size(132, 22);
            orderToolStripMenuItem.Text = "Order";
            orderToolStripMenuItem.Click += orderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(2);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Padding = new Padding(2);
            toolStripSeparator1.Size = new Size(6, 40);
            // 
            // buttonSave
            // 
            buttonSave.DropDownItems.AddRange(new ToolStripItem[] { saveAndNewToolStripMenuItem, saveAndExitToolStripMenuItem });
            buttonSave.Image = projectResources.save;
            buttonSave.ImageTransparentColor = Color.Magenta;
            buttonSave.Margin = new Padding(2);
            buttonSave.Name = "buttonSave";
            buttonSave.Padding = new Padding(2);
            buttonSave.Size = new Size(87, 40);
            buttonSave.Text = "Save";
            buttonSave.ButtonClick += buttonSave_ButtonClick;
            // 
            // saveAndNewToolStripMenuItem
            // 
            saveAndNewToolStripMenuItem.Name = "saveAndNewToolStripMenuItem";
            saveAndNewToolStripMenuItem.Size = new Size(165, 22);
            saveAndNewToolStripMenuItem.Text = "Save and New";
            saveAndNewToolStripMenuItem.Click += saveAndNewToolStripMenuItem_Click;
            // 
            // saveAndExitToolStripMenuItem
            // 
            saveAndExitToolStripMenuItem.Name = "saveAndExitToolStripMenuItem";
            saveAndExitToolStripMenuItem.Size = new Size(165, 22);
            saveAndExitToolStripMenuItem.Text = "Save and Close";
            saveAndExitToolStripMenuItem.Click += saveAndExitToolStripMenuItem_Click;
            // 
            // seperatorSave
            // 
            seperatorSave.Margin = new Padding(2);
            seperatorSave.Name = "seperatorSave";
            seperatorSave.Padding = new Padding(2);
            seperatorSave.Size = new Size(6, 40);
            // 
            // buttonDelete
            // 
            buttonDelete.Image = projectResources.delete;
            buttonDelete.ImageTransparentColor = Color.Magenta;
            buttonDelete.Margin = new Padding(2);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Padding = new Padding(2);
            buttonDelete.Size = new Size(85, 40);
            buttonDelete.Text = "Delete";
            buttonDelete.Click += buttonDelete_Click;
            // 
            // seperatorDelete
            // 
            seperatorDelete.Margin = new Padding(2);
            seperatorDelete.Name = "seperatorDelete";
            seperatorDelete.Padding = new Padding(2);
            seperatorDelete.Size = new Size(6, 40);
            // 
            // buttonCloseTab
            // 
            buttonCloseTab.Alignment = ToolStripItemAlignment.Right;
            buttonCloseTab.DropDownItems.AddRange(new ToolStripItem[] { allTabsToolStripMenuItem, otherTabsToolStripMenuItem });
            buttonCloseTab.Image = projectResources.close;
            buttonCloseTab.ImageTransparentColor = Color.Magenta;
            buttonCloseTab.Margin = new Padding(2);
            buttonCloseTab.Name = "buttonCloseTab";
            buttonCloseTab.Padding = new Padding(2);
            buttonCloseTab.RightToLeft = RightToLeft.Yes;
            buttonCloseTab.Size = new Size(117, 40);
            buttonCloseTab.Text = "Close Tab";
            buttonCloseTab.ButtonClick += buttonCloseTab_Click;
            // 
            // allTabsToolStripMenuItem
            // 
            allTabsToolStripMenuItem.Name = "allTabsToolStripMenuItem";
            allTabsToolStripMenuItem.Size = new Size(140, 22);
            allTabsToolStripMenuItem.Text = "All Tabs";
            allTabsToolStripMenuItem.Click += allTabsToolStripMenuItem_Click;
            // 
            // otherTabsToolStripMenuItem
            // 
            otherTabsToolStripMenuItem.Name = "otherTabsToolStripMenuItem";
            otherTabsToolStripMenuItem.Size = new Size(140, 22);
            otherTabsToolStripMenuItem.Text = "Other Tabs";
            otherTabsToolStripMenuItem.Click += otherTabsToolStripMenuItem_Click;
            // 
            // seperatorCloseTab
            // 
            seperatorCloseTab.Alignment = ToolStripItemAlignment.Right;
            seperatorCloseTab.Margin = new Padding(2);
            seperatorCloseTab.Name = "seperatorCloseTab";
            seperatorCloseTab.Padding = new Padding(2);
            seperatorCloseTab.Size = new Size(6, 40);
            // 
            // buttonRefresh
            // 
            buttonRefresh.Alignment = ToolStripItemAlignment.Right;
            buttonRefresh.Image = projectResources.refresh;
            buttonRefresh.ImageTransparentColor = Color.Magenta;
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.RightToLeft = RightToLeft.Yes;
            buttonRefresh.Size = new Size(88, 41);
            buttonRefresh.Text = "Refresh";
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // mainContainer
            // 
            mainContainer.BackColor = Color.FromArgb(224, 224, 224);
            mainContainer.BorderStyle = BorderStyle.Fixed3D;
            mainContainer.Dock = DockStyle.Fill;
            mainContainer.Location = new Point(0, 54);
            mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            mainContainer.Panel1.BackColor = Color.White;
            mainContainer.Panel1.Controls.Add(splitContainer1);
            mainContainer.Panel1MinSize = 70;
            // 
            // mainContainer.Panel2
            // 
            mainContainer.Panel2.BackColor = Color.White;
            mainContainer.Panel2.Controls.Add(mainTabControl);
            mainContainer.Panel2.Controls.Add(toolStrip2);
            mainContainer.Panel2MinSize = 900;
            mainContainer.Size = new Size(1235, 645);
            mainContainer.SplitterDistance = 241;
            mainContainer.SplitterWidth = 5;
            mainContainer.TabIndex = 3;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(sidebarLayout);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Enabled = false;
            splitContainer1.Size = new Size(237, 641);
            splitContainer1.SplitterDistance = 172;
            splitContainer1.TabIndex = 0;
            // 
            // sidebarLayout
            // 
            sidebarLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            sidebarLayout.ColumnCount = 1;
            sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sidebarLayout.Controls.Add(sidebarButtonOrder, 0, 2);
            sidebarLayout.Controls.Add(sidebarButtonCustomer, 0, 1);
            sidebarLayout.Controls.Add(sidebarButtonProduct, 0, 0);
            sidebarLayout.Dock = DockStyle.Fill;
            sidebarLayout.Location = new Point(0, 0);
            sidebarLayout.Name = "sidebarLayout";
            sidebarLayout.RowCount = 3;
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            sidebarLayout.Size = new Size(237, 172);
            sidebarLayout.TabIndex = 0;
            // 
            // sidebarButtonOrder
            // 
            sidebarButtonOrder.AutoSize = true;
            sidebarButtonOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sidebarButtonOrder.DisplayText = "Orders";
            sidebarButtonOrder.Dock = DockStyle.Fill;
            sidebarButtonOrder.HighlightColor = Color.LightBlue;
            sidebarButtonOrder.Image = projectResources.order;
            sidebarButtonOrder.Location = new Point(6, 118);
            sidebarButtonOrder.Margin = new Padding(4);
            sidebarButtonOrder.MinimumSize = new Size(160, 32);
            sidebarButtonOrder.Name = "sidebarButtonOrder";
            sidebarButtonOrder.NormalColor = Color.White;
            sidebarButtonOrder.Size = new Size(225, 48);
            sidebarButtonOrder.TabIndex = 7;
            sidebarButtonOrder.ButtonClicked += sidebarButtonOrder_Click;
            // 
            // sidebarButtonCustomer
            // 
            sidebarButtonCustomer.AutoSize = true;
            sidebarButtonCustomer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sidebarButtonCustomer.DisplayText = "Customers";
            sidebarButtonCustomer.Dock = DockStyle.Fill;
            sidebarButtonCustomer.HighlightColor = Color.LightBlue;
            sidebarButtonCustomer.Image = projectResources.customer;
            sidebarButtonCustomer.Location = new Point(6, 62);
            sidebarButtonCustomer.Margin = new Padding(4);
            sidebarButtonCustomer.MinimumSize = new Size(160, 32);
            sidebarButtonCustomer.Name = "sidebarButtonCustomer";
            sidebarButtonCustomer.NormalColor = Color.White;
            sidebarButtonCustomer.Size = new Size(225, 46);
            sidebarButtonCustomer.TabIndex = 6;
            sidebarButtonCustomer.ButtonClicked += sidebarButtonCustomer_Click;
            // 
            // sidebarButtonProduct
            // 
            sidebarButtonProduct.AutoSize = true;
            sidebarButtonProduct.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sidebarButtonProduct.DisplayText = "Products";
            sidebarButtonProduct.Dock = DockStyle.Fill;
            sidebarButtonProduct.HighlightColor = Color.LightBlue;
            sidebarButtonProduct.Image = projectResources.product;
            sidebarButtonProduct.Location = new Point(6, 6);
            sidebarButtonProduct.Margin = new Padding(4);
            sidebarButtonProduct.MinimumSize = new Size(160, 32);
            sidebarButtonProduct.Name = "sidebarButtonProduct";
            sidebarButtonProduct.NormalColor = Color.White;
            sidebarButtonProduct.Size = new Size(225, 46);
            sidebarButtonProduct.TabIndex = 0;
            sidebarButtonProduct.ButtonClicked += sidebarButtonProduct_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(985, 613);
            mainTabControl.TabIndex = 1;
            mainTabControl.SelectedIndexChanged += mainTabControl_SelectedIndexChanged;
            // 
            // toolStrip2
            // 
            toolStrip2.Dock = DockStyle.Bottom;
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.Items.AddRange(new ToolStripItem[] { toggleSidebarButton, labelStatus });
            toolStrip2.Location = new Point(0, 613);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(985, 28);
            toolStrip2.TabIndex = 0;
            toolStrip2.Text = "toolStrip2";
            // 
            // toggleSidebarButton
            // 
            toggleSidebarButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleSidebarButton.Image = projectResources.left;
            toggleSidebarButton.ImageTransparentColor = Color.Magenta;
            toggleSidebarButton.Margin = new Padding(2);
            toggleSidebarButton.Name = "toggleSidebarButton";
            toggleSidebarButton.Padding = new Padding(2);
            toggleSidebarButton.Size = new Size(24, 24);
            toggleSidebarButton.Text = "toolStripButton2";
            toggleSidebarButton.Click += toggleSidebarButton_Click;
            // 
            // labelStatus
            // 
            labelStatus.Alignment = ToolStripItemAlignment.Right;
            labelStatus.Margin = new Padding(2);
            labelStatus.Name = "labelStatus";
            labelStatus.Padding = new Padding(2);
            labelStatus.Size = new Size(4, 24);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1235, 699);
            Controls.Add(mainContainer);
            Controls.Add(mainToolStrip);
            DoubleBuffered = true;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OrderSystem";
            mainToolStrip.ResumeLayout(false);
            mainToolStrip.PerformLayout();
            mainContainer.Panel1.ResumeLayout(false);
            mainContainer.Panel2.ResumeLayout(false);
            mainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainContainer).EndInit();
            mainContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            sidebarLayout.ResumeLayout(false);
            sidebarLayout.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip mainToolStrip;
        public SplitContainer mainContainer;
        private SplitContainer splitContainer1;
        public TabControl mainTabControl;
        private ToolStrip toolStrip2;
        private ToolStripButton toggleSidebarButton;
        private TableLayoutPanel sidebarLayout;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator seperatorCloseTab;
        private ToolStripSplitButton buttonCloseTab;
        private ToolStripMenuItem allTabsToolStripMenuItem;
        private ToolStripMenuItem otherTabsToolStripMenuItem;
        private ToolStripSplitButton buttonSave;
        private ToolStripMenuItem saveAndNewToolStripMenuItem;
        private ToolStripMenuItem saveAndExitToolStripMenuItem;
        private ToolStripSeparator seperatorSave;
        private ToolStripButton buttonDelete;
        private ToolStripSeparator seperatorDelete;
        private ToolStripLabel labelStatus;
        private TabPage tabPage1;
        private Controls.SidebarButton sidebarButtonProduct;
        private Controls.SidebarButton sidebarButtonOrder;
        private Controls.SidebarButton sidebarButtonCustomer;
        private TableLayoutPanel panelOrder;
        private Label labelOrders;
        private PictureBox pictureOrders;
        private TableLayoutPanel panelCustomer;
        private Label labelCustomer;
        private PictureBox pictureCustomer;
        private ToolStripDropDownButton buttonNewGeneric;
        private ToolStripMenuItem productToolStripMenuItem;
        private ToolStripMenuItem customerToolStripMenuItem;
        private ToolStripMenuItem orderToolStripMenuItem;
        private ToolStripButton buttonNewProduct;
        private ToolStripButton buttonNewCustomer;
        private ToolStripButton buttonNewOrder;
        private ToolStripButton buttonRefresh;
    }
}