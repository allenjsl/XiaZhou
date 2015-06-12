using System;
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace Web.FinanceManage.Apply
{
    /// <summary>
    /// 报销报账-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class List : BackPage
    {
        protected string CaoZuoString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            this.Title = "财务管理-报销报账";
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            MBZSearch queryModel = new MBZSearch();
            //团号
            queryModel.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            //线路名称
            queryModel.RouteName = Utils.GetQueryStringValue("txt_lineName");
            //出团时间--始
            queryModel.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_SDate"));
            //出团时间--终
            queryModel.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_EDate"));
            //导游
            queryModel.Guide = txt_Guide.GuidName = Utils.GetQueryStringValue(txt_Guide.GuidNameClient);
            //导游Id
            queryModel.GuideId = txt_Guide.GuidID = Utils.GetQueryStringValue(txt_Guide.GuidIDClient);
            //计调
            queryModel.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            //计调Id
            queryModel.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            //销售
            queryModel.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            //销售Id
            queryModel.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);

            string baoxiaoorbaozhang=Utils.GetQueryStringValue("isDealt") ;
            //是否已报销或报账
            queryModel.IsDealt = (baoxiaoorbaozhang == "1" || baoxiaoorbaozhang == "2");
            #endregion

            IList<MBZInfo> ls = new List<MBZInfo>();

            if (baoxiaoorbaozhang == "-2" || baoxiaoorbaozhang == "2")//报销
            {
                ls = new BTour().GetFincBXList(
                CurrentUserCompanyID,
                pageSize,
                pageIndex,
                ref recordCount,
                queryModel,
                SiteUserInfo.DeptId);
            }
            else//报账
            {
                ls = new BTour().GetFincBZList(
                CurrentUserCompanyID,
                pageSize,
                pageIndex,
                ref recordCount,
                queryModel,
                SiteUserInfo.DeptId);
            }

            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;

            if (baoxiaoorbaozhang == "-2")
            {
                CaoZuoString="<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\">报销</a>";
            }
            else if (baoxiaoorbaozhang == "2")
            {
                CaoZuoString = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\">查看</a>";
            }
            else if (baoxiaoorbaozhang == "1")
            {
                CaoZuoString = "<a data-class=\"a_Apply\" href=\"javascript:void(0);\">查看</a>";
            }
            else
            {
                CaoZuoString = "<a data-class=\"a_Apply\" href=\"javascript:void(0);\">审批</a>";
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_报销报账_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, true);
                return;
            }
        }
        /// <summary>
        /// 拼接导游姓名
        /// </summary>
        /// <param name="ls">导游列表</param>
        /// <returns></returns>
        protected string PingDYName(IList<MGuidInfo> ls)
        {
            string namesStr = string.Empty;
            if (ls != null && ls.Count > 0)
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    namesStr += ls[i].Name + ",";
                    if (i > 0 && i % 5 == 0)
                    {
                        namesStr += "<br/>";
                    }
                }
                //截取末尾逗号
                namesStr = namesStr.Substring(0, namesStr.Length - 1);
            }
            return namesStr;
        }
        /// <summary>
        /// 拼接计调姓名
        /// </summary>
        /// <param name="ls">计调列表</param>
        /// <returns></returns>
        protected string PingJDName(IList<MTourPlaner> ls)
        {
            string namesStr = string.Empty;
            if (ls != null && ls.Count > 0)
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    namesStr += ls[i].Planer + ",";
                    if (i > 0 && i % 5 == 0)
                    {
                        namesStr += "//n";
                    }
                }
                //截取末尾逗号
                namesStr = namesStr.Substring(0, namesStr.Length - 1);
            }
            return namesStr;
        }
        #endregion
    }
}
