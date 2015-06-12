using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.EnumType.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Text;

namespace Web.FinanceManage.ComeAndGoReconciliation
{
    /// <summary>
    /// 今日付款-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class ReturnMoneyList : BackPage
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
            CustomerUnitSelect1.DefaultTab = PlanProject.酒店;
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            MAuditBase queryModel = new MAuditBase();
            queryModel.IssueTimeS = Utils.GetQueryStringValue("SDate");
            queryModel.IssueTimeE = Utils.GetQueryStringValue("EDate");
            queryModel.PlanerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryModel.Planer = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryModel.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            queryModel.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryModel.SignAmount = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            queryModel.Amount = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));
            queryModel.PlanItem = (PlanProject?)Utils.GetEnumValueNull(typeof(PlanProject), Utils.GetQueryStringValue("item"));
            decimal sum = 0;
            IList<MReconciliation> ls = new BFinance().GetReconciliationLst(
                  pageSize,
                  pageIndex,
                  ref recordCount,
                  ref sum,
                  ReconciliationType.今日付款,
                  CurrentUserCompanyID,
                  queryModel
                 );
            lbl_sum.Text = UtilsCommons.GetMoneyString(sum, ProviderToMoney);
            if (ls != null && ls.Count > 0)
            {
                pan_sum.Visible = !(pan_Msg.Visible = false);
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            //金额汇总信息
            decimal sum = 0;
            IList<MReconciliation> ls = new BFinance().GetReconciliationLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref  recordCount,
                ref sum,
                ReconciliationType.今日付款,
                CurrentUserCompanyID,
                new MAuditBase());
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n",
                    "团号",
                    "计调项",
                    "供应商单位",
                    "计调员",
                    "销售员",
                    "支付金额",
                    "财务人",
                    "支付时间");

                foreach (MReconciliation item in ls)
                {
                    sb.Append(item.TourCode + "\t");
                    sb.Append(item.PlanItem + "\t");
                    sb.Append(item.Crm + "\t");
                    sb.Append(item.Planer + "\t");
                    sb.Append(item.SellerName + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Amount, ProviderToMoney) + "\t");
                    sb.Append(item.Operator + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.IssueTime, ProviderToDate) + "\n");

                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_往来对账_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_往来对账_栏目, true);
                return;
            }
        }
        #endregion
    }
}
