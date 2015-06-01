using System;
using System.Linq;
using System.Web.UI.WebControls;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.DBScripts
{
    public partial class ExecutedScripts : System.Web.UI.Page
    {
        private readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private string SortExpression;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitiaizeControls();
                PopulateScripts();
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
            shops.Add(new { Id = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString(), Name = "-Web-" });
            ShopsDropDownList.DataSource = shops;
            ShopsDropDownList.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
            ShopsDropDownList.DataBind();
            ScriptsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        private void PopulateScripts()
        {
            var scripts = dc.ExecutedScripts.Select(l => l);

            scripts = scripts.Where(p => p.IdShop.ToString() == ShopsDropDownList.SelectedValue || (Convert.ToString(ShopsDropDownList.SelectedValue) == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString() && p.IdShop == null));

            switch (SortExpression)
            {
                case "IdScript ASC":
                    {
                        scripts = scripts.OrderBy(l => l.IdScript);
                    }
                    break;
                case "IdScript DESC":
                    {
                        scripts = scripts.OrderByDescending(l => l.IdScript);
                    }
                    break;
                case "IdShop ASC":
                    {
                        scripts = scripts.OrderBy(l => l.IdShop);
                    }
                    break;
                case "IdShop DESC":
                    {
                        scripts = scripts.OrderByDescending(l => l.IdShop);
                    }
                    break;
                case "ModifiedDate ASC":
                    {
                        scripts = scripts.OrderBy(l => l.ModifiedDate);
                    }
                    break;
                case "ModifiedDate DESC":
                    {
                        scripts = scripts.OrderByDescending(l => l.ModifiedDate);
                    }
                    break;

            }

            ScriptsGridView.DataSource = scripts;
            ScriptsGridView.DataBind();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            PopulateScripts();
        }

        protected void ScriptsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ScriptsGridView.PageIndex = e.NewPageIndex;
            PopulateScripts();
        }

        protected void ScriptsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression + " " + SortDirection;
            PopulateScripts();
        }

        protected void ScriptsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteExecutedScript();
        }

        private void DeleteExecutedScript()
        {
            var script = dc.ExecutedScripts.Where(c => c.Id.ToString() == ScriptsGridView.SelectedDataKey.Value.ToString()).Single();
            dc.ExecutedScripts.DeleteOnSubmit(script);
            dc.SubmitChanges();

            PopulateScripts();
        }
    }
}