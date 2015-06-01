using System;
using System.Windows.Forms;

namespace Mega.Common.Helpers
{
    public class DialogHelper
    {
        private const string ERROR_CAPTION = "Mensaje de Error del Sistema";
        private const string INFO_CAPTION = "Mensaje de Información del Sistema";
        private const string WARNING_CAPTION = "Mensaje de Advertencia del Sistema";

        public static void ShowError(IWin32Window parent, string msg)
        {
            MessageBox.Show(parent, msg, ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError(IWin32Window parent, string msg, Exception ex)
        {
            MessageBox.Show(parent, string.Format("{0}{1}Detalle: {2}", msg, Environment.NewLine, ex.Message),
                            ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError( string msg, Exception ex)
        {
            MessageBox.Show(string.Format("{0}{1}Detalle: {2}", msg, Environment.NewLine, ex.Message),
                            ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInformation(IWin32Window parent, string msg)
        {
            MessageBox.Show(parent, msg, INFO_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarningQuestion(IWin32Window parent, string msg)
        {
            return MessageBox.Show(parent, msg, WARNING_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public static void ShowWarningInfo(IWin32Window parent, string msg)
        {
            MessageBox.Show(parent, msg, WARNING_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region ADTECH - MESSAGES PARA FACTURACION
        /*
         *Cesar Equihua
         *25/03/2014
         *Messages para confirmaciones notificaciones y alertas en facturacion
         */
        public static DialogResult ShowConfirmationQuestion(IWin32Window parent, string msg)
        {
            return MessageBox.Show(parent, msg, INFO_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static DialogResult ShowConfirmationQuestionWarning(IWin32Window parent, string msg)
        {
            return MessageBox.Show(parent, msg, WARNING_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        #endregion
    }
}
