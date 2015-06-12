using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 订单信息汇总表
    /// 方琪 2012-05-15
    /// </summary>
    public partial class dingdanxinxihuizongbiao : System.Web.UI.Page
    {
        protected string url = "";
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (_IsLogin)
            {
                string type = Utils.GetQueryStringValue("type");
                if (!string.IsNullOrEmpty(tourId) && SiteUserInfo != null)
                {
                    InitPage(tourId, type);
                }
                this.Title = PrintTemplateType.订单信息汇总表.ToString();
                url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, PrintTemplateType.游客名单);
                url += "?tourId=" + tourId + "&type=" + type;
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="tourId"></param>
        protected void InitPage(string tourId, string type)
        {
            EyouSoft.BLL.TourStructure.BTour BTour = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo model = BTour.GetTourInfo(tourId);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
            }
            EyouSoft.BLL.TourStructure.BTourOrder BLL = new EyouSoft.BLL.TourStructure.BTourOrder();
            IList<EyouSoft.Model.TourStructure.MTourOrderSummary> list = BLL.GetTourOrderSummaryByTourId(tourId);
            if (list != null && list.Count > 0)
            {
                if (type == "1")
                {
                    rpt_OrderList.DataSource = list.Where(i => i.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位);
                }
                else
                {
                    rpt_OrderList.DataSource = list.Where(i => i.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交);
                }
                rpt_OrderList.DataBind();
            }
        }

        /// <summary>
        /// 嵌套repeater绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_OrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater BackList = (Repeater)e.Item.FindControl("rpt_BackList");
                Repeater CustomerList = (Repeater)e.Item.FindControl("rpt_CustomerList");
                MTourOrderSummary model = ((MTourOrderSummary)e.Item.DataItem) ?? new MTourOrderSummary();

                IList<MTourOrderSales> list = model.TourOrderSalesList;
                if (model.TourOrderTravellerList != null)
                {
                    IList<MTourOrderTraveller> ls = new List<MTourOrderTraveller>();
                    ls = model.TourOrderTravellerList.Where(item => item.TravellerStatus == TravellerStatus.在团).ToList();
                    if (ls != null && ls.Count > 0)
                    {
                        ls = null;
                        ls = new List<MTourOrderTraveller>();
                        ls.Add(model.TourOrderTravellerList.Where(item => item.TravellerStatus == TravellerStatus.在团).ToList().First());
                    }

                    CustomerList.DataSource = ls;
                    CustomerList.DataBind();
                }
                if (list != null && list.Count > 0)
                {
                    BackList.DataSource = list;
                    BackList.DataBind();
                }

                Literal ltrXiaoShouZengJian = (Literal)e.Item.FindControl("ltrXiaoShouZengJian");

                if (model.SaleAddCost != 0 || !string.IsNullOrEmpty(model.SaleAddCostRemark) || model.SaleReduceCost != 0 || !string.IsNullOrEmpty(model.SaleReduceCostRemark))
                {
                    string s = string.Empty;
                    if (model.SaleAddCost != 0 || !string.IsNullOrEmpty(model.SaleAddCostRemark))
                    {
                        s += string.Format("<tr><th align='right'>销售增加</th><td>{0}</td><th align='right'>备注：</th><td>{1}</td></tr>", EyouSoft.Common.UtilsCommons.GetMoneyString(model.SaleAddCost, ProviderToMoney), model.SaleAddCostRemark);
                    }
                    if (model.SaleReduceCost != 0 || !string.IsNullOrEmpty(model.SaleReduceCostRemark))
                    {
                        s += string.Format("<tr><th align='right'>销售减少</th><td>{0}</td><th align='right'>备注：</th><td>{1}</td></tr>", EyouSoft.Common.UtilsCommons.GetMoneyString(model.SaleReduceCost, ProviderToMoney), model.SaleReduceCostRemark);
                    }
                    ltrXiaoShouZengJian.Text = s;
                }

                Literal ltrBianGengZengJian = (Literal)e.Item.FindControl("ltrBianGengZengJian");

                if (model.SumPriceAddCost != 0 || !string.IsNullOrEmpty(model.SumPriceAddCostRemark) || model.SumPriceReduceCost != 0 || !string.IsNullOrEmpty(model.SumPriceReduceCostRemark))
                {
                    string s = string.Empty;
                    if (model.SumPriceAddCost != 0 || !string.IsNullOrEmpty(model.SumPriceAddCostRemark))
                    {
                        s += string.Format("<tr><th align='right'>变更增加</th><td>{0}</td><th align='right'>备注：</th><td>{1}</td></tr>", EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPriceAddCost, ProviderToMoney), model.SumPriceAddCostRemark);
                    }
                    if (model.SumPriceReduceCost != 0 || !string.IsNullOrEmpty(model.SumPriceReduceCostRemark))
                    {
                        s += string.Format("<tr><th align='right'>变更减少</th><td>{0}</td><th align='right'>备注：</th><td>{1}</td></tr>", EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPriceReduceCost, ProviderToMoney), model.SumPriceReduceCostRemark);
                    }
                    ltrBianGengZengJian.Text = s;
                }

                Literal ltrDingDanBeiZhu = (Literal)e.Item.FindControl("ltrDingDanBeiZhu");
                if (!string.IsNullOrEmpty(model.OrderRemark))
                {
                    ltrDingDanBeiZhu.Text = string.Format("<tr><th align='right'>订单备注</th><td colspan='3'>{0}</td></tr>", EyouSoft.Common.Function.StringValidate.TextToHtml(model.OrderRemark));
                }
            }
        }
    }
}
