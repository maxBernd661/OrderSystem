namespace OrderSystem.Win.Forms
{
    partial class PopupView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStrip = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            panel = new Panel();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.ImageScalingSize = new Size(32, 32);
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Margin = new Padding(4);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(4);
            toolStrip.Size = new Size(784, 52);
            toolStrip.Stretch = true;
            toolStrip.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = projectResources.save;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Margin = new Padding(4);
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(121, 36);
            toolStripButton1.Text = "Save and Exit";
            // 
            // panel
            // 
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 52);
            panel.Name = "panel";
            panel.Size = new Size(784, 409);
            panel.TabIndex = 1;
            // 
            // PopupView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(784, 461);
            Controls.Add(panel);
            Controls.Add(toolStrip);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(800, 500);
            Name = "PopupView";
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "PopupView";
            TopMost = true;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton toolStripButton1;
        private Panel panel;
    }
}