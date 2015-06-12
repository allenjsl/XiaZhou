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
using EyouSoft.BLL.ComStructure;
namespace Web.ManageCenter.Rule
{
    /// <summary>
    /// 行政中心-规章制度-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class RuleAdd : BackPage
    {
        #region 页面加载
        /// <summary>
        /// 页面加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            if (doType == "checkDutyName")
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

        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 初始化用户控件
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion
            BRegulation BLL = new BRegulation();
            MGovRegulation Model = BLL.GetGovRegulationModel(id);
            if (null != Model)
            {
                //主键
                this.hidRuleId.Value = Model.RegId;
                //规章制度编号
                this.txtRuleId.Text = Model.Code;
                //制度标题
                this.txtRuleTitle.Text = Model.Title;
                //制度内容
                this.txtRuleContent.Text = Utils.InputText(Model.Content);
                //适用部门
                this.SelectSection1.SectionID = GetSectionID(Model.ApplyDeptList, 1);
                this.SelectSection1.SectionName = GetSectionID(Model.ApplyDeptList, 2);
                //发布部门
                this.SelectSection2.SectionID = Model.IssuedDeptId.ToString();
                this.SelectSection2.SectionName = Model.IssuedDepartName;
                //发布人
                this.HrSelect1.HrSelectID = Model.IssuedId.ToString();
                this.HrSelect1.HrSelectName = Model.IssuedName;
                // 发布时间
                this.IssueTime.Text = Model.IssueTime.ToString(ProviderToDate);
                #region 注释代码
                //附件
                StringBuilder strFile = new StringBuilder();
                IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = Model.ComAttachList;
                if (null != lstFile && lstFile.Count > 0)
                {
                    for (int i = 0; i < lstFile.Count; i++)
                    {
                        strFile.AppendFormat("<span  class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", lstFile[i].FilePath, lstFile[i].Name);
                    }
                }
                this.lbFiles.Text = strFile.ToString();//附件
                #endregion

                #region 权限判断
                if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                {
                    this.ph_save.Visible = false;
                }
                #endregion
            }

        }
        #endregion

        #region 获取适用部门
        /// <summary>
        /// 获取适用部门
        /// </summary>
        /// <param name="ApplyDeptList">适用部门集合</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected string GetSectionID(IList<MGovRegApplyDept> ApplyDeptList, int flag)
        {
            string str = string.Empty;
            if (ApplyDeptList != null && ApplyDeptList.Count > 0)
            {
                switch (flag)
                {
                    case 1:
                        foreach (var item in ApplyDeptList)
                        {
                            str += item.DepartId.ToString() + ",";
                        }
                        break;
                    case 2:
                        foreach (var item in ApplyDeptList)
                        {
                            str += item.DepartName + ",";
                        }
                        break;
                    default:
                        break;
                }
                return str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
            }
            return string.Empty;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            string code = Utils.GetFormValue(this.txtRuleId.UniqueID);
            string title = Utils.GetFormValue(this.txtRuleTitle.UniqueID);
            string content = Utils.GetFormValue(this.txtRuleContent.UniqueID);
            string issueddepartname = Utils.GetFormValue(this.SelectSection2.SelectNameClient);
            string issueddeptid = Utils.GetFormValue(this.SelectSection2.SelectIDClient);
            string issuedid = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient);
            string issuedname = Utils.GetFormValue(this.HrSelect1.HrSelectNameClient);
            string issuetime = Utils.GetFormValue(this.IssueTime.UniqueID);
            string applydeptid = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            string applydeptname = Utils.GetFormValue(this.SelectSection1.SelectNameClient);
            string ruleid = Utils.GetFormValue(this.hidRuleId.UniqueID);
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(code))
            {
                msg += "-请输入制度编号！<br/>";
            }
            if (string.IsNullOrEmpty(title))
            {
                msg += "-请输入制度标题！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                result = false;
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
                return;
            }
            #endregion

            #region 实体赋值
            BRegulation BLL = new BRegulation();
            MGovRegulation Model = new MGovRegulation();
            Model.RegId = ruleid;
            //制度编号
            Model.Code = code;
            //制度标题
            Model.Title = title;
            //制度内容
            Model.Content = content;
            //发布部门名称
            Model.IssuedDepartName = issueddepartname;
            //发布部门编号
            Model.IssuedDeptId = Utils.GetInt(issueddeptid);
            //发布人编号
            Model.IssuedId = issuedid;
            //发布人名称
            Model.IssuedName = issuedname;
            //适用部门
            Model.ApplyDeptList = GetApplyDeptList(applydeptname, applydeptid, ruleid);
            Model.IssueTime = Utils.GetDateTime(issuetime, DateTime.Now);
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.ComAttachList = NewGetAttach();
            #endregion

            #region 提交回应
            if (doType == "update")
            {
                result = BLL.UpdateRegulation(Model);
                msg = result ? "修改成功！" : "修改失败！";
            }
            if (doType == "add")
            {
                result = BLL.AddGovRegulation(Model);
                msg = result ? "添加成功！" : "添加失败";
                //新增
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }
        #endregion

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
        #region 获取实体的适用部门列表
        /// <summary>
        /// 获取实体的适用部门列表
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <param name="name">部门名称</param>
        /// <param name="regId">制度编号</param>
        /// <returns></returns>
        protected IList<MGovRegApplyDept> GetApplyDeptList(string name, string id, string regId)
        {
            IList<MGovRegApplyDept> deptList = new List<MGovRegApplyDept>();

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
            {
                string[] ids = id.Split(',');
                string[] names = name.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    MGovRegApplyDept deptModel = new MGovRegApplyDept();
                    deptModel.DepartId = Utils.GetInt(ids[i]);
                    deptModel.DepartName = names[i];
                    deptModel.RegId = regId;
                    deptList.Add(deptModel);
                }
            }
            return deptList;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "update")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_修改, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_新增, false);
                    }
                }
            }



        }
        #endregion

        #region 检查制度编号是否重复
        /// <summary>
        /// 检查制度编号是否重复 
        /// </summary>
        protected void CheckNum()
        {
            String str = String.Empty;
            String id = Request.QueryString["id"].Trim();
            String num = Request.QueryString["num"].Trim();
            if (!String.IsNullOrEmpty(num))
            {
                BRegulation BLL = new BRegulation();
                if (String.IsNullOrEmpty(id))
                {//新增
                    if (BLL.ExistsCode(num, "", this.SiteUserInfo.CompanyId))
                    {
                        str = "1";
                    }
                }
                else
                { //编辑
                    MGovRegulation Model = BLL.GetGovRegulationModel(id);
                    if (null != Model && !String.Equals(num, Model.Code))
                    {
                        if (BLL.ExistsCode(num, "", this.SiteUserInfo.CompanyId))
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