using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.YouLun
{
    public partial class HuiFuBox : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
            if (Utils.GetQueryStringValue("save") == "save") HuiFu();
        }
        void initPage()
        {
            var huifu = new EyouSoft.BLL.YlStructure.BHuiYuan().GetWenDaInfo(Utils.GetQueryStringValue("id"));
            if (huifu != null)
            {
                txtHuiDa.Text = huifu.DaNeiRong;
            }
        }
        void HuiFu()
        {
            string huifuID = Utils.GetQueryStringValue("id");
            var huifu = new EyouSoft.BLL.YlStructure.BHuiYuan().GetWenDaInfo(huifuID);
            if (huifu == null) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
            huifu.DaNeiRong = Utils.GetFormValue(txtHuiDa.UniqueID);
            huifu.DaShiJian = DateTime.Now;
            huifu.DaOperatorId = SiteUserInfo.UserId;
            int result = new EyouSoft.BLL.YlStructure.BHuiYuan().HuiFuWenDa(huifu);
            if (result == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
    }
}
