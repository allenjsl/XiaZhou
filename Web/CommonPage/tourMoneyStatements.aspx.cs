using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.CommonPage
{
    /// <summary>
    /// 团款确认单
    /// 李晓欢 2012-04-05
    /// </summary>
    /// OrderId=订单编号
    /// tourType=团队类型
    /// action  页面来源
    /// action=1  销售收款  action=2  销售报账 计调报账   action=3  财务应收管理
    public partial class tourMoneyStatements : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            #region ajax请求处理
            string doType = Utils.GetQueryStringValue("action");            

            switch (doType)
            {
                case "save":
                    Response.Clear();
                    Response.Write(PageSave());
                    Response.End();
                    break;
                case "QuXiaoQueRenHeTongJinE":
                    QuXiaoQueRenHeTongJinE();
                    break;
                default: break;
            }
            #endregion

            DataInit();
        }

        #region 页面初始化
        /// <summary>
        /// 团款详细信息
        /// </summary>
        /// <param name="orderID">订单类型</param>
        /// <param name="tourType">团队类型</param>
        protected void DataInit()
        {
            string orderId = Utils.GetQueryStringValue("OrderId");
            int tourType = Utils.GetInt(Utils.GetQueryStringValue("tourType"));            
            string action = Utils.GetQueryStringValue("action");

            var privs = (action == "3") ? EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_确认合同金额 : EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_确认合同金额;
            pan_Save.Visible = (action == "1" || action == "3") && CheckGrant(privs);

            if (!string.IsNullOrEmpty(orderId))
            {
                EyouSoft.Model.TourStructure.MOrderSale salesmodel = new EyouSoft.BLL.TourStructure.BTourOrder().GetSettlementOrderByOrderId(orderId, (EyouSoft.Model.EnumType.TourStructure.TourType)tourType);
                if (salesmodel != null)
                {
                    EyouSoft.Model.TourStructure.MTourBaseInfo tourInfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(salesmodel.TourId);

                    string _printPageJSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
                    ltrCaoZuoTiShi.Text = string.Format("<a target=\"_blank\"  class=\"unbtn\" href=\"{0}?OrderId={1}&tourType={2}\">查看确认件</a>", _printPageJSD, salesmodel.OrderId, (int)tourInfo.TourType);
                    
                    //已经确认过合同金额的不可再确认
                    if (salesmodel.ConfirmMoneyStatus)
                    {
                        pan_Save.Visible = false;
                        ltrCaoZuoTiShi.Text += string.Format("合同金额已确认&nbsp;&nbsp;&nbsp;&nbsp;", salesmodel.OrderId, (int)tourInfo.TourType);

                        if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_取消确认合同金额))
                        {
                            ph_QuXiaoQueRen.Visible = true;
                        }
                    }

                    //非订单销售员不可确认合同金额
                    if (pan_Save.Visible && salesmodel.SellerId != SiteUserInfo.UserId)
                    {
                        pan_Save.Visible = false;
                        ltrCaoZuoTiShi.Text += "非订单销售员不能确认合同金额&nbsp;&nbsp;&nbsp;&nbsp;";
                    }

                    if (salesmodel.ConfirmMoneyStatus)
                    {
                        this.txtSettlementName.Text = salesmodel.ConfirmPeople;
                    }
                    else
                    {
                        this.txtSettlementName.Text = this.SiteUserInfo.Name;
                    }
                    this.txtChangeAddMoney.Text = Utils.FilterEndOfTheZeroDecimal(salesmodel.SumPriceAddCost);
                    this.txtChangeRemark.Text = salesmodel.SumPriceAddCostRemark;
                    this.txtChangelessonMoney.Text = Utils.FilterEndOfTheZeroDecimal(salesmodel.SumPriceReduceCost);
                    this.txtChangeRemarks.Text = salesmodel.SumPriceReduceCostRemark;
                    decimal sumPrices = salesmodel.SumPrice;
                    decimal addPrices = salesmodel.SumPriceAddCost;
                    decimal lessPrices = salesmodel.SumPriceReduceCost;
                    //decimal countPrices = sumPrices + addPrices - lessPrices;
                    this.txtComfirmMoney.Text = Utils.FilterEndOfTheZeroDecimal(salesmodel.ConfirmMoney);
                    this.hidComfirmMoney.Value = Utils.FilterEndOfTheZeroDecimal(salesmodel.ConfirmMoney);
                    this.txtchangeEsplain.Text = salesmodel.ConfirmRemark;
                    //已支付金额
                    this.litpayMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(salesmodel.CheckMoney, ProviderToMoney);
                    this.hidPayMoney.Value = Utils.FilterEndOfTheZeroDecimal(salesmodel.CheckMoney);
                    decimal payMoney = salesmodel.CheckMoney;
                    decimal debtMoney = salesmodel.ConfirmMoney - payMoney;
                    //尚欠金额
                    this.txtDebtMoney.Text = Utils.FilterEndOfTheZeroDecimal(debtMoney);

                    //出团时间
                    this.litLDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(salesmodel.LDate, ProviderToDate);
                    //线路名称
                    this.litRouteName.Text = salesmodel.RouteName;
                    //订单信息
                    this.litorderCode.Text = salesmodel.OrderCode;
                    this.litCompanyName.Text = salesmodel.BuyCompanyName;
                    this.litContectName.Text = salesmodel.ContactName;
                    this.litContectPhone.Text = salesmodel.ContactTel;
                    //退款信息
                    if (salesmodel.TourOrderSalesList != null && salesmodel.TourOrderSalesList.Count > 0)
                    {
                        this.repTourOrderSalesList.DataSource = salesmodel.TourOrderSalesList;
                        this.repTourOrderSalesList.DataBind();
                    }
                    else
                    {
                        phTuiKuan.Visible = false;
                    }
                    //增加费用 备注
                    this.litAddMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(salesmodel.SaleAddCost, ProviderToMoney);
                    this.litAddRemark.Text = salesmodel.SaleAddCostRemark;
                    //减少费用 备注
                    this.litlessenMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(salesmodel.SaleReduceCost, ProviderToMoney);
                    this.litlessenRemark.Text = salesmodel.SaleReduceCostRemark;                    
                    //合计费用
                    this.litAccountPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(salesmodel.SumPrice, ProviderToMoney);
                    //订单合计金额
                    this.hidAccountPrices.Value = EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(salesmodel.SumPrice);

                    string _jiaGeHtml = string.Empty;
                    _jiaGeHtml += string.Format("成人价：<b>{0}</b> * 成人数：<b>{1}</b>", salesmodel.AdultPrice.ToString("F2"), salesmodel.Adults);
                    _jiaGeHtml += string.Format(" + 儿童价：<b>{0}</b> * 儿童数：<b>{1}</b>", salesmodel.ChildPrice.ToString("F2"), salesmodel.Childs);
                    
                    if (tourInfo != null)
                    {
                        if (tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 
                            || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队 
                            || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队)
                        {
                            //对外报价 整团 分项
                            EyouSoft.Model.TourStructure.MTourTeamInfo TeamInfo = (EyouSoft.Model.TourStructure.MTourTeamInfo)tourInfo;
                            if (TeamInfo != null)
                            {
                                if (TeamInfo.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                                {
                                    //分项报价
                                    if (TeamInfo.TourTeamPrice != null && TeamInfo.TourTeamPrice.Count > 0)
                                    {
                                        this.repForeignQuotelist.DataSource = TeamInfo.TourTeamPrice;
                                        this.repForeignQuotelist.DataBind();
                                    }
                                    this.TourQuoteView1.Visible = true;
                                }
                                else
                                {
                                    this.litServerStandard.Text = TeamInfo.TourService != null ? TeamInfo.TourService.ServiceStandard : "";
                                    this.TourQuoteView.Visible = true;
                                }
                            }
                            
                            _jiaGeHtml += string.Format(" + 其它费用：<b>{0}</b> ", salesmodel.OtherCost.ToString("F2"));
                        }

                        if (tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼
                            || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼
                            || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼)
                        {
                            
                        }
                    }

                    ltrJiaGe.Text = _jiaGeHtml;

                    if (!string.IsNullOrEmpty(salesmodel.DingDanBeiZhu))
                    {
                        ltrDingDanBeiZhu.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(salesmodel.DingDanBeiZhu);
                    }

                    if (!string.IsNullOrEmpty(salesmodel.NeiBuXinXi))
                    {
                        ltrNeiBuXinXi.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(salesmodel.NeiBuXinXi);
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected string PageSave()
        {
            string setMsg = string.Empty;
            //变更增加费用 备注
            decimal addMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtChangeAddMoney.UniqueID));
            string addMoneyRemark = Utils.GetFormValue(this.txtChangeRemark.UniqueID);
            //变更减少费用 备注
            decimal lessenMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtChangelessonMoney.UniqueID));
            string lessenRemark = Utils.GetFormValue(this.txtChangeRemarks.UniqueID);
            //确认金额
            decimal comfirmMoney = Utils.GetDecimal(Utils.GetFormValue(this.hidComfirmMoney.UniqueID));
            //金额变更说明
            string changeEsplain = Utils.GetFormValue(this.txtchangeEsplain.UniqueID);
            //已支付金额 尚欠款金额
            decimal payMoney = Utils.GetDecimal(Utils.GetFormValue(this.hidPayMoney.UniqueID));
            decimal bedtMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtDebtMoney.UniqueID));            
            //财务信息 
            string AccountId = Utils.GetFormValue("txt_AccountId");
            //结算人
            string settlementName = Utils.GetFormValue(this.txtSettlementName.UniqueID);
            EyouSoft.Model.TourStructure.MOrderSaleBase saleBase = new EyouSoft.Model.TourStructure.MOrderSaleBase();
            saleBase.CheckMoney = payMoney;
            saleBase.CompanyId = this.SiteUserInfo.CompanyId;
            saleBase.ConfirmMoney = comfirmMoney;
            saleBase.ConfirmMoneyStatus = Utils.GetQueryStringValue("confirm") == "1";
            saleBase.ConfirmPeople = this.SiteUserInfo.Name;
            saleBase.ConfirmPeopleId = this.SiteUserInfo.UserId;
            saleBase.ConfirmRemark = changeEsplain;
            saleBase.DeptId = this.SiteUserInfo.DeptId;
            saleBase.OrderId = Utils.GetQueryStringValue("OrderId");
            saleBase.PayMentAccountId = Utils.GetInt(AccountId);
            //增加 减少的费用 备注
            saleBase.SumPriceAddCost = addMoney;
            saleBase.SumPriceAddCostRemark = addMoneyRemark;
            saleBase.SumPriceReduceCost = lessenMoney;
            saleBase.SumPriceReduceCostRemark = lessenRemark;
            saleBase.TourId = Utils.GetQueryStringValue("tourId");
            saleBase.SumPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidAccountPrices.UniqueID));

            EyouSoft.Model.TourStructure.MTourOrderChange orderChange = new EyouSoft.Model.TourStructure.MTourOrderChange();
            orderChange.TourId = Utils.GetQueryStringValue("tourId");
            orderChange.OrderId = Utils.GetQueryStringValue("OrderId");
            orderChange.CompanyId = this.SiteUserInfo.CompanyId;

            bool result = new EyouSoft.BLL.TourStructure.BTourOrder().UpdateOrderSettlement(saleBase, orderChange);
            if (result)
            {
                setMsg = UtilsCommons.AjaxReturnJson("1", "合同金额确认成功！");
            }
            else
            {
                setMsg = UtilsCommons.AjaxReturnJson("0", "合同金额确认失败！");
            }
            return setMsg;
        }
        #endregion        

        #region 指定页面类型
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion

        /// <summary>
        /// 取消确认合同金额
        /// </summary>
        void QuXiaoQueRenHeTongJinE()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_取消确认合同金额))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));
            }

            string orderId = Utils.GetQueryStringValue("orderid");

            int bllRetCode = new EyouSoft.BLL.TourStructure.BTourOrder().QuXiaoQueRenHeTongJinE(CurrentUserCompanyID, SiteUserInfo.UserId, orderId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "取消确认合同金额成功！"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "未确认合同金额或订单信息不存在！"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "计划已核算结束，不能取消确认合同金额！"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }
    }
}
