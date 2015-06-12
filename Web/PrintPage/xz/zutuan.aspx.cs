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
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 团队行程单
    /// 创建人：刘飞
    /// 时间：2012-5-15
    /// </summary>
    public partial class zutuan : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //团队编号
            string tourid = EyouSoft.Common.Utils.GetQueryStringValue("tourId");
            //报价编号
            string quoteid = EyouSoft.Common.Utils.GetQueryStringValue("quoteId");
            //
            string type = EyouSoft.Common.Utils.GetQueryStringValue("type");
            this.Title = "团队行程单";
            if (!IsPostBack)
            {
                PageInit(tourid, quoteid, type);
            }
        }
        private void PageInit(string tourid, string quoteid, string type)
        {
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            /*this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;*/

            //派团计划实体
            if (!string.IsNullOrEmpty(tourid))
            {

                EyouSoft.Model.TourStructure.MTourTeamInfo model = null;
                EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
                EyouSoft.Model.EnumType.TourStructure.TourType tourtype = bll.GetTourType(tourid);
                switch (tourtype)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                        //跳转到散拼
                        EyouSoft.BLL.ComStructure.BComSetting bcom = new EyouSoft.BLL.ComStructure.BComSetting();

                        Response.Redirect(bcom.GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单) + "?tourId=" + tourid);
                        break;
                    case EyouSoft.Model.EnumType.TourStructure.TourType.单项服务:
                        this.TAll.Visible = false;
                        this.TChildren.Visible = false;
                        this.TNeedAttention.Visible = false;
                        this.TNoService.Visible = false;
                        this.TOption.Visible = false;
                        this.TPlanFeature.Visible = false;
                        this.TSelfProject.Visible = false;
                        this.TService.Visible = false;
                        this.TShopping.Visible = false;
                        this.TTourPrice.Visible = false;
                        this.TWarmRemind.Visible = false;
                        return;
                }
                if (type == "")
                {
                    model = (EyouSoft.Model.TourStructure.MTourTeamInfo)bll.GetTourInfo(tourid);
                }
                else
                {
                    model = (EyouSoft.Model.TourStructure.MTourTeamInfo)bll.GetOldTourInfo(tourid, this.SiteUserInfo.CompanyId);
                }
                if (model != null)
                {
                    this.lbTourCode.Text = model.TourCode;
                    this.lbPeoNum.Text = model.Adults.ToString() + "<sup>+" + model.Childs.ToString() + "</sup>";
                    this.lbAdultsNum.Text = model.Adults.ToString() + "人";
                    this.lbChildsNum.Text = model.Childs.ToString() + "人";
                    this.lbRouteName.Text = model.RouteName;
                    if (model.CompanyInfo != null)
                    {
                        this.txtunitname.Text = model.CompanyInfo.CompanyName;
                        this.txtunitContactname.Text = model.CompanyInfo.Contact;
                        this.txtunittel.Text = model.CompanyInfo.Phone;
                    }

                    #region 行程
                    IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> planinfo = model.TourPlan.OrderBy(m => m.Days).ToList();
                    if (planinfo != null && planinfo.Count > 0)
                    {
                        StringBuilder strAllDateInfo = new StringBuilder();
                        string Dinner = string.Empty;//包餐(早、中、晚)
                        foreach (EyouSoft.Model.TourStructure.MPlanBaseInfo Plan in planinfo)
                        {
                            if (Plan.Breakfast) { Dinner += "早、"; }
                            if (Plan.Lunch) { Dinner += "中、"; }
                            if (Plan.Supper) { Dinner += "晚、"; }

                            string riQi = "第" + Plan.Days + "天&nbsp;";
                            if (model.LDate.HasValue)
                            {
                                riQi += model.LDate.Value.AddDays(Plan.Days - 1).ToString("yyyy-MM-dd");
                            }

                            strAllDateInfo.AppendFormat("<table width='696' border='0' align='center' cellpadding='0' cellspacing='0' class='borderline_2'><tr><td width='35%' class='small_title'><b class='font16'>{0}  {6}</b></td><td width='15%' class='small_title'><b class='font14'>交通：{1}</b></td><td width='20%' class='small_title'><b class='font14'>餐：{2}</b></td><td width='30%' class='small_title'><b class='font14'>住宿：{3}</b></td></tr></table><table width='696' border='0' align='center' cellpadding='0' cellspacing='0' style='margin-top:0px' class='list_2'><tr><td class='td_text' style='border-top:none;' width='{7}'>{4}</td>{5}</tr></table>", riQi, Plan.Traffic, Dinner, Plan.Hotel, Plan.Content, string.IsNullOrEmpty(Plan.FilePath) ? "" : "<td style='border-top:none;'><img src='http://" + Request.Url.Authority + Plan.FilePath + "' width='202' height='163' /></td>", Plan.Section, string.IsNullOrEmpty(Plan.FilePath) ? "100%" : "480px");
                            Dinner = string.Empty;
                        }
                        this.lbtourplan.Text = strAllDateInfo.ToString();
                    }
                    #endregion

                    #region 线路特色
                    if (string.IsNullOrEmpty(model.PlanFeature))
                    {
                        this.TPlanFeature.Visible = false;
                    }
                    else
                    {
                        this.lbPlanFeature.Text = model.PlanFeature;
                    }
                    #endregion

                    #region 计划服务
                    if (model.TourService != null)
                    {
                        #region 服务标准
                        /*if (string.IsNullOrEmpty(model.TourService.ServiceStandard))
                        {
                            this.TService.Visible = false;
                        }
                        else
                        {
                            this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ServiceStandard);
                        }*/
                        TService.Visible = false;
                        #endregion

                        #region 服务不含
                        if (string.IsNullOrEmpty(model.TourService.NoNeedItem))
                        {
                            this.TNoService.Visible = false;
                        }
                        else
                        {
                            this.lbnoService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NoNeedItem);
                        }
                        #endregion

                        #region 购物安排
                        if (string.IsNullOrEmpty(model.TourService.ShoppingItem))
                        {
                            this.TShopping.Visible = false;
                        }
                        else
                        {
                            this.lbshopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ShoppingItem);
                        }
                        #endregion

                        #region 儿童安排
                        if (string.IsNullOrEmpty(model.TourService.ChildServiceItem))
                        {
                            this.TChildren.Visible = false;
                        }
                        else
                        {
                            this.lbchildren.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ChildServiceItem);
                        }
                        #endregion

                        #region 自费项目
                        if (string.IsNullOrEmpty(model.TourService.OwnExpense))
                        {
                            this.TSelfProject.Visible = false;
                        }
                        else
                        {
                            this.lbselfproject.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.OwnExpense);
                        }
                        #endregion

                        #region 温馨提醒
                        if (string.IsNullOrEmpty(model.TourService.WarmRemind))
                        {
                            this.TWarmRemind.Visible = false;
                        }
                        else
                        {
                            this.lbwarmremind.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.WarmRemind);
                        }
                        #endregion

                        #region 注意事项
                        if (string.IsNullOrEmpty(model.TourService.NeedAttention))
                        {
                            this.TNeedAttention.Visible = false;
                        }
                        else
                        {
                            this.lbneedattention.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NeedAttention);
                        }
                        #endregion
                    }
                    else
                    {
                        this.TPlanService.Visible = false;
                    }
                    #endregion

                    #region 服务标准
                    if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                    {
                        this.TOption.Visible = false;
                        if (model.TourService != null)
                        {
                            this.lballservice.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ServiceStandard);
                        }
                    }
                    else
                    {
                        this.TAll.Visible = false;
                        IList<MTourTeamPrice> TeamPrice = model.TourTeamPrice;
                        StringBuilder strTeamPrice = new StringBuilder();
                        foreach (MTourTeamPrice teamprice in TeamPrice)
                        {
                            if (!string.IsNullOrEmpty(teamprice.ServiceType.ToString()))
                            {
                                strTeamPrice.AppendFormat("<tr><td width='9%' align='left'><b class='font14'>{0}</b></td><td align='left'>{1}</td><td align='center' width='13%'>{2}</td>", teamprice.ServiceType.ToString(), EyouSoft.Common.Function.StringValidate.TextToHtml(teamprice.ServiceStandard), EyouSoft.Common.UtilsCommons.GetMoneyString(teamprice.Quote, ProviderToMoney) + "/" + teamprice.Unit.ToString());
                            }
                        }
                        this.lboptionservice.Text = strTeamPrice.ToString();
                    }
                    #endregion

                    #region 团队报价
                    this.lbChildPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ChildPrice, ProviderToMoney) + "/人";
                    this.lbAdultPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.AdultPrice, ProviderToMoney) + "/人";
                    this.lbotherprice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.OtherCost, ProviderToMoney) + "/团";
                    this.lbtotleprice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.SumPrice, ProviderToMoney);
                    this.lbremark.Text = model.QuoteRemark;

                    #endregion

                    #region 销售员
                    if (model.SaleInfo != null)
                    {
                        var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(model.SaleInfo.SellerId, SiteUserInfo.CompanyId);
                        if (xiaoShouYuanInfo != null)
                        {
                            this.txtname.Text = xiaoShouYuanInfo.ContactName;
                            this.txttel.Text = xiaoShouYuanInfo.ContactTel;
                            this.txtfax.Text = xiaoShouYuanInfo.ContactFax;
                        }
                    }
                    #endregion
                }
            }
            else//团队报价实体
            {
                EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
                EyouSoft.Model.TourStructure.MTourQuoteInfo model = bll.GetQuoteInfo(quoteid);
                if (model != null)
                {
                    this.lbRouteName.Text = model.RouteName;
                    this.lbPeoNum.Text = model.Adults.ToString() + "<sup>+" + model.Childs.ToString() + "</sup>";
                    this.lbAdultsNum.Text = model.Adults.ToString() + "人";
                    this.lbChildsNum.Text = model.Childs.ToString() + "人";
                    this.txtunitname.Text = model.BuyCompanyName;
                    this.txtunitContactname.Text = model.Contact;
                    this.txtunittel.Text = model.Phone;
                    this.lbRouteName.Text = model.RouteName;
                    #region 行程
                    IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> planinfo = model.QuotePlan.OrderBy(m => m.Days).ToList();
                    if (planinfo != null && planinfo.Count > 0)
                    {
                        StringBuilder strAllDateInfo = new StringBuilder();
                        string Dinner = string.Empty;//包餐(早、中、晚)
                        foreach (EyouSoft.Model.TourStructure.MPlanBaseInfo Plan in planinfo)
                        {
                            if (Plan.Breakfast) { Dinner += "早、"; }
                            if (Plan.Lunch) { Dinner += "中、"; }
                            if (Plan.Supper) { Dinner += "晚、"; }
                            strAllDateInfo.AppendFormat("<table width='696' border='0' align='center' cellpadding='0' cellspacing='0' class='borderline_2'><tr><td width='35%' class='small_title'><b class='font16'>第{0}天  {6}</b></td><td width='15%' class='small_title'><b class='font14'>交通：{1}</b></td><td width='20%' class='small_title'><b class='font14'>餐：{2}</b></td><td width='30%' class='small_title'><b class='font14'>住宿：{3}</b></td></tr></table><table width='696' border='0' align='center' cellpadding='0' cellspacing='0' style='margin-top:0px' class='list_2'><tr><td class='td_text' style='border-top:none;' width='{7}'>{4}</td>{5}</tr></table>", Plan.Days.ToString(), Plan.Traffic, Dinner, Plan.Hotel, Plan.Content, string.IsNullOrEmpty(Plan.FilePath) ? "" : "<td style='border-top:none;'><img src='http://" + Request.Url.Authority + Plan.FilePath + "' width='202' height='163' /></td>", Plan.Section, string.IsNullOrEmpty(Plan.FilePath) ? "100%" : "480px");
                            Dinner = string.Empty;
                        }
                        this.lbtourplan.Text = strAllDateInfo.ToString();
                    }
                    #endregion

                    #region 线路特色
                    if (string.IsNullOrEmpty(model.PlanFeature))
                    {
                        this.TPlanFeature.Visible = false;
                    }
                    else
                    {
                        this.lbPlanFeature.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.PlanFeature);
                    }
                    #endregion

                    #region 计划服务
                    if (model.TourService != null)
                    {
                        #region 服务标准
                        if (string.IsNullOrEmpty(model.TourService.ServiceStandard))
                        {
                            this.TService.Visible = false;
                        }
                        else
                        {
                            this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ServiceStandard);
                        }
                        #endregion

                        #region 服务不含
                        if (string.IsNullOrEmpty(model.TourService.NoNeedItem))
                        {
                            this.TNoService.Visible = false;
                        }
                        else
                        {
                            this.lbnoService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NoNeedItem);
                        }
                        #endregion

                        #region 购物安排
                        if (string.IsNullOrEmpty(model.TourService.ShoppingItem))
                        {
                            this.TShopping.Visible = false;
                        }
                        else
                        {
                            this.lbshopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ShoppingItem);
                        }
                        #endregion

                        #region 儿童安排
                        if (string.IsNullOrEmpty(model.TourService.ChildServiceItem))
                        {
                            this.TChildren.Visible = false;
                        }
                        else
                        {
                            this.lbchildren.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ChildServiceItem);
                        }
                        #endregion

                        #region 自费项目
                        if (string.IsNullOrEmpty(model.TourService.OwnExpense))
                        {
                            this.TSelfProject.Visible = false;
                        }
                        else
                        {
                            this.lbselfproject.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.OwnExpense);
                        }
                        #endregion

                        #region 温馨提醒
                        if (string.IsNullOrEmpty(model.TourService.WarmRemind))
                        {
                            this.TWarmRemind.Visible = false;
                        }
                        else
                        {
                            this.lbwarmremind.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.WarmRemind);
                        }
                        #endregion

                        #region 注意事项
                        if (string.IsNullOrEmpty(model.TourService.NeedAttention))
                        {
                            this.TNeedAttention.Visible = false;
                        }
                        else
                        {
                            this.lbneedattention.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NeedAttention);
                        }
                        #endregion
                    }
                    else
                    {
                        this.TPlanService.Visible = false;
                    }
                    #endregion

                    #region 服务标准
                    if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                    {
                        this.TOption.Visible = false;
                        this.lballservice.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard);
                    }
                    else
                    {
                        this.TAll.Visible = false;
                        IList<MTourTeamPrice> TeamPrice = model.TourTeamPrice;
                        StringBuilder strTeamPrice = new StringBuilder();
                        foreach (MTourTeamPrice teamprice in TeamPrice)
                        {
                            if (!string.IsNullOrEmpty(teamprice.ServiceType.ToString()))
                            {
                                strTeamPrice.AppendFormat("<tr><td width='9%' align='left'><b class='font14'>{0}</b></td><td align='left'>{1}</td><td width='13%' align='center'>{2}</td>", teamprice.ServiceType.ToString(), EyouSoft.Common.Function.StringValidate.TextToHtml(teamprice.ServiceStandard), EyouSoft.Common.UtilsCommons.GetMoneyString(teamprice.Quote, ProviderToMoney) + "/" + teamprice.Unit.ToString());
                            }
                        }
                        this.lboptionservice.Text = strTeamPrice.ToString();
                    }
                    #endregion

                    #region 团队报价
                    this.lbChildPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.ChildPrice, ProviderToMoney) + "/人";
                    this.lbAdultPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.AdultPrice, ProviderToMoney) + "/人";
                    this.lbotherprice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.OtherCost, ProviderToMoney) + "/团";
                    this.lbtotleprice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.TotalPrice, ProviderToMoney);
                    this.lbremark.Text = model.QuoteRemark;

                    #endregion

                    #region 销售员
                    if (model.SaleInfo != null)
                    {
                        var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(model.SaleInfo.SellerId, SiteUserInfo.CompanyId);
                        if (xiaoShouYuanInfo != null)
                        {
                            this.txtname.Text = xiaoShouYuanInfo.ContactName;
                            this.txttel.Text = xiaoShouYuanInfo.ContactTel;
                            this.txtfax.Text = xiaoShouYuanInfo.ContactFax;
                        }
                    }
                    #endregion
                }
            }
        }
    }
}
