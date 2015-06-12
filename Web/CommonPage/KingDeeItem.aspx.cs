using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Common.Page;
using EyouSoft.Model.FinStructure;

namespace EyouSoft.Web.CommonPage
{
    /// <summary>
    /// 核算科目 选用列表
    /// </summary>
    public partial class KingDeeItem : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<MKingDeeChk> ls = new BFinance().GetKingDeeChkLst(CurrentUserCompanyID,"0");
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
        }
    }
}
