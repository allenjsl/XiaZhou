using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    public partial class XuanZeHangQi : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int pageSize = 40;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;
            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHangQiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();

            info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing), Utils.GetQueryStringValue("txtLeiXing"));
            info.MingCheng = Utils.GetQueryStringValue("txtMingCheng");
            info.BianHao = Utils.GetQueryStringValue("txtBianHao");

            return info;
        }
        #endregion
    }
}
