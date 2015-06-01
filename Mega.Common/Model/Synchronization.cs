using Mega.Common.Helpers;

namespace Mega.Common
{
    public partial class Synchronization
    {
        public string ShopName
        {
            get { return Shop.Name; }
        }

        public string DaysPlanInFromated
        {
            get { return ToolHelper.GetDaysInConfig(DaysPlanIn); }
        }

        public string DaysPlanOutFromated
        {
            get { return ToolHelper.GetDaysInConfig(DaysPlanOut); }
        }
    }
}
