using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 酒店-最后确认单
    /// 方琪 2012-05-22
    /// </summary>
    public partial class jiudianyukongquerendan : System.Web.UI.Page
    {
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        protected void Page_Load(object sender, EventArgs e)
        {
            string sueId = Utils.GetQueryStringValue("sueId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(sueId) && SiteUserInfo != null)
            {
                InitPage(sueId);
            }
        }

        protected void InitPage(string sueId)
        {
            IList<MSueUse> ls = null;
            IList<MSueUse> newls = null;
            int recordCount = 0;
            ls = new BSourceControl().GetHotelUseList(sueId, this.SiteUserInfo.CompanyId, 1, 1, ref recordCount);
            if (recordCount > 0)
            {
                newls = new BSourceControl().GetHotelUseList(sueId, this.SiteUserInfo.CompanyId, 1, recordCount, ref recordCount);
            }
            if (newls != null && newls.Count > 0)
            {
                this.rpt_tuandui.DataSource = newls;
                this.rpt_tuandui.DataBind();
            }
            else
            {
                this.ph_tuandui.Visible = false;
            }
            MSourceSueHotel hotelModel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByHotelId(sueId, this.SiteUserInfo.CompanyId);
            if (hotelModel != null)
            {
                this.lbHotelName.Text = hotelModel.SourceName;
                this.lbRoomType.Text = hotelModel.RoomType;
                this.lbRoomNum.Text = hotelModel.ControlNum.ToString();
                this.lbDate.Text = hotelModel.PeriodStartTime.ToString("yyyy-MM-dd");
                this.lbLastDate.Text = hotelModel.LastTime.ToString("yyyy-MM-dd");
                this.lbRoomPrice.Text = UtilsCommons.GetMoneyString(hotelModel.UnitPrice, ProviderToMoney);
                this.lbTotalPrcie.Text = UtilsCommons.GetMoneyString(hotelModel.TotalPrice, ProviderToMoney);
                this.lbPreMoney.Text = UtilsCommons.GetMoneyString(hotelModel.Advance, ProviderToMoney);
                this.lbRemark.Text = hotelModel.Remark;
            }
        }

        /// <summary>
        /// 获取导游导游电话或者游客游客电话
        /// </summary>
        /// <param name="list">导游列表</param>
        /// <returns></returns>
        protected string GetGuideOrTouristStr(object guideList)
        {
            try
            {
                IList<MSourceGuide> ls = (IList<MSourceGuide>)guideList;
                if (ls != null && ls.Count > 0)
                {
                    return ls[0].Name + "</td><td align=center>" + ls[0].Mobile;
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
