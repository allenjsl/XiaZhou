using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.ObjectManage
{
    /// <summary>
    /// 行政中心-物品管理-物品发放列表
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-20
    public partial class ObjSendList : BackPage
    {
        private int pageSize = 10;
        private int pageIndex = 0;
        private int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "delete") Delete();
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string goodId = Utils.GetQueryStringValue("goodId");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
            IList<MGovGoodUseList> lst = BLL.GetGovUseGoodList(this.SiteUserInfo.CompanyId, goodId, 1, pageSize, pageIndex, ref recordCount);
            if (lst != null && lst.Count > 0)
            {
                this.rpt_List.DataSource = lst;
                this.rpt_List.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;
                }
                else
                {
                    BindPage();
                }
            }
            else
            {
                this.rpt_List.Controls.Add(new Label() { Text = "<tr><td colspan='20' height='28' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 删除领用信息
        /// </summary>
        void Delete()
        {
            string s = Utils.GetQueryStringValue("lingyongid");
            if (string.IsNullOrEmpty(s)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            var bllRetCode = new EyouSoft.BLL.GovStructure.BGood().DeleteLingYong(s, SiteUserInfo.CompanyId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }
        #endregion
    }
}