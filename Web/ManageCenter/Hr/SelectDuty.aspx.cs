using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.ManageCenter.Hr
{
    /// <summary>
    /// 行政中心-人事档案-职务选择
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-21
    public partial class SelectDuty : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            int i = 0;
            EyouSoft.BLL.GovStructure.BPosition BLL = new EyouSoft.BLL.GovStructure.BPosition();
            IList<EyouSoft.Model.GovStructure.MGovPosition> lst = BLL.GetGovPositionList(this.SiteUserInfo.CompanyId, 1, 1, ref i);
            if (i > 0)
            {
                lst = BLL.GetGovPositionList(this.SiteUserInfo.CompanyId, i, 0, ref i);
            }
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<li style='text-align:center;'>对不起，没有相关数据！</li>" });
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion
    }
}