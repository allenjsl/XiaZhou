using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Model.GovStructure;
using System.Web.UI.HtmlControls;
using EyouSoft.Model.EnumType.GovStructure;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-批量考勤
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-28
    public partial class AttGroup : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            if (!IsPostBack)
            {
                PageInit();
            }
            string save = Utils.GetQueryStringValue("save");
            if (save == "save")
            {
                PageSave();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            string names = Utils.GetQueryStringValue("names");
            this.attendNames.Text = names;
            string ids = Utils.GetQueryStringValue("id");
            string depts = Utils.GetQueryStringValue("deptids");
            this.hdattenIds.Value = ids;
            this.hdattenNames.Value = names;
            this.hdattenDepts.Value = depts;
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave()
        {
            #region 表单取值
            string atttype = Utils.GetFormValue(attType.UniqueID);
            string stime = Utils.GetFormValue(tbStime.UniqueID);
            string etime = Utils.GetFormValue(tbEtime.UniqueID);
            string sub = Utils.GetFormValue(subject.UniqueID);
            string ids = Utils.GetFormValue(hdattenIds.UniqueID);
            string names = Utils.GetFormValue(hdattenNames.UniqueID);
            string depts = Utils.GetFormValue(hdattenDepts.UniqueID);
            string timecount = Utils.GetFormValue(timeCount.UniqueID);
            #endregion

            #region 表单验证
            string msg = string.Empty;
            if (string.IsNullOrEmpty(stime))
            {
                msg += "请选择考勤开始时间！";
            }
            if (string.IsNullOrEmpty(etime))
            {
                msg += "请选择考勤结束时间！";
            }
            if (string.IsNullOrEmpty(sub) && Utils.GetInt(atttype, 0) > 0)
            {
                msg += "请输入原因！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            #endregion

            #region 实体赋值
            //时间
            TimeSpan ts = new TimeSpan(Utils.GetDateTime(stime).Ticks);
            TimeSpan te = new TimeSpan(Utils.GetDateTime(etime).Ticks);
            TimeSpan total = ts.Subtract(te).Duration();
            IList<MGovAttendance> list = new List<MGovAttendance>();
            string[] arryIds = ids.Split(',');
            string[] arryNames = names.Split('、');
            string[] arryDepts = depts.Split(',');
            if (arryIds.Length == arryNames.Length && arryIds.Length == arryDepts.Length)
            {
                //小于一天
                if (total.Days == 0)
                {
                    for (int i = 0; i < arryIds.Length; i++)
                    {
                        MGovAttendance model = new MGovAttendance();
                        model.StaffId = arryIds[i];
                        model.CompanyId = this.SiteUserInfo.CompanyId;
                        model.DeptId = Utils.GetInt(arryDepts[i]);
                        model.OperatorId = this.SiteUserInfo.UserId;
                        model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype));
                        model.StartTime = Utils.GetDateTimeNullable(stime);
                        model.AttendanceTime = Utils.GetDateTimeNullable(stime);
                        model.EndTime = Utils.GetDateTimeNullable(etime);
                        model.Subject = sub;
                        model.TimeCount = string.IsNullOrEmpty(timecount) ? 1 : Utils.GetDecimal(timecount); ;
                        model.IssueTime = DateTime.Now;
                        model.OperatorId = this.SiteUserInfo.UserId;
                        list.Add(model);
                    }
                }
                else
                {
                    //大于一天
                    for (int j = 0; j < total.Days; j++)
                    {
                        for (int i = 0; i < arryIds.Length; i++)
                        {
                            MGovAttendance model = new MGovAttendance();
                            model.StaffId = arryIds[i];
                            model.CompanyId = this.SiteUserInfo.CompanyId;
                            model.DeptId = Utils.GetInt(arryDepts[i]);
                            model.OperatorId = this.SiteUserInfo.UserId;
                            model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype));
                            model.AttendanceTime = Utils.GetDateTime(stime).AddDays(j);
                            model.StartTime = Utils.GetDateTime(stime).AddDays(j);
                            model.EndTime = Utils.GetDateTime(etime).AddDays(-total.Days + j);
                            model.Subject = sub;
                            model.TimeCount = (AttendanceType)(Utils.GetInt(atttype, 0)) == AttendanceType.加班 ? Utils.GetDecimal(timecount) : 1; ;
                            model.IssueTime = DateTime.Now;
                            model.OperatorId = this.SiteUserInfo.UserId;
                            list.Add(model);
                        }

                    }
                    //多出的小时数
                    if (total.Hours > 0)
                    {
                        for (int i = 0; i < arryIds.Length; i++)
                        {
                            MGovAttendance model = new MGovAttendance();
                            model.StaffId = arryIds[i];
                            model.CompanyId = this.SiteUserInfo.CompanyId;
                            model.DeptId = Utils.GetInt(arryDepts[i]);
                            model.OperatorId = this.SiteUserInfo.UserId;
                            model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype));
                            model.AttendanceTime = Utils.GetDateTime(stime).AddDays(total.Days);
                            model.StartTime = Utils.GetDateTime(stime).AddDays(total.Days);
                            model.EndTime = Utils.GetDateTimeNullable(etime);
                            model.Subject = sub;
                            model.TimeCount = (AttendanceType)(Utils.GetInt(atttype, 0)) == AttendanceType.加班 ? Utils.GetDecimal(timecount) : 1; ;
                            model.IssueTime = DateTime.Now;
                            model.OperatorId = this.SiteUserInfo.UserId;
                            list.Add(model);
                        }
                    }
                }
            }
            #endregion

            #region 提交验证
            EyouSoft.BLL.GovStructure.BAttendance BLL = new EyouSoft.BLL.GovStructure.BAttendance();
            bool result = BLL.AddAllGovAttendanceList(list);
            msg = result ? "批量考勤成功！" : "批量考勤失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }


        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目, false);
            }
            else
            {
                ph_save.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_考勤管理);
            }
        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion

    }
}