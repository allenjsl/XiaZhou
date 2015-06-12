using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Common.Page;
using System.Text;

namespace Web.TeamCenter
{
    /// <summary>
    /// 增加/修改团队计划 创建日期：2011.9.13
    /// 修改人：DYZ 
    /// </summary>
    public partial class AddTeamPlan : BackPage
    {
        /// <summary>
        /// 国家编号
        /// </summary>
        protected string CountryID;
        /// <summary>
        /// 省份编号
        /// </summary>
        protected string ProvinceID;

        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 操作类型
        /// </summary>
        protected string act = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            act = Utils.GetQueryStringValue("act");
            //权限验证
            PowerControl();

            #region 用户控件设置
            this.Journey1.IsSuppliers = false;
            this.UC_CustomerUnitSelect.CallBack = "AddTeamPlan.CustomerUnitCallBack";
            this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
            this.UploadControl1.IsUploadMore = true;
            this.UploadControl1.IsUploadSelf = true;
            this.UC_CustomerUnitSelect.IsApply = true;
            this.UC_CustomerUnitSelect.BoxyTitle = "客户单位";
            #endregion

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
                    case "getAmount":
                        Response.Write(GetAmount());
                        break;
                    default: break;
                }
                Response.End();
            }
            #endregion

            this.lblApplyMan.Text = SiteUserInfo.Name;
            this.lblApplyDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (!IsPostBack)
            {

                //获得操作ID
                string id = Utils.GetQueryStringValue("id");

                //根据ID初始化页面
                if (id != "")
                {
                    PageInit(id);
                }
                else
                {
                    BtnPowerControl();
                    switch (type)
                    {
                        case 1:
                        case 2:
                            this.phdTravelControlS.Visible = false;
                            this.phdVisaFile.Visible = false;
                            break;
                        case 3:
                            this.phdTravelControl.Visible = false;

                            break;
                    }
                    this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                    this.SellsSelect1.SellsName = SiteUserInfo.Name;
                    BindAreaList(0);
                }
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {

            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo baseModel = bll.GetTourInfo(id);

            if (baseModel != null && (baseModel.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 || baseModel.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 || baseModel.TourType== EyouSoft.Model.EnumType.TourStructure.TourType.组团团队))
            {
                EyouSoft.Model.TourStructure.MTourTeamInfo model = (EyouSoft.Model.TourStructure.MTourTeamInfo)baseModel;
                if (model.TourCode.Trim() != "")
                {
                    this.lblTourCode.Text = model.TourCode;
                }
                BindAreaList(model.AreaId);
                this.hideRouteID.Value = model.RouteId;
                this.txt_RouteName.Text = model.RouteName;
                this.txt_Days.Text = model.TourDays.ToString();
                this.UC_CustomerUnitSelect.CustomerUnitId = model.CompanyInfo != null ? model.CompanyInfo.CompanyId : "";
                this.UC_CustomerUnitSelect.CustomerUnitName = model.CompanyInfo != null ? model.CompanyInfo.CompanyName : "";
                this.txt_Contact.Text = model.CompanyInfo != null ? model.CompanyInfo.Contact : "";
                this.txt_ConTel.Text = model.CompanyInfo != null ? model.CompanyInfo.Phone : "";
                this.hideContactDeptId.Value = model.ContactDepartId;
                if (act == "copy")
                {
                    this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                    this.SellsSelect1.SellsName = SiteUserInfo.Name;
                    lblTourCode.Text = "";
                }
                else
                {
                    this.SellsSelect1.SellsID = model.SaleInfo.SellerId;
                    this.SellsSelect1.SellsName = model.SaleInfo.Name;
                }
                this.txt_Adult.Text = model.Adults.ToString();
                this.txt_Child.Text = model.Childs.ToString();
                this.txtPlanContent.Text = model.PlanFeature;
                this.txtAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.AdultPrice);
                this.txtChildPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.ChildPrice);
                this.txtOtherPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.OtherCost);
                this.txtSumPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SumPrice);
                this.txtQuoteRemark.Text = model.QuoteRemark;
                this.CountryID = model.CountryId.ToString();
                this.ProvinceID = model.ProvinceId.ToString();
                if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                {
                    this.ForeignQuote1.IsTourOrSubentry = false;
                    this.ForeignQuote1.StandardTourList = model.TourTeamPrice;
                }
                else
                {
                    this.ForeignQuote1.IsTourOrSubentry = true;
                    this.ForeignQuote1.GroupService = model.TourService != null ? model.TourService.ServiceStandard : "";
                }
                this.Journey1.SetPlanList = model.TourPlan;
                CostAccounting1.CostCalculation = model.CostCalculation;
                if (model.TourService != null)
                {
                    CostAccounting1.NoNeedItem = model.TourService.NoNeedItem;
                    CostAccounting1.ShoppingItem = model.TourService.ShoppingItem;
                    CostAccounting1.ChildServiceItem = model.TourService.ChildServiceItem;
                    CostAccounting1.OwnExpense = model.TourService.OwnExpense;
                    CostAccounting1.NeedAttention = model.TourService.NeedAttention;
                    CostAccounting1.WarmRemind = model.TourService.WarmRemind;
                    CostAccounting1.InsiderInfor = model.TourService.InsiderInfor;
                }
                this.txtLDate.Text = model.LDate.HasValue ? model.LDate.Value.ToString("yyyy-MM-dd") : "";
                this.txtSuccesssStraffBegin.Text = model.LTraffic;
                this.txtSuccesssStraffEnd.Text = model.RTraffic;
                this.txtSuccessGather.Text = model.Gather;
                this.txtSuccessAddPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SaleAddCost);
                this.txtSuccessAddPriceRemark.Text = model.AddCostRemark;
                this.txtSuccessReducePrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SaleReduceCost);
                this.txtSuccessReducePriceRemark.Text = model.ReduceCostRemark;
                this.txtSuccessGuideIncome.Text = Utils.FilterEndOfTheZeroDecimal(model.GuideIncome);
                this.lblSuccessSalerIncome.Text = Utils.FilterEndOfTheZeroDecimal(model.SalerIncome);
                this.txtSuccessOrderRemark.Text = model.OrderRemark;

                if (act != "copy")
                {
                    txtHeTongHao.HeTongId= model.HeTongId;
                    txtHeTongHao.HeTongCode=model.HeTongCode;
                }

                //签证附件
                if (model.VisaFileList != null && model.VisaFileList.Count > 0)
                {
                    string visaStr = string.Empty;
                    for (int i = 0; i < model.VisaFileList.Count; i++)
                    {

                        visaStr += "<span class='upload_filename'>&nbsp;<a href='" + model.VisaFileList[i].FilePath + "' target='_blank'>" + model.VisaFileList[i].Name + "</a><a href='javascript:void(0);' onclick='AddTeamPlan.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideVisaFile' value='" + model.VisaFileList[i].Name + "|" + model.VisaFileList[i].FilePath + "|" + model.VisaFileList[i].Downloads.ToString() + "'>；</span>";
                    }
                    this.lblVisaFiles.Text = visaStr;
                }

                switch (model.TourType)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                        this.phdTravelControlS.Visible = false;
                        this.TravelControl1.SetTravelList = model.Traveller;
                        break;
                    case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                        this.phdTravelControl.Visible = false;
                        this.TravelControlS1.SetTravelList = model.Traveller;
                        break;
                }


                #region 修改控制
                this.phdSave.Visible = false;
                if (model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置 ||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕||
                    model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.资金超限
                    )
                {
                    this.phdSave.Visible = true;
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

                switch (model.TourStatus)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.垫付申请: this.hideOverrunState.Value = "3"; break;
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.审核失败: this.hideOverrunState.Value = "4"; break;
                }

            }
            else {
                Utils.ResponseGoBack();
            }
        }


        /// <summary>
        /// 保存新的报价
        /// </summary>
        /// <returns></returns>
        private string PageSave()
        {
            string msg = string.Empty;
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            #region 获取表单
            //线路区域编号
            int areaID = Utils.GetInt(Utils.GetFormValue("sltArea"));
            //线路编号
            string routeID = Utils.GetFormValue(this.hideRouteID.UniqueID);
            //线路名称
            string routeName = Utils.GetFormValue(this.txt_RouteName.UniqueID);
            //天数
            int days = Utils.GetInt(Utils.GetFormValue(this.txt_Days.UniqueID));
            //客源地 国家
            int contryID = Utils.GetInt(Utils.GetFormValue("sltCountry"));
            //客源地 省份
            int provinceID = Utils.GetInt(Utils.GetFormValue("sltProvince"));
            //询价单位 编号
            string buyCompanyID = Utils.GetFormValue(this.UC_CustomerUnitSelect.ClientNameKHBH);
            //询价单位 名称
            string buyCompanyName = Utils.GetFormValue(this.UC_CustomerUnitSelect.ClientNameKHMC);
            //联系人
            string contactName = Utils.GetFormValue(this.txt_Contact.UniqueID);
            //联系电话
            string contactTel = Utils.GetFormValue(this.txt_ConTel.UniqueID);
            //联系人部门编号
            string contactDepartId = Utils.GetFormValue(this.hideContactDeptId.UniqueID);
            //销售员编号
            string sellsID = Utils.GetFormValue(this.SellsSelect1.SellsIDClient);
            //销售员名称
            string sellsName = Utils.GetFormValue(this.SellsSelect1.SellsNameClient);
            //报价员 取当前登录用户
            EyouSoft.Model.TourStructure.MOperatorInfo operatorModel = new EyouSoft.Model.TourStructure.MOperatorInfo();
            operatorModel.OperatorId = this.SiteUserInfo.UserId;
            operatorModel.Name = this.SiteUserInfo.Name;
            operatorModel.Phone = this.SiteUserInfo.Telephone;
            //成人数
            int adultCount = Utils.GetInt(Utils.GetFormValue(this.txt_Adult.UniqueID));
            //成人价格
            decimal adultPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtAdultPrice.UniqueID));
            //儿童数
            int childCount = Utils.GetInt(Utils.GetFormValue(this.txt_Child.UniqueID));
            //儿童价格
            decimal childPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtChildPrice.UniqueID));
            //其它价格
            decimal otherPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtOtherPrice.UniqueID));
            //行程特色
            string planContent = Utils.EditInputText(Request.Form[this.txtPlanContent.UniqueID]);
            //价格备注
            string quoteRemark = Utils.GetFormValue(this.txtQuoteRemark.UniqueID);
            //合计金额
            decimal sumPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtSumPrice.UniqueID));
            //签字资料(附件)


            #region 表单获取
            //出团时间
            DateTime successDateBegin = Utils.GetDateTime(Utils.GetFormValue(this.txtLDate.UniqueID), DateTime.Now);
            //出发交通
            string successsStraffBegin = Utils.GetFormValue(this.txtSuccesssStraffBegin.UniqueID);
            //返回交通
            string successsStraffEnd = Utils.GetFormValue(this.txtSuccesssStraffEnd.UniqueID);
            //集合方式
            string successGather = Utils.GetFormValue(this.txtSuccessGather.UniqueID);
            //增加费用
            decimal successAddPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtSuccessAddPrice.UniqueID), 0);
            //增加费用备注
            string successAddPriceRemark = Utils.GetFormValue(this.txtSuccessAddPriceRemark.UniqueID);
            //减少费用
            decimal successReducePrice = Utils.GetDecimal(Utils.GetFormValue(this.txtSuccessReducePrice.UniqueID));
            //减少费用备注
            string successReducePriceRemark = Utils.GetFormValue(this.txtSuccessReducePriceRemark.UniqueID);
            //导游现收
            decimal successGuideIncome = Utils.GetDecimal(Utils.GetFormValue(this.txtSuccessGuideIncome.UniqueID));
            //订单备注
            string successOrderRemark = Utils.GetFormValue(this.txtSuccessOrderRemark.UniqueID);
            #region 超限申请表单获取
            //垫付金额
            decimal applyPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtApplyPrice.UniqueID));
            //超限备注
            string applyRemarks = Utils.GetFormValue(this.txtApplyRemarks.UniqueID);
            //申请人编号
            string applyManID = this.SiteUserInfo.UserId;
            //申请日期
            DateTime applyDateTime = DateTime.Now;
            #endregion

            //变更标题
            string changeTitle = Utils.GetFormValue("txt_ChangeTitle");
            //变更备注     
            string changeRemark = Utils.GetFormValue("txt_ChangeRemark");
            //签证附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideVisaFile");
            #endregion

            #endregion
            //1=保存
            string saveType = Utils.GetQueryStringValue("saveType");

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
            if (buyCompanyID == "")
            {
                msg += "<br />请选择客户单位!";
            }
            if (sellsID == "")
            {
                msg += "<br />请输入销售员!";
            }
            if (adultCount == 0)
            {
                msg += "<br />请输入成人数!";
            }
            if (adultPrice == 0)
            {
                msg += "<br />请输入成人价!";
            }

            if (msg != "")
            {
                return "{\"result\":\"0\",\"msg\":\"" + msg + "\"}";
            }

            #endregion


            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourTeamInfo model = new EyouSoft.Model.TourStructure.MTourTeamInfo();
            act = Utils.GetQueryStringValue("act");
            if (act == "update")
            {
                model.TourId = Utils.GetQueryStringValue("id");
            }
            model.AddCostRemark = successAddPriceRemark;
            model.AdultPrice = adultPrice;
            model.Adults = adultCount;
            model.AdvanceApp = null;
            model.AreaId = areaID;
            //model.AreaName = "";
            model.ChildPrice = childPrice;
            model.Childs = childCount;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.CompanyInfo = new MCompanyInfo();
            model.CompanyInfo.CompanyId = buyCompanyID;
            model.CompanyInfo.CompanyName = buyCompanyName;
            model.CompanyInfo.Contact = contactName;
            model.CompanyInfo.Phone = contactTel;
            model.ContactDepartId = contactDepartId;
            string costCalculation = string.Empty;
            EyouSoft.Model.TourStructure.MTourService tourService = UtilsCommons.GetTourService(out costCalculation);
            model.CostCalculation = costCalculation;
            model.CountryId = contryID;
            model.Gather = successGather;
            model.GuideIncome = successGuideIncome;

            model.LDate = successDateBegin;

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
                            visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.报价签证资料;
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

            model.LTraffic = successsStraffBegin;
            model.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo();
            model.OperatorInfo.OperatorId = this.SiteUserInfo.UserId;
            model.OperatorInfo.Name = this.SiteUserInfo.Name;
            model.OperatorInfo.Phone = this.SiteUserInfo.Telephone;
            model.OrderRemark = successOrderRemark;
            model.OtherCost = otherPrice;
            Dictionary<string, object> quoteType = UtilsCommons.GetServiceType();
            bool IsTourOrSubentry = (bool)quoteType["IsTourOrSubentry"];
            if (IsTourOrSubentry)
            {
                //整团
                tourService.ServiceStandard = quoteType["Service"].ToString();
                model.OutQuoteType = EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团;
            }
            else
            {
                //分项
                model.TourTeamPrice = (IList<EyouSoft.Model.TourStructure.MTourTeamPrice>)quoteType["Service"];
                model.OutQuoteType = EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项;
            }

            model.PlanFeature = planContent;
            model.ProvinceId = provinceID;
            model.QuoteRemark = quoteRemark;
            model.ReduceCostRemark = successReducePriceRemark;
            model.RouteId = routeID;
            model.RouteName = routeName;
            model.RTraffic = successsStraffEnd;
            model.SaleAddCost = successAddPrice;
            model.TourChangeTitle = changeTitle;
            model.TourChangeContent = changeRemark;

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

            model.SaleReduceCost = successReducePrice;
            model.SalerIncome = sumPrice - successGuideIncome;
            model.AdvanceApp = null;
            model.SumPrice = sumPrice;

            model.TourDays = days;
            model.TourPlan = UtilsCommons.GetPlanList();
            model.TourService = tourService;
            #region 无需赋值
            //model.PlanPeopleNumber
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

            model.HeTongCode = Utils.GetFormValue(txtHeTongHao.HeTongCodeClientID);
            model.HeTongId = Utils.GetFormValue(txtHeTongHao.HeTongIdClientID);

            //超限赋值
            if (saveType == "2")
            {
                model.AdvanceApp = new EyouSoft.Model.TourStructure.MAdvanceApp();
                model.AdvanceApp.Applier = this.SiteUserInfo.Name;
                model.AdvanceApp.ApplierId = this.SiteUserInfo.UserId;
                model.AdvanceApp.DisburseAmount = applyPrice;
                model.AdvanceApp.ApplyTime = applyDateTime;
                model.AdvanceApp.DeptId = this.SiteUserInfo.DeptId;
                model.AdvanceApp.Remark = applyRemarks;
            }
            //变更明细

            switch (type)
            {
                case 1:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.组团团队;
                    model.Traveller = UtilsCommons.GetTravelList();
                    break;
                case 2:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.地接团队;
                    model.Traveller = UtilsCommons.GetTravelList();
                    break;
                case 3:
                    model.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.出境团队;
                    model.Traveller = UtilsCommons.GetTravelListS();
                    break;
            }
            if (act == "add" || act == "copy")
            {
                model.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;
                int r = bll.AddTourTeam(model);
                switch (r)
                {
                    case 0: msg = UtilsCommons.AjaxReturnJson("0", "新增计划 失败，请稍后再试!"); break;
                    case 1: msg = UtilsCommons.AjaxReturnJson("1", "新增计划 成功,正在跳转.."); break;
                    case 2: msg = UtilsCommons.AjaxReturnJson("1", "新增计划 成功,已提交垫付申请!"); break;
                    case 3:
                        msg = UtilsCommons.AjaxReturnJson("2", "操作成功! 销售员已超限,请收款或超限申请!", model.TourId);
                        break;
                    case 4:
                        msg = UtilsCommons.AjaxReturnJson("2", "操作成功! 客户单位已超限,请收款或超限申请!", model.TourId);
                        break;
                    case 5:
                        msg = UtilsCommons.AjaxReturnJson("2", "操作成功! 销售员和客户单位已超限,请收款或超限申请!", model.TourId);
                        break;
                }
            }
            if (act == "update" && model.TourId != "")
            {
                model.UpdateTime = DateTime.Now;
                if (bll.UpdateTourTeam(model))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "修改计划 成功,正在跳转..", model.TourId);
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "修改计划 失败,请稍后再试!", model.TourId);
                }
            }
            return msg;
        }

        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindAreaList(int selectIndex)
        {
            this.litArea.Text = EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(selectIndex, this.SiteUserInfo.CompanyId);
        }
        /// <summary>
        /// 栏目权限判断
        /// </summary>
        protected void PowerControl()
        {
            #region 权限分类判断
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 2:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;

                default: return;
            }
            #endregion
        }

        /// <summary>
        /// 按钮权限控制
        /// </summary>
        protected void BtnPowerControl()
        {
            #region 权限分类判断
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 2:

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_新增))
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_修改))
                    {
                        this.phdSave.Visible = false;
                    }
                    break;

                default: return;
            }
            #endregion
        }

        /// <summary>
        /// 获得超限信息
        /// </summary>
        /// <returns></returns>
        private string GetAmount()
        {
            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
            string msg = string.Empty;
            //询价单位编号
            string buyID = Utils.GetQueryStringValue("buyID");
            //销售员编号
            string sellsID = Utils.GetQueryStringValue("sellsID");
            //合计金额
            decimal price = Utils.GetDecimal(Utils.GetQueryStringValue("price"));
            //返回状态1、2、3
            string state = string.Empty;
            //是否超限
            bool isOverrun = true;
            //报价编号

            string cHtml = string.Empty;
            string sHtml = string.Empty;
            EyouSoft.Model.FinStructure.MCustomerWarning customerWarningModel = bll.GetCustomerOverrunDetail(buyID, price, SiteUserInfo.CompanyId);
            EyouSoft.Model.FinStructure.MSalesmanWarning salesmanWarningModel = bll.GetSaleOverrunDetail(sellsID, price, SiteUserInfo.CompanyId);
            if (customerWarningModel != null)
            {
                cHtml = "<tr><td height='28' bgcolor='#FFFFFF' align='center'>" + customerWarningModel.Customer + "</td><td bgcolor='#FFFFFF' align='center'><b class='fontbsize12'>" + UtilsCommons.GetMoneyString(customerWarningModel.AmountOwed, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><a class='link1'><strong>" + UtilsCommons.GetMoneyString(customerWarningModel.Arrear, this.ProviderToMoney) + "</strong></a></td><td bgcolor='#FFFFFF' align='center'><b class='fontgreen'>" + UtilsCommons.GetMoneyString(customerWarningModel.Transfinite, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'>" + UtilsCommons.GetDateString(customerWarningModel.TransfiniteTime, this.ProviderToDate) + "</td><td bgcolor='#FFFFFF' align='center'>" + customerWarningModel.Deadline.ToString() + "</td><td bgcolor='#FFFFFF' align='center'>" + (customerWarningModel.DeadDay <= 0 ? 0 : customerWarningModel.DeadDay) + "</td></tr>";

                isOverrun = false;
            }

            if (salesmanWarningModel != null)
            {

                sHtml = "<tr><td height='28' bgcolor='#FFFFFF' align='center'>" + salesmanWarningModel.SellerName + "</td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.AmountOwed, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.ConfirmAdvances, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.PreIncome, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><b class='fontblue'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.SumPay, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><b class='fontbsize12'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.Arrear, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><b class='fontgreen'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.Transfinite, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'>" + UtilsCommons.GetDateString(salesmanWarningModel.TransfiniteTime, this.ProviderToDate) + "</td></tr>";

                isOverrun = false;
            }


            if (isOverrun)
            {
                //未超限处理
                msg = "{\"type\":\"3\",\"chtml\":\"" + cHtml + "\",\"shtml\":\"" + sHtml + "\"}";
            }
            else
            {
                //超限 未申请
                msg = "{\"type\":\"1\",\"chtml\":\"" + cHtml + "\",\"shtml\":\"" + sHtml + "\"}";
            }
            return msg;
        }
    }
}
