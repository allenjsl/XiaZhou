using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.StatStructure;
using System.Xml.Linq;
using EyouSoft.Model.EnumType.PrivsStructure;
using System.Text;

namespace Web.FinanceManage.Statistics
{
    /// <summary>
    /// 利润统计-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-15
    public partial class ProfitList : BackPage
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
            #region 查询条件
            MProfitStatisticsBase queryString = new MProfitStatisticsBase();
            queryString.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            //queryString.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryString.Code = Utils.GetQueryStringValue("txt_teamNumber");
            queryString.RouteName = Utils.GetQueryStringValue("txt_lineName");
            queryString.LDateS = Utils.GetQueryStringValue("txt_SDate");
            queryString.LDateE = Utils.GetQueryStringValue("txt_EDate");
            queryString.IssueTimeS = Utils.GetQueryStringValue("txt_adjustSDate");
            queryString.IssueTimeE = Utils.GetQueryStringValue("txt_adjustEDate");
            queryString.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryString.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryString.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            queryString.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            queryString.GuideId = txt_Guide.GuidID = Utils.GetQueryStringValue(txt_Guide.GuidIDClient);
            queryString.Guide = txt_Guide.GuidName = Utils.GetQueryStringValue(txt_Guide.GuidNameClient);
            #endregion
            MProfitStatistics sumModel = new MProfitStatistics();
            IList<MProfitStatistics> ls = new BFinance().GetProfitStatisticsLst(
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
        private void BindSum(MProfitStatistics sumModel)
        {
            lbl_income.Text = UtilsCommons.GetMoneyString(sumModel.Income, ProviderToMoney);
            lbl_outlay.Text = UtilsCommons.GetMoneyString(sumModel.Outlay, ProviderToMoney);
            lbl_profit.Text = UtilsCommons.GetMoneyString(sumModel.Profit, ProviderToMoney);
            lbl_peopleNum.Text = sumModel.PeopleNum.ToString();
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
            MProfitStatistics sumModel = new MProfitStatistics();
            int recordCount = 0;
            MProfitStatisticsBase queryString = new MProfitStatisticsBase();
            queryString.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            //queryString.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryString.Code = Utils.GetQueryStringValue("txt_teamNumber");
            queryString.RouteName = Utils.GetQueryStringValue("txt_lineName");
            queryString.LDateS = Utils.GetQueryStringValue("txt_SDate");
            queryString.LDateE = Utils.GetQueryStringValue("txt_EDate");
            queryString.IssueTimeS = Utils.GetQueryStringValue("txt_adjustSDate");
            queryString.IssueTimeE = Utils.GetQueryStringValue("txt_adjustEDate");
            queryString.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryString.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryString.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            queryString.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            queryString.GuideId = txt_Guide.GuidID = Utils.GetQueryStringValue(txt_Guide.GuidIDClient);
            queryString.Guide = txt_Guide.GuidName = Utils.GetQueryStringValue(txt_Guide.GuidNameClient);
            IList<MProfitStatistics> ls = new BFinance().GetProfitStatisticsLst(
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
                    "团号/订单号 ",
                    "线路名称",
                    "客户单位",
                    "出团时间",
                    "人数",
                    "销售员",
                    "计调员",
                    "导游",
                    "收入",
                    "支出",
                    "毛利",
                    "核算日期");

                foreach (MProfitStatistics item in ls)
                {
                    sb.Append(item.Code + "\t");
                    sb.Append(item.RouteName + "\t");
                    sb.Append(item.Crm + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.LDate, ProviderToDate) + "\t");
                    sb.Append(item.PeopleNum + "\t");
                    sb.Append(item.SellerName + "\t");
                    sb.Append(item.Planer + "\t");
                    sb.Append(item.Guide + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.Income, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.Outlay, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetMoneyString(item.Profit, ProviderToMoney) + "\t");
                    sb.Append(EyouSoft.Common.UtilsCommons.GetDateString(item.IssueTime, ProviderToDate) + "\n");

                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }
        #endregion
    }
}
