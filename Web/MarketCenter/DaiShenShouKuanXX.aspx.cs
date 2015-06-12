using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.MarketCenter
{
    /// <summary>
    /// 待审核收款明细
    /// </summary>
    public partial class DaiShenShouKuanXX : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("istoxls") == "1") ToXls();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            string s = Utils.GetQueryStringValue("orderid");
            string[] s1 = s.Split(',');

            var items = new EyouSoft.BLL.FinStructure.BFinance().GetBatchTourOrderSalesCheck(s1);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                decimal heJiJinE = 0;
                foreach (var item in items)
                {
                    heJiJinE += item.CollectionRefundAmount;
                }
                ltrHeJiJinE.Text = UtilsCommons.GetMoneyString(heJiJinE, ProviderToMoney);

                phEmpty.Visible = false;
                phHeJiJinE.Visible = true;
            }
            else
            {
                phHeJiJinE.Visible = false;
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            string s = Utils.GetQueryStringValue("orderid");
            string[] s1 = s.Split(',');

            string[] s2 = Utils.GetFormValues("chk");

            if (s2 == null || s2.Length == 0) ResponseToXls(string.Empty);

            var items = new EyouSoft.BLL.FinStructure.BFinance().GetBatchTourOrderSalesCheck(s1);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            System.Text.StringBuilder s3 = new System.Text.StringBuilder();
            s3.Append("订单号\t收款日期\t收款人\t收款金额\t收款方式\t备注\n");

            foreach (var item in items)
            {
                if (s2.Contains(item.Id))
                {
                    s3.Append(item.OrderCode + "\t");
                    s3.Append(item.CollectionRefundDate.Value.ToString("yyyy-MM-dd") + "\t");
                    s3.Append(item.CollectionRefundOperator + "\t");
                    s3.Append(item.CollectionRefundAmount.ToString("F2") + "\t");
                    s3.Append(item.CollectionRefundModeName + "\t");
                    s3.Append(item.Memo.Replace("\t", "    ").Replace("\r\n", "    ") + "\n");
                }
            }

            ResponseToXls(s3.ToString());
        }
        #endregion
    }
}
