using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.GroupEnd.Suppliers
{
    /// <summary>
    /// 供应商财务管理
    /// 
    /// </summary>
    public partial class FinanceList : SupplierPage
    {

        protected int pageSize = 20;

        protected int pageIndex = 1;

        private int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    //公告
                    this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;

                    BindSource();
                }
            }
        }

        /// <summary>
        /// 绑定数据源、分页控件
        /// </summary>
        private void BindSource()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            //应付
            EyouSoft.Model.FinStructure.MPayableBase search = new EyouSoft.Model.FinStructure.MPayableBase();
            search.CompanyId = SiteUserInfo.CompanyId;
            search.SupplierId = SiteUserInfo.SourceCompanyInfo.CompanyId;
            search.Supplier = SiteUserInfo.SourceCompanyInfo.CompanyName;
            search.IsDj = true;

            search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            search.AreaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"), 0);
            search.Planer = Utils.GetQueryStringValue("txtPlaner");
            search.LDateStart = Utils.GetQueryStringValue("txtBeginLDate");
            search.LDateEnd = Utils.GetQueryStringValue("txtEndLDate"); 
            search.SignUnpaid = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(this.CaiWuShaiXuan1.ClientUniqueIDOperator));
            search.Unpaid = Utils.GetDecimalNull(Utils.GetQueryStringValue(this.CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));

            //统计实体
            EyouSoft.Model.FinStructure.MPayableSum mSum = new EyouSoft.Model.FinStructure.MPayableSum();

            EyouSoft.BLL.FinStructure.BFinance bFinance = new EyouSoft.BLL.FinStructure.BFinance();
            IList<EyouSoft.Model.FinStructure.MPayable> list = bFinance.GetPayableLst(pageSize, pageIndex, ref recordCount, ref mSum, search);
            this.RpFinance.DataSource = list;
            this.RpFinance.DataBind();

            //统计
            this.LtTotalPaid.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mSum.TotalPaid, this.ProviderToMoney);
            this.LtTotalPayable.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mSum.TotalPayable, this.ProviderToMoney);
            this.LtTotalUnpaid.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(System.Math.Abs(mSum.TotalPaid - mSum.TotalPayable), this.ProviderToMoney);



            BindPage();
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.PhSumPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.PhSumPage.Visible = true;
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }
    }
}
