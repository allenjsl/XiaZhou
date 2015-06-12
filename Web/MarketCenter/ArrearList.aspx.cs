using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using System.Xml.Linq;
using EyouSoft.Model.StatStructure;

namespace EyouSoft.Web.MarketCenter
{
    /// <summary>
    ///  拖欠金额
    /// </summary>
    public partial class ArrearList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            lbl_Title.Text = SiteUserInfo.CompanyName + "【与 <b class=fontred>" + Utils.GetQueryStringValue("crmName") + "</b> 的核算单】";
            string crmId = Utils.GetQueryStringValue("crmId");
            #region 分页参数
            int pageSize = 10;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1); ;
            int recordCount = 0;
            #endregion
            MBatchWriteOffOrder sumModel = new MBatchWriteOffOrder();
            IList<MBatchWriteOffOrder> ls = new BFinance().GetArrearOrderLstByCrmId(pageSize, pageIndex, ref recordCount, ref sumModel, crmId);
            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            //绑定统计数据
            BindSum(sumModel);

            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定统计数据
        /// </summary>
        /// <param name="xmlSum">xml类型的字符串</param>
        private void BindSum(MBatchWriteOffOrder sumModel)
        {
            if (sumModel != null)
            {
                //应付
                lbl_receivable.Text = UtilsCommons.GetMoneyString(sumModel.Receivable, ProviderToMoney);
                //已付
                lbl_received.Text = UtilsCommons.GetMoneyString(sumModel.Received, ProviderToMoney);
                //未付
                lbl_unreceivable.Text = UtilsCommons.GetMoneyString(sumModel.Unreceivable, ProviderToMoney);
            }
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
    }
}
