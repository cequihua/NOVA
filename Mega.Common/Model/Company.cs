namespace Mega.Common
{
    public partial class Company
    {
        public string CurrencyCode
        {
            get { return UDCItem != null ? UDCItem.Code : string.Empty; }
        }


    }
}
