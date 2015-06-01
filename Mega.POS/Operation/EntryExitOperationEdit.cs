using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;
using Mega.POS.Properties;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Operation
{
    public partial class EntryExitOperationEdit : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EntryExitOperationEdit));

        protected AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        protected Common.Operation currentItem;
        protected Product currentProduct;
        protected IEnumerable<Product_Price> prices;
        protected Product_Price currentPrice;
        private string authorizedBy;

        protected Guid id;

        protected OperationType? operationType;

        public EntryExitOperationEdit(OperationType? operationType, Guid id)
        {
            InitializeComponent();

            this.id = id;
            this.operationType = operationType;
        }

        public EntryExitOperationEdit(OperationType? saleType)
            : this(saleType, Guid.Empty)
        {
        }

        private void EntryExitOperationEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (ApplicationHelper.IsOpenSimilarForm(this))
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                    Close();
                    return;
                }

                if (VerifyAuthorization())
                {
                    Text = string.Format("{0} [{1}]", "Movimientos de Inventario",
                                         ApplicationHelper.GetCurrentShop().Name);

                    KeyPreview = true;
                    FillInventoryMovementTypes();

                    ApplicationHelper.ConfigureGridView(dataGridView1);
                    AddProductButton.Size = new Size(0, 0);

                    LoadCurrentOperation();
                }
                else
                {
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en EntryExitOperationEdit_Load", ex);
                DialogHelper.ShowError(this,
                                       "Ha ocurrido un error inesperado durante el proceso de carga del formulario", ex);
                Close();
            }
        }

        private bool VerifyAuthorization()
        {
            if (ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
            {
                authorizedBy = ApplicationHelper.GetCurrentUser();
                return true;
            }

            var f = new AuthorizationRequired(Constant.SupervisorOrMore,
                                                                "Un usuario con Rol de [Supervisor] o [Gerente] necesita autorizar esta Operación.");

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                authorizedBy = f.AuthorizedUser;
                return true;
            }

            return false;
        }

        private void FillInventoryMovementTypes()
        {
            MovementTypeComboBox.DisplayMember = "Name";
            MovementTypeComboBox.ValueMember = "Id";
            MovementTypeComboBox.DataSource = DataHelper.GetUDCInventoryMovements(dc);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DialogHelper.ShowError(this,
                                   "Ha ocurrido un error en el manejo interno del componente Grid. Repórte de continuar.",
                                   e.Exception);
        }

        private void LoadCurrentOperation()
        {
            if (currentItem == null && id == Guid.Empty)
            {
                RequestNewOperation();
            }
            else
            {
                if (currentItem == null)
                {
                    currentItem = dc.Operations.Where(o => o.Id == id).Single();
                }

                FillSubTypeCombo();

                if (currentItem.IdType == (int)OperationType.Transfer)
                {
                    Location2ComboBox.Visible = true;
                    Location2Label.Visible = true;

                    dataGridView1.Columns["Location2Name"].Visible = true;
                    dataGridView1.Columns["LocationName"].HeaderText = "Ubicación Origen";
                }
                else
                {
                    Location2ComboBox.Visible = false;
                    Location2Label.Visible = false;

                    dataGridView1.Columns["Location2Name"].Visible = false;
                    dataGridView1.Columns["LocationName"].HeaderText = "Ubicación";
                }

                LoadAddProductControls();
                LoadResumeControls();
                LoadGridDetails();

                HiddenPanel.Visible = false;

                IdProductTextBox.Focus();
                ActiveControl = IdProductTextBox;
            }
        }

        private void FillSubTypeCombo()
        {
            SubTypeComboBox.DisplayMember = "Name";
            SubTypeComboBox.ValueMember = "Id";
            SubTypeComboBox.DataSource = DataHelper.GetUDCOperationSubTypeComboList(dc, currentItem.IdType);
        }

        private void RequestNewOperation()
        {
            HiddenPanel.Top = SaleInfoGroupBox.Top;
            HiddenPanel.Left = 0;
            HiddenPanel.Width = Width;
            HiddenPanel.Height = Height;
            HiddenPanel.Visible = true;
            HiddenPanel.TabIndex = 1000;
            HiddenPanel.Update();

            DeleteOperationButton.Visible = false;

            NewOperationButton.Focus();
            ActiveControl = NewOperationButton;
        }

        private void LoadAddProductControls()
        {
            var locations = currentItem.Shop.Locations.Where(l => !l.Disabled).ToList();

            if (locations.Count() == 0)
            {
                DialogHelper.ShowError(this,
                                       "No existen Ubicaciones permitidas para realizar Ventas. Debe agregar al menos una a la Tienda");
                AddProductButton.Enabled = false;
            }
            else
            {
                LocationComboBox.DataSource = null;
                LocationComboBox.ValueMember = "Id";
                LocationComboBox.DisplayMember = "Name";
                LocationComboBox.DataSource = locations;

                var locations2 = currentItem.Shop.Locations.Where(l => !l.Disabled).ToList();

                Location2ComboBox.DataSource = locations2;
                Location2ComboBox.ValueMember = "Id";
                Location2ComboBox.DisplayMember = "Name";
            }
        }

        private void LoadResumeControls()
        {
            ConsecutiveLabel.Text = currentItem.Consecutive;
            StatusLabel.Text = currentItem.StatusName;
            ReferenceTextBox.Text = currentItem.Reference;
            NotesTextBox.Text = currentItem.Notes;

            //Solo para un mejor efecto visual
            MovementTypeComboBox.SelectedValue = currentItem.IdType;

            MovementTypeLabel.Text = DataHelper.GetUDCItemRow(dc, currentItem.IdType).Name;
            SubTypeComboBox.SelectedValue = currentItem.IdSubType ?? Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;

            SetTotalValuesToControls();

            switch (currentItem.IdStatus)
            {
                case (int)OperationStatus.NotConfirmed:
                    ConfirmOperationButton.Visible = true;
                    CancelOperationButton.Visible = false;
                    AddProductButton.Enabled = true;
                    DeleteProductButton.Enabled = true;
                    IdProductTextBox.Enabled = true;
                    UMComboBox.Enabled = true;
                    LocationComboBox.Enabled = true;
                    CountTextBox.Enabled = true;
                    LotComboBox.Enabled = true;
                    PrintButton.Enabled = false;
                    DeleteOperationButton.Visible = true;
                    ReferenceTextBox.Enabled = true;
                    NotesTextBox.Enabled = true;
                    SubTypeComboBox.Enabled = true;
                    break;
                case (int)OperationStatus.Confirmed:
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Location = ConfirmOperationButton.Location;
                    CancelOperationButton.Visible = true;
                    AddProductButton.Enabled = false;
                    DeleteProductButton.Enabled = false;
                    IdProductTextBox.Enabled = false;
                    UMComboBox.Enabled = false;
                    LocationComboBox.Enabled = false;
                    CountTextBox.Enabled = false;
                    LotComboBox.Enabled = false;
                    PrintButton.Enabled = true;
                    DeleteOperationButton.Visible = false;
                    ReferenceTextBox.Enabled = false;
                    NotesTextBox.Enabled = false;
                    SubTypeComboBox.Enabled = false;
                    break;
                default: //OperationStatus.Canceled
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Visible = false;
                    AddProductButton.Enabled = false;
                    DeleteProductButton.Enabled = false;
                    IdProductTextBox.Enabled = false;
                    UMComboBox.Enabled = false;
                    LocationComboBox.Enabled = false;
                    CountTextBox.Enabled = false;
                    LotComboBox.Enabled = false;
                    PrintButton.Enabled = true;
                    DeleteOperationButton.Visible = false;
                    ReferenceTextBox.Enabled = false;
                    NotesTextBox.Enabled = false;
                    SubTypeComboBox.Enabled = false;
                    break;
            }
        }

        private void SetTotalValuesToControls()
        {
            SubTotalLabel.Text = (currentItem.SubTotalOperationAmount ?? 0).ToString("N");
            TotalIVALabel.Text = (currentItem.TotalIVAOperation ?? 0).ToString("N");
            TotalOperationAmountLabel.Text = currentItem.OperationAmount.ToString("N");
            ConfirmOperationButton.Enabled = currentItem.IdStatus == (int)OperationStatus.NotConfirmed &&
                                  currentItem.OperationAmount != 0;
        }

        private void LoadGridDetails()
        {
            var details = currentItem.OperationDetails.OrderByDescending(o => o.AddedDate).ToList();
            dataGridView1.DataSource = details;
        }

        private void NewOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                int opType = Convert.ToInt32(MovementTypeComboBox.SelectedValue);
                string opName = ((UDCItem)MovementTypeComboBox.SelectedItem).Name;

                if (opType == (int)OperationType.Transfer && ApplicationHelper.GetCurrentShop().Locations.Count < 2)
                {
                    DialogHelper.ShowWarningInfo(this, "Su Tienda posee solamente una Ubicación por lo que no es posible crear una Transferencia entre Ubicaciones");
                    return;
                }

                if (DialogHelper.ShowWarningQuestion(this, string.Format("Agregará una operación de tipo: {0}. Está seguro?", opName)) == DialogResult.Yes)
                {
                    currentItem = new Common.Operation
                    {
                        Id = Guid.NewGuid(),
                        IdShop = Settings.Default.CurrentShop,
                        IdType = opType,
                        IdCashier = new Guid(Settings.Default.CurrentCashier),
                        Consecutive = GetNextConsecutive(),
                        IdOperationCurrency = ApplicationHelper.GetCurrencyByCurrentShop(),
                        ChangeRate = 1,
                        OperationAmount = 0,
                        Amount = 0,
                        IdStatus = (int)OperationStatus.NotConfirmed,
                        AddedDate = DateTime.Now
                    };

                    DataHelper.FillAuditoryValuesDesktop(currentItem);

                    ApplicationHelper.GetPosDataContext().Operations.InsertOnSubmit(currentItem);
                    ApplicationHelper.GetPosDataContext().SubmitChanges();

                    operationType = (OperationType)Enum.Parse(typeof(OperationType), opType.ToString());

                    LoadCurrentOperation();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error Insertando Operación");
                DialogHelper.ShowError(this, "Error realizando procesos internos", ex);
            }
        }

        private void DeleteOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (currentItem.IdStatus != (int)OperationStatus.NotConfirmed)
                {
                    DialogHelper.ShowWarningInfo(this, "No puede eliminar Operaciones Confirmadas o Canceladas");
                }
                else if (DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea eliminar la Operación actual?") ==
                         DialogResult.Yes)
                {
                    dc.Operations.DeleteOnSubmit(currentItem);
                    dc.SubmitChanges();

                    currentItem = null;
                    RequestNewOperation();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error DeleteSaleButton_Click", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado al intentar eliminar la Venta.", ex);
            }
        }

        private void GotoPreviousOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChanges();

                DateTime findDate = currentItem == null ? DateTime.Now : currentItem.AddedDate;

                var sale =
                    dc.Operations.Where(
                        o =>
                        (o.IdStatus != (int)OperationStatus.NotConfirmed ||
                         o.IdCashier == ApplicationHelper.GetCurrentCashierIdAsGuid()) &&
                        o.IdType != (int)OperationType.Receipt &&
                        o.IdType != (int)OperationType.Sale &&
                        o.IdType != (int)OperationType.Consignation &&
                        o.IdType != (int)OperationType.ConsignationReturn && o.AddedDate < findDate).OrderByDescending(
                            o => o.AddedDate).Take(1).SingleOrDefault();

                if (sale != null)
                {
                    currentItem = sale;
                    LoadCurrentOperation();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en GotoPreviousOrderButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando la Operación Anterior.", ex);
            }
        }

        private void GotoNextOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChanges();

                if (currentItem == null)
                {
                    GotoPreviousOrderButton_Click(null, null);
                }
                else
                {
                    DateTime findDate = currentItem.AddedDate;

                    var op =
                        dc.Operations.Where(
                            o => (o.IdStatus != (int)OperationStatus.NotConfirmed ||
                                  o.IdCashier == ApplicationHelper.GetCurrentCashierIdAsGuid()) &&
                                 o.IdType != (int)OperationType.Receipt &&
                                 o.IdType != (int)OperationType.Sale &&
                                 o.IdType != (int)OperationType.Consignation &&
                                 o.IdType != (int)OperationType.ConsignationReturn && o.AddedDate > findDate).OrderBy(
                                     o => o.AddedDate).Take(1).SingleOrDefault();

                    if (op != null)
                    {
                        currentItem = op;
                        LoadCurrentOperation();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en GotoNextOrderButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando la Operación Siguiente.", ex);
            }
        }

        private void FindSaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OperationConsecutiveTextBox.Text))
                {
                    SaveChanges();

                    var op =
                        dc.Operations.Where(
                            o =>
                            (o.IdStatus != (int)OperationStatus.NotConfirmed &&
                            o.IdType != (int)OperationType.Receipt &&
                             o.IdType != (int)OperationType.Sale &&
                             o.IdType != (int)OperationType.Consignation &&
                             o.IdType != (int)OperationType.ConsignationReturn) &&
                            o.OfficialConsecutive == OperationConsecutiveTextBox.Text).
                            SingleOrDefault();

                    if (op != null)
                    {
                        currentItem = op;
                        LoadCurrentOperation();
                        OperationConsecutiveTextBox.Text = string.Empty;
                    }
                    else
                    {
                        DialogHelper.ShowWarningInfo(this, "No se encontró la Operación buscada. Verifíque el número.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en FindSaleButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando el No. de Order de la Venta.", ex);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChanges();

                Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Cerrarndo formulario de Operaciones", ex);
                DialogHelper.ShowError(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (IsValidData())
                {
                    var rowDetail = CreatePrincipalProductDetailItem();

                    if (currentProduct.IdType == (int)ProductType.Composite)
                    {
                        var childs = CreateChildProducts(rowDetail.Id);

                        foreach (var child in childs)
                        {
                            currentItem.OperationDetails.Insert(0, child);
                        }

                        //Muy importante para el orden de impresion y presentacion
                        rowDetail.AddedDate = DateTime.Now;
                        currentItem.OperationDetails.Insert(0, rowDetail);
                    }
                    else
                    {
                        currentItem.OperationDetails.Insert(0, rowDetail);
                    }

                    dc.SubmitChanges();

                    RefreshDataAndSave();

                    ClearEntryControls();
                    IdProductTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando producto a la Operación", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha ocurrido al agregar el producto a la Operación. Si el error persiste cierre el formulario y ábralo nuevamente.",
                                       ex);
            }
        }


        private void ClearEntryControls()
        {
            IdProductTextBox.Text = string.Empty;
            ProductDescription.Text = string.Empty;
            UMComboBox.SelectedIndex = -1;
            UMComboBox.DataSource = null;

            LotComboBox.SelectedIndex = -1;
            LotComboBox.DataSource = null;

            CountTextBox.Text = "1";
            PriceLabel.Text = "0";
        }

        private string GetFirstLot(string idProductSimple)
        {
            var lots = GetLots(idProductSimple);

            if (lots == null)
            {
                return Constant.CFG_NOT_LOT_CODE;
            }

            dynamic lot = lots[0];
            return lot.Id;
        }

        private IEnumerable<OperationDetail> CreateChildProducts(Guid parentRow)
        {
            IList<OperationDetail> list = new List<OperationDetail>();

            var childs = currentProduct.ProductCompositions;

            foreach (var child in childs)
            {
                double count = Convert.ToDouble(CountTextBox.Text) * child.Count;
                string lot = GetFirstLot(child.IdProductSimple);

                if (lot == Constant.CFG_NOT_LOT_CODE)
                {
                    DialogHelper.ShowWarningInfo(this,
                                                 string.Format(
                                                     "El producto con Id: {0}, parte integrante de este Producto Compuesto, no tiene existencias en Inventario",
                                                     child.IdProductSimple));
                }

                var det = new OperationDetail
                {
                    Id = Guid.NewGuid(),
                    IdOperation = currentItem.Id,
                    IdLocation =
                        new Guid(Convert.ToString(LocationComboBox.SelectedValue)),

                    IdProduct = child.IdProductSimple,
                    IdProductType = (int)ProductType.CompositeChild,
                    IdProductParent = currentProduct.Id,
                    ParentRow = parentRow,
                    IdUM = child.IdUM,
                    Count = count,
                    Lot = lot,
                    IdPriceType = currentPrice.IdPriceType,
                    OperationPrice = child.Price,
                    OperationAmount = Math.Round((decimal)count * child.Price, 2),
                    Points = 0,
                    Management = 0,
                    Pediment =
                        dc.OperationDetails.Where(
                            o =>
                            o.IdProduct == child.IdProductSimple &&
                            o.Lot == lot &&
                            o.Operation.IdType == (int)OperationType.Receipt).Select(
                                o => o.Operation.Pediment).FirstOrDefault(),
                    AddedDate = DateTime.Now
                };

                if (operationType == OperationType.Transfer)
                {
                    det.IdLocation2 = new Guid(Convert.ToString(Location2ComboBox.SelectedValue));
                }

                list.Add(det);
            }

            return list;
        }

        private OperationDetail CreatePrincipalProductDetailItem()
        {
            double count = Convert.ToDouble(CountTextBox.Text);

            Guid guidDetail = Guid.NewGuid();
            var det = new OperationDetail
            {
                Id = guidDetail,
                IdOperation = currentItem.Id,
                IdLocation = new Guid(Convert.ToString(LocationComboBox.SelectedValue)),
                IdLocation2 = new Guid(Convert.ToString(Location2ComboBox.SelectedValue)),
                IdProductType = currentProduct.IdType,
                IdProductParent = currentProduct.Id,
                IdProduct = currentProduct.Id,
                ParentRow = guidDetail,
                IdUM = currentPrice.IdUM,
                Count = count,
                Lot =
                    currentProduct.IdType == (int)ProductType.Simple
                        ? LotComboBox.SelectedValue.ToString()
                        : string.Empty,
                IdPriceType = currentPrice.IdPriceType,
                OperationPrice = currentPrice.Price,
                OperationAmount = Math.Round(currentPrice.Price * (decimal)count, 2),
                Points = 0,
                Management = 0,
                Pediment = string.Empty,
                AddedDate = DateTime.Now
            };

            return det;
        }

        private bool IsValidData()
        {
            if (string.IsNullOrWhiteSpace(IdProductTextBox.Text))
            {
                DialogHelper.ShowError(this, "Debe entrar el No. de Artículo/Producto que desea vender.");
                return false;
            }

            double count;
            if (string.IsNullOrWhiteSpace(CountTextBox.Text) || !double.TryParse(CountTextBox.Text, out count))
            {
                DialogHelper.ShowError(this, "La cantidad que desea vender debe tener valor y ser de tipo numérico.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LotComboBox.SelectedValue.ToString()))
            {
                DialogHelper.ShowError(this, "El Lote es requerido para realizar la Venta.");
                return false;
            }

            if (operationType == OperationType.Transfer &&
                Convert.ToString(LocationComboBox.SelectedValue) == Convert.ToString(Location2ComboBox.SelectedValue))
            {
                DialogHelper.ShowError(this, "La Ubicación origen y destino deben ser diferentes.");
                return false;
            }

            return true;
        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe exitir una fila seleccionada");
                }
                else
                {
                    ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                    var item = (OperationDetail)dataGridView1.SelectedRows[0].DataBoundItem;

                    dc.OperationDetails.DeleteAllOnSubmit(dc.OperationDetails.Where(d => d.ParentRow == item.ParentRow));
                    dc.SubmitChanges();

                    RefreshDataAndSave();
                    IdProductTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando producto a la Operación", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha ocurrido al eliminando el producto de la Operación. Si el error persiste cierre el formulario y ábralo nuevamente.",
                                       ex);
            }
        }

        private void RefreshDataAndSave()
        {
            LoadGridDetails();
            CalculateResumeOperationValuesAndRefreshControls();
        }

        private decimal GetCurrentRate(Common.Operation operation)
        {
            if (operation.Shop.IdCurrency == operation.IdOperationCurrency)
            {
                return 1;
            }

            return DataHelper.GetChangeRate(dc, operation.IdOperationCurrency, operation.Shop.IdCurrency);
        }

        private void CalculateResumeOperationValuesAndRefreshControls()
        {
            currentItem.ChangeRate = GetCurrentRate(currentItem);
            currentItem.DiscountPercentApply = 0;

            decimal subTotalOpAmount = currentItem.OperationDetails.Sum(d => d.OperationAmount);
            decimal totalOpAmount = subTotalOpAmount;

            currentItem.TotalPointOperation = 0;
            currentItem.SubTotalOperationAmount = subTotalOpAmount;
            currentItem.TotalManagementOperation = 0;
            currentItem.TotalIVAOperation = 0;
            currentItem.TotalOperationDiscount = 0;

            if (DataHelper.IsActiveRoundByFive(dc, currentItem.IdOperationCurrency))
            {
                currentItem.OperationAmount = ToolHelper.RoundByFiveByExcess(totalOpAmount);
                currentItem.Amount = ToolHelper.RoundByFiveByExcess(totalOpAmount * currentItem.ChangeRate);
            }
            else
            {
                currentItem.OperationAmount = Math.Round(totalOpAmount, 2);
                currentItem.Amount = Math.Round(totalOpAmount * currentItem.ChangeRate, 2);
            }

            DataHelper.FillAuditoryValuesDesktop(currentItem);

            SetTotalValuesToControls();

            dc.SubmitChanges();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrintTicket();
            }
            catch (Exception ex)
            {
                Logger.Error("Error en ReceiptEdit.PrintButton_Click", ex);
                DialogHelper.ShowError(this, "Error inesperado durante la impresión del Reporte.", ex);
            }
        }

        private void PrintTicket()
        {
            var rv = new ReportViewer("Mega.POS.Report.EntryExitDetails.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Operation") { Value = new List<Common.Operation> { currentItem } });

            rv.LocalReport.SubreportProcessing += FillSubreportEventHandler;

            rv.Show();
        }

        void FillSubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "EntryExitDetailsSubReport")
            {
                e.DataSources.Add(new ReportDataSource("OperationDetails") { Value = currentItem.OperationDetails.OrderByDescending(o => o.AddedDate) });
            }
        }

        private void ConfirmOperationButton_Click(object sender, EventArgs e)
        {
            if (currentItem.IdType != (int)OperationType.Transfer && Convert.ToInt32(SubTypeComboBox.SelectedValue) == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                DialogHelper.ShowWarningInfo(this, "Debe seleccionar el [Motivo] de este Movimiento antes de Confirmarlo");
                return;
            }

            var result = DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea Confirmar y reflejar en el Inventario esta operación? Después de Confirmada no podrá modificar ninguno de sus valores, solo podrá ser Cancelada");

            if (result == DialogResult.No) return;

            try
            {
                Logger.InfoFormat("Iniciando la Confirmación del Movimiento de Inventario: Consecutive {0}, Tipo {1}",
                                  currentItem.Consecutive, currentItem.TypeName);

                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede realizar la Confirmación del Movimiento de Inventario porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    ConfirmOperation();

                    trans.Complete();
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                Logger.Error(
                    string.Format("Error Confirmando el Movimiento de Inventario. Consecutivo {0} Tipo {1}",
                                  currentItem.Consecutive, currentItem.TypeName), ex);


                const string msg =
                    @"Un Excepción ha ocurrido durante la confirmación de la Operación. 
Para que la aplicación se recupere puede que este formulario deba cerrarse. 
Si el error persiste cierre la aplicación y ejecútela nuevamente por favor.";

                DialogHelper.ShowError(this, msg, ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void SaveChanges()
        {
            if (currentItem != null && currentItem.IdStatus == (int)OperationStatus.NotConfirmed)
            {
                currentItem.Reference = ReferenceTextBox.Text;
                currentItem.Notes = NotesTextBox.Text;
                currentItem.SubType = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(SubTypeComboBox.SelectedValue ?? Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY));

                dc.SubmitChanges();
            }
        }

        private void ConfirmOperation()
        {
            SaveChanges();

            currentItem.OfficialConsecutive = GetNextOfficialConsecutive();
            currentItem.Status = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Confirmed);

            DataHelper.FillAuditoryValuesDesktop(currentItem);
            dc.SubmitChanges();

            GenerateKardexAndInventoryEntries(false);

            try
            {
                PrintTicket();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Un error inesperado ocurrio durante la Impresión del Ticket");
                Logger.Error("Error imprimiendo Ticket con numero: " + currentItem.OfficialConsecutive, ex);
            }
        }

        private void CancelOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.InfoFormat("Iniciando la Cancelación del Movimiento de Inventario: Consecutive {0}, OfficialConsecutive {1}, Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName);

                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede realizar la Cancelación del Movimiento de Inventario porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                if (currentItem.ModifiedDate.Date != DateTime.Today)
                {
                    DialogHelper.ShowWarningInfo(this, "No puede Cancelar una Operación de otro día.");
                    return;
                }

                if (ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
                {
                    authorizedBy = ApplicationHelper.GetCurrentUser();

                    var result = DialogHelper.ShowWarningQuestion(this,
                                                                  "¿Está seguro que desea Cancelar esta operación? Después de Cancelada, las cantidades reflejadas previamente en el Inventario y Kardex serán revertidas.");
                    if (result == DialogResult.No) return;
                }
                else
                {
                    var f = new AuthorizationRequired(Constant.SupervisorOrMore,
                                                                        "Un usuario con Rol de [Supervisor] o [Gerente] necesita autorizar esta Operación.");

                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        authorizedBy = f.AuthorizedUser;
                    }
                    else
                    {
                        return;
                    }
                }

                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    Cursor = Cursors.WaitCursor;

                    dc.SubmitChanges();

                    currentItem.CancelConsecutive = GetNextCanceledConsecutive();
                    currentItem.Status = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Canceled);
                    DataHelper.FillAuditoryValuesDesktop(currentItem);
                    currentItem.User = dc.Users.Where(u => u.Id == authorizedBy).Single();

                    GenerateKardexAndInventoryEntries(true);
                    dc.SubmitChanges();

                    trans.Complete();
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }

                Cursor = Cursors.Default;

                try
                {
                    PrintTicket();
                }
                catch (Exception ex)
                {
                    DialogHelper.ShowError(this, "Un error inesperado ocurrio durante la Impresión del Ticket");
                    Logger.Error("Error imprimiendo Ticket con numero: " + currentItem.OfficialConsecutive, ex);
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                Logger.Error(
                    string.Format("Error Cancelando el Movimiento de Inventario. Consecutivo {0} OfficialConsecutive {1} Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName), ex);

                DialogHelper.ShowError(this,
                                       "Un Error delicado ha sido detectado durante la Cancelación de la Operación. Para que la aplicación se recupere puede que este formulario deba cerrarse. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.");

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void GenerateKardexAndInventoryEntries(bool isCanceling)
        {
            int sign = DataHelper.GetOperationSign(dc, currentItem.IdType);

            if (isCanceling)
            {
                sign *= -1;
            }

            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Inventories);

            foreach (var op in currentItem.OperationDetails)
            {
                double invCount = 0;

                if (op.IdProductType != (int)ProductType.Composite)
                {
                    Inventory inv;

                    try
                    {
                        inv = dc.Inventories.Where(
                            i => i.IdProduct == op.IdProduct && i.IdLocation == op.IdLocation && i.Lot == op.Lot).
                            SingleOrDefault();
                    }
                    catch (Exception ex)
                    {
                        string msg = string.Format(
                                "Existen filas de Inventario Duplicadas para el Producto: {0}, Ubicación: {1}, Lote: {2}",
                                op.IdProduct, op.IdLocation, op.Lot);

                        Logger.Error(msg);
                        throw new Exception(msg, ex);
                    }

                    if (inv == null)
                    {
                        inv = new Inventory
                        {
                            Id = Guid.NewGuid(),
                            IdShop = op.Location.IdShop,
                            IdLocation = op.IdLocation,
                            IdProduct = op.IdProduct,
                            Lot = op.Lot,
                            LotAddedDate = DateTime.Now,
                            Count = sign * op.Count,
                            ModifiedDate = DateTime.Now
                        };

                        dc.Inventories.InsertOnSubmit(inv);
                    }
                    else
                    {
                        inv.Count = inv.Count + (sign * op.Count);
                        inv.ModifiedDate = DateTime.Now;
                    }

                    invCount = inv.Count;

                    if (invCount < 0 && !op.Location.AllowNegativeSale)
                    {
                        string msg = string.Format(
                            "La Confirmacion no se puede realizar porque generaria Inventarios negativos sobre el Producto: {0}, Almacen {1}, Cantidad a Vender: {2}, Cantidad final en Inventario: {3}",
                            op.Product.Id, op.Location.Name, op.Count, inv.Count);

                        DialogHelper.ShowError(this, msg);

                        throw new Exception(msg);
                    }
                }

                var k = new Kardex
                {
                    Id = Guid.NewGuid(),
                    IdShop = Settings.Default.CurrentShop,
                    IdOperation = currentItem.Id,
                    IdOperationType = currentItem.IdType,
                    IdLocation = op.IdLocation,
                    IdOperationCurrency = currentItem.IdOperationCurrency,
                    OperationDate = DateTime.Now,
                    ChangeRate = currentItem.ChangeRate,
                    IdProduct = op.IdProduct,
                    IdProductType = op.IdProductType,
                    IdProductComposite = op.IdProductParent,
                    IdUM = op.IdUM,
                    Lot = op.Lot,
                    IdPriceType = op.IdPriceType,
                    OperationPrice = op.OperationPrice,
                    OperationCount = op.Count * sign,
                    InventoryCount = invCount,
                    ByCancelation = isCanceling,
                    Notes = string.Format("{0} - {1}", currentItem.IdSubType, currentItem.SubTypeName)
                };

                dc.Kardexes.InsertOnSubmit(k);

                if (operationType == OperationType.Transfer)
                {
                    //double invCont = 0;
                    Inventory invLoc2 = null;

                    if (op.IdProductType != (int)ProductType.Composite)
                    {
                        invLoc2 = dc.Inventories.Where(
                            i =>
                            i.IdProduct == op.IdProduct && i.IdLocation == op.IdLocation2 &&
                            i.Lot == op.Lot).SingleOrDefault();

                        if (invLoc2 == null)
                        {
                            invLoc2 = new Inventory
                            {
                                Id = Guid.NewGuid(),
                                IdShop = op.Location.IdShop,
                                IdLocation = op.IdLocation2,
                                IdProduct = op.IdProduct,
                                Lot = op.Lot,
                                LotAddedDate = DateTime.Now,
                                Count = sign * -1 * op.Count,
                                ModifiedDate = DateTime.Now
                            };

                            dc.Inventories.InsertOnSubmit(invLoc2);
                        }
                        else
                        {
                            invLoc2.Count += (sign * -1 * op.Count);
                            invLoc2.ModifiedDate = DateTime.Now;
                        }

                        if (invLoc2.Count < 0 && !op.Location.AllowNegativeSale)
                        {
                            string msg = string.Format(
                                "La Operación no se puede realizar porque generaria Inventarios negativos sobre el Producto: {0}, Dim {1}, Cantidad a Vender: {2}, Cantidad final en Inventario: {3}",
                                op.Product.Id, op.Operation.DimName, op.Count, invLoc2.Count);

                            DialogHelper.ShowError(this, msg);

                            throw new Exception(msg);
                        }
                    }

                    var k1 = new Kardex
                    {
                        Id = Guid.NewGuid(),
                        IdShop = Settings.Default.CurrentShop,
                        IdOperation = currentItem.Id,
                        IdOperationType = currentItem.IdType,
                        IdDim = currentItem.IdDim,
                        IdOperationCurrency = currentItem.IdOperationCurrency,
                        OperationDate = DateTime.Now,
                        ChangeRate = currentItem.ChangeRate,
                        IdProduct = op.IdProduct,
                        IdProductType = op.IdProductType,
                        IdProductComposite = op.IdProductParent,
                        IdUM = op.IdUM,
                        Lot = op.Lot,
                        IdPriceType = op.IdPriceType,
                        OperationPrice = op.OperationPrice,
                        OperationCount = op.Count * sign * -1,
                        InventoryCount = invLoc2.Count,
                        ByCancelation = isCanceling,
                    };

                    dc.Kardexes.InsertOnSubmit(k1);
                }

                dc.SubmitChanges();
            }
        }

        private void EntryExitOperationEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    ConfirmOperationButton.PerformClick();
                    break;
                case Keys.F11:
                    PrintButton.PerformClick();
                    break;

                case Keys.F9:
                    NewOperationButton.PerformClick();
                    break;
            }
        }

        private void OperationConsecutiveTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                FindOperationButton.PerformClick();
            }
        }

        private string GetNextConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.InventoryMovementConsecutive).ToString();
        }

        private string GetNextCanceledConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.InventoryMovementCanceledConsecutive).ToString();
        }

        private string GetNextOfficialConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.InventoryMovementOfficialConsecutive).ToString();
        }

        private void IdProductTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (IdProductTextBox.Text.Trim() != string.Empty)
                {
                    string pid = IdProductTextBox.Text.Trim();

                    currentProduct =
                        dc.Products.Where(p => !p.Disabled && (p.Id == pid || p.BarCode == pid)).SingleOrDefault();

                    if (currentProduct == null)
                    {
                        DialogHelper.ShowError(this,
                                               "Este producto no existe en el Cátalogo actual o no está habilitado para la venta. Si desea salir de este campo sin agregar déjelo en blanco.");
                        e.Cancel = true;
                    }
                    else
                    {
                        if (currentProduct.IdType == (int)ProductType.Composite && !IsExitOperation)
                        {
                            e.Cancel = true;
                            DialogHelper.ShowError(this,
                                               "No puede dar entrada directamente Códigos de Productos Compuestos. En su lugar introduzca individualmente cada uno de sus componentes para que pueda indicar correctamente el LOTE al que pertenecen");

                        }

                        var sb = new StringBuilder();
                        sb.Append("[" + currentProduct.Id + "]").Append(" [" + currentProduct.BarCode + "]").Append(
                            " " + currentProduct.Name);

                        ProductDescription.Text = sb.ToString();

                        prices = GetPrices(currentProduct.Product_Prices);

                        if (prices.Count() == 0)
                        {
                            DialogHelper.ShowError(this,
                                                   "Este producto no tiene Lista de Precios vigente en el Cátalogo actual. Debe existir al menos un precio Activo y Vigente en Fecha para la Moneda de Operación. Si desea salir de este campo sin agregar déjelo en blanco.");
                            e.Cancel = true;
                        }
                        else
                        {
                            UMComboBox.ValueMember = "Id";
                            UMComboBox.DisplayMember = "Name";
                            UMComboBox.DataSource =
                                prices.Select(p => new { Id = p.IdUM, Name = p.UDCItem.Code }).ToList();

                            if (prices.Select(p => p.IdUM).Contains(currentProduct.IdUM))
                            {
                                UMComboBox.SelectedValue = currentProduct.IdUM;
                            }

                            FillProductPricesControls();
                            FillLots();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error validando entrada de prodcuto en ventas", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha sido detectado validando el Producto. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.");
            }
        }

        private IEnumerable<Product_Price> GetPrices(IEnumerable<Product_Price> productPrices)
        {
            var pprices = productPrices.Where(p => !p.Disabled && p.IdUM == currentProduct.IdUM &&
                                                  p.IdCurrency == currentItem.IdOperationCurrency &&
                                                  p.IdClientType == (int)DimType.DIM &&
                                                  (p.IdCompany == null || p.IdCompany == currentItem.Shop.IdCompany) &&
                                                  (p.IdShop == null || p.IdShop == currentItem.IdShop) &&
                                                  (p.IdPriceType == (int)ListPriceType.Normal ||
                                                   (SqlMethods.DateDiffDay(p.InitialDate, DateTime.Today) >= 0) &&
                                                   SqlMethods.DateDiffDay(DateTime.Today, p.FinalDate) >= 0));

            pprices = pprices.OrderByDescending(p => p.IdShop).ThenByDescending(p => p.IdCompany)
                .ThenByDescending(p => p.UDCItem2.Optional1).Take(1);

            return pprices;
        }

        private void FillProductPricesControls()
        {
            if (UMComboBox.SelectedIndex != -1)
            {
                currentPrice = prices.Where(p => p.IdUM == Convert.ToInt32(UMComboBox.SelectedValue)).Single();

                PriceLabel.Text = currentPrice.Price.ToString("N");
            }
        }

        private void FillLots()
        {
            if (IdProductTextBox.Text != string.Empty && currentProduct != null)
            {
                LotComboBox.ValueMember = "Id";
                LotComboBox.DisplayMember = "Name";

                if (currentProduct.IdType == (int)ProductType.Simple)
                {
                    var lots = GetLots(currentProduct.Id);

                    LotComboBox.DataSource = lots.Count != 0
                                                 ? lots
                                                 : new ArrayList { new { Id = Constant.CFG_NOT_LOT_CODE, Name = "0: Libre de Lote" } };

                    if (lots.Count == 0)
                    {
                        DialogHelper.ShowWarningInfo(this,
                                                     "No hay existencias del Producto para la Ubicación seleccionado");
                    }
                }
                else
                {
                    LotComboBox.DataSource = new ArrayList { new { Id = Constant.CFG_NOT_LOT_CODE, Name = "0: Lotes de Hijos" } };
                }
            }
        }

        private IList GetLots(string idProduct)
        {
            IList invLots;

            if (IsExitOperation)
            {
                invLots = dc.Inventories.Where(i =>
                                               i.IdLocation.ToString() == Convert.ToString(LocationComboBox.SelectedValue) &&
                                               i.IdProduct == idProduct &&
                                               i.Count > 0).OrderBy(i => i.LotAddedDate).
                    Select(i =>
                           new
                           {
                               Id = i.Lot,
                               Name =
                           string.Format("{0}: {1} {2}", i.Lot, i.Count, currentProduct.UMCode)
                           }).ToList();
            }
            else
            {
                //Se supone que esta mercancia esta retornando por lo que en algun momento se recibio por
                //explicitamente por un RECIBO DE MERCANCIA por lo que mostraremos todos los Lotes que existan
                //para dicho codigo aun cuando esten en cero claro
                //TODO: Quizas exista el caso para Transferencia entre CNV que debamos permitir entrar el Lote

                invLots = dc.Inventories.Where(i => i.IdProduct == idProduct).
                    Select(i =>
                           new
                           {
                               Id = i.Lot,
                               Name = i.Lot
                           }).Distinct().ToList();

                //TODO: Este Distint no debe funcionar aun, corregirlo
            }

            return invLots;
        }

        protected bool IsExitOperation
        {
            get
            {
                return operationType != null && DataHelper.GetOperationSign(dc, (int)operationType) == -1;
            }
        }

        private void IdProductTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IdProductTextBox.Text.Length == Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_BARCODE_MAX_SIZE_UDCITEM_KEY).Optional1))
            {
                AddProductButton.PerformClick();
            }
        }

        private void IdProductTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                CountTextBox.Focus();
            }
        }

        private void CountTextBox_Click(object sender, EventArgs e)
        {
            CountTextBox.SelectAll();
        }

        private void CountTextBox_Enter(object sender, EventArgs e)
        {
            CountTextBox.SelectAll();
        }

        private void CountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddProductButton.PerformClick();
            }
        }

        private void CountTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (CountTextBox.Text.Trim() != string.Empty && IdProductTextBox.Text.Trim() != string.Empty)
                {
                    double count;

                    if (double.TryParse(CountTextBox.Text, out count))
                    {
                        if (IsExitOperation)
                        {
                            var inv = dc.Inventories.Where(i => i.IdLocation != null &&
                                                            i.IdLocation.ToString() ==
                                                            Convert.ToString(LocationComboBox.SelectedValue) &&
                                                            i.IdProduct == currentProduct.Id &&
                                                            i.Lot == LotComboBox.SelectedValue.ToString()).
                            SingleOrDefault();

                            if (inv == null || count > inv.Count)
                            {
                                DialogHelper.ShowWarningInfo(this,
                                                             "La cantidad que desea Vender sobrepasará la existencia actual para la Ubicación y Lote seleccionado");
                            }
                        }
                    }
                    else
                    {
                        DialogHelper.ShowError(this, "El tipo de dato de cantidad debe ser numérico");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error CountTextBox_Validating", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsExitOperation)
                {
                    FillLots();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error LocationComboBox_SelectedIndexChanged", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void UMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillProductPricesControls();
                //FillLots();
            }
            catch (Exception ex)
            {
                Logger.Error("Error UMComboBox_SelectedIndexChanged", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void Location2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EntryExitOperationEdit_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }

    }
}
