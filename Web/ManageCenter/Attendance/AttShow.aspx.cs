using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.Model.GovStructure;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-查看
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-27
    public partial class AttShow : BackPage
    {
        protected DateTime dt;
        protected StringBuilder nullDayEndStr = new StringBuilder();//日历结尾的空li
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
            //传过来的时间
            dt = DateTime.Now;
            //根据ID初始化页面
            PageInit(id);
            //if (UtilsCommons.IsToXls()) ToXls();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            BAttendance BLL = new BAttendance();
            //统计信息(当月考勤情况)
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.Model.GovStructure.MAttendanceAbout m = BLL.GetAttendanceAbout(this.SiteUserInfo.CompanyId, id, dt.Year, dt.Month);
                if (m != null)
                {
                    this.lbAttInfo.Text = string.Format("全勤<strong class=\"red\">{0}</strong>天，迟到<strong class=\"red\">{1}</strong>天，早退<strong class=\"red\">{2}</strong>天，旷工<strong class=\"red\">{3}</strong>天，请假<strong class=\"red\">{4}</strong>天，加班<strong class=\"red\">{5}</strong>小时，停职<strong class=\"red\">{6}</strong>天，出差<strong class=\"red\">{7}</strong>天，休假<strong class=\"red\">{8}</strong>天", m.Punctuality, m.Late, m.LeaveEarly, m.Absenteeism, Math.Round(m.Vacation, 1), Math.Round(m.OverTime, 1), Math.Round(m.Suspension, 1), Math.Round(m.Travel, 1), Math.Round(m.Rest, 1));
                }
            }
        }
        /// <summary>
        /// 生成主体表格
        /// </summary>
        /// <returns></returns>
        protected string getTables()
        {   //需要传个时间过来，以判断当前月有多少天
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < DateTime.DaysInMonth(dt.Year, dt.Month); i++)
            {
                str.Append(this.getUnit(i + 1));
            }
            return str.ToString();
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目, false);
            }
        }
        /// <summary>
        /// 生成单元格（包括日期和考勤情况信息）
        /// </summary>
        /// <param name="i">当月几号</param>
        /// <returns></returns>
        protected string getUnit(int i)
        {
            Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string> dic = new Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string>();
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.迟到, "class=\"rl-red\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.出差, "class=\"rl-perpel\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.加班, "class=\"rl-blue\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.旷工, "class=\"rl-yellow\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.请假, "class=\"rl-gray\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.停职, "class=\"rl-skyblue\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.休假, "class=\"rl-lime\"");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.早退, "class=\"rl-green\"");
            DateTime dtCurrent = Convert.ToDateTime(string.Format("{0}{1}", string.Format("{0:yyyy-MM-}", dt), i < 10 ? "0" + i.ToString() : i.ToString()));
            BAttendance BLL = new BAttendance();
            IList<MGovAttendance> lst = BLL.GetList(this.SiteUserInfo.CompanyId, Utils.GetQueryStringValue("id"), dtCurrent);
            string classStr = string.Empty;
            string type = string.Empty;
            StringBuilder strInfo = new StringBuilder();
            string id = Utils.GetQueryStringValue("id");
            if (null != lst && lst.Count > 0)
            {
                foreach (MGovAttendance m in lst)
                {
                    if (m.AttendanceType != AttendanceType.全勤)
                    {
                        classStr = dic[m.AttendanceType];
                    }
                    if (m.AttendanceType == AttendanceType.全勤)
                    {
                        strInfo.AppendFormat("<a href='AttDetail.aspx?doType=update&attID={0}&date={2}&sl={3}' data-class='attDetail'>{1}</a>", m.AttendanceId,
                            m.AttendanceType.ToString(), string.Format("{0:yyyy-MM-dd}", dtCurrent), Utils.GetQueryStringValue("sl"));
                    }
                    else
                    {
                        bool flag = m.AttendanceType == EyouSoft.Model.EnumType.GovStructure.AttendanceType.加班;
                        strInfo.AppendFormat("<a href='AttDetail.aspx?doType=update&attID={1}&date={2}&sl={3}' class='attendBt' data-class='attDetail'>{0}</a>",
                            m.AttendanceType.ToString(), m.AttendanceId,
                            string.Format("{0:yyyy-MM-dd}", dtCurrent), Utils.GetQueryStringValue("sl"));
                        strInfo.AppendFormat("<div style='display: none'>");
                        strInfo.AppendFormat("<table cellspacing='0' cellpadding='0' border='0' width='100%'><tr>");
                        strInfo.AppendFormat("<td height='23' align='right'>考勤时间：</td>");
                        strInfo.AppendFormat("<td align='left'>{0}</td></tr>", string.Format("{0:yyyy-MM-dd}", m.AttendanceTime));
                        strInfo.AppendFormat("<tr><th align='right' valign='top'>考勤情况：</th>");
                        strInfo.AppendFormat("<td>{0}时间:{1}至{2}&nbsp;{0}{3}：",
                            m.AttendanceType.ToString(),
                            string.Format("{0:yyyy-MM-dd HH:mm}", m.StartTime),
                            string.Format("{0:yyyy-MM-dd HH:mm}", m.EndTime),
                            flag ? "时数" : "天数");
                        strInfo.AppendFormat("<strong class='fontred'>{0}{1}</strong>", string.Format("({0})", Math.Round(m.TimeCount, 1)),
                            flag ? "小时" : "天");
                        strInfo.AppendFormat("&nbsp;<p>{0}原因：{1}</p></td></tr></table></div> ", m.AttendanceType.ToString(), m.Subject);
                    }
                }
            }
            return string.Format("<li {0}><p>&nbsp;{1}&nbsp;</p><p>&nbsp;{2}&nbsp;</p></li>", classStr, i < 10 ? "0" + i.ToString() : i.ToString(), strInfo.ToString());
        }
        /// <summary>
        /// 判断当前月的第一天是星期几，如：如果是星期二，则生成一个空的"li",以便repeater的第一天星期数正确
        /// </summary>
        /// <returns></returns>
        protected string getNullDay()
        {
            StringBuilder str = new StringBuilder("");
            //当月一号为星期几
            int i = (int)(Convert.ToDateTime(string.Format("{0}-{1}-01", dt.Year, dt.Month)).DayOfWeek);//0(星期日)-6(星期六)
            int m = 0;//要生成空li的个数
            switch (i)
            {
                case 0:
                    m = 6;
                    break;
                default:
                    m = i - 1;
                    break;
            }
            //日历开始的空单元格
            for (int j = 0; j < m; j++)
            {
                str.Append("<li><p>&nbsp;</p><p>&nbsp;</p></li>");
            }
            //日历结束的空单元格
            for (int j = 0; j < 42 - DateTime.DaysInMonth(dt.Year, dt.Month) - m; j++)
            {
                this.nullDayEndStr.Append("<li><p>&nbsp;</p><p>&nbsp;</p></li>");
            }
            return str.ToString();
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