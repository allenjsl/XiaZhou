using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.SellCenter
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-7
    /// 说明：同业分销 中 收客计划 列表

    public partial class OrderCenterList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax 操作
            //ajax操作类型
            string ajaxtype = "";
            ajaxtype = Utils.GetQueryStringValue("ajaxtype");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);
            #endregion
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();


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
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 搜索条件
            //团号
            string teamNum = Utils.GetQueryStringValue("txtTourCode");
            //订单号
            string OrderCode = Utils.GetQueryStringValue("txtOrderCode");
            //线路名称
            string lineName = Utils.GetQueryStringValue("txtRouteName");
            //销售员
            string salesMan = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsName = salesMan;
            //出团时间
            DateTime? leaveTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLeaveBeginTime"));
            //回团时间
            DateTime? backTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLeaveEndTime"));

            //下单开始时间
            DateTime? OrderIssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOrderIssueBeginTime"));
            //下单结束时间
            DateTime? OrderIssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOrderIssueEndTime"));
            //订单类型
            int intOrderType = Utils.GetInt(Utils.GetQueryStringValue("OrderTypeBySearch"), 0);

            EyouSoft.Model.TourStructure.MSearchOrderCenter searchModel = new EyouSoft.Model.TourStructure.MSearchOrderCenter();
            searchModel.OrderCode = OrderCode;
            searchModel.OrderIssueBeginTime = backTime;
            searchModel.OrderIssueEndTime = leaveTime;
            searchModel.LeaveBeginTime = backTime;
            searchModel.LeaveEndTime = leaveTime;
            searchModel.TourCode = teamNum;
            searchModel.RouteName = lineName;
            searchModel.SellerName = salesMan;
            searchModel.CompanyId = SiteUserInfo.CompanyId;
            searchModel.OrderTypeBySearch = (OrderTypeBySearch)intOrderType;
            #endregion
            //声明list对象保存收客列表数据
            /*object[] heJi;
            IList<MTradeOrder> list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderList(searchModel, pageSize, pageIndex, ref recordCount, out heJi);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }*/
        }
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_查看全部))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_查看全部, false);
                return;
            }
        }

        #region ajax操作
        private void Ajax(string type)
        {
            string result = "";
            switch (type)
            {
                case "OperatorInfo":
                    result = "";
                    break;
            }
            Response.Clear();
            Response.Write(result);
            Response.End();
        }

        #endregion

        #endregion

        #region 前台调用方法

        #endregion
    }
}
