using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Client;
using Mega.POS.Helper;
using Mega.POS.Movement;
using Mega.POS.Properties;

namespace Mega.POS
{
    public class SyncroFacturacionXSA
    {
        private Timer timer = null;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SyncroFacturacionXSA));
        protected AdminDataContext dc = null;
        private string idShop = "";
        private string nameShop = "";
        private BackgroundWorker bw = null;
        //private Operation.InvoicesOperations oInvoiceOperation = null;

        public SyncroFacturacionXSA()
        {
            bw = new BackgroundWorker();
            bw.DoWork +=new DoWorkEventHandler(bw_DoWork);
            dc = ApplicationHelper.GetPosDataContext();
            idShop = ApplicationHelper.GetCurrentShop().Id;
            nameShop = ApplicationHelper.GetCurrentShop().Name;
        }

        public void Execute()
        {
            bw.RunWorkerAsync();
        }

        private void SendInvoicesPendingToXSA(object obj)
        {
            SendInvIndividualToXSA();
            SendInvGeneralByIdCasherCloseToXSA();
        }

        public void SendInvIndividualToXSA()
        {
            bool errorFiscalData = false;
            bool failConection = false;
            bool statusDoc = false;
            string errorDescription = "";

            try
            {
                int typeInvIndividual = DataHelper.GetUDCItemIdTypeInvoiceIndividual(dc);
                int statusInvPend = DataHelper.GetUDCItemIdStatusXSAPendienteInvoice(dc);

                List<Invoices> listInvPending = dc.Invoices.Where(o => o.IdStatusXSA == statusInvPend && o.IdTypeInvoice == typeInvIndividual).ToList();

                foreach (Invoices oInv in listInvPending)
                {
                    
                    Operation.InvoicesOperations oInvoiceOperation = new Operation.InvoicesOperations(Guid.Empty, this.idShop, 0);
                    oInvoiceOperation.IdInvoice = oInv.IdInvoice;
                    oInvoiceOperation.ShopName = nameShop;

                    if (oInvoiceOperation.CreateFileTXT(oInv.IdInvoice))
                    {
                        //Mega.POS.Operation.XSAMegahealthAuto XSA = new Operation.XSAMegahealthAuto(oInvoiceOperation.PathFile, oInvoiceOperation.ShopName, oInvoiceOperation.DocumentName, oInvoiceOperation.IdInvoice);
                        //XSA.SendToXSA();
                        oInvoiceOperation.SendToXSA(ref errorFiscalData, ref failConection, ref errorDescription, ref statusDoc);
                        if (errorFiscalData)
                        {
                            Logger.Error(errorDescription);
                        }

                        if (failConection)
                        {
                            Logger.Error(errorDescription);
                        }

                        if (statusDoc)
                        {
                            Logger.Info("La factura individual con id " + oInv.IdInvoice + " se realizo con exito");
                        }
                        else
                        {
                            Logger.Error(errorDescription);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void SendInvGeneralByIdCasherCloseToXSA()
        {
            bool errorFiscalData = false;
            bool failConection = false;
            bool statusDoc = false;
            string errorDescription = "";

            try
            {
                int typeInvGeneral = DataHelper.GetUDCItemIdTypeInvoiceGeneral(dc);
                int statusInvPend = DataHelper.GetUDCItemIdStatusXSAPendienteInvoice(dc);

                //List<Invoices> listInvGenPending = dc.Invoices.Where(o => o.IdStatusXSA == statusInvPend && o.IdTypeInvoice == typeInvGeneral && o.IdCasherClose != null).Distinct().ToList();
                List<string> listIdCasherClose = (from i in dc.Invoices where i.IdStatusXSA == statusInvPend && i.IdTypeInvoice == typeInvGeneral && i.IdCasherClose != null select i.IdCasherClose.ToString()).Distinct().ToList();
                foreach (string idCasherClose in listIdCasherClose)
                {
                    Operation.InvoicesOperations oInvoiceOperation = new Operation.InvoicesOperations(Guid.Empty, this.idShop, 0);
                    oInvoiceOperation.ShopName = nameShop;

                    if (oInvoiceOperation.CreateFileTXTInvoiceGeneral(new Guid(idCasherClose)))
                    {
                        oInvoiceOperation.SendToXSA(ref errorFiscalData, ref failConection, ref errorDescription, ref statusDoc, idCasherClose, 2);
                        if (errorFiscalData)
                        {
                            Logger.Error(errorDescription);
                        }

                        if (failConection)
                        {
                            Logger.Error(errorDescription);
                        }

                        if (statusDoc)
                        {
                            Logger.Info("La factura general se realizo con exito");
                        }
                        else
                        {
                            Logger.Error(errorDescription);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            long timems = (1000 * (Properties.Settings.Default.TimeMinutesSendXSA_Automatic * 60));
            timer = new Timer(SendInvoicesPendingToXSA, null, 0, timems);
        }
    }
}
