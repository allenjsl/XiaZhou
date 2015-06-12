using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.Common;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using System.Text;
using EyouSoft.Model.EnumType.IndStructure;

namespace Web.UserCenter.WorkExchange
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 修改人：蔡永辉
    /// 创建时间：2012-4-6
    /// 说明：个人中心-工作汇报-审批
    public partial class WorkReportApproval : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //权限验证
            PowerControl();

            #region ajax操作
            string Isajax = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(Isajax))
                Ajax(Isajax);
            #endregion
            if (!IsPostBack)
            {
                //获得操作ID
                string WorkId = Utils.GetQueryStringValue("WorkId");
                //存储WorkId在隐藏域
                hidID.Value = WorkId;
                //根据ID初始化页面
                PageInit(WorkId);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                BIndividual bllBIndividual = new BIndividual();
                //根据id获取汇报实体
                MWorkReport modelMWorkReport = bllBIndividual.GetWorkReport(Utils.GetInt(id));
                if (modelMWorkReport != null)
                {
                    //汇报标题
                    lblTitle.Text = modelMWorkReport.Title;
                    //汇报人
                    lblOperatorName.Text = modelMWorkReport.OperatorName;
                    //汇报部门
                    if (modelMWorkReport.DepartmentId > 0)
                    {
                        lblDepartment.Text = GetDepartmentById(modelMWorkReport.DepartmentId, SiteUserInfo.CompanyId);
                    }
                    //汇报时间
                    lblIssueTime.Text = modelMWorkReport.IssueTime.ToShortDateString();
                    if (modelMWorkReport.list != null && modelMWorkReport.list.Count > 0)
                    {
                        StringBuilder strList = new StringBuilder();
                        #region
                        strList.Append("<table width=\"99%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
                        strList.Append("<tr style=\"background: url(../images/y-formykinfo.gif) repeat-x center top;\">");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批人</td>");
                        strList.Append("<td height=\"23\" align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批时间</td>");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批状态</td>");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批意见</td>");
                        strList.Append(" <td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">操作</td>");
                        strList.Append("</tr>");
                        #endregion
                        foreach (MWorkReportCheck item in modelMWorkReport.list)
                        {
                            if (item != null)
                            {
                                //如果审批人为自己（操作审批）
                                //if (item.Approver.Trim() == SiteUserInfo.Username && item.Status == Status.审批中)

                                if (item.ApproverId.Trim() == SiteUserInfo.UserId && item.Status == Status.审批中)
                                {
                                    #region 自己审核人
                                    strList.Append("<tr>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong class=\"fontred\">{0}</strong></td>", item.Approver);
                                    strList.Append("<td height=\"28\" align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"txtApproveTime\"  onfocus=\"WdatePicker()\" type=\"text\" class=\"formsize120\" id=\"txtApproveTime\" size=\"30\"/></td>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"chbStatus\" type=\"checkbox\" id=\"chbStatus\" checked=\"checked\" value=\"{0}\"/></td>", item.Id);
                                    strList.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"txtComment\" type=\"text\" class=\"formsize180\" id=\"txtComment\" size=\"30\"/></td>");
                                    strList.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><a href=\"javascript:void(0)\" onclick=\"WorkReportApproval.IntiData()\">审批</a></td>");
                                    strList.Append("</tr>");
                                    #endregion
                                }
                                else//查看别人的审批状态
                                {
                                    #region 其他审核人
                                    string strimage = string.Empty;
                                    string strimagetitle = string.Empty;
                                    strList.Append("<tr>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>{0}</strong></td>", item.Approver);
                                    strList.AppendFormat("<td height=\"28\" align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>", Utils.GetDateTime(item.ApproveTime.ToString()).ToShortDateString() == "1900-1-1" ? "" : item.ApproveTime.ToString());
                                    strList.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">");
                                    #region 根据状态获取相应的图片
                                    switch (item.Status)
                                    {
                                        case Status.审批中:
                                            strimage = "y-delupdateicon.gif";
                                            strimagetitle = "审批中";
                                            break;
                                        case Status.已结束:
                                            strimage = "y-cuohao.gif";
                                            strimagetitle = "已结束";
                                            break;
                                        case Status.已审批:
                                            strimage = "y-duihao.gif";
                                            strimagetitle = "已审批";
                                            break;
                                    }
                                    strList.AppendFormat("<img src=\"/images/{0}\" width=\"13\" title=\"{1}\" height=\"9\" />", strimage, strimagetitle);
                                    #endregion
                                    strList.Append("</td>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>", item.Comment);
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>", "");
                                    strList.Append("</tr>");
                                    #endregion
                                }
                            }
                        }
                        strList.Append("</table>");
                        litList.Text = strList.ToString();
                    }

                    //是否可以操作该数据
                    if (!SiteUserInfo.IsHandleElse)
                    {
                        if (modelMWorkReport.OperatorId != null && modelMWorkReport.OperatorId != SiteUserInfo.UserId)
                        {
                            this.phdPrint.Visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="DepartmentID">部门ID</param>
        protected string GetDepartmentById(int DepartmentID, string companyid)
        {
            //返回信息
            string result = "";
            //实例化部门业务层
            BComDepartment BLL = new BComDepartment();
            MComDepartment model = BLL.GetModel(DepartmentID, companyid);
            if (model != null)
            {
                result = model.DepartName;
            }
            return result;
        }

        #region Ajax操作
        private void Ajax(string type)
        {
            switch (type)
            {
                case "AjaxSetStatus"://ajax操作保存
                    PageSave();
                    break;
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //ajax返回信息
            string resultmsg = "";
            BIndividual bllBIndividual = new BIndividual();
            MWorkReportCheck modelMWorkReportCheck = new MWorkReportCheck();
            //审核人
            modelMWorkReportCheck.Approver = SiteUserInfo.Name;
            string strApproveTime = "";
            if (!string.IsNullOrEmpty(Utils.GetFormValue("ApproveTime")))
            {
                strApproveTime = Utils.GetFormValue("ApproveTime");
            }
            else
                strApproveTime = DateTime.Now.ToShortDateString();
            //审核时间
            modelMWorkReportCheck.ApproveTime = Utils.GetDateTime(strApproveTime);
            if (Utils.GetFormValue("Status") == ((int)Status.已审批).ToString())
            {
                //审核状态
                modelMWorkReportCheck.Status = (Status)Enum.Parse(typeof(Status), Utils.GetFormValue("Status"));
            }
            else
            {
                resultmsg = UtilsCommons.AjaxReturnJson("false", "审核状态未勾选");
                Response.Clear();
                Response.Write(resultmsg);
                Response.End();
            }
            //审核意见
            modelMWorkReportCheck.Comment = Utils.GetFormValue("Comment");
            //审核人编号
            modelMWorkReportCheck.ApproverId = SiteUserInfo.UserId;
            //工作汇报编号
            modelMWorkReportCheck.WorkId = Utils.GetInt(Utils.GetFormValue("WorkId"));
            //主键编号
            modelMWorkReportCheck.Id = Utils.GetInt(Utils.GetFormValue("listId"));
            if (bllBIndividual.SetWorkReportStatus(modelMWorkReportCheck))
                resultmsg = UtilsCommons.AjaxReturnJson("true", "审批成功");
            else
                resultmsg = UtilsCommons.AjaxReturnJson("false", "审批失败");

            Response.Clear();
            Response.Write(resultmsg);
            Response.End();
        }




        #endregion
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作汇报栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作汇报栏目, false);
                return;
            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

    }
}
