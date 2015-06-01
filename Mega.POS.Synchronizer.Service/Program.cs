using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading;
using log4net;
using log4net.Config;
using Mega.POS.Synchronizer.Service.Helpers;
using Mega.POS.Synchronizer.Service.Properties;
using Mega.Synchronizer.Common.Helpers;

namespace Mega.POS.Synchronizer.Service
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        private static DateTime ImportWorkStartTime = DateTime.MinValue;
        private static Thread ImportThread;
      
        private static DateTime ExportWorkStartTime = DateTime.MinValue;
        private static Thread ExportThread;
      
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            ImportThread = new Thread(Import);
            ImportThread.Start();

            ExportThread = new Thread(Export);
            ExportThread.Start();
        }

        private static void Import()
        {
            var dc = ApplicationHelper.GetPosDataContext();

            while (true)
            {
                if (ImportWorkStartTime != DateTime.MinValue)
                {
                    Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ImportWorkStartTime.ToLongTimeString()));
                }
                else
                {
                    try
                    {
                        var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

                        if (dbSynchronizationRow == null)
                        {
                            throw new Exception(string.Format("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}", Settings.Default.CurrentShop));
                        }

                        var days = dbSynchronizationRow.DaysPlanIn.Split(',');
                        var hours = OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanIn.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));


                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto.");
                        }

                        if (days[(int)DateTime.Now.DayOfWeek - 1].Equals("1") && (hours.Any(h => SqlMethods.DateDiffMinute(DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD * -1), h) >= 0 && SqlMethods.DateDiffMinute(h, DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD)) >= 0)))
                        {
                            ImportWorkStartTime = DateTime.Now;
                            Synchronizer.Import();
                            ImportWorkStartTime = DateTime.MinValue;

                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 2);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error! El proceso de sincronzación del POS ha fallado.", ex);
                        Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                    }
                }
            }
        }

        private static void Export()
        {
            var dc = ApplicationHelper.GetPosDataContext();

            while (true)
            {
                if (ExportWorkStartTime != DateTime.MinValue)
                {
                    Logger.Warn(string.Format("Advertencia! El proceso está ocupado desde {0}", ExportWorkStartTime.ToLongTimeString()));
                }
                else
                {
                    try
                    {
                        var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

                        if (dbSynchronizationRow == null)
                        {
                            throw new Exception(string.Format("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}", Settings.Default.CurrentShop));
                        }

                        var days = dbSynchronizationRow.DaysPlanIn.Split(',');
                        var hours = OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanOut.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));


                        if (hours.Count() == 0)
                        {
                            throw new Exception("Error! Valor de Configuración Incorrecto.");
                        }

                        if (days[(int)DateTime.Now.DayOfWeek - 1].Equals("1") && (hours.Any(h => SqlMethods.DateDiffMinute(DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD * -1), h) >= 0 && SqlMethods.DateDiffMinute(h, DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD)) >= 0)))
                        {
                            ExportWorkStartTime = DateTime.Now;
                            Synchronizer.Export();
                            ExportWorkStartTime = DateTime.MinValue;
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD * 2);
                        }
                        else
                        {
                            Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error! El proceso de sincronzación del POS ha fallado.", ex);
                        Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                    }
                }
            }
        }
    }
}
