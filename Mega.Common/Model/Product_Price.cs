namespace Mega.Common
{
    public partial class Product_Price
    {
        public string PriorityValue
        {
            get { return UDCItem2.Optional1; }
        }
        
        public string ClientTypeName
        {
            get { return UDCItem3 != null ? UDCItem3.Name : string.Empty; }
        }
        
        public string CurrencyCode
        {
            get { return UDCItem1 != null ? UDCItem1.Code : string.Empty; }
        }
        
        public string CompanyName
        {
            get { return Company != null ? Company.Name : string.Empty; }
        }
        
        public string ShopName
        {
            get { return Shop != null ? Shop.Name : string.Empty; }
        }

        public string PriceTypeName
        {
            get { return UDCItem2 != null ? UDCItem2.Name : string.Empty; }
        }

        public string DisabledAsText
        {
            get { return Disabled ? "Sí" : "No"; }
        }
    }
}
