using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.TongJi
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;

    /// <summary>
    /// 部门业绩统计统计图
    /// </summary>
    /// 周文超 2012-05-17
    public partial class DepartImg : BackPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private const int _pageSize = 100;
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
        /// 二级栏目编号
        /// </summary>
        protected int Sl = 0;

        /// <summary>
        /// 合计实体
        /// </summary>
        protected Model.StatStructure.MDepartmentTongJi SumMoney;

        /// <summary>
        /// Flash数据源XML
        /// </summary>
        protected string FlashDataXml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获取页面参数

            Sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));

            #endregion

            if (!IsPostBack)
            {
                PowerControl();
                InitData();
            }
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
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompanyId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_部门业绩统计_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            SumMoney = new Model.StatStructure.MDepartmentTongJi();
            var list = new BLL.StatStructure.BStatistics().GetDepartmentLst(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            InitFlashData(list);
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
        /// 构造flash数据源
        /// </summary>
        /// <param name="list">数据集合</param>
        private void InitFlashData(IList<Model.StatStructure.MDepartment> list)
        {
            if (list == null || !list.Any()) return;

            var strXml = new System.Text.StringBuilder();
            //订单数
            var strOrderNum = new System.Text.StringBuilder();
            //订单人数
            var strOrderPeopleNum = new System.Text.StringBuilder();
            //毛利
            var strMl = new System.Text.StringBuilder();

            strXml.Append("<graph");
            strXml.Append(" formatNumber='1' ");
            strXml.Append(" formatNumberScale='0' ");
            strXml.Append(" baseFont='Arial' ");
            strXml.Append(" baseFontSize='20' ");
            strXml.Append(" xaxisname='部门' ");
            strXml.Append(" yaxisname='人数金额' ");
            strXml.Append(" hovercapbg='DEDEBE' ");
            strXml.Append(" hovercapborder='889E6D' ");
            strXml.Append(" rotateNames='0' ");
            strXml.Append(" yAxisMinValue='0' ");
            strXml.Append(" yAxisMaxValue='0' ");
            strXml.Append(" numdivlines='9' ");
            strXml.Append(" divLineColor='CCCCCC' ");
            strXml.Append(" divLineAlpha='80' ");
            strXml.Append(" decimalPrecision='0' ");
            strXml.Append(" showAlternateHGridColor='1' ");
            strXml.Append(" AlternateHGridAlpha='30' ");
            strXml.Append(" AlternateHGridColor='CCCCCC' ");
            strXml.Append(" limitsDecimalPrecision='0' ");
            strXml.Append(" divLineDecimalPrecision='0' ");
            strXml.Append(" caption='部门业绩统计' ");
            strXml.Append(" subcaption='' ");
            strXml.Append(" > ");

            strXml.Append("<categories font='Arial' fontSize='15' fontColor='000000'>");
            strOrderNum.Append("<dataset seriesname='订单数' color='FDC12E'>");
            strOrderPeopleNum.Append("<dataset seriesname='订单人数' color='56B9F9'>");
            strMl.Append("<dataset seriesname='毛利' color='C9198D'>");
            foreach (var t in list)
            {
                if (t == null) continue;

                strXml.AppendFormat("<category name='{0}' hoverText='{0}'/>", t.DeptName);
                strOrderNum.AppendFormat("<set value='{0}' />", t.OrderNum);
                strOrderPeopleNum.AppendFormat("<set value='{0}' />", t.OrderPersonNum);
                strMl.AppendFormat("<set value='{0}' />", t.GrossProfit);
            }

            strXml.Append("</categories>");
            strOrderNum.Append("</dataset>");
            strOrderPeopleNum.Append("</dataset>");
            strMl.Append("</dataset>");

            strXml.Append(strOrderNum.ToString());
            strXml.Append(strOrderPeopleNum.ToString());
            strXml.Append(strMl.ToString());
            strXml.Append("</graph>");

            FlashDataXml = strXml.ToString();
        }
    }
}
