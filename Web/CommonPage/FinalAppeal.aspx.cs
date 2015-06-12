using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Common;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.EnumType.KingDee;

namespace Web.CommonPage
{
    

    /// <summary>
    /// 计调：计调终审-计调终审
    /// 财务：单团核算-核算
    /// 财务：报销报账-审批
    /// 公共页面
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2012-3-16
    /// 页面备注：
    /// 该页面的利润分配添加修改页面在/FinanceManage/CommonAddDistributeProfit.aspx
    /// 必传参数
    /// tourId=团队编号
    /// source : 页面来源
    /// source = 1  计调终审-计调终审
    /// source = 2  单团核算-核算
    /// source = 3  报销报账-审批
    public partial class FinalAppeal : BackPage
    {
        #region attributes
        /// <summary>
        /// 毛利 = 团款收入结算金额 - 团队支出结算金额
        /// </summary>
        protected string Price = "0";
        /// <summary>
        /// 团款收入结算金额
        /// </summary>
        private decimal confirmSettlementMoney = 0;
        /// <summary>
        /// 团队支出结算金额
        /// </summary>
        private decimal tourMoneyOutSumNum = 0;
        /// <summary>
        /// 团号
        /// </summary>
        protected string TourCode = string.Empty;
        /// <summary>
        /// 是否已核算标识（1：已核算）
        /// </summary>
        protected string flag = string.Empty;
        /// <summary>
        /// 代收栏目权限
        /// </summary>
        bool Privs_DaiShouLanMu = false;
        /// <summary>
        /// 代收审批权限
        /// </summary>
        bool Privs_DaiShouShenPi = false;
        /// <summary>
        /// 团队结算单路径
        /// </summary>
        protected string PringPageJSD = string.Empty;
        /// <summary>
        /// 核算单链接
        /// </summary>
        protected string PrintPageHSD = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            flag = Utils.GetQueryStringValue("flag");
            switch (Utils.GetQueryStringValue("type"))
            {
                case "ReturnLastInstace":
                    ReturnLastInstace();
                    break;
                case "AppealEnd":
                    AppealEnd();
                    break;
                case "Del":
                    Del();
                    break;

            }
            DataInit();
            InitDaiShous();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //团队编号
            string tourId = Utils.GetQueryStringValue("tourId");
            PringPageJSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
            PrintPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            PrintPageHSD += "?referertype=4&tourid=" + tourId;

            //权限控制
            PowerControl(tourId);

            //计调BLL
            BPlan bpBLL = new BPlan();

            //初始化团队基本信息
            TourModelInit(tourId);

            //团款收入
            TourOrderInit(tourId);

            //初始化其他收入列表
            OtherMoneyIn(bpBLL, tourId);

            //初始化团队支出列表
            TourMoneyOut(bpBLL, tourId);
            //计算毛利
            Price = Utils.FilterEndOfTheZeroDecimal(confirmSettlementMoney - tourMoneyOutSumNum);
            //报账总汇
            TourSummarizing(bpBLL, tourId);

            //团队收支总汇
            TourInOutSum();
        }

        #region 团队基本数据
        /// <summary>
        /// 初始化团队实体数据
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void TourModelInit(string tourId)
        {
            MTourBaseInfo model = new BTour().GetTourInfo(tourId);
            if (model != null)
            {
                TourCode = model.TourCode;
                //线路名称
                lbl_routeName.Text = model.RouteName;
                //出团时间
                lbl_lDate.Text = UtilsCommons.GetDateString(model.LDate, ProviderToDate);
                //团号
                lbl_tourCode.Text = model.TourCode;
                //团队天数
                lbl_tourDays.Text = model.TourDays.ToString();
                //人数
                lbl_number.Text = string.Format("<b class=fontblue>{0}</b><sup class=fontred>+{1}</sup>", model.Adults, model.Childs);
                //销售
                lbl_saleInfoName.Text = model.SaleInfo.Name;
                //计调
                lbl_tourPlaner.Text = UtilsCommons.PingPlaner(model.TourPlaner);
                //导游
                lbl_mGuidInfoName.Text = UtilsCommons.PingGuide(model.GuideList);
            }
        }


