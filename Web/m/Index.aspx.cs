using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.m
{
    /// <summary>
    /// 手机版-首页
    /// 创建人：赵晓慧
    /// 创建时间:2012-07-09
    /// </summary>
    public partial class Index : EyouSoft.Common.Page.MobilePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目))
            {
                Utils.MobileResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目);
                return;
            }
        }
    }
}
