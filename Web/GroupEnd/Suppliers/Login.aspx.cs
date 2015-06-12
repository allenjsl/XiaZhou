using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.GroupEnd.Suppliers
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //公告
                //Notice1.CompanyId = SiteUserInfo.CompanyId;
                Notice1.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.供应商; 
            }
        }
    }
}
