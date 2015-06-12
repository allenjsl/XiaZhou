using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Security.Cryptography;
using System.Text;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class MiMa : EyouSoft.YlWeb.HuiYuanPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                this.Save();
            }
        }

        void Save()
        {
            var oldMM=Utils.GetFormValue(this.txtOld.UniqueID);
            var newMM=Utils.GetFormValue(this.txtNew.UniqueID);
            var conMM=Utils.GetFormValue(this.txtConfirm.UniqueID);
            //var Email=Utils.GetFormValue(this.txtEmail.UniqueID);
            var b = new BLL.YlStructure.BHuiYuan();
            var m = b.GetHuiYuanInfo(this.HuiYuanInfo.HuiYuanId);
            var i = 0;
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            if (string.IsNullOrEmpty(oldMM.Trim()))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请输入原始密码！"));
                return;
            }
            if (string.IsNullOrEmpty(newMM.Trim()))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请输入新密码！"));
                return;
            }
            if (string.IsNullOrEmpty(conMM.Trim()))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请输入确认密码！"));
                return;
            }
            if (newMM != conMM)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "您输入的两次密码不一致，请重新输入！"));
                return;
            }
            if (m.MD5Password != BitConverter.ToString(hashMD5.ComputeHash(Encoding.Default.GetBytes(oldMM))).Replace("-", "").ToLower())
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请输入正确的原始密码！"));
                return;
            }
            i = b.SheZhiHuiYuanMiMa(this.HuiYuanInfo.HuiYuanId, m.MD5Password, BitConverter.ToString(hashMD5.ComputeHash(Encoding.Default.GetBytes(newMM))).Replace("-", "").ToLower());

            if (i > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "设置成功！")); else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "设置失败！"));
        }
    }
}
