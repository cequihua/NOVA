using System;
using System.IO;
using Mega.Common;
using Mega.Synchronizer.Service.Properties;

namespace Mega.Synchronizer.Service.Helpers
{
    static class ApplicationHelper
    {
        public static AdminDataContext GetPosDataContext()
        {
            var dataContext = new AdminDataContext(Settings.Default.MegaAdminConnectionString);
            //dataContext.ClearCache();

            return dataContext;
        }

        public static string AppDataDir()
        {
            //var appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mega.Synchronizer.Service");
            var appDataDir = Path.Combine(Environment.CurrentDirectory, "TempFiles");

            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }
            return appDataDir;
        }
    }
}
