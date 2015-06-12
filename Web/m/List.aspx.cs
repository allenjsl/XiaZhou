using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.m
{
    /// <summary>
    /// 手机版导游报账
    /// 创建人：赵晓慧
    /// 创建时间：2012-07-09
    /// </summary>
    public partial class List : EyouSoft.Common.Page.MobilePage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        { 
            PowerControl();
            GetList();
        }

        #region 私有方法
        /// <summary>
        /// 获取列表
        /// </summary>
        private void GetList()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"),1);
            EyouSoft.Model.TourStructure.MBZSearch Search = new EyouSoft.Model.TourStructure.MBZSearch();
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            Search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            Search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStarTime"));
            Search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            Search.IsDealt = false;
            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetGuidBZList(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, Search, this.SiteUserInfo.DeptId);
            if (list != null && list.Count > 0)
            {
                this.replist.DataSource = list;
                this.replist.DataBind();
                if (recordCount > pageSize)
                {
                    //绑定分页
                    BindPage();
                }
                else {
                    this.ExporPageInfoSelect1.Visible = false;
                }
            }
            else {
                this.ExporPageInfoSelect1.Visible = false;
                this.lblMsg.Text = "未搜索到相关线路!";
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
        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目))
            {
                Utils.MobileResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目);
                return;
            }
        }
        #endregion
    }
}