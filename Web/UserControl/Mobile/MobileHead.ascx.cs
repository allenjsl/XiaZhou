using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.UserControl.Mobile
{
    public partial class MobileHead : System.Web.UI.UserControl
    {
        private string _logoImgUrl;
        /// <summary>
        /// 设置logo图片的URL
        /// </summary>
        public string LogoImgUrl
        {
            get { return _logoImgUrl; }
            set { _logoImgUrl = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EyouSoft.Model.SysStructure.MSysDomain sysDomain = EyouSoft.Security.Membership.UserProvider.GetDomain();
                if (sysDomain != null)
                {
                    EyouSoft.Model.ComStructure.MComSetting settingModel = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(sysDomain.CompanyId);
                    if (settingModel != null) {
                        this.LogoImgUrl = settingModel.MLogo;
                    }
                }
                if (this.LogoImgUrl != "")
                {
                    this.litLogo.Text = "<img src='" + this.LogoImgUrl + "' border='0' alt='' />";
                }
            }
        }
    }
}