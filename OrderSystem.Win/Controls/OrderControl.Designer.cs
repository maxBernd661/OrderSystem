namespace OrderSystem.Win.Controls
{
    partial class OrderControl
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
            textBoxStatus = new TextBox();
            labelStatus = new Label();
            labelCustomer = new Label();
            comboBoxCustomer = new ComboBox();
            mainLayout.SuspendLayout();
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
            mainLayout.Controls.Add(textBoxStatus, 1, 1);
            mainLayout.Controls.Add(labelStatus, 0, 1);
            mainLayout.Controls.Add(labelCustomer, 0, 0);
            mainLayout.Controls.Add(comboBoxCustomer, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Margin = new Padding(4);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.Size = new Size(424, 72);
            mainLayout.TabIndex = 0;
            // 
            // textBoxStatus
            // 
            textBoxStatus.Dock = DockStyle.Fill;
            textBoxStatus.Location = new Point(216, 41);
            textBoxStatus.Margin = new Padding(4);
            textBoxStatus.MaxLength = 100;
            textBoxStatus.MinimumSize = new Size(200, 25);
            textBoxStatus.Name = "textBoxStatus";
            textBoxStatus.ReadOnly = true;
            textBoxStatus.Size = new Size(202, 25);
            textBoxStatus.TabIndex = 3;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Dock = DockStyle.Fill;
            labelStatus.Location = new Point(6, 41);
            labelStatus.Margin = new Padding(4);
            labelStatus.MinimumSize = new Size(200, 25);
            labelStatus.Name = "labelStatus";
            labelStatus.Padding = new Padding(4);
            labelStatus.Size = new Size(200, 25);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "Status";
            // 
            // labelCustomer
            // 
            labelCustomer.AutoSize = true;
            labelCustomer.Dock = DockStyle.Fill;
            labelCustomer.Location = new Point(6, 6);
            labelCustomer.Margin = new Padding(4);
            labelCustomer.MinimumSize = new Size(200, 25);
            labelCustomer.Name = "labelCustomer";
            labelCustomer.Padding = new Padding(4);
            labelCustomer.Size = new Size(200, 25);
            labelCustomer.TabIndex = 1;
            labelCustomer.Text = "Customer";
            // 
            // comboBoxCustomer
            // 
            comboBoxCustomer.Dock = DockStyle.Fill;
            comboBoxCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCustomer.FormattingEnabled = true;
            comboBoxCustomer.Location = new Point(216, 6);
            comboBoxCustomer.Margin = new Padding(4);
            comboBoxCustomer.MaxDropDownItems = 64;
            comboBoxCustomer.MinimumSize = new Size(200, 0);
            comboBoxCustomer.Name = "comboBoxCustomer";
            comboBoxCustomer.Size = new Size(202, 25);
            comboBoxCustomer.TabIndex = 0;
            comboBoxCustomer.SelectionChangeCommitted += (sender, args) => OnChanged(); 
            // 
            // OrderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "OrderControl";
            Size = new Size(424, 72);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Label labelCustomer;
        private Label labelStatus;
        private TextBox textBoxStatus;
        private ComboBox comboBoxCustomer;
    }
}
