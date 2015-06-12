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
using EyouSoft.Model.EnumType.PrivsStructure;
using System.Text;



namespace Web.FinanceManage.Arrearage
{
    /// <summary>
    /// 超限审批-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-14
    public partial class OverrunList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private readonly int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 结算单路径
        /// </summary>
        string PringPageJSD = string.Empty;
        #endregion

        /// <summary>
        /// 是否拥有审核权限
        /// </summary>
        protected bool IsHaveCheckPriv = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            //权限判断
            PowerControl();
            //初始化
            DataInit();

        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            PringPageJSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);

            #region 查询实体
            MChaXianShenPiChaXunInfo queryModel = new MChaXianShenPiChaXunInfo();
            //线路名称
            queryModel.RouteName = Utils.InputText(Utils.GetQueryStringValue("txt_lineName"));
            //申请人
            queryModel.ShenQingRenId = SellsSelect1.SellsID = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            queryModel.ShenQingRenName = SellsSelect1.SellsName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.CrmName = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            queryModel.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            queryModel.SShenQingTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ApplyTimeS"));
            queryModel.EShenQingTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ApplyTimeE"));
            queryModel.Status = (EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus), Utils.GetQueryStringValue("txtStatus"));

            queryModel.ShenPiRenId = this.txtShenPiRen.SellsID = Utils.GetQueryStringValue(txtShenPiRen.SellsIDClient);
            queryModel.ShenPiRenName = this.txtShenPiRen.SellsName = Utils.GetQueryStringValue(txtShenPiRen.SellsNameClient);
            queryModel.ShenPiSTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShenPiSTime"));
            queryModel.ShenPiETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShenPiETime"));

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
                //绑定分页
                BindPage();
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
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_欠款预警_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_欠款预警_栏目, true);
                return;
            }

            IsHaveCheckPriv = CheckGrant(Privs.财务管理_欠款预警_超限审批);
        }
        #endregion

        /// <summary>
        /// 获取查看链接
        /// </summary>
        /// <param name="itemType">超限类型</param>
        /// <param name="tourId">计划编号</param>
        /// <param name="orderId">订单编号</param>
        /// <param name="tourType">计划类型</param>
        /// <returns></returns>
        protected string GetChaKanLink(object itemType, object tourId,object orderId,object tourType)
        {
            if (itemType == null || tourId == null || orderId == null || tourType == null) return "javascript:void(0)";

            return string.Format("{0}?OrderId={1}&tourType={2}&ykxc=1", PringPageJSD, orderId.ToString(), (int)tourType);
        }
    }
}
