using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.UserCenter.WorkExchange
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-27
    /// 说明：个人中心：工作计划 查看
    public partial class WorkExInfo : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
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
            this.lblID.Text = "";
            this.lblTtile.Text = "";
            this.lblContent.Text = "";
            this.lblPlanRemarks.Text = "";
            this.lblDateTime.Text = "";
            this.lblSubmitName.Text = "";
            this.lblGetName.Text = "";
            this.lblFristDateTime.Text = "";
            this.lblSecondDateTime.Text = "";
            this.lblState.Text = "";
            this.lblHigherRemarks.Text = "";
            this.lblBossRemarks.Text = "";
            this.lblAddDateTime.Text = "";
            this.lblUpdateTime.Text = "";
        }


       

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {

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
