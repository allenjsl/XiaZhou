using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb
{
    public partial class LoginOut : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginOutWeb();
            Response.Clear();
            Response.Write(EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "退出成功！"));
            Response.End();
        }
        private void LoginOutWeb()
        {
            EyouSoft.Security.Membership.YlHuiYuanProvider.Logout();
        }
    }
}
