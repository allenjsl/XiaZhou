using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Common;

namespace Web.ResourceManage.Path
{
    public partial class Goustlist : BackPage
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
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获得操作ID
            string id = Utils.GetQueryStringValue("routeid");
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.SourceStructure.MAcceptGuestModel> List = bll.GetSKModelList(id, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
            if (List != null && List.Count > 0)
            {
                this.RptList.DataSource = List;
                this.RptList.DataBind();
                BindPage();
            }
            else
            {
                this.Label1.Text = "<tr class=\"odd\"><td height=\"30px\" colspan=\"6\" align=\"center\">暂无交易信息</td></tr>";
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
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #region 前台调用方法
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="id">SourceId</param>
        /// <returns></returns>
        protected string GetContactInfo(object sellModel)
        {
            EyouSoft.Model.ComStructure.MComUser Muser = (MComUser)sellModel;
            return "销售员:" + Muser.ContactName + "</br>电话:" + Muser.ContactTel + "</br>手机:" + Muser.ContactMobile;
        }
        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="sellModel"></param>
        /// <returns></returns>
        protected string GetUnitInfo(object unitname, object contactname, object contacttel)
        {
            return "<b>" + unitname.ToString() + "</b></br>联系人:" + contactname.ToString() + "</br>联系方式:" + contacttel;
        }
        #endregion

    }
}
