using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.CommonPage
{
    public partial class HrSelect : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 16;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int recordCount = 0;

        protected int listCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                this.DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string name = Utils.GetQueryStringValue("userName");
            bool isshow = Utils.GetQueryStringValue("isShow").ToLower() == "true";
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            string sModel = Utils.GetQueryStringValue("sModel");
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            EyouSoft.Model.GovStructure.MSearchGovFile model = new EyouSoft.Model.GovStructure.MSearchGovFile();
            model.Name = name;
            if (!isshow)
            {
                model.IsAccount = isshow;
            }
            if (sModel != "2")
            {
                this.ph_checkbox.Visible = false;
            }
            model.NoLeft = true;
            IList<EyouSoft.Model.GovStructure.MGovFile> lst = BLL.GetSearchArchivesList(model, this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                listCount = lst.Count;
                BindPage();
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<li style='text-align:center;'>对不起，没有相关数据！</li>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
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

        }
        #endregion
    }
}
