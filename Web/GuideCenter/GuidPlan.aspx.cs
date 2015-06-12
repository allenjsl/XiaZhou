using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace Web.GuideCenter
{
    /// <summary>
    /// 导游出团安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-19
    /// </summary>
    public partial class GuidPlan : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            string name = Utils.GetQueryStringValue("name");
            string save = Utils.GetQueryStringValue("save");
            string type = Utils.GetQueryStringValue("type");
            string guideid = Utils.GetQueryStringValue("guideid");
            if (save != "")
            {
                Response.Clear();
                Response.Write(PageSave(name, guideid));
                Response.End();
            }
            if (!IsPostBack)
            {                
                #region 初始化导游信息
                this.labName.Text = name;                
                this.GuidControl1.GuidName = name;
                this.GuidControl1.GuidID = guideid;
                #endregion
            }
        }



        #region 权限判断
        protected void PowerControl()
        {

        }
        #endregion

        #region 保存
        protected string PageSave(string name, string guideid)
        {
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            #region 表单赋值
            //线路名称
            string routename = Utils.GetFormValue(LineSelect1.LineNameClient);
            //导游姓名
            string guidName = Utils.GetFormValue(this.GuidControl1.GuidNameClient);
            //电话
            string tel = Utils.GetFormValue(this.txttel.UniqueID);
            //出团时间
            string StartTime = Utils.GetFormValue(this.txtDate_Start.UniqueID);
            //接待行程
            string treval = Utils.GetFormValue(this.txttravel.UniqueID);
            //返回时间
            string endTime = Utils.GetFormValue(this.txtDate_End.UniqueID);
            //上团地点
            string StartAddress = Utils.GetFormValue(this.txtStartAddress.UniqueID);
            //任务类型
            string guidJobType = Utils.GetFormValue("seleJobType");
            //下团地点
            string EndAddress = Utils.GetFormValue(this.txtEndAddress.UniqueID);
            //费用明细
            string CostDesc = Utils.GetFormValue(this.txtCostDesc.UniqueID);
            //备注
            string remark = Utils.GetFormValue(this.txtrmark.UniqueID);
            //服务标准
            string Service = Utils.GetFormValue(this.txtService.UniqueID);
            //结算费用
            string PaidCost = Utils.GetFormValue(this.txtPaidCost.UniqueID);
            //团号
            string TourCode = Utils.GetFormValue(this.txtTourCode.UniqueID);

            #endregion
            #region 后台验证
            if (string.IsNullOrEmpty(guidName))
            {
                msg += "请选择导游!<br/>";
            }
            if (string.IsNullOrEmpty(StartAddress))
            {
                msg += "请输入上团地点!<br/>";
            }
            if (string.IsNullOrEmpty(StartTime.ToString()))
            {
                msg += "请选择上团时间!<br/>";
            }
            if (string.IsNullOrEmpty(EndAddress))
            {
                msg += "请输入下团地点!<br/>";
            }
            if (string.IsNullOrEmpty(endTime.ToString()))
            {
                msg += "请选择下团时间!<br/>";
            }
            if (string.IsNullOrEmpty(guidJobType))
            {
                msg += "请选择导游任务类型!<br/>";
            }
            if (Utils.GetDecimal(PaidCost) <= 0)
            {
                msg += "请填写结算费用！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                return UtilsCommons.AjaxReturnJson("0", "" + msg + "");
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseInfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseInfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseInfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseInfo.Confirmation = Utils.GetDecimal(PaidCost);
            baseInfo.ContactPhone = tel;
            baseInfo.CostDetail = CostDesc;
            baseInfo.IssueTime = System.DateTime.Now;
            TimeSpan ts1 = new TimeSpan(Utils.GetDateTime(endTime).Ticks);
            TimeSpan ts2 = new TimeSpan(Utils.GetDateTime(StartTime).Ticks);
            baseInfo.Num = ts1.Subtract(ts2).Duration().Days;
            baseInfo.PlanGuide = new EyouSoft.Model.PlanStructure.MPlanGuide();
            baseInfo.PlanGuide.NextLocation = EndAddress;
            baseInfo.PlanGuide.OnLocation = StartAddress;
            baseInfo.PlanGuide.TaskType = (EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType), guidJobType);            
            baseInfo.SourceId = guideid;
            baseInfo.SourceName = guidName;
            baseInfo.TourId = Utils.GetFormValue("hidTourID");
            baseInfo.ReceiveJourney = treval;
            baseInfo.Remarks = remark;
            baseInfo.ServiceStandard = Service;
            baseInfo.StartDate = Utils.GetDateTimeNullable(StartTime);
            baseInfo.EndDate = Utils.GetDateTimeNullable(endTime);
            baseInfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游;            
            baseInfo.OperatorId = this.SiteUserInfo.UserId;
            baseInfo.OperatorName = this.SiteUserInfo.Name;
            baseInfo.DeptId = this.SiteUserInfo.DeptId;
            baseInfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue(this.ddlState.UniqueID));
            #endregion

            #region 提交操作

            if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseInfo) > 0)
            {
                msg += "保存成功!";
                seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
            }
            else
            {
                msg += "保存失败!";
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
            }

            #endregion
            return seterrorMsg;
        }
        #endregion

        #region 绑定导游任务类型
        /// <summary>
        /// 绑定导游任务类型
        /// </summary>
        /// <param name="selected">类型id</param>
        /// <returns></returns>
        protected string GetGuidJobHtml(string selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> Guidjob = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType));
            if (Guidjob != null && Guidjob.Count > 0)
            {
                sb.Append("<select name=\"seleJobType\" id=\"seleJobType\" class=\"inputselect\" valid=\"required\" errmsg=\"*请选择任务类型!\">");
                sb.Append("<option value='-1'>--请选择--</option>");
                for (int i = 0; i < Guidjob.Count; i++)
                {
                    if (Guidjob[i].Value == selected)
                    {
                        sb.Append("<option selected='selected' value='" + Guidjob[i].Value + "'>" + Guidjob[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + Guidjob[i].Value + "'>" + Guidjob[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        #endregion

    }
}
