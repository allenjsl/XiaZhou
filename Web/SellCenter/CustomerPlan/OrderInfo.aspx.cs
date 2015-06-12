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

namespace Web.SellCenter
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：蔡永辉
    /// 创建时间：2012-4-12
    /// 说明：同业分销 订单列表 查看订单

    public partial class OrderInfo : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax 操作
            string ajaxtype = Utils.GetQueryStringValue("AjaxType");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);
            #endregion

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
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
                    litAdultPrice.Text = UtilsCommons.GetMoneyString(model.AdultPrice, ProviderToMoney);
                    //成人数
                    litAdults.Text = model.Adults.ToString();
                    //客源单位
                    litBuyCompanyName.Text = model.BuyCompanyName;
                    //儿童价
                    litChildPrice.Text = UtilsCommons.GetMoneyString(model.ChildPrice, ProviderToMoney);
                    //儿童数
                    litChilds.Text = model.Childs.ToString();
                    //其他费用
                    litother.Text = UtilsCommons.GetMoneyString(model.OtherCost, ProviderToMoney);
                    //联系人
                    litDContactName.Text = model.ContactName;
                    //联系电话
                    litDContactTel.Text = model.ContactTel;
                    //导游现收
                    litGuideIncome.Text = UtilsCommons.GetMoneyString(model.GuideIncome, ProviderToMoney);
                    //下单人
                    litOperator.Text = model.Operator;
                    //订单号
                    litOrderCode.Text = model.OrderCode;
                    //订单备注
                    litOrderRemark.Text = model.OrderRemark.ToString();
                    //销售费用增加
                    litSaleAddCost.Text = UtilsCommons.GetMoneyString(model.SaleAddCost, ProviderToMoney);
                    //销售费用增加备注
                    litSaleAddCostRemark.Text = model.SaleAddCostRemark;
                    //销售费用减少
                    litSaleReduceCost.Text = UtilsCommons.GetMoneyString(model.SaleReduceCost, ProviderToMoney);
                    //销售费用减少备注
                    litSaleReduceCostRemark.Text = model.SaleReduceCostRemark;
                    //销售员
                    litSellerName.Text = model.SellerName;
                    //合计金额
                    litSumPrice.Text = UtilsCommons.GetMoneyString(model.SumPrice, ProviderToMoney);
                    //销售应收
                    litSalerIncome.Text = UtilsCommons.GetMoneyString(model.SumPrice - model.GuideIncome, ProviderToMoney);
                    //价格备注
                    ltrJiaGeBeiZhu.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(new EyouSoft.BLL.TourStructure.BTour().GetTourJiaGeBeiZhu(model.TourId));
                    #endregion

                    #region 判断游客类型

                    if (model.TourType == TourType.组团团队 || model.TourType == TourType.地接团队)
                    {
                        //非出境团队
                        this.TravelControl1.SetTravelList = model.MTourOrderTravellerList;
                        //隐藏出境团队控件
                        this.TravelControlS1.Visible = false;
                    }
                    else if (model.TourType == TourType.出境团队)
                    {
                        //出境团队
                        this.TravelControlS1.SetTravelList = model.MTourOrderTravellerList;
                        //隐藏非出境团队控件
                        this.TravelControl1.Visible = false;
                    }
                    #endregion



                    //是否可以操作该数据
                    if (!SiteUserInfo.IsHandleElse)
                    {
                        if (model.TourStatus == TourStatus.封团 || (model.OperatorId == null && model.OperatorId != SiteUserInfo.UserId))
                        {
                            this.phdSave.Visible = false;
                            this.phdCancel.Visible = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目, false);
                return;
            }
        }

        #region ajax操作
        private void Ajax(string type)
        {
            //ajax前台调用返回
            string result = "";
            switch (type)
            {
                case "SaveData":
                    result = SaveData();//提交保存数据
                    break;
            }
            Response.Clear();
            Response.Write(result);
            Response.End();
        }

        private string SaveData()
        {
            //返回的信息
            string result = "";
            string orderId = Utils.GetQueryStringValue("OrderId");
            if (!string.IsNullOrEmpty(orderId))
            {
                MTourOrderExpand model = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderExpandByOrderId(orderId);
                if (model != null)
                {
                    //非出境团队
                    if (model.TourType == TourType.地接团队 || model.TourType == TourType.组团团队)
                        model.MTourOrderTravellerList = UtilsCommons.GetTravelList();
                    else if (model.TourType == TourType.出境团队)//出境团队
                        model.MTourOrderTravellerList = UtilsCommons.GetTravelListS();
                    //修改订单实体
                    if (new EyouSoft.BLL.TourStructure.BTourOrder().UpdateTourOrderExpand(orderId, model.MTourOrderTravellerList))
                    {
                        result = UtilsCommons.AjaxReturnJson("true", "保存成功");
                    }
                    else
                        result = UtilsCommons.AjaxReturnJson("false", "保存失败");
                }
                else
                    result = UtilsCommons.AjaxReturnJson("false", "数据为空");
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "参数为空");
            return result;
        }

        #endregion

        #endregion

    }
}
