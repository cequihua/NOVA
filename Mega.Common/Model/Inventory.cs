namespace Mega.Common
{
    public partial class Inventory
    {
        public string ProductName
        {
            get { return Product.Name; }
        }

        public string ProductBarCode
        {
            get { return Product.Name; }
        }

        public string ProductUM
        {
            get { return Product.UMCode; }
        }

        public string LocationName
        {
            get { return Location == null ? string.Empty : Location.Name; }
        }

        public string DimName
        {
            get { return Dim == null ? string.Empty : Dim.FullName; }
        }

        public string LocationTypeName
        {
            get { return IdDim == null ?  "Ubicación" : "Consignación"; }
        }

        public string LocationOrDimName
        {
            get { return IdDim == null ? LocationName : DimName; }
        }
    }
}
