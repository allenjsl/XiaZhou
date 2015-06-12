//单项业务单据 汪奇志 2013-05-10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.SingleServe
{
    /// <summary>
    /// 单项业务单据
    /// </summary>
    public partial class DanXiangYeWuDanJu : BackPage
    {
        #region attributes
        /// <summary>
        /// 单项业务编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 单项业务-游客确认单路径
        /// </summary>
        string PrintPagePath_YouKeQueRenDan = string.Empty;
        /// <summary>
        /// 单项业务-供应商确认单路径
        /// </summary>
        string PrintPagePath_GysQueRenDan = string.Empty;
        /// <summary>
        /// 单项业务-核算单路径
        /// </summary>
        string PrintPagePath_HeSuanDan = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            if (string.IsNullOrEmpty(TourId)) Utils.RCWE("异常请求");

            PrintPagePath_GysQueRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务供应商确认单);
            PrintPagePath_YouKeQueRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);
            PrintPagePath_HeSuanDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);

            InitInfo();
        }

        #region private members
        void InitInfo()
        {
            var info = new EyouSoft.BLL.TourStructure.BSingleService().GetSingleServiceExtendByTourId(TourId);
            if (info == null) Utils.RCWE("异常请求");
            if (info.CompanyId != SiteUserInfo.CompanyId) Utils.RCWE("异常请求");

            int i = 1;
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            s.AppendFormat("<p>委托日期：{0}</p>", info.WeiTuoRiQi.ToString("yyyy-MM-dd"));
            s.AppendFormat("<p>订单号：{0}</p>", info.OrderCode);
            s.AppendFormat("<p>销售员：{0}</p>", info.SellerName);

            if (!string.IsNullOrEmpty(PrintPagePath_HeSuanDan) && PrintPagePath_HeSuanDan != "javascript:void(0)")
            {
                s.Append("<p>");
                s.AppendFormat("<a target='_blank' href='{0}?referertype=2&tourid={1}'>{2}.单项服务核算单</a>", PrintPagePath_HeSuanDan, TourId, i.ToString().PadLeft(2, '0')) ;
                s.Append("</p>");
                i++;
            }

            if (!string.IsNullOrEmpty(PrintPagePath_YouKeQueRenDan) && PrintPagePath_YouKeQueRenDan != "javascript:void(0)")
            {
                s.Append("<p>");
                s.AppendFormat("<a target='_blank' href='{0}?tourid={1}'>{2}.单项服务委托预定单</a>", PrintPagePath_YouKeQueRenDan, TourId, i.ToString().PadLeft(2, '0'));
                s.Append("</p>");
                i++;
            }

            if (!string.IsNullOrEmpty(PrintPagePath_GysQueRenDan)
                && PrintPagePath_GysQueRenDan != "javascript:void(0)"
                &&info.PlanBaseInfoList != null 
                && info.PlanBaseInfoList.Count > 0)
            {
                foreach (var item in info.PlanBaseInfoList)
                {
                    s.Append("<p>");
                    s.AppendFormat("<a target='_blank' href='{0}?tourid={1}&anpaiid={2}'>{3}.{4}确认件({5})</a>", PrintPagePath_GysQueRenDan, TourId, item.PlanId, i.ToString().PadLeft(2, '0'), item.Type, item.SourceName);
                    s.Append("</p>");
                    i++;
                }
            }

            ltr.Text = s.ToString();
        }
        #endregion
    }
}
