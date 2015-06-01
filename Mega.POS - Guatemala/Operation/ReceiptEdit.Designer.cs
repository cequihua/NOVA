namespace Mega.POS.Operation
{
    partial class ReceiptEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptEdit));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModifiedDateLabel = new System.Windows.Forms.Label();
            this.PedimentTextBox = new System.Windows.Forms.TextBox();
            this.AddedDateLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.OperationAmountLabel = new System.Windows.Forms.Label();
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.ShopLabel = new System.Windows.Forms.Label();
            this.ConsecutiveLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operationDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CloseButton = new System.Windows.Forms.Button();
            this.ConfirmOperationButton = new System.Windows.Forms.Button();
            this.CancelOperationButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.idProductDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ModifiedDateLabel);
            this.groupBox1.Controls.Add(this.PedimentTextBox);
            this.groupBox1.Controls.Add(this.AddedDateLabel);
            this.groupBox1.Controls.Add(this.StatusLabel);
            this.groupBox1.Controls.Add(this.OperationAmountLabel);
            this.groupBox1.Controls.Add(this.CurrencyLabel);
            this.groupBox1.Controls.Add(this.ShopLabel);
            this.groupBox1.Controls.Add(this.ConsecutiveLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos generales de la Operación";
            // 
            // ModifiedDateLabel
            // 
            this.ModifiedDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModifiedDateLabel.Location = new System.Drawing.Point(601, 66);
            this.ModifiedDateLabel.Name = "ModifiedDateLabel";
            this.ModifiedDateLabel.Size = new System.Drawing.Size(170, 13);
            this.ModifiedDateLabel.TabIndex = 20;
            this.ModifiedDateLabel.Text = "Fecha Actualizado";
            // 
            // PedimentTextBox
            // 
            this.PedimentTextBox.Location = new System.Drawing.Point(346, 12);
            this.PedimentTextBox.Name = "PedimentTextBox";
            this.PedimentTextBox.Size = new System.Drawing.Size(109, 20);
            this.PedimentTextBox.TabIndex = 19;
            this.PedimentTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.PedimentTextBox_Validating);
            // 
            // AddedDateLabel
            // 
            this.AddedDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddedDateLabel.Location = new System.Drawing.Point(601, 18);
            this.AddedDateLabel.Name = "AddedDateLabel";
            this.AddedDateLabel.Size = new System.Drawing.Size(170, 13);
            this.AddedDateLabel.TabIndex = 16;
            this.AddedDateLabel.Text = "Fecha de Alta";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(601, 42);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(170, 13);
            this.StatusLabel.TabIndex = 14;
            this.StatusLabel.Text = "Status";
            // 
            // OperationAmountLabel
            // 
            this.OperationAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationAmountLabel.Location = new System.Drawing.Point(343, 64);
            this.OperationAmountLabel.Name = "OperationAmountLabel";
            this.OperationAmountLabel.Size = new System.Drawing.Size(170, 13);
            this.OperationAmountLabel.TabIndex = 13;
            this.OperationAmountLabel.Text = "0.00";
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyLabel.Location = new System.Drawing.Point(343, 40);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(170, 13);
            this.CurrencyLabel.TabIndex = 12;
            this.CurrencyLabel.Text = "MXP";
            // 
            // ShopLabel
            // 
            this.ShopLabel.Location = new System.Drawing.Point(101, 42);
            this.ShopLabel.Name = "ShopLabel";
            this.ShopLabel.Size = new System.Drawing.Size(170, 13);
            this.ShopLabel.TabIndex = 11;
            this.ShopLabel.Text = "Tienda";
            // 
            // ConsecutiveLabel
            // 
            this.ConsecutiveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsecutiveLabel.Location = new System.Drawing.Point(101, 18);
            this.ConsecutiveLabel.Name = "ConsecutiveLabel";
            this.ConsecutiveLabel.Size = new System.Drawing.Size(170, 13);
            this.ConsecutiveLabel.TabIndex = 9;
            this.ConsecutiveLabel.Text = "No. Orden";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(520, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Fecha Efectiva:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(520, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Estatus:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(520, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Fecha Alta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Importe:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Moneda:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Pedimento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tienda:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Orden:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProductDataGridViewTextBoxColumn,
            this.ProductName,
            this.LotColumn,
            this.countDataGridViewTextBoxColumn,
            this.UMName,
            this.LocationName,
            this.operationPriceColumn,
            this.operationAmountColumn,
            this.DocCount});
            this.dataGridView1.DataSource = this.operationDetailBindingSource;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView1.Location = new System.Drawing.Point(2, 101);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(787, 311);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // operationDetailBindingSource
            // 
            this.operationDetailBindingSource.DataSource = typeof(Mega.Common.OperationDetail);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(665, 418);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(124, 50);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ConfirmOperationButton
            // 
            this.ConfirmOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfirmOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmOperationButton.Location = new System.Drawing.Point(3, 418);
            this.ConfirmOperationButton.Name = "ConfirmOperationButton";
            this.ConfirmOperationButton.Size = new System.Drawing.Size(129, 50);
            this.ConfirmOperationButton.TabIndex = 3;
            this.ConfirmOperationButton.Text = "&Confirmar la Operación";
            this.ConfirmOperationButton.UseVisualStyleBackColor = true;
            this.ConfirmOperationButton.Click += new System.EventHandler(this.ConfirmOperationButton_Click);
            // 
            // CancelOperationButton
            // 
            this.CancelOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelOperationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelOperationButton.Location = new System.Drawing.Point(144, 418);
            this.CancelOperationButton.Name = "CancelOperationButton";
            this.CancelOperationButton.Size = new System.Drawing.Size(129, 50);
            this.CancelOperationButton.TabIndex = 4;
            this.CancelOperationButton.Text = "&Cancelar la Operación";
            this.CancelOperationButton.UseVisualStyleBackColor = true;
            this.CancelOperationButton.Click += new System.EventHandler(this.CancelOperationButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(523, 418);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(129, 50);
            this.PrintButton.TabIndex = 6;
            this.PrintButton.Text = "&Imprimir";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // idProductDataGridViewTextBoxColumn
            // 
            this.idProductDataGridViewTextBoxColumn.DataPropertyName = "IdProduct";
            this.idProductDataGridViewTextBoxColumn.HeaderText = "No. Artículo";
            this.idProductDataGridViewTextBoxColumn.Name = "idProductDataGridViewTextBoxColumn";
            this.idProductDataGridViewTextBoxColumn.ReadOnly = true;
            this.idProductDataGridViewTextBoxColumn.Width = 89;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Nombre";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 69;
            // 
            // LotColumn
            // 
            this.LotColumn.DataPropertyName = "Lot";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.LotColumn.HeaderText = "Lote";
            this.LotColumn.Name = "LotColumn";
            this.LotColumn.Width = 53;
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.countDataGridViewTextBoxColumn.HeaderText = "Cantidad";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.Width = 74;
            // 
            // UMName
            // 
            this.UMName.DataPropertyName = "UMCode";
            this.UMName.HeaderText = "U.M.";
            this.UMName.Name = "UMName";
            this.UMName.ReadOnly = true;
            this.UMName.Width = 55;
            // 
            // LocationName
            // 
            this.LocationName.DataPropertyName = "LocationName";
            this.LocationName.HeaderText = "Ubicación";
            this.LocationName.Name = "LocationName";
            this.LocationName.ReadOnly = true;
            this.LocationName.Width = 80;
            // 
            // operationPriceColumn
            // 
            this.operationPriceColumn.DataPropertyName = "OperationPrice";
            dataGridViewCellStyle3.Format = "C4";
            dataGridViewCellStyle3.NullValue = null;
            this.operationPriceColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.operationPriceColumn.HeaderText = "Precio Compra";
            this.operationPriceColumn.Name = "operationPriceColumn";
            this.operationPriceColumn.ReadOnly = true;
            this.operationPriceColumn.Visible = false;
            this.operationPriceColumn.Width = 101;
            // 
            // operationAmountColumn
            // 
            this.operationAmountColumn.DataPropertyName = "OperationAmount";
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.operationAmountColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.operationAmountColumn.HeaderText = "Importe";
            this.operationAmountColumn.Name = "operationAmountColumn";
            this.operationAmountColumn.ReadOnly = true;
            this.operationAmountColumn.Visible = false;
            this.operationAmountColumn.Width = 67;
            // 
            // DocCount
            // 
            this.DocCount.DataPropertyName = "DocCount";
            this.DocCount.HeaderText = "DocCount";
            this.DocCount.Name = "DocCount";
            this.DocCount.ReadOnly = true;
            this.DocCount.Visible = false;
            this.DocCount.Width = 80;
            // 
            // ReceiptEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.CancelOperationButton);
            this.Controls.Add(this.ConfirmOperationButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ReceiptEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edición del Recibo";
            this.Activated += new System.EventHandler(this.ReceiptEdit_Activated);
            this.Load += new System.EventHandler(this.ReceiptEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationDetailBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label AddedDateLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label OperationAmountLabel;
        private System.Windows.Forms.Label CurrencyLabel;
        private System.Windows.Forms.Label ShopLabel;
        private System.Windows.Forms.Label ConsecutiveLabel;
        private System.Windows.Forms.Button ConfirmOperationButton;
        private System.Windows.Forms.Button CancelOperationButton;
        private System.Windows.Forms.BindingSource operationDetailBindingSource;
        private System.Windows.Forms.TextBox PedimentTextBox;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label ModifiedDateLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProductDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocCount;
    }
}