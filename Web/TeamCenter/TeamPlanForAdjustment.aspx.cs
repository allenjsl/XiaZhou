using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Common.Page;

namespace Web.TeamCenter
{
    /// <summary>
    /// 组团派团给计调
    /// 修改人：DYZ 創建日期：2011-10-12
    /// </summary>
    public partial class TeamPlanForAdjustment : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string doType = Utils.GetQueryStringValue("doType");
            if (doType != "" && doType == "save")
            {
                Response.Clear();
                Response.Write(PageSave());
                Response.End();
            }
            if (!IsPostBack)
            {
                this.SellsSelect1.SMode = true;//多选
                this.SellsSelect1.ReadOnly = true;
                this.SellsSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                this.SellsSelect1.ClientDeptID = this.hideDeptId.ClientID;
                string id = Utils.GetQueryStringValue("id");
                if (id != "")
                {
                    PageInit(id);
                }
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id"></param>
        private void PageInit(string id)
        {
            BTour bll = new BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo model = bll.GetTourInfo(id);
            //lblTourCode.Text = bll.GenerateTourNo(SiteUserInfo.DeptId, SiteUserInfo.CompanyId, model.TourType, (DateTime)model.LDate);
            if (model != null)
            {
                if (model.TourService != null)
                {
                    //绑定内部信息
                    this.txtInsiderInfor.Text = model.TourService.InsiderInfor;
                }
                if (model.TourPlanItem != null)
                {
                    for (int i = 0; i < model.TourPlanItem.Count; i++)
                    {
                        //绑定原计调安排项
                        this.hidePlanItem.Value += ((int)model.TourPlanItem[i].PlanType).ToString() + ",";
                    }
                }
                if (model.TourPlaner != null && model.TourPlaner.Count > 0)
                {
                    for (int i = 0; i < model.TourPlaner.Count; i++)
                    {
                        this.SellsSelect1.SellsID += model.TourPlaner[i].PlanerId + ",";
                        this.SellsSelect1.SellsName += model.TourPlaner[i].Planer + ",";
                    }
                    //绑定 计调员
                    this.SellsSelect1.SellsID = this.SellsSelect1.SellsID.Remove(this.SellsSelect1.SellsID.Length - 1, 1);
                    this.SellsSelect1.SellsName = this.SellsSelect1.SellsName.Remove(this.SellsSelect1.SellsName.Length - 1, 1);
                }

            }
            bll = null;
        }
        #endregion

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        #region 保存 计调安排
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        private string PageSave()
        {
            string msg = string.Empty;
            //获取表单
            string[] sellsArray = Utils.GetFormValue(this.SellsSelect1.SellsIDClient).Split(',');
            //string[] sellsNameArray = Utils.GetFormValue(this.SellsSelect1.SellsNameClient).Split(',');
            //string[] sellsDeptArray = Utils.GetFormValue(this.hideDeptId.UniqueID).Split(',');
            string[] item = Utils.GetFormValues("chk_item");
            string tourId = Utils.GetQueryStringValue("tourID");
            string insiderInfor = Utils.GetFormValue(this.txtInsiderInfor.UniqueID);

            //验证
            if (sellsArray.Length == 0)
            {
                return UtilsCommons.AjaxReturnJson("0", "请选择计调员!");
            }
            if (item.Length == 0)
            {
                return UtilsCommons.AjaxReturnJson("0", "请选择计调安排项目!");
            }

            BTour bll = new BTour();

            IList<EyouSoft.Model.TourStructure.MTourPlaner> TourPlaner = new List<EyouSoft.Model.TourStructure.MTourPlaner>();
            for (int i = 0; i < sellsArray.Length; i++)
            {
                EyouSoft.Model.TourStructure.MTourPlaner planerModel = new EyouSoft.Model.TourStructure.MTourPlaner();
                planerModel.PlanerId = sellsArray[i];
                //planerModel.Planer = sellsNameArray[i];
                //planerModel.DeptId = Utils.GetInt(sellsDeptArray[i]);
                planerModel.TourId = tourId;
                TourPlaner.Add(planerModel);
            }

            IList<EyouSoft.Model.TourStructure.MTourPlanItem> TourPlanItem = new List<EyouSoft.Model.TourStructure.MTourPlanItem>();
            for (int i = 0; i < item.Length; i++)
            {
                EyouSoft.Model.TourStructure.MTourPlanItem tpiModel = new EyouSoft.Model.TourStructure.MTourPlanItem();
                tpiModel.TourId = tourId;
                tpiModel.PlanType = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(item[i]);
                TourPlanItem.Add(tpiModel);
            }



            EyouSoft.Model.TourStructure.MTourBaseInfo tourModel = bll.GetTourInfo(tourId);
            if (tourModel != null)
            {
                EyouSoft.Model.TourStructure.MSendTour sendTourModel = new EyouSoft.Model.TourStructure.MSendTour();
                sendTourModel.CompanyId = SiteUserInfo.CompanyId;
                sendTourModel.DeptId = SiteUserInfo.DeptId;
                sendTourModel.InsiderInfor = insiderInfor;
                sendTourModel.LDate = Convert.ToDateTime(tourModel.LDate);
                sendTourModel.Operator = SiteUserInfo.Name;
                sendTourModel.OperatorId = SiteUserInfo.UserId;

                sendTourModel.Planer = TourPlaner;
                sendTourModel.PlanItem = TourPlanItem;
                //sendTourModel.TourCode = bll.GenerateTourNo(SiteUserInfo.DeptId, SiteUserInfo.CompanyId, tourModel.TourType, sendTourModel.LDate);
                sendTourModel.TourId = tourId;
                sendTourModel.TourType = tourModel.TourType;

                bool result = false;

                lock (PaiTuanXuHaoLockHelper)
                {
                    result = bll.SendTour(sendTourModel);
                }

                if (result)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "安排计调成功!");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "安排计调失败!");
                }
            }



            return msg;
        }
        #endregion
    }
}
