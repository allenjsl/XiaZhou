using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调安排—全局计调
    /// 创建人：李晓欢
    /// 创建时间：2011-09-26
    /// </summary>
    public partial class Operaterglobal : EyouSoft.Common.Page.BackPage
    {
        //计调项数据列表标识
        protected string PlanItemsDataListIsEnpty = string.Empty;

        //登录人
        protected string UserId = string.Empty;
        //是否是团队计调员
        protected bool ret = false;
        //地接社确认单
        protected string dijieshePrintUrl = string.Empty;
        //导游任务单
        protected string daoyouPrintUrl = string.Empty;
        //酒店确认单
        protected string hotelPrintUrl = string.Empty;
        //车队行程单
        protected string carPrintUrl = string.Empty;
        //机票订单 //火车票订单//汽车票订单
        protected string querenAirUrl = string.Empty;
        protected string querenTrainUrl = string.Empty;
        protected string querenBusUrl = string.Empty;
        //景点通知单
        protected string jingdianPrintUrl = string.Empty;
        //游船预控确认单
        protected string querenforeirnUrl = string.Empty;
        protected string querenchinaUrl = string.Empty;
        //用餐
        protected string yongcanPrintUrl = string.Empty;
        //购物
        protected string gouwuPrintUrl = string.Empty;
        //其它
        protected string qitaPrintUrl = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            UserId = this.SiteUserInfo.UserId;

            //初始化菜单控件css
            this.OperaterMenu1.IndexClass = "2";
            OperaterMenu1.CompanyId = SiteUserInfo.CompanyId;

            #region 确认单
            dijieshePrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.地接确认单);

            daoyouPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.导游任务单);

            hotelPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.酒店确认单);

            carPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.用车确认单);

            querenAirUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.机票确认单);

            querenTrainUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.火车确认单);

            querenBusUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.汽车确认单);

            jingdianPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.景点确认单);

            querenforeirnUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.涉外游轮确认单);
            querenchinaUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.国内游轮确认单);

            yongcanPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订餐确认单);

            gouwuPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.购物确认单);

            qitaPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.其它安排确认单);
            #endregion

            #region 处理AJAX请求
            //获取ajax请求 删除 修改 保存
            string doType = Utils.GetQueryStringValue("action");
            string planID = Utils.GetQueryStringValue("planID");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "config":
                        Response.Clear();
                        Response.Write(GlobalOpConfig(Utils.GetQueryStringValue("tourId")));
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(DeletePlanByID(planID));
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion

            //团号
            string tourId = Utils.GetQueryStringValue("tourId");
            DataBindPlanList(tourId);
            DataInit(tourId);
        }

        #region 初始化
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourInfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(tourID);
                if (tourInfo != null)
                {
                    this.litTourCode.Text = tourInfo.TourCode;
                    if (!string.IsNullOrEmpty(tourInfo.AreaId.ToString()))
                    {
                        EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(tourInfo.AreaId, SiteUserInfo.CompanyId);
                        if (AreaModel != null)
                        {
                            this.litAreaName.Text = AreaModel.AreaName;
                        }
                        AreaModel = null;
                    }
                    this.litRouteName.Text = tourInfo.RouteName;
                    this.litDays.Text = tourInfo.TourDays.ToString();
                    this.litStartDate.Text = UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                    this.litPeoples.Text = tourInfo.PlanPeopleNumber.ToString();
                    this.litEndDate.Text = UtilsCommons.GetDateString(tourInfo.RDate, ProviderToDate);
                    //带团导游
                    if (tourInfo.GuideList != null && tourInfo.GuideList.Count > 0)
                    {
                        this.litGuidNames.Text = UtilsCommons.PingGuide(tourInfo.GuideList);
                    }

                    //销售员
                    if (tourInfo.SaleInfo != null)
                    {
                        this.litSellers.Text = tourInfo.SaleInfo.Name;
                    }
                    //计调员
                    if (tourInfo.TourPlaner != null && tourInfo.TourPlaner.Count > 0)
                    {
                        this.litOperaters.Text = UtilsCommons.PingPlaner(tourInfo.TourPlaner);
                    }
                    //计调项
                    if (tourInfo.TourPlanItem != null && tourInfo.TourPlanItem.Count > 0)
                    {
                        for (int i = 0; i < tourInfo.TourPlanItem.Count; i++)
                        {
                            if (i == tourInfo.TourPlanItem.Count - 1)
                            {
                                this.litPlanItems.Text += "" + tourInfo.TourPlanItem[i].PlanType.ToString() + "";
                            }
                            else
                            {
                                this.litPlanItems.Text += "" + tourInfo.TourPlanItem[i].PlanType.ToString() + ",";
                            }
                        }
                    }

                    //团队状态
                    if (ret)
                    {
                        this.BtnglobalAction.Visible = !EyouSoft.Common.UtilsCommons.GetTourStatusByTourID(SiteUserInfo.CompanyId, tourID);
                    }
                    else
                    {
                        this.BtnglobalAction.Visible = false;
                    }

                    if (tourInfo.TourService != null)
                    {
                        ltrNeiBuXinXi.Text = tourInfo.TourService.InsiderInfor;
                    }
                    ltrChengBenHeSuan.Text = tourInfo.CostCalculation;
                }
            }
        }
        #endregion

        #region 绑定安排的计调项列表
        /// <summary>
        /// 绑定安排的计调项列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataBindPlanList(string tourID)
        {

            ListPower = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourID, SiteUserInfo.UserId);
            //地接
            IList<EyouSoft.Model.PlanStructure.MPlan> listAyency = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listAyency != null && listAyency.Count > 0)
            {
                this.tabAyencyView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接 + ",";
                this.repAyencyList.DataSource = listAyency;
                this.repAyencyList.DataBind();
            }


            //导游
            IList<EyouSoft.Model.PlanStructure.MPlan> listGuid = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listGuid != null && listGuid.Count > 0)
            {
                this.tabGuidView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游 + ",";
                this.repGuidList.DataSource = listGuid;
                this.repGuidList.DataBind();
            }


            //酒店
            IList<EyouSoft.Model.PlanStructure.MPlan> listHotel = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listHotel != null && listHotel.Count > 0)
            {
                this.tabHotelView.Visible = true;
                this.repHotelList.DataSource = listHotel;
                this.repHotelList.DataBind();
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 + ",";
            }

            //车队 
            IList<EyouSoft.Model.PlanStructure.MPlan> listCars = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listCars != null && listCars.Count > 0)
            {
                this.tabCarView.Visible = true;
                this.repCarlist.DataSource = listCars;
                this.repCarlist.DataBind();
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车 + ",";
            }

            //机票
            IList<EyouSoft.Model.PlanStructure.MPlan> listAirs = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listAirs != null && listAirs.Count > 0)
            {
                this.tabAirView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机 + ",";
                this.repAirList.DataSource = listAirs;
                this.repAirList.DataBind();
            }

            //火车
            IList<EyouSoft.Model.PlanStructure.MPlan> listTrains = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listTrains != null && listTrains.Count > 0)
            {
                this.tabTrainView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车 + ",";
                this.reptrainList.DataSource = listTrains;
                this.reptrainList.DataBind();
            }

            //汽车
            IList<EyouSoft.Model.PlanStructure.MPlan> listBus = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listBus != null && listBus.Count > 0)
            {
                this.tabBusView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车 + ",";
                this.repBusList.DataSource = listBus;
                this.repBusList.DataBind();
            }

            //景点
            IList<EyouSoft.Model.PlanStructure.MPlan> listAtts = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listAtts != null && listAtts.Count > 0)
            {
                this.tabAttrView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 + ",";
                this.repAttrList.DataSource = listAtts;
                this.repAttrList.DataBind();
            }

            //涉外游轮 国内游轮
            IList<EyouSoft.Model.PlanStructure.MPlan> listShipForeign = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listShipForeign != null && listShipForeign.Count > 0)
            {
                this.tabForeignShipView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮 + ",";
                this.repForeignShipList.DataSource = listShipForeign;
                this.repForeignShipList.DataBind();
            }

            IList<EyouSoft.Model.PlanStructure.MPlan> listShipChina = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listShipChina != null && listShipChina.Count > 0)
            {
                this.tabShipChinaView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 + ",";
                this.repChinaShipList.DataSource = listShipChina;
                this.repChinaShipList.DataBind();
            }

            //用餐 
            IList<EyouSoft.Model.PlanStructure.MPlan> listDin = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listDin != null && listDin.Count > 0)
            {
                this.tabDinView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐 + ",";
                this.repDinList.DataSource = listDin;
                this.repDinList.DataBind();
            }

            //购物
            IList<EyouSoft.Model.PlanStructure.MPlan> listShop = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listShop != null && listShop.Count > 0)
            {
                this.tabShopView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物 + ",";
                this.repShopList.DataSource = listShop;
                this.repShopList.DataBind();
            }

            //领料
            IList<EyouSoft.Model.PlanStructure.MPlan> listPick = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listPick != null && listPick.Count > 0)
            {
                this.tabPickView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料 + ",";
                this.repPickList.DataSource = listPick;
                this.repPickList.DataBind();
            }

            //其它 
            IList<EyouSoft.Model.PlanStructure.MPlan> listOther = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourID);
            if (listOther != null && listOther.Count > 0)
            {
                this.tabOtherView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它 + ",";
                this.repOtherList.DataSource = listOther;
                this.repOtherList.DataBind();
            }

            //当所有安排的计调项数据列表都为null的时候，就隐藏全部配置按钮
            if (PlanItemsDataListIsEnpty != "")
            {
                this.BtnglobalAction.Visible = true;
            }
            else
            {
                this.BtnglobalAction.Visible = false;
            }
        }
        #endregion

        #region 全局计调配置
        /// <summary>
        /// 全局计调配置
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <param name="tourStatus">团队状态</param>
        /// <returns></returns>
        protected string GlobalOpConfig(string tourID)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(tourID))
            {
                bool ret = new EyouSoft.BLL.PlanStructure.BPlan().IsExist(tourID);
                if (ret)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "还有未落实的计调项，请落实！");
                }
                else
                {
                    EyouSoft.Model.TourStructure.MTourStatusChange statusChangeModel = new EyouSoft.Model.TourStructure.MTourStatusChange();
                    statusChangeModel.CompanyId = this.SiteUserInfo.CompanyId;
                    statusChangeModel.DeptId = this.SiteUserInfo.DeptId;
                    statusChangeModel.IssueTime = System.DateTime.Now;
                    statusChangeModel.Operator = this.SiteUserInfo.Name;
                    statusChangeModel.OperatorId = this.SiteUserInfo.UserId;
                    statusChangeModel.TourId = tourID;
                    statusChangeModel.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕;
                    bool result = new EyouSoft.BLL.TourStructure.BTour().UpdateTourStatus(statusChangeModel);
                    if (result)
                    {
                        msg = UtilsCommons.AjaxReturnJson("1", "配置成功！");
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "配置失败！");
                    }
                }
            }
            return msg;
        }
        #endregion

        #region 删除计调项
        /// <summary>
        /// 删除计调项
        /// </summary>
        /// <param name="planID">计调id</param>
        /// <returns></returns>
        protected string DeletePlanByID(string planID)
        {
            string setMsg = string.Empty;
            if (!string.IsNullOrEmpty(planID))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planID))
                {
                    setMsg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    setMsg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return setMsg;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            ret = UtilsCommons.GetTourPlanItemBytourID(tourId, this.SiteUserInfo.UserId);
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(tourId);

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_计调配置完毕))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_计调配置完毕, false);
                        return;
                    }
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_计调配置完毕))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_计调配置完毕, false);
                        return;
                    }
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_计调配置完毕))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_计调配置完毕, false);
                        return;
                    }
                    break;
            }

        }
        #endregion
    }
}
