using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using System.Text;
namespace Web.ManageCenter.Advice
{
    /// <summary>
    /// 行政中心-意见建议箱-添加
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-21
    public partial class AdviceAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            string doType = Request.QueryString["doType"];
            PowerControl(doType);
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("id");
                PageInit(id,doType);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id,string dotype)
        {
            this.SellsSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            //编辑初始化
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                BOpinion BLL = new BOpinion();
                MGovOpinion Model = BLL.GetGovOpinionModel(id);
                if (null != Model)
                {
                    this.txtTitle.Value = Model.Title;//标题
                    this.txtContent.Value = Model.Content;//内容
                    this.txtHandleContent.Value = Model.ProcessOpinion;
                    this.txtTime.Value =String.Format("{0:yyyy-MM-dd HH:mm}",Model.ProcessTime);
                    //this.rdIsOpen.SelectedIndex = this.rdIsOpen.Items.IndexOf(this.rdIsOpen.Items.FindByValue(Model.IsOpen));//公开与否
                    this.ckHide.Checked = String.IsNullOrEmpty(Model.Submit.Trim()) ? true : false;
                    this.submitName.Text = Model.Submit;
                    this.txtSubmitTime.Value = String.Format("{0:yyyy-MM-dd HH:mm}", Model.SubmitTime);
                    //附件
                    StringBuilder strFile = new StringBuilder();
                    IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = Model.ComAttachList;
                    if (null != lstFile && lstFile.Count > 0)
                    {
                        for (int i = 0; i < lstFile.Count; i++)
                        {
                            strFile.AppendFormat("<span class='divFile_{5}'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&itemID={0}&type={1}' target='_blank' title='{2}({3}KB，已被下载{4}次)'>{2}</a><a href=\"javascript:delFile('{5}')\" style='color:#f00;font-weight:bolder;' title='删除附件'>×</a></span><input type=\"hidden\" id=\"hideFileInfo_{5}\" name=\"hideFileInfo\" value='{5}'/>", lstFile[i].ItemId, (int)lstFile[i].ItemType, lstFile[i].Name, lstFile[i].Size, lstFile[i].Downloads, i);
                        }
                        this.lbFiles.Text = strFile.ToString();
                    }
                    //接收人
                    IList<EyouSoft.Model.GovStructure.MGovOpinionUser> lst = Model.MGovOpinionUserList;
                    if (null != lst && lst.Count > 0)
                    {
                        List<String> names = new List<String>();
                        List<String> ids = new List<String>();
                        foreach (MGovOpinionUser m in lst)
                        {
                            names.Add(m.User);
                            ids.Add(m.UserId);
                        }
                        this.SellsSelect1.SellsName = names.Count == 0 ? "" : String.Join(",", names.ToArray());
                        this.SellsSelect1.SellsID = ids.Count == 0 ? "" : String.Join(",", ids.ToArray());
                    }
                }
            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType,string id)
        {
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            //数据验证开始
            StringBuilder strMsg = new StringBuilder(); 
            if (String.IsNullOrEmpty(this.txtTitle.Value.Trim()))
            {
                strMsg.Append("标题不能为空！\\n");
            }
            if (String.IsNullOrEmpty(this.txtSubmitTime.Value))
            {
                strMsg.Append("提交时间不能为空！\\n");
            }
            if (strMsg.Length > 0)
            {
                MessageBox.ResponseScript(this, String.Format("alert('{0}');", strMsg));
                return;
            }
            //数据验证结束
            //附件
            string newFilePath = String.Empty;//文件上传后的路径, 如："/UploadFiles/xxx/201010/guid.doc" 
            string newFileName = String.Empty;//文件上传后的文件名， 如："guid.doc"
            string oldFileName = string.Empty;//上传前的文件名,如："我的文档.doc"
            int fileSize = 0;//文件大小(kb)
            if (this.txtFile.HasFile)
            {
                string[] str = { ".txt", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".bmp", ".gif", ".jpeg", ".rar", ".zip" };
                string msg;
                if (UploadFile.CheckFileType(Request.Files, "txtFile", str, 4, out msg))
                {
                    bool flag = UploadFile.FileUpLoad(this.txtFile.PostedFile, "ManageCenter/Advice", out newFilePath, out newFileName);
                    if (flag)
                    {
                        oldFileName = this.txtFile.FileName;
                        fileSize = this.txtFile.PostedFile.ContentLength / 1024;
                    }
                    else
                    {
                        //文件上传失败
                        MessageBox.ResponseScript(this, "alert('文件上传失败!');");
                        return;
                    }
                }
                else
                {
                    //文件上传失败
                    MessageBox.ResponseScript(this, string.Format("alert('{0}');", msg));
                    return;
                }
            }
            BOpinion BLL = new BOpinion();
            MGovOpinion Model;
            bool result = false;
            if (t)
            {//-----新增-----
                Model = new MGovOpinion();
                this.commonModel(Model,newFilePath,newFileName,oldFileName,fileSize);
                result = BLL.AddGovOpinion(Model);
            }
            else
            {//-----编辑-----
                Model = BLL.GetGovOpinionModel(id);
                this.commonModel(Model, newFilePath, newFileName, oldFileName, fileSize);
                result = BLL.UpdateGovOpinion(Model);
            }
            string m = t ? "新增" : "修改";
            if (result)
            {
                Utils.ShowMsgAndCloseBoxy(m + "成功！", Utils.GetQueryStringValue("IframeId"), true);
            }
            else
            {
                MessageBox.ResponseScript(this, string.Format("alert('{0}失败!');", m));
            }
        }
        /// <summary>
        /// 新增与编辑的公用model部分
        /// </summary>
        protected void commonModel(MGovOpinion Model, String newFilePath, String newFileName, String oldFileName, int fileSize)
        {
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.Content = this.txtContent.Value;
            //Model.IsOpen = this.rdIsOpen.SelectedValue;
            Model.IssueTime = DateTime.Now;
            Model.Operator = this.SiteUserInfo.Name;
            Model.OperatorId = this.SiteUserInfo.UserId;
            //if (this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_处理))
            {
                Model.ProcessOpinion = this.txtHandleContent.Value;
                Model.ProcessTime =Utils.GetDateTimeNullable(this.txtTime.Value);
            }
            Model.Status = String.IsNullOrEmpty(this.txtTime.Value) ? "1" : "2";
            Model.Submit = this.ckHide.Checked ? "" : this.SiteUserInfo.Name;
            Model.SubmitTime = Utils.GetDateTimeNullable(this.txtSubmitTime.Value);
            Model.Title = this.txtTitle.Value;
            /*接收人员开始*/
            string[] strNames = this.SellsSelect1.SellsName.Split(',');
            string[] strIDs = this.SellsSelect1.SellsID.Split(',');
            if (strNames.Length > 0 && strIDs.Length > 0 && strIDs.Length == strNames.Length)
            {
                List<MGovOpinionUser> lst = new List<MGovOpinionUser>();
                MGovOpinionUser userModel;
                for (int i = 0; i < strNames.Length; i++)
                {
                    userModel = new MGovOpinionUser();
                    userModel.User = strNames[i];
                    userModel.UserId = strIDs[i];
                    lst.Add(userModel);
                }
                if (null != lst && lst.Count > 0)
                {
                    Model.MGovOpinionUserList = lst;   
                }
            }
            /*接收人员结束*/
            /*附件开始*/
            List<EyouSoft.Model.ComStructure.MComAttach> lstFile = new List<EyouSoft.Model.ComStructure.MComAttach>();
            if (!string.IsNullOrEmpty(newFilePath) && !string.IsNullOrEmpty(newFileName))
            {
                EyouSoft.Model.ComStructure.MComAttach filesModel = new EyouSoft.Model.ComStructure.MComAttach();
                filesModel.Downloads = 0;
                filesModel.FilePath = newFilePath;
                filesModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.意见建议;
                filesModel.Name = oldFileName;
                filesModel.Size = fileSize;
                lstFile.Add(filesModel);
                Model.ComAttachList = lstFile;
            }
            else
            {
                if (String.IsNullOrEmpty(Utils.GetFormValue("hideFileInfo").Trim()))
                {
                    Model.ComAttachList = lstFile;
                }
            }
            /*附件结束*/
        }
        protected void btnClick(object sender, EventArgs e)
        {
            this.PageSave(Utils.GetQueryStringValue("doType"), Utils.GetQueryStringValue("id"));
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(string dotype)
        {
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase))
            {
                //if (!this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_处理))
                {
                    //Utils.ResponseNoPermit(Common.Enum.TravelPermission.行政中心_意见建议箱_处理, false);
                    return;
                }
            }
            else
            {
                //if (!this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_提交))
                {
                    //Utils.ResponseNoPermit(Common.Enum.TravelPermission.行政中心_意见建议箱_提交, false);
                    return;
                }
            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}