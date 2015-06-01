using System;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Operation
{
    public partial class OperationList : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OperationList));

        private int? operationTypeId;

        private int OperationTypeIdEx
        {
            get { return operationTypeId ?? Convert.ToInt32(MovementTypeComboBox.SelectedValue); }
        }
       
        public OperationList(int? operationTypeId)
            : this()
        {
            this.operationTypeId = operationTypeId;
        }

        public OperationList()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (OperationTypeIdEx)
                {
                    case (int)OperationType.Sale:
                        new SaleEdit(OperationType.Sale).Show();
                        Close();
                        break;
                    case (int)OperationType.Consignation:
                        new SaleEdit(OperationType.Consignation).Show();
                        Close();
                        break;
                    case (int)OperationType.ConsignationReturn:
                        new SaleEdit(OperationType.ConsignationReturn).Show();
                        Close();
                        break;

                    default:
                        Close();
                        new EntryExitOperationEdit((OperationType)Enum.Parse(typeof(OperationType), OperationTypeIdEx.ToString())).Show();
                        break;
                }

                LoadList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario AgregarVenta", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar/editar Venta.", ex);
            }
        }

        private void OperationList_Load(object sender, EventArgs e)
        {
            try
            {
                InitialDateTimePicker.Value = DateTime.Today;
                FinalDateTimePicker.Value = DateTime.Today;
                ExpiredConsignationCheckBox.Visible = OperationTypeIdEx == (int) OperationType.Consignation; 

                ApplicationHelper.ConfigureGridView(dataGridView1);

                if (operationTypeId == null)
                {
                    MovementTypeLabel.Visible = true;
                    MovementTypeComboBox.Visible = true;

                    Text += string.Format(": {0}", "Movimientos de Inventario");

                    FillInventoryMovementTypes();
                }
                else
                {
                    MovementTypeLabel.Visible = false;
                    MovementTypeComboBox.Visible = false;

                    Text += string.Format(": {0}",
                                      ApplicationHelper.GetPosDataContext().UDCItems.Where(
                                          u => u.Id == operationTypeId).Single().Name);
                }
                
                switch (OperationTypeIdEx)
                {
                    case (int)OperationType.Receipt:
                        AddButton.Visible = false;
                        DeleteOperationButton.Visible = false;
                        dataGridView1.Columns["DimNameColumn"].Visible = false;
                        dataGridView1.Columns["IdDimColumn"].Visible = false;

                        break;

                    case (int)OperationType.Consignation:
                    case (int)OperationType.Sale:
                        AddButton.Visible = true;
                        DeleteOperationButton.Visible = true;
                        dataGridView1.Columns["DimNameColumn"].Visible = true;
                        dataGridView1.Columns["OfficialConsecutiveColumn"].HeaderText = "Factura";
                        dataGridView1.Columns["ModifiedDateColumn"].HeaderText = "F. Operación";

                        dataGridView1.Columns["OperationCurrencyCodeColumn"].Visible = false;
                        dataGridView1.Columns["ChangeRateColumn"].Visible = false;
                        dataGridView1.Columns["OperationAmountColumn"].Visible = false;

                        break;

                    default:
                        AddButton.Visible = true;
                        DeleteOperationButton.Visible = true;
                        dataGridView1.Columns["DimNameColumn"].Visible = false;
                        dataGridView1.Columns["IdDimColumn"].Visible = false;

                        break;
                }

                LoadStatusComboValues();
                LoadList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario OperationList", ex);
                DialogHelper.ShowError(this, "Error inesperado durnte la carga de este formulario.", ex);
            }
        }

        private void FillInventoryMovementTypes()
        {
            MovementTypeComboBox.DisplayMember = "Name";
            MovementTypeComboBox.ValueMember = "Id";
            MovementTypeComboBox.DataSource = DataHelper.GetUDCInventoryMovements(ApplicationHelper.GetPosDataContext());
        }

        private void LoadStatusComboValues()
        {
            StatusComboBox.ValueMember = "Id";
            StatusComboBox.DisplayMember = "Name";
            StatusComboBox.DataSource = DataHelper.GetOperationStatusComboList(ApplicationHelper.GetPosDataContext(), "-Todos-");
            StatusComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCurrentSelection();
        }

        private void EditCurrentSelection()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe haber una fila seleccionada");
                }
                else
                {
                    Common.Operation item = (Common.Operation)dataGridView1.SelectedRows[0].DataBoundItem;

                    if (item.IdType != (int)OperationType.Receipt && !IsEditableInThisCashier(item))
                    {
                        DialogHelper.ShowError(this, "Solo puede Editar Operaciones No Confirmadas creadas por esta Caja.");
                        return;
                    }

                    switch (operationTypeId)
                    {
                        case (int)OperationType.Receipt:
                          //  Close();
                            new ReceiptEdit(item.Id).Show();
                            break;

                        case (int)OperationType.Sale:
                            //Close();
                            new SaleEdit(OperationType.Sale, item.Id).Show();
                            break;

                        case (int)OperationType.Consignation:
                            //Close();
                            new SaleEdit(OperationType.Consignation, item.Id).Show();
                            break;

                        case (int)OperationType.ConsignationReturn:
                            //Close();
                            new SaleEdit(OperationType.ConsignationReturn, item.Id).Show();
                            break;

                        default:
                            //Close();
                            new EntryExitOperationEdit((OperationType) Enum.Parse(typeof(OperationType),  OperationTypeIdEx.ToString()), item.Id).Show();
                            break;
                    }

                    //LoadList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario OperationEdit o refrescando los datos en Listado", ex);
                DialogHelper.ShowError(this, "Error inesperado.", ex);
            }
        }

        private bool IsEditableInThisCashier(Common.Operation op)
        {
            return op.IdStatus != (int) OperationStatus.NotConfirmed ||
                   op.IdCashier.ToString() == Properties.Settings.Default.CurrentCashier;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditCurrentSelection();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                LoadList();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logger.Error("Error filtrando datos en OperationList", ex);
                Cursor = Cursors.Default;

                DialogHelper.ShowError(this, "Error inesperado en la operación de Filtrado.", ex);
            }
        }

        private void LoadList()
        {
            int statusSelected = Convert.ToInt32(StatusComboBox.SelectedValue);

            DateTime lastdate = FinalDateTimePicker.Value.Date.AddDays(1).AddMinutes(-1);

            var dc = ApplicationHelper.GetPosDataContext();
            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Operations);

            var list = dc.Operations.Where(
                o => o.IdType == OperationTypeIdEx &&
                     (statusSelected == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY || o.IdStatus == statusSelected) &&
                     o.ModifiedDate > InitialDateTimePicker.Value.Date && o.ModifiedDate < lastdate);

            if (FindTextBox.Text.Trim() != string.Empty)
            {
                string w = FindTextBox.Text.Trim().ToUpper();

                list = list.Where(o => o.Consecutive.Contains(w) || o.OfficialConsecutive.Contains(w) ||
                                       o.IdDim.ToString().Contains(w) || o.Dim.Name.Contains(w) ||
                                       o.CancelConsecutive.Contains(w) || o.Amount.ToString().Contains(w) || 
                                       o.UDCItem.Name.Contains(w) || o.UDCItem1.Name.Contains(w) || 
                                       o.UDCItem2.Name.Contains(w)).OrderByDescending(o => o.AddedDate);
            }

            if (ExpiredConsignationCheckBox.Visible && ExpiredConsignationCheckBox.Checked)
            {
                int maxExpiredDays = Convert.ToInt32(DataHelper.GetUDCItemRow(ApplicationHelper.GetPosDataContext(),
                                                        Constant.MAX_DAYS_RETURN_CONSIGNATION_UDC_KEY).Optional1);

                list = list.Where(
                    o =>
                    o.IdStatus == (int) OperationStatus.Confirmed && o.Reference.Trim() == string.Empty &&
                    SqlMethods.DateDiffDay(DateTime.Today, o.ModifiedDate) > maxExpiredDays);
            }

            dataGridView1.DataSource = list.OrderByDescending(o => o.AddedDate);
        }

        private void DeleteOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe haber una fila seleccionada");
                }
                else
                {
                    var item = (Common.Operation)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (item.IdStatus != (int)OperationStatus.NotConfirmed)
                    {
                        DialogHelper.ShowError(this, "Solo se permiten Eliminar Operaciones en Estado [No Confirmada]");
                    }
                    else
                    {
                        ApplicationHelper.GetPosDataContext().Operations.DeleteOnSubmit(item);
                        ApplicationHelper.GetPosDataContext().SubmitChanges();

                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario OperationEdit o refrescando los datos en Listado", ex);
                DialogHelper.ShowError(this, "Error inesperado.", ex);
            }
        }

        private void FindTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                FindButton.PerformClick();
            }
        }
    }
}
