using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
namespace EyouSoft.Web.MarketCenter
{
    public partial class AdvancesList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        protected int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("sId")))
            {
                DataInit(Utils.GetQueryStringValue("sId"));
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string sId)
        {
            decimal payableSum=0;
            decimal paidSum=0;
            decimal returnedSum = 0;
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            IList<EyouSoft.Model.FinStructure.MSalesmanWarning> list = new EyouSoft.BLL.FinStructure.BFinance().GetSalesmanConfirmLstBySellerId(pageSize, pageIndex, ref recordCount, ref payableSum, ref paidSum, ref returnedSum, sId);
            if (list != null && list.Count > 0)
            {
                repList.DataSource = list;
                repList.DataBind();
                this.labSumMoney.Text = UtilsCommons.GetMoneyString((payableSum - paidSum + returnedSum), ProviderToMoney);
                BindPage(pageSize, pageIndex, recordCount);
            }
            else
            {
                this.repList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
                ExporPageInfoSelect1.Visible = false;
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
