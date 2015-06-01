namespace Mega.Common
{
    public partial class ProductComposition
    {
        public string UMCode
        {
            get { return UDCItem != null ? UDCItem.Code : string.Empty; }
        }

        public string ProductName
        {
            get { return Product1.Name; }
        }
    }
}
