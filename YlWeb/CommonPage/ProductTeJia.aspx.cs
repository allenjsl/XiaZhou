using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.CommonPage
{
    public partial class ProductTeJia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductTeJia1.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
        }
    }
}
