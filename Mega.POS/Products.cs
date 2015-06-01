using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS
{
    public partial class Products : Form
    {
        protected AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        public Products()
        {
            InitializeComponent();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            var inv = (from p in dc.Products where p.Id.Contains(txtProduct.Text) || p.Name.Contains(txtProduct.Text) select new { Id = p.Id, Name = p.Name }); 
            //from i in dc.Inventories join p in dc.Products on i.IdProduct equals p.Id where i.IdProduct == txtProduct.Text && p.Name == txtProduct.Text select p;
             dtgProductos.DataSource = inv;
            
        }
    }
}
