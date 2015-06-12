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

namespace Web.ResourceManage.Other
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-09
    /// 说明：资源管理：其它： 添加，修改
    public partial class OtherEdit : BackPage
    {
        protected int SetListCount = 0;
        private IList<EyouSoft.Model.SourceStructure.MSourceOtherType> _setOtherList;
        private IList<EyouSoft.Model.SourceStructure.MSourceOtherType> SetOtherList
        {
            get { return _setOtherList; }
            set { _setOtherList = value; }
        }
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string type = Utils.GetQueryStringValue("type");
            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
            //权限验证
            PowerControl(doType);
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
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id, doType);
                SetDataList();
            }
            else
            {
                GetDataList();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string dotype)
        {
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.UploadControl2.CompanyID = this.SiteUserInfo.CompanyId;
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceOther model = bll.GetOtherModel(id);
                if (model != null)
                {
                    if (model.OtherTypeList != null && model.OtherTypeList.Count > 0)
                    {
                        SetListCount = model.OtherTypeList.Count;
                        this.SetOtherList = model.OtherTypeList;
                    }
                    if (model.LinkManList != null && model.LinkManList.Count > 0)
                    {
                        this.Contact1.SetTravelList = model.LinkManList;
                    }
                    if (model.SourceModel != null)
                    {
                        //签单
                        RadSign_no.Checked = true;
                        if (model.SourceModel.IsPermission)
                        {
                            RadSign_yes.Checked = true;
                            RadSign_no.Checked = false;
                        }
                        //推荐
                        RadRecommend_no.Checked = true;
                        if (model.SourceModel.IsRecommend)
                        {
                            RadRecommend_yes.Checked = true;
                            RadRecommend_no.Checked = false;
                        }
                        //合同
                        Radio_hd_no.Checked = true;
                        if (model.SourceModel.IsSignContract)
                        {
                            Radio_hd_yes.Checked = true;
                            Radio_hd_no.Checked = false;
                            this.txtContractNum.Visible = true;
                            this.txtContractNum.Text = model.SourceModel.ContractCode;
                            this.txtContractDate.Text = model.SourceModel.ContractCode;
                        }

                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodStart);
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodEnd);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                        this.txtLastDate.Text = model.SourceModel.LastModifyTime.ToString();
                        this.txtaddress.Text = model.SourceModel.Address;
                        this.txtUnitName.Text = model.SourceModel.Name;
                        this.txtRemarks.Text = model.SourceModel.Remark;
                        if (model.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (model.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"OtherEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                            }
                            this.lbhd.Text = strFile.ToString();
                        }
                        if (model.SourceModel.AgreementFile != "")
                        {
                            StringBuilder agreement = new StringBuilder();
                            agreement.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"OtherEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideagreement\" value='{1}|{0}'/></span>", model.SourceModel.AgreementFile, getfilename(model.SourceModel.AgreementFile));
                            this.lbFiles.Text = agreement.ToString();
                        }
                    }
                }
                else
                {
                    Utils.ResponseGoBack();
                }
            }
            this.txtLastDate.Enabled = false;
            this.txtLastHuman.Enabled = false;
        }
        private string getfilename(string path)
        {
            string[] arr = null;
            if (path.Contains("//"))
            {
                arr = path.Split('\\');
            }
            if (path.Contains("/"))
            {
                arr = path.Split('/');
            }
            if (arr != null && arr.Length > 0)
            {
                return arr[arr.Length - 1];
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave(string doType, string id)
        {
            string msg = string.Empty;
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            //国家
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
            string UnitName = Utils.GetFormValue(this.txtUnitName.UniqueID).Trim();
            //备注
            string remarks = Utils.GetFormValue(this.txtRemarks.UniqueID);
            //合同开始时间
            string ContractStartDate = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同有到期时间
            string ContractDate = Utils.GetFormValue(this.txtContractDate.UniqueID);
            //合同号
            string ContractNum = Utils.GetFormValue(this.txtContractNum.UniqueID);

            EyouSoft.Model.SourceStructure.MSourceOther Model = new EyouSoft.Model.SourceStructure.MSourceOther();
            EyouSoft.Model.SourceStructure.MSource source = new EyouSoft.Model.SourceStructure.MSource();
            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            if (!String.IsNullOrEmpty(id))
            {
                source.SourceId = id;
            }
            //合作协议
            string[] agrUpload = Utils.GetFormValues(this.UploadControl2.ClientHideID);
            string[] oldagrUpload = Utils.GetFormValues("hideagreement");
            #region 合作协议上传
            string agreement = string.Empty;
            if (oldagrUpload.Length > 0)
            {
                for (int i = 0; i < oldagrUpload.Length; i++)
                {
                    agreement = oldagrUpload[i].Split('|')[1];
                }
            }
            if (agrUpload.Length > 0)
            {
                for (int i = 0; i < agrUpload.Length; i++)
                {
                    if (agrUpload[i].Trim() != "")
                    {
                        if (agrUpload[i].Split('|').Length > 1)
                        {
                            if (agrUpload[i].Length > 1)
                            {
                                agreement = agrUpload[i].Split('|')[1];
                            }
                        }
                    }
                }
            }

            #endregion

            //合同附件
            //合同附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideFileInfo");
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

            #endregion
            source.ContractAttach = visaModel;
            source.AgreementFile = agreement;
            source.Address = address;
            source.Remark = remarks;
            source.CountryId = Utils.GetInt(country);
            source.ProvinceId = Utils.GetInt(provice);
            source.CityId = Utils.GetInt(city);
            source.CountyId = Utils.GetInt(area);
            source.IssueTime = DateTime.Now;
            source.CompanyId = this.SiteUserInfo.CompanyId;
            source.OperatorId = this.SiteUserInfo.UserId;
            source.DeptId = this.SiteUserInfo.DeptId;
            source.LastModifierId = this.SiteUserInfo.Name;
            source.LastModifyTime = DateTime.Now;
            source.Name = UnitName;
            source.ContractPeriodEnd = string.IsNullOrEmpty(ContractDate) ? null : (DateTime?)DateTime.Parse(ContractDate);
            source.ContractPeriodStart = string.IsNullOrEmpty(ContractStartDate) ? null : (DateTime?)DateTime.Parse(ContractStartDate);
            //是否签单
            source.IsPermission = false;
            if (RadSign_yes.Checked)
            {
                source.IsPermission = true;
            }
            source.CompanyId = this.SiteUserInfo.CompanyId;

            //是否推荐
            source.IsRecommend = false;
            if (RadRecommend_yes.Checked)
            {
                source.IsRecommend = true;
            }
            //是否签订合同
            source.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                source.IsSignContract = true;
                source.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }

            Model.SourceModel = source;
            Model.LinkManList = UtilsCommons.GetDataList();
            Model.OtherTypeList = GetDataList();
            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddOtherModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "单位名称已存在");
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateOtherModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "单位名称已存在");
                    return msg;
                }
            }
            string type = t ? "新增" : "修改";
            if (result > 0)
            {
                msg = UtilsCommons.AjaxReturnJson("1", type + "成功");
                return msg;
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", type + "失败");
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_新增))
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
        protected IList<EyouSoft.Model.SourceStructure.MSourceOtherType> GetDataList()
        {
            //名称
            string[] TypeName = Utils.GetFormValues("TypeName");
            //价格
            string[] Price = Utils.GetFormValues("Price");
            //结算价
            string[] ClosingCost = Utils.GetFormValues("ClosingCost");
            //简要描述
            string[] Desc = Utils.GetFormValues("Desc");

            string errorMsg = "";

            if (TypeName.Length > 0)
            {
                IList<EyouSoft.Model.SourceStructure.MSourceOtherType> list = new List<EyouSoft.Model.SourceStructure.MSourceOtherType>();
                for (int i = 0; i < TypeName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(TypeName[i].ToString()))
                    {
                        EyouSoft.Model.SourceStructure.MSourceOtherType model = new EyouSoft.Model.SourceStructure.MSourceOtherType();
                        if (TypeName[i].Trim() == "")
                            errorMsg += "第" + i.ToString() + "的名称不能为空! &nbsp;";
                        else
                            model.Name = TypeName[i];//房型
                        model.Price = Utils.GetDecimal(Price[i]);
                        //网络价
                        model.ClosingCost = Utils.GetDecimal(ClosingCost[i]);
                        //散客价
                        model.Desc = Desc[i];
                        list.Add(model);
                    }
                }
                if (errorMsg != "")
                    return null;
                else
                    return list;
            }
            else
                return null;
        }

        /// <summary>
        /// 页面初始化时绑定数据
        /// </summary>
        private void SetDataList()
        {
            this.rptlist.DataSource = this.SetOtherList;
            this.rptlist.DataBind();
        }
    }
}
