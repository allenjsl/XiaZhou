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
    /// 导游中心-导游带团次数
    /// lixh 2012-04-16
    /// </summary>
    public partial class GuidRecordNums : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int PageSize = 10;
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

        protected void DataInit()
        {
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            string Guid = Utils.GetQueryStringValue("Guid");
            if (!string.IsNullOrEmpty(Guid))
            {
                DateTime? StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
                DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
                IList<EyouSoft.Model.SourceStructure.MGuideTourList> list = new EyouSoft.BLL.SourceStructure.BSource().GetGuideTourList(Guid, null, null, StartTime, endTime, this.SiteUserInfo.CompanyId, PageIndex, PageSize, ref RecordCount);
                if (list != null && list.Count > 0)
                {
                    this.replist.DataSource = list;
                    this.replist.DataBind();
                    BindPage();
                }
                else
                {
                    this.lab_Msg.Text = "<tr><td height=\"28px\" bgcolor=\"#FFFFFF\" colspan=\"6\" align=\"center\">暂无数据</td></tr>";
                    this.lab_Msg.Visible = true;
                    this.ExporPageInfoSelect1.Visible = false;
                }
                list = null;
            }
        }

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
