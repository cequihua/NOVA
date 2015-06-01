namespace Mega.Common
{
    public partial class SynchronizationLog
    {
        public string ShopName
        {
            get { return Shop != null ? Shop.Name : "Servidor Central"; }
        }
    }
}
