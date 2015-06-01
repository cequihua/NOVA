namespace Mega.POS.Movement
{
    partial class MovementAdd
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
            this.CancelFormButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.OperationGroupBox = new System.Windows.Forms.GroupBox();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrencyComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.OperationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelFormButton
            // 
            this.CancelFormButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelFormButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelFormButton.Location = new System.Drawing.Point(233, 252);
            this.CancelFormButton.Name = "CancelFormButton";
            this.CancelFormButton.Size = new System.Drawing.Size(75, 46);
            this.CancelFormButton.TabIndex = 3;
            this.CancelFormButton.Text = "&Cerrar";
            this.CancelFormButton.UseVisualStyleBackColor = true;
            this.CancelFormButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(143, 252);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 46);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "&Agregar";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // OperationGroupBox
            // 
            this.OperationGroupBox.Controls.Add(this.NoteTextBox);
            this.OperationGroupBox.Controls.Add(this.label4);
            this.OperationGroupBox.Controls.Add(this.AmountTextBox);
            this.OperationGroupBox.Controls.Add(this.label3);
            this.OperationGroupBox.Controls.Add(this.CurrencyComboBox);
            this.OperationGroupBox.Controls.Add(this.label2);
            this.OperationGroupBox.Controls.Add(this.TypeComboBox);
            this.OperationGroupBox.Controls.Add(this.label1);
            this.OperationGroupBox.Location = new System.Drawing.Point(7, 12);
            this.OperationGroupBox.Name = "OperationGroupBox";
            this.OperationGroupBox.Size = new System.Drawing.Size(301, 234);
            this.OperationGroupBox.TabIndex = 0;
            this.OperationGroupBox.TabStop = false;
            this.OperationGroupBox.Text = "Datos del Movimiento";
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoteTextBox.Location = new System.Drawing.Point(82, 105);
            this.NoteTextBox.MaxLength = 1000;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.Size = new System.Drawing.Size(207, 119);
            this.NoteTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nota:";
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountTextBox.Location = new System.Drawing.Point(82, 79);
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new System.Drawing.Size(103, 22);
            this.AmountTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Importe:";
            // 
            // CurrencyComboBox
            // 
            this.CurrencyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyComboBox.FormattingEnabled = true;
            this.CurrencyComboBox.Location = new System.Drawing.Point(82, 51);
            this.CurrencyComboBox.Name = "CurrencyComboBox";
            this.CurrencyComboBox.Size = new System.Drawing.Size(207, 24);
            this.CurrencyComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Moneda:";
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(82, 23);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(207, 24);
            this.TypeComboBox.TabIndex = 0;
            this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo: ";
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(52, 252);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(75, 46);
            this.PrintButton.TabIndex = 2;
            this.PrintButton.Text = "&Imprimir";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Visible = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // MovementAdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(314, 304);
            this.ControlBox = false;
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.OperationGroupBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CancelFormButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MovementAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Movimiento en Caja";
            this.Activated += new System.EventHandler(this.MovementAdd_Activated);
            this.Load += new System.EventHandler(this.MovementAdd_Load);
            this.OperationGroupBox.ResumeLayout(false);
            this.OperationGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelFormButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox OperationGroupBox;
        private System.Windows.Forms.TextBox AmountTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CurrencyComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NoteTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button PrintButton;
    }
}