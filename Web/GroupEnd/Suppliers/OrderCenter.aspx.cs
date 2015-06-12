using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Page;

namespace Web.GroupEnd.Suppliers
{
    /// <summary>
    /// 功能：订单中心列表
    /// 创建人：马昌雄 2011-10-18
    /// </summary>
    public partial class OrderCenter : SupplierPage
    {
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //公告
                this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;

                string tourId = Utils.GetQueryStringValue("TourId");
                BindSource(tourId);


            }
        }





        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindSource(string tourId)
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            EyouSoft.Model.TourStructure.MSearchSupplierOrder search = new EyouSoft.Model.TourStructure.MSearchSupplierOrder();
            search.CompanyId = SiteUserInfo.CompanyId;
            search.SourceId = SiteUserInfo.SourceCompanyInfo.CompanyId;

            search.TourId = tourId;
            search.OrderCode = Utils.GetQueryStringValue("txtOrderCode");
            search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            search.AreaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"), 0);
            search.BeginIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginIssueTime"));
            search.EndIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndIssueTime"));
            search.BeginLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginLDate"));
            search.EndLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndLDate"));

            search.Status = !string.IsNullOrEmpty(Utils.GetQueryStringValue("ddlOrderStatus")) ? (EyouSoft.Model.EnumType.TourStructure.OrderStatus?)Utils.GetInt(Utils.GetQueryStringValue("ddlOrderStatus")) : null;

            EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
            IList<EyouSoft.Model.TourStructure.MSupplierOrder> list = bOrder.GetOrderList(search, pageSize, pageIndex, ref recordCount);
            this.RpOrder.DataSource = list;
            this.RpOrder.DataBind();


            BindPage();


        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }
    }
}
