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
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 游船-最后确认单
    /// 方琪 2012-05-22
    /// </summary>
    public partial class youchuanyukongquerendan : System.Web.UI.Page
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
            ls = new BSourceControl().GetShipUseList(this.SiteUserInfo.CompanyId, sueId, 1, 1, ref recordCount);
            if (recordCount > 0)
            {
                newls = new BSourceControl().GetShipUseList(this.SiteUserInfo.CompanyId, sueId, 1, recordCount, ref recordCount);
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
            MSourceSueShip shipModel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByShipId(sueId, this.SiteUserInfo.CompanyId);
            if (shipModel != null)
            {
                this.lbBoatCompany.Text = shipModel.ShipCompany;
                this.lbBoatName.Text = shipModel.ShipName;
                this.lbBoatNum.Text = shipModel.ControlNum.ToString();
                this.lbPreDate.Text = shipModel.GoShipTime.HasValue ? shipModel.GoShipTime.Value.ToString("yyyy-MM-dd") : "";
                this.lbLastDate.Text = shipModel.LastTime.HasValue ? shipModel.LastTime.Value.ToString("yyyy-MM-dd") : "";
                this.lbBoatPrice.Text = UtilsCommons.GetMoneyString(shipModel.UnitPrice, ProviderToMoney);
                this.lbTotalPrice.Text = UtilsCommons.GetMoneyString(shipModel.TotalPrice, ProviderToMoney);
                this.lbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(shipModel.Remark);
            }
        }

        /// <summary>
        /// 获取导游导游电话或者游客游客电话
        /// </summary>
        /// <param name="list">导游列表</param>
        /// <returns></returns>
        protected string GetGuideOrTouristStr(object touristList)
        {
            try
            {
                IList<MTourOrderTraveller> ls = (IList<MTourOrderTraveller>)touristList;
                if (ls != null && ls.Count > 0)
                {
                    return ls[0].CnName + "</td><td align=center>" + ls[0].Contact;
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
