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
using EyouSoft.Model.ComStructure;

namespace Web.ManageCenter.Pack
{
    /// <summary>
    /// 行政中心-公司合同-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-05
    public partial class PackAdd : BackPage
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
            if (doType == "checkNum")
            {
                CheckNum();
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
            #region 初始化用户控件
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion
            //编辑初始化
            if (!string.IsNullOrEmpty(id))
            {
                BContract BLL = new BContract();
                MGovContract Model = BLL.GetGovContractModel(id);
                if (null != Model)
                {
                    this.hidKeyId.Value = Model.ID;
                    this.txtNumber.Text = Model.Number;
                    this.txtType.Text = Model.Type;
                    this.txtCompany.Text = Model.Company;
                    this.txtStartTime.Text = UtilsCommons.GetDateString(Model.SignedTime, this.ProviderToDate);
                    this.txtEndTime.Text = UtilsCommons.GetDateString(Model.MaturityTime, this.ProviderToDate);
                    this.txtContent.Text = Model.Description;
                    this.HrSelect1.HrSelectID = Model.signierId;
                    this.HrSelect1.HrSelectName = Model.signier;
                    this.SelectSection1.SectionID = Model.SignedDepId.ToString();
                    this.SelectSection1.SectionName = Model.SignedDep;
                    if (Model.IsRemind)
                    {
                        warn.Checked = true;
                    }
                    else
                    {
                        nowarn.Checked = true;
                    }
                    StringBuilder strFile = new StringBuilder();
                    IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = Model.ComAttachList;
                    if (Model.ComAttachList != null && Model.ComAttachList.Count > 0)
                    {
                        for (int i = 0; i < lstFile.Count; i++)
                        {
                            strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" style='color:#f00;font-weight:bolder;' title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", lstFile[i].FilePath, lstFile[i].Name);
                        }
                    }
                    this.lbFiles.Text = strFile.ToString();
                    if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                    {
                        this.ph_Save.Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string keyId = Utils.GetFormValue(hidKeyId.UniqueID);
            string number = Utils.GetFormValue(txtNumber.UniqueID);
            string type = Utils.GetFormValue(txtType.UniqueID);
            string company = Utils.GetFormValue(txtCompany.UniqueID);
            string startTime = Utils.GetFormValue(txtStartTime.UniqueID);
            string endTime = Utils.GetFormValue(txtEndTime.UniqueID);
            string description = Utils.GetFormValue(txtContent.UniqueID);
            string signerId = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient);
            string signerName = Utils.GetFormValue(this.HrSelect1.HrSelectNameClient);
            string signDepId = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            string signDepName = Utils.GetFormValue(this.SelectSection1.SelectNameClient);
            string isRemind = Utils.GetFormValue("Iswarn");
            #endregion
            #region 表单验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(number))
            {
                msg += "-请输入合同编号！";
            }
            if (string.IsNullOrEmpty(type))
            {
                msg += "-请输入合同类型！";
            }
            if (string.IsNullOrEmpty(company))
            {
                msg += "-请输入合同单位！";
            }
            if (string.IsNullOrEmpty(startTime))
            {
                msg += "-请输入合同签订日期！";
            }
            if (string.IsNullOrEmpty(endTime))
            {
                msg += "-请输入合同到期时间！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            #endregion
            #region 实体赋值
            MGovContract Model = new MGovContract();
            Model.ID = keyId;
            Model.Company = company;
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.Description = description;
            Model.IsRemind = isRemind == "warn" ? true : false;
            Model.IssueTime = DateTime.Now;
            Model.MaturityTime = Utils.GetDateTimeNullable(endTime);
            Model.SignedTime = Utils.GetDateTimeNullable(startTime);
            Model.Number = number;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.SignedDepId = Utils.GetInt(signDepId);
            Model.SignedDep = signDepName;
            Model.signierId = signerId;
            Model.signier = signerName;
            Model.Type = type;
            Model.ComAttachList = NewGetAttach();
            #endregion
            #region 保存提交
            BContract BLL = new BContract();
            if (doType == "add")
            {

                result = BLL.AddGovContract(Model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            else
            {
                result = BLL.UpdateGovContract(Model);
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

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "update")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_修改, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_新增, false);
                    }
                }
            }

        }
        /// <summary>
        /// 检查会议编号是否重复 
        /// </summary>
        protected void CheckNum()
        {
            String str = String.Empty;
            String id = Request.QueryString["id"].Trim();
            String num = Request.QueryString["num"].Trim();
            if (!String.IsNullOrEmpty(num))
            {
                BContract BLL = new BContract();
                if (String.IsNullOrEmpty(id))
                {//新增
                    if (BLL.ExistsNumber(num, "", this.SiteUserInfo.CompanyId))
                    {
                        str = "1";
                    }
                }
                else
                { //编辑
                    MGovContract Model = BLL.GetGovContractModel(id);
                    if (null != Model && !String.Equals(num, Model.Number))
                    {
                        if (BLL.ExistsNumber(num, "", this.SiteUserInfo.CompanyId))
                        {
                            str = "1";
                        }
                    }
                }
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
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