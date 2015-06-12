using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.PlanStructure;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Text;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;


namespace EyouSoft.Web.m
{
    /// <summary>
    /// 手机版导游报账
    /// 创建人：赵晓慧
    /// 创建时间：2012-07-09
    /// </summary>
    public partial class BaoZhang : EyouSoft.Common.Page.MobilePage
    {
        protected PlanChangeChangeClass? ParentType = null;
        /// <summary>
        /// 0 导游报账_栏目权限 , 1 导游报账_导游报账操作权限
        /// </summary>
        protected string PrivsPage = "0";
        string mark ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        #region 初始化数据
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void DataInit()
        {
            string b = Utils.GetQueryStringValue("mark");
            switch (b)
            {
                case "1":
                    this.lblMsg.Text = "提交成功！";
                    break;
                case "2":
                    this.lblMsg.Text = "提交失败！";
                    break;
                case "3":
                    this.lblMsg.Text = "保存成功！";
                    break;
            }
            string tourId = Utils.GetQueryStringValue("tourId");
            BPlan bll = new BPlan();

            //团队支出
            IList<MPlanBaseInfo> list =  bll.GetList(tourId);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                this.litFrist.Text = "&nbsp;&nbsp;&nbsp;&nbsp;<span>类型</span>&nbsp;-&nbsp;<span>名称</span>&nbsp;-&nbsp;<span>结算费用</span>";
            }
            else {
                this.litFrist.Text = "暂无支出!";
            }

            //导游收入
            EyouSoft.Model.TourStructure.MOrderSum sum = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> orders = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref sum, tourId);

            if (orders != null && orders.Count > 0)
            {
                repGuidInMoney.DataSource = orders;
                repGuidInMoney.DataBind();
                this.litSecond.Text = "&nbsp;&nbsp;&nbsp;&nbsp;<span>客源单位</span>&nbsp;-&nbsp;<span>团款现收</span>";
            }
            else {
                this.litSecond.Text = "暂无收入!";
            }

            //导游借款
            IList<MDebit> ls = new EyouSoft.BLL.FinStructure.BFinance().GetDebitLstByTourId(tourId, true);
            if (ls != null && ls.Count > 0)
            {
                this.rptDebit.DataSource = ls;
                this.rptDebit.DataBind();
                this.litThird.Text = "&nbsp;&nbsp;&nbsp;&nbsp;<span>姓名</span>&nbsp;-&nbsp;<span>日期</span>&nbsp;-&nbsp;<span>金额</span>";
            }
            else {
                this.litThird.Text = "暂无借款!";
            }

