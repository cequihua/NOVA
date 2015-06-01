namespace Mega.Synchronizer.Service.Monitor
{
    partial class ViewTraces
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTraces));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowButton = new System.Windows.Forms.Button();
            this.SelectedDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ConsoleLogTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowButton);
            this.panel1.Controls.Add(this.SelectedDateDateTimePicker);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 46);
            this.panel1.TabIndex = 0;
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(309, 12);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(75, 23);
            this.ShowButton.TabIndex = 13;
            this.ShowButton.Text = "Mostrar";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.Show_Click);
            // 
            // SelectedDateDateTimePicker
            // 
            this.SelectedDateDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.SelectedDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SelectedDateDateTimePicker.Location = new System.Drawing.Point(135, 13);
            this.SelectedDateDateTimePicker.Name = "SelectedDateDateTimePicker";
            this.SelectedDateDateTimePicker.Size = new System.Drawing.Size(165, 20);
            this.SelectedDateDateTimePicker.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione el día:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ConsoleLogTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 416);
            this.panel2.TabIndex = 1;
            // 
            // ConsoleLogTextBox
            // 
            this.ConsoleLogTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.ConsoleLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleLogTextBox.Multiline = true;
            this.ConsoleLogTextBox.Name = "ConsoleLogTextBox";
            this.ConsoleLogTextBox.ReadOnly = true;
            this.ConsoleLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConsoleLogTextBox.Size = new System.Drawing.Size(784, 416);
            this.ConsoleLogTextBox.TabIndex = 0;
            // 
            // ViewTraces
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ViewTraces";
            this.Text = "Trazas internas de la aplicación";
            this.Load += new System.EventHandler(this.ViewTraces_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.DateTimePicker SelectedDateDateTimePicker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox ConsoleLogTextBox;
    }
}