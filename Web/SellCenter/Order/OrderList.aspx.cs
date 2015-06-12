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
using EyouSoft.BLL.ComStructure;

namespace Web.SellCenter.Order
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-7
    /// 说明：同业分销 中 收客计划 列表

    public partial class OrderList : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 订单类型
        /// </summary>
        protected int intOrderType = 0;
        /// <summary>
        /// 结算单打印路径
        /// </summary>
        string Print_JieSuanDan = string.Empty;
        /// <summary>
        /// 游客名单打印路径
        /// </summary>
        string Print_YouKeMingDan = string.Empty;
        /// <summary>
        /// 行程单(团队)
        /// </summary>
        string Print_XingChengDan_TuanDui = string.Empty;
        /// <summary>
        /// 行程单(散拼)
        /// </summary>
        string Print_XingChengDan_SanPin = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            if (UtilsCommons.IsToXls()) ToXls();

            InitPrint();            
            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MSearchOrderCenter GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TourStructure.MSearchOrderCenter();

            info.TourCode=Utils.GetQueryStringValue("txtTourCode");
            info.OrderCode=Utils.GetQueryStringValue("txtOrderCode");
            info.RouteName=Utils.GetQueryStringValue("txtRouteName");
            info.SellerId=SellsSelect1.SellsID=Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
            info.SellerName=SellsSelect1.SellsName= Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
            info.LeaveBeginTime= Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLeaveBeginTime"));
            info.LeaveEndTime=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLeaveEndTime"));
            info.OrderIssueBeginTime=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOrderIssueBeginTime"));
            info.OrderIssueEndTime=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOrderIssueEndTime"));
            info.OrderTypeBySearch = Utils.GetEnumValue<OrderTypeBySearch>(Utils.GetQueryStringValue("OrderTypeBySearch"), OrderTypeBySearch.我操作的订单);
            info.CompanyId = CurrentUserCompanyID;
            info.XiaDanRenId = txtXiaDanRen.SellsID = Utils.GetQueryStringValue(txtXiaDanRen.SellsIDClient);
            info.XiaDanRenName = txtXiaDanRen.SellsName = Utils.GetQueryStringValue(txtXiaDanRen.SellsNameClient);

            string status = Utils.GetQueryStringValue(txtOrderStatus.ZTIdClientID);
            if (!string.IsNullOrEmpty(status))
            {
                string[] items = status.Split(',');
                if (items != null && items.Length > 0)
                {
                    info.OrderStatus = new EyouSoft.Model.EnumType.TourStructure.OrderStatus[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        info.OrderStatus[i] = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.OrderStatus>(items[i], EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交);
                    }
                }
            }

            info.CrmId = Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHBH);
            info.CrmName = Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHMC);

            string xiaoShouYuanDepts = txtXiaoShouYuanDept.SectionID = Utils.GetQueryStringValue(txtXiaoShouYuanDept.SelectIDClient);
            txtXiaoShouYuanDept.SectionName = Utils.GetQueryStringValue(txtXiaoShouYuanDept.SelectNameClient);

            if (!string.IsNullOrEmpty(xiaoShouYuanDepts))
            {
                string[] items = xiaoShouYuanDepts.Split(',');
                if (items != null && items.Length > 0)
                {
                    info.XiaoShouYuanDeptIds = new int[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        info.XiaoShouYuanDeptIds[i] = Utils.GetInt(items[i]);
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            object[] heJi;
            
            IList<MTradeOrder> list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderList(chaXun, pageSize, pageIndex, ref recordCount, out heJi);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                BindPage();

                ltrHeJi0.Text = recordCount.ToString();
                ltrHeJi1.Text = heJi[0].ToString();
                ltrHeJi2.Text = heJi[1].ToString();
                ltrHeJi3.Text = UtilsCommons.GetMoneyString(heJi[2], ProviderToMoney);
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;

                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
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
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 初始化打印信息
        /// </summary>
        void InitPrint()
        {
            var printBLL = new EyouSoft.BLL.ComStructure.BComSetting();
            Print_JieSuanDan = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
            Print_YouKeMingDan = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游客名单);
            Print_XingChengDan_SanPin = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单); 
            Print_XingChengDan_TuanDui = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            printBLL = null;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;           

            var chaXun = GetChaXunInfo();
            object[] heJi;
            var items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderList(chaXun, toXlsRecordCount, 1, ref _recordCount, out heJi);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);


            StringBuilder s = new StringBuilder();

            s.Append("线路名称\t订单号\t下单人\t销售员\t客源单位\t人数\t合计金额\t状态\t下单时间\t订单备注\n");

            foreach (var item in items)
            {
                s.Append(item.RouteName + "\t");
                s.Append(item.OrderCode + "\t");
                s.Append(item.Operator + "\t");
                s.Append(item.SellerName + "\t");
                s.Append(item.BuyCompanyName + "\t");
                s.Append(item.Adults + "+" + item.Childs + "\t");
                s.Append(item.ConfirmMoney.ToString("F2") + "\t");
                s.Append(item.OrderStatus + "\t");
                s.Append(item.IssueTime.Value.ToString("yyyy-MM-dd HH:mm") + "\t");
                s.Append(item.DingDanBeiZhu.Replace("\t", "    ").Replace("\r\n", "    ") + "\n");
            }
            
            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取结算单打印路径
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tourType"></param>
        /// <returns></returns>
        protected string GetPrintJieSuanDan(object orderId, object tourType)
        {
            if (orderId == null || tourType == null || Print_JieSuanDan == "javascript:void(0)") return "javascript:void(0)";
            string _orderId = orderId.ToString();
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            return Print_JieSuanDan + "?OrderId=" + _orderId + "&tourType=" + (int)_tourType;
        }

        /// <summary>
        /// 获取游客名称打印路径
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        protected string GetPrintYouKeMingDan(object orderId)
        {
            if (orderId == null || Print_YouKeMingDan == "javascript:void(0)") return "javascript:void(0)";

            string _orderId = orderId.ToString();
            return Print_YouKeMingDan + "?OrderId=" + _orderId;
        }

        /// <summary>
        /// 获取游客确认单打印路径
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="tourType">团队类型</param>
        /// <returns></returns>
        protected string GetPrintYouKeQueRenDan(object orderId, object tourType)
        {
            if (orderId == null || tourType == null || Print_JieSuanDan == "javascript:void(0)") return "javascript:void(0)";

            string _orderId = orderId.ToString() ;
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            return Print_JieSuanDan + "?OrderId=" + _orderId + "&tourType=" + (int)_tourType + "&ykxc=1";
        }

        /// <summary>
        /// 获取行程单打印路径
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="tourType">团队类型</param>
        /// <returns></returns>
        protected string GetPrintXingChengDan(object tourId,object tourType)
        {
            if (tourId == null || tourType == null) return "javascript:void(0)";
            string _tourId = tourId.ToString();
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            string s=string.Empty;

            if (_tourType == TourType.出境团队
                || _tourType == TourType.地接团队
                || _tourType == TourType.组团团队) s = Print_XingChengDan_TuanDui;
            else if (_tourType == TourType.单项服务) s = string.Empty;
            else s = Print_XingChengDan_SanPin;

            if (string.IsNullOrEmpty(s) || s == "javascript:void(0)") return "javascript:void(0)";

            return s + "?tourid=" + _tourId;
        }
        #endregion
    }
}
