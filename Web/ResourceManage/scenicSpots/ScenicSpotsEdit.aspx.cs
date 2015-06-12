using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.Common.Function;
using System.Text;
using System.IO;

namespace Web.ResourceManage.scenic_spots
{
    public partial class Scenic_spotsEdit : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-26
        /// 说明：资源管理：景点： 添加，修改

        protected int listCount = 0;
        protected int SpotListCount = 0;
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        private IList<EyouSoft.Model.SourceStructure.MSpotPriceSystemModel> _setSpotList;
        private IList<EyouSoft.Model.SourceStructure.MSpotPriceSystemModel> SetSpotList
        {
            get { return _setSpotList; }
            set { this._setSpotList = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string id = Utils.GetQueryStringValue("id");
            string type = Utils.GetQueryStringValue("type");
            //权限验证
            PowerControl(doType);
            #region Ajax请求执行保存
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
                SetDataList();
            }
            else
            {
                GetSpotList();
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
            string star = string.Empty;
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceSpot model = bll.GetSpotModel(id);
                if (model != null)
                {
                    SpotListCount = model.PriceSystemList.Count;
                    if (model.PriceSystemList != null && model.PriceSystemList.Count > 0)
                    {
                        SetSpotList = model.PriceSystemList;
                    }
                    if (model.SourceModel != null)
                    {
                        this.txtPolicy.Text = model.SourceModel.UnitPolicy;
                        this.txtUnitName.Text = model.SourceModel.Name;
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodEnd);
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodStart);
                        this.txtUnitName.Text = model.SourceModel.Name;
                        this.txtLastDate.Text = UtilsCommons.GetDateString(model.SourceModel.LastModifyTime, ProviderToDate);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
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
                            this.txtContractNum.Visible = true;
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
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"ScenicEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                            }
                            this.lbFiles.Text = strFile.ToString();
                        }
                    }
                    if (model.LinkManList.Count > 0)
                    {
                        this.Contact1.SetTravelList = model.LinkManList;
                    }

