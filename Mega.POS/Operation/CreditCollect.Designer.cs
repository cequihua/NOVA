namespace Mega.POS.Operation
{
    partial class CreditCollect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditCollect));
            this.panel = new System.Windows.Forms.Panel();
            this.SaleInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.TotalCreditlabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DIMLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.FindDimButton = new System.Windows.Forms.Button();
            this.DimTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.ConfirmCollectButton = new System.Windows.Forms.Button();
            this.CreditsdataGridView = new System.Windows.Forms.DataGridView();
            this.SelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IdOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OfficialConsecutive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Billed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NotesTextBox = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            this.SaleInfoGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CreditsdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.SaleInfoGroupBox);
            this.panel.Controls.Add(this.FindDimButton);
            this.panel.Controls.Add(this.DimTextBox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(704, 94);
            this.panel.TabIndex = 0;
            // 
            // SaleInfoGroupBox
            // 
            this.SaleInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaleInfoGroupBox.Controls.Add(this.TotalCreditlabel);
            this.SaleInfoGroupBox.Controls.Add(this.label2);
            this.SaleInfoGroupBox.Controls.Add(this.DIMLabel);
            this.SaleInfoGroupBox.Controls.Add(this.label10);
            this.SaleInfoGroupBox.Location = new System.Drawing.Point(124, 5);
            this.SaleInfoGroupBox.Name = "SaleInfoGroupBox";
            this.SaleInfoGroupBox.Size = new System.Drawing.Size(572, 81);
            this.SaleInfoGroupBox.TabIndex = 7;
            this.SaleInfoGroupBox.TabStop = false;
            // 
            // TotalCreditlabel
            // 
            this.TotalCreditlabel.AutoSize = true;
            this.TotalCreditlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCreditlabel.Location = new System.Drawing.Point(119, 50);
            this.TotalCreditlabel.Name = "TotalCreditlabel";
            this.TotalCreditlabel.Size = new System.Drawing.Size(16, 16);
            this.TotalCreditlabel.TabIndex = 23;
            this.TotalCreditlabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Crédito actual:";
            // 
            // DIMLabel
            // 
            this.DIMLabel.AutoSize = true;
            this.DIMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DIMLabel.Location = new System.Drawing.Point(120, 20);
            this.DIMLabel.Name = "DIMLabel";
            this.DIMLabel.Size = new System.Drawing.Size(332, 16);
            this.DIMLabel.TabIndex = 20;
            this.DIMLabel.Text = "(Escriba un número de DIM y presione ENTER)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Nombre:";
            // 
            // FindDimButton
            // 
            this.FindDimButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindDimButton.Location = new System.Drawing.Point(10, 43);
            this.FindDimButton.Name = "FindDimButton";
            this.FindDimButton.Size = new System.Drawing.Size(106, 41);
            this.FindDimButton.TabIndex = 4;
            this.FindDimButton.Text = "Buscar DIM";
            this.FindDimButton.UseVisualStyleBackColor = true;
            this.FindDimButton.Click += new System.EventHandler(this.FindDimButton_Click);
            // 
            // DimTextBox
            // 
            this.DimTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimTextBox.Location = new System.Drawing.Point(10, 12);
            this.DimTextBox.Name = "DimTextBox";
            this.DimTextBox.Size = new System.Drawing.Size(106, 26);
            this.DimTextBox.TabIndex = 3;
            this.DimTextBox.TextChanged += new System.EventHandler(this.DimTextBox_TextChanged);
            this.DimTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DimTextBox_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CloseButton);
            this.panel1.Controls.Add(this.PrintButton);
            this.panel1.Controls.Add(this.ConfirmCollectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 381);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 70);
            this.panel1.TabIndex = 1;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(571, 9);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(124, 53);
            this.CloseButton.TabIndex = 13;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintButton.Enabled = false;
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(283, 9);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(129, 53);
            this.PrintButton.TabIndex = 12;
            this.PrintButton.Text = "&Imprimir Ticket (F11)";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // ConfirmCollectButton
            // 
            this.ConfirmCollectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmCollectButton.Enabled = false;
            this.ConfirmCollectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmCollectButton.Location = new System.Drawing.Point(427, 8);
            this.ConfirmCollectButton.Name = "ConfirmCollectButton";
            this.ConfirmCollectButton.Size = new System.Drawing.Size(129, 54);
            this.ConfirmCollectButton.TabIndex = 4;
            this.ConfirmCollectButton.Text = "C&onfirmar Cobro";
            this.ConfirmCollectButton.UseVisualStyleBackColor = true;
            this.ConfirmCollectButton.Click += new System.EventHandler(this.ConfirmCollectButton_Click);
            // 
            // CreditsdataGridView
            // 
            this.CreditsdataGridView.AllowUserToAddRows = false;
            this.CreditsdataGridView.AllowUserToDeleteRows = false;
            this.CreditsdataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CreditsdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.CreditsdataGridView.ColumnHeadersHeight = 25;
            this.CreditsdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectColumn,
            this.IdOperation,
            this.OfficialConsecutive,
            this.CreditAmount,
            this.Billed,
            this.ToBill,
            this.ModifiedDate});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CreditsdataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.CreditsdataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.CreditsdataGridView.Location = new System.Drawing.Point(10, 128);
            this.CreditsdataGridView.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.CreditsdataGridView.Name = "CreditsdataGridView";
            this.CreditsdataGridView.RowTemplate.Height = 30;
            this.CreditsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CreditsdataGridView.Size = new System.Drawing.Size(685, 180);
            this.CreditsdataGridView.TabIndex = 3;
            // 
            // SelectColumn
            // 
            this.SelectColumn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SelectColumn.HeaderText = "";
            this.SelectColumn.Name = "SelectColumn";
            this.SelectColumn.Width = 5;
            // 
            // IdOperation
            // 
            this.IdOperation.DataPropertyName = "IdOperation";
            this.IdOperation.HeaderText = "IdOperation";
            this.IdOperation.Name = "IdOperation";
            this.IdOperation.Visible = false;
            this.IdOperation.Width = 87;
            // 
            // OfficialConsecutive
            // 
            this.OfficialConsecutive.DataPropertyName = "OfficialConsecutive";
            this.OfficialConsecutive.HeaderText = "No. Ticket";
            this.OfficialConsecutive.Name = "OfficialConsecutive";
            this.OfficialConsecutive.ReadOnly = true;
            this.OfficialConsecutive.Width = 82;
            // 
            // CreditAmount
            // 
            this.CreditAmount.DataPropertyName = "CreditAmount";
            this.CreditAmount.HeaderText = "Importe a Crédito";
            this.CreditAmount.Name = "CreditAmount";
            this.CreditAmount.ReadOnly = true;
            this.CreditAmount.Width = 112;
            // 
            // Billed
            // 
            this.Billed.DataPropertyName = "Billed";
            this.Billed.HeaderText = "Importe Pagado";
            this.Billed.Name = "Billed";
            this.Billed.ReadOnly = true;
            this.Billed.Width = 107;
            // 
            // ToBill
            // 
            this.ToBill.DataPropertyName = "ToBill";
            this.ToBill.HeaderText = "Importe por Pagar";
            this.ToBill.Name = "ToBill";
            this.ToBill.ReadOnly = true;
            this.ToBill.Width = 116;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.DataPropertyName = "ModifiedDate";
            this.ModifiedDate.HeaderText = "Fecha de Modificación";
            this.ModifiedDate.Name = "ModifiedDate";
            this.ModifiedDate.Width = 140;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(481, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Lista de Ventas a Crédito sin pagar (seleccione la que desea pagar)";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Notas aclaratoria (opcional):";
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextBox.Location = new System.Drawing.Point(10, 331);
            this.NotesTextBox.Multiline = true;
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NotesTextBox.Size = new System.Drawing.Size(685, 44);
            this.NotesTextBox.TabIndex = 23;
            // 
            // CreditCollect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 451);
            this.ControlBox = false;
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CreditsdataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditCollect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobro de Crédito";
            this.Load += new System.EventHandler(this.CreditCollect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditCollect_KeyDown);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.SaleInfoGroupBox.ResumeLayout(false);
            this.SaleInfoGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CreditsdataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button FindDimButton;
        private System.Windows.Forms.TextBox DimTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ConfirmCollectButton;
        private System.Windows.Forms.DataGridView CreditsdataGridView;
        private System.Windows.Forms.GroupBox SaleInfoGroupBox;
        private System.Windows.Forms.Label DIMLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TotalCreditlabel;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NotesTextBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn OfficialConsecutive;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Billed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
    }
}