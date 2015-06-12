using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace Web.ManageCenter.File
{
    /// <summary>
    /// 行政中心-文件管理-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-05
    public partial class FileAdd : BackPage
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
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 用户控件初始化
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.Model.GovStructure.MGovDocuments model = new EyouSoft.Model.GovStructure.MGovDocuments();
                EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
                model = BLL.GetGovFilePersonnelModel(id);
                if (model != null)
                {
                    this.hidKeyId.Value = model.DocumentsId;
                    this.txtfileSize.Text = model.FontSize;
                    this.txtcompany.Text = model.Company;
                    this.txttitle.Text = model.Title;
                    //审批或传阅
                    this.shenpi.Checked = model.FileType == FileType.审批;
                    this.chuanyue.Checked = model.FileType == FileType.传阅;
                    //经办人
                    this.HrSelect1.HrSelectID = model.AttnId;
                    this.HrSelect1.HrSelectName = model.AttnName;
                    //审批人
                    this.SellsSelect1.SellsID = GetSells(model.GovDocumentsApproveList, 1);
                    this.SellsSelect1.SellsName = GetSells(model.GovDocumentsApproveList, 2);
                    StringBuilder strFile = new StringBuilder();
                    IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = model.ComAttachList;
                    if (null != lstFile && lstFile.Count > 0)
                    {
                        for (int i = 0; i < lstFile.Count; i++)
                        {
                            strFile.AppendFormat("<span  class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", lstFile[i].FilePath, lstFile[i].Name);

                        }
                    }
                    this.lbFiles.Text = strFile.ToString();//附件
                    if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != model.OperatorID)
                    {
                        this.ph_Save.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        protected string GetSells(IList<MGovDocumentsApprove> list, int i)
        {
            string str = string.Empty;
            if (list != null && list.Count > 0)
            {
                if (i == 1)
                {
                    foreach (var item in list)
                    {
                        str += item.ApproveID + ",";
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        str += item.ApproveName + ",";
                    }
                }
                str = str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
            }
            return str;
        }

        /// <summary>
        /// 获取审批人列表
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        protected IList<MGovDocumentsApprove> GetList(string ids, string names)
        {
            if (!string.IsNullOrEmpty(ids) && !string.IsNullOrEmpty(names))
            {
                IList<MGovDocumentsApprove> list = new List<MGovDocumentsApprove>();
                string[] id = ids.Split(',');
                string[] name = names.Split(',');
                for (int i = 0; i < id.Length; i++)
                {
                    MGovDocumentsApprove model = new MGovDocumentsApprove();
                    model.ApproveID = id[i];
                    model.ApproveName = name[i];
                    model.ApproveState = ApprovalStatus.待审核;
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 页面保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string documentid = Utils.GetFormValue(hidKeyId.UniqueID);
            string fontsize = Utils.GetFormValue(txtfileSize.UniqueID);
            string company = Utils.GetFormValue(txtcompany.UniqueID);
            string title = Utils.GetFormValue(txttitle.UniqueID);
            string attnid = Utils.GetFormValue(HrSelect1.HrSelectIDClient);
            string attnname = Utils.GetFormValue(HrSelect1.HrSelectNameClient);
            string approveid = Utils.GetFormValue(SellsSelect1.SellsIDClient);
            string approvename = Utils.GetFormValue(SellsSelect1.SellsNameClient);
            string isPass = Utils.GetFormValue("isPass");
            #endregion
            #region 表单验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(fontsize))
            {
                msg += "-请输入文件字号！";
            }
            if (string.IsNullOrEmpty(company))
            {
                msg += "-请输入文件发布单位！";
            }
            if (string.IsNullOrEmpty(title))
            {
                msg += "-请输入文件标题！";
            }
            if (string.IsNullOrEmpty(approveid) || string.IsNullOrEmpty(approvename))
            {
                msg += "-请选择审批或者传阅人！";
            }
            if (string.IsNullOrEmpty(attnid) || string.IsNullOrEmpty(attnname))
            {
                msg += "-请选择经办人！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            #endregion
            #region 实体赋值
            EyouSoft.Model.GovStructure.MGovDocuments model = new EyouSoft.Model.GovStructure.MGovDocuments();
            model.AttnId = attnid;
            model.AttnName = attnname;
            model.Company = company;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.DocumentsId = documentid;
            model.FileType = isPass == "shenpi" ? FileType.审批 : FileType.传阅;
            model.FontSize = fontsize;
            model.GovDocumentsApproveList = GetList(approveid, approvename);
            model.IssueTime = DateTime.Now;
            model.OperatorID = this.SiteUserInfo.UserId;
            model.Title = title;
            model.ComAttachList = NewGetAttach();
            #endregion
            #region 保存提交
            EyouSoft.BLL.GovStructure.BDocuments BLL = new EyouSoft.BLL.GovStructure.BDocuments();
            if (doType == "add")
            {
                result = BLL.AddGovDocuments(model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            else
            {
                result = BLL.UpdateGovDocuments(model, AttachItemType.文件管理);
                msg = result ? "修改成功！" : "修改失败！";
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }

        private IList<MComAttach> NewGetAttach()
        {
            //之前上传的附件
            string stroldupload = Utils.GetFormValue("hideFileInfo");
            IList<MComAttach> lst = new List<MComAttach>();
            if (!string.IsNullOrEmpty(stroldupload))
            {
                string[] oldupload = stroldupload.Split(',');
                if (oldupload != null && oldupload.Length > 0)
                {
                    for (int i = 0; i < oldupload.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(oldupload[i]))
                        {
                            string[] uploaditem = oldupload[i].Split('|');
                            MComAttach attModel = new MComAttach();
                            attModel.Name = uploaditem[0];
                            attModel.FilePath = uploaditem[1];
                            lst.Add(attModel);
                        }
                    }
                }
            }
            //新上传附件
            string[] upload = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
            for (int i = 0; i < upload.Length; i++)
            {
                string[] newupload = upload[i].Split('|');
                if (newupload != null && newupload.Length > 1)
                {
                    MComAttach attModel = new MComAttach();
                    attModel.FilePath = newupload[1];
                    attModel.Name = newupload[0];
                    lst.Add(attModel);
                }
            }
            return lst;
        }

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_栏目, false);
            }
            string doType = Utils.GetQueryStringValue("doType");
            if (doType == "add")
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_新增))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_新增, false);
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_修改))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_文件管理_修改, false);
                }
            }
        }
        #endregion

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
