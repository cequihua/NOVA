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
    public class InvoicesOperations
    {
        private int idInvoice = 0;
        private string pathFile = "";
        private string documentName = "";
        private string shopName = "";
        private string serieFolio = "";
        Guid idOperation;
        string idShop = "";
        int optTypeInvoice = 0;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InvoicesOperations));
        protected AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        protected mx.com.redmega.cfdi.FileReceiverService XSAService = null;
        //protected XSAMegahealthService.FileReceiverService XSAMega = null;
        private bool foliosAl75porciento;
        public bool FoliosAl75porciento { get { return foliosAl75porciento; } set { foliosAl75porciento = value; } }

        private string _msgError = "";

        public InvoicesOperations(Guid _idOperation, string _idShop, int _optTypeInvoice)
        {
            this.idOperation = _idOperation;
            this.idShop = _idShop;
            this.optTypeInvoice = _optTypeInvoice;
            XSAService = new mx.com.redmega.cfdi.FileReceiverService();
            //XSAService.Timeout = 10000;

            //XSAMegahealthService.FileReceiverService xsa = new XSAMegahealthService.FileReceiverService();

            //XSAMega = new XSAMegahealthService.FileReceiverService();
            //XSAService.guardarDocumentoCompleted +=new mx.com.redmega.cfdi.guardarDocumentoCompletedEventHandler(XSAService_guardarDocumentoCompleted);
        }

        public string msgError { get { return _msgError; } set { _msgError = value; } }
        public int IdInvoice { get { return idInvoice; } set { idInvoice = value; } }
        public string PathFile { get { return pathFile; } }
        public string DocumentName { get { return documentName; } }
        public string ShopName { get { return shopName; } set { shopName = value; } }
        public string SerieFolio { get { return serieFolio; } set { serieFolio = value; } }

        public void GenerateInvoiceAndFileTxt()
        {
            try
            {
                FiscalDataShop oFShop = dc.FiscalDataShop.SingleOrDefault();
                Shop oShop = dc.Shops.Single(o => o.Id == this.idShop);
                this.shopName = oShop.Name;

                int typeInvIndividual = DataHelper.GetUDCItemIdTypeInvoiceIndividual(dc);
                int typeInvGeneral = DataHelper.GetUDCItemIdTypeInvoiceGeneral(dc);
                int statusInvPend = DataHelper.GetUDCItemIdStatusXSAPendienteInvoice(dc);
                //string serie = DataHelper.GetSerieInvoice(dc);
                string serie = oFShop.Serie;
                //int statusInvFac = DataHelper.GetUDCItemIdStatusXSAFacturadoInvoice(dc);
                int typeInvAsigned = 0;
                string folioAsign = "";

                // VALIDAMOS SI YA EXISTE EL idOperation en Invoices para no volver a crear la factura
                int iObj = (from o in dc.Invoices where o.IdOperation == this.idOperation select o).Count();

                if (iObj == 0)
                {
                    //OBTENER LA FECHA DE CREACION DEL TICKET
                    DateTime emissionDate = dc.Operations.Where(o => o.Id == this.idOperation).Select(o => o.AddedDate).Single();
                    using (TransactionScope trans = new TransactionScope())
                    {
                        typeInvAsigned = typeInvIndividual;
                        // AUMENTAMOS EL FOLIO A 1
                        //oShop.String1 = (Convert.ToInt32(oShop.String1) + 1).ToString();
                        oFShop.Folio = (Convert.ToInt32(oFShop.Folio) + 1).ToString();
                        //dc.SubmitChanges();
                        folioAsign = oFShop.Folio;

                        //if (this.optTypeInvoice == 1)
                        //{
                        //    typeInvAsigned = typeInvIndividual;
                        //    // AUMENTAMOS EL FOLIO A 1
                        //    oShop.String1 = (Convert.ToInt32(oShop.String1) + 1).ToString();

                        //    //dc.SubmitChanges();
                        //    folioAsign = oShop.String1;
                        //}
                        //else
                        //{
                        //    typeInvAsigned = typeInvGeneral;
                        //    folioAsign = "0";
                        //}

                        //CREAR LA FACTURA EN LA TABLA Invoices
                        Invoices oInvoice = new Invoices
                        {
                            IdOperation = this.idOperation,
                            IdTypeInvoice = typeInvAsigned,
                            IdStatusXSA = statusInvPend,
                            AddedDate = DateTime.Now,
                            EmissionDate = emissionDate,
                            Folio = folioAsign
                        };

                        dc.Invoices.InsertOnSubmit(oInvoice);

                        //OBTENEMOS LA OPERATION PARA ACTUALIZAR EL FOLIO
                        //if (this.optTypeInvoice == 1)
                        //{
                        //    Common.Operation op = dc.Operations.Where(o => o.Id == this.idOperation).Single();
                        //    op.Reference = string.Format("{0}-{1}", serie, folioAsign);
                        //}

                        Common.Operation op = dc.Operations.Where(o => o.Id == this.idOperation).Single();
                        op.Reference = string.Format("{0}-{1}", serie, folioAsign);

                        ResolutionFiscal res = dc.ResolutionFiscal.Single(o => o.Active == true);
                        int r = (int)res.RangeFinal;
                        double porcFolios = Convert.ToDouble(r) * 0.75;

                        if (res.RangeFinal == Convert.ToInt32(oFShop.Folio))
                            res.Active = false;
                        else if (Convert.ToInt32(oFShop.Folio) >= porcFolios)
                            FoliosAl75porciento = true;

                        dc.SubmitChanges();

                        this.idInvoice = oInvoice.IdInvoice;

                        trans.Complete();
                    }
                }
                else
                {
                    this.idInvoice = dc.Invoices.Single(o => o.IdOperation == this.idOperation).IdInvoice;
                }

                //if (optTypeInvoice == 1)
                //{
                //    //CREAR EL ARCHIVO DE TEXTO SEPARADO POR PIPES SI DECIDIO FACTURAR
                //    if (!CreateFileTXT(this.idInvoice))
                //        throw new Exception("Error al generar el archivo de texto de la factura numero " + idInvoice);
                //}
            }
            catch (Exception ex)
            {
                _msgError = ex.Message;
            }
        }

        public bool CreateFileTXT(int idInvoice)
        {
            this.pathFile = AppDomain.CurrentDomain.BaseDirectory + @"InvoiceTemp\";
            int numberLines = 0;
            String contenidoTXT = "";
            bool generado = false;

            try
            {
                var invDetailTXT = dc.GetInvoiceDetailTXT(this.idInvoice);
                var invLineas = dc.GetInvoiceDetailTXT(this.idInvoice);
                GetInvoiceDetailTXTResult vwInvoiceDetail = invDetailTXT.Take(1).Single(); //invDetailTXT.Single(o => o.IdInvoice == idInvoice);

                this.documentName = vwInvoiceDetail.idArchivo;
                this.serieFolio = vwInvoiceDetail.serie + vwInvoiceDetail.Folio;
                /* INICIO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegINICFDI + "|" + vwInvoiceDetail.idArchivo + "|" + vwInvoiceDetail.etiquetaPlantilla + "|" + Environment.NewLine;

                /* ENCABEZADO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipopRegENCCFDI + "|" + vwInvoiceDetail.idUnicoCFD + "|" + vwInvoiceDetail.serie + "|" + vwInvoiceDetail.Folio + "|" + vwInvoiceDetail.fechaEmision.ToString("yyyy-MM-ddThh:mm:ss") + "|" + vwInvoiceDetail.subtotal.ToString() + "|" + vwInvoiceDetail.total.ToString() + "|" + vwInvoiceDetail.totalImpuestoTrasladado.ToString() + "|" + vwInvoiceDetail.totalImpuestoRetenido.ToString() + "|" + vwInvoiceDetail.descuento + "|" + vwInvoiceDetail.motivoDescuento + "|" + vwInvoiceDetail.totalLetra + "|" + vwInvoiceDetail.moneda + "|" + vwInvoiceDetail.tipoCambio + "|" + vwInvoiceDetail.idDim + "|" + vwInvoiceDetail.SubtotalGravado + "|" + vwInvoiceDetail.Domicilio + "|" + vwInvoiceDetail.observaciones3 + "|" + vwInvoiceDetail.telefono + "|" + vwInvoiceDetail.Fax + "|" + vwInvoiceDetail.modoentrega + "|" + Environment.NewLine;

                /* DATOS DEL PAGO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegDatosPagoCFDI + "|" + vwInvoiceDetail.formapago + "|" + vwInvoiceDetail.condicionesPago + "|" + vwInvoiceDetail.metodoPago + "|" + /*Convert.ToDateTime(vwInvoiceDetail.fechaVencimiento).ToString("yyyy/MM/dd") +*/ "|" + vwInvoiceDetail.observacionesPago + "|" + vwInvoiceDetail.banco + "|" + Environment.NewLine;

                /* DATOS DEL RECEPTOR DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegReceptorCFDI + "|" + vwInvoiceDetail.idUnicoReceptor + "|" + vwInvoiceDetail.RFCReceptor + "|" + vwInvoiceDetail.NombreReceptor + "|" + vwInvoiceDetail.paisReceptor.ToUpper() + "|" + vwInvoiceDetail.calleReceptor + "|" + vwInvoiceDetail.numeroExtReceptor + "|" + vwInvoiceDetail.numeroIntReceptor + "|" + vwInvoiceDetail.coloniaReceptor + "|" + vwInvoiceDetail.localidadReceptor + "|" + vwInvoiceDetail.referenciaReceptor + "|" + vwInvoiceDetail.municipioReceptor + "|" + vwInvoiceDetail.estadoReceptor + "|" + vwInvoiceDetail.CPReceptor + "|" + vwInvoiceDetail.taxIdReceptor + "|" + Environment.NewLine;

                /* DATOS DE LA DIRRECCION DE EMBARQUE */
                contenidoTXT += vwInvoiceDetail.tipoRegEmbarque + "|" + vwInvoiceDetail.idUnicoReceptorEnbarque + "|" + vwInvoiceDetail.NombreEmbarque + "|" + vwInvoiceDetail.paisEmbarque.ToUpper() + "|" + vwInvoiceDetail.calleEmbarque + "|" + vwInvoiceDetail.numeroExtEmbarque + "|" + vwInvoiceDetail.numeroIntEmbarque + "|" + vwInvoiceDetail.coloniaEmbarque + "|" + vwInvoiceDetail.localidadEmbarque + "|" + vwInvoiceDetail.referenciaEmbarque + "|" + vwInvoiceDetail.municipioEmbarque + "|" + vwInvoiceDetail.estadoEmbarque + "|" + vwInvoiceDetail.CPEmbarque + "|" + Environment.NewLine;

                /* OBTENER LAS LINEAS DEL PEDIDO */
                foreach (var lin in invLineas)
                {
                    contenidoTXT += vwInvoiceDetail.tipoRegConceptos + "|" + lin.idUnicoIntConcepto + "|" + lin.idUnicoExtConcepto + "|" + lin.cantidadConcepto.ToString() + "|" + lin.descripcionConcepto + "|" + lin.valorUnitarioConcepto.ToString() + "|" + lin.importeConcepto.ToString() + "|" + lin.unidadMedidaConcepto.ToString() + "|" + lin.categoriaConcepto + "|" + Environment.NewLine;
                    numberLines++;
                }

                /* OTROS IMPUESTOS */
                contenidoTXT += vwInvoiceDetail.tipoRegOtrosImpuestos + "|" + vwInvoiceDetail.impuestoOtrosImpuestos + "|" + vwInvoiceDetail.tasaotrosImpuestos.ToString() + "|" + vwInvoiceDetail.importeOtrosImpuestos + "|" + Environment.NewLine;

                /* ENVIO AUTOMATICO */
                contenidoTXT += vwInvoiceDetail.tipoRegEnvio + "|" + vwInvoiceDetail.idUnicoIntEnvio + "|" + vwInvoiceDetail.Email + "|" + vwInvoiceDetail.Asunto + "|" + vwInvoiceDetail.Mensaje + "|" + vwInvoiceDetail.Adjunto + "|" + Environment.NewLine;

                /* FIN DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegFinCFDI + "|" + (numberLines + 8).ToString() + "|";

                if (!System.IO.Directory.Exists(this.pathFile))
                    System.IO.Directory.CreateDirectory(this.pathFile);

                this.pathFile = this.pathFile + vwInvoiceDetail.idArchivo + ".txt";
                System.IO.FileStream strreamFile = System.IO.File.Create(this.pathFile);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strreamFile, System.Text.Encoding.Default);

                sw.Write(contenidoTXT);
                sw.Close();

                generado = true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error al generar el archivo de texto de la factura " + idInvoice.ToString(), ex);
            }

            return generado;
        }

        public bool CreateFileTXTInvoiceGeneral(Guid idCasherClose)
        {
            this.pathFile = AppDomain.CurrentDomain.BaseDirectory + @"InvoiceTemp\";
            int numberLines = 0;
            String contenidoTXT = "";
            bool generado = false;

            try
            {
                int typeInvGeneral = DataHelper.GetUDCItemIdTypeInvoiceGeneral(dc);
                int statusInvPend = DataHelper.GetUDCItemIdStatusXSAPendienteInvoice(dc);

                var invDetailTXT = dc.GetInvoiceGeneralByCasherCloseDetailTXT(idCasherClose, typeInvGeneral, statusInvPend);
                var invLineas = dc.GetInvoiceGeneralByCasherCloseDetailTXT(idCasherClose, typeInvGeneral, statusInvPend);
                GetInvoiceGeneralByCasherCloseDetailTXTResult vwInvoiceDetail = invDetailTXT.Take(1).Single(); //invDetailTXT.Single(o => o.IdInvoice == idInvoice);

                this.documentName = vwInvoiceDetail.idArchivo;
                this.serieFolio = vwInvoiceDetail.serie + vwInvoiceDetail.Folio;

                /* INICIO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegINICFDI + "|" + vwInvoiceDetail.idArchivo + "|" + vwInvoiceDetail.etiquetaPlantilla + "|" + Environment.NewLine;

                /* ENCABEZADO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipopRegENCCFDI + "|" + vwInvoiceDetail.idUnicoCFD + "|" + vwInvoiceDetail.serie + "|" + vwInvoiceDetail.Folio + "|" + vwInvoiceDetail.fechaEmision + "|" + vwInvoiceDetail.subtotal.ToString() + "|" + vwInvoiceDetail.total.ToString() + "|" + vwInvoiceDetail.totalImpuestoTrasladado.ToString() + "|" + vwInvoiceDetail.totalImpuestoRetenido.ToString() + "|" + vwInvoiceDetail.descuento + "|" + vwInvoiceDetail.motivoDescuento + "|" + vwInvoiceDetail.totalLetra + "|" + vwInvoiceDetail.moneda + "|" + vwInvoiceDetail.tipoCambio + "|" + vwInvoiceDetail.idDim + "|" + vwInvoiceDetail.SubtotalGravado + "|" + vwInvoiceDetail.Domicilio + "|" + vwInvoiceDetail.observaciones3 + "|" + vwInvoiceDetail.telefono + "|" + vwInvoiceDetail.Fax + "|" + vwInvoiceDetail.modoentrega + "|" + Environment.NewLine;

                /* DATOS DEL PAGO DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegDatosPagoCFDI + "|" + vwInvoiceDetail.formapago + "|" + vwInvoiceDetail.condicionesPago + "|" + vwInvoiceDetail.metodoPago + "|" + /*Convert.ToDateTime(vwInvoiceDetail.fechaVencimiento).ToString("yyyy/MM/dd") +*/ "|" + vwInvoiceDetail.observacionesPago + "|" + vwInvoiceDetail.banco + "|" + Environment.NewLine;

                /* DATOS DEL RECEPTOR DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegReceptorCFDI + "|" + vwInvoiceDetail.idUnicoReceptor + "|" + vwInvoiceDetail.RFCReceptor + "|" + vwInvoiceDetail.NombreReceptor + "|" + vwInvoiceDetail.paisReceptor.ToUpper() + "|" + vwInvoiceDetail.calleReceptor + "|" + vwInvoiceDetail.numeroExtReceptor + "|" + vwInvoiceDetail.numeroIntReceptor + "|" + vwInvoiceDetail.coloniaReceptor + "|" + vwInvoiceDetail.localidadReceptor + "|" + vwInvoiceDetail.referenciaReceptor + "|" + vwInvoiceDetail.municipioReceptor + "|" + vwInvoiceDetail.estadoReceptor + "|" + vwInvoiceDetail.CPReceptor + "|" + vwInvoiceDetail.taxIdReceptor + "|" + Environment.NewLine;

                /* DATOS DE LA DIRRECCION DE EMBARQUE */
                contenidoTXT += vwInvoiceDetail.tipoRegEmbarque + "|" + vwInvoiceDetail.idUnicoReceptorEnbarque + "|" + vwInvoiceDetail.NombreEmbarque + "|" + vwInvoiceDetail.paisEmbarque.ToUpper() + "|" + vwInvoiceDetail.calleEmbarque + "|" + vwInvoiceDetail.numeroExtEmbarque + "|" + vwInvoiceDetail.numeroIntEmbarque + "|" + vwInvoiceDetail.coloniaEmbarque + "|" + vwInvoiceDetail.localidadEmbarque + "|" + vwInvoiceDetail.referenciaEmbarque + "|" + vwInvoiceDetail.municipioEmbarque + "|" + vwInvoiceDetail.estadoEmbarque + "|" + vwInvoiceDetail.CPEmbarque + "|" + Environment.NewLine;

                /* OBTENER LAS LINEAS DEL PEDIDO */
                foreach (var lin in invLineas)
                {
                    contenidoTXT += vwInvoiceDetail.tipoRegConceptos + "|" + lin.idUnicoIntConcepto + "|" + lin.idUnicoExtConcepto + "|" + lin.cantidadConcepto.ToString() + "|" + lin.descripcionConcepto + "|" + lin.valorUnitarioConcepto.ToString() + "|" + lin.importeConcepto.ToString() + "|" + lin.unidadMedidaConcepto.ToString() + "|" + lin.categoriaConcepto + "|" + Environment.NewLine;
                    numberLines++;
                }

                /* OTROS IMPUESTOS */
                contenidoTXT += vwInvoiceDetail.tipoRegOtrosImpuestos + "|" + vwInvoiceDetail.impuestoOtrosImpuestos + "|" + vwInvoiceDetail.tasaotrosImpuestos.ToString() + "|" + vwInvoiceDetail.importeOtrosImpuestos + "|" + Environment.NewLine;

                /* ENVIO AUTOMATICO */
                contenidoTXT += vwInvoiceDetail.tipoRegEnvio + "|" + vwInvoiceDetail.idUnicoIntEnvio + "|" + vwInvoiceDetail.Email + "|" + vwInvoiceDetail.Asunto + "|" + vwInvoiceDetail.Mensaje + "|" + vwInvoiceDetail.Adjunto + "|" + Environment.NewLine;

                /* FIN DEL CFD */
                contenidoTXT += vwInvoiceDetail.tipoRegFinCFDI + "|" + (numberLines + 8).ToString() + "|";

                if (!System.IO.Directory.Exists(this.pathFile))
                    System.IO.Directory.CreateDirectory(this.pathFile);

                this.pathFile = this.pathFile + vwInvoiceDetail.idArchivo + ".txt";
                System.IO.FileStream strreamFile = System.IO.File.Create(this.pathFile);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strreamFile, System.Text.Encoding.Default);

                sw.Write(contenidoTXT);
                sw.Close();

                generado = true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error al generar el archivo de texto de la factura " + idInvoice.ToString(), ex);
            }

            return generado;
        }

        public void SendToXSA(ref bool errorFiscalData, ref bool failConection, ref string errorFiscalDataDescription, ref bool statusDoc, string idCasherClose = "", int typeInvoice = 1)
        {
            try
            {
                string RFCCompany = DataHelper.GetRFCCompany(dc);
                string key = DataHelper.GetTokenXSA(dc);
                string tipoComprobante = DataHelper.GetTipoComprobanteFiscal(dc);
                int statusInvFac = DataHelper.GetUDCItemIdStatusXSAFacturadoInvoice(dc);
                //int statusInvPend = DataHelper.GetUDCItemIdStatusXSAPendienteInvoice(dc);
                Invoices oInvoice = null;

                if (typeInvoice == 1)
                    oInvoice = dc.Invoices.Single(o => o.IdInvoice == this.idInvoice);
                //else
                //    oInvoice = dc.Invoices.Where(o => o.IdCasherClose == new Guid(idCasherClose)).First();

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
                        String contenTxt = System.IO.File.ReadAllText(this.pathFile, Encoding.Default);
                        //BORRAMOS EL ARCHIVO DE TEXTO
                        System.IO.File.Delete(this.pathFile);
                        //ENVIAR A XSA
                        XSAService.guardarDocumento(key + "-" + RFCCompany, this.shopName, tipoComprobante, this.documentName, contenTxt);

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
                                statusDoc = true;
                          
                                if (typeInvoice == 1)
                                {
                                    //Invoices oInvoice = dc.Invoices.Single(o => o.IdInvoice == this.idInvoice);
                                    oInvoice.IdStatusXSA = statusInvFac;
                                    dc.SubmitChanges();
                                }
                                else
                                {
                                    //ACTUALIZAMOS EL ESTATUS A LAS FACTURAS GENERALES DE LA TABLA Invoices POR EL IdCassherClose DEL ULTIMO CORTE DEL DIA
                                    dc.UpdateIdStatusXSAInvoiceByIdCasherClose(new Guid(idCasherClose));
                                    dc.SubmitChanges();
                                }

                                break;
                            }
                            else if (status.Contains("ERROR"))
                            {
                                statusDoc = false;
                                errorFiscalDataDescription = aStatus[1];

                                if (typeInvoice == 1)
                                {
                                    //Invoices oInvoice = dc.Invoices.Single(o => o.IdInvoice == this.idInvoice);
                                    oInvoice.IdStatusXSA = statusInvFac;
                                    dc.SubmitChanges();
                                }
                                else
                                {
                                    //ACTUALIZAMOS EL ESTATUS A LAS FACTURAS GENERALES DE LA TABLA Invoices POR EL IdCassherClose DEL ULTIMO CORTE DEL DIA
                                    dc.UpdateIdStatusXSAInvoiceByIdCasherClose(new Guid(idCasherClose));
                                    dc.SubmitChanges();
                                }

                                break;
                            }

                            intentos++;
                        }

                        if (intentos > 3)
                        {
                            statusDoc = false;
                            errorFiscalDataDescription = "Demasiado tiempo de espera para generar la factura";
                            Logger.Error("Demasiado tiempo de espera para generar la factura " + SerieFolio + " en XSA");
                        }
                    }
                    else
                    {
                        Logger.Error("Error al intentar generar la factura " + SerieFolio + " en XSA " + webResponse.StatusDescription);
                        failConection = true;
                    }
                }
                catch (System.Net.WebException wex)
                {
                    Logger.Error("Error al intentar generar la factura " + SerieFolio + " en XSA " + wex);
                    failConection = true;
                }
            }
            catch (Exception ex)
            {
                errorFiscalData = true;
                errorFiscalDataDescription = ex.Message;
                Logger.Error("Error de datos fiscales de la factura " + SerieFolio + " en XSA " + ex);
            }
        }

        public void UpdateFolioAndIdCasherInvoiceGeneral(Guid idCasherClose)
        {
            dc.UpdateFolioIdCasherCloseInvoicesGeneral(idCasherClose);
        }
    }
}