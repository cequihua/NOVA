using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using log4net;
using log4net.Config;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Service.Helpers;
using Mega.Synchronizer.Service.Properties;

namespace Mega.Synchronizer.Service
{
    public partial class SynchronizerService : ServiceBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SynchronizerService));

        private static DateTime ImportWorkStartTime = DateTime.MinValue;
        private static Thread ImportThread;

        private static DateTime ExportWorkStartTime = DateTime.MinValue;
        private static Thread ExportThread;
        private static bool Stopped;

        public SynchronizerService()
        {
            InitializeComponent();
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            StartProcess();
        }

        public void StartProcess()
        {
            try
            {
                XmlConfigurator.Configure();

                Logger.Info("Iniciando el Servicio.");

                Stopped = false;

                if (!string.IsNullOrEmpty(Settings.Default.CurrentShop))
                {
                    if (Settings.Default.AutomaticImportEnabled)
                    {
                        if (ImportThread == null)
                        {
                            ImportThread = new Thread(DoPosImportWork);
                            ImportThread.Start();
                        }
                    }

                    if (Settings.Default.AutomaticExportEnabled)
                    {
                        if (ExportThread == null)
                        {
                            ExportThread = new Thread(DoPosExportWork);
                            ExportThread.Start();
                        }
                    }
                }
                else
                {
                    if (Settings.Default.AutomaticImportEnabled)
                    {
                        if (ImportThread == null)
                        {
                            ImportThread = new Thread(DoServerImportWork);
                            ImportThread.Start();
                        }
                    }

                    if (Settings.Default.AutomaticExportEnabled)
                    {
                        if (ExportThread == null)
                        {
                            ExportThread = new Thread(DoServerExportWork);
                            ExportThread.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error Iniciando el Servicio. ", ex);
            }
        }

        public static void DoPosImportWork(object state)
        {
            while (!Stopped)
            {
                try
                {
                    if (ImportWorkStartTime != DateTime.MinValue)
                    {
                        Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ImportWorkStartTime.ToLongTimeString()));
                    }
                    else
                    {
                        var dc = ApplicationHelper.GetPosDataContext();

                        CheckToResetPoints(dc);

                        var dbSynchronizationRow = GetDbSynchronizationRow(dc, Settings.Default.CurrentShop);

                        var days = dbSynchronizationRow.DaysPlanIn.Split(',');
                        var hours = OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanIn.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));

                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto en Horas." + dbSynchronizationRow.HoursPlanIn);
                        }

                        //SqlMethods.DateDiffMinute(DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD * -1), h) >= 0 && 
                        //        SqlMethods.DateDiffMinute(h, DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD)) >= 0)))

                        if (days[(int)DateTime.Now.DayOfWeek].Equals("4") && 
                            hours.Any(h => Math.Abs(SqlMethods.DateDiffMillisecond(h, DateTime.Now)) <= Settings.Default.PROCESS_PERIOD))
                        {
                            ImportWorkStartTime = DateTime.Now;
                            POSOperations.Import();
                            ImportWorkStartTime = DateTime.MinValue;

                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 3);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImportWorkStartTime = DateTime.MinValue;
                    Logger.Error("Error! El proceso de sincronización del POS ha fallado.", ex);
                    Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                }
            }
        }

        private static void ValidateExportOpcional2()
        {
            //Alguna validacion extra que se desee.
        }

        private static Synchronization GetDbSynchronizationRow(AdminDataContext dc, string idShop)
        {
            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == idShop).FirstOrDefault();

            if (dbSynchronizationRow == null)
            {
                Logger.ErrorFormat("Error! No existe configuración de Sincronización para la tienda {0}. Agregando...", idShop);

                dbSynchronizationRow = OutInDataHelper.EnsureSynchronizationConfiguration(dc,
                                                                                          dc.Shops.Where(
                                                                                              s =>
                                                                                              s.Id == idShop)
                                                                                              .FirstOrDefault(), Settings.Default.DefaultHoursPlanIn, Settings.Default.DefaultHoursPlanOut);

                Logger.DebugFormat("Configuraciones de Sincronización por defecto agregadas satisfactoriamente para la tienda {0}.", idShop);
            }
            return dbSynchronizationRow;
        }

        public static void DoPosExportWork(object state)
        {
            while (!Stopped)
            {
                try
                {
                    if (ExportWorkStartTime != DateTime.MinValue)
                    {
                        Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ExportWorkStartTime.ToLongTimeString()));
                    }
                    else
                    {
                        ValidateExportOpcional2();

                        var dc = ApplicationHelper.GetPosDataContext();

                        var dbSynchronizationRow = GetDbSynchronizationRow(dc, Settings.Default.CurrentShop);

                        var days = dbSynchronizationRow.DaysPlanIn.Split(',');
                        var hours = OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanOut.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));

                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto de Horas. " + dbSynchronizationRow.HoursPlanOut);
                        }

                        if (days[(int)DateTime.Now.DayOfWeek].Equals("1") &&
                            hours.Any(h => Math.Abs(SqlMethods.DateDiffMillisecond(h, DateTime.Now)) <= Settings.Default.PROCESS_PERIOD))
                        {
                            ExportWorkStartTime = DateTime.Now;

                            POSOperations.Export();

                            ExportWorkStartTime = DateTime.MinValue;

                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 3);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExportWorkStartTime = DateTime.MinValue;
                    Logger.Error("Error! El proceso de sincronización del POS ha fallado.", ex);
                    Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                }
            }
        }

        public static void DoServerImportWork(object state)
        {
            while (!Stopped)
            {
                if (ImportWorkStartTime != DateTime.MinValue)
                {
                    Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ImportWorkStartTime.ToLongTimeString()));
                }
                else
                {
                    try
                    {
                        var dc = ApplicationHelper.GetPosDataContext();

                        CheckToResetPoints(dc);

                        var config = DataHelper.GetUDCWebSynch(dc);
                        var days = config.Optional3.Split(',');

                        var hours = OutInDataHelper.EnsureHourConfiguration(config.Optional4.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));

                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto de Horas. " + config.Optional4);
                        }

                        //Environment.UserInteractive || 
                        if (days[(int)DateTime.Now.DayOfWeek].Equals("1") &&
                            hours.Any(h => Math.Abs(SqlMethods.DateDiffMillisecond(h, DateTime.Now)) <= Settings.Default.PROCESS_PERIOD))
                        {
                            ImportWorkStartTime = DateTime.Now;

                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                ServerOperations.Import(shop.Id);
                            }

                            ImportWorkStartTime = DateTime.MinValue;

                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 3);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                    catch (Exception ex)
                    {
                        ImportWorkStartTime = DateTime.MinValue;
                        Logger.Error("Error! El proceso de sincronización ha fallado.", ex);
                        Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                    }
                }
            }
        }

        private static void CheckToResetPoints(AdminDataContext dc)
        {
            int yyyymm = DateTime.Today.Year*100 + DateTime.Today.Month;

            var seq = dc.Sequences.Where(s => s.IdSequence == (int)SequenceId.ResetPointMonthControlValue).SingleOrDefault();

            if (seq == null || seq.SequenceValue != yyyymm)
            {
                dc.ResetPoints(false);

                if (seq != null)
                {
                    seq.SequenceValue = yyyymm;
                }
                else
                {
                    seq = new Sequence();
                    seq.IdSequence = (int)SequenceId.ResetPointMonthControlValue;
                    seq.SequenceValue = yyyymm;

                    dc.Sequences.InsertOnSubmit(seq);
                }

                dc.SubmitChanges();
            }
        }

        public static void DoServerExportWork(object state)
        {
            while (!Stopped)
            {
                if (ExportWorkStartTime != DateTime.MinValue)
                {
                    Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ExportWorkStartTime.ToLongTimeString()));
                }
                else
                {
                    try
                    {
                        ValidateExportOpcional2();

                        var dc = ApplicationHelper.GetPosDataContext();

                        var config = DataHelper.GetUDCWebSynch(dc);
                        var days = config.Optional1.Split(',');
                        var hours = OutInDataHelper.EnsureHourConfiguration(config.Optional2.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));

                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto en horas ." + config.Optional2);
                        }

                        string vns = days[(int)DateTime.Now.DayOfWeek].ToString();
                      
                        //Environment.UserInteractive || 

                         if (days[(int)DateTime.Now.DayOfWeek].Equals("1") &&
                           hours.Any(h => Math.Abs(SqlMethods.DateDiffMillisecond(h, DateTime.Now)) <= Settings.Default.PROCESS_PERIOD))
                   
                        {
                            ExportWorkStartTime = DateTime.Now;

                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                ServerOperations.IsExportCentral = true;
                                ServerOperations.Export(shop.Id);
                            }

                            ExportWorkStartTime = DateTime.MinValue;

                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 3);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExportWorkStartTime = DateTime.MinValue;
                        Logger.Error("Error! El proceso de sincronización ha fallado.", ex);
                        Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                    }
                }
            }
        }

        protected override void OnCustomCommand(int command)
        {
            try
            {
                Logger.InfoFormat("Procesando OnCustomCommand. {0}",
                                  command.Equals((int)SynchronizationOperationEnun.Import)
                                      ? "Importación"
                                      : "Exportación");

                if (!string.IsNullOrEmpty(Settings.Default.CurrentShop))
                {
                    if (command.Equals((int)SynchronizationOperationEnun.Import))
                    {
                        Logger.Debug("Iniciando Proceso de Importación del POS.");
                        POSOperations.Import();
                    }
                    else
                    {
                        Logger.Debug("Iniciando Proceso de Exportación del POS.");
                        ValidateExportOpcional2();

                        POSOperations.Export();
                    }
                }
                else
                {
                    var dc = ApplicationHelper.GetPosDataContext();

                    if (command.Equals((int)SynchronizationOperationEnun.Import))
                    {
                        Logger.Debug("Iniciando Proceso de Importación del Web.");

                        if (!DataHelper.GeCurrentShopIdFromMonitorAction(dc).Optional1.Equals(Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString()))
                        {
                            ServerOperations.Import(DataHelper.GeCurrentShopIdFromMonitorAction(dc).Optional1);
                        }
                        else
                        {
                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                ServerOperations.Import(shop.Id);
                            }
                        }
                    }
                    else
                    {
                        Logger.Debug("Iniciando Proceso de Exportación del Web.");
                        ValidateExportOpcional2();

                        if (!DataHelper.GeCurrentShopIdFromMonitorAction(dc).Optional1.Equals(Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString()))
                        {
                            ServerOperations.Export(DataHelper.GeCurrentShopIdFromMonitorAction(dc).Optional1);
                        }
                        else
                        {
                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                ServerOperations.Export(shop.Id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error Procesando OnCustomCommand. ", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                Stopped = true;

                Logger.InfoFormat("Recibida una solicitud para detener el Servicio. {0}", DateTime.Now);

                if (ExportThread != null && ExportThread.ThreadState != ThreadState.Suspended)
                {
                    ExportThread.Abort();
                }

                if (ImportThread != null && ImportThread.ThreadState != ThreadState.Suspended)
                {
                    ImportThread.Abort();
                }
                
                Logger.InfoFormat("Terminadas las acciones para detener el Servicio. {0}", DateTime.Now);
            }
            catch (Exception ex)
            {
                ExitCode = 1; // ExitCode != 0 is error.
                Logger.Error("Error intentando Detener el Servicio.", ex);
            }
        }
    }
}
