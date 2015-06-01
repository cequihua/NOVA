namespace Mega.Configurator
{
    partial class CashierListForm
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
            this.CashierListDataGridView = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CashierListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CashierListDataGridView
            // 
            this.CashierListDataGridView.AllowUserToAddRows = false;
            this.CashierListDataGridView.AllowUserToDeleteRows = false;
            this.CashierListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CashierListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CashierListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.NameColumn});
            this.CashierListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CashierListDataGridView.Location = new System.Drawing.Point(0, 0);
            this.CashierListDataGridView.MultiSelect = false;
            this.CashierListDataGridView.Name = "CashierListDataGridView";
            this.CashierListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CashierListDataGridView.ShowEditingIcon = false;
            this.CashierListDataGridView.Size = new System.Drawing.Size(593, 306);
            this.CashierListDataGridView.TabIndex = 1;
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "ID";
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Nombre";
            this.NameColumn.Name = "NameColumn";
            // 
            // CashierListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 306);
            this.Controls.Add(this.CashierListDataGridView);
            this.MinimumSize = new System.Drawing.Size(609, 344);
            this.Name = "CashierListForm";
            this.Text = "Lista de Cajas en la BD actual";
            this.Load += new System.EventHandler(this.CashierList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CashierListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CashierListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
    }
}