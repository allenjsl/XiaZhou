using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.Leave
{
    /// <summary>
    /// 行政中心-员工离职-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-31
    public partial class LeaveList : BackPage
    {
        #region 分页变量
        /// <summary>
        /// 页面数据总记录数
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        private int recordCount = 0;
        #endregion
        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            string txtName = Utils.GetQueryStringValue("txtName");
            EyouSoft.BLL.GovStructure.BGovFilePersonnel BLL = new EyouSoft.BLL.GovStructure.BGovFilePersonnel();
            IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> lst = BLL.GetGovFilePersonnelList(this.SiteUserInfo.CompanyId, txtName, this.pageSize, this.pageIndex, ref this.recordCount);
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='5' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;

            }
        }
        #endregion

        #region 绑定分页
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
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private void DeleteData()
        {
            string id = Utils.GetQueryStringValue("id");
            bool result = false;
            string msg = "";
            if (!String.IsNullOrEmpty(id))
            {
                BGovFilePersonnel BLL = new BGovFilePersonnel();
                result = BLL.DeleteGovFilePersonnel(id.Split(','));
                msg = result ? "删除成功！" : "删除失败！";
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目, false);
            }
            else
            {
                ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_新增);
                ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_修改);
                ph_Del.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_删除);
            }
        }
        #endregion

        #region 前台调用方法
        /// <summary>
        /// 获取操作状态
        /// </summary>
        /// <param name="o">审核状态</param>
        /// <param name="obj">编号</param>
        /// <param name="staffStatus">员工状态</param>
        /// <returns></returns>
        protected string GetApproveState(object o, object obj, object staffStatus)
        {
            string str = string.Empty;
            if (null != o && obj != null && staffStatus != null)
            {
                //员工离职审批状态
                if ((StaffStatus)staffStatus != StaffStatus.离职)
                {
                    ApprovalStatus m = (ApprovalStatus)o;
                    switch (m)
                    {
                        case ApprovalStatus.待审核:
                            str = string.Format("<a class='approve' data-class='inapprove' data-id='{0}' href='javascript:void(0)'>审核中</a>", obj.ToString());
                            break;
                        case ApprovalStatus.审核通过:
                            str = string.Format("<a class='approve' data-class='approved' data-id='{0}' href='javascript:void(0)'>可办理离职</a>", obj.ToString());
                            break;
                        case ApprovalStatus.审核未通过:
                            str = string.Format("<a class='approve' data-class='inapprove' data-id='{0}' href='javascript:void(0)'>审核中</a>", obj.ToString());
                            break;
                    }
                }
                //员工已经离职
                else
                {
                    str = string.Format("<a class='approve' data-class='approved' data-id='{0}' href='javascript:void(0)'>已离职</a>", obj.ToString());
                }
            }
            return str;
        }
        /// <summary>
        /// 获取审核人
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetGovPersonnel(object o, object fileId)
        {
            string str = "";
            if (o != null)
            {
                IList<MGovPersonnelApprove> lst = (IList<MGovPersonnelApprove>)o;
                if (lst != null && lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        if (item.ApproveState == ApprovalStatus.待审核)
                        {
                            str += string.Format("<a class='approve' data-id='{0}'  style='color:Red;' href='javascript:void(0)'>{1}</a>、", fileId.ToString(), item.ApproveName);
                        }
                        else
                        {
                            str += string.Format("<a class='approve'  data-id='{0}' href='javascript:void(0)'>{1}</a>、", fileId.ToString(), item.ApproveName);
                        }
                    }
                    str = str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
                }
            }
            return str;
        }
        #endregion
    }
}