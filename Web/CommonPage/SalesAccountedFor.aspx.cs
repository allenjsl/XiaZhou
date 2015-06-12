using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Model.PlanStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;
using System.Text;
using System.Collections;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.CommonPage
{
    using EyouSoft.Model.EnumType.KingDee;

    /// <summary>
    /// 销售报账
    /// 财务报账
    /// 导游报账
    /// 计调报账
    /// 公用
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    /// 必传参数：
    /// sl:你懂得
    /// tourId:团队编号
    /// source:页面来源
    /// 1 = 导游报账
    /// 2 = 计调报账
    /// 3 = 销售报账
    /// 4 = 财务报账
    public partial class SalesAccountedFor : BackPage
    {
        #region attributes
        /// <summary>
        /// 订单编号
        /// </summary>
        protected string OrderID = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        protected string OrderCode = string.Empty;
        /// <summary>
        /// 客源单位
        /// </summary>
        protected string buyCompanyName = string.Empty;
        /// <summary>
        /// 导游应收
        /// </summary>
        protected string guidIncome = string.Empty;
        /// <summary>
        /// 团队状态
        /// </summary>
        protected TourStatus status;

        /// <summary>
        /// 是否可以提交报账
        /// </summary>
        protected bool IsSubmit;

        /// <summary>
        /// 是否可以修改导游收入、支出
        /// </summary>
        protected bool IsChangeDaoYou = false;

        /// <summary>
        /// 是否可以修改其它收入
        /// </summary>
        protected bool IsChangeShouRu = false;
        /// <summary>
        /// 代收栏目权限
        /// </summary>
        bool Privs_DaiShouLanMu = false;
        /// <summary>
        /// 代收登记权限
        /// </summary>
        bool Privs_DaiShouDengJi = false;
        /// <summary>
        /// 核算单链接
        /// </summary>
        protected string PrintPageHSD = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "deletedaishou": DeleteDaiShou();break;
                default: break;
            }    

            DataInit();

            #region ajax请求
            string type = Utils.GetQueryStringValue("type");
            string tourID = Utils.GetQueryStringValue("tourId");
            //其它收入id
            string Id = Utils.GetQueryStringValue("ID");
            string orderID = Utils.GetQueryStringValue("OrderId");
            switch (type)
            {
                case "save":/*计调终审*/
                    OperaterCheck();
                    break;
                case "saveFreeItem":/*其他收入添加修改*/
                    AddAndUpdateOtherMoneyIn(Id);
                    break;
                case "DeleteFreeItem":/*其他收入删除*/
                    deleteOtherMoneyIn(Id);
                    break;
                case "saveGuidMoneyIn":/*导游收入添加修改*/
                    AddAndUpdateGuidRealIncome(orderID);
                    break;
                case "SellsExamineV":
                    SellsExamineV();
                    break;
                case "OperaterExamineV":
                    OperaterExamineV();
                    break;
                case "ApplyOver":/*报销完成*/
                    ApplyOver();
                    break;                
                default: break;
            }

            #endregion

            InitDaiShous();
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            //权限判断
            PowerControl(tourId);
            //配置团队支出
            TourMoneyOut.TourID = tourId;
            //团队基本数据
            TourModelInit(tourId);
            //导游收入
            TourGuideMoneyIn(tourId);
            //导游借款
            Debit(tourId);
            //计调BLL
            BPlan bll = new BPlan();
            //其他收入
            OtherMoneyIn(bll, tourId);
            //报账汇总
            TourSummarizing(bll, tourId);
            //团队收支总汇
            TourInOutSum(bll, tourId);

            PrintPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            PrintPageHSD += "?referertype=1&tourid=" + tourId;
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
                //团号
                lbl_TourCode.Text = model.TourCode;
                //线路区域               
                EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(model.AreaId, SiteUserInfo.CompanyId);
                if (AreaModel != null)
                {
                    this.lbl_AreaName.Text = AreaModel.AreaName;
                }
                AreaModel = null;

                //线路名称
                lbl_RouteName.Text = model.RouteName;
                //天数
                lbl_TourDays.Text = model.TourDays.ToString();
                //出发时间
                lbl_LDate.Text = UtilsCommons.GetDateString(model.LDate, ProviderToDate);
                //返回时间
                lbl_RDate.Text = UtilsCommons.GetDateString(model.RDate, ProviderToDate);
                //导游
                lbl_TourGride.Text = UtilsCommons.PingGuide(model.GuideList);
                //出发交通
                lbl_LTraffic.Text = model.LTraffic;
                //销售员
                this.hideSaleId.Value = model.SaleInfo.SellerId;
                lbl_SaleInfo.Text = model.SaleInfo.Name;
                //返回交通
                lbl_RTraffic.Text = model.RTraffic;
                //计调
                lbl_TourPlaner.Text = UtilsCommons.PingPlaner(model.TourPlaner);
                //集合方式
                lbl_Gather.Text = model.Gather;
            }
        }

        #endregion

        #region 导游收入
        /// <summary>
        /// 初始化导游收入
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void TourGuideMoneyIn(string tourId)
        {
            EyouSoft.Model.TourStructure.MOrderSum sum = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> orders = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref  sum, tourId);

            //导游报账 导游收入列表绑定
            if (orders != null && orders.Count > 0)
            {
                repGuidInMoney.DataSource = orders;
                repGuidInMoney.DataBind();
            }
        }

        /// <summary>
        /// 导游收入 添加 修改
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns></returns>
        protected void AddAndUpdateGuidRealIncome(string orderID)
        {

            //导游现收
            decimal GuideIncome = Utils.GetDecimal(Utils.GetFormValue("txtGuideIncome"));
            //导游实收
            decimal realIncom = Utils.GetDecimal(Utils.GetFormValue("txtRealIncome"));
            //备注
            string remarks = Utils.GetFormValue("txtConfirmRemark");
            MTourOrderSales model = new MTourOrderSales();
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(CurrentUserCompanyID) ?? new List<EyouSoft.Model.ComStructure.MComPayment>();
            ls = ls.Where(item => item.Name == "导游现收" && item.IsSystem).ToList();
            if (ls != null && ls.Count > 0)
            {
                model.CollectionRefundMode = ls[0].PaymentId;
                model.CollectionRefundModeName = ls[0].Name;
            }
            model.OrderId = orderID;
            model.Operator = this.SiteUserInfo.Name;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.CollectionRefundAmount = realIncom;
            model.CollectionRefundState = CollectionRefundState.收款;
            model.CollectionRefundDate = System.DateTime.Now;
            model.CollectionRefundOperator = this.SiteUserInfo.Name;
            model.CollectionRefundOperatorID = this.SiteUserInfo.UserId;
            model.IsGuideRealIncome = true;
            model.ShouKuanType = EyouSoft.Model.EnumType.FinStructure.ShouKuanType.导游实收;

            //修改导游收入
            if (Utils.GetQueryStringValue("actionType") == "update")
            {
                if (new EyouSoft.BLL.TourStructure.BTourOrder().UpdateGuideRealIncome(orderID, GuideIncome, realIncom, remarks, model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功！"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "修改失败！"));
                }
            }
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
                repOtherMoneyIn.DataSource = ls;
                repOtherMoneyIn.DataBind();
            }

            if (IsSubmit == false)
            {
                panMoneyInView.Visible = false;
            }

        }

        /// <summary>
        /// 其他收入  添加 其他收入 修改
        /// <param name="Id">收入id</param>
        /// </summary>
        private void AddAndUpdateOtherMoneyIn(string ID)
        {
            string result = string.Empty;
            EyouSoft.Model.FinStructure.MOtherFeeInOut outFree = new MOtherFeeInOut();

            outFree.PayType = Utils.GetInt(Utils.GetFormValue("other_payment"));
            outFree.PayTypeName = Utils.GetFormValue("paymentText");
            outFree.TourId = Utils.GetQueryStringValue("tourId");
            outFree.FeeItem = Utils.GetFormValue("txtFreeItem");
            outFree.DeptId = this.SiteUserInfo.DeptId;
            outFree.Operator = this.SiteUserInfo.Name;
            outFree.OperatorId = this.SiteUserInfo.UserId;
            outFree.IssueTime = System.DateTime.Now;
            outFree.FeeAmount = Utils.GetDecimal(Utils.GetFormValue("txtFeeAmount"));
            outFree.Remark = Utils.GetFormValue("txtRemark");
            outFree.Crm = Utils.GetFormValue("crmName");
            outFree.CrmId = Utils.GetFormValue("crmId");
            outFree.CompanyId = this.SiteUserInfo.CompanyId;
            outFree.DealTime = System.DateTime.Now;

            switch (Utils.GetQueryStringValue("source"))
            {
                case "1": outFree.IsGuide = PlanAddStatus.导游报账时添加; break;
                case "2": outFree.IsGuide = PlanAddStatus.计调报账时添加; break;
                case "3": outFree.IsGuide = PlanAddStatus.销售报账时添加; break;
                default: outFree.IsGuide = PlanAddStatus.其他; break;
            }

            outFree.TourCode = Utils.GetQueryStringValue("tourCode");
            outFree.DealerId = Utils.GetQueryStringValue("sellerId");
            outFree.Dealer = Server.UrlDecode(Utils.GetQueryStringValue("sellerName"));
            if (!string.IsNullOrEmpty(ID))
            {
                outFree.Id = Utils.GetInt(ID);
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().UpdOtherFeeInOut(ItemType.收入, outFree) ? "1" : "-1", "提交失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().AddOtherFeeInOut(ItemType.收入, outFree) ? "1" : "-1", "提交失败!"));
            }
        }

        #region 其它收入
        /// <summary>
        /// 其它收入
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected bool GetOutFreeStatus(EyouSoft.Model.EnumType.FinStructure.FinStatus status)
        {
            bool result = true;
            if (IsSubmit == false || status == EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付)
            {
                result = false;
            }
            return result;
        }
        #endregion


        /// <summary>
        /// 其他收入 删除
        /// </summary>
        /// <param name="Id">收入id</param>
        private void deleteOtherMoneyIn(string Ids)
        {
            AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().DelOtherFeeInOut(this.SiteUserInfo.CompanyId, ItemType.收入, Utils.ConvertToIntArray(Ids.Split(','))) > 0 ? "1" : "-1", "删除成功!"));

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

        #region 导游借款
        /// <summary>
        /// 导游借款
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void Debit(string tourId)
        {
            IList<MDebit> ls = new EyouSoft.BLL.FinStructure.BFinance().GetDebitLstByTourId(tourId, true);
            if (ls != null && ls.Count > 0)
            {
                pan_DebitMsg.Visible = false;
                this.rpt_Debit.DataSource = ls;
                this.rpt_Debit.DataBind();
            }
        }
        #endregion

        #region  团队收入
        /// <summary>
        /// 团队收入
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetOrderListByTourId(string tourID)
        {
            EyouSoft.Model.TourStructure.MOrderSum orders = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> tourOrder = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orders, tourID);
            if (tourOrder != null && tourOrder.Count > 0)
            {
                this.repTourMoneyInList.DataSource = tourOrder;
                this.repTourMoneyInList.DataBind();
                this.repOrderList.DataSource = tourOrder;
                this.repOrderList.DataBind();
                this.litConfirmMoneyCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orders.ConfirmMoney, ProviderToMoney);
                this.litConfirmSettlementMoneyCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orders.ConfirmSettlementMoney, ProviderToMoney);
                this.litProfitCount.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(orders.Profit, ProviderToMoney);
            }
            else
            {
                this.labTourMsg.Text = "暂无数据！";
                this.labOrderMsg.Text = "暂无数据!";
                this.trCountView.Visible = false;
                this.trSanPingOrderCount.Visible = true;
                this.trTourCountMsg.Visible = true;
            }
        }
        #endregion

        #region 团队收支总汇
        /// <summary>
        /// 团队收支总汇
        /// </summary>
        private void TourInOutSum(BPlan BLL, string tourID)
        {
            /*EyouSoft.Model.TourStructure.MOrderSum orders = new EyouSoft.Model.TourStructure.MOrderSum();
            new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orders, tourID);
            //团队收入
            decimal tourMoneyIn = 0;
            if (orders != null)
            {
                tourMoneyIn = orders.ConfirmSettlementMoney;
            }
            //团队支出
            decimal tourMoneyOut = 0;
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> ls = BLL.GetList(tourID);
            if (ls != null && ls.Count > 0)
            {
                tourMoneyOut = ls.Sum(item => item.Confirmation);
            }
            //团队利润=团队收入-团队支出
            lbl_tourMoney.Text = (tourMoneyIn - tourMoneyOut).ToString();
            //利润率 = 团队利润/团队收入(不要把/100.00改成100 原因请参考.net基础之 加减乘除)
            lbl_tourMoneyRate.Text = tourMoneyIn != 0 ? (((double)(Utils.GetDecimal(lbl_tourMoney.Text) / tourMoneyIn * 10000)) / 100.00).ToString("f2") + "%" : "0%";
            //格式化团队利润
            lbl_tourMoney.Text = UtilsCommons.GetMoneyString(lbl_tourMoney.Text, ProviderToMoney);
            //格式化团队收入
            lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(tourMoneyIn, ProviderToMoney);
            //格式化团队支出
            lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(tourMoneyOut, ProviderToMoney);*/

            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(tourID);
            this.lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(info.TourIncome + info.QiTaShouRu, ProviderToMoney);
            this.lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(info.TourOutlay, ProviderToMoney);
            this.lbl_tourMoney.Text = UtilsCommons.GetMoneyString(info.TourProfit, ProviderToMoney);
            this.lbl_tourMoneyRate.Text = info.TourProRate.ToString("F2") + "%";
        }
        #endregion
        /// <summary>
        /// 提交计调终审
        /// </summary>
        /// <returns></returns>
        protected void OperaterCheck()
        {
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审, Utils.GetQueryStringValue("tourId"));
        }
        /// <summary>
        /// 提交销售审核
        /// </summary>
        private void SellsExamineV()
        {
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.销售待审, Utils.GetQueryStringValue("tourId"));
        }
        /// <summary>
        /// 提交计调审核
        /// </summary>
        private void OperaterExamineV()
        {
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审, Utils.GetQueryStringValue("tourId"));
        }
        /// <summary>
        /// 报销完成
        /// </summary>
        private void ApplyOver()
        {
            this.AjaxResponse(UtilsCommons.AjaxReturnJson(new BTour().Apply(
                this.SiteUserInfo.DeptId,
               this.SiteUserInfo.UserId,
               this.SiteUserInfo.Name, 
               Utils.GetQueryStringValue("tourId"), 
               this.CurrentUserCompanyID) ? "1" : "-1", "报销成功!"));

        }
        /// <summary>
        /// 改变团队状态
        /// </summary>
        /// <param name="tourStatus">计划状态</param>
        /// <param name="tourId">团队编号</param>
        private void UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus, string tourId)
        {
            var info = new MTourStatusChange
            {
                TourId = tourId,/*团队编号*/
                CompanyId = CurrentUserCompanyID,/*系统公司编号*/
                DeptId = SiteUserInfo.DeptId,/*操作人部门Id*/
                Operator = SiteUserInfo.Name,/*操作人*/
                OperatorId = SiteUserInfo.UserId,/*操作人ID*/
                IssueTime = System.DateTime.Now,/*操作时间*/
                TourStatus = tourStatus/*团队状态*/
            };

            bool bllRetCode = new BTour().UpdateTourStatus(info);

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "提交成功"));
            else if (info.OutputCode == -99) RCWE(UtilsCommons.AjaxReturnJson("-1", "存在未报账的订单信息，请先进行订单报账后再提交报账。"));
            else if (info.OutputCode == -97) RCWE(UtilsCommons.AjaxReturnJson("-1", "存在导游收入-导游实收未保存的信息，请先保存导游实收信息再提交报账。"));
            else if (info.OutputCode == -96) RCWE(UtilsCommons.AjaxReturnJson("-1", "该计划下存在未处理(或已留位、或留位过期、或资金超限、或垫付申请中、或垫付申请未通过)的订单，请先处理好订单后再提交报账。"));
            else RCWE(UtilsCommons.AjaxReturnJson("-1", "提交失败!"));
        }

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl(string tourId)
        {
            int source = Utils.GetInt(Utils.GetQueryStringValue("source"));

            TourMoneyOut.ParentType = (PlanChangeChangeClass?)Utils.GetEnumValueNull(typeof(PlanChangeChangeClass), Utils.GetQueryStringValue("source"));
            if (source == 4)
            {
                TourMoneyOut.IsPlanChangeChange = false;

            }
            //团队实体
            MTourBaseInfo model = new BTour().GetTourInfo(tourId) ?? new MTourBaseInfo();
            IList<MGuidInfo> guideList = (model.GuideList ?? new List<MGuidInfo>()).Where(item => item.GuidId == SiteUserInfo.UserId).ToList();
            IList<MTourPlaner> tourPlaner = (model.TourPlaner ?? new List<MTourPlaner>()).Where(item => item.PlanerId == SiteUserInfo.UserId).ToList();
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            status = model.TourStatus;

            this.pan_OperaterExamineV.Visible = pan_OperaterCheck.Visible = pan_SellsExamineV.Visible = false;

            #region 修改删除权限控制
            //导游报账
            if (source == 1)
            {
                IsSubmit = IsChangeDaoYou = pan_SellsExamineV.Visible = TourMoneyOut.IsChangeDaoYou = TourMoneyOut.IsPlanChangeChange = Privs_DaoYouBaoZhang();
            }

            if (source == 3) //销售报账
            {
                //团队状态
                GetTourType();
                //显示操作
                GetOrderListByTourId(tourId);
                IsSubmit = pan_OperaterExamineV.Visible = TourMoneyOut.IsPlanChangeChange = Privs_XiaoShouBaoZhang(out IsChangeDaoYou);
                TourMoneyOut.IsChangeDaoYou = IsChangeDaoYou;
            }

            //计调报账
            if (source == 2)
            {
                //团队收入
                GetOrderListByTourId(tourId);
                //团队状态
                GetTourType();
                pan_OperaterCheck.Visible = IsSubmit = TourMoneyOut.IsPlanChangeChange = Privs_JiDiaoBaoZhang(out IsChangeDaoYou);
                TourMoneyOut.IsChangeDaoYou = IsChangeDaoYou;
            }

            #endregion

            /*
             * 财务提交-报销完成
             */
            if (source == 4 && CheckGrant(Privs.财务管理_报销报账_报销完成))
            {
                TourStatus[] status1 = { TourStatus.销售未结算, TourStatus.销售待审, TourStatus.计调待审, TourStatus.待终审, TourStatus.财务核算 };

                if (comModel.SkipGuide)
                {
                    status1 = new TourStatus[] { TourStatus.待终审, TourStatus.财务核算 };
                }

                //报销完成
                pan_ApplyOver.Visible = status1.Contains(model.TourStatus) && (!model.IsSubmit);
                //财务入账
                pan_InAccount.Visible = comModel.IsEnableKis && model.IsSubmit;
            }
            else
            {
                pan_ApplyOver.Visible = false;
                pan_InAccount.Visible = false;
            }
        }

        /// <summary>
        /// 团队状态
        /// </summary>
        protected void GetTourType()
        {
            int tourType = Utils.GetInt(Utils.GetQueryStringValue("tourType"));
            if ((EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.地接团队 || (EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.组团团队 || (EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.出境团队)
            {
                //团队收入(派团)
                this.phdTourMoneyIn.Visible = true;
            }

            if ((EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.出境散拼 || (EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.地接散拼 || (EyouSoft.Model.EnumType.TourStructure.TourType)(tourType) == TourType.组团散拼)
            {
                //团队收入(散拼)
                this.orderListView.Visible = true;
            }
        }

        /// <summary>
        /// 获得其它收入的支付方式
        /// </summary>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        protected string GetPayMentStr(string selectValue, out bool isChangeShouRu)
        {
            string PaymentStr = " <select class='inputselect' name='other_payment' onchange='AccountedForPage.PayMentChange(this);'>";
            EyouSoft.BLL.ComStructure.BComPayment payMentBll = new BComPayment();
            IList<EyouSoft.Model.ComStructure.MComPayment> list = payMentBll.GetList(this.SiteUserInfo.CompanyId, null, ItemType.收入);
            isChangeShouRu = false;
            if (list == null || list.Count == 0) return string.Empty;

            for (int i = 0; i < list.Count; i++)
            {
                if (IsChangeDaoYou == false && list[i].IsSystem && selectValue == list[i].PaymentId.ToString())
                {
                    isChangeShouRu = false;
                    return list[i].Name;

                }
                else if (IsChangeDaoYou == false && list[i].IsSystem)
                {
                    //PaymentStr += "";
                }
                else
                {
                    PaymentStr += "<option " + (selectValue == list[i].PaymentId.ToString() ? "selected='selected'" : "") + " value='" + list[i].PaymentId.ToString() + "'>" + list[i].Name + "</option>";
                    isChangeShouRu = true;
                }
            }
            PaymentStr += "</select>";
            return PaymentStr;
        }
        #endregion

        #endregion

        #region privs
        /// <summary>
        /// 导游报账权限控制，返回是否允许导游报账
        /// </summary>
        /// <returns></returns>
        bool Privs_DaoYouBaoZhang()
        {
            //无权限 不可报账
            if (!CheckGrant(Privs.导游中心_导游报账_导游报账操作)) return false;

            var tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Utils.GetQueryStringValue("tourId"));
            if (tourinfo == null) return false;
            if (tourinfo.SaleInfo == null) return false;
            if (tourinfo.TourPlaner == null || tourinfo.TourPlaner.Count == 0) return false;

            //已安排导游 非安排导游不可报账
            if (tourinfo.GuideList != null && tourinfo.GuideList.Count > 0)
            {
                /*bool isexists = false;
                foreach (var daoyou1 in tourinfo.GuideList)
                {
                    if (daoyou.GuidId == SiteUserInfo.UserId) { isexists = true; break; }
                }

                if (!isexists) return false;*/

                var daoyou = tourinfo.GuideList.FirstOrDefault(item => item.GuidId == SiteUserInfo.UserId);
                if (daoyou == null) return false;
            }
            else//未安排导游，团队销售员、计调员均可操作导游报账
            {
                var jidiao = tourinfo.TourPlaner.FirstOrDefault(item => item.PlanerId == SiteUserInfo.UserId);

                //if (!(tourinfo.SaleInfo.SellerId == SiteUserInfo.UserId || jidiao != null)) return false;

                if (tourinfo.SaleInfo.SellerId == SiteUserInfo.UserId || jidiao != null)
                {

                }
                else return false;
            }

            //团队状态判断
            TourStatus[] status = { TourStatus.导游带团, TourStatus.导游报帐 };
            if (!status.Contains(tourinfo.TourStatus)) return false;

            return true;
        }


        /// <summary>
        /// 销售报账报账权限控制，返回是否允许销售报账
        /// </summary>
        /// <param name="caoZuoDaoYouShouZhi">是否允许操作导游收入、支出</param>
        bool Privs_XiaoShouBaoZhang(out bool caoZuoDaoYouShouZhi)
        {
            caoZuoDaoYouShouZhi = false;

            //无权限 不可报账
            if (!CheckGrant(Privs.销售中心_销售报账_销售报账操作)) return false;

            var tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Utils.GetQueryStringValue("tourId"));
            if (tourinfo == null) return false;
            if (tourinfo.SaleInfo == null) return false;

            //非本团销售员 不可报账
            if (tourinfo.SaleInfo.SellerId != SiteUserInfo.UserId) return false;

            //合同金额未确认 不可报账
            if (!new EyouSoft.BLL.TourStructure.BTour().GetConfirmMoneyStatus(Utils.GetQueryStringValue("tourId"))) return false;

            bool tiaoGuoDaoYouBaoZhang = false;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (setting != null) tiaoGuoDaoYouBaoZhang = setting.SkipGuide;

            TourStatus[] status = { TourStatus.销售待审 };
            if (tiaoGuoDaoYouBaoZhang) status = new TourStatus[] { TourStatus.导游带团, TourStatus.导游报帐, TourStatus.销售待审 };

            //团队状态判断
            if (!status.Contains(tourinfo.TourStatus)) return false;

            if (tiaoGuoDaoYouBaoZhang) caoZuoDaoYouShouZhi = true;

            return true;
        }

        /// <summary>
        /// 计调报账报账权限控制，返回是否允许计调报账
        /// </summary>
        /// <param name="caoZuoDaoYouShouZhi">是否允许操作导游收入、支出</param>
        bool Privs_JiDiaoBaoZhang(out bool caoZuoDaoYouShouZhi)
        {
            caoZuoDaoYouShouZhi = false;

            //无权限 不可报账
            if (!CheckGrant(Privs.计调中心_计调报账_计调报账操作)) return false;

            var tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Utils.GetQueryStringValue("tourId"));
            if (tourinfo == null) return false;
            if (tourinfo.TourPlaner == null || tourinfo.TourPlaner.Count == 0) return false;

            //非本团计调不可报账
            var jidiao = tourinfo.TourPlaner.FirstOrDefault(item => item.PlanerId == SiteUserInfo.UserId);
            if (jidiao == null) return false;

            //合同金额未确认 不可报账
            if (!new EyouSoft.BLL.TourStructure.BTour().GetConfirmMoneyStatus(Utils.GetQueryStringValue("tourId"))) return false;

            bool tiaoGuoDaoYouBaoZhang = false;
            bool tiaoGuoXiaoShouBaoZhang = false;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (setting != null)
            {
                tiaoGuoDaoYouBaoZhang = setting.SkipGuide;
                tiaoGuoXiaoShouBaoZhang = setting.SkipGuide;
            }

            TourStatus[] status = { TourStatus.计调待审 };

            if (tiaoGuoDaoYouBaoZhang)
            {
                if (tiaoGuoXiaoShouBaoZhang)
                {
                    status = new TourStatus[] { TourStatus.导游带团, TourStatus.导游报帐, TourStatus.销售待审, TourStatus.计调待审 };
                }
                else
                {
                    //status = new TourStatus[] { TourStatus.计调待审 };
                }
            }
            else
            {
                if (tiaoGuoXiaoShouBaoZhang)
                {
                    status = new TourStatus[] { TourStatus.销售待审, TourStatus.计调待审 };
                }
                else
                {
                    //status = new TourStatus[] { TourStatus.计调待审 };
                }
            }

            //团队状态判断
            if (!status.Contains(tourinfo.TourStatus)) return false;

            //if (tiaoGuoDaoYouBaoZhang && tiaoGuoXiaoShouBaoZhang) caoZuoDaoYouShouZhi = true;
            if (tiaoGuoDaoYouBaoZhang) caoZuoDaoYouShouZhi = true;

            return true;
        }
        #endregion

        /// <summary>
        /// 获取财务入账
        /// </summary>
        /// <returns></returns>
        protected string GetFinIn()
        {
            var bll = new BFinance();
            return bll.IsFinIn(
                DefaultProofType.团未完导游先报账, Utils.GetQueryStringValue("tourId"), this.SiteUserInfo.CompanyId)
                   ||
                   bll.IsFinIn(DefaultProofType.单团核算, Utils.GetQueryStringValue("tourId"), this.SiteUserInfo.CompanyId)
                       ? "已入帐"
                       : "未入账";
        }

        /// <summary>
        /// 获取团队收入订单金额颜色
        /// </summary>
        /// <param name="confirmMoneyStatus">合同确认金额状态</param>
        /// <returns></returns>
        protected string GetJinEYanSe(object confirmMoneyStatus)
        {
            string s = string.Empty;

            return s;
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
        /// 获取代收操作列HTML
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetDaiShouCaoZuoHtml(object status)
        {
            if (status == null) return string.Empty;

            var _status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)status;
            string s = string.Empty;

            switch (_status)
            {
                case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未审批:
                    if (Privs_DaiShouDengJi)
                    {
                        s += "<a href='javascript:void(0)' class='i_daishouupdate'>修改</a>&nbsp;&nbsp;";
                        s += "<a href='javascript:void(0)' class='i_daishoudelete'>删除</a>";
                    }
                    else
                    {
                        s += "<a href='javascript:void(0)' class='i_daishouupdate' i_chakan='1'>查看</a>";
                    }
                    break;
                case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未通过:
                    s += "<a href='javascript:void(0)' class='i_daishouupdate' i_chakan='1'>查看</a>";
                    break;
                case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.已通过:
                    s += "<a href='javascript:void(0)' class='i_daishouupdate' i_chakan='1'>查看</a>";
                    break;
                default: break;
            }

            return s;
        }

        /// <summary>
        /// 删除代收登记信息
        /// </summary>
        void DeleteDaiShou()
        {
            if (!Privs_DaiShouDengJi) RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));

            string daiShouId = Utils.GetFormValue("daishouid");

            int bllRetCode = new EyouSoft.BLL.FinStructure.BDaiShou().Delete(daiShouId, SiteUserInfo.CompanyId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "要删除的代收登记信息不存在或已删除"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "要删除的代收登记信息已审批"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_DaiShouLanMu = new EyouSoft.BLL.SysStructure.BSys().IsExistsMenu2(SiteUserInfo.SysId, Menu2.财务管理_代收管理);
            Privs_DaiShouDengJi = CheckGrant(Privs.销售中心_销售报账_代收登记);
        }

        /// <summary>
        /// 导游收入是否保存
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        protected bool IsBaoCunDaoYouShouRu(object orderid)
        {
            bool retCode = false;

            var bllRetCode = new EyouSoft.BLL.TourStructure.BTourOrder().GetBaoCunDaoYouShouRuStatus(orderid.ToString());
            if (bllRetCode == 2) retCode = true;

            return retCode;
        }
    }
}
