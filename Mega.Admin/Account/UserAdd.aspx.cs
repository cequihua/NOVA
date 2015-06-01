using System;
using System.Web;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Account
{
    public partial class UserAdd : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(UserAdd));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PasswordTextBox.Text = DataHelper.GetNewPassword();
                //ValidaDateTextBox.Text = DataHelper.GetPasswordValidDate(PortalHelper.GetNewAdminDataContext()).ToString("d");
                //<p>
                //    <label>
                //        Válida hasta:
                //    </label>
                //    <asp:TextBox ID="ValidaDateTextBox" runat="server" ReadOnly="true" />
                //</p>
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var newUser = new User
                                      {
                                          Id = LoginTextBox.Text,
                                          Name = NameTextBox.Text,
                                          Email = EmailTextBox.Text,
                                          Disabled = false,
                                          Password = PasswordTextBox.Text,
                                          ValidDate = DateTime.Today
                                      };

                    //Asignar los valores de Auditoria
                    DataHelper.FillAuditoryValues(newUser, HttpContext.Current);

                    AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
                    dc.Users.InsertOnSubmit(newUser);
                    dc.SubmitChanges();

                    Response.Redirect("~/Account/UserEdit.aspx?id=" + LoginTextBox.Text);
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en UserAdd (AddButton_Click). Detalle: " + ex.Message);
                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }
    }
}