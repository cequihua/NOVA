using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Helpers;
using Mega.Synchronizer.Common.Helpers;
using Mega.Synchronizer.Service.Monitor.Helpers;
using System.Linq;
using Mega.Synchronizer.Service.Monitor.Properties;

namespace Mega.Synchronizer.Service.Monitor
{
    public partial class Main : Form
    {
        private DateTime BufferStartedDate;
        private string Buffer;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Main));

        public Main()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        private void InitializeControls()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.CurrentShop))
            {
                ShopComboBox.Visible = true;
                ShopComboBox.ValueMember = "Id";
                ShopComboBox.DisplayMember = "Name";

                var shops = ApplicationHelper.GetPosDataContext().Shops.Where(c => !c.Disabled).Select(c => new { Id = c.Id.ToString(), Name = c.NameWithId }).ToList();
                shops.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "- Todas las Tiendas -" });
                ShopComboBox.DataSource = shops;
                ShopComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
                ChangeExportDateButton.Visible = false;
            }
            else
            {
                ChangeExportDateButton.Visible = true;
                ShopComboBox.Visible = false;
            }
        }

        private void LoadLogs()
        {
            try
            {
                while (true)
                {
                    var dc = ApplicationHelper.GetPosDataContext();
                    var logs = dc.SynchronizationLogs.Where(s => s.SynDate >= BufferStartedDate).OrderBy(s => s.SynDate);

                    foreach (var synchronizationLog in logs)
                    {
                        Buffer = string.Format("{0} - [{4}] - [{3}] - {1} - {2}", synchronizationLog.SynDate, synchronizationLog.Notes,
                            Environment.NewLine, synchronizationLog.IsOk ? "EXITOSA" : "FALLIDA", synchronizationLog.IsExportation ? "EXPORTACION" : "IMPORTACION") + Buffer;
                    }

                    BufferStartedDate = DateTime.Now;

                    SetText(Buffer);

                    Thread.Sleep(Settings.Default.IntervalToRefreshLog);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error cargando las Trazas de sincronización.", ex);
                DialogHelper.ShowError("Ha ocurrido un error cargando las Trazas de sincronización.", ex);
            }
        }

        private void SetText(string text)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(SetText), text);
                }
                else
                {
                    if (ConsoleLogtextBox.Lines.Length > 1500)
                    {
                        ConsoleLogtextBox.Text = text;
                    }
                    else
                    {
                        ConsoleLogtextBox.Text = text + ConsoleLogtextBox.Text;
                    }

                    ConsoleLogtextBox.SelectionStart = 0;
                    ConsoleLogtextBox.ScrollToCaret();
                    ConsoleLogtextBox.Text = text;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando Texto [SetText].", ex);
            }
        }

        public void RefreshUi()
        {
            try
            {
                var dc = ApplicationHelper.GetPosDataContext();
                SynchronizerServiceHelper.ServiceController.Refresh();

                if (SynchronizerServiceHelper.ServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    ExportNow.Enabled = false;
                    ImportNow.Enabled = false;
                    StartService.Enabled = true;
                    StopService.Enabled = false;
                }

                if (SynchronizerServiceHelper.ServiceController.Status == ServiceControllerStatus.Running)
                {
                    ExportNow.Enabled = true;
                    ImportNow.Enabled = true;
                    StopService.Enabled = true;
                    StartService.Enabled = false;
                }

                if (!string.IsNullOrWhiteSpace(Settings.Default.CurrentShop))
                {
                    var dbSynchronizationRow = GetDbSynchronizationRow(dc, Settings.Default.CurrentShop);

                    if (dbSynchronizationRow == null)
                    {
                        throw new Exception(string.Format("Error! No existen en el Sistema configuraciones de Sincronización para la tienda {0}", Settings.Default.CurrentShop));
                    }

                    var daysIn = ToolHelper.GetDaysInConfig(dbSynchronizationRow.DaysPlanIn);
                    var hoursIn = string.Join(", ", OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanIn.Split(',')));

                    DaysPlanIn.Text = string.Format(DaysPlanIn.Text, daysIn);
                    HoursPlanIn.Text = string.Format(HoursPlanIn.Text, hoursIn);

                    var daysOut = ToolHelper.GetDaysInConfig(dbSynchronizationRow.DaysPlanIn);
                    var hoursOut = string.Join(", ", OutInDataHelper.EnsureHourConfiguration(dbSynchronizationRow.HoursPlanOut.Split(',')));

                    DaysPlanOut.Text = string.Format(DaysPlanOut.Text, daysOut);
                    HoursPlanOut.Text = string.Format(HoursPlanOut.Text, hoursOut);

                    var lastOkExport = dc.SynchronizationLogs.Where(s => s.IdShop == Settings.Default.CurrentShop && s.IsOk && s.IsExportation).OrderByDescending(s => s.SynDate).FirstOrDefault();
                    LastExport.Text = string.Format(LastExport.Text, lastOkExport != null ? lastOkExport.SynDate.ToString() : "-");

                    var lastOkImport = dc.SynchronizationLogs.Where(s => s.IdShop == Settings.Default.CurrentShop && s.IsOk && !s.IsExportation).OrderByDescending(s => s.SynDate).FirstOrDefault();
                    LastImport.Text = string.Format(LastImport.Text, lastOkImport != null ? lastOkImport.SynDate.ToString() : "-");
                }
                else
                {
                    var config = DataHelper.GetUDCWebSynch(dc);

                    var daysIn = ToolHelper.GetDaysInConfig(config.Optional3);
                    var hoursIn = string.Join(", ", OutInDataHelper.EnsureHourConfiguration(config.Optional4.Split(',')));

                    DaysPlanIn.Text = string.Format(DaysPlanIn.Text, daysIn);
                    HoursPlanIn.Text = string.Format(HoursPlanIn.Text, hoursIn);

                    var daysOut = ToolHelper.GetDaysInConfig(config.Optional1);
                    var hoursOut = string.Join(", ", OutInDataHelper.EnsureHourConfiguration(config.Optional2.Split(',')));

                    DaysPlanOut.Text = string.Format(DaysPlanOut.Text, daysOut);
                    HoursPlanOut.Text = string.Format(HoursPlanOut.Text, hoursOut);

                    var lastOkExport = dc.SynchronizationLogs.Where(s => s.IsOk && s.IsExportation).OrderByDescending(s => s.SynDate).FirstOrDefault();
                    LastExport.Text = string.Format(LastExport.Text, lastOkExport != null ? lastOkExport.SynDate.ToString() : "-");

                    var lastOkImport = dc.SynchronizationLogs.Where(s => s.IsOk && !s.IsExportation).OrderByDescending(s => s.SynDate).FirstOrDefault();
                    LastImport.Text = string.Format(LastImport.Text, lastOkImport != null ? lastOkImport.SynDate.ToString() : "-");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Inesperado. [RefreshUi]", ex);
                DialogHelper.ShowError("Ha ocurrido un error Inesperado. [RefreshUi]", ex);
            }
        }

        private void StopService_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Deteniendo Servicio.");
                SynchronizerServiceHelper.StopService();
                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Deteniendo el servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Deteniendo el servicio.", ex);
            }
        }

        private void StartService_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Iniciando Servicio.");
                SynchronizerServiceHelper.StartService();
                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el servicio.", ex);
            }
        }

        private void ImportNow_Click(object sender, EventArgs e)
        {
            try
            {
                Buffer = string.Format("{0} - {1}  {2}", DateTime.Now, "Iniciando Proceso de Importación", Environment.NewLine) + Buffer;
                SetText(Buffer);

                Logger.Info("Iniciando Proceso de Importación.");

                SendShopIdToService();

                SynchronizerServiceHelper.ImportNow();

                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el proceso de Importación del servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el proceso de Importación del servicio.", ex);
            }
        }

        private void SendShopIdToService()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.CurrentShop))
            {
                var dc = ApplicationHelper.GetPosDataContext();
                DataHelper.GeCurrentShopIdFromMonitorAction(dc).Optional1 = ShopComboBox.SelectedValue.ToString();
                dc.SubmitChanges();
            }
        }

        private void ExportNow_Click(object sender, EventArgs e)
        {
            try
            {
                Buffer = string.Format("{0} - {1}  {2}", DateTime.Now, "Iniciando Proceso de Exportación", Environment.NewLine) + Buffer;
                SetText(Buffer);

                Logger.Info("Iniciando Proceso de Exportación");

                SendShopIdToService();

                SynchronizerServiceHelper.ExportNow();

                RefreshUi();
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error Iniciando el proceso de Exportación del servicio.", ex);
                DialogHelper.ShowError("Ha ocurrido un error Iniciando el proceso de Exportación del servicio.", ex);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewSynchronizerTraces_Click(object sender, EventArgs e)
        {
            var viewTraces = new ViewTraces(SynchronizerServiceHelper.SERVICE_LOG_FOLDER);
            viewTraces.ShowDialog();
        }

        private void ViewMonitorTraces_Click(object sender, EventArgs e)
        {
            var viewTraces = new ViewTraces(SynchronizerServiceHelper.MONITOR_LOG_FOLDER);
            viewTraces.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Text =
                string.Format(
                    "Mega - Monitor de Sincronización [{0}]  => Conectado al servidor: [{1}], BD: [{2}]",
                    Assembly.GetExecutingAssembly().GetName().Version,
                    ApplicationHelper.GetPosDataContext().Connection.DataSource,
                    ApplicationHelper.GetPosDataContext().Connection.Database);

            InitializeControls();

            BufferStartedDate = DateTime.Today;

            var mainThread = new Thread(LoadLogs) { IsBackground = true };
            mainThread.Start();

            RefreshUi();
        }

        private Synchronization GetDbSynchronizationRow(AdminDataContext dc, string idShop)
        {
            var dbSynchronizationRow = dc.Synchronizations.Where(s => s.IdShop == idShop).FirstOrDefault();

            if (dbSynchronizationRow == null)
            {
                var message1 = string.Format("{0} - {1} {3} {2}", DateTime.Now, "Error! No existen en el Sistema configuraciones de Sincronización para la tienda", Environment.NewLine, idShop);
                Buffer = message1 + Buffer;
                Logger.Error(message1);

                var message2 = string.Format("{0} - {1} {3} {2}", DateTime.Now, "Agregando configuraciones de Sincronización por defecto para la tienda", Environment.NewLine, idShop);
                Buffer = message2 + Buffer;
                Logger.Info(message2);

                SetText(Buffer);

                dbSynchronizationRow = OutInDataHelper.EnsureSynchronizationConfiguration(dc,
                                                                                          dc.Shops.Where(
                                                                                              s =>
                                                                                              s.Id == idShop)
                                                                                              .FirstOrDefault(),
                                                                                              Settings.Default.DefaultHoursPlanIn,
                                                                                              Settings.Default.DefaultHoursPlanOut);

                var message3 = string.Format("{0} - {1} {3} {2}", DateTime.Now, "Configuraciones de Sincronización por defecto agregadas satisfactoriamente para la tienda", Environment.NewLine, idShop);
                Buffer = message3 + Buffer;
                Logger.Info(message3);

                SetText(Buffer);
            }

            return dbSynchronizationRow;
        }

        private void ChangeExportDateButton_Click(object sender, EventArgs e)
        {
            var changeExportDate = new ChangeExportDate();
            changeExportDate.ShowDialog(this);
        }
    }
}
