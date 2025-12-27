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
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            productToolStripMenuItem = new ToolStripMenuItem();
            customerToolStripMenuItem = new ToolStripMenuItem();
            orderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonCloseTab = new ToolStripSplitButton();
            allTabsToolStripMenuItem = new ToolStripMenuItem();
            otherTabsToolStripMenuItem = new ToolStripMenuItem();
            seperatorCloseTab = new ToolStripSeparator();
            buttonSave = new ToolStripSplitButton();
            saveAndNewToolStripMenuItem = new ToolStripMenuItem();
            saveAndExitToolStripMenuItem = new ToolStripMenuItem();
            seperatorSave = new ToolStripSeparator();
            mainContainer = new SplitContainer();
            splitContainer1 = new SplitContainer();
            sidebarLayout = new TableLayoutPanel();
            panelOrder = new TableLayoutPanel();
            labelOrders = new Label();
            pictureOrders = new PictureBox();
            panelProduct = new TableLayoutPanel();
            labelProduct = new Label();
            pictureProduct = new PictureBox();
            panelCustomer = new TableLayoutPanel();
            labelCustomer = new Label();
            pictureCustomer = new PictureBox();
            mainTabControl = new TabControl();
            toolStrip2 = new ToolStrip();
            toggleSidebarButton = new ToolStripButton();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainContainer).BeginInit();
            mainContainer.Panel1.SuspendLayout();
            mainContainer.Panel2.SuspendLayout();
            mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            sidebarLayout.SuspendLayout();
            panelOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureOrders).BeginInit();
            panelProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureProduct).BeginInit();
            panelCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureCustomer).BeginInit();
            toolStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripMargin = new Padding(0);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(32, 32);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator1, buttonCloseTab, seperatorCloseTab, buttonSave, seperatorSave });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(5, 5, 0, 5);
            toolStrip1.Size = new Size(1235, 66);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "mainToolStrip";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { productToolStripMenuItem, customerToolStripMenuItem, orderToolStripMenuItem });
            toolStripDropDownButton1.Image = resources.newItem;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(47, 53);
            toolStripDropDownButton1.Text = "New";
            toolStripDropDownButton1.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // productToolStripMenuItem
            // 
            productToolStripMenuItem.Name = "productToolStripMenuItem";
            productToolStripMenuItem.Size = new Size(180, 22);
            productToolStripMenuItem.Text = "Product";
            productToolStripMenuItem.Click += productToolStripMenuItem_Click;
            // 
            // customerToolStripMenuItem
            // 
            customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            customerToolStripMenuItem.Size = new Size(180, 22);
            customerToolStripMenuItem.Text = "Customer";
            customerToolStripMenuItem.Click += customerToolStripMenuItem_Click;
            // 
            // orderToolStripMenuItem
            // 
            orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            orderToolStripMenuItem.Size = new Size(180, 22);
            orderToolStripMenuItem.Text = "Order";
            orderToolStripMenuItem.Click += orderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 56);
            // 
            // buttonCloseTab
            // 
            buttonCloseTab.DropDownItems.AddRange(new ToolStripItem[] { allTabsToolStripMenuItem, otherTabsToolStripMenuItem });
            buttonCloseTab.Image = resources.close;
            buttonCloseTab.ImageTransparentColor = Color.Magenta;
            buttonCloseTab.Name = "buttonCloseTab";
            buttonCloseTab.Size = new Size(81, 53);
            buttonCloseTab.Text = "Close Tab";
            buttonCloseTab.TextImageRelation = TextImageRelation.ImageAboveText;
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
            seperatorCloseTab.Name = "seperatorCloseTab";
            seperatorCloseTab.Size = new Size(6, 56);
            // 
            // buttonSave
            // 
            buttonSave.DropDownItems.AddRange(new ToolStripItem[] { saveAndNewToolStripMenuItem, saveAndExitToolStripMenuItem });
            buttonSave.Image = resources.save;
            buttonSave.ImageTransparentColor = Color.Magenta;
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(51, 53);
            buttonSave.Text = "Save";
            buttonSave.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonSave.ButtonClick += buttonSave_ButtonClick;
            // 
            // saveAndNewToolStripMenuItem
            // 
            saveAndNewToolStripMenuItem.Name = "saveAndNewToolStripMenuItem";
            saveAndNewToolStripMenuItem.Size = new Size(159, 22);
            saveAndNewToolStripMenuItem.Text = "Save and New";
            saveAndNewToolStripMenuItem.Click += saveAndNewToolStripMenuItem_Click;
            // 
            // saveAndExitToolStripMenuItem
            // 
            saveAndExitToolStripMenuItem.Name = "saveAndExitToolStripMenuItem";
            saveAndExitToolStripMenuItem.Size = new Size(159, 22);
            saveAndExitToolStripMenuItem.Text = "Save and Exit";
            saveAndExitToolStripMenuItem.Click += saveAndExitToolStripMenuItem_Click;
            // 
            // seperatorSave
            // 
            seperatorSave.Name = "seperatorSave";
            seperatorSave.Size = new Size(6, 56);
            // 
            // mainContainer
            // 
            mainContainer.BackColor = Color.FromArgb(224, 224, 224);
            mainContainer.BorderStyle = BorderStyle.Fixed3D;
            mainContainer.Dock = DockStyle.Fill;
            mainContainer.Location = new Point(0, 66);
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
            mainContainer.Size = new Size(1235, 633);
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
            splitContainer1.Size = new Size(237, 629);
            splitContainer1.SplitterDistance = 172;
            splitContainer1.TabIndex = 0;
            // 
            // sidebarLayout
            // 
            sidebarLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            sidebarLayout.ColumnCount = 1;
            sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sidebarLayout.Controls.Add(panelOrder, 0, 2);
            sidebarLayout.Controls.Add(panelProduct, 0, 0);
            sidebarLayout.Controls.Add(panelCustomer, 0, 1);
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
            // panelOrder
            // 
            panelOrder.ColumnCount = 2;
            panelOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2491455F));
            panelOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7508545F));
            panelOrder.Controls.Add(labelOrders, 1, 0);
            panelOrder.Controls.Add(pictureOrders, 0, 0);
            panelOrder.Dock = DockStyle.Fill;
            panelOrder.Location = new Point(5, 117);
            panelOrder.Name = "panelOrder";
            panelOrder.RowCount = 1;
            panelOrder.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelOrder.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            panelOrder.Size = new Size(227, 50);
            panelOrder.TabIndex = 5;
            panelOrder.Click += navOrders_Click;
            panelOrder.MouseEnter += navOrder_MouseEnter;
            panelOrder.MouseLeave += navOrder_MouseLeave;
            // 
            // labelOrders
            // 
            labelOrders.AutoSize = true;
            labelOrders.Dock = DockStyle.Fill;
            labelOrders.Location = new Point(92, 0);
            labelOrders.Name = "labelOrders";
            labelOrders.Size = new Size(132, 50);
            labelOrders.TabIndex = 1;
            labelOrders.Text = "Orders";
            labelOrders.TextAlign = ContentAlignment.MiddleCenter;
            labelOrders.Click += navOrders_Click;
            labelOrders.MouseEnter += navOrder_MouseEnter;
            labelOrders.MouseLeave += navOrder_MouseLeave;
            // 
            // pictureOrders
            // 
            pictureOrders.Dock = DockStyle.Fill;
            pictureOrders.Image = resources.order;
            pictureOrders.Location = new Point(3, 3);
            pictureOrders.Name = "pictureOrders";
            pictureOrders.Size = new Size(83, 44);
            pictureOrders.SizeMode = PictureBoxSizeMode.Zoom;
            pictureOrders.TabIndex = 0;
            pictureOrders.TabStop = false;
            pictureOrders.Click += navOrders_Click;
            pictureOrders.MouseEnter += navOrder_MouseEnter;
            pictureOrders.MouseLeave += navOrder_MouseLeave;
            // 
            // panelProduct
            // 
            panelProduct.BackgroundImageLayout = ImageLayout.None;
            panelProduct.ColumnCount = 2;
            panelProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2491455F));
            panelProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7508545F));
            panelProduct.Controls.Add(labelProduct, 1, 0);
            panelProduct.Controls.Add(pictureProduct, 0, 0);
            panelProduct.Dock = DockStyle.Fill;
            panelProduct.Location = new Point(5, 5);
            panelProduct.Name = "panelProduct";
            panelProduct.RowCount = 1;
            panelProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelProduct.Size = new Size(227, 48);
            panelProduct.TabIndex = 4;
            panelProduct.Click += navProducts_Click;
            panelProduct.MouseEnter += navProducts_MouseEnter;
            panelProduct.MouseLeave += navProducts_MouseLeave;
            // 
            // labelProduct
            // 
            labelProduct.AutoSize = true;
            labelProduct.Dock = DockStyle.Fill;
            labelProduct.Location = new Point(92, 0);
            labelProduct.Name = "labelProduct";
            labelProduct.Size = new Size(132, 48);
            labelProduct.TabIndex = 1;
            labelProduct.Text = "Products";
            labelProduct.TextAlign = ContentAlignment.MiddleCenter;
            labelProduct.Click += navProducts_Click;
            labelProduct.MouseEnter += navProducts_MouseEnter;
            labelProduct.MouseLeave += navProducts_MouseLeave;
            // 
            // pictureProduct
            // 
            pictureProduct.Dock = DockStyle.Fill;
            pictureProduct.Image = resources.product;
            pictureProduct.Location = new Point(3, 3);
            pictureProduct.Name = "pictureProduct";
            pictureProduct.Size = new Size(83, 42);
            pictureProduct.SizeMode = PictureBoxSizeMode.Zoom;
            pictureProduct.TabIndex = 0;
            pictureProduct.TabStop = false;
            pictureProduct.Click += navProducts_Click;
            pictureProduct.MouseEnter += navProducts_MouseEnter;
            pictureProduct.MouseLeave += navProducts_MouseLeave;
            // 
            // panelCustomer
            // 
            panelCustomer.ColumnCount = 2;
            panelCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2491455F));
            panelCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7508545F));
            panelCustomer.Controls.Add(labelCustomer, 1, 0);
            panelCustomer.Controls.Add(pictureCustomer, 0, 0);
            panelCustomer.Dock = DockStyle.Fill;
            panelCustomer.Location = new Point(5, 61);
            panelCustomer.Name = "panelCustomer";
            panelCustomer.RowCount = 1;
            panelCustomer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelCustomer.Size = new Size(227, 48);
            panelCustomer.TabIndex = 1;
            panelCustomer.Click += navCustomers_Click;
            panelCustomer.MouseEnter += navCustomer_MouseEnter;
            panelCustomer.MouseLeave += navCustomer_MouseLeave;
            // 
            // labelCustomer
            // 
            labelCustomer.AutoSize = true;
            labelCustomer.Dock = DockStyle.Fill;
            labelCustomer.Location = new Point(92, 0);
            labelCustomer.Name = "labelCustomer";
            labelCustomer.Size = new Size(132, 48);
            labelCustomer.TabIndex = 1;
            labelCustomer.Text = "Customers";
            labelCustomer.TextAlign = ContentAlignment.MiddleCenter;
            labelCustomer.Click += navCustomers_Click;
            labelCustomer.MouseEnter += navCustomer_MouseEnter;
            labelCustomer.MouseLeave += navCustomer_MouseLeave;
            // 
            // pictureCustomer
            // 
            pictureCustomer.Dock = DockStyle.Fill;
            pictureCustomer.Image = resources.customer;
            pictureCustomer.Location = new Point(3, 3);
            pictureCustomer.Name = "pictureCustomer";
            pictureCustomer.Size = new Size(83, 42);
            pictureCustomer.SizeMode = PictureBoxSizeMode.Zoom;
            pictureCustomer.TabIndex = 0;
            pictureCustomer.TabStop = false;
            pictureCustomer.Click += navCustomers_Click;
            pictureCustomer.MouseEnter += navCustomer_MouseEnter;
            pictureCustomer.MouseLeave += navCustomer_MouseLeave;
            // 
            // mainTabControl
            // 
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(985, 604);
            mainTabControl.SizeMode = TabSizeMode.Fixed;
            mainTabControl.TabIndex = 1;
            // 
            // toolStrip2
            // 
            toolStrip2.Dock = DockStyle.Bottom;
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.Items.AddRange(new ToolStripItem[] { toggleSidebarButton });
            toolStrip2.Location = new Point(0, 604);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(985, 25);
            toolStrip2.TabIndex = 0;
            toolStrip2.Text = "toolStrip2";
            // 
            // toggleSidebarButton
            // 
            toggleSidebarButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleSidebarButton.Image = resources.left;
            toggleSidebarButton.ImageTransparentColor = Color.Magenta;
            toggleSidebarButton.Name = "toggleSidebarButton";
            toggleSidebarButton.Size = new Size(23, 22);
            toggleSidebarButton.Text = "toolStripButton2";
            toggleSidebarButton.Click += toggleSidebarButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1235, 699);
            Controls.Add(mainContainer);
            Controls.Add(toolStrip1);
            DoubleBuffered = true;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            mainContainer.Panel1.ResumeLayout(false);
            mainContainer.Panel2.ResumeLayout(false);
            mainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainContainer).EndInit();
            mainContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            sidebarLayout.ResumeLayout(false);
            panelOrder.ResumeLayout(false);
            panelOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureOrders).EndInit();
            panelProduct.ResumeLayout(false);
            panelProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureProduct).EndInit();
            panelCustomer.ResumeLayout(false);
            panelCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureCustomer).EndInit();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem productToolStripMenuItem;
        private ToolStripMenuItem customerToolStripMenuItem;
        private ToolStripMenuItem orderToolStripMenuItem;
        public SplitContainer mainContainer;
        private SplitContainer splitContainer1;
        public TabControl mainTabControl;
        private ToolStrip toolStrip2;
        private ToolStripButton toggleSidebarButton;
        private TableLayoutPanel sidebarLayout;
        private TableLayoutPanel panelOrder;
        private Label labelOrders;
        private PictureBox pictureOrders;
        private TableLayoutPanel panelProduct;
        private Label labelProduct;
        private PictureBox pictureProduct;
        private TableLayoutPanel panelCustomer;
        private Label labelCustomer;
        private PictureBox pictureCustomer;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator seperatorCloseTab;
        private ToolStripSplitButton buttonCloseTab;
        private ToolStripMenuItem allTabsToolStripMenuItem;
        private ToolStripMenuItem otherTabsToolStripMenuItem;
        private ToolStripSplitButton buttonSave;
        private ToolStripMenuItem saveAndNewToolStripMenuItem;
        private ToolStripMenuItem saveAndExitToolStripMenuItem;
        private ToolStripSeparator seperatorSave;
    }
}