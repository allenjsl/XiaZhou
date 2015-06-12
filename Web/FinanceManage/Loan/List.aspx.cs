using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.BLL.ComStructure;

namespace Web.FinanceManage.Loan
{
    using EyouSoft.Model.EnumType.KingDee;

    /// <summary>
    /// 借款管理-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class List : BackPage
    {
        protected bool IsEnableKis;
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
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            IsEnableKis = comModel.IsEnableKis;
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            MDebitBase queryModel = new MDebitBase();
            queryModel.IsVerificated = Utils.GetQueryStringValue("verificated") == "1";
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            queryModel.Borrower = txt_Seller.GuidName = Utils.GetQueryStringValue(txt_Seller.GuidNameClient);
            queryModel.BorrowerId = txt_Seller.GuidID = Utils.GetQueryStringValue(txt_Seller.GuidIDClient);
            IList<MDebit> ls = new BFinance().GetDebitLst(pageSize, pageIndex, ref recordCount, queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
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
            if (!CheckGrant(Privs.财务管理_借款管理_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_借款管理_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 获取财务入账
        /// </summary>
        /// <param name="id">付款登记编号</param>
        /// <returns></returns>
        protected string GetFinIn(object id)
        {
            return new BFinance().IsFinIn(DefaultProofType.导游借款, id.ToString(), this.SiteUserInfo.CompanyId)
                       ? "已入帐"
                       : "未入账";
        }
        #endregion
    }
}
