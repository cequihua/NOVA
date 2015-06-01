using System;
using System.Reflection;
using Mega.Admin.Code;

namespace Mega.Admin
{
    public partial class About : CommonPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VersionStatusLabel.Text = string.Format("Versión: [{0}]", Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
