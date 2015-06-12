using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.GroupEnd.Distribution
{
    public partial class OrderSee : EyouSoft.Common.Page.FrontPage
    {
        //获取tourid
        protected string tourID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = EyouSoft.Common.Utils.GetQueryStringValue("Type");
            tourID = EyouSoft.Common.Utils.GetQueryStringValue("tourid");

            if (!string.IsNullOrEmpty(type))
            {
                string id = EyouSoft.Common.Utils.GetQueryStringValue("OrderId");
                Response.Clear();
                Response.Write(DoUpdate(id));
                Response.End();
            }

            if (!IsPostBack)
            {
                string orderId = EyouSoft.Common.Utils.GetQueryStringValue("OrderId");
                string dotype = EyouSoft.Common.Utils.GetQueryStringValue("Type");
                if (!string.IsNullOrEmpty(orderId))
                {
                    PageInit(orderId, dotype);
                }
            }
        }


        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="orderId"></param>
        private void PageInit(string orderId, string type)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand order = bOrder.GetTourOrderExpandByOrderId(orderId);
            if (order != null)
            {
                this.LtOrderCode.Text = order.OrderCode;
                this.LtDCompanyName.Text = order.DCompanyName;
                this.LtDContactName.Text = order.DContactName;
                this.LtDContactTel.Text = order.DContactTel;

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
                this.LtSaveSeatDate.Text = order.SaveSeatDate.HasValue ? order.SaveSeatDate.Value.ToString() : string.Empty;
                this.LtOrderRemark.Text = order.OrderRemark;

                EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
                EyouSoft.Model.TourStructure.MTourBaseInfo model = bll.GetTourInfo(Convert.ToString(tourID));
                if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
                {
                    string CarLocation = string.Empty;
                    if (order.TourOrderCarLocation != null)
                    {
                        CarLocation = order.TourOrderCarLocation.Location;
                    }
                    if (!string.IsNullOrEmpty(CarLocation))
                    {
                        this.setCarLocation.Text = CarLocation;
                    }
                    else
                    {
                        this.setCarLocation.Text = "<font class='fontred'>未设置上车地点</font>";
                    }
                    string carInfo = string.Empty;
                    if (order.TourCarTypeList != null && order.TourCarTypeList.Count > 0)
                    {
                        for (int i = 0; i < order.TourCarTypeList.Count; i++)
                        {
                            if (i == order.TourCarTypeList.Count - 1)
                            {
                                carInfo += order.TourCarTypeList[i].CarTypeName;
                            }
                            else
                            {
                                carInfo += order.TourCarTypeList[i].CarTypeName + "、";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(carInfo))
                    {
                        this.carInfo.Text = carInfo;
                    }
                    else
                    {
                        this.carInfo.Text = "<font class='fontred'>未设置车型</font>";
                    }
                }
                else
                {
                    this.PhCarLocation.Visible = false;
                }

                if (order.MTourOrderTravellerList != null && order.MTourOrderTravellerList.Count != 0)
                {
                    this.RpTravller.DataSource = order.MTourOrderTravellerList;
                    this.RpTravller.DataBind();
                }
                else
                {
                    this.phTraveller.Visible = false;
                }



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
            string strStatus = EyouSoft.Common.Utils.GetQueryStringValue("Status");
            if (string.IsNullOrEmpty(strStatus))
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "订单修改 失败！");
            }
            else
            {

                EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                if (bOrder.UpdateTourOrderExpand(orderId, (EyouSoft.Model.EnumType.TourStructure.OrderStatus)int.Parse(strStatus), null)==1)
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "订单修改 成功！");
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
