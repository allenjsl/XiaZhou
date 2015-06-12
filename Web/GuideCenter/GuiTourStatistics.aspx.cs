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
    /// 导游中心—上团统计
    /// 创建人：李晓欢
    /// 创建时间：2011-09-14
    /// </summary>
    public partial class GuiTourStatistics : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        protected int pageCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限
            PowerControl();
            //导出
            if (UtilsCommons.IsToXls())
            { ToXls(); }
            //初始化
            DataInit();

        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        protected void ToXls()
        {
            System.Text.StringBuilder _s = new System.Text.StringBuilder();
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            //导游姓名
            string guidName = Utils.GetQueryStringValue("txtGuidName");
            //上团时间
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            //下团时间
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));

            IList<EyouSoft.Model.SourceStructure.MGuideListGroup> GuidList = new EyouSoft.BLL.SourceStructure.BSource().GetGuideListGroup(guidName, startTime, endTime, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
            _s.Append("导游姓名\t团队数\t团天数\t评分（质量管理中导游总分/评分次数\n");
            if (GuidList != null && GuidList.Count > 0)
            {
                foreach (var item in GuidList)
                {
                    _s.AppendFormat("{0}\t{1}\t{2}\t{3}\n",
                        item.Name,
                        item.GroupCount,
                        item.DayCount,
                        item.Score);
                }
            }
            ResponseToXls(_s.ToString());
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            //导游姓名
            string guidName = Utils.GetQueryStringValue("txtGuidName");
            //上团时间
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            //下团时间
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));

            IList<EyouSoft.Model.SourceStructure.MGuideListGroup> GuidList = new EyouSoft.BLL.SourceStructure.BSource().GetGuideListGroup(guidName, startTime, endTime, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
            if (GuidList != null && GuidList.Count > 0)
            {
                this.replist.DataSource = GuidList;
                this.replist.DataBind();
                BindPage();
            }
            else
            {
                this.lab_text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_上团统计_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_上团统计_栏目, false);
                return;
            }
        }
        #endregion

    }
}
