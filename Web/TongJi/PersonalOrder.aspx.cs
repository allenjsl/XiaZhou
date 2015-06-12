using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 统计分析-个人业绩统计-订单数量查看弹窗
    /// </summary>
    /// 周文超 2012-04-24
    public partial class PersonalOrder : BackPage
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
        /// 合计金额实体
        /// </summary>
        protected Model.StatStructure.MPersonalOrderListTongJi SumMoney;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UtilsCommons.IsToXls()) ToXls();

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
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            SumMoney = new Model.StatStructure.MPersonalOrderListTongJi();
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetPersonalOrderListBySellerId(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            rptOrder.DataSource = list;
            rptOrder.DataBind();

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
        private Model.StatStructure.MPersonalSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MPersonalSearch
            {
                LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                SReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateS")),
                EReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateE")),
                SellerId = Utils.GetQueryStringValue("sellerId")
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

        /// <summary>
        /// 返回金额的百分比形式
        /// </summary>
        /// <param name="obj">金额</param>
        /// <param name="length">小数点后面的长度</param>
        /// <returns></returns>
        protected string GetBfbString(object obj, int length)
        {
            if (obj == null) return string.Empty;
            if (length <= 0) length = 0;

            return (Utils.GetDecimal(obj.ToString(), 0) * 100).ToString(string.Format("F{0}", length)) + "%";
        }

        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var tongJiInfo = new Model.StatStructure.MPersonalOrderListTongJi();
            int deptId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"));
            if (deptId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_查看全部))
            {
                deptId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            }
            var items = new BLL.StatStructure.BStatistics().GetPersonalOrderListBySellerId(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, deptId, this.GetSearchModel(), ref tongJiInfo);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            StringBuilder s = new StringBuilder();
            s.Append("序号\t团号\t订单号\t线路名称\t客户单位\t出团时间\t人数\t收入\t支出\t毛利\t毛利率\t下单人\n");
            int i = 1;

            foreach (var item in items)
            {
                s.Append(i + "\t");
                s.Append(item.TourCode + "\t");
                s.Append(item.OrderCode + "\t");
                s.Append(item.RouteName + "\t");
                s.Append(item.BuyCompanyName + "\t");
                s.Append(item.LDate.ToString("yyyy-MM-dd") + "\t");
                s.Append(item.PeopleNum + "\t");
                s.Append(item.TotalIncome.ToString("F2") + "\t");
                s.Append(item.TotalOutlay.ToString("F2") + "\t");
                s.Append(item.GrossProfit.ToString("F2") + "\t");
                s.Append(GetBfbString(item.GrossProfitRate, 2) + "\t");
                s.Append(item.Operator + "\n");

                i++;
            }

            ResponseToXls(s.ToString());
        }
    }
}
