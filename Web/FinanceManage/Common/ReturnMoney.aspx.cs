using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;

namespace Web.FinanceManage.Common
{
    /// <summary>
    /// 退款
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    /// 传值：单个退款传值OrderId（订单编号）,批量退款传值OrderId字符串
    /// 如：
    /// 单个OrderId=XXXXXXX
    /// 批量OrderId=XXXXX,YYYYYY,ZZZZZZ
    public partial class ReturnMoney : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
