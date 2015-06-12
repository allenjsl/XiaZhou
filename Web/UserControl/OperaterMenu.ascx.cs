using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.UserControl
{
    /// <summary>
    /// 计调中心-地接计调，出境计调菜单列表
    /// 创建人：李晓欢
    /// 创建时间：2011-09-14
    /// </summary>
    public partial class OperaterMenu : System.Web.UI.UserControl
    {
        #region attributes
        private string _IndexClass;
        public string IndexClass
        {
            get { return _IndexClass; }
            set { _IndexClass = value; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 导游任务单-打印路径
        /// </summary>
        protected string Print_DaoYouRenWuDan = string.Empty;
        /// <summary>
        /// 游客行程单-打印路径
        /// </summary>
        protected string Print_XingChengDan = string.Empty;
        /// <summary>
        /// 游客名单-打印路径
        /// </summary>
        protected string Print_YouKeMingDan = string.Empty;
        /// <summary>
        /// 订单信息汇总表-打印路径
        /// </summary>
        protected string Print_DingDanXingXiHuiZongBiao = string.Empty;
        /// <summary>
        /// 团队编号
        /// </summary>
        protected string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");

            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(TourId);

            var bll = new EyouSoft.BLL.ComStructure.BComSetting();
            Print_DaoYouRenWuDan = bll.GetPrintUri(CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.导游任务单);
            if (tourType == TourType.出境团队
                ||tourType== TourType.地接团队
                || tourType == TourType.组团团队) Print_XingChengDan = bll.GetPrintUri(CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            else Print_XingChengDan = bll.GetPrintUri(CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);
            Print_YouKeMingDan = bll.GetPrintUri(CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游客名单);
            Print_DingDanXingXiHuiZongBiao = bll.GetPrintUri(CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订单信息汇总表);

            if (!string.IsNullOrEmpty(Print_DaoYouRenWuDan) && Print_DaoYouRenWuDan != "javascript:void(0)") Print_DaoYouRenWuDan += "?tourid=" + TourId;
            else Print_DaoYouRenWuDan = "javascript:void(0)";

            if (!string.IsNullOrEmpty(Print_XingChengDan) && Print_XingChengDan != "javascript:void(0)") Print_XingChengDan += "?tourid=" + TourId;
            else Print_XingChengDan = "javascript:void(0)";

            if (!string.IsNullOrEmpty(Print_YouKeMingDan) && Print_YouKeMingDan != "javascript:void(0)") Print_YouKeMingDan += "?tourid=" + TourId;
            else Print_YouKeMingDan = "javascript:void(0)";

            if (!string.IsNullOrEmpty(Print_DingDanXingXiHuiZongBiao) && Print_DingDanXingXiHuiZongBiao != "javascript:void(0)") Print_DingDanXingXiHuiZongBiao += "?type=2&tourid=" + TourId;
            else Print_DingDanXingXiHuiZongBiao = "javascript:void(0)";
        }
    }
}