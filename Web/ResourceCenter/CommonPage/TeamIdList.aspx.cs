using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common.Page;

namespace Web.ResourceCenter.CommonPage
{
    /// <summary>
    /// 团号列表
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-26
    public partial class TeamIdList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        private readonly int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }

        }
        private void DataInit()
        {
            //BTour bll = new BTour();
            //IList<ControlTour> ls = bll.GetControlTourList(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount);
            //if (ls != null && ls.Count > 0)
            //{
            //    rpt_list.DataSource = ls;
            //    rpt_list.DataBind();
            //}
            //else
            //{
            //    lbl_msg.Text = "无团号数据！";
            //    ExporPageInfoSelect1.Visible = false;
            //}
            //BindPage();

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
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
