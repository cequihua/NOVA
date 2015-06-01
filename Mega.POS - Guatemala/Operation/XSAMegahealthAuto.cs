using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Client;
using Mega.POS.Helper;
using Mega.POS.Movement;
using Mega.POS.Properties;

namespace Mega.POS.Operation
{
    public class XSAMegahealthAuto
    {
        private string pathFile = "";
        private string shopName = "";
        private string documentName = "";
        private int idInvoice = 0;
        private bool errorData;
        private bool failConection;
        private string errorDataDescription = "";
        private bool statusDoc;

        private string RFCCompany = "";
        private string key = "";
        private string tipoComprobante = "";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(InvoicesOperations));
        private  AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private mx.com.redmega.cfdi.FileReceiverService XSAService = null;

        public bool ErrorData { get { return errorData; } }
        public bool FailConection { get { return failConection; } }
        public string ErroDataDescription { get { return errorDataDescription; } }
        public bool StatusDoc { get { return statusDoc; } }

        private string _msgError = "";
        public string MsgError { get { return _msgError; } }

        public XSAMegahealthAuto(string _pathFile, string _shopName, string _documentName, int _idInvoice)
        {
            this.pathFile = _pathFile;
            this.shopName = _shopName;
            this.documentName = _documentName;
            this.idInvoice = _idInvoice;

            XSAService = new mx.com.redmega.cfdi.FileReceiverService();
            XSAService.obtenerEstadoDocumentoCompleted+=new mx.com.redmega.cfdi.obtenerEstadoDocumentoCompletedEventHandler(XSAService_obtenerEstadoDocumentoCompleted);
            XSAService.guardarDocumentoCompleted+=new mx.com.redmega.cfdi.guardarDocumentoCompletedEventHandler(XSAService_guardarDocumentoCompleted);
        }

        public void SendToXSA()
        {
            try
            {
                this.RFCCompany = DataHelper.GetRFCCompany(dc);
                this.key = DataHelper.GetTokenXSA(dc);
                this.tipoComprobante = DataHelper.GetTipoComprobanteFiscal(dc);

                System.Net.WebRequest webRequest;
                System.Net.HttpWebResponse webResponse;

                /* ANTES DE ENVIAR SE VERIFICA EL ESTADO DE CONEXION AL WEB SERVICE XSA*/
                try
                {
                    webRequest = System.Net.HttpWebRequest.Create(new Uri(Properties.Settings.Default.Mega_POS_mx_com_redmega_cfdi_FileReceiverService));

                    webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                    if (webResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        failConection = false;
                        //OBTENER EL CONTENIDO DEL ARCHIVO DE TEXTO
                        string contenTxt = System.IO.File.ReadAllText(this.pathFile, Encoding.Default);
                        //BORRAMOS EL ARCHIVO DE TEXTO
                        System.IO.File.Delete(this.pathFile);
                        //ENVIAR A XSA
                        XSAService.guardarDocumento(this.key + "-" + this.RFCCompany, this.shopName, tipoComprobante, this.documentName, contenTxt);

                        int intentos = 1;

                        while (intentos <= 3)
                        {
                            System.Threading.Thread.Sleep(5000);

                            string status = XSAService.obtenerEstadoDocumento(RFCCompany, this.documentName, key);
                            string[] aStatus = null;

                            if (status.Contains("ERROR"))
                                aStatus = status.Split('|');

                            if (status == "GENERADO")
                            {
                                //SI SE GENERO CORRECTAMENTE ACTUALIZAMOS EL IdStatusXSA EN TABLA Invoices a Generado
                                this.statusDoc = true;
                                int statusInvFac = DataHelper.GetUDCItemIdStatusXSAFacturadoInvoice(dc);
                                Invoices oInvoice = dc.Invoices.Single(o => o.IdInvoice == this.idInvoice);
                                oInvoice.IdStatusXSA = statusInvFac;
                                dc.SubmitChanges();
                                break;
                            }
                            else if (status.Contains("ERROR"))
                            {
                                statusDoc = false;
                                this.errorDataDescription = aStatus[1];
                                break;
                            }

                            intentos++;
                        }
                    }
                    else
                    {
                        Logger.Error("Error al intentar generar la factura en XSA " + webResponse.StatusDescription);
                        this.failConection = true;
                    }
                }
                catch (System.Net.WebException wex)
                {
                    Logger.Error("Error al intentar generar la factura en XSA " + wex.Message);
                    this.failConection = true;
                }
            }
            catch (Exception ex)
            {
                this.errorData = true;
                this._msgError = ex.Message;
                Logger.Error("Error al intentar generar la factura en XSA " + ex.Message);
            }
        }

        private void XSAService_guardarDocumentoCompleted(object sender, EventArgs e)
        {
            //XSAService.obtenerEstadoDocumentoAsync(this.RFCCompany, this.documentName, this.key);
        }

        private void XSAService_obtenerEstadoDocumentoCompleted(object sender, mx.com.redmega.cfdi.obtenerEstadoDocumentoCompletedEventArgs e)
        {
            //if (e.Result == "GENERADO")
            //{
            //    //SI SE GENERO CORRECTAMENTE ACTUALIZAMOS EL IdStatusXSA EN TABLA Invoices a Generado
            //    this.statusDoc = true;
            //    int statusInvFac = DataHelper.GetUDCItemIdStatusXSAFacturadoInvoice(dc);
            //    Invoices oInvoice = dc.Invoices.Single(o => o.IdInvoice == this.idInvoice);
            //    oInvoice.IdStatusXSA = statusInvFac;
            //    dc.SubmitChanges();

            //    Logger.Info("La factura " + this.idInvoice.ToString() + " se genero correctamente en XSA");
            //}
            //else if (e.Result.Contains("ERROR"))
            //{
            //    this.statusDoc = false;
            //    this.errorDataDescription = e.Result;
            //    Logger.Error(e.Result);
            //}
        }
    
    }
}
