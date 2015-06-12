using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;

namespace Web.UserControl
{
    public partial class HeadUserControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-27
        /// 说明：模板页头部用户信息

        private MUserInfo _userInfo = null;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public MUserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
            set
            {
                _userInfo = value;
            }
        }

        protected string resultPrivs = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
                if (SiteUserInfo != null)
                {

                    this.lblUserName.Text = SiteUserInfo.Name;
                }
            }
        }


        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            if (!SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目) &&
                !SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目) &&
                !SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_公告通知_栏目))
            {
                this.phPersonCenter.Visible = false;
            }
            if (SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目))
                resultPrivs += "<a href=\"/UserCenter/WorkExchange/WorkReportList.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_工作交流+ "\"><s></s>工作交流</a>";

            if (SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目))
                resultPrivs += "<a href=\"/UserCenter/WorkAwake/OrderRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 + "\"><s></s>事务提醒</a>";

            if (SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_公告通知_栏目))
                resultPrivs += "<a href=\"/UserCenter/Notice/NoticeList.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_公告通知+ "\"><s></s>公告通知</a>";

            if (SiteUserInfo.Privs.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目))
                resultPrivs += "*<a href=\"/SmsCenter/SendMessage.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.常用工具_短信中心+ "\"><s></s>短信中心</a>";
            else
                this.phTools.Visible = false;
        }
    }
}