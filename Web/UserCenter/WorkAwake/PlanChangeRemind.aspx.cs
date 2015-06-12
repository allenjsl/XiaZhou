using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using System.Collections.Generic;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;

namespace Web.UserCenter.WorkAwake
{
    public partial class PlanChangeRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：蔡永辉
        /// 创建时间：2012-3-26
        /// 说明：事物中心：变更提醒（计划变更提醒）


        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 15;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //设置选择
                this.UserCenterNavi1.NavIndex = 7;
                this.UserCenterNavi1.PrivsList = SiteUserInfo.Privs;
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            BIndividual bllBIndividual = new BIndividual();
            IList<MTourChangeRemind> listChangeRemind = bllBIndividual.GetTourChangeRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, SiteUserInfo.UserId);
            if (listChangeRemind != null && listChangeRemind.Count > 0)
            {
                this.rptList.DataSource = listChangeRemind;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目, true);
                return;
            }      
        }

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            string argument = Utils.GetQueryStringValue("argument");
            //对应执行操作
            switch (doType)
            {
                case "IsSureChange":
                    //执行并获取结果
                    msg = IsSureChange(argument);
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }

        /// <summary>
        /// 确认变更
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        private string IsSureChange(string tourid)
        {
            string result = "";
            if (!string.IsNullOrEmpty(tourid))
            {
                //实例化计划变更实体
                MTourPlanChange modelMTourPlanChange = new MTourPlanChange();
                //实例化业务
                BTour bllBTour = new BTour();
                modelMTourPlanChange = bllBTour.GetTourChangeModel(SiteUserInfo.CompanyId, Utils.GetInt(tourid));
                if (modelMTourPlanChange != null)
                {
                    //主键编号
                    modelMTourPlanChange.Id = Utils.GetInt(tourid);
                    //确认变更人
                    modelMTourPlanChange.Confirmer = SiteUserInfo.Name;
                    //确认变更人ID
                    modelMTourPlanChange.ConfirmerId = SiteUserInfo.UserId;
                    //确认变更时间
                    modelMTourPlanChange.ConfirmTime = DateTime.Now;
                    //公司编号
                    modelMTourPlanChange.CompanyId = SiteUserInfo.CompanyId;
                    if (bllBTour.TourChangeSure(modelMTourPlanChange))
                        result = UtilsCommons.AjaxReturnJson("false", "确认变更成功");
                    else
                        result = UtilsCommons.AjaxReturnJson("false", "确认变更失败");
                }
                else
                    result = UtilsCommons.AjaxReturnJson("false", "数据对象为空");
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "数据参数id为空");
            return result;
        }
        #endregion

        #endregion
        #region 前台调用方法

        #region 计划变更 颜色处理
        protected string GetTourPlanIschange(bool State)
        {
            System.Text.StringBuilder sbChange = new System.Text.StringBuilder();

            //确认 绿色 未确认 红色
            if (State)
            {
                sbChange.Append("<span class=\"fontgreen\"><a target=\"_blank\" href=\"javascript:void(0);\">(变)</a></span>");
            }
            else
            {
                sbChange.Append("<span class=\"fontred\"><a target=\"_blank\" href=\"javascript:void(0);\">(变)</a></span>");
            }

            return sbChange.ToString();
        }
        #endregion

        #region 获取操作人信息
        protected string GetOperaterInfo(string tourid)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MTourBaoInfo info = new EyouSoft.BLL.TourStructure.BTour().GetTourBaoInfo(tourid);
            if (info != null)
            {
                sb.Append("<b>" + info.TourCode + "</b><br />发布人：" + info.Operator + "<br />发布时间：" + EyouSoft.Common.UtilsCommons.GetDateString(info.IssueTime, ProviderToDate) + "");
            }
            info = null;
            return sb.ToString();
        }
        #endregion

        #region Repeater嵌套绑定
        /// <summary>
        /// Repeater嵌套绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //判断里层repeater处于外层repeater的哪个位置（ AlternatingItemTemplate，FooterTemplate，
            //HeaderTemplate，，ItemTemplate，SeparatorTemplate）
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repTourPlaner = e.Item.FindControl("rptTourPlaner") as Repeater;//找到里层的repeater对象
                Repeater repTourGuide = e.Item.FindControl("rptTourGuide") as Repeater;//找到里层的repeater对象
                //Repeater数据源
                repTourPlaner.DataSource = ((MTourChangeRemind)e.Item.DataItem).TourPlaner;
                repTourPlaner.DataBind();

                //Repeater数据源
                repTourGuide.DataSource = ((MTourChangeRemind)e.Item.DataItem).TourGuide;
                repTourGuide.DataBind();

            }
        }
        #endregion


        #endregion
    }
}
