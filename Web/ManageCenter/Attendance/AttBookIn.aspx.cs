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
using EyouSoft.Model.EnumType.GovStructure;
using System.Web.UI.HtmlControls;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-登记
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-16
    public partial class AttBookIn : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("id");
                PageInit(id);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            this.txtTime.Value = String.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now);
            this.ckAdd.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.加班);
            this.ckChiDao.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.迟到);
            this.ckKuangGong.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.旷工);
            this.ckLeave.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.请假);
            this.ckZaoTui.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.早退);
            //this.ckZuiDian.Value = Convert.ToString((int)EyouSoft.Model.EnumType.GovStructure.AttendanceType.准点);
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string id)
        {
            //数据验证开始
            StringBuilder strMsg = new StringBuilder();
            if (String.IsNullOrEmpty(this.txtTime.Value))
            {
                strMsg.Append("考勤时间不能为空！\\n");
            }
            if (!this.ckChiDao.Checked && !this.ckKuangGong.Checked && !this.ckZaoTui.Checked && !this.ckZuiDian.Checked && !this.ckLeave.Checked && !this.ckAdd.Checked)
            {
                strMsg.Append("考勤类型必须选择！\\n");
            }
            if (this.ckLeave.Checked)
            {
                if (String.IsNullOrEmpty(this.txtLeaveStartTime.Value) || String.IsNullOrEmpty(this.txtLeaveEndTime.Value))
                {
                    strMsg.Append("请假时间不能为空！\\n");
                }
                else
                {
                    if (Convert.ToDateTime(this.txtLeaveEndTime.Value).Subtract(Convert.ToDateTime(this.txtLeaveStartTime.Value)).Ticks < 0)
                    {
                        strMsg.Append("请假结束时间不能早于开始时间！\\n");
                    }
                }
            }
            if (this.ckAdd.Checked)
            {
                if (String.IsNullOrEmpty(this.txtAddEndTime.Value) || String.IsNullOrEmpty(this.txtAddStartTime.Value))
                {
                    strMsg.Append("加班时间不能为空！\\n");
                }
                else
                {
                    if (Convert.ToDateTime(this.txtAddEndTime.Value).Subtract(Convert.ToDateTime(this.txtAddStartTime.Value)).Ticks < 0)
                    {
                        strMsg.Append("加班结束时间不能早于开始时间！\\n");
                    }
                }
            }
            if (strMsg.Length > 0)
            {
                MessageBox.ResponseScript(this, String.Format("alert('{0}');", strMsg));
                return;
            }
            //数据验证结束
            EyouSoft.BLL.GovStructure.BAttendance BLL = new EyouSoft.BLL.GovStructure.BAttendance();
            List<EyouSoft.Model.GovStructure.MGovAttendance> lst = new List<EyouSoft.Model.GovStructure.MGovAttendance>();
            String workStart = "08:00";//工作时间（始）
            String workEnd = "18:00";//工作时间（终）
            foreach (Control c in this.form1.Controls)
            {
                if (c.GetType().ToString().Equals("System.Web.UI.HtmlControls.HtmlInputCheckBox"))
                {
                    System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)c;
                    if (ck.Checked)
                    {
                        if (ck == this.ckLeave)//请假
                        {
                            DateTime? leaveStartTime = Utils.GetDateTimeNullable(this.txtLeaveStartTime.Value);
                            DateTime? leaveEndTime = Utils.GetDateTimeNullable(this.txtLeaveEndTime.Value);
                            if (null != leaveStartTime && null != leaveEndTime)
                            {
                                this.getDays(leaveStartTime.ToString(), leaveEndTime.ToString(),  workStart, workEnd, lst,ck,id);
                            }
                        }
                        else if (ck == this.ckAdd)//加班
                        {
                            DateTime? addStartTime = Utils.GetDateTimeNullable(this.txtAddStartTime.Value);
                            DateTime? addEndTime = Utils.GetDateTimeNullable(this.txtAddEndTime.Value);
                            if (null != addStartTime && null != addEndTime)
                            {
                                this.getDays(addStartTime.ToString(), addEndTime.ToString(), workStart, workEnd, lst, ck,id);
                            }
                        }
                        else
                        {
                            //检查是否已经登记过考勤
                            if (BLL.ExistsNum(this.SiteUserInfo.CompanyId, id, Convert.ToDateTime(this.txtTime.Value), (AttendanceType)Utils.GetInt(ck.Value), ""))
                            {
                                MessageBox.ResponseScript(this, "alert('抱歉， 该用户的考勤已经登记过了！');");
                                return;
                            }
                            MGovAttendance Model = new MGovAttendance();
                            this.commonModel(Model, id, ck,Convert.ToDateTime(this.txtTime.Value));
                            lst.Add(Model);
                        }
                    }
                }
            }
            bool result = BLL.AddAllGovAttendanceList(lst);
            if (result)
            {
                Utils.ShowMsgAndCloseBoxy("登记成功！", Utils.GetQueryStringValue("IframeId"), true);
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('登记失败!');");
            }
        }
        protected void commonModel(MGovAttendance Model,String id,HtmlInputCheckBox ck,DateTime dt)
        {
            Model.AttendanceType = (EyouSoft.Model.EnumType.GovStructure.AttendanceType)Utils.GetInt(ck.Value);
            //如果是“准点、迟到、早退、旷工”，考勤时间为第一个文本框的值 。
            //如果是“请假、加班”，则为遍历时的当天时间。
            Model.AttendanceTime = dt;
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.IssueTime = DateTime.Now;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.StaffId = id;
            if (ck == this.ckLeave)
            {
                Model.Subject = this.txtLeaveCause.Value;
            }
            if (ck == this.ckAdd)
            {
                Model.Subject = this.txtAddContent.Value;
            }
        }
        /// <summary>
        /// 时间分段
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="workStart">工作时间（始）</param>
        /// <param name="workEnd">工作时间（终）</param>
        protected void getDays(String start, String end, String workStart, String workEnd, List<MGovAttendance> lst,HtmlInputCheckBox ck,String id)
        {
            MGovAttendance Model;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            DateTime oldStartDay =Convert.ToDateTime(dtStart.ToShortDateString());
            DateTime oldEndDay = Convert.ToDateTime(dtEnd.ToShortDateString());
            String oldStartStr = Convert.ToDateTime(start).ToShortTimeString();
            String oldEndStr = Convert.ToDateTime(end).ToShortTimeString();
            String newStartStr =workStart ;//如："08:00" 工作时间（始）
            String newEndStr =workEnd ;//如："18:00" 工作时间（终）
            String[] oldStart = oldStartStr.Split(':');
            String[] oldEnd = oldEndStr.Split(':');
            String[] newStart = newStartStr.Split(':');
            String[] newEnd = newEndStr.Split(':');
            int oldStartSec = Utils.GetInt(oldStart[0]) * 3600 + Utils.GetInt(oldStart[1]) * 60;
            int oldEndSec = Utils.GetInt(oldEnd[0]) * 3600 + Utils.GetInt(oldEnd[1]) * 60;
            int newStartSec = Utils.GetInt(newStart[0]) * 3600 + Utils.GetInt(newStart[1]) * 60;
            int newEndSec = Utils.GetInt(newEnd[0]) * 3600 + Utils.GetInt(newEnd[1]) * 60;
            //2011-10-25 邵：按8：00-18：00拆分
            if (String.Equals(oldStartDay, oldEndDay))
            { //开始日期和结束日期是同一天
                Model = new MGovAttendance();
                String strStart = (oldStartSec > newStartSec) ? oldStartStr : newStartStr;
                String strEnd = (oldEndSec > newEndSec) ? newEndStr : oldEndStr;
                this.commonModel(Model, id, ck,dtStart);
                Model.StartTime = Utils.GetDateTimeNullable(String.Format("{0} {1}", String.Format("{0:yyyy-MM-dd}", oldStartDay), strStart));
                Model.EndTime = Utils.GetDateTimeNullable(String.Format("{0} {1}", String.Format("{0:yyyy-MM-dd}", oldEndDay), strEnd));
                decimal timecouts = Convert.ToDecimal(Math.Round(Convert.ToDateTime(Model.EndTime).Subtract(Convert.ToDateTime(Model.StartTime)).TotalSeconds / ((newEndSec - newStartSec) * 1.0), 1));
                if (ck == this.ckAdd)
                {
                    Model.TimeCount = timecouts * 24;//加班是按小时算
                }
                else
                {
                    Model.TimeCount = timecouts;//请假是按天算   
                }
                lst.Add(Model);
            }
            else
            {  //开始日期和结束日期不是同一天，且结束日期大于开始日期（数据验证中有验证）
                int span = oldEndDay.Subtract(oldStartDay).Days;
                DateTime dt = oldStartDay;
                String strStart = (oldStartSec > newStartSec) ? oldStartStr : newStartStr;
                String strEnd = (oldEndSec > newEndSec) ? newEndStr : oldEndStr;
                String st = String.Empty;
                String se = String.Empty;
                for (int i = 0; i <= span; i++)
                {
                    Model = new MGovAttendance();
                    this.commonModel(Model, id, ck,dt);
                    st = i == 0 ? strStart : newStartStr;
                    se = i == span ? strEnd : newEndStr;
                    Model.StartTime = Utils.GetDateTimeNullable(String.Format("{0} {1}", String.Format("{0:yyyy-MM-dd}", dt), st));
                    Model.EndTime = Utils.GetDateTimeNullable(String.Format("{0} {1}", String.Format("{0:yyyy-MM-dd}", dt), se));
                    decimal timecouts=Convert.ToDecimal(Math.Round(Convert.ToDateTime(Model.EndTime).Subtract(Convert.ToDateTime(Model.StartTime)).TotalSeconds / ((newEndSec - newStartSec) * 1.0), 1));
                    if (ck == this.ckAdd)
                    {
                        Model.TimeCount = timecouts * 24;//加班是按小时算
                    }
                    else
                    {
                        Model.TimeCount = timecouts;//请假是按天算   
                    }
                    lst.Add(Model);
                    dt = dt.AddDays(1);
                }
            }
        }
        protected void btnClick(object sender, EventArgs e)
        {
            this.PageSave(Utils.GetQueryStringValue("id"));
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.行政中心_考勤管理_登记))
            {
                //Utils.ResponseNoPermit(Common.Enum.TravelPermission.行政中心_考勤管理_登记, false);
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