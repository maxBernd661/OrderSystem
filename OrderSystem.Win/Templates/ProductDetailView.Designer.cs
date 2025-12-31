namespace OrderSystem.Win.Templates
{
    partial class ProductDetailViewDummy
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
            mainLayout = new TableLayoutPanel();
            productControl1 = new OrderSystem.Win.Controls.ProductControl();
            persistentEntityBaseControl1 = new OrderSystem.Win.Controls.PersistentEntityBaseControl();
            mainLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle());
            mainLayout.Controls.Add(productControl1, 0, 0);
            mainLayout.Controls.Add(persistentEntityBaseControl1, 0, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(4, 4);
            mainLayout.Margin = new Padding(4);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(4);
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.Size = new Size(544, 303);
            mainLayout.TabIndex = 0;
            // 
            // productControl1
            // 
            productControl1.AutoSize = true;
            productControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            productControl1.Dock = DockStyle.Fill;
            productControl1.Location = new Point(8, 8);
            productControl1.Margin = new Padding(4);
            productControl1.Name = "productControl1";
            productControl1.Padding = new Padding(4);
            productControl1.Size = new Size(528, 123);
            productControl1.TabIndex = 0;
            // 
            // persistentEntityBaseControl1
            // 
            persistentEntityBaseControl1.AutoSize = true;
            persistentEntityBaseControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            persistentEntityBaseControl1.Dock = DockStyle.Fill;
            persistentEntityBaseControl1.Location = new Point(8, 139);
            persistentEntityBaseControl1.Margin = new Padding(4);
            persistentEntityBaseControl1.Name = "persistentEntityBaseControl1";
            persistentEntityBaseControl1.Padding = new Padding(4);
            persistentEntityBaseControl1.Size = new Size(528, 156);
            persistentEntityBaseControl1.TabIndex = 1;
            // 
            // ProductDetailViewDummy
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(mainLayout);
            Margin = new Padding(4);
            Name = "ProductDetailViewDummy";
            Padding = new Padding(4);
            Size = new Size(552, 311);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Controls.ProductControl productControl1;
        private Controls.PersistentEntityBaseControl persistentEntityBaseControl1;
        public TableLayoutPanel mainLayout;
    }
}
