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
    /// 计划跟订单变更详细信息
    /// 李晓欢 2012-04-06
    /// </summary>
    public partial class ChangeInfo :EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 请求类型，tour：计划变更，order：订单变更。
        /// </summary>
        string RequestType = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestType = Utils.GetQueryStringValue("type");
            if (string.IsNullOrEmpty(RequestType)) RequestType = "tour";

            #region ajax操作
            //确认变更操作
            string changeId = Utils.GetQueryStringValue("changeId");
            string dotype = Utils.GetQueryStringValue("action");
            switch (dotype)
            {
                case "save":
                    Response.Clear();
                    if (RequestType== "order")
                    {
                        Response.Write(ConfirmOrder(changeId));
                    }
                    else if (RequestType == "tour")
                    {
                        Response.Write(ConfirmChange(changeId));
                    }
                    else
                    {                        
                        Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败：错误的请求！"));
                    }
                    Response.End();
                    break;
                default:break;
            }
            #endregion

            InitPrivs();

            if (RequestType != "order" && RequestType != "tour")
            {
                AjaxResponse("错误的请求");
            }

            string bianGengId = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrEmpty(bianGengId))
            {
                if (RequestType == "order")
                {
                    this.changeTitleView.Visible = false;
                    GetOrderChangeListById(bianGengId);
                }
                else if (RequestType == "tour")
                {
                    GetTourChangeListById(bianGengId);
                }
            }
        }

        #region 计划变更详细信息
        /// <summary>
        /// 计划变更信息
        /// </summary>
        /// <param name="Id">主键id</param>
        protected void GetTourChangeListById(string Id)
        {
            EyouSoft.Model.TourStructure.MTourPlanChange changeTour = new EyouSoft.BLL.TourStructure.BTour().GetTourChangeModel(this.SiteUserInfo.CompanyId, Convert.ToInt32(Id));
            if (changeTour != null)
            {
                this.litChangeTitle.Text = changeTour.Title;
                this.litChangeName.Text = changeTour.Operator;
                this.litChangeTime.Text = EyouSoft.Common.UtilsCommons.GetDateString(changeTour.IssueTime, ProviderToDate);
                this.litChangeContect.Text = changeTour.Content;

                if (changeTour.State) btnCheckView.Visible = false;
            }
        }
        #endregion

        #region 计划确认变更
        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        protected string ConfirmChange(string Id)
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_业务变更_确认变更))
            {
                return UtilsCommons.AjaxReturnJson("0", "操作失败：无权限！");
            }

            string ms = string.Empty;
            EyouSoft.Model.TourStructure.MTourPlanChange change = new EyouSoft.Model.TourStructure.MTourPlanChange();
            change.CompanyId = this.SiteUserInfo.CompanyId;
            change.Id = Convert.ToInt32(Id);
            EyouSoft.Model.TourStructure.MTourPlanChange changeTour = new EyouSoft.BLL.TourStructure.BTour().GetTourChangeModel(this.SiteUserInfo.CompanyId, Convert.ToInt32(Id));
            if (changeTour != null)
            {                
                change.Confirmer = changeTour.Confirmer;
                change.ConfirmTime = changeTour.ConfirmTime;
                change.ConfirmerId = changeTour.ConfirmerId;                
                change.State = changeTour.State;
                change.TourId = changeTour.TourId;                
            }
            
            bool result = new EyouSoft.BLL.TourStructure.BTour().TourChangeSure(change);
            if (result)
            {
                ms = UtilsCommons.AjaxReturnJson("1", "确认成功！");
            }
            else
            {
                ms = UtilsCommons.AjaxReturnJson("0", "确认失败！");
            }
            return ms;
        }
        #endregion

        #region 订单变更详细信息
        /// <summary>
        /// 订单变更详细信息
        /// </summary>
        /// <param name="id">变更id</param>
        protected void GetOrderChangeListById(string id)
        {
            EyouSoft.Model.TourStructure.MTourOrderChange orderChange = new EyouSoft.BLL.TourStructure.BTourOrderChange().GetTourOrderChangById(id);
            if (orderChange != null)
            {
                this.litChangeName.Text = orderChange.Operator;
                this.litChangeTime.Text = EyouSoft.Common.UtilsCommons.GetDateString(orderChange.IssueTime, ProviderToDate);
                this.litChangeContect.Text = orderChange.Content;

                if (orderChange.IsSure) btnCheckView.Visible = false;
            }
        }
        #endregion

        #region 订单变更确认
        /// <summary>
        /// 订单变更确认
        /// </summary>
        /// <param name="id">变更id</param>
        /// <returns></returns>
        protected string ConfirmOrder(string id)
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_业务变更_确认变更))
            {
                return UtilsCommons.AjaxReturnJson("0", "操作失败：无权限！");
            }

            string msg = string.Empty;
            bool result = new EyouSoft.BLL.TourStructure.BTourOrderChange().UpdateTourOrderChange(id, this.SiteUserInfo.UserId, this.SiteUserInfo.Name);
            if (result)
            {
                msg = UtilsCommons.AjaxReturnJson("1","确认成功！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0","确认失败！");
            }
            return msg;
        }
        #endregion

        #region 指定页面类型
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion

        /// <summary>
        /// 权限处理
        /// </summary>
        void InitPrivs()
        {
            btnCheckView.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_业务变更_确认变更);
        }
    }
}
