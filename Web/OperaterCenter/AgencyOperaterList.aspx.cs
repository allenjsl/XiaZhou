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
    /// 计调中心—地接计调列表
    /// 创建人：李晓欢
    /// 创建时间：2011-09-23
    /// </summary>
    public partial class AgencyOperaterList : Eyousoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //销售员
                string SelerName = this.Seller1.SellsName;
                //销售员id
                string SelerId = this.Seller1.SellsID;
                //客户单位id
                string CustomerId = this.Customer1.CustomerUnitId;
                //客户单位
                string CustomerName = this.Customer1.CustomerUnitName;
                if (SelerId != "" && SelerName != "")
                {
                    this.Seller1.SellsID = SelerId;
                    this.Seller1.SellsName = SelerName;
                }
                if (CustomerId != "" && CustomerName != "")
                {
                    this.Customer1.CustomerUnitId = CustomerId;
                    this.Customer1.CustomerUnitName = CustomerName;
                }
                //初始化
                DataInit();
            }
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.TourStructure.MPlanListSearch Search = new EyouSoft.Model.TourStructure.MPlanListSearch();
            Search.CompanyInfo = new EyouSoft.Model.TourStructure.MCompanyInfo();
            Search.CompanyInfo.CompanyId = this.Customer1.CustomerUnitId;
            Search.CompanyInfo.CompanyName = this.Customer1.CustomerUnitName;
            Search.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo();
            Search.SaleInfo.SellerId = this.Seller1.SellsID;
            Search.SaleInfo.Name = this.Seller1.SellsName;
            Search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("TxtmeetTeamTime"));
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            IList<EyouSoft.Model.TourStructure.MPlanList> Agencylist = new EyouSoft.BLL.TourStructure.BTour().GetDJPlanList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, Search);
            if (Agencylist != null && Agencylist.Count > 0)
            {
                this.AgOperaterList.DataSource = Agencylist;
                this.AgOperaterList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lab_text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }

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

        #region 获取销售员信息
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

        #region 根据团队状态判断计调操作
        protected string GetOperate(string state, string tourid)
        {
            string str = "";
            string sl = Utils.GetQueryStringValue("sl");
            if (state == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收.ToString())
            {
                str = "<a href=\"javascript:\" class=\"receiveOp\" tourid=\"" + tourid + "\">接收</a>";
            }
            else
            {
                str = "<a href=\"/OperaterCenter/OperaterHotelList.aspx?sl=" + sl + "&type=Agency&Id=" + tourid + "\">安排</a>";             
            }
            return str;
        }
        #endregion

        #region 获取计调员
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
        protected string GetOperaterInfo(string tourid)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MTourBaoInfo info = new EyouSoft.BLL.TourStructure.BTour().GetTourBaoInfo(tourid);
            if (info != null)
            {
                sb.Append("<b>" + info.TourCode + "</b><br />发布人：" + info.Operator + "<br />发布时间：" + info.IssueTime.ToString("yyyy-MM-dd") + "");
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (CheckGrant(Common.Enum.TravelPermission.计调中心_地接计调_栏目))
            {
                if (!CheckGrant(Common.Enum.TravelPermission.计调中心_地接计调_查看全部))
                {
                    Utils.ResponseNoPermit(Common.Enum.TravelPermission.计调中心_地接计调_查看全部, false);
                    return;
                }
            }
            else
            {
                Utils.ResponseNoPermit(Common.Enum.TravelPermission.计调中心_地接计调_栏目, false);
                return;
            }
        }
        #endregion
    }
}
