using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.GroupEnd.Suppliers
{
    public partial class OrderSee : EyouSoft.Common.Page.SupplierPage
    {
        /// <summary>
        /// 系统配置的留位时间
        /// </summary>
        protected string MaxDateTime;

        /// <summary>
        /// 最小留位时间
        /// </summary>
        protected string MinDateTime = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm");

        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = EyouSoft.Common.Utils.GetFormValue("Type");
            if (!string.IsNullOrEmpty(type))
            {
                string id = EyouSoft.Common.Utils.GetFormValue("OrderId");
                Response.Clear();
                Response.Write(DoUpdate(id));
                Response.End();
            }

            if (!IsPostBack)
            {
                string orderId = EyouSoft.Common.Utils.GetQueryStringValue("OrderId");

                if (!string.IsNullOrEmpty(orderId))
                {
                    PageInit(orderId);
                    this.hfLDate.Value = Utils.GetQueryStringValue("LDate");
                    this.hfRDate.Value = Utils.GetQueryStringValue("RDate");
                }

                #region 获得留位时间
                EyouSoft.BLL.ComStructure.BComSetting settingBll = new EyouSoft.BLL.ComStructure.BComSetting();
                EyouSoft.Model.ComStructure.MComSetting settModel = settingBll.GetModel(SiteUserInfo.CompanyId);
                if (settModel != null)
                {
                    if (settModel.SaveTime != 0)
                    {
                        this.MaxDateTime = DateTime.Now.AddMinutes(settModel.SaveTime).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
                    }
                }
                else
                {
                    this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
                }
                settingBll = null;
                settModel = null;
                #endregion
            }
        }


        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="orderId"></param>
        private void PageInit(string orderId)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand order = bOrder.GetTourOrderExpandByOrderId(orderId);
            if (order != null)
            {
                this.LtOrderCode.Text = order.OrderCode;
                this.LtSellerName.Text = order.SellerName;
                this.LtOperator.Text = order.Operator;
                this.LtAdults.Text = order.Adults.ToString();
                this.LtChilds.Text = order.Childs.ToString();
                this.LtAdultPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.AdultPrice, this.ProviderToMoney);
                this.LtChildPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.ChildPrice, this.ProviderToMoney);
                this.LtSaleAddCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SaleAddCost, this.ProviderToMoney);
                this.LtSaleAddCostRemark.Text = order.SaleAddCostRemark;
                this.LtSaleReduceCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SaleReduceCost, this.ProviderToMoney);
                this.LtSaleReduceCostRemark.Text = order.SaleReduceCostRemark;
                this.LtSumPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SumPrice, this.ProviderToMoney);
                this.LtGuideIncome.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.GuideIncome, this.ProviderToMoney);
                this.LtSalerIncome.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SalerIncome, this.ProviderToMoney);
                this.txtSaveSeatDate.Text = order.SaveSeatDate.HasValue ? order.SaveSeatDate.Value.ToString() : string.Empty;
                this.LtOrderRemark.Text = order.OrderRemark;

                if (order.MTourOrderTravellerList != null && order.MTourOrderTravellerList.Count != 0)
                {
                    this.RpTravller.DataSource = order.MTourOrderTravellerList;
                    this.RpTravller.DataBind();
                }
                else
                {
                    this.phTraveller.Visible = false;
                }



                switch (order.OrderStatus)
                {
                    case EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理:
                    case EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位:
                        this.PhDo.Visible = true;
                        break;
                    default:
                        this.PhDo.Visible = false;
                        break;
                }


                //写入计调信息的TourId
                this.hfTourId.Value = order.TourId;
                this.hfAdults.Value = order.Adults.ToString();
                this.hfChilds.Value = order.Childs.ToString();
                this.hfConfirmSettlementMoney.Value = order.ConfirmSettlementMoney.ToString();

            }
        }


        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private string DoUpdate(string orderId)
        {
            string msg = string.Empty;
            string strStatus = EyouSoft.Common.Utils.GetFormValue("Status");
            if (string.IsNullOrEmpty(strStatus))
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "订单修改 失败！");
            }
            else
            {
                EyouSoft.Model.EnumType.TourStructure.OrderStatus OrderStatus = (EyouSoft.Model.EnumType.TourStructure.OrderStatus)int.Parse(strStatus);

                EyouSoft.Model.PlanStructure.MPlanBaseInfo plan = null;
                if (OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交)
                {
                    plan = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
                    plan.CompanyId = SiteUserInfo.CompanyId;
                    plan.TourId = Utils.GetFormValue("TourId");
                    plan.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接;
                    plan.SourceId = SiteUserInfo.SourceCompanyInfo.CompanyId;
                    plan.SourceName = SiteUserInfo.SourceCompanyInfo.CompanyName;

                    plan.ContactName = SiteUserInfo.Name;
                    plan.ContactPhone = SiteUserInfo.Telephone;
                    plan.ContactFax = SiteUserInfo.Fax;
                    plan.Num = Utils.GetInt(Utils.GetFormValue("Adults")) + Utils.GetInt(Utils.GetFormValue("Childs"));
                    plan.PlanCost = Utils.GetDecimal(Utils.GetFormValue(this.hfConfirmSettlementMoney.UniqueID));
                    plan.PaymentType = EyouSoft.Model.EnumType.PlanStructure.Payment.财务支付;
                    plan.Status = EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实;

                    //已确认（存储过程已自动写入计划的审核人信息。）
                    //plan.CostId = SiteUserInfo.UserId;
                    //plan.CostName = SiteUserInfo.Username;
                    plan.CostStatus = true;
                    plan.CostTime = DateTime.Now;
                    plan.Confirmation = Utils.GetDecimal(Utils.GetFormValue("ConfirmSettlementMoney"));

                    plan.StartDate = Utils.GetDateTimeNullable(Utils.GetFormValue("LDate"));
                    plan.EndDate = Utils.GetDateTimeNullable(Utils.GetFormValue("RDate"));

                    //存储过程已自动写入计划的审核人信息。
                    //plan.DeptId = SiteUserInfo.DeptId;
                    //plan.OperatorId = SiteUserInfo.UserId;
                    //plan.OperatorName = SiteUserInfo.Username;
                    plan.IssueTime = DateTime.Now;
                    plan.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;

                }

                string strSaveSeatDate = EyouSoft.Common.Utils.GetFormValue("SaveSeatDate");
                DateTime? SaveSeatDate = !string.IsNullOrEmpty(strSaveSeatDate) ? (DateTime?)Utils.GetDateTime(strSaveSeatDate) : null;

                EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();

                if (bOrder.UpdateTourOrderExpand(orderId, OrderStatus, SaveSeatDate, null, plan))
                {

                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "订单已修改为 " + OrderStatus + "！");
                }
                else
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "订单修改 失败！");

                }
            }

            return msg;
        }
    }
}
