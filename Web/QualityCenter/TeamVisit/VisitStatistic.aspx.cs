using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.QualityCenter.TeamVisit
{
    /// <summary>
    /// 质量管理-团队回访-每日汇总
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-11
    public partial class VisitStatistic : BackPage
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
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            DateTime? txtCrmStime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCrmStime"));
            DateTime? txtCrmEtime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCrmEtime"));
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            EyouSoft.BLL.CrmStructure.BCrmVisit BLL = new EyouSoft.BLL.CrmStructure.BCrmVisit();
            IList<EyouSoft.Model.CrmStructure.MDayTotalModel> lst = BLL.GetDayTotalModelList(this.SiteUserInfo.CompanyId, txtCrmStime, txtCrmEtime, pageIndex, pageSize, ref recordCount);
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='9' height='28' align='center'>对不起，没有相关数据！</td></tr>" });
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

        protected String GetGuidName(object o)
        {
            String[] str = { };
            if (!String.IsNullOrEmpty(Convert.ToString(o)))
            {
                str = (String[])o;
            }
            return String.Join(",", str.ToArray());
        }

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

        #endregion
    }
}