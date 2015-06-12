using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;
using System.Text;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-考勤明细
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-27
    public partial class AttDetail : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(doType);
            }
        }
        #region 初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string doType)
        {
            //考勤编号
            string attId = Utils.GetQueryStringValue("attID");
            string date = Utils.GetQueryStringValue("date");
            //如果为修改
            if (doType == "update" && !string.IsNullOrEmpty(attId))
            {
                BAttendance BLL = new BAttendance();
                MGovAttendance model = BLL.GetGovAttendanceModel(attId, this.SiteUserInfo.CompanyId);
                if (model != null)
                {
                    //考勤编号
                    this.attentID.Value = model.AttendanceId;
                    //考勤类型
                    this.attType.Value = ((int)model.AttendanceType).ToString();
                    //部门编号
                    this.deptID.Value = model.DeptId.ToString();
                    //员工编号
                    this.staffID.Value = model.StaffId;
                    //开始时间
                    this.sTime.Value = model.StartTime.HasValue ? model.StartTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    //结束时间
                    this.eTime.Value = model.EndTime.HasValue ? model.EndTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    //考勤时间
                    this.tbtime.Text = model.AttendanceTime.HasValue ? model.AttendanceTime.Value.ToString("yyyy-MM-dd") : "";
                    //原因
                    this.subject.Value = model.Subject;
                }
            }
            else  //考勤登记
            {
                //员工编号
                string id = Utils.GetQueryStringValue("id");
                //员工部门编号
                string deptid = Utils.GetQueryStringValue("deptid");
                //考勤默认时间为当前日期
                this.tbtime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.deptID.Value = deptid;
                this.staffID.Value = id;
            }
        }
        #endregion


        /// <summary>
        /// 保存
        /// <param name="id">考勤ID</param>
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //考勤编号
            string attid = Utils.GetFormValue(this.attentID.UniqueID);
            //员工编号
            string staffid = Utils.GetFormValue(this.staffID.UniqueID);
            //部门编号
            string deptid = Utils.GetFormValue(this.deptID.UniqueID);
            //考勤时间
            string atttime = Utils.GetFormValue(this.tbtime.UniqueID);
            //考勤类型
            string atttype = Utils.GetFormValue(this.attType.UniqueID);
            //开始时间
            string stime = Utils.GetFormValue(this.sTime.UniqueID);
            //结束时间
            string etime = Utils.GetFormValue(this.eTime.UniqueID);
            //天数/时数
            string count = Utils.GetFormValue(this.timeCount.UniqueID);
            //理由
            string sub = Utils.GetFormValue(this.subject.UniqueID);
            #endregion

            #region 表单验证
            string msg = string.Empty;
            if (string.IsNullOrEmpty(atttime) && Utils.GetInt(atttype, 0) < 4)
            {
                msg += "请选择考勤时间！<br/>";
            }
            if (string.IsNullOrEmpty(stime) && Utils.GetInt(atttype, 0) > 3)
            {
                msg += "请选择开始时间！<br/>";
            }
            if (string.IsNullOrEmpty(etime) && Utils.GetInt(atttype, 0) > 3)
            {
                msg += "请选择结束时间！<br/>";
            }
            if (string.IsNullOrEmpty(sub) && Utils.GetInt(atttype, 0) > 0)
            {
                msg += "请输入原因！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            #endregion

            #region 实体赋值
            IList<MGovAttendance> list = new List<MGovAttendance>();
            TimeSpan ts = new TimeSpan(Utils.GetDateTime(stime).Ticks);
            TimeSpan te = new TimeSpan(Utils.GetDateTime(etime).Ticks);
            TimeSpan total = ts.Subtract(te).Duration();
            //时间不超过一天
            if (total.Days == 0)
            {
                MGovAttendance model = new MGovAttendance();
                model.AttendanceId = attid;
                model.AttendanceTime = string.IsNullOrEmpty(stime) ? Utils.GetDateTimeNullable(atttime) : Utils.GetDateTimeNullable(stime);
                model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype, 0));
                model.CompanyId = this.SiteUserInfo.CompanyId;
                model.DeptId = Utils.GetInt(deptid);
                model.EndTime = string.IsNullOrEmpty(etime) ? Utils.GetDateTimeNullable(atttime) : Utils.GetDateTimeNullable(etime);
                model.IssueTime = DateTime.Now;
                model.OperatorId = this.SiteUserInfo.UserId;
                model.StaffId = staffid;
                model.StartTime = string.IsNullOrEmpty(stime) ? Utils.GetDateTimeNullable(atttime) : Utils.GetDateTimeNullable(stime);
                model.Subject = sub;
                //全、迟、退、旷工 算一天 其他的用户自己填
                model.TimeCount = string.IsNullOrEmpty(count) ? 1 : Utils.GetDecimal(count); //total.Hours > 4 ? 1 : Utils.GetDecimal("0.5");
                list.Add(model);
            }
            else//时间超过一天
            {
                for (int i = 0; i < total.Days; i++)
                {
                    MGovAttendance model = new MGovAttendance();
                    model.AttendanceId = attid;
                    model.AttendanceTime = Utils.GetDateTime(stime).AddDays(i);
                    model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype, 0));
                    model.CompanyId = this.SiteUserInfo.CompanyId;
                    model.DeptId = Utils.GetInt(deptid);
                    model.EndTime = Utils.GetDateTime(etime).AddDays(-total.Days + i);
                    model.IssueTime = DateTime.Now;
                    model.OperatorId = this.SiteUserInfo.UserId;
                    model.StaffId = staffid;
                    model.StartTime = Utils.GetDateTime(stime).AddDays(i);
                    model.Subject = sub;
                    model.TimeCount = (AttendanceType)(Utils.GetInt(atttype, 0)) == AttendanceType.加班 ? Utils.GetDecimal(count) : 1;
                    list.Add(model);
                }
                if (total.Hours > 0)
                {
                    MGovAttendance model = new MGovAttendance();
                    model.AttendanceId = attid;
                    model.AttendanceTime = Utils.GetDateTime(stime).AddDays(total.Days);
                    model.AttendanceType = (AttendanceType)(Utils.GetInt(atttype, 0));
                    model.CompanyId = this.SiteUserInfo.CompanyId;
                    model.DeptId = Utils.GetInt(deptid);
                    model.EndTime = Utils.GetDateTime(etime);
                    model.IssueTime = DateTime.Now;
                    model.OperatorId = this.SiteUserInfo.UserId;
                    model.StaffId = staffid;
                    model.StartTime = Utils.GetDateTime(stime).AddDays(total.Days);
                    model.Subject = sub;
                    model.TimeCount = (AttendanceType)(Utils.GetInt(atttype, 0)) == AttendanceType.加班 ? Utils.GetDecimal(count) : 1;
                    list.Add(model);
                }
            }
            #endregion
            #region 提交保存
            BAttendance BLL = new BAttendance();
            bool result = BLL.UpdateGovAttendance(Utils.GetFormValue(staffID.UniqueID), this.SiteUserInfo.CompanyId, list);
            msg = result ? "提交保存成功！" : "提交保存失败";
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