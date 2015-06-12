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
    /// 统计分析--收入对账单-未收查看弹窗
    /// </summary>
    /// 周文超 2012-04-24
    public partial class RestAmount : BackPage
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
        /// 合计信息实体
        /// </summary>
        protected Model.StatStructure.MReconciliationTongJi SumMoney;

        protected void Page_Load(object sender, EventArgs e)
        {
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel();

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
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            SumMoney = new Model.StatStructure.MReconciliationTongJi();
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompanyId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetReconciliationRestAmountLst(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            rptRestAmount.DataSource = list;
            rptRestAmount.DataBind();

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
        private Model.StatStructure.MReconciliationSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MReconciliationSearch
                {
                    LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                    LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                    RestAmount = Utils.GetDecimalNull(Utils.GetQueryStringValue("Money")),
                    SellerId = Utils.GetQueryStringValue("sellerId"),
                    DeptId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"))
                };
            search.EqualSign = (Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue("Calculation"));

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
        /// 导出Excel
        /// </summary>
        private void ListToExcel()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            SumMoney = new Model.StatStructure.MReconciliationTongJi();
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompanyId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            s.Append("序号\t订单号\t线路名称\t出团时间\t客户单位\t人数\t应收款\t已收款\t未收款\n");
            var list = new BLL.StatStructure.BStatistics().GetReconciliationRestAmountLst(
                SiteUserInfo.CompanyId,
                toXlsRecordCount,
                1,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            if (list != null && list.Any())
            {
                int index = 1;
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
                        index,
                        t.OrderCode,
                        t.RouteName,
                        UtilsCommons.GetDateString(t.LDate, ProviderToDate),
                        t.BuyCompanyName,
                        t.PeopleNum,
                        UtilsCommons.GetMoneyString(t.TotalAmount, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.InAmount, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.RestAmount, ProviderToMoney));

                    index++;
                }
                s.AppendFormat(
                    "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
                    " ",
                    " ",
                    " ",
                    " ",
                    " ",
                    "合计：",
                    UtilsCommons.GetMoneyString(SumMoney.TotalAmount, ProviderToMoney),
                    UtilsCommons.GetMoneyString(SumMoney.InAmount, ProviderToMoney),
                    UtilsCommons.GetMoneyString(SumMoney.RestAmount, ProviderToMoney));
            }

            ResponseToXls(s.ToString());
        }
    }
}
