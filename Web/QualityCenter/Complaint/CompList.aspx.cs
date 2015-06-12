using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.CrmStructure;

namespace Web.QualityCenter.Complaint
{
    /// <summary>
    /// 质量管理-投诉管理-投诉列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-11
    public partial class CompList : BackPage
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


        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            if (!IsPostBack)
            {
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
            string teamNum = Utils.GetQueryStringValue("teamNum");//团号
            string lineName = Utils.GetQueryStringValue("lineName");//线路名称
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startTime"));//出团日期(始)
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endTime"));//出团日期(终)
            string complaintsType = Utils.GetQueryStringValue("complaintsType");
            string complaintsName = Utils.GetQueryStringValue("complaintsName");
            BCrmComplaint BLL = new BCrmComplaint();
            EyouSoft.Model.CrmStructure.MComplaintsSearchModel model = new EyouSoft.Model.CrmStructure.MComplaintsSearchModel();
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.EndTime = endTime;
            model.RouteName = lineName;
            model.StartTime = startTime;
            model.TourCode = teamNum;
            model.ComplaintsName = complaintsName;
            model.ComplaintsType = complaintsType;
            IList<EyouSoft.Model.CrmStructure.MComplaintsListModel> lst = BLL.GetVisitShowModel(model, pageIndex, pageSize, ref recordCount);
            if (lst != null && lst.Count > 0)
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='9' align='center'>对不起，没有相关数据！</td></tr>" });
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
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目, false);
            }
            else
            {
                ph_tousu.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_登记);
            }
        }
        #endregion


    }
}