using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Model.SourceStructure;
using System.Text;

namespace Web.ResourceManage.Cruise
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-28
    /// 说明：资源管理：游轮： 添加，修改
    public partial class CruiseEdit : BackPage
    {
        private IList<EyouSoft.Model.SourceStructure.MSourceSubShip> _setShipList;

        public IList<EyouSoft.Model.SourceStructure.MSourceSubShip> SetShipList
        {
            get { return _setShipList; }
            set { _setShipList = value; }
        }
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
            string type = Utils.GetQueryStringValue("type");
            //权限验证
            PowerControl(doType);
            #region  Ajax请求
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
                PageInit(doType, id);
                BindShipList();
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
        protected void PageInit(string dotype, string id)
        {
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.txtLastHuman.Text = this.SiteUserInfo.Name;
            this.txtLastHuman.Enabled = false;
            this.txtLastDate.Enabled = false;

            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceShip msource = BLL.GetShipModel(id);
                if (msource != null)
                {
                    if (msource.SourceModel != null)
                    {
                        Countryindex = msource.SourceModel.CountryId;
                        Provinceindex = msource.SourceModel.ProvinceId;
                        Cityindex = msource.SourceModel.CityId;
                        Areaindex = msource.SourceModel.CountyId;
                        this.txtCruiseCompany.Text = msource.SourceModel.Name;
                        this.txtaddress.Text = msource.SourceModel.Address;
                        this.txtUnitPolicy.Text = msource.SourceModel.UnitPolicy;
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(msource.SourceModel.ContractPeriodEnd);
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(msource.SourceModel.ContractPeriodStart);
                        Radio_hd_no.Checked = true;
                        if (msource.SourceModel.IsSignContract)
                        {
                            this.Radio_hd_yes.Checked = true;
                            this.Radio_hd_no.Checked = false;
                            this.txtContractNum.Text = msource.SourceModel.ContractCode;
                            this.txtContractNum.Visible = true;
                        }
                        radno.Checked = true;
                        if (msource.SourceModel.IsCommission)
                        {
                            radyes.Checked = true;
                            radno.Checked = false;
                        }
                        RadRecommend_no.Checked = true;
                        if (msource.SourceModel.IsRecommend)
                        {
                            RadRecommend_yes.Checked = true;
                            RadRecommend_no.Checked = false;
                        }
                        RadSign_no.Checked = true;
                        if (msource.SourceModel.IsPermission)
                        {
                            RadSign_yes.Checked = true;
                            RadSign_no.Checked = false;
                        }
                        this.txtLastDate.Text = UtilsCommons.GetDateString(msource.SourceModel.LastModifyTime, ProviderToDate);
                        this.txtLastHuman.Text = msource.SourceModel.LastModifierId;
                        if (msource.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (msource.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"CruiseEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", msource.SourceModel.ContractAttach.FilePath, msource.SourceModel.ContractAttach.Name);
                            }
                            this.lbFiles.Text = strFile.ToString();
                        }
                    }

                    if (msource.LinkManList != null && msource.LinkManList.Count > 0)
                    {
                        this.Contact1.SetTravelList = msource.LinkManList;
                    }
                    if (msource.SubShipList != null && msource.SubShipList.Count > 0)
                    {
                        this.SetShipList = msource.SubShipList;
                    }
                    this.txtRoutes.Text = msource.Routes;
                    this.txtCruiseTel.Text = msource.Telephone;
                    this.txtOwerRoutes.Text = msource.OwerRoutes;
                    this.txtstarttime.Text = msource.StartTime.ToString();
                    this.txtCruiseTel.Text = msource.Telephone;
                    this.txtendtime.Text = msource.EndTime.ToString();
                    this.txtPriceSystem.Text = msource.PriceSystem;
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
            //联系人
            string address = Utils.GetFormValue(this.txtaddress.UniqueID);
            //合同开始时间
            string ContractStartDate = Utils.GetFormValue(this.txtContractDate_Start.UniqueID);
            //合同有到期时间
            string ContractDate = Utils.GetFormValue(this.txtContractDate.UniqueID);
            string ContactNum = Utils.GetFormValue(this.txtContractNum.UniqueID);
            string PriceSystem = Utils.GetFormValue(this.txtPriceSystem.UniqueID);
            string UnitPolicy = Utils.GetFormValue(this.txtUnitPolicy.UniqueID);
            //轮船公司
            string CruiseCompany = Utils.GetFormValue(this.txtCruiseCompany.UniqueID);
            //船载电话
            string CruiseTel = Utils.GetFormValue(this.txtCruiseTel.UniqueID);
            //游轮景点
            string Scenic = Utils.GetFormValue(this.txtRoutes.UniqueID);
            //自费景点
            string OwerRoutes = Utils.GetFormValue(this.txtOwerRoutes.UniqueID);
            //开始时间
            string StarTime = Utils.GetFormValue(this.txtstarttime.UniqueID);
            //结束时间
            string EndTime = Utils.GetFormValue(this.txtendtime.UniqueID);
            if (String.IsNullOrEmpty(CruiseCompany))
            {
                msg = "{\"result\":\"0\",\"msg\":\"游轮公司不能为空!\"}";
                return msg;
            }

            EyouSoft.BLL.SourceStructure.BSource Bll = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MSourceShip model = new MSourceShip();
            EyouSoft.Model.SourceStructure.MSource mModel = new EyouSoft.Model.SourceStructure.MSource();
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> linkman = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            string userid = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                model = new EyouSoft.BLL.SourceStructure.BSource().GetShipModel(id);
                userid = model.SourceModel.UserId;
            }
            mModel.CityId = Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID));
            mModel.CountryId = Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID));
            mModel.CountyId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            mModel.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ddlProvince.UniqueID));
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
            mModel.Address = address;
            mModel.ContractPeriodStart = String.IsNullOrEmpty(ContractStartDate) ? null : (DateTime?)DateTime.Parse(ContractStartDate);
            mModel.ContractPeriodEnd = String.IsNullOrEmpty(ContractDate) ? null : (DateTime?)DateTime.Parse(ContractDate);
            mModel.ContractAttach = visaModel;
            model.PriceSystem = PriceSystem;
            mModel.UnitPolicy = UnitPolicy;
            mModel.LastModifierId = this.SiteUserInfo.Name;
            mModel.LastModifyTime = DateTime.Now;
            mModel.CompanyId = this.SiteUserInfo.CompanyId;
            mModel.OperatorId = this.SiteUserInfo.UserId;
            mModel.DeptId = this.SiteUserInfo.DeptId;
            mModel.IssueTime = DateTime.Now;
            mModel.IsCommission = true;
            //是否签单
            mModel.IsPermission = false;
            if (RadSign_yes.Checked)
            {
                mModel.IsPermission = true;
            }
            //是否返佣
            mModel.IsCommission = false;
            if (radyes.Checked)
            {
                mModel.IsCommission = true;
            }
            //是否推荐
            mModel.IsRecommend = false;
            if (RadRecommend_yes.Checked)
            {
                mModel.IsRecommend = true;
            }
            //是否签订合同
            mModel.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                mModel.IsSignContract = true;
                mModel.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }
            if (!String.IsNullOrEmpty(id))
                model.SourceModel.SourceId = id;
            model.OwerRoutes = OwerRoutes;
            model.Routes = Scenic;

            model.StartTime = string.IsNullOrEmpty(StarTime) ? null : (DateTime?)Convert.ToDateTime(StarTime);


            model.EndTime = string.IsNullOrEmpty(StarTime) ? null : (DateTime?)Convert.ToDateTime(StarTime);

            model.Telephone = CruiseTel;
            model.LinkManList = UtilsCommons.GetDataList();
            model.SubShipList = this.GetDataList();
            mModel.UserId = userid;
            mModel.Name = CruiseCompany.Trim();
            model.SourceModel = mModel;

            int result = 0;
            if (t)
            {//新增
                result = Bll.AddShipModel(model);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"游轮名称已存在!\"}";
                    return msg;
                }
            }
            else
            { //编辑
                result = Bll.UpdateShipModel(model);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"游轮名称已存在!\"}";
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_新增))
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
        /// <summary>
        /// 获取控件的数据
        /// </summary>
        protected IList<EyouSoft.Model.SourceStructure.MSourceSubShip> GetDataList()
        {
            //船名
            string[] ShipName = Utils.GetFormValues("txtShipName");
            //星级
            string[] ShipStar = Utils.GetFormValues("selShipStar");
            //仓位数
            string[] ShipSpace = Utils.GetFormValues("txtShipSpace");
            //舰载电话
            string[] ContactTel = Utils.GetFormValues("txtContactTel");
            //联系人
            string[] ContactName = Utils.GetFormValues("txtContactName");
            //航线
            string[] ShipRoute = Utils.GetFormValues("txtShipRoute");
            //船图片附件
            string[] fileImg = Utils.GetFormValues("hide_Cruiseimg_file");

            string[] txtYouLunId = Utils.GetFormValues("txtYouLunId");

            string errorMsg = "";


            if (ShipName.Length > 0)
            {
                IList<EyouSoft.Model.SourceStructure.MSourceSubShip> list = new List<EyouSoft.Model.SourceStructure.MSourceSubShip>();
                EyouSoft.Model.ComStructure.MComAttach attach = new EyouSoft.Model.ComStructure.MComAttach();
                for (int i = 0; i < ShipName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ShipName[i].ToString()))
                    {
                        EyouSoft.Model.SourceStructure.MSourceSubShip model = new EyouSoft.Model.SourceStructure.MSourceSubShip();
                        if (ShipName[i].Trim() == "")
                            errorMsg += "第" + i.ToString() + "个的船名不能为空! &nbsp;";
                        else
                            model.ShipName = ShipName[i];//船名
                        if (fileImg[i].Split('|').Length > 1)
                        {
                            model.AttachModel = new EyouSoft.Model.ComStructure.MComAttach { FilePath = fileImg[i].Split('|')[1].ToString(), Name = fileImg[i].Split('|')[0].ToString() };
                        }
                        else
                        {
                            model.AttachModel = new EyouSoft.Model.ComStructure.MComAttach { FilePath = "", Name = "" };
                        }
                        model.ContactName = ContactName[i];
                        model.ShipRoute = ShipRoute[i];
                        model.ShipSpace = Utils.GetInt(ShipSpace[i].ToString());
                        model.ShipStar = (EyouSoft.Model.EnumType.SourceStructure.ShipStar)(Utils.GetInt(ShipStar[i].ToString()));
                        model.Telephone = ContactTel[i];
                        model.SubId = txtYouLunId[i];
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
        private void BindShipList()
        {
            this.rptList.DataSource = this.SetShipList;
            this.rptList.DataBind();
        }
    }
}
