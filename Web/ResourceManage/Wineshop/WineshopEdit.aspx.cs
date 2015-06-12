using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Common.Function;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace Web.ResourceManage.Wineshop
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-28
    /// 说明：资源管理：酒店： 添加，修改
    public partial class WineshopEdit : BackPage
    {
        protected int Countryindex, Provinceindex, Cityindex, Areaindex = 0;
        /// <summary>
        /// 存放价格列表数量
        /// </summary>
        protected int SetListCount = 0;
        /// <summary>
        /// 存放用餐类型
        /// </summary>
        protected string dinnertype = string.Empty;

        protected int listCount = 0;
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        private IList<EyouSoft.Model.SourceStructure.MSourceHotelRoom> _setHotelList;

        private IList<EyouSoft.Model.SourceStructure.MSourceHotelRoom> SetHotelList
        {
            get { return _setHotelList; }
            set { _setHotelList = value; }
        }
        protected string HotelImg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
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
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id, doType);
                SetDataList();
            }
            else
                GetDataList();

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string dotype)
        {
            string star = string.Empty;
            this.txtaddress.Text = "";
            this.txtHotelDesc.Text = "";
            this.txtRemarks.Text = "";
            this.txtHotelName.Text = "";
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.UploadControl2.CompanyID = this.SiteUserInfo.CompanyId;
            this.txtLastHuman.Text = this.SiteUserInfo.Name;
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MSourceHotel model = BLL.GetOneHotelModel(id);
                if (model != null)
                {
                    if (model.SourceModel != null)
                    {
                        this.txtaddress.Text = model.SourceModel.Address;
                        this.txtHotelDesc.Text = model.SourceModel.Desc;
                        this.txtHotelName.Text = model.SourceModel.Name;
                        this.txtRemarks.Text = model.SourceModel.Remark;
                        this.txtContractDate_Start.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodStart);
                        this.txtContractDate.Text = UtilsCommons.SetDateTimeFormart(model.SourceModel.ContractPeriodEnd);
                        this.txtLastHuman.Text = model.SourceModel.LastModifierId;
                        this.txtLastDate.Text = UtilsCommons.GetDateString(model.SourceModel.LastModifyTime, ProviderToDate);
                        if (model.SourceModel.ContractAttach != null)
                        {
                            StringBuilder strFile = new StringBuilder();
                            if (model.SourceModel.ContractAttach.FilePath != "")
                            {
                                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"Controlwine.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", model.SourceModel.ContractAttach.FilePath, model.SourceModel.ContractAttach.Name);
                            }
                            this.lbFiles.Text = strFile.ToString();
                        }
                        star = Convert.ToString((int)model.Star);
                        //签单
                        radno.Checked = true;
                        if (model.SourceModel.IsPermission)
                        {
                            radyes.Checked = true;
                            radno.Checked = false;
                        }
                        //返佣
                        RadioButton2.Checked = true;
                        if (model.SourceModel.IsCommission)
                        {
                            RadioButton1.Checked = true;
                            RadioButton2.Checked = false;
                        }
                        //推荐
                        radRecommendno.Checked = true;
                        if (model.SourceModel.IsRecommend)
                        {
                            radRecommendyes.Checked = true;
                            radRecommendno.Checked = false;
                        }
                        //合同
                        Radio_hd_no.Checked = true;
                        if (model.SourceModel.IsSignContract)
                        {
                            Radio_hd_yes.Checked = true;
                            Radio_hd_no.Checked = false;
                            this.txtContractNum.Visible = true;
                            this.txtContractNum.Text = model.SourceModel.ContractCode;
                        }

                        Countryindex = model.SourceModel.CountryId;
                        Provinceindex = model.SourceModel.ProvinceId;
                        Cityindex = model.SourceModel.CityId;
                        Areaindex = model.SourceModel.CountyId;
                    }
                    this.txtTel.Text = model.ReceptionTel;
                    if (model.LinkManList.Count > 0)
                        this.Contact1.SetTravelList = model.LinkManList;
                    if (model.HotelRoomList.Count > 0)
                        SetHotelList = model.HotelRoomList;
                    SetListCount = SetHotelList.Count;
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
            
            this.txtLastDate.Enabled = false;
            this.txtLastHuman.Enabled = false;
            this.litStar.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.HotelStar)), star, false);
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave(string doType, string id)
        {
            string msg = string.Empty;
            EyouSoft.BLL.SourceStructure.BSource Bll = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MSourceHotel modelHotel = new EyouSoft.Model.SourceStructure.MSourceHotel();
            EyouSoft.Model.SourceStructure.MSource Msource = new EyouSoft.Model.SourceStructure.MSource();
            modelHotel.SourceModel = new EyouSoft.Model.SourceStructure.MSource();
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            if (!string.IsNullOrEmpty(id))
            {
                modelHotel = new EyouSoft.BLL.SourceStructure.BSource().GetOneHotelModel(id);
            }
            //酒店图片
            string[] imgUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldimgUpload = Utils.GetFormValues("hideimg");
            #region 酒店图片上传
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
            modelHotel.AttachList = imglist;
            #endregion

            //合同附件
            //合同附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl2.ClientHideID);
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

            Msource.ContractAttach = visaModel;
            #endregion



            //if (!String.IsNullOrEmpty(newFilePath))
            //{
            //    modelHotel.SourceModel.ContractAttach = new MComAttach() { FilePath = newFilePath, Name = newFileName };
            //}
            //else
            //{
            //    if (String.IsNullOrEmpty(this.Agreement.Value))
            //    {
            //        modelHotel.SourceModel.ContractAttach = new MComAttach() { FilePath = "", Name = "" };
            //    }
            //}
            Msource.ContractPeriodStart = String.IsNullOrEmpty(Utils.GetFormValue(this.txtContractDate_Start.UniqueID)) ? null : (DateTime?)DateTime.Parse(Utils.GetFormValue(this.txtContractDate_Start.UniqueID));
            Msource.ContractPeriodEnd = String.IsNullOrEmpty(Utils.GetFormValue(this.txtContractDate.UniqueID)) ? null : (DateTime?)DateTime.Parse(Utils.GetFormValue(this.txtContractDate.UniqueID));

            modelHotel.ReceptionTel = Utils.GetFormValue(txtTel.UniqueID);
            modelHotel.AttachList = imglist;
            modelHotel.HotelRoomList = GetDataList();
            modelHotel.LinkManList = UtilsCommons.GetDataList();
            //是否签单
            Msource.IsPermission = false;
            if (radyes.Checked)
            {
                Msource.IsPermission = true;
            }
            Msource.CompanyId = this.SiteUserInfo.CompanyId;

            Msource.IssueTime = DateTime.Now;
            //是否返佣
            Msource.IsCommission = false;
            if (RadioButton1.Checked)
            {
                Msource.IsCommission = true;
            }
            //是否推荐
            Msource.IsRecommend = false;
            if (radRecommendyes.Checked)
            {
                Msource.IsRecommend = true;
            }
            //是否签订合同
            Msource.IsSignContract = false;
            if (Radio_hd_yes.Checked)
            {
                Msource.IsSignContract = true;
                Msource.ContractCode = Utils.GetFormValue(this.txtContractNum.UniqueID);
            }
            Msource.LastModifierId = this.SiteUserInfo.Name;
            Msource.LastModifyTime = DateTime.Now;
            Msource.Address = Utils.GetFormValue(this.txtaddress.UniqueID);
            Msource.Desc = txtHotelDesc.Text;
            Msource.CountryId = Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID));
            Msource.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ddlProvice.UniqueID));
            Msource.CityId = Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID));
            Msource.CountyId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            Msource.Name = Utils.GetFormValue(txtHotelName.UniqueID).Trim();
            Msource.Remark = Utils.GetFormValue(txtRemarks.UniqueID);
            Msource.OperatorId = this.SiteUserInfo.UserId;
            modelHotel.Star = (EyouSoft.Model.EnumType.SourceStructure.HotelStar)Utils.GetInt(Utils.GetFormValue(this.StarValue.UniqueID));
            if (!String.IsNullOrEmpty(id))
                modelHotel.SourceId = id;
            modelHotel.SourceModel = Msource;
            int result = 0;
            if (t)
            {//新增
                result = Bll.AddHotelModel(modelHotel);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"酒店名称已存在!\"}";
                    return msg;
                }
                if (result == -2)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"该酒店已经存在相同的酒店房型!\"}";
                    return msg;
                }
                if (result == -3)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"已经存在相同的附件!\"}";
                    return msg;
                }
            }
            else
            { //编辑
                result = Bll.UpdateHotelModel(modelHotel);
                if (result == -1)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"酒店名称已存在!\"}";
                    return msg;
                }
                if (result == -2)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"该酒店已经存在相同的酒店房型!\"}";
                    return msg;
                }
                if (result == -3)
                {
                    msg = "{\"result\":\"0\",\"msg\":\"已经存在相同的附件!\"}";
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
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_修改))
                {
                    this.btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增))
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
        protected IList<EyouSoft.Model.SourceStructure.MSourceHotelRoom> GetDataList()
        {
            //房型
            string[] TypeName = Utils.GetFormValues("TypeName");
            //前台销售价
            string[] PriceQT = Utils.GetFormValues("PriceQT");
            //网络价
            string[] PriceWL = Utils.GetFormValues("PriceWL");
            //散客价
            string[] PriceSK = Utils.GetFormValues("PriceSK");
            //淡季价格
            string[] PriceDJ = Utils.GetFormValues("PriceDJ");
            //旺季价格
            string[] PriceWJ = Utils.GetFormValues("PriceWJ");
            //平季价格
            string[] PricePJ = Utils.GetFormValues("PricePJ");

            string[] DiningType = Utils.GetFormValues("IsBreakFast");

            string[] txtRoomId = Utils.GetFormValues("txtRoomId");

            string errorMsg = "";

            if (TypeName.Length > 0 && PriceQT.Length > 0 && PriceWL.Length > 0 && PriceSK.Length > 0 && PriceDJ.Length > 0 && PriceWJ.Length > 0 && PricePJ.Length > 0 && DiningType.Length > 0)
            {
                IList<EyouSoft.Model.SourceStructure.MSourceHotelRoom> list = new List<EyouSoft.Model.SourceStructure.MSourceHotelRoom>();
                for (int i = 0; i < TypeName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(TypeName[i].ToString()))
                    {
                        EyouSoft.Model.SourceStructure.MSourceHotelRoom model = new EyouSoft.Model.SourceStructure.MSourceHotelRoom();
                        if (TypeName[i].Trim() == "")
                            errorMsg += "第" + i.ToString() + "的房型不能为空! &nbsp;";
                        else
                            model.TypeName = TypeName[i];//房型
                        model.PriceQT = Utils.GetDecimal(PriceQT[i]);
                        //网络价
                        model.PriceWL = Utils.GetDecimal(PriceWL[i]);
                        //散客价
                        model.PriceSK = Utils.GetDecimal(PriceSK[i]);
                        //淡季价格
                        model.PriceDJ = Utils.GetDecimal(PriceDJ[i]);
                        //平季价格
                        model.PricePJ = Utils.GetDecimal(PricePJ[i]);
                        //旺季价格
                        model.PriceWJ = Utils.GetDecimal(PriceWJ[i]);
                        //用餐类型(早中晚)
                        model.IsBreakfast = DiningType[i].ToString() == "0" ? false : true;
                        model.RoomId = txtRoomId[i];
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
            this.rptList.DataSource = this.SetHotelList;
            this.rptList.DataBind();
        }
        protected string GetPrice(object obj)
        {
            decimal price = Convert.ToDecimal(obj);
            return price == 0 ? "" : Utils.FilterEndOfTheZeroDecimal(price);
        }
    }
}
