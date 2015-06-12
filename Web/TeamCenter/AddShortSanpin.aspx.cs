using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using System.Text;

namespace EyouSoft.Web.TeamCenter
{
    public partial class AddShortSanpin : BackPage
    {
        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;
        protected string act = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            act = Utils.GetQueryStringValue("act");
            //权限验证
            PowerControl();

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                Response.Clear();
                switch (doType)
                {
                    case "save":
                        Response.Write(PageSave());
                        break;
                }
                Response.End();
            }
            #endregion

            #region 用户控件初始化

            this.PriceStand1.CompanyID = SiteUserInfo.CompanyId;
            this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
            this.UploadControl1.IsUploadMore = true;
            this.UploadControl1.IsUploadSelf = true;
            this.UploadControl2.CompanyID = SiteUserInfo.CompanyId;
            this.UploadControl2.IsUploadMore = false;
            this.UploadControl2.IsUploadSelf = true;
            this.CostAccounting1.IsSanPin = true;
            this.PriceStand1.InitTour = false;
            #endregion

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                if (type != 3)
                {
                    this.phdVisaFile.Visible = false;
                }
                //根据ID初始化页面
                if (id != "" && act != "add")
                {
                    if (act == "update")
                    {
                        this.phdSelectDate.Visible = false;
                    }
                    PageInit(id);
                }
                else
                {
                    //按钮权限控制
                    BtnPowerControl();
                    BindAreaList(0);
                    this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                    this.SellsSelect1.SellsName = SiteUserInfo.Name;
                    //预设车型
                    this.PresetBusType1.ContrloModel = act;
                }
            }
        }

        #region 初始化线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindAreaList(int selectIndex)
        {

            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(SiteUserInfo.CompanyId);
            sb.Append("<option value=\"0\">-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                string type = string.Empty;
                for (int i = 0; i < list.Count; i++)
                {
                    switch (list[i].Type)
                    {
                        case EyouSoft.Model.EnumType.ComStructure.AreaType.国内线: type = "0";
                            break;
                        case EyouSoft.Model.EnumType.ComStructure.AreaType.省内线: type = "1";
                            break;
                        case EyouSoft.Model.EnumType.ComStructure.AreaType.出境线: type = "2";
                            break;
                    }
                    if (list[i].AreaId != selectIndex)
                    {
                        sb.Append("<option data-type='" + type + "' value=\"" + list[i].AreaId + "\">" + list[i].AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option data-type='" + type + "' value=\"" + list[i].AreaId + "\" selected=\"selected\">" + list[i].AreaName + "</option>");
                    }
                }
            }
            this.litArea.Text = sb.ToString();
            EyouSoft.Model.ComStructure.MComSetting comSettingModel = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (comSettingModel != null)
            {
                this.hideSysStopCount.Value = "" + comSettingModel.CountryArea.ToString() + "," + comSettingModel.ProvinceArea.ToString() + "," + comSettingModel.ExitArea.ToString() + "";
            }
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 用户控件初始化

            #endregion
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();

            EyouSoft.Model.TourStructure.MTourSanPinInfo model = (EyouSoft.Model.TourStructure.MTourSanPinInfo)bll.GetTourInfo(id);
            if (model != null)
            {
                #region 表单控件赋值
                this.lblLeaveDateMore.Text = "出团日期:";
                BindAreaList(model.AreaId);
                this.cbxDistribution.Checked = model.IsShowDistribution;
                this.hideRouteID.Value = model.RouteId;
                this.txt_RouteName.Text = model.RouteName;
                this.txt_Days.Text = model.TourDays.ToString();
                this.txtPeopleCount.Text = model.PlanPeopleNumber.ToString();
                this.lblLeaveDate.Text = UtilsCommons.GetDateString(model.LDate, this.ProviderToDate);
                this.hideLeaveDate.Value = model.LDate.HasValue ? model.LDate.Value.ToString("yyyy-MM-dd") : "";
                this.txtSuccesssStraffBegin.Text = model.LTraffic;
                this.txtSuccesssStraffEnd.Text = model.RTraffic;
                this.txtSuccessGather.Text = model.Gather;
                this.txtStopDate.Text = model.StopDays.ToString();

                if (act == "copy")
                {
                    this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                    this.SellsSelect1.SellsName = SiteUserInfo.Name;
                }
                else
                {
                    this.SellsSelect1.SellsID = model.SaleInfo.SellerId;
                    this.SellsSelect1.SellsName = model.SaleInfo.Name;
                }
                this.txtPlanContent.Text = model.PlanFeature;
                this.txtSearchKey.Text = model.KeyName;
                // 报价标准
                this.PriceStand1.InitMode = false;
                this.PriceStand1.SetPriceStandard = model.MTourPriceStandard;
                //行程安排
                this.Journey1.SetPlanList = model.TourPlan;
                //散拼 服务
                if (model.TourService != null)
                {
                    CostAccounting1.ServiceStandard = model.TourService.ServiceStandard;
                    CostAccounting1.NoNeedItem = model.TourService.NoNeedItem;
                    CostAccounting1.ShoppingItem = model.TourService.ShoppingItem;
                    CostAccounting1.ChildServiceItem = model.TourService.ChildServiceItem;
                    CostAccounting1.OwnExpense = model.TourService.OwnExpense;
                    CostAccounting1.NeedAttention = model.TourService.NeedAttention;
                    CostAccounting1.WarmRemind = model.TourService.WarmRemind;
                    CostAccounting1.InsiderInfor = model.TourService.InsiderInfor;
                }
                //上车地点设置
                if (model.TourCarLocation != null && model.TourCarLocation.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    string strId = string.Empty, strText = string.Empty, strPriceJ = string.Empty,
                        strPriceS = string.Empty, strDesc = string.Empty, strPickUpTourId = string.Empty;
                    foreach (var item in model.TourCarLocation)
                    {
                        sb.AppendFormat("<li><a href='javascript:void(0);' onclick='AddSanPlan.RemoveLi(this);return false;' data-isExist='{2}' class='car-close' data-id='{0}'></a><span>{1}</span></li>", item.CarLocationId, item.Location, item.isTourOrderExists ? 1 : 0);
                        strPickUpTourId += item.TourLocationId + ",";
                        strId += item.CarLocationId + ",";
                        strText += item.Location + ",";
                        strPriceJ += item.OffPrice.ToString() + ",";
                        strPriceS += item.OnPrice.ToString() + ",";
                        strDesc += item.Desc + "&&&";
                    }
                    this.PickUpPoint1.PickUpOptions = sb.ToString();
                    this.PickUpPoint1.PickUpTourId = model.TourId;
                    this.PickUpPoint1.PickUpID = strId.Substring(0, strId.Length - 1);
                    this.PickUpPoint1.PickUpText = strText.Substring(0, strText.Length - 1);
                    this.PickUpPoint1.PickUpPriceJ = strPriceJ.Substring(0, strPriceJ.Length - 1);
                    this.PickUpPoint1.PickUpPriceS = strPriceS.Substring(0, strPriceS.Length - 1);
                    this.PickUpPoint1.PickUpDesc = strDesc.Substring(0, strDesc.Length - 3);
                }
                //预设车型
                this.PresetBusType1.TourId = model.TourId;
                this.PresetBusType1.ContrloModel = act;
                if (model.TourCarType != null && model.TourCarType.Count > 0)
                {
                    this.PresetBusType1.PreSetTypeList = model.TourCarType;
                }
                else 
                {
                    this.DivPrisetBus.Visible = false;
                }
                #endregion

                #region 附件
                //附件
                if (model.FilePath.Trim() != "")
                {
                    this.lblFiles.Text = "<span class='upload_filename'>&nbsp;<a href='" + model.FilePath + "' target='_blank'>查看附件</a><a href='javascript:void(0);' onclick='AddSanPlan.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideOtherFile' value='|" + model.FilePath + "'></span>";
                }

                //签证附件
                if (model.VisaFileList != null && model.VisaFileList.Count > 0)
                {
                    string visaStr = string.Empty;
                    for (int i = 0; i < model.VisaFileList.Count; i++)
                    {

                        visaStr += "<span class='upload_filename'>&nbsp;<a href='" + model.VisaFileList[i].FilePath + "' target='_blank'>" + model.VisaFileList[i].Name + "</a><a href='javascript:void(0);' onclick='AddSanPlan.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideVisaFile' value='" + model.VisaFileList[i].Name + "|" + model.VisaFileList[i].FilePath + "|" + model.VisaFileList[i].Downloads.ToString() + "'></span>";
                    }
                    this.lblVisaFiles.Text = visaStr;
                }
                #endregion

                #region 修改控制
                this.phdSave.Visible = false;
                if (model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕
                    )
                {
                    //供应商发布的散拼计划不能修改
                    if (model.SourceId.Trim() != "")
                    {
                        this.phdSave.Visible = false;
                    }
                    else
                    {
                        this.phdSave.Visible = true;
                    }
                }

                if (act == "copy")
                {
                    this.phdSave.Visible = true;
                }

                //按钮权限控制
                BtnPowerControl();

                //是否可以操作该数据
                if (!SiteUserInfo.IsHandleElse && act != "copy")
                {
                    if (model.OperatorInfo != null && model.OperatorInfo.OperatorId != SiteUserInfo.UserId && model.SaleInfo != null && model.SaleInfo.SellerId != SiteUserInfo.UserId)
                    {
                        this.phdSave.Visible = false;
                    }
                }
                #endregion

                #region 变更控制
                if (model.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划)
                {
                    this.hideIsChangeInput.Value = "true";
                }
                else
                {
                    this.hideIsChangeInput.Value = "false";
                }
                #endregion
            }
            else
            {
                Utils.ResponseGoBack();
            }
        }
        #endregion

        #region 保存，修改表单
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave()
        {
            string msg = string.Empty;

            #region 获取表单
            //线路区域编号
            int areaID = Utils.GetInt(Utils.GetFormValue("sltArea"));
            //线路编号
            string routeID = Utils.GetFormValue(this.hideRouteID.UniqueID);
            //线路名称
            string routeName = Utils.GetFormValue(this.txt_RouteName.UniqueID);
            //天数
            int days = Utils.GetInt(Utils.GetFormValue(this.txt_Days.UniqueID));
            //销售员编号
            string sellsID = Utils.GetFormValue(this.SellsSelect1.SellsIDClient);
            //销售员名称
            string sellsName = Utils.GetFormValue(this.SellsSelect1.SellsNameClient);
            //报价员 取当前登录用户
            EyouSoft.Model.TourStructure.MOperatorInfo operatorModel = new EyouSoft.Model.TourStructure.MOperatorInfo();
            operatorModel.OperatorId = this.SiteUserInfo.UserId;
            operatorModel.Name = this.SiteUserInfo.Name;
            operatorModel.Phone = this.SiteUserInfo.Telephone;
            //出发交通
            string successsStraffBegin = Utils.GetFormValue(this.txtSuccesssStraffBegin.UniqueID);
            //返回交通
            string successsStraffEnd = Utils.GetFormValue(this.txtSuccesssStraffEnd.UniqueID);
            //集合方式
            string successGather = Utils.GetFormValue(this.txtSuccessGather.UniqueID);
            //出团时间
            string[] successDateBegin = Utils.GetFormValue(this.hideLeaveDate.UniqueID).Split(',');
            //预控人数
            int planPeopleNumber = Utils.GetInt(Utils.GetFormValue(this.txtPeopleCount.UniqueID));
            //行程特色
            string planContent = Utils.EditInputText(Utils.GetFormValue(this.txtPlanContent.UniqueID));
            //停收时间
            int stopDate = Utils.GetInt(Utils.GetFormValue(this.txtStopDate.UniqueID));
            //是否同业分销
            bool isShowDistribution = Utils.GetFormValue(this.cbxDistribution.UniqueID) == "on" ? true : false;
            //变更标题
            string changeTitle = Utils.GetFormValue("txt_ChangeTitle");
            //变更备注     
            string changeRemark = Utils.GetFormValue("txt_ChangeRemark");
            //附件
            string filsPath = Utils.GetFormValue(this.UploadControl2.ClientHideID);
            if (filsPath == "")
            {
                filsPath = Utils.GetFormValue("hideOtherFile");
            }
            //签证附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideVisaFile");
            //关键字
            string searchKey = Utils.GetFormValue(this.txtSearchKey.UniqueID);
            #endregion

            #region 表单后台验证
            if (areaID == 0)
            {
                msg = "请选择线路区域!";
            }
            if (routeName == "")
            {
                msg += "<br />请输入线路名称!";
            }
            if (days == 0)
            {
                msg += "<br />请输入天数!";
            }
            if (planPeopleNumber == 0)
            {
                msg += "<br />请输入预控人数!";
            }

            if (sellsID == "")
            {
                msg += "<br />请输入销售员!";
            }
            if (successDateBegin.Length == 0)
            {
                msg += "<br />请选择出团日期!";
            }
            if (msg != "")
            {
                return UtilsCommons.AjaxReturnJson("0", msg);
            }
            #endregion


            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourSanPinInfo model = new EyouSoft.Model.TourStructure.MTourSanPinInfo();
            string act = Utils.GetQueryStringValue("act");
            if (act == "update")
            {
                model.TourId = Utils.GetQueryStringValue("id");
            }

            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.AreaId = areaID;
            string costCalculation = string.Empty;
            EyouSoft.Model.TourStructure.MTourService tourService = UtilsCommons.GetTourService(out costCalculation);
            model.CostCalculation = costCalculation;
            model.Gather = successGather;
            model.PlanPeopleNumber = planPeopleNumber;
            model.LTraffic = successsStraffBegin;
            model.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo();
            model.OperatorInfo.OperatorId = this.SiteUserInfo.UserId;
            model.OperatorInfo.Name = this.SiteUserInfo.Name;
            model.OperatorInfo.Phone = this.SiteUserInfo.Telephone;
            model.PlanFeature = planContent;
            model.RouteId = routeID;
            model.RouteName = routeName;
            model.RTraffic = successsStraffEnd;
            model.StopDays = stopDate;
            model.IsShowDistribution = isShowDistribution;
            model.TourChangeTitle = changeTitle;
            model.TourChangeContent = changeRemark;
            model.KeyName = searchKey;

            #region 附件
            if (filsPath != "")
            {
                string[] filesArray = filsPath.Split('|');
                if (filesArray.Length > 1)
                {
                    model.FilePath = filesArray[1];
                }
            }
            #endregion

            #region 签证附件
            IList<EyouSoft.Model.ComStructure.MComAttach> visaList = null;
            if (visaUpload.Length > 0)
            {
                visaList = new List<EyouSoft.Model.ComStructure.MComAttach>();
                for (int i = 0; i < visaUpload.Length; i++)
                {
                    if (visaUpload[i].Trim() != "")
                    {
                        if (visaUpload[i].Split('|').Length > 1)
                        {
                            EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();
                            visaModel.Downloads = 0;
                            visaModel.FilePath = visaUpload[i].Split('|')[1];
                            visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.计划签证资料;
                            visaModel.Name = visaUpload[i].Split('|')[0];
                            visaModel.Size = 0;
                            visaList.Add(visaModel);
                        }
                    }
                }
            }
            if (oldVisaUpload.Length > 0)
            {
                if (visaList == null)
                {
                    visaList = new List<EyouSoft.Model.ComStructure.MComAttach>();
                }
                for (int i = 0; i < oldVisaUpload.Length; i++)
                {
                    EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();
                    visaModel.Downloads = Utils.GetInt(oldVisaUpload[i].Split('|')[2]);
                    visaModel.FilePath = oldVisaUpload[i].Split('|')[1];
                    visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.计划签证资料;
                    visaModel.Name = oldVisaUpload[i].Split('|')[0];
                    visaModel.Size = 0;
                    visaList.Add(visaModel);
                }
            }
            model.VisaFileList = visaList;
            #endregion


            //获得销售员信息实体
            EyouSoft.Model.ComStructure.MComUser sellsModel = new EyouSoft.BLL.ComStructure.BComUser().GetModel(sellsID, SiteUserInfo.CompanyId);
            if (sellsModel != null)
            {
                model.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo();
                model.SaleInfo.SellerId = sellsID;
                model.SaleInfo.Name = sellsName;
                model.SaleInfo.Phone = sellsModel.ContactMobile;
                model.SaleInfo.DeptId = sellsModel.DeptId;
            }
            model.TourDays = days;
            model.TourPlan = UtilsCommons.GetPlanList();
            model.TourService = tourService;

            model.MTourPriceStandard = UtilsCommons.GetPriceStandard();

            model.TourCarLocation = GetTourCarLocationList();
            model.TourCarType = GetTourCarTypeList();
            model.LDate = null;
            model.RDate = null;

            #region 无需赋值
            //model.QuoteRemark
            //model.RealPeopleNumber
            //model.Review
            //model.ReviewTime
            //model.TourPlaner
            //model.TourPlanItem
            //model.TourPlanStatus
            //model.PeopleNumberLast
            //model.GuideList = null;
            //model.IsChange = false;
            //model.IsReview
            //model.IsSubmit
            //model.IsSure 列表
            //model.LeavePeopleNumber
            #endregion
            //超限赋值
            switch (type)
            {
                case 1:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线; break;
                case 2:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼; break;
                case 3:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼; break;
            }
            if (act == "add" || act == "copy")
            {
                bool result = false;
                model.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;

                model.TourChildrenInfo = new List<MTourChildrenInfo>();
                for (int i = 0; i < successDateBegin.Length; i++)
                {
                    MTourChildrenInfo childModel = new MTourChildrenInfo();
                    childModel.LDate = Utils.GetDateTime(successDateBegin[i]);
                    model.TourChildrenInfo.Add(childModel);
                }
                result = bll.AddTourSanPin(model);
                if (result)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "新增散拼计划 成功,正在跳转");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "新增散拼计划 失败，请稍后再试!");
                }
            }
            if (act == "update" && model.TourId != "")
            {
                model.UpdateTime = DateTime.Now;
                model.LDate = Utils.GetDateTime(successDateBegin[0]);
                if (bll.UpdateTourSanPin(model))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "修改散拼计划 成功,正在跳转..");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "修改散拼计划 失败,请稍后再试!");
                }
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// 获取上车地点集合
        /// </summary>
        /// <returns>list</returns>
        private IList<MTourCarLocation> GetTourCarLocationList()
        {
            IList<MTourCarLocation> list = new List<MTourCarLocation>();
            string pickUpTourIdArr = Utils.GetFormValue(this.PickUpPoint1.PickUpTourIdClientID);
            string[] idArr = Utils.GetFormValue(this.PickUpPoint1.PickUpIDClientID).Split(',');
            string[] textArr = Utils.GetFormValue(this.PickUpPoint1.PickUpTextClientID).Split(',');
            string[] priceJArr = Utils.GetFormValue(this.PickUpPoint1.PickUpPriceJClientID).Split(',');
            string[] priceSArr = Utils.GetFormValue(this.PickUpPoint1.PickUpPriceSClientID).Split(',');
            string[] descArr = Utils.GetFormValue(this.PickUpPoint1.PickUpDescClientID).Split(new string[] { "&&&" }, StringSplitOptions.None);
            if (idArr.Length > 0)
            {
                for (int i = 0; i < idArr.Length; i++)
                {
                    if (idArr[i].Trim() != "" && textArr[i].Trim() != "")
                    {
                        MTourCarLocation model = new MTourCarLocation();
                        model.TourLocationId = pickUpTourIdArr;
                        model.CarLocationId = idArr[i];
                        model.Location = textArr[i];
                        model.OnPrice = Utils.GetDecimal(priceJArr[i]);
                        model.OffPrice = Utils.GetDecimal(priceSArr[i]);
                        model.Desc = descArr[i];
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取预设车型列表
        /// </summary>
        /// <returns></returns>
        private IList<MTourCarType> GetTourCarTypeList()
        {
            IList<MTourCarType> list = new List<MTourCarType>();
            string[] tourCarTypeIdArr = Utils.GetFormValues(this.PresetBusType1.hideTourCarTypeIdClientID);
            string[] carTypeIdArr = Utils.GetFormValues(this.PresetBusType1.hidCarTypeIdClientID);
            string[] carTypeNameArr = Utils.GetFormValues(this.PresetBusType1.hidCarTypeNameClientID);
            string[] descArr = Utils.GetFormValues(this.PresetBusType1.hidDescClientID);
            string[] seatNum = Utils.GetFormValues(this.PresetBusType1.hidSeatNumClientID);
            if (tourCarTypeIdArr.Length > 0)
            {
                for (int i = 0; i < carTypeIdArr.Length; i++)
                {
                    if (carTypeIdArr[i].Trim() != "")
                    {
                        MTourCarType model = new MTourCarType();
                        model.TourCarTypeId = tourCarTypeIdArr[i];
                        model.CarTypeId = carTypeIdArr[i];
                        model.CarTypeName = carTypeNameArr[i];
                        model.Desc = descArr[i];
                        model.SeatNum = Utils.GetInt(seatNum[i]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        #region 栏目权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            #region 权限分类判断
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_栏目, true);
                        return;
                    }
                    break;
                case 2:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_栏目, true);
                        return;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_栏目, true);
                        return;
                    }
                    break;
            }
            #endregion
        }
        #endregion

        #region 按钮权限控制
        /// <summary>
        /// 按钮权限控制
        /// </summary>
        protected void BtnPowerControl()
        {
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_新增))
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 2:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_新增))
                    {
                        this.phdSave.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_新增))
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
            }

        }
        #endregion
    }
}
