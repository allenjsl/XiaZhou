using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using System.Text;
namespace Web.ManageCenter.Organize
{
    /// <summary>
    /// 行政中心-组织机构-添加下级
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-20
    public partial class OrgAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            if (save == "save")
            {
                PageSave(doType);
            }
            PowerControl();
            string id = Utils.GetQueryStringValue("id");
            PageInit(id, doType);

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string doType)
        {
            #region 用户控件初始化
            this.HrSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.SetTitle = "部门主管";
            this.uc_Head.CompanyID = this.SiteUserInfo.CompanyId;
            this.uc_Foot.CompanyID = this.SiteUserInfo.CompanyId;
            this.uc_Temp.CompanyID = this.SiteUserInfo.CompanyId;
            this.uc_Seal.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion
            BComDepartment BLL = new BComDepartment();
            //根据编辑传过来的编号获取部门信息实体
            MComDepartment Model = BLL.GetModel(Utils.GetInt(id), this.SiteUserInfo.CompanyId);
            if (doType == "update")
            {
                if (null != Model)
                {
                    //部门名称
                    this.txtDepartName.Text = Model.DepartName;
                    //部门编号
                    this.hidDepartId.Value = Model.DepartId.ToString();
                    //联系电话
                    this.txtContact.Text = Model.Contact;
                    //传真
                    this.txtFaxa.Text = Model.Fax;
                    //备注
                    this.txtRemark.Text = Model.Remarks;
                    string strDel = "<span  class='upload_filename'><a href='{0}' target='_blank'>查看</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\"  title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hide_{1}\" value=\"{0}\"/></span>";
                    if (!string.IsNullOrEmpty(Model.PrintHeader))
                    {
                        //页眉
                        this.lbTxtHead.Text = string.Format(strDel, Model.PrintHeader, "head");
                    }
                    if (!string.IsNullOrEmpty(Model.PrintFooter))
                    {
                        //页脚
                        this.lbTxtFoot.Text = string.Format(strDel, Model.PrintFooter, "foot");
                    }
                    if (!string.IsNullOrEmpty(Model.PrintTemplates))
                    {
                        //模板
                        this.lbTxtTemp.Text = string.Format(strDel, Model.PrintTemplates, "temp");
                    }
                    if (!string.IsNullOrEmpty(Model.Seal))
                    {
                        //公章
                        this.lbTxtSeal.Text = string.Format(strDel, Model.Seal, "seal");
                    }
                    //部门主管编号
                    this.HrSelect1.HrSelectID = Model.DepartHead;

                    //this.HrSelect1.HrSelectName = Model.DepartHead;
                    MComDepartment ModelPart = BLL.GetModel(Model.PrevDepartId, Model.CompanyId);
                    if (ModelPart != null)
                    {
                        //上级部门名称和编号
                        this.txtUpSection.Text = ModelPart.DepartName;
                        this.hidupsectionId.Value = ModelPart.DepartId.ToString();
                    }
                    else
                    {
                        this.txtUpSection.Text = "股东会";
                    }
                    //通过部门主管编号获取人事档案信息实体
                    EyouSoft.BLL.GovStructure.BArchives BLLManager = new EyouSoft.BLL.GovStructure.BArchives();
                    EyouSoft.Model.GovStructure.MGovFile ModelManager;
                    ModelManager = BLLManager.GetArchivesModel(Model.DepartHead);
                    if (ModelManager != null)
                    {
                        this.HrSelect1.HrSelectName = ModelManager.Name;
                        this.HrSelect1.HrSelectID = ModelManager.ID;
                    }
                }
                else
                {
                    this.txtUpSection.Text = "股东会";
                }
            }
            else
            {
                if (Model != null)
                {
                    this.txtUpSection.Text = Model.DepartName;
                    this.hidupsectionId.Value = Model.DepartId.ToString();
                }
                else
                {
                    this.txtUpSection.Text = "股东会";
                    this.hidupsectionId.Value = "0";
                }
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //部门名称
            string deptname = Utils.GetFormValue(txtDepartName.UniqueID);
            //部门编号
            string deptid = Utils.GetFormValue(hidDepartId.UniqueID);
            //部门主管编号
            string depthead = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient);
            //上级部门编号
            string upsection = Utils.GetFormValue(this.hidupsectionId.UniqueID);
            //联系电话
            string contact = Utils.GetFormValue(txtContact.UniqueID);
            //传真
            string fax = Utils.GetFormValue(txtFaxa.UniqueID);
            string remark = Utils.GetFormValue(txtRemark.UniqueID);
            string head = Utils.GetFormValue(this.uc_Head.ClientHideID);
            string[] headpath = Utils.GetFormValue(this.uc_Head.ClientHideID).Split('|');
            string[] footpath = Utils.GetFormValue(this.uc_Foot.ClientHideID).Split('|');
            string[] temppath = Utils.GetFormValue(this.uc_Temp.ClientHideID).Split('|');
            string[] sealpath = Utils.GetFormValue(this.uc_Seal.ClientHideID).Split('|');
            string oldhead = Utils.GetFormValue("hide_head");
            string oldfoot = Utils.GetFormValue("hide_foot");
            string oldtemp = Utils.GetFormValue("hide_temp");
            string oldseal = Utils.GetFormValue("hide_seal");
            #endregion

            #region 数据验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(deptname))
            {
                msg += "-请输入部门名称！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
                Response.End();
            }
            #endregion

            #region 实体赋值
            MComDepartment model = new MComDepartment();
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.Contact = contact;
            model.DepartHead = depthead;
            //model.DepartId = 
            model.DepartName = deptname;
            model.DepartId = Utils.GetInt(deptid, 0);
            model.Fax = fax;
            model.IssueTime = DateTime.Now;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.PrevDepartId = Utils.GetInt(upsection);
            if (footpath != null && footpath.Length > 1)
            {
                model.PrintFooter = footpath[1];
            }
            else
            {
                model.PrintFooter = oldfoot;
            }
            if (headpath != null && headpath.Length > 1)
            {
                model.PrintHeader = headpath[1];
            }
            else
            {
                model.PrintHeader = oldhead;
            }
            if (temppath != null && temppath.Length > 1)
            {
                model.PrintTemplates = temppath[1];
            }
            else
            {
                model.PrintTemplates = oldtemp;
            }
            if (sealpath != null && sealpath.Length > 1)
            {
                model.Seal = sealpath[1];
            }
            else
            {
                model.Seal = oldseal;
            }
            model.Remarks = remark;
            #endregion

            #region 提交保存
            Response.Clear();
            BComDepartment BLL = new BComDepartment();
            if (doType == "add")
            {
                result = BLL.Add(model);
                msg = result ? "添加成功！" : "添加失败！";
                Response.Write("{\"result\":\"" + (result ? "1" : "0") + "\",\"msg\":\"" + msg + "\",\"id\":\"" + model.PrevDepartId + "\"}");
            }
            else
            {
                result = BLL.Update(model);
                msg = result ? "修改成功！" : "修改失败！";
                Response.Write("{\"result\":\"" + (result ? "1" : "0") + "\",\"msg\":\"" + msg + "\",\"id\":\"" + model.DepartId + "\",\"nowName\":\"" + model.DepartName + "\"}");
            }
            Response.End();
            #endregion

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_组织机构_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_组织机构_栏目, false);
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