using System;
using System.Linq;
using System.Web.UI.WebControls;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Synchronization
{
    public partial class POSConfiguration : CommonPage
    {
        readonly AdminDataContext Dc = PortalHelper.GetNewAdminDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            POSConfigurationsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
           
            AddConfigurationsPanel.Visible = Dc.Shops.Where(s => !s.Disabled).Except(Dc.Synchronizations.Select(s => s.Shop)).ToList().Count > 0;
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext(); 
        }
        protected void AddConfigurationsButton_Click(object sender, EventArgs e)
        {
            var shops = Dc.Shops.Where(s => !s.Disabled).Except(Dc.Synchronizations.Select(s => s.Shop)).ToList();

            foreach (var synchronization in shops.Select(shop => new Common.Synchronization
            {
                Shop = shop,
                DaysPlanIn = "1,1,1,1,1,1,1",
                HoursPlanIn = WebConfiguration.DefaultHoursPlanIn,
                DaysPlanOut = "1,1,1,1,1,1,1",
                HoursPlanOut = WebConfiguration.DefaultHoursPlanOut
            }))
            {
                Dc.Synchronizations.InsertOnSubmit(synchronization);
            }

            Dc.SubmitChanges();

            POSConfigurationsGridView.DataBind();

            AddConfigurationsPanel.Visible = Dc.Shops.Where(s => !s.Disabled).Except(Dc.Synchronizations.Select(s => s.Shop)).ToList().Count > 0;
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            POSConfigurationsGridView.DataBind();
        }
    }
}