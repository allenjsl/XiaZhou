﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.TeamCenter
{
    public partial class ShortSanKeBaoMing : BackPage
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
        /// 最晚留位时间
        /// </summary>
        protected string MaxDateTime;
        /// <summary>
        /// 上车地点下拉选项
        /// </summary>
        protected string strPickUpPosition = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 控件设置
            this.SellsSelect1.SetTitle = "销售员";
            this.SellsSelect1.CallBackFun = "OrderInfoPage.CallBackFun";
            this.SellsSelect1.ClientDeptID = this.hideDeptID.ClientID;
            this.SellsSelect1.ClientDeptName = this.hideDeptName.ClientID;
            this.PriceStand1.CompanyID = SiteUserInfo.CompanyId;
            this.PriceStand1.InitMode = false;
            this.PriceStand1.ShowModel = true;
            this.CustomerUnitSelect1.CallBack = "OrderInfoPage.CustomerUnitCallBack";
            this.CustomerUnitSelect1.IsApply = true;
            #endregion

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("dotype");

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



            if (!IsPostBack)
            {
                string tourID = Utils.GetQueryStringValue("tourID");
                string orderID = Utils.GetQueryStringValue("orderID");

                #region 获得结算价等级
                EyouSoft.BLL.ComStructure.BComLev comLevBll = new EyouSoft.BLL.ComStructure.BComLev();
                IList<EyouSoft.Model.ComStructure.MComLev> sysComLev = comLevBll.GetList(SiteUserInfo.CompanyId);
                if (sysComLev != null && sysComLev.Count > 0)
                {
                    EyouSoft.Model.ComStructure.MComLev peerLevModel = sysComLev.FirstOrDefault(p => p.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.内部结算价);
                    if (peerLevModel != null)
                    {
                        this.hideSettLevelID.Value = peerLevModel.Id.ToString();
                    }
                }
                comLevBll = null;
                sysComLev = null;
                #endregion

                #region 获得留位时间
                GetMaxDateTime();
                this.txtSaveSeatDate.Attributes.Add("onfocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'" + this.MaxDateTime + "',minDate:'" + DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "'})");

                #endregion

                //根据ID初始化页面
                if (tourID != "")
                {
                    PageInitForTour(tourID);
                }
                if (orderID != "")
                {
                    PageInitForOrder(orderID);
                }

                if (tourID == "" && orderID == "")
                {
                    Utils.ResponseGoBack();
                }
                /*
                 * 报名途径
                系统后台报名：确认成交（已成交）、留位（留位）
                分销商平台报名：提交订单（未处理）
                分销商平台订单列表可取消未处理状态的订单
                派团计划保存后订单就是已成交

                 * 订单类型
                未处理：确认成交，同意留位，取消订单
                已留位：确认成交、继续留位、取消订单
                留位过期：确认成交，继续留位，取消订单                     
                已成交:修改（派团给计调前）或变更（派团后计调后）、取消订单
                不受理：无操作
                 */
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作tourID</param>
        protected void PageInitForTour(string tourID)
        {
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo model = bll.GetTourInfo(tourID);

            if (model != null && model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
            {
                EyouSoft.Model.TourStructure.MTourSanPinInfo tourBaseModel = (EyouSoft.Model.TourStructure.MTourSanPinInfo)model;
                //权限控制
                PowerControl(tourBaseModel.TourType);
                //根据计划类型显示
                if (tourBaseModel.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼)
                {
                    this.pnlTravelFrist.Visible = false;
                }
                else
                {
                    this.pnlTravelSecond.Visible = false;
                }
                this.hideTourType.Value = ((int)tourBaseModel.TourType).ToString();
                this.hideSourceID.Value = tourBaseModel.SourceId;
                this.PriceStand1.ShowModel = true;
                this.PriceStand1.SetPriceStandard = tourBaseModel.MTourPriceStandard;
                this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                this.SellsSelect1.SellsName = SiteUserInfo.Name;
                this.hideDeptID.Value = SiteUserInfo.DeptId.ToString();
                this.hideDeptName.Value = SiteUserInfo.DeptName;
                this.lblOrderMan.Text = SiteUserInfo.Name;


                this.pdhJiXuLiuWei.Visible = false;
                this.pdhBaoCun.Visible = false;
                this.phdQuXiao.Visible = false;
                this.phdBuShouLi.Visible = false;
                this.phdQueRenYuLiu.Visible = false;
                this.phsQueRenChengJiao.Visible = false;

                //如果是供应商的团
                if (tourBaseModel.SourceId != null && tourBaseModel.SourceId.Trim() != "")
                {
                    this.pdhBaoCun.Visible = true;
                }
                else
                {
                    this.phdQueRenYuLiu.Visible = true;
                    this.phsQueRenChengJiao.Visible = true;
                }

                #region 上车地点下拉框
                IList<EyouSoft.Model.TourStructure.MTourCarLocation> CarList = bll.GetTourCarLocation(tourID);
                if (CarList != null && CarList.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<option value=''>请选择</option>");
                    for (int i = 0; i < CarList.Count; i++)
                    {
                        sb.AppendFormat("<option value='{0}' data-onprice='{2}' data-offprice='{3}' data-desc='{4}' >{1}</option>", CarList[i].TourLocationId, CarList[i].Location, Utils.FilterEndOfTheZeroString(CarList[i].OnPrice.ToString("0.00")), Utils.FilterEndOfTheZeroString(CarList[i].OffPrice.ToString("0.00")), CarList[i].Desc);
                    }
                    strPickUpPosition = sb.ToString();
                    this.hideIslocation.Value = "Show";
                }
                else
                {
                    this.ph_PickUpPostion.Visible = false;
                    this.hideIslocation.Value = "";
                }
                #endregion

                //座位设置
                if (tourBaseModel.TourCarType == null || tourBaseModel.TourCarType.Count == 0)
                {
                    this.phdSetSeat.Visible = false;
                    this.hideIscarmodel.Value = "";
                }

                #region 预设车型
                this.SetSeat1.TourId = tourBaseModel.TourId;

                #endregion
            }
            else
            {
                Utils.ResponseGoBack();
            }
        }

        protected void PageInitForOrder(string orderID)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bll = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = bll.GetTourOrderExpandByOrderId(orderID);

            
            if (orderModel != null)
            {

                if (orderModel.TourType != EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线) {
                    Utils.ResponseGoBack();
                    return;
                } 

                this.lblOrderNum.Text = orderModel.OrderCode;
                this.SellsSelect1.SellsID = orderModel.SellerId;
                this.SellsSelect1.SellsName = orderModel.SellerName;
                this.hideDeptID.Value = orderModel.DeptId.ToString();
                this.CountryID = orderModel.BuyCountryId.ToString();
                this.ProvinceID = orderModel.BuyProvincesId.ToString();
                this.CustomerUnitSelect1.CustomerUnitId = orderModel.BuyCompanyId;
                this.CustomerUnitSelect1.CustomerUnitName = orderModel.BuyCompanyName;
                this.txtContact.Text = orderModel.ContactName;
                this.txtContactTel.Text = orderModel.ContactTel;
                this.hideContactDeptId.Value = orderModel.ContactDepartId;
                this.lblOrderMan.Text = orderModel.Operator;
                this.txtAdultCount.Text = orderModel.Adults.ToString();
                this.txtChildCount.Text = orderModel.Childs.ToString();
                this.hideTourId.Value = orderModel.TourId;
                this.hideOldAdultCount.Value = orderModel.Adults.ToString() + "|" + orderModel.Childs.ToString();
                txtHeTongHao.HeTongId = orderModel.ContractId;
                txtHeTongHao.HeTongCode = orderModel.ContractCode;
                this.hideSourceID.Value = orderModel.SourceId;

                #region 销售价与报价等级
                this.hideLevelID.Value = orderModel.LevId.ToString();
                this.hidePriceStandID.Value = orderModel.PriceStandId.ToString();
                this.hideAdultPrice.Value = Utils.FilterEndOfTheZeroDecimal(orderModel.AdultPrice);
                this.hideChildPrcie.Value = Utils.FilterEndOfTheZeroDecimal(orderModel.ChildPrice);
                #endregion

                #region 结算价与报价等级
                this.hideSettAdultPrice.Value = Utils.FilterEndOfTheZeroDecimal(orderModel.PeerAdultPrice);
                this.hideSettChildPrice.Value = Utils.FilterEndOfTheZeroDecimal(orderModel.PeerChildPrice);
                #endregion


                EyouSoft.Model.TourStructure.MTourSanPinInfo tourModel = (EyouSoft.Model.TourStructure.MTourSanPinInfo)new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(orderModel.TourId);
                if (tourModel != null)
                {
                    //权限控制
                    PowerControl(tourModel.TourType);

                    this.hideTourType.Value = ((int)tourModel.TourType).ToString();
                    this.PriceStand1.SetPriceStandard = tourModel.MTourPriceStandard;
                    switch (tourModel.TourType)
                    {
                        case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                            this.pnlTravelFrist.Visible = false;
                            if (orderModel.MTourOrderTravellerList != null)
                            {
                                this.TravelControlS1.SetTravelList = orderModel.MTourOrderTravellerList.Where(p => p.TravellerStatus == EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团).ToList();
                            }
                            break;
                        default:
                            this.pnlTravelSecond.Visible = false;
                            if (orderModel.MTourOrderTravellerList != null)
                            {
                                this.TravelControl1.SetTravelList = orderModel.MTourOrderTravellerList.Where(p => p.TravellerStatus == EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团).ToList();
                            }
                            break;
                    }
                    if (orderModel.MTourOrderTravellerList != null)
                    {
                        IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> outTraveller = orderModel.MTourOrderTravellerList.Where(p => p.TravellerStatus == EyouSoft.Model.EnumType.TourStructure.TravellerStatus.退团).ToList();
                        if (outTraveller != null && outTraveller.Count > 0)
                        {
                            this.rptOutTraveller.DataSource = outTraveller;
                            this.rptOutTraveller.DataBind();
                        }
                    }

                    //如果计划上没有设置上车地点，则隐藏
                    if (tourModel.TourCarLocation == null || tourModel.TourCarLocation.Count == 0)
                    {
                        this.ph_PickUpPostion.Visible = false;
                        this.hideIslocation.Value = "";
                    }
                    else
                    {
                        #region 上车地点
                        this.hideIslocation.Value = "Show";
                        IList<MTourCarLocation> list = new EyouSoft.BLL.TourStructure.BTour().GetTourCarLocation(orderModel.TourId);
                        if (list != null && list.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<option value=''>请选择</option>");
                            foreach (var item in list)
                            {
                                if (orderModel.TourOrderCarLocation != null && item.TourLocationId == orderModel.TourOrderCarLocation.TourLocationId)
                                {
                                    sb.AppendFormat("<option value='{0}' data-onprice='{2}' data-offprice='{3}' data-desc='{4}' data-location='{5}' selected='selected' >{1}</option>", item.TourLocationId, item.Location, Utils.FilterEndOfTheZeroString(item.OnPrice.ToString("0.00")), Utils.FilterEndOfTheZeroString(item.OffPrice.ToString("0.00")), item.Desc,item.Location);
                                }
                                else
                                {
                                    sb.AppendFormat("<option value='{0}' data-onprice='{2}' data-offprice='{3}' data-desc='{4}' data-location='{5}' >{1}</option>", item.TourLocationId, item.Location, Utils.FilterEndOfTheZeroString(item.OnPrice.ToString("0.00")), Utils.FilterEndOfTheZeroString(item.OffPrice.ToString("0.00")), item.Desc,item.Location);
                                }

                            }
                            strPickUpPosition = sb.ToString();
                            if (orderModel.TourOrderCarLocation != null)
                            {
                                this.lbOnPrice.Text = this.hidOnPrice.Value = this.lbOnPrice.Text = Utils.FilterEndOfTheZeroString(orderModel.TourOrderCarLocation.OnPrice.ToString("0.00"));
                                this.lbOffPrice.Text = this.hidOffPrice.Value = Utils.FilterEndOfTheZeroString(orderModel.TourOrderCarLocation.OffPrice.ToString("0.00"));
                                this.lbDesc.Text = this.hidDesc.Value = orderModel.TourOrderCarLocation.Desc;
                                this.hidLocation.Value = orderModel.TourOrderCarLocation.Location;
                            }
                        }

                        #endregion

                    }
                    //如果团上没有车型，则隐藏
                    if (tourModel.TourCarType == null || tourModel.TourCarType.Count == 0)
                    {
                        this.phdSetSeat.Visible = false;
                        this.hideIscarmodel.Value = "";
                    }
                    else
                    {
                        #region 预设车型
                        this.hideIscarmodel.Value = "Show";
                        this.SetSeat1.TourId = orderModel.TourId;
                        this.SetSeat1.OrderId = orderModel.OrderId;
                        this.SetSeat1.PeoNum = this.SetSeat1.OldPeoNum = (orderModel.Adults + orderModel.Childs).ToString();
                        if (orderModel.TourOrderCarTypeSeatList != null && orderModel.TourOrderCarTypeSeatList.Count > 0)
                        {
                            this.SetSeat1.setSeatHidValue = Newtonsoft.Json.JsonConvert.SerializeObject(orderModel.TourOrderCarTypeSeatList);
                        }
                        #endregion
                    }

                }
                this.txtAddPrice.Text = Utils.FilterEndOfTheZeroDecimal(orderModel.SaleAddCost);
                this.txtRemarksFrist.Text = orderModel.SaleAddCostRemark;
                this.txtReducePrice.Text = orderModel.SaleReduceCost.ToString("f2");
                this.txtRemarksSecond.Text = orderModel.SaleReduceCostRemark;
                this.txtTotalPrice.Text = orderModel.SumPrice.ToString("f2");
                this.hideOldSumMoney.Value = orderModel.SumPrice.ToString("f2");
                this.txtGuidePrice.Text = orderModel.GuideIncome.ToString("f2");
                this.lblSellPrice.Text = orderModel.SalerIncome.ToString("f2");
                this.txtSaveSeatDate.Text = orderModel.SaveSeatDate.HasValue ? orderModel.SaveSeatDate.Value.ToString("yyyy-MM-dd HH:mm") : "";
                this.txtRemarksOrder.Text = orderModel.OrderRemark;
                this.hideOrderFrom.Value = ((int)orderModel.OrderType).ToString();


                this.pdhJiXuLiuWei.Visible = false;
                this.phdQueRenYuLiu.Visible = false;
                this.pdhBaoCun.Visible = false;
                this.phdQuXiao.Visible = false;
                this.phdBuShouLi.Visible = false;
                this.phsQueRenChengJiao.Visible = false;

                #region 获得该订单的计划状态
                EyouSoft.BLL.TourStructure.BTour btour = new EyouSoft.BLL.TourStructure.BTour();
                EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus = btour.GetTourStatus(SiteUserInfo.CompanyId, orderModel.TourId);
                if (tourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划)
                {
                    //该团已派计调
                    this.hideOrderBianGeng.Value = "true";
                }
                else
                {
                    this.hideOrderBianGeng.Value = "false";
                }
                #endregion

                //必须是下单人或者是销售员才可以修改订单 或者拥有修改所有数据的权限

                bool isUpdate = false;
                if (!SiteUserInfo.IsHandleElse)
                {
                    if (orderModel.SellerId == SiteUserInfo.UserId || orderModel.OperatorId == SiteUserInfo.UserId || orderModel.TourSellerId == SiteUserInfo.UserId)
                    {
                        isUpdate = true;
                    }
                    if (isUpdate == false)
                    {
                        if (orderModel.TourPlanerList != null && orderModel.TourPlanerList.Count > 0)
                        {
                            for (int i = 0; i < orderModel.TourPlanerList.Count; i++)
                            {
                                if (orderModel.TourPlanerList[i].PlanerId == SiteUserInfo.UserId)
                                {
                                    isUpdate = true;
                                }
                            }
                        }
                    }

                }

                if (isUpdate)
                {
                    this.hideOrderState.Value = ((int)orderModel.OrderStatus).ToString();

                    //合同金额未确认 才可以继续操作
                    //if (orderModel.ConfirmMoneyStatus == false)
                    //{

                    if (!string.IsNullOrEmpty(orderModel.SourceId) && orderModel.SourceId.Trim() != "")
                    {
                        if (orderModel.OrderType == EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单)
                        {
                            if (orderModel.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理)
                            {
                                this.pdhBaoCun.Visible = true;
                            }
                            else
                            {
                                this.pdhAllBtns.Visible = false;
                            }
                        }
                        else
                        {
                            if ((orderModel.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理 || orderModel.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功) && orderModel.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 && orderModel.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消)
                            {
                                this.pdhBaoCun.Visible = true;
                                this.phdQuXiao.Visible = true;
                            }
                            else
                            {
                                this.pdhAllBtns.Visible = false;
                            }

                        }
                    }
                    else
                    {
                        switch (orderModel.OrderStatus)
                        {
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.不受理:
                                this.litOrderMsg.Text = "<div class='tishi_info'>该订单不受理，无法操作!</div>";
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.已取消:
                                this.litOrderMsg.Text = "<div class='tishi_info'>该订单已取消，无法操作!</div>";
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核:
                                this.litOrderMsg.Text = "<div class='tishi_info'>该订单正在垫付申请审核中，无法操作!</div>";
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败:
                                if (orderModel.OrderType == EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单)
                                {
                                    this.phdBuShouLi.Visible = true;
                                }
                                else
                                {
                                    this.phdQuXiao.Visible = true;
                                }
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功:
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理:
                                this.pdhBaoCun.Visible = true;
                                this.phdQueRenYuLiu.Visible = true;
                                //封团以后不能在成交订单
                                if (orderModel.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.封团)
                                {
                                    this.phsQueRenChengJiao.Visible = true;
                                }
                                if (orderModel.OrderType == EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单)
                                {
                                    this.phdBuShouLi.Visible = true;
                                }
                                else
                                {
                                    this.phdQuXiao.Visible = true;
                                }
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.留位过期:
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位:
                                if (orderModel.OrderType == EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单)
                                {
                                    this.phdBuShouLi.Visible = true;
                                }
                                else
                                {
                                    this.phdQuXiao.Visible = true;
                                }
                                this.pdhJiXuLiuWei.Visible = true;
                                //封团以后不能在成交订单
                                if (orderModel.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.封团)
                                {
                                    this.phsQueRenChengJiao.Visible = true;
                                }
                                break;
                            case EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交:
                                //封团且成交的订单 无法修改
                                if (orderModel.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.封团)
                                {
                                    this.pdhBaoCun.Visible = true;
                                    this.phdQuXiao.Visible = true;
                                }
                                break;
                        }
                    }
                    //}
                }

            }
            else
            {
                Utils.ResponseGoBack();
            }
        }


        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave()
        {
            string msg = "";

            #region 获得参数
            string doType = Utils.GetQueryStringValue("doType");
            string saveType = Utils.GetQueryStringValue("saveType");
            string tourID = Utils.GetQueryStringValue("tourID");
            string orderID = Utils.GetQueryStringValue("orderID");
            #endregion

            #region 获得表单
            //联系人
            string contactName = Utils.GetFormValue(this.txtContact.UniqueID);
            string contactTel = Utils.GetFormValue(this.txtContactTel.UniqueID);
            //联系人部门编号
            string contactDepartId = Utils.GetFormValue(this.hideContactDeptId.UniqueID);
            //销售员
            string sellsID = Utils.GetFormValue(this.SellsSelect1.SellsIDClient);
            string sellsName = Utils.GetFormValue(this.SellsSelect1.SellsNameClient);
            //客户单位
            string companyID = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHBH);
            string companyName = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHMC);
            #region 团队计划价格组成
            //成人单价
            decimal adultPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideAdultPrice.UniqueID));
            //成人数
            int adultCount = Utils.GetInt(Utils.GetFormValue(this.txtAdultCount.UniqueID));
            //儿童单价
            decimal childPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideChildPrcie.UniqueID));
            //儿童数量
            int childCount = Utils.GetInt(Utils.GetFormValue(this.txtChildCount.UniqueID));
            #endregion
            //增加费用
            decimal addPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtAddPrice.UniqueID));
            //增加费用备注
            string remarksFrist = Utils.GetFormValue(this.txtRemarksFrist.UniqueID);
            //减少费用
            decimal reducePrice = Utils.GetDecimal(Utils.GetFormValue(this.txtReducePrice.UniqueID));
            //减少费用备注
            string remarksSecond = Utils.GetFormValue(this.txtRemarksSecond.UniqueID);
            //导游现收
            decimal guidePrice = Utils.GetDecimal(Utils.GetFormValue(this.txtGuidePrice.UniqueID));
            //合计金额
            decimal totalPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrice.UniqueID));
            //销售应收
            decimal sellPrice = totalPrice - guidePrice;
            //订单备注
            string orderRemarks = Utils.GetFormValue(this.txtRemarksOrder.UniqueID);
            //合同号
            string contractCode = Utils.GetFormValue(txtHeTongHao.HeTongCodeClientID);
            string contractCodeId = Utils.GetFormValue(txtHeTongHao.HeTongIdClientID);
            //客源地 国家
            int countryID = Utils.GetInt(Utils.GetFormValue("sltCountry"));
            //客源地 省份
            int provinceID = Utils.GetInt(Utils.GetFormValue("sltProvince"));
            //预留截至时间
            DateTime? saveSeatDate = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtSaveSeatDate.UniqueID));
            //团类型
            EyouSoft.Model.EnumType.TourStructure.TourType tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)Utils.GetInt(Utils.GetFormValue(this.hideTourType.UniqueID));
            //订单来源
            EyouSoft.Model.EnumType.TourStructure.OrderType orderType = (EyouSoft.Model.EnumType.TourStructure.OrderType)Utils.GetInt(Utils.GetFormValue(this.hideOrderFrom.UniqueID));
            #endregion

            #region 表单验证

            if (contactName == "")
            {
                msg = "请输入联系人!<br />";
            }
            if (sellsID == "" || sellsName == "")
            {
                msg += "请选择销售员!<br />";
            }
            if (adultPrice <= 0)
            {
                msg += "请输入成人单价!<br />";
            }
            if (adultCount <= 0)
            {
                msg += "请输入成人数!<br />";
            }
            if (saveType == "1" && saveSeatDate == null)
            {
                msg += "请选择预留日期!<br />";
            }
            if (saveType == "1" && saveSeatDate != null)
            {
                GetMaxDateTime();
                if (saveSeatDate > Utils.GetDateTime(MaxDateTime) || saveSeatDate < DateTime.Now.AddMinutes(5))
                {
                    msg += "请选择新的预留日期!<br />";
                }
            }

            if (msg != "")
            {
                return UtilsCommons.AjaxReturnJson("0", msg);
            }

            #endregion

            EyouSoft.BLL.TourStructure.BTourOrder orderBll = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = new EyouSoft.Model.TourStructure.MTourOrderExpand();

            orderModel.BuyCompanyId = companyID;
            orderModel.BuyCompanyName = companyName;
            orderModel.ContactName = contactName;
            orderModel.ContactTel = contactTel;
            orderModel.ContactDepartId = contactDepartId;
            orderModel.SellerId = sellsID;
            orderModel.SellerName = sellsName;
            orderModel.OperatorId = SiteUserInfo.UserId;
            orderModel.Operator = SiteUserInfo.Name;
            orderModel.Adults = adultCount;
            orderModel.Childs = childCount;
            orderModel.PriceStandId = Utils.GetInt(Utils.GetFormValue(this.hidePriceStandID.UniqueID));
            orderModel.BuyCountryId = countryID;
            orderModel.BuyProvincesId = provinceID;
            orderModel.SettlementMoney = adultPrice * adultCount + childCount * childPrice;
            #region 销售价
            orderModel.LevId = Utils.GetInt(Utils.GetFormValue(this.hideLevelID.UniqueID));
            orderModel.AdultPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideAdultPrice.UniqueID));
            orderModel.ChildPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideChildPrcie.UniqueID));
            #endregion

            #region 结算价
            orderModel.PeerAdultPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideSettAdultPrice.UniqueID));
            orderModel.PeerChildPrice = Utils.GetDecimal(Utils.GetFormValue(this.hideSettChildPrice.UniqueID));
            orderModel.PeerLevId = Utils.GetInt(Utils.GetFormValue(this.hideSettLevelID.UniqueID));
            #endregion

            orderModel.ContractCode = contractCode;
            orderModel.ContractId = contractCodeId;
            orderModel.OrderRemark = orderRemarks;
            orderModel.SaleAddCost = addPrice;
            orderModel.SaleAddCostRemark = remarksFrist;
            orderModel.SaleReduceCost = reducePrice;
            orderModel.SaleReduceCostRemark = remarksSecond;
            orderModel.SalerIncome = sellPrice;
            orderModel.GuideIncome = guidePrice;
            orderModel.SumPrice = totalPrice;
            orderModel.CompanyId = SiteUserInfo.CompanyId;
            orderModel.DeptId = Utils.GetInt(Utils.GetFormValue(this.hideDeptID.UniqueID));
            orderModel.TourId = tourID;
            //switch (tourType)
            //{
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队: orderModel.MTourOrderTravellerList = UtilsCommons.GetTravelListS(); break;

            //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队: orderModel.MTourOrderTravellerList = UtilsCommons.GetTravelList(); break;
            //}
            orderModel.MTourOrderTravellerList = UtilsCommons.GetTravelList(); 
            int result = 0;

            //散拼订单新增

            orderModel.TourType = tourType;
            orderModel.IssueTime = DateTime.Now;

            #region 变更实体
            EyouSoft.Model.TourStructure.MTourOrderChange changeModel = new EyouSoft.Model.TourStructure.MTourOrderChange();
            changeModel.CompanyId = orderModel.CompanyId;
            changeModel.TourId = orderModel.TourId;
            changeModel.ChangePerson = adultCount + childCount;
            changeModel.ChangePrice = totalPrice;
            changeModel.CompanyId = SiteUserInfo.CompanyId;
            if (Utils.GetFormValue(this.hideOldAdultCount.UniqueID).Split('|').Length == 2)
            {
                int oldAdultCount = Utils.GetInt(Utils.GetFormValue(this.hideOldAdultCount.UniqueID).Split('|')[0]);
                int oldChildCount = Utils.GetInt(Utils.GetFormValue(this.hideOldAdultCount.UniqueID).Split('|')[1]);

                changeModel.Content = "成人数由" + adultCount + "变" + oldAdultCount;

                changeModel.Content += " 儿童数由" + childCount + "变" + oldChildCount;

            }
            changeModel.IssueTime = DateTime.Now;
            changeModel.IsSure = false;
            changeModel.Operator = SiteUserInfo.Name;
            changeModel.OperatorId = SiteUserInfo.UserId;
            changeModel.OrderId = orderID;
            changeModel.ChangeType = EyouSoft.Model.EnumType.TourStructure.ChangeType.修改;
            if (Utils.GetFormValue(this.hideOrderBianGeng.UniqueID) == "true")
            {
                changeModel.ChangeType = EyouSoft.Model.EnumType.TourStructure.ChangeType.变更;
            }

            #endregion

            #region 上车地点&座位分配
            //上车地点数据
            if (Utils.GetFormValue("selPickUpPosition").Trim() != "")
            {
                MTourOrderCarLocation modelcar = new MTourOrderCarLocation();
                modelcar.Location = Utils.GetFormValue(this.hidLocation.UniqueID);
                modelcar.Desc = Utils.GetFormValue(this.hidDesc.UniqueID);
                modelcar.OffPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidOffPrice.UniqueID));
                modelcar.OnPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidOnPrice.UniqueID));
                modelcar.OrderId = orderID;
                modelcar.TourLocationId = Utils.GetFormValue("selPickUpPosition");
                orderModel.TourOrderCarLocation = modelcar;
            }
            //座位数据
            IList<MTourOrderCarTypeSeat> listSeat = new List<MTourOrderCarTypeSeat>();
            listSeat = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<MTourOrderCarTypeSeat>>(Utils.GetFormValue(this.SetSeat1.setSeatHidClientID));
            orderModel.TourOrderCarTypeSeatList = listSeat;
            #endregion

            switch (saveType)
            {
                //确认预留
                case "1":
                    orderModel.OrderStatus = EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位;
                    orderModel.SaveSeatDate = saveSeatDate;
                    orderModel.TourOrderChange = changeModel;
                    break;
                //确定成交
                case "2":
                    orderModel.OrderStatus = EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交;
                    orderModel.SaveSeatDate = null;
                    orderModel.TourOrderChange = changeModel;
                    break;
                //修改
                case "4":
                    orderModel.OrderStatus = (EyouSoft.Model.EnumType.TourStructure.OrderStatus)Utils.GetInt(Utils.GetFormValue(this.hideOrderState.UniqueID));
                    orderModel.SaveSeatDate = saveSeatDate;
                    orderModel.TourOrderChange = changeModel;
                    break;
                //取消订单
                case "5":
                    int bllRetCode1 = 0;
                    bllRetCode1 = orderBll.UpdateTourOrderExpand(orderID, EyouSoft.Model.EnumType.TourStructure.OrderStatus.已取消, null);

                    if (bllRetCode1 == 1) return UtilsCommons.AjaxReturnJson("1", "操作成功");
                    else if (bllRetCode1 == -99) return UtilsCommons.AjaxReturnJson("0", "不存在的订单信息");
                    else if (bllRetCode1 == -98) return UtilsCommons.AjaxReturnJson("0", "已取消的订单不可重复取消订单");
                    else if (bllRetCode1 == -97) return UtilsCommons.AjaxReturnJson("0", "该计划状态不允许取消订单");
                    else if (bllRetCode1 == -96) return UtilsCommons.AjaxReturnJson("0", "已确认合同金额不允许取消订单");
                    else if (bllRetCode1 == -95) return UtilsCommons.AjaxReturnJson("0", "已存在收款信息不允许取消订单");
                    else return UtilsCommons.AjaxReturnJson("0", "操作失败");

                //不受理    
                case "6":
                    int bllRetCode2 = 0;
                    bllRetCode2 = orderBll.UpdateTourOrderExpand(orderID, EyouSoft.Model.EnumType.TourStructure.OrderStatus.不受理, null);

                    if (bllRetCode2 == 1) return UtilsCommons.AjaxReturnJson("1", "操作成功");
                    else if (bllRetCode2 == -99) return UtilsCommons.AjaxReturnJson("0", "不存在的订单信息");
                    else if (bllRetCode2 == -98) return UtilsCommons.AjaxReturnJson("0", "已取消的订单不可重复取消订单");
                    else if (bllRetCode2 == -97) return UtilsCommons.AjaxReturnJson("0", "该计划状态不允许取消订单");
                    else if (bllRetCode2 == -96) return UtilsCommons.AjaxReturnJson("0", "已确认合同金额不允许取消订单");
                    else if (bllRetCode2 == -95) return UtilsCommons.AjaxReturnJson("0", "已存在收款信息不允许取消订单");
                    else return UtilsCommons.AjaxReturnJson("0", "操作失败");
            }


            IList<MTourOrderCarTypeSeat> list = new List<MTourOrderCarTypeSeat>();
            #region 新增订单
            if (tourID != "" && orderID == "")
            {
                orderModel.OrderId = Guid.NewGuid().ToString();
                orderModel.OrderType = EyouSoft.Model.EnumType.TourStructure.OrderType.代客预定;
                result = orderBll.AddTourOrderExpand(orderModel, ref list);
            }
            #endregion

            #region 修改订单
            if (orderID != "")
            {
                //如果计划是供应商 发布的，且不超限 那么订单状态永远是未处理
                if (Utils.GetFormValue(this.hideSourceID.UniqueID).Trim() != "")
                {
                    orderModel.OrderStatus = EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理;
                }
                orderModel.OrderId = orderID;
                orderModel.OrderType = orderType;
                result = orderBll.UpdateTourOrderExpand(orderModel, ref list);
            }
            var obj = new { OrderId = orderModel.OrderId, list = list };

            string seatMsg = string.Empty;
            if (list != null && list.Count > 0)
            {
                seatMsg = "<br />{0}.部分座位被占用，请重新安排!";
            }
            switch (result)
            {
                case 1:
                    msg = UtilsCommons.AjaxReturnJson("0", "报名失败,总人数超过计划剩余人数!");
                    break;
                case 2:
                    msg = UtilsCommons.AjaxReturnJson("0", "报名失败,合同号未领用!");
                    break;
                case 3:
                    if (saveType == "1")
                    {
                        if (list != null && list.Count > 0)
                        {
                            msg = UtilsCommons.AjaxReturnJson("-1", "1.预留成功!" + string.Format(seatMsg, "2"), obj);
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "预留成功,正在跳转..");
                        }

                    }
                    if (saveType == "2")
                    {
                        if (list != null && list.Count > 0)
                        {
                            msg = UtilsCommons.AjaxReturnJson("-1", "1.确认成功!" + string.Format(seatMsg, "2"), obj);
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "确认成功!");
                        }
                    }
                    if (saveType == "3")
                    {
                        msg = UtilsCommons.AjaxReturnJson("1", "垫付申请成功!");
                    }
                    if (saveType == "4")
                    {
                        if (list != null && list.Count > 0)
                        {
                            msg = UtilsCommons.AjaxReturnJson("-1", "1.操作成功!" + string.Format(seatMsg, "2"), obj);
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "操作成功!");
                        }
                    }
                    break;
                case 4:
                    msg = UtilsCommons.AjaxReturnJson("0", "报名失败,未知错误!");
                    break;
                case 5:
                case 9:
                    msg = UtilsCommons.AjaxReturnJson("2", "1.操作成功!<br />2.销售员已超限,请收款或超限申请!" + string.Format(seatMsg, "3"), obj);
                    break;
                case 6:
                case 10:
                    msg = UtilsCommons.AjaxReturnJson("2", "操作成功! 客户单位已超限,请收款或超限申请!" + string.Format(seatMsg, "3"), obj);
                    break;
                case 7:
                case 11:
                    msg = UtilsCommons.AjaxReturnJson("2", "操作成功! 销售员和客户单位已超限,请收款或超限申请!" + string.Format(seatMsg, "3"), obj);
                    break;
            }
            #endregion
            return msg;
        }

        /// <summary>
        /// 栏目权限判断
        /// </summary>
        protected void PowerControl(EyouSoft.Model.EnumType.TourStructure.TourType tourType)
        {
            EyouSoft.Model.EnumType.PrivsStructure.Privs menuPrivs = (EyouSoft.Model.EnumType.PrivsStructure.Privs)Utils.GetInt(Utils.GetQueryStringValue("sl"));
            if (menuPrivs == EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目 || menuPrivs == EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目)
            {
                if (menuPrivs == EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目)
                {
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目, true);
                        return;
                    }
                }
                else
                {
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目, true);
                        return;
                    }
                }
            }
            else
            {
                switch (tourType)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                        if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_订单报名))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_订单报名, true);
                            return;
                        }
                        break;
                    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                        if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_订单报名))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_订单报名, true);
                            return;
                        }
                        break;
                    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                        if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_订单报名))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_订单报名, true);
                            return;
                        }
                        break;
                    default:
                        Utils.ResponseGoBack();
                        break;
                }
            }
        }

        #region 获得超限信息
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
                msg = "{\"type\":\"1\",\"chtml\":\"" + cHtml + "\",\"shtml\":\"" + sHtml + "\"}";
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// 获得最大留位时间
        /// </summary>
        private void GetMaxDateTime()
        {
            EyouSoft.BLL.ComStructure.BComSetting settingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            EyouSoft.Model.ComStructure.MComSetting settModel = settingBll.GetModel(CurrentUserCompanyID);
            if (settModel != null)
            {
                if (settModel.SaveTime != 0)
                {
                    this.MaxDateTime = DateTime.Now.AddMinutes(settModel.SaveTime).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
                }
            }
            else
            {
                this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            }

            settingBll = null;
            settModel = null;
        }
    }
}
