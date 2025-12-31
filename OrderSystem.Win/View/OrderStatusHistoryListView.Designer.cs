namespace OrderSystem.Win.View
{
    partial class OrderStatusHistoryListView
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGrid = new DataGridView();
            orderDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            changedToDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            changedFromDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            createdAtDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            updatedAtDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.AllowUserToOrderColumns = true;
            dataGrid.AllowUserToResizeRows = false;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.BackgroundColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGrid.Columns.AddRange(new DataGridViewColumn[] { orderDataGridViewTextBoxColumn, changedToDataGridViewTextBoxColumn, changedFromDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn, createdAtDataGridViewTextBoxColumn, updatedAtDataGridViewTextBoxColumn });
            dataGrid.DataSource = bindingSource;
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.Location = new Point(0, 0);
            dataGrid.MinimumSize = new Size(300, 200);
            dataGrid.Name = "dataGrid";
            dataGrid.ReadOnly = true;
            dataGrid.Size = new Size(300, 200);
            dataGrid.TabIndex = 1;
            // 
            // orderDataGridViewTextBoxColumn
            // 
            orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            orderDataGridViewTextBoxColumn.HeaderText = "Order";
            orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            orderDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // changedToDataGridViewTextBoxColumn
            // 
            changedToDataGridViewTextBoxColumn.DataPropertyName = "ChangedTo";
            changedToDataGridViewTextBoxColumn.HeaderText = "ChangedTo";
            changedToDataGridViewTextBoxColumn.Name = "changedToDataGridViewTextBoxColumn";
            changedToDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // changedFromDataGridViewTextBoxColumn
            // 
            changedFromDataGridViewTextBoxColumn.DataPropertyName = "ChangedFrom";
            changedFromDataGridViewTextBoxColumn.HeaderText = "ChangedFrom";
            changedFromDataGridViewTextBoxColumn.Name = "changedFromDataGridViewTextBoxColumn";
            changedFromDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdAtDataGridViewTextBoxColumn
            // 
            createdAtDataGridViewTextBoxColumn.DataPropertyName = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.HeaderText = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.Name = "createdAtDataGridViewTextBoxColumn";
            createdAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedAtDataGridViewTextBoxColumn
            // 
            updatedAtDataGridViewTextBoxColumn.DataPropertyName = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.HeaderText = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.Name = "updatedAtDataGridViewTextBoxColumn";
            updatedAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource
            // 
            bindingSource.DataSource = typeof(Core.Entities.OrderStatusHistoryDTO);
            // 
            // OrderStatusHistoryListView
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(dataGrid);
            MinimumSize = new Size(300, 200);
            Name = "OrderStatusHistoryListView";
            Size = new Size(300, 200);
            ((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public DataGridView dataGrid;
        private DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn changedToDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn changedFromDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn createdAtDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn updatedAtDataGridViewTextBoxColumn;
        private BindingSource bindingSource;
    }
}
