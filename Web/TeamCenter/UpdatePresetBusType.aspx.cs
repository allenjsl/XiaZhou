using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.TeamCenter
{
    /// <summary>
    /// 修改预设车型
    /// </summary>
    public partial class UpdatePresetBusType : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            string save = Utils.GetQueryStringValue("save");
            #region  Ajax请求
            if (!string.IsNullOrEmpty(tourId) && save == "save")
            {
                Response.Clear();
                Response.Write(PageSave(tourId));
                Response.End();
            }
            #endregion
            if (!IsPostBack)
            {
                PageInit(tourId);
            }
        }


        protected void PageInit(string tourId)
        {
            //根据计划Id获取预设车型集合
            EyouSoft.BLL.TourStructure.BTour BLL = new EyouSoft.BLL.TourStructure.BTour();
            IList<MTourCarType> list = BLL.GetTourCarType(tourId);
            if (list != null && list.Count > 0)
            {
                this.PresetBusType1.PreSetTypeList = list;
            }
            this.PresetBusType1.TourId = tourId;
            this.PresetBusType1.ContrloModel = "updateTrue";

            EyouSoft.Model.TourStructure.MTourBaseInfo baseModel = BLL.GetBasicTourInfo(tourId);

            if (baseModel != null && baseModel.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
            {
                if (baseModel.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团)
                {
                    this.ph_Save.Visible = false;
                }
                if (baseModel.SaleInfo != null)
                {
                    this.OperateID.Value = baseModel.SaleInfo.SellerId;
                }
            }
        }
        private string PageSave(string tourId)
        {
            string OperaterID = this.OperateID.Value;
            if (SiteUserInfo.UserId != OperaterID)
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "你不是该计划的销售员,无法修改车型!");
            }
            string msg = string.Empty;
            string[] CarTypeId = Utils.GetFormValues(PresetBusType1.hidCarTypeIdClientID);
            string[] CarTypeName = Utils.GetFormValues(PresetBusType1.hidCarTypeNameClientID);
            string[] Desc = Utils.GetFormValues(PresetBusType1.hidDescClientID);
            string[] SeatNum = Utils.GetFormValues(PresetBusType1.hidSeatNumClientID);
            string[] TourCarTypeId = Utils.GetFormValues(PresetBusType1.hideTourCarTypeIdClientID);
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            IList<MTourCarType> list = new List<MTourCarType>();
            if (CarTypeId.Length > 0)
            {
                for (int i = 0; i < CarTypeId.Length; i++)
                {
                    if (CarTypeName[i].ToString() != "")
                    {
                        MTourCarType model = new MTourCarType();
                        model.TourCarTypeId = TourCarTypeId[i].ToString();
                        model.SeatNum = Utils.GetInt(SeatNum[i].ToString());
                        model.Desc = Desc[i].ToString();
                        model.CarTypeName = CarTypeName[i].ToString();
                        model.CarTypeId = CarTypeId[i].ToString();
                        list.Add(model);
                    }
                }
            }
            if (!string.IsNullOrEmpty(msg))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", msg);
            }
            else
            {
                if (bll.UpdateTourCarType(tourId, SiteUserInfo.Username, SiteUserInfo.UserId, list))
                {
                    return EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "修改成功!");
                }
                else
                {
                    return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "修改失败!");
                }
            }
        }
    }
}
