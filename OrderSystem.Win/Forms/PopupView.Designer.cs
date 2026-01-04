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
            buttonSave = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonClose = new ToolStripButton();
            panel = new Panel();
            toolStrip1 = new ToolStrip();
            labelValidation = new ToolStripLabel();
            toolStrip.SuspendLayout();
            panel.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.ImageScalingSize = new Size(32, 32);
            toolStrip.Items.AddRange(new ToolStripItem[] { buttonSave, toolStripSeparator1, buttonClose });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Margin = new Padding(4);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(4);
            toolStrip.Size = new Size(794, 52);
            toolStrip.Stretch = true;
            toolStrip.TabIndex = 0;
            // 
            // buttonSave
            // 
            buttonSave.Image = projectResources.save;
            buttonSave.ImageTransparentColor = Color.Magenta;
            buttonSave.Margin = new Padding(4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(121, 36);
            buttonSave.Text = "Save and Exit";
            buttonSave.Click += buttonSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(4);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 36);
            // 
            // buttonClose
            // 
            buttonClose.Image = projectResources.close;
            buttonClose.ImageTransparentColor = Color.Magenta;
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(64, 41);
            buttonClose.Text = "Exit";
            buttonClose.Click += buttonClose_Click;
            // 
            // panel
            // 
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.Controls.Add(toolStrip1);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 52);
            panel.Name = "panel";
            panel.Size = new Size(794, 442);
            panel.TabIndex = 1;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { labelValidation });
            toolStrip1.Location = new Point(0, 417);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(794, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // labelValidation
            // 
            labelValidation.Margin = new Padding(4);
            labelValidation.Name = "labelValidation";
            labelValidation.Size = new Size(0, 17);
            // 
            // PopupView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(794, 494);
            ControlBox = false;
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
            panel.ResumeLayout(false);
            panel.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton buttonSave;
        private Panel panel;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonClose;
        private ToolStrip toolStrip1;
        private ToolStripLabel labelValidation;
    }
}