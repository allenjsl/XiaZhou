using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;
using System.Web.UI.HtmlControls;
using EyouSoft.Common;

namespace Web.FinanceManage.Common
{
    /// <summary>
    /// 收款
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    /// 传值：单个收款传值OrderId（订单编号）,批量收款传值OrderId字符串
    /// 如：
    /// 单个OrderId=XXXXXXX
    /// 批量OrderId=XXXXX,YYYYYY,ZZZZZZ
    public partial class SetMoney : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string a = Utils.GetQueryStringValue("iframeId");
            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
