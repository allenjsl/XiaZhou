using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.Web.CommonPage
{
    public partial class LineAreaSelect : EyouSoft.Common.Page.BackPage
    {
        #region 页面参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
        }

        private void PageInit()
        {
            EyouSoft.BLL.ComStructure.BComArea BLL = new EyouSoft.BLL.ComStructure.BComArea();
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            MComAreaSearch model = new MComAreaSearch();
            model.AreaName = Utils.GetQueryStringValue("txtlineName");
            model.Type = Utils.GetQueryStringValue("areaType") == "" ? (AreaType?)null : (AreaType)Utils.GetInt(Utils.GetQueryStringValue("areaType"));
            IList<EyouSoft.Model.ComStructure.MComArea> lst = BLL.GetList(pageIndex, pageSize, ref recordCount, this.SiteUserInfo.CompanyId, model);
            if (lst != null && lst.Count > 0)
            {
                this.rpt_list.DataSource = lst;
                this.rpt_list.DataBind();
                if (pageSize >= recordCount)
                {
                    this.ExporPageInfoSelect1.Visible = false;
                }
                else
                {
                    BindPage();
                }
            }
            else
            {
                this.rpt_list.Controls.Add(new Label() { Text = "<tr><td colspan='3' height='23' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
    }
}
