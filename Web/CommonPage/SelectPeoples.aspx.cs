using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;
using EyouSoft.Common;

namespace Web.CommonPage
{
    public partial class SelectPeoples : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
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
                //权限判断
                PowerControl();
                string userName = Utils.GetQueryStringValue("userName");
                //初始化

                DataInit(userName);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string userName)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.ComStructure.MComUserSearch searchModel = new EyouSoft.Model.ComStructure.MComUserSearch();
            searchModel.UserName = userName;
            IList<EyouSoft.Model.ComStructure.MComUser> userList = new EyouSoft.BLL.ComStructure.BComUser().GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId, searchModel);
            if (userList != null && userList.Count > 0)
            {
                this.rptList.DataSource = userList;
                this.rptList.DataBind();
                listCount = userList.Count;
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
            }

            //绑定分页
            BindPage();
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            //this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            //this.ExportPageInfo1.UrlParams = Request.QueryString;
            //this.ExportPageInfo1.intPageSize = pageSize;
            //this.ExportPageInfo1.CurrencyPage = pageIndex;
            //this.ExportPageInfo1.intRecordCount = recordCount;
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
