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

namespace EyouSoft.YlWeb.Print
{
    public partial class XingChengDan : System.Web.UI.Page
    {
        protected EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮;
        protected string ChuanZhiName = string.Empty;
        protected string MingCheng = string.Empty;
        protected string HangXianMingCheng = string.Empty;
        protected string FeiYongShuoMing = string.Empty;
        protected string QianZhengQianZhu = string.Empty;
        protected string YuDingXuZhi = string.Empty;
        protected string YouQingTiShi = string.Empty;
        protected string YouLunGongLue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "行程单";
            var r = new BLL.YlStructure.BHangQi().GetHangQiInfo(EyouSoft.Common.Utils.GetQueryStringValue("HangQiId"));
            if (r != null)
            {
                YouLunLeiXing = r.LeiXing;
                ChuanZhiName = r.ChuanZhiName;
                MingCheng = r.MingCheng;
                HangXianMingCheng = r.HangXianMingCheng;
                FeiYongShuoMing = r.FeiYongShuoMing;
                QianZhengQianZhu = r.QianZhengQianZhu;
                YuDingXuZhi = r.YuDingXuZhi;
                YouQingTiShi = r.YouQingTiShi;
                YouLunGongLue = r.GongLue;
                if (r.XingChengs != null && r.XingChengs.Count > 0)
                {
                    this.rpt.DataSource = r.XingChengs;
                    this.rpt.DataBind();
                }
            }
        }
    }
}
