using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.CrmStructure;
using EyouSoft.Model.CrmStructure;
namespace Web.QualityCenter.TeamVisit
{
    /// <summary>
    /// 质量管理-团队回访-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-10
    public partial class VisitList : BackPage
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
        #endregion
        protected bool isVisit = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();

                //初始化
                DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //参数获取
            string teamNum = Utils.GetQueryStringValue("teamNum");//团号
            string lineName = Utils.GetQueryStringValue("lineName");//线路名称
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startTime"));//出团日期(始)
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endTime"));//出团日期(终)
            string unitname = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);
            string txtGuid = Utils.GetQueryStringValue("txtGuid");
            BCrmVisit BLL = new BCrmVisit();
            MVisitListModel Model = new MVisitListModel();
            Model.CompanyId = SiteUserInfo.CompanyId;
            Model.RouteName = lineName;
            Model.TourCode = teamNum;
            Model.UnitName = unitname;
            if (!string.IsNullOrEmpty(txtGuid))
            {
                string[] arry = { txtGuid };
                Model.GuideName = arry;
            }

            IList<EyouSoft.Model.CrmStructure.MVisitListModel> lst = BLL.GetVisitShowModel(Model, startTime, endTime, this.pageIndex, this.pageSize, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                if (pageSize > recordCount)
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='11' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
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
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_团队回访_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_团队回访_栏目, false);
            }
            else
            {
                isVisit = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_团队回访_登记);
            }
        }
        #endregion
        #region 前台调用方法
        protected String GetGuidName(object o)
        {
            String[] str = { };
            if (!String.IsNullOrEmpty(Convert.ToString(o)))
            {
                str = (String[])o;
            }
            return String.Join(",", str.ToArray());
        }
        #endregion
    }
}