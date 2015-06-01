using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mega.Common.Helpers;
using Mega.Common;
using Mega.Configurator.Helpers;
using Microsoft.Data.ConnectionUI;
using System.Transactions;

namespace Mega.Configurator
{
    public partial class Main : Form
    {
        private ConfigSettingsHelper ConfigSettingsHelper;
        private AdminDataContext dc = null;

        public Main()
        {
            InitializeComponent();
            dc = new AdminDataContext();
        }

        private void ChangeConnectionStringbutton_Click(object sender, EventArgs e)
        {
            var dcd = new DataConnectionDialog();
            DataSource.AddStandardDataSources(dcd);

            dcd.SelectedDataSource = DataSource.SqlDataSource;
            dcd.ConnectionString = ConnectionStringTextBox.Text;

            foreach (var dataProvider in
                dcd.SelectedDataSource.Providers.Where(dataProvider => dataProvider.Name == DataProvider.SqlDataProvider.Name))
            {
                dcd.SelectedDataProvider = dataProvider;
            }

            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                ConnectionStringTextBox.Text = dcd.ConnectionString;
                ConfigSettingsHelper.WriteConnectionString(dcd.ConnectionString);
            }
        }

        private void SettingsDataGridView_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (SettingsDataGridView.SelectedRows.Count > 0)
            {
                if (ConfigSettingsHelper.ExistsApplicationSettings())
                {
                    ConfigSettingsHelper.WriteApplicationSetting(SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), SettingsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
                else
                {
                    ConfigSettingsHelper.WriteAppSetting(SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), SettingsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                }

            }
        }

        private void SettingsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (SettingsDataGridView.SelectedRows.Count > 0 && e.ColumnIndex == 1)
            {
                SettingsDataGridView.Rows[e.RowIndex].ErrorText = "";
                if (SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailSmtpPort")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("IntervalToRefreshLog")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("PROCESS_PERIOD")
                    )
                {
                    long number;

                    if (!long.TryParse(e.FormattedValue.ToString(), out number))
                    {
                        e.Cancel = true;
                        SettingsDataGridView.Rows[e.RowIndex].ErrorText = "El valor debe ser numérico.";
                    }
                }

                if (SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailSSLEnabled")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailSmtpNeedsAuthentication")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("FtpUsePassive")
                    )
                {
                    bool number;

                    if (!bool.TryParse(e.FormattedValue.ToString(), out number))
                    {
                        e.Cancel = true;
                        SettingsDataGridView.Rows[e.RowIndex].ErrorText = "El valor debe ser de tipo [True/False].";
                    }
                }

