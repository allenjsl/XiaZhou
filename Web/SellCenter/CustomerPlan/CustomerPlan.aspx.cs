using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.TourStructure;

namespace Web.SellCenter
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-7
    /// 说明：同业分销 中 收客计划 的线路区域列表
    public partial class CustomerPlan : BackPage
    {
        #region 获取页面参数
        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            #region 获取页面参数
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化 
                DataInit();
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            IList<MKeyTour> list =  new EyouSoft.BLL.TourStructure.BTour().GetKeyTour(SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.DataBind();
            }
            else
            {
                this.licMsg.Text = "";
            }
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目, true);
                return;
            }
        }
        #endregion
    }
} 
