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
    internal static class ServerOperations
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServerOperations));
        #region AD TECH espeficica si es exportacion de central para no exportar la tabla DeniedProducts

        private static bool isExportCentral;
        public static bool IsExportCentral { get { return isExportCentral; } set { isExportCentral = value; } }

        #endregion

        public static void Import(string idShop)
        {
            Logger.DebugFormat("Comenzando el proceso de Importación del Web para la tienda {0}", idShop);

            var dc = ApplicationHelper.GetPosDataContext();
            var synchLog = new SynchronizationLog
                               {
                                   IdShop = idShop,
                                   Id = Guid.NewGuid(),
                                   IsExportation = false,
                                   SynDate = DateTime.Now,
                                   IsOk = false
                               };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), Guid.NewGuid() + ".zip");

            var dbSynchronizationRow = GetDbSynchronizationRow(dc, idShop);

            try
            {
                try
                {
                    Logger.Debug(string.Format("Descargando Archivo de Importación para la tienda {1} desde {0}.", string.Format(Settings.Default.ExportURLFormat, idShop), idShop));

                    FtpProvider.Download(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                                          string.Format(Settings.Default.ExportURLFormat, idShop),
                                                          fileName, Settings.Default.FtpUsePassive);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error! Descargando Archivo de Importación para la tienda {0} desde {1}.", idShop, string.Format(Settings.Default.ExportURLFormat, idShop)), e);
                }

                var ds = new PosOutDataSet();

                try
                {
                    Logger.DebugFormat("Descompactando Archivo descargado para la tienda {0}.", idShop);

                    ZipHelper.UnZip(fileName, OutInDataHelper.XML_OUT_FILENAME);

                    Logger.DebugFormat("Cargando Datos del Archivo descargado para la tienda {0}.", idShop);

                    ds.ReadXml(Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_OUT_FILENAME));

                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error! Descompactando o Cargando datos del archivo hacia la BD para la tienda {0}.", idShop), e);
                }

                var xmlSynchronizationRow =
                    Enumerable.Where(ds.Xml_Synchronization,
                                     s =>
                                     s.IdShop == idShop &&
                                     (dbSynchronizationRow.LastFinalDateOut == null ||
                                      s.LastInitialDateOut <= dbSynchronizationRow.LastFinalDateOut)).FirstOrDefault();

                if (xmlSynchronizationRow != null)
                {
                    var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                    Logger.DebugFormat("Importando Datos del Archivo descargado para la tienda {0}.", idShop);

                    dc.PosOut_CreateTmpTables();
                    outInDataHelper.WebImportFillTablesInDb(ds);

                    if (!Settings.Default.PartialImportEnabled)
                    {
                        dc.PosOut_SynchronizeTables();
                        dc.PosOut_DropTmpTables();
                    }
                    
                    dbSynchronizationRow.LastInitialDateOut = xmlSynchronizationRow.LastInitialDateOut;
                    dbSynchronizationRow.LastFinalDateOut = xmlSynchronizationRow.LastFinalDateOut;
                    dbSynchronizationRow.LastNotesOut = xmlSynchronizationRow.LastNotesOut;

                    dbSynchronizationRow.LastInitialDateIn = OutInDataHelper.GetDateTimeValue(xmlSynchronizationRow, "LastFinalDateIn");
                    //dbSynchronizationRow.LastFinalDateIn = OutInDataHelper.GetDateTimeValue(xmlSynchronizationRow, "LastFinalDateIn");
                    dbSynchronizationRow.LastNotesIn = OutInDataHelper.GetValue(xmlSynchronizationRow, "LastNotesIn");

                    dc.SubmitChanges();

                    synchLog.InitialDate = dbSynchronizationRow.LastInitialDateOut;
                    synchLog.FinalDate = dbSynchronizationRow.LastFinalDateOut;
                    synchLog.IsOk = true;

                    synchLog.Notes = string.Format(
                        "El proceso de Importación {0}en  Web para la tienda {1} ha Terminado con éxito. ",
                        Settings.Default.PartialImportEnabled ? "PARCIAL " : string.Empty, idShop);
                }
                else
                {
                    throw new Exception(string.Format("No coinciden las fechas en el fichero de Importación. Fecha Inicial Maxima Esperada: {0}", dbSynchronizationRow.LastFinalDateOut));
                }

                if (!Settings.Default.PartialImportEnabled)
                {
                    try
                    {
                        Logger.Debug(string.Format("Eliminando Archivo de Importación para la tienda {1} desde {0}.",
                                                   string.Format(Settings.Default.ExportURLFormat, idShop), idShop));

                        RemoveImportFiles(fileName);

                        FtpProvider.Delete(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                           string.Format(Settings.Default.ExportURLFormat, idShop),
                                           Settings.Default.FtpUsePassive);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(
                            string.Format("Error! Eliminando Archivo de Importación para la tienda {1} desde {0}.",
                                          string.Format(Settings.Default.ExportURLFormat, idShop), idShop), e);
                    }
                }

                Logger.InfoFormat("El proceso de Importación del Web ha Terminado con éxito para la tienda {0}.", idShop);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("El proceso de Importación del Web ha fallado para la tienda {0}. Error: {1}", idShop, ex));

                synchLog.IsOk = false;
                synchLog.Notes =
                    string.Format("El proceso de Importación del Web ha fallado para la tienda {0}. Error: {1}", idShop,
                                 ToolHelper.TrucateString(ex.Message, 1800));
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

        public static void Export(string idShop)
        {
            Logger.Debug(string.Format("Comenzando el proceso de Exportación del Web para la tienda {0}", idShop));

            var dc = ApplicationHelper.GetPosDataContext();

            var synchLog = new SynchronizationLog
                               {
                                   IdShop = idShop,
                                   Id = Guid.NewGuid(),
                                   IsExportation = true,
                                   SynDate = DateTime.Now,
                                   IsOk = false
                               };

            var fileName = Path.Combine(ApplicationHelper.AppDataDir(), string.Format("{0}-{1}", idShop, OutInDataHelper.XML_IN_FILENAME.Replace(".xml", ".zip")));

            var dbSynchronizationRow = GetDbSynchronizationRow(dc, idShop);

            try
            {
                var initialDate = (dbSynchronizationRow.LastInitialDateIn ?? DateTime.Today.AddYears(-20)).Date;
                var finalDate = DateTime.Now;

                Logger.Debug(
                    string.Format(
                        "Generando Archivo de Exportación para la tienda {1} desde la ultima fecha de Exportación {0}.",
                        initialDate, idShop));
                
                var outInDataHelper = new OutInDataHelper(Settings.Default.MegaAdminConnectionString);

                dc.PosIn_FillTmpTables(initialDate, idShop);
                if (Settings.Default.IsExportServer)
                    outInDataHelper.IsExportCentral = true;

                var ds = outInDataHelper.WebExportLoadDataSet();

                dc.PosIn_DropTmpTables();

                var xmlSynchronizationRow = Enumerable.Where(ds.Xml_Synchronization, s => s.IdShop == idShop).FirstOrDefault();

                xmlSynchronizationRow.LastInitialDateIn = initialDate;
                xmlSynchronizationRow.LastFinalDateIn = finalDate;
            
                string fisicalFile = Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME);
                ds.WriteXml(fisicalFile, XmlWriteMode.WriteSchema);

                try
                {
                    Logger.DebugFormat("Compactando Archivo generado para la tienda {0}.", idShop);

                    ZipHelper.Zip(fileName, Path.Combine(ApplicationHelper.AppDataDir(), OutInDataHelper.XML_IN_FILENAME));
                }
                catch (Exception e)
                {
                    Logger.ErrorFormat("Error! Compactando Archivo generado para la tienda {0}.", idShop);
                    throw new Exception(string.Format("Error! Compactando Archivo generado para la tienda {0}.", idShop), e);
                }

                try
                {
                    Logger.Debug(string.Format("Subiendo Archivo de Exportación para la tienda {1} al FTP {0}.", string.Format(Settings.Default.ImportURLFormat, idShop), idShop));

                    FtpProvider.Upload(Settings.Default.FtpUser, Settings.Default.FtpPassword,
                                                          string.Format(Settings.Default.ImportURLFormat, idShop),
                                                          fileName, Settings.Default.FtpUsePassive);
                }
                catch (Exception e)
                {
                    Logger.ErrorFormat("Error! Subiendo Archivo de Exportación para la tienda {1} al FTP {0}.", string.Format(Settings.Default.ImportURLFormat, idShop), idShop);
                    throw new Exception(string.Format("Error! Subiendo Archivo de Exportación para la tienda {1} al FTP {0}.", string.Format(Settings.Default.ImportURLFormat, idShop), idShop), e);
                }

                RemoveExportFiles(fileName);

                dbSynchronizationRow.LastInitialDateIn = initialDate;
                dbSynchronizationRow.LastFinalDateIn = finalDate;
                dbSynchronizationRow.LastNotesOut = "El proceso de Exportación ha Terminado con éxito";

                synchLog.InitialDate = initialDate;
                synchLog.FinalDate = finalDate;
                synchLog.IsOk = true;
                synchLog.Notes = string.Format("El proceso de Exportación para la tienda {0} ha Terminado con éxito",
                                               idShop);

                Logger.Debug(string.Format("El proceso de Exportación para la tienda {0} ha Terminado con éxito", idShop));
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error! El proceso de Exportación del Web para la tienda {0} ha fallado.", idShop), ex);

                synchLog.IsOk = false;
                synchLog.Notes =
                    string.Format("El proceso de Exportación del Web ha fallado para la tienda {0}. Error: {1}", idShop,
                                  ToolHelper.TrucateString(ex.Message, 1800));
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

        private static Synchronization GetDbSynchronizationRow(AdminDataContext dc, string idShop)
        {
            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == idShop).FirstOrDefault();

            if (dbSynchronizationRow == null)
            {
                Logger.ErrorFormat("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}.", idShop);

                Logger.DebugFormat("Agregando configuraciones de Sincronización por defecto para la tienda {0}.", idShop);

                dbSynchronizationRow = OutInDataHelper.EnsureSynchronizationConfiguration(dc,
                                                                                          dc.Shops.Where(
                                                                                              s =>
                                                                                              s.Id == idShop)
                                                                                              .FirstOrDefault(), Settings.Default.DefaultHoursPlanIn, Settings.Default.DefaultHoursPlanOut);

                Logger.DebugFormat("Configuraciones de Sincronización por defecto agregadas satisfactoriamente para la tienda {0}.", idShop);
            }
            return dbSynchronizationRow;
        }
    }
}
