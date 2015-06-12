﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Common;
using System.Text;

namespace Web.ResourceManage.AjaxRequest
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-11
    /// 说明: Ajax 请求酒店预控
    public partial class AjaxHotelSupplier : BackPage
    {
        protected int pageSize = 5;
        protected int pageIndex = 1;
        protected int recordCount;
        protected int pageCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            //省份
            string Provice = Utils.GetQueryStringValue("provice");
            //城市
            string City = Utils.GetQueryStringValue("city");
            //名称
            string Name = Utils.GetQueryStringValue("name");
            string tourid = Utils.GetQueryStringValue("tourid");
            ListDataInit(Provice, City, Name, tourid);
        }
        #region 初始化列表
        /// <summary>
        /// 列表数据初始化
        /// </summary>
        /// <param name="searchModel"></param>
        private void ListDataInit(string provice, string city, string name, string tourid)
        {
            EyouSoft.BLL.SourceStructure.BSourceControl bsource = new BSourceControl();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            MSourceSueHotelSearch mSourceHotels = new MSourceSueHotelSearch();
            mSourceHotels.CompanyId = this.SiteUserInfo.CompanyId;
            mSourceHotels.OperatorId = this.SiteUserInfo.UserId;
            mSourceHotels.SourceName = name;
            mSourceHotels.ProvinceId = Utils.GetInt(provice);
            mSourceHotels.CityId = Utils.GetInt(city);
            IList<MSourceSueHotel> list = bsource.GetListSueHotel(pageIndex, this.pageSize, ref pageCount, tourid, this.SiteUserInfo.UserId, mSourceHotels);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    recordCount = list.Count;
                    this.RepList.DataSource = list;
                    this.RepList.DataBind();
                    BindPage();
                }
                else
                {
                    //提示没有查询到酒店
                    this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='9' align='center'>未找到相关酒店,建议您修改相关查询条件后再查询！</td></tr>" });
                    ExporPageInfoSelect1.Visible = false;
                }

            }
            else
            {
                //提示没有查询到酒店
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='9' align='center'>未找到相关酒店,建议您修改相关查询条件后再查询！</td></tr>" });
                ExporPageInfoSelect1.Visible = false;
            }
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = pageCount;
            this.ExporPageInfoSelect1.PageLinkURL = "#/UseSupplier/UseSupplier.aspx?";
        }
        #endregion

        /// <summary>
        /// 根据预控类型显示预控时间（总控显示开始日期，单控显示起、止日期）
        /// </summary>
        /// <param name="PeriodType"></param>
        /// <returns></returns>
        protected string GetPeriodTime(object PeriodType, object StartTime, object EndTime)
        {
            EyouSoft.Model.EnumType.SourceStructure.SourceControlType type = (EyouSoft.Model.EnumType.SourceStructure.SourceControlType)PeriodType;
            string str = string.Empty;
            str = UtilsCommons.GetDateString(DateTime.Parse(StartTime.ToString()), ProviderToDate);
            if (type == EyouSoft.Model.EnumType.SourceStructure.SourceControlType.单控)
            {
                str = UtilsCommons.GetDateString(DateTime.Parse(StartTime.ToString()), ProviderToDate) + "-" + UtilsCommons.GetDateString(DateTime.Parse(EndTime.ToString()), ProviderToDate);
            }
            return str;
        }
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="sid">供应商编号</param>
        /// <returns></returns>
        protected string GetContactInfo(object sid, string type)
        {
            string sourceID = sid.ToString();
            EyouSoft.BLL.SourceStructure.BSource bll = new BSource();
            EyouSoft.Model.SourceStructure.MSourceHotel model = bll.GetOneHotelModel(sourceID);
            StringBuilder stb = new System.Text.StringBuilder();
            if (model != null)
            {
                IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = model.LinkManList;
                bool IsPermission = false;
                bool IsRecommend = false;
                if (model.SourceModel != null)
                {
                    IsPermission = model.SourceModel.IsPermission;
                    IsRecommend = model.SourceModel.IsRecommend;
                }
                switch (type)
                {
                    //供应商是否签单和推荐
                    case "icon":
                        stb.Append(EyouSoft.Common.UtilsCommons.GetCompanyRecommend((object)IsRecommend, (object)IsPermission));
                        break;
                    case "name":
                        if (list != null && list.Count > 0)
                        {
                            stb.Append(list[0].Name);
                        }
                        break;
                    case "tel":
                        if (list != null && list.Count > 0)
                        {
                            stb.Append(string.IsNullOrEmpty(list[0].Telephone) ? list[0].MobilePhone : list[0].Telephone);
                        }
                        break;
                    case "fax":
                        if (list != null && list.Count > 0)
                        {
                            stb.Append(list[0].Fax);
                        }
                        break;
                    case "list":
                        if (list != null && list.Count > 0)
                        {
                            stb.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' width='7%' align='center'>编号</th><th width='19%' align='center'>联系人</th><th align='center' width='20%'>电话</th><th align='center'>手机</th><th align='center' width='18%'>传真</th></tr>");
                            for (int i = 0; i < list.Count; i++)
                            {
                                stb.Append("<tr><td align='center' width='7%'>" + (i + 1).ToString() + "</td><td  width='19%' align='center'>" + list[i].Name + "</td><td align='center' width='20%'>" + list[i].Telephone + "</td><td align='center' width='18%'>" + list[i].MobilePhone + "</td><td align='center' width='19%'>" + list[i].Fax + "</td></tr>");
                            }
                            stb.Append("</table>");
                        }
                        break;
                }
            }
            return stb.ToString();
        }
    }
}
