using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace Enow.Finawin.Web.Install
{
    public partial class SystemExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpdateConfig_Click(object sender, System.EventArgs e)
        {
            string strErr = "";
            string LicenseNumber = this.txtSnNumber.Text;
            if (LicenseNumber == null || String.Empty == LicenseNumber)
                strErr = "请填写时间注册码!";
            else
                LicenseNumber = LicenseNumber.Trim();
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            else
            {
                Adpost.Common.Function.ConfigModel.SetConfigKeyValue("EyouSoftUD", LicenseNumber);
                //Adpost.Finawin.Utility.AppsettingConfigUtils.UpdateAppConfig("FinwinUD", LicenseNumber);
                MessageBox.ShowAndRedirect(this, "配置成功!", "/");
                return;
            }
        }
    }
}