using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SystemSet
{
    /// <summary>
    /// 线路区域列表
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class RouterAreaList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("state") == "del")
            {
                DelArea();
            }
            PageInit();
        }

        //删除线路区域
        private void DelArea()
        {
            int[] AreaIds = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            Response.Clear();
            if (new EyouSoft.BLL.ComStructure.BComArea().Delete(SiteUserInfo.CompanyId, AreaIds))
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
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                this.repList.EmptyText = "<tr><td colspan=\"5\">暂无线路区域!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
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

            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        public string GetMComAreaPlan(object o)
        {
            if (o == null) return "";
            string str = string.Empty;
            IList<EyouSoft.Model.ComStructure.MComAreaPlan> list = (IList<EyouSoft.Model.ComStructure.MComAreaPlan>)o;
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].Planer + ",";
            }
            if (str != "") str = str.TrimEnd(',');
            return str;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_线路区域栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_线路区域栏目, false);
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
