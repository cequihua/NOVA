using Mega.Common.Enum;
using Mega.Common.Helpers;
using System.Linq;

namespace Mega.Common
{
    public partial class Operation
    {
        public string TypeName
        {
            get { return UDCItem.Name; }
        }

        public string TypeOptional4
        {
            get { return UDCItem.Optional4; }
        }

        public string ShopName
        {
            get { return Shop.Name; }
        }

        public string ShopTicketCity
        {
            get { return Shop.TicketCity; }
        }

        public bool IsConsignationOrReturn
        {
            get { return IdType == (int)OperationType.Consignation || IdType == (int)OperationType.ConsignationReturn; }
        }

        public UDCItem OperationCurrency
        {
            get { return UDCItem2; }
        }

        public string OperationCurrencyCode
        {
            get { return UDCItem2.Code; }
        }

        public string CashierName
        {
            get { return Cashier == null ? string.Empty : Cashier.Name; }
        }

        public UDCItem Status
        {
            get { return UDCItem1; }
            set { UDCItem1 = value; }
        }

        public string StatusName
        {
            get { return UDCItem1.Name; }
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

        public string ModifiedByName
        {
            get { return ModifiedBy == null ? string.Empty : User.Name; }
        }

        /// <summary>
        /// Retornara la cantidad de productos fisicos (simples)
        /// </summary>
        public double ProductCount
        {
            get { return OperationDetails.Where(p => p.IdProductType != (int)ProductType.Composite).Sum(p => p.Count); }
        }

        public decimal SubTotalMinusDiscontOperationAmount
        {
            get { return OperationAmount - (TotalIVAOperation ?? 0); }
        }

        public string AmountOperationInLetter
        {
            get { return ToolHelper.ConvetNumberToLetters(OperationAmount, OperationCurrency.Optional2, OperationCurrency.Optional3); }
        }

        public string CompanyName
        {
            get { return Shop.CompanyName; }
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

        public decimal OperationAmountValue
        {
            get { return IdType.Equals((int)OperationType.ConsignationReturn) ? -1 * OperationAmount : OperationAmount; }
        }

        public string IsConsignationReturn
        {
            get { return IdType.Equals((int)OperationType.ConsignationReturn) ? "Si" : "No"; }
        }

        public UDCItem SubType
        {
            get { return UDCItem3; }
            set { UDCItem3 = value; }
        }

        public string SubTypeName
        {
            get { return UDCItem3 == null ? string.Empty : UDCItem3.Name; }
        }

        public decimal CreditAmount
        {
            get
            {
                return
                    Operation_Pays.Where(opay1 => opay1.IdType == (int)MovementType.SaleCredit).Sum(
                        e => e.OperationAmount);
            }
        }

        public string Folio
        {
            get
            {
                string fol = "";
                var f = from i in Invoices where i.IdOperation == Id select i.Folio;
                if (f.SingleOrDefault() != null)
                    fol = f.SingleOrDefault();
                return fol;
            }
        }

        public decimal CreditCollected { get; set; }

        public decimal CreditToCollect
        {
            get { return CreditAmount - CreditCollected; }
        }
    }
}
