//预算表-支出明细 汪奇志 2014-02-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 预算表-支出明细
    /// </summary>
    public partial class YuSuanBiaoZhiChu : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");

            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.StatStructure.BYuSuanBiao().GetZhiChus(TourId);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                if (items.Count > 1)
                {
                    decimal zhiChuJinE = 0;

                    foreach (var item in items)
                    {
                        zhiChuJinE += item.JinE;
                    }

                    ltrJinE.Text = zhiChuJinE.ToString("F2");
                }
                else
                {
                    phHeJi.Visible = false;
                }
            }
            else
            {
                phEmpty.Visible = true;
                phHeJi.Visible = false;
            }
        }
        #endregion
    }
}
