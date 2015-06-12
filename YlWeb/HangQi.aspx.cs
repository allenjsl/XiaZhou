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

namespace EyouSoft.YlWeb
{
    public partial class HangQi : EyouSoft.YlWeb.WzPage
    {
        protected int Year = Common.Utils.GetInt(Common.Utils.GetQueryStringValue("y"), DateTime.Now.Year);
        protected int Month = Common.Utils.GetInt(Common.Utils.GetQueryStringValue("m"), DateTime.Now.Month);
        protected int HangXianId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            InitData();
            if (Common.Utils.GetQueryStringValue("doajax") == "gethangqi") { Response.Clear(); Response.Write(GetHangQi()); Response.End(); }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        void InitData()
        {
            //航线初始化
            var l = new BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo() { LeiXing = Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线, YouLunLeiXing = Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 });
            if (l != null && l.Count > 0) 
            {
                HangXianId = Common.Utils.GetInt(Common.Utils.GetQueryStringValue("x"), l[0].XinXiId);
                rptHangXian.DataSource = l; 
                rptHangXian.DataBind();
            }
        }

        /// <summary>
        ///获取航线
        /// </summary>
        /// <returns></returns>
        protected string GetHangXian()
        {
            var m = new BLL.YlStructure.BJiChuXinXi().GetJiChuXinXiInfo(HangXianId);
            if (m != null) { return m.MingCheng; }
            return string.Empty;
        }

        /// <summary>
        /// 获取航期表
        /// </summary>
        /// <returns></returns>
        protected string GetHangQi()
        {
            var sb = new System.Text.StringBuilder();                                           //航期表
            var st = new System.Text.StringBuilder();                                           //船只信息
            var d = Common.Utils.GetDateTime(Year + "-" + Month + "-1");                        //当前月第一天
            var w = (int)d.DayOfWeek;                                                           //当前月第一天星期几
            var f = d.AddDays(-w);                                                              //起始日期
            var t = d.AddMonths(1).AddDays(6 - (DateTime.DaysInMonth(Year, Month) + w) % 7);    //结束日期
            var s = string.Empty;                                                               //td样式
            var b = string.Empty;                                                               //tr样式
            var bl = new BLL.YlStructure.BJiChuXinXi();
            System.Collections.Generic.IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> l = null;

            sb.AppendFormat("<div class=\"font24 hq_yellow\" style=\"text-align:center;\"><strong>{0}年{1}月  {2}</strong></div>",Year,Month,GetHangXian());
            sb.Append("<div class=\"hq_side02\">");
            sb.Append("     <div class=\"hq_rilibox\">");
            sb.Append("       <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("         <tr class=\"headbg\">");
            sb.Append("           <td>周日</td>");
            sb.Append("           <td>周一</td>");
            sb.Append("           <td>周二</td>");
            sb.Append("           <td>周三</td>");
            sb.Append("           <td>周四</td>");
            sb.Append("           <td>周五</td>");
            sb.Append("           <td class=\"noborderR\">周六</td>");
            sb.Append("         </tr>");
            //从起始日期到结束日期循环
            for (var c = f; c <= t; c = c.AddDays(1))
            {
                //判断是否星期六设置td样式
                if (c.DayOfWeek == DayOfWeek.Saturday)
                {
                    s = " class=\"noborderR\"";
                }
                else
                {
                    s = string.Empty;
                }
                //判断是否最后七天设置tr样式
                if ((t-c).TotalDays <= 7)
                {
                    b = "bg02 noborderB";
                }
                else
                {
                    b = "bg02";
                }
                //判断是否当前年当前月
                if (c.Year == Year && c.Month == Month)
                {
                    //航期日历
                    if (c.DayOfWeek == DayOfWeek.Sunday)
                        sb.Append("<tr class=\"bg01\">");
                    sb.AppendFormat("  <td{0}>{1}</td>", s, string.Format("{0:00}", c.Day));
                    if (c.DayOfWeek == DayOfWeek.Saturday)
                        sb.Append("</tr>");
                    //航期船只信息
                    if (c.DayOfWeek == DayOfWeek.Sunday)
                        st.AppendFormat("<tr class=\"{0}\">", b);
                    st.AppendFormat("  <td{0}>", s);
                    //获取当天航期的船只信息
                    l = bl.GetChuanZhiInfo(YuMingInfo.CompanyId, c, HangXianId);
                    if (l != null && l.Count > 0)
                    {
                        st.Append("    <ul>");
                        foreach (var m in l)
                        {
                            if (!String.IsNullOrEmpty(m.RiQiId))
                                m.RiQiId = "/" + m.RiQiId;
                            switch (m.YouLunLeiXing)
                            { 
                                case EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮:
                                    st.AppendFormat("        <li><a target=\"_blank\" href=\"/hangqi/{1}{2}.html\">{0}</a></li>", m.MingCheng, m.HangQiId, m.RiQiId);
                                    break;
                                case EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮:
                                    st.AppendFormat("        <li><a target=\"_blank\" href=\"/hangqi/HY{1}{2}.html\">{0}</a></li>", m.MingCheng, m.HangQiId, m.RiQiId);
                                    break;
                            }
                        }
                        st.Append("    </ul>");
                    }
                    st.Append("  </td>");
                    if (c.DayOfWeek == DayOfWeek.Saturday)
                        st.Append("</tr>");
                }
                else
                {
                    //航期日历
                    if (c.DayOfWeek == DayOfWeek.Sunday)
                        sb.Append("<tr class=\"bg01\">");
                    sb.AppendFormat("  <td{0}></td>", s);
                    if (c.DayOfWeek == DayOfWeek.Saturday)
                        sb.Append("</tr>");
                    //航期船只信息
                    if (c.DayOfWeek == DayOfWeek.Sunday)
                        st.AppendFormat("<tr class=\"{0}\">", b);
                    st.AppendFormat("  <td{0}>", s);
                    st.Append("  </td>");
                    if (c.DayOfWeek == DayOfWeek.Saturday)
                        st.Append("</tr>");
                }
                //判断是否星期六
                if (c.DayOfWeek == DayOfWeek.Saturday)
                {
                    //以一周为单位合并日历和船只
                    sb.Append(st);
                    //清空一周船只信息
                    st.Remove(0, st.Length);
                }
            }
            sb.Append("       </table>");
            sb.Append("     </div>");
            sb.Append(" </div>");

            return sb.ToString();
        }
    }
}
