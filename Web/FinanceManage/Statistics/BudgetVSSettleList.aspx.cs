using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.StatStructure;
using EyouSoft.BLL.FinStructure;
using System.Xml.Linq;
using EyouSoft.Model.EnumType.PrivsStructure;
using System.Text;

namespace Web.FinanceManage.Statistics
{
    /// <summary>
    /// 结算预算对比表-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class BudgetVSSettleList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1); ;
            int recordCount = 0;

            #endregion
            MBudgetContrastBase queryString = new MBudgetContrastBase();
            queryString.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            queryString.RouteName = Utils.GetQueryStringValue("txt_lineName");
            queryString.LDateS = Utils.GetQueryStringValue("txt_SDate");
            queryString.LDateE = Utils.GetQueryStringValue("txt_EDate");
            queryString.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryString.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryString.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            queryString.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);

            MBudgetContrast sumModel = new MBudgetContrast();
            IList<MBudgetContrast> ls = new BFinance().GetBudgetContrastLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref  sumModel,
                CurrentUserCompanyID,
                queryString);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            pan_sum.Visible = !pan_Msg.Visible;
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
            BindSum(sumModel);
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 绑定统计数据
        /// </summary>
        /// <param name="xmlSum">xml类型的字符串</param>
        private void BindSum(MBudgetContrast sumModel)
        {
            lbl_budgetIncome.Text = UtilsCommons.GetMoneyString(sumModel.BudgetIncome, ProviderToMoney);
            lbl_budgetOutgo.Text = UtilsCommons.GetMoneyString(sumModel.BudgetOutgo, ProviderToMoney);
            lbl_budgetProfit.Text = UtilsCommons.GetMoneyString(sumModel.BudgetGProfit, ProviderToMoney);
            lbl_clearingIncome.Text = UtilsCommons.GetMoneyString(sumModel.ClearingIncome, ProviderToMoney);
            lbl_clearingOutgo.Text = UtilsCommons.GetMoneyString(sumModel.ClearingOutgo, ProviderToMoney);
            lbl_clearingProfit.Text = UtilsCommons.GetMoneyString(sumModel.ClearingGProfit, ProviderToMoney);
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_财务统计_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_财务统计_栏目, true);
                return;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            MBudgetContrast sumModel = new MBudgetContrast();
            MBudgetContrastBase queryString = new MBudgetContrastBase();
            queryString.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            queryString.RouteName = Utils.GetQueryStringValue("txt_lineName");
            queryString.LDateS = Utils.GetQueryStringValue("txt_SDate");
            queryString.LDateE = Utils.GetQueryStringValue("txt_EDate");
            queryString.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryString.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryString.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            queryString.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);

            IList<MBudgetContrast> ls = new BFinance().GetBudgetContrastLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref recordCount,
                ref  sumModel,
                CurrentUserCompanyID,
                queryString);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\n",
                    "团号",
                    "线路名称",
                    "客户单位",
                    "出团时间",
                    "销售员",
                    "计调员",
                    "费用预算收入",
                    "费用预算支出",
                    "费用预算毛利",
                    "结算费用收入",
                    "结算费用支出",
                    "结算费用毛利");
                foreach (MBudgetContrast item in ls)
                {
                    sb.Append(item.TourCode + "\t");
                    sb.Append(item.RouteName + "\t");
                    sb.Append(item.Crm + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.LDate, ProviderToDate) + "\t");
                    sb.Append(item.SellerName + "\t");
                    sb.Append(item.Planer + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.BudgetIncome, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.BudgetOutgo, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.BudgetGProfit, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.ClearingIncome, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.ClearingOutgo, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.ClearingGProfit, ProviderToMoney) + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }
        #endregion
    }
}
