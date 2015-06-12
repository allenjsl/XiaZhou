using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.YlWeb
{
    public partial class logindomain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string u = Utils.InputText(Request.QueryString["u"]);
            string p = Utils.InputText(Request.QueryString["p"]);
            string pmd = Utils.InputText(Request.QueryString["pmd"]);
            string vc = Utils.InputText(Request.QueryString["vc"]);
            string callback = Utils.InputText(Request.QueryString["callback"]);
            var ischeck = false;
            if (Request.QueryString["is"] == "true") ischeck = true;

            var YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            if (YuMingInfo == null || string.IsNullOrEmpty(YuMingInfo.CompanyId))
            {
                Response.Clear();
                Response.Write(";" + callback + "({m:'系统域名配置错误'});");
                Response.End();
            }

            string companyId = YuMingInfo.CompanyId;

            int isUserValid = 0;
            MYlHuiYuanInfo userInfo = null;

            var pwdInfo = new EyouSoft.Model.ComStructure.MPasswordInfo();
            pwdInfo.SetMD5Pwd(pmd);

            int expires_lx = 0;
            if (ischeck) expires_lx = 1;

            isUserValid = EyouSoft.Security.Membership.YlHuiYuanProvider.Login(companyId, u, pwdInfo, out userInfo, expires_lx);

            if (isUserValid == 1)
            {
                string html = "1";
                Response.Clear();
                Response.Write(";" + callback + "({h:" + html + "});");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write(";" + callback + "({m:'用户名或密码不正确'});");
                Response.End();
            }           
        }
    }
}
