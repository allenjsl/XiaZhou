using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.TeamCenter
{
    /// <summary>
    /// 超限申请页 create by DYZ at 2012-08-21
    /// </summary>
    public partial class ChaoXianShenQing : BackPage
    {
        protected string[] PageHtml = new string[2];
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();


            string dotype = Utils.GetQueryStringValue("dotype");
            if (dotype == "save")
            {
                SaveForm();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageInit()
        {
            this.lblApplyMan.Text = SiteUserInfo.Name;
            this.lblApplyDateTime.Text = UtilsCommons.GetDateString(DateTime.Now, this.ProviderToDate);

            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();

            string tourId = Utils.GetQueryStringValue("tourId");
            string orderId = Utils.GetQueryStringValue("orderId");
            string msg = string.Empty;
            //询价单位编号
            string buyID = string.Empty;
            //销售员编号
            string sellsID = string.Empty;
            //合计金额
            decimal price = 0;
            decimal settePrice = 0;
            if (tourId != "")
            {
                this.hideTourId.Value = tourId;
                EyouSoft.BLL.TourStructure.BTour tourBll = new EyouSoft.BLL.TourStructure.BTour();

                EyouSoft.Model.TourStructure.MTourTeamInfo model = (EyouSoft.Model.TourStructure.MTourTeamInfo)tourBll.GetTourInfo(tourId);
                if (model != null)
                {
                    if (model.SaleInfo != null)
                    {
                        sellsID = model.SaleInfo.SellerId;
                    }
                    if (model.CompanyInfo != null)
                    {
                        buyID = model.CompanyInfo.CompanyId;
                    }
                    settePrice = price = model.SumPrice;
                }
            }

            if (orderId != "")
            {
                this.hideOrderId.Value = orderId;
                EyouSoft.BLL.TourStructure.BTourOrder orderBll = new EyouSoft.BLL.TourStructure.BTourOrder();
                EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = orderBll.GetTourOrderExpandByOrderId(orderId);
                if (orderModel != null)
                {
                    sellsID = orderModel.SellerId;
                    buyID = orderModel.BuyCompanyId;
                    price = orderModel.SumPrice;
                    settePrice = orderModel.ConfirmSettlementMoney;
                }
            }

            if (buyID != "" && sellsID != "" && price > 0)
            {
                EyouSoft.Model.FinStructure.MCustomerWarning customerWarningModel = bll.GetCustomerOverrunDetail(buyID, price, SiteUserInfo.CompanyId);
                EyouSoft.Model.FinStructure.MSalesmanWarning salesmanWarningModel = bll.GetSaleOverrunDetail(sellsID, settePrice, SiteUserInfo.CompanyId);
                if (customerWarningModel != null)
                {
                    PageHtml[0] = "<tr><td height='28' bgcolor='#FFFFFF' align='center'>" + customerWarningModel.Customer + "</td><td bgcolor='#FFFFFF' align='center'><b class='fontbsize12'>" + UtilsCommons.GetMoneyString(customerWarningModel.AmountOwed, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><a class='link1'><strong>" + UtilsCommons.GetMoneyString(customerWarningModel.Arrear, this.ProviderToMoney) + "</strong></a></td><td bgcolor='#FFFFFF' align='center'><b class='fontgreen'>" + UtilsCommons.GetMoneyString(customerWarningModel.Transfinite, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'>" + UtilsCommons.GetDateString(customerWarningModel.TransfiniteTime, this.ProviderToDate) + "</td><td bgcolor='#FFFFFF' align='center'>" + customerWarningModel.Deadline.ToString() + "</td><td bgcolor='#FFFFFF' align='center'>" + (customerWarningModel.DeadDay <= 0 ? 0 : customerWarningModel.DeadDay) + "</td></tr>";
                }
                else
                {
                    this.phdKehu.Visible = false;
                }

                if (salesmanWarningModel != null)
                {
                    PageHtml[1] = "<tr><td height='28' bgcolor='#FFFFFF' align='center'>" + salesmanWarningModel.SellerName + "</td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.AmountOwed, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.ConfirmAdvances, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><strong>" + UtilsCommons.GetMoneyString(salesmanWarningModel.PreIncome, this.ProviderToMoney) + "</strong></td><td bgcolor='#FFFFFF' align='center'><b class='fontblue'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.SumPay, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><b class='fontbsize12'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.Arrear, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'><b class='fontgreen'>" + UtilsCommons.GetMoneyString(salesmanWarningModel.Transfinite, this.ProviderToMoney) + "</b></td><td bgcolor='#FFFFFF' align='center'>" + UtilsCommons.GetDateString(salesmanWarningModel.TransfiniteTime, this.ProviderToDate) + "</td></tr>";
                }
                else
                {
                    this.phdXiaoshou.Visible = false;
                }
            }
            else
            {
                this.phdKehu.Visible = false;
                this.phdXiaoshou.Visible = false;
            }
        }

        /// <summary>
        /// 保存表单
        /// </summary>
        private void SaveForm()
        {
            //垫付金额
            decimal applyPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtApplyPrice.UniqueID), 0);
            //超限备注
            string applyRemarks = Utils.GetFormValue(this.txtApplyRemarks.UniqueID);
            //申请人编号
            string applyManID = this.SiteUserInfo.UserId;
            //申请日期
            DateTime applyDateTime = DateTime.Now;

            if (applyPrice == 0)
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "金额不能为0!"));
                Response.End();
            }

            EyouSoft.Model.TourStructure.MAdvanceApp model = new EyouSoft.Model.TourStructure.MAdvanceApp();
            model.Applier = SiteUserInfo.Name;
            model.ApplierId = SiteUserInfo.UserId;
            model.ApplyTime = applyDateTime;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.DeptId = SiteUserInfo.DeptId;
            model.DisburseAmount = applyPrice;
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.Remark = applyRemarks;
            string tourId = Utils.GetFormValue(this.hideTourId.UniqueID);
            string orderId = Utils.GetFormValue(this.hideOrderId.UniqueID);
            if (tourId != "")
            {
                model.ItemId = tourId;
                model.ItemType = EyouSoft.Model.EnumType.FinStructure.TransfiniteType.成团;
            }
            if (orderId != "")
            {
                model.ItemId = orderId;
                model.ItemType = EyouSoft.Model.EnumType.FinStructure.TransfiniteType.报名;
            }
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            int result = bll.AddAdvanceApp(model);
            Response.Clear();
            switch (result)
            {
                case 1:
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "垫付申请已提交!"));
                    break;
                case 2:
                    Response.Write(UtilsCommons.AjaxReturnJson("2", "未超限，无需垫付申请!"));
                    break;
                case -99:
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败：不能重复提交垫付申请信息!"));
                    break;
                case 0:
                default:
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "申请失败!"));
                    break;
            }
            Response.End();
        }
    }
}
