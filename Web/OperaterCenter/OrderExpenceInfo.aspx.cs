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
    /// 订单报账 
    /// 李晓欢 2012-04-09
    /// </summary>
    public partial class OrderExpenceInfo : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax请求 保存
            string type = Utils.GetQueryStringValue("type");
            //团号
            string tourId = Utils.GetQueryStringValue("tourId");
            //订单号
            string OrderID = Utils.GetQueryStringValue("OrderId");
            if (type == "save")
            {
                if (!string.IsNullOrEmpty(tourId) && !string.IsNullOrEmpty(OrderID))
                {
                    Response.Clear();
                    Response.Write(OrderConfirmPageSave(OrderID, tourId));
                    Response.End();
                }
            }
            #endregion

            if (!IsPostBack)
            {
                //订单号 初始化订单信息
                string orderId = Utils.GetQueryStringValue("OrderId");
                GetOrderInfoByOrderID(orderId);                
            }
           
        }

        #region 订单详细信息
        /// <summary>
        /// 订单详细信息
        /// </summary>
        /// <param name="OrderID">订单号</param>
        protected void GetOrderInfoByOrderID(string OrderID)
        {
            if (!string.IsNullOrEmpty(OrderID))
            {
                EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderExpandByOrderId(OrderID);
                if (orderModel != null)
                {
                    this.litOrderCode.Text = orderModel.OrderCode;
                    this.litBuyCompany.Text = orderModel.BuyCompanyName;
                    this.litContectName.Text = orderModel.ContactName;
                    this.litOrderSellers.Text = orderModel.SellerName;
                    this.litOperator.Text = orderModel.Operator;
                    this.litAdultNums.Text = orderModel.Adults.ToString();
                    this.litAdultPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.AdultPrice, ProviderToMoney);
                    this.litChildPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.ChildPrice,ProviderToMoney);
                    this.litChildNums.Text = orderModel.Childs.ToString();
                    this.litAddMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.SaleAddCost, ProviderToMoney);
                    this.litAddRemark.Text = orderModel.SaleAddCostRemark;
                    this.litLessMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.SaleReduceCost,ProviderToMoney);
                    this.litLessRemark.Text = orderModel.SaleReduceCostRemark;
                    this.litSellerRe.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.SalerIncome, ProviderToMoney);
                    this.litGuidRe.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orderModel.GuideIncome, ProviderToMoney);
                    this.litOrderRemark.Text = orderModel.OrderRemark;
                    //游客信息                    
                    if (orderModel.MTourOrderTravellerList != null && orderModel.MTourOrderTravellerList.Count > 0)
                    {
                        this.repCusomerList.DataSource = orderModel.MTourOrderTravellerList;
                        this.repCusomerList.DataBind();
                    }
                }
            }
        }
        #endregion

        #region 订单结算信息
        /// <summary>
        /// 订单结算信息保存
        /// 订单id
        /// </summary>
        /// <returns></returns>
        protected string OrderConfirmPageSave(string orderID,string tourID)
        {
            string msg = string.Empty;
            EyouSoft.Model.TourStructure.MOrderSettlement settleMent = new EyouSoft.Model.TourStructure.MOrderSettlement();
            settleMent.OrderId = orderID;
            settleMent.ConfirmSettlementMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtConfirmMoney.UniqueID));
            settleMent.PeerAddCost = Utils.GetDecimal(Utils.GetFormValue(this.txtAddMoney.UniqueID));
            settleMent.PeerAddCostRemark = Utils.GetFormValue(this.txtAddMoneyRemark.UniqueID);
            settleMent.PeerReduceCost = Utils.GetDecimal(Utils.GetFormValue(this.txtLessMoney.UniqueID));
            settleMent.PeerReduceCostRemark = Utils.GetFormValue(this.txtLessMoneyRemark.UniqueID);
            settleMent.Profit = Utils.GetDecimal(Utils.GetFormValue(this.txtOrderProfit.UniqueID));
            settleMent.SettlementPeople = this.SiteUserInfo.Name;
            settleMent.SettlementPeopleId = this.SiteUserInfo.UserId;            
            EyouSoft.Model.TourStructure.MTourOrderChange orderChange = new EyouSoft.Model.TourStructure.MTourOrderChange();
            orderChange.CompanyId = this.SiteUserInfo.CompanyId;
            orderChange.OrderId = orderID;
            orderChange.TourId = tourID;
            bool result = new EyouSoft.BLL.TourStructure.BTourOrder().UpdateOrderSettlement(settleMent, orderChange);
            if (result)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "保存成功！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "保存失败！");
            }
            return msg;
        }
        #endregion
    }
}
