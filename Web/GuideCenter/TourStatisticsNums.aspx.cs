using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.GuideCenter
{
    /// <summary>
    /// 导游带团查看 lixh 2012-04-17
    /// </summary>
    public partial class TourStatisticsNums : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int PageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int RecordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            DataInit();
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void DataInit()
        {
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            string Id = Utils.GetQueryStringValue("ID");
            if (!string.IsNullOrEmpty(Id))
            {
                IList<EyouSoft.Model.SourceStructure.MGuideTourList> list = new EyouSoft.BLL.SourceStructure.BSource().GetGuideTourList(Id, startTime, endTime, null, null, this.SiteUserInfo.CompanyId, PageIndex, PageSize, ref RecordCount);
                if (list != null && list.Count > 0)
                {
                    this.repList.DataSource = list;
                    this.repList.DataBind();
                    BindPage();
                }
                else
                {

                    this.lab_msg.Text = "<tr><td height=\"28px\" bgcolor=\"#FFFFFF\" colspan=\"6\" align=\"center\">暂无数据</td></tr>";
                    this.ExporPageInfoSelect1.Visible = false;
                }
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }
        #endregion

        #region 弹窗页面设置
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion
    }
}
