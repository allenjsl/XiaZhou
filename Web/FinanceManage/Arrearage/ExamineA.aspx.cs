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
using EyouSoft.Model.EnumType.PrivsStructure;

namespace Web.FinanceManage.Arrearage
{
    /// <summary>
    /// 超限审批
    /// 审批
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-29
    public partial class ExamineA : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            PowerControl();

            if (Utils.GetFormValue("disburseId").Length > 0)
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            MTransfinite model = new BFinance().GetTransfiniteMdl(Utils.GetQueryStringValue("disburseid"));
            if (model != null)
            {
                lbl_approverName.Text = model.Applier;
                lbl_disburseaount.Text = UtilsCommons.GetMoneyString(model.DisburseAmount, ProviderToMoney);
                lbl_applyTime.Text = UtilsCommons.GetDateString(model.ApplyTime, ProviderToDate);
                lbl_remark.Text = model.Remark;
                //销售超限
                IList<MApplyOverrun> swsl = null;
                //客户单位超限
                IList<MApplyOverrun> cwsl = null;

                if (model.AdvanceAppList != null)
                {
                    swsl = model.AdvanceAppList.Where(item => item.OverrunType == EyouSoft.Model.EnumType.FinStructure.OverrunType.销售员超限).ToList();
                    cwsl = model.AdvanceAppList.Where(item => item.OverrunType != EyouSoft.Model.EnumType.FinStructure.OverrunType.销售员超限).ToList();
                }

                //客户单位超限列表
                if (cwsl != null && cwsl.Count > 0)
                {
                    pan_msg.Visible = false;
                    rpt_cwList.DataSource = cwsl;
                    rpt_cwList.DataBind();
                }
                //销售员超限
                if (swsl != null && swsl.Count > 0)
                {

                    rpt_swList.DataSource = swsl;
                    rpt_swList.DataBind();
                }
                else
                {
                    pan_sw.Visible = false;
                }

                if (model.IsApprove != EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未审批)
                {
                    txt_approver.Text = model.Approver;
                    txt_approveTime.Text = UtilsCommons.GetDateString(model.ApproveTime, ProviderToDate);
                    txt_remark.Text = model.ApproveRemark;

                    if (model.IsApprove == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.通过) BtnType1.Checked = true;
                    if (model.IsApprove == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未通过) BtnType2.Checked = true;

                    phSubmit.Visible = false;
                }
                else
                {
                    txt_approver.Text = SiteUserInfo.Name;
                    txt_approveTime.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
                }

            }
            //txt_approver.Text = SiteUserInfo.Name;
            //txt_approveTime.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            /*bool retBool = new BFinance().SetTransfiniteChk(
                Utils.GetFormValue("disburseId"),
                CurrentUserCompanyID,
                SiteUserInfo.DeptId,
                SiteUserInfo.UserId,
                SiteUserInfo.Name,
                DateTime.Now,
                Utils.GetFormValue("remark"),
                (EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Utils.GetIntSign(Utils.GetFormValue("status")),
                Utils.GetFormValue("itemId"),
                (EyouSoft.Model.EnumType.FinStructure.TransfiniteType)Utils.GetInt(Utils.GetFormValue("itemType")));
            if (retBool)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1", "提交成功!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "提交失败!"));
            }*/

            var info = new EyouSoft.Model.FinStructure.MChaoXianShenPiInfo();
            info.ChaoXianShenQingId = Utils.GetFormValue("disburseId");
            info.CompanyId = CurrentUserCompanyID;
            info.IssueTime = DateTime.Now;
            info.OperatorId = SiteUserInfo.UserId;
            info.Status = (EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Utils.GetIntSign(Utils.GetFormValue("status"));
            info.YiJian = Utils.GetFormValue("remark");

            int bllRetCode = new BFinance().ChaoXianShenPi(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "提交成功!"));
            else RCWE(UtilsCommons.AjaxReturnJson("-1", "提交失败!"));
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_欠款预警_超限审批))
            {
                Utils.ResponseNoPermit(Privs.财务管理_欠款预警_超限审批, true);
                return;
            }
        }
    }
}
