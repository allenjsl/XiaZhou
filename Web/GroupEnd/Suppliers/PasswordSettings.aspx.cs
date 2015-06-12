using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.GroupEnd.Suppliers
{
    public partial class PasswordSettings : EyouSoft.Common.Page.SupplierPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Update"))
                {
                    Response.Write(DoUpdate());
                    Response.End();
                }
            }
            if (!IsPostBack)
            {
                //公告
                this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;
            }
        }

        /// <summary>
        /// 获取当前用户的明文密码
        /// </summary>
        /// <returns></returns>
        private string GetPassword()
        {
            EyouSoft.BLL.ComStructure.BComUser BUser = new EyouSoft.BLL.ComStructure.BComUser();
            EyouSoft.Model.ComStructure.MComUser User = BUser.GetModel(SiteUserInfo.UserId, SiteUserInfo.CompanyId);
            return User.Password;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        private string DoUpdate()
        {
            string Result = string.Empty;

            string oldPwd = EyouSoft.Common.Utils.GetFormValue("txtOldPwd");
            string newPwd = EyouSoft.Common.Utils.GetFormValue("txtNewPwd");
            string surePwd = EyouSoft.Common.Utils.GetFormValue("txtSurePwd");

            string old=GetPassword();
            if (string.IsNullOrEmpty(oldPwd))
            {
                Result += "原密码不能为空！ </br>";
            }
            if (!oldPwd.Equals(old))
            {
                Result += "原密码不正确！ </br>";
            }
            if (string.IsNullOrEmpty(newPwd))
            {
                Result += "新密码不能为空！ </br>";
            }
            if (!newPwd.Equals(surePwd))
            {
                Result += "新密码与确认新密码必须一致 ！</br>";
            }

            if (Result.Length <= 0)
            {
                EyouSoft.BLL.ComStructure.BComUser BUser = new EyouSoft.BLL.ComStructure.BComUser();
                if (BUser.PwdModify(SiteUserInfo.UserId, old, surePwd))
                {
                    Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "密码修改成功，请重新登录。正在跳转...");
                }
                else
                {
                    Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "密码修改失败！");
                }
            }
            else
            {
                Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", Result);
            }


            return Result;
        }
    }
}
