using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.MarketCenter
{
    /// <summary>
    /// 销售中心-预付确认
    /// </summary>
    public partial class YuFuQueRen : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "ShenHe")
            {
                PageShenHe();
            }
            #endregion
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageShenHe()
        {

            int[] registerIds = Utils.ConvertToIntArray(Utils.GetQueryStringValue("RegisterId").Split(','));
            string msg = string.Empty;
            bool result = false;
            if (registerIds.Length <= 0)
            {
                msg = "审批失败!";
            }
            if (msg.Length <= 0)
            {
                int i = new EyouSoft.BLL.FinStructure.BFinance().SetRegisterApprove(
                     SiteUserInfo.UserId, //审核人ID
                     SiteUserInfo.Name,//审核人
                     DateTime.Now,//审核时间(当前时间)
                     "审核意见",//审核意见
                     EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批,//审核状态
                     CurrentUserCompanyID,//公司ID
                     registerIds//登记编号集合
                     );
                if (i > 0)
                {
                    result = true;
                    msg = "审核成功！";
                }
                else
                {
                    result = false;
                    msg = "审批失败！";
                }
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();

        }
        #endregion

        #region private members
        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应付管理_付款审批, true);
                return;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.FinStructure.MPayableApproveBase queryModel = new EyouSoft.Model.FinStructure.MPayableApproveBase();
            #region 查询参数
            //公司id
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.Status = EyouSoft.Model.EnumType.FinStructure.FinStatus.销售待确认;
            queryModel.IsPrepaidConfirm = true;
            #endregion
            decimal sum = 0;
            IList<EyouSoft.Model.FinStructure.MPayableApprove> ls = new EyouSoft.BLL.FinStructure.BFinance().GetMPayableApproveLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref sum,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                rpt.DataSource = ls;
                rpt.DataBind();
            }
            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
            ExporPageInfoSelect1.Visible = recordCount > pageSize;
            pan_Msg.Visible = recordCount == 0;
        }

        #endregion
    }
}
