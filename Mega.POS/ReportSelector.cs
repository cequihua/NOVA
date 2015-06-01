using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;
using Microsoft.Reporting.WinForms;

namespace Mega.POS
{
    public partial class ReportSelector : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReportSelector));

        protected string SelectedReport { get; set; }
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();

        public ReportSelector()
        {
            InitializeComponent();
        }

        private void ReportSelector_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            try
            {
                if (ApplicationHelper.IsOpenSimilarForm(this))
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                    Close();
                    return;
                }

                LoadComboBoxData();
                SetDefaulValuesToControls();
                EnableFilterControls();
            }
            catch (Exception ex)
            {
                Logger.Error("Error en ReportSelector.ReportSelector_Load");
                DialogHelper.ShowError(this, "Ha ocurrido un error durante el proceso de Carga de este formulario. Intente de nuevo por favor.", ex);
            }

        }

        private void SetDefaulValuesToControls()
        {
            InitialDateCheckBox.Checked = true;
            FinalDateCheckBox.Checked = true;
            InitialDateTimePicker.Value = DateTime.Today;
            FinalDateTimePicker.Value = DateTime.Today;
            InventoryByLocationReportRadioButton.Checked = true;
        }

        private void LoadComboBoxData()
        {
            LocationComboBox.ValueMember = "Id";
            LocationComboBox.DisplayMember = "Name";

            ProductComboBox.ValueMember = "Id";
            ProductComboBox.DisplayMember = "Name";

            UserComboBox.ValueMember = "Id";
            UserComboBox.DisplayMember = "Name";

            CashierComboBox.ValueMember = "Id";
            CashierComboBox.DisplayMember = "Name";

            DimTypeComboBox.ValueMember = "Id";
            DimTypeComboBox.DisplayMember = "Name";

            MovementTypeComboBox.ValueMember = "Id";
            MovementTypeComboBox.DisplayMember = "Name";

            var locations = dc.Locations.Where(l => l.IdShop == Properties.Settings.Default.CurrentShop).Select(
                    l => new { Id = l.Id.ToString(), l.Name }).ToList();
            locations.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todas-" });
            LocationComboBox.DataSource = locations;
            LocationComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

            var products = dc.Products.Select(p => new { p.Id, Name = p.Id + " " + p.Name }).OrderBy(p => p.Id).ToList();
            products.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todos-" });
            ProductComboBox.DataSource = products;
            ProductComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

            var users = dc.Users.Where(u => u.User_Shops.Select(s => s.IdShop).Contains(Properties.Settings.Default.CurrentShop))
                    .Select(u => new {u.Id, u.Name}).ToList();

            users.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todos-" });
            UserComboBox.DataSource = users;
            UserComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

            var cashiers = dc.Cashiers.Where(c => c.IdShop == Properties.Settings.Default.CurrentShop).Select(
                c => new { Id = c.Id.ToString(), c.Name }).ToList();
            cashiers.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todas-" });
            CashierComboBox.DataSource = cashiers;
            CashierComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

            var dimTypes = DataHelper.GetUDCDimType(dc).Select(
                c => new { Id = c.Id.ToString(), c.Name }).ToList();
            dimTypes.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todas-" });
            DimTypeComboBox.DataSource = dimTypes;
            DimTypeComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

            var movementTypes = DataHelper.GetUDCManualMovementType(dc).Select(
                c => new { Id = c.Id.ToString(), c.Name }).ToList();

            movementTypes.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todas-" });
            MovementTypeComboBox.DataSource = movementTypes;
            MovementTypeComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
        }

        private void EnableFilterControls()
        {
            LocationComboBox.Enabled = false;
            LotTextBox.Enabled = false;
            ProductComboBox.Enabled = false;
            ExcludeZeroCountCheckBox.Enabled = false;
            DimTypeComboBox.Enabled = false;
            InitialDateCheckBox.Enabled = false;
            InitialDateTimePicker.Enabled = false;
            FinalDateCheckBox.Enabled = false;
            FinalDateTimePicker.Enabled = false;
            DimTextBox.Enabled = false;
            UserComboBox.Enabled = false;
            UserLabel.Text = "Asesor:";
            CashierComboBox.Enabled = false;
            JustWithDiffCheckBox.Enabled = false;
            MovementTypeComboBox.Enabled = false;
            OnlyPromortionsCheckBox.Enabled = false;
            BarCodeTextBox.Enabled = false;
            OnlyApplyDiscountCheckBox.Enabled = false;
            OnlyApplyDiscountCheckBox.Enabled = false;
            SpecificPriceToCurrentShopCheckBox.Enabled = false;
            txtNumeroFolio.Visible = false;

            if (SelectedReport == InventoryByLocationReportRadioButton.Name)
            {
                LocationComboBox.Enabled = true;
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                ExcludeZeroCountCheckBox.Enabled = true;
            }
            else if (SelectedReport == ConsignationInventoryRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                ExcludeZeroCountCheckBox.Enabled = true;
                DimTypeComboBox.Enabled = true;
                DimTextBox.Enabled = true;
            }
            else if (SelectedReport == TotalInventoryRadioButton.Name)
            {
                LocationComboBox.Enabled = true;
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                DimTextBox.Enabled = true;
            }
            else if (SelectedReport == SaleReportRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                DimTypeComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                LocationComboBox.Enabled = true;
            }
            else if (SelectedReport == CashierCloseReportRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                JustWithDiffCheckBox.Enabled = true;
            }
            else if (SelectedReport == CashierVerificationRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                JustWithDiffCheckBox.Enabled = true;
            }
            else if (SelectedReport == ConsignationAccountStateReportRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                DimTypeComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                LocationComboBox.Enabled = true;
            }
            else if (SelectedReport == CreditAmountStateRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                LocationComboBox.Enabled = true;
            }

            else if (SelectedReport == SalesBySimpleProductsAndPromotionsReportRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                DimTypeComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                LocationComboBox.Enabled = true;
            }
            else if (SelectedReport == ReceiptsInTransitReportRadioButton.Name)
            {
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
            }
            else if (SelectedReport == ConsignationReturnRadioButton.Name)
            {
                LocationComboBox.Enabled = true;
                LotTextBox.Enabled = true;
                ProductComboBox.Enabled = true;
                DimTypeComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
            }
            else if (SelectedReport == ProductsCatalogReportRadioButton.Name)
            {
                ProductComboBox.Enabled = true;
                OnlyPromortionsCheckBox.Enabled = true;
                BarCodeTextBox.Enabled = true;
                OnlyApplyDiscountCheckBox.Enabled = true;
                OnlyApplyDiscountCheckBox.Enabled = true;
                SpecificPriceToCurrentShopCheckBox.Enabled = true;
            }
            else if (SelectedReport == CashierMovementRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
                MovementTypeComboBox.Enabled = true;
                UserLabel.Text = "Hecha por:";
            }
            else if (SelectedReport == ProductMovementRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                LocationComboBox.Enabled = true;
                ProductComboBox.Enabled = true;
                LotTextBox.Enabled = true;
            }
            else if (SelectedReport == InventoryMovementByProductsRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                LocationComboBox.Enabled = true;
                ProductComboBox.Enabled = true;
                LotTextBox.Enabled = true;
            }
            else if (SelectedReport == OperationsByLocationByProductsRadioButton.Name)
            {
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                LocationComboBox.Enabled = true;
                ProductComboBox.Enabled = true;
                LotTextBox.Enabled = true;
            }
            else if (SelectedReport == DimCreditCollectrRadioButton.Name)
            {
                DimTypeComboBox.Enabled = true;
                InitialDateCheckBox.Enabled = true;
                InitialDateTimePicker.Enabled = true;
                FinalDateCheckBox.Enabled = true;
                FinalDateTimePicker.Enabled = true;
                DimTextBox.Enabled = true;
                UserComboBox.Enabled = true;
                CashierComboBox.Enabled = true;
            }
            else if (SelectedReport == CashierCloseReportRadioButtonByOperationPay.Name)
            {
                CashierComboBox.Enabled = true;
                txtNumeroFolio.Text = "";
                txtNumeroFolio.Visible = true;
                txtNumeroFolio.Focus();
            }
        }

        private void ViewReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidFilterValues())
                {
                    return;
                }

                dc = ApplicationHelper.GetFreePosDataContext();

                if (SelectedReport == InventoryByLocationReportRadioButton.Name ||
                        SelectedReport == ConsignationInventoryRadioButton.Name ||
                        SelectedReport == TotalInventoryRadioButton.Name)
                {
                    ShowInventoryReport();
                }
                else if (SelectedReport == SaleReportRadioButton.Name)
                {
                    ShowSalesReport();
                }
                else if (SelectedReport == CashierCloseReportRadioButton.Name)
                {
                    ShowCashierCloseReport();
                }
                else if (SelectedReport == CashierVerificationRadioButton.Name)
                {
                    CashierVerificationReport();
                }
                else if (SelectedReport == SalesBySimpleProductsAndPromotionsReportRadioButton.Name)
                {
                    SalesBySimpleProductsAndPromotionsReport();
                }
                else if (SelectedReport == ReceiptsInTransitReportRadioButton.Name)
                {
                    ShowReceiptsInTransitReport();
                }
                else if (SelectedReport == ConsignationReturnRadioButton.Name)
                {
                    ShowConsignationReturnReport();
                }
                else if (SelectedReport == ConsignationAccountStateReportRadioButton.Name)
                {
                    ShowConsignationAccountStateReport();
                }
                else if (SelectedReport == ProductsCatalogReportRadioButton.Name)
                {
                    ShowProductsCatalogReport();
                }
                else if (SelectedReport == CashierMovementRadioButton.Name)
                {
                    ShowCashierMovementReport();
                }
                else if (SelectedReport == CreditAmountStateRadioButton.Name)
                {
                    ShowCreditAmountStateReport();
                }
                else if (SelectedReport == InventoryMovementByProductsRadioButton.Name)
                {
                    InventoryMovementByProductsReport();
                }
                else if (SelectedReport == ProductMovementRadioButton.Name)
                {
                    ProductMovementsReport();
                }
                else if (SelectedReport == OperationsByLocationByProductsRadioButton.Name)
                {
                    OperationsByLocationByProductsReport();
                }
                else if (SelectedReport == DimCreditCollectrRadioButton.Name)
                {
                    ShowCreditCollectReport();
                }
                else if (SelectedReport ==  CashierCloseReportRadioButtonByOperationPay.Name)
                {
                     decimal Folio;
                      if (string.IsNullOrWhiteSpace(txtNumeroFolio.Text) ||
                          !decimal.TryParse(txtNumeroFolio.Text, out Folio))
                      {
                          DialogHelper.ShowWarningInfo(this, "Por favor, capture el número de folio a procesar");
                          txtNumeroFolio.Focus();
                      }
                      else
                      {
                           ShowCashierClosebyOperationPay();
                      }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en ReportSelector.ViewReportButton_Click");
                DialogHelper.ShowError(this, "Ha ocurrido un error durante el procesamiento del Reporte Solicitado. Intente de nuevo por favor.", ex);
            }
        }

        private void ShowCreditCollectReport()
        {
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
           
            var collects =
                dc.DimCreditCollects.Where(c => c.Cashier.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                    c.IdStatus == (int)OperationStatus.Confirmed);

            if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                collects = collects.Where(c => c.Dim.IdType == dimType);
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                collects = collects.Where(c => c.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (InitialDateCheckBox.Checked)
            {
                collects = collects.Where(c => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, c.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                collects = collects.Where(c => SqlMethods.DateDiffDay(c.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                collects = collects.Where(c => c.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                collects = collects.Where(c => c.ModifiedBy.ToString() == user);
            }

            collects = collects.OrderBy(c => c.ModifiedDate);

            var rv = new ReportViewer("Mega.POS.Report.CreditColects.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("CreditCollect", collects));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowProductsCatalogReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);

            var products = dc.Products.Select(p => p);

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                products = products.Where(p => p.Id == product);
            }

            if (OnlyPromortionsCheckBox.Checked)
            {
                products = products.Where(p => p.IdType == (int)ProductType.Composite
                    || (p.Product_Prices.All(pp => pp.IdPriceType == (int)ListPriceType.Promotional
                        && SqlMethods.DateDiffDay(pp.InitialDate, DateTime.Now) >= 0
                        && SqlMethods.DateDiffDay(DateTime.Now, pp.FinalDate) >= 0)));
            }



            if (OnlyApplyDiscountCheckBox.Checked)
            {
                products = products.Where(p => p.ApplyDiscount);
            }

            if (SpecificPriceToCurrentShopCheckBox.Checked)
            {
                products = products.Where(p => p.Product_Prices.Any(pp => pp.IdShop.ToString() == Properties.Settings.Default.CurrentShop));
            }

            if (!string.IsNullOrWhiteSpace(BarCodeTextBox.Text))
            {
                products = products.Where(p => p.BarCode.Contains(BarCodeTextBox.Text));
            }

            var rv = new ReportViewer("Mega.POS.Report.ProductsCatalog.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Products", products));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.LocalReport.SubreportProcessing += FillSubreportEventHandler;

            rv.Show();

        }

        void FillSubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "CompositionSubreport")
            {
                e.DataSources.Add(new ReportDataSource("Products") { Value = dc.ProductCompositions.Where(pc => pc.IdProductComposite == e.Parameters[0].Values[0].ToString()) });
            }

            if (e.ReportPath == "ProductPricesSubreport")
            {
                e.DataSources.Add(new ReportDataSource("ProductPrices") { Value = dc.Product_Prices.Where(pp => pp.IdProduct == e.Parameters[0].Values[0].ToString()) });
            }
        }

        private void ProductMovementsReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var movements = dc.Kardexes.Where(k => k.IdLocation != null && k.IdShop.ToString() == Properties.Settings.Default.CurrentShop);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                movements = movements.Where(od => od.IdLocation.ToString() == location);
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                movements = movements.Where(od => od.IdProduct == product);
            }

            if (InitialDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.OperationDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(o.OperationDate, FinalDateTimePicker.Value) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                movements = movements.Where(od => od.Lot == LotTextBox.Text.Trim());
            }

            var result = movements.OrderBy(k => k.OperationDate);

            var rv = new ReportViewer("Mega.POS.Report.ProductMovements.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Movements", result));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }
        private void InventoryMovementByProductsReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var movements =
                dc.Kardexes.Where(
                    k => k.IdLocation != null && k.IdShop.ToString() == Properties.Settings.Default.CurrentShop);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                movements = movements.Where(od => od.IdLocation.ToString() == location);
            }

            movements = product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString()
                            ? movements.Where(od => od.IdProduct == product)
                            : movements.Where(
                                od =>
                                od.Product.IdType == (int)ProductType.Simple ||
                                od.Product.IdType == (int)ProductType.CompositeChild);


            if (InitialDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.OperationDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(o.OperationDate, FinalDateTimePicker.Value) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                movements = movements.Where(od => od.Lot == LotTextBox.Text.Trim());
            }

            var result = movements.OrderBy(k => k.OperationDate).GroupBy(m => new { m.IdProduct }).
                Select(g => new InventoryMovementReport
                                {
                                    IdProduct = g.Key.IdProduct,
                                    OperationCount = g.Count(),
                                    InitialInventoryCount = g.First().InventoryCount - g.First().OperationCount,
                                    Entries = g.Sum(k => k.OperationCount > 0 ? k.OperationCount : 0),
                                    Exits = g.Sum(k => k.OperationCount < 0 ? -k.OperationCount : 0),
                                    FinalInventoryCount = g.OrderByDescending(l => l.OperationDate).First().InventoryCount,
                                    UMCode = g.First().Product.UMCode,
                                    ProductName = g.First().Product.Name
                                });

            var rv = new ReportViewer("Mega.POS.Report.InventoryMovementByProducts.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Movements", result.OrderBy(p => p.IdProduct)));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }
        
        private void OperationsByLocationByProductsReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var movements = dc.Kardexes.Where(k => k.IdLocation != null && k.IdShop.ToString() == Properties.Settings.Default.CurrentShop);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                movements = movements.Where(od => od.IdLocation.ToString() == location);
            }

            movements = product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString()
                            ? movements.Where(od => od.IdProduct == product)
                            : movements.Where(
                                od =>
                                od.Product.IdType == (int)ProductType.Simple ||
                                od.Product.IdType == (int)ProductType.CompositeChild);

            if (InitialDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.OperationDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                movements = movements.Where(o => SqlMethods.DateDiffDay(o.OperationDate, FinalDateTimePicker.Value) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                movements = movements.Where(od => od.Lot == LotTextBox.Text.Trim());
            }

            var result = movements.OrderBy(k => k.OperationDate).GroupBy(m => new { m.IdProduct }).
                Select(g => new InventoryMovementReport
                                {
                                    IdProduct = g.Key.IdProduct,
                                    OperationCount = g.Count(),
                                    InitialInventoryCount = g.First().InventoryCount - g.First().OperationCount,
                                    Entries = g.Sum(k => k.OperationCount > 0 ? k.OperationCount : 0),
                                    Exits = g.Sum(k => k.OperationCount < 0 ? -k.OperationCount : 0),
                                    FinalInventoryCount = g.OrderByDescending(l => l.OperationDate).First().InventoryCount,
                                    UMCode = g.First().Product.UMCode,
                                    ProductName = g.First().Product.Name,
                                    IdLocation = g.First().IdLocation.ToString(),
                                    LocationName = g.First().Location.Name,
                                });

            var rv = new ReportViewer("Mega.POS.Report.OperationsByLocationByProducts.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Movements", result.OrderBy(m => m.IdLocation).ThenBy(m => m.IdProduct)));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void SalesBySimpleProductsAndPromotionsReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var operationDetails =
                dc.OperationDetails.Where(od => od.Operation.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                    od.Operation.IdType == (int)OperationType.Sale && od.Operation.IdStatus == (int)OperationStatus.Confirmed);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operationDetails = operationDetails.Where(od => od.IdLocation.ToString() == location);
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operationDetails = operationDetails.Where(od => od.IdProduct == product);
            }

            if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                operationDetails = operationDetails.Where(od => od.Operation.Dim.IdType == dimType);
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                operationDetails = operationDetails.Where(od => od.Operation.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                operationDetails = operationDetails.Where(od => od.Lot == LotTextBox.Text.Trim());
            }

            if (InitialDateCheckBox.Checked)
            {
                operationDetails = operationDetails.Where(od => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, od.Operation.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                operationDetails = operationDetails.Where(od => SqlMethods.DateDiffDay(od.Operation.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operationDetails = operationDetails.Where(od => od.Operation.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operationDetails = operationDetails.Where(od => od.Operation.ModifiedBy.ToString() == user);
            }

            var rv = new ReportViewer("Mega.POS.Report.SalesBySimpleProductsAndPromotions.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("OperationDetails", operationDetails));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowConsignationReturnReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var operations =
                dc.Operations.Where(o => o.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                                         o.IdType == (int)OperationType.ConsignationReturn &&
                                         o.IdStatus == (int)OperationStatus.Confirmed);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdLocation.ToString() == location));
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdProduct == product));
            }

            if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                operations = operations.Where(i => i.Dim.IdType == dimType);
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                operations = operations.Where(i => i.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.Lot == LotTextBox.Text.Trim()));
            }

            if (InitialDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(o.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.ModifiedBy.ToString() == user);
            }

            operations = operations.OrderBy(o => o.ModifiedDate);

            var rv = new ReportViewer("Mega.POS.Report.ConsignationReturn.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Operation", operations));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }
        private void ShowCashierClosebyOperationPay()
        {
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);

            if (cashier.ToString() != "4")
            {
                var Folio = dc.CashierCloses.Where(r => r.Consecutive == Convert.ToInt32(txtNumeroFolio.Text) && r.IdCashier.ToString() == cashier).SingleOrDefault();


                if (Folio != null)
                {
                    var CashierCloseDetails = dc.CashierCloseDetails.Where(v => v.IdCashierClose == Folio.Id);

                    var HeaderCashier = dc.CashierCloses.Where(n => n.Consecutive == Convert.ToInt32(txtNumeroFolio.Text));
                    var rv = new ReportViewer("Mega.POS.Report.CashierCloseOperationbyPay.rdlc");
                    rv.LocalReport.DataSources.Add(new ReportDataSource("CashierByPay", CashierCloseDetails));
                    rv.LocalReport.DataSources.Add(new ReportDataSource("HeaderCashierClose", HeaderCashier));
                    rv.LocalReport.SetParameters(new ReportParameter("Folio", txtNumeroFolio.Text));

                    rv.Show();

                }
                else
                {
                    DialogHelper.ShowWarningInfo(this, "No existe el folio introducido");
                }

            }
            else
            {
                DialogHelper.ShowWarningInfo(this, "Seleccione una caja por favor ");
                CashierComboBox.Focus();
            }
          
         
        }
        private void ShowCashierMovementReport()
        {
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var movementType = Convert.ToString(MovementTypeComboBox.SelectedValue);

            var moneyMovements =
                dc.MoneyMovements.Where(mt => mt.Cashier.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop && mt.IsManual);

            if (InitialDateCheckBox.Checked)
            {
                moneyMovements = moneyMovements.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.AddedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                moneyMovements = moneyMovements.Where(o => SqlMethods.DateDiffDay(o.AddedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                moneyMovements = moneyMovements.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                moneyMovements = moneyMovements.Where(o => o.SupervisedBy.ToString() == user);
            }

            if (movementType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                moneyMovements = moneyMovements.Where(o => o.IdType.ToString() == movementType);
            }

            moneyMovements = moneyMovements.OrderBy(o => o.AddedDate);

            var rv = new ReportViewer("Mega.POS.Report.CashierMovementsByCashier.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("CashierMovements", moneyMovements));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }


        private void ShowCreditAmountStateReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var operations = dc.Operations.Where(op => op.Operation_Pays.Any(pay => pay.IdType == (int)MovementType.SaleCredit)
                                                && op.IdShop.ToString() == Properties.Settings.Default.CurrentShop
                                                && op.IdType == (int)OperationType.Sale
                                                && op.IdStatus == (int)OperationStatus.Confirmed);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdLocation.ToString() == location));
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdProduct == product));
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                operations = operations.Where(o => o.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.Lot == LotTextBox.Text.Trim()));
            }

            if (InitialDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(o.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.ModifiedBy.ToString() == user);
            }

            foreach (var op in operations)
            {
                op.CreditCollected = dc.Dim_CreditSaleCollecteds.Where(
                    dcc => dcc.OfficialConsecutive == op.OfficialConsecutive && dcc.IdShop == op.IdShop).Count() > 0
                                         ? dc.Dim_CreditSaleCollecteds.Where(
                                             dcc =>
                                             dcc.OfficialConsecutive == op.OfficialConsecutive &&
                                             dcc.IdShop == op.IdShop).Sum(
                                                 dcc1 => dcc1.OperationAmount)
                                         : 0;
            }

            operations = operations.OrderBy(o => o.ModifiedDate);

            var rv = new ReportViewer("Mega.POS.Report.CreditAmountState.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("CreditAmount", operations));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void CashierVerificationReport()
        {
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);

            var cashierVerifications =
                dc.CashierCloses.Where(c => c.Cashier.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop && c.IsClosed && c.IsCashierVerification);

            if (InitialDateCheckBox.Checked)
            {
                cashierVerifications = cashierVerifications.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.AddedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                cashierVerifications = cashierVerifications.Where(o => SqlMethods.DateDiffDay(o.AddedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                cashierVerifications = cashierVerifications.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                cashierVerifications = cashierVerifications.Where(o => o.ModifiedBy.ToString() == user);
            }

            if (JustWithDiffCheckBox.Checked)
            {
                cashierVerifications = cashierVerifications.Where(c => !c.IsOK);
            }

            var rv = new ReportViewer("Mega.POS.Report.CashierVerifications.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("CashierCloses", cashierVerifications));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowCashierCloseReport()
        {
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);

            var cashierCloses =
                dc.CashierCloses.Where(c => c.Cashier.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop && c.IsClosed && !c.IsCashierVerification);

            if (InitialDateCheckBox.Checked)
            {
                cashierCloses = cashierCloses.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.AddedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                cashierCloses = cashierCloses.Where(o => SqlMethods.DateDiffDay(o.AddedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                cashierCloses = cashierCloses.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                cashierCloses = cashierCloses.Where(o => o.ModifiedBy.ToString() == user);
            }

            if (JustWithDiffCheckBox.Checked)
            {
                cashierCloses = cashierCloses.Where(c => !c.IsOK);
            }

            var rv = new ReportViewer("Mega.POS.Report.CashierCloses.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("CashierCloses", cashierCloses));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowConsignationAccountStateReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var operations =
                dc.Operations.Where(o => o.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                                         (o.IdType == (int)OperationType.Consignation ||
                                         o.IdType == (int)OperationType.ConsignationReturn) &&
                                         o.IdStatus == (int)OperationStatus.Confirmed);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdLocation.ToString() == location));
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdProduct == product));
            }

            if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                operations = operations.Where(i => i.Dim.IdType == dimType);
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                operations = operations.Where(i => i.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.Lot == LotTextBox.Text.Trim()));
            }

            if (InitialDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(o.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.ModifiedBy.ToString() == user);
            }

            var rv = new ReportViewer("Mega.POS.Report.ConsignationAccountState.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Operation", operations));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowReceiptsInTransitReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);

            var receipts =
                dc.OperationDetails.Where(
                    i =>
                    i.Operation.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                    i.Operation.IdType == (int)OperationType.Receipt &&
                    i.Operation.IdStatus == (int)OperationStatus.NotConfirmed);

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                receipts = receipts.Where(o => o.Operation.OperationDetails.Any(op => op.IdProduct == product));
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                receipts = receipts.Where(o => o.Lot == LotTextBox.Text.Trim());
            }

            if (InitialDateCheckBox.Checked)
            {
                receipts = receipts.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.Operation.AddedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                receipts = receipts.Where(o => SqlMethods.DateDiffDay(o.Operation.AddedDate, FinalDateTimePicker.Value) >= 0);
            }

            var rv = new ReportViewer("Mega.POS.Report.ReceiptsInTransit.rdlc");
            rv.LocalReport.DataSources.Add(new ReportDataSource("Receipt", receipts));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));

            rv.Show();
        }

        private void ShowSalesReport()
        {
            var product = Convert.ToString(ProductComboBox.SelectedValue);
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);
            var cashier = Convert.ToString(CashierComboBox.SelectedValue);
            var user = Convert.ToString(UserComboBox.SelectedValue);
            var location = Convert.ToString(LocationComboBox.SelectedValue);

            var operations =
                dc.Operations.Where(o => o.Shop.Id.ToString() == Properties.Settings.Default.CurrentShop &&
                    o.IdType == (int)OperationType.Sale && o.IdStatus == (int)OperationStatus.Confirmed);

            if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdLocation.ToString() == location));
            }

            if (product != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.IdProduct == product));
            }

            if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                operations = operations.Where(o => o.Dim.IdType == dimType);
            }

            if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
            {
                operations = operations.Where(o => o.IdDim.ToString() == DimTextBox.Text.Trim());
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                operations = operations.Where(o => o.OperationDetails.Any(op => op.Lot == LotTextBox.Text.Trim()));
            }

            if (InitialDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(InitialDateTimePicker.Value, o.ModifiedDate) >= 0);
            }

            if (FinalDateCheckBox.Checked)
            {
                operations = operations.Where(o => SqlMethods.DateDiffDay(o.ModifiedDate, FinalDateTimePicker.Value) >= 0);
            }

            if (cashier != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.IdCashier.ToString() == cashier);
            }

            if (user != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                operations = operations.Where(o => o.ModifiedBy.ToString() == user);
            }

            operations = operations.OrderBy(o => o.ModifiedDate);

            var rv = new ReportViewer("Mega.POS.Report.Sales.rdlc");
            rv.LocalReport.SubreportProcessing += llenaSubReporteEventHandler;
            rv.LocalReport.DataSources.Add(new ReportDataSource("Operation", operations));
            rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));
            
            rv.Show();
        }

        void llenaSubReporteEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            string parametro = e.Parameters[0].Values[0].ToString();
            var ds2 = new ReportDataSource("DataSet1"){
                Value = dc.OperationDetails
                        .Where(o => o.Operation.Id.ToString() == parametro)
                        .OrderByDescending(o => o.AddedDate)
            };
            e.DataSources.Add(ds2);
        }


        private void ShowInventoryReport()
        {
            var location = Convert.ToString(LocationComboBox.SelectedValue);
            var productId = Convert.ToString(ProductComboBox.SelectedValue);
            var dimType = Convert.ToInt32(DimTypeComboBox.SelectedValue);

            var invent = dc.Inventories.Where(i => i.IdShop.ToString() == Properties.Settings.Default.CurrentShop);

            if (SelectedReport != ConsignationInventoryRadioButton.Name)
            {
                if (location != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
                {
                    invent = invent.Where(i => i.IdLocation.ToString() == location);
                }
            }

            if (SelectedReport != InventoryByLocationReportRadioButton.Name)
            {
                if (dimType != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
                {
                    invent = invent.Where(i => i.Dim.IdType == dimType);
                }

                if (!string.IsNullOrWhiteSpace(DimTextBox.Text))
                {
                    invent = invent.Where(i => i.IdDim.ToString() == DimTextBox.Text.Trim());
                }
            }

            if (productId != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                var product = dc.Products.Where(p => p.Id == productId).Single();

                invent = product.IdType == (int)ProductType.Simple
                             ? invent.Where(i => i.IdProduct == productId)
                             : invent.Where(
                                 i => i.Product.ProductCompositions1.Any(c => c.IdProductComposite == productId));
            }

            if (!string.IsNullOrWhiteSpace(LotTextBox.Text))
            {
                invent = invent.Where(i => i.Lot == LotTextBox.Text.Trim());
            }

            if (ExcludeZeroCountCheckBox.Checked)
            {
                //Para mostrar cualquier valor negativo no esperado
                invent = invent.Where(i => i.Count != 0);
            }

            ReportViewer rv = null;

            if (SelectedReport == InventoryByLocationReportRadioButton.Name)
            {
                invent = invent.Where(i => i.IdLocation != null);

                rv = new ReportViewer("Mega.POS.Report.Inventory.rdlc");
                rv.LocalReport.DataSources.Add(new ReportDataSource("Inventory", invent));
                rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));
            }
            else if (SelectedReport == ConsignationInventoryRadioButton.Name)
            {
                invent = invent.Where(i => i.IdDim != null);

                rv = new ReportViewer("Mega.POS.Report.ConsignationInventory.rdlc");
                rv.LocalReport.DataSources.Add(new ReportDataSource("Inventory", invent));
                rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));
            }
            else if (SelectedReport == TotalInventoryRadioButton.Name)
            {
                rv = new ReportViewer("Mega.POS.Report.TotalInventory.rdlc");
                rv.LocalReport.DataSources.Add(new ReportDataSource("Inventory", invent.OrderBy(p => p.IdProduct)));
                rv.LocalReport.SetParameters(new ReportParameter("ShopName", ApplicationHelper.GetCurrentShop().Name));
            }

            rv.Show();
        }

        private bool IsValidFilterValues()
        {
            //Si algun control de filtro necesita validacion iria aqui, de la forma: 

            if (LotTextBox.Enabled)
            {
                //Validar su valor, emitir mensaje de error y returnar false
            }

            if (InitialDateCheckBox.Enabled && FinalDateCheckBox.Enabled && InitialDateTimePicker.Value > FinalDateTimePicker.Value)
            {
                DialogHelper.ShowError(this, "La Fecha Inicial no puede ser mayor que la Final");
                return false;
            }

            if (SelectedReport == ProductMovementRadioButton.Name && Convert.ToString(ProductComboBox.SelectedValue) == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                DialogHelper.ShowError(this, "Debe seleccionar un Producto para visualizar este reporte");
                return false;
            }

            return true;
        }

        private void InventoryReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void SaleReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void CashierCloseReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void SalesBySimpleProductsAndPromotionsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void ReceiptReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void ProductsCatalogReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void CashierMovementRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitialDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitialDateTimePicker.Enabled = InitialDateCheckBox.Checked;
        }

        private void FinalDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FinalDateTimePicker.Enabled = FinalDateCheckBox.Checked;
        }

        private void ConsignationInventoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void TotalInventoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void CashierVerificationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void ConsignationAccountStateReportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void CreditAmountStateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void ConsignationReturnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void InventoryMovementByProductsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void OperationsByLocationByProductsRadioButtonn_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void ProductMovementRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void DimCreditCollectrRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }

        private void CashierCloseReportRadioButtonByOperationPay_CheckedChanged(object sender, EventArgs e)
        {
            SelectedReport = ((RadioButton)sender).Name;
            EnableFilterControls();
        }
    }
}
