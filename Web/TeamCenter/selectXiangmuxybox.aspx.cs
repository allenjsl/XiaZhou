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
    /// 報價項目選擇彈出框
    /// 創建人：田想兵 創建日期：2011-9-22
    /// </summary>
    public partial class selectXiangmuxybox : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                Response.Clear();
                switch (doType)
                {
                    case "save":
                        Response.Write(PageSave());
                        break;
                }
                Response.End();
            }
            #endregion

            if (!IsPostBack)
            {
                PageInit();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private string PageSave()
        {
            string msg = string.Empty;
            //获得保存内容的类型
            EyouSoft.Model.EnumType.ComStructure.ContainProjectType containProjectType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(Utils.GetQueryStringValue("proType"));
            EyouSoft.Model.EnumType.ComStructure.ProjectType projectType = Utils.GetQueryStringValue("type") == "" ? EyouSoft.Model.EnumType.ComStructure.ProjectType.包含项目 : (EyouSoft.Model.EnumType.ComStructure.ProjectType)Utils.GetInt(Utils.GetQueryStringValue("type"));

            string txtNewInfo = Utils.GetFormValue("txtNewInfo");
            if (txtNewInfo.Trim() == "")
            {
                msg = UtilsCommons.AjaxReturnJson("0", "保存内容不能为空!");
                return msg;
            }
            EyouSoft.BLL.ComStructure.BComProject bll = new EyouSoft.BLL.ComStructure.BComProject();
            EyouSoft.Model.ComStructure.MComProject model = new EyouSoft.Model.ComStructure.MComProject();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Content = txtNewInfo;
            model.IssueTime = DateTime.Now;
            model.ItemType = containProjectType;
            model.OperatorId = SiteUserInfo.UserId;
            model.Type = projectType;
            if (bll.Add(model))
            {
                msg = UtilsCommons.AjaxReturnJson("1", "保存成功!");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "保存失败!");
            }
            return msg;
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            EyouSoft.Model.EnumType.ComStructure.ContainProjectType? containProjectType = null;
            if (Utils.GetQueryStringValue("proType") != "")
            {
                containProjectType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(Utils.GetQueryStringValue("proType"));
            }
            EyouSoft.Model.EnumType.ComStructure.ProjectType projectType = Utils.GetQueryStringValue("type") == "" ? EyouSoft.Model.EnumType.ComStructure.ProjectType.包含项目 : (EyouSoft.Model.EnumType.ComStructure.ProjectType)Utils.GetInt(Utils.GetQueryStringValue("type"));

            EyouSoft.BLL.ComStructure.BComProject bll = new EyouSoft.BLL.ComStructure.BComProject();
            IList<EyouSoft.Model.ComStructure.MComProject> list = bll.GetList(projectType, containProjectType, this.SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                rpt_List.DataSource = list;
                rpt_List.DataBind();
            }
            bll = null;
            list = null;

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
