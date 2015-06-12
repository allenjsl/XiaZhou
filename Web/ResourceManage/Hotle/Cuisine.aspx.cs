using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.ResourceManage.Hotle
{
    public partial class Cuisine : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-26
        /// 说明：资源管理：餐馆：菜系：选择菜系
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

        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            bool result = true;
            if (result)
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改成功!');;window.location='目标地址';");
            else
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改失败!');;window.location='目标地址';");
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
