namespace OrderSystem.Win.Controls
{
    partial class SidebarButton
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
            label = new Label();
            pictureBox = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.Controls.Add(label, 1, 0);
            tableLayoutPanel1.Controls.Add(pictureBox, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.MaximumSize = new Size(0, 128);
            tableLayoutPanel1.MinimumSize = new Size(160, 32);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(160, 32);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.MouseEnter += OnMouseEnter;
            tableLayoutPanel1.MouseLeave += OnMouseLeave;
            tableLayoutPanel1.Click += (_, _) => OnClicked(); 
            // 
            // label
            // 
            label.AutoSize = true;
            label.Dock = DockStyle.Fill;
            label.Location = new Point(40, 0);
            label.Margin = new Padding(0);
            label.MaximumSize = new Size(0, 128);
            label.MinimumSize = new Size(128, 32);
            label.Name = "label";
            label.Size = new Size(128, 32);
            label.TabIndex = 1;
            label.Text = "label1";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.MouseEnter += OnMouseEnter;
            label.MouseLeave += OnMouseLeave;
            label.Click += (_, _) => OnClicked();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = projectResources.newItem;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Margin = new Padding(0);
            pictureBox.MaximumSize = new Size(128, 128);
            pictureBox.MinimumSize = new Size(32, 32);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(40, 32);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            pictureBox.MouseEnter += OnMouseEnter;
            pictureBox.MouseLeave += OnMouseLeave;
            pictureBox.Click += (_, _) => OnClicked();
            // 
            // SidebarButton
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            MinimumSize = new Size(160, 32);
            Name = "SidebarButton";
            Size = new Size(160, 32);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox;
        private Label label;
    }
}
