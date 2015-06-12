using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心-计调终审
    /// 创建人：lixh 创建时间:2012-03-13
    /// </summary>
    public partial class OperaterLastInstace : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int PageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int RecordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            //获取分页参数并强转
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.TourStructure.MBZSearch Search = new EyouSoft.Model.TourStructure.MBZSearch();
            //团号
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            //线路名称
            Search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            //出团时间
            Search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            Search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            //导游
            SelectedGuid.GuidName = Search.Guide = Utils.GetQueryStringValue(this.SelectedGuid.GuidNameClient);
            SelectedGuid.GuidID = Search.GuideId = Utils.GetQueryStringValue(this.SelectedGuid.GuidIDClient);
            //计调员
            planers.SellsName = Search.Planer = Utils.GetQueryStringValue(this.planers.SellsNameClient);
            planers.SellsID = Search.PlanerId = Utils.GetQueryStringValue(this.planers.SellsIDClient);
            //销售员
            sellers.SellsName = Search.SellerName = Utils.GetQueryStringValue(this.sellers.SellsNameClient);
            sellers.SellsID = Search.SellerId = Utils.GetQueryStringValue(this.sellers.SellsIDClient);
            //已审 未审
            Search.IsDealt = Utils.GetQueryStringValue("IsDealt") == "1";

            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetPlanEndList(this.SiteUserInfo.CompanyId, PageSize, PageIndex, ref RecordCount, Search, this.SiteUserInfo.DeptId);
            if (list != null && list.Count > 0)
            {
                this.repOpInstaceList.DataSource = list;
                this.repOpInstaceList.DataBind();
                BindPage();
            }
            else
            {
                this.lab_Text.Text = "对不起,没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = this.ExporPageInfoSelect2.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = this.ExporPageInfoSelect2.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = this.ExporPageInfoSelect2.intRecordCount = RecordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_栏目, true);
                return;
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取收入的颜色
        /// </summary>
        /// <param name="tourStatus">团队状态</param>
        /// <returns></returns>
        protected string GetShouRuYanSe(object tourStatus)
        {
            if (tourStatus == null) return "fontred";
            var _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
            var _status = new EyouSoft.Model.EnumType.TourStructure.TourStatus[] { EyouSoft.Model.EnumType.TourStructure.TourStatus.封团
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.销售待审};

            if (_status.Contains(_tourStatus)) return string.Empty;

            return "fontred";
        }

        /// <summary>
        /// 获取操作列HTML
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="tourStatus">计划状态</param>
        /// <returns></returns>
        protected string GetCaoZuoHtml(object tourId, object tourStatus)
        {
            if (tourId == null || tourStatus == null) return string.Empty;
            var _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
            string s = string.Empty;

            if (_tourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审)
            {
                s = "<a href='/OperaterCenter/OperaterCheckend.aspx?tourId=" + tourId.ToString() + "&sl=" + SL + "' class='check-btn' title='查看'></a>";
            }
            else
            {
                s = "<img src='/images/y-duihao.gif' /><a href='/OperaterCenter/OperaterCheckend.aspx?tourId=" + tourId.ToString() + "&sl=" + SL + "'>计调终审</a>";
            }

            return s;
        }

        /// <summary>
        /// 绑定导游
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetGuidList(object list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MGuidInfo> GuInfo = (List<EyouSoft.Model.TourStructure.MGuidInfo>)list;
            if (GuInfo != null && GuInfo.Count > 0)
            {
                for (int i = 0; i < GuInfo.Count; i++)
                {
                    if (i == GuInfo.Count - 1)
                    {
                        sb.Append("" + GuInfo[i].Name + "");
                    }
                    else
                    {
                        sb.Append("" + GuInfo[i].Name + ",");
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计调员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetOperaList(object list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MTourPlaner> Oplist = (List<EyouSoft.Model.TourStructure.MTourPlaner>)list;
            if (Oplist != null && Oplist.Count > 0)
            {
                for (int i = 0; i < Oplist.Count; i++)
                {
                    if (i == Oplist.Count - 1)
                    {
                        sb.Append("" + Oplist[i].Planer + "");
                    }
                    else
                    {
                        sb.Append("" + Oplist[i].Planer + ",");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
