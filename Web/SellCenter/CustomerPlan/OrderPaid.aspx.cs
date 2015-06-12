using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.Model.TourStructure;
using System.Text;

namespace Web.SellCenter
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-7
    /// 说明：同业分销 中 收客计划 列表 实收人数查看

    public partial class OrderPaid : BackPage
    {
        #region attributes
        /// <summary>
        /// 结算单url
        /// </summary>
        public string Print_JieSuanDan = "";
        /// <summary>
        /// 是否是短线
        /// </summary>
        protected string tourtype = string.Empty;
        /// <summary>
        /// 总记录数
        /// </summary>
        protected int RecordCount = 0;

        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourID");
            if (UtilsCommons.IsToXls()) ToXls();
            PowerControl();
            InitPrintPath();            
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            MOrderSum orderSum = new MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> list = null;

            var model = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Convert.ToString(TourId));

            if (model != null)
            {
                tourtype = model.TourType == TourType.组团散拼短线 ? "短线" : "";
            }

            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("plan")))//计调订单查询的订单为已成交订单
            {
                list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orderSum, TourId);
            }
            else
            {
                list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(TourId, ref orderSum);
            }

            if (list != null && list.Count > 0)
            {
                RecordCount = list.Count;
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                if (tourtype == "短线")
                {
                    this.litMsg.Text = "<tr><td align='center' colspan='11'>没有订单!</td></tr>";
                }
                else
                {
                    this.litMsg.Text = "<tr><td align='center' colspan='10'>没有订单!</td></tr>";
                    this.phUnset.Visible = false;
                }
            }

            list = null;
            orderSum = null;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            MOrderSum orderSum = new MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> items = null;

            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("plan")))//计调订单查询的订单为已成交订单
            {
                items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orderSum, TourId);
            }
            else
            {
                items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(TourId, ref orderSum);
            }

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);


            StringBuilder s = new StringBuilder();

            s.Append("订单号\t下单人\t销售员\t客源单位\t人数\t合计金额\t状态\t下单时间\t订单备注\n");

            foreach (var item in items)
            {
                s.Append(item.OrderCode + "\t");
                s.Append(item.Operator + "\t");
                s.Append(item.SellerName + "\t");
                s.Append(item.BuyCompanyName + "\t");
                s.Append(item.Adults + "+" + item.Childs + "\t");
                s.Append(item.ConfirmMoney.ToString("F2") + "\t");
                s.Append(item.OrderStatus + "\t");
                s.Append(item.IssueTime.Value.ToString("yyyy-MM-dd HH:mm") + "\t");
                s.Append(item.OrderRemark.Replace("\t", "    ").Replace("\r\n", "    ") + "\n");
            }

            ResponseToXls(s.ToString());
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
        /// 初始化打印路径信息
        /// </summary>
        void InitPrintPath()
        {
            var printBLL = new EyouSoft.BLL.ComStructure.BComSetting();
            Print_JieSuanDan = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
            printBLL = null;
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
        /// 是否显示未分配座位人数列(短线显示)
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <returns></returns>
        protected string IsShow(object unset)
        {
            string str = string.Empty;
            if (tourtype == "短线")
            {
                str += "<td align='center' class='fontred'>" + Convert.ToString(unset) + "</td>";
            }
            else
            {
                this.phUnset.Visible = false;
            }
            return str;
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

            string _orderId = orderId.ToString();
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            return Print_JieSuanDan + "?OrderId=" + _orderId + "&tourType=" + (int)_tourType + "&ykxc=1";
        }
        #endregion

    }
}
