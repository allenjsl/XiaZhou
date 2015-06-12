using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb.Ashx
{
    /// <summary>
    /// 首页热销产品
    /// </summary>
    public class GetHotSell : IHttpHandler
    {
        /// <summary>
        /// ERP上传文件路径
        /// </summary>
        private string ErpFilepath = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            string ErpFilepath = string.Empty;
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            if (m != null)
            {
                if (!string.IsNullOrEmpty(m.CompanyId))
                {
                    ErpFilepath = "http://" + m.ErpYuMing;
                }
            }

            string CompanyId = Utils.GetQueryStringValue("CompanyId");
            var LeiXing = Utils.GetQueryStringValue("LeiXing");
            StringBuilder tmpStr = new StringBuilder();
            tmpStr.Append("<ul id=\"i_ul_rexiao\">");
            tmpStr.Append(HotList(m.CompanyId, LeiXing, ErpFilepath));
            tmpStr.Append("</ul>");
            context.Response.ContentType = "text/plain";
            context.Response.Write(tmpStr.ToString());
        }
        /// <summary>
        /// 热销排行
        /// </summary>
        private string HotList(string CompanyId,string LeiXing,string ErpFilepath)
        {
            StringBuilder tmpStr = new StringBuilder();
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            MHangQiChaXunInfo chaxun = new MHangQiChaXunInfo()
            {
                IsYouXiao = true,
                IsReXiao = true,
            };
            if (!string.IsNullOrEmpty(LeiXing)) chaxun.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(LeiXing);
            int recordCount = 0;
            var list = bll.GetHangQis(CompanyId, 12, 1, ref recordCount, chaxun);
            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++) {
                    if (Utils.GetQueryStringValue("isindex").ToLower() == "true")
                    {
                        tmpStr.Append("<li>");
                        tmpStr.AppendFormat("	<div class=\"i-num\">{0}</div>",i+1);
                        tmpStr.Append("	   <p class=\"paihangT\"><a target=\"_blank\" href=\"/Hangqi/" + list[i].HangQiId + ".html\">" + Common.Utils.GetText(list[i].MingCheng,24,true) + "</a></p>");
                        tmpStr.Append("	   <p>【出发时间】" + ChuGangTimeHtml(list[i].RiQis) + "</p>");
                        tmpStr.Append("	   <p class=\"paihang-price\"><span class=\"s-price\">¥<em>" + list[i].QiShiJiaGe.ToString("F0") + "</em></span><span class=\"floatR\"><a target=\"_blank\" href=\"/Hangqi/" + list[i].HangQiId + ".html\" class=\"i-ydbtn\">立即预订</a></span></p>");
                        tmpStr.Append("</li>");
                    }
                    else
                    {
                        tmpStr.Append("<li><dl>");
                        tmpStr.Append("<dt><a target=\"_blank\" href=\"/Hangqi/" + list[i].HangQiId + ".html\">" + list[i].MingCheng + "</a></dt>");
                        tmpStr.Append("<dd>【出发时间】</dd>");
                        tmpStr.Append("<dd>" + ChuGangTimeHtml(list[i].RiQis) + "</dd>");
                        tmpStr.Append("<dd class=\"price\">" + list[i].QiShiJiaGe.ToString("C2") + "/人</dd>");
                        tmpStr.Append("</dl>");
                        tmpStr.Append("<div class=\"R_img\"><a target=\"_blank\" href=\"/Hangqi/" + list[i].HangQiId + ".html\">" + GetReXiaoImg(list[i].FuJians, ErpFilepath) + "<span class=\"\">立即预订</span></a></div>");
                        tmpStr.Append("</li>");
                    }
                }
            }
            return tmpStr.ToString();
        }
        /// <summary>
        /// 出发时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ChuGangTimeHtml(IList<MHangQiRiQiInfo> obj)
        {
            string str = "";
            if (obj != null)
            {
                if (obj != null && obj.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (obj.Count > i)
                        {
                            str += obj[i].RiQi.ToString("MM月dd日") + "、";
                        }
                    }
                    str = str.Substring(0, str.Length - 1);

                    str += "...";
                }
            }
            return str;
        }
        private string GetReXiaoImg(object fujians,string ErpFilepath)
        {
            if (fujians == null) return string.Empty;

            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)fujians;

            if (items != null && items.Count > 0)
            {
                return string.Format("<img src=\"{0}\" />", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + items[0].Filepath, 193, 146));
            }

            return string.Empty;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
