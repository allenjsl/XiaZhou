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
    /// 统计分析--收入对账单
    /// </summary>
    /// 周文超 2012-04-24
    public partial class Income : BackPage
    {
        #region attributes

        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private const int _pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int _recordCount = 0;        

        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int Sl = 0;

        /// <summary>
        /// 合计实体
        /// </summary>
        protected Model.StatStructure.MReconciliationTongJi SumMoney;

        /// <summary>
        /// 分公司编号,取查询参数分公司编号,无查看全部权限时为当前登录用户所在部门的分公司编号
        /// </summary>
        int FenGongSiId = 0;
        /// <summary>
        /// 是否查看所有
        /// </summary>
        bool Privs_ChaKanSuoYou = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel();

            #region 获取页面参数

            Sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            FenGongSiId = Utils.GetInt(Utils.GetQueryStringValue("SunCompan"));

            #endregion

            if (!IsPostBack)
            {
                PowerControl();
                BindSunCompany();
                InitData();
            }
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

            Privs_ChaKanSuoYou = CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_查看全部);

            if (FenGongSiId <= 0 || !Privs_ChaKanSuoYou)
            {
                FenGongSiId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            }
            /*else
            {
                var list = new BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);

                if (list != null && list.Count > 0)
                {
                    if (!list.Any(item => item.PrevDepartId <= 0 && item.DepartId == FenGongSiId))
                    {
                        FenGongSiId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
                    }
                }
            }*/
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            SumMoney = new Model.StatStructure.MReconciliationTongJi();
            var list = new BLL.StatStructure.BStatistics().GetReconciliationLst(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                FenGongSiId,
                this.GetSearchModel(),
                ref SumMoney);

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
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = _pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = _pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = _recordCount;
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
                    RestAmount = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber))
                };

            search.EqualSign = (Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));

            //销售员
            string sellerId = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;
            int departId = Utils.GetInt(Utils.GetQueryStringValue(SelectSection1.SelectIDClient));
            string departName = Utils.GetQueryStringValue(SelectSection1.SelectNameClient);
            SelectSection1.SectionID = departId.ToString();
            SelectSection1.SectionName = departName;

            search.DeptId = departId;
            search.SellerId = sellerId;
            search.SellerName = sellerName;

            return search;
        }

        /// <summary>
        /// 绑定分公司
        /// </summary>
        private void BindSunCompany()
        {
            //一级部门即为分公司
            var list = new BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);
            if (list != null) list = list.Where(t => (t.PrevDepartId <= 0)).ToList();
            string strHtml = string.Empty;
            if (list != null && list.Any())
            {
                foreach (var t in list)
                {
                    if (t == null) continue;

                    //有查看所有的权限，显示所有的分公司
                    if (Privs_ChaKanSuoYou)
                    {
                        if (t.DepartId == FenGongSiId)
                        {
                            strHtml += string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", t.DepartId, t.DepartName);
                        }
                        else
                        {
                            strHtml += string.Format("<option value=\"{0}\">{1}</option>", t.DepartId, t.DepartName);
                        }
                    }
                    else
                    {
                        //没有查看所有的权限，只显示自己所在的分公司
                        if (t.DepartId == FenGongSiId)
                            strHtml += string.Format("<option value=\"{0}\">{1}</option>", t.DepartId, t.DepartName);
                    }
                }
            }

            ltrSunCompany.Text = strHtml;
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
            s.Append("部门\t销售员\t应收款\t已收款\t未收\n");
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompan"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_收入对账单_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetReconciliationLst(
                SiteUserInfo.CompanyId,
                toXlsRecordCount,
                1,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel(),
                ref SumMoney);

            if (list != null && list.Any())
            {
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\n",
                        t.DeptName,
                        t.SellerName,
                        UtilsCommons.GetMoneyString(t.TotalAmount, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.InAmount, ProviderToMoney),
                        UtilsCommons.GetMoneyString(t.RestAmount, ProviderToMoney));
                }

                s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\n",
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
