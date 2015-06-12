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
    public partial class Traveller : BackPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private const int _pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int RecordCount = 0;                

        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int Sl = 0;

        /// <summary>
        /// 游客类型
        /// </summary>
        protected int TravellerType = 0;
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
            #region 获取页面参数

            Sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            TravellerType = Utils.GetInt(Utils.GetQueryStringValue("ttp"));
            FenGongSiId = Utils.GetInt(Utils.GetQueryStringValue("SunCompan"));
            #endregion

            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel(TravellerType);

            if (!IsPostBack)
            {
                PowerControl();
                BindSunCompany();
                InitData(TravellerType);
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_游客统计表_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_游客统计表_栏目, true);
                return;
            }

            Privs_ChaKanSuoYou = CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_游客统计表_查看全部);

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
        /// 返回游客类型枚举
        /// </summary>
        /// <param name="ttp">游客类型编号</param>
        /// <returns></returns>
        private Model.EnumType.IndStructure.TravellerFlowType GetTravellerType(int ttp)
        {
            switch (ttp)
            {
                case 0:
                    return Model.EnumType.IndStructure.TravellerFlowType.组团游客;
                case 1:
                    return Model.EnumType.IndStructure.TravellerFlowType.地接游客;
                case 2:
                    return Model.EnumType.IndStructure.TravellerFlowType.出镜游客;
                default:
                    return Model.EnumType.IndStructure.TravellerFlowType.组团游客;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        /// <param name="ttp">游客类型编号</param>
        private void InitData(int ttp)
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            var list = new BLL.StatStructure.BStatistics().GetTravellerFlowLst(
                SiteUserInfo.CompanyId,
                _pageSize,
                _pageIndex,
                ref RecordCount,
                FenGongSiId,
                this.GetSearchModel(),
                GetTravellerType(ttp));

            rptTraveller.DataSource = list;
            rptTraveller.DataBind();

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
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }

        /// <summary>
        /// 通过查询参数返回查询实体
        /// </summary>
        /// <returns>查询实体</returns>
        private Model.StatStructure.MTravellerFlowSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MTravellerFlowSearch
            {
                LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE"))
            };

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
                        if (t.DepartId == UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId))
                            strHtml += string.Format("<option value=\"{0}\">{1}</option>", t.DepartId, t.DepartName);
                    }
                }
            }

            ltrSunCompany.Text = strHtml;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="ttp">游客类型编号</param>
        private void ListToExcel(int ttp)
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("序号\t人数（订单人数合计）\t人天数（订单人数*计划天数合计）\t客源地\n");
            int sunCompanyId = Utils.GetInt(Utils.GetQueryStringValue("SunCompan"));
            if (sunCompanyId <= 0 || !CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_游客统计表_查看全部))
                sunCompanyId = UtilsCommons.GetFirstDepartId(SiteUserInfo.CompanyId, SiteUserInfo.DeptId);
            var list = new BLL.StatStructure.BStatistics().GetTravellerFlowLst(
                SiteUserInfo.CompanyId,
                toXlsRecordCount,
                1,
                ref RecordCount,
                sunCompanyId,
                this.GetSearchModel(),
                this.GetTravellerType(ttp));

            if (list != null && list.Any())
            {
                int index = 1;
                foreach (var t in list)
                {
                    s.AppendFormat("{0}\t{1}\t{2}\t{3}\n", index, t.PeopleNum, t.PeopleDayNum, t.Place);

                    index++;
                }
            }

            ResponseToXls(s.ToString());
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
