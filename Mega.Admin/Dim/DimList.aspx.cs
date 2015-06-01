using System;
using System.Linq;
using System.Web.UI.WebControls;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common.Helpers;

namespace Mega.Admin.Dim
{
    public partial class DimList : CommonPage
    {
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
                //PopulateDims();
                InitilializeControls();
            }
        }

        private void InitilializeControls()
        {
            DimsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        public int DimID
        {
            get { return DimsGridView.SelectedDataKey != null ? (int)DimsGridView.SelectedDataKey.Value : -1; }
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            PopulateDims();
        }

        private void PopulateDims()
        {
            var list = PortalHelper.GetNewAdminDataContext().Dims.Select(d => d);

            if (FindByDimTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByDimTextBox.Text.Trim().ToUpper();

                list = list.Where(
                        d =>
                        d.Id.ToString().Contains(w));
            }

            if (FindByNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.Name.Contains(w));
            }

            if (FindByLastNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByLastNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.LastName.Contains(w));
            }

            if (FindByMotherMaidenNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByMotherMaidenNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.MotherMaidenName.Contains(w));
            }

            switch (SortExpression)
            {
                case "Id ASC":
                    {
                        list = list.OrderBy(l => l.Id);
                    }
                    break;
                case "Id DESC":
                    {
                        list = list.OrderByDescending(l => l.Id);
                    }
                    break;
                case "AddedDate ASC":
                    {
                        list = list.OrderBy(l => l.AddedDate);
                    }
                    break;
                case "AddedDate DESC":
                    {
                        list = list.OrderByDescending(l => l.AddedDate);
                    }
                    break;
                case "TypeName ASC":
                    {
                        list = list.OrderBy(l => l.UDCItem6.Name);
                    }
                    break;
                case "TypeName DESC":
                    {
                        list = list.OrderByDescending(l => l.UDCItem6.Name);
                    }
                    break;
                case "FullName ASC":
                    {
                        list = list.OrderBy(l => l.Name.Trim()).ThenBy(l => l.LastName.Trim()).ThenBy(l => l.MotherMaidenName.Trim());
                    }
                    break;
                case "FullName DESC":
                    {
                        list = list.OrderByDescending(l => l.Name.Trim()).ThenByDescending(l => l.LastName.Trim()).ThenByDescending(l => l.MotherMaidenName.Trim());
                    }
                    break;
                case "SexName ASC":
                    {
                        list = list.OrderBy(l => l.UDCItem5.Name);
                    }
                    break;
                case "SexName DESC":
                    {
                        list = list.OrderByDescending(l => l.UDCItem5.Name);
                    }
                    break;
                case "CoHolder ASC":
                    {
                        list = list.OrderBy(l => l.CoHolder);
                    }
                    break;
                case "CoHolder DESC":
                    {
                        list = list.OrderByDescending(l => l.CoHolder);
                    }
                    break;
                case "SaleRetention ASC":
                    {
                        list = list.OrderBy(l => l.SaleRetention);
                    }
                    break;
                case "SaleRetention DESC":
                    {
                        list = list.OrderByDescending(l => l.SaleRetention);
                    }
                    break;
                case "Disabled ASC":
                    {
                        list = list.OrderBy(l => l.Disabled);
                    }
                    break;
                case "Disabled DESC":
                    {
                        list = list.OrderByDescending(l => l.Disabled);
                    }
                    break;
            }

            DimsGridView.DataSource = list;
            DimsGridView.DataBind();
        }


        protected void DimsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DimsGridView.PageIndex = e.NewPageIndex;
            PopulateDims();
        }

        protected void DimsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression + " " + SortDirection;
            PopulateDims();
        }
    }
}