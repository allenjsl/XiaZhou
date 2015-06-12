using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using System.Text;

namespace Web.FinanceManage.InvoiceManage
{
    /// <summary>
    /// 发票管理
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-10
    public partial class List : BackPage
    {
        protected bool IsShenHePower = false;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion

            var chaXunInfo = GetChaXunInfo();
            IList<MBill> ls = new BFinance().GetBillLst(pageSize, pageIndex, ref recordCount, CurrentUserCompanyID, chaXunInfo);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_发票管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_发票管理_栏目, true);
            }
            else
            {
                IsShenHePower = this.phdShenhe.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_发票管理_开票审核);
            }
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MBill GetChaXunInfo()
        {
            MBill info = new MBill();
            info.CompanyId = CurrentUserCompanyID;
            info.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            decimal billAmount = Utils.GetDecimal(Utils.GetQueryStringValue("billAmount"), -1);
            if (billAmount > 0)
            {
                info.BillAmount = billAmount;
            }
            info.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            info.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            info.CustomerId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            info.Customer = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            info.BillTimeS = Utils.GetQueryStringValue("billTimeS");
            info.BillTimeE = Utils.GetQueryStringValue("billTimeE");

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            StringBuilder s = new StringBuilder();

            var chaXunInfo = GetChaXunInfo();
            var items = new BFinance().GetBillLst(toXlsRecordCount, 1, ref _recordCount, CurrentUserCompanyID, chaXunInfo);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            s.Append("团号\t客户单位\t销售员\t金额\t开票时间\t票据号\n");
            foreach (var item in items)
            {
                s.Append(item.TourCode + "\t");
                s.Append(item.Customer + "\t");
                s.Append(item.SellerName + "\t");
                s.Append(item.BillAmount.Value.ToString("F2") + "\t");
                s.Append(item.BillTime.ToString("yyyy-MM-dd") + "\t");
                s.Append(item.BillNo + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
