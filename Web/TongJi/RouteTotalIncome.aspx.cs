using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 统计分析--线路流量统计-总收入查看弹窗
    /// </summary>
    /// 周文超 2012-04-24
    public partial class RouteTotalIncome : BackPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private const int _pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int _recordCount = 0;

        #endregion

        /// <summary>
        /// 合计金额
        /// </summary>
        protected string SumIncome = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                InitData();
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

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_线路流量统计_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_线路流量统计_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"));
            if (sunCompanyId <= 0 || (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_线路流量统计_查看全部)))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetRouteFlowtOrderListByAreaId(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                ref SumIncome,
                sunCompanyId,
                GetSearchModel());

            rptIncome.DataSource = list;
            rptIncome.DataBind();

            //绑定分页
            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = _pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = _pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = _recordCount;
        }

        /// <summary>
        /// 通过查询参数返回查询实体
        /// </summary>
        /// <returns>查询实体</returns>
        private Model.StatStructure.MRouteFlowSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MRouteFlowSearch
            {
                AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId")),
                LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                SReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateS")),
                EReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateE"))
            };

            return search;
        }

        /// <summary>
        /// 计算序号
        /// </summary>
        /// <param name="currIndex">当前行的索引</param>
        /// <returns></returns>
        protected string GetXh(int currIndex)
        {
            return (_pageSize * (_pageIndex - 1) + currIndex).ToString();
        }
    }
}
