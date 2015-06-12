using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common.Page;

namespace Web.MarketCenter
{
    /// <summary>
    /// 销售中心-超限审批
    /// </summary>
    public partial class ChaoXianShenPi : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        private readonly int pageSize = 20;
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
            MChaXianShenPiChaXunInfo queryModel = new MChaXianShenPiChaXunInfo();
            //线路名称
            queryModel.RouteName = Utils.InputText(Utils.GetQueryStringValue("txt_lineName"));
            //申请人
            queryModel.ShenQingRenId = SellsSelect1.SellsID = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            queryModel.ShenQingRenName = SellsSelect1.SellsName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.CrmName =  Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            queryModel.CrmId  = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryModel.SShenQingTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ApplyTimeS"));
            queryModel.EShenQingTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ApplyTimeE"));
            #endregion
            SellsSelect1.SetTitle = "选用 申请人";
            CustomerUnitSelect1.ThisTitle = "客户单位";

            IList<MTransfinite> ls = new BFinance().GetTransfiniteLst(
                pageSize,
                pageIndex,
                ref recordCount,
                queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;


            //绑定分页
            BindPage();

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
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售预警_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售预警_超限审批栏目, true);
                return;
            }
        }

        #endregion
    }
}