            //报账汇总
            MBZHZ model = bll.GetBZHZ(tourId);
            if (model != null)
            {
                lbl_guidesIncome.Text = UtilsCommons.GetMoneyString(model.GuideIncome, ProviderToMoney);
                lbl_guidesBorrower.Text = UtilsCommons.GetMoneyString(model.GuideBorrow, ProviderToMoney);
                lbl_guidesSpending.Text = UtilsCommons.GetMoneyString(model.GuideOutlay, ProviderToMoney);
                lbl_replacementOrReturn.Text = UtilsCommons.GetMoneyString(model.GuideMoneyRtn, ProviderToMoney);
            }
        }
        #endregion


        #region 权限判断
        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目))
            {
                Utils.MobileResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目);
                return;
            }
            else
            {
                if (Privs_DaoYouBaoZhang())
                {
                    PrivsPage = "1";

                    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_导游报账操作))
                    {
                        this.butSellsExamineV.Visible = true;
                    }
                    else
                    {
                        this.butSellsExamineV.Visible = false;
                    }
                }
            }

        }
        /// <summary>
        /// 判断是否可以导游报账
        /// </summary>
        /// <returns></returns>
        private bool Privs_DaoYouBaoZhang()
        {
            var tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Utils.GetQueryStringValue("tourId"));
            if (tourinfo == null) return false;
            if (tourinfo.SaleInfo == null) return false;
            if (tourinfo.TourPlaner == null || tourinfo.TourPlaner.Count == 0) return false;

            //已安排导游 非安排导游不可报账
            if (tourinfo.GuideList != null && tourinfo.GuideList.Count > 0)
            {
                var daoyou = tourinfo.GuideList.FirstOrDefault(item => item.GuidId == SiteUserInfo.UserId);
                if (daoyou == null) return false;
            }
            else//未安排导游，团队销售员、计调员均可操作导游报账
            {
                var jidiao = tourinfo.TourPlaner.FirstOrDefault(item => item.PlanerId == SiteUserInfo.UserId);
                if (tourinfo.SaleInfo.SellerId != SiteUserInfo.UserId && jidiao == null) return false;
            }
            //团队状态判断
            TourStatus[] status = { TourStatus.导游带团, TourStatus.导游报帐 };
            if (!status.Contains(tourinfo.TourStatus)) return false;

            return true;
        }
        #endregion


        #region 提交销售审核

        /// <summary>
        /// 提交销售审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butSellsExamineV_Click(object sender, EventArgs e)
        {
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.销售待审, Utils.GetQueryStringValue("tourId"));
            
        }


        #region 提交销售审核-修改退队状态
        /// <summary>
        /// 修改团队状态
        /// </summary>
        /// <param name="tourStatus">团队状态</param>
        /// <param name="tourId">计调编号</param>
        private void UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus, string tourId)
        {
            
            BTour btour = new BTour();
            EyouSoft.Model.TourStructure.MTourStatusChange mtourstatuschange = new EyouSoft.Model.TourStructure.MTourStatusChange();
            mtourstatuschange.TourId = tourId;
            mtourstatuschange.DeptId = SiteUserInfo.DeptId;
            mtourstatuschange.Operator = SiteUserInfo.Name;
            mtourstatuschange.OperatorId = SiteUserInfo.UserId;
            mtourstatuschange.IssueTime = System.DateTime.Now;
            mtourstatuschange.TourStatus = tourStatus;
            mtourstatuschange.CompanyId = SiteUserInfo.CompanyId;
            if (btour.UpdateTourStatus(mtourstatuschange)) 
            {
                mark = "1";
            }
            else
            {
                mark = "2";
            }
            Response.Redirect("/m/baozhang.aspx?sl=" + Utils.GetQueryStringValue("sl") + "&source=1&tourId=" + tourId + "&tourType=" + Utils.GetQueryStringValue("tourType") + "&mark=" + mark);
        }
        #endregion
        #endregion


        #region 保存
        protected void butSave_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("/m/baozhang.aspx?sl=" + Utils.GetQueryStringValue("sl") + "&source=1&tourId=" + Utils.GetQueryStringValue("tourId") + "&tourType=" + Utils.GetQueryStringValue("tourType") + "&mark=3");
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string[] hidIncome = Utils.GetFormValues("hidIncome");
            string[] txtRealIncome = Utils.GetFormValues("txtRealIncome");
            string[] hidOrderId = Utils.GetFormValues("hidOrderId");


            EyouSoft.Model.TourStructure.MTourOrderSales model = new EyouSoft.Model.TourStructure.MTourOrderSales();
            model.Operator = this.SiteUserInfo.Name;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.CollectionRefundState = EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款;
            model.CollectionRefundDate = System.DateTime.Now;
            model.CollectionRefundOperator = this.SiteUserInfo.Name;
            model.CollectionRefundOperatorID = this.SiteUserInfo.UserId;
            model.IsGuideRealIncome = true;

            EyouSoft.BLL.TourStructure.BTourOrder bll = new EyouSoft.BLL.TourStructure.BTourOrder();

            for (int i = 0; i < hidIncome.Length; i++)
            {
                model.OrderId = hidOrderId[i];
                decimal income = Utils.GetDecimal(hidIncome[i]);
                model.CollectionRefundAmount = Utils.GetDecimal(txtRealIncome[i]);
                bll.UpdateGuideRealIncome(model.OrderId, income, model.CollectionRefundAmount, "", model);
            }
        }
        #endregion
    }
}
