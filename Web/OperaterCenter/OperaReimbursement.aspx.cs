using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心—计调报账
    /// 创建人：李晓欢
    /// 创建时间：2011-09-16
    /// </summary>
    public partial class OperaReimbursement : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        /// <summary>
        /// 打印页面Url
        /// </summary>
        protected string printUrl = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            printUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            PowerControl();

            if (UtilsCommons.IsToXls()) ToXls();
            
            DataInit();
        }

        #region private members
        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var chaXun = GetChaXunInfo();
            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetPlanBZList(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun, this.SiteUserInfo.DeptId);
            if (list != null && list.Count > 0)
            {
                this.replist.DataSource = list;
                this.replist.DataBind();
                BindPage();
            }
            else
            {
                this.lab_text.Text = "对不起，没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
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
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调报账_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调报账_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MBZSearch GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TourStructure.MBZSearch();

            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.RouteName = Utils.GetQueryStringValue("txtRouteName");
            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStatTime"));
            info.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtTimeEnd"));
            info.Guide = guid1.GuidName = Utils.GetQueryStringValue(this.guid1.GuidNameClient);
            info.GuideId = guid1.GuidID = Utils.GetQueryStringValue(this.guid1.GuidIDClient);
            info.SellerId = sellers1.SellsID = Utils.GetQueryStringValue(this.sellers1.SellsIDClient);
            info.SellerName = sellers1.SellsName = Utils.GetQueryStringValue(this.sellers1.SellsNameClient);
            info.IsDealt = Utils.GetQueryStringValue("isDealt") == "1";
            info.TourId = Utils.GetQueryStringValue("tourid");
            info.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus), Utils.GetQueryStringValue("tourState"));
            info.PlanerId = txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsIDClient);
            info.Planer = txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            return info;
        }

        /// <summary>
        /// toxls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.TourStructure.BTour().GetPlanBZList(this.SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun, this.SiteUserInfo.DeptId);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            StringBuilder s = new StringBuilder();
            s.Append("团号\t线路名称\t出团日期\t人数\t导游\t计调\t销售员\t收入\t支出\t毛利\t状态\n");

            foreach (var item in items)
            {
                s.Append(item.TourCode + "\t");
                s.Append(item.RouteName + "\t");
                s.Append(item.LDate.ToString("yyyy-MM-dd") + "\t");
                s.Append(item.Adults.ToString() + "+" + item.Childs.ToString() + "\t");
                s.Append(GetGuidInfoHtml(item.MGuidInfo) + "\t");
                s.Append(GetOperaterList(item.MPlanerInfo) + "\t");
                s.Append(item.SellerName + "\t");
                s.Append(item.ZongShouRu.ToString("F2") + "\t");
                s.Append(item.TourPay.ToString("F2") + "\t");
                s.Append(item.Profit.ToString("F2") + "\t");
                s.Append(item.TourStatus + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 绑定导游
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetGuidInfoHtml(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MGuidInfo> info = (IList<EyouSoft.Model.TourStructure.MGuidInfo>)o;
            if (info != null && info.Count > 0)
            {
                for (int i = 0; i < info.Count; i++)
                {
                    if (i == info.Count - 1)
                    {
                        sb.Append("" + info[i].Name + "");
                    }
                    else
                    {
                        sb.Append("" + info[i].Name + ",");
                    }
                }
            }
            return sb.ToString();
        }

        protected string GetOperaterList(object o)
        {
            System.Text.StringBuilder _s = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MTourPlaner> PlanerList = (IList<EyouSoft.Model.TourStructure.MTourPlaner>)o;
            if (PlanerList != null && PlanerList.Count > 0)
            {
                for (int i = 0; i < PlanerList.Count; i++)
                {
                    if (i == PlanerList.Count - 1)
                    {
                        _s.Append("" + PlanerList[i].Planer + "");
                    }
                    else
                    {
                        _s.Append("" + PlanerList[i].Planer + ",");
                    }
                }
            }
            return _s.ToString();
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
