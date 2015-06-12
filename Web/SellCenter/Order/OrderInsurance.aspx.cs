using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;

namespace Web.SellCenter.Order
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-29
    /// 说明：订单游客 保险
    public partial class OrderInsurance : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }
        private void DataInit()
        {
            IList<EyouSoft.Model.ComStructure.MComInsurance> list = new EyouSoft.BLL.ComStructure.BComInsurance().GetList(SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
    }
}
