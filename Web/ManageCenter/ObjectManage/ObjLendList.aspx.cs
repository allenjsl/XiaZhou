using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.ObjectManage
{
    /// <summary>
    /// 行政中心-物品管理-物品借阅列表
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-20
    public partial class ObjLendList : BackPage
    {
        private int pageSize = 10;
        private int pageIndex = 0;
        private int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            if (string.Equals(doType, "lendback"))
            {
                LendBack();
            }
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
            string goodId = Utils.GetQueryStringValue("goodId");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
            IList<MGovGoodUseList> lst = BLL.GetGovUseGoodList(this.SiteUserInfo.CompanyId, goodId, 2, pageSize, pageIndex, ref recordCount);
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
                this.rpt_List.Controls.Add(new Label() { Text = "<tr><td colspan='8' height='28' align='center'>对不起，没有相关数据！</td></tr>" });
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
        /// 物品借阅归还
        /// </summary>
        private void LendBack()
        {
            string ids = Utils.GetQueryStringValue("ids");
            EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
            int flag = BLL.ReturnGovGoodBorrow(ids.Split(','));
            string msg = flag > 0 ? "借阅归还成功！" : "借阅归还失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(flag.ToString(), msg));
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目, false);
            }
            else
            {
                this.ph_back.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_借阅管理);
            }

        }
        #endregion

    }
}