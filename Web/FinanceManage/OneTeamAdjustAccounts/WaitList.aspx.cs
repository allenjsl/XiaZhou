using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;
using EyouSoft.Common;
using Common.Enum;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.BLL.FinStructure;

namespace Web.FinanceManage.OneTeamAdjustAccounts
{
    /// <summary>
    /// 单团核算-待核算-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    public partial class WaitList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 查询实体
            MTourCheckBase queryModel = new MTourCheckBase();
            queryModel.TourStatus = TourStatus.财务待核算;
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.LDateStart = Utils.GetQueryStringValue("txt_SDate");
            queryModel.LDateEnd = Utils.GetQueryStringValue("txt_EDate");
            queryModel.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            queryModel.RouteName = Utils.GetQueryStringValue("txt_lineName");
            #endregion
            IList<MTourCheck> ls = new BFinance().GetTourCheckLst(
                pageSize,
                pageIndex,
                ref recordCount,
                queryModel,
                CheckGrant(TravelPermission.账务管理_单团核算_查看全部));
            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                lbl_msg.Text = "没有相关数据！";
                ExporPageInfoSelect1.Visible = false;
                ExporPageInfoSelect2.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;

            ExporPageInfoSelect2.PageLinkURL = ExporPageInfoSelect1.PageLinkURL;
            ExporPageInfoSelect2.UrlParams = ExporPageInfoSelect1.UrlParams;
            ExporPageInfoSelect2.intPageSize = ExporPageInfoSelect1.intPageSize;
            ExporPageInfoSelect2.CurrencyPage = ExporPageInfoSelect1.CurrencyPage;
            ExporPageInfoSelect2.intRecordCount = ExporPageInfoSelect1.intRecordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(TravelPermission.账务管理_单团核算_栏目))
            {
                Utils.ResponseNoPermit(TravelPermission.账务管理_单团核算_栏目, true);
                return;
            }
        }

        #endregion
    }
}
