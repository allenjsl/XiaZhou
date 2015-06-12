using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.CommonPage
{
    /// <summary>
    /// 团号列表
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-19
    /// 页面备注:
    /// isRadio=true;表示单选,该参数默认多选
    /// callBackFun 回调函数
    public partial class TourListBoxy : BackPage
    {
        /// <summary>
        /// 总数
        /// </summary>
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataInit();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 40;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #endregion
            MControlTourSearch queryString = new MControlTourSearch();
            queryString.LDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("lDateS"));
            queryString.LDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("lDateE"));
            queryString.TourCode = Utils.GetQueryStringValue("tourCode");
            queryString.TourType = (TourType?)Utils.GetEnumValueNull(typeof(TourType), Utils.GetQueryStringValue("tourType"));
            IList<MControlTour> ls = new BTour().GetControlTourList(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, queryString);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex);
            }
            else
            {
                ExporPageInfoSelect1.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;


        }
    }
}
