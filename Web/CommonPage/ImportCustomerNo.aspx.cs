using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.CommonPage
{
    public partial class ImportCustomerNo : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void PageInit()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int? ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("sltProvince"));
            int? CityId = Utils.GetIntNull(Utils.GetQueryStringValue("sltCity"));
            int? CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("sltCountry"));
            EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType? selCrmType = (EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType), Utils.GetQueryStringValue("selCrmType"));
            EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo search = new EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo();
            search.CityId = CityId;
            search.CountryId = CountryId;
            search.ProvinceId = ProvinceId;
            search.DanWeiType = selCrmType;
            var list = new EyouSoft.BLL.SmsStructure.BDaoRuLxr().GetLxrs(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, search);
            if (list == null || list.Count == 0)
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.repList.EmptyText = "<tr><td colspan=\"5\" align=\"center\">暂无相关记录!</td></tr>";
            }
            else
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        public string GetCompanyAddress(object o)
        {

            if (o != null)
            {
                EyouSoft.Model.ComStructure.MCPCC model = (EyouSoft.Model.ComStructure.MCPCC)o;
                return model.ProvinceName + "-" + model.CityName;
            }
            return "";
        }
    }
}
