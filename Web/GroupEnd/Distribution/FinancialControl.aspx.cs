using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.GroupEnd.Distribution
{
    /// <summary>
    /// 供应商平台-财务管理
    /// 创建时间：2011-10-9
    /// 创建者：王磊
    /// 2012-12-18 郑知远 查询条件去掉责任销售SalesmanId，新增是否显示在同行端IsShowDistribution
    /// </summary>
    public partial class FinancialControl : FrontPage
    {
        #region 分页参数配置
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                PageInit();
            }

        }

        /// <summary>
        /// 绑定控件
        /// </summary>
        private void PageInit()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            EyouSoft.Model.FinStructure.MReceivableBase search = new EyouSoft.Model.FinStructure.MReceivableBase();
            search.CompanyId = SiteUserInfo.CompanyId;
            search.CustomerId = SiteUserInfo.TourCompanyInfo.CompanyId;
            search.Customer = SiteUserInfo.TourCompanyInfo.CompanyName;
            //search.SalesmanId = SiteUserInfo.UserId;
            search.IsShowDistribution = true;

            search.OrderCode = Utils.GetQueryStringValue("txtOrderCode");
            search.RouteName = Utils.GetQueryStringValue("txtRouteName");

            string jieQingStatus = Utils.GetQueryStringValue("ddlIsClean");
            if (jieQingStatus == "1") search.IsClean = true;
            else if (jieQingStatus == "0") search.IsClean = false;

            //统计实体
            EyouSoft.Model.FinStructure.MReceivableSum mSum = new EyouSoft.Model.FinStructure.MReceivableSum();

            EyouSoft.BLL.FinStructure.BFinance bFinance = new EyouSoft.BLL.FinStructure.BFinance();
            IList<EyouSoft.Model.FinStructure.MReceivableInfo> list = bFinance.GetReceivableInfoLst(pageSize, pageIndex, ref recordCount, ref mSum, search);
            this.RpFinancial.DataSource = list;
            this.RpFinancial.DataBind();

            //合计
            this.LtTotalReceived.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mSum.TotalReceived, this.ProviderToMoney);
            this.LtTotalSumPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mSum.TotalSumPrice, this.ProviderToMoney);
            this.LtTotalUnReceived.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mSum.TotalUnReceived, this.ProviderToMoney);


            BindPage();


        }


        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        protected string GetFinancialStatus(string select)
        {
            System.Text.StringBuilder option = new System.Text.StringBuilder();

            option.Append("<option value=''>请选择</option>");
            if (string.IsNullOrEmpty(select))
            {
                option.Append("<option value='0'>未结清</option>");
                option.Append("<option value='1'>已结清</option>");
            }
            else
            {
                if (select=="0")
                {
                    option.Append("<option value='0' selected='selected'>未结清</option>");
                    option.Append("<option value='1'>已结清</option>");
                }
                else
                {
                    option.Append("<option value='0'>未结清</option>");
                    option.Append("<option value='1' selected='selected' >已结清</option>");
                }
            }
            return option.ToString();
        }


    }
}
