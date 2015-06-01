using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using log4net;
using Mega.Admin.Code.Helpers;
using System.Web.Security;
using Mega.Common;

namespace Mega.Admin.Account
{
    public partial class Login : Page
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Login));

        protected void Page_Load(object sender, EventArgs e)
        {
            Form.DefaultButton = loginButton.UniqueID;

            if (!IsPostBack)
            {
                txtLogin.Focus();
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                PerformLogin(txtLogin.Text.Trim(), txtPassword.Text);
            }
            else
            {
                ErrorLabel.Text = Resources.CommonStrings.PageNotIsValid;
            }
        }

        private void PerformLogin(string login, string password)
        {
            ErrorLabel.Text = string.Empty;
            
            try
            {
                var dc = PortalHelper.GetNewAdminDataContext();

                var user = dc.Users.Where(
                    x => x.Id == login && x.Password == password &&
                        !x.Disabled).Single();

                if (user.User_Rols.Where(r => r.IdRol == Constant.CFG_SYSTEM_ADMIN_ROLE_UDCITEM_KEY).Count() != 0)
                {
                    //SessionHelper.UserRol = user.ROL.ToString();
                    StoreAuthenticationData(login, Constant.CFG_SYSTEM_ADMIN_ROLE_UDCITEM_KEY.ToString());

                    FormsAuthentication.RedirectFromLoginPage(login, true);
                }
                else
                    throw new Exception("El usuario no posee el rol de Administrador del sistema");
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Usuario o Contraseña incorrectos!";
                Logger.Error("Error autenticando Usuario", ex);
            }
        }

        public static void StoreAuthenticationData(string login, string roles)
        {
            var ticket = new FormsAuthenticationTicket(1, login,
                                                     DateTime.Now, DateTime.Now.AddMinutes(30),
                                                     true, roles,
                                                     FormsAuthentication.FormsCookiePath);

            string sCookie = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, sCookie);

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
