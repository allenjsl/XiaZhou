using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.FinanceManage.DaiShou
{
    /// <summary>
    /// 代收登记、修改、查看
    /// </summary>
    public partial class Edit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 代收编号
        /// </summary>
        string DaiShouId = string.Empty;
        /// <summary>
        /// 代收登记权限
        /// </summary>
        bool Privs_DaiShouDengJi = false;
        /// <summary>
        /// 代收登记审批权限
        /// </summary>
        bool Privs_DaiShouShenPi = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            DaiShouId = Utils.GetQueryStringValue("daishouid");
            if (string.IsNullOrEmpty(TourId)) RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常"));

            InitPrivs();

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "submit": BaoCun(); break;
                case "shenpi": ShenPi(); break;
                default: break;
            }

            InitOrders();
            InitJiDiaoAnPais();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// 初始化订单信息
        /// </summary>
        void InitOrders()
        {
            var items = new EyouSoft.BLL.FinStructure.BDaiShou().GetOrders(TourId);

            if (items != null && items.Count > 0)
            {
                rptOrder.DataSource = items;
                rptOrder.DataBind();
            }
            else
            {
                phEmptyOrder.Visible = true;
            }
        }

        /// <summary>
        /// 初始化计调安排信息
        /// </summary>
        void InitJiDiaoAnPais()
        {
            var items = new EyouSoft.BLL.FinStructure.BDaiShou().GetAnPais(TourId);

            if (items != null && items.Count > 0)
            {
                rptAnPai.DataSource = items;
                rptAnPai.DataBind();
            }
            else
            {
                phEmptyAnPai.Visible = true;
            }
        }

        /// <summary>
        /// 初始化编辑信息
        /// </summary>
        void InitEditInfo()
        {
            if (!string.IsNullOrEmpty(DaiShouId))
            {
                var info = new EyouSoft.BLL.FinStructure.BDaiShou().GetInfo(DaiShouId);
                if (info == null) RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常"));

                txtBeiZhu.Value = info.BeiZhu;
                txtJinE.Value = info.JinE.ToString("F2");
                txtTime.Value = info.Time.ToString("yyyy-MM-dd");
                ltrDengJiRen.Text = info.OperatorName + "&nbsp;" + info.IssueTime.ToString("yyyy-MM-dd HH:mm");
                txtOrderId.Value = info.OrderId;
                txtAnPaiId.Value = info.AnPaiId;

                switch (info.Status)
                {
                    case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未审批:
                        string s = string.Empty;
                        if (Privs_DaiShouDengJi) s += "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
                        if (Privs_DaiShouShenPi && Utils.GetQueryStringValue("isshenpi") == "1") s += "<a id=\"i_a_shenpi\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">审批</a>";

                        if (!string.IsNullOrEmpty(s)) ltrOperatorHtml.Text = s;
                        else ltrOperatorHtml.Text = "你没有代收登记操作权限";
                        break;
                    case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未通过:
                        ltrOperatorHtml.Text = "该代收信息<b>审批未通过</b>";
                        break;
                    case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.已通过:
                        phShenPiRen.Visible = true;
                        ltrShenPiRen.Text = info.ShenPiRenName + "&nbsp;" + (info.ShenPiTime.HasValue ? info.ShenPiTime.Value.ToString("yyyy-MM-dd HH:mm") : "");
                        ltrOperatorHtml.Text = "该代收信息<b>已审批通过</b>";
                        break;
                    default: break;
                }

                return;
            }

            txtTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            ltrDengJiRen.Text = SiteUserInfo.Name + "&nbsp;" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (Privs_DaiShouDengJi) ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
            else ltrOperatorHtml.Text = "你没有代收登记权限";
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_DaiShouDengJi = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_代收登记);
            Privs_DaiShouShenPi = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_代收管理_审批);
        }

        /// <summary>
        /// 保存
        /// </summary>
        void BaoCun()
        {
            if (!Privs_DaiShouDengJi) RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));

            var info = new EyouSoft.Model.FinStructure.MDaiShouInfo();
            info.AnPaiId = Utils.GetFormValue("radioAnPai");
            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.CompanyId = SiteUserInfo.CompanyId;
            info.DaiShouId = DaiShouId;
            info.IssueTime = DateTime.Now;
            info.JinE = Utils.GetDecimal(Utils.GetFormValue(txtJinE.UniqueID));
            info.OperatorId = SiteUserInfo.UserId;
            info.OrderId = Utils.GetFormValue("radioOrder");
            info.Status = EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未审批;
            info.Time = Utils.GetDateTime(Utils.GetFormValue(txtTime.UniqueID), DateTime.Now);

            int bllRetCode=0;
            if (string.IsNullOrEmpty(info.DaiShouId)) bllRetCode = new EyouSoft.BLL.FinStructure.BDaiShou().Insert(info);
            else bllRetCode = new EyouSoft.BLL.FinStructure.BDaiShou().Update(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "选中的需要代收的订单信息不存在"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "代收信息不存在"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "代收信息已审批"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// 审批
        /// </summary>
        void ShenPi()
        {
            if (!Privs_DaiShouShenPi) RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));

            var info = new EyouSoft.Model.FinStructure.MDaiShouShenPiInfo();
            info.CompanyId = CurrentUserCompanyID;
            info.DaiShouId = DaiShouId;
            info.OperatorId = SiteUserInfo.UserId;
            info.Status = EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.已通过;
            info.Time = DateTime.Now;

            int bllRetCode = new EyouSoft.BLL.FinStructure.BDaiShou().ShenPi(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：代收信息已经审批或不存在"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
