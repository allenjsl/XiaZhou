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
    /// 说明：个人中心-工作计划-审批
    public partial class WorkPlanApproval : BackPage
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
                string id = Utils.GetQueryStringValue("id");
                //存储id在隐藏域
                hidID.Value = id;
                //根据ID初始化页面
                PageInit(id);
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
                //根据id获取计划实体
                MWorkPlan modelMWorkPlan = bllBIndividual.GetWorkPlan(id);
                if (modelMWorkPlan != null)
                {
                    //计划标题
                    lblTitle.Text = modelMWorkPlan.Title;
                    //计划人
                    lblOperatorName.Text = modelMWorkPlan.OperatorName;
                    //计划部门
                    if (modelMWorkPlan.DeptId > 0)
                    {
                        lblDepartment.Text = GetDepartmentById(modelMWorkPlan.DeptId, SiteUserInfo.CompanyId);
                    }
                    //计划时间
                    lblIssueTime.Text = modelMWorkPlan.IssueTime.ToShortDateString();
                    if (modelMWorkPlan.list != null && modelMWorkPlan.list.Count > 0)
                    {
                        StringBuilder strList = new StringBuilder();
                        #region
                        strList.Append("<table width=\"99%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
                        strList.Append("<tr style=\"background: url(../images/y-formykinfo.gif) repeat-x center top;\">");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批人</td>");
                        strList.Append("<td height=\"23\" align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批时间</td>");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批状态</td>");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">审批意见</td>");
                        strList.Append("<td align=\"center\" bgcolor=\"#B7E0F3\" class=\"alertboxTableT\">操作</td>");
                        strList.Append("</tr>");
                        #endregion
                        foreach (MWorkPlanCheck item in modelMWorkPlan.list)
                        {
                            if (item != null)
                            {
                                //如果审批人为自己（操作审批）
                                //if (item.Approver.Trim() == SiteUserInfo.Username && item.Status == Status.审批中)
                                //if (item.Approver.Trim() == "fq" && item.Status == Status.审批中)
                                if (item.ApproverId == SiteUserInfo.UserId && item.Status == Status.审批中)
                                {
                                    #region 自己审核人
                                    strList.Append("<tr>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong class=\"fontred\">{0}</strong></td>", item.Approver);
                                    strList.Append("<td height=\"28\" align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"txtApproveTime\"  onfocus=\"WdatePicker()\" type=\"text\" class=\"formsize120\" id=\"txtApproveTime\" size=\"30\"/></td>");
                                    strList.AppendFormat("<td align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"chbStatus\" type=\"checkbox\" id=\"chbStatus\" checked=\"checked\" value=\"{0}\" /></td>", item.Id);
                                    strList.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><input name=\"txtComment\" type=\"text\" class=\"formsize180\" id=\"txtComment\" size=\"30\"/></td>");
                                    strList.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><a href=\"javascript:void(0)\" onclick=\"WorkPlanApproval.IntiData()\">审批</a></td>");
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
                                    strList.AppendFormat("<td height=\"28\" align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>", Utils.GetDateTime(item.ApproveTime.ToString()).ToShortDateString() == "0001-1-1" ? "" : Utils.GetDateTime(item.ApproveTime.ToString()).ToShortDateString());
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
                        if (modelMWorkPlan.Status == Status.已审批)
                        {
                            strList.Append("<div class=\"hr_5\"></div>");
                            strList.Append("<table width=\"99%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
                            strList.Append("<tr>");
                            strList.Append("<td height=\"28\" align=\"left\" bgcolor=\"#FFFFFF\">实际完成时间：");
                            strList.Append("<input name=\"txtActualTime\" onfocus=\"WdatePicker()\" type=\"text\" class=\"formsize120\" id=\"txtActualTime\" size=\"30\"/>结果：");
                            strList.Append("<input name=\"txtEnd\" type=\"text\" class=\"formsize180\" id=\"txtEnd\" size=\"30\"/>");
                            strList.Append("<input name=\"chbIsEnd\" type=\"checkbox\" id=\"chbIsEnd\" checked=\"checked\"/>是否结束");
                            strList.Append("&nbsp;<img src=\"/images/baocunimg.gif\" width=\"48\" height=\"20\" onclick=\"WorkPlanEnd.IntiData()\" /></td>");
                            strList.Append("</tr>");
                            strList.Append("</table>");
                        }
                        litList.Text = strList.ToString();
                    }



                    //是否可以操作该数据
                    if (!SiteUserInfo.IsHandleElse)
                    {
                        if (modelMWorkPlan.Status == Status.已审批 || (modelMWorkPlan.OperatorId != null && modelMWorkPlan.OperatorId != SiteUserInfo.UserId))
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

                case "AjaxSetEnd"://ajax操作结束
                    PageEnd();
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
            MWorkPlanCheck modelMWorkPlanCheck = new MWorkPlanCheck();
            //审核人
            modelMWorkPlanCheck.Approver = SiteUserInfo.Name;
            //审核时间
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("ApproveTime")))
                modelMWorkPlanCheck.ApproveTime = Utils.GetDateTime(Utils.GetQueryStringValue("ApproveTime"));
            else
                modelMWorkPlanCheck.ApproveTime = DateTime.Now;
            if (Utils.GetFormValue("Status") == ((int)Status.已审批).ToString())
            {
                //审核状态
                modelMWorkPlanCheck.Status = (Status)Enum.Parse(typeof(Status), Utils.GetFormValue("Status"));
            }
            else
            {
                resultmsg = UtilsCommons.AjaxReturnJson("false", "审核状态未勾选");
                Response.Clear();
                Response.Write(resultmsg);
                Response.End();
            }
            //审核意见
            modelMWorkPlanCheck.Comment = Utils.GetFormValue("Comment");
            //审核人编号
            modelMWorkPlanCheck.ApproverId = SiteUserInfo.UserId;
            //计划编号
            modelMWorkPlanCheck.WorkPlanId = Utils.GetInt(Utils.GetFormValue("WorkPlanId"));
            //主键编号
            modelMWorkPlanCheck.Id = Utils.GetInt(Utils.GetFormValue("listId"));
            if (bllBIndividual.SetWorkPlanStatus(modelMWorkPlanCheck))
                resultmsg = UtilsCommons.AjaxReturnJson("true", "审批成功");
            else
                resultmsg = UtilsCommons.AjaxReturnJson("false", "审批失败");

            Response.Clear();
            Response.Write(resultmsg);
            Response.End();
        }


        /// <summary>
        /// 保存按纽点击执行结束方法
        /// </summary>
        protected void PageEnd()
        {
            //ajax返回信息
            string resultmsg = "";
            BIndividual bllBIndividual = new BIndividual();
            MWorkPlan modelMWorkReportCheck = new MWorkPlan();
            if (!string.IsNullOrEmpty(Utils.GetFormValue("WorkId")))
            {
                //根据编号获取工作计划实体
                modelMWorkReportCheck = bllBIndividual.GetWorkPlan(Utils.GetFormValue("WorkId"));
                if (modelMWorkReportCheck != null)
                {
                    if (Utils.GetFormValue("Status") == ((int)Status.已结束).ToString())
                    {
                        //审核状态
                        modelMWorkReportCheck.Status = Status.已结束;
                    }
                    else
                    {
                        resultmsg = UtilsCommons.AjaxReturnJson("false", "审核状态未勾选");
                        Response.Clear();
                        Response.Write(resultmsg);
                        Response.End();
                    }
                    //实际完成时间
                    if (!string.IsNullOrEmpty(Utils.GetFormValue("ActualTime")))
                        modelMWorkReportCheck.ActualTime = Utils.GetDateTime(Utils.GetFormValue("ActualTime"));
                    else
                        modelMWorkReportCheck.ActualTime = DateTime.Now;
                    //结束
                    modelMWorkReportCheck.Result = Utils.GetFormValue("Result");
                    //最后修改时间
                    modelMWorkReportCheck.UpdateTime = DateTime.Now;
                    if (bllBIndividual.SetWorkPlanEnd(modelMWorkReportCheck))
                        resultmsg = UtilsCommons.AjaxReturnJson("true", "结束成功");
                    else
                        resultmsg = UtilsCommons.AjaxReturnJson("false", "结束失败");
                }
                else
                    resultmsg = UtilsCommons.AjaxReturnJson("false", "结束失败 无法获取结束数据");
            }
            else
                resultmsg = UtilsCommons.AjaxReturnJson("false", "结束失败 参数编号为空");
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作计划栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作计划栏目, false);
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
