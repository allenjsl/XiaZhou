using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class GysXuanYong : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {            
            int pageSize = 48;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var chaXun = new EyouSoft.Model.GysStructure.MXuanYongChaXunInfo();
            chaXun.GysName = Utils.GetQueryStringValue("txtName");
            chaXun.GysLeiXing = (EyouSoft.Model.EnumType.SourceStructure.SourceType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SourceStructure.SourceType), Utils.GetQueryStringValue("txtLeiXing"));
            chaXun.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));
            chaXun.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));

            var items = new EyouSoft.BLL.GysStructure.BGys().GetXuanYongs(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            else
            {

            }

            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            paging.Visible = paging.intRecordCount > paging.intPageSize;
            //phEmpty.Visible = paging.intRecordCount == 0;
        }
        #endregion
    }
}
