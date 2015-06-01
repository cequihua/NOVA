using System;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Client;
using Mega.POS.Helper;
using Mega.POS.Movement;
using Mega.POS.Operation;
using Mega.POS.Properties;
using System.Threading;
using System.Globalization;


namespace Mega.POS
{
    public partial class Main : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Main));

        public Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                NewSaleToolStripButton.Text = string.Format("Nueva{0}Venta", Environment.NewLine);
                SaleQueryToolStripButton.Text = string.Format("Consulta{0}de Ventas", Environment.NewLine);
                CashExtractToolStripButton.Text = string.Format("Retiro de{0}Efectivo", Environment.NewLine);
                NewInventoryMovementToolStripButton.Text = string.Format("Nuevo {0}Movimiento{0}Inventario", Environment.NewLine);
                QueryInventoryMovementToolStripButton.Text = string.Format("Consultar {0}Movimientos{0} de Inventario", Environment.NewLine);

                VersionToolStripStatusLabel.Text = string.Format("=> Versión del POS: [{0}] - {1}", Assembly.GetExecutingAssembly().GetName().Version, Settings.Default.AmericaCenterCountry );
                DBToolStripStatusLabel.Text = string.Format("=> POS conectado al servidor: [{0}], BD: [{1}]",
                                                            ApplicationHelper.GetPosDataContext().Connection.DataSource,
                                                            ApplicationHelper.GetPosDataContext().Connection.Database);

                var cashier = ApplicationHelper.GetCurrentCashier();
                
                if (cashier == null || cashier.Disabled)
                {
                    DialogHelper.ShowError(this, "La aplicación no puede abrir porque la Caja configurada esta Desactivada");
                    CloseApplication();
                    return;
                }

                Text += string.Format(" [{0}] [Caja: {1}] [Moneda: {2}]", cashier.Shop.Name, cashier.Name, cashier.Shop.CurrencyCode);

                var time = DataHelper.GetServerDateTime(ApplicationHelper.GetPosDataContext());

                if (Math.Abs(SqlMethods.DateDiffMinute(DateTime.Now, time)) > 1)
                {
                    Logger.ErrorFormat("Existe más de 1 MINUTO de diferencia entre las Horas de este POS y el Servidor de BD {0} {1}", time, DateTime.Now);

                    DialogHelper.ShowError(this, "Existe más de 1 MINUTO de diferencia entre las Horas de este POS y el Servidor de BD. Este POS se cerrará hasta que arregle una de las 2 horas");

                    CloseApplication();
                    return;
                }

                var f = new Login();

                if (f.ShowDialog() != DialogResult.OK)
                {
                    DialogHelper.ShowError(this, "La aplicación se cerrará porque no se ha autenticado un usuario correctamente");
                    CloseApplication();
                    return;
                }

                Logger.InfoFormat("El sistema ha sido abierto por el usuario {0}", f.AuthenticatedLogin);
                ActivateCurrentLoginOptions(f.AuthenticatedLogin, f.AuthenticatedRolName);

                newSaleToolStripMenuItem_Click(null, null);

                #region ADTECH - ENVIO AUTOMATICO DE FACTURAS INDIVIDUALES y GENERALES EN ESTATUS PENDIENTE A XSA

                //SyncroFacturacionXSA oSyncFac = new SyncroFacturacionXSA();
                //oSyncFac.Execute();

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error("Error al cargar el formulario principal", ex);
                DialogHelper.ShowError(null, "Ha ocurrido un error inesperado al cargar la aplicación. Reinicie nuevamente la misma.", ex);

                CloseApplication();
            }
        }

        private void CloseApplication()
        {
            Close();
            Application.Exit();
        }

        private void clientListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new ClientList().Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Clientes", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario de Clientes", ex);
            }
        }

        private void addClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new ClientAdd().Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Agregar Clientes", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario de Agregar Clientes", ex);
            }
        }

        private void receptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new OperationList((int)OperationType.Receipt).Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Lista de Operaciones", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario Lista de Operaciones", ex);
            }
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new OperationList((int)OperationType.Sale).Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Lista de Operaciones", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario Lista de Operaciones", ex);
            }
        }

        private void newSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new SaleEdit(OperationType.Sale);
                f.Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario AgregarVenta", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar/editar Venta.", ex);
            }
        }

        private void addMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new MovementAdd().Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario Agregar Movimiento", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar Movimiento.", ex);
            }
        }

        private void cashierCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogHelper.ShowWarningQuestion(this, "Esta acción creará un registro de Cierre de Caja. No debe realizar ninguna operación en esta Caja a partir de este momento. ¿Desea continuar?") == DialogResult.Yes)
                {
                    new CashierClose(false).Show(this);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario cashierCloseToolStripMenuItem_Click", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Cierre de caja.", ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void pruebaDeImpresionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Test().Show();
        }

        private void ReportSelectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportSelector().Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario Report Selector", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario del Centro de Reportes.", ex);
            }
        }

        private void cashierVerificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogHelper.ShowWarningQuestion(this, "Esta accción creará un registro de Arqueo de Caja. No debe realizar ninguna otra operación en esta caja a partir de este momento. ¿Desea continuar?") == DialogResult.Yes)
                {
                    new CashierClose(true).Show(this);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo Formulario en cashierVerificationToolStripMenuItem_Click", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Arqueo de caja.", ex);
            }
        }

        private void consignmentSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new OperationList((int)OperationType.Consignation).Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Lista de Operaciones, Ventas en Consignación", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario Lista de Operaciones, Ventas en Consignación", ex);
            }
        }

        private void loginToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form ownedForm in OwnedForms)
            {
                ownedForm.Close();
            }

            var f = new Login();

            if (f.ShowDialog() == DialogResult.OK)
            {
                Logger.InfoFormat("Se ha cambiado de usuario sin cerrar el Sistema. Nuevo usuario {0}", f.AuthenticatedLogin);
                ActivateCurrentLoginOptions(f.AuthenticatedLogin, f.AuthenticatedRolName);

                newSaleToolStripMenuItem_Click(null, null);
            }
        }

        private void ActivateCurrentLoginOptions(string login, string rol)
        {
            loginToolStripButton.Text = string.Format("Login: {0}{2}Rol: {1}{2}Cambiar", login, rol, Environment.NewLine);
        }

        private void LoginChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginToolStripButton_Click(null, null);
        }

        private void NewSaleToolStripButton_Click(object sender, EventArgs e)
        {
            newSaleToolStripMenuItem_Click(null, null);
        }

        private void SaleQueryToolStripButton_Click(object sender, EventArgs e)
        {
            salesToolStripMenuItem_Click(null, null);
        }

        private void CashExtractToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                new MovementAdd(MovementType.Extract).Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario Agregar Movimiento", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar Movimiento.", ex);
            }
        }

        private void consignationReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new OperationList((int)OperationType.ConsignationReturn).Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Lista de Operaciones, Devoluciones de Consignación", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario Lista de Operaciones, Devoluciones de Consignación", ex);
            }
        }

        private void InventoryMovemementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new OperationList(null).Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error abriendo formulario de Lista de Operaciones, Movimientos de Inventario", ex);
                DialogHelper.ShowError(this, "Error inesperado abriendo el formulario Lista de Operaciones, Movimientos de Inventario", ex);
            }
        }

        private void NewInvnetoryMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new EntryExitOperationEdit(null);
                f.Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario Movimientos", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar/editar Movimientos.", ex);
            }
        }

        private void NewInventoryMovementToolStripButton_Click(object sender, EventArgs e)
        {
            NewInvnetoryMovementToolStripMenuItem_Click(sender, e);
        }

        private void QueryInventoryMovementToolStripButton_Click(object sender, EventArgs e)
        {
            InventoryMovemementToolStripMenuItem_Click(sender, e);
        }

        private void ccrdtCollectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new CreditCollect();
                f.Show(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario Cobro de Créeditos", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el Formulario de Cobro de Créeditos.", ex);
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }

        private void toolStriptProducts_Click(object sender, EventArgs e)
        {
            var p = new Products();
            p.Show(this);
        }

        private void toolStripMenuResolution_Click(object sender, EventArgs e)
        {
            var user_rol = ApplicationHelper.GetPosDataContext().User_Rols.Where(
                        u => u.IdUser == ApplicationHelper.GetCurrentUser()).SingleOrDefault();

            if(user_rol.IdRol != (int)Roles.PosAdmin)
            {
                DialogHelper.ShowWarningInfo(this, "No tiene permiso para realizar esta operacion");
                return;
            }

            var frmRf = new FormResolutionFiscalList();
            frmRf.Show(this);
        }

    }
}