        #endregion

        #region 团款收入
        /// <summary>
        /// 团款收入
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void TourOrderInit(string tourId)
        {
            MOrderSum sumModel = new MOrderSum();
            IList<MTourOrder> sl = new BTourOrder().GetTourOrderListById(ref sumModel, tourId);
            if (sl != null && sl.Count > 0)
            {

                rpt_tourMoneyIn.DataSource = sl;
                rpt_tourMoneyIn.DataBind();
                //合同金额
                lbl_sumPrice.Text = UtilsCommons.GetMoneyString(TourIncome.Value = sumModel.ConfirmMoney.ToString(), ProviderToMoney);
                //团队收入
                lbl_tourMoneyIn.Text = (sumModel.ConfirmSettlementMoney).ToString();
                //结算金额
                lbl_confirmSettlementMoney.Text = UtilsCommons.GetMoneyString(TourSettlement.Value = (confirmSettlementMoney = sumModel.ConfirmSettlementMoney).ToString(), ProviderToMoney);
                //导游实收
                lbl_guideRealIncome.Text = UtilsCommons.GetMoneyString(sumModel.GuideRealIncome, ProviderToMoney);
                //财务实收
                lbl_checkMoney.Text = UtilsCommons.GetMoneyString(sumModel.ConfirmMoney - sumModel.GuideRealIncome, ProviderToMoney);
                //订单利润
                lbl_profit.Text = UtilsCommons.GetMoneyString(sumModel.Profit, ProviderToMoney);
                //已收金额
                ltrYiShouJinEHeJi.Text = UtilsCommons.GetMoneyString(sumModel.CheckMoney, ProviderToMoney);
                //待审金额
                ltrDaiShenJinE.Text = UtilsCommons.GetMoneyString(sumModel.DengJiJinE - sumModel.CheckMoney, ProviderToMoney);
                //未收金额
                ltrWeiShouJinE.Text= UtilsCommons.GetMoneyString(sumModel.ConfirmMoney - sumModel.CheckMoney, ProviderToMoney);
            }
            pan_tourMoneyInMsg.Visible = !(pan_tourMoneyInSum.Visible = sl != null && sl.Count > 0);//
        }
        #endregion

        #region 其他收入
        /// <summary>
        /// 其他收入(这个"其他"不是计调项的其他,是财务的杂费收入)
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        /// <param name="tourId">团队编号</param>
        private void OtherMoneyIn(BPlan BLL, string tourId)
        {

            IList<MOtherFeeInOut> ls = BLL.GetOtherIncome(tourId);

            if (ls != null && ls.Count > 0)
            {
                rpt_restsMoneyIn.DataSource = ls;
                rpt_restsMoneyIn.DataBind();
                TourOtherIncome.Value = ls.Sum(item => item.FeeAmount).ToString();
            }
            lbl_restsMoneyInMsg.Visible = !(ls != null && ls.Count > 0);

        }
        #endregion

