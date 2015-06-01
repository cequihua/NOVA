using System;
using System.Data.Linq;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Client
{
    public partial class ClientList : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClientList));

        public ClientList()
        {
            InitializeComponent();
            InitializePager();
        }

        private void InitializePager()
        {
            GridPager.SDataGridView = dataGridView1;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ClientAdd().ShowDialog(this) == DialogResult.OK)
                {
                    LoadList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error llamando al Formulario AgregarCliente", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar/editar Cliente.", ex);
            }
        }

        private void ClientList_Load(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.ConfigureGridView(dataGridView1);
                //LoadList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario ClientList", ex);
                DialogHelper.ShowError(this, "Error inesperado durnte la carga de este formulario.", ex);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCurrentSelection();
        }

        private void EditCurrentSelection()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe haber una fila seleccionada");
                }
                else
                {
                    dynamic item = dataGridView1.SelectedRows[0].DataBoundItem;

                    var f = new ClientAdd { Id = item.Id };

                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario ClientAdd o recargando los Clientes", ex);
                DialogHelper.ShowError(this, "Error inesperado.", ex);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditCurrentSelection();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                LoadList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error filtrando datos en ClientList", ex);
                Cursor = Cursors.Default;
                DialogHelper.ShowError(this, "Error inesperado en la operación de Filtrado.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void LoadList()
        {
            var dc = ApplicationHelper.GetPosDataContextReseted();
            //dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Dims);

            var list = dc.Dims.Select(d => d);

            if (FindByDimTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByDimTextBox.Text.Trim().ToUpper();

                list = list.Where(
                        d =>
                        d.Id.ToString().Contains(w));
            }

            if (FindByNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.Name.Contains(w));
            }

            if (FindByLastNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByLastNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.LastName.Contains(w));
            }

            if (FindByMotherMaidenNameTextBox.Text.Trim() != string.Empty)
            {
                var w = FindByMotherMaidenNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.MotherMaidenName.Contains(w));
            }

            GridPager.Data = list;
            GridPager.Populate();
        }

        private void FindTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                FindButton.PerformClick();
            }
        }
    }
}
