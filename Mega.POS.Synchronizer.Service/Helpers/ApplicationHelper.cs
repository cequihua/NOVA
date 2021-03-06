﻿using System;
using System.IO;
using Mega.Common;
using Mega.POS.Synchronizer.Service.Properties;

namespace Mega.POS.Synchronizer.Service.Helpers
{
    class ApplicationHelper
    {
     
        public static AdminDataContext GetPosDataContext()
        {
            return new AdminDataContext(Settings.Default.MegaAdminConnectionString);
        }

        public static string AppDataDir()
        {
            var appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mega.POS.Synchronizer.Service");

            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }
            return appDataDir;
        }
    }
}
