namespace OrderSystem.Win.Controls
{
    partial class PersistentEntityBaseControl
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
            textBoxUpdated = new TextBox();
            textBoxCreated = new TextBox();
            labelId = new Label();
            textBoxId = new TextBox();
            labelCreated = new Label();
            labelUpdated = new Label();
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
            mainLayout.Controls.Add(textBoxUpdated, 1, 2);
            mainLayout.Controls.Add(textBoxCreated, 1, 1);
            mainLayout.Controls.Add(labelId, 0, 0);
            mainLayout.Controls.Add(textBoxId, 1, 0);
            mainLayout.Controls.Add(labelCreated, 0, 1);
            mainLayout.Controls.Add(labelUpdated, 0, 2);
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
            mainLayout.TabIndex = 0;
            // 
            // textBoxUpdated
            // 
            textBoxUpdated.Dock = DockStyle.Fill;
            textBoxUpdated.Location = new Point(219, 79);
            textBoxUpdated.MinimumSize = new Size(200, 25);
            textBoxUpdated.Name = "textBoxUpdated";
            textBoxUpdated.ReadOnly = true;
            textBoxUpdated.Size = new Size(202, 25);
            textBoxUpdated.TabIndex = 5;
            textBoxUpdated.Text = "\r\n";
            // 
            // textBoxCreated
            // 
            textBoxCreated.Dock = DockStyle.Fill;
            textBoxCreated.Location = new Point(219, 44);
            textBoxCreated.MinimumSize = new Size(200, 25);
            textBoxCreated.Name = "textBoxCreated";
            textBoxCreated.ReadOnly = true;
            textBoxCreated.Size = new Size(202, 25);
            textBoxCreated.TabIndex = 4;
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Dock = DockStyle.Fill;
            labelId.Location = new Point(10, 10);
            labelId.Margin = new Padding(4);
            labelId.MinimumSize = new Size(200, 25);
            labelId.Name = "labelId";
            labelId.Padding = new Padding(4);
            labelId.Size = new Size(200, 25);
            labelId.TabIndex = 0;
            labelId.Text = "ID";
            // 
            // textBoxId
            // 
            textBoxId.Dock = DockStyle.Fill;
            textBoxId.Location = new Point(219, 9);
            textBoxId.MinimumSize = new Size(200, 25);
            textBoxId.Name = "textBoxId";
            textBoxId.ReadOnly = true;
            textBoxId.Size = new Size(202, 25);
            textBoxId.TabIndex = 1;
            // 
            // labelCreated
            // 
            labelCreated.AutoSize = true;
            labelCreated.Dock = DockStyle.Fill;
            labelCreated.Location = new Point(10, 45);
            labelCreated.Margin = new Padding(4);
            labelCreated.MinimumSize = new Size(200, 25);
            labelCreated.Name = "labelCreated";
            labelCreated.Padding = new Padding(4);
            labelCreated.Size = new Size(200, 25);
            labelCreated.TabIndex = 2;
            labelCreated.Text = "Created At";
            // 
            // labelUpdated
            // 
            labelUpdated.AutoSize = true;
            labelUpdated.Dock = DockStyle.Fill;
            labelUpdated.Location = new Point(10, 80);
            labelUpdated.Margin = new Padding(4);
            labelUpdated.MinimumSize = new Size(200, 25);
            labelUpdated.Name = "labelUpdated";
            labelUpdated.Padding = new Padding(4);
            labelUpdated.Size = new Size(200, 25);
            labelUpdated.TabIndex = 3;
            labelUpdated.Text = "Updated At";
            // 
            // PersistentEntityBaseControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "PersistentEntityBaseControl";
            Size = new Size(430, 115);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label labelId;
        public TableLayoutPanel mainLayout;
        private TextBox textBoxUpdated;
        private TextBox textBoxCreated;
        private TextBox textBoxId;
        private Label labelCreated;
        private Label labelUpdated;
    }
}
