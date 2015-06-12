using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.GovStructure;
using EyouSoft.BLL.GovStructure;
using System.Text;
namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-个人考勤表
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-16
    public partial class MyAtt : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                string id = Utils.GetQueryStringValue("id");
                DataInit(id); 
                this.initDate();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(String id)
        {
            //查询参数
            int selYear =Utils.GetInt(Utils.GetQueryStringValue("selYear"));//年
            int selMonth = Utils.GetInt(Utils.GetQueryStringValue("selMonth"));//月
            //绑定表格数据
            if (selYear >= 1 && selYear <= 9999 && selMonth >= 1 && selMonth <= 12)
            {
                //统计信息
                BAttendance attBLL = new BAttendance();
                MAttendanceAbout attModel = attBLL.GetAttendanceAbout(this.SiteUserInfo.CompanyId, id, selYear, selMonth);
                if (null != attModel)
                {
                    this.lbAttInfo.Text = string.Format("准点<strong class=\"red\">{0}</strong>天，迟到<strong class=\"red\">{1}</strong>天，早退<strong class=\"red\">{2}</strong>天，旷工<strong class=\"red\">{3}</strong>天，请假<strong class=\"red\">{4}</strong>天，加班<strong class=\"red\">{5}</strong>小时", attModel.Punctuality, attModel.Late, attModel.LeaveEarly, attModel.Absenteeism, Math.Round(attModel.Vacation, 1), Math.Round(attModel.OverTime, 1));
                }
                //表头编号
                List<string> lstDays = new List<string>();
                for (int i = 0; i < DateTime.DaysInMonth(selYear, selMonth); i++)
                {
                    lstDays.Add(i.ToString());
                }
                this.RepDateList.DataSource = lstDays;
                this.RepDateList.DataBind();
                //考勤状况
                this.RepList.DataSource = lstDays;
                this.RepList.DataBind(); 
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {

        }
        /// <summary>
        /// 生成单元格（包括日期和考勤情况信息）
        /// </summary>
        /// <param name="i">当月几号</param>
        /// <returns></returns>
        protected string getUnit(int i)
        {
            int selYear = Utils.GetInt(Utils.GetQueryStringValue("selYear"));//年
            int selMonth = Utils.GetInt(Utils.GetQueryStringValue("selMonth"));//月
            DateTime dtCurrent = Convert.ToDateTime(String.Format("{0}-{1}-{2}", selYear, selMonth, i));
            BAttendance BLL = new BAttendance();
            IList<MGovAttendance> lst = BLL.GetList(this.SiteUserInfo.CompanyId, Utils.GetQueryStringValue("id"), dtCurrent);
            String type = String.Empty;
            StringBuilder strInfo = new StringBuilder();
            if (null != lst && lst.Count > 0)
            {
                foreach (MGovAttendance m in lst)
                {
                    strInfo.Append(m.AttendanceType);
                }
            }
            return strInfo.ToString();
        }
        /// <summary>
        /// 初始化年月
        /// </summary>
        protected void initDate()
        {
            //年份绑定为当前年份的前十年
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
            {
                this.selYear.Items.Add(new ListItem(currentYear.ToString(), currentYear.ToString()));
                currentYear--;
            }
            this.selYear.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion
    }
}