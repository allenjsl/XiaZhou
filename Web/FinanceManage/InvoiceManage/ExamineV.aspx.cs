using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;

namespace Web.FinanceManage.InvoiceManage
{
    /// <summary>
    /// 发票管理
    /// 审核,批量审核
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-10
    /// 该页面列表绑定是由js完成
    public partial class ExamineV : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType").Length > 0)
            {
                Examinev();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            lbl_Name.Text = SiteUserInfo.Name;
            txt_dateTime.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
        }
        /// <summary>
        /// 审批
        /// </summary>
        private void Examinev()
        {
            List<MBill> ls = new List<MBill>();
            int[] billIds = Utils.ConvertToIntArray(Utils.GetFormValue("BillIds").Split(','));
            string[] billNo = Utils.GetFormValue("BillNo").Split(',');
            string approveRemark = Utils.GetFormValue("ApproveRemark");
            if (billIds.Length == billNo.Length)
            {
                int i = billIds.Length;
                while (i-- > 0)
                {
                    ls.Add(new MBill
                      {
                          //发票编号
                          Id = billIds[i],
                          //票据号
                          BillNo = billNo[i],
                          //审核人Id
                          ApproverId = SiteUserInfo.UserId,
                          //审核人
                          Approver = SiteUserInfo.Name,
                          //公司编号
                          CompanyId = CurrentUserCompanyID,
                          //审核备注
                          ApproveRemark = approveRemark,
                          //审核时间
                          ApproveTime = DateTime.Now,
                          //操作员
                          Operator = SiteUserInfo.UserId,
                          //操作时间
                          IssueTime = DateTime.Now,
                          IsApprove = true
                      });
                }
                AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().SetApproveBill(ls) ? "1" : "-1"));
            }
            AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
        }

    }
}
