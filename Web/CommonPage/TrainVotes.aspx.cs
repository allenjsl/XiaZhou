using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.CommonPage
{
    /// <summary>
    /// 计调安排—火车出票点选用页面
    /// 创建人：李晓欢
    /// 创建时间:2011-09-26
    /// </summary>
    public partial class TrainVotes : Eyousoft.Common.Page.BackPage
    {
        protected string votes = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
            //出票点
            votes = Utils.GetQueryStringValue("Txtvotes");
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            //大交通出票点选择
            if (Utils.GetQueryStringValue("Meth") == "Agency")
            {

            }
            else if (Utils.GetQueryStringValue("Meth") == "Team") //团队计调 大交通出票点选择
            {

            }
            else //出境大交通出票点选择
            {

            }
        }

        #region 设置弹出窗体
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = Eyousoft.Common.Page.PageType.boxyPage;
        }
        #endregion
    }
}
