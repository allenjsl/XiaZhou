using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.PageT
{
    public partial class WebEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                Response.Clear();
                switch (doType)
                {
                    case "delete":
                        Response.Write(DeleteData("id"));
                        break;
                    case "save":

                        Response.Write(PageSave());
                        break;
                }
                Response.End();
            }
            #endregion

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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            string msg = "{result:1,msg:'保存成功!'}";

            return msg;
        }

        /// <summary>
        /// 保存执行方法
        /// </summary>
        private string PageSave()
        {
            string msg = "{result:1,msg:'保存成功!'}";

            return msg;
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
