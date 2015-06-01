using Mega.Common.Enum;

namespace Mega.Common
{
    public partial class CashierCloseMoney
    {
        public string CurrencyCode
        {
            get { return UDCItem == null ? string.Empty : UDCItem.Code; }
        }

        public UDCItem Currency
        {
            get { return UDCItem; }
        }

        public decimal Difference
        {
            get { return CashierOperationAmount - OperationAmount; }
        }

        public string TypeName
        {
            get
            {
                switch (IdType)
                {
                    case (int)MovementType.SaleCash:
                        return "Moneda Local";
                    case (int)MovementType.SaleCurrency:
                        return "Divisa";
                    default:
                        return UDCItem1.Name;
                }
            }
        }
    }
}
