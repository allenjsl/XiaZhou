using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 团队结算单
    /// 方琪 2012-05-15
    /// </summary>
    public partial class tuanduijiesuandan : System.Web.UI.Page
    {
        #region attributes
        protected int Count1 = 0;
        protected int Count2 = 0;
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        /// <summary>
        /// 订单编号
        /// </summary>
        string OrderId = string.Empty;
        /// <summary>
        /// 团队类型
        /// </summary>
        EyouSoft.Model.EnumType.TourStructure.TourType? TourType;
        /// <summary>
        /// 团队编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 是否是游客确认单
        /// </summary>
        bool IsYouKeQueRenDan = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            OrderId = Utils.GetQueryStringValue("OrderId");
            TourType = (EyouSoft.Model.EnumType.TourStructure.TourType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.TourStructure.TourType), Utils.GetQueryStringValue("tourType"));
            IsYouKeQueRenDan = Utils.GetQueryStringValue("ykxc") == "1";

            if (string.IsNullOrEmpty(OrderId) || !TourType.HasValue) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "错误的请求。"));
            if (!_IsLogin) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "错误的请求：当前未登录。"));

            InitPage();
            
            if (IsYouKeQueRenDan)
            {
                InitYouKe();
                InitXingCheng();
            }
        }

        #region private members
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            EyouSoft.Model.TourStructure.MOrderSale model = new EyouSoft.BLL.TourStructure.BTourOrder().GetSettlementOrderByOrderId(OrderId, TourType.Value);
            if (model == null) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "错误的请求：未找到相关数据。"));

            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                var _url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);
                if (!string.IsNullOrEmpty(_url) && _url != "javascript:void(0)")
                {
                    Response.Redirect(_url + "?tourid=" + model.TourId);
                    Response.End();
                }
            }

            //名称/联系人
            this.txtCompanyName.Text = model.BuyCompanyName;
            this.txtCompanyContactName.Text = model.ContactName;
            this.txtContact.Text = model.ContactTel;
            this.txtFax.Text = model.ContactFax;
            //公司名、联系人
            this.txtSelfName.Text = this.SiteUserInfo.CompanyName;
            /*this.txtSelfContactName.Text = this.SiteUserInfo.Name;
            this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
            this.txtSelfFax.Text = this.SiteUserInfo.Fax;*/
            //线路名称
            this.ltr1.Text = (IsYouKeQueRenDan ? "" : "团队结算单&nbsp; &nbsp; ") + model.RouteName;
            //接团时间
            this.lbLDate.Text = model.LDate.HasValue ? model.LDate.Value.ToString("yyyy-MM-dd") : "";
            //人数
            this.lbPersonNum.Text = model.Adults + "&nbsp;+&nbsp;" + model.Childs;
            //整团
            if (!string.IsNullOrEmpty(model.ServiceStandard)) this.lbServiceStandard.Text = model.ServiceStandard;
            else this.ph_Standard.Visible = false;
            //分项
            if (model.TourTeamPriceList != null && model.TourTeamPriceList.Count > 0)
            {
                this.rpt_QuoteList.DataSource = model.TourTeamPriceList;
                this.rpt_QuoteList.DataBind();
                this.Count1 = model.TourTeamPriceList.Count;
            }
            else this.ph_Quote.Visible = false;

            //报价信息
            string _baoJiaHtml = string.Empty;
            if (model.BaoJiaBiaoZhunId > 0 && model.KeHuLevId > 0)
            {
                _baoJiaHtml += model.BaoJiaBiaoZhunName + "/";
                _baoJiaHtml += model.KeHuLevName;
            }
            if (!string.IsNullOrEmpty(_baoJiaHtml)) _baoJiaHtml += "&nbsp;&nbsp;";
            ltrBaoJia.Text = _baoJiaHtml;

            //成人单价
            this.lbAdultPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.AdultPrice, ProviderToMoney);
            //成人数
            this.lbAdultCount.Text = model.Adults.ToString();
            //儿童单价
            this.lbChildPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ChildPrice, ProviderToMoney);
            //儿童数
            this.lbChildCount.Text = model.Childs.ToString();
            //其他费用
            if (model.OtherCost > 0) this.lbOtherCost.Text = "+ 其它费用：<b>" + EyouSoft.Common.UtilsCommons.GetMoneyString(model.OtherCost, ProviderToMoney) + "</b>";

            //销售增加费用、备注
            if (model.SaleAddCost != 0 || !string.IsNullOrEmpty(model.SaleAddCostRemark))
            {
                phZengJiaFeiYong.Visible = true;
                this.lbSumPriceAddCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SaleAddCost, ProviderToMoney);
                this.lbSumPriceAddCostRemark.Text = model.SaleAddCostRemark;
            }
            //销售减少费用、备注
            if (model.SaleReduceCost != 0 || !string.IsNullOrEmpty(model.SaleReduceCostRemark))
            {
                phJianShaoFeiYong.Visible = true;
                this.lbSumPriceReduceCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SaleReduceCost, ProviderToMoney);
                this.lbSumPriceReduceCostRemark.Text = model.SaleReduceCostRemark;
            }

            //合计金额
            this.lbSumPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPrice, ProviderToMoney);            

            //退款信息
            if (model.TourOrderSalesList != null && model.TourOrderSalesList.Count > 0)
            {
                rpt_BackList.DataSource = model.TourOrderSalesList;
                rpt_BackList.DataBind();
                Count2 = model.TourOrderSalesList.Count;
            }
            else ph_BackList.Visible = false;

            //变更增加、备注
            if (model.SumPriceAddCost != 0 || !string.IsNullOrEmpty(model.SumPriceAddCostRemark))
            {
                phBianGengZengJiaFeiYong.Visible = true;
                this.lbChangeAddMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPriceAddCost, ProviderToMoney);
                this.lbChangeRemark.Text = model.SumPriceAddCostRemark;
            }

            //变更减少、备注
            if (model.SumPriceReduceCost != 0 || !string.IsNullOrEmpty(model.SumPriceReduceCostRemark))
            {
                phBianGengJianShaoFeiYong.Visible = true;
                this.lbChangelessonMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPriceReduceCost, ProviderToMoney);
                this.lbChangeRemarks.Text = model.SumPriceReduceCostRemark;
            }

            //合同确认金额信息
            if (model.ConfirmMoneyStatus)
            {
                phQueRenJinE.Visible = true;

                //订单确认金额
                this.lbComfirmMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ConfirmMoney, ProviderToMoney);

                //已支付金额
                this.lbPayMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.CheckMoney, ProviderToMoney);
                //尚欠金额
                //decimal sumPrices = model.SumPrice;
                //decimal addPrices = model.SumPriceAddCost;
                //decimal lessPrices = model.SumPriceReduceCost;
                //decimal payMoney = model.CheckMoney;
                //decimal countPrices = sumPrices + addPrices - lessPrices;
                //decimal debtMoney = countPrices - payMoney;
                //this.lbDebtMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(debtMoney, ProviderToMoney);
                lbDebtMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ConfirmMoney - model.CheckMoney, ProviderToMoney);
                //还款年 月 日
                //this.txtYear.Text = DateTime.Now.Year.ToString();
                //this.txtMonth.Text = model.PayMentMonth;
                //this.txtDate.Text = model.PayMentDay;

                //InitYinHangZhangHu();
            }
            else
            {
                if (phBianGengZengJiaFeiYong.Visible || phBianGengJianShaoFeiYong.Visible)
                {
                    phQueRenJinE.Visible = true;
                    lbComfirmMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ConfirmMoney, ProviderToMoney);
                    lbPayMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.CheckMoney, ProviderToMoney);
                    lbDebtMoney.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ConfirmMoney - model.CheckMoney, ProviderToMoney);
                }
            }

            this.lbDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            TourId = model.TourId;

            ltr2.Text = "订单号：" + model.OrderCode;

            if (model != null && !string.IsNullOrEmpty(model.SellerId))
            {
                var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(model.SellerId, SiteUserInfo.CompanyId);
                if (xiaoShouYuanInfo != null)
                {
                    this.txtSelfContactName.Text = xiaoShouYuanInfo.ContactName;
                    this.txtSelfContact.Text = xiaoShouYuanInfo.ContactTel;
                    this.txtSelfFax.Text = xiaoShouYuanInfo.ContactFax;
                }
            }

            if (!string.IsNullOrEmpty(model.ConfirmRemark))
            {
                phJinEBianGengShuoMing.Visible = true;
                ltrJinEBianGengShuoMing.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ConfirmRemark);
            }

            this.txtYear.Text = DateTime.Now.Year.ToString();
            InitYinHangZhangHu();
        }

        /// <summary>
        /// 初始化行程信息（行程内容，行程特色）
        /// </summary>
        void InitXingCheng()
        {
            this.Title = "游客确认单";
            ltr3.Visible = false;

            if (string.IsNullOrEmpty(TourId)) return;

            var info = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(TourId);
            if (info == null) return;

            //行程特色
            if (!string.IsNullOrEmpty(info.PlanFeature))
            {
                phXingChengTeSe.Visible = true;
                ltrXingChengTeSe.Text = info.PlanFeature;
            }

            if (info.TourPlan != null && info.TourPlan.Count > 0) InitXingChengNeiRong(info.TourPlan);
            InitFuWuBiaoZhun(info);
        }

        /// <summary>
        /// 初始化行程内容
        /// </summary>
        /// <param name="items">行程信息集合</param>
        void InitXingChengNeiRong(IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> items)
        {
            phXingCheng.Visible = true;
            items = items.OrderBy(m => m.Days).ToList();

            StringBuilder s = new StringBuilder();

            foreach (EyouSoft.Model.TourStructure.MPlanBaseInfo item in items)
            {
                string yongCan = string.Empty;
                if (item.Breakfast) { yongCan += "早、"; }
                if (item.Lunch) { yongCan += "中、"; }
                if (item.Supper) { yongCan += "晚、"; }
                yongCan = yongCan.TrimEnd('、');

                s.AppendFormat("<table i_sh='table_chk_xcap' width='696' border='0' align='center' cellpadding='0' cellspacing='0' class='borderline_2'><tr><td width='35%' class='small_title'><b class='font16'>第{0}天  {6}</b></td><td width='15%' class='small_title'><b class='font14'>交通：{1}</b></td><td width='20%' class='small_title'><b class='font14'>餐：{2}</b></td><td width='30%' class='small_title'><b class='font14'>住宿：{3}</b></td></tr></table><table i_sh='table_chk_xcap' width='696' border='0' align='center' cellpadding='0' cellspacing='0' style='margin-top:0px' class='list_2'><tr><td class='td_text' style='border-top:none;' width='{7}'>{4}</td>{5}</tr></table>"
                    , item.Days.ToString()
                    , item.Traffic
                    , yongCan
                    , item.Hotel
                    , item.Content
                    , string.IsNullOrEmpty(item.FilePath) ? "" : "<td style='border-top:none;'><img src='http://" + Request.Url.Authority + item.FilePath + "' width='202' height='163' /></td>"
                    , item.Section
                    , string.IsNullOrEmpty(item.FilePath) ? "100%" : "480px");
            }

            ltrXingCheng.Text = s.ToString();
        }

        /// <summary>
        /// 初始化服务标准
        /// </summary>
        /// <param name="info"></param>
        void InitFuWuBiaoZhun(EyouSoft.Model.TourStructure.MTourBaseInfo info)
        {
            TPlanService.Visible = true;

            if (info == null || info.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务 || info.TourService == null)
            {
                TPlanService.Visible = false;
                return;
            }

            #region 服务标准
            if (info.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 
                || info.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 
                || info.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队)
            {
                var info1 = (EyouSoft.Model.TourStructure.MTourTeamInfo)info;
                if (info1.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                {
                    divTuanDuiFenXiangBaoJia.Visible = false;

                    if (string.IsNullOrEmpty(info.TourService.ServiceStandard))
                    {
                        this.TService.Visible = false;
                    }
                    else
                    {
                        this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.ServiceStandard);
                    }
                }
                else
                {
                    TService.Visible = false;
                    StringBuilder s = new StringBuilder();
                    foreach (var item in info1.TourTeamPrice)
                    {
                        if (!string.IsNullOrEmpty(item.ServiceType.ToString()))
                        {
                            s.AppendFormat("<tr><td width='9%' align='left'><b class='font14'>{0}</b></td><td align='left'>{1}</td><td align='center' width='13%'>{2}</td>", item.ServiceType.ToString(), EyouSoft.Common.Function.StringValidate.TextToHtml(item.ServiceStandard), EyouSoft.Common.UtilsCommons.GetMoneyString(item.Quote, ProviderToMoney) + "/" + item.Unit.ToString());
                        }
                    }
                    this.lboptionservice.Text = s.ToString();
                }
            }
            else
            {
                divTuanDuiFenXiangBaoJia.Visible = false;

                if (string.IsNullOrEmpty(info.TourService.ServiceStandard))
                {
                    this.TService.Visible = false;
                }
                else
                {
                    this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.ServiceStandard);
                }
            }
            #endregion

            #region 服务不含
            if (string.IsNullOrEmpty(info.TourService.NoNeedItem))
            {
                this.TNoService.Visible = false;
            }
            else
            {
                this.lbnoService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.NoNeedItem);
            }
            #endregion

            #region 购物安排
            if (string.IsNullOrEmpty(info.TourService.ShoppingItem))
            {
                this.TShopping.Visible = false;
            }
            else
            {
                this.lbshopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.ShoppingItem);
            }
            #endregion

            #region 儿童安排
            if (string.IsNullOrEmpty(info.TourService.ChildServiceItem))
            {
                this.TChildren.Visible = false;
            }
            else
            {
                this.lbchildren.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.ChildServiceItem);
            }
            #endregion

            #region 自费项目
            if (string.IsNullOrEmpty(info.TourService.OwnExpense))
            {
                this.TSelfProject.Visible = false;
            }
            else
            {
                this.lbselfproject.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.OwnExpense);
            }
            #endregion

            #region 温馨提醒
            if (string.IsNullOrEmpty(info.TourService.WarmRemind))
            {
                this.TWarmRemind.Visible = false;
            }
            else
            {
                this.lbwarmremind.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.WarmRemind);
            }
            #endregion

            #region 注意事项
            if (string.IsNullOrEmpty(info.TourService.NeedAttention))
            {
                this.TNeedAttention.Visible = false;
            }
            else
            {
                this.lbneedattention.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(info.TourService.NeedAttention);
            }
            #endregion
        }

        /// <summary>
        /// 初始化游客信息
        /// </summary>
        void InitYouKe()
        {
            var items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderBuyCompanyTravellerByOrderId(OrderId);

            if (items != null && items.Count > 0)
            {
                phYouKe.Visible = true;
                rptsYouKe.DataSource = items;
                rptsYouKe.DataBind();
            }
        }

        /// <summary>
        /// 初始化银行账户信息
        /// </summary>
        void InitYinHangZhangHu()
        {
            phYinHangZhangHao.Visible = true;
            var items = new EyouSoft.BLL.ComStructure.BComAccount().GetList(this.SiteUserInfo.CompanyId);

            if (items != null && items.Count > 0)
            {
                rptYinHangZhangHu.DataSource = items;
                rptYinHangZhangHu.DataBind();
            }

            items = null;
        }
        #endregion
    }
}
