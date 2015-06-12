using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.TourStructure;

namespace Web.ResourceCenter.CommonPage
{
    /// <summary>
    /// 预控 已使用数量
    /// </summary>
    /// 创建人:柴逸宁
    public partial class USED : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 分页参数
            int pageSize = 5;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            IList<MSueUse> ls = null;
            SourceControlCategory sourceControlCategory = (SourceControlCategory)Utils.GetInt(Utils.GetQueryStringValue("sourceControlCategory"));
            string sueid = Utils.GetQueryStringValue("sueId");
            switch (sourceControlCategory)
            {
                case SourceControlCategory.车辆:
                    ls = new BSourceControl().GetCarUseList(CurrentUserCompanyID, sueid, pageIndex, pageSize, ref recordCount);
                    break;
                case SourceControlCategory.酒店:
                    ls = new BSourceControl().GetHotelUseList(sueid, CurrentUserCompanyID, pageIndex, pageSize, ref recordCount);
                    break;
                case SourceControlCategory.游轮:
                    ls = new BSourceControl().GetShipUseList(CurrentUserCompanyID, sueid, pageIndex, pageSize, ref recordCount);
                    break;
                case SourceControlCategory.景点:
                    ls = new BSourceControl().GetSightUseList(CurrentUserCompanyID, sueid, pageIndex, pageSize, ref recordCount);
                    break;
                case SourceControlCategory.其他:
                    ls = new BSourceControl().GetOtherUseList(CurrentUserCompanyID, sueid, pageIndex, pageSize, ref recordCount);
                    break;
            }
            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;

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
        /// 获取导游导游电话或者游客游客电话
        /// </summary>
        /// <param name="list">导游列表</param>
        /// <returns></returns>
        protected string GetGuideOrTouristStr(object guideList, object touristList)
        {
            try
            {
                SourceControlCategory sourceControlCategory = (SourceControlCategory)Utils.GetInt(Utils.GetQueryStringValue("sourceControlCategory"));
                if (sourceControlCategory == SourceControlCategory.游轮)
                {
                    IList<MTourOrderTraveller> ls = (IList<MTourOrderTraveller>)touristList;
                    if (ls != null && ls.Count > 0)
                    {
                        return ls[0].CnName + "</td><td align=center>" + ls[0].Contact;
                    }
                }
                else
                {
                    IList<MSourceGuide> ls = (IList<MSourceGuide>)guideList;
                    if (ls != null && ls.Count > 0)
                    {
                        return ls[0].Name + "</td><td align=center>" + ls[0].Mobile;
                    }
                }

                return "</td><td align=center>";
            }
            catch
            {
                return "</td><td align=center>";
            }
        }

    }
}
