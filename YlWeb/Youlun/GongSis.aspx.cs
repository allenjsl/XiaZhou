using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Youlun
{
    public partial class GongSis : WzPage
    {
        protected EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
        protected string LeiXing1 = "长江游轮合作公司";
        protected string LeiXing2 = "合作游轮公司";
        protected string GongSiUrl = "youlungongsicj.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            LeiXing = Utils.GetEnumValue(Utils.GetQueryStringValue("lx"), EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮);

            InitRpt();

            if (LeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
            {
                Title = "海洋邮轮合作公司";
                LeiXing1 = "海洋邮轮合作公司";
                LeiXing2 = "合作邮轮公司";
                GongSiUrl = "youlungongsi.aspx";
            }

            Master.LeiXing = LeiXing;
        }

        void InitRpt()
        {
            int pageSize = 12;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var chaXun = new EyouSoft.Model.YlStructure.MGongSiChaXunInfo();
            chaXun.GongSiLeiXing = LeiXing;

            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSis(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
        }
    }
}
