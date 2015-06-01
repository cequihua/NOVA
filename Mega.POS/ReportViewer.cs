using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Mega.POS
{
    public partial class ReportViewer : Form
    {
        public LocalReport LocalReport
        {
            get { return reportViewer1.LocalReport; }
        }
        
        public MemoryStream RenderedReport
        {
            get
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                var bytes = LocalReport.Render("PDF", null, out mimeType,
                      out encoding, out extension, out streamids, out warnings);

                var memoryStream = new MemoryStream(bytes);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }
        }

        protected string ReportEmbeddedResource { get; set; }

        public ReportViewer(string reportEmbeddedResourceName)
        {
            InitializeComponent();

            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportEmbeddedResource = reportEmbeddedResourceName; 
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
