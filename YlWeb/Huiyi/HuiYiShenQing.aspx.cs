using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Security.Membership;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.YlStructure;

namespace EyouSoft.YlWeb.YouLunMeet
{
    public partial class HuiYiShenQing : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = WzGuangGaoWeiZhi.游轮会议横幅;
            string type = Utils.GetQueryStringValue("dotype");
            if (type == "shenqing") Sava();
            if (IsPostBack)
            {
                
            }
        }

        private void Sava()
        {
            EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo model = new EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo();
            model.ChuLiBeiZhu = "";
            model.ChuLiOperatorId = string.Empty;
            model.ChuLiShiJian = null;
            model.CompanyId = YuMingInfo.CompanyId;
            model.GuiMo = Utils.GetFormValue("huiyi_guimo");
            model.HangYeLxShouJi = Utils.GetFormValue("hangye_shouji");
            model.HangYeMingCheng = Utils.GetFormValue("hangye_mingcheng");
            model.HangYeLxShouJi = Utils.GetFormValue("hangye_job");
            model.IssueTime = DateTime.Now;
            model.LeiXing = (YouLunLeiXing)int.Parse(Utils.GetFormValue("youlun_leixing"));
            model.LxrChengShiId = int.Parse(Utils.GetFormValue("sel_city"));
            model.LxrDiZhi = Utils.GetFormValue("dizhi");
            model.LxrGuoJiaId = 0;
            model.LxrShengFenId = int.Parse(Utils.GetFormValue("sel_province"));
            model.LxrShouJi = Utils.GetFormValue("shouji");
            model.LxrXianQuId = 0;
            model.LxrXingMing = Utils.GetFormValue("xingming");
            model.LxrYouXiang = Utils.GetFormValue("email");
            model.ShenQingId = "";
            model.YuJiShiJian = Utils.GetFormValue("huiyi_shijian");

            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            if (bll.InsertHuiYiShenQing(model) == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "申请成功！"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "申请失败！"));
            }
        }
    }
}
