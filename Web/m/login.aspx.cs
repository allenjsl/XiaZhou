using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.m
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string u = Utils.GetFormValue("u");
                string p = Utils.GetFormValue("p");
                string companyId = string.Empty;
                int isUserValid = 0;

                if (u.Trim() != "" && p.Trim() != "")
                {
                    EyouSoft.Model.SysStructure.MSysDomain sysDomain = EyouSoft.Security.Membership.UserProvider.GetDomain();
                    if (sysDomain != null)
                    {
                        companyId = sysDomain.CompanyId;
                    }

                    EyouSoft.Model.SSOStructure.MUserInfo userInfo = null;

                    EyouSoft.Model.ComStructure.MPasswordInfo pwdInfo = new EyouSoft.Model.ComStructure.MPasswordInfo();
                    pwdInfo.NoEncryptPassword = p;
                    isUserValid = EyouSoft.Security.Membership.UserProvider.Login(companyId, u, pwdInfo, out userInfo);

                    if (isUserValid == 1)
                    {
                        Response.Redirect("/m/Index.aspx?sl=" + ((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目).ToString());
                    }
                    else if (isUserValid == -4)
                    {
                        this.lblMsg.Text = "用户名或密码不正确";
                    }
                    else if (isUserValid == -7)
                    {
                        this.lblMsg.Text = "您的账户已停用或已过期，请联系管理员";
                    }
                    else
                    {
                        this.lblMsg.Text = "登录异常，请联系管理员";
                    }
                }
                else {
                    this.lblMsg.Text = "请输入用户名和密码!";
                }
            }
        }
    }
}
