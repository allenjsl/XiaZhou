using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.TeamCenter
{
    /// <summary>
    /// 线路信息選擇彈出框
    /// 修改人：DYZ 創建日期：2011-9-22
    /// </summary>
    public partial class selectLine : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private int pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

                AreaInit();

                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MRouteListModel seachModel = new EyouSoft.Model.SourceStructure.MRouteListModel();
            seachModel.CompanyId = SiteUserInfo.CompanyId;
            seachModel.RouteAreaId = Utils.GetInt(Utils.GetQueryStringValue("areaId"));
            seachModel.RouteName = Utils.GetQueryStringValue("txtRouteName");
            IList<EyouSoft.Model.SourceStructure.MRouteListModel> list = bll.GetRouteShowModel(seachModel, null, null, pageIndex, pageSize, ref recordCount);
            rpt_List.DataSource = list;
            rpt_List.DataBind();
            BindPage();
            if (list == null || list.Count < 40)
            {
                this.ExporPageInfoSelect1.Visible = false;
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

        /// <summary>
        /// 线路区域初始化
        /// </summary>
        private void AreaInit()
        {
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(this.SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptAreaList.DataSource = list;
                this.rptAreaList.DataBind();
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
    }
}
