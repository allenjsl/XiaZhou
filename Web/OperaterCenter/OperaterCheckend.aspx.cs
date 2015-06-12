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
    /// 计调中心-计调终审
    /// 李晓欢 2012-04-09
    /// </summary>
    public partial class OperaterCheckend : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected decimal hidTourIncomCount = 0;
        protected decimal hidTourExpenceCount = 0;
        /// <summary>
        /// 代收栏目权限
        /// </summary>
        bool Privs_DaiShouLanMu = false;
        /// <summary>
        /// 核算单链接
        /// </summary>
        protected string PrintPageHSD = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 单项业务游客确认单
        /// </summary>
        string PrintPage_DanXiangYeWuYouKeQuRenDan = string.Empty;
        /// <summary>
        /// 计划类型
        /// </summary>
        EyouSoft.Model.EnumType.TourStructure.TourType? TourType = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourId");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitPrivs();
            PowerControl();
            
            string type = Utils.GetQueryStringValue("type");

            switch (type)
            {
                case "saveReturn": ReturnOperaterCheck(EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审); break;
                case "saveConfirm": ReturnOperaterCheck(EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算); break;
                default: break;
            }

            PrintPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            PrintPageHSD += "?referertype=3&tourid=" + TourId;
            PrintPage_DanXiangYeWuYouKeQuRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);

            InitTourInfo();
            DataInitOtherIncome();
            DataInitRenderAccount();
            DataInitTourExpenceIncome();
            DataInitTourPayIncome();
            IncomCount();                

            InitDaiShous();            
        }

        #region private members
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        void InitTourInfo()
        {
            EyouSoft.Model.TourStructure.MTourBaseInfo baseinfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(TourId);

            if (baseinfo == null) RCWE("异常请求");

            this.litRouteName.Text = baseinfo.RouteName;
            this.litStartTime.Text = UtilsCommons.GetDateString(baseinfo.LDate, ProviderToDate);
            this.litTourCode.Text = baseinfo.TourCode;
            this.litDays.Text = baseinfo.TourDays.ToString();
            this.litAdults.Text = baseinfo.Adults.ToString();
            this.litChilds.Text = baseinfo.Childs.ToString();
            if (baseinfo.SaleInfo != null)
            {
                this.litSellersName.Text = baseinfo.SaleInfo.Name;
            }
            this.litOperaterName.Text = UtilsCommons.PingPlaner(baseinfo.TourPlaner);
            this.litGuidName.Text = UtilsCommons.PingGuide(baseinfo.GuideList);

            TourType = baseinfo.TourType;

            if (baseinfo.TourStatus!= EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审)
            {
                this.panView.Visible = false;
            }
            else
            {
                this.panViewReturnOp.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_退回计调操作) && (baseinfo.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审);
                this.panViewSubmitFin.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_提交财务操作) && (baseinfo.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审);

            }
        }

        /// <summary>
        /// 团款收入
        /// </summary>
        protected void DataInitTourPayIncome()
        {
            EyouSoft.Model.TourStructure.MOrderSum sumOrder = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> tourlist = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(TourId, ref sumOrder);
            if (tourlist != null && tourlist.Count > 0)
            {
                var list = tourlist.Where(p => p.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交).ToList();
                if (list != null && list.Count > 0)
                {
                    this.repTourIncomList.DataSource = list;
                    this.repTourIncomList.DataBind();
                    this.litConfirmMoneyCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(list.Sum(p => p.ConfirmMoney), ProviderToMoney);
                    this.litConfirmSettlementMoneyCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(list.Sum(p => p.ConfirmSettlementMoney), ProviderToMoney);
                    this.litSalerIncomeCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(list.Sum(p => p.GuideRealIncome), ProviderToMoney);
                    decimal finIncome = list.Sum(p => p.ConfirmMoney) - list.Sum(p => p.GuideRealIncome);
                    this.litCheckMoneyCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(finIncome, ProviderToMoney);
                    this.litProfitCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(list.Sum(p => p.Profit), ProviderToMoney);
                    //团队收入结算金额汇总                    
                    this.hidTourIncomCount = list.Sum(p => p.ConfirmSettlementMoney);
                }
            }
        }

        /// <summary>
        /// 其它收入
        /// </summary>
        void DataInitOtherIncome()
        {
            IList<EyouSoft.Model.FinStructure.MOtherFeeInOut> otherFeeInOutlist = new EyouSoft.BLL.PlanStructure.BPlan().GetOtherIncome(TourId);
            if (otherFeeInOutlist != null && otherFeeInOutlist.Count > 0)
            {
                this.repOtherIncomList.DataSource = otherFeeInOutlist;
                this.repOtherIncomList.DataBind();
                //其它团队收入金额汇总
                hidTourIncomCount += otherFeeInOutlist.Sum(p => p.FeeAmount);
            }
        }
        /// <summary>
        /// 团队支出
        /// </summary>
        void DataInitTourExpenceIncome()
        {
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> baseInfolist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(TourId);
            if (baseInfolist != null && baseInfolist.Count > 0)
            {
                this.RepTourSeationExpList.DataSource = baseInfolist.Where(p => p.Type != EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物);
                this.RepTourSeationExpList.DataBind();
                this.litConfirmaCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(baseInfolist.Sum(p => p.Confirmation), ProviderToMoney);
                //团队支出结算金额汇总                    
                hidTourExpenceCount = baseInfolist.Sum(p => p.Confirmation);
            }
        }

        /// <summary>
        /// 报账汇总
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInitRenderAccount()
        {
            EyouSoft.Model.PlanStructure.MBZHZ renderAccount = new EyouSoft.BLL.PlanStructure.BPlan().GetBZHZ(TourId);
            if (renderAccount != null)
            {
                this.litGuideBorrowCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(renderAccount.GuideBorrow, ProviderToMoney);
                this.litGuideIncomeCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(renderAccount.GuideIncome, ProviderToMoney);
                this.litGuideMoneyRtnCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(renderAccount.GuideMoneyRtn, ProviderToMoney);
                this.litGuideOutlayCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(renderAccount.GuideOutlay, ProviderToMoney);
                this.litGuideRelSignCount.Text = renderAccount.GuideRelSign.ToString();
                this.litGuideSignRtnCount.Text = renderAccount.GuideSignRtn.ToString();
                this.litGuideUsedCount.Text = renderAccount.GuideUsed.ToString();
            }
        }

        /// <summary>
        /// 团队收支汇总
        /// </summary>
        void IncomCount()
        {
            /*this.litTourIncomCount.Text = UtilsCommons.GetMoneyString(hidTourIncomCount, ProviderToMoney);
            this.litTourExpenceCount.Text = UtilsCommons.GetMoneyString(hidTourExpenceCount, ProviderToMoney);
            //团队利润      
            this.litProfit1Count.Text = UtilsCommons.GetMoneyString((hidTourIncomCount - hidTourExpenceCount).ToString(), ProviderToMoney);
            if (hidTourIncomCount != 0)
            {
                //团队利润率
                this.litProfitLCount.Text = UtilsCommons.GetMoneyString(((hidTourIncomCount - hidTourExpenceCount) / hidTourIncomCount) * 100, ProviderToMoney);
            }
            else
            {
                this.litProfitLCount.Text = "0";
            }*/

            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(TourId);
            this.litTourIncomCount.Text = UtilsCommons.GetMoneyString(info.TourIncome + info.QiTaShouRu, ProviderToMoney);
            this.litTourExpenceCount.Text = UtilsCommons.GetMoneyString(info.TourOutlay, ProviderToMoney);
            this.litProfit1Count.Text = UtilsCommons.GetMoneyString(info.TourProfit, ProviderToMoney);
            this.litProfitLCount.Text = info.TourProRate.ToString("F2") + "%";
        }

        /// <summary>
        /// 退回计调 提交财务
        /// </summary>
        /// <returns></returns>
        void ReturnOperaterCheck(EyouSoft.Model.EnumType.TourStructure.TourStatus status)
        {
            string msg = string.Empty;
            EyouSoft.Model.TourStructure.MTourStatusChange statusChange = new EyouSoft.Model.TourStructure.MTourStatusChange();
            statusChange.CompanyId = this.SiteUserInfo.CompanyId;
            statusChange.DeptId = this.SiteUserInfo.DeptId;
            statusChange.IssueTime = System.DateTime.Now;
            statusChange.Operator = this.SiteUserInfo.Name;
            statusChange.OperatorId = this.SiteUserInfo.UserId;
            statusChange.TourId = TourId;
            statusChange.TourStatus = status;
            bool result = new EyouSoft.BLL.TourStructure.BTour().UpdateTourStatus(statusChange);
            if (result)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "提交成功！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "提交失败！");
            }

            RCWE(msg);
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调终审_栏目, true);
                return;
            }
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
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取收入合同金额链接
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="jinE">合同确认金额</param>
        /// <param name="isQuRen">合同金额是否确认</param>
        /// <returns></returns>
        protected string GetShouRuHeTongJinELinkHtml(object orderId, object jinE, object isQuRen)
        {
            if (orderId == null || jinE == null || isQuRen == null) return string.Empty;

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            if (TourType.Value == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                var _url = PrintPage_DanXiangYeWuYouKeQuRenDan;
                if (!string.IsNullOrEmpty(_url) && _url != "javascript:void(0)")
                {
                    _url += "?tourid=" + TourId;
                }

                s.Append(" <a target='_blank' ");
                s.AppendFormat(" href='{0}' ", _url);
                if (!(bool)isQuRen)
                {
                    s.Append(" style='color:#ff0000;' ");
                }
                s.Append(" > ");
                s.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(jinE, ProviderToMoney));
                s.Append("</a>");

                return s.ToString();
            }

            s.Append(" <a target='_blank' class='i_hetongquerenjine' ");
            s.AppendFormat(" i_tourid='{0}' ", TourId);
            s.AppendFormat(" i_tourtype='{0}' ", (int)TourType.Value);
            s.AppendFormat(" i_orderid='{0}' ", orderId.ToString());
            s.AppendFormat(" href='javascript:void(0)' ");
            if (!(bool)isQuRen)
            {
                s.Append(" style='color:#ff0000;' ");
            }
            s.Append(" > ");
            s.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(jinE, ProviderToMoney));
            s.Append("</a>");

            return s.ToString();
        }
        #endregion

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
