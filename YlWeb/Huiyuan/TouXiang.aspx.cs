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

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class TouXiang : EyouSoft.YlWeb.HuiYuanPage
    {
        protected string TuXiang1 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                this.Save();
            }

            var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(HuiYuanInfo.HuiYuanId);
            if (huiYuanInfo != null)
            {
                var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
                TuXiang = string.IsNullOrEmpty(huiYuanInfo.TuXiang) ? "/images/default-head.jpg" : TuPian.F1("http://" + yuMingInfo.YuMing + huiYuanInfo.TuXiang, 235, 235);
            }

        }
        void Save()
        {
            var src = Utils.GetFormValue(this.upFiles.ClientHideID);
            var b = new BLL.YlStructure.BHuiYuan();
            var i = 0;

            if (string.IsNullOrEmpty(src.Trim()))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请选择新头像！"));
                return;
            }
            i = b.SheZhiHuiYuanTouXiang(this.HuiYuanInfo.HuiYuanId, src.Split('|')[1]);

            if (i > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功！")); else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败！"));
        }
    }
}
