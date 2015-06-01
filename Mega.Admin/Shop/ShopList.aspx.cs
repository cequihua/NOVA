using System;
using System.Web.UI.WebControls;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common.Helpers;

namespace Mega.Admin.Shop
{
    public partial class ShopList : CommonPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs  e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
    }
}