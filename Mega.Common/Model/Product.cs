namespace Mega.Common
{
    public partial class Product
    {
        public string Category1Name
        {
            get { return UDCItem1.Name; }
        }

        public string Category2Name
        {
            get { return UDCItem2 != null ? UDCItem2.Name : string.Empty; }
        }

        public string Category3Name
        {
            get
            {
                return UDCItem3 != null ? UDCItem3.Code : string.Empty;
            }
        }

        public string UMCode
        {
            get { return UDCItem4 != null ? UDCItem4.Code : string.Empty; }
        }

        public string ProductTypeName
        {
            get { return UDCItem5.Name; }
        }

        public string ApplyDiscountAsText
        {
            get { return ApplyDiscount ? "Sí" : "No"; }
        }

        public string DisabledAsText
        {
            get { return Disabled ? "Sí" : "No"; }
        }

        public UDCItem IVAType
        {
            get { return UDCItem; }
        }

        public string IVATypeAsText
        {
            get { return UDCItem != null ? UDCItem.Name : string.Empty;  }
        }
    }
}
