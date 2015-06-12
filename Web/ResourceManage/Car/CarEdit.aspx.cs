using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Common.Function;
using System.Text.RegularExpressions;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace Web.ResourceManage.Car
{
    public partial class CarEidt : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：刘飞
        /// 创建时间：2011-9-26
        /// 说明：资源管理：车队： 添加，修改



        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        protected int SetListCount = 0;

        ///  /// <summary>
        /// 设置控件的数据源
        /// </summary>
        private IList<EyouSoft.Model.SourceStructure.MSourceCar> _setTravelList;

        private IList<EyouSoft.Model.SourceStructure.MSourceCar> SetTravelList
        {
            get { return _setTravelList; }
            set { _setTravelList = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //操作类型
            string doType = Utils.GetQueryStringValue("doType");
            string type = Utils.GetQueryStringValue("type");
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
            this.txtLastHuman.Text = this.SiteUserInfo.Name;
            this.txtLastDate.Enabled = false;
            this.txtLastHuman.Enabled = false;

            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceMotorcade Model = BLL.GetMotorcadeModel(id);
                if (Model != null)
                {
                    Countryindex = Model.SourceModel.CountryId;
                    Provinceindex = Model.SourceModel.ProvinceId;
                    Cityindex = Model.SourceModel.CityId;
                    Areaindex = Model.SourceModel.CountyId;
                    this.txtRemark.Text = Model.SourceModel.Remark;
                    this.txtteamName.Text = Model.SourceModel.Name;
                    this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(Model.SourceModel.ContractPeriodEnd);
                    this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(Model.SourceModel.ContractPeriodStart);

                    this.txtLastHuman.Text = Model.SourceModel.LastModifierId;
                    this.txtLastDate.Text = UtilsCommons.GetDateString(Model.SourceModel.LastModifyTime, ProviderToDate);
                    if (Model.SourceModel != null)
                    {
                        this.RadioButton2.Checked = true;
                        if (Model.SourceModel.IsCommission)
                        {
                            this.RadioButton1.Checked = true;
                            this.RadioButton2.Checked = false;
                        }
                        radRecommendno.Checked = true;
                        if (Model.SourceModel.IsRecommend)
                        {
                            radRecommendyes.Checked = true;
                            radRecommendno.Checked = false;
                        }
                        radno.Checked = true;
                        if (Model.SourceModel.IsPermission)
                        {
                            radyes.Checked = true;
                            radno.Checked = false;
                        }
                        Radio_hd_no.Checked = true;
                        if (Model.SourceModel.IsSignContract)
                        {
                            this.Radio_hd_yes.Checked = true;
                            this.Radio_hd_no.Checked = false;
                            this.txtContractNum.Visible = true;
                            this.txtContractNum.Text = Model.SourceModel.ContractCode;
                        }
                        if (Model.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (Model.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"CarEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", Model.SourceModel.ContractAttach.FilePath, Model.SourceModel.ContractAttach.Name);
                            }
                            this.lbFiles.Text = strFile.ToString();
                        }
                    }
                    if (Model.LinkManList.Count > 0)
                    {
                        //获取联系人信息
                        this.Contact1.SetTravelList = Model.LinkManList;
                    }
                    if (Model.CarList.Count > 0)
                    {
                        //获取联系人信息
                        this.SetTravelList = Model.CarList;
                    }
                    SetListCount = SetTravelList.Count;
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
            if (String.IsNullOrEmpty(Utils.GetFormValue(txtteamName.UniqueID)))
            {
                msg = "{\"result\":\"0\",\"msg\":\"车队名称不能为空!\"}";
                return msg;
            }
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;

            EyouSoft.BLL.SourceStructure.BSource Bll = new EyouSoft.BLL.SourceStructure.BSource();
            MSourceMotorcade model = new MSourceMotorcade();
            EyouSoft.Model.SourceStructure.MSource mModel = new EyouSoft.Model.SourceStructure.MSource();
            IList<EyouSoft.Model.SourceStructure.MSourceCar> carlist = GetDataList();
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("id")))
            {
                model = new EyouSoft.BLL.SourceStructure.BSource().GetMotorcadeModel(Utils.GetQueryStringValue("id"));
            }
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

            mModel.ContractAttach = visaModel;
            mModel.Name = Utils.GetFormValue(txtteamName.UniqueID).Trim();
            mModel.Remark = Utils.GetFormValue(txtRemark.UniqueID);
            mModel.Address = Utils.GetFormValue(this.address.UniqueID);
            model.SourceModel = mModel;
            model.SourceModel.CountryId = Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID));
            model.SourceModel.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ddlProvice.UniqueID));
            model.SourceModel.CityId = Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID));
            model.SourceModel.CountyId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            model.SourceModel.LastModifierId = this.SiteUserInfo.Name;
            model.SourceModel.LastModifyTime = DateTime.Now;
            model.SourceModel.ContractPeriodStart = EyouSoft.Common.Utils.GetDateTimeNullable(Utils.GetFormValue(txtContractDate_Start.UniqueID));
            model.SourceModel.ContractPeriodEnd = EyouSoft.Common.Utils.GetDateTimeNullable(Utils.GetFormValue(txtContractDate.UniqueID));
            //获取车辆信息 
            model.CarList = carlist;
            //获取联系人信息
            model.LinkManList = UtilsCommons.GetDataList();
            model.SourceModel.CompanyId = this.SiteUserInfo.CompanyId;
            model.SourceModel.IssueTime = DateTime.Now;
            if (!String.IsNullOrEmpty(id))
                model.SourceModel.SourceId = id;
            //是否返佣
            model.SourceModel.IsCommission = false;
            if (this.RadioButton1.Checked)
            {
                model.SourceModel.IsCommission = true;
            }
            //是否签单
            model.SourceModel.IsPermission = false;
            if (this.radyes.Checked)
            {
                model.SourceModel.IsPermission = true;
            }
            //是否推荐
            model.SourceModel.IsRecommend = false;
            if (radRecommendyes.Checked)
            {
                model.SourceModel.IsRecommend = true;
            }
            //是否签订合同
            model.SourceModel.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                model.SourceModel.IsSignContract = true;
                model.SourceModel.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }

            model.SourceModel.OperatorId = this.SiteUserInfo.UserId;
            int result = 0;
            if (t)
            {//新增
                result = Bll.AddMotorcadeModel(model);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"车辆名已存在!\"}";
                    return msg;
                }
            }
            else
            { //编辑
                result = Bll.UpdateMotorcadeModel(model);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"车辆名已存在!\"}";
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_新增))
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
        /// 获取车辆信息
        /// </summary>
        protected IList<EyouSoft.Model.SourceStructure.MSourceCar> GetDataList()
        {
            //车型
            string[] TypeName = Utils.GetFormValues("TypeName");
            //座位数
            string[] SeatCount = Utils.GetFormValues("SeatCount");
            //车号
            string[] CarNumber = Utils.GetFormValues("CarNumber");
            //司机
            string[] Driver = Utils.GetFormValues("Driver");
            //司机电话
            string[] DriverTel = Utils.GetFormValues("DriverTel");
            //保险金额
            string[] InsAmount = Utils.GetFormValues("InsAmount");
            //保险期限
            string[] Timelimit = Utils.GetFormValues("Timelimit");
            //是否保险
            string[] IsInsurance = Utils.GetFormValues("IsInsurance");
            string[] txtCheXingId = Utils.GetFormValues("txtCheXingId");

            string errMsg = "";

            if (TypeName.Length > 0)
            {
                IList<EyouSoft.Model.SourceStructure.MSourceCar> list = new List<EyouSoft.Model.SourceStructure.MSourceCar>();
                for (int i = 0; i < TypeName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(TypeName[i].ToString()))
                    {
                        MSourceCar model = new MSourceCar();
                        if (TypeName[i].Trim() == "")
                            errMsg += "第" + i.ToString() + "车辆不能为空! &nbsp;";
                        else
                            model.TypeName = TypeName[i];//姓名
                        model.CarNumber = CarNumber[i];
                        //司机
                        model.Driver = Driver[i];
                        //司机电话
                        model.DriverTel = DriverTel[i];
                        model.IsInsurance = Convert.ToBoolean(int.Parse(IsInsurance[i].ToString() == "" ? "1" : IsInsurance[i].ToString()));
                        //保险金额
                        model.InsuranceAmount = Utils.GetDecimal(InsAmount[i]);
                        //保险期限
                        model.InsuranceLimit = Utils.GetDateTimeNullable(Timelimit[i]);

                        //座位数
                        model.SeatNumber = Utils.GetInt(SeatCount[i]);
                        model.CarId = txtCheXingId[i];
                        list.Add(model);
                    }
                }
                if (errMsg != "")
                {
                    MessageBox.Show(this, errMsg);
                    return null;
                }
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
            this.rptListcar.DataSource = this.SetTravelList;
            this.rptListcar.DataBind();
        }

    }
}
