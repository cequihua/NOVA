namespace Mega.Common
{
    public partial class Kardex
    {

        public string UMCode
        {
            get { return Product == null ? string.Empty : Product.UMCode; }
        }

        public string ProductName
        {
            get { return Product == null ? string.Empty : Product.Name; }
        }

        public string OperationTypeName
        {
            get { return UDCItem == null ? string.Empty : UDCItem.Name; }
        }

        public string OperationOffNumber
        {
            get { return ByCancelation ? string.Format("{0} Canc: {1}", Operation.OfficialConsecutive, Operation.CancelConsecutive) : Operation.OfficialConsecutive; }
        }
    }

}
