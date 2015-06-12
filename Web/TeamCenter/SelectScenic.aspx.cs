using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.SourceStructure;
using System.Collections;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.TeamCenter
{
    public partial class SelectScenic : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页数量
        /// </summary>
        protected int pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 一共多少页
        /// </summary>
        int recordCount = 0;
        protected int listCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Rpt_List();
        }

        #region private members
        void Rpt_List()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var chaXun = new EyouSoft.Model.GysStructure.MXuanYongChaXunInfo();
            chaXun.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCountry"));
            chaXun.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            chaXun.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            chaXun.DistrictId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlArea"));
            chaXun.JingDianName = Utils.GetQueryStringValue("textfield");

            var items = new EyouSoft.BLL.GysStructure.BGys().GetXuanYongJingDians(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                listCount = items.Count;
                rpt_ScenicList.DataSource = items;
                rpt_ScenicList.DataBind();

                ExporPageInfoSelect1.PageLinkURL = ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                ExporPageInfoSelect1.UrlParams = Request.QueryString;
                ExporPageInfoSelect1.intPageSize = pageSize;
                ExporPageInfoSelect1.CurrencyPage = pageIndex;
                ExporPageInfoSelect1.intRecordCount = recordCount;
            }
            else
            {
                litMsg.Text = "<tr class='old'><td colspan='11' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }
        #endregion
    }
}
