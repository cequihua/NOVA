namespace Mega.Common
{
    public partial class MoneyMovement
    {
        public string CurrencyCode
        {
            get { return UDCItem.Code; }
        }
        
        public UDCItem Currency
        {
            get { return UDCItem; }
        }

        public UDCItem Type
        {
            get { return UDCItem1; }
        }

        public string TypeName
        {
            get { return UDCItem1.Name; }
        }

        //public bool IsManualOperation
        //{
        //    get { return UDCItem1.Optional2 == "1" ? true : false; }
        //}

        public bool IsCashOperationType
        {
            get { return UDCItem1.Optional3 == "1" ? true : false; }
        }

        public string ShopName
        {
            get { return Cashier.Shop.Name; }
        }

        public string CashierName
        {
            get { return Cashier.Name; }
        }

        public string ModifyByName
        {
            get { return User == null ? string.Empty : User.Name; }
        }

        public string SupervisedByName
        {
            get { return User1 == null ? string.Empty : User1.Name; }
        }
    }
}
