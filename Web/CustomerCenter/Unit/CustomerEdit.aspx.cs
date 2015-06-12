using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL;
using EyouSoft.Model;

namespace Web.CustomerCenter.Unit
{
    /// <summary>
    /// 客户管理 单位直客 添加修改
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class CustomerEdit : EyouSoft.Common.Page.BackPage
    {
        #region 私有变量
        EyouSoft.BLL.ComStructure.BComCity cityBll = new EyouSoft.BLL.ComStructure.BComCity();
        EyouSoft.BLL.CrmStructure.BCrm crmBll = new EyouSoft.BLL.CrmStructure.BCrm();
        EyouSoft.BLL.ComStructure.BComUser userBll = new EyouSoft.BLL.ComStructure.BComUser();


        #endregion

        protected string crmId = string.Empty;
        protected string Province = "0";
        protected string City = "0";
        protected string Country = "0";
        protected string County = "0";
        protected string FilePath = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            string doType = Utils.GetQueryStringValue("dotype");
            this.UploadControl1.CompanyID = base.SiteUserInfo.CompanyId;
            if (doType != "")
            {
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                }
            }

            if (!IsPostBack)
            {
                this.Seller1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                this.Seller1.ReadOnly = true;
                this.Seller1.SMode = false;
                crmId = Utils.GetQueryStringValue("crmId");
                BindCrmLevId();
                //存在ajax请求
                
                if (crmId != string.Empty)
                {

                    BindListData(crmId);
                }
                else
                {

                    BindAddList();
                }
                
            }
        }

        /// <summary>
        /// 绑定客户等级
        /// </summary>
        /// <returns></returns>
        protected void BindCrmLevId()
        {
            EyouSoft.BLL.ComStructure.BComLev levBll = new EyouSoft.BLL.ComStructure.BComLev();
            IList<EyouSoft.Model.ComStructure.MComLev> list = levBll.GetList(base.SiteUserInfo.CompanyId);
            ddlLevId.DataSource = list;
            ddlLevId.DataValueField = "Id";
            ddlLevId.DataTextField = "Name";
            ddlLevId.DataBind();
            ddlLevId.Items.Insert(0, new ListItem("--未选择--", "0"));
            if (string.IsNullOrEmpty(Utils.GetQueryStringValue("crmId")))
            {
                ddlLevId.SelectedValue = "0";
            }
            
        }


        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave()
        {
            EyouSoft.Model.CrmStructure.MCrm crmModel = new EyouSoft.Model.CrmStructure.MCrm();
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("crmId")))
            {
                crmModel = crmBll.GetInfo(EyouSoft.Common.Utils.GetQueryStringValue("crmId"));
            }
            #region 客户主体信息
            //获取form参数
            int Country = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID), 0);//国家
            int Provice = Utils.GetInt(Utils.GetFormValue(ddlProvice.UniqueID), 0);//省份
            int City = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID), 0);//城市
            int County = Utils.GetInt(Utils.GetFormValue(ddlCounty.UniqueID), 0);//县区
            int Type = (int)EyouSoft.Model.EnumType.CrmStructure.CrmType.同行客户;//客户类型
            string Name = Utils.GetFormValue(txtName.UniqueID);//单位名称
            string Address = Utils.GetFormValue(txtAddress.UniqueID);//地址
            string OrganizationCode = Utils.GetFormValue(txtOrganizationCode.UniqueID);//机构代码
            //string VouchersCode = Utils.GetFormValue("txtVouchersCode");//凭证代码
            int LevId = Utils.GetInt(Utils.GetFormValue(ddlLevId.UniqueID), 0);//客户等级
            string License = Utils.GetFormValue(txtLicense.UniqueID);//许可证号
            string LegalRepresentative = Utils.GetFormValue(txtLegalRepresentative.UniqueID);//法人代表
            string LegalRepresentativePhone = Utils.GetFormValue(txtLegalRepresentativePhone.UniqueID);//法人代表电话
            string LegalRepresentativeMobile = Utils.GetFormValue(txtLegalRepresentativeMobile.UniqueID);//法人代表手机
            string FinancialName = Utils.GetFormValue(txtFinancialName.UniqueID);//财务姓名
            string FinancialPhone = Utils.GetFormValue(txtFinancialPhone.UniqueID);//财务电话
            string FinancialMobile = Utils.GetFormValue(txtFinancialMobile.UniqueID);//财务手机

            //decimal PreDeposits =Utils.GetDecimal(Utils.GetFormValue("txtPreDeposits"));//预存款
            string RebatePolicy = Utils.GetFormValue(txtRebatePolicy.UniqueID);//返利政策
            string BrevityCode = Utils.GetFormValue(txtBrevityCode.UniqueID);//简码
            //string UserAccount = Utils.GetFormValue("txtUserAccount");//分销账号

            crmModel.Address = Address;

            crmModel.CityId = City;
            crmModel.CompanyId = base.SiteUserInfo.CompanyId;

            crmModel.IssueTime = DateTime.Now;
            crmModel.LegalRepresentative = LegalRepresentative;
            crmModel.LegalRepresentativePhone = LegalRepresentativePhone;
            crmModel.LevId = LevId;
            crmModel.License = License;
            crmModel.Name = Name;
            crmModel.OperatorId = base.SiteUserInfo.UserId;
            crmModel.OrganizationCode = OrganizationCode;
            crmModel.ProvinceId = Provice;
            crmModel.RebatePolicy = RebatePolicy;
            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改责任销售))
                crmModel.SellerId = Utils.GetFormValue(Seller1.SellsIDClient);
            crmModel.Type = (EyouSoft.Model.EnumType.CrmStructure.CrmType)(Type);
            crmModel.BrevityCode = BrevityCode;
            crmModel.CountryId = Country;
            crmModel.CountyId = County;
            crmModel.DeptId = base.SiteUserInfo.DeptId;
            crmModel.FinancialMobile = FinancialMobile;
            crmModel.FinancialName = FinancialName;
            crmModel.FinancialPhone = FinancialPhone;
            crmModel.LegalRepresentativeMobile = LegalRepresentativeMobile;

            #endregion

            #region 常用联系人
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> linkManList = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            string[] BirthdayArray = Utils.GetFormValues("txtlBirthday");
            string[] DepartmentArray = Utils.GetFormValues("txtlDepartment");
            string[] FaxArray = Utils.GetFormValues("txtlFax");
            string[] MobilePhoneArray = Utils.GetFormValues("txtlMobilePhone");
            string[] NameArray = Utils.GetFormValues("txtllinkManName");
            string[] QQArray = Utils.GetFormValues("txtlQQ");
            string[] TelephoneArray = Utils.GetFormValues("txtlTel");
            string[] AddressArray = Utils.GetFormValues("txtlAddress");
            string[] IsRemindArray = Utils.GetFormValues("hidIsRemind");
            string[] LinkManId = Utils.GetFormValues("hidlLinkManId");
            string[] UserId = Utils.GetFormValues("hidlUserId");
            for (int j = 0; j < DepartmentArray.Length; j++)
            {
                EyouSoft.Model.CrmStructure.MCrmLinkman model = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                if (LinkManId.Length == 0)
                    model.Id = string.Empty;
                model.Id = LinkManId[j] == string.Empty ? string.Empty : LinkManId[j];
                if (UserId.Length == 0)
                    model.UserId = string.Empty;
                else
                    model.UserId = UserId[j] == string.Empty ? string.Empty : UserId[j];
                model.Birthday = !string.IsNullOrEmpty(BirthdayArray[j]) ? (DateTime?)(DateTime.Parse(BirthdayArray[j])) : null;
                model.CompanyId = base.SiteUserInfo.CompanyId;
                model.Department = !string.IsNullOrEmpty(DepartmentArray[j]) ? DepartmentArray[j] : string.Empty;
                model.Fax = !string.IsNullOrEmpty(FaxArray[j]) ? FaxArray[j] : string.Empty;
                model.MobilePhone = !string.IsNullOrEmpty(MobilePhoneArray[j]) ? MobilePhoneArray[j] : string.Empty;
                model.Name = !string.IsNullOrEmpty(NameArray[j]) ? NameArray[j] : string.Empty;
                model.QQ = !string.IsNullOrEmpty(QQArray[j]) ? QQArray[j] : string.Empty;
                model.Telephone = !string.IsNullOrEmpty(TelephoneArray[j]) ? TelephoneArray[j] : string.Empty;
                model.Type = (EyouSoft.Model.EnumType.ComStructure.LxrType)(Type);
                if (IsRemindArray.Length == 0)
                    model.IsRemind = false;
                else
                    model.IsRemind = IsRemindArray[j] == string.Empty ? false : true;
                model.Address = !string.IsNullOrEmpty(AddressArray[j]) ? AddressArray[j] : string.Empty;
                if (string.IsNullOrEmpty(model.Birthday.ToString()) && string.IsNullOrEmpty(model.Department) && string.IsNullOrEmpty(model.Fax) && string.IsNullOrEmpty(model.MobilePhone) && string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(model.QQ) && string.IsNullOrEmpty(model.Telephone) && string.IsNullOrEmpty(model.Address))
                {

                }
                else
                {
                    linkManList.Add(model);
                }
            }
            crmModel.LinkManList = linkManList != null || linkManList.Count != 0 ? linkManList : null;
            #endregion
            string filepath = string.Empty;
            string filename = string.Empty;
            #region 结算账户
            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_常用设置))
            {
                decimal AmountOwed = Utils.GetDecimal(Utils.GetFormValue("txtAmountOwed"));//欠款额度
                int Deadline = Utils.GetInt(Utils.GetFormValue("txtDeadline"));//单团账龄期限
                crmModel.AmountOwed = AmountOwed;
                crmModel.Deadline = Deadline;
                IList<EyouSoft.Model.CrmStructure.MCrmBank> bankList = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
                string[] BankAccountArray = Utils.GetFormValues("txtBankAccount");
                string[] BankNameArray = Utils.GetFormValues("txtBankName");
                string[] BankId = Utils.GetFormValues("hidBankId");
                for (int j = 0; j < BankNameArray.Length; j++)
                {
                    EyouSoft.Model.CrmStructure.MCrmBank model = new EyouSoft.Model.CrmStructure.MCrmBank();
                    model.BankAccount = !string.IsNullOrEmpty(BankAccountArray[j]) ? BankAccountArray[j] : string.Empty;
                    model.BankName = !string.IsNullOrEmpty(BankNameArray[j]) ? BankNameArray[j] : string.Empty;
                    model.BankId = BankId[j] == string.Empty ? string.Empty : BankId[j];
                    if (string.IsNullOrEmpty(model.BankAccount) && string.IsNullOrEmpty(model.BankName))
                    {

                    }
                    else
                    {
                        bankList.Add(model);
                    }
                }

                if (rbtnIsSignContractYes.Checked)
                {
                    crmModel.IsSignContract = true;
                }
                else if (rbtnIsSignContractNo.Checked)
                {
                    crmModel.IsSignContract = false;
                }


                string[] path = Utils.GetFormValue(this.UploadControl1.ClientHideID).Split('|');
                if (path.Length > 1)
                {
                    filename = path[0];
                    filepath = path[1];
                    EyouSoft.Model.ComStructure.MComAttach attachModel = new EyouSoft.Model.ComStructure.MComAttach();
                    attachModel.Downloads = 0;
                    attachModel.FilePath = filepath;
                    attachModel.Name = System.IO.Path.GetFileName(filepath);
                    crmModel.AttachModel = attachModel;
                }
                crmModel.BankList = bankList != null || bankList.Count != 0 ? bankList : null;
            }

            #endregion

            bool isAdd = Utils.GetQueryStringValue("isadd") == "false" ? false : true;
            #region 数据操作
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("crmId")))
            {
                crmModel.CrmId = Utils.GetQueryStringValue("crmId"); ;

                int result = 0;//crmBll.UpdateUnitCustomerModel(crmModel, isAdd);

                switch (result)
                {
                    case -1:
                        return UtilsCommons.AjaxReturnJson("-1", "已经存在相同的银行卡号!");

                    case -4:
                        return UtilsCommons.AjaxReturnJson("-4", "客户编号未赋值!");

                    case -2:
                        return UtilsCommons.AjaxReturnJson("-2", "已经存在相同的分销商帐号!");

                    case -3:
                        return UtilsCommons.AjaxReturnJson("-3", "客户必填信息不完善!");

                    case 0:
                        return UtilsCommons.AjaxReturnJson("0", "事务回滚!");

                    case 1:
                        return UtilsCommons.AjaxReturnJson("1", "修改成功!");
                    case -5:
                        return UtilsCommons.AjaxReturnJson("-5", "已经存在相同的单位名称");
                    default:
                        return UtilsCommons.AjaxReturnJson("-7", "数据操作失败!");
                }
            }
            else
            {
                int result = 0; //crmBll.AddUnitCustomerModel(crmModel, isAdd);

                switch (result)
                {
                    case -1:
                        return UtilsCommons.AjaxReturnJson("-1", "已经存在相同的银行卡号!");
                    case -4:
                        return UtilsCommons.AjaxReturnJson("-4", "公司编号未赋值!");
                    case -2:
                        return UtilsCommons.AjaxReturnJson("-2", "已经存在相同的分销商帐号!");
                    case -3:
                        return UtilsCommons.AjaxReturnJson("-3", "客户必填信息不完善!");
                    case 0:
                        return UtilsCommons.AjaxReturnJson("0", "事务回滚!");
                    case 1:
                        return UtilsCommons.AjaxReturnJson("1", "添加成功!");
                    case -5:
                        return UtilsCommons.AjaxReturnJson("-5", "已经存在相同的单位名称");
                    default:
                        return UtilsCommons.AjaxReturnJson("-7", "数据操作失败!");
                }
            }
            #endregion

        }

        private void DeleteFile(string file)
        {
            if (file != string.Empty)
            {
                System.IO.File.Delete(Server.MapPath(file));
            }
        }


        protected string GetDate(object obj)
        {
            if (obj != null)
            {
                return Convert.ToDateTime(obj).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }
        private void BindListData(string crmId)
        {

            EyouSoft.Model.CrmStructure.MCrm model = crmBll.GetInfo(crmId);
            if (model != null)
            {
                txtAddress.Text = model.Address;


                txtLegalRepresentative.Text = model.LegalRepresentative;
                txtLegalRepresentativePhone.Text = model.LegalRepresentativePhone;
                txtLicense.Text = model.License;
                txtName.Text = model.Name;
                txtOrganizationCode.Text = model.OrganizationCode;

                txtRebatePolicy.Text = model.RebatePolicy;
                Seller1.SellsID = model.SellerId;
                Seller1.SellsName = userBll.GetModel(model.SellerId, base.SiteUserInfo.CompanyId).ContactName;
                ddlCity.SelectedValue = model.CityId.ToString();
                ddlLevId.SelectedValue = model.LevId.ToString();
                ddlProvice.SelectedValue = model.ProvinceId.ToString();



                Province = model.ProvinceId.ToString();
                City = model.CityId.ToString();
                Country = model.CountryId.ToString();
                County = model.CountyId.ToString();
                txtBrevityCode.Text = model.BrevityCode;
                txtFinancialMobile.Text = model.FinancialMobile;
                txtFinancialName.Text = model.FinancialName;
                txtFinancialPhone.Text = model.FinancialPhone;
                txtLegalRepresentativeMobile.Text = model.LegalRepresentativeMobile;


                txtAmountOwed.Text = model.AmountOwed.ToString("C");
                txtDeadline.Text = model.Deadline.ToString();
                if (model.IsSignContract)
                {
                    rbtnIsSignContractYes.Checked = true;
                }
                else
                {
                    rbtnIsSignContractNo.Checked = true;
                }
                if (model.AttachModel != null)
                    FilePath = model.AttachModel.FilePath;
                #region 结算账户

                if (model.BankList.Count > 0)
                {
                    rptJSZHList.DataSource = model.BankList;
                    rptJSZHList.DataBind();
                }
                else
                {
                    IList<EyouSoft.Model.CrmStructure.MCrmBank> list = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
                    list.Add(new EyouSoft.Model.CrmStructure.MCrmBank() { BankAccount = string.Empty, BankName = string.Empty });
                    rptJSZHList.DataSource = list;
                    rptJSZHList.DataBind();
                }

               
                //修改责任销售权限
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改责任销售))
                {
                    this.Seller1.ReadOnly = true;

                }
                #endregion
            }


            #region 常用联系人绑定
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (model.LinkManList.Count > 0)
            {
                rptCYLXRList.DataSource = model.LinkManList;
                rptCYLXRList.DataBind();
            }
            else
            {
                IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
                list.Add(new EyouSoft.Model.CrmStructure.MCrmLinkman() { Department = string.Empty, Name = string.Empty, Telephone = string.Empty, MobilePhone = string.Empty, Birthday = null, QQ = string.Empty, Fax = string.Empty, Address = string.Empty, IsRemind = false, UserId = string.Empty });
                rptCYLXRList.DataSource = list;
                rptCYLXRList.DataBind();
            }
            #endregion


        }


        

        private void BindAddList()
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> linkManList = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            linkManList.Add(new EyouSoft.Model.CrmStructure.MCrmLinkman() { Department = string.Empty, Name = string.Empty, Telephone = string.Empty, MobilePhone = string.Empty, Birthday = null, QQ = string.Empty, Fax = string.Empty, Address = string.Empty, IsRemind = false });
            rptCYLXRList.DataSource = linkManList;
            rptCYLXRList.DataBind();
            IList<EyouSoft.Model.CrmStructure.MCrmBank> bankList = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
            bankList.Add(new EyouSoft.Model.CrmStructure.MCrmBank() { BankAccount = string.Empty, BankName = string.Empty });
            rptJSZHList.DataSource = bankList;
            rptJSZHList.DataBind();
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("crmId")))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改, false);
                    return;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_新增))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_新增, false);
                    return;
                }
            }

            //权限判定(常用设置)
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_常用设置))
            {
                txtAmountOwed.Text = "5000";
                txtDeadline.Text = "10";
                rbtnIsSignContractNo.Checked = true;
                rbtnIsSignContractNo.Enabled = false;
                rbtnIsSignContractYes.Enabled = false;
                UploadControl1.Visible = false;
                FilePath = string.Empty;

            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetEmpty(object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString()))
                return "false";
            else
                return obj.ToString();
        }


        /// <summary>
        /// 获取部门能否只读
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetEnable(object obj)
        {
            if ((bool)obj == false)
                return "readonly=true";
            else
                return string.Empty;

        }

        /// <summary>
        /// 获得联系人生日提醒
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetIsRemind(object obj)
        {
            if ((bool)obj == true)
            {
                return "checked=true";
            }
            else
                return string.Empty;
        }

        protected string GetPower()
        {
            //权限判定(常用设置)
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_常用设置))
            {
                return "readonly=true";
            }
            else
                return string.Empty;
        }

        protected string GetBtnPower()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_常用设置))
                return "style=\"display:none\"";
            else
                return string.Empty;
        }


        //protected string GetDate(object obj)
        //{
        //    if (obj != null)
        //        return ((DateTime)(obj)).ToShortDateString();
        //    else
        //        return string.Empty;
        //}
    }
}
