using System;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;
using System.Linq;

namespace Mega.Admin.Shop
{
    public partial class ShopAdd : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(ShopAdd));
        readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var newItem = new Common.Shop
                                      {
                                          Id = IdTextBox.Text,
                                          Name = NameTextBox.Text,
                                          IdCompany = int.Parse(CompanyDropDownList.SelectedValue),
                                          Address = AddressTextBox.Text,
                                          TicketCity = TicketCityTextBox.Text,
                                          IdCountry = int.Parse(CountryDropDownList.SelectedValue),
                                          Phone1 = Phone1TextBox.Text,
                                          Phone2 = Phone2TextBox.Text,
                                          Phone3 = Phone3TextBox.Text,
                                          Email = EmailTextBox.Text,
                                          Email2 = Email2TextBox.Text,
                                          KeyCodeName = KeyCodeNameTextBox.Text,
                                          KeyCodeSize = int.Parse(KeyCodeSizeTextBox.Text),
                                          IdCurrency = int.Parse(CurrencyDropDownList.SelectedValue),
                                          Disabled = false,
                                          IdIVAGroup = int.Parse(IVAGroupDropDownList.SelectedValue),
                                          IdIVATypeByManagement = int.Parse(IVATypeByManagementDropDownList.SelectedValue), 
                                      };

                    //Asignar los valores de Auditoria
                    DataHelper.FillAuditoryValues(newItem, HttpContext.Current);

                    dc.Shops.InsertOnSubmit(newItem);
                    dc.SubmitChanges();

                    Response.Redirect("~/Shop/ShopEdit.aspx?id=" + HttpUtility.UrlEncode(IdTextBox.Text));
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en ShopAdd (AddButton_Click). Detalle: " + ex.Message);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        protected void CompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var company = dc.Companies.Where(c => c.Id == int.Parse(CompanyDropDownList.SelectedValue)).Single();

            CurrencyDropDownList.SelectedValue = company.IdCurrency.ToString();
        }

        protected void IdCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var shop = dc.Shops.Where(c => c.Id == IdTextBox.Text).FirstOrDefault();

            if (shop == null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                PortalHelper.ShowMessage(this, IdCustomValidator.ErrorMessage);
            }
        }
    }
}