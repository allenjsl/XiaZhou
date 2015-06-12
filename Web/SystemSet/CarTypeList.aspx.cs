using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 车型管理
    /// 创建人：赵晓慧
    /// 创建时间：2012-08-13
    /// </summary>
    public partial class CarTypeList : BackPage
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
            if (Utils.GetQueryStringValue("state")=="del")
            {
                DelCarType();
            }
            PageInit();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            IList<EyouSoft.Model.ComStructure.MComCarType> list = new EyouSoft.BLL.ComStructure.BComCarType().GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                repList.DataSource = list;
                repList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        /// <summary>
        /// 删除车型
        /// </summary>
        private void DelCarType()
        {
            string cartypeids = Utils.GetQueryStringValue("id");
            int i = new EyouSoft.BLL.ComStructure.BComCarType().DelComCarType(cartypeids);
            if (i == 1)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            }
            else if (i == 2)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0", "该车型已被计划使用！"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0", "删除失败！"));
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
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_车型管理栏目))
            {
                this.caozuo.Visible = false;
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_车型管理栏目, false);
                return;
            }
            else
            {
                this.caozuo.Visible = true;
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
