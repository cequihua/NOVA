namespace Mega.Common
{
    public partial class CashierCloseDetail
    {
        public string MovementTypeName
        {
            get { return UDCItem == null ? string.Empty : UDCItem.Name; }
        }

        public UDCItem MovementType
        {
            get { return UDCItem; }
        }

        public string CurrencyCode
        {
            get { return UDCItem1 == null ? string.Empty : UDCItem1.Code; }
        }

        public UDCItem Currency
        {
            get { return UDCItem1; }
        }

        public decimal Difference
        {
            get { return CashierOperationAmount - OperationAmount; }
        }

        public string CancelingAsText
        {
            get { return IsCanceling ? "Cancelación" : string.Empty; }
        }

        public string MovementTypePlusCancelText
        {
            get { return MovementTypeName + " " + CancelingAsText; }
        }
    }
}
