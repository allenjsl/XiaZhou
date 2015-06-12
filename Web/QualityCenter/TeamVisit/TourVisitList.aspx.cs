using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.QualityCenter.TeamVisit
{
    public partial class TourVisitList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 10;
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
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }
        private void DataInit()
        {
            DateTime? txtCrmStime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCrmStime"));
            DateTime? txtCrmEtime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCrmEtime"));
            string tourId = Utils.GetQueryStringValue("id");

            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            EyouSoft.BLL.CrmStructure.BCrmVisit BLL = new EyouSoft.BLL.CrmStructure.BCrmVisit();
            IList<EyouSoft.Model.CrmStructure.MCrmVisit> lst = BLL.GetCrmVisit(pageIndex, pageSize, ref recordCount, this.SiteUserInfo.CompanyId, tourId, txtCrmStime, txtCrmEtime);
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='5' height='28' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }


        /// <summary>
        /// 分页控件绑定
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
        }

    }
}
