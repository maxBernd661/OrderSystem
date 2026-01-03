namespace OrderSystem.Win.Templates
{
    partial class OrderDetailView
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
            tableLayoutPanel1 = new TableLayoutPanel();
            listViewDummy1 = new OrderSystem.Win.Controls.ListViewDummy();
            orderControl1 = new OrderSystem.Win.Controls.OrderControl();
            persistentEntityBaseControl1 = new OrderSystem.Win.Controls.PersistentEntityBaseControl();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(listViewDummy1, 0, 1);
            tableLayoutPanel1.Controls.Add(orderControl1, 0, 0);
            tableLayoutPanel1.Controls.Add(persistentEntityBaseControl1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(438, 505);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // listViewDummy1
            // 
            listViewDummy1.AutoSize = true;
            listViewDummy1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            listViewDummy1.Dock = DockStyle.Fill;
            listViewDummy1.EntityType = "OrderItem";
            listViewDummy1.FilterKey = null;
            listViewDummy1.Location = new Point(3, 81);
            listViewDummy1.MinimumSize = new Size(200, 300);
            listViewDummy1.Name = "listViewDummy1";
            listViewDummy1.OnlyRelevantData = true;
            listViewDummy1.Size = new Size(432, 300);
            listViewDummy1.TabIndex = 2;
            // 
            // orderControl1
            // 
            orderControl1.AutoSize = true;
            orderControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            orderControl1.Dock = DockStyle.Fill;
            orderControl1.Location = new Point(3, 3);
            orderControl1.Name = "orderControl1";
            orderControl1.Size = new Size(432, 72);
            orderControl1.TabIndex = 3;
            // 
            // persistentEntityBaseControl1
            // 
            persistentEntityBaseControl1.AutoSize = true;
            persistentEntityBaseControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            persistentEntityBaseControl1.Dock = DockStyle.Fill;
            persistentEntityBaseControl1.Location = new Point(3, 387);
            persistentEntityBaseControl1.Name = "persistentEntityBaseControl1";
            persistentEntityBaseControl1.Size = new Size(432, 115);
            persistentEntityBaseControl1.TabIndex = 4;
            // 
            // OrderDetailView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Name = "OrderDetailView";
            Size = new Size(438, 505);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.ListViewDummy listViewDummy1;
        private Controls.OrderControl orderControl1;
        private Controls.PersistentEntityBaseControl persistentEntityBaseControl1;
    }
}
