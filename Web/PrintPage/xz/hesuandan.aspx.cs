using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 地接社通知单
    /// 方琪 2012-05-17
    /// </summary>
    public partial class hesuandan : System.Web.UI.Page
    {
        protected string ProviderToMoney = "zh-cn";

        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (string.IsNullOrEmpty(tourId)) Utils.RCWE("异常请求");

            InitPage(tourId);
            
            //referertype 请求来源            
            //referertype=1 导游报账、销售报账、计调报账
            //referertype=2 单项业务修改页面
            //referertype=3 计调终审
            //referertype=4 财务报账
            //referertype=default 单团核算
            //referertype=5 统计分析-状态查询表-单项业务
            string referertype = Utils.GetQueryStringValue("referertype");

            if (referertype == "1" || referertype == "3" || referertype == "4")
            {
                ltrDanJuTitle.Text = Title = "报账单";
            }

            InitDaiShous();
        }

        protected void InitPage(string tourId)
        {
            //核算单
            MTourBaseInfo model = new BTour().GetTourInfo(tourId);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
                this.lbLDate.Text = model.LDate.HasValue ? model.LDate.Value.ToString("yyyy-MM-dd") : "";
                this.lbTourDays.Text = model.TourDays.ToString();
                this.lbPersonNum.Text = string.Format("<b class=fontblue>{0}</b><sup class=fontred>+{1}</sup>", model.Adults, model.Childs);
                this.lbSeller.Text = model.SaleInfo.Name;
                if (model.GuideList != null && model.GuideList.Count > 0)
                {
                    this.lbGuid.Text = UtilsCommons.PingGuide(model.GuideList);
                }
                if (model.TourPlaner != null && model.TourPlaner.Count > 0)
                {
                    this.lbTourPlaner.Text = UtilsCommons.PingPlaner(model.TourPlaner);
                }
            }
            //团款收入
            EyouSoft.Model.TourStructure.MOrderSum orders = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> tourOrder = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orders, tourId);
            if (tourOrder != null && tourOrder.Count > 0)
            {
                this.rpt_tuankuan.DataSource = tourOrder;
                this.rpt_tuankuan.DataBind();
                this.lbConfirmMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmMoney, ProviderToMoney);
                this.lbSettlementMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmSettlementMoney, ProviderToMoney);
                this.lbGuideIncomeCount.Text = UtilsCommons.GetMoneyString(orders.GuideRealIncome, ProviderToMoney);
                this.lbCheckMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmMoney - orders.GuideRealIncome, ProviderToMoney);
                this.lbProfitCount.Text = UtilsCommons.GetMoneyString(orders.Profit, ProviderToMoney);
            }
            else
            {
                this.ph_tuankuan.Visible = false;
            }
            //其他收入
            IList<MOtherFeeInOut> otherList = new EyouSoft.BLL.PlanStructure.BPlan().GetOtherIncome(tourId);
            if (otherList != null && otherList.Count > 0)
            {
                this.rpt_qita.DataSource = otherList;
                this.rpt_qita.DataBind();
            }
            else
            {
                this.ph_qita.Visible = false;
            }
            //团队支出
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> payList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(tourId);
            if (payList != null && payList.Count > 0)
            {
                this.rpt_zhichu.DataSource = payList;
                this.rpt_zhichu.DataBind();
                int Count = 0;
                decimal TotalMoney = 0;
                foreach (var item in payList)
                {
                    Count += item.Num;
                    TotalMoney += item.Confirmation;
                }
                //this.lbNumCount.Text = Count.ToString();
                this.lbSettlementMoney.Text = UtilsCommons.GetMoneyString(TotalMoney, ProviderToMoney);
            }
            else
            {
                this.ph_zhichu.Visible = false;
            }
            //利润分配
            IList<MProfitDistribute> profitList = new EyouSoft.BLL.FinStructure.BFinance().GetProfitDistribute(tourId);
            if (profitList != null && profitList.Count > 0)
            {
                this.rpt_lirun.DataSource = profitList;
                this.rpt_lirun.DataBind();
            }
            else
            {
                this.ph_lirun.Visible = false;
            }
            //报帐汇总
            MBZHZ BZmodel = new EyouSoft.BLL.PlanStructure.BPlan().GetBZHZ(tourId);
            if (model != null)
            {
                this.lb_guidesIncome.Text = UtilsCommons.GetMoneyString(BZmodel.GuideIncome, ProviderToMoney);
                this.lb_guidesBorrower.Text = UtilsCommons.GetMoneyString(BZmodel.GuideBorrow, ProviderToMoney);
                this.lb_guidesSpending.Text = UtilsCommons.GetMoneyString(BZmodel.GuideOutlay, ProviderToMoney);
                this.lb_replacementOrReturn.Text = UtilsCommons.GetMoneyString(BZmodel.GuideMoneyRtn, ProviderToMoney);
                this.lb_RCSN.Text = BZmodel.GuideRelSign.ToString();
                this.lb_HUSN.Text = BZmodel.GuideUsed.ToString();
                this.lb_RSN.Text = BZmodel.GuideSignRtn.ToString();
            }
            //团队汇总信息
            EyouSoft.Model.PlanStructure.MTourTotalInOut tourModel = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(tourId);
            /*团队收入*/
            this.lb_tourMoneyIn.Text = UtilsCommons.GetMoneyString(tourModel.TourIncome + tourModel.QiTaShouRu, ProviderToMoney);
            /*团队支出*/
            this.lb_tourMoneyOut.Text = UtilsCommons.GetMoneyString(tourModel.TourOutlay, ProviderToMoney);
            /*团队利润*/
            this.lb_tourMoney.Text = UtilsCommons.GetMoneyString(tourModel.TourProfit, ProviderToMoney);
            /*团队利润率*/
            this.lb_tourMoneyRate.Text = tourModel.TourProRate.ToString("F2") + "%";

        }

        /// <summary>
        /// 获取支出数量
        /// </summary>
        /// <param name="anPaiLeiXing">安排类型</param>
        /// <param name="num">人数（int）</param>
        /// <param name="dnum">人数（decimal）</param>
        /// <returns></returns>
        protected string GetZhiChuShuLiang(object anPaiLeiXing, object num, object dnum)
        {
            if (anPaiLeiXing == null || num == null || dnum == null) return "0";
            var _anPaiLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)anPaiLeiXing;

            if (_anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮 || _anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮)
            {
                return ((decimal)dnum).ToString("F2");
            }

            return num.ToString();
        }

        /// <summary>
        /// 初始化代收信息
        /// </summary>
        void InitDaiShous()
        {
            var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();
            if (domain == null) return;

            if (!new EyouSoft.BLL.SysStructure.BSys().IsExistsMenu2(domain.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_代收管理))
            {
                ph_daishou.Visible = false;
                return;
            }

            var items = new EyouSoft.BLL.FinStructure.BDaiShou().GetDaiShous(Utils.GetQueryStringValue("tourid"));
            if (items != null && items.Count > 0)
            {
                rptDaiShou.DataSource = items;
                rptDaiShou.DataBind();
            }
            else
            {
                phEmptyDaiShou.Visible = true;
                ph_daishou.Visible = false;
            }
        }
    }
}
