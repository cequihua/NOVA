using System;
using System.Data;
using System.IO;
using System.Linq;
using log4net;
using Mega.Common;
using Mega.POS.Synchronizer.Service.Helpers;
using Mega.POS.Synchronizer.Service.Properties;
using Mega.Synchronizer.Common.Data;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Common.Utilities;

namespace Mega.POS.Synchronizer.Service
{
    public static class Synchronizer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Synchronizer));

        public static void Import()
        {
            Logger.Debug("Comenzando el proceso de Importación.");

            var dc = ApplicationHelper.GetPosDataContext();
            var synchLog = new SynchronizationLog { IdShop = Settings.Default.CurrentShop, Id = Guid.NewGuid(), InitialDate = DateTime.Now, IsExportation = false, SynDate = DateTime.Now};

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), Guid.NewGuid() + ".zip");


            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();
            try
            {
                FtpProvider.Download(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                      string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop),
                                      fileName);

                var ds = new PosInDataSet();

                try
                {
                    ZipHelper.UnZip(fileName, OutInDataHelper.XML_IN_FILENAME);

                    ds.ReadXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));

                }
                catch (Exception e)
                {
                    throw new Exception("Error abriendo el archivo Xml", e);
                }

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == Settings.Default.CurrentShop && s.LastInitialDateIn == dbSynchronizationRow.LastFinalDateIn).FirstOrDefault();

                if (xmlSynchronizationRow != null)
                {
                    var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                    outInDataHelper.PosImportFillTablesInDb(ds);

                    outInDataHelper.PosImportSynchonizeTables();

                    dbSynchronizationRow.LastInitialDateIn = xmlSynchronizationRow.LastInitialDateIn;
                    dbSynchronizationRow.LastFinalDateIn = xmlSynchronizationRow.LastInitialDateIn;

                    dbSynchronizationRow.LastInitialDateOut = xmlSynchronizationRow.LastInitialDateOut;
                    dbSynchronizationRow.LastFinalDateOut = xmlSynchronizationRow.LastFinalDateOut;
                    dbSynchronizationRow.LastNotesOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesOut");

                    UpdateSynchronizationDbFields(dbSynchronizationRow, xmlSynchronizationRow);

                    synchLog.SynDate = xmlSynchronizationRow.LastInitialDateIn;
                    synchLog.IsOk = true;
                }
                else
                {
                    throw new Exception("Error! El proceso de sincronzación del POS ha fallado, por no coincidir las fechas en el fichero de importación.");
                }

                RemoveImportFiles(fileName);

                Logger.Debug("El proceso de Importación ha Terminado con éxito");

            }
            catch (Exception ex)
            {
                synchLog.IsOk = false;
                synchLog.Notes = ex.Message;
                dbSynchronizationRow.LastNotesIn = ex.Message;

                RemoveImportFiles(fileName);

                Logger.Error("Error! El proceso de Importación del POS ha fallado.", ex);
            }

            synchLog.FinalDate = DateTime.Now;

            dc.SynchronizationLogs.InsertOnSubmit(synchLog);

            dc.SubmitChanges();
        }

        public static void Export()
        {
            Logger.Debug("Comenzando el proceso de Exportación.");

            var dc = ApplicationHelper.GetPosDataContext();

            var synchLog = new SynchronizationLog { IdShop = Settings.Default.CurrentShop, Id = Guid.NewGuid(), InitialDate = DateTime.Now, IsExportation = true, SynDate = DateTime.Now };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), string.Format("{0}-{1}", Settings.Default.CurrentShop, OutInDataHelper.XML_OUT_FILENAME.Replace(".xml", ".zip")));

            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

            var temp = dbSynchronizationRow.LastFinalDateOut;
            var temp1 = DateTime.Now;

            try
            {
                var lastExportation = temp != null ? ((DateTime)temp).AddSeconds(1) : DateTime.Today.AddYears(-20);

                var strDateI = lastExportation.ToString("MM-dd-yyyy");

                var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                var ds = outInDataHelper.PosExportLoadDataSet(strDateI);

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

                xmlSynchronizationRow.LastInitialDateOut = (DateTime)temp;
                xmlSynchronizationRow.LastFinalDateOut = temp1;

                ds.WriteXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME), XmlWriteMode.WriteSchema);

                outInDataHelper.PosExportDropTablesInDb();

                try
                {
                    ZipHelper.Zip(fileName, Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME));
                }
                catch (Exception e)
                {
                    throw new Exception("Error compactando el archivo Xml", e);
                }

                FtpProvider.Upload(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                      string.Format(Settings.Default.ExportURLFormat, Settings.Default.CurrentShop),
                                      fileName);

                RemoveExportFiles(fileName);

                dbSynchronizationRow.LastInitialDateOut = temp;
                dbSynchronizationRow.LastFinalDateOut = temp1;

                dbSynchronizationRow.LastInitialDateIn = xmlSynchronizationRow.LastInitialDateIn;
                dbSynchronizationRow.LastFinalDateIn = xmlSynchronizationRow.LastFinalDateIn;
                dbSynchronizationRow.LastNotesIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesIn");

                synchLog.SynDate = temp1;
                synchLog.IsOk = true;

                UpdateSynchronizationDbFields(dbSynchronizationRow, xmlSynchronizationRow);

                Logger.Debug("El proceso de Exportación ha Terminado con éxito");
            }
            catch (Exception ex)
            {
                synchLog.IsOk = false;
                synchLog.Notes = ex.Message;
                dbSynchronizationRow.LastNotesOut = ex.Message;

                RemoveExportFiles(fileName);

                Logger.Error("Error! El proceso de Exportación del POS ha fallado.", ex);
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

            if (File.Exists(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME)))
            {
                File.Delete(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));
            }
        }

        private static void RemoveExportFiles(string fileName)
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
    }
}
