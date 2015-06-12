using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.GroupEnd.Suppliers
{
    public partial class CompanySettings : EyouSoft.Common.Page.SupplierPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;
                    BindSource();
                }
            }

        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindSource()
        {

            EyouSoft.BLL.SourceStructure.BSource bSource = new EyouSoft.BLL.SourceStructure.BSource();

            EyouSoft.Model.SourceStructure.MSourceTravel sourceTravel = bSource.GetTravelServiceModel(SiteUserInfo.SourceCompanyInfo.CompanyId);
            if (sourceTravel != null)
            {
                //查询国家、省市区域
                EyouSoft.BLL.ComStructure.BComCity bComCity = new EyouSoft.BLL.ComStructure.BComCity();
                EyouSoft.Model.ComStructure.MCPCC m = bComCity.GetCPCD(CurrentUserCompanyID, sourceTravel.SourceModel.CountryId, sourceTravel.SourceModel.ProvinceId, sourceTravel.SourceModel.CityId, sourceTravel.SourceModel.CountyId);
                if (m != null)
                {
                    this.lblCountry.Text = m.CountryName;
                    this.lblProvice.Text = m.ProvinceName;
                    this.lblCity.Text = m.CityName;
                    this.lblCounty.Text = m.CountyName;

                    this.lblType.Text = m.CountryName == "中国" ? "国内" : "国外";

                }
                this.lblName.Text = sourceTravel.SourceModel.Name;


                this.lblLicense.Text = sourceTravel.SourceModel.LicenseKey;
                this.lblLegalRepresentative.Text = sourceTravel.SourceTravelModel.LegalRepresentative;
                this.lblLegalRepresentativePhone.Text = sourceTravel.SourceTravelModel.Telephone;
                this.lblAddress.Text = sourceTravel.SourceModel.Address;

                this.lblIsSignContract.Text = sourceTravel.SourceModel.IsSignContract == true ? "是" : "否";
                this.lblEffectTime.Text = sourceTravel.SourceModel.ContractPeriodEnd != null ? sourceTravel.SourceModel.ContractPeriodEnd.Value.ToString("yyyy-MM-dd") : string.Empty;

                this.lblFeatureRoute.Text = sourceTravel.SourceTravelModel.Routes;

                this.lblIsSign.Text = sourceTravel.SourceModel.IsPermission == true ? "是" : "否";

                this.lblIsRecommend.Text = sourceTravel.SourceModel.IsRecommend == true ? "是" : "否";

                this.lblIsRebatePolicy.Text = sourceTravel.SourceModel.IsCommission==true ? "是" : "否";

                this.lblRebatePolicy.Text = sourceTravel.SourceModel.UnitPolicy;

            }
        }
    }
}
