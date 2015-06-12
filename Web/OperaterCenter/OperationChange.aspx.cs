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
    /// 计调中心—业务变更-行程变更
    /// 创建人：李晓欢
    /// 创建时间：2011-09-15
    /// </summary>
    public partial class OperationChange : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数 
        /// </summary>
        protected int recordCount = 0;
        /// <summary>
        /// 请求类型，tour：计划变更，order：订单变更。
        /// </summary>
        string RequestType = string.Empty;
        #endregion

        /// <summary>
        /// 打印页面Url
        /// </summary>
        protected string PrintUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            RequestType = Utils.GetQueryStringValue("type");

            if (string.IsNullOrEmpty(RequestType)) RequestType = "tour";

            PowerControl();
            
            if (RequestType == "order")
            {
                DataInitOrderList();
                this.tabTourChangeView.Visible = false;
                this.tabOrderChangeView.Visible = true;
            }
            else if (RequestType == "tour")  //计划变更
            {
                DataInitTourList();
                this.tabTourChangeView.Visible = true;
                this.tabOrderChangeView.Visible = false;
            }
            else
            {
                AjaxResponse("错误的请求。");
            }

        }

        #region 获取操作人信息
        /// <summary>
        /// 操作人信息
        /// </summary>
        /// <param name="tourid">团号</param>
        /// <returns></returns>
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

        #region 私有方法
        /// <summary>
        ///计划变更绑定列表
        /// </summary>
        private void DataInitTourList()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            IList<EyouSoft.Model.TourStructure.MTourPlanChange> PlanChangeList = new EyouSoft.BLL.TourStructure.BTour().GetTourPlanChange(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount);
            if (PlanChangeList != null && PlanChangeList.Count > 0)
            {
                this.repTourChangeList.DataSource = PlanChangeList;
                this.repTourChangeList.DataBind();
                BindPage();
            }
            else
            {
                this.lab_Text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }
        #endregion

        #region 订单变更绑定列表
        /// <summary>
        /// 订单变更绑定列表
        /// </summary>
        protected void DataInitOrderList()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            IList<EyouSoft.Model.TourStructure.MTourOrderChange> orderChange = new EyouSoft.BLL.TourStructure.BTourOrderChange().GetTourOrderChangeList(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.TourStructure.ChangeType.变更, pageSize, pageIndex, ref recordCount);
            if (orderChange != null && orderChange.Count > 0)
            {
                this.repOrderChangelist.DataSource = orderChange;
                this.repOrderChangelist.DataBind();
                BindPage();
            }
            else
            {
                this.lab_Text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }
        #endregion

        #region 计调员 导游绑定
        protected string GetOperaterList(object o)
        {
            System.Text.StringBuilder sbOp = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MTourPlaner> Oplist = (List<EyouSoft.Model.TourStructure.MTourPlaner>)o;
            if (Oplist != null && Oplist.Count > 0)
            {
                for (int i = 0; i < Oplist.Count; i++)
                {
                    if (i == Oplist.Count - 1)
                    {
                        sbOp.Append("" + Oplist[i].Planer + "");
                    }
                    else
                    {
                        sbOp.Append("" + Oplist[i].Planer + ",");
                    }
                }
            }
            return sbOp.ToString();
        }
        protected string GetGuidList(object o)
        {
            System.Text.StringBuilder sbGuid = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MGuidInfo> guidList = (List<EyouSoft.Model.TourStructure.MGuidInfo>)o;
            if (guidList != null && guidList.Count > 0)
            {
                for (int i = 0; i < guidList.Count; i++)
                {
                    if (i == guidList.Count - 1)
                    {
                        sbGuid.Append("" + guidList[i].Name + "");
                    }
                    else
                    {
                        sbGuid.Append("" + guidList[i].Name + ",");
                    }
                }
            }
            return sbGuid.ToString();
        }
        #endregion

        #region 分页
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
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_业务变更_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_业务变更_栏目, true);
                return;
            }
        }
        #endregion


    }
}
