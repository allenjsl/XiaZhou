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
    /// 统计分析--部门业绩统计-查看部门业绩弹窗
    /// </summary>
    /// 周文超 2012-05-17
    public partial class DepartSeller : BackPage
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
        protected Model.StatStructure.MDepartmentPeopleListTongJi SumMoney;

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
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_部门业绩统计_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_部门业绩统计_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            SumMoney = new Model.StatStructure.MDepartmentPeopleListTongJi();
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompanyId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_部门业绩统计_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetDepartmentPeopleListByDeptId(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            rptSeller.DataSource = list;
            rptSeller.DataBind();

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
        private Model.StatStructure.MDepartmentSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MDepartmentSearch
                {
                    LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                    LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                    SReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateS")),
                    EReviewTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckDateE")),
                    DeptId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"))
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

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ListToExcel()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            SumMoney = new Model.StatStructure.MDepartmentPeopleListTongJi();
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompanyId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_部门业绩统计_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            s.Append("序号\t姓名\t订单人数\t订单数量\t收入\t支出\t毛利\t毛利率\n");
            var list = new BLL.StatStructure.BStatistics().GetDepartmentPeopleListByDeptId(
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
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n",
                        index,
                        t.SellerName,
                        t.PeopleNum,
                        t.OrderNum,
                        UtilsCommons.GetMoneyString(t.TotalIncome, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.TotalOutlay, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.GrossProfit, ProviderToMoney),
                        GetBfbString(t.GrossProfitRate, 0));

                    index++;
                }
                s.AppendFormat(
                    "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n",
                    " ",
                    "合计：",
                    SumMoney.PeopleNum,
                    SumMoney.OrderNum,
                    UtilsCommons.GetMoneyString(SumMoney.InCome, ProviderToMoney),
                    UtilsCommons.GetMoneyString(SumMoney.Pay, ProviderToMoney),
                    UtilsCommons.GetMoneyString(SumMoney.GrossProfit, ProviderToMoney),
                    "");
            }

            ResponseToXls(s.ToString());
        }
    }
}
