using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace Web.GroupEnd.Distribution
{
    /// <summary>
    /// 供应商平台-配置管理
    /// 创建时间：2011-10-13
    /// 创建者：王磊
    /// </summary>
    public partial class ConfigSettings : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                Response.Clear();
                Response.Write(Save());
                Response.End();
            }

            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                //上传控件
                this.UploadControl1.IsUploadSelf = true;
                this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl1.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";


                this.UploadControl2.IsUploadSelf = true;
                this.UploadControl2.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl2.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";

                this.UploadControl3.IsUploadSelf = true;
                this.UploadControl3.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl3.FileTypes = "*.dot";

                this.UploadControl4.IsUploadSelf = true;
                this.UploadControl4.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl4.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";

                PageInit();

            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void PageInit()
        {
            EyouSoft.BLL.CrmStructure.BCrm bCrm = new EyouSoft.BLL.CrmStructure.BCrm();
            EyouSoft.Model.CrmStructure.MCrm mCrm = bCrm.GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
            if (null != mCrm)
            {
                if (!string.IsNullOrEmpty(mCrm.PrintHeader))
                {
                    this.lblPrintHeader.Text = string.Format("<span class='upload_filename'><a href='{0}'>打印页眉</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"PrintHeader\" value='{0}'/></span>", mCrm.PrintHeader);
                }

                if (!string.IsNullOrEmpty(mCrm.PrintFooter))
                {
                    this.lblPrintFooter.Text = string.Format("<span class='upload_filename'><a href='{0}'>打印页脚</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"PrintFooter\" value='{0}'/></span>", mCrm.PrintFooter);
                }

                if (!string.IsNullOrEmpty(mCrm.PrintTemplates))
                {
                    this.lblPrintTemplates.Text = string.Format("<span class='upload_filename'><a href='{0}' target='_blank'>打印模板</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"PrintTemplates\" value='{0}'/></span>", mCrm.PrintTemplates);
                }

                if (!string.IsNullOrEmpty(mCrm.Seal))
                {
                    this.lblSeal.Text = string.Format("<span class='upload_filename'><a href='{0}'>公司公章</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"Seal\" value='{0}'/></span>", mCrm.Seal);
                }
            }
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        private string Save()
        {
            string printHead = Utils.GetFormValue(this.UploadControl1.ClientHideID);
            if (!string.IsNullOrEmpty(printHead))
            {
                printHead = printHead.Split('|')[1];
            }
            else
            {
                printHead = Utils.GetFormValue("PrintHeader");
            }


            string printFooter = Utils.GetFormValue(this.UploadControl2.ClientHideID);
            if (!string.IsNullOrEmpty(printFooter))
            {
                printFooter = printFooter.Split('|')[1];
            }
            else
            {
                printFooter = Utils.GetFormValue("PrintFooter");
            }

            string printTemplates = Utils.GetFormValue(this.UploadControl3.ClientHideID);
            if (!string.IsNullOrEmpty(printTemplates))
            {
                printTemplates = printTemplates.Split('|')[1];
            }
            else
            {

                printTemplates = Utils.GetFormValue("PrintTemplates");
            }

            string seal = Utils.GetFormValue(this.UploadControl4.ClientHideID);
            if (!string.IsNullOrEmpty(seal))
            {
                seal = seal.Split('|')[1];
            }
            else
            {
                seal = Utils.GetFormValue("Seal");
            }

            EyouSoft.BLL.CrmStructure.BCrm bCrm = new EyouSoft.BLL.CrmStructure.BCrm();

            if (bCrm.UpdatePrintSet(SiteUserInfo.TourCompanyInfo.CompanyId, printHead, printFooter, printTemplates, seal))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "打印设置成功！ 正在跳转...");
            }
            else
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "打印设置失败！");
            }


        }
    }
}
