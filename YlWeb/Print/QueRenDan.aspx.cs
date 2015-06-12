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
    public partial class QueRenDan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "确认单";
            var r = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(EyouSoft.Common.Utils.GetQueryStringValue("DingDanId"));
            if (r != null)
            {
                ltlMingCheng.Text = r.MingCheng;
                ltlChanPinBianHao.Text = r.BianHao;
                ltlTian.Text = r.TianShu1.ToString();
                ltlWan.Text = r.TianShu2.ToString();
                if (r.JiaGes != null && r.JiaGes.Count > 0)
                {
                    rptFangXing.DataSource = r.JiaGes;
                    rptFangXing.DataBind();
                }
                var hangqi = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(r.HangQiId);
                var riqi = new EyouSoft.BLL.YlStructure.BHangQi().GetRiQiInfo(r.RiQiId);
                var chuanzhi = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(hangqi.ChuanZhiId);
                if (hangqi != null)
                {
                    ltlChuFaDate.Text = riqi != null ? riqi.RiQi.ToShortDateString() : string.Empty;
                    ltlChuFaGang.Text = hangqi.ChuFaGangKouMingCheng;
                    ltlDiDaGang.Text = hangqi.DiDaGangKouMingCheng;
                    ltlZongTaiTel.Text = chuanzhi != null && r.FuKuanStatus == Model.EnumType.YlStructure.FuKuanStatus.已付款 ? chuanzhi.ChuanZaiDianHua : string.Empty;
                }
                if (r.YouKes != null && r.YouKes.Count > 0)
                {
                    rptYouKe.DataSource = r.YouKes;
                    rptYouKe.DataBind();
                }
                ltlYuDingRen.Text = r.YuDingRenName;
                ltlYuDingMail.Text = r.YuDingRenYouXiang;
                ltlYuDingMobile.Text = r.YuDingRenShouJi;
                if (r.FuJiaChanPins != null && r.FuJiaChanPins.Count > 0)
                {
                    rptFuJiaChanPin.DataSource = r.FuJiaChanPins;
                    rptFuJiaChanPin.DataBind();
                }
            }
        }
        protected void rptyoukes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex < 0) return;

            var info = (EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo)e.Item.DataItem;

            var ltrYK_ZJYXQ = (Literal)e.Item.FindControl("ltrYK_ZJYXQ");
            var ltrYK_CSRQ = (Literal)e.Item.FindControl("ltrYK_CSRQ");
            var ltrYK_XB = (Literal)e.Item.FindControl("ltrYK_XB");

            switch (info.ZhengJianLeiXing)
            {
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.户口本:
                    ltrYK_CSRQ.Text = info.SR1 + "-" + info.SR2 + "-" + info.SR3;
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.护照:
                    ltrYK_CSRQ.Text = info.SR1 + "-" + info.SR2 + "-" + info.SR3;
                    ltrYK_ZJYXQ.Text = info.YXQ1 + "-" + info.YXQ2 + "-" + info.YXQ3;
                    ltrYK_XB.Text = info.XingBie.ToString();
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.请选择:
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.稍后提供:
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.身份证:
                    if (info.ChuShengRiQi.HasValue) ltrYK_CSRQ.Text = info.ChuShengRiQi.Value.ToString("yyyy-MM-dd");
                    ltrYK_XB.Text = info.XingBie.ToString();
                    break;
            }

        }
        protected string GetJiChuXX(object xinxiid)
        {
            var m = new BLL.YlStructure.BJiChuXinXi().GetJiChuXinXiInfo(Common.Utils.GetInt(xinxiid.ToString()));
            if (m != null)
            {
                return m.MingCheng;
            }
            return string.Empty;
        }
    }
}
