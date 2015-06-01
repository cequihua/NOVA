using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Mega.Common;
using Mega.Common.Helpers;
using Mega.POS.Properties;

namespace Mega.POS.Helper
{
    class ApplicationHelper
    {
        private static AdminDataContext posDataContext;

        public static AdminDataContext GetPosDataContext()
        {
            return posDataContext ??
                   (posDataContext =
                    new AdminDataContext(Properties.Settings.Default.MegaAdminConnectionString));
        }

        public static AdminDataContext GetFreePosDataContext()
        {
            var adminDataContext = new AdminDataContext(Properties.Settings.Default.MegaAdminConnectionString);
            //adminDataContext.ClearCache(); 
            return adminDataContext;
        }

        public static void ResetPosDataContext()
        {
            //posDataContext.ClearCache();
            posDataContext = null;
        }

        public static AdminDataContext GetPosDataContextReseted()
        {
            ResetPosDataContext();
            return GetPosDataContext();
        }

        public static void ConfigureComboUDCItems(ComboBox comboBox)
        {
            comboBox.ValueMember = "Id";
            comboBox.DisplayMember = "Name";
        }

        public static void ConfigureGridView(DataGridView dataGridView1)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.MultiSelect = false;
            //dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 22;
            dataGridView1.RowTemplate.Height = 27;

            DataGridViewCellStyle rowHeadersDefaultCellStyle = new DataGridViewCellStyle();

            rowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rowHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            rowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((0)));
            rowHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            rowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            rowHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            rowHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = rowHeadersDefaultCellStyle;
        }

        public static string GetCurrentUser()
        {
            return string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ? null : Thread.CurrentPrincipal.Identity.Name;
        }

        public static bool IsCurrentUserInRole(int[] rolList)
        {
            foreach (var rol in rolList)
            {
                if (Thread.CurrentPrincipal.IsInRole(rol.ToString()))
                    return true;
            }

            return false;
        }

        public static int GetCurrencyByCurrentShop()
        {
            return GetPosDataContext().Shops.Where(s => s.Id == Properties.Settings.Default.CurrentShop).
                Single().IdCurrency;
        }

        public static Shop GetCurrentShop()
        {
            return GetPosDataContext().Shops.Where(s => s.Id == Properties.Settings.Default.CurrentShop).
                Single();
        }

        public static Cashier GetCurrentCashier()
        {
            return DataHelper.GetCashier(GetPosDataContext(), Properties.Settings.Default.CurrentCashier);
        }

        public static Guid GetCurrentCashierIdAsGuid()
        {
            return new Guid(Properties.Settings.Default.CurrentCashier);
        }

        public static void DisableCopyAndPaste(TextBox textBoxControl)
        {
            textBoxControl.ContextMenu = new ContextMenu();
        }

        public static bool IsOpenSimilarForm(Form toOpen)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (!openForm.Equals(toOpen) && openForm.GetType() == toOpen.GetType())
                {
                    return true;
                }
            }

            return false;
        }

        public static void VerifyOperationModifiedDateState(Common.Operation currentItem)
        {
            var newDC = GetFreePosDataContext();

            var dbOp = newDC.Operations.Where(o => o.Id == currentItem.Id).SingleOrDefault();

            if (dbOp == null || SqlMethods.DateDiffSecond(currentItem.ModifiedDate, dbOp.ModifiedDate) != 0)
            {
                throw new Exception("La Operación ha sido modificada externamente después de cargada. Cierre el Formulario y carguéla de nuevo.");
            }
        }

        public static void VerifyEnviromentConditions(IWin32Window frm, AdminDataContext dc)
        {
            var time = DataHelper.GetServerDateTime(dc);

            if (Math.Abs(SqlMethods.DateDiffMinute(DateTime.Now, time)) > 1)
            {
                string msg =
                    string.Format(
                        "Existe más de 1 MINUTO de diferencia entre las Horas de este POS y el Servidor de BD {0} {1}",
                        time, DateTime.Now);

                throw new Exception(msg);
            }

            CashierClose lastClose = DataHelper.GetLastCashierClose(dc, new Guid(Settings.Default.CurrentCashier));

            if (lastClose != null && DateTime.Now <= lastClose.FinalDate)
            {
                string msg =
                    string.Format("La Fecha y Hora actuales son anteriores al ultimo cierre de Caja del día {0}",
                                  lastClose.AddedDate);

                throw new Exception(msg);
            }

            MoneyMovement lastOpen = DataHelper.GetLastCashierOpenMovement(dc, new Guid(Settings.Default.CurrentCashier));

            if (lastClose != null && lastOpen != null && lastOpen.AddedDate >= lastClose.FinalDate && DateTime.Today > lastOpen.AddedDate.Date)
            {
                string msg =
                    string.Format("Está realizando operaciones al menos un día después de la última Apertura de Caja {0} Puede PROCEDER con la Operación pero verifíque que la fecha actual es correcta en el SO.",
                                  lastOpen.AddedDate);

                DialogHelper.ShowWarningInfo(frm, msg);
            }
        }
    }
}
