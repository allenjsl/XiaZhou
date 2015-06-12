using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.FinanceManage.Common
{
    /// <summary>
    /// 团队下面所有订单列表
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-5-2
    public partial class OrderList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<MTourOrderDisInfo> ls = new BTour().GetTourOrderDisList(Utils.GetQueryStringValue("tourId"));
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
        }
    }
}
