namespace OrderSystem.Win.Templates
{
    partial class OrderItemDetailView
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
            orderItemControl1 = new OrderSystem.Win.Controls.OrderItemControl();
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
            tableLayoutPanel1.Controls.Add(orderItemControl1, 0, 0);
            tableLayoutPanel1.Controls.Add(persistentEntityBaseControl1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(438, 201);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // orderItemControl1
            // 
            orderItemControl1.AutoSize = true;
            orderItemControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            orderItemControl1.Dock = DockStyle.Fill;
            orderItemControl1.Location = new Point(4, 4);
            orderItemControl1.Margin = new Padding(4);
            orderItemControl1.Name = "orderItemControl1";
            orderItemControl1.Size = new Size(430, 72);
            orderItemControl1.TabIndex = 5;
            // 
            // persistentEntityBaseControl1
            // 
            persistentEntityBaseControl1.AutoSize = true;
            persistentEntityBaseControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            persistentEntityBaseControl1.Dock = DockStyle.Fill;
            persistentEntityBaseControl1.Location = new Point(3, 83);
            persistentEntityBaseControl1.Name = "persistentEntityBaseControl1";
            persistentEntityBaseControl1.Size = new Size(432, 115);
            persistentEntityBaseControl1.TabIndex = 6;
            // 
            // OrderItemDetailView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Name = "OrderItemDetailView";
            Size = new Size(438, 201);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.OrderItemControl orderItemControl1;
        private Controls.PersistentEntityBaseControl persistentEntityBaseControl1;
    }
}
