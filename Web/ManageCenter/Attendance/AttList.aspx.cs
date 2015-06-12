using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace Web.ManageCenter.Attendance
{
    /// <summary>
    /// 行政中心-考勤管理-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class AttList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;

        /// <summary>
        /// 考勤权限
        /// </summary>
        protected bool IsKaoQin = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                DataInit();
            }
        }
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string txtNum = Utils.GetQueryStringValue("txtNum");
            string txtName = Utils.GetQueryStringValue("txtName");
            string sectionID = Utils.GetQueryStringValue(this.SelectSection1.SelectIDClient);
            string sectionName = Utils.GetQueryStringValue(this.SelectSection1.SelectNameClient);
            this.SelectSection1.SectionID = sectionID;
            this.SelectSection1.SectionName = sectionName;
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.BLL.GovStructure.BAttendance BLL = new EyouSoft.BLL.GovStructure.BAttendance();
            IList<EyouSoft.Model.GovStructure.MAttendanceAbout> lst = BLL.GetList(txtNum, txtName,sectionID, sectionName, this.SiteUserInfo.CompanyId, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;
                }
                else
                {
                    BindPage();
                }
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='7' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        #endregion

        #region 绑定分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_栏目, false);
            }
            else { 
                IsKaoQin  = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_考勤管理_考勤管理);
            }
        } 
        #endregion
    }
}