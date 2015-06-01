using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Mega.Common.Helpers;
using Mega.Synchronizer.Service.Monitor.Helpers;
using Mega.Synchronizer.Service.Monitor.Properties;

namespace Mega.Synchronizer.Service.Monitor
{
    static class Program
    {
        static NotifyIcon AppIcon;
        static System.Timers.Timer Timer;
        static Main main;
        static ContextMenu ContextMenu;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        [STAThread]
        static void Main()
        {
            if (!IsValidDbConnection())
            {
                MessageBox.Show("La aplicación [Mega - Sincronizador] no puede abrir porque no se ha comprobado una conexión correcta a la Base de datos",
                            "Mensaje de Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            var appSingleTon = new Mutex(false, "Service Monitor");

            if (appSingleTon.WaitOne(0, false))
            {
                Application.EnableVisualStyles();
                IntializeIcon();
                Microsoft.Win32.SystemEvents.SessionEnded += SystemEvent_SessionEnded;
                Timer = new System.Timers.Timer(10000);
                Timer.Elapsed += ServiceChecker;
                Timer.Enabled = true;
                ServiceChecker(null, null);

                Application.Run();
            }
            appSingleTon.Close();
        }

        private static bool IsValidDbConnection()
        {
            return ApplicationHelper.GetPosDataContext().DatabaseExists();
        }

        private static void IntializeIcon()
        {
            AppIcon = new NotifyIcon { Icon = Resources.on, Visible = true };

            ContextMenu = new ContextMenu();

            var item = new MenuItem("Panel de Control") { DefaultItem = true };
            item.Click += ShowMainForm;
            ContextMenu.MenuItems.Add(item);

            ContextMenu.MenuItems.Add("-");

            var item1 = new MenuItem("Importar Ahora", ImportNow);
            ContextMenu.MenuItems.Add(item1);

            ContextMenu.MenuItems.Add("-");

            var item2 = new MenuItem("Exportar Ahora", ExportNow);
            ContextMenu.MenuItems.Add(item2);

            ContextMenu.MenuItems.Add("-");

            var item3 = new MenuItem("Detener Servicio", StopService);
            ContextMenu.MenuItems.Add(item3);

            ContextMenu.MenuItems.Add("-");

            var item4 = new MenuItem("Iniciar Servicio", StartService);
            ContextMenu.MenuItems.Add(item4);

            ContextMenu.MenuItems.Add("-");

            var closeItem = new MenuItem("Salir", CloseItem);
            ContextMenu.MenuItems.Add(closeItem);

            AppIcon.ContextMenu = ContextMenu;
            AppIcon.Text = "Mega Monitor Servicio Corriendo";

            AppIcon.Click += ShowMainForm;
        }

        private static void ShowMainForm(object sender, EventArgs e)
        {
            if (main == null)
            {
                main = new Main();
            }

            try
            {
                main.Visible = true;
            }
            catch
            {
                main = new Main { Visible = true };
            }
        }

        private static void RefreshUi()
        {
            if (main != null)
            {
                main.RefreshUi();
            }
        }

        private static void StopService(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Deteniendo Servicio.");
                SynchronizerServiceHelper.StopService();
                ServiceChecker(null, null);
                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Deteniendo el servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Deteniendo el servicio.", ex);
            }
        }

        private static void ImportNow(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Iniciando Proceso de Importación.");
                SynchronizerServiceHelper.ImportNow();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el proceso de Importación del servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el proceso de Importación del servicio.", ex);
            }
        }

        private static void ExportNow(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Iniciando Proceso de Exportación");
                SynchronizerServiceHelper.ExportNow();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el proceso de Exportación del servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el proceso de Exportación del servicio.", ex);
            }
        }

        private static void StartService(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Iniciando Servicio.");
                SynchronizerServiceHelper.StartService();
                ServiceChecker(null, null);
                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el servicio.", ex);
            }
        }

        private static void SystemEvent_SessionEnded(object sender, Microsoft.Win32.SessionEndedEventArgs e)
        {
            AppIcon.Visible = false;
            Application.Exit();
        }

        private static void CloseItem(object sender, EventArgs e)
        {
            AppIcon.Visible = false;
            Application.Exit();
        }

        private static void ServiceChecker(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SynchronizerServiceHelper.ServiceController.Refresh();

                if (SynchronizerServiceHelper.ServiceController.Status == ServiceControllerStatus.Running)
                {
                    AppIcon.Icon = Resources.on;
                    AppIcon.Text = "Mega Monitor Servicio Corriendo";

                    ContextMenu.MenuItems[2].Enabled = true;
                    ContextMenu.MenuItems[4].Enabled = true;
                    ContextMenu.MenuItems[6].Enabled = true;
                    ContextMenu.MenuItems[8].Enabled = false;
                }
                else if (SynchronizerServiceHelper.ServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    AppIcon.Icon = Resources.off;
                    AppIcon.Text = "Servicio Detenido !!!";

                    ContextMenu.MenuItems[2].Enabled = false;
                    ContextMenu.MenuItems[4].Enabled = false;
                    ContextMenu.MenuItems[6].Enabled = false;
                    ContextMenu.MenuItems[8].Enabled = true;
                }
            }
            catch (Exception ex)
            {
                AppIcon.Icon = Resources.off;
                AppIcon.Text = "Servicio NO INSTALADO en el Sistema !!!";

                ContextMenu.MenuItems[2].Enabled = false;
                ContextMenu.MenuItems[4].Enabled = false;
                ContextMenu.MenuItems[6].Enabled = false;
                ContextMenu.MenuItems[8].Enabled = false;

                Logger.Error("Servicio NO INSTALADO en el Sistema !!!.", ex);
            }
        }
    }
}