                    IList<EyouSoft.Model.ComStructure.MComAttach> imglist = model.AttachList;
                    if (imglist != null && imglist.Count > 0)
                    {
                        listCount = imglist.Count;
                        this.rplimg.DataSource = imglist;
                        this.rplimg.DataBind();
                    }
                }
                else
                {
                    Utils.ResponseGoBack();
                }
            }
            this.txtLastHuman.Enabled = false;
            this.txtLastDate.Enabled = false;
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave(string doType, string id)
        {
            #region 获取表单值
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
            //政策
            string Policy = Utils.GetFormValue(this.txtPolicy.UniqueID);
            //合同开始时间
            string ContractStartDate = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同有到期时间
            string ContractDate = Utils.GetFormValue(this.txtContractDate.UniqueID);
            //景点名称
            string scenicName = Utils.GetFormValue(this.txtUnitName.UniqueID);
            //合同号
            string ContractNum = Utils.GetFormValue(this.txtContractNum.UniqueID);

            if (String.IsNullOrEmpty(scenicName))
            {
                msg = UtilsCommons.AjaxReturnJson("0", "单位名称不能为空");
                return msg;
            }
            #endregion

            //景点图片
            string[] imgUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldimgUpload = Utils.GetFormValues("hideimg");
            #region 景点图片上传
            IList<EyouSoft.Model.ComStructure.MComAttach> imglist = null;
            if (imgUpload.Length > 0)
            {
                imglist = new List<EyouSoft.Model.ComStructure.MComAttach>();
                for (int i = 0; i < imgUpload.Length; i++)
                {
                    if (imgUpload[i].Trim() != "")
                    {
                        if (imgUpload[i].Split('|').Length > 1)
                        {
                            EyouSoft.Model.ComStructure.MComAttach imgModel = new EyouSoft.Model.ComStructure.MComAttach();
                            imgModel.Downloads = 0;
                            imgModel.FilePath = imgUpload[i].Split('|')[1];
                            imgModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商;
                            imgModel.Name = imgUpload[i].Split('|')[0];
                            imgModel.Size = 0;
                            imglist.Add(imgModel);
                        }
                    }
                }
            }
            if (oldimgUpload.Length > 0)
            {
                if (imglist == null)
                {
                    imglist = new List<EyouSoft.Model.ComStructure.MComAttach>();
                }
                for (int i = 0; i < oldimgUpload.Length; i++)
                {
                    if (oldimgUpload[i].Trim() != "")
                    {
                        EyouSoft.Model.ComStructure.MComAttach imgModel = new EyouSoft.Model.ComStructure.MComAttach();
                        imgModel.FilePath = oldimgUpload[i].Split('|')[1];
                        imgModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商;
                        imgModel.Name = oldimgUpload[i].Split('|')[0];
                        imgModel.Size = 0;
                        imglist.Add(imgModel);
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

            #region 景区实体赋值
            EyouSoft.Model.SourceStructure.MSourceSpot Model = new EyouSoft.Model.SourceStructure.MSourceSpot();
            EyouSoft.Model.SourceStructure.MSource source = new EyouSoft.Model.SourceStructure.MSource();

            if (!string.IsNullOrEmpty(id))
            {
                //Model = new EyouSoft.BLL.SourceStructure.BSource().GetSpotModel(id);

                source.SourceId = id;
            }

            Model.AttachList = imglist;
            source.ContractAttach = visaModel;
            source.CountryId = Utils.GetInt(country);
            source.CityId = Utils.GetInt(city);
            source.ProvinceId = Utils.GetInt(provice);
            source.CountyId = Utils.GetInt(area);
            source.UnitPolicy = Policy;
            source.OperatorId = this.SiteUserInfo.UserId;
            source.DeptId = this.SiteUserInfo.DeptId;
            source.IssueTime = DateTime.Now;
            source.CompanyId = this.SiteUserInfo.CompanyId;
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
            source.Name = scenicName.Trim();
            source.ContractPeriodEnd = string.IsNullOrEmpty(ContractDate) ? null : (DateTime?)DateTime.Parse(ContractDate);
            source.ContractPeriodStart = string.IsNullOrEmpty(ContractStartDate) ? null : (DateTime?)DateTime.Parse(ContractStartDate);
            source.LastModifierId = this.SiteUserInfo.Name;
            source.LastModifyTime = DateTime.Now;
            //Model.Star = (EyouSoft.Model.EnumType.SourceStructure.SpotStar)int.Parse(ddlstar.SelectedValue);
            Model.SourceModel = source;
            Model.LinkManList = UtilsCommons.GetDataList();
            Model.PriceSystemList = GetSpotList();
            #endregion

            #region 执行保存（新增，修改）操作，并返回结果
            EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            int result = 0;
            if (t)
            {//新增
                result = bllsource.AddSpotModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "景点名称已存在");
                    return msg;
                }
                if (result == -2)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "该附件已存在");
                    return msg;
                }
            }
            else
            { //编辑
                result = bllsource.UpdateSpotModel(Model);
                if (result == -1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "景点名称已存在");
                    return msg;
                }
                if (result == -2)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "该附件已存在");
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
            #endregion
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(string dotype)
        {
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_新增))
                {
                    this.btnSave.Visible = false;
                }
            }
        }
        /// <summary>
        /// 获取控件的数据
        /// </summary>
        protected IList<EyouSoft.Model.SourceStructure.MSpotPriceSystemModel> GetSpotList()
        {
            //景点名称
            string[] SpotName = Utils.GetFormValues("SpotName");
            //景点星级
            string[] SpotStar = Utils.GetFormValues("SpotStar");
            //挂牌价格
            string[] PriceGP = Utils.GetFormValues("PriceGP");
            //散客价
            string[] PriceSK = Utils.GetFormValues("PriceSK");
            //团队价格
            string[] PriceTD = Utils.GetFormValues("PriceTD");
            //儿童价格
            string[] PriceRT = Utils.GetFormValues("PriceRT");
            //60到70岁老人价格
            string[] PriceLR1 = Utils.GetFormValues("PriceLR1");
            //70岁以上老人价格
            string[] PriceLR2 = Utils.GetFormValues("PriceLR2");
            //学生价
            string[] PriceXS = Utils.GetFormValues("PriceXS");
            //6军人价
            string[] PriceJR = Utils.GetFormValues("PriceJR");
            //景点描述
            string[] SpotDesc = Utils.GetFormValues("SpotDesc");
            string[] txtJingDianId = Utils.GetFormValues("txtJingDianId");
            string errorMsg = "";

            if (SpotName.Length > 0)
            {
                IList<EyouSoft.Model.SourceStructure.MSpotPriceSystemModel> list = new List<EyouSoft.Model.SourceStructure.MSpotPriceSystemModel>();
                for (int i = 0; i < SpotName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(SpotName[i].ToString()))
                    {
                        EyouSoft.Model.SourceStructure.MSpotPriceSystemModel model = new EyouSoft.Model.SourceStructure.MSpotPriceSystemModel();
                        if (SpotName[i].Trim() == "")
                            errorMsg += "第" + i.ToString() + "个的景点名称不能为空! &nbsp;";
                        else
                            model.SpotName = SpotName[i];//景区名称
                        model.PriceGP = Utils.GetDecimal(PriceGP[i]);
                        //网络价
                        model.PriceJR = Utils.GetDecimal(PriceJR[i]);
                        //散客价
                        model.PriceSK = Utils.GetDecimal(PriceSK[i]);
                        //淡季价格
                        model.PriceLR1 = Utils.GetDecimal(PriceLR1[i]);
                        //平季价格
                        model.PriceLR2 = Utils.GetDecimal(PriceLR2[i]);
                        //旺季价格
                        model.PriceRT = Utils.GetDecimal(PriceRT[i]);
                        model.PriceTD = Utils.GetDecimal(PriceTD[i]);
                        //用餐类型(早中晚)
                        model.PriceXS = Utils.GetDecimal(PriceXS[i]);
                        model.GuideWord = SpotDesc[i];
                        model.Star = (EyouSoft.Model.EnumType.SourceStructure.SpotStar)(Utils.GetInt(SpotStar[i].ToString()));
                        model.Id = txtJingDianId[i];
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
            this.rptList.DataSource = this.SetSpotList;
            this.rptList.DataBind();
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
        protected string GetPrice(object obj)
        {
            decimal price = Convert.ToDecimal(obj);
            return price == 0 ? "" : Utils.FilterEndOfTheZeroDecimal(price);
        }
    }
}
