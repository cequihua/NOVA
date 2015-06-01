using System;
using System.Web.UI;
using log4net;

namespace Mega.Admin.Code
{
    public class CommonPage : Page
    {
        protected static ILog Log = LogManager.GetLogger(typeof(CommonPage));

        protected override void OnError(EventArgs e)
        {
            Exception anError = Server.GetLastError();
            if (anError != null)
            {
                Log.Error(anError);
                Server.Transfer("~/Error.aspx");
            }
        }
    }
}