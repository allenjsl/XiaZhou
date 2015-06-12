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
    /// 上车地点管理
    /// 创建时间：2012-08-13
    /// 创建人：赵晓慧
    /// </summary>
    public partial class CarPlaceList : BackPage 
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
            if (!IsPostBack)
            {
                if (Utils.GetQueryStringValue("state") == "del")
                {
                    DelCarPlace();
                }
            }
            PageInit();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageInit()
        {
            //获取查询条件
            string txtsearch = Utils.GetFormValue("txtSearch");
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"),1);
            IList<EyouSoft.Model.ComStructure.MComCarLocation> list = new EyouSoft.BLL.ComStructure.BComCarLocation().GetList(pageIndex, pageSize,ref recordCount, SiteUserInfo.CompanyId, null,txtsearch);
            if (list != null && list.Count > 0)
            {
                repList.DataSource = list;
                repList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                repList.DataSource = null;
                repList.DataBind();
                this.ExporPageInfoSelect1.Visible = false;
            }
            //释放
            list = null;
        }

        /// <summary>
        /// 删除上车地点
        /// </summary>
        private void DelCarPlace()
        {
            string CarPlaceId = Utils.GetQueryStringValue("id");
            int i = new EyouSoft.BLL.ComStructure.BComCarLocation().DelCarLocation(CarPlaceId);
            if (i==1)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            }
            else if (i==2)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0","该上车地点已被计划使用！"));
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
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_上车地点管理栏目))
            {
                this.caozuo.Visible = false;
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_上车地点管理栏目, false);
                return;
            }
            else
            {
                this.caozuo.Visible = true;
            }
        }

        /// <summary>
        /// 获取上车地点状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetCarPlaceStatus(string status)
        {
            string sss = "";
            if (status == "True")
            {
                sss = "启用";
            }
            else
            {
                sss = "<font color='gray'>禁用</font>";
            }
            return sss;
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
