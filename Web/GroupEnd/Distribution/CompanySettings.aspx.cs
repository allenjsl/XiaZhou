using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.GroupEnd.Distribution
{
    /// <summary>
    /// 供应商平台-公司信息设置
    /// 创建时间：2011-10-9
    /// 创建者：王磊
    /// </summary>
    public partial class CompanySettings : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {

                    //公告
                    this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                    BindSource();

                }
            }

        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindSource()
        {
            EyouSoft.BLL.CrmStructure.BCrm bCrm = new EyouSoft.BLL.CrmStructure.BCrm();
            EyouSoft.Model.CrmStructure.MCrm mCrm = bCrm.GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
            //查询国家、省市区域的
            EyouSoft.BLL.ComStructure.BComCity bComCity = new EyouSoft.BLL.ComStructure.BComCity();
            EyouSoft.Model.ComStructure.MCPCC m = bComCity.GetCPCD(CurrentUserCompanyID, mCrm.CountryId, mCrm.ProvinceId, mCrm.CityId, mCrm.CountyId);
            if (m != null)
            {
                this.lblCountry.Text = m.CountryName;
                this.lblProvice.Text = m.ProvinceName;
                this.lblCity.Text = m.CityName;
                this.lblCounty.Text = m.CountyName;
            }

            this.lblName.Text = mCrm.Name;
            this.lblAddress.Text = mCrm.Address;
            this.lblOrganizationCode.Text = mCrm.OrganizationCode;
            this.lblLegalRepresentative.Text = mCrm.LegalRepresentative;
            this.lblLegalRepresentativeMobile.Text = mCrm.LegalRepresentativeMobile;
            this.lblLegalRepresentativePhone.Text = mCrm.LegalRepresentativePhone;
            this.lblLicense.Text = mCrm.License;
            this.lblFinancialName.Text = mCrm.FinancialName;
            this.lblFinancialPhone.Text = mCrm.FinancialPhone;
            this.lblFinancialMobile.Text = mCrm.FinancialMobile;

            //查询销售员
            EyouSoft.BLL.ComStructure.BComUser bUser = new EyouSoft.BLL.ComStructure.BComUser();
            EyouSoft.Model.ComStructure.MComUser user = bUser.GetModel(mCrm.SellerId, SiteUserInfo.CompanyId);
            if (null != user)
            {
                this.lblSeller.Text = user.UserName;
            }

            this.lblBrevityCode.Text = mCrm.BrevityCode;
            this.lblRebatePolicy.Text = mCrm.RebatePolicy;
        }

    }
}
