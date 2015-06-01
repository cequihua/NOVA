namespace Mega.Synchronizer.Service.Monitor
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChangeExportDateButton = new System.Windows.Forms.Button();
            this.ShopComboBox = new System.Windows.Forms.ComboBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.StartService = new System.Windows.Forms.Button();
            this.StopService = new System.Windows.Forms.Button();
            this.ExportNow = new System.Windows.Forms.Button();
            this.ImportNow = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ViewMonitorTraces = new System.Windows.Forms.Button();
            this.ViewSynchronizerTraces = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LastExport = new System.Windows.Forms.Label();
            this.HoursPlanOut = new System.Windows.Forms.Label();
            this.DaysPlanOut = new System.Windows.Forms.Label();
            this.LastImport = new System.Windows.Forms.Label();
            this.HoursPlanIn = new System.Windows.Forms.Label();
            this.DaysPlanIn = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ConsoleLogtextBox = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ChangeExportDateButton);
            this.panel2.Controls.Add(this.ShopComboBox);
            this.panel2.Controls.Add(this.CloseButton);
            this.panel2.Controls.Add(this.StartService);
            this.panel2.Controls.Add(this.StopService);
            this.panel2.Controls.Add(this.ExportNow);
            this.panel2.Controls.Add(this.ImportNow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 45);
            this.panel2.TabIndex = 1;
            // 
            // ChangeExportDateButton
            // 
            this.ChangeExportDateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangeExportDateButton.Location = new System.Drawing.Point(231, 11);
            this.ChangeExportDateButton.Name = "ChangeExportDateButton";
            this.ChangeExportDateButton.Size = new System.Drawing.Size(160, 23);
            this.ChangeExportDateButton.TabIndex = 6;
            this.ChangeExportDateButton.Text = "Cambiar &Fecha Exportación";
            this.ChangeExportDateButton.UseVisualStyleBackColor = true;
            this.ChangeExportDateButton.Click += new System.EventHandler(this.ChangeExportDateButton_Click);
            // 
            // ShopComboBox
            // 
            this.ShopComboBox.FormattingEnabled = true;
            this.ShopComboBox.Items.AddRange(new object[] {
            "- Todas las Tiendas -"});
            this.ShopComboBox.Location = new System.Drawing.Point(237, 12);
            this.ShopComboBox.Name = "ShopComboBox";
            this.ShopComboBox.Size = new System.Drawing.Size(171, 21);
            this.ShopComboBox.TabIndex = 5;
            this.ShopComboBox.Text = "- Todas las Tiendas -";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(672, 11);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(99, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // StartService
            // 
            this.StartService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartService.Location = new System.Drawing.Point(564, 11);
            this.StartService.Name = "StartService";
            this.StartService.Size = new System.Drawing.Size(99, 23);
            this.StartService.TabIndex = 3;
            this.StartService.Text = "&Iniciar Servicio";
            this.StartService.UseVisualStyleBackColor = true;
            this.StartService.Click += new System.EventHandler(this.StartService_Click);
            // 
            // StopService
            // 
            this.StopService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopService.Location = new System.Drawing.Point(457, 11);
            this.StopService.Name = "StopService";
            this.StopService.Size = new System.Drawing.Size(99, 23);
            this.StopService.TabIndex = 2;
            this.StopService.Text = "&Detener Servicio";
            this.StopService.UseVisualStyleBackColor = true;
            this.StopService.Click += new System.EventHandler(this.StopService_Click);
            // 
            // ExportNow
            // 
            this.ExportNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportNow.Location = new System.Drawing.Point(124, 11);
            this.ExportNow.Name = "ExportNow";
            this.ExportNow.Size = new System.Drawing.Size(98, 23);
            this.ExportNow.TabIndex = 1;
            this.ExportNow.Text = "&Exportar Ahora";
            this.ExportNow.UseVisualStyleBackColor = true;
            this.ExportNow.Click += new System.EventHandler(this.ExportNow_Click);
            // 
            // ImportNow
            // 
            this.ImportNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportNow.Location = new System.Drawing.Point(12, 11);
            this.ImportNow.Name = "ImportNow";
            this.ImportNow.Size = new System.Drawing.Size(103, 23);
            this.ImportNow.TabIndex = 0;
            this.ImportNow.Text = "I&mportar Ahora";
            this.ImportNow.UseVisualStyleBackColor = true;
            this.ImportNow.Click += new System.EventHandler(this.ImportNow_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ViewMonitorTraces);
            this.panel3.Controls.Add(this.ViewSynchronizerTraces);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.LastExport);
            this.panel3.Controls.Add(this.HoursPlanOut);
            this.panel3.Controls.Add(this.DaysPlanOut);
            this.panel3.Controls.Add(this.LastImport);
            this.panel3.Controls.Add(this.HoursPlanIn);
            this.panel3.Controls.Add(this.DaysPlanIn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 125);
            this.panel3.TabIndex = 0;
            // 
            // ViewMonitorTraces
            // 
            this.ViewMonitorTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewMonitorTraces.Location = new System.Drawing.Point(597, 94);
            this.ViewMonitorTraces.Name = "ViewMonitorTraces";
            this.ViewMonitorTraces.Size = new System.Drawing.Size(174, 23);
            this.ViewMonitorTraces.TabIndex = 10;
            this.ViewMonitorTraces.Text = "Ver Trazas internas del Monitor";
            this.ViewMonitorTraces.UseVisualStyleBackColor = true;
            this.ViewMonitorTraces.Click += new System.EventHandler(this.ViewMonitorTraces_Click);
            // 
            // ViewSynchronizerTraces
            // 
            this.ViewSynchronizerTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewSynchronizerTraces.Location = new System.Drawing.Point(390, 94);
            this.ViewSynchronizerTraces.Name = "ViewSynchronizerTraces";
            this.ViewSynchronizerTraces.Size = new System.Drawing.Size(201, 23);
            this.ViewSynchronizerTraces.TabIndex = 9;
            this.ViewSynchronizerTraces.Text = "Ver Trazas internas del Sincronizador";
            this.ViewSynchronizerTraces.UseVisualStyleBackColor = true;
            this.ViewSynchronizerTraces.Click += new System.EventHandler(this.ViewSynchronizerTraces_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Histórico de Sincronizaciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(388, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Días de Exportación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Días de Importación:";
            // 
            // LastExport
            // 
            this.LastExport.AutoSize = true;
            this.LastExport.Location = new System.Drawing.Point(388, 75);
            this.LastExport.Name = "LastExport";
            this.LastExport.Size = new System.Drawing.Size(133, 13);
            this.LastExport.TabIndex = 5;
            this.LastExport.Text = "Última Exportación OK: {0}";
            // 
            // HoursPlanOut
            // 
            this.HoursPlanOut.AutoSize = true;
            this.HoursPlanOut.Location = new System.Drawing.Point(388, 53);
            this.HoursPlanOut.Name = "HoursPlanOut";
            this.HoursPlanOut.Size = new System.Drawing.Size(129, 13);
            this.HoursPlanOut.TabIndex = 4;
            this.HoursPlanOut.Text = "Horas de Exportación: {0}";
            // 
            // DaysPlanOut
            // 
            this.DaysPlanOut.AutoSize = true;
            this.DaysPlanOut.Location = new System.Drawing.Point(388, 31);
            this.DaysPlanOut.Name = "DaysPlanOut";
            this.DaysPlanOut.Size = new System.Drawing.Size(21, 13);
            this.DaysPlanOut.TabIndex = 3;
            this.DaysPlanOut.Text = "{0}";
            // 
            // LastImport
            // 
            this.LastImport.AutoSize = true;
            this.LastImport.Location = new System.Drawing.Point(5, 75);
            this.LastImport.Name = "LastImport";
            this.LastImport.Size = new System.Drawing.Size(132, 13);
            this.LastImport.TabIndex = 2;
            this.LastImport.Text = "Última Importación OK: {0}";
            // 
            // HoursPlanIn
            // 
            this.HoursPlanIn.AutoSize = true;
            this.HoursPlanIn.Location = new System.Drawing.Point(5, 53);
            this.HoursPlanIn.Name = "HoursPlanIn";
            this.HoursPlanIn.Size = new System.Drawing.Size(128, 13);
            this.HoursPlanIn.TabIndex = 1;
            this.HoursPlanIn.Text = "Horas de Importación: {0}";
            // 
            // DaysPlanIn
            // 
            this.DaysPlanIn.AutoSize = true;
            this.DaysPlanIn.Location = new System.Drawing.Point(5, 31);
            this.DaysPlanIn.Name = "DaysPlanIn";
            this.DaysPlanIn.Size = new System.Drawing.Size(21, 13);
            this.DaysPlanIn.TabIndex = 0;
            this.DaysPlanIn.Text = "{0}";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ConsoleLogtextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 292);
            this.panel1.TabIndex = 2;
            // 
            // ConsoleLogtextBox
            // 
            this.ConsoleLogtextBox.BackColor = System.Drawing.SystemColors.Info;
            this.ConsoleLogtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLogtextBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleLogtextBox.Multiline = true;
            this.ConsoleLogtextBox.Name = "ConsoleLogtextBox";
            this.ConsoleLogtextBox.ReadOnly = true;
            this.ConsoleLogtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConsoleLogtextBox.Size = new System.Drawing.Size(784, 292);
            this.ConsoleLogtextBox.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Main";
            this.Text = "Mega - Monitor de Sincronización";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ImportNow;
        private System.Windows.Forms.Button ExportNow;
        private System.Windows.Forms.Button StartService;
        private System.Windows.Forms.Button StopService;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label DaysPlanIn;
        private System.Windows.Forms.Label LastImport;
        private System.Windows.Forms.Label HoursPlanIn;
        private System.Windows.Forms.Label LastExport;
        private System.Windows.Forms.Label HoursPlanOut;
        private System.Windows.Forms.Label DaysPlanOut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ConsoleLogtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ShopComboBox;
        private System.Windows.Forms.Button ViewSynchronizerTraces;
        private System.Windows.Forms.Button ChangeExportDateButton;
        private System.Windows.Forms.Button ViewMonitorTraces;
    }
}