        #region 团队支出
        /// <summary>
        /// 团队支出
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        /// <param name="tourId">团队编号</param>
        private void TourMoneyOut(BPlan BLL, string tourId)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> ls = BLL.GetList(tourId);
            if (ls != null && ls.Count > 0)
            {
                rpt_tourMoneyOut.DataSource = ls.Where(p => p.Type != PlanProject.购物);
                rpt_tourMoneyOut.DataBind();
                //lbl_tourMoneyOutSumNum.Text = ls.Sum(item => item.Num).ToString();
                lbl_tourMoneyOutSumConfirmation.Text = UtilsCommons.GetMoneyString((lbl_tourMoneyOut.Text = TourPay.Value = (tourMoneyOutSumNum = ls.Sum(item => item.Confirmation)).ToString()), ProviderToMoney);
                decimal yingFuJinE = ls.Sum(item => item.Confirmation);
                decimal yiFuJinE = ls.Sum(item => item.Prepaid);
                ltrYiFuJinE.Text = UtilsCommons.GetMoneyString(yiFuJinE, ProviderToMoney);
                ltrWeiFuJinE.Text = UtilsCommons.GetMoneyString(yingFuJinE - yiFuJinE, ProviderToMoney);
            }
            pan_tourMoneyOutMsg.Visible = !(pan_tourMoneyOut.Visible = ls != null && ls.Count > 0);
        }
        #endregion

        #region 利润分配
        /// <summary>
        /// 利润分配
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void MoneyDistribute(string tourId)
        {
            IList<EyouSoft.Model.FinStructure.MProfitDistribute> ls = new EyouSoft.BLL.FinStructure.BFinance().GetProfitDistribute(tourId);
            if (ls != null && ls.Count > 0)
            {
                pan_mongyAllotMsg.Visible = false;
                rpt_mongyAllot.DataSource = ls;
                rpt_mongyAllot.DataBind();
                //团队订单分配利润
                ls = ls.Where(item => item.OrderId != null && item.OrderId.Length > 0).ToList();
                if (ls != null && ls.Count > 0)
                {
                    DisOrderProfit.Value = ls.Sum(item => item.Amount).ToString();
                }
                ls = ls.Where(item => item.OrderId == null || item.OrderId.Length <= 0).ToList();
                if (ls != null && ls.Count > 0)
                {
                    DisTourProfit.Value = ls.Sum(item => item.Amount).ToString();
                }
            }

        }
        #endregion

        #region 报账汇总
        /// <summary>
        /// 报账汇总
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        /// <param name="tourId">团号</param>
        private void TourSummarizing(BPlan BLL, string tourId)
        {
            MBZHZ model = BLL.GetBZHZ(tourId);
            if (model != null)
            {
                lbl_guidesIncome.Text = UtilsCommons.GetMoneyString(model.GuideIncome, ProviderToMoney);
                lbl_guidesBorrower.Text = UtilsCommons.GetMoneyString(model.GuideBorrow, ProviderToMoney);
                lbl_guidesSpending.Text = UtilsCommons.GetMoneyString(model.GuideOutlay, ProviderToMoney);
                lbl_replacementOrReturn.Text = UtilsCommons.GetMoneyString(model.GuideMoneyRtn, ProviderToMoney);
                lbl_RCSN.Text = model.GuideRelSign.ToString();
                lbl_HUSN.Text = model.GuideUsed.ToString();
                lbl_RSN.Text = model.GuideSignRtn.ToString();
            }
        }
        #endregion

        #region 团队收支总汇
        /// <summary>
        /// 团队收支总汇
        /// </summary>
        private void TourInOutSum()
        {
            /*//团队利润=团队收入-团队支出
            lbl_tourMoney.Text = TourProfit.Value = (Utils.GetDecimal(lbl_tourMoneyIn.Text) - Utils.GetDecimal(lbl_tourMoneyOut.Text)).ToString();
            //利润率 = 团队利润/团队收入(不要把/100.00改成100 原因请参考.net基础之 加减乘除)
            lbl_tourMoneyRate.Text = Utils.GetDecimal(lbl_tourMoneyIn.Text) != 0 ? (((int)(Utils.GetDecimal(lbl_tourMoney.Text) / Utils.GetDecimal(lbl_tourMoneyIn.Text) * 10000)) / 100.00).ToString() + "%" : "0%";
            //格式化团队利润
            lbl_tourMoney.Text = UtilsCommons.GetMoneyString(lbl_tourMoney.Text, ProviderToMoney);
            //格式化团队收入
            lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(lbl_tourMoneyIn.Text, ProviderToMoney);
            //格式化团队支出
            lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(lbl_tourMoneyOut.Text, ProviderToMoney);*/

            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(Utils.GetQueryStringValue("tourId"));
            this.lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(info.TourIncome + info.QiTaShouRu, ProviderToMoney);
            this.lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(info.TourOutlay, ProviderToMoney);
            this.lbl_tourMoney.Text = UtilsCommons.GetMoneyString(info.TourProfit, ProviderToMoney);
            this.lbl_tourMoneyRate.Text = info.TourProRate.ToString("F2") + "%";
        }
        #endregion

        #region Ajax

        /// <summary>
        /// 提交财务
        /// </summary>
        private void SubmitFinanceManage()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算, tourId);
        }
        /// <summary>
        /// 退回计调
        /// </summary>
        private void ReturnOperater()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审, tourId);
        }
        /// <summary>
        /// 核算结束
        /// </summary>
        private void AppealEnd()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.封团, tourId);
        }
        /// <summary>
        /// 退回终审
        /// </summary>
        private void ReturnLastInstace()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审, tourId);
        }
        /// <summary>
        /// 改变团队状态
        /// </summary>
        /// <param name="tourStatus">计划状态</param>
        /// <param name="tourId">团队编号</param>
        private void UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus, string tourId)
        {
            var info = new MTourStatusChange();
            info.TourId = tourId;/*团队编号*/
            info.CompanyId = CurrentUserCompanyID;/*系统公司编号*/
            info.DeptId = SiteUserInfo.DeptId;/*操作人部门Id*/
            info.Operator = SiteUserInfo.Name;/*操作人*/
            info.OperatorId = SiteUserInfo.UserId;/*操作人ID*/
            info.IssueTime = System.DateTime.Now;/*操作时间*/
            info.TourStatus = tourStatus;/*团队状态*/
            /*以下参数仅封团操作才有效*/
            info.TourSettlement = Utils.GetDecimal(Utils.GetQueryStringValue(TourSettlement.UniqueID));/*团队订单结算金额*/
            info.TourPay = Utils.GetDecimal(Utils.GetQueryStringValue(TourPay.UniqueID));/*团队总支出*/
            info.TourProfit = Utils.GetDecimal(Utils.GetQueryStringValue(TourProfit.UniqueID));/*团队利润*/
            info.DisOrderProfit = Utils.GetDecimal(Utils.GetQueryStringValue(DisOrderProfit.UniqueID));/*团队订单分配利润*/
            info.DisTourProfit = Utils.GetDecimal(Utils.GetQueryStringValue(DisTourProfit.UniqueID));/*团队分配利润*/
            info.TourIncome = Utils.GetDecimal(Utils.GetQueryStringValue(TourIncome.UniqueID));/*团队订单合同确认金额合计*/
            info.TourOtherIncome = Utils.GetDecimal(Utils.GetQueryStringValue(TourOtherIncome.UniqueID));/*团队其他收入*/

            bool retBool = new BTour().UpdateTourStatus(info);
            if (retBool)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1", "提交成功!"));
            }
            else
            {
                if (info.OutputCode == -98)
                {
                    RCWE(UtilsCommons.AjaxReturnJson("-1", "操作失败：有导游现收款，报销未完成的计划不能核算结束!"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "提交失败!"));
                }
            }
        }
        /// <summary>
        /// 删除利润分配
        /// </summary>
        private void Del()
        {
            AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().DelProfitDis(Utils.GetInt(Utils.GetQueryStringValue("id")), CurrentUserCompanyID) ? "1" : "-1", "删除失败!"));
        }
        #endregion

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl(string tourId)
        {
            BTour bll = new BTour();
            //团队实体
            MTourBaseInfo model = bll.GetTourInfo(tourId);

            if (model != null)
            {
                /* 根据页面来源判断 利润分配列表是否显示
                 * 计调终审-计调终审   无法查看利润分配 */
                int source = Utils.GetInt(Utils.GetQueryStringValue("source"));
                if (source > 1)
                {
                    //初始化利润分配
                    MoneyDistribute(tourId);
                    //利润分配添加修改权限控制
                    pan_AddMongyAllot.Visible = model.TourStatus == TourStatus.财务核算;
                    //利润分配栏目  财务进入
                    pan_moneyAllot.Visible = true;
                }

                /*
                 * 退回计调
                 * 待终审状态   And  不是财务进去
                 */
                pan_returnOperater.Visible =
                    CheckGrant(Privs.计调中心_计调终审_退回计调操作) &&
                    (model.TourStatus == TourStatus.待终审 && source <= 1);
                /*
                 * 提交财务
                 * 待终审状态   And  不是财务进去
                 */
                pan_submitFinance.Visible =
                    CheckGrant(Privs.计调中心_计调终审_提交财务操作) &&
                    (model.TourStatus == TourStatus.待终审 && source <= 1);

                /*
                * 退回终审
                * 财务核算状态 And   不是计调进入
                */
                pan_returnCourtFinal.Visible =
                     CheckGrant(Privs.财务管理_报销报账_核算结束) &&
                    (model.TourStatus == TourStatus.财务核算 && source > 1);
                //系统配置实体
                MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
                //封团
                pan_sealTour.Visible =
                      (source > 1) &&
                      CheckGrant(Privs.财务管理_报销报账_核算结束) &&
                      bll.GetCostStatus(tourId) &&
                      (model.IsSubmit || model.GuideList == null) &&
                      ((model.TourStatus == TourStatus.待终审 && comModel.SkipFinalJudgment) ||
                      (model.TourStatus == TourStatus.财务核算 && model.GuideList == null) ||
                      (model.TourStatus == TourStatus.财务核算 && model.GuideList != null && model.IsSubmit));
                //系统配置实体

                //如果是单团核算 财务入账
                if (source == 2)
                {
                    pan_InAccount.Visible = (CheckGrant(Privs.财务管理_单团核算_核算结束)) && comModel.IsEnableKis && model.TourStatus == TourStatus.封团;
                }

                //如果是报销报账 财务入账
                if (source == 3)
                {
                    pan_InAccount.Visible = (CheckGrant(Privs.财务管理_报销报账_核算结束)) && comModel.IsEnableKis && model.TourStatus == TourStatus.封团;
                }


            }
        }

        /// <summary>
        /// 获取财务入账
        /// </summary>
        /// <returns></returns>
        protected string GetFinIn()
        {
            return new BFinance().IsFinIn(DefaultProofType.单团核算, Utils.GetQueryStringValue("tourId"), this.SiteUserInfo.CompanyId)
                       ? "已入帐"
                       : "未入账";
        }

        /// <summary>
        /// 初始化代收信息
        /// </summary>
        void InitDaiShous()
        {
            if (!Privs_DaiShouLanMu)
            {
                phDaiShou.Visible = false;
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
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_DaiShouLanMu = new EyouSoft.BLL.SysStructure.BSys().IsExistsMenu2(SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_代收管理);
            Privs_DaiShouShenPi = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_代收管理_审批);
        }

        /// <summary>
        /// 获取代收登记操作列HTML
        /// </summary>
        /// <returns></returns>
        protected string GetDaiShouCaoZuoHtml(object status)
        {
            if (status == null) return string.Empty;
            var _status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)status;
            string s = string.Empty;

            if (_status == EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未审批 && Privs_DaiShouShenPi)
            {
                s = "<a href='javascript:void(0)' class='i_daishouchakan' i_shenpi='1'>审批</a>";
            }
            else
            {
                s = "<a href='javascript:void(0)' class='i_daishouchakan' i_shenpi='0'>查看</a>";
            }

            return s;
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
    }
}
