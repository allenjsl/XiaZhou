using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;


namespace Web.GroupEnd.Suppliers
{
    /// <summary>
    /// 功能：新增修改复制计划操作
    /// 创建人：马昌雄 2011-10-11
    /// </summary>
    public partial class ProductOperating : SupplierPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Params["Type"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.Write(PageSave(type));
                Response.End();
            }


            if (!IsPostBack)
            {
                //链接过来的参数
                string _type = Utils.GetQueryStringValue("_Type");
                if (!string.IsNullOrEmpty(_type))
                {
                    string tourId = Utils.GetQueryStringValue("TourId");
                    switch (_type)
                    {
                        case "DoUpdate":
                            this.ltMsg.Text = "出团日期：";
                            PageInit(tourId,_type);
                            break;
                        case "DoCopy":
                            this.ltMsg.Text = "出团日期：";
                            PageInit(tourId, _type);
                            break;
                        default:
                            //绑定线路区域
                            this.litArea.Text = GetArea(0);
                            break;
                    }
                }
                //公告
                this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;

                //附件上传
                this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl1.IsUploadSelf = true;

                //行程安排
                this.Journey1.IsSuppliers = true;

            }
        }

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns></returns>
        public string GetArea(int routeAreaId)
        {
            System.Text.StringBuilder area = new System.Text.StringBuilder();
            area.Append("<option value=''>-请选择线路区域-</option>");
            var info = new EyouSoft.BLL.GysStructure.BGys().GetGysUserInfo(SiteUserInfo.SourceCompanyInfo.CompanyId);

            if (info != null && info.Areas != null && info.Areas.Count > 0)
            {
                foreach (var item in info.Areas)
                {
                    if (routeAreaId == item.AreaId)
                    {
                        area.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", item.AreaId, item.AreaName);
                    }
                    else
                    {
                        area.AppendFormat("<option value='{0}' >{1}</option>", item.AreaId, item.AreaName);
                    }
                }
            }
            return area.ToString();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="tourId"></param>
        private void PageInit(string tourId,string type)
        {

            EyouSoft.BLL.TourStructure.BTour bTour = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourSanPinInfo tour = bTour.GetTourInfo(tourId) as EyouSoft.Model.TourStructure.MTourSanPinInfo;
            if (tour != null)
            {

                if (tour.IsCheck && type.Equals("DoUpdate"))
                {
                    this.phMsg.Visible = true;
                    this.phSave.Visible = false;
                    this.phBack.Visible = true;

                    //发班周期
                    this.phdSelectDate.Visible = false;
                    //this.lblLeaveDate.Text = UtilsCommons.GetDateString(tour.LDate.Value, this.ProviderToDate);
                    //this.hideLeaveDate.Value = UtilsCommons.GetDateString(tour.LDate.Value, this.ProviderToDate);
                }

                //线路区域
                this.litArea.Text = GetArea(tour.AreaId);

                this.txtRouteName.Text = tour.RouteName;
                this.txtTourDays.Text = tour.TourDays.ToString();
                this.txtPlanPeopleNumber.Text = tour.PlanPeopleNumber.ToString();

                //发班周期
                //copy可选择法班周期
                //this.phdSelectDate.Visible = false;
                this.lblLeaveDate.Text = UtilsCommons.GetDateString(tour.LDate.Value, this.ProviderToDate);
                this.hideLeaveDate.Value = UtilsCommons.GetDateString(tour.LDate.Value, this.ProviderToDate);

                //停收提前的天数
                this.txtStopDays.Text = tour.StopDays.ToString();

                this.txtLTraffic.Text = tour.LTraffic;
                this.txtRTraffic.Text = tour.RTraffic;
                this.txtGather.Text = tour.Gather;

                //附件
                if (!string.IsNullOrEmpty(tour.FilePath))
                {
                    System.Text.StringBuilder agreement = new System.Text.StringBuilder();
                    agreement.AppendFormat("<span class='upload_filename'><a href='{0}' target='_blank'>查看附件</a><a href=\"javascript:void(0)\" onclick=\"AddSupplierPlan.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideagreement\" value='{0}'/></span>", tour.FilePath);
                    this.lblFile.Text = agreement.ToString();
                }

                this.txtPlanFeature.Text = tour.PlanFeature;
                this.txtPeerAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(tour.MSupplierPublishPrice.SettleAdultPrice);
                this.txtPeerChildPrice.Text = Utils.FilterEndOfTheZeroDecimal(tour.MSupplierPublishPrice.SettleChildPrice);


                //行程安排
                this.Journey1.SetPlanList = tour.TourPlan;


                //购物安排
                this.txtServiceStandard.Text = tour.TourService.ServiceStandard;
                this.txtNoNeedItem.Text = tour.TourService.NoNeedItem;
                this.txtShoppingItem.Text = tour.TourService.ShoppingItem;
                this.txtChildServiceItem.Text = tour.TourService.ChildServiceItem;
                this.txtOwnExpense.Text = tour.TourService.OwnExpense;
                this.txtNeedAttention.Text = tour.TourService.NeedAttention;
                this.txtWarmRemind.Text = tour.TourService.WarmRemind;
                this.txtInsiderInfor.Text = tour.TourService.InsiderInfor;
            }
        }






        private string PageSave(string type)
        {
            string msg = string.Empty;

            EyouSoft.Model.TourStructure.MTourSanPinInfo tour = new EyouSoft.Model.TourStructure.MTourSanPinInfo();
            //供应商信息
            tour.CompanyId = SiteUserInfo.CompanyId;
            tour.SourceId = SiteUserInfo.SourceCompanyInfo.CompanyId;
            tour.SourceCompanyName = SiteUserInfo.SourceCompanyInfo.CompanyName;

            //销售员为null
            // tour.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo();

            
            tour.IsShowDistribution = true;

            tour.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo();
            tour.OperatorInfo.OperatorId = SiteUserInfo.UserId;
            tour.OperatorInfo.Name = SiteUserInfo.Name;
            tour.OperatorInfo.Mobile = SiteUserInfo.Mobile;
           


            tour.AreaId = Utils.GetInt(Utils.GetFormValue("sltArea"));
            if (tour.AreaId <= 0)
            {
                msg += "请选择线路区域 </br>";
            }

            tour.RouteName = Utils.GetFormValue(this.txtRouteName.UniqueID);
            if (string.IsNullOrEmpty(tour.RouteName))
            {
                msg += "请填写线路名称 </br>";
            }

            tour.TourDays = Utils.GetInt(Utils.GetFormValue(this.txtTourDays.UniqueID));
            if (tour.TourDays <= 0)
            {
                msg += "请填写团队天数 </br>";
            }

            string leaveDate = Utils.GetFormValue(this.hideLeaveDate.UniqueID);
            if (string.IsNullOrEmpty(leaveDate))
            {
                msg += "请选择发班周期 </br>";
            }


            //预控人数
            tour.PlanPeopleNumber = Utils.GetInt(Utils.GetFormValue(this.txtPlanPeopleNumber.UniqueID), 0);
            if (tour.PlanPeopleNumber < 0)
            {
                msg += "请填写预控人数 </br>";
            }

            tour.StopDays = Utils.GetInt(Utils.GetFormValue(this.txtStopDays.UniqueID));
            if (tour.StopDays < 0)
            {
                msg += "请选填写停收提前天数 </br>";
            }
            decimal adultPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtPeerAdultPrice.UniqueID), 0);
            if (adultPrice <= 0)
            {
                msg += "请填写正确的成人结算价格 </br>";
            }

            decimal childPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtPeerChildPrice.UniqueID), 0);
            if (childPrice <= 0)
            {
                msg += "请填写正确的儿童结算价格 </br>";
            }

            tour.MSupplierPublishPrice = new EyouSoft.Model.TourStructure.MSupplierPublishPrice();

            tour.MSupplierPublishPrice.SettleAdultPrice = adultPrice;
            tour.MSupplierPublishPrice.SettleChildPrice = childPrice;

            tour.LTraffic = Utils.GetFormValue(this.txtLTraffic.UniqueID);
            tour.RTraffic = Utils.GetFormValue(this.txtRTraffic.UniqueID);
            tour.Gather = Utils.GetFormValue(this.txtGather.UniqueID);
            tour.PlanFeature = Utils.GetFormValue(this.txtPlanFeature.UniqueID);


            //附件
            string file = Utils.GetFormValue(this.UploadControl1.ClientHideID);
            if (!string.IsNullOrEmpty(file))
            {
                tour.FilePath = file.Split('|')[1];
            }
            else
            {
                tour.FilePath = Utils.GetFormValue("hideagreement");
            }


            //行程安排
            tour.TourPlan = UtilsCommons.GetPlanList();


            //购物安排
            tour.TourService = new EyouSoft.Model.TourStructure.MTourService();
            tour.TourService.ServiceStandard = Utils.GetFormValue(this.txtServiceStandard.UniqueID);
            tour.TourService.NoNeedItem = Utils.GetFormValue(this.txtNoNeedItem.UniqueID);
            tour.TourService.ShoppingItem = Utils.GetFormValue(this.txtShoppingItem.UniqueID);
            tour.TourService.ChildServiceItem = Utils.GetFormValue(this.txtChildServiceItem.UniqueID);
            tour.TourService.OwnExpense = Utils.GetFormValue(this.txtOwnExpense.UniqueID);
            tour.TourService.NeedAttention = Utils.GetFormValue(this.txtNeedAttention.UniqueID);
            tour.TourService.WarmRemind = Utils.GetFormValue(this.txtWarmRemind.UniqueID);
            tour.TourService.InsiderInfor = Utils.GetFormValue(this.txtInsiderInfor.UniqueID);


            tour.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼;
            //未审核的线路才能修改，在此之前线路状态：销售未派计划
            tour.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;

            if (msg.Length <= 0)
            {
                //选择发班周期
                var selLdateCount = Utils.GetFormValue(this.hideLeaveDate.UniqueID).Split(',');
                switch (type)
                {
                    case "DoAdd":
                        //发班周期、添加子团
                        tour.TourDays = Utils.GetInt(Utils.GetFormValue("txtTourDays"), 0);

                        string[] successDateBegin = string.IsNullOrEmpty(Utils.GetFormValue(this.hideLeaveDate.UniqueID)) ? null : selLdateCount;
                        if (successDateBegin != null)
                        {
                            tour.TourChildrenInfo = new List<EyouSoft.Model.TourStructure.MTourChildrenInfo>();
                            for (int i = 0; i < successDateBegin.Length; i++)
                            {
                                EyouSoft.Model.TourStructure.MTourChildrenInfo childModel = new EyouSoft.Model.TourStructure.MTourChildrenInfo();
                                childModel.LDate = Utils.GetDateTime(successDateBegin[i]);
                                childModel.RDate = Utils.GetDateTime(successDateBegin[i]).AddDays(tour.TourDays);
                                tour.TourChildrenInfo.Add(childModel);
                            }
                        }

                        EyouSoft.BLL.TourStructure.BTour bTourA = new EyouSoft.BLL.TourStructure.BTour();
                        if (bTourA.AddTourSanPin(tour))
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "产品投放 成功,正在跳转..");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "产品投放 失败!");
                        }
                        break;

                    case "DoUpdate":
                        tour.TourId = Utils.GetQueryStringValue("TourId");
                        tour.TourDays = Utils.GetInt(Utils.GetFormValue(this.txtTourDays.UniqueID), 0);
                        if (selLdateCount.Count()>1)
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "请选择一个出团日期！");
                            return msg;
                        }
                        tour.LDate = Utils.GetDateTime(selLdateCount[0]);
                        tour.RDate = tour.LDate.Value.AddDays(tour.TourDays);

                        EyouSoft.BLL.TourStructure.BTour bTourU = new EyouSoft.BLL.TourStructure.BTour();
                        if (bTourU.UpdateTourSanPin(tour))
                        {

                            msg = UtilsCommons.AjaxReturnJson("1", "产品修改 成功,正在跳转..");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "产品修改 失败！");
                        }
                        break;

                    case "DoCopy":
                        tour.TourDays = Utils.GetInt(Utils.GetFormValue("txtTourDays"), 0);
                        if (selLdateCount.Count() > 1)
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "请选择一个出团日期！");
                            return msg;
                        }
                        tour.LDate = Utils.GetDateTime(selLdateCount[0]);
                        tour.RDate = tour.LDate.Value.AddDays(tour.TourDays);

                        EyouSoft.BLL.TourStructure.BTour bTourC = new EyouSoft.BLL.TourStructure.BTour();
                        if (bTourC.AddTourSanPin(tour))
                        {

                            msg = UtilsCommons.AjaxReturnJson("1", "产品复制 成功,正在跳转..");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "产品复制 失败！");
                        }
                        break;
                    default:
                        msg = UtilsCommons.AjaxReturnJson("0", "未执行任何操作！");
                        break;

                }
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", msg);
            }




            return msg;
        }







    }
}
