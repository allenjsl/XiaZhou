using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Newtonsoft.Json;
using EyouSoft.Common.Page;

namespace Web.TeamCenter
{
    /// <summary>
    /// 审核计划
    /// 修改人：DYZ  创建日期:2011.10.27
    /// </summary>
    public partial class CheckTour : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UC_PriceStand.CompanyID = this.SiteUserInfo.CompanyId;
            this.UC_PriceStand.InitMode = true;


            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            string tourId = Utils.GetQueryStringValue("id");
            //存在ajax请求
            if (doType == "shenhe")
            {
                Response.Clear();
                Response.Write(CheckData(tourId));
                Response.End();
            }
            #endregion

            if (!IsPostBack)
            {

                DataInit(tourId);
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="tourId"></param>
        private void DataInit(string tourId)
        {
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();

            EyouSoft.Model.TourStructure.MSupplierPublishPrice model = bll.GetSupplyPrice(tourId);
            if (model != null)
            {
                this.hideAdultPrice.Value = Utils.FilterEndOfTheZeroDecimal(model.SettleAdultPrice);
                this.hideChildPrice.Value = Utils.FilterEndOfTheZeroDecimal(model.SettleChildPrice);
                IList<EyouSoft.Model.ComStructure.MComLev> sysComLev = new EyouSoft.BLL.ComStructure.BComLev().GetList(SiteUserInfo.CompanyId);
                if (sysComLev != null)
                {
                    EyouSoft.Model.ComStructure.MComLev lev = sysComLev.FirstOrDefault(p => p.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.内部结算价);
                    if (lev != null)
                    {
                        this.hidePriceLevelId.Value = lev.Id.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        private string CheckData(string id)
        {
            //审核操作
            bool result = false;
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.EnumType.TourStructure.ShowPublisher showPuEmue = Utils.GetFormValue("rdo_CheckTour") == "1" ? EyouSoft.Model.EnumType.TourStructure.ShowPublisher.审核人 : EyouSoft.Model.EnumType.TourStructure.ShowPublisher.供应商;
            EyouSoft.Model.TourStructure.MSaleInfo saleInfo = new EyouSoft.Model.TourStructure.MSaleInfo();
            saleInfo.DeptId = SiteUserInfo.DeptId;
            saleInfo.Mobile = SiteUserInfo.Mobile;
            saleInfo.Name = SiteUserInfo.Name;
            saleInfo.Phone = SiteUserInfo.Telephone;
            saleInfo.SellerId = SiteUserInfo.UserId;
            result = bll.Review(id, showPuEmue, saleInfo, UtilsCommons.GetPriceStandard());
            if (result == false)
            {
                return UtilsCommons.AjaxReturnJson("0", "审核失败!");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("1", "审核成功,正在刷新页面");
            }
        }
    }
}
