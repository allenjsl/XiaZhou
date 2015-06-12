using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.MarketCenter
{
    /// <summary>
    /// 销售中心-销售报账
    /// 邵权江 2012-04-13
    /// </summary>
    public partial class XiaoShouBaoZhang : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPages = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PrintPages = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);
            if (!IsPostBack)
            {
                PowerControl();
                DataInit();
            }
        }

        #region private members
        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            #region 绑定状态
            int tourStatus = Utils.GetInt(Utils.GetQueryStringValue("sltTourStatus"), -1);
            this.litTourStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus)), tourStatus.ToString(), true);
            #endregion
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            var searchInfo = GetSearchInfo(tourStatus);
            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetSaleBZList(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchInfo, this.SiteUserInfo.DeptId);

            if (list != null && list.Count > 0)
            {
                this.rpt.DataSource = list;
                this.rpt.DataBind();
            }
            else
            {
                this.rpt.Controls.Add(new Label() { Text = "<tr><td colspan='13' align='center'>对不起，没有相关数据！</td></tr>" });
            }
            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
            if (recordCount <= pageSize)
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MBZSearch GetSearchInfo(int tourStatus)
        {
            //导游
            string guidId = Utils.GetQueryStringValue(txtDaoYou.GuidIDClient);
            string guidName = Utils.GetQueryStringValue(txtDaoYou.GuidNameClient);
            if (!string.IsNullOrEmpty(guidId) && !string.IsNullOrEmpty(guidName))
            {
                this.txtDaoYou.GuidID = guidId;
                this.txtDaoYou.GuidName = guidName;
            }
            //销售员
            string selerId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient);
            string selerName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            if (!string.IsNullOrEmpty(selerId))
            {
                this.txtXiaoShouYuan.SellsID = selerId;
            }
            if (!string.IsNullOrEmpty(selerName))
            {
                this.txtXiaoShouYuan.SellsName = selerName;
            }

            var info = new EyouSoft.Model.TourStructure.MBZSearch();
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.RouteName = Utils.GetQueryStringValue("txtRouteName");
            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLSDate"));
            info.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLEDate"));
            info.Guide = guidName;
            info.GuideId = guidId;
            info.SellerId = selerId;
            info.SellerName = selerName;
            info.IsDealt = Utils.GetInt(Utils.GetQueryStringValue("sltStatus"), 0) != 1 ? false : true;
            if (tourStatus > -1)
            {
                info.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
            }

            info.FaBuRenId = txtFaBuRen.SellsID = Utils.GetQueryStringValue(txtFaBuRen.SellsIDClient);
            info.FaBuRenName = txtFaBuRen.SellsName = Utils.GetQueryStringValue(txtFaBuRen.SellsNameClient);

            info.PlanerId = txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsIDClient);
            info.Planer = txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            return info;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_栏目, true);
                return;
            }
        }
        #endregion


        #region protected members
        /// <summary>
        /// 打印单
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="TourType"></param>
        /// <returns></returns>
        protected string PrintUrl(object TourId, object TourType)
        {
            string Url = "#";
            if (TourType != null)
            {
                if (TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼.ToString() || TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼.ToString() || TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼.ToString())
                    Url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单) + "?tourId=" + TourId;
                if (TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.出境团队.ToString() || TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.地接团队.ToString() || TourType.ToString() == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队.ToString())
                    Url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单) + "?tourId=" + TourId;
            }
            return Url;

        }

        /// <summary>
        /// 获取导游
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetGuidInfo(object o)
        {
            string str = string.Empty;
            IList<EyouSoft.Model.TourStructure.MGuidInfo> info = (List<EyouSoft.Model.TourStructure.MGuidInfo>)o;
            if (info != null && info.Count > 0)
            {
                foreach (var item in info)
                {
                    str += item.Name + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        /// <summary>
        /// 获取计调员
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetPlanInfo(object o)
        {
            string str = string.Empty;
            IList<EyouSoft.Model.TourStructure.MTourPlaner> info = (List<EyouSoft.Model.TourStructure.MTourPlaner>)o;
            if (info != null && info.Count > 0)
            {
                foreach (var item in info)
                {
                    str += item.Planer + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        /// <summary>
        /// 获取收入的颜色
        /// </summary>
        /// <param name="tourStatus">团队状态</param>
        /// <returns></returns>
        protected string GetShouRuYanSe(object tourStatus)
        {
            if (tourStatus == null) return "fontred";
            var _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
            var _status = new EyouSoft.Model.EnumType.TourStructure.TourStatus[] { EyouSoft.Model.EnumType.TourStructure.TourStatus.封团
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审
                , EyouSoft.Model.EnumType.TourStructure.TourStatus.销售待审};

            if (_status.Contains(_tourStatus)) return string.Empty;

            return "fontred";
        }
        #endregion
    }
}
