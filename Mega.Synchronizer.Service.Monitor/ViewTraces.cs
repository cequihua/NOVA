using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Mega.Synchronizer.Service.Monitor.Helpers;

namespace Mega.Synchronizer.Service.Monitor
{
    public partial class ViewTraces : Form
    {
        private string Folder;

        public ViewTraces()
        {
            InitializeComponent();
        }

        public ViewTraces(string folder)
        {
            InitializeComponent();

            Folder = folder;
        }

        private void LoadLogFileContent()
        {
            var path = string.Format("{1}\\{2}\\{0}.log", SelectedDateDateTimePicker.Value.ToString(Properties.Settings.Default.Log4netDatePattern),
                Path.GetDirectoryName(Application.ExecutablePath), Folder);

            if (File.Exists(path))
            {
                ConsoleLogTextBox.Text = File.ReadAllText(path, Encoding.UTF7);
            }
            else
            {
                MessageBox.Show(string.Format("Error!!! No Existe fichero de Trazas para la fecha especificada. [{0}]", path),
                    "Mensaje de Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            LoadLogFileContent();
        }

        private void ViewTraces_Load(object sender, EventArgs e)
        {
            Text = string.Format("Trazas internas del {0}", Folder == SynchronizerServiceHelper.MONITOR_LOG_FOLDER ? "MONITOR" : "SERVICIO");

            LoadLogFileContent();
        }
    }
}
