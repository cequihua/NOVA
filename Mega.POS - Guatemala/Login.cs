using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS
{
    public partial class Login : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Login));

        public string AuthenticatedLogin { get; set; }
        public string AuthenticatedRolName { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoginTextBox.Focus();
            ActiveControl = LoginTextBox;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text.Trim()) || string.IsNullOrEmpty(PswTextBox.Text.Trim()))
                {
                    DialogHelper.ShowError(this, "Debe entrar valores a los campos Usuario y Password");
                }
                else
                {
                    var user = ApplicationHelper.GetPosDataContext().Users.Where(
                        u => u.Id == LoginTextBox.Text && u.Password == PswTextBox.Text).SingleOrDefault();

                    if (user == null)
                    {
                        DialogHelper.ShowError(this, "El usuario o la contraseña son incorrectos");
                        Logger.ErrorFormat("El usuario {0} o la contraseña son incorrectos", LoginTextBox.Text);
                    }
                    else if (user.Disabled)
                    {
                        DialogHelper.ShowError(this, "El usuario se encuentra Desactivado");
                        Logger.ErrorFormat("El usuario {0} se encuentra Desactivado", LoginTextBox.Text);
                    }
                    //else if (user.ValidDate < DateTime.Today)
                    //{
                    //    DialogHelper.ShowError(this, "La fecha de validez del usuario ha expirado");
                    //}
                    else if (!user.User_Shops.Select(s => s.IdShop).Contains(Properties.Settings.Default.CurrentShop))
                    {
                        DialogHelper.ShowError(this, "Este Usuario no tiene permiso para autenticarse en esta Tienda/UDC");
                        Logger.ErrorFormat("El usuario {0} no tiene permiso para autenticarse en esta Tienda/UDC", LoginTextBox.Text);
                    }
                    else
                    {
                        var roles = ApplicationHelper.GetPosDataContext().User_Rols.Where(
                            r => r.IdUser == LoginTextBox.Text).Select(r => r.UDCItem);

                        if (roles.Count() == 0)
                        {
                            throw new Exception("No existen roles (accesos) asignados al usuario actual");
                        }

                        var rol = roles.Take(1).Single();

                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(LoginTextBox.Text),
                                                           new[] { Convert.ToString(rol.Id) });

                        AuthenticatedLogin = LoginTextBox.Text;
                        AuthenticatedRolName = rol.Name; 

                        DialogResult = DialogResult.OK;
                        Close();

                        return;
                    }
                }

                DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                Logger.Error("Error autenticando", ex);
                DialogHelper.ShowError(this, "Error inesperado al validar el Usuario", ex);
                DialogResult = DialogResult.None;
            }
        }
    }
}
