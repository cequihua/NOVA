using System;
using System.Windows.Forms;
using Mega.Common.Enum;
using Mega.POS.Helper;
using System.Linq;
using Microsoft.Reporting.WinForms;

namespace Mega.POS
{
    public partial class Test : Form
    {
        private Guid operationId = new Guid("2b9f5682-65d7-4861-8c4d-77fa8e3a9419");
        private OperationType operationType = OperationType.Sale;

        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            rViewer.ProcessingMode = ProcessingMode.Local;
            rViewer.LocalReport.ReportEmbeddedResource = "Mega.POS.Report.SaleTicket.rdlc";
            rViewer.LocalReport.SubreportProcessing += MySubreportEventHandler;

            ReportDataSource ds1 = new ReportDataSource("Sale");
            ds1.Value = ApplicationHelper.GetPosDataContext().Operations.Where(o => o.Id == operationId).Select(o => o);
            rViewer.LocalReport.DataSources.Add(ds1);

            //rViewer.SetPageSettings(ReportPrintDocument.GetTicketPrinterMargins());
            rViewer.RefreshReport();

            //if (currentItem.Dim.IdType == (int)DimType.Employment)
            //{
            //    saleReport.SetParameters(new ReportParameter("EmployeeSaleNote", currentItem.Dim.Type.Optional4));
            //}

            //ReportPrintDocument rp = new ReportPrintDocument(saleReport);
            //rp.Print();
        }

        void MySubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "SaleDetailSubReport")
            {
                ReportDataSource ds2 = new ReportDataSource("SaleDetail");
                ds2.Value = ApplicationHelper.GetPosDataContext().OperationDetails.Where(o => o.Operation.Id == operationId).Select(o => o);
                e.DataSources.Add(ds2);
            }

            if (operationType == OperationType.Sale)
            {
                if (e.ReportPath == "OperationPay")
                {
                    ReportDataSource ds3 = new ReportDataSource("OperationPay");
                    ds3.Value = ApplicationHelper.GetPosDataContext().Operation_Pays.Where(o => o.Operation.Id == operationId).Select(o => o);
                    e.DataSources.Add(ds3);
                }
                else if (e.ReportPath == "SalePayNotes")
                {
                    ReportDataSource ds4 = new ReportDataSource("SalePayNotes");
                    var tmp = ApplicationHelper.GetPosDataContext().Operation_Pays.Where(o => o.Operation.Id == operationId).
                            Select(o => o.UDCItem1).GroupBy(u => u.Id).Select(g => g.First());

                    ds4.Value = tmp;
                    e.DataSources.Add(ds4);
                }
            }
        }
    }
}