                if (SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("ImportURLFormat")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("ExportURLFormat")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailFromName")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailLogoImageName")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailDateFormat")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailSmtpServer")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailFromEMail")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("MailAdminAddress")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("DefaultHoursPlanIn")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("DefaultHoursPlanOut")
                    )
                {
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        e.Cancel = true;
                        SettingsDataGridView.Rows[e.RowIndex].ErrorText = "El valor de esta configuración es Requerido.";
                    }
                }

                if (SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("DefaultHoursPlanIn")
                    || SettingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("DefaultHoursPlanOut")
                    )
                {
                    if (!string.IsNullOrEmpty(ToolHelper.GetInvalidHours(e.FormattedValue.ToString().Split(','))))
                    {
                        e.Cancel = true;
                        SettingsDataGridView.Rows[e.RowIndex].ErrorText = "El valor debe ser de tipo [hh:mm] o [hh], divididos por ','.";
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                var path = string.Format("{0}\\MegaHealth",
                                                         Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                var dialog = new OpenFileDialog
                {
                    Filter = "Archivos de Configuración (*.config)|*.config",
                    InitialDirectory = path,
                    Title = "Seleccione un Archivo de Configuración",
                    RestoreDirectory = true
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ConfigSettingsHelper = new ConfigSettingsHelper(dialog.FileName);

                    if (ConfigSettingsHelper.ExistsConnectionString())
                    {
                        ConnectionStringTabPage.Enabled = true;
                        ConnectionStringTextBox.Text = ConfigSettingsHelper.ReadConnectionString();
                        ShowCashierButton.Enabled = true;
                    }
                    else
                    {
                        ConnectionStringTabPage.Enabled = false;
                        ConnectionStringTextBox.Text = string.Empty;
                        ShowCashierButton.Enabled = false;
                    }

                    if (ConfigSettingsHelper.ExistsApplicationSettings())
                    {
                        var readSettings = ConfigSettingsHelper.ReadApplicationSettings();
                        SettingsDataGridView.DataSource = readSettings;
                        SettingsDataGridView.Refresh();
                        SettingsTabPage.Enabled = true;
                    }
                    else if (ConfigSettingsHelper.ExistsAppSettings())
                    {
                        var readSettings = ConfigSettingsHelper.ReadAppSettings();
                        SettingsDataGridView.DataSource = readSettings;
                        SettingsDataGridView.Refresh();
                        SettingsTabPage.Enabled = true;
                    }
                    else
                    {
                        SettingsTabPage.Enabled = false;
                        SettingsDataGridView.DataSource = new DataTable();
                        SettingsDataGridView.Refresh();
                    }

                    SaveButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Se ha producido un error mientras se cargaba el fichero de Configuración.", ex);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar())
                {
                    if (GuardarConfigFacturacion())
                    {
                        if (DialogHelper.ShowWarningQuestion(this, "Ocurrio un error al intentar guardar la configuracion de facturacion \n ¿ desea continuar ?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            ConfigSettingsHelper.Save();
                            DialogHelper.ShowInformation(this, "EL fichero de Configuración fue guardado satisfactoriamente.");
                        }
                    }
                    else
                    {
                        ConfigSettingsHelper.Save();
                        DialogHelper.ShowInformation(this, "EL fichero de Configuración fue guardado satisfactoriamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Se ha producido un error mientras se guardaba el fichero de Configuración.", ex);
            }
        }

        private void ShowCashierButton_Click(object sender, EventArgs e)
        {
            try
            {
                var cashierList = new CashierListForm(ConfigSettingsHelper.ReadConnectionString());

                cashierList.Show(this);
                ShowCashierButton.Enabled = true;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Se ha producido un error mientras se mostraba el listado de Cajas.", ex);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        #region ADTECH - Configuracion Facturacion
        public void CargarDatosFacturacion()
        {
            try
            {
                if (ConnectionStringTextBox.Text == "")
                {
                    DialogHelper.ShowWarningInfo(this, "Por favor configure la conexion a la base de datos");
                    tabConfig.SelectedIndex = 0;
                    return;
                }

                dc.Connection.ConnectionString = ConnectionStringTextBox.Text;

                txtToken.Text = DataHelper.GetTokenXSA(dc);
                txtRfcCompañia.Text = DataHelper.GetRFCCompany(dc);
                txtCompFiscal.Text = DataHelper.GetTipoComprobanteFiscal(dc);
                txtSerie.Text = dc.FiscalDataShop.Select(o => o.Serie).Single();
                //txtSerie.Text = DataHelper.GetSerieInvoice(dc);
                txtSucursal.Text = dc.Shops.Select(o => o.Name).Single();
                txtFolio.Text = dc.FiscalDataShop.Select(o => o.Folio).Single();
                //txtFolio.Text = dc.Shops.Select(o => o.String1).Single();

                txtRfcGral.Text = dc.FiscalDataShop.Select(o => o.RFC).Single();
                txtPais.Text = dc.FiscalDataShop.Select(o => o.Pais).Single();
                txtCalle.Text = dc.FiscalDataShop.Select(o => o.Calle).Single();
                txtNumInt.Text = dc.FiscalDataShop.Select(o => o.NumeroInt).Single();
                txtNumExt.Text = dc.FiscalDataShop.Select(o => o.NumeroExt).Single();
                txtColinia.Text = dc.FiscalDataShop.Select(o => o.Colonia).Single();
                txtMunicipio.Text = dc.FiscalDataShop.Select(o => o.Municipio).Single();
                txtEstado.Text = dc.FiscalDataShop.Select(o => o.Estado).Single();
                txtCP.Text = dc.FiscalDataShop.Select(o => o.CP).Single();
                txtEmail.Text = dc.FiscalDataShop.Select(o => o.Email).Single();

            }
            catch (Exception ex)
            {
                DialogHelper.ShowWarningInfo(this, "Por favor asegurese de ejecutar el script SQL de instalacion y configurar correctamente la conexion a la base de datos");
            }
        }
        
        private void tabConfig_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 2)
                CargarDatosFacturacion();
        }

        private bool validar()
        {
            if (txtToken.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el Token XSA");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtRfcCompañia.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el RFC de la compañia");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtCompFiscal.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el comprobante fiscal");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtRfcGral.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el RFC general");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtSerie.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese la serie");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtFolio.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el folio");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtSucursal.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el nombre de la sucursal");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtEmail.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el email para el envio de la factura general");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtCalle.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese la calle");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtNumExt.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el numero exterior");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtColinia.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese la colonia");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtCP.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el codigo postal");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtPais.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el pais");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtEstado.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el estado");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            if (txtMunicipio.Text == "")
            {
                DialogHelper.ShowWarningInfo(this, "Por favor ingrese el municipio");
                tabConfig.SelectedIndex = 2;
                return false;
            }

            return true;
        }

        private bool GuardarConfigFacturacion()
        {
            bool error;
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dc.Connection.ConnectionString = ConnectionStringTextBox.Text;
                    UDCItem key = dc.UDCItems.Single(o => o.IdUDC == "KEYXSA" && o.Code == "KEYXSA");
                    UDCItem rfcComp = dc.UDCItems.Single(o => o.IdUDC == "RFCCOM" && o.Code == "RFCCOM");
                    UDCItem compFis = dc.UDCItems.Single(o => o.IdUDC == "COMPF" && o.Code == "COMPF");
                    //UDC serie = dc.UDCs.Single(o => o.Id == "SERIE");
                    Shop suc = dc.Shops.Single();

                    key.Name = txtToken.Text;
                    rfcComp.Name = txtRfcCompañia.Text;
                    compFis.Name = txtCompFiscal.Text;
                    //serie.Name = txtSerie.Text;
                    suc.Name = txtSucursal.Text;
                    //suc.String1 = txtFolio.Text;

                    int t = (from i in dc.FiscalDataShop select i).Count();
                    if (t == 0)
                    {
                        FiscalDataShop tienda = new FiscalDataShop
                        {
                            RFC = txtRfcGral.Text,
                            Nombre = "PUBLICO EN GENERAL",
                            Pais = txtPais.Text,
                            Calle = txtCalle.Text,
                            NumeroInt = txtNumInt.Text,
                            NumeroExt = txtNumExt.Text,
                            Colonia = txtColinia.Text,
                            Localidad = txtMunicipio.Text,
                            Referencia = "",
                            Municipio = txtMunicipio.Text,
                            Estado = txtEstado.Text,
                            CP = txtCP.Text,
                            Email = txtEmail.Text,
                            Serie = txtSerie.Text,
                            Folio = txtFolio.Text
                        };

                        dc.FiscalDataShop.InsertOnSubmit(tienda);
                    }
                    else
                    {
                        FiscalDataShop tiendaEdit = dc.FiscalDataShop.Single();
                        tiendaEdit.RFC = txtRfcGral.Text;
                        tiendaEdit.Nombre = "PUBLICO EN GENERAL";
                        tiendaEdit.Pais = txtPais.Text;
                        tiendaEdit.Calle = txtCalle.Text;
                        tiendaEdit.NumeroInt = txtNumInt.Text;
                        tiendaEdit.NumeroExt = txtNumExt.Text;
                        tiendaEdit.Colonia = txtColinia.Text;
                        tiendaEdit.Localidad = txtMunicipio.Text;
                        tiendaEdit.Referencia = "";
                        tiendaEdit.Municipio = txtMunicipio.Text;
                        tiendaEdit.Estado = txtEstado.Text;
                        tiendaEdit.CP = txtCP.Text;
                        tiendaEdit.Email = txtEmail.Text;
                        tiendaEdit.Serie = txtSerie.Text;
                        tiendaEdit.Folio = txtFolio.Text;
                    }

                    dc.SubmitChanges();
                    trans.Complete();
                }

                error = false;
            }
            catch
            {
                error = true;
            }

            return error;
        }

        #endregion
    }
}
