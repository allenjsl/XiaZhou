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
using EyouSoft.Model.EnumType.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using System.Text;

namespace Web.FinanceManage.Statistics
{
    /// <summary>
    /// 日记帐-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class DiaryList : BackPage
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
            MDayRegisterBase queryString = new MDayRegisterBase();
            queryString.LDateS = Utils.GetQueryStringValue("LDateS");
            queryString.LDateE = Utils.GetQueryStringValue("LDateE");
            queryString.Summary = Utils.GetQueryStringValue("Summary");
            int paymentId = Utils.GetIntSign(Utils.GetQueryStringValue("PaymentId"), -1);
            if (paymentId >= 0)
            {
                queryString.PaymentId = paymentId;
            }
            queryString.SignAmount = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            queryString.Amount = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber)); 
            queryString.CompanyId = CurrentUserCompanyID;
            #endregion
            MDayRegister sumModel = new MDayRegister();

            IList<MDayRegister> ls = new BFinance().GetDayRegisterLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref  sumModel,
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
        private void BindSum(MDayRegister sumModel)
        {
            lbl_debitCash.Text = UtilsCommons.GetMoneyString(sumModel.DebitCash, ProviderToMoney);
            lbl_debitBank.Text = UtilsCommons.GetMoneyString(sumModel.DebitBank, ProviderToMoney);
            lbl_lenderCash.Text = UtilsCommons.GetMoneyString(sumModel.LenderCash, ProviderToMoney);
            lbl_lenderBank.Text = UtilsCommons.GetMoneyString(sumModel.LenderBank, ProviderToMoney);
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
            MDayRegister sumModel = new MDayRegister();
            int recordCount = 0;
            IList<MDayRegister> ls = new BFinance().GetDayRegisterLst(
               UtilsCommons.GetToXlsRecordCount(),
               1,
               ref recordCount,
               ref  sumModel,
               new MDayRegisterBase
               {
                   CompanyId = CurrentUserCompanyID
               });
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n",
                    "时间",
                    "摘要",
                    "借方现金",
                    "借方银行存款",
                    "贷方现金",
                    "贷方银行存款");

                foreach (MDayRegister item in ls)
                {
                    sb.Append(UtilsCommons.GetDateString(item.LDate, ProviderToDate) + "\t");
                    sb.Append(item.Summary + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.DebitCash, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.DebitBank, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.LenderCash, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.LenderBank, ProviderToMoney) + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }
        #endregion
    }
}
