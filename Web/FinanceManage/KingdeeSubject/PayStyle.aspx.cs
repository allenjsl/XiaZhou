using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.FinanceManage.KingdeeSubject
{
    public partial class PayStyle : BackPage
    {
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(CurrentUserCompanyID);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                recordCount = ls.Count;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
        }
    }
}
