namespace Mega.Common
{
    public partial class Shop
    {
        public string CompanyName
        {
            get { return Company.Name; }
        }

        public string CountryName
        {
            get { return UDCItem1.Name; }
        }

        public UDCItem Country
        {
            get { return UDCItem1; }
        }

        public string CurrencyCode
        {
            get { return UDCItem2.Code; }
        }

        public UDCItem Currency
        {
            get { return UDCItem2; }
        }

        public UDCItem IVATypeByManagement
        {
            get { return UDCItem; }
        }

        public string NameWithId
        {
            get { return string.Format("{0} {1}", Id, Name); }
        }
    }
}
