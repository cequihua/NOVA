using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.UDC
{
    public partial class UdcItem : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(UdcItem));

        protected void Page_Load(object sender, EventArgs e)
        {
            UDCItemsGridView.PageSize = DataHelper.GetUDCGridViewPageSize(PortalHelper.GetNewAdminDataContext());
        }

        private void UpdateVisualState()
        {
            try
            {
                if (UDCDropDownList.SelectedIndex != -1)
                {
                    UDCStateLabel.Text = string.Empty;
                    AddUDCItemButton.Enabled = true;
                    UDCItemsGridView.Enabled = true;

                    var udc =
                        PortalHelper.GetNewAdminDataContext().UDCs.Where(u => u.Id == UDCDropDownList.SelectedValue).Single();

                    if (!string.IsNullOrEmpty(udc.Description))
                    {
                        UDCDescLabel.Text = udc.Description;
                        UDCDescPanel.Visible = true;
                    }
                    else
                    {
                        UDCDescPanel.Visible = false;
                    }

                    if (udc.Disabled)
                    {
                        UDCStateLabel.Text += "[No Habilitado] ";
                        AddUDCItemButton.Enabled = false;
                        UDCItemsGridView.Enabled = false;
                    }
                    else
                    {
                        if (!udc.AllowEdit)
                        {
                            UDCStateLabel.Text += "[No Editable] ";
                            UDCItemsGridView.Enabled = false;
                        }

                        if (!udc.AllowAdd)
                        {
                            UDCStateLabel.Text += "[No Insertable] ";
                            AddUDCItemButton.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error actualizando las politicas de UDC Items", ex);
                PortalHelper.ShowMessage(this, "Error inesperado, por favor reportelo al administrador del sistema");
            }
        }

        protected void UDCDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisualState();
        }

        protected void UDCItemsLinqDataSource_Updating(object sender, LinqDataSourceUpdateEventArgs e)
        {
            var item = e.NewObject;

            DataHelper.FillAuditoryValues(item, HttpContext.Current);
        }

        protected void AddUDCItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var item = new UDCItem
                                   {
                                       Id = GetLastID(),
                                       IdUDC = UDCDropDownList.SelectedValue,
                                       Code = CodeTextBox.Text,
                                       Name = NameTextBox.Text,
                                       Optional1 = Optional1TextBox.Text,
                                       Optional2 = Optional2TextBox.Text,
                                       Optional3 = Optional3TextBox.Text,
                                       Optional4 = Optional4TextBox.Text
                                   };

                    DataHelper.FillAuditoryValues(item, HttpContext.Current);

                    AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
                    dc.UDCItems.InsertOnSubmit(item);
                    dc.SubmitChanges();

                    CodeTextBox.Text = string.Empty;
                    NameTextBox.Text = string.Empty;
                    Optional1TextBox.Text = string.Empty;
                    Optional2TextBox.Text = string.Empty;
                    Optional3TextBox.Text = string.Empty;
                    Optional4TextBox.Text = string.Empty;

                    UDCItemsGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando UDC Items", ex);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        private int GetLastID()
        {
            return PortalHelper.GetNewAdminDataContext().UDCItems.Max(x => x.Id) + 1;
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }
    }
}