namespace Mega.Common
{
    public partial class CashierClose
    {
        public string ShopName
        {
            get { return Cashier.Shop.Name; }
        }

        public string CashierName
        {
            get { return Cashier.Name; }
        }

        public string ModifiedByName
        {
            get { return User != null ? User.Name : "Usuario No Autenticado"; }
        }

        public string AuthorizedByName
        {
            get { return User1 != null ? User1.Name : "Usuario No Autenticado"; }
        }
        
        public string WithoutDifferences
        {
            get { return IsOK  ? "Si" : "No"; }
        }

        public string WasNotifysent
        {
            get { return NotifySent ? "Si" : "No"; }
        }

    }
}
