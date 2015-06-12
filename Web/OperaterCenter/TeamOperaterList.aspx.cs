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
    /// 页面功能：计调中心—组团计调列表
    /// 创建人：李晓欢
    /// 创建时间：2011-09-07
    /// </summary>
    public partial class TourOperaterList : EyouSoft.Common.Page.BackPage
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
        //团队状态html
        protected System.Text.StringBuilder TourStatusHtml = new System.Text.StringBuilder();
        //团队行程单
        protected string teamPrintUrl = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //行程单
            teamPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //客户单位Id
                string CustomerId = Utils.GetQueryStringValue(this.Customer1.CustomerUnitId);
                //客户单位
                string CustomerName = Utils.GetQueryStringValue(this.Customer1.CustomerUnitName);
                //销售员 计调员 Id
                string SelerId = Utils.GetQueryStringValue(this.Seller1.SellsIDClient);
                string PlanerId = Utils.GetQueryStringValue(this.seller2.SellsIDClient);
                //销售员 计调员
                string SelerName = Utils.GetQueryStringValue(this.Seller1.SellsNameClient);
                string PlanerName = Utils.GetQueryStringValue(this.seller2.SellsNameClient);
                if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(CustomerName))
                {
                    this.Customer1.CustomerUnitId = CustomerId;
                    this.Customer1.CustomerUnitName = CustomerName;
                }
                if (!string.IsNullOrEmpty(SelerId) && !string.IsNullOrEmpty(SelerName))
                {
                    this.Seller1.SellsID = SelerId;
                    this.Seller1.SellsName = SelerName;
                }
                if (!string.IsNullOrEmpty(PlanerId) && !string.IsNullOrEmpty(PlanerName))
                {
                    this.seller2.SellsID = PlanerId;
                    this.seller2.SellsName = PlanerName;
                }
                //初始化
                DataInit();
                //绑定团队状态
                string tourState = Utils.GetQueryStringValue("tourState");
                BindTourState(tourState);
            }
        }



        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.TourStructure.MPlanListSearch Search = new EyouSoft.Model.TourStructure.MPlanListSearch();
            //团号
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            //客源单位
            Search.CompanyInfo = new EyouSoft.Model.TourStructure.MCompanyInfo();
            Search.CompanyInfo.CompanyId = Utils.GetQueryStringValue(this.Customer1.ClientNameKHBH);
            Search.CompanyInfo.CompanyName = Utils.GetQueryStringValue(this.Customer1.ClientNameKHMC);
            //出团时间
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            Search.SLDate = startTime;
            Search.LLDate = endTime;
            //销售员
            Search.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo();
            Search.SaleInfo.SellerId = Utils.GetQueryStringValue(this.Seller1.SellsIDClient);
            Search.SaleInfo.Name = Utils.GetQueryStringValue(this.Seller1.SellsNameClient);
            //计调员       
            Search.PlanerId = Utils.GetQueryStringValue(this.seller2.SellsIDClient);
            Search.Planer = Utils.GetQueryStringValue(this.seller2.SellsNameClient);
            //团队状态
            string tourState = Utils.GetQueryStringValue("tourState");
            if (!string.IsNullOrEmpty(tourState) && tourState != "-1")
            {
                Search.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)Utils.GetInt(tourState);
            }

            //组团计调列表
            if (Utils.GetQueryStringValue("sl") == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_组团计调).ToString())
            {
                IList<EyouSoft.Model.TourStructure.MPlanList> TeamOplist = new EyouSoft.BLL.TourStructure.BTour().GetZTPlanList(SiteUserInfo.CompanyId, PageSize, PageIndex, ref RecordCount, Search);
                GetListSource(TeamOplist);
                Search = null;
            }
            //地接计调列表
            if (Utils.GetQueryStringValue("sl") == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_地接计调).ToString())
            {
                IList<EyouSoft.Model.TourStructure.MPlanList> TeamOplist = new EyouSoft.BLL.TourStructure.BTour().GetDJPlanList(SiteUserInfo.CompanyId, PageSize, PageIndex, ref RecordCount, Search);
                GetListSource(TeamOplist);
                Search = null;
            }
            //出境计调列表
            if (Utils.GetQueryStringValue("sl") == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_出境计调).ToString())
            {
                IList<EyouSoft.Model.TourStructure.MPlanList> TeamOplist = new EyouSoft.BLL.TourStructure.BTour().GetCJPlanList(SiteUserInfo.CompanyId, PageSize, PageIndex, ref RecordCount, Search);
                GetListSource(TeamOplist);
                Search = null;
            }

        }

        #region
        /// <summary>
        /// 绑定数据源 组团 地接 出境
        /// </summary>
        /// <param name="TeamOplist"></param>
        protected void GetListSource(IList<EyouSoft.Model.TourStructure.MPlanList> TeamOplist)
        {
            if (TeamOplist != null && TeamOplist.Count > 0)
            {
                this.TeamOperaterList.DataSource = TeamOplist;
                this.TeamOperaterList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lab_Text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
            TeamOplist = null;
        }
        #endregion

        #region 计划变更 颜色处理
        /// <summary>
        /// 计划变更 颜色处理
        /// </summary>
        /// <param name="isChange">是否变更</param>
        /// <param name="IsSure">是否确认</param>
        /// <returns></returns>
        protected string GetTourPlanIschange(bool isChange, bool IsSure, string tourId)
        {
            System.Text.StringBuilder sbChange = new System.Text.StringBuilder();
            if (isChange)
            {
                //确认 绿色 未确认 红色
                if (IsSure)
                {
                    sbChange.Append("<span><a target=\"_blank\" href=\"" + teamPrintUrl + "?type=1&tourId=" + tourId + "&sl=" + Utils.GetQueryStringValue("sl") + "\" class=\"fontgreen\">(变)</a></span>");
                }
                else
                {
                    sbChange.Append("<span><a target=\"_blank\" href=\"" + teamPrintUrl + "?type=1&tourId=" + tourId + "&sl=" + Utils.GetQueryStringValue("sl") + "\" class=\"fontred\">(变)</a></span>");
                }
            }
            return sbChange.ToString();
        }
        #endregion

        #region 获取客户单位信息
        protected string GetCustomerInfo(object customer, string type)
        {
            System.Text.StringBuilder bs = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MCompanyInfo> Company = (List<EyouSoft.Model.TourStructure.MCompanyInfo>)customer;
            if (Company != null && Company.Count > 0)
            {
                if (type == "single")
                {
                    bs.Append("" + Company[0].CompanyName + "");
                }
                else
                {
                    bs.Append("" + Company[0].CompanyName + "<br />联系人：" + Company[0].Contact + "<br />联系电话：" + Company[0].Phone + "");
                }
            }
            return bs.ToString();
        }
        #endregion

        #region 根据团队状态判断计调操作
        /// <summary>
        /// 根据团队状态判断计调操作
        /// </summary>
        /// <param name="state"></param>
        /// <param name="tourid"></param>
        /// <param name="tourQType">对外报价类型 分项 整团</param>
        /// <param name="tourType">团队类型 组团 散拼</param>
        /// <returns></returns>
        protected string GetOperate(EyouSoft.Model.EnumType.TourStructure.TourStatus state, string tourid, EyouSoft.Model.EnumType.TourStructure.TourType tourType, EyouSoft.Model.EnumType.TourStructure.TourQuoteType tourQType)
        {
            string str = string.Empty;
            string sl = Utils.GetQueryStringValue("sl");
            //string tour_Type = string.Empty;
            //计调类型 组团，地接，出境
            /*string type = string.Empty;
            if (sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_组团计调).ToString())
            {
                type = "Team";
            }
            if (sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_出境计调).ToString())
            {
                type = "Departure";
            }
            if (sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_地接计调).ToString())
            {
                type = "Agency";
            }*/
            //团队类型
            /*if (tourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 || tourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 || tourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼)
            {
                tour_Type = "sanping";
            }
            if (tourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 || tourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 || tourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队)
            {
                tour_Type = "zutuan";
                //团队报价类型 
                if (tourQType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                {
                    tour_Type += "&TourQType=" + ((int)EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团) + "";
                }
                else
                {
                    tour_Type += "&TourQType=" + ((int)EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项) + "";
                }
            }*/

            //计调未接收的显示接收任务，计调已接收的显示安排
            if (state == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收)
            {
                str = "<a data-class=\"receiveOp\" data-TourId=" + tourid + " data-teamPlaner=\"" + UtilsCommons.GetTourPlanItemBytourID(tourid, this.SiteUserInfo.UserId) + "\" href=\"javascript:void(0);\">接收任务</a>";
            }
            else
            {
                str = "<a href=\"/OperaterCenter/OperaterConfigPage.aspx?&sl=" + sl + "&tourId=" + tourid + "\">安排</a>";
            }
            return str;
        }
        #endregion

        #region 获取销售员信息
        /// <summary>
        /// 销售员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetSellerInfo(object model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MSaleInfo sale = (EyouSoft.Model.TourStructure.MSaleInfo)model;
            if (sale != null)
            {
                sb.Append("" + sale.Name + "");
            }
            return sb.ToString();
        }
        #endregion

        #region 获取计调员
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

        #region 获取操作人信息
        /// <summary>
        /// 操作人信息
        /// </summary>
        /// <param name="tourid">团号</param>
        /// <returns></returns>
        protected string GetOperaterInfo(string tourid)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MTourBaoInfo info = new EyouSoft.BLL.TourStructure.BTour().GetTourBaoInfo(tourid);
            if (info != null)
            {
                sb.Append("<b>" + info.TourCode + "</b><br />发布人：" + info.Operator + "<br />发布时间：" + EyouSoft.Common.UtilsCommons.GetDateString(info.IssueTime, ProviderToDate) + "");
            }
            info = null;
            return sb.ToString();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = this.ExporPageInfoSelect2.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = this.ExporPageInfoSelect2.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = this.ExporPageInfoSelect2.intRecordCount = RecordCount;
        }
        #endregion

        #region 绑定团队状态
        /// <summary>
        /// 团队状态
        /// </summary>
        /// <param name="statusID">状态id</param>
        /// <returns></returns>
        protected string BindTourState(string statusID)
        {
            TourStatusHtml.Append("<select name=\"tourState\" class=\"inputselect\"><option value=\"-1\">--请选择--</option>");
            List<EyouSoft.Common.EnumObj> tourStatus = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus));
            if (tourStatus != null && tourStatus.Count > 0)
            {
                for (int i = 0; i < tourStatus.Count; i++)
                {
                    if (tourStatus[i].Value == statusID)
                    {
                        TourStatusHtml.Append("<option value=\"" + tourStatus[i].Value + "\" selected=\"selected\">" + tourStatus[i].Text + "</option>");
                    }
                    else
                    {
                        TourStatusHtml.Append("<option value=\"" + tourStatus[i].Value + "\">" + tourStatus[i].Text + "</option>");
                    }
                }
            }
            TourStatusHtml.Append("</select>");
            return TourStatusHtml.ToString();
        }
        #endregion

        #region 计划类型
        /// <summary>
        /// 计划类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool GetTourType(EyouSoft.Model.EnumType.TourStructure.TourType type)
        {
            bool ret = true;
            if (type == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 || type == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 || type == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼)
            {
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            string sl = Utils.GetQueryStringValue("sl");
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_栏目) &&
                sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_地接计调).ToString())
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_栏目, false);
                return;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_栏目) &&
                sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_组团计调).ToString())
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_栏目, false);
                return;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_栏目) &&
                sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_出境计调).ToString())
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_栏目, false);
                return;
            }
        }
        #endregion

        #endregion
    }
}
