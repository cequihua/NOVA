using System.Windows.Forms;
using Mega.Common;
using System.Linq;

namespace Mega.Configurator
{
    public partial class CashierListForm : Form
    {
        private readonly string ConnectionString;

        public CashierListForm()
        {
            InitializeComponent();
        }

        public CashierListForm(string connectionString)
        {
            InitializeComponent();
            ConnectionString = connectionString;
        }

        private void CashierList_Load(object sender, System.EventArgs e)
        {
            var dataContext = new AdminDataContext(ConnectionString);

            CashierListDataGridView.DataSource = dataContext.Cashiers.Where(c => !c.Disabled).Select(c => new { ID = c.Id, c.Name }).ToList();
            CashierListDataGridView.Refresh();
        }
    }
}
