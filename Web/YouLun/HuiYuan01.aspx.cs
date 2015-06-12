using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    public partial class HuiYuan01 : EyouSoft.Common.Page.BackPage
    {
        string EditId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitInfo();
        }

        void InitInfo()
        {
            var info = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(EditId);
            if (info == null) Utils.RCWE("异常请求");

            txtYongHuMing.Value = info.Username;
        }

        void BaoCun()
        {
            string yonghuming = Utils.GetFormValue(txtYongHuMing.UniqueID);
            string mima = Utils.GetFormValue(txtMiMa.UniqueID);

            if (string.IsNullOrEmpty(yonghuming)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "用户名不能为空"));
            if (yonghuming.Length > 50) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));
            if (!string.IsNullOrEmpty(mima) && mima.Length > 50) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));
            if (string.IsNullOrEmpty(EditId)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            int bllRetCode = new EyouSoft.BLL.YlStructure.BHuiYuan().SetHuiYuanUsername(EditId, yonghuming);

            if (bllRetCode == 1)
            {
                if (!string.IsNullOrEmpty(mima))
                {
                    var pwd = new EyouSoft.Model.ComStructure.MPasswordInfo();
                    pwd.NoEncryptPassword = mima;

                    new EyouSoft.BLL.YlStructure.BHuiYuan().SetHuiYuanMiMa(EditId, pwd.MD5Password);
                }
            }

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -98) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:用户名已存在"));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
    }
}
