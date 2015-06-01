using System;
using System.Data;
using System.IO;
using System.Linq;
using log4net;
using Mega.Common;
using Mega.Synchronizer.Common.Data;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Common.Utilities;
using Mega.Web.Synchronizer.Service.Helpers;
using Mega.Web.Synchronizer.Service.Properties;

namespace Mega.Web.Synchronizer.Service
{
    public static class Synchronizer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Synchronizer));

        public static void Import(string idShop)
        {
            Logger.Debug(string.Format("Comenzando el proceso de Importación para la tienda {0}", idShop));

            var dc = ApplicationHelper.GetPosDataContext();
            var synchLog = new SynchronizationLog { IdShop = idShop, Id = Guid.NewGuid(), InitialDate = DateTime.Now, IsExportation = false, SynDate = DateTime.Now };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), Guid.NewGuid() + ".zip");

            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == idShop).FirstOrDefault();

            try
            {
                if (dbSynchronizationRow == null)
                {
                    throw new Exception(string.Format("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}", idShop));
                }

                FtpProvider.Download(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                      string.Format(Settings.Default.ImportURLFormat, idShop),
                                      fileName);

                var ds = new PosOutDataSet();

                try
                {
                    ZipHelper.UnZip(fileName, OutInDataHelper.XML_OUT_FILENAME);

                    ds.ReadXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME));

                }
                catch (Exception e)
                {
                    throw new Exception("Error! abriendo el archivo Xml", e);
                }

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == idShop && s.LastInitialDateOut == dbSynchronizationRow.LastFinalDateOut).FirstOrDefault();

                if (xmlSynchronizationRow != null)
                {
                    var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                    outInDataHelper.WebImportFillTablesInDb(ds);

                    outInDataHelper.WebImportSynchonizeTables();

                    dbSynchronizationRow.LastInitialDateOut = xmlSynchronizationRow.LastInitialDateOut;
                    dbSynchronizationRow.LastFinalDateOut = xmlSynchronizationRow.LastFinalDateOut;

                    dbSynchronizationRow.LastInitialDateIn = xmlSynchronizationRow.LastInitialDateIn;
                    dbSynchronizationRow.LastFinalDateIn = xmlSynchronizationRow.LastFinalDateIn;
                    dbSynchronizationRow.LastNotesIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesIn");

                    dc.SubmitChanges();

                    synchLog.SynDate = xmlSynchronizationRow.LastInitialDateIn;
                    synchLog.IsOk = true;
                }
                else
                {
                    throw new Exception("Error! El proceso de sincronzación del POS ha fallado, por no coincidir las fechas en el fichero de Importación.");
                }

                RemoveImportFiles(fileName);

                Logger.Debug(string.Format("El proceso de Importación para la tienda {0} ha Terminado con éxito", idShop));
            }
            catch (Exception ex)
            {
                synchLog.IsOk = false;
                synchLog.Notes = ex.Message;

                RemoveImportFiles(fileName);

                Logger.Error(string.Format("El proceso de Importación para la tienda {0} ha fallado. Error: {1}", idShop, ex));
            }

            synchLog.FinalDate = DateTime.Now;

            dc.SynchronizationLogs.InsertOnSubmit(synchLog);

            dc.SubmitChanges();
        }

        public static void Export(string idShop)
        {
            Logger.Debug(string.Format("Comenzando el proceso de Exportación para la tienda {0}", idShop));

            var dc = ApplicationHelper.GetPosDataContext();

            var synchLog = new SynchronizationLog { IdShop = idShop, Id = Guid.NewGuid(), InitialDate = DateTime.Now, IsExportation = true, SynDate = DateTime.Now };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), string.Format("{0}-{1}", idShop, OutInDataHelper.XML_IN_FILENAME.Replace(".xml", ".zip")));

            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == idShop).FirstOrDefault();
            
            try
            {
                if (dbSynchronizationRow == null)
                {
                    throw new Exception(string.Format("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}", idShop));
                }

                var temp = dbSynchronizationRow.LastFinalDateIn;
                var temp1 = DateTime.Now;

                var lastExportation = temp != null ? ((DateTime)temp).AddSeconds(1) : DateTime.Today.AddYears(-20);

                var strDateI = lastExportation.ToString("MM-dd-yyyy");

                var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                var ds = outInDataHelper.WebExportLoadDataSet(strDateI, idShop);

                outInDataHelper.WebExportDropTablesInDb();

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == idShop).FirstOrDefault();

                xmlSynchronizationRow.LastInitialDateIn = (DateTime)temp;
                xmlSynchronizationRow.LastFinalDateIn = temp1;

                ds.WriteXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME), XmlWriteMode.WriteSchema);

                outInDataHelper.PosExportDropTablesInDb();

                try
                {
                    ZipHelper.Zip(fileName, Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));
                }
                catch (Exception e)
                {
                    throw new Exception("Error! compactando el archivo Xml", e);
                }

                FtpProvider.Upload(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                      string.Format(Settings.Default.ExportURLFormat, idShop),
                                      fileName);

                RemoveExportFiles(fileName);

                dbSynchronizationRow.LastInitialDateIn = temp;
                dbSynchronizationRow.LastInitialDateIn = temp1;

                dbSynchronizationRow.LastInitialDateOut = xmlSynchronizationRow.LastInitialDateOut;
                dbSynchronizationRow.LastFinalDateOut = xmlSynchronizationRow.LastFinalDateOut;
                dbSynchronizationRow.LastNotesOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesOut");
                
                UpdateSynchronizationDbFields(dbSynchronizationRow, xmlSynchronizationRow);
                dc.SubmitChanges();

                synchLog.SynDate = temp1;
                synchLog.IsOk = true;

                Logger.Debug(string.Format("El proceso de Exportación para la tienda {0} ha Terminado con éxito", idShop));
            }
            catch (Exception ex)
            {
                synchLog.IsOk = false;
                synchLog.Notes = ex.Message;

                RemoveExportFiles(fileName);

                Logger.Error(string.Format("El proceso de Exportación para la tienda {0} ha fallado. Error: {1}", idShop, ex));
            }

            synchLog.FinalDate = DateTime.Now;

            dc.SynchronizationLogs.InsertOnSubmit(synchLog);

            dc.SubmitChanges();
        }

        private static void UpdateSynchronizationDbFields(Synchronization dbSynchronizationRow, DataRow xmlSynchronizationRow)
        {
            dbSynchronizationRow.DaysPlanIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "DaysPlanIn");
            dbSynchronizationRow.HoursPlanIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "HoursPlanIn");
            dbSynchronizationRow.DaysPlanOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "DaysPlanOut");
            dbSynchronizationRow.HoursPlanIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "HoursPlanIn");
        }

        private static void RemoveImportFiles(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            if (File.Exists(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME)))
            {
                File.Delete(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME));
            }
        }

        private static void RemoveExportFiles(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            if (File.Exists(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME)))
            {
                File.Delete(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));
            }
        }
    }
}
