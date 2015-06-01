using Mega.Common.Helpers;
using System.Linq;

namespace Mega.Common
{
    public partial class DimCreditCollect
    {
        public Shop Shop
        {
            get { return Cashier.Shop; }
        }

        public string CompanyName
        {
            get { return Shop.CompanyName; }
        }

        public string CashierName
        {
            get { return Cashier.Name; }
        }

        public string CompanyLegalCertificate
        {
            get { return Shop.Company.LegalCertificate; }
        }

        public string CompanyAddress
        {
            get { return Shop.Company.FullAddress; }
        }

        public string CompanyCity
        {
            get { return Shop.Company.City; }
        }

        public string CompanyPhone1
        {
            get { return Shop.Company.Phone1; }
        }

        public string CompanyFax
        {
            get { return Shop.Company.Fax; }
        }


        public string OperationCurrencyCode
        {
            get { return UDCItem1.Code; }
        }

        public string DimName
        {
            get { return Dim == null ? string.Empty : Dim.FullName; }
        }

        public string DimStatus
        {
            get { return Dim == null ? string.Empty : Dim.Calification.ToString(); }
        }

        public decimal DimMaxCreditAmount
        {
            get { return Dim == null ? 0 : Dim.MaxCreditAmount; }
        }

        public decimal DimCreditAmount
        {
            get { return Dim == null ? 0 : Dim.CreditAmount; }
        }

        public string DimDiscount
        {
            get { return Dim == null ? string.Empty : Dim.DiscountPercent.ToString("N0"); }
        }

        public string DimMaxConsignableAmount
        {
            get { return Dim == null ? string.Empty : Dim.MaxConsignableAmount.ToString("N0"); }
        }

        public string DimConsignableAmount
        {
            get { return Dim == null ? string.Empty : Dim.ConsignableAmount.ToString("N0"); }
        }

        public string DimPoints
        {
            get { return Dim == null ? string.Empty : Dim.PointAmount.ToString("N"); }
        }

        public string DimAddress
        {
            get { return Dim == null ? string.Empty : Dim.FullAddress; }
        }

        public string DimTaxRegister
        {
            get { return Dim == null ? string.Empty : Dim.TaxRegister; }
        }

        public string SupervisedByName
        {
            get { return User1.Name; }
        }

        public string ModifiedByName
        {
            get { return User.Name; }
        }

        public string AmountOperationInLetter
        {
            get { return ToolHelper.ConvetNumberToLetters(OperationAmount, OperationCurrency.Optional2, OperationCurrency.Optional3); }
        }

        public UDCItem OperationCurrency
        {
            get { return UDCItem1; }
        }

        public string OperationsString
        {
            get
            {
                return string.Join(", ", Dim_CreditSaleCollecteds.Select(dsc => dsc.OfficialConsecutive));
            }
        }

    }
}
