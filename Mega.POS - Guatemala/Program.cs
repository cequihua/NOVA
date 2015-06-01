using System;
using System.Windows.Forms;
using Mega.POS.Helper;

namespace Mega.POS
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool running;
            var m = new System.Threading.Mutex(true, "MegaPOS", out running);

            if (!running)
            {
                MessageBox.Show("Mega - POS ya se encuentra en ejecución", "MegaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GC.KeepAlive(m);    // important!

            if (!IsValidDbConnection())
            {
                MessageBox.Show("La aplicación no puede abrir porque no se ha comprabado una conexión correcta a la Base de datos", "MegaPOS", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static bool IsValidDbConnection()
        {
            return ApplicationHelper.GetPosDataContext().DatabaseExists();
        }
    }
}
