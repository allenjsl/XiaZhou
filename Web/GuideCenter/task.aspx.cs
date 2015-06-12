using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.GuideCenter
{
    /// <summary>
    /// 导游任务选择
    /// 创建人：李晓欢
    /// 创建时间：2011-09-19
    /// </summary>
    public partial class task :  EyouSoft.Common.Page.BackPage
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
        //任务安排
        protected string TaskName = "";
        //团号
        protected string TourCode = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
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
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            TaskName = Utils.GetQueryStringValue("txtTaskName");
            TourCode = Utils.GetQueryStringValue("txtTourCode");
            //绑定分页
            BindPage();
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
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion

        #region 设置弹出窗体
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);           
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion
    }
}
