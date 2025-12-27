namespace OrderSystem.Win.Controls
{
    partial class ProductControl
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
            numericUpDownWeight = new NumericUpDown();
            labelName = new Label();
            textBoxName = new TextBox();
            labelUnitPrice = new Label();
            numericUpDownPrice = new NumericUpDown();
            labelWeight = new Label();
            mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrice).BeginInit();
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
            mainLayout.Controls.Add(numericUpDownWeight, 1, 2);
            mainLayout.Controls.Add(labelName, 0, 0);
            mainLayout.Controls.Add(textBoxName, 1, 0);
            mainLayout.Controls.Add(labelUnitPrice, 0, 1);
            mainLayout.Controls.Add(numericUpDownPrice, 1, 1);
            mainLayout.Controls.Add(labelWeight, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Margin = new Padding(4);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(4);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.Size = new Size(430, 115);
            mainLayout.TabIndex = 1;
            // 
            // numericUpDownWeight
            // 
            numericUpDownWeight.Dock = DockStyle.Fill;
            numericUpDownWeight.Location = new Point(220, 80);
            numericUpDownWeight.Margin = new Padding(4);
            numericUpDownWeight.MinimumSize = new Size(200, 0);
            numericUpDownWeight.Name = "numericUpDownWeight";
            numericUpDownWeight.Size = new Size(200, 25);
            numericUpDownWeight.TabIndex = 5;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Dock = DockStyle.Fill;
            labelName.Location = new Point(10, 10);
            labelName.Margin = new Padding(4);
            labelName.MinimumSize = new Size(200, 25);
            labelName.Name = "labelName";
            labelName.Padding = new Padding(4);
            labelName.Size = new Size(200, 25);
            labelName.TabIndex = 0;
            labelName.Text = "Name";
            // 
            // textBoxName
            // 
            textBoxName.Dock = DockStyle.Fill;
            textBoxName.Location = new Point(220, 10);
            textBoxName.Margin = new Padding(4);
            textBoxName.MinimumSize = new Size(200, 25);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(200, 25);
            textBoxName.TabIndex = 1;
            // 
            // labelUnitPrice
            // 
            labelUnitPrice.AutoSize = true;
            labelUnitPrice.Dock = DockStyle.Fill;
            labelUnitPrice.Location = new Point(10, 45);
            labelUnitPrice.Margin = new Padding(4);
            labelUnitPrice.MinimumSize = new Size(200, 25);
            labelUnitPrice.Name = "labelUnitPrice";
            labelUnitPrice.Padding = new Padding(4);
            labelUnitPrice.Size = new Size(200, 25);
            labelUnitPrice.TabIndex = 2;
            labelUnitPrice.Text = "Unit Price";
            // 
            // numericUpDownPrice
            // 
            numericUpDownPrice.Dock = DockStyle.Fill;
            numericUpDownPrice.Location = new Point(220, 45);
            numericUpDownPrice.Margin = new Padding(4);
            numericUpDownPrice.MinimumSize = new Size(200, 0);
            numericUpDownPrice.Name = "numericUpDownPrice";
            numericUpDownPrice.Size = new Size(200, 25);
            numericUpDownPrice.TabIndex = 3;
            // 
            // labelWeight
            // 
            labelWeight.AutoSize = true;
            labelWeight.Dock = DockStyle.Fill;
            labelWeight.Location = new Point(10, 80);
            labelWeight.Margin = new Padding(4);
            labelWeight.MinimumSize = new Size(200, 25);
            labelWeight.Name = "labelWeight";
            labelWeight.Padding = new Padding(4);
            labelWeight.Size = new Size(200, 25);
            labelWeight.TabIndex = 4;
            labelWeight.Text = "Weight";
            // 
            // ProductControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "ProductControl";
            Size = new Size(430, 115);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private NumericUpDown numericUpDownWeight;
        private Label labelName;
        private TextBox textBoxName;
        private Label labelUnitPrice;
        private NumericUpDown numericUpDownPrice;
        private Label labelWeight;
    }
}
