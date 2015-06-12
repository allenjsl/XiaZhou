using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SystemSet
{
    /// <summary>
    /// 用户编辑
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class UserEdit : BackPage
    {
        public string Pwd = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            else if (Utils.GetQueryStringValue("dotype") == "getGovInfo")
            {
                Response.Clear();
                Response.Write(GetGovFileInfo(Utils.GetQueryStringValue("id")));
                Response.End();
            }
            else
            {
                PageInit();
            }
        }

        protected void Save()
        {
            EyouSoft.Model.ComStructure.MComUser model = new EyouSoft.Model.ComStructure.MComUser();
            //内部用户为string.Empty,客户单位或供应商就是相就的公司编号
            model.TourCompanyId = string.Empty;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.UserName = txtUserName.Text;
            model.Password = Utils.GetFormValue("txtPwd");
            model.ContactName = Utils.GetFormValue(HrSelect1.HrSelectNameClient);
            model.GovFileId = Utils.GetFormValue(HrSelect1.HrSelectIDClient);
            model.ContactSex = Convert.ToChar(ddlSex.SelectedValue);
            model.DeptId = Utils.GetInt(Utils.GetFormValue(BelongDepart.SelectIDClient));
            model.DeptIdJG = Utils.GetInt(Utils.GetFormValue(ManageDepart.SelectIDClient));
            model.Arrears = Utils.GetDecimal(txtDebt.Value);
            model.ContactTel = txtPhone.Value;
            model.ContactFax = txtFax.Value;
            model.ContactMobile = txtMobile.Value;
            model.QQ = txtQQ.Value;
            model.MSN = txtMSN.Value;
            model.ContactEmail = txtEmail.Value;
            model.PeopProfile = txtIntroduction.Text;
            model.Remark = txtRemark.Text;
            model.RoleId = Utils.GetInt(Utils.GetFormValue(ddlRoleList.ClientID));
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.OperDeptId = SiteUserInfo.DeptId;
            //model.UserStatus = chkState.Checked;
            Response.Clear();
            if (string.IsNullOrEmpty(UserID))
            {
                model.UserType = EyouSoft.Model.EnumType.ComStructure.UserType.内部员工;
                if (!new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(model.UserName, SiteUserInfo.CompanyId, string.Empty))
                {
                    if (new EyouSoft.BLL.ComStructure.BComUser().Add(model))
                    {
                        Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功!"));
                    }
                    else
                    {
                        Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                    }
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "用户名已存在!"));
                }
            }
            else
            {
                model.UserId = UserID;
                if (new EyouSoft.BLL.ComStructure.BComUser().Update(model))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功!"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败!"));
                }
            }
            Response.End();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            this.HrSelect1.Isshow = false;
            string iframeId = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.SetTitle = "姓名";
            this.HrSelect1.SModel = "1";
            this.HrSelect1.ParentIframeID = iframeId;
            this.HrSelect1.CallBackFun = "UserEdit.BindGovFile";
            this.ddlSex.DataTextField = "Text";
            this.ddlSex.DataValueField = "Value";
            this.ddlSex.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
            this.ddlSex.DataBind();
            this.ddlSex.Items.Insert(0, new ListItem("请选择", ""));
            IList<EyouSoft.Model.ComStructure.MComRole> rolelist = new EyouSoft.BLL.ComStructure.BComRole().GetList(SiteUserInfo.CompanyId);
            this.ddlRoleList.DataTextField = "RoleName";
            this.ddlRoleList.DataValueField = "Id";
            this.ddlRoleList.DataSource = rolelist;
            this.ddlRoleList.DataBind();
            this.ddlRoleList.Items.Insert(0, new ListItem("请选择", ""));
            this.ManageDepart.IsNotValid = false;
            if (!string.IsNullOrEmpty(UserID))
            {
                EyouSoft.Model.ComStructure.MComUser model = new EyouSoft.BLL.ComStructure.BComUser().GetModel(UserID, SiteUserInfo.CompanyId);
                txtUserName.Text = model.UserName;
                txtUserName.ReadOnly = true;
                
                HrSelect1.HrSelectName = model.ContactName;
                HrSelect1.HrSelectID = model.GovFileId;
                HrSelect1.IsDisplay = true;
                if (this.ddlSex.Items.FindByValue(model.ContactSex.ToString()) != null)
                {
                    this.ddlSex.SelectedValue = model.ContactSex.ToString();
                }
                this.BelongDepart.SectionID = model.DeptId.ToString();
                this.BelongDepart.SectionName = model.DeptName;
                this.ManageDepart.SectionID = model.DeptIdJG.ToString();
                this.ManageDepart.SectionName = model.JGDeptName;
                if (this.ddlRoleList.Items.FindByValue(model.RoleId.ToString()) != null)
                {
                    this.ddlRoleList.SelectedValue = model.RoleId.ToString();
                }
                txtDebt.Value = Utils.FilterEndOfTheZeroString(model.Arrears.ToString());
                txtPhone.Value = model.ContactTel;
                txtFax.Value = model.ContactFax;
                txtMobile.Value = model.ContactMobile;
                txtQQ.Value = model.QQ;
                txtMSN.Value = model.MSN;
                txtEmail.Value = model.ContactEmail;
                txtIntroduction.Text = model.PeopProfile;
                txtRemark.Text = model.Remark;
                //chkState.Checked = model.IsEnable;
                txtUserName.Enabled = false;
                Pwd = model.Password;
            }
            else
            {
                ddlSex.Items[0].Selected = true;
                txtUserName.Enabled = true;
                //chkState.Checked = false;
            }
        }

        /// <summary>
        /// 获得人事档案信息
        /// </summary>
        /// <param name="GovId"></param>
        /// <returns></returns>
        public string GetGovFileInfo(string GovId)
        {
            if (!string.IsNullOrEmpty(GovId))
            {
                var model = new EyouSoft.BLL.GovStructure.BArchives().GetArchivesModel(GovId);
                return Newtonsoft.Json.JsonConvert.SerializeObject(model);
            }
            return "";
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目, false);
                return;
            }
            if (string.IsNullOrEmpty(UserID))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_新增))
                {
                    placeSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                {
                    placeSave.Visible = false;
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

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get { return Utils.GetQueryStringValue("id"); } }
    }
}
