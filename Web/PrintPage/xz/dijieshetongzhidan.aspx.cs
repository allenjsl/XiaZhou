using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 地接社通知单
    /// 方琪 2012-05-15
    /// </summary>
    public partial class dijieshetongzhidan : System.Web.UI.Page
    {
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        protected void Page_Load(object sender, EventArgs e)
        {
            string planId = Utils.GetQueryStringValue("planId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(planId) && SiteUserInfo != null)
            {
                InitPage(planId);
            }
            this.Title = PrintTemplateType.地接确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.PlanStructure.BPlan BLL = new EyouSoft.BLL.PlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, planId);

            if (model != null)
            {
                //地接社名称/联系人
                this.txtCompanyName.Text = model.SourceName;
                this.txtCompanyContactName.Text = model.ContactName;
                this.txtContact.Text = model.ContactPhone;
                this.txtFax.Text = model.ContactFax;
                //公司名、联系人
                this.txtSelfName.Text = this.SiteUserInfo.CompanyName;
                this.txtSelfContactName.Text = this.SiteUserInfo.Name;
                this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
                this.txtSelfFax.Text = this.SiteUserInfo.Fax;
                //线路名称
                //this.lbRouteName.Text = model.RouteName;
                //团号
                this.lbTourCode.Text = model.TourCode;
                //人数
                this.lbPersonNum.Text = model.Num.ToString();
                //接团日期
                this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                //送团日期
                this.lbEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "";
                //接待行程
                this.lbReceiveJourney.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ReceiveJourney);
                //服务标准
                this.lbServiceStandard.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard);
                //费用明细
                this.lbCostDetail.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CostDetail);
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注                                 
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //游客信息
                this.lbCustomerInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CustomerInfo);
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
