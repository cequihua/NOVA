using System;
using System.Linq;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Enum;

namespace Mega.Admin.Product
{
    public partial class Product : CommonPage
    {

        readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private Common.Product CurrentProduct;

        private string ProductID
        {
            get { return Request["id"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentProduct = dc.Products.Where(c => c.Id == ProductID).Single();

            InitializeValues();
        }

        private void InitializeValues()
        {
            IDLabel.Text = CurrentProduct.Id;
            ProductTypeLabel.Text = CurrentProduct.ProductTypeName;
            NameLabel.Text = CurrentProduct.Name;
            DescriptionLabel.Text = CurrentProduct.Description;
            UMLabel.Text = CurrentProduct.UMCode;
            BarCodeLabel.Text = CurrentProduct.BarCode;
            CostPriceLabel.Text = CurrentProduct.CostPrice.ToString("N");
            IVATypeLabel.Text = CurrentProduct.IVATypeAsText;
            Category1Label.Text = CurrentProduct.Category1Name;
            Category2Label.Text = CurrentProduct.Category2Name;
            Category3Label.Text = CurrentProduct.Category3Name;
            ApplyDiscountCheckBox.Checked = CurrentProduct.ApplyDiscount;
            DisabledCheckBox.Checked = CurrentProduct.Disabled;

            if (CurrentProduct.IdType == (int)ProductType.Composite)
            {
                CompositionPanel.Visible = true;

                ProductsGridView.DataSource = CurrentProduct.ProductCompositions;
                ProductsGridView.DataBind();

            }
            else
            {
                CompositionPanel.Visible = false;
            }

            PricesGridView.DataSource = CurrentProduct.Product_Prices;
            PricesGridView.DataBind();
        }
    }
}