namespace Mega.POS
{
    partial class FormResolutionFiscal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResolutionFiscal));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.txtDe = new System.Windows.Forms.TextBox();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFechaAcc = new System.Windows.Forms.DateTimePicker();
            this.txtFechaAuto = new System.Windows.Forms.DateTimePicker();
            this.checkActive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Accion:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha Autorizacion:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Resolucion:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(312, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Serie:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Rango Factura";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "DE:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "AL:";
            // 
            // txtResolution
            // 
            this.txtResolution.Location = new System.Drawing.Point(381, 25);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(149, 20);
            this.txtResolution.TabIndex = 9;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(381, 61);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(149, 20);
            this.txtSerie.TabIndex = 10;
            // 
            // txtDe
            // 
            this.txtDe.Location = new System.Drawing.Point(143, 121);
            this.txtDe.Name = "txtDe";
            this.txtDe.Size = new System.Drawing.Size(74, 20);
            this.txtDe.TabIndex = 11;
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(315, 121);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(74, 20);
            this.txtHasta.TabIndex = 12;
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAceptar.Location = new System.Drawing.Point(455, 172);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 30);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(374, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFechaAcc
            // 
            this.txtFechaAcc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaAcc.Location = new System.Drawing.Point(126, 28);
            this.txtFechaAcc.Name = "txtFechaAcc";
            this.txtFechaAcc.Size = new System.Drawing.Size(158, 20);
            this.txtFechaAcc.TabIndex = 15;
            // 
            // txtFechaAuto
            // 
            this.txtFechaAuto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaAuto.Location = new System.Drawing.Point(126, 64);
            this.txtFechaAuto.Name = "txtFechaAuto";
            this.txtFechaAuto.Size = new System.Drawing.Size(158, 20);
            this.txtFechaAuto.TabIndex = 16;
            // 
            // checkActive
            // 
            this.checkActive.AutoSize = true;
            this.checkActive.Location = new System.Drawing.Point(437, 124);
            this.checkActive.Name = "checkActive";
            this.checkActive.Size = new System.Drawing.Size(56, 17);
            this.checkActive.TabIndex = 17;
            this.checkActive.Text = "Activo";
            this.checkActive.UseVisualStyleBackColor = true;
            // 
            // FormResolutionFiscal
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(538, 214);
            this.Controls.Add(this.checkActive);
            this.Controls.Add(this.txtFechaAuto);
            this.Controls.Add(this.txtFechaAcc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.txtDe);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.txtResolution);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormResolutionFiscal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resolucion Fiscal";
            this.Load += new System.EventHandler(this.FormResolutionFiscal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.TextBox txtDe;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker txtFechaAcc;
        private System.Windows.Forms.DateTimePicker txtFechaAuto;
        private System.Windows.Forms.CheckBox checkActive;
    }
}