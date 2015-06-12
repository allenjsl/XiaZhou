﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace Web.ResourceManage.Wineshop
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-28
    /// 说明: 资源管理： 酒店-交易情况
    public partial class Transactions : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
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

        protected string hotelname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Utils.GetQueryStringValue("sourceid");
            hotelname = Utils.GetQueryStringValue("hotelname");
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel(id);
            if (!IsPostBack)
            {
                //初始化
                DataInit(id);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string id)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            EyouSoft.BLL.SourceStructure.BSource Bll = new EyouSoft.BLL.SourceStructure.BSource();
            //IList<EyouSoft.Model.SourceStructure.MHotelClosingCostModel> list = Bll.GetHotelTradeListModel(id, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
            //if (list != null && list.Count > 0)
            //{
            //    this.rptList.DataSource = list;
            //    this.rptList.DataBind();
            //    this.lbClosingCostSum.Text = UtilsCommons.GetMoneyString(list[0].TradeMoneySum, ProviderToMoney);
            //    this.lbUnpaidCostSum.Text = UtilsCommons.GetMoneyString(list[0].UnPaidCostSum, ProviderToMoney);
            //    BindPage();
            //}
            //else
            //{
            //    this.lbMsg.Visible = true;
            //    this.lbMsg.Text = "<tr class=\"odd\"><td height=\"30px\" colspan=\"10\" align=\"center\">暂无交易信息</td></tr>";
            //    this.ExporPageInfoSelect1.Visible = false;
            //}
            //绑定分页
            BindPage();
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
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ListToExcel(string id)
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            var s = new StringBuilder();
            s.Append("团号\t线路名称\t天数\t销售员\t计调\t导游\t明细\t金额\t未付金额\n");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.SourceStructure.BSource Bll = new EyouSoft.BLL.SourceStructure.BSource();
            //IList<EyouSoft.Model.SourceStructure.MHotelClosingCostModel> list = Bll.GetHotelTradeListModel(id, this.SiteUserInfo.CompanyId, pageIndex, toXlsRecordCount, ref recordCount);

            //if (list != null && list.Count > 0)
            //{
            //    foreach (var t in list)
            //    {
            //        s.AppendFormat(
            //            "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
            //            t.TourCode,
            //            t.RouteName,
            //            t.DayCount.ToString(),
            //            t.Seller,
            //            t.PlanerModel.ContactName,
            //            (t.GuideList != null && t.GuideList.Count > 0) ? t.GuideList[0].Name : "",
            //            t.CostDetail.Replace("\t", "    ").Replace("\r\n", "    "),
            //            UtilsCommons.GetMoneyString(t.TradeMoney, ProviderToMoney),
            //            UtilsCommons.GetMoneyString(t.UnPaidCost, ProviderToMoney));
            //    }
            //    s.AppendFormat(
            //            "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "合计",
            //            UtilsCommons.GetMoneyString(list[0].TradeMoneySum, ProviderToMoney),
            //            UtilsCommons.GetMoneyString(list[0].UnPaidCostSum, ProviderToMoney));
            //}

            ResponseToXls(s.ToString());
        }
        #endregion

        #region 前台调用方法

        /// <summary>
        /// 获取导游（只显示第一个）
        /// </summary>
        /// <param name="guide"></param>
        /// <returns></returns>
        protected string GetGuide(object guide)
        {
            IList<EyouSoft.Model.SourceStructure.MSourceGuide> Guidelist = (IList<EyouSoft.Model.SourceStructure.MSourceGuide>)guide;
            if (Guidelist != null && Guidelist.Count > 0)
            {
                return Guidelist[0].Name;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取导游信息
        /// </summary>
        /// <param name="id">SourceId</param>
        /// <returns></returns>
        protected string GetGuideList(object GuideList)
        {
            IList<EyouSoft.Model.SourceStructure.MSourceGuide> Guidelist = (IList<EyouSoft.Model.SourceStructure.MSourceGuide>)GuideList;
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>编号</th><th align='center'>导游</th><th align='center'>电话</th><th align='center'>手机</th></tr>");
            if (Guidelist != null && Guidelist.Count > 0)
            {
                for (int i = 0; i < Guidelist.Count; i++)
                {
                    contactinfo.Append("<tr><td align='center'>" + (i + 1).ToString() + "</td><td align='center'>" + Guidelist[i].Name + "</td><td align='center' >" + Guidelist[i].HomeTel + "</td><td align='center'>" + Guidelist[i].Mobile + "</td></tr>");
                }
            }
            contactinfo.Append("</table>");
            return contactinfo.ToString();
        }
        /// <summary>
        /// 获取计调员信息
        /// </summary>
        /// <param name="name">计调Model</param>
        /// <returns></returns>
        protected string GetContactinfo(object obj)
        {
            EyouSoft.Model.ComStructure.MComUser Muser = (MComUser)obj;
            if (Muser != null)
                return "计调员:" + Muser.ContactName + "</br>电话:" + Muser.ContactTel + "</br>手机:" + Muser.ContactMobile;
            else
                return "";
        }
        #endregion
    }
}
