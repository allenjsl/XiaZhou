using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    /// <summary>
    /// 公共页面-上车地点选用
    /// </summary>
    /// 时间：2012-08-13
    public partial class PickUpPoint : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            EyouSoft.BLL.ComStructure.BComCarLocation BLL = new EyouSoft.BLL.ComStructure.BComCarLocation();
            string strLocation = Utils.GetQueryStringValue("text");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            IList<EyouSoft.Model.ComStructure.MComCarLocation> list = BLL.GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId, true, strLocation);
            if (list != null && list.Count > 0)
            {
                this.rpt_PickUpPoint.DataSource = list;
                this.rpt_PickUpPoint.DataBind();
                BindPage();
            }
            else
            {
                this.rpt_PickUpPoint.Controls.Add(new Label() { Text = "<tr><td colspan='5' align='center'>暂无数据</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
    }
}
