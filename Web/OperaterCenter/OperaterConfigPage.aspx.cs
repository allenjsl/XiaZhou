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
    /// 计调配置页面
    /// 创建人：李晓欢 创建时间:2012-03-13
    /// </summary>
    public partial class OperaterConfigPage : EyouSoft.Common.Page.BackPage
    {
        //需要安排计调项
        protected string strOperaterPlanHtml = string.Empty;
        //散客报价标准
        protected System.Text.StringBuilder TourPriceStandardHtml = new System.Text.StringBuilder();
        //出团时间处理
        protected DateTime dt = DateTime.Now;
        //团队确认单
        protected string OrderUrl = string.Empty;
        protected int TourType = 0;
        protected int BaoJiaType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //团队确认单
            OrderUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            
            if (!IsPostBack)
            {
                this.OperaterMenu1.IndexClass = "1";
                OperaterMenu1.CompanyId = SiteUserInfo.CompanyId;

                string tourId = Utils.GetQueryStringValue("tourId");
                if (!string.IsNullOrEmpty(tourId))
                {
                    DataInitTourInfo(tourId);
                    BindOrderList(tourId);
                }
            }
        }

        #region 初始化团队信息
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="tourID"></param>
        protected void DataInitTourInfo(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourInfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(tourID);
                if (tourInfo != null)
                {
                    TourType = (int)tourInfo.TourType;
                    this.litTourCode.Text = tourInfo.TourCode;
                    EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(tourInfo.AreaId, SiteUserInfo.CompanyId);
                    if (AreaModel != null)
                    {
                        this.litAreaName.Text = AreaModel.AreaName;
                    }
                    AreaModel = null;

                    this.litRouteName.Text = tourInfo.RouteName;
                    this.litDays.Text = tourInfo.TourDays.ToString();
                    this.litStartDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                    this.litEndDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.RDate, ProviderToDate);
                   
                    //人数
                    this.litPeoples.Text = tourInfo.PlanPeopleNumber.ToString();
                    //导游人数
                    if (tourInfo.GuideList != null && tourInfo.GuideList.Count > 0)
                    {
                        this.litGuidNames.Text = UtilsCommons.PingGuide(tourInfo.GuideList);
                    }
                    //需安排计调项
                    if (tourInfo.TourPlanItem != null && tourInfo.TourPlanItem.Count > 0)
                    {
                        for (int i = 0; i < tourInfo.TourPlanItem.Count; i++)
                        {
                            if (i == tourInfo.TourPlanItem.Count - 1)
                            {
                                strOperaterPlanHtml += "" + tourInfo.TourPlanItem[i].PlanType.ToString();
                            }
                            else
                            {
                                strOperaterPlanHtml += "" + tourInfo.TourPlanItem[i].PlanType.ToString() + "、";
                            }
                        }
                    }
                    else
                    {
                        this.planItemView.Visible = false;
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

                    if (tourInfo.TourService != null)
                    {
                        //内部信息
                        this.LitInterInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.InsiderInfor);
                        //服务标准
                        //this.litCostAccount.Text = tourInfo.TourService.ServiceStandard;
                        //不含项目
                        this.litObjectItem.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.NoNeedItem);
                        //购物安排
                        this.litShoppPlan.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.ShoppingItem);
                        //儿童安排
                        this.litChildrenPlan.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.ChildServiceItem);
                        //自费项目
                        this.litExpenceObj.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.OwnExpense);
                        //注意事项
                        this.litAttenTion.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.NeedAttention);
                        //温馨提醒
                        this.litWenxinTix.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.TourService.WarmRemind);
                    }
                    //成本核算
                    this.litCostCalculation.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(tourInfo.CostCalculation);
                    //行程安排
                    this.dt = tourInfo.LDate.HasValue ? tourInfo.LDate.Value : DateTime.Now;
                    if (tourInfo.TourPlan != null && tourInfo.TourPlan.Count > 0)
                    {
                        this.repSchedulePlan.DataSource = tourInfo.TourPlan;
                        this.repSchedulePlan.DataBind();
                    }
                    //团队类型 散拼 组团
                    if (tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼)
                    {
                        //价格组成
                        EyouSoft.Model.TourStructure.MTourSanPinInfo SanpinInfo = (EyouSoft.Model.TourStructure.MTourSanPinInfo)tourInfo;
                        if (SanpinInfo != null)
                        {
                            if (SanpinInfo.MTourPriceStandard != null && SanpinInfo.MTourPriceStandard.Count > 0)
                            {
                                //客户等级列
                                System.Text.StringBuilder priceLeaveCols = new System.Text.StringBuilder();
                                priceLeaveCols.Append("<tr>");
                                //标价标准表头
                                TourPriceStandardHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"jd-table01\" style=\"border-bottom: 1px solid #A9D7EC;\">");
                                TourPriceStandardHtml.Append("<tr><th width=\"100\" rowspan=\"2\" align=\"center\" class=\"border-l\">标准</th>");

                                if (SanpinInfo.MTourPriceStandard[0].PriceLevel != null && SanpinInfo.MTourPriceStandard[0].PriceLevel.Count > 0)
                                {
                                    for (int i = 0; i < SanpinInfo.MTourPriceStandard[0].PriceLevel.Count; i++)
                                    {
                                        TourPriceStandardHtml.Append("<th colspan=\"2\" align=\"center\">" + SanpinInfo.MTourPriceStandard[0].PriceLevel[i].LevelName + "</th>");

                                        priceLeaveCols.Append("<th align=\"center\">成人</th><th align=\"center\">儿童</th>");
                                    }
                                    TourPriceStandardHtml.Append("</tr>");
                                    priceLeaveCols.Append("</tr>");
                                }
                                TourPriceStandardHtml.Append("" + priceLeaveCols.ToString() + "");

                                //报价标准价格
                                for (int j = 0; j < SanpinInfo.MTourPriceStandard.Count; j++)
                                {
                                    TourPriceStandardHtml.Append("<tr><td align=\"center\" class=\"border-l\">" + SanpinInfo.MTourPriceStandard[j].StandardName + "</td>");
                                    for (int k = 0; k < SanpinInfo.MTourPriceStandard[j].PriceLevel.Count; k++)
                                    {
                                        TourPriceStandardHtml.Append("<th align=\"center\">" + Utils.FilterEndOfTheZeroDecimal(SanpinInfo.MTourPriceStandard[j].PriceLevel[k].AdultPrice) + "</th><th align=\"center\">" + Utils.FilterEndOfTheZeroDecimal(SanpinInfo.MTourPriceStandard[j].PriceLevel[k].ChildPrice) + "</th>");
                                    }
                                }
                                TourPriceStandardHtml.Append("</tr></table>");
                            }

                            if (SanpinInfo.TourService != null)
                            {
                                litCostAccount.Text = SanpinInfo.TourService.ServiceStandard;
                                phFWBZ.Visible = true;
                            }
                        }
                        SanpinInfo = null;
                    }
                    if (tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 || tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队)
                    {
                        //对外报价 整团 分项
                        EyouSoft.Model.TourStructure.MTourTeamInfo TeamInfo = (EyouSoft.Model.TourStructure.MTourTeamInfo)tourInfo;
                        if (TeamInfo != null)
                        {
                            if (TeamInfo.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                            {
                                //分项报价
                                if (TeamInfo.TourTeamPrice != null && TeamInfo.TourTeamPrice.Count > 0)
                                {
                                    this.repQuoteList.DataSource = TeamInfo.TourTeamPrice;
                                    this.repQuoteList.DataBind();
                                }                                
                            }
                            else
                            {
                                this.litServerStandard.Text = TeamInfo.TourService != null ? EyouSoft.Common.Function.StringValidate.TextToHtml(TeamInfo.TourService.ServiceStandard) : "";
                            }
                            BaoJiaType = (int)TeamInfo.OutQuoteType;
                            //成人价
                            this.litAdultPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(TeamInfo.AdultPrice, ProviderToMoney);
                            //儿童价
                            this.litChilrenPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(TeamInfo.ChildPrice, ProviderToMoney);
                            //其它费用
                            this.litOtherPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(TeamInfo.OtherCost, ProviderToMoney);
                            //合计费用
                            this.litAccountPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(TeamInfo.SumPrice, ProviderToMoney);
                        }
                        TeamInfo = null;
                    }

                }
                tourInfo = null;
            }
        }
        #endregion

        #region 行程景点
        protected string GetTourPlanSpot(object list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MTourPlanSpot> SpotList = (List<EyouSoft.Model.TourStructure.MTourPlanSpot>)list;
            if (SpotList != null && SpotList.Count > 0)
            {
                for (int i = 0; i < SpotList.Count; i++)
                {
                    if (i == SpotList.Count - 1)
                    {
                        if (SpotList[i].SpotName != "")
                        {
                            sb.Append("" + SpotList[i].SpotName + "");
                        }
                    }
                    else
                    {
                        if (SpotList[i].SpotName != "")
                        {
                            sb.Append("" + SpotList[i].SpotName + ",");
                        }
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(Utils.GetQueryStringValue("tourid"));

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //this.holerView1.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排酒店);
                    //this.holerView2.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用车);
                    //this.holerView3.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排景点);
                    //this.holerView4.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排游轮);
                    //this.holerView5.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排地接);                    
                    //this.holerView6.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用餐);
                    //this.holerView7.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排购物);
                    //this.holerView8.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排领料);
                    //this.holerView9.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排大交通);
                    //this.holerView10.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排导游);
                    //this.holerView11.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排其它);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //this.holerView1.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排酒店);
                    //this.holerView2.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用车);
                    //this.holerView3.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排景点);
                    //this.holerView4.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排游轮);
                    //this.holerView5.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排地接);
                    //this.holerView6.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用餐);
                    //this.holerView7.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排购物);
                    //this.holerView8.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排领料);
                    //this.holerView9.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排大交通);
                    //this.holerView10.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排导游);
                    //this.holerView11.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排其它);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //this.holerView1.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排酒店);
                    //this.holerView2.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用车);
                    //this.holerView3.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排景点);
                    //this.holerView4.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排游轮);
                    //this.holerView5.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排地接);
                    //this.holerView6.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用餐);
                    //this.holerView7.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排购物);
                    //this.holerView8.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排领料);
                    //this.holerView9.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排大交通);
                    //this.holerView10.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排导游);
                    //this.holerView11.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排其它);
                    break;
            }
        }
        #endregion

        #region 订单列表
        /// <summary>
        /// 获取某个团下面的订单数
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void BindOrderList(string tourID)
        {
            EyouSoft.Model.TourStructure.MOrderSum orders = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> items = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(tourID, ref orders);
            if (items != null && items.Count > 0) items = items.Where(c => c.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交).ToList();

            if (items != null && items.Count > 0)
            {
                phEmptyDingDan.Visible = false;

                repTourOrderList.DataSource = items;
                repTourOrderList.DataBind();
            }
            else
            {
                phEmptyDingDan.Visible = true;
            }
        }

        /// <summary>
        /// 获取订单报价信息
        /// </summary>
        /// <param name="tourType">团队类型</param>
        /// <param name="keHuLevName">客户等级名称</param>
        /// <param name="baoJiaBiaoZhunName">报价标准名称</param>
        /// <returns></returns>
        protected string GetBaoJiaInfo(object tourType, object keHuLevName, object baoJiaBiaoZhunName)
        {
            string s = string.Empty;
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;

            if (_tourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 
                || _tourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 
                || _tourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 
                || _tourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
            {
                if (baoJiaBiaoZhunName != null) s += "标准：" + baoJiaBiaoZhunName.ToString()+"&nbsp;";
                if (keHuLevName != null) s += "等级：" + keHuLevName.ToString();
            }

            return s;
        }
        #endregion

    }
}
