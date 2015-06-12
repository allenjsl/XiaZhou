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
using EyouSoft.Model.SourceStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 团队行程单
    /// 创建人：刘飞
    /// 时间：2012-5-15
    /// </summary>
    public partial class xianluziyuandayindan : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string gysID = EyouSoft.Common.Utils.GetQueryStringValue("xianluid");
            this.Title = "线路资源打印单";
            if (!IsPostBack)
            {
                PageInit(gysID);
            }
        }
        private void PageInit(string GYSID)
        {
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;

            EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MRoute model = BLL.GetRouteModel(GYSID);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                //this.lbPeoNum.Text = model.Adults.ToString() + "<sup>+" + model.Childs.ToString() + "</sup>";
                //this.lbAdultsNum.Text = model.Adults.ToString() + "人";
                //this.lbChildsNum.Text = model.Childs.ToString() + "人";
                //this.txtunitname.Text = model.BuyCompanyName;
                //this.txtunitContactname.Text = model.Contact;
                //this.txtunittel.Text = model.Phone;
                //this.lbRouteName.Text = model.RouteName;
                #region 行程
                IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> planinfo = model.PlanModelList.OrderBy(m => m.Days).ToList();
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
                if (string.IsNullOrEmpty(model.TripAdvantage))
                {
                    this.TPlanFeature.Visible = false;
                }
                else
                {
                    this.lbPlanFeature.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TripAdvantage);
                }
                #endregion

                #region 计划服务
                if (model.ServicesModel != null)
                {
                    #region 服务标准
                    if (string.IsNullOrEmpty(model.ServicesModel.ServiceStandard))
                    {
                        this.TService.Visible = false;
                    }
                    else
                    {
                        this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.ServiceStandard);
                    }
                    #endregion

                    #region 服务不含
                    if (string.IsNullOrEmpty(model.ServicesModel.NoNeedItem))
                    {
                        this.TNoService.Visible = false;
                    }
                    else
                    {
                        this.lbnoService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.NoNeedItem);
                    }
                    #endregion

                    #region 购物安排
                    if (string.IsNullOrEmpty(model.ServicesModel.ShoppingItem))
                    {
                        this.TShopping.Visible = false;
                    }
                    else
                    {
                        this.lbshopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.ShoppingItem);
                    }
                    #endregion

                    #region 儿童安排
                    if (string.IsNullOrEmpty(model.ServicesModel.ChildServiceItem))
                    {
                        this.TChildren.Visible = false;
                    }
                    else
                    {
                        this.lbchildren.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.ChildServiceItem);
                    }
                    #endregion

                    #region 自费项目
                    if (string.IsNullOrEmpty(model.ServicesModel.OwnExpense))
                    {
                        this.TSelfProject.Visible = false;
                    }
                    else
                    {
                        this.lbselfproject.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.OwnExpense);
                    }
                    #endregion

                    #region 温馨提醒
                    if (string.IsNullOrEmpty(model.ServicesModel.WarmRemind))
                    {
                        this.TWarmRemind.Visible = false;
                    }
                    else
                    {
                        this.lbwarmremind.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.WarmRemind);
                    }
                    #endregion

                    #region 注意事项
                    if (string.IsNullOrEmpty(model.ServicesModel.NeedAttention))
                    {
                        this.TNeedAttention.Visible = false;
                    }
                    else
                    {
                        this.lbneedattention.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServicesModel.NeedAttention);
                    }
                    #endregion
                }
                else
                {
                    this.TPlanService.Visible = false;
                }
                #endregion

                #region 服务标准
                if (model.IsTourOrSubentry)
                {
                    this.TOption.Visible = false;
                    this.lballservice.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Service);
                }
                else
                {
                    this.TAll.Visible = false;
                    IList<EyouSoft.Model.SourceStructure.MRouteStandard> TeamPrice = model.StandardModelList;
                    StringBuilder strTeamPrice = new StringBuilder();
                    foreach (MRouteStandard teamprice in TeamPrice)
                    {
                        if (!string.IsNullOrEmpty(teamprice.Type.ToString()))
                        {
                            strTeamPrice.AppendFormat("<tr><td width='9%' align='left'><b class='font14'>{0}</b></td><td align='left'>{1}</td><td width='13%' align='center'>{2}</td>", teamprice.Type.ToString(), EyouSoft.Common.Function.StringValidate.TextToHtml(teamprice.Standard), EyouSoft.Common.UtilsCommons.GetMoneyString(teamprice.UnitPrice, ProviderToMoney) + "/" + teamprice.Unit.ToString());
                        }
                    }
                    this.lboptionservice.Text = strTeamPrice.ToString();
                }
                #endregion

                #region 报价备注
                this.lbremark.Text = model.PathRemark;
                #endregion

                //#region 销售员
                //if (model.SaleInfo != null)
                //{
                //    var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(model.SaleInfo.SellerId, SiteUserInfo.CompanyId);
                //    if (xiaoShouYuanInfo != null)
                //    {
                //        this.txtname.Text = xiaoShouYuanInfo.ContactName;
                //        this.txttel.Text = xiaoShouYuanInfo.ContactTel;
                //        this.txtfax.Text = xiaoShouYuanInfo.ContactFax;
                //    }
                //}
                //#endregion
            }

        }
    }
}
