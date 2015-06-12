using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Model.EnumType.PlanStructure;


namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 导游任务单
    /// 创建人：刘飞
    /// 时间：2012-5-15
    /// </summary>
    public partial class daoyourenwudan : BackPage
    {
        #region attributes
        protected int dijie = 0, hotel = 0,
            chedui = 0, plane = 0, train = 0,
            bus = 0, jingdian = 0, shewaichuan = 0,
            guoneichuan = 0, yongcan = 0, gouwu = 0,
            lingliao = 0, qita = 0, guid = 0;

        /// <summary>
        /// 安排编号
        /// </summary>
        string AnPaiId = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 是否仅显示导游支出
        /// </summary>
        protected bool IsJinDaoYouZhiFu = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourId");
            AnPaiId = Utils.GetQueryStringValue("anpaiid");
            IsJinDaoYouZhiFu = Utils.GetQueryStringValue("isjindaoyouzhifu") == "1";

            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitInfo();
            InitAnPaiInfo();

            InitZhiChuHeJi();
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitInfo()
        {
            string orderid = string.Empty;
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();

            EyouSoft.Model.TourStructure.MTourBaseInfo baseModel = bll.GetTourInfo(TourId);

            if (baseModel == null) RCWE("异常请求");

            EyouSoft.Model.TourStructure.MTourTeamInfo teamModel = null;
            EyouSoft.Model.TourStructure.MTourSanPinInfo sanpinModel = null;

            switch (baseModel.TourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    teamModel = (EyouSoft.Model.TourStructure.MTourTeamInfo)baseModel;
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.单项服务:
                    this.TGuideNote.Visible = false;
                    this.TReceiveJourney.Visible = false;
                    this.TService.Visible = false;
                    this.ph_rpt_OrderinfoList.Visible = false;
                    return;
                default:
                    sanpinModel = (EyouSoft.Model.TourStructure.MTourSanPinInfo)baseModel;
                    break;
            }

            #region 订单信息

            EyouSoft.BLL.TourStructure.BTourOrder bllorder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MOrderSum ordersum = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> MtourOrder = bllorder.GetTourOrderListById(TourId, ref ordersum).Where(c => c.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交).ToList();
            if (MtourOrder != null && MtourOrder.Count > 0)
            {
                this.rpt_OrderinfoList.DataSource = MtourOrder;
                this.rpt_OrderinfoList.DataBind();
            }
            else
            {
                this.ph_rpt_OrderinfoList.Visible = false;
            }
            #endregion

            #region 团队信息
            if (teamModel != null)
            {
                this.lbRouteName.Text = teamModel.RouteName;
                this.lbTourCode.Text = teamModel.TourCode;
                this.lbdayCount.Text = teamModel.TourDays.ToString();
                this.lbstarttime.Text = UtilsCommons.GetDateString(teamModel.LDate, ProviderToDate);
                this.lbendtime.Text = UtilsCommons.GetDateString(teamModel.RDate, ProviderToDate);
                if (teamModel.GuideList != null && teamModel.GuideList.Count > 0)
                {
                    string guidelist = string.Empty;
                    for (int i = 0; i < teamModel.GuideList.Count; i++)
                    {
                        if (i == teamModel.GuideList.Count - 1)
                        {
                            guidelist += teamModel.GuideList[i].Name;
                        }
                        else
                        {
                            guidelist += teamModel.GuideList[i].Name + "、";
                        }
                    }
                    this.lbguidename.Text = guidelist;
                }
                this.lbpeoplecount.Text = teamModel.PlanPeopleNumber.ToString();
                if (teamModel.TourPlaner != null && teamModel.TourPlaner.Count > 0)
                {
                    string planerlist = string.Empty;
                    for (int i = 0; i < teamModel.TourPlaner.Count; i++)
                    {
                        if (i == teamModel.TourPlaner.Count - 1)
                        {
                            planerlist += teamModel.TourPlaner[i].Planer;
                        }
                        else
                        {
                            planerlist += teamModel.TourPlaner[i].Planer + "、";
                        }
                    }                    

                    var jiDiaoInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(teamModel.TourPlaner[0].PlanerId, CurrentUserCompanyID);
                    if (jiDiaoInfo != null)
                    {
                        planerlist += " " + jiDiaoInfo.ContactMobile + " " + jiDiaoInfo.ContactTel;
                    }
                    this.lbplander.Text = planerlist;
                }
                if (teamModel.SaleInfo != null)
                {
                    var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(teamModel.SaleInfo.SellerId, CurrentUserCompanyID);
                    if (xiaoShouYuanInfo != null)
                    {
                        teamModel.SaleInfo.Mobile = xiaoShouYuanInfo.ContactMobile;
                        teamModel.SaleInfo.Phone = xiaoShouYuanInfo.ContactTel;
                    }
                    this.lbsellerinfo.Text = teamModel.SaleInfo.Name + " " + teamModel.SaleInfo.Mobile + " " + teamModel.SaleInfo.Phone;
                }
            }
            else if (sanpinModel != null)
            {
                this.lbRouteName.Text = baseModel.RouteName;
                this.lbTourCode.Text = baseModel.TourCode;
                this.lbdayCount.Text = baseModel.TourDays.ToString();
                this.lbstarttime.Text = UtilsCommons.GetDateString(baseModel.LDate, ProviderToDate);
                this.lbendtime.Text = UtilsCommons.GetDateString(baseModel.RDate, ProviderToDate);
                if (baseModel.GuideList != null && baseModel.GuideList.Count > 0)
                {
                    string guidelist = string.Empty;
                    for (int i = 0; i < baseModel.GuideList.Count; i++)
                    {
                        if (i == baseModel.GuideList.Count - 1)
                        {
                            guidelist += baseModel.GuideList[i].Name;
                        }
                        else
                        {
                            guidelist += baseModel.GuideList[i].Name + "、";
                        }
                    }
                    this.lbguidename.Text = guidelist;
                }
                this.lbpeoplecount.Text = baseModel.PlanPeopleNumber.ToString();
                if (baseModel.TourPlaner != null && baseModel.TourPlaner.Count > 0)
                {
                    string planerlist = string.Empty;
                    for (int i = 0; i < baseModel.TourPlaner.Count; i++)
                    {
                        if (i == baseModel.TourPlaner.Count - 1)
                        {
                            planerlist += baseModel.TourPlaner[i].Planer;
                        }
                        else
                        {
                            planerlist += baseModel.TourPlaner[i].Planer + "、";
                        }
                    }
                    var jiDiaoInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(baseModel.TourPlaner[0].PlanerId, CurrentUserCompanyID);
                    if (jiDiaoInfo != null)
                    {
                        planerlist += " " + jiDiaoInfo.ContactMobile + " " + jiDiaoInfo.ContactTel;
                    }
                    this.lbplander.Text = planerlist;
                }
                if (baseModel.SaleInfo != null)
                {
                    var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(baseModel.SaleInfo.SellerId, CurrentUserCompanyID);
                    if (xiaoShouYuanInfo != null)
                    {
                        baseModel.SaleInfo.Mobile = xiaoShouYuanInfo.ContactMobile;
                        baseModel.SaleInfo.Phone = xiaoShouYuanInfo.ContactTel;
                    }
                    this.lbsellerinfo.Text = baseModel.SaleInfo.Name + " " + baseModel.SaleInfo.Mobile + " " + baseModel.SaleInfo.Phone;
                }
            }
            else
            {
                return;
            }
            #endregion

            #region 计调信息
            EyouSoft.BLL.PlanStructure.BPlan bllPlan = new EyouSoft.BLL.PlanStructure.BPlan();
            EyouSoft.Model.PlanStructure.MPlanBaseInfo planinfo = bllPlan.GetGuidePrint(TourId);

            #region 导游安排接待行程
            if (!string.IsNullOrEmpty(planinfo.ReceiveJourney))
            {
                TReceiveJourney.Visible = true;
                this.lbReceiveJourney.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.ReceiveJourney);
            }
            else
            {
                TReceiveJourney.Visible = false;
            }
            #endregion

            #region 导游安排服务标准
            if (!string.IsNullOrEmpty(planinfo.ServiceStandard))
            {
                TService.Visible = true;
                this.lbServiceStandard.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.ServiceStandard);
            }
            else
            {
                TService.Visible = false;
            }
            #endregion

            #region 导游须知
            if (!string.IsNullOrEmpty(planinfo.GuideNotes))
            {
                TGuideNote.Visible = true;
                this.lbGuidNote.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.GuideNotes);
            }
            else
            {
                this.TGuideNote.Visible = false;
            }
            #endregion

            #region  团队支付详单

            #region 导游

            IList<EyouSoft.Model.PlanStructure.MPlan> GuidePlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, null, null, false, null, TourId, PlanState.已落实);
            GuidePlanList = GetZhiChus(GuidePlanList);
            if (GuidePlanList != null && GuidePlanList.Count > 0)
            {
                this.guid = GuidePlanList.Count;
                this.rpt_guid.DataSource = GuidePlanList;
                this.rpt_guid.DataBind();
            }
            else
            {
                this.ph_guid.Visible = false;
            }

            #endregion

            #region 地接

            IList<EyouSoft.Model.PlanStructure.MPlan> groundPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, null, false, null, TourId, PlanState.已落实);
            groundPlanList = GetZhiChus(groundPlanList);
            if (groundPlanList != null && groundPlanList.Count > 0)
            {
                this.dijie = groundPlanList.Count;
                this.rpt_dijie.DataSource = groundPlanList;
                this.rpt_dijie.DataBind();
            }
            else
            {
                this.ph_dijie.Visible = false;
            }

            #endregion

            #region 飞机

            IList<EyouSoft.Model.PlanStructure.MPlan> phanePlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, null, null, false, null, TourId, PlanState.已落实);
            phanePlanList = GetZhiChus(phanePlanList);
            if (phanePlanList != null && phanePlanList.Count > 0)
            {
                this.plane = phanePlanList.Count;
                this.rpt_plane.DataSource = phanePlanList;
                this.rpt_plane.DataBind();
            }
            else
            {
                this.ph_plane.Visible = false;
            }

            #endregion

            #region 购物

            IList<EyouSoft.Model.PlanStructure.MPlan> shopPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, null, null, false, null, TourId, PlanState.已落实);
            shopPlanList = GetZhiChus(shopPlanList);
            if (shopPlanList != null && shopPlanList.Count > 0)
            {
                this.gouwu = shopPlanList.Count;
                this.rpt_gouwu.DataSource = shopPlanList;
                this.rpt_gouwu.DataBind();
            }
            else
            {
                this.ph_gouwu.Visible = false;
            }

            #endregion

            #region 国内游轮

            IList<EyouSoft.Model.PlanStructure.MPlan> InshipPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮, null, null, false, null, TourId, PlanState.已落实);
            InshipPlanList = GetZhiChus(InshipPlanList);
            if (InshipPlanList != null && InshipPlanList.Count > 0)
            {
                this.guoneichuan = InshipPlanList.Count;
                this.rpt_guoneichuan.DataSource = InshipPlanList;
                this.rpt_guoneichuan.DataBind();
            }
            else
            {
                this.ph_guoneichuan.Visible = false;
            }

            #endregion

            #region 涉外游轮

            IList<EyouSoft.Model.PlanStructure.MPlan> OutshipPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, null, null, false, null, TourId, PlanState.已落实);
            OutshipPlanList = GetZhiChus(OutshipPlanList);
            if (OutshipPlanList != null && OutshipPlanList.Count > 0)
            {
                this.shewaichuan = OutshipPlanList.Count;
                this.rpt_shewaichuan.DataSource = OutshipPlanList;
                this.rpt_shewaichuan.DataBind();
            }
            else
            {
                this.ph_shewaichuan.Visible = false;
            }

            #endregion

            #region 火车

            IList<EyouSoft.Model.PlanStructure.MPlan> trainPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, null, null, false, null, TourId, PlanState.已落实);
            trainPlanList = GetZhiChus(trainPlanList);
            if (trainPlanList != null && trainPlanList.Count > 0)
            {
                this.train = trainPlanList.Count;
                this.rpt_train.DataSource = trainPlanList;
                this.rpt_train.DataBind();
            }
            else
            {
                this.ph_train.Visible = false;
            }

            #endregion

            #region 景点

            IList<EyouSoft.Model.PlanStructure.MPlan> scenicPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null, null, false, null, TourId, PlanState.已落实);
            scenicPlanList = GetZhiChus(scenicPlanList);
            if (scenicPlanList != null && scenicPlanList.Count > 0)
            {
                this.jingdian = scenicPlanList.Count;
                this.rpt_jingdian.DataSource = scenicPlanList;
                this.rpt_jingdian.DataBind();
            }
            else
            {
                this.ph_jingdian.Visible = false;
            }

            #endregion

            #region 酒店

            IList<EyouSoft.Model.PlanStructure.MPlan> hotelPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, null, false, null, TourId, PlanState.已落实);
            hotelPlanList = GetZhiChus(hotelPlanList);
            if (hotelPlanList != null && hotelPlanList.Count > 0)
            {
                this.hotel = hotelPlanList.Count;
                this.rpt_hotellistk.DataSource = hotelPlanList;
                this.rpt_hotellistk.DataBind();
            }
            else
            {
                this.ph_hotel.Visible = false;
            }

            #endregion

            #region 领料

            IList<EyouSoft.Model.PlanStructure.MPlan> lingliaolPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, null, false, null, TourId, PlanState.已落实);
            lingliaolPlanList = GetZhiChus(lingliaolPlanList);
            if (lingliaolPlanList != null && lingliaolPlanList.Count > 0)
            {
                this.lingliao = lingliaolPlanList.Count;
                this.rpt_lingliao.DataSource = lingliaolPlanList;
                this.rpt_lingliao.DataBind();
            }
            else
            {
                this.ph_lingliao.Visible = false;
            }

            #endregion

            #region 其它

            IList<EyouSoft.Model.PlanStructure.MPlan> otherlPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, null, false, null, TourId, PlanState.已落实);
            otherlPlanList = GetZhiChus(otherlPlanList);
            if (otherlPlanList != null && otherlPlanList.Count > 0)
            {
                this.qita = otherlPlanList.Count;
                this.rpt_qita.DataSource = otherlPlanList;
                this.rpt_qita.DataBind();
            }
            else
            {
                this.ph_qita.Visible = false;
            }

            #endregion

            #region 汽车

            IList<EyouSoft.Model.PlanStructure.MPlan> busPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, null, null, false, null, TourId, PlanState.已落实);
            busPlanList = GetZhiChus(busPlanList);
            if (busPlanList != null && busPlanList.Count > 0)
            {
                this.bus = busPlanList.Count;
                this.rpt_bus.DataSource = busPlanList;
                this.rpt_bus.DataBind();
            }
            else
            {
                this.ph_bus.Visible = false;
            }

            #endregion

            #region 用餐

            IList<EyouSoft.Model.PlanStructure.MPlan> yongcanPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null, null, false, null, TourId, PlanState.已落实);
            yongcanPlanList = GetZhiChus(yongcanPlanList);
            if (yongcanPlanList != null && yongcanPlanList.Count > 0)
            {
                this.yongcan = yongcanPlanList.Count;
                this.rpt_yongcan.DataSource = yongcanPlanList;
                this.rpt_yongcan.DataBind();
            }
            else
            {
                this.ph_yongcan.Visible = false;
            }

            #endregion

            #region 用车

            IList<EyouSoft.Model.PlanStructure.MPlan> carPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null, null, false, null, TourId, PlanState.已落实);
            carPlanList = GetZhiChus(carPlanList);
            if (carPlanList != null && carPlanList.Count > 0)
            {
                this.chedui = carPlanList.Count;
                this.rpt_chedui.DataSource = carPlanList;
                this.rpt_chedui.DataBind();
            }
            else
            {
                this.ph_chedui.Visible = false;
            }

            #endregion

            #endregion
            #endregion
        }

        /// <summary>
        /// 初始化计调安排信息
        /// </summary>
        void InitAnPaiInfo()
        {
            if (string.IsNullOrEmpty(AnPaiId)) return;
            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, AnPaiId);
            if (info == null || string.IsNullOrEmpty(info.PlanId)) RCWE("异常请求");

            if (!string.IsNullOrEmpty(info.GuideNotes))
            {
                TGuideNote.Visible = true;
                lbGuidNote.Text = Utils.TextareaToHTML(info.GuideNotes);
            }
            else
            {
                TGuideNote.Visible = false;
            }

            if (!string.IsNullOrEmpty(info.ReceiveJourney))
            {
                TReceiveJourney.Visible = true;
                lbReceiveJourney.Text = Utils.TextareaToHTML(info.ReceiveJourney);
            }
            else
            {
                TReceiveJourney.Visible = false;
            }

            if (!string.IsNullOrEmpty(info.ServiceStandard))
            {
                TService.Visible = true;
                lbServiceStandard.Text = Utils.TextareaToHTML(info.ServiceStandard);
            }
            else
            {
                TService.Visible = false;
            }
        }

        /// <summary>
        /// 初始化支出合计
        /// </summary>
        void InitZhiChuHeJi()
        {
            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(TourId);

            if (info != null)
            {
                if (!IsJinDaoYouZhiFu)
                {
                    ltrZhiChuHeJi.Text = UtilsCommons.GetMoneyString(info.TourOutlay, ProviderToMoney);
                }
                else
                {
                    ltrZhiChuHeJi.Text = UtilsCommons.GetMoneyString(info.DaoYouZhiFuJinE, ProviderToMoney);
                }
            }
            else
            {
                ltrZhiChuHeJi.Text = UtilsCommons.GetMoneyString(0, ProviderToMoney);
            }
        }

        /// <summary>
        /// 获取支出信息集合，筛选导游收款
        /// </summary>
        /// <param name="items">支出集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.PlanStructure.MPlan> GetZhiChus(IList<EyouSoft.Model.PlanStructure.MPlan> items)
        {
            if (items == null || items.Count == 0) return null;

            if (!IsJinDaoYouZhiFu) return items;

            var _items = new List<EyouSoft.Model.PlanStructure.MPlan>();

            foreach (var item in items)
            {
                if (item.PaymentType == Payment.导游签单 || item.PaymentType == Payment.导游现付)
                {
                    _items.Add(item);
                }
            }

            return _items;
        }

        #region 前台调用方法
        protected string GetshipNum(object obj, string type)
        {
            EyouSoft.Model.PlanStructure.MPlanShip mplanship = (EyouSoft.Model.PlanStructure.MPlanShip)obj;
            if (mplanship != null && mplanship.PlanShipPriceList != null && mplanship.PlanShipPriceList.Count > 0)
            {
                if (type == "adult")
                {
                    return mplanship.PlanShipPriceList[0].AdultNumber.ToString();
                }
                else
                {
                    return mplanship.PlanShipPriceList[0].ChildNumber.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取大交通的出发时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetDepartureTime(object obj)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanLargeTime> LargeTime = (IList<EyouSoft.Model.PlanStructure.MPlanLargeTime>)obj;
            if (LargeTime != null && LargeTime.Count > 0)
            {
                return UtilsCommons.SetDateTimeFormart(LargeTime[0].DepartureTime);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取订单第一个游客信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        protected string GetYouKeXinXi(object orderId)
        {
            string s = string.Empty;
            if (orderId == null) return string.Empty;
            string _orderId = orderId.ToString();
            if (string.IsNullOrEmpty(_orderId)) return string.Empty;

            var items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderTravellerByOrderId(_orderId);

            if (items != null && items.Count > 0)
            {
                s = items[0].CnName + " " + items[0].Contact;
            }

            return s;
        }
        #endregion
    }
}
