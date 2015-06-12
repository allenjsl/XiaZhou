//单项服务委托预定单 汪奇志 2013-05-10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.PrintPage.xz
{
    /// <summary>
    /// 单项服务委托预定单
    /// </summary>
    public partial class DanXiangYeWuYouKeQueRenDan : System.Web.UI.Page
    {
        #region attributes
        /// <summary>
        /// 登录用户信息
        /// </summary>
        EyouSoft.Model.SSOStructure.MUserInfo SiteUserInfo = null;
        /// <summary>
        /// 单项业务编号
        /// </summary>
        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!isLogin) Utils.RCWE("异常请求");
            if (string.IsNullOrEmpty(TourId)) Utils.RCWE("异常请求");

            InitInfo();
            InitYinHangZhangHu();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.TourStructure.BSingleService().GetSingleServiceExtendByTourId(TourId);
            if (info == null) Utils.RCWE("异常请求");
            if (info.CompanyId != SiteUserInfo.CompanyId) Utils.RCWE("异常请求");

            txtKeHuName.Text = info.BuyCompanyName;
            txtKeHuLxr.Text = info.ContactName;
            txtKeHuTelephone.Text = info.ContactTel;

            var keHuLxrInfo = new EyouSoft.BLL.CrmStructure.BCrmLinkMan().GetLinkManModel(info.ContactDepartId);
            if (keHuLxrInfo != null) txtKeHuFax.Text = keHuLxrInfo.Fax;

            txtCompanyName.Text = SiteUserInfo.CompanyName;
            txtXiaoShouYuanName.Text = info.SellerName;

            var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(info.SellerId, SiteUserInfo.CompanyId);
            if (xiaoShouYuanInfo != null)
            {
                txtXiaoShouYuanTelephone.Text = xiaoShouYuanInfo.ContactTel;
                txtXiaoShouYuanFax.Text = xiaoShouYuanInfo.ContactFax;
            }

            ltrOrderCode.Text = info.OrderCode;

            rptJuTiYaoQiu.DataSource = info.TourTeamPriceList;
            rptJuTiYaoQiu.DataBind();

            ltrHeTongJinE.Text = info.TourIncome.ToString("C2");

            rptYouKe.DataSource = info.TourOrderTravellerList;
            rptYouKe.DataBind();

            ltrQianFaRiQi.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 初始化银行账户信息
        /// </summary>
        void InitYinHangZhangHu()
        {
            var items = new EyouSoft.BLL.ComStructure.BComAccount().GetList(this.SiteUserInfo.CompanyId);

            if (items != null && items.Count > 0)
            {
                rptYinHangZhangHu.DataSource = items;
                rptYinHangZhangHu.DataBind();
            }

            items = null;
        }
        #endregion
    }
}
