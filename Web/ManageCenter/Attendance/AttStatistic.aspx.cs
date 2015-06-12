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
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.ComStructure;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-汇总
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-27
    public partial class AttStatistic : BackPage
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            year = Utils.GetInt(Utils.GetQueryStringValue("selYear"), year);
            month = Utils.GetInt(Utils.GetQueryStringValue("selMonth"), month);
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit();
            }
            if (UtilsCommons.IsToXls()) ToXls();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            string txtNum = Utils.GetQueryStringValue("txtNum");
            string txtName = Utils.GetQueryStringValue("txtName");
            string sectionID = Utils.GetQueryStringValue(this.SelectSection1.SelectIDClient);
            string sectionName = Utils.GetQueryStringValue(this.SelectSection1.SelectNameClient);
            this.SelectSection1.SectionName = sectionName;
            this.SelectSection1.SectionID = sectionID;
            BAttendance BLL = new BAttendance();
            MSearchInfo Model = new MSearchInfo();
            if (!string.IsNullOrEmpty(sectionID))
            {
                Model.DepartIds = sectionID;
            }
            Model.DepartName = sectionName;
            Model.FileNumber = txtNum;
            Model.Month = month;
            Model.StaffName = txtName;
            Model.Year = year;
            IList<MGovAttendanceList> lst = BLL.GetList(this.SiteUserInfo.CompanyId, Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.ExporPageInfoSelect2.intRecordCount = lst.Count;
            }

        }

        /// <summary>
        /// 生成表头日期序号
        /// </summary>
        /// <returns></returns>
        protected String getMonthDays()
        {
            StringBuilder str = new StringBuilder();
            if (year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year && month >= DateTime.MinValue.Month && month <= DateTime.MaxValue.Month)
            {
                for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
                {
                    str.Append(String.Format("<td width=\"2%\"  height=\"28\" align=\"center\" bgcolor=\"#b7e0f3\">{0}</td>", i + 1));
                }
            }
            return str.ToString();
        }
        /// <summary>
        /// 主体表格
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        protected String getTables(object AttendanceList)
        {
            StringBuilder str = new StringBuilder();

            BAttendance BLL = new BAttendance();
            IList<MGovAttendance> lst = (List<MGovAttendance>)AttendanceList;
            Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string> dic = new Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string>();
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.迟到, "<font color='#FF0000'><strong>迟</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.出差, "<font color='#800080'><strong>差</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.加班, "<font color='#0000FF'><strong>加</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.旷工, "<font color='#6600CC'><strong>旷</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.请假, "<font color='#FFA500'><strong>请</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.全勤, "<font color='#A9A9A9'>准</font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.停职, "<font color='#00BFFF'><strong>停</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.休假, "<font color='#00FF00'><strong>休</strong></font>");
            dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.早退, "<font color='#008000'><strong>退</strong></font>");
            for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                if (null != lst && lst.Count > 0)
                {
                    StringBuilder s = new StringBuilder();
                    DateTime dtCurrent = Convert.ToDateTime(String.Format("{0}-{1}-{2}", year, month, i + 1));
                    var slst = (lst.Where(item => (item.AttendanceTime.Value.Date == dtCurrent)));
                    if (slst != null && slst.ToList().Count > 0)
                    {
                        str.Append(string.Format("<td>{0}</td>", dic[slst.ToList().First().AttendanceType]));
                    }
                    else
                    {
                        str.Append(string.Format("<td></td>"));
                    }
                }
                else
                {
                    str.Append(String.Format("<td></td>"));
                }
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
        /// 导出
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            StringBuilder str = new StringBuilder();
            string txtNum = Utils.GetQueryStringValue("txtNum");
            string txtName = Utils.GetQueryStringValue("txtName");
            string sectionID = Utils.GetQueryStringValue(this.SelectSection1.SelectIDClient);
            string sectionName = Utils.GetQueryStringValue(this.SelectSection1.SelectNameClient);
            BAttendance BLL = new BAttendance();
            MSearchInfo Model = new MSearchInfo();
            if (!string.IsNullOrEmpty(sectionID))
            {
                Model.DepartIds = sectionID;
            }
            Model.DepartName = sectionName;
            Model.FileNumber = txtNum;
            Model.Month = month;
            Model.StaffName = txtName;
            Model.Year = year;
            IList<MGovAttendanceList> data = BLL.GetList(this.SiteUserInfo.CompanyId, Model);
            //加载表头
            str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/><table border='1' style='border-collapse:collapse;'><tr><th width='105'>员工编号" +
                "</th><th width='105'>部门</th><th width='107'>姓名</th>");
            //循环读取当前月的天数
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < days; i++)
            {
                str.AppendFormat("<th>{0}</th>", i + 1);
            }
            str.AppendFormat("</tr>");
            //查询出来的数据读取
            if (data != null && data.Count > 0)
            {
                Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string> dic = new Dictionary<EyouSoft.Model.EnumType.GovStructure.AttendanceType, string>();
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.迟到, "<font color='#FF0000'><strong>迟</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.出差, "<font color='#800080'><strong>差</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.加班, "<font color='#0000FF'><strong>加</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.旷工, "<font color='#6600CC'><strong>旷</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.请假, "<font color='#FFA500'><strong>请</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.全勤, "<font color='#A9A9A9'>准</font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.停职, "<font color='#00BFFF'><strong>停</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.休假, "<font color='#00FF00'><strong>休</strong></font>");
                dic.Add(EyouSoft.Model.EnumType.GovStructure.AttendanceType.早退, "<font color='#008000'><strong>退</strong></font>");
                for (int i = 0; i < data.Count; i++)
                {
                    str.AppendFormat("<tr><td align='center' style='vnd.ms-excel.numberformat:@'>{0}</td><td align='center'>{1}</td><td align='center'>{2}</td>",
                        data[i].FileNumber, data[i].DepartName, data[i].Name);
                    for (int j = 0; j < days; j++)
                    {
                        DateTime dtCurrent = Convert.ToDateTime(String.Format("{0}-{1}-{2}", year, month, j + 1));
                        if (data[i].AttendanceList != null && data[i].AttendanceList.Count > 0)
                        {
                            IList<MGovAttendance> list = data[i].AttendanceList.Where(a => (a.AttendanceTime.Value.Date == dtCurrent)).ToList();
                            if (list != null && list.Count > 0)
                            {
                                str.AppendFormat("<td>{0}</td>", dic[list.First().AttendanceType]);
                            }
                            else
                            {
                                str.Append("<td></td>");
                            }
                        }
                        else
                        {
                            str.Append("<td></td>");
                        }

                    }
                    str.Append("</tr>");
                }
            }
            str.AppendFormat("</table>");
            string browser = this.Context.Request.UserAgent.ToUpper();
            String fileName = "考勤汇总信息.xls";
            if (browser.Contains("MS") && browser.Contains("IE"))
            {
                fileName = System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }

            ResponseToXls(str.ToString(), Encoding.UTF8, fileName);
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