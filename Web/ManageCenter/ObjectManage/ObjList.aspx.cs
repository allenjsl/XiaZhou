using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.ManageCenter.ObjectManage
{
    /// <summary>
    /// 行政中心-物品管理-物品管理列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-31
    public partial class ObjList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                DeleteData();
            }
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
            //获取查询参数
            string txtName = Utils.GetQueryStringValue("txtName");//物品名称;
            string txtInSTime = Utils.GetQueryStringValue("txtInSTime");//入库开始时间
            string txtInETime = Utils.GetQueryStringValue("txtInETime");//入库结束时间

            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
            IList<EyouSoft.Model.GovStructure.MGovGoodList> lst = BLL.GetGovGoodList(this.SiteUserInfo.CompanyId, txtName, Utils.GetDateTimeNullable(txtInSTime), Utils.GetDateTimeNullable(txtInETime), this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='9' align='center'>对不起，没有相关数据！</td></tr>" });
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
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private void DeleteData()
        {
            string id = Utils.GetQueryStringValue("id");
            string msg = String.Empty;
            int b = 0;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
                b = BLL.DeleteGovGood(id.Split(','));
            }
            switch (b)
            {
                case 1:
                    msg = "删除成功！";//成功
                    break;
                case 2:
                    msg = "删除失败，该物品已被使用！";
                    break;
                default:
                    msg = "删除失败！";
                    break;
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(b.ToString(), msg));
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
                this.ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_入库登记);
                this.ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_物品修改);
                this.ph_Delete.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_物品删除);
                this.ph_Get.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_领用登记);
                this.ph_Lend.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_借阅管理);
                this.ph_Send.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_发放登记);
            }
        }
        #endregion
    }
}