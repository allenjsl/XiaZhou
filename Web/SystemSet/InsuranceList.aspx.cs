using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 基础数据保险
    /// 修改记录：
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public partial class InsuranceList : BackPage
    {
        protected int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("state") == "del")
            {
                DelInsurance();
            }
            else
            {
                PageInit();
            }
        }

        //删除保险
        private void DelInsurance()
        {
            Response.Clear();
            string InsuranceIds = Utils.GetQueryStringValue("ids");
            if (new EyouSoft.BLL.ComStructure.BComInsurance().Delete(InsuranceIds, CurrentUserCompanyID))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功"));

            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "删除失败"));
            }
            Response.End();
        }

        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComInsurance> list = new EyouSoft.BLL.ComStructure.BComInsurance().GetList(SiteUserInfo.CompanyId);
            if (list == null || list.Count == 0)
            {
                this.repList.EmptyText = "<tr><td colspan=\"8\">暂无保险信息!</td></tr>";
            }
            else
            {
                i = list.Count;
                this.repList.DataSource = list;
                this.repList.DataBind();
            }
        }

        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_保险类型栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_保险类型栏目, false);
                return;
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
