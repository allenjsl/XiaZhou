using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace Web.ResourceManage.Shopping
{
    public partial class ShopEdit : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-26
        /// 说明：资源管理：景点： 添加，修改

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
                EyouSoft.Model.SourceStructure.MSourceShop model = bll.GetShopModel(id);
                if (model != null)
                {
                    this.txtSellType.Text = model.SellType;
                    if (model.SourceModel != null)
                    {
                        this.hidSourceID.Value = model.SourceModel.SourceId;
                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
                        this.txtRemarks.Text = model.SourceModel.Remark;
                        this.txtaddress.Text = model.SourceModel.Address;
                        this.txtPolicy.Text = model.SourceModel.UnitPolicy;
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodEnd);
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodStart);
                        this.txtLastDate.Text = UtilsCommons.GetDateString(model.SourceModel.LastModifyTime, ProviderToDate);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                        this.txtShopName.Text = model.SourceModel.Name;
                        //返佣
                        if (model.SourceModel.IsCommission)
                        {
                            radyes.Checked = true;
                            radno.Checked = false;
                        }
                        //返单
                        if (model.SourceModel.IsBackSingle)
                        {
                            radBackSingleyes.Checked = true;
                            radBackSingleno.Checked = false;
                        }
                        //签单
                        if (model.SourceModel.IsPermission)
                        {
                            RadSign_yes.Checked = true;
                            RadSign_no.Checked = false;
                        }
                        //合同
                        if (model.SourceModel.IsSignContract)
                        {
                            Radio_hd_yes.Checked = true;
                            Radio_hd_no.Checked = false;
                            this.txtContractNum.Text = model.SourceModel.ContractCode;
                        }
                        //推荐
                        if (model.SourceModel.IsRecommend)
                        {
                            RadRecommend_yes.Checked = true;
                            RadRecommend_no.Checked = false;
                        }
                        if (model.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (model.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"ShopEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                            }
                            this.lbhd.Text = strFile.ToString();
                        }
                        if (model.SourceModel.AgreementFile != "")
                        {
                            StringBuilder agreement = new StringBuilder();
                            agreement.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"ShopEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideagreement\" value='{1}|{0}'/></span>", model.SourceModel.AgreementFile, getfilename(model.SourceModel.AgreementFile));
                            this.lbFiles.Text = agreement.ToString();
                        }
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
            //商店名称
            string ShopName = Utils.GetFormValue(this.txtShopName.UniqueID).Trim();
            //商品类别
            string saleType = Utils.GetFormValue(this.txtSellType.UniqueID);
            //备注
            string remarks = Utils.GetFormValue(this.txtRemarks.UniqueID);
            //返佣政策
            string Policy = Utils.GetFormValue(this.txtPolicy.UniqueID);
            //合同开始时间
            string ContractStartDate = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同有到期时间
            string ContractDate = Utils.GetFormValue(this.txtContractDate.UniqueID);
            //合同号
            string ContractNum = Utils.GetFormValue(this.txtContractNum.UniqueID);

            if (String.IsNullOrEmpty(ShopName))
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "商店名称不能为空");
                return msg;
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
            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MSourceShop Model = new EyouSoft.Model.SourceStructure.MSourceShop();
            EyouSoft.Model.SourceStructure.MSource source = new EyouSoft.Model.SourceStructure.MSource();
            source.CompanyId = this.SiteUserInfo.CompanyId;
            source.OperatorId = this.SiteUserInfo.UserId;
            source.DeptId = this.SiteUserInfo.DeptId;
            source.LastModifierId = this.SiteUserInfo.Name;
            source.Name = ShopName;
            source.UnitPolicy = Policy;
            Model.SellType = saleType;
            source.ContractPeriodEnd = string.IsNullOrEmpty(ContractDate) ? null : (DateTime?)DateTime.Parse(ContractDate);
            source.ContractPeriodStart = string.IsNullOrEmpty(ContractStartDate) ? null : (DateTime?)DateTime.Parse(ContractStartDate);
            source.LastModifyTime = DateTime.Now;
            source.IssueTime = DateTime.Now;
            source.Address = address;
            source.ContractAttach = visaModel;
            source.AgreementFile = agreement;
            if (!String.IsNullOrEmpty(id))
            {
                source.SourceId = id;
            }
            //返佣
            source.IsCommission = false;
            if (radyes.Checked)
            {
                source.IsCommission = true;
            }
            //返单
            source.IsBackSingle = false;
            if (radBackSingleyes.Checked)
            {
                source.IsBackSingle = true;
            }
            //签单
            source.IsPermission = false;
            if (RadSign_yes.Checked)
            {
                source.IsPermission = true;
            }
            //推荐
            source.IsRecommend = false;
            if (RadRecommend_yes.Checked)
            {
                source.IsRecommend = true;
            }
            //合同
            source.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                source.IsSignContract = true;
                source.ContractCode = ContractNum;
            }
            source.Remark = remarks;
            source.CountryId = Utils.GetInt(country);
            source.ProvinceId = Utils.GetInt(provice);
            source.CityId = Utils.GetInt(city);
            source.CountyId = Utils.GetInt(area);
            source.Name = ShopName;
            //商品类别未赋值
            source.OperatorId = this.SiteUserInfo.UserId;
            Model.LinkManList = UtilsCommons.GetDataList();
            Model.SourceModel = source;
            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddShopModel(Model);
                if (result == -1)
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "商店名称已存在");
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateShopModel(Model);
                if (result == -1)
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "商店名称已存在");
                    return msg;
                }
            }
            string type = t ? "新增" : "修改";
            if (result > 0)
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", type + "成功");
                return msg;
            }
            else
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", type + "失败");
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_新增))
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
