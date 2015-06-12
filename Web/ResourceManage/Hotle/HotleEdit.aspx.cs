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

namespace Web.ResourceManage.Hotle
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-22
    /// 说明：资源管理：地接社： 添加，修改
    public partial class HotleEdit : BackPage
    {
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            string doType = Request.QueryString["doType"];
            string type = Request.QueryString["type"];
            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
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
            this.txtLastHuman.Text = this.SiteUserInfo.Name;
            this.txtLastDate.Enabled = false;
            this.txtLastHuman.Enabled = false;

            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceDining model = bll.GetDiningModel(id);
                if (model != null)
                {
                    if (model.SourceModel != null)
                    {
                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
                        this.txtaddress.Text = model.SourceModel.Address;
                        this.txthotleinfo.Text = model.SourceModel.Desc;
                        this.txtHotleName.Text = model.SourceModel.Name;
                        this.txtRemarks.Text = model.SourceModel.Remark;
                        this.txtContractDate.Text = UtilsCommons.GetDateString(model.SourceModel.ContractPeriodEnd, ProviderToDate);
                        this.txtContractDate_Start.Text = UtilsCommons.GetDateString(model.SourceModel.ContractPeriodStart, ProviderToDate);
                        this.txtLastDate.Text = UtilsCommons.GetDateString(model.SourceModel.LastModifyTime, ProviderToDate);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                        string cuisinename = string.Empty;
                        string cuisineid = string.Empty;
                        if (model.DiningCuisineList != null && model.DiningCuisineList.Count > 0)
                        {
                            for (int i = 0; i < model.DiningCuisineList.Count; i++)
                            {
                                if (model.DiningCuisineList.Count - 1 == i)
                                {
                                    cuisinename += model.DiningCuisineList[i].Cuisine.ToString();
                                    cuisineid += ((int)model.DiningCuisineList[i].Cuisine).ToString();
                                }
                                else
                                {
                                    cuisinename += model.DiningCuisineList[i].Cuisine.ToString() + ",";
                                    cuisineid += ((int)model.DiningCuisineList[i].Cuisine).ToString() + ",";
                                }
                            }
                        }
                        this.CuisineSelect1.CuisineSelectName = cuisinename;
                        this.CuisineSelect1.CuisineSelectID = cuisineid;

                        radno.Checked = true;
                        if (model.SourceModel.IsCommission)
                        {
                            radyes.Checked = true;
                            radno.Checked = false;
                        }
                        Radio_hd_no.Checked = true;
                        if (model.SourceModel.IsSignContract)
                        {
                            Radio_hd_yes.Checked = true;
                            this.Radio_hd_no.Checked = false;
                            this.txtContractNum.Visible = true;
                            this.txtContractNum.Text = model.SourceModel.ContractCode;
                        }
                        RadRecommend_no.Checked = true;
                        if (model.SourceModel.IsRecommend)
                        {
                            RadRecommend_yes.Checked = true;
                            RadRecommend_no.Checked = false;
                        }
                        RadSign_no.Checked = true;
                        if (model.SourceModel.IsPermission)
                        {
                            RadSign_yes.Checked = true;
                            RadSign_no.Checked = false;
                        }
                        if (model.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (model.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"CruiseEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                            }
                            this.lbFiles.Text = strFile.ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(model.DiningStandard) && model.DiningStandard.Split('-').Length > 1)
                    {
                        this.txtPmax.Text = model.DiningStandard.Split('-')[1].Replace("元", "");
                        this.txtPmin.Text = model.DiningStandard.Split('-')[0].Replace("元", "");
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
            
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave(string doType, string id)
        {
            string msg = string.Empty;
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            //餐馆简介
            string Hotleinfo = Utils.GetFormValue(this.txthotleinfo.UniqueID);
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
            //菜系编号
            string cuisineid = Utils.GetFormValue(this.CuisineSelect1.CuisineSelectIDClient);
            //餐馆名称
            string HotleName = Utils.GetFormValue(this.txtHotleName.UniqueID).Trim();
            //备注
            string remarks = Utils.GetFormValue(this.txtRemarks.UniqueID);
            //餐标最大价格
            string Pmax = Utils.GetFormValue(this.txtPmax.UniqueID);
            //餐标最小价格
            string Pmin = Utils.GetFormValue(this.txtPmin.UniqueID);
            //合同开始时间
            string StartTime = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同终止时间
            string EndTime = Utils.GetFormValue(this.txtContractDate.UniqueID);
            if (String.IsNullOrEmpty(HotleName))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "请填写餐馆名称！");
            }
            if (string.IsNullOrEmpty(cuisineid))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "请选择菜系!");
            }
            EyouSoft.Model.SourceStructure.MSourceDining mDining = new EyouSoft.Model.SourceStructure.MSourceDining();
            EyouSoft.Model.SourceStructure.MSource msource = new EyouSoft.Model.SourceStructure.MSource();
            if (!string.IsNullOrEmpty(id))
            {
                mDining = new EyouSoft.BLL.SourceStructure.BSource().GetDiningModel(id);
            }
            IList<EyouSoft.Model.SourceStructure.MSourceDiningCuisine> cuisine = new List<EyouSoft.Model.SourceStructure.MSourceDiningCuisine>();
            for (int i = 0; i < cuisineid.Split(',').Length; i++)
            {
                cuisine.Add(new EyouSoft.Model.SourceStructure.MSourceDiningCuisine() { Cuisine = (EyouSoft.Model.EnumType.SourceStructure.SourceCuisine)(Utils.GetInt(cuisineid.Split(',')[i])) });
            }

            mDining.DiningCuisineList = cuisine;
            if (!string.IsNullOrEmpty(Pmax + Pmin))
            {
                mDining.DiningStandard = Pmin + "元-" + Pmax + "元";
            }
            else
            {
                mDining.DiningStandard = "";
            }
            mDining.LinkManList = Contact1.GetTravelList;
            if (!String.IsNullOrEmpty(id))
                mDining.SourceId = id;
            msource.CompanyId = this.SiteUserInfo.CompanyId;
            msource.Address = address;
            msource.ProvinceId = Utils.GetInt(provice);
            msource.CityId = Utils.GetInt(city);
            msource.CountyId = Utils.GetInt(area);
            msource.CountryId = Utils.GetInt(country);
            msource.Desc = Hotleinfo;
            msource.Remark = remarks;
            msource.LastModifierId = this.SiteUserInfo.Name;
            msource.LastModifyTime = DateTime.Now;
            msource.ContractPeriodStart = Utils.GetDateTimeNullable(StartTime);
            msource.ContractPeriodEnd = Utils.GetDateTimeNullable(EndTime);
            msource.OperatorId = this.SiteUserInfo.UserId;
            msource.IssueTime = DateTime.Now;
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
            msource.ContractAttach = visaModel;
            msource.IsCommission = true;
            //是否签单
            msource.IsPermission = false;
            if (RadSign_yes.Checked)
            {
                msource.IsPermission = true;
            }
            //是否返佣
            msource.IsCommission = false;
            if (radyes.Checked)
            {
                msource.IsCommission = true;
            }
            //是否推荐
            msource.IsRecommend = false;
            if (RadRecommend_yes.Checked)
            {
                msource.IsRecommend = true;
            }
            //是否签订合同
            msource.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                msource.IsSignContract = true;
                msource.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }
            msource.Name = HotleName;
            mDining.SourceModel = msource;
            mDining.LinkManList = UtilsCommons.GetDataList();

            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddDiningModel(mDining);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"餐馆名称已存在!\"}";
                    return msg;
                }
                if (result == -2)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"餐馆已存在此菜系!\"}";
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateDiningModel(mDining);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"餐馆名称已存在!\"}";
                    return msg;
                }
                if (result == -2)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"餐馆已存在此菜系!\"}";
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_新增))
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
