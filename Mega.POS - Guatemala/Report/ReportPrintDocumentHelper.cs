//-----------------------------------------------------------------------
//  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Mega.POS.Report
{
    /// <summary>
    /// The ReportPrintDocument will print all of the pages of a ServerReport or LocalReport.
    /// The pages are rendered when the print document is constructed.  Once constructed,
    /// call Print() on this class to begin printing.
    /// </summary>
    public class ReportPrintDocument : PrintDocument
    {
        //private static readonly ILog Logger = LogManager.GetLogger(typeof(ReportPrintDocument));
        private PageSettings m_pageSettings;
        private int m_currentPage;
        private List<Stream> m_pages = new List<Stream>();
        private string m_pageSize;
        private string m_pageMargins; 

        public ReportPrintDocument(ServerReport serverReport, bool isTicket, string ticketPageSizes, string ticketPageMargins)
            : this((Microsoft.Reporting.WinForms.Report)serverReport, isTicket, ticketPageSizes, ticketPageMargins)
        {
            RenderAllServerReportPages(serverReport);
        }

        public ReportPrintDocument(LocalReport localReport, bool isTicket, string ticketPageSizes, string ticketPageMargins)
            : this((Microsoft.Reporting.WinForms.Report)localReport, isTicket, ticketPageSizes, ticketPageMargins)
        {
            RenderAllLocalReportPages(localReport);
        }

        private ReportPrintDocument(Microsoft.Reporting.WinForms.Report report, bool isTicket, string ticketPageSizes, string ticketPageMargins)
        {
            if (isTicket)
            {
                m_pageSize = ticketPageSizes;
                m_pageMargins = ticketPageMargins;
                m_pageSettings = GetTicketPrinterMargins();
            }
            else
            {
                // Set the page settings to the default defined in the report
                ReportPageSettings reportPageSettings = report.GetDefaultPageSettings();

                // The page settings object will use the default printer unless
                // PageSettings.PrinterSettings is changed.  This assumes there
                // is a default printer.
                m_pageSettings = new PageSettings();
                m_pageSettings.PaperSize = reportPageSettings.PaperSize;
                m_pageSettings.Margins = reportPageSettings.Margins;
            }
        }

        private PageSettings GetTicketPrinterMargins()
        {
            string[] margins = m_pageMargins.Split(',');
            string[] sizes = m_pageSize.Split(',');

            PageSettings ps = new PageSettings();
            ps.Margins = new Margins((int)(Convert.ToDouble(margins[0]) * 100),
                                     (int)(Convert.ToDouble(margins[1]) * 100),
                                     (int)(Convert.ToDouble(margins[2]) * 100),
                                     (int)(Convert.ToDouble(margins[3]) * 100));

            ps.PaperSize = new PaperSize("Custom", (int)(Convert.ToDouble(sizes[0]) * 100),
                                     (int)(Convert.ToDouble(sizes[1]) * 100));
            return ps;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                foreach (Stream s in m_pages)
                {
                    s.Dispose();
                }

                m_pages.Clear();
            }
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);

            m_currentPage = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            Stream pageToPrint = m_pages[m_currentPage];
            pageToPrint.Position = 0;

            // Load each page into a Metafile to draw it.
            using (Metafile pageMetaFile = new Metafile(pageToPrint))
            {
                Rectangle adjustedRect = new Rectangle(
                        e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                        e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                        e.PageBounds.Width,
                        e.PageBounds.Height);

                // Draw a white background for the report
                e.Graphics.FillRectangle(Brushes.White, adjustedRect);

                // Draw the report content
                e.Graphics.DrawImage(pageMetaFile, adjustedRect);

                // Prepare for next page.  Make sure we haven't hit the end.
                m_currentPage++;
                e.HasMorePages = m_currentPage < m_pages.Count;
            }
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            e.PageSettings = (PageSettings)m_pageSettings.Clone();
        }

        private void RenderAllServerReportPages(ServerReport serverReport)
        {
            string deviceInfo = CreateEMFDeviceInfo();

            // Generating Image renderer pages one at a time can be expensive.  In order
            // to generate page 2, the server would need to recalculate page 1 and throw it
            // away.  Using PersistStreams causes the server to generate all the pages in
            // the background but return as soon as page 1 is complete.
            NameValueCollection firstPageParameters = new NameValueCollection();
            firstPageParameters.Add("rs:PersistStreams", "True");

            // GetNextStream returns the next page in the sequence from the background process
            // started by PersistStreams.
            NameValueCollection nonFirstPageParameters = new NameValueCollection();
            nonFirstPageParameters.Add("rs:GetNextStream", "True");

            string mimeType;
            string fileExtension;
            Stream pageStream = serverReport.Render("IMAGE", deviceInfo, firstPageParameters, out mimeType, out fileExtension);

            // The server returns an empty stream when moving beyond the last page.
            while (pageStream.Length > 0)
            {
                m_pages.Add(pageStream);

                pageStream = serverReport.Render("IMAGE", deviceInfo, nonFirstPageParameters, out mimeType, out fileExtension);
            }
        }

        private void RenderAllLocalReportPages(LocalReport localReport)
        {
            string deviceInfo = CreateEMFDeviceInfo();

            Warning[] warnings;

            localReport.Render(Properties.Settings.Default.ReportRenderMethod, deviceInfo, LocalReportCreateStreamCallback, out warnings);
        }

        private Stream LocalReportCreateStreamCallback(
            string name,
            string extension,
            Encoding encoding,
            string mimeType,
            bool willSeek)
        {
            MemoryStream stream = new MemoryStream();
            m_pages.Add(stream);

            return stream;
        }

        private string CreateEMFDeviceInfo()
        {
            PaperSize paperSize = m_pageSettings.PaperSize;
            Margins margins = m_pageSettings.Margins;

            // The device info string defines the page range to print as well as the size of the page.
            // A start and end page of 0 means generate all pages.
            return string.Format(
                "<DeviceInfo><OutputFormat>{6}</OutputFormat><StartPage>0</StartPage><EndPage>0</EndPage><MarginTop>{0}</MarginTop><MarginLeft>{1}</MarginLeft><MarginRight>{2}</MarginRight><MarginBottom>{3}</MarginBottom><PageHeight>{4}</PageHeight><PageWidth>{5}</PageWidth></DeviceInfo>",
                ToInches(margins.Top),
                ToInches(margins.Left),
                ToInches(margins.Right),
                ToInches(margins.Bottom),
                ToInches(paperSize.Height),
                ToInches(paperSize.Width),
                Properties.Settings.Default.EMFDeviceInfoOutputFormat);
        }

        private static string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches + "in";
        }
    }
}
