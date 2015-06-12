using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 统计分析--状态查询表
    /// </summary>
    /// 周文超 2012-05-17
    public partial class QueryState : BackPage
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
        /// 行程单-组团
        /// </summary>
        string PrintFilePath_ZT_XingChengDan = string.Empty;
        /// <summary>
        /// 行程单-散拼
        /// </summary>
        string PrintFilePath_SP_XingChengDan = string.Empty;
        /// <summary>
        /// 核算单
        /// </summary>
        string PrintFilePath_HeSuanDan = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {  
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel();

            PowerControl();
            InitPrintFilePath();
            InitData();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Model.EnumType.PrivsStructure.Privs.统计分析_状态查询表_栏目))
            {
                Utils.ResponseNoPermit(Model.EnumType.PrivsStructure.Privs.统计分析_状态查询表_栏目, true);
                return;
            }

            phSetTourStatus.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_状态查询表_计划状态变更);
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitData()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            object[] heJI;

            IList<Model.StatStructure.MTourStatus> list;
            list = new BLL.StatStructure.BStatistics().GetTourStatusLst(SiteUserInfo.CompanyId, _pageSize, _pageIndex, ref _recordCount, this.GetSearchModel(), out heJI);

            if (list != null && list.Count > 0)
            {
                rptTourList.DataSource = list;
                rptTourList.DataBind();

                ltrRenShuHeJi.Text = heJI[0].ToString();
            }
            else
            {
                phHeJi.Visible = false;
            }

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
        private Model.StatStructure.MTourStatusSearch GetSearchModel()
        {
            var search = new Model.StatStructure.MTourStatusSearch
                {
                    LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateS")),
                    LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LeaveDateE")),
                    RDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("BackDateS")),
                    RDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("BackDateE")),
                    TourCode = Utils.GetQueryStringValue("TourNo")
                };

            //int tState = Utils.GetInt(Utils.GetQueryStringValue("TourState"), -1);
            //if (tState >= 0) search.TourStatus = (Model.EnumType.TourStructure.TourStatus)tState;

            string status = Utils.GetQueryStringValue(txtTourStatus.ValueClientID);
            if (!string.IsNullOrEmpty(status))
            {
                string[] items = status.Split(',');
                if (items != null && items.Length > 0)
                {
                    search.TourStatus = new EyouSoft.Model.EnumType.TourStructure.TourStatus[items.Length];
                    for (int i = 0; i < items.Length;i++ )
                    {
                        search.TourStatus[i] = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.TourStatus>(items[i], EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划);
                    }
                }
            }

            //销售员
            string sellerId = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;
            //导游
            string guidId = Utils.GetQueryStringValue(GuidsSelect1.GuidIDClient);
            string guidName = Utils.GetQueryStringValue(GuidsSelect1.GuidNameClient);
            this.GuidsSelect1.GuidID = guidId;
            this.GuidsSelect1.GuidName = guidName;

            search.SellerId = sellerId;
            search.SellerName = sellerName;
            search.GuiderId = guidId;
            search.Guide = guidName;

            search.RouteName = Utils.GetQueryStringValue("txtRouteName");

            string tourSellerDepts = txtTourSellerDept.SectionID = Utils.GetQueryStringValue(txtTourSellerDept.SelectIDClient);
            txtTourSellerDept.SectionName = Utils.GetQueryStringValue(txtTourSellerDept.SelectNameClient);

            if (!string.IsNullOrEmpty(tourSellerDepts))
            {
                string[] items = tourSellerDepts.Split(',');
                if (items != null && items.Length > 0)
                {
                    search.TourSellerDeptIds = new int[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        search.TourSellerDeptIds[i] = Utils.GetInt(items[i]);
                    }
                }
            }

            search.JiDiaoYuanId = txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsIDClient);
            search.JiDiaoYuanName = txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            return search;
        }

        /// <summary>
        /// 获取计调员名称
        /// </summary>
        /// <param name="obj">计调集合</param>
        /// <returns>计调员名称</returns>
        protected string GetPlanerName(object obj)
        {
            if (obj == null) return string.Empty;
            var list = (IList<Model.TourStructure.MTourPlaner>)obj;
            if (!list.Any()) return string.Empty;

            var strName = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null) continue;

                strName.AppendFormat("{0}，", t.Planer);
            }

            if (!string.IsNullOrEmpty(strName.ToString())) return strName.ToString().TrimEnd('，');

            return string.Empty;
        }

        /// <summary>
        /// 获取导游名称
        /// </summary>
        /// <param name="obj">导游集合</param>
        /// <returns>导游名称</returns>
        protected string GetGuiderName(object obj)
        {
            if (obj == null) return string.Empty;
            var list = (IList<Model.TourStructure.MGuidInfo>)obj;
            if (!list.Any()) return string.Empty;

            var strName = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.Name)) continue;
                if (strName.ToString().IndexOf(t.Name) > -1) continue;
                strName.AppendFormat("{0}，", t.Name);
            }

            if (!string.IsNullOrEmpty(strName.ToString())) return strName.ToString().TrimEnd('，');

            return string.Empty;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ListToExcel()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("团号\t线路名称\t出团时间\t回团时间\t人数\t销售员\t计调员\t导游\t状态\n");

            object[] heJi;

            IList<Model.StatStructure.MTourStatus> list;
            list = new BLL.StatStructure.BStatistics().GetTourStatusLst(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, this.GetSearchModel(),out heJi);

            if (list != null && list.Any())
            {
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
                        t.TourCode,
                        t.RouteName,
                        UtilsCommons.GetDateString(t.LDate, ProviderToDate),
                        UtilsCommons.GetDateString(t.RDate, ProviderToDate),
                        t.PersonNum,
                        t.SellerName,
                        GetPlanerName(t.Planer),
                        GetGuiderName(t.Guide),
                        t.TourStatus);
                }
            }

            ResponseToXls(s.ToString());
        }

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="tourType">团队类型</param>
        /// <param name="tourStatus">状态</param>
        /// <returns></returns>
        protected string GetTourStatus(object tourType, object tourStatus)
        {
            string s = string.Empty;
            if (tourType == null || tourStatus == null) return string.Empty;

            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            EyouSoft.Model.EnumType.TourStructure.TourStatus _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;

            if (_tourType != EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                return _tourStatus.ToString();
            }

            switch (_tourStatus)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划:
                    s = "操作中";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕:
                    s = "已落实";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审:
                    s = "待终审";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算:
                    s = "财务核算";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团:
                    s = "核算结束";
                    break;
                default: 
                    s = _tourStatus.ToString();
                    break;
            }

            return s;
        }

        /// <summary>
        /// 初始化打印路径
        /// </summary>
        void InitPrintFilePath()
        {
            var bll = new EyouSoft.BLL.ComStructure.BComSetting();

            PrintFilePath_HeSuanDan = bll.GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            PrintFilePath_ZT_XingChengDan = bll.GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            PrintFilePath_SP_XingChengDan = bll.GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);

            bll = null;
        }

        /// <summary>
        /// 获取线路名称链接
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="tourType">团队类型</param>
        /// <returns></returns>
        protected string GetRouteNameHref(object tourId,object tourType)
        {
            if (tourId == null || tourType == null) return "javascript:void(0)";

            var _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            string s = "javascript:void(0)";

            switch (_tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                    s = PrintFilePath_SP_XingChengDan + "?tourid=" + tourId.ToString();
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    s = PrintFilePath_ZT_XingChengDan + "?tourid=" + tourId.ToString();
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.单项服务:
                    s = PrintFilePath_HeSuanDan + "?referertype=5&tourid=" + tourId.ToString();
                    break;
                default: break;
            }

            return s;
        }
    }
}
