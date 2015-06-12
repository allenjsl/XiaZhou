using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.TeamCenter
{
    /// <summary>
    /// 新增/修改报价
    /// 修改人:DYZ 创建日期:2011.9.13
    /// </summary>
    public partial class AddPrice : BackPage
    {
        protected int type = 0;
        protected string act = "add";
        protected int sl = 0;

        /// <summary>
        /// 国家编号
        /// </summary>
        protected string CountryID;
        /// <summary>
        /// 省份编号
        /// </summary>
        protected string ProvinceID;
        /// <summary>
        /// 组团打印单链接
        /// </summary>
        protected string PrintPageZt = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            act = Utils.GetQueryStringValue("act");
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));

            //栏目权限验证
            if (act != "forOper")
            {
                PowerControl();
            }

            #region 获得组团打印单链接
            PrintPageZt = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            #endregion

            #region 处理AJAX请求
            string doType = Utils.GetQueryStringValue("doType");
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
                    case "updateCost":
                        Response.Write(UpdateCostCalculation());
                        break;
                }
                Response.End();
            }
            #endregion
            this.UC_CustomerUnitSelect.CallBack = "AddPrice.CustomerUnitCallBack";
            this.UC_CustomerUnitSelect.IsApply = true;
            this.UC_CustomerUnitSelect.BoxyTitle = "询价单位";
            this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
            this.UploadControl1.IsUploadMore = true;
            this.UploadControl1.IsUploadSelf = true;

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                if (act != "update")
                {
                    this.phdPriceCount.Visible = false;
                }
                if (type != 3)
                {
                    this.phdVisaFile.Visible = false;
                    this.phdTravelFrist.Visible = true;
                    this.phdTavelSecond.Visible = false;
                }
                else
                {
                    this.phdTravelFrist.Visible = false;
                    this.phdTavelSecond.Visible = true;
                }
                //根据ID初始化页面
                if (id != "" && act != "add")
                {
                    PageInit(id);
                }
                else
                {
                    //按钮权限控制
                    BtnPowerControl();
                    //设置线路区域
                    BindAreaList(0);
                    UC_SellsSelect.SellsID = SiteUserInfo.UserId;
                    UC_SellsSelect.SellsName = SiteUserInfo.Name;
                    this.lblQuote.Text = SiteUserInfo.Name;
                    this.phdCanel.Visible = false;
                    this.phdPrint.Visible = false;
                    this.phdNewAdd.Visible = false;
                }

            }

        }
        /// <summary>
        /// ajax处理
        /// </summary>
        private void Ajax()
        {
            Response.Clear();
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            string by = Utils.GetQueryStringValue("by");
            switch (by)
            {
                case "getTourCode":
                    {
                        //DateTime startDate = Utils.GetDateTime(Utils.GetQueryStringValue("SDate"));

                        // Response.Write(
                    } break;
            }

            Response.End();
            return;
        }

        #region 绑定线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindAreaList(int selectIndex)
        {
            this.litArea.Text = EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(selectIndex, this.SiteUserInfo.CompanyId);
        }
        #endregion
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 用户控件初始化
            this.UC_Journey.IsSuppliers = false;
            #endregion
            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
            EyouSoft.Model.TourStructure.MTourQuoteInfo model = bll.GetQuoteInfo(id);
            if (model != null)
            {
                #region 绑定报价次数

                //如果其它报价有非处理的，那么该报价不能操作
                bool isCanDo = true;
                string isCanDoQuote = string.Empty;
                if (model.TourQuoteNo != null && model.TourQuoteNo.Count > 0)
                {
                    this.rptChildPrice.DataSource = model.TourQuoteNo.OrderBy(p => p.Times);
                    this.rptChildPrice.DataBind();

                    //如果多次报价中有一个是未处理的，那就记录下来
                    var cdModel = model.TourQuoteNo.FirstOrDefault(p => p.QuoteState != EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理);
                    if (cdModel != null)
                    {
                        isCanDo = false;
                        isCanDoQuote = cdModel.QuoteId;
                    }

                    //获得第一次报价的编号
                    var fristModel = model.TourQuoteNo.FirstOrDefault(p => p.Times == 1);
                    if (fristModel != null)
                    {
                        this.hideFristQuoteId.Value = fristModel.QuoteId;
                    }
                }
                else
                {
                    this.phdPriceCount.Visible = false;
                    this.hideFristQuoteId.Value = model.QuoteId;
                }
                #endregion


                this.hideOverrunState.Value = ((int)model.QuoteState).ToString();
                BindAreaList(model.AreaId);
                this.CountryID = model.CountryId.ToString();
                this.ProvinceID = model.ProvinceId.ToString();
                this.hideRouteID.Value = model.RouteId;
                this.txt_RouteName.Text = model.RouteName;
                this.txt_Days.Text = model.Days.ToString();
                this.UC_CustomerUnitSelect.CustomerUnitId = model.BuyCompanyID;
                this.UC_CustomerUnitSelect.CustomerUnitName = model.BuyCompanyName;
                this.txt_Contact.Text = model.Contact;
                this.txt_ConTel.Text = model.Phone;
                this.hideContactDeptId.Value = model.ContactDepartId;
                this.UC_SellsSelect.SellsID = model.SaleInfo.SellerId;
                this.UC_SellsSelect.SellsName = model.SaleInfo.Name;
                //this.cbxJdxj.Checked = model.IsPlanerQuote;
                this.lblQuote.Text = model.OperatorInfo.Name;
                this.txt_Adult.Text = model.Adults.ToString();
                this.txt_Child.Text = model.Childs.ToString();
                this.txtPlanContent.Text = model.PlanFeature;
                this.txtAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.AdultPrice);
                this.txtChildPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.ChildPrice);
                this.txtOtherPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.OtherCost);
                this.txtSumPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.TotalPrice);
                this.txtQuoteRemark.Text = model.QuoteRemark;
                if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                {
                    this.UC_ForeignQuote.IsTourOrSubentry = false;
                    this.UC_ForeignQuote.StandardTourList = model.TourTeamPrice;
                }
                else
                {
                    this.UC_ForeignQuote.IsTourOrSubentry = true;
                    this.UC_ForeignQuote.GroupService = model.ServiceStandard;
                }
                this.UC_Journey.SetPlanList = model.QuotePlan;
                UC_CostAccounting.CostCalculation = model.CostCalculation;
                UC_CostAccounting.NoNeedItem = model.TourService.NoNeedItem;
                UC_CostAccounting.ShoppingItem = model.TourService.ShoppingItem;
                UC_CostAccounting.ChildServiceItem = model.TourService.ChildServiceItem;
                UC_CostAccounting.OwnExpense = model.TourService.OwnExpense;
                UC_CostAccounting.NeedAttention = model.TourService.NeedAttention;
                UC_CostAccounting.WarmRemind = model.TourService.WarmRemind;
                UC_CostAccounting.InsiderInfor = model.TourService.InsiderInfor;

                //签证附件
                if (model.VisaFileList != null && model.VisaFileList.Count > 0)
                {
                    string visaStr = string.Empty;
                    for (int i = 0; i < model.VisaFileList.Count; i++)
                    {

                        visaStr += "<span class='upload_filename'>&nbsp;<a href='" + model.VisaFileList[i].FilePath + "' target='_blank'>" + model.VisaFileList[i].Name + "</a><a href='javascript:void(0);' onclick='AddPrice.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideVisaFile' value='" + model.VisaFileList[i].Name + "|" + model.VisaFileList[i].FilePath + "|" + model.VisaFileList[i].Downloads.ToString() + "'></span>";
                    }
                    this.lblVisaFiles.Text = visaStr;
                }

                if (model.PlanerId != "")
                {
                    if (model.Planer == "")
                    {
                        EyouSoft.Model.ComStructure.MComUser planerModel = new EyouSoft.BLL.ComStructure.BComUser().GetModel(model.PlanerId, SiteUserInfo.CompanyId);
                        if (planerModel != null)
                        {
                            model.Planer = planerModel.ContactName;
                        }
                    }
                    this.lblToOper.Text = "<span class='upload_filename'>&nbsp;计调员:" + model.Planer + "<a href='javascript:void(0);' onclick='AddPrice.RemoveOper(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideToOperID' value='" + model.PlanerId + "'></span>";
                }


                this.phdSave.Visible = false;
                this.phdQuote.Visible = false;
                this.phdNewAdd.Visible = false;
                this.phdCanel.Visible = false;

                //如果其它报价有非处理的，那么该报价不能操作
                if (isCanDo || (isCanDo == false && isCanDoQuote == model.QuoteId))
                {
                    switch (model.QuoteState)
                    {
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功:
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价: break;
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.垫付申请审核:
                            this.litMsg.Text = "<div class='tishi_info'>该报价垫付申请审核中，无法操作!</div>";
                            //this.phdSave.Visible = true;
                            //this.phdQuote.Visible = true;
                            //this.phdCanel.Visible = true;
                            break;
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核成功:
                            this.phdQuote.Visible = true;
                            this.phdCanel.Visible = true;
                            break;
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核失败:
                            this.phdCanel.Visible = true;
                            break;
                        case EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理:
                            this.phdSave.Visible = true;
                            this.phdQuote.Visible = true;
                            this.phdNewAdd.Visible = true;
                            this.phdCanel.Visible = true;
                            break;
                    }
                }

                if (act == "copy")
                {
                    this.phdPriceCount.Visible = false;
                    this.phdSave.Visible = true;
                    this.phdQuote.Visible = true;
                    this.phdNewAdd.Visible = false;
                    this.phdCanel.Visible = false;
                    this.hideOverrunState.Value = "0";
                    this.UC_SellsSelect.SellsID = SiteUserInfo.UserId;
                    this.UC_SellsSelect.SellsName = SiteUserInfo.Name;
                    this.lblQuote.Text = SiteUserInfo.Name;
                    this.litMsg.Visible = false;
                }

                //权限控制
                BtnPowerControl();

                //是否可以操作该数据
                if (!SiteUserInfo.IsHandleElse && act != "copy")
                {
                    if ((model.OperatorInfo != null && model.OperatorInfo.OperatorId != SiteUserInfo.UserId) || act == "forOper")
                    {
                        this.phdSave.Visible = false;
                        this.phdQuote.Visible = false;
                        this.phdNewAdd.Visible = false;
                        this.phdCanel.Visible = false;
                    }
                }
            }
            else
            {
                Utils.ResponseGoBack();
            }
        }


        /// <summary>
        /// 栏目权限控制
        /// </summary>
        protected void PowerControl()
        {
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_栏目, true);
                        return;
                    }
                    break;
                case 2:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_栏目, true);
                        return;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_栏目, true);
                        return;
                    }
                    break;
                default:
                    Response.Redirect("/default.aspx");
                    break;
            }
        }

        /// <summary>
        /// 按钮权限控制
        /// </summary>
        /// <param name="quoteState"></param>
        protected void BtnPowerControl()
        {
            #region 权限分类判断
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_新增) && (act == "add" || act == "copy"))
                    {
                        this.phdNewAdd.Visible = false;
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_修改) && act == "update")
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_取消))
                    {
                        this.phdCanel.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_报价成功))
                    {
                        this.phdQuote.Visible = false;
                    }

                    break;
                case 2:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_新增) && (act == "add" || act == "copy"))
                    {
                        this.phdNewAdd.Visible = false;
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_修改) && act == "update")
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_取消))
                    {
                        this.phdCanel.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_报价成功))
                    {
                        this.phdQuote.Visible = false;
                    }
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_新增) && (act == "add" || act == "copy"))
                    {
                        this.phdNewAdd.Visible = false;
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_修改) && act == "update")
                    {
                        this.phdSave.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_取消))
                    {
                        this.phdCanel.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_报价成功))
                    {
                        this.phdQuote.Visible = false;
                    } break;
                default: Response.Redirect("/default.aspx"); break;
            }
            #endregion

        }

        /// <summary>
        /// 保存新的报价
        /// </summary>
        /// <returns></returns>
        private string PageSave()
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
            //客源地 国家
            int countryID = Utils.GetInt(Utils.GetFormValue("sltCountry"));
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
            //销售员编号
            string sellsID = Utils.GetFormValue(this.UC_SellsSelect.SellsIDClient);
            //销售员名称
            string sellsName = Utils.GetFormValue(this.UC_SellsSelect.SellsNameClient);
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

            //询价员
            string toOper = Utils.GetFormValue("hideToOperID");

            #region 未超限表单获取
            //出团时间
            DateTime successDateBegin = Utils.GetDateTime(Utils.GetFormValue(this.txtSuccessDateBegin.UniqueID), DateTime.Now);
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
            //是否询价
            bool isPlanerQuote = Utils.GetFormValue(this.cbxJdxj.UniqueID) == "on" ? true : false;
            //签证附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideVisaFile");
            //联系人部门编号
            string contactDeptId = Utils.GetFormValue(this.hideContactDeptId.UniqueID);
            #endregion

            #endregion
            //1=保存,2=报价超限，3=报价未超，4=保存新报价
            string saveType = Utils.GetQueryStringValue("saveType");

            //如果是保存和保存新报价 则不做控制
            if (saveType == "1" || saveType == "4")
            {
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
                    msg += "<br />请输入询价单位!";
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
                    return UtilsCommons.AjaxReturnJson("0", msg);
                }
                
                #endregion
            }

            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
            EyouSoft.Model.TourStructure.MTourQuoteInfo model = new EyouSoft.Model.TourStructure.MTourQuoteInfo();
            string act = Utils.GetQueryStringValue("act");
            string qid = Utils.GetQueryStringValue("id");

            model.AdultPrice = adultPrice;
            model.Adults = adultCount;
            model.AdvanceApp = null;
            model.AreaId = areaID;
            model.BuyCompanyID = buyCompanyID;
            model.BuyCompanyName = buyCompanyName;
            model.IsPlanerQuote = isPlanerQuote;
            model.ChildPrice = childPrice;
            model.Childs = childCount;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.CompanyInfo = new EyouSoft.Model.TourStructure.MCompanyInfo() { CompanyId = buyCompanyID, CompanyName = buyCompanyName, Contact = contactName, Phone = contactTel };
            model.Contact = contactName;
            string costCalculation = string.Empty;
            EyouSoft.Model.TourStructure.MTourService tourService = UtilsCommons.GetTourService(out costCalculation);
            model.TourService = tourService;
            model.CostCalculation = costCalculation;
            model.CountryId = countryID;
            model.Days = days;
            model.InquiryTime = DateTime.Now;
            model.IsPlanerQuote = false;
            model.MTourQuoteTourInfo = null;
            model.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo();
            model.OperatorInfo.OperatorId = this.SiteUserInfo.UserId;
            model.OperatorInfo.Name = this.SiteUserInfo.Name;
            model.OperatorInfo.Phone = this.SiteUserInfo.Telephone;
            model.OtherCost = otherPrice;
            if (isPlanerQuote)
            {
                model.PlanerId = toOper;
            }
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
                    visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.报价签证资料;
                    visaModel.Name = oldVisaUpload[i].Split('|')[0];
                    visaModel.Size = 0;
                    visaList.Add(visaModel);
                }
            }
            model.VisaFileList = visaList;
            #endregion

            Dictionary<string, object> quoteType = UtilsCommons.GetServiceType();
            bool IsTourOrSubentry = (bool)quoteType["IsTourOrSubentry"];

            if (IsTourOrSubentry)
            {
                //整团
                model.ServiceStandard = quoteType["Service"].ToString();
                model.OutQuoteType = EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团;
            }
            else
            {
                //分项
                model.TourTeamPrice = (IList<EyouSoft.Model.TourStructure.MTourTeamPrice>)quoteType["Service"];
                model.OutQuoteType = EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项;
            }
            model.Phone = contactTel;
            model.ContactDepartId = contactDeptId;
            model.PlanFeature = planContent;
            model.ProvinceId = provinceID;
            model.QuotePlan = UtilsCommons.GetPlanList();
            model.QuoteRemark = quoteRemark;
            model.QuoteState = EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理;
            model.QuoteType = (EyouSoft.Model.EnumType.TourStructure.ModuleType)(type - 1);
            model.RouteId = routeID;
            model.RouteName = routeName;
            model.TotalPrice = sumPrice;
            model.QuoteId = qid;
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

            bool result = false;
            //新增，修改，复制
            if (saveType == "1")
            {
                if (act == "add" || act == "copy")
                {
                    model.ParentId = "0";
                    result = bll.AddTourQuote(model);
                    msg = UtilsCommons.AjaxReturnJson("1", "新增报价成功,正在跳转..");
                }
                if (act == "update")
                {
                    model.QuoteId = qid;
                    model.UpdateTime = DateTime.Now;
                    result = bll.UpdateTourQuote(model);
                    msg = UtilsCommons.AjaxReturnJson("1", "修改成功,正在跳转..");
                }
            }

            //超限实体赋值
            if (saveType == "2")
            {
                model.AdvanceApp = new EyouSoft.Model.TourStructure.MAdvanceApp();
                model.AdvanceApp.Applier = this.SiteUserInfo.Name;
                model.AdvanceApp.ApplierId = this.SiteUserInfo.UserId;
                //model.AdvanceApp.DisburseAmount = applyPrice;
                //model.AdvanceApp.ApplyTime = applyDateTime;
                model.AdvanceApp.DeptId = this.SiteUserInfo.DeptId;
                //model.AdvanceApp.Remark = applyRemarks;
                model.MTourQuoteTourInfo = null;
                int r = bll.SuccessTourQuote(model);
                result = (r == 1 || r == 3) ? true : false;
                msg = UtilsCommons.AjaxReturnJson("1", "成功提交垫付申请,等待审核!");
            }
            //未超限实体赋值
            if (saveType == "3")
            {
                model.MTourQuoteTourInfo = new EyouSoft.Model.TourStructure.MTourQuoteTourInfo();
                model.MTourQuoteTourInfo.AddCostRemark = successAddPriceRemark;
                model.MTourQuoteTourInfo.Gather = successGather;
                model.MTourQuoteTourInfo.GuideIncome = successGuideIncome;
                model.MTourQuoteTourInfo.LDate = successDateBegin;
                model.MTourQuoteTourInfo.LTraffic = successsStraffBegin;
                model.MTourQuoteTourInfo.OrderRemark = successOrderRemark;
                model.MTourQuoteTourInfo.ReduceCostRemark = successReducePriceRemark;
                model.MTourQuoteTourInfo.RTraffic = successsStraffEnd;
                model.MTourQuoteTourInfo.SaleAddCost = successAddPrice;
                model.MTourQuoteTourInfo.SaleReduceCost = successReducePrice;
                model.MTourQuoteTourInfo.SalerIncome = sumPrice - successGuideIncome;
                model.AdvanceApp = null;

                switch (type)
                {
                    case 1:
                        model.MTourQuoteTourInfo.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.组团团队;
                        model.MTourQuoteTourInfo.Traveller = UtilsCommons.GetTravelList();
                        break;
                    case 2:
                        model.MTourQuoteTourInfo.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.地接团队;
                        model.MTourQuoteTourInfo.Traveller = UtilsCommons.GetTravelList();
                        break;
                    case 3:
                        model.MTourQuoteTourInfo.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.出境团队;
                        model.MTourQuoteTourInfo.Traveller = UtilsCommons.GetTravelListS();
                        break;
                }
                model.MTourQuoteTourInfo.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;
                model.UpdateTime = DateTime.Now;

                int successState = bll.SuccessTourQuote(model);

                switch (successState)
                {

                    case 2:
                        msg = UtilsCommons.AjaxReturnJson("1", "报价成功!");
                        result = true;
                        break;
                    case 4:
                        msg = UtilsCommons.AjaxReturnJson("0", "操作失败!");
                        break;
                    case 5:
                        msg = UtilsCommons.AjaxReturnJson("2", "报价成功，但销售员超限!,是否需要进行收款操作?");
                        result = true;
                        break;
                    case 6:
                        msg = UtilsCommons.AjaxReturnJson("2", "报价成功，但客户单位超限!,是否需要进行收款操作?");
                        result = true;
                        break;
                    case 7:
                        msg = UtilsCommons.AjaxReturnJson("2", "报价成功，但销售员和客户单位超限!,是否需要进行收款操作?");
                        result = true;
                        break;
                    default:
                        msg = UtilsCommons.AjaxReturnJson("0", "操作失败!");
                        break;
                }

                if (successState == 1)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "超限、垫付申请中，不能报价!");

                }
                if (successState == 2)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "报价成功!");
                    result = true;
                }
            }

            if (saveType == "4")
            {

                model.ParentId = Utils.GetFormValue(this.hideFristQuoteId.UniqueID);
                result = bll.AddNewTourQuote(model);
                msg = UtilsCommons.AjaxReturnJson("1", "新增新报价成功,正在跳转..");
            }
            if (saveType == "5")
            {
                result = bll.CalcelTourQuote(qid, Utils.GetFormValue("txtCanelRemark"));
                msg = UtilsCommons.AjaxReturnJson("1", "取消成功,正在跳转..");
            }

            if (!result)
            {
                msg = UtilsCommons.AjaxReturnJson("0", "操作失败,请稍后尝试!");
            }
            return msg;
            //model.CancelReason = "";
            //model.IsLatest
            //model.OrderCode = "";
            //model.OrderId = "";
            //model.Planer
            //model.TimeCount
            //model.TotalPrice
            //model.TourPrice
            //model.TourQuoteNo
            //model.UpdateTime
            //model.VisaFileList

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
            string id = Utils.GetQueryStringValue("id");

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

        /// <summary>
        /// 修改成本核算
        /// </summary>
        /// <returns></returns>
        private string UpdateCostCalculation()
        {
            string id = Utils.GetQueryStringValue("id");
            string content = Server.UrlDecode(Utils.GetFormValue("content"));
            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
            if (bll.PlanerQuote(id, content))
            {
                return UtilsCommons.AjaxReturnJson("1", "报价成功!");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("0", "报价失败!");
            }

        }
    }
}
