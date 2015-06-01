using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;

namespace Mega.Admin.Product
{
    public partial class ProductList : CommonPage
    {
        private readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private string SortExpression;

        public string SortDirection
        {
            get
            {
                if (ViewState["sortOrder"] == null || ViewState["sortOrder"].ToString() == "DESC")
                {
                    ViewState["sortOrder"] = "ASC";
                }
                else
                {
                    ViewState["sortOrder"] = "DESC";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControls();
            }

            PopulateProducts();
        }

        private void InitializeControls()
        {
            var shops = dc.Shops.Select(p => new { p.Id, Name = p.Id + " " + p.Name }).ToList();
            shops.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todos-" });
            ShopsDropDownList.DataSource = shops;
            ShopsDropDownList.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
            ShopsDropDownList.DataBind();

            ProductsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        private void PopulateProducts()
        {
            var products = dc.Products.Select(p => p);

            if (OnlyPromortionsCheckBox.Checked)
            {
                products = products.Where(p => p.IdType == (int)ProductType.Composite
                    || (p.Product_Prices.Count > 0 && p.Product_Prices.All(pp => pp.IdPriceType == (int)ListPriceType.Promotional
                        && SqlMethods.DateDiffDay(pp.InitialDate, DateTime.Now) >= 0
                        && SqlMethods.DateDiffDay(DateTime.Now, pp.FinalDate) >= 0)));
            }

            if (OnlyApplyDiscountCheckBox.Checked)
            {
                products = products.Where(p => p.ApplyDiscount);
            }

            if (Convert.ToString(ShopsDropDownList.SelectedValue) != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                products = products.Where(p => p.Product_Prices.Any(pp => pp.IdShop.ToString() == ShopsDropDownList.SelectedValue));
            }

            if (!string.IsNullOrWhiteSpace(IDOrBarCodeTextBox.Text))
            {
                products = products.Where(p => p.BarCode.Contains(IDOrBarCodeTextBox.Text) || p.Id.Contains(IDOrBarCodeTextBox.Text));
            }

            switch (SortExpression)
            {
                case "Id ASC":
                    {
                        products = products.OrderBy(l => l.Id);
                    }
                    break;
                case "Id DESC":
                    {
                        products = products.OrderByDescending(l => l.Id);
                    }
                    break;
                case "UMCode ASC":
                    {
                        products = products.OrderBy(l => l.UDCItem4.Code);
                    }
                    break;
                case "UMCode DESC":
                    {
                        products = products.OrderByDescending(l => l.UDCItem4.Code);
                    }
                    break;
                case "Name ASC":
                    {
                        products = products.OrderBy(l => l.Name);
                    }
                    break;
                case "Name DESC":
                    {
                        products = products.OrderByDescending(l => l.Name);
                    }
                    break;
                case "ProductTypeName ASC":
                    {
                        products = products.OrderBy(l => l.UDCItem5.Name);
                    }
                    break;
                case "ProductTypeName DESC":
                    {
                        products = products.OrderByDescending(l => l.UDCItem5.Name);
                    }
                    break;
                case "BarCode ASC":
                    {
                        products = products.OrderBy(l => l.BarCode);
                    }
                    break;
                case "BarCode DESC":
                    {
                        products = products.OrderByDescending(l => l.BarCode);
                    }
                    break;
                case "IVATypeAsText ASC":
                    {
                        products = products.OrderBy(l => l.UDCItem.Name);
                    }
                    break;
                case "IVATypeAsText DESC":
                    {
                        products = products.OrderByDescending(l => l.UDCItem.Name);
                    }
                    break;
                case "ApplyDiscount ASC":
                    {
                        products = products.OrderBy(l => l.ApplyDiscount);
                    }
                    break;
                case "ApplyDiscount DESC":
                    {
                        products = products.OrderByDescending(l => l.ApplyDiscount);
                    }
                    break;
            }

            ProductsGridView.DataSource = products;
            ProductsGridView.DataBind();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            PopulateProducts();
        }

        protected void ProductsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductsGridView.PageIndex = e.NewPageIndex;
            PopulateProducts();
        }

        protected void ProductsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression + " " + SortDirection;
            PopulateProducts();
        }
    }
}