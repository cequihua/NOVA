using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Account
{
    public partial class UserEdit : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(CommonPage));

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

                    LoadUserData();

                    LoadRoles();
                    LoadShops();

                }
                catch (Exception ex)
                {
                    Logger.Error("Error en UserEdit.Page_Load. Detalle: " + ex.Message);
                    throw;
                }
            }
        }

        private void LoadShops()
        {
            try
            {
                AdminDataContext dc = PortalHelper.GetNewAdminDataContext();

                ShopsCheckBoxList.Items.Clear();

                var allShops = dc.Shops.Select(s => new { s.Id, Name = s.NameWithId });
                var asignedShops = dc.User_Shops.Where(r => r.IdUser == Id).Select(r => r.IdShop);

                foreach (var item in allShops)
                {
                    ListItem listItem = new ListItem(item.Name, item.Id);

                    if (asignedShops.Contains(item.Id))
                    {
                        listItem.Selected = true;
                    }

                    ShopsCheckBoxList.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando Shops en UserEdit", ex);
                PortalHelper.ShowMessage(this, "Ha ocurrido un error cargando los Shops. Por favor notifíquelo al administrador del sistema");
            }
        }

        private void LoadRoles()
        {
            try
            {
                RolesCheckBoxList.Items.Clear();

                AdminDataContext dc = PortalHelper.GetNewAdminDataContext();

                var allRoles = DataHelper.GetUDCRoles(dc).Select(r => new { r.Id, r.Name });
                var asignedRoles = dc.User_Rols.Where(r => r.IdUser == Id).Select(r => r.IdRol);

                foreach (var r in allRoles)
                {
                    ListItem listItem = new ListItem(r.Name, r.Id.ToString());

                    if (asignedRoles.Contains(r.Id))
                    {
                        listItem.Selected = true;
                    }

                    RolesCheckBoxList.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando Roles en UserEdit", ex);
                PortalHelper.ShowMessage(this, "Ha ocurrido un error cargando los Roles. Por favor notifíquelo al administrador del sistema");
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var dc = PortalHelper.GetNewAdminDataContext();
                    var user = dc.Users.Where(u => u.Id == Id).Single();

                    user.Name = NameTextBox.Text;
                    user.Email = EmailTextBox.Text;

                    DataHelper.FillAuditoryValues(user, HttpContext.Current);
                    dc.SubmitChanges();

                    //PortalHelper.ShowMessage(this, "Los datos han sido guardados correctamente");
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

        protected void DisableUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool gotoDisable = !Convert.ToBoolean(DisableUserButton.CommandArgument);

                if (gotoDisable && IsLastAdminUser())
                {
                    PortalHelper.ShowMessage(this, "No puede Desabilitar al ultimo usuario ACTIVO con Rol de Administrador del Sistema");
                }
                else
                {
                    var dc = PortalHelper.GetNewAdminDataContext();
                    var user = dc.Users.Where(u => u.Id == Id).Single();

                    user.Disabled = !user.Disabled;
                    dc.SubmitChanges();

                    RefreshDisableButton(user.Disabled);
                }
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        private void LoadUserData()
        {
            var user = PortalHelper.GetNewAdminDataContext().Users.Where(u => u.Id == Id).Single();

            LoginTextBox.Text = user.Id;
            NameTextBox.Text = user.Name;
            EmailTextBox.Text = user.Email;
            PasswordTextBox.Text = user.Password;

            //ValidDateTextBox.Text = user.ValidDate.ToString("d");
            //<p>
            //    <label>
            //        Válida hasta:
            //    </label>
            //    <asp:TextBox ID="ValidDateTextBox" runat="server" ReadOnly="true" />
            //</p>

            RefreshDisableButton(user.Disabled);
        }

        private void RefreshDisableButton(bool userDisabled)
        {
            if (userDisabled)
            {
                DisableUserButton.Text = "Habilitar a este Usuario nuevamente";
                userLegend.InnerText = "Datos del Usuario (DESABILITADO)";
                userLegend.Style.Add("color", "Red");
            }
            else
            {
                DisableUserButton.Text = "Desabilitar a este Usuario (No podrá operar en el sistema)";
                userLegend.InnerText = "Datos del Usuario ";
                userLegend.Style.Add("color", "Inherit");
            }

            DisableUserButton.CommandArgument = userDisabled.ToString();
        }

        private bool IsLastAdminUser()
        {
            return
                PortalHelper.GetNewAdminDataContext().User_Rols.Where(r => r.IdUser != Id && r.IdRol == Constant.CFG_SYSTEM_ADMIN_ROLE_UDCITEM_KEY && r.User.Disabled == false).
                    Count() == 0;
        }

        protected void NotifyUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();
                var user = dc.Users.Where(u => u.Id == Id).Single();

                SendNotificationMailToUser(user);

                PortalHelper.ShowMessage(this, "Un email con credenciales y accesos ha sido enviado al usuario: " + user.Email);
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        protected void PswResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();
                var user = dc.Users.Where(u => u.Id == Id).Single();

                ResetPassword(dc, user);
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        private void ResetPassword(AdminDataContext dc, User user)
        {
            user.Password = DataHelper.GetNewPassword();
            //user.ValidDate = DataHelper.GetPasswordValidDate(PortalHelper.GetNewAdminDataContext());
            DataHelper.FillAuditoryValues(user, HttpContext.Current);

            dc.SubmitChanges();

            PasswordTextBox.Text = user.Password;
            PasswordTextBox.Font.Bold = true;

            //ValidDateTextBox.Text = user.ValidDate.ToString("d");
            //ValidDateTextBox.Font.Bold = true;
        }

        protected void PswResetAndNotifyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();
                var user = dc.Users.Where(u => u.Id == Id).Single();

                ResetPassword(dc, user);

                SendNotificationMailToUser(user);

                PortalHelper.ShowMessage(this, "Contraseña reseteada y notificación enviada al usuario por el email: " + user.Email);
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        private void SendNotificationMailToUser(User user)
        {
            try
            {
                string templatePath = Server.MapPath("~/MailTemplate/user-notification-mail.xslt");
                string imagePath = Server.MapPath("~/MailTemplate/Images/");

                AlternateView htmlBody = MailTemplateHelper.RetrieveUserNotificationBody(
                    templatePath, imagePath, WebConfiguration.MailLogoImageName, user);

                string adminMail = WebConfiguration.MailAdminAddress;

                MailTemplateHelper.SendMessage("Notificación de alta o cambios a su Usuario: " + user.Id, htmlBody,
                                               user.Email, string.Empty, adminMail);
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        protected void SaveRolesButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();

                dc.User_Rols.DeleteAllOnSubmit(dc.User_Rols.Where(rm => rm.IdUser == Id));

                foreach (ListItem chb in RolesCheckBoxList.Items)
                {
                    if (chb.Selected)
                    {
                        var item = new User_Rol
                                       {
                                           IdUser = Id,
                                           IdRol = int.Parse(chb.Value)
                                       };

                        DataHelper.FillAuditoryValues(item, HttpContext.Current);
                        dc.User_Rols.InsertOnSubmit(item);
                    }
                }

                dc.SubmitChanges();

                //PortalHelper.ShowMessage(this, "Elementos guardados correctamente");
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }

        protected void SaveShopsButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();

                dc.User_Shops.DeleteAllOnSubmit(dc.User_Shops.Where(rm => rm.IdUser == Id));

                foreach (ListItem chb in ShopsCheckBoxList.Items)
                {
                    if (chb.Selected)
                    {
                        var item = new User_Shop
                        {
                            IdUser = Id,
                            IdShop = chb.Value
                        };

                        DataHelper.FillAuditoryValues(item, HttpContext.Current);
                        dc.User_Shops.InsertOnSubmit(item);
                    }
                }

                dc.SubmitChanges();

                //PortalHelper.ShowMessage(this, "Elementos guardados correctamente");
            }
            catch (Exception ex)
            {
                PortalHelper.ShowMessage(this, "Error Inesperado: " + ex.Message);
            }
        }
    }
}