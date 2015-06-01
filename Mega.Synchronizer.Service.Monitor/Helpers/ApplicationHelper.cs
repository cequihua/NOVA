using System;
using System.IO;
using Mega.Common;
using Mega.Synchronizer.Service.Monitor.Properties;

namespace Mega.Synchronizer.Service.Monitor.Helpers
{
    class ApplicationHelper
    {
        public static AdminDataContext GetPosDataContext()
        {
            var dataContext = new AdminDataContext(Settings.Default.MegaAdminConnectionString);
            //dataContext.ClearCache();

            return dataContext;
        }

        public static string AppDataDir()
        {
            var appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mega.Synchronizer.Service");

            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }
            return appDataDir;
        }
    }
}
