using System;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS
{
    public partial class AuthorizationRequired : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuthorizationRequired));

        private int[] authorizedRoles;

        public string AuthorizedUser { get; set; }
        public string InfoToUser { get; set; }

        public AuthorizationRequired(int[] authorizedRoles, string infoToUser)
        {
            this.authorizedRoles = authorizedRoles;
            InfoToUser = infoToUser; 

            InitializeComponent();

            InfoToUserLabel.Text = InfoToUser;
        }

        private void CancelFormButton_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text.Trim()) || string.IsNullOrEmpty(PasswordTextBox.Text.Trim()))
                {
                    DialogHelper.ShowError(this, "Debe entrar valores a los campos Usuario y Password");
                }
                else
                {
                    var user = ApplicationHelper.GetPosDataContext().Users.Where(
                        u => u.Id == LoginTextBox.Text && u.Password == PasswordTextBox.Text).SingleOrDefault();

                    if (user == null)
                    {
                        DialogHelper.ShowError(this, "El usuario o la contraseña son incorrectos");
                    }
                    else if (user.Disabled)
                    {
                        DialogHelper.ShowError(this, "El usuario se encuentra Desactivado");
                    }
                    //else if (user.ValidDate < DateTime.Today)
                    //{
                    //    DialogHelper.ShowError(this, "La fecha de validez del usuario ha expirado");
                    //}
                    else if (!user.User_Shops.Select(s => s.IdShop).Contains(Properties.Settings.Default.CurrentShop))
                    {
                        DialogHelper.ShowError(this, "Este Usuario no tiene permiso para autenticarse en esta Tienda/UDC");
                    }
                    else
                    {
                        var roles = ApplicationHelper.GetPosDataContext().User_Rols.Where(r => r.IdUser == LoginTextBox.Text).Select(
                            r => r.UDCItem);

                        if (roles.Count() == 0)
                        {
                            DialogHelper.ShowError(this, "Este usuario no cuenta con los Roles o funciones necesarios");
                        }
                        else
                        {
                            foreach(var r in roles)
                            {
                                if (authorizedRoles.Contains(r.Id))
                                {
                                    AuthorizedUser = user.Id;
                                    DialogResult = DialogResult.OK;
                                    Close();
                                }
                                    
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en AuthorizationRequired.OkButton_Click", ex);
                DialogHelper.ShowError(this, "Error inesperado intentando autenticar.", ex);
            }
        }

        private void AuthorizationRequired_Load(object sender, EventArgs e)
        {
            LoginTextBox.Focus();
            ActiveControl = LoginTextBox;
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OkButton.PerformClick();
            }
        }
    }
}
