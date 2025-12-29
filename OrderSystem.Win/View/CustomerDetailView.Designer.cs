namespace OrderSystem.Win.View
{
    partial class CustomerDetailView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            orderListView1 = new ListViewDummy();
            customerControl = new OrderSystem.Win.Controls.CustomerControl();
            tableLayoutPanel1 = new TableLayoutPanel();
            persistentBaseInfoControl1 = new OrderSystem.Win.Controls.PersistentBaseInfoControl();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // orderListView1
            // 
            orderListView1.AutoSize = true;
            orderListView1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            orderListView1.Dock = DockStyle.Fill;
            orderListView1.EntityType = "Order";
            orderListView1.Location = new Point(5, 132);
            orderListView1.MinimumSize = new Size(300, 200);
            orderListView1.Name = "orderListView1";
            orderListView1.Size = new Size(430, 200);
            orderListView1.TabIndex = 1;
            // 
            // customerControl
            // 
            customerControl.AutoSize = true;
            customerControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            customerControl.Dock = DockStyle.Fill;
            customerControl.Location = new Point(5, 5);
            customerControl.Name = "customerControl";
            customerControl.Size = new Size(430, 119);
            customerControl.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(customerControl, 0, 0);
            tableLayoutPanel1.Controls.Add(orderListView1, 0, 1);
            tableLayoutPanel1.Controls.Add(persistentBaseInfoControl1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(440, 460);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // persistentBaseInfoControl1
            // 
            persistentBaseInfoControl1.AutoSize = true;
            persistentBaseInfoControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            persistentBaseInfoControl1.Dock = DockStyle.Fill;
            persistentBaseInfoControl1.Location = new Point(5, 340);
            persistentBaseInfoControl1.Name = "persistentBaseInfoControl1";
            persistentBaseInfoControl1.Size = new Size(430, 115);
            persistentBaseInfoControl1.TabIndex = 2;
            // 
            // CustomerDetailView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Name = "CustomerDetailView";
            Size = new Size(440, 460);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewDummy orderListView1;
        private Controls.CustomerControl customerControl;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.PersistentBaseInfoControl persistentBaseInfoControl1;
    }
}
