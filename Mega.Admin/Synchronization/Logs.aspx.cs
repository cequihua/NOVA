using System;
using System.Linq;
using System.Web.UI.WebControls;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Synchronization
{
    public partial class Logs : CommonPage
    {
        private readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private string SortExpression;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitiaizeControls();
                PopulateLogs();
            }
        }

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

        private void InitiaizeControls()
        {
            var shops = dc.Shops.Select(p => new { p.Id, Name = p.Id + " " + p.Name }).ToList();
            shops.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Todos-" });
            ShopsDropDownList.DataSource = shops;
            ShopsDropDownList.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
            ShopsDropDownList.DataBind();
            LogsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        private void PopulateLogs()
        {
            var logs = dc.SynchronizationLogs.Select(l => l);

            if (Convert.ToString(ShopsDropDownList.SelectedValue) != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString())
            {
                logs = logs.Where(p => p.IdShop.ToString() == ShopsDropDownList.SelectedValue);
            }

            switch (SortExpression)
            {
                case "ShopName ASC":
                    {
                        logs = logs.OrderBy(l => l.Shop.Name);
                    }
                    break;
                case "ShopName DESC":
                    {
                        logs = logs.OrderByDescending(l => l.Shop.Name);
                    }
                    break;
                case "InitialDate ASC":
                    {
                        logs = logs.OrderBy(l => l.InitialDate);
                    }
                    break;
                case "InitialDate DESC":
                    {
                        logs = logs.OrderByDescending(l => l.InitialDate);
                    }
                    break;
                case "IsExportation ASC":
                    {
                        logs = logs.OrderBy(l => l.IsExportation);
                    }
                    break;
                case "IsExportation DESC":
                    {
                        logs = logs.OrderByDescending(l => l.IsExportation);
                    }
                    break;
                case "IsOk ASC":
                    {
                        logs = logs.OrderBy(l => l.IsOk);
                    }
                    break;
                case "IsOk DESC":
                    {
                        logs = logs.OrderByDescending(l => l.IsOk);
                    }
                    break;
                case "FinalDate ASC":
                    {
                        logs = logs.OrderBy(l => l.FinalDate);
                    }
                    break;
                case "FinalDate DESC":
                    {
                        logs = logs.OrderByDescending(l => l.FinalDate);
                    }
                    break;

            }

            LogsGridView.DataSource = logs;
            LogsGridView.DataBind();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            PopulateLogs();
        }

        protected void LogsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LogsGridView.PageIndex = e.NewPageIndex;
            PopulateLogs();
        }

        protected void LogsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression + " " + SortDirection;
            PopulateLogs();
        }
    }
}