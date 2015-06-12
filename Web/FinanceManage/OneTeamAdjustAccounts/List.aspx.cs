using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.ComStructure;
using System.Text;

namespace Web.FinanceManage.OneTeamAdjustAccounts
{
    /// <summary>
    /// 单团核算-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    /// 修改人：柴逸宁
    /// 修改时间：2012.3.16
    /// 修改备注：单团合算 未审核与已审核统一成一张页面,原来的已审核页面已删除
    public partial class List : BackPage
    {
        protected string PrintUri = string.Empty;
        protected bool IsYiHeSuan = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();

            if (UtilsCommons.IsToXls()) ToXls();//导出

            //初始化
            DataInit();
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion

            var chaXun = GetChaXunInfo();
            PrintUri = new BComSetting().GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);

            IsYiHeSuan = chaXun.IsDealt;

            IList<MBZInfo> ls = new BTour().GetFinHSList(
                CurrentUserCompanyID,
                pageSize,
                pageIndex,
                ref recordCount,
                chaXun,
                SiteUserInfo.DeptId);


            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_单团核算_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_单团核算_栏目, true);
                return;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        MBZSearch GetChaXunInfo()
        {
            MBZSearch info = new MBZSearch();
            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("SDate"));
            info.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EDate"));
            info.TourCode = Utils.GetQueryStringValue("teamNumber");
            info.RouteName = Utils.GetQueryStringValue("lineName");
            info.IsDealt = Utils.GetIntSign(Utils.GetQueryStringValue("adjustAccountsType"), -1) > 0;
            info.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            //导游
            info.Guide = txt_Guide.GuidName = Utils.GetQueryStringValue(txt_Guide.GuidNameClient);
            //导游Id
            info.GuideId = txt_Guide.GuidID = Utils.GetQueryStringValue(txt_Guide.GuidIDClient);
            //计调
            info.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            //计调Id
            info.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            //销售
            info.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            //销售Id
            info.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);

            info.HeSuanTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtHeSuanTime1"));
            info.HeSuanTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtHeSuanTime2"));
            return info;
        }

        /// <summary>
        /// toxls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();

            if (toXlsRecordCount >= 5000) Utils.RCWE("当前导出数据量较大(一次最多可导出5000个记录)，请分批次导出。<a href=\"javascript:history.go(-1)\">点击这里返回</a>");

            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            StringBuilder s = new StringBuilder();
            var chaXun = GetChaXunInfo();
            var items = new BTour().GetFinHSList(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun, SiteUserInfo.DeptId);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            s.AppendFormat("团号\t线路名称\t出团时间\t人数\t总人数\t销售\t计调\t导游\t收入\t支出\t毛利\t利润分配\t净利润\t核算时间\n");

            foreach (var item in items)
            {
                s.AppendFormat(item.TourCode + "\t");
                s.AppendFormat(item.RouteName + "\t");
                s.AppendFormat(item.LDate.ToString("yyyy-MM-dd") + "\t");
                s.AppendFormat(item.Adults + "+" + item.Childs + "\t");
                s.AppendFormat(item.Adults + item.Childs + "\t");
                s.AppendFormat(item.SellerName + "\t");
                s.AppendFormat(EyouSoft.Common.UtilsCommons.PingPlaner(item.MPlanerInfo)+"\t");
                s.AppendFormat(EyouSoft.Common.UtilsCommons.PingGuide(item.MGuidInfo)+"\t");
                s.AppendFormat(item.ZongShouRu.ToString("F2") + "\t");
                s.AppendFormat(item.TourPay.ToString("F2") + "\t");
                s.AppendFormat(item.Profit.ToString("F2") + "\t");
                s.AppendFormat(item.DisProfit.ToString("F2") + "\t");
                s.AppendFormat(item.JProfit.ToString("F2") + "\t");
                if (item.HeSuanTime.HasValue)
                {
                    s.AppendFormat(item.HeSuanTime.Value.ToString("yyyy-MM-dd") + "\n");
                }
                else
                {
                    s.AppendFormat("  " + "\n");
                }
            }
            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
