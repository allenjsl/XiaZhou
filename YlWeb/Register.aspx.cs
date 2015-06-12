using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.YlStructure;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb
{
    public partial class Register : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Utils.GetQueryStringValue("re");
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "ck")
                {
                    RCWE(ckUserName());
                }
                if (type == "reg")
                {
                    RCWE(SavaReg());
                }
            }

            InitGuangGao();

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            if (isLogin) Response.Redirect("/");
        }

        /// <summary>
        /// 用户名检测
        /// </summary>
        /// <returns></returns>
        protected string ckUserName()
        {
            string userName = Utils.GetQueryStringValue("UserName");
            bool re = false;
            if (!string.IsNullOrEmpty(userName))
            {
                BHuiYuan bll = new BHuiYuan();

                re = bll.IsExistsUsername(YuMingInfo.CompanyId, userName);
                if (!re)
                {
                    return UtilsCommons.AjaxReturnJson("1", "当前用户名可以使用！");
                }
                else
                    return UtilsCommons.AjaxReturnJson("0", "当前用户名已经存在！");
            }
            else
                return UtilsCommons.AjaxReturnJson("0", "用户名不能为空！");
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        protected string SavaReg()
        {
            string msg = "";
            int re = 0;
            #region model
            EyouSoft.Model.ComStructure.MPasswordInfo pwdInfo = new EyouSoft.Model.ComStructure.MPasswordInfo();
            pwdInfo.NoEncryptPassword = Utils.GetFormValue(txtPass1.UniqueID);
            string name = Utils.GetFormValue(txtUserName.UniqueID);
            string email = Utils.GetFormValue(txtMail.UniqueID);

            MHuiYuanInfo model = new MHuiYuanInfo()
            {
                CompanyId = YuMingInfo.CompanyId,
                Username = name,
                MD5Password = pwdInfo.MD5Password,
                YouXiang = email,
                ShengRi = DateTime.Now,
                LeiXing = EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing.注册会员
            };
            #endregion

            #region check
            if (string.IsNullOrEmpty(model.Username))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写用户名！");
            }
            if (string.IsNullOrEmpty(model.YouXiang))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写常用邮箱！");
            }
            if (model.MD5Password == null && string.IsNullOrEmpty(model.MD5Password))
            {
                return UtilsCommons.AjaxReturnJson("0", "密码不能为空且两次输入必须一致！");
            }
            #endregion
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo userInfo = null;
            BHuiYuan Hui = new BHuiYuan();
            re = Hui.InsertHuiYuan(model);
            if (re > 0)
            {
                EyouSoft.Security.Membership.YlHuiYuanProvider.Login(model.CompanyId, model.Username, pwdInfo, out userInfo, 0);
            }
            switch (re)
            {
                case -98:
                    msg = "邮箱已被注册，请换一个常用邮箱！";
                    break;
                case -99:
                    msg = "用户名已存在！";
                    break;
                default:
                    msg = string.Format("注册{0}", re > 0 ? "成功，正在跳转页面，请稍后……" : "失败！");
                    break;
            }
            return UtilsCommons.AjaxReturnJson(re > 0 ? "1" : "0", msg);
        }

        void InitGuangGao()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.注册页右侧大图;

            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(YuMingInfo.CompanyId, 1, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                ltrGuangGao.Text = string.Format("<img src=\"{0}\">", ErpFilepath + items[0].Filepath);
            }
            else
            {
                ltrGuangGao.Text = string.Format("<img  src=\"\">");
            }
        }
    }
}
