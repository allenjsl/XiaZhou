using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SourceStructure;

namespace Web.ResourceManage.Ground
{
    public partial class GroundEdit : BackPage
    {
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-22
        /// 说明：资源管理：地接社： 添加，修改
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            string doType = Utils.GetQueryStringValue("dotype");
            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
            string type = Utils.GetQueryStringValue("type");
            PowerControl(doType);

            #region 处理AJAX请求
            //存在ajax请求
            if (doType != "")
            {
                switch (type)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave(doType, id));
                        Response.End();
                        break;
                }
            }
            #endregion
            //根据ID初始化页面
            if (!IsPostBack)
            {
                PageInit(id, doType);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string dotype)
        {
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.txtLastHuman.Text = this.SiteUserInfo.Name;
            this.txtLastDate.Enabled = false;
            this.txtLastHuman.Enabled = false;

            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceTravel Model = BLL.GetTravelServiceModel(id);
                if (Model != null && Model.SourceModel != null)
                {
                    this.txtaddress.Text = Model.SourceModel.Address;
                    this.hidSourceID.Value = Model.SourceModel.SourceId;
                    if (Model.SourceModel.IsCommission == false)
                    {
                        this.radyes.Checked = false;
                        this.radno.Checked = true;
                    }
                    else
                    {
                        this.radno.Checked = false;
                        this.radyes.Checked = true;
                    }
                    this.txtUnitName.Text = Model.SourceModel.Name;
                    Countryindex = Model.SourceModel.CountryId;
                    Provinceindex = Model.SourceModel.ProvinceId;
                    Cityindex = Model.SourceModel.CityId;
                    Areaindex = Model.SourceModel.CountyId;
                    if (Model.LinkManList.Count > 0)
                        this.Contact1.SetTravelList = Model.LinkManList;
                    if (Model.SourceModel.ContractAttach != null)
                    {
                        //<a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{
                        StringBuilder strFile = new StringBuilder();
                        if (Model.SourceModel.ContractAttach.FilePath != "")
                        {
                            strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"GroundEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", Model.SourceModel.ContractAttach.FilePath, Model.SourceModel.ContractAttach.Name);
                        }
                        this.lbFiles.Text = strFile.ToString();
                    }
                    this.txtCharacter.Text = Model.SourceTravelModel.Routes;
                    this.txtLegalName.Text = Model.SourceTravelModel.LegalRepresentative;
                    this.txtTel.Text = Model.SourceTravelModel.Telephone;
                    //this.txtTel.Text=Model.SourceModel.未提供电话
                    this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(Model.SourceModel.ContractPeriodEnd);
                    this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(Model.SourceModel.ContractPeriodStart);

                    if (Model.SourceModel.IsSignContract)
                    {
                        this.Radio_hd_yes.Checked = true;
                        this.Radio_hd_no.Checked = false;
                        this.txtContractNum.Visible = true;
                        this.txtContractNum.Text = Model.SourceModel.ContractCode;
                    }
                    this.RadSign_no.Checked = true;
                    if (Model.SourceModel.IsPermission)
                    {
                        this.RadSign_yes.Checked = true;
                        this.RadSign_no.Checked = false;
                    }
                    this.RadRecommend_no.Checked = true;
                    if (Model.SourceModel.IsRecommend)
                    {
                        RadRecommend_yes.Checked = true;
                        RadRecommend_no.Checked = false;
                    }
                    this.txtLastDate.Text = UtilsCommons.GetDateString(Model.SourceModel.LastModifyTime, ProviderToDate);                    
                    this.txtLastHuman.Text = Model.SourceModel.LastModifierId;
                    this.txtPermitNum.Text = Model.SourceModel.LicenseKey;
                    this.txtpolicy.Text = Model.SourceModel.UnitPolicy;
                }
                else
                {
                    Utils.ResponseGoBack();
                }
            }
            
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        private string PageSave(string doType, string id)
        {

            string msg = string.Empty;
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            //国家
            #region 表单取值
            string country = Utils.GetFormValue(this.ddlCountry.UniqueID);
            //省份
            string provice = Utils.GetFormValue(this.ddlProvice.UniqueID);
            //城市
            string city = Utils.GetFormValue(this.ddlCity.UniqueID);
            //县区
            string area = Utils.GetFormValue(this.ddlArea.UniqueID);
            //地址
            string address = Utils.GetFormValue(this.txtaddress.UniqueID);
            //单位名称
            string UnitName = Utils.GetFormValue(this.txtUnitName.UniqueID);
            //线路特色
            string Character = Utils.GetFormValue(this.txtCharacter.UniqueID);
            //合同开始时间
            string ContractStartDate = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同有到期时间
            string ContractDate = Utils.GetFormValue(this.txtContractDate.UniqueID);
            //合同号
            string ContractNum = Utils.GetFormValue(this.txtContractNum.UniqueID);
            //法人代表
            string LegalName = Utils.GetFormValue(this.txtLegalName.UniqueID);
            //许可证号
            string PermitNum = Utils.GetFormValue(this.txtPermitNum.UniqueID);
            //返佣政策
            string Policy = Utils.GetFormValue(this.txtpolicy.UniqueID);
            //联系电话
            string Tel = Utils.GetFormValue(this.txtTel.UniqueID);
            //合同附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideFileInfo");
            #endregion

            EyouSoft.Model.CrmStructure.MCrmLinkman m = new EyouSoft.Model.CrmStructure.MCrmLinkman();
            MComUser mUser = new MComUser();
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = UtilsCommons.GetDataList();
            if (list != null && list.Count > 0)
            {
                mUser.ContactMobile = list[0].MobilePhone;
                mUser.ContactName = list[0].Name;
                mUser.ContactTel = list[0].Telephone;
                mUser.QQ = list[0].QQ;
                m.Post = list[0].Post;
            }
            if (string.IsNullOrEmpty(UnitName))
            {
                msg = "{\"result\":\"0\",\"msg\":\"单位名称不能为空!\"}";
                return msg;
            }
            EyouSoft.Model.SourceStructure.MSourceTravel mTravel = new EyouSoft.Model.SourceStructure.MSourceTravel();
            if (!string.IsNullOrEmpty(id))
            {
                mTravel = new EyouSoft.BLL.SourceStructure.BSource().GetTravelServiceModel(id);
            }
            EyouSoft.Model.SourceStructure.MSource mModel = new EyouSoft.Model.SourceStructure.MSource();
            mModel.Address = this.txtaddress.Text;
            mModel.CityId = Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID));
            mModel.CountryId = Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID));
            mModel.CountyId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            mModel.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ddlProvice.UniqueID));
            mModel.Name = Utils.GetFormValue(this.txtUnitName.UniqueID).Trim();
            mModel.CompanyId = this.SiteUserInfo.CompanyId;
            mModel.OperatorId = this.SiteUserInfo.UserId;
            mModel.ContractCode = ContractNum;
            mModel.DeptId = this.SiteUserInfo.DeptId;

            mModel.ContractPeriodStart = String.IsNullOrEmpty(ContractStartDate) ? null : (DateTime?)DateTime.Parse(ContractStartDate);
            mModel.ContractPeriodEnd = String.IsNullOrEmpty(ContractDate) ? null : (DateTime?)DateTime.Parse(ContractDate);

            //签单
            if (RadSign_no.Checked)
            {
                mModel.IsPermission = false;
            }
            else
            {
                mModel.IsPermission = true;
            }
            //推荐
            if (RadRecommend_no.Checked)
            {
                mModel.IsRecommend = false;
            }
            else
            {
                mModel.IsRecommend = true;
            }
            //返佣
            mModel.IsCommission = true;
            if (radno.Checked)
            {
                mModel.IsCommission = false;
            }
            //合同
            if (Radio_hd_no.Checked)
            {
                mModel.IsSignContract = false;
            }
            else
            {
                mModel.IsSignContract = true;
                mModel.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }
            mModel.LastModifierId = this.SiteUserInfo.Name;
            mModel.LastModifyTime = DateTime.Now;
            mModel.LicenseKey = PermitNum;
            mModel.UnitPolicy = Policy;
            mModel.Desc = Character;
            mModel.IssueTime = DateTime.Now;

            if (!String.IsNullOrEmpty(id))
            {
                mModel.SourceId = id;
            }

            #region 合同附件
            EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();
            if (oldVisaUpload.Length > 0)
            {
                for (int i = 0; i < oldVisaUpload.Length; i++)
                {
                    visaModel.FilePath = oldVisaUpload[i].Split('|')[1];
                    visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
                    visaModel.Name = oldVisaUpload[i].Split('|')[0];
                    visaModel.Size = 0;
                }
            }
            if (visaUpload.Length > 0)
            {
                for (int i = 0; i < visaUpload.Length; i++)
                {
                    if (visaUpload[i].Trim() != "")
                    {
                        if (visaUpload[i].Split('|').Length > 1)
                        {
                            visaModel.Downloads = 0;
                            visaModel.FilePath = visaUpload[i].Split('|')[1];
                            visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
                            visaModel.Name = visaUpload[i].Split('|')[0];
                            visaModel.Size = 0;
                        }
                    }
                }
            }

            mModel.ContractAttach = visaModel;
            #endregion

            mUser.CompanyId = this.SiteUserInfo.CompanyId;
            //mUser.IsEnable = true;
            mUser.IssueTime = DateTime.Now;
            mUser.UserType = EyouSoft.Model.EnumType.ComStructure.UserType.供应商;
            MSourceTravelInfo Traveinfo = new MSourceTravelInfo();
            Traveinfo.LegalRepresentative = LegalName;
            Traveinfo.Routes = Character;
            Traveinfo.Telephone = Tel;
            mTravel.SourceTravelModel = Traveinfo;
            //mTravel.UserModel = mUser;
            mTravel.SourceModel = mModel;
            mTravel.LinkManList = list;
            //mTravel.SourceTravelModel
            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.BLL.ComStructure.BComUser blluser = new EyouSoft.BLL.ComStructure.BComUser();

            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddTravelServiceModel(mTravel);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"单位名称已存在!\"}";
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateTravelServiceModel(mTravel);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"单位名称已存在!\"}";
                    return msg;
                }
            }
            string type = t ? "新增" : "修改";
            if (result > 0)
            {
                msg = "{\"result\":\"1\",\"msg\":\"" + type + "成功!\"}";
                return msg;
            }
            else
            {
                msg = "{\"result\":\"0\",\"msg\":\"" + type + "失败!\"}";
                return msg;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(string dotype)
        {
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_新增))
                {
                    this.btnSave.Visible = false;
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
