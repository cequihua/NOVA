namespace Mega.POS.Operation
{
    partial class OperationList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationList));
            this.panelTop = new System.Windows.Forms.Panel();
            this.ExpiredConsignationCheckBox = new System.Windows.Forms.CheckBox();
            this.MovementTypeComboBox = new System.Windows.Forms.ComboBox();
            this.MovementTypeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FinalDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.InitialDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusComboBox = new System.Windows.Forms.ComboBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ConsecutiveColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OfficialConsecutiveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdDimColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DimNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationCurrencyCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeRateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelBottom = new System.Windows.Forms.Panel();
            this.DeleteOperationButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationBindingSource)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.ExpiredConsignationCheckBox);
            this.panelTop.Controls.Add(this.MovementTypeComboBox);
            this.panelTop.Controls.Add(this.MovementTypeLabel);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.FinalDateTimePicker);
            this.panelTop.Controls.Add(this.InitialDateTimePicker);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.StatusComboBox);
            this.panelTop.Controls.Add(this.FindButton);
            this.panelTop.Controls.Add(this.FindTextBox);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(776, 75);
            this.panelTop.TabIndex = 0;
            // 
            // ExpiredConsignationCheckBox
            // 
            this.ExpiredConsignationCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpiredConsignationCheckBox.AutoSize = true;
            this.ExpiredConsignationCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExpiredConsignationCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpiredConsignationCheckBox.Location = new System.Drawing.Point(18, 40);
            this.ExpiredConsignationCheckBox.Name = "ExpiredConsignationCheckBox";
            this.ExpiredConsignationCheckBox.Size = new System.Drawing.Size(237, 20);
            this.ExpiredConsignationCheckBox.TabIndex = 1;
            this.ExpiredConsignationCheckBox.Text = "Ver sólo Consignaciones Vencidas";
            this.ExpiredConsignationCheckBox.UseVisualStyleBackColor = true;
            // 
            // MovementTypeComboBox
            // 
            this.MovementTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MovementTypeComboBox.FormattingEnabled = true;
            this.MovementTypeComboBox.Location = new System.Drawing.Point(97, 11);
            this.MovementTypeComboBox.Name = "MovementTypeComboBox";
            this.MovementTypeComboBox.Size = new System.Drawing.Size(158, 21);
            this.MovementTypeComboBox.TabIndex = 0;
            // 
            // MovementTypeLabel
            // 
            this.MovementTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MovementTypeLabel.AutoSize = true;
            this.MovementTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovementTypeLabel.Location = new System.Drawing.Point(6, 13);
            this.MovementTypeLabel.Name = "MovementTypeLabel";
            this.MovementTypeLabel.Size = new System.Drawing.Size(87, 16);
            this.MovementTypeLabel.TabIndex = 9;
            this.MovementTypeLabel.Text = "Tipo de Mov.";
            this.MovementTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(492, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hasta: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(492, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Desde: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FinalDateTimePicker
            // 
            this.FinalDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FinalDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FinalDateTimePicker.Location = new System.Drawing.Point(548, 39);
            this.FinalDateTimePicker.Name = "FinalDateTimePicker";
            this.FinalDateTimePicker.Size = new System.Drawing.Size(103, 22);
            this.FinalDateTimePicker.TabIndex = 5;
            // 
            // InitialDateTimePicker
            // 
            this.InitialDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InitialDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitialDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.InitialDateTimePicker.Location = new System.Drawing.Point(549, 10);
            this.InitialDateTimePicker.Name = "InitialDateTimePicker";
            this.InitialDateTimePicker.Size = new System.Drawing.Size(102, 22);
            this.InitialDateTimePicker.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(284, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "y el Estado:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusComboBox.FormattingEnabled = true;
            this.StatusComboBox.Location = new System.Drawing.Point(365, 38);
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(121, 24);
            this.StatusComboBox.TabIndex = 3;
            // 
            // FindButton
            // 
            this.FindButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindButton.Location = new System.Drawing.Point(657, 9);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(113, 52);
            this.FindButton.TabIndex = 6;
            this.FindButton.Text = "Buscar y &Filtrar";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // FindTextBox
            // 
            this.FindTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindTextBox.Location = new System.Drawing.Point(366, 10);
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.Size = new System.Drawing.Size(120, 22);
            this.FindTextBox.TabIndex = 2;
            this.FindTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(265, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "con la palabra:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.dataGridView1);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 75);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelCenter.Size = new System.Drawing.Size(776, 405);
            this.panelCenter.TabIndex = 1;
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
            this.dataGridView1.ColumnHeadersHeight = 22;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConsecutiveColumnColumn,
            this.OfficialConsecutiveColumn,
            this.IdDimColumn,
            this.DimNameColumn,
            this.OperationCurrencyCodeColumn,
            this.OperationAmountColumn,
            this.ChangeRateColumn,
            this.AmountColumn,
            this.StatusNameColumn,
            this.ModifiedDateColumn});
            this.dataGridView1.DataSource = this.operationBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(764, 324);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // ConsecutiveColumnColumn
            // 
            this.ConsecutiveColumnColumn.DataPropertyName = "Consecutive";
            this.ConsecutiveColumnColumn.HeaderText = "Orden";
            this.ConsecutiveColumnColumn.Name = "ConsecutiveColumnColumn";
            this.ConsecutiveColumnColumn.ReadOnly = true;
            this.ConsecutiveColumnColumn.Width = 61;
            // 
            // OfficialConsecutiveColumn
            // 
            this.OfficialConsecutiveColumn.DataPropertyName = "OfficialConsecutive";
            this.OfficialConsecutiveColumn.HeaderText = "Orden Efectiva";
            this.OfficialConsecutiveColumn.Name = "OfficialConsecutiveColumn";
            this.OfficialConsecutiveColumn.ReadOnly = true;
            this.OfficialConsecutiveColumn.Width = 103;
            // 
            // IdDimColumn
            // 
            this.IdDimColumn.DataPropertyName = "IdDim";
            this.IdDimColumn.HeaderText = "No. DIM";
            this.IdDimColumn.Name = "IdDimColumn";
            this.IdDimColumn.ReadOnly = true;
            this.IdDimColumn.Width = 72;
            // 
            // DimNameColumn
            // 
            this.DimNameColumn.DataPropertyName = "DimName";
            this.DimNameColumn.HeaderText = "Nombre";
            this.DimNameColumn.Name = "DimNameColumn";
            this.DimNameColumn.ReadOnly = true;
            this.DimNameColumn.Width = 69;
            // 
            // OperationCurrencyCodeColumn
            // 
            this.OperationCurrencyCodeColumn.DataPropertyName = "OperationCurrencyCode";
            this.OperationCurrencyCodeColumn.HeaderText = "Moneda Op.";
            this.OperationCurrencyCodeColumn.Name = "OperationCurrencyCodeColumn";
            this.OperationCurrencyCodeColumn.ReadOnly = true;
            this.OperationCurrencyCodeColumn.Width = 91;
            // 
            // OperationAmountColumn
            // 
            this.OperationAmountColumn.DataPropertyName = "OperationAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.OperationAmountColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.OperationAmountColumn.HeaderText = "Importe Mon. Op.";
            this.OperationAmountColumn.Name = "OperationAmountColumn";
            this.OperationAmountColumn.ReadOnly = true;
            this.OperationAmountColumn.Width = 114;
            // 
            // ChangeRateColumn
            // 
            this.ChangeRateColumn.DataPropertyName = "ChangeRate";
            this.ChangeRateColumn.HeaderText = "Tipo Cambio";
            this.ChangeRateColumn.Name = "ChangeRateColumn";
            this.ChangeRateColumn.ReadOnly = true;
            this.ChangeRateColumn.Width = 91;
            // 
            // AmountColumn
            // 
            this.AmountColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.AmountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.AmountColumn.HeaderText = "Importe Final";
            this.AmountColumn.Name = "AmountColumn";
            this.AmountColumn.ReadOnly = true;
            this.AmountColumn.Width = 92;
            // 
            // StatusNameColumn
            // 
            this.StatusNameColumn.DataPropertyName = "StatusName";
            this.StatusNameColumn.HeaderText = "Estatus";
            this.StatusNameColumn.Name = "StatusNameColumn";
            this.StatusNameColumn.ReadOnly = true;
            this.StatusNameColumn.Width = 67;
            // 
            // ModifiedDateColumn
            // 
            this.ModifiedDateColumn.DataPropertyName = "ModifiedDate";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.ModifiedDateColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ModifiedDateColumn.HeaderText = "F. Efectiva";
            this.ModifiedDateColumn.Name = "ModifiedDateColumn";
            this.ModifiedDateColumn.ReadOnly = true;
            this.ModifiedDateColumn.Width = 83;
            // 
            // operationBindingSource
            // 
            this.operationBindingSource.DataSource = typeof(Mega.Common.Operation);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.DeleteOperationButton);
            this.panelBottom.Controls.Add(this.EditButton);
            this.panelBottom.Controls.Add(this.AddButton);
            this.panelBottom.Controls.Add(this.CloseButton);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 405);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(776, 75);
            this.panelBottom.TabIndex = 2;
            // 
            // DeleteOperationButton
            // 
            this.DeleteOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteOperationButton.Location = new System.Drawing.Point(294, 10);
            this.DeleteOperationButton.Name = "DeleteOperationButton";
            this.DeleteOperationButton.Size = new System.Drawing.Size(128, 55);
            this.DeleteOperationButton.TabIndex = 3;
            this.DeleteOperationButton.Text = "Eliminar seleccionada";
            this.DeleteOperationButton.UseVisualStyleBackColor = true;
            this.DeleteOperationButton.Click += new System.EventHandler(this.DeleteOperationButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(6, 10);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(128, 55);
            this.EditButton.TabIndex = 2;
            this.EditButton.Text = "&Editar seleccionada";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(150, 10);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(128, 55);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "&Agregar Nueva";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(642, 10);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(128, 55);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // OperationList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(776, 480);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OperationList";
            this.Text = "Lista de Operaciones";
            this.Load += new System.EventHandler(this.OperationList_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationBindingSource)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.TextBox FindTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddButton;
        
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.BindingSource operationBindingSource;
        private System.Windows.Forms.ComboBox StatusComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DeleteOperationButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker FinalDateTimePicker;
        private System.Windows.Forms.DateTimePicker InitialDateTimePicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsecutiveColumnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OfficialConsecutiveColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDimColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DimNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationCurrencyCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDateColumn;
        private System.Windows.Forms.ComboBox MovementTypeComboBox;
        private System.Windows.Forms.Label MovementTypeLabel;
        private System.Windows.Forms.CheckBox ExpiredConsignationCheckBox;
    }
}