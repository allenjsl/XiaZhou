using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.FinStructure;

namespace Web.FinanceManage.Arrearage
{
    /// <summary>
    /// 销售欠款预警-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class SellList : BackPage
    {
        /// <summary>
        /// 部门下拉
        /// </summary>
        protected string DepartmentOptionStr = string.Empty;
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

            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            txt_SellsSelect.SetTitle = "选择 销售员";
            txt_SellsSelect.SellsName = Utils.GetQueryStringValue(txt_SellsSelect.SellsNameClient);
            txt_SellsSelect.SellsID = Utils.GetQueryStringValue(txt_SellsSelect.SellsIDClient);
            MWarningBase model = new MWarningBase();
            model.SellerId = Utils.GetQueryStringValue(txt_SellsSelect.SellsIDClient);
            model.SellerName = Utils.GetQueryStringValue(txt_SellsSelect.SellsNameClient);
            model.SignArrear = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            model.Arrear = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));
            model.SignTransfinite = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator));
            model.Transfinite = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber));
            //部门
            int department = Utils.GetIntSign(Utils.GetQueryStringValue("sel_department"), -1);
            if (department >= 0)
            {
                model.DeptId = department;
            }
            IList<MComDepartment> lst = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(CurrentUserCompanyID);
            StringBuilder sb = new StringBuilder();
            foreach (MComDepartment item in lst)
            {
                sb.Append("<option " + ((item.DepartId == model.DeptId) ? "selected=selected" : "") + " value=" + item.DepartId + ">-" + item.DepartName + "-</option>");
            }
            DepartmentOptionStr = sb.ToString();
            IList<MSalesmanWarning> ls = new BFinance().GetSalesmanWarningLst(
                       pageSize,
                       pageIndex,
                       ref recordCount,
                       CurrentUserCompanyID,
                       model);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect2.UrlParams = Request.QueryString;
            ExporPageInfoSelect2.intPageSize = pageSize;
            ExporPageInfoSelect2.CurrencyPage = pageIndex;
            ExporPageInfoSelect2.intRecordCount = recordCount;


        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_欠款预警_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_欠款预警_栏目, true);
                return;
            }
        }

        #endregion
    }
}
