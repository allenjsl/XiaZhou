using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.GovStructure;
using System.Text;
namespace Web.ManageCenter.File
{
    /// <summary>
    /// 行政中心-文件管理-审批
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-06
    public partial class FileApprove : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "approve")
            {
                PageApprove();
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }

        protected void PageInit(string id)
        {
            EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
            EyouSoft.Model.GovStructure.MGovDocuments model = BLL.GetGovFilePersonnelModel(id);
            if (model != null)
            {
                this.lbTitle.Text = model.Title;
                this.lbCompany.Text = model.Company;
                this.lbFontSize.Text = model.FontSize;
                IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = model.ComAttachList;
                StringBuilder strFile = new StringBuilder();
                if (null != lstFile && lstFile.Count > 0)
                {
                    for (int i = 0; i < lstFile.Count; i++)
                    {
                        strFile.AppendFormat("<span  class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a></span>", lstFile[i].FilePath, lstFile[i].Name);

                    }
                }
                this.lbFiles.Text = strFile.ToString();
                if (model.GovDocumentsApproveList != null && model.GovDocumentsApproveList.Count > 0)
                {
                    this.rpt_approve.DataSource = model.GovDocumentsApproveList;
                    this.rpt_approve.DataBind();
                    if (model.GovDocumentsApproveList.Where(item => (item.ApproveID) == this.SiteUserInfo.UserId).ToList().Count > 0)
                    {
                        this.ph_Flies.Visible = true;
                    }
                    else
                    {
                        this.ph_Flies.Visible = false;
                    }
                }
            }
        }

        #region 审批
        /// <summary>
        /// 审批
        /// </summary>
        protected void PageApprove()
        {
            string msg = "";
            bool result = false;
            //表单取值
            string id = Utils.GetQueryStringValue("id");
            string appid = Utils.GetQueryStringValue("appid");
            string appName = Utils.GetQueryStringValue("appname");
            string appTime = Utils.GetQueryStringValue("apptime");
            string appView = Utils.GetQueryStringValue("appview");
            //验证
            if (string.IsNullOrEmpty(appTime))
            {
                msg += "-请输入审批时间！<br/>";
            }
            if (string.IsNullOrEmpty(appView))
            {
                msg += "-请输入审批意见！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            //实体赋值
            EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
            EyouSoft.Model.GovStructure.MGovDocumentsApprove model = new EyouSoft.Model.GovStructure.MGovDocumentsApprove();
            model.DocumentsId = id;
            model.ApproveID = appid;
            model.ApproveState = ApprovalStatus.审核通过;
            model.ApproveTime = Utils.GetDateTimeNullable(appTime);
            model.ApprovalViews = appView;
            model.ApproveName = appName;
            //提交保存
            result = BLL.UpdateGovDocumentsApprove(model);
            msg = result ? "审批通过!" : "审批失败!";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion


        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_修改))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_修改, false);
            }

        }

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion
    }
}
