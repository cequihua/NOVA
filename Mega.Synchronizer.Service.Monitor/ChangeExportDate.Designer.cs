namespace Mega.Synchronizer.Service.Monitor
{
    partial class ChangeExportDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeExportDate));
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NotExistsLabel = new System.Windows.Forms.Label();
            this.ExportDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(289, 168);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Guardar";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.NotExistsLabel);
            this.groupBox1.Controls.Add(this.ExportDateTimePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 148);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha INICIAL para la próxima Exportación";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 67);
            this.label2.TabIndex = 6;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // NotExistsLabel
            // 
            this.NotExistsLabel.AutoSize = true;
            this.NotExistsLabel.ForeColor = System.Drawing.Color.Red;
            this.NotExistsLabel.Location = new System.Drawing.Point(8, 46);
            this.NotExistsLabel.Name = "NotExistsLabel";
            this.NotExistsLabel.Size = new System.Drawing.Size(271, 13);
            this.NotExistsLabel.TabIndex = 5;
            this.NotExistsLabel.Text = "* No se ha realizado ninguna exportación en esta tienda";
            this.NotExistsLabel.Visible = false;
            // 
            // ExportDateTimePicker
            // 
            this.ExportDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ExportDateTimePicker.Location = new System.Drawing.Point(54, 19);
            this.ExportDateTimePicker.Name = "ExportDateTimePicker";
            this.ExportDateTimePicker.Size = new System.Drawing.Size(194, 20);
            this.ExportDateTimePicker.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha:";
            // 
            // ChangeExportDate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(375, 199);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeExportDate";
            this.Text = "Cambiar Fecha Inicial de Exportación";
            this.Load += new System.EventHandler(this.ChangeExportDate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NotExistsLabel;
        private System.Windows.Forms.DateTimePicker ExportDateTimePicker;
        private System.Windows.Forms.Label label1;
    }
}