namespace OrderSystem.Win.Controls
{
    partial class OrderItemControl
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
            labelQuantity = new Label();
            labelProduct = new Label();
            comboBoxProduct = new ComboBox();
            numericUpDownQuantity = new NumericUpDown();
            mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantity).BeginInit();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.AutoSize = true;
            mainLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle());
            mainLayout.ColumnStyles.Add(new ColumnStyle());
            mainLayout.Controls.Add(labelQuantity, 0, 1);
            mainLayout.Controls.Add(labelProduct, 0, 0);
            mainLayout.Controls.Add(comboBoxProduct, 1, 0);
            mainLayout.Controls.Add(numericUpDownQuantity, 1, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Margin = new Padding(4);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.Size = new Size(424, 72);
            mainLayout.TabIndex = 1;
            // 
            // labelQuantity
            // 
            labelQuantity.AutoSize = true;
            labelQuantity.Dock = DockStyle.Fill;
            labelQuantity.Location = new Point(6, 41);
            labelQuantity.Margin = new Padding(4);
            labelQuantity.MinimumSize = new Size(200, 25);
            labelQuantity.Name = "labelQuantity";
            labelQuantity.Padding = new Padding(4);
            labelQuantity.Size = new Size(200, 25);
            labelQuantity.TabIndex = 2;
            labelQuantity.Text = "Quantity";
            // 
            // labelProduct
            // 
            labelProduct.AutoSize = true;
            labelProduct.Dock = DockStyle.Fill;
            labelProduct.Location = new Point(6, 6);
            labelProduct.Margin = new Padding(4);
            labelProduct.MinimumSize = new Size(200, 25);
            labelProduct.Name = "labelProduct";
            labelProduct.Padding = new Padding(4);
            labelProduct.Size = new Size(200, 25);
            labelProduct.TabIndex = 1;
            labelProduct.Text = "Product";
            // 
            // comboBoxProduct
            // 
            comboBoxProduct.Dock = DockStyle.Fill;
            comboBoxProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProduct.FormattingEnabled = true;
            comboBoxProduct.Location = new Point(216, 6);
            comboBoxProduct.Margin = new Padding(4);
            comboBoxProduct.MaxDropDownItems = 64;
            comboBoxProduct.MinimumSize = new Size(200, 0);
            comboBoxProduct.Name = "comboBoxProduct";
            comboBoxProduct.Size = new Size(202, 25);
            comboBoxProduct.TabIndex = 0;
            comboBoxProduct.SelectedIndexChanged += (sender, args) => OnChanged(); 
            // 
            // numericUpDownQuantity
            // 
            numericUpDownQuantity.Dock = DockStyle.Fill;
            numericUpDownQuantity.Location = new Point(216, 41);
            numericUpDownQuantity.Margin = new Padding(4);
            numericUpDownQuantity.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numericUpDownQuantity.MinimumSize = new Size(200, 0);
            numericUpDownQuantity.Name = "numericUpDownQuantity";
            numericUpDownQuantity.Size = new Size(200, 25);
            numericUpDownQuantity.TabIndex = 3;
            // 
            // OrderItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "OrderItemControl";
            Size = new Size(424, 72);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Label labelQuantity;
        private Label labelProduct;
        private ComboBox comboBoxProduct;
        private NumericUpDown numericUpDownQuantity;
    }
}
