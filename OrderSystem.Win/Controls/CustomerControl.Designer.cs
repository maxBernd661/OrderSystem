namespace OrderSystem.Win.Controls
{
    partial class CustomerControl
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
            labelName = new Label();
            labelMail = new Label();
            checkBoxActive = new CheckBox();
            textBoxName = new TextBox();
            textBoxMail = new TextBox();
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
            mainLayout.Controls.Add(labelName, 0, 0);
            mainLayout.Controls.Add(labelMail, 0, 1);
            mainLayout.Controls.Add(checkBoxActive, 0, 2);
            mainLayout.Controls.Add(textBoxName, 1, 0);
            mainLayout.Controls.Add(textBoxMail, 1, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Margin = new Padding(4);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(4);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.Size = new Size(430, 119);
            mainLayout.TabIndex = 0;
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
            // labelMail
            // 
            labelMail.AutoSize = true;
            labelMail.Dock = DockStyle.Fill;
            labelMail.Location = new Point(10, 45);
            labelMail.Margin = new Padding(4);
            labelMail.MinimumSize = new Size(200, 25);
            labelMail.Name = "labelMail";
            labelMail.Padding = new Padding(4);
            labelMail.Size = new Size(200, 25);
            labelMail.TabIndex = 1;
            labelMail.Text = "Email";
            // 
            // checkBoxActive
            // 
            checkBoxActive.AutoSize = true;
            checkBoxActive.Dock = DockStyle.Fill;
            checkBoxActive.Location = new Point(10, 80);
            checkBoxActive.Margin = new Padding(4);
            checkBoxActive.MinimumSize = new Size(200, 25);
            checkBoxActive.Name = "checkBoxActive";
            checkBoxActive.Padding = new Padding(4);
            checkBoxActive.RightToLeft = RightToLeft.Yes;
            checkBoxActive.Size = new Size(200, 29);
            checkBoxActive.TabIndex = 3;
            checkBoxActive.Text = "Is Active";
            checkBoxActive.TextAlign = ContentAlignment.MiddleRight;
            checkBoxActive.UseVisualStyleBackColor = true;
            checkBoxActive.CheckedChanged += (sender, args) => OnChanged(); 
            // 
            // textBoxName
            // 
            textBoxName.Dock = DockStyle.Fill;
            textBoxName.Location = new Point(220, 10);
            textBoxName.Margin = new Padding(4);
            textBoxName.MaxLength = 50;
            textBoxName.MinimumSize = new Size(200, 0);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(200, 25);
            textBoxName.TabIndex = 4;
            textBoxName.TextChanged += (sender, args) => OnChanged();
            // 
            // textBoxMail
            // 
            textBoxMail.Dock = DockStyle.Fill;
            textBoxMail.Location = new Point(220, 45);
            textBoxMail.Margin = new Padding(4);
            textBoxMail.MaxLength = 100;
            textBoxMail.MinimumSize = new Size(200, 0);
            textBoxMail.Name = "textBoxMail";
            textBoxMail.Size = new Size(200, 25);
            textBoxMail.TabIndex = 5;
            textBoxMail.TextChanged += (sender, args) => OnChanged();
            // 
            // CustomerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "CustomerControl";
            Size = new Size(430, 119);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TableLayoutPanel mainLayout;
        private Label labelName;
        private Label labelMail;
        private CheckBox checkBoxActive;
        private TextBox textBoxName;
        private TextBox textBoxMail;
    }
}
