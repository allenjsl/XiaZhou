using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;
namespace Web.CommonPage
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2012-4-16
    /// 销售报账-订单报账
    public partial class OrderBaoZhang : BackPage
    {
        #region attbibutes
        /// <summary>
        /// 控制游客信息显示内容
        /// </summary>
        protected bool IsShow = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            //PowerControl();

            #region ajax 操作
            string ajaxtype = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);
            #endregion

            if (!IsPostBack)
            {
                //订单编号
                string OrderId = Utils.GetQueryStringValue("OrderId");                
                //存储订单编号在隐藏域
                hidorderID.Value = OrderId;
                //初始化
                DataInit(OrderId);
            }
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                //获取订单详细实体
                MTourOrderExpand model = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderExpandByOrderId(orderId);
                if (model != null)
                {
                    #region 订单属性
                    //成人价
                    litAdultPrice.Text = model.AdultPrice.ToString("0.00");
                    txtAdultPrice.Text = model.PeerAdultPrice.ToString("0.00");
                    //成人数
                    litAdults.Text = model.Adults.ToString();
                    txtAdults.Text = model.Adults.ToString();
                    //客源单位
                    litBuyCompanyName.Text = model.BuyCompanyName;
                    //儿童价
                    litChildPrice.Text = model.ChildPrice.ToString("0.00");
                    txtChildPrice.Text = model.PeerChildPrice.ToString("0.00");
                    //儿童数
                    litChilds.Text = model.Childs.ToString();
                    txtChilds.Text = model.Childs.ToString();
                    //联系人
                    litDContactName.Text = model.ContactName;
                    //导游现收
                    //litGuideIncome.Text = model.GuideIncome.ToString("0.00");
                    //下单人
                    litOperator.Text = model.Operator;
                    //订单号
                    litOrderCode.Text = model.OrderCode;

                    //销售费用增加
                    if (model.SaleAddCost != 0 || !string.IsNullOrEmpty(model.SaleAddCostRemark))
                    {
                        //销售费用增加
                        litSaleAddCost.Text = model.SaleAddCost.ToString("F2");
                        //销售费用增加备注
                        litSaleAddCostRemark.Text = model.SaleAddCostRemark;
                    }

                    //销售费用减少
                    if (model.SaleReduceCost != 0 || !string.IsNullOrEmpty(model.SaleReduceCostRemark))
                    {
                        //销售费用减少
                        litSaleReduceCost.Text = model.SaleReduceCost.ToString("F2");
                        //销售费用减少备注
                        litSaleReduceCostRemark.Text = model.SaleReduceCostRemark;
                    }

                    //确认合同金额增加
                    if (model.SumPriceAddCost != 0 || !string.IsNullOrEmpty(model.SumPriceAddCostRemark))
                    {
                        //变更增加
                        litAddCost.Text = model.SumPriceAddCost.ToString("0.00");
                        //变更增加备注
                        litAddCostRemark.Text = model.SumPriceAddCostRemark;
                    }

                    //确认合同金额减少
                    if (model.SumPriceReduceCost != 0 || !string.IsNullOrEmpty(model.SumPriceReduceCostRemark))
                    {
                        //变更减少
                        litReduceCost.Text = model.SumPriceReduceCost.ToString("0.00");                    
                        //变更减少备注
                        litReduceCostRemark.Text = model.SumPriceReduceCostRemark;
                    }
                    
                    //结算费用增加
                    txtAddCost.Text = model.PeerAddCost.ToString("0.00");  
                    //结算费用增加备注
                    txtAddCostRemark.Text = model.PeerAddCostRemark;
                    //结算费用减少
                    txtReduceCost.Text = model.PeerReduceCost.ToString("0.00");
                    //结算费用减少备注
                    txtReduceCostRemark.Text = model.PeerReduceCostRemark;
                    
                    //销售员
                    litSellerName.Text = model.SellerName;
                    //合计金额
                    litSumPrice.Text = model.SumPrice.ToString("0.00");
                    //确认金额
                    hidConfirmMoney.Value = model.ConfirmMoney.ToString("0.00");
                    litConfirmMoney.Text = model.ConfirmMoney.ToString("0.00");
                    //销售应收
                    //litSalerIncome.Text = model.SalerIncome.ToString("0.00");
                    //确认结算金额
                    //txtSettlementMoney.Text = model.ConfirmSettlementMoney.ToString("0.00");
                    decimal ConfirmSettlementMoney = model.ConfirmSettlementMoney > 0 ? model.ConfirmSettlementMoney : (model.PeerAdultPrice * model.Adults + model.PeerChildPrice * model.Childs + model.PeerAddCost - model.PeerReduceCost);
                    txtSettlementMoney.Text = ConfirmSettlementMoney.ToString("0.00");
                    //订单利润
                    //txtProfit.Text = model.Profit.ToString("0.00");
                    txtProfit.Text = model.Profit > 0 ? model.Profit.ToString("0.00") : (model.ConfirmMoney - ConfirmSettlementMoney).ToString("0.00");
                    //是否确认合同金额
                    hidConfirmMoneyStatus.Value = model.ConfirmMoneyStatus ? "T" : "F";
                    //团队编号
                    hidTourID.Value = model.TourId;
                    //团号
                    hidTourCode.Value = model.TourName;
                    //结算人
                    if (model.JieSuanStatus)
                    {
                        ltrJieSuanXinXi.Text = "该订单结算信息在" + model.JieSuanTime.ToString("yyyy-MM-dd HH:mm") + "由" + model.SettlementPeople + "提交。";
                    }
                    EyouSoft.Model.EnumType.TourStructure.TourStatus status = new EyouSoft.BLL.TourStructure.BTour().GetTourStatus(CurrentUserCompanyID, model.TourId);
                    //if ((int)status > (int)TourStatus.销售待审)
                    if (status != TourStatus.销售待审 && status != TourStatus.导游报帐)
                    {
                        phdSave.Visible = false;
                        ltrCaoZuoTiShi.Text = "计划状态不是<b>销售待审</b>状态，不能提交订单结算信息。";
                    }
                    #endregion

                    if (model.TourType == TourType.出境散拼 || model.TourType == TourType.出境团队)
                    {
                        this.IsShow = true;
                        this.PHchujingtitle.Visible = true;
                    }

                    if (model.MTourOrderTravellerList != null && model.MTourOrderTravellerList.Count > 0)
                    {
                        this.rptList.DataSource = model.MTourOrderTravellerList;
                        this.rptList.DataBind();
                    }

                    if (!string.IsNullOrEmpty(model.OrderRemark))
                    {
                        ltrDingDanBeiZhu.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.OrderRemark);
                    }

                    if (!string.IsNullOrEmpty(model.NeiBuXinXi))
                    {
                        ltrNeiBuXinXi.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.NeiBuXinXi);
                    }
                }
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_销售报账操作))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_销售报账操作, true);
            }
        }

        #region ajax操作
        private void Ajax(string type)
        {
            //ajax前台调用返回
            switch (type)
            {
                case "save":
                    SaveData();//提交保存数据
                    break;
                case "show":
                    //this.tabOrderView.Visible = false;
                    this.phdSave.Visible = false;
                    break;
            }
        }

        private void SaveData()
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            decimal addCost = Utils.GetDecimal(Utils.GetFormValue(txtAddCost.UniqueID));
            decimal reduceCost = Utils.GetDecimal(Utils.GetFormValue(txtReduceCost.UniqueID));
            decimal confirmSettlementMoney = Utils.GetDecimal(Utils.GetFormValue(txtSettlementMoney.UniqueID));
            decimal profit = Utils.GetDecimal(Utils.GetFormValue(txtProfit.UniqueID));
            #endregion

            #region 提交回应
            //订单的结算费用信息
            MOrderSettlement orderModel = new MOrderSettlement();
            orderModel.OrderId = Utils.GetFormValue(hidorderID.UniqueID);
            orderModel.SettlementPeople = this.SiteUserInfo.Name;
            orderModel.SettlementPeopleId = this.SiteUserInfo.UserId;
            orderModel.PeerAddCost = addCost;
            orderModel.PeerReduceCost = reduceCost;
            orderModel.PeerAddCostRemark = Utils.GetFormValue(txtAddCostRemark.UniqueID);
            orderModel.PeerReduceCostRemark = Utils.GetFormValue(txtReduceCostRemark.UniqueID);
            orderModel.ConfirmSettlementMoney = confirmSettlementMoney;
            orderModel.ConfirmMoneyStatus = Utils.GetFormValue(hidConfirmMoneyStatus.UniqueID) == "T" ? true : false;
            orderModel.ConfirmMoney = Utils.GetDecimal(Utils.GetFormValue(hidConfirmMoney.UniqueID));
            orderModel.Profit = profit;
            //订单变更
            MTourOrderChange orderChangeModel = null;

            if (new EyouSoft.BLL.TourStructure.BTourOrder().UpdateOrderSettlement(orderModel, orderChangeModel))
            {
                result = true;
                msg = "成功！";
            }
            else
            {
                result = false;
                msg = "失败！";
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();
            #endregion
        }

        #endregion

        /// <summary>
        /// 获得保险数量
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsuranceImage(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string imgShow = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                imgShow = "<img src=\"/images/y-duihao.gif\" border=\"0\">";
            }
            else
            {
                imgShow = "<img src=\"/images/y-cuohao.gif\" border=\"0\">";
            }
            return imgShow;
        }

        #endregion
    }
}
