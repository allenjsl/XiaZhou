using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.CrmCenter
{
    /// <summary>
    /// 客户资料查看
    /// 创建人：钱琦 2012-4-10
    /// </summary>
    public partial class Details :EyouSoft.Common.Page.BackPage
    {
        EyouSoft.BLL.CrmStructure.BCrm crmBll = new EyouSoft.BLL.CrmStructure.BCrm();
        EyouSoft.BLL.ComStructure.BComCity cityBll = new EyouSoft.BLL.ComStructure.BComCity();
        EyouSoft.BLL.ComStructure.BComUser userBll = new EyouSoft.BLL.ComStructure.BComUser();
        protected int pageIndex = 1;
        protected int pageSize = 100;
        protected string filename = string.Empty;
        protected string filepath = string.Empty;
        static IDictionary<string, string> userListString = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userListString = new Dictionary<string, string>();
                Bind();
            }
        }

        private void Bind()
        {
            if (!string.IsNullOrEmpty(EyouSoft.Common.Utils.GetQueryStringValue("crmId")))
            {
                EyouSoft.Model.CrmStructure.MCrm crmModel = crmBll.GetInfo(EyouSoft.Common.Utils.GetQueryStringValue("crmId"));
                if (crmBll != null)
                {
                    lblAddress.Text = crmModel.Address;
                    lblBrevityCode.Text = crmModel.BrevityCode;
                    lblAmountOwed.Text = crmModel.AmountOwed.ToString("C");
                    EyouSoft.Model.ComStructure.MCPCC cpccModel = cityBll.GetCPCD(crmModel.CompanyId, crmModel.CountryId, crmModel.ProvinceId, crmModel.CityId, crmModel.CountyId);
                    lblCity.Text = cpccModel.CityName;
                    lblCountry.Text = cpccModel.CountryName;
                    lblCounty.Text = cpccModel.CountyName;
                    lblProvince.Text = cpccModel.ProvinceName;
                    lblDeadline.Text = crmModel.Deadline.ToString();
                    lblFinancialMobile.Text = crmModel.FinancialMobile;
                    lblFinancialName.Text = crmModel.FinancialName;
                    lblFinancialPhone.Text = crmModel.FinancialPhone;
                    lblIsSignContract.Text = (!crmModel.IsSignContract) ? "否" : "是";
                    lblLegalRepresentative.Text = crmModel.LegalRepresentative;
                    lblLegalRepresentativeMobile.Text = crmModel.LegalRepresentativeMobile;
                    lblLegalRepresentativePhone.Text = crmModel.LegalRepresentativePhone;
                    lblLicense.Text = crmModel.License;
                    lblName.Text = crmModel.Name;
                    lblOrganizationCode.Text = crmModel.OrganizationCode;
                    //lblRebatePolicy.Text = crmModel.RebatePolicy;                    
                    lblSellerId.Text = crmModel.SellerName;
                    if (crmModel.BankList != null && crmModel.BankList.Count > 0)
                    {
                        rptBank.DataSource = crmModel.BankList;
                        rptBank.DataBind();
                    }
                    else
                    {
                        phYinHangZhangHuEmpty.Visible = true;
                    }

                    if (crmModel.LinkManList != null && crmModel.LinkManList.Count > 0)
                    {
                        for (int i=0;i<crmModel.LinkManList.Count;i++)
                        {
                            EyouSoft.Model.ComStructure.MComUser userModel = userBll.GetModel(crmModel.LinkManList[i].UserId, base.SiteUserInfo.CompanyId);
                            string html = string.Empty;
                            if (userModel != null)
                            {
                                html = string.Format("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>帐号</th><th align='center'>密码</th><th height='23' align='center'>状态</th></tr><tr ><td align='center'>{0}</td><td align='center'>{1}</td><td align='center'>{2}</td></tr></table>", userModel.UserName, userModel.Password, userModel.UserStatus.ToString());
                                userListString.Add(crmModel.LinkManList[i].UserId, html);
                            }                            
                        }
                        
                        rptLinkMan.DataSource = crmModel.LinkManList;
                        rptLinkMan.DataBind();
                    }
                    if (crmModel.AttachModel != null)
                    {
                        filename = crmModel.AttachModel.Name;
                        filepath = crmModel.AttachModel.FilePath;
                    }
                }
            }
        }

        protected string GetAccountList(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return string.Empty;
            string value = string.Empty;
            userListString.TryGetValue(userId, out value);
            return value;
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
    }
}
