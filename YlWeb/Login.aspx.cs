using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.YlWeb
{
    public partial class Login : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string login = Utils.GetQueryStringValue("login");
            if (!string.IsNullOrEmpty(login))
            {                
                //Response.Clear();
                //Response.Write(this.UserLogin());
                //Response.End();
            }

            InitGuangGao();

            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            if (isLogin) Response.Redirect("/");
        }

        /*
        private string UserLogin()
        {
            string un = Utils.GetFormValue(txtUserName.UniqueID);
            string pw = Utils.GetFormValue(txtPassWord.UniqueID);
            
            if (string.IsNullOrEmpty(un))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写用户名！");
            }
            if (string.IsNullOrEmpty(pw))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写密码！");
            }

            MYlHuiYuanInfo userInfo = null;
            EyouSoft.Model.ComStructure.MPasswordInfo pwdInfo = new MPasswordInfo();
            pwdInfo.NoEncryptPassword = pw;

            int isUserValid = -8;
            if (YuMingInfo != null)
            {
               isUserValid=EyouSoft.Security.Membership.YlHuiYuanProvider.Login(YuMingInfo.CompanyId, un, pwdInfo, out userInfo);
            }

            if (isUserValid == 1)
            {
                return UtilsCommons.AjaxReturnJson("1", "登录成功，正在跳转，请稍后……");
            }
            else if (isUserValid == 0)
            {
                return UtilsCommons.AjaxReturnJson("0", "用户名或密码错误！");
            }
            else if (isUserValid == -1)
            {
                return UtilsCommons.AjaxReturnJson("-1", "用户名不能为空！");
            }
            else if (isUserValid == -2)
            {
                return UtilsCommons.AjaxReturnJson("-2", "密码不能为空！");
            }
            else if (isUserValid == -3)
            {
                return UtilsCommons.AjaxReturnJson("-3", "您的账户不存在，请联系管理员！");
            }
            else if (isUserValid == -4)
            {
                return UtilsCommons.AjaxReturnJson("-4", "您的账户不可用，请联系管理员！");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("-8", "登录错误，请联系管理员！");
            }
        }
        */

        void InitGuangGao()
        {
            int recordCount = 0;
            var chaXun=new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            var b = new EyouSoft.BLL.YlStructure.BWz();
            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.登录页左侧大图;

            var items = b.GetGuangGaos(YuMingInfo.CompanyId, 1, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                ltrGuangGao.Text = string.Format("<img  src=\"{0}\">", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + items[0].Filepath, 743, 495));
            }
            else
            {
                ltrGuangGao.Text = string.Format("<img  src=\"\">");
            }

            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.登录页右下图片;
            items = b.GetGuangGaos(YuMingInfo.CompanyId, 1, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                this.Literal1.Text = string.Format("<img src=\"{0}\">", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + items[0].Filepath, 341, 110));
            }
            else
            {
                this.Literal1.Text = string.Format("<img  src=\"\">");
            }
        }
    }
}
