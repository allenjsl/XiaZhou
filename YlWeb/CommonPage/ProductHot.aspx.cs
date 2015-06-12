using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.CommonPage
{
    public partial class ProductHot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductHot1.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
        }
    }
}