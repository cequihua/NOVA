using System;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Client
{
    public partial class FindClient : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FindClient));

        private string selecetedDim;
        private string idNameToFind;
        private bool checkRetentionAndDisabled;

        public string SelectedDIM
        {
            get { return selecetedDim; }
        }

        private void FindClient_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            ApplicationHelper.ConfigureGridView(dataGridView1);

            if (!string.IsNullOrWhiteSpace(idNameToFind))
            {
                int tmp;

                if (int.TryParse(idNameToFind, out tmp))
                {
                    FindByDimTextBox.Text = idNameToFind;
                }
                else
                {
                    FindByNameTextBox.Text = idNameToFind;
                    FindByLastNameTextBox.Text = idNameToFind;
                }

                Refresh();
                LoadListFirstTime();
            }
        }

        public FindClient(string idNameToFind, bool checkRetentionAndDisabled)
        {
            this.idNameToFind = idNameToFind;
            this.checkRetentionAndDisabled = checkRetentionAndDisabled;

            InitializeComponent();
            InitializePager();
        }

        private void InitializePager()
        {
            GridPager.SDataGridView = dataGridView1;
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
                Cursor = Cursors.Default;
                Logger.Error("Error buscando datos en FindClient", ex);
                DialogHelper.ShowError(this, "Error inesperado en la operación de Busqueda, intente de nuevo.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void LoadListFirstTime()
        {
            var list = ApplicationHelper.GetPosDataContext().Dims.Where(d => !d.Disabled);

            if (!string.IsNullOrWhiteSpace(FindByDimTextBox.Text))
            {
                string dim = FindByDimTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.Id.ToString().Contains(dim));
            }
            else
            {
                string lastName = FindByLastNameTextBox.Text.Trim().ToUpper();
                string name = FindByNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => (string.IsNullOrEmpty(name) || d.Name.Contains(name))
                                       || (string.IsNullOrEmpty(lastName) || d.Id.ToString().Contains(lastName)));
            }
            
            GridPager.Data = list;
            GridPager.Populate();

            ResultInfoLabel.Text = string.Format("Resultados: {0} coindidencias", list.Count());

            dataGridView1.Focus();
            ActiveControl = dataGridView1;
        }

        private void LoadList()
        {
            if (FindByDimTextBox.Text.Trim() + FindByNameTextBox.Text.Trim() + FindByLastNameTextBox.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(FindByDimTextBox, "Debe alguna la frase de búsqueda");
                errorProvider2.SetError(FindByNameTextBox, "Debe alguna la frase de búsqueda");
                errorProvider3.SetError(FindByLastNameTextBox, "Debe alguna la frase de búsqueda");
                return;
            }

            var list = ApplicationHelper.GetPosDataContext().Dims.Where(d => !d.Disabled);

            if (FindByDimTextBox.Text.Trim() != string.Empty)
            {
                string w = FindByDimTextBox.Text.Trim().ToUpper();

                list = list.Where(
                        d =>
                        d.Id.ToString().Contains(w));
            }

            if (FindByNameTextBox.Text.Trim() != string.Empty)
            {
                string w = FindByNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.Name.Contains(w));
            }

            if (FindByLastNameTextBox.Text.Trim() != string.Empty)
            {
                string w = FindByLastNameTextBox.Text.Trim().ToUpper();

                list = list.Where(d => d.LastName.Contains(w));
            }

            GridPager.Data = list;
            GridPager.Populate();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FindTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                FindButton.PerformClick();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dynamic dim = dataGridView1.Rows[e.RowIndex].DataBoundItem;

            ReturnDim(dim);
        }

        private void ReturnDim(dynamic dim)
        {
            if (checkRetentionAndDisabled && (dim.SaleRetention || dim.Disabled))
            {
                DialogHelper.ShowError(this, "El DIM seleccionado se encuentra Desactivado o en Retención de Ventas!!. No puede Venderle");
            }
            else
            {
                selecetedDim = Convert.ToString(dataGridView1.SelectedRows[0].Cells[0].Value);

                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void SelectDimButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                DialogHelper.ShowInformation(this, "Debe exitir una fila seleccionada");
            }
            else
            {
                dynamic dim = dataGridView1.SelectedRows[0].DataBoundItem;

                ReturnDim(dim);
            }
        }

        private void FindClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                SelectDimButton.PerformClick();
            }
        }
    }
}
