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

namespace Web.ResourceManage.Ticket
{
    public partial class TicketEdit : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-22
        /// 说明：资源管理：地接社： 添加，修改
        /// 
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string type = Utils.GetQueryStringValue("type");
            string id = Utils.GetQueryStringValue("id");
            #region Ajax 执行保存
            if (doType != "")
            {
                switch (type.ToLower())
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave(doType, id));
                        Response.End();
                        break;
                }
            }
            #endregion
            //权限验证
            PowerControl(doType);

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(doType, id);
            }

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string dotype, string id)
        {
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.UploadControl2.CompanyID = this.SiteUserInfo.CompanyId;
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceTicket model = bll.GetTicketModel(id);
                if (model != null)
                {
                    if (model.SourceModel != null)
                    {
                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
                        this.hidSourceID.Value = id;
                        this.txtaddress.Text = model.SourceModel.Address;
                        this.txtRemarks.Text = model.SourceModel.Remark;
                        this.txtUnitName.Text = model.SourceModel.Name;
                        this.txtPolicy.Text = model.SourceModel.UnitPolicy;
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodEnd);
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodStart);
                        if (model.SourceModel.IsSignContract)
                        {
                            this.Radio_hd_no.Checked = false;
                            this.Radio_hd_yes.Checked = true;
                            this.txtContractNum.Visible = true;
                            this.txtContractNum.Text = model.SourceModel.ContractCode;
                        }
                        this.txtLastDate.Text = UtilsCommons.GetDateString(model.SourceModel.LastModifyTime, ProviderToDate);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                    }

                    if (model.SourceModel.IsCommission)
                    {
                        this.radyes.Checked = true;
                        radno.Checked = false;
                    }
                    if (model.SourceModel.IsPermission)
                    {
                        this.RadSign_yes.Checked = true;
                        this.RadSign_no.Checked = false;
                    }
                    if (model.SourceModel.IsRecommend)
                    {
                        this.RadRecommend_yes.Checked = true;
                        this.RadRecommend_no.Checked = false;
                    }
                    if (model.SourceModel.ContractAttach != null)
                    {
                        StringBuilder strFile = new StringBuilder();
                        if (model.SourceModel.ContractAttach.FilePath != "")
                        {
                            strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"TicketPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                        }
                        this.lbhd.Text = strFile.ToString();
                    }
                    if (model.SourceModel.AgreementFile != "")
                    {
                        StringBuilder agreement = new StringBuilder();
                        agreement.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"TicketPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideagreement\" value='{1}|{0}'/></span>", model.SourceModel.AgreementFile, getfilename(model.SourceModel.AgreementFile));
                        this.lbFiles.Text = agreement.ToString();
                    }

                    if (model.LinkManList.Count > 0)
                    {
                        this.Contact1.SetTravelList = model.LinkManList;
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
            #region 获取表单值
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
            //备注
            string remarks = Utils.GetFormValue(this.txtRemarks.UniqueID);
            //单位名称
            string UnitName = Utils.GetFormValue(this.txtUnitName.UniqueID).Trim();
            //政策
            string UnitPolicy = Utils.GetFormValue(this.txtPolicy.UniqueID);
            string ContractStart = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            string ContractEnd = Utils.GetFormValue(this.txtContractDate.UniqueID);
            string ContractNum = Utils.GetFormValue(this.txtContractNum.UniqueID);
            #endregion
            if (String.IsNullOrEmpty(UnitName))
            {
                msg = UtilsCommons.AjaxReturnJson("0", "单位名存不能为空");
                return msg;
            }

            //合作协议
            string[] agrUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
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
            string[] visaUpload = Utils.GetFormValues(this.UploadControl2.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideFileInfo");
            #region 合同附件
            EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();

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
            #endregion

            #region 实体赋值
            EyouSoft.Model.SourceStructure.MSourceTicket Model = new EyouSoft.Model.SourceStructure.MSourceTicket();
            EyouSoft.Model.SourceStructure.MSource source = new EyouSoft.Model.SourceStructure.MSource();
            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            if (!String.IsNullOrEmpty(id))
            {
                source.SourceId = id;
            }
            source.CompanyId = this.SiteUserInfo.CompanyId;
            source.OperatorId = this.SiteUserInfo.UserId;
            source.DeptId = this.SiteUserInfo.DeptId;
            source.AgreementFile = agreement;
            source.ContractAttach = visaModel;
            source.Address = address;
            //合同
            if (Radio_hd_yes.Checked)
            {
                source.IsSignContract = true;
                source.ContractCode = ContractNum;
            }
            else
            {
                source.IsSignContract = false;
                source.ContractCode = "";
            }
            //推荐
            source.IsRecommend = false;
            if (RadRecommend_yes.Checked)
            {
                source.IsRecommend = true;
            }
            //签单
            source.IsPermission = false;
            if (RadSign_yes.Checked)
            {
                source.IsPermission = true;
            }
            //返佣
            source.IsCommission = false;
            if (radyes.Checked)
            {
                source.IsCommission = true;
            }
            source.ContractPeriodStart = String.IsNullOrEmpty(ContractStart) ? null : (DateTime?)DateTime.Parse(ContractStart);
            source.ContractPeriodEnd = String.IsNullOrEmpty(ContractEnd) ? null : (DateTime?)DateTime.Parse(ContractEnd);
            source.IssueTime = DateTime.Now;
            source.CountryId = Utils.GetInt(country);
            source.ProvinceId = Utils.GetInt(provice);
            source.CityId = Utils.GetInt(city);
            source.CountyId = Utils.GetInt(area);
            source.Name = UnitName;
            source.Remark = remarks;
            source.UnitPolicy = UnitPolicy;
            source.LastModifierId = this.SiteUserInfo.Name;
            source.LastModifyTime = DateTime.Now;
            Model.LinkManList = UtilsCommons.GetDataList();
            Model.SourceModel = source;
            #endregion

            #region 执行新增、修改 并返回执行结果
            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddTicketModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "单位名已存在！");
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateTicketModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "单位名已存在！");
                    return msg;
                }
            }
            string type = t ? "新增" : "修改";
            if (result > 0)
            {
                msg = UtilsCommons.AjaxReturnJson("1", type + "成功!");
                return msg;
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", type + "失败！");
                return msg;
            }
            #endregion
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(string dotype)
        {
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_票务_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_票务_新增))
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
