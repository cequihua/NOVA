using Mega.Common.Enum;

namespace Mega.Common
{
    public partial class OperationDetail
    {
        public string LocationName
        {
            get { return Location == null ? string.Empty : Location.Name; }
        }

        public string OfficialConsecutive
        {
            get { return Operation == null ? string.Empty : Operation.OfficialConsecutive; }
        }

        public string Location2Name
        {
            get { return Location1 == null ? string.Empty : Location1.Name; }
        }

        public string ProductName
        {
            get { return Product == null ? string.Empty : Product.Name; }
        }

        public string ProductDescription
        {
            get { return Product == null ? string.Empty : Product.Description; }
        }

        public string ProductType
        {
            /*
             Promocional = Compuesto o Simple con Precio Promocional			
             Normal = Simple con Precio Normal 			
             */
            get
            {
                if ((IdPriceType == (int)ListPriceType.Promotional && Product.IdType == (int)Enum.ProductType.Simple) || Product.IdType == (int)Enum.ProductType.Composite)
                {
                    return "Promocional";
                }

                if ((IdPriceType == (int)ListPriceType.Normal && Product.IdType == (int)Enum.ProductType.Simple))
                {
                    return "Normal";
                }

                return "Otro";
            }
        }

        public string ProductBarCode
        {
            get { return Product == null ? string.Empty : Product.BarCode; }
        }

        public string UMCode
        {
            get { return Product == null ? string.Empty : UDCItem.Code; }
        }

        public string IdProductForTicket
        {
            get
            {
                if (IdProductType == (int)Enum.ProductType.CompositeChild)
                    return " └> " + IdProduct;

                return IdProduct;
            }
        }

        public string PedimentForTicket
        {
            get
            {
                if (IdProductType == (int)Enum.ProductType.Composite)
                    return "| ";

                return Pediment;
            }
        }

        public bool IsVisibleDocCountReportField
        {
            get { return Operation.IdStatus != (int) OperationStatus.NotConfirmed; }
        }
    }
}
