//供应商交易明细-购物 汪奇志 2013-05-15
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.ResourceManage
{
    /// <summary>
    /// 供应商交易明细-购物
    /// </summary>
    public partial class JiaoYiMingXiGouWu : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 单位名称
        /// </summary>
        protected string GysName = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        protected string GysId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            GysName = Utils.GetQueryStringValue("gysname");

            if (UtilsCommons.IsToXls()) ToXls();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MJiaoYiMingXiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.GysStructure.MJiaoYiMingXiChaXunInfo();

            info.LEDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLEDate"));
            info.LSDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLSDate"));

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            object[] heJi;
            var items = new EyouSoft.BLL.GysStructure.BGys().GetJiaoYiMingXis(SiteUserInfo.CompanyId, GysId, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();

                if (items[0].AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 || items[0].AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮)
                {
                    ltrShuLiangHeJi.Text = ((decimal)heJi[1]).ToString("F2");
                }
                else
                {
                    ltrShuLiangHeJi.Text = heJi[0].ToString();
                }

                phEmpty.Visible = false;
                phHeJi.Visible = true;

                paging.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                paging.Visible = false;
                phEmpty.Visible = true;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var chaXun = GetChaXunInfo();
            object[] heJi;
            int _recordCount = 0;
            var items = new EyouSoft.BLL.GysStructure.BGys().GetJiaoYiMingXis(SiteUserInfo.CompanyId, GysId, toXlsRecordCount, 1, ref _recordCount, chaXun, out heJi);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("团号\t线路名称\t销售员\t计调\t导游\t数量\n");

            foreach (var item in items)
            {
                s.Append(item.TourCode + "\t");
                s.Append(item.RouteName + "\t");                
                s.Append(item.XiaoShouYuanName + "\t");
                s.Append(item.JiDiaoYuanName + "\t");
                s.Append(item.DaoYouname + "\t");
                if (item.AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 || item.AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮)
                {
                    s.Append(item.DShuLiang.ToString("F2") + "\n");
                }
                else
                {
                    s.Append(item.ShuLiang + "\n");
                }
            }

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="shuLiang">数量（int）</param>
        /// <param name="dShuLiang">数量（decimal）</param>
        /// <param name="anPaiLeiXing">安排类型</param>
        /// <returns></returns>
        protected string GetShuLiang(object shuLiang, object dShuLiang, object anPaiLeiXing)
        {
            if (shuLiang == null || dShuLiang == null || anPaiLeiXing == null) return "0";
            var _anPaiLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)anPaiLeiXing;

            if (_anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 || _anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮)
            {
                return ((decimal)dShuLiang).ToString("F2");
            }

            return shuLiang.ToString();
        }

        /// <summary>
        /// 获取结算金额
        /// </summary>
        /// <param name="jinE">结算金额</param>
        /// <param name="tourStatus">计划状态</param>
        /// <param name="tourType">计划类型</param>
        /// <returns></returns>
        protected string GetJinE(object jinE, object tourStatus, object tourType)
        {
            if (jinE == null || tourStatus == null || tourType == null) return "0.00";
            var _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
            var _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;

            string s = _tourStatus.ToString();
            if (_tourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                switch (_tourStatus)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划: s = "操作中"; break;
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕: s = "已落实"; break;
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团: s = "核算结束"; break;
                    default: break;
                }
            }

            if (_tourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算 || _tourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团)
            {
                return "<span style='color:#000000;' title='已终审，计划状态：" + s + "'>" + UtilsCommons.GetMoneyString(jinE, ProviderToMoney) + "</span>";
            }

            return "<span style='color:#ff0000;' title='未终审，计划状态：" + s + "'>" + UtilsCommons.GetMoneyString(jinE, ProviderToMoney) + "</span>";
        }
        #endregion
    }
}
