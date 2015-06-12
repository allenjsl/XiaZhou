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
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.YlWeb.CommonPage
{
    public partial class SouSuo : EyouSoft.YlWeb.WzPage
    {
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing = null;
        protected string RiQiId = string.Empty;
        protected int recordCount = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing), Utils.GetQueryStringValue("lx"));
            //if (!LeiXing.HasValue) LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
            
            InitHQ();
        }

        string[] GetStringArr(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            string[] s1 = s.Split(',');
            if (s1 == null | s1.Length == 0) return null;
            return s1;
        }

        int[] GetIntArr(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            string[] s1 = s.Split(',');

            if (s1 == null || s1.Length == 0) return null;


            int[] s2 = new int[s1.Length];

            for (int i = 0; i < s1.Length; i++)
            {
                s2[i] = Utils.GetInt(s1[i]);
            }

            return s2;
        }

        EyouSoft.Model.YlStructure.MHangQiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();
            info.IsYouXiao = true;

            info.LeiXing = LeiXing;

            string hx = Utils.GetQueryStringValue("hx");
            info.HX = GetIntArr(hx);
            string xl = Utils.GetQueryStringValue("xl");
            info.XL = GetStringArr(xl);
            string gs = Utils.GetQueryStringValue("gs");
            info.GS = GetStringArr(gs);
            string gk = Utils.GetQueryStringValue("gk");
            info.GK = GetIntArr(gk);
            string ts = Utils.GetQueryStringValue("ts");
            info.TS = GetIntArr(ts);
            string jg = Utils.GetQueryStringValue("jg");
            info.JG = GetIntArr(jg);
            string yf = Utils.GetQueryStringValue("yf");
            info.YF = GetStringArr(yf);
            info.GuanJianZi = Server.UrlDecode(Utils.GetQueryStringValue("gjz"));
            string sj = Utils.GetQueryStringValue("sj");
            info.RiQi1 = Utils.GetDateTimeNullable(sj);
            info.RiQi2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sj1"));

            info.PaiXu = Utils.GetInt(Utils.GetQueryStringValue("px"));
            info.ChuanZhiId = Utils.GetQueryStringValue("cz");

            return info;
        }

        void InitHQ()
        {
            var chaXun = GetChaXunInfo();
            int pageSize = 6;
            int pageIndex = UtilsCommons.GetPadingIndex();

            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rptHQ.DataSource = items;
                rptHQ.DataBind();
            }

            RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
        }

        protected string GetHtml(object leixing, object hangqiid, object fujians, object mingcheng, object youhuixinxi, object riqis, object qishijiage, object TuJingChengShi, object ChanPinTeSe, object XiaoLiang, object haoping)
        {
            var html = new StringBuilder();
            var shijian = GetChuFaShiJian(riqis);
            var url = GetHqUrl(leixing);
            if (!String.IsNullOrEmpty(RiQiId))
                RiQiId = "/" + RiQiId;

            html.AppendFormat("<li>");
            //html.AppendFormat(" <div style=\"display:none\">");
            html.AppendFormat("  <div class=\"S-box01\">");
            html.AppendFormat("   <a href=\"{0}{1}{2}.html\" target=\"_blank\"><img src=\"{3}\" alt=\"" + mingcheng.ToString().Replace('"', '“') + "\" /></a>", url, hangqiid, RiQiId, GetFuJian(fujians));
            html.AppendFormat("   <div class=\"s-price\">¥ <em>{3}</em>起/人<br /><a href=\"{0}{1}{2}.html\" class=\"s-ydbtn\" target=\"_blank\">立即预订</a><br /><div class=\"manyidu\">最近成交 <i>{4}</i>单<br />满意度：<i>{5}%</i></div></div>", url, hangqiid, RiQiId, Utils.GetDecimal(qishijiage.ToString()).ToString("F2"), XiaoLiang, Utils.GetDecimal(haoping.ToString()).ToString("F2"));
            html.AppendFormat("   <p><a href=\"{0}{1}{2}.html\" class=\"biaoti\" title=\"" + mingcheng.ToString().Replace('"','“') + "\">{3}</a></p>", url, hangqiid, RiQiId, Utils.GetText(mingcheng.ToString(), 30, true));
            html.AppendFormat("   <p class=\"txt\">{0}</p>", GetYouHui(youhuixinxi));
            html.AppendFormat("   <p class=\"txt\">出发时间：<i>{0}</i></p>", shijian);
            html.AppendFormat("   <p class=\"txt\">途径城市：<i>{0}</i></p>", TuJingChengShi);
            html.AppendFormat("  </div>");
            //html.AppendFormat("  <div class=\"S-box02 fixed\">");
            //html.AppendFormat("    <div class=\"S-box02-L\">");
            //html.AppendFormat("       <p><i>【产品特色】</i></p>");
            //html.AppendFormat("       <p>{0}</p>", ChanPinTeSe);
            //html.AppendFormat("    </div>");
            //html.AppendFormat("    <div class=\"s-price\">最近成交 <i>{0}</i>单<br />满意度：<i>{1}%</i></div>", XiaoLiang, Utils.GetDecimal(haoping.ToString()).ToString("F2"));
            //html.AppendFormat("  </div>");
            //html.AppendFormat(" </div>");
            //html.AppendFormat(" <div style=\"display:block\">");
            //html.AppendFormat("     <span class=\"s-price\">¥ <em>{3}</em>起/人</span><a href=\"{0}?id={1}&riqiid={2}\" class=\"biaoti\">{4}</a>", url, hangqiid, RiQiId, Utils.GetDecimal(qishijiage.ToString()).ToString("F2"), mingcheng);
            //html.AppendFormat(" </div>");
            html.AppendFormat("</li>");

            //html.AppendFormat("<a href=\"{0}?id={1}&riqiid={2}\">", url,hangqiid,RiQiId);
            //html.AppendFormat("<img src=\"{0}\"></a>", GetFuJian(fujians));
            //html.AppendFormat("<div class=\"Rcont\">");
            //html.AppendFormat("    <p class=\"name\">");
            //html.AppendFormat("        <a href=\"{0}?id={1}&riqiid={3}\">{2}</a></p>",url,hangqiid,mingcheng,RiQiId);
            //html.AppendFormat("    <p class=\"txt\">");
            //html.AppendFormat("        {0}", GetYouHui(youhuixinxi));
            //html.AppendFormat("    出发时间：<em>{0}</em></p>", shijian);
            //html.AppendFormat("    <p class=\"price\">");
            //html.AppendFormat("    <em>{0}</em>起</p>",Utils.GetDecimal(qishijiage.ToString()).ToString("C2"));
            //html.AppendFormat("</div>");

            return html.ToString();
        }

        protected string GetChuFaShiJian(object obj)
        {
            string s = "";
            if (obj == null) return string.Empty;

            var sj = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sj"));
            var sj1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sj1"));
            var items = (IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo>)obj;
            if (sj.HasValue || sj1.HasValue)
            {
                if (sj.HasValue)
                    items = items.Where(m => m.RiQi >= sj.Value).ToList();
                if (sj1.HasValue)
                    items = items.Where(m => m.RiQi <= sj1.Value).ToList();
            }
            else
            {
                var y = Utils.GetQueryStringValue("yf");
                if (!string.IsNullOrEmpty(y))
                {
                    var yfs = y.Split(',');
                    if (yfs != null && yfs.Length > 0)
                    {
                        var youxiaoqi = new List<MYouXiaoQi>();
                        foreach (var yf in yfs)
                        {
                            youxiaoqi.Add(new MYouXiaoQi() { KaiShiRiQi = Utils.GetDateTimeNullable(yf + "-01") });
                        }
                        if (youxiaoqi != null && youxiaoqi.Count > 0)
                        {
                            var l = new List<EyouSoft.Model.YlStructure.MHangQiRiQiInfo>();
                            foreach (var m in youxiaoqi)
                            {
                                foreach (var i in items)
                                {
                                    if (m.KaiShiRiQi.HasValue && m.JieZhiRiQi.HasValue && i.RiQi >= m.KaiShiRiQi.Value && i.RiQi <= m.JieZhiRiQi.Value)
                                    {
                                        l.Add(i);
                                    }
                                }
                            }
                            items = l;
                        }
                    }
                }
            }
            if (items == null || items.Count == 0) return string.Empty;

            for (int i = 0; i < items.Count; i++)
            {
                if (i == 0) RiQiId = items[i].RiQiId;
                //if (i > 2) break;
                s += items[i].RiQi.ToString("M") + "/";
            }
            s = s.Trim('/');
            return s;
        }

        protected string GetHqUrl(object leiXing)
        {
            string url = "/hangqi/";
            var _leiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)leiXing;
            if (_leiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                url = "/hangqi/HY";

            return url;
        }

        protected string GetFuJian(object obj)
        {
            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)obj;
            if (items != null && items.Count > 0)
            {
                return TuPian.F1(ErpFilepath + items[0].Filepath, 177, 95);
            }
            return string.Empty;
            /*
            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)obj;
            if (items != null && items.Count > 0)
            {
                return "/Ashx/GetImg.ashx?f=" + Server.UrlEncode(ErpFilepath + items[0].Filepath) + "&w=177&h=95";
            }
            else
            {
                return string.Empty;
            }
             * */
        }

        protected string GetYouHui(object obj)
        {
            string s = string.Empty;
            if (obj != null) s = obj.ToString();
            if (string.IsNullOrEmpty(s))
            {
                //s = "暂无优惠<br/>";

                //return s;

                return "<br/>";
            }


            s = Utils.InputText(s);
            s = Utils.GetText(s, 29, true);

            s = "【优惠信息】" + s + "<br/>";

            return s;
        }
    }

    public class MYouXiaoQi
    {
        public DateTime? KaiShiRiQi { get; set; }
        public DateTime? JieZhiRiQi { get { return this.KaiShiRiQi.HasValue ? this.KaiShiRiQi.Value.AddMonths(1).AddDays(-1) : new DateTime?(); } }
    }

}
