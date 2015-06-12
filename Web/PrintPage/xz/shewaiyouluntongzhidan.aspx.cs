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
    /// 涉外游轮通知单
    /// 方琪 2012-05-15
    /// </summary>
    public partial class shewaiyouluntongzhidan : System.Web.UI.Page
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
            this.Title = PrintTemplateType.涉外游轮确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.PlanStructure.BPlan BLL = new EyouSoft.BLL.PlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, planId);

            if (model != null)
            {
                //名称/联系人
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
                //船名
                this.lbShipName.Text = model.PlanShip.ShipName;
                //登船日期
                this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                //登船码头
                this.lbLoadDock.Text = model.PlanShip.LoadDock;
                //航行
                this.lbLine.Text = model.PlanShip.Line;
                //停靠景点
                this.lbSight.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.PlanShip.Sight);
                //费用明细
                this.lbCostDetail.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CostDetail);
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //游客信息
                this.lbCustomerInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CustomerInfo);
                //登船号
                this.lbLoadCode.Text = model.PlanShip.LoadCode;
                //签发日期
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
