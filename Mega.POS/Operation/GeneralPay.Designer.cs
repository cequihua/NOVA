namespace Mega.POS.Operation
{
    partial class GeneralPay
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.PayAndConfirmButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DimInfoLabel2 = new System.Windows.Forms.Label();
            this.DimNameLabel = new System.Windows.Forms.Label();
            this.payDataGridView = new System.Windows.Forms.DataGridView();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationPayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DeleteEntryButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ByPayLabel = new System.Windows.Forms.Label();
            this.AmountOperationLabel = new System.Windows.Forms.Label();
            this.CurrencyOperationLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.payTabControl = new System.Windows.Forms.TabControl();
            this.tabPageCash = new System.Windows.Forms.TabPage();
            this.AddCashButton = new System.Windows.Forms.Button();
            this.CashChangeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CashReceivedTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageCard = new System.Windows.Forms.TabPage();
            this.CardValidityTextBox = new System.Windows.Forms.MaskedTextBox();
            this.CardNumberTextBox = new System.Windows.Forms.MaskedTextBox();
            this.CardNumberHiddenTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.CreditcardRadioButton = new System.Windows.Forms.RadioButton();
            this.AddCardButton = new System.Windows.Forms.Button();
            this.CardAmountTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CardAuthorizationTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageDivisa = new System.Windows.Forms.TabPage();
            this.CurrencyToPayLabel = new System.Windows.Forms.Label();
            this.CurrencyToPayTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.CurrencyChangeRateTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.CurrencyReceivedLabel = new System.Windows.Forms.Label();
            this.AddCurrencyPayButton = new System.Windows.Forms.Button();
            this.CurrencyChangeTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyConvertionReceivedTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyReceivedTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.payDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationPayBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.payTabControl.SuspendLayout();
            this.tabPageCash.SuspendLayout();
            this.tabPageCard.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageDivisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(605, 437);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(123, 55);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PayAndConfirmButton
            // 
            this.PayAndConfirmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PayAndConfirmButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PayAndConfirmButton.Location = new System.Drawing.Point(187, 437);
            this.PayAndConfirmButton.Name = "PayAndConfirmButton";
            this.PayAndConfirmButton.Size = new System.Drawing.Size(159, 55);
            this.PayAndConfirmButton.TabIndex = 1;
            this.PayAndConfirmButton.Text = "&Confirmar Pago y Operación (F12)";
            this.PayAndConfirmButton.UseVisualStyleBackColor = true;
            this.PayAndConfirmButton.Click += new System.EventHandler(this.PayAndConfirmButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DimInfoLabel2);
            this.groupBox1.Controls.Add(this.DimNameLabel);
            this.groupBox1.Location = new System.Drawing.Point(352, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del DIM";
            // 
            // DimInfoLabel2
            // 
            this.DimInfoLabel2.AutoSize = true;
            this.DimInfoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimInfoLabel2.Location = new System.Drawing.Point(9, 60);
            this.DimInfoLabel2.Name = "DimInfoLabel2";
            this.DimInfoLabel2.Size = new System.Drawing.Size(144, 13);
            this.DimInfoLabel2.TabIndex = 3;
            this.DimInfoLabel2.Text = "Información Financiera: ";
            // 
            // DimNameLabel
            // 
            this.DimNameLabel.AutoSize = true;
            this.DimNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimNameLabel.Location = new System.Drawing.Point(9, 20);
            this.DimNameLabel.Name = "DimNameLabel";
            this.DimNameLabel.Size = new System.Drawing.Size(57, 13);
            this.DimNameLabel.TabIndex = 1;
            this.DimNameLabel.Text = "ID/DIM: ";
            // 
            // payDataGridView
            // 
            this.payDataGridView.AllowUserToAddRows = false;
            this.payDataGridView.AutoGenerateColumns = false;
            this.payDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.payDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeNameDataGridViewTextBoxColumn,
            this.CurrencyCode,
            this.OperationAmount,
            this.ChangeRate,
            this.amountDataGridViewTextBoxColumn});
            this.payDataGridView.DataSource = this.operationPayBindingSource;
            this.payDataGridView.Location = new System.Drawing.Point(6, 295);
            this.payDataGridView.Name = "payDataGridView";
            this.payDataGridView.ReadOnly = true;
            this.payDataGridView.Size = new System.Drawing.Size(722, 135);
            this.payDataGridView.TabIndex = 3;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "Tipo";
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // CurrencyCode
            // 
            this.CurrencyCode.DataPropertyName = "CurrencyCode";
            this.CurrencyCode.HeaderText = "Moneda";
            this.CurrencyCode.Name = "CurrencyCode";
            this.CurrencyCode.ReadOnly = true;
            // 
            // OperationAmount
            // 
            this.OperationAmount.DataPropertyName = "OperationAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.OperationAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.OperationAmount.HeaderText = "Importe Moneda";
            this.OperationAmount.Name = "OperationAmount";
            this.OperationAmount.ReadOnly = true;
            // 
            // ChangeRate
            // 
            this.ChangeRate.DataPropertyName = "ChangeRate";
            this.ChangeRate.HeaderText = "Tipo de Cambio";
            this.ChangeRate.Name = "ChangeRate";
            this.ChangeRate.ReadOnly = true;
            this.ChangeRate.Width = 80;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.amountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.amountDataGridViewTextBoxColumn.HeaderText = "Importe";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.ReadOnly = true;
            this.amountDataGridViewTextBoxColumn.Width = 150;
            // 
            // operationPayBindingSource
            // 
            this.operationPayBindingSource.DataSource = typeof(Mega.Common.Operation_Pay);
            // 
            // DeleteEntryButton
            // 
            this.DeleteEntryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteEntryButton.Location = new System.Drawing.Point(6, 437);
            this.DeleteEntryButton.Name = "DeleteEntryButton";
            this.DeleteEntryButton.Size = new System.Drawing.Size(159, 55);
            this.DeleteEntryButton.TabIndex = 3;
            this.DeleteEntryButton.Text = "Eliminar registro de pago seleccionado";
            this.DeleteEntryButton.UseVisualStyleBackColor = true;
            this.DeleteEntryButton.Click += new System.EventHandler(this.DeleteEntryButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ByPayLabel);
            this.groupBox2.Controls.Add(this.AmountOperationLabel);
            this.groupBox2.Controls.Add(this.CurrencyOperationLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 80);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información de la Operación";
            // 
            // ByPayLabel
            // 
            this.ByPayLabel.AutoSize = true;
            this.ByPayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ByPayLabel.Location = new System.Drawing.Point(87, 60);
            this.ByPayLabel.Name = "ByPayLabel";
            this.ByPayLabel.Size = new System.Drawing.Size(67, 13);
            this.ByPayLabel.TabIndex = 5;
            this.ByPayLabel.Text = "Por Pagar:";
            // 
            // AmountOperationLabel
            // 
            this.AmountOperationLabel.AutoSize = true;
            this.AmountOperationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountOperationLabel.Location = new System.Drawing.Point(87, 40);
            this.AmountOperationLabel.Name = "AmountOperationLabel";
            this.AmountOperationLabel.Size = new System.Drawing.Size(86, 13);
            this.AmountOperationLabel.TabIndex = 4;
            this.AmountOperationLabel.Text = "Importe Total:";
            // 
            // CurrencyOperationLabel
            // 
            this.CurrencyOperationLabel.AutoSize = true;
            this.CurrencyOperationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyOperationLabel.Location = new System.Drawing.Point(87, 20);
            this.CurrencyOperationLabel.Name = "CurrencyOperationLabel";
            this.CurrencyOperationLabel.Size = new System.Drawing.Size(56, 13);
            this.CurrencyOperationLabel.TabIndex = 3;
            this.CurrencyOperationLabel.Text = "Moneda:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Por Pagar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Importe Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Moneda:";
            // 
            // payTabControl
            // 
            this.payTabControl.Controls.Add(this.tabPageCash);
            this.payTabControl.Controls.Add(this.tabPageCard);
            this.payTabControl.Controls.Add(this.tabPageDivisa);
            this.payTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payTabControl.Location = new System.Drawing.Point(6, 89);
            this.payTabControl.Name = "payTabControl";
            this.payTabControl.Padding = new System.Drawing.Point(6, 10);
            this.payTabControl.SelectedIndex = 0;
            this.payTabControl.Size = new System.Drawing.Size(726, 199);
            this.payTabControl.TabIndex = 0;
            this.payTabControl.SelectedIndexChanged += new System.EventHandler(this.payTabControl_SelectedIndexChanged);
            // 
            // tabPageCash
            // 
            this.tabPageCash.Controls.Add(this.AddCashButton);
            this.tabPageCash.Controls.Add(this.CashChangeTextBox);
            this.tabPageCash.Controls.Add(this.label5);
            this.tabPageCash.Controls.Add(this.CashReceivedTextBox);
            this.tabPageCash.Controls.Add(this.label1);
            this.tabPageCash.Location = new System.Drawing.Point(4, 39);
            this.tabPageCash.Name = "tabPageCash";
            this.tabPageCash.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCash.Size = new System.Drawing.Size(718, 156);
            this.tabPageCash.TabIndex = 0;
            this.tabPageCash.Text = "Pago en Efectivo (F5)";
            this.tabPageCash.UseVisualStyleBackColor = true;
            // 
            // AddCashButton
            // 
            this.AddCashButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCashButton.Location = new System.Drawing.Point(135, 75);
            this.AddCashButton.Name = "AddCashButton";
            this.AddCashButton.Size = new System.Drawing.Size(163, 40);
            this.AddCashButton.TabIndex = 2;
            this.AddCashButton.Text = "&Aceptar";
            this.AddCashButton.UseVisualStyleBackColor = true;
            this.AddCashButton.Click += new System.EventHandler(this.AddCashButton_Click);
            // 
            // CashChangeTextBox
            // 
            this.CashChangeTextBox.Enabled = false;
            this.CashChangeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashChangeTextBox.Location = new System.Drawing.Point(135, 42);
            this.CashChangeTextBox.Name = "CashChangeTextBox";
            this.CashChangeTextBox.Size = new System.Drawing.Size(163, 26);
            this.CashChangeTextBox.TabIndex = 1;
            this.CashChangeTextBox.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cambio:";
            // 
            // CashReceivedTextBox
            // 
            this.CashReceivedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashReceivedTextBox.Location = new System.Drawing.Point(135, 10);
            this.CashReceivedTextBox.Name = "CashReceivedTextBox";
            this.CashReceivedTextBox.Size = new System.Drawing.Size(163, 26);
            this.CashReceivedTextBox.TabIndex = 0;
            this.CashReceivedTextBox.Text = "0";
            this.CashReceivedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CashReceivedTextBox_KeyPress);
            this.CashReceivedTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CashReceivedTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Recibido:";
            // 
            // tabPageCard
            // 
            this.tabPageCard.Controls.Add(this.CardValidityTextBox);
            this.tabPageCard.Controls.Add(this.CardNumberTextBox);
            this.tabPageCard.Controls.Add(this.CardNumberHiddenTextBox);
            this.tabPageCard.Controls.Add(this.groupBox3);
            this.tabPageCard.Controls.Add(this.AddCardButton);
            this.tabPageCard.Controls.Add(this.CardAmountTextBox);
            this.tabPageCard.Controls.Add(this.label9);
            this.tabPageCard.Controls.Add(this.CardAuthorizationTextBox);
            this.tabPageCard.Controls.Add(this.label8);
            this.tabPageCard.Controls.Add(this.label6);
            this.tabPageCard.Controls.Add(this.label7);
            this.tabPageCard.Location = new System.Drawing.Point(4, 39);
            this.tabPageCard.Name = "tabPageCard";
            this.tabPageCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCard.Size = new System.Drawing.Size(718, 156);
            this.tabPageCard.TabIndex = 1;
            this.tabPageCard.Text = "Pago con Tarjeta (F6)";
            this.tabPageCard.UseVisualStyleBackColor = true;
            // 
            // CardValidityTextBox
            // 
            this.CardValidityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.CardValidityTextBox.Location = new System.Drawing.Point(134, 42);
            this.CardValidityTextBox.Mask = "00/00";
            this.CardValidityTextBox.Name = "CardValidityTextBox";
            this.CardValidityTextBox.Size = new System.Drawing.Size(66, 26);
            this.CardValidityTextBox.TabIndex = 2;
            this.CardValidityTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CardValidityTextBox_MouseClick);
            // 
            // CardNumberTextBox
            // 
            this.CardNumberTextBox.BeepOnError = true;
            this.CardNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.CardNumberTextBox.Location = new System.Drawing.Point(134, 10);
            this.CardNumberTextBox.Mask = "0000 0000 0000 0000";
            this.CardNumberTextBox.Name = "CardNumberTextBox";
            this.CardNumberTextBox.Size = new System.Drawing.Size(187, 26);
            this.CardNumberTextBox.TabIndex = 0;
            this.CardNumberTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CardNumberTextBox_MouseClick);
            this.CardNumberTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CardNumberTextBox_Validating);
            // 
            // CardNumberHiddenTextBox
            // 
            this.CardNumberHiddenTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardNumberHiddenTextBox.Location = new System.Drawing.Point(468, 105);
            this.CardNumberHiddenTextBox.Name = "CardNumberHiddenTextBox";
            this.CardNumberHiddenTextBox.Size = new System.Drawing.Size(187, 26);
            this.CardNumberHiddenTextBox.TabIndex = 1;
            this.CardNumberHiddenTextBox.Visible = false;
            this.CardNumberHiddenTextBox.Enter += new System.EventHandler(this.CardNumberHiddenTextBox_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.CreditcardRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(134, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 39);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(124, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(91, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "T. Debito";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // CreditcardRadioButton
            // 
            this.CreditcardRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreditcardRadioButton.AutoSize = true;
            this.CreditcardRadioButton.Checked = true;
            this.CreditcardRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditcardRadioButton.Location = new System.Drawing.Point(7, 12);
            this.CreditcardRadioButton.Name = "CreditcardRadioButton";
            this.CreditcardRadioButton.Size = new System.Drawing.Size(95, 24);
            this.CreditcardRadioButton.TabIndex = 0;
            this.CreditcardRadioButton.TabStop = true;
            this.CreditcardRadioButton.Text = "T. Credito";
            this.CreditcardRadioButton.UseVisualStyleBackColor = true;
            // 
            // AddCardButton
            // 
            this.AddCardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCardButton.Location = new System.Drawing.Point(468, 45);
            this.AddCardButton.Name = "AddCardButton";
            this.AddCardButton.Size = new System.Drawing.Size(163, 40);
            this.AddCardButton.TabIndex = 5;
            this.AddCardButton.Text = "&Aceptar";
            this.AddCardButton.UseVisualStyleBackColor = true;
            this.AddCardButton.Click += new System.EventHandler(this.AddCardButton_Click);
            // 
            // CardAmountTextBox
            // 
            this.CardAmountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardAmountTextBox.Location = new System.Drawing.Point(468, 13);
            this.CardAmountTextBox.Name = "CardAmountTextBox";
            this.CardAmountTextBox.Size = new System.Drawing.Size(163, 26);
            this.CardAmountTextBox.TabIndex = 4;
            this.CardAmountTextBox.Text = "0";
            this.CardAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CardAmountTextBox_KeyPress);
            this.CardAmountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CardAmountTextBox_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(370, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "A Pagar:";
            // 
            // CardAuthorizationTextBox
            // 
            this.CardAuthorizationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardAuthorizationTextBox.Location = new System.Drawing.Point(134, 74);
            this.CardAuthorizationTextBox.Name = "CardAuthorizationTextBox";
            this.CardAuthorizationTextBox.Size = new System.Drawing.Size(187, 26);
            this.CardAuthorizationTextBox.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Autorizacion:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Validez:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tarjeta:";
            // 
            // tabPageDivisa
            // 
            this.tabPageDivisa.Controls.Add(this.CurrencyToPayLabel);
            this.tabPageDivisa.Controls.Add(this.CurrencyToPayTextBox);
            this.tabPageDivisa.Controls.Add(this.label19);
            this.tabPageDivisa.Controls.Add(this.CurrencyChangeRateTextBox);
            this.tabPageDivisa.Controls.Add(this.label18);
            this.tabPageDivisa.Controls.Add(this.label17);
            this.tabPageDivisa.Controls.Add(this.CurrencyReceivedLabel);
            this.tabPageDivisa.Controls.Add(this.AddCurrencyPayButton);
            this.tabPageDivisa.Controls.Add(this.CurrencyChangeTextBox);
            this.tabPageDivisa.Controls.Add(this.CurrencyConvertionReceivedTextBox);
            this.tabPageDivisa.Controls.Add(this.CurrencyReceivedTextBox);
            this.tabPageDivisa.Controls.Add(this.CurrencyComboBox);
            this.tabPageDivisa.Controls.Add(this.label15);
            this.tabPageDivisa.Location = new System.Drawing.Point(4, 39);
            this.tabPageDivisa.Name = "tabPageDivisa";
            this.tabPageDivisa.Size = new System.Drawing.Size(718, 156);
            this.tabPageDivisa.TabIndex = 3;
            this.tabPageDivisa.Text = "Pago con Divisa (F8)";
            this.tabPageDivisa.UseVisualStyleBackColor = true;
            // 
            // CurrencyToPayLabel
            // 
            this.CurrencyToPayLabel.AutoSize = true;
            this.CurrencyToPayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyToPayLabel.Location = new System.Drawing.Point(7, 80);
            this.CurrencyToPayLabel.Name = "CurrencyToPayLabel";
            this.CurrencyToPayLabel.Size = new System.Drawing.Size(82, 20);
            this.CurrencyToPayLabel.TabIndex = 18;
            this.CurrencyToPayLabel.Text = "A pagar: ";
            // 
            // CurrencyToPayTextBox
            // 
            this.CurrencyToPayTextBox.Enabled = false;
            this.CurrencyToPayTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyToPayTextBox.Location = new System.Drawing.Point(137, 78);
            this.CurrencyToPayTextBox.Name = "CurrencyToPayTextBox";
            this.CurrencyToPayTextBox.Size = new System.Drawing.Size(163, 26);
            this.CurrencyToPayTextBox.TabIndex = 2;
            this.CurrencyToPayTextBox.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(7, 46);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(113, 20);
            this.label19.TabIndex = 16;
            this.label19.Text = "Tipo Cambio:";
            // 
            // CurrencyChangeRateTextBox
            // 
            this.CurrencyChangeRateTextBox.Enabled = false;
            this.CurrencyChangeRateTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyChangeRateTextBox.Location = new System.Drawing.Point(137, 44);
            this.CurrencyChangeRateTextBox.Name = "CurrencyChangeRateTextBox";
            this.CurrencyChangeRateTextBox.Size = new System.Drawing.Size(163, 26);
            this.CurrencyChangeRateTextBox.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(337, 78);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 20);
            this.label18.TabIndex = 14;
            this.label18.Text = "Cambio:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(334, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(148, 20);
            this.label17.TabIndex = 13;
            this.label17.Text = "Total Conversión:";
            // 
            // CurrencyReceivedLabel
            // 
            this.CurrencyReceivedLabel.AutoSize = true;
            this.CurrencyReceivedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyReceivedLabel.Location = new System.Drawing.Point(334, 14);
            this.CurrencyReceivedLabel.Name = "CurrencyReceivedLabel";
            this.CurrencyReceivedLabel.Size = new System.Drawing.Size(84, 20);
            this.CurrencyReceivedLabel.TabIndex = 12;
            this.CurrencyReceivedLabel.Text = "Recibido:";
            // 
            // AddCurrencyPayButton
            // 
            this.AddCurrencyPayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCurrencyPayButton.Location = new System.Drawing.Point(486, 107);
            this.AddCurrencyPayButton.Name = "AddCurrencyPayButton";
            this.AddCurrencyPayButton.Size = new System.Drawing.Size(163, 40);
            this.AddCurrencyPayButton.TabIndex = 6;
            this.AddCurrencyPayButton.Text = "&Aceptar";
            this.AddCurrencyPayButton.UseVisualStyleBackColor = true;
            this.AddCurrencyPayButton.Click += new System.EventHandler(this.AddCurrencyPayButton_Click);
            // 
            // CurrencyChangeTextBox
            // 
            this.CurrencyChangeTextBox.Enabled = false;
            this.CurrencyChangeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyChangeTextBox.Location = new System.Drawing.Point(486, 75);
            this.CurrencyChangeTextBox.Name = "CurrencyChangeTextBox";
            this.CurrencyChangeTextBox.Size = new System.Drawing.Size(163, 26);
            this.CurrencyChangeTextBox.TabIndex = 5;
            this.CurrencyChangeTextBox.Text = "0";
            // 
            // CurrencyConvertionReceivedTextBox
            // 
            this.CurrencyConvertionReceivedTextBox.Enabled = false;
            this.CurrencyConvertionReceivedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyConvertionReceivedTextBox.Location = new System.Drawing.Point(486, 43);
            this.CurrencyConvertionReceivedTextBox.Name = "CurrencyConvertionReceivedTextBox";
            this.CurrencyConvertionReceivedTextBox.Size = new System.Drawing.Size(163, 26);
            this.CurrencyConvertionReceivedTextBox.TabIndex = 4;
            this.CurrencyConvertionReceivedTextBox.Text = "0";
            // 
            // CurrencyReceivedTextBox
            // 
            this.CurrencyReceivedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyReceivedTextBox.Location = new System.Drawing.Point(486, 11);
            this.CurrencyReceivedTextBox.Name = "CurrencyReceivedTextBox";
            this.CurrencyReceivedTextBox.Size = new System.Drawing.Size(163, 26);
            this.CurrencyReceivedTextBox.TabIndex = 3;
            this.CurrencyReceivedTextBox.Text = "0";
            this.CurrencyReceivedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CurrencyReceivedTextBox_KeyPress);
            this.CurrencyReceivedTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CurrencyReceivedTextBox_Validating);
            // 
            // CurrencyComboBox
            // 
            this.CurrencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrencyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyComboBox.FormattingEnabled = true;
            this.CurrencyComboBox.Location = new System.Drawing.Point(137, 10);
            this.CurrencyComboBox.Name = "CurrencyComboBox";
            this.CurrencyComboBox.Size = new System.Drawing.Size(163, 28);
            this.CurrencyComboBox.TabIndex = 0;
            this.CurrencyComboBox.SelectedIndexChanged += new System.EventHandler(this.CurrencyComboBox_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 20);
            this.label15.TabIndex = 4;
            this.label15.Text = "Moneda:";
            // 
            // GeneralPay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(731, 495);
            this.ControlBox = false;
            this.Controls.Add(this.payTabControl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DeleteEntryButton);
            this.Controls.Add(this.payDataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PayAndConfirmButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GeneralPay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Cobro de Operaciones";
            this.Activated += new System.EventHandler(this.GeneralPay_Activated);
            this.Load += new System.EventHandler(this.Pay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pay_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.payDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationPayBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.payTabControl.ResumeLayout(false);
            this.tabPageCash.ResumeLayout(false);
            this.tabPageCash.PerformLayout();
            this.tabPageCard.ResumeLayout(false);
            this.tabPageCard.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageDivisa.ResumeLayout(false);
            this.tabPageDivisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button PayAndConfirmButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label DimNameLabel;
        private System.Windows.Forms.DataGridView payDataGridView;
        private System.Windows.Forms.Button DeleteEntryButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ByPayLabel;
        private System.Windows.Forms.Label AmountOperationLabel;
        private System.Windows.Forms.Label CurrencyOperationLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DimInfoLabel2;
        private System.Windows.Forms.TabControl payTabControl;
        private System.Windows.Forms.TabPage tabPageCash;
        private System.Windows.Forms.TabPage tabPageCard;
        private System.Windows.Forms.TabPage tabPageDivisa;
        private System.Windows.Forms.TextBox CashChangeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CashReceivedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddCashButton;
        private System.Windows.Forms.BindingSource operationPayBindingSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CardAmountTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox CardAuthorizationTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button AddCardButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton CreditcardRadioButton;
        private System.Windows.Forms.TextBox CardNumberHiddenTextBox;
        private System.Windows.Forms.MaskedTextBox CardNumberTextBox;
        private System.Windows.Forms.MaskedTextBox CardValidityTextBox;
        private System.Windows.Forms.ComboBox CurrencyComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button AddCurrencyPayButton;
        private System.Windows.Forms.TextBox CurrencyChangeTextBox;
        private System.Windows.Forms.TextBox CurrencyConvertionReceivedTextBox;
        private System.Windows.Forms.TextBox CurrencyReceivedTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label CurrencyReceivedLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox CurrencyChangeRateTextBox;
        private System.Windows.Forms.Label CurrencyToPayLabel;
        private System.Windows.Forms.TextBox CurrencyToPayTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrencyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
    }
}