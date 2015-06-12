using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common.Page;

namespace Web.ResourceCenter.CommonPage
{
    /// <summary>
    /// 酒店预控-总控
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-23
    public partial class Control : BackPage
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
