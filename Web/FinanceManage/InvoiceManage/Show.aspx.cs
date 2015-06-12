using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common;

namespace Web.FinanceManage.InvoiceManage
{
    /// <summary>
    /// 发票管理
    /// 查看
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-10
    public partial class Show : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MBill model = new BFinance().GetBillMdl(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("billID")));
            if (model != null)
            {
                lbl_billTime.Text = UtilsCommons.GetDateString(model.BillTime, ProviderToDate);
                lbl_billAmount.Text = UtilsCommons.GetMoneyString(model.BillAmount, ProviderToMoney);
                lbl_billNo.Text = model.BillNo;
                lbl_remark.Text = model.Remark;
                lbl_approver.Text = model.Approver;
                lbl_approveTime.Text = UtilsCommons.GetDateString(model.ApproveTime, ProviderToDate);
                lbl_approveRemark.Text = model.ApproveRemark;
            }
        }
    }
}
