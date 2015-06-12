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
    /// 统计分析--个人业绩统计
    /// </summary>
    /// 周文超 2012-04-24
    public partial class Personal : BackPage
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
            FenGongSiId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"));
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
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_栏目, true);
                return;
            }

            Privs_ChaKanSuoYou = CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_查看全部);

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
            var list = new BLL.StatStructure.BStatistics().GetPersonalLst(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref _recordCount,
                FenGongSiId,
                this.GetSearchModel());

            rptPersonal.DataSource = list;
            rptPersonal.DataBind();

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
        private Model.StatStructure.MPersonalSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MPersonalSearch
            {
                LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                SReviewTime = Utils.GetDateTime(Utils.GetQueryStringValue("CheckDateS"), Utils.GetFristDayOfMonth()),
                EReviewTime = Utils.GetDateTime(Utils.GetQueryStringValue("CheckDateE"), Utils.GetLastDayOfMonth())
            };

            //销售员
            string sellerId = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;
            search.SellerId = sellerId;

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
                        {
                            strHtml += string.Format("<option value=\"{0}\">{1}</option>", t.DepartId, t.DepartName);
                        }
                    }
                }
            }

            ltrDepartHtml.Text = strHtml;
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
            s.Append("销售员\t人数\t订单数\t总收入\t总支出\t毛利\t毛利率\n");
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("DepartId"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_个人业绩统计_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetPersonalLst(
                SiteUserInfo.CompanyId,
                toXlsRecordCount,
                1,
                ref _recordCount,
                sunCompanyId,
                this.GetSearchModel());

            if (list != null && list.Any())
            {
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
                        t.SellerName,
                        t.PeopleNum,
                        t.OrderNum,
                        t.TotalIncome.ToString("F2"),
                        t.TotalOutlay.ToString("F2"),
                        t.GrossProfit.ToString("F2"),
                        GetBfbString(t.GrossProfitRate, 0));
                }
            }

            ResponseToXls(s.ToString());
        }
    }
}
