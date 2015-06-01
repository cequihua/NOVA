namespace Mega.POS.Movement
{
    partial class CashierClose
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LastCloseLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CashierNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MakeCloseButton = new System.Windows.Forms.Button();
            this.CancelFormButton = new System.Windows.Forms.Button();
            this.PrintCloseCashier = new System.Windows.Forms.Button();
            this.CloseByMoneyDataGridView = new System.Windows.Forms.DataGridView();
            this.TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyCodeByMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashierOperationAmountByMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationAmountByMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastChangeRateByMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountByMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cashierCloseMoneyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cashierCloseDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseByMoneyDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashierCloseMoneyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashierCloseDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LastCloseLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CashierNameLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Caja";
            // 
            // LastCloseLabel
            // 
            this.LastCloseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LastCloseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastCloseLabel.Location = new System.Drawing.Point(403, 21);
            this.LastCloseLabel.Name = "LastCloseLabel";
            this.LastCloseLabel.Size = new System.Drawing.Size(62, 13);
            this.LastCloseLabel.TabIndex = 3;
            this.LastCloseLabel.Text = "label2";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cierre Anterior:";
            // 
            // CashierNameLabel
            // 
            this.CashierNameLabel.AutoSize = true;
            this.CashierNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashierNameLabel.Location = new System.Drawing.Point(43, 21);
            this.CashierNameLabel.Name = "CashierNameLabel";
            this.CashierNameLabel.Size = new System.Drawing.Size(97, 13);
            this.CashierNameLabel.TabIndex = 1;
            this.CashierNameLabel.Text = "Nombre de Caja";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caja: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resumen de Movimientos desde el último Cierre";
            // 
            // MakeCloseButton
            // 
            this.MakeCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MakeCloseButton.Enabled = false;
            this.MakeCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MakeCloseButton.Location = new System.Drawing.Point(349, 321);
            this.MakeCloseButton.Name = "MakeCloseButton";
            this.MakeCloseButton.Size = new System.Drawing.Size(110, 47);
            this.MakeCloseButton.TabIndex = 1;
            this.MakeCloseButton.Text = "&Efectuar Cierre";
            this.MakeCloseButton.UseVisualStyleBackColor = true;
            this.MakeCloseButton.Click += new System.EventHandler(this.MakeCloseButton_Click);
            // 
            // CancelFormButton
            // 
            this.CancelFormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelFormButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelFormButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelFormButton.Location = new System.Drawing.Point(473, 321);
            this.CancelFormButton.Name = "CancelFormButton";
            this.CancelFormButton.Size = new System.Drawing.Size(110, 47);
            this.CancelFormButton.TabIndex = 0;
            this.CancelFormButton.Text = "&Cerrar";
            this.CancelFormButton.UseVisualStyleBackColor = true;
            this.CancelFormButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PrintCloseCashier
            // 
            this.PrintCloseCashier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintCloseCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintCloseCashier.Location = new System.Drawing.Point(225, 321);
            this.PrintCloseCashier.Name = "PrintCloseCashier";
            this.PrintCloseCashier.Size = new System.Drawing.Size(110, 47);
            this.PrintCloseCashier.TabIndex = 3;
            this.PrintCloseCashier.Text = "&Imprimir el Cierre";
            this.PrintCloseCashier.UseVisualStyleBackColor = true;
            this.PrintCloseCashier.Click += new System.EventHandler(this.PrintCloseCashier_Click);
            // 
            // CloseByMoneyDataGridView
            // 
            this.CloseByMoneyDataGridView.AllowUserToAddRows = false;
            this.CloseByMoneyDataGridView.AllowUserToDeleteRows = false;
            this.CloseByMoneyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseByMoneyDataGridView.AutoGenerateColumns = false;
            this.CloseByMoneyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CloseByMoneyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeName,
            this.CurrencyCodeByMoney,
            this.CashierOperationAmountByMoney,
            this.OperationAmountByMoney,
            this.LastChangeRateByMoney,
            this.AmountByMoney});
            this.CloseByMoneyDataGridView.DataSource = this.cashierCloseMoneyBindingSource;
            this.CloseByMoneyDataGridView.Location = new System.Drawing.Point(8, 78);
            this.CloseByMoneyDataGridView.Name = "CloseByMoneyDataGridView";
            this.CloseByMoneyDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CloseByMoneyDataGridView.Size = new System.Drawing.Size(575, 237);
            this.CloseByMoneyDataGridView.TabIndex = 8;
            this.CloseByMoneyDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.CloseByMoneyDataGridView_DataError);
            // 
            // TypeName
            // 
            this.TypeName.DataPropertyName = "TypeName";
            this.TypeName.HeaderText = "Tipo";
            this.TypeName.Name = "TypeName";
            this.TypeName.ReadOnly = true;
            this.TypeName.Width = 150;
            // 
            // CurrencyCodeByMoney
            // 
            this.CurrencyCodeByMoney.DataPropertyName = "CurrencyCode";
            this.CurrencyCodeByMoney.HeaderText = "Moneda";
            this.CurrencyCodeByMoney.Name = "CurrencyCodeByMoney";
            this.CurrencyCodeByMoney.ReadOnly = true;
            // 
            // CashierOperationAmountByMoney
            // 
            this.CashierOperationAmountByMoney.DataPropertyName = "CashierOperationAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.CashierOperationAmountByMoney.DefaultCellStyle = dataGridViewCellStyle1;
            this.CashierOperationAmountByMoney.HeaderText = "Su Importe";
            this.CashierOperationAmountByMoney.Name = "CashierOperationAmountByMoney";
            // 
            // OperationAmountByMoney
            // 
            this.OperationAmountByMoney.DataPropertyName = "OperationAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.OperationAmountByMoney.DefaultCellStyle = dataGridViewCellStyle2;
            this.OperationAmountByMoney.HeaderText = "Importe Real";
            this.OperationAmountByMoney.Name = "OperationAmountByMoney";
            this.OperationAmountByMoney.ReadOnly = true;
            this.OperationAmountByMoney.Visible = false;
            // 
            // LastChangeRateByMoney
            // 
            this.LastChangeRateByMoney.DataPropertyName = "LastChangeRate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LastChangeRateByMoney.DefaultCellStyle = dataGridViewCellStyle3;
            this.LastChangeRateByMoney.HeaderText = "T. Cambio";
            this.LastChangeRateByMoney.Name = "LastChangeRateByMoney";
            this.LastChangeRateByMoney.Visible = false;
            // 
            // AmountByMoney
            // 
            this.AmountByMoney.DataPropertyName = "Amount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.AmountByMoney.DefaultCellStyle = dataGridViewCellStyle4;
            this.AmountByMoney.HeaderText = "Importe Mon. Oficial";
            this.AmountByMoney.Name = "AmountByMoney";
            this.AmountByMoney.Visible = false;
            // 
            // cashierCloseMoneyBindingSource
            // 
            this.cashierCloseMoneyBindingSource.DataSource = typeof(Mega.Common.CashierCloseMoney);
            // 
            // cashierCloseDetailBindingSource
            // 
            this.cashierCloseDetailBindingSource.DataSource = typeof(Mega.Common.CashierCloseDetail);
            // 
            // CashierClose
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(592, 373);
            this.ControlBox = false;
            this.Controls.Add(this.CloseByMoneyDataGridView);
            this.Controls.Add(this.PrintCloseCashier);
            this.Controls.Add(this.CancelFormButton);
            this.Controls.Add(this.MakeCloseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CashierClose";
            this.Text = "Cierre de Caja o Arqueo";
            this.Activated += new System.EventHandler(this.CashierClose_Activated);
            this.Load += new System.EventHandler(this.CashierClose_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseByMoneyDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashierCloseMoneyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashierCloseDetailBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LastCloseLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CashierNameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button MakeCloseButton;
        private System.Windows.Forms.Button CancelFormButton;
        private System.Windows.Forms.BindingSource cashierCloseDetailBindingSource;
        private System.Windows.Forms.Button PrintCloseCashier;
        private System.Windows.Forms.DataGridView CloseByMoneyDataGridView;
        private System.Windows.Forms.BindingSource cashierCloseMoneyBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrencyCodeByMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashierOperationAmountByMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationAmountByMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastChangeRateByMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountByMoney;
    }
}