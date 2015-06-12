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
using System.Text;

namespace Web.FinanceManage.ComeAndGoReconciliation
{
    /// <summary>
    /// 今日收款-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class GatheringList : BackPage
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
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            MAuditBase queryModel = new MAuditBase();
            queryModel.IssueTimeS = Utils.GetQueryStringValue("SDate");
            queryModel.IssueTimeE = Utils.GetQueryStringValue("EDate");
            queryModel.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            queryModel.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryModel.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            queryModel.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryModel.SignAmount = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            queryModel.Amount = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));
            decimal sum = 0;
            IList<MReconciliation> ls = new BFinance().GetReconciliationLst(
                  pageSize,
                  pageIndex,
                  ref recordCount,
                  ref sum,
                  ReconciliationType.今日收款,
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
                ReconciliationType.今日收款,
                CurrentUserCompanyID,
                new MAuditBase());
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
                    "订单号",
                    "客户单位",
                    "销售员",
                    "线路名称",
                    "收款金额",
                    "财务人",
                    "审核时间");

                foreach (MReconciliation item in ls)
                {
                    //sb.Append("'" + item.OrderCode + "\t");
                    sb.Append(item.OrderCode + "\t");
                    sb.Append(item.Crm + "\t");
                    sb.Append(item.SellerName + "\t");
                    sb.Append(item.RouteName + "\t");
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
