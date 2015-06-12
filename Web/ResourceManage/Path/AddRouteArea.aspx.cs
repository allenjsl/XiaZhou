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
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using System.Collections.Generic;

namespace Web.ResourceManage.Path
{
    public partial class AddRouteArea : BackPage
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
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                }
            }
            #endregion

            if (!IsPostBack)
            {
                this.SellsSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit();
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {

        }

        /// <summary>
        /// 保存执行方法
        /// </summary>
        private string PageSave()
        {
            EyouSoft.BLL.ComStructure.BComArea bll = new EyouSoft.BLL.ComStructure.BComArea();
            EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.Model.ComStructure.MComArea();
            string areaName = Utils.GetFormValue(this.txtAreaName.UniqueID);
            string planerName = Utils.GetFormValue(SellsSelect1.SellsNameClient);
            string planerID = Utils.GetFormValue(SellsSelect1.SellsIDClient);
            string areatype = Utils.GetFormValue("selecttype");
            if (string.IsNullOrEmpty(planerName.Trim()))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "请选择责任计调");
            }
            model.AreaName = areaName;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.Type = (EyouSoft.Model.EnumType.ComStructure.AreaType)Utils.GetInt(areatype);
            //责任计调
            MComAreaPlan plan = new MComAreaPlan();
            if (planerName.Split(',').Length > 0)
            {
                for (int i = 0; i < planerName.Split(',').Length; i++)
                {
                    plan.OperatorId = planerName.Split(',')[i].ToString();
                }
            }
            else
            {
                plan.OperatorId = planerName;
            }
            IList<MComAreaPlan> planlist = new List<MComAreaPlan>();
            planlist.Add(plan);
            model.Plan = planlist;
            string msg = string.Empty;
            int result = bll.Add(model);
            if (result > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("id", result);
                dic.Add("type", model.Type);
                dic.Add("name", model.AreaName);
                msg = UtilsCommons.AjaxReturnJson("1", "新增成功", dic);
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "新增失败");
            }
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
