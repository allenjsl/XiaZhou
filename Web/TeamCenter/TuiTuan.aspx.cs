using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.TeamCenter
{
    public partial class TuiTuan : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            if (dotype != null && dotype == "tuituan")
            {
                Response.Clear();
                Response.Write(PageSave());
                Response.End();
            }

            if (!IsPostBack)
            {
                string travellerId = Utils.GetQueryStringValue("id");
                if (travellerId != "")
                {
                    Get_Morder(travellerId);
                }
            }
        }
        /// <summary>
        /// 根据游客ID获取游客信息
        /// </summary>
        /// <param name="str_id">游客ID</param>
        protected void Get_Morder(string travellerId)
        {
            MTourOrderTraveller Model_Order = new BTourOrder().GetTourOrderTravellerById(travellerId);
            if (Model_Order != null)
            {
                txt_reBackName.Text = Model_Order.CnName;
                if (Model_Order.Gender == EyouSoft.Model.EnumType.GovStructure.Gender.男)
                {
                    
                    rad_Man.Checked = true;
                }
                else
                {
                    rad_Women.Checked = true;
                }
            }
        }

        /// <summary>
        /// 退团事件
        /// </summary>
        /// <returns></returns>
        private string PageSave()
        {
            string msg = string.Empty;

            #region 获取表单
            string travellerId = Utils.GetQueryStringValue("id");
            string travellerName = Utils.GetFormValue(this.txt_reBackName.UniqueID);
            EyouSoft.Model.EnumType.GovStructure.Gender travellerSex = Utils.GetFormValue(this.rad_Man.UniqueID) == "0" ? EyouSoft.Model.EnumType.GovStructure.Gender.男 : EyouSoft.Model.EnumType.GovStructure.Gender.女;
            DateTime leaveDate = Utils.GetDateTime(Utils.GetFormValue(this.txt_reBackDate.UniqueID));
            decimal reBackMoney = Utils.GetDecimal(Utils.GetFormValue(this.txt_reBackMoney.UniqueID));
            string reMarkMoney = Utils.GetFormValue(this.txt_reMarkMoney.UniqueID);
            string reBackResion = Utils.GetFormValue(this.txt_ReBackResion.UniqueID);
            string tourId = Utils.GetQueryStringValue("tourId");
            string orderId = Utils.GetQueryStringValue("orderId");
            #endregion

            #region 游客实体类
            BTourOrder bll = new BTourOrder();
            MTourOrderTraveller travellerModel = bll.GetTourOrderTravellerById(travellerId);
            travellerModel.TravellerId = travellerId;
            travellerModel.TravellerStatus = EyouSoft.Model.EnumType.TourStructure.TravellerStatus.退团;
            travellerModel.RTime = leaveDate;
            travellerModel.RAmountRemark = reMarkMoney;
            travellerModel.RAmount = reBackMoney;
            travellerModel.OrderId = orderId;
            travellerModel.CnName = travellerName;
            travellerModel.TravellerId = travellerId;
            travellerModel.RRemark = reBackResion;

            #endregion

            #region 订单变更表实体类
            MTourOrderChange Model_Chang = new MTourOrderChange();

            Model_Chang.CompanyId = SiteUserInfo.CompanyId;
            Model_Chang.TourId = tourId;
            Model_Chang.ChangePerson = 1;
            Model_Chang.ChangePrice = reBackMoney;
            Model_Chang.IssueTime = DateTime.Now;
            Model_Chang.Operator = SiteUserInfo.Name;
            Model_Chang.OrderId = travellerModel.OrderId;
            Model_Chang.Content = "申请退团";
            #endregion

            #region 执行更改
            bool result = bll.UpdateTourOrderTraveller(travellerModel, Model_Chang);
            if (result)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "退团成功!");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "退团失败!");
            }
            return msg;
            #endregion
        }
    }
}
