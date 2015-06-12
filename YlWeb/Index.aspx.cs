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
using System.Collections.Generic;

namespace EyouSoft.YlWeb
{
    public partial class Index : EyouSoft.YlWeb.WzPage
    {
        protected int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            if (!this.IsPostBack) { InitData(); }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        void InitData()
        {
            //首页轮播图片
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo() { WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.首页轮播图片 };
            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(this.YuMingInfo.CompanyId, 14, 1, ref recordCount, chaXun);
            if (items != null && items.Count > 0) { this.rptGuangGao.DataSource = items; this.rptGuangGao.DataBind(); }
        }

        protected string GetJingXuan(Model.EnumType.YlStructure.YouLunLeiXing leixing, Model.EnumType.YlStructure.HangQiBiaoQian biaoqian,int pagesize)
        {
            var recordCount = 0;
            var chaxun = new Model.YlStructure.MHangQiChaXunInfo() { IsYouXiao = true, LeiXing = leixing, BiaoQian = biaoqian };
            var list = new BLL.YlStructure.BHangQi().GetHangQis(this.YuMingInfo.CompanyId, pagesize, 1, ref recordCount, chaxun);
            var s = new System.Text.StringBuilder();
            var url = string.Empty;

            switch (leixing)
            {
                case EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮:
                    url = "/hangqi/";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮:
                    url = "/hangqi/HY";
                    break;
            }

            if (list != null && list.Count > 0)
            {
                switch (biaoqian)
                {
                    case EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门推荐:
                        foreach (var m in list)
                        {
                            s.Append("<div class=\"imgbox\">");
                            s.AppendFormat("   <a target=\"_blank\" href=\"{0}\" title=\"{2}\"><img src=\"{1}\" />", url + m.HangQiId + ".html", m.FuJians != null && m.FuJians.Count > 0 ? EyouSoft.YlWeb.TuPian.F1(Common.Utils.GetErpFilepath() + m.FuJians[0].Filepath, 230, 160) : string.Empty,m.MingCheng);
                            s.Append("   <dl>");
                            s.AppendFormat("      <dt>{0}</dt>", Common.Utils.GetText(m.MingCheng, 13, true));
                            s.AppendFormat("	  <dd>乘坐邮轮：{0}</dd>",m.ChuanZhiName);
                            s.AppendFormat("	  <dd>出发时间：{0}</dd>", m.RiQis != null && m.RiQis.Count > 0 ? m.RiQis[0].RiQi.ToShortDateString() : string.Empty);
                            s.AppendFormat("	  <dd>登船地点：{0}</dd>", m.ChuFaGangKouMingCheng);
                            s.AppendFormat("	  <dd><div class=\"i-price\">¥<i>{0}</i></div></dd>", m.QiShiJiaGe.ToString("F0"));
                            s.Append("   </dl></a>");
                            s.Append("</div>");
                        }
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门:
                        foreach (var m in list)
                        {
                            s.Append("<li>");
                            s.AppendFormat("   <div class=\"s-price\">¥<em>{0}</em></div>", m.QiShiJiaGe.ToString("F0"));
                            s.Append("  <dl>");
                            s.AppendFormat("    <a target=\"_blank\" href=\"{1}\" title=\"{2}\"><dt>{0}</dt></a>", Common.Utils.GetText(m.MingCheng, 24, true), url + m.HangQiId + ".html", m.MingCheng);
                            s.AppendFormat("	<a target=\"_blank\" href=\"{2}\" title=\"{1}\"><dd><strong>出发日期：</strong><i>{0}</i></dd></a>", new EyouSoft.YlWeb.Ashx.GetHotSell().ChuGangTimeHtml(m.RiQis), string.Join("、", m.RiQis.Select(r => r.RiQi.ToString("MM月dd日")).Distinct().ToArray()), url + m.HangQiId + ".html");
                            s.AppendFormat("	<a target=\"_blank\" href=\"{2}\"><dd><strong>优惠信息：</strong><font class=\"color_7 youhuixinxi\">{0}</font><span style=\"display:none;\">{1}</span></dd></a>", Common.Utils.GetText(Common.Utils.InputText(m.YouHuiXinXi), 24, true), m.YouHuiXinXi, url + m.HangQiId + ".html");
                            s.Append("  </dl>");
                            s.Append("  ");
                            s.Append("</li>");
                        }
                        break;
                }
            }

            return s.ToString();
        }
    }
}
