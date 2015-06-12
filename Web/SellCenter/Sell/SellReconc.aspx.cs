using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.SellCenter.Sell
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-13
    /// 说明：同业分销 中 销售收款：当日收款对账列表
    public partial class SellReconc : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();

                //初始化
                DataInit();
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            IList<EyouSoft.Model.EnumType.TourStructure.TourType> enumList = new List<EyouSoft.Model.EnumType.TourStructure.TourType>();
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼);
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.出境团队);
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼);
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.地接团队);
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼);
            enumList.Add(EyouSoft.Model.EnumType.TourStructure.TourType.组团团队);

            //IList<EyouSoft.Model.FinStructure.MDayReceivablesChk> list = new EyouSoft.BLL.FinStructure.BFinance().GetDayReceivablesChkLst(pageSize, pageIndex, ref recordCount, false, enumList, true, SiteUserInfo.CompanyId);
            //if (list != null && list.Count > 0)
            //{
            //    this.rptList.DataSource = list;
            //    this.rptList.DataBind();
            //}
            //else
            //{
            //    this.lblMsg.Text = "没有相关数据!";
            //    this.ExporPageInfoSelect1.Visible = false;
            //    this.ExporPageInfoSelect2.Visible = false;
            //}

            //绑定分页
            BindPage();
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
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
        private void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_查看当日收款))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.同业分销_销售收款_查看当日收款, false);
            //    return;
            //}
        }
        #endregion

    }
}
