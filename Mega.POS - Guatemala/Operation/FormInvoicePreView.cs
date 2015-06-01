using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using Microsoft.Reporting.WinForms;
using Mega.POS.Helper;
using Mega.POS.Report;
using Mega.Common.Helpers;

namespace Mega.POS.Operation
{
    public partial class FormInvoicePreView : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FormInvoicePreView));
        private Guid _idOperation;

        public Guid IdOperation { get { return _idOperation; } set { _idOperation = value; } }
        
        public FormInvoicePreView(Guid idOperation)
        {
            _idOperation = idOperation;
            InitializeComponent();
        }

        private void FormInvoicePreView_Load(object sender, EventArgs e)
        {
            CargaReporte();
        }

        private void CargaReporte()
        {
            try
            {
                rptViewerInvoice.ProcessingMode = ProcessingMode.Local;
                rptViewerInvoice.LocalReport.ReportEmbeddedResource = "Mega.POS.Report.InvoicePrewView.rdlc";

                ReportDataSource ds1 = new ReportDataSource("dsVwInvoicePreView")
                {
                    Value =
                       ApplicationHelper.GetPosDataContext().vwInvoicePreView.Where(
                                vwo => vwo.idOperation == IdOperation),
                    Name = "vwInvoicePreView"
                };

                rptViewerInvoice.LocalReport.DataSources.Add(ds1);
                rptViewerInvoice.LocalReport.Refresh();
                rptViewerInvoice.RefreshReport();
            }
            catch (Exception ex)
            {
                Logger.Error("No fue posible cargar la vista previa de la factura", ex);
                DialogHelper.ShowError(this, "Error al mostrar la vista previa de la factura", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
