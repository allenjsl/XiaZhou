using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class BangZhu : System.Web.UI.UserControl
    {

        EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing _LeiXing1 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答;
        EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing _LeiXing2 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略;

        public EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing LeiXing1
        {
            get { return _LeiXing1; }
            set { _LeiXing1 = value; }
        }

        public EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing LeiXing2
        {
            get { return _LeiXing2; }
            set { _LeiXing2 = value; }
        }

        protected string GL = "游轮攻略";

        protected void Page_Load(object sender, EventArgs e)
        {
            var YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo();
            chaXun.LeiXing = LeiXing1;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetZiXuns(YuMingInfo.CompanyId, 8, 1, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                rptList_WenDa.DataSource = items;
                rptList_WenDa.DataBind();
            }
            else
            {
                pHWenDa.Visible = true;
            }

            chaXun.LeiXing = LeiXing2;
            var items1 = new EyouSoft.BLL.YlStructure.BWz().GetZiXuns(YuMingInfo.CompanyId, 8, 1, ref recordCount, chaXun);

            if (items1 != null && items1.Count > 0)
            {
                rptList_GongLue.DataSource = items1;
                rptList_GongLue.DataBind();
            }
            else
            {
                pHGongLue.Visible = true;
            }

            if (LeiXing2 == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略) GL = "邮轮攻略";
        }
    }
}