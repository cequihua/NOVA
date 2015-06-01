using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using Mega.Common;


namespace Mega.Admin.Code.Helpers
{
    public class PortalHelper
    {
        private static AdminDataContext adminDataContext;

        //public static AdminDataContext GetNewAdminDataContext()
        //{
        //    return adminDataContext ??
        //           (adminDataContext =
        //            new AdminDataContext(ConfigurationManager.ConnectionStrings["AdminConnectionString"].ConnectionString));
        //}

        public static AdminDataContext GetNewAdminDataContext()
        {
            return new AdminDataContext(ConfigurationManager.ConnectionStrings["AdminConnectionString"].ConnectionString);
        }

        public static void ShowMessage(Page sourcePage, string message)
        {
            sourcePage.ClientScript.RegisterStartupScript(typeof(string), "ShowMessage",
                    string.Format("alert('{0}');", message), true);
        }


        public static string GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static void ThrowInvalidPageException()
        {
            throw new Exception(Resources.CommonStrings.PageNotIsValid);
        }

        public static bool IsCurrentUserInRole(int[] rolList)
        {
            return rolList.Any(rol => HttpContext.Current.User.IsInRole(rol.ToString()));
        }

    }
}