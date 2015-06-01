using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Shop
{
    public partial class ShopEdit : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(ShopEdit));
        readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();

        protected string Id
        {
            get { return Request["id"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Id))
                    {
                        throw new ArgumentNullException("id", Resources.CommonStrings.PageInvalidParams);
                    }
                    
                    LoadItemData();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error en UserEdit.Page_Load. Detalle: " + ex.Message);
                    throw;
                }
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var item = dc.Shops.Where(i => i.Id == Id).Single();

                    item.Name = NameTextBox.Text;
                    item.IdCompany = int.Parse(CompanyDropDownList.SelectedValue);
                    item.Address = AddressTextBox.Text;
                    item.TicketCity = TicketCityTextBox.Text;
                    item.IdCountry = int.Parse(CountryDropDownList.SelectedValue);
                    item.Phone1 = Phone1TextBox.Text;
                    item.Phone2 = Phone2TextBox.Text;
                    item.Phone3 = Phone3TextBox.Text;
                    item.Email = EmailTextBox.Text;
                    item.Email2 = Email2TextBox.Text;
                    item.KeyCodeName = KeyCodeNameTextBox.Text;
                    item.KeyCodeSize = int.Parse(KeyCodeSizeTextBox.Text);
                    item.Disabled = DisabledCheckBox.Checked;
                    item.IdCurrency = int.Parse(CurrencyDropDownList.SelectedValue);
                    item.IdIVAGroup = int.Parse(IVAGroupDropDownList.SelectedValue);
                    item.IdIVATypeByManagement = int.Parse(IVATypeByManagementDropDownList.SelectedValue);
                    item.Disabled = DisabledCheckBox.Checked;

                    DataHelper.FillAuditoryValues(item, HttpContext.Current);
                    dc.SubmitChanges();

                    PortalHelper.ShowMessage(this, "Los datos han sido guardados correctamente");
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        private void LoadItemData()
        {
            var item = PortalHelper.GetNewAdminDataContext().Shops.Where(i => i.Id == Id).Single();

            IdTextBox.Text = item.Id;
            NameTextBox.Text = item.Name;
            CompanyDropDownList.SelectedValue = item.IdCompany.ToString();
            CurrencyDropDownList.SelectedValue = item.IdCurrency.ToString();
            AddressTextBox.Text = item.Address;
            TicketCityTextBox.Text = item.TicketCity;
            CountryDropDownList.SelectedValue = item.IdCountry.ToString();
            Phone1TextBox.Text = item.Phone1;
            Phone2TextBox.Text = item.Phone2;
            Phone3TextBox.Text = item.Phone3;
            EmailTextBox.Text = item.Email;
            Email2TextBox.Text = item.Email2;
            KeyCodeNameTextBox.Text = item.KeyCodeName;
            KeyCodeSizeTextBox.Text = item.KeyCodeSize.ToString();
            IVAGroupDropDownList.SelectedValue = item.IdIVAGroup.ToString();
            IVATypeByManagementDropDownList.SelectedValue = item.IdIVATypeByManagement.ToString();
            DisabledCheckBox.Checked = item.Disabled;
        }

        protected void AddCashierButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var item = new Cashier
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = CashierNameTextBox.Text,
                                       TicketCountToPrint = int.Parse(TicketCountToPrintTextBox.Text),
                                       TicketPageMargin = TicketPageMarginTextBox.Text,
                                       TicketPageSize = TicketPageSizeTextBox.Text,
                                       IdShop = Id,
                                       Disabled = false
                                   };

                    DataHelper.FillAuditoryValues(item, HttpContext.Current);

                    dc.Cashiers.InsertOnSubmit(item);
                    dc.SubmitChanges();

                    CashiersGridView.DataBind();
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error AddCashier", ex);
                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        protected void AddLocationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var item = new Location
                    {
                        Id = Guid.NewGuid(),
                        Name = LocationNameTextBox.Text,
                        IdShop = Id,
                        IsSalePoint = LocationIsSalePointCheckBox.Checked,
                        Disabled = false
                    };

                    DataHelper.FillAuditoryValues(item, HttpContext.Current);

                    dc.Locations.InsertOnSubmit(item);
                    dc.SubmitChanges();

                    LocationsGridView.DataBind();
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error AddCashier", ex);
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
    }
}