//单项服务供应商确认件 汪奇志 2013-05-10
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
    /// 单项服务供应商确认件
    /// </summary>
    public partial class DanXiangYeWuGysQueRenDan : System.Web.UI.Page
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
        /// <summary>
        /// 供应商安排编号
        /// </summary>
        string AnPaiId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            AnPaiId = Utils.GetQueryStringValue("anpaiid");
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!isLogin) Utils.RCWE("异常请求");
            if (string.IsNullOrEmpty(TourId) || string.IsNullOrEmpty(AnPaiId)) Utils.RCWE("异常请求");

            InitInfo();
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
            if (info.PlanBaseInfoList == null || info.PlanBaseInfoList.Count == 0) Utils.RCWE("异常请求：不存在的供应商安排");            

            txtCompanyName.Text = SiteUserInfo.CompanyName;
            txtXiaoShouYuanName.Text = info.SellerName;

            var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(info.SellerId, SiteUserInfo.CompanyId);
            if (xiaoShouYuanInfo != null)
            {
                txtXiaoShouYuanTelephone.Text = xiaoShouYuanInfo.ContactTel;
                txtXiaoShouYuanFax.Text = xiaoShouYuanInfo.ContactFax;
            }

            ltrTourCode.Text = info.TourCode;

            EyouSoft.Model.PlanStructure.MPlanBaseInfo anPaiInfo = null;

            foreach (var item in info.PlanBaseInfoList)
            {
                if (item.PlanId == AnPaiId) { anPaiInfo = item; break; }
            }

            if (anPaiInfo == null) Utils.RCWE("异常请求：不存在的供应商安排");

            ltrJuTiAnPai.Text = Utils.TextareaToHTML(anPaiInfo.GuideNotes);
            ltrFeiYongMingXi.Text = Utils.TextareaToHTML(anPaiInfo.CostDetail);
            ltrShuLiang.Text = anPaiInfo.Num.ToString();
            ltrZhiFuFangShi.Text = anPaiInfo.PaymentType.ToString();
            ltrJieSuanJinE.Text = anPaiInfo.Confirmation.ToString("C2");

            txtGysName.Text = anPaiInfo.SourceName;
            txtGysLxr.Text = anPaiInfo.ContactName;
            txtGysTelephone.Text = anPaiInfo.ContactPhone;
            txtGysFax.Text = anPaiInfo.ContactFax;

            ltrQianFaRiQi.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion
    }
}
