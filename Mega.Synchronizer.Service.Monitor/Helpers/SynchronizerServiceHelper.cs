using System;
using System.ServiceProcess;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Service.Monitor.Properties;

namespace Mega.Synchronizer.Service.Monitor.Helpers
{
    internal static class SynchronizerServiceHelper
    {
        static ServiceController Sc;
        private static readonly string SERVICE_NAME = Settings.Default.SERVICE_NAME;

        public const string MONITOR_LOG_FOLDER = "Monitor-Logs";
        public const string SERVICE_LOG_FOLDER = "Logs";

        public static ServiceController ServiceController
        {
            get { return Sc ?? (Sc = new ServiceController(SERVICE_NAME)); }
        }

        public static void StartService()
        {
            ServiceController.Refresh();

            if (ServiceController.Status == ServiceControllerStatus.Stopped || ServiceController.Status == ServiceControllerStatus.StopPending)
            {
                ServiceController.Start();
                ServiceController.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 1, 0));
            }
        }

        public static void StopService()
        {
            ServiceController.Refresh();

            if (ServiceController.Status != ServiceControllerStatus.Stopped && ServiceController.Status != ServiceControllerStatus.StopPending)
            {
                ServiceController.Stop();
                ServiceController.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 1, 0));
            }
        }

        public static void ImportNow()
        {
            ServiceController.ExecuteCommand((int)SynchronizationOperationEnun.Import);
        }

        public static void ExportNow()
        {
            ServiceController.ExecuteCommand((int)SynchronizationOperationEnun.Export);
        }
    }
}
