using System;
using System.Data;
using System.IO;
using System.Linq;
using log4net;
using Mega.Common;
using Mega.Common.Helpers;
using Mega.Synchronizer.Common.Data;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Common.Utilities;
using Mega.Synchronizer.Service.Properties;

namespace Mega.Synchronizer.Service.Helpers
{
    internal static class POSOperations
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(POSOperations));

        public static void Import()
        {
            Logger.DebugFormat("Comenzando el proceso de Importación del POS para la tienda {0}", Settings.Default.CurrentShop);

            var dc = ApplicationHelper.GetPosDataContext();

            var synchLog = new SynchronizationLog
                               {
                                   IdShop = Settings.Default.CurrentShop,
                                   Id = Guid.NewGuid(),
                                   IsExportation = false,
                                   SynDate = DateTime.Now,
                                   IsOk = false
                               };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), Guid.NewGuid() + ".zip");

            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

            try
            {
                try
                {
                    Logger.Debug(string.Format("Descargando Archivo de Importación para la tienda {1} desde {0}.", string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop), Settings.Default.CurrentShop));

                    FtpProvider.Download(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                      string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop),
                                      fileName, Settings.Default.FtpUsePassive);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error! Descargando el archivo de Importación para la tienda {1} desde {0}.", string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop), Settings.Default.CurrentShop), e);
                }

                var ds = new PosInDataSet();

                try
                {
                    Logger.DebugFormat("Descompactando Archivo descargado para la tienda {0}.", Settings.Default.CurrentShop);

                    ZipHelper.UnZip(fileName, OutInDataHelper.XML_IN_FILENAME);

                    Logger.DebugFormat("Cargando Datos del Archivo descargado para la tienda {0}.", Settings.Default.CurrentShop);

                    ds.ReadXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error! Procesando el archivo de Importación Descargado para la tienda {0}.", Settings.Default.CurrentShop), e);
                }

                var xmlSynchronizationRow =
                    Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == Settings.Default.CurrentShop &&
                                                                  (dbSynchronizationRow.LastFinalDateIn == null ||
                                                                   s.LastInitialDateIn <=
                                                                   dbSynchronizationRow.LastFinalDateIn)).FirstOrDefault();

                if (xmlSynchronizationRow != null)
                {
                    var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                    Logger.DebugFormat("Importando Datos del Archivo descargado para la tienda {0}.", Settings.Default.CurrentShop);

                    dc.PosIn_CreateTmpTables();
                    outInDataHelper.PosImportFillTablesInDb(ds);

                    if (!Settings.Default.PartialImportEnabled)
                    {
                        dc.PosIn_SynchronizeTables();
                        dc.PosIn_DropTmpTables();
                    }

                    dbSynchronizationRow.LastInitialDateIn = xmlSynchronizationRow.LastInitialDateIn;
                    dbSynchronizationRow.LastFinalDateIn = xmlSynchronizationRow.LastFinalDateIn;
                    dbSynchronizationRow.LastNotesIn = "El proceso de Importación ha Terminado con éxito";

                    //dbSynchronizationRow.LastInitialDateOut = OutInDataHelper.GetDateTimeValue(xmlSynchronizationRow, "LastInitialDateOut");

                    //Voy a correr la fecha inicial a la que viene como FINAL del WEB. Es decir, solo a una confirmada recibida.
                    dbSynchronizationRow.LastInitialDateOut = OutInDataHelper.GetDateTimeValue(xmlSynchronizationRow, "LastFinalDateOut");
                    //dbSynchronizationRow.LastFinalDateOut = OutInDataHelper.GetDateTimeValue(xmlSynchronizationRow, "LastFinalDateOut");
                    dbSynchronizationRow.LastNotesOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesOut");

                    UpdateSynchronizationDbFields(dbSynchronizationRow, xmlSynchronizationRow);

                    synchLog.InitialDate = dbSynchronizationRow.LastInitialDateIn;
                    synchLog.FinalDate = dbSynchronizationRow.LastFinalDateIn;
                    synchLog.IsOk = true;
                    synchLog.Notes = "El proceso de Importación ha Terminado con éxito";

                }
                else
                {
                    throw new Exception(string.Format("No coinciden las fechas en el fichero de Importación. Fecha Inicial Maxima Esperada: {0}", dbSynchronizationRow.LastFinalDateIn));
                }

                if (!Settings.Default.PartialImportEnabled)
                {
                    try
                    {
                        Logger.Debug(string.Format("Eliminando Archivo de Importación para la tienda {1} desde {0}.",
                                                   string.Format(Settings.Default.ImportURLFormat,
                                                                 Settings.Default.CurrentShop),
                                                   Settings.Default.CurrentShop));

                        RemoveImportFiles(fileName);

                        FtpProvider.Delete(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                           string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop),
                                           Settings.Default.FtpUsePassive);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(
                            string.Format("Error! Eliminando el archivo de Importación para la tienda {1} desde {0}.",
                                          string.Format(Settings.Default.ImportURLFormat, Settings.Default.CurrentShop),
                                          Settings.Default.CurrentShop), e);
                    }
                }

                synchLog.Notes = string.Format(
                    "El proceso de Importación {0}del POS ha Terminado con éxito para la tienda {1}.",
                    Settings.Default.PartialImportEnabled ? "PARCIAL " : string.Empty, Settings.Default.CurrentShop);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("Error! El proceso de Importación del POS ha Fallado para la tienda {0}. {1}", Settings.Default.CurrentShop, ex);

                synchLog.IsOk = false;
                synchLog.Notes =
                    string.Format("El proceso de Importación del POS ha fallado para la tienda {0}. Error: {1}", Settings.Default.CurrentShop,
                                  ToolHelper.TrucateString(ex.Message,1800));

                dbSynchronizationRow.LastNotesIn = synchLog.Notes;
            }
            finally
            {
                try
                {
                    synchLog.SynDate = DateTime.Now;

                    dc.SynchronizationLogs.InsertOnSubmit(synchLog);
                    dc.SubmitChanges();
                }
                catch (Exception ex1)
                {
                    Logger.Error("No se pudo Guardar el estado final de la Importación", ex1);
                }
            }
        }

        public static void Export()
        {
            Logger.DebugFormat("Comenzando el proceso de Exportación del POS para la tienda {0}.", Settings.Default.CurrentShop);

            var dc = ApplicationHelper.GetPosDataContext();

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), string.Format("{0}-{1}", Settings.Default.CurrentShop, OutInDataHelper.XML_OUT_FILENAME.Replace(".xml", ".zip")));

            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

            var synchLog = new SynchronizationLog
                               {
                                   IdShop = Settings.Default.CurrentShop,
                                   Id = Guid.NewGuid(),
                                   IsExportation = true,
                                   SynDate = DateTime.Now,
                                   IsOk = false
                               };

            //var temp = (dbSynchronizationRow.LastFinalDateOut ?? DateTime.Today.AddYears(-20)).Date;

            var initialDate = (dbSynchronizationRow.LastInitialDateOut ?? DateTime.Today.AddYears(-20)).Date;
            var finalDate = DateTime.Now;

            try
            {
                //var initialDate = temp.AddSeconds(1);

                Logger.Debug(string.Format("Generando Archivo de Exportación para la tienda {1} desde la ultima fecha de Exportación {0}.", initialDate.ToString("MM/dd/yyyy hh:mm tt"), Settings.Default.CurrentShop));

                var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                dc.PosOut_FillTmpTables(initialDate);

                var ds = outInDataHelper.PosExportLoadDataSet();

                outInDataHelper.UpdateShopToExecuteScript(ds, Settings.Default.CurrentShop);

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

                xmlSynchronizationRow.LastInitialDateOut = initialDate;
                xmlSynchronizationRow.LastFinalDateOut = finalDate;

                ds.WriteXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME), XmlWriteMode.WriteSchema);

                dc.PosOut_DropTmpTables();

                try
                {
                    Logger.DebugFormat("Compactando Archivo generado para la tienda {0}.", Settings.Default.CurrentShop);

                    ZipHelper.Zip(fileName, Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME));
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error !Compactando Archivo generado para la tienda {0}.", Settings.Default.CurrentShop), e);
                }


                try
                {
                    Logger.Debug(string.Format("Subiendo Archivo de Exportación para la tienda {1} al FTP {0}.", string.Format(Settings.Default.ExportURLFormat, Settings.Default.CurrentShop), Settings.Default.CurrentShop));

                    FtpProvider.Upload(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                                         string.Format(Settings.Default.ExportURLFormat, Settings.Default.CurrentShop),
                                                         fileName, Settings.Default.FtpUsePassive);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error! Subiendo Archivo de Exportación para la tienda {1} al FTP {0}.", string.Format(Settings.Default.ExportURLFormat, Settings.Default.CurrentShop), Settings.Default.CurrentShop), e);
                }

                RemoveExportFiles(fileName);

                dbSynchronizationRow.LastInitialDateOut = initialDate;
                dbSynchronizationRow.LastFinalDateOut = finalDate;
                dbSynchronizationRow.LastNotesOut = "El proceso de Exportación ha Terminado con éxito";

                synchLog.InitialDate = initialDate;
                synchLog.FinalDate = finalDate;
                synchLog.IsOk = true;
                synchLog.Notes = "El proceso de Exportación ha Terminado con éxito";

                UpdateSynchronizationDbFields(dbSynchronizationRow, xmlSynchronizationRow);

                Logger.DebugFormat("El proceso de Exportación del POS ha Terminado con éxito para la tienda {0}.", Settings.Default.CurrentShop);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error! El proceso de Exportación del POS para la tienda {0} ha fallado.", Settings.Default.CurrentShop), ex);

                synchLog.IsOk = false;
                synchLog.Notes =
                    string.Format("El proceso de Exportación del POS ha fallado para la tienda {0}. Error: {1}",
                                  Settings.Default.CurrentShop, ToolHelper.TrucateString(ex.Message, 1800));

                dbSynchronizationRow.LastNotesOut = synchLog.Notes;
            }
            finally
            {
                try
                {
                    synchLog.SynDate = DateTime.Now;

                    dc.SynchronizationLogs.InsertOnSubmit(synchLog);
                    dc.SubmitChanges();
                }
                catch (Exception ex1)
                {
                    Logger.Error("No se pudo Guardar el estado final de la Exportación", ex1);
                }
            }
        }

        private static void UpdateSynchronizationDbFields(Synchronization dbSynchronizationRow, DataRow xmlSynchronizationRow)
        {
            dbSynchronizationRow.DaysPlanIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "DaysPlanIn");
            dbSynchronizationRow.HoursPlanIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "HoursPlanIn");
            dbSynchronizationRow.DaysPlanOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "DaysPlanOut");
            dbSynchronizationRow.HoursPlanOut = OutInDataHelper.GetValue(xmlSynchronizationRow, "HoursPlanOut");
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
