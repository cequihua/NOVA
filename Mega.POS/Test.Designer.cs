namespace Mega.POS
{
    partial class Test
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rViewer
            // 
            this.rViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ResumeAndDetail";
            reportDataSource1.Value = null;
            this.rViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rViewer.Location = new System.Drawing.Point(0, 0);
            this.rViewer.Name = "rViewer";
            this.rViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rViewer.ServerReport.ReportServerUrl = new System.Uri("", System.UriKind.Relative);
            this.rViewer.ShowStopButton = false;
            this.rViewer.Size = new System.Drawing.Size(308, 416);
            this.rViewer.TabIndex = 1;
            // 
            // Test
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(308, 416);
            this.Controls.Add(this.rViewer);
            this.Name = "Test";
            this.Text = "Visor de Ticket de Compra";
            this.Load += new System.EventHandler(this.Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rViewer;
    }
}