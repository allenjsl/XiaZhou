using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.SourceStructure;
using System.Text;

namespace Web.ResourceManage.AjaxRequest
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-11
    /// 说明: Ajax 请求供应商信息
    public partial class AjaxSupplier : BackPage
    {
        #region attributes
        protected int pageSize = 24;
        protected int pageIndex = 0;
        protected int recordCount = 0;
        protected int listCount = 0;
        protected string NodataMsg = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            string gysLeiXing = Utils.GetQueryStringValue("type");
            var chaXun = GetChaXunInfo();
            pageIndex = UtilsCommons.GetPadingIndex();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetXuanYongs(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                listCount = items.Count;
                RepList.DataSource = items;
                RepList.DataBind();

                BindPage(Utils.GetQueryStringValue("urltype"));
            }
            else
            {
                NodataMsg = "<tr class='old'><td colspan='11' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MXuanYongChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.GysStructure.MXuanYongChaXunInfo();
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("provice"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("city"));
            info.DistrictId = Utils.GetIntNull(Utils.GetQueryStringValue("area"));
            info.GysName = Utils.GetQueryStringValue("name");
            info.IsLxr = true;

            if (info.ProvinceId.HasValue && info.ProvinceId.Value < 1) info.ProvinceId = null;
            if (info.CityId.HasValue && info.CityId.Value < 1) info.CityId = null;
            if (info.DistrictId.HasValue && info.DistrictId.Value < 1) info.DistrictId = null;

            string gysLeiXing = Utils.GetQueryStringValue("type");
            if (!string.IsNullOrEmpty(gysLeiXing)) gysLeiXing = gysLeiXing.ToLower();

            switch (gysLeiXing)
            {
                case "car":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.车队; break;
                case "hotel":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.餐馆; break;
                case "wineshop":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.酒店; break;
                case "cruise":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.游轮; break;
                case "ground":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.地接社; break;
                case "shopping":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.购物; break;
                case "scenicspots":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.景点; break;
                case "ticket":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.票务; break;
                case "other":info.GysLeiXing= EyouSoft.Model.EnumType.SourceStructure.SourceType.其他; break;
                default: info.GysLeiXing = EyouSoft.Model.EnumType.SourceStructure.SourceType.其他; break;
            }

            return info;
        }

        void BindPage(string urltype)
        {
            if (!string.IsNullOrEmpty(urltype))
            {
                this.ExporPageInfoSelect1.PageLinkURL = "/CommonPage/Supplier.aspx?";
            }
            else
            {
                this.ExporPageInfoSelect1.PageLinkURL = "/CommonPage/UseSupplier.aspx?";
            }
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="linkManlist"></param>
        /// <returns></returns>
        protected string GetContactInfo(object lxrs, string type, string sid, string sName,object jiuDianQianTaiTelephone)
        {
            IList<EyouSoft.Model.GysStructure.MLxrInfo> list = (IList<EyouSoft.Model.GysStructure.MLxrInfo>)lxrs;
            StringBuilder stb = new System.Text.StringBuilder();
            switch (type)
            {
                case "name":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(list[0].Name);
                    }
                    break;
                case "tel":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(string.IsNullOrEmpty(list[0].Telephone) ? list[0].Mobile : list[0].Telephone);
                    }
                    break;
                case "fax":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(list[0].Fax);
                    }
                    break;
                case "list":
                    if (jiuDianQianTaiTelephone == null) jiuDianQianTaiTelephone = string.Empty;
                    if (list != null && list.Count > 0)
                    {
                        stb.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' width='7%' align='center'></th><th width='19%' align='center'>联系人</th><th align='center' width='20%'>电话</th><th align='center'>手机</th><th align='center' width='18%'>传真</th></tr>");
                        for (int i = 0; i < list.Count; i++)
                        {
                            stb.Append("<tr><td align='center' width='7%'><input onclick='useSupplierPage.RadioClickFun(this);' type='radio' value='" + sid + "," + sName + "," + list[i].Name + "," + list[i].Telephone + "," + list[i].Fax + "," + jiuDianQianTaiTelephone.ToString() + "' /></td><td  width='19%' align='center'>" + list[i].Name + "</td><td align='center' width='20%'>" + list[i].Telephone + "</td><td align='center' width='18%'>" + list[i].Mobile + "</td><td align='center' width='19%'>" + list[i].Fax + "</td></tr>");
                        }
                        stb.Append("</table>");
                    }
                    break;
            }
            return stb.ToString();
        }

        protected string GetContactInfo(object linkManlist, string type)
        {
            return GetContactInfo(linkManlist, type, "", "", "");
        }
        #endregion
    }
}
