using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading;
using log4net;
using log4net.Config;
using Mega.Common.Helpers;
using Mega.Synchronizer.Common.Helpers;
using Mega.Web.Synchronizer.Service.Helpers;
using Mega.Web.Synchronizer.Service.Properties;

namespace Mega.Web.Synchronizer.Service
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
                    var config = DataHelper.GetUDCWebSynch(dc);
                    var days = config.Optional3.Split(',');

                    var hours = OutInDataHelper.EnsureHourConfiguration(config.Optional4.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));

                    if (hours.Count() == 0)
                    {
                        throw new Exception("Error! Valor de Configuración Incorrecto.");
                    }

                    if (days[(int)DateTime.Now.DayOfWeek - 1].Equals("1") && (hours.Any(h => SqlMethods.DateDiffMinute(DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD * -1), h) >= 0 && SqlMethods.DateDiffMinute(h, DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD)) >= 0)))
                    {
                        ImportWorkStartTime = DateTime.Now;

                        try
                        {
                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                Synchronizer.Import(shop.Id);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Error! El proceso de sincronzación ha fallado.", ex);
                        }

                        ImportWorkStartTime = DateTime.MinValue;

                        Thread.Sleep(Settings.Default.PROCESS_PERIOD * 2);
                    }
                    else
                    {
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
                    var config = DataHelper.GetUDCWebSynch(dc);
                    var days = config.Optional1.Split(',');
                    var hours = OutInDataHelper.EnsureHourConfiguration(config.Optional2.Split(',')).Select(h => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(h.Split(':')[0]), int.Parse(h.Split(':')[1]), 0));


                    if (hours.Count() == 0)
                    {
                        throw new Exception("Error! Valor de Configuración Incorrecto.");
                    }

                    if (days[(int)DateTime.Now.DayOfWeek - 1].Equals("1") && (hours.Any(h => SqlMethods.DateDiffMinute(DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD * -1), h) >= 0 && SqlMethods.DateDiffMinute(h, DateTime.Now.AddMilliseconds(Settings.Default.PROCESS_PERIOD)) >= 0)))
                    {
                        ExportWorkStartTime = DateTime.Now;

                        try
                        {
                            foreach (var shop in dc.Shops.Where(s => !s.Disabled))
                            {
                                Synchronizer.Export(shop.Id);
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Error! El proceso de sincronzación ha fallado.", ex);
                        }

                        ExportWorkStartTime = DateTime.MinValue;

                        Thread.Sleep(Settings.Default.PROCESS_PERIOD * 2);
                    }
                    else
                    {
                        Thread.Sleep(Settings.Default.PROCESS_PERIOD);
                    }
                }
            }
        }
    }
}
