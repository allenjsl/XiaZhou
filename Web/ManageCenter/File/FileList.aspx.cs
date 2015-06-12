using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;

namespace Web.ManageCenter.File
{
    /// <summary>
    /// 行政中心-文件管理-列表
    /// </summary>
    /// 创建人：方琪
    /// 创建时间：2012-3-13
    public partial class FileList : BackPage
    {
        #region 分页变量
        /// <summary>
        /// 页面数据总记录数
        /// </summary>
        private int PageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int PageIndex = 1;
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        private int RecordCount = 0;
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            if (doType != string.Empty && doType != null)
            {
                Ajax(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();

                PageInit();
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void PageInit()
        {
            string fontSize = Utils.GetQueryStringValue("fontSize");
            string company = Utils.GetQueryStringValue("fbdw");
            string title = Utils.GetQueryStringValue("txttitle");
            EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
            IList<EyouSoft.Model.GovStructure.MGovDocuments> DocList = BLL.GetGovDocumentsList(this.SiteUserInfo.CompanyId, fontSize, company, title, PageSize, PageIndex, ref RecordCount);
            if (DocList != null && DocList.Count > 0)
            {
                this.rptFileList.DataSource = DocList;
                this.rptFileList.DataBind();
                if (RecordCount <= PageSize)
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
                this.rptFileList.Controls.Add(new Label() { Text = "<tr><td colspan='12' align='center'>对不起，暂无数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }

        /// <summary>
        /// ajax操作
        /// </summary>
        /// <param name="doType"></param>
        protected void Ajax(string doType)
        {
            if (doType == "delete")
            {
                string id = Utils.GetQueryStringValue("id");
                bool result = DeleteData(id);
                string msg = result ? "删除成功！" : "删除失败！";
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        protected bool DeleteData(string id)
        {
            bool b = false;
            if (id != null && id != string.Empty)
            {
                string[] ids = id.Split(',');
                EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
                b = BLL.DeleteGovDocuments(ids);
            }
            return b;
        }

        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        protected string GetSells(object obj)
        {
            string str = string.Empty;
            if (obj != null)
            {
                List<MGovDocumentsApprove> list = (List<MGovDocumentsApprove>)obj;
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.ApproveState == ApprovalStatus.待审核)
                        {
                            str += string.Format("<a class='approve' data-id='{0}'  style='color:Red;' href='javascript:void(0)'>{1}</a>、", item.DocumentsId, item.ApproveName);
                        }
                        else
                        {
                            str += string.Format("<a class='approve' data-id='{0}'  href='javascript:void(0)'>{1}</a>、", item.DocumentsId, item.ApproveName);
                        }
                    }
                    str = str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
                }
            }
            return str;
        }


        protected string GetStatus(object FileType, object ApproveState)
        {
            string str = string.Empty;
            EyouSoft.Model.EnumType.GovStructure.FileType filetype = (EyouSoft.Model.EnumType.GovStructure.FileType)FileType;
            EyouSoft.Model.EnumType.GovStructure.ApprovalStatus approvestate = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)ApproveState;
            if (filetype == EyouSoft.Model.EnumType.GovStructure.FileType.传阅)
            {
                if (approvestate != EyouSoft.Model.EnumType.GovStructure.ApprovalStatus.审核通过)
                {
                    str = "待传阅";
                }
                else
                {
                    str = "已传阅";
                }
            }
            else
            {
                if (approvestate != EyouSoft.Model.EnumType.GovStructure.ApprovalStatus.审核通过)
                {
                    str = "待审批";
                }
                else
                {
                    str = "已审批";
                }
            }
            return str;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目, false);
            }
            else
            {
                this.ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_新增);
                this.ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_修改);
                this.ph_Del.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_删除);
            }
        }
        #endregion
    }
}
