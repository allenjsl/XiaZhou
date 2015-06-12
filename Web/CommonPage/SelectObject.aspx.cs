using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.CommonPage
{
    /// <summary>
    ///选择物品
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-10-11
    public partial class SelectObject : BackPage
    {
        private int pageSize = 24;
        private int pageIndex = 0;
        private int recordCount = 0;
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {

            //string txtName = Utils.GetQueryStringValue("txtName");//物品名称;
            //string txtInSTime = Utils.GetQueryStringValue("txtInSTime");//入库开始时间
            //string txtInETime = Utils.GetQueryStringValue("txtInETime");//入库结束时间
            //获取分页参数并强转'
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);

            EyouSoft.BLL.GovStructure.BGood BLL = new EyouSoft.BLL.GovStructure.BGood();
            IList<EyouSoft.Model.GovStructure.MGovGoodList> lst = BLL.GetGovGoodList(this.SiteUserInfo.CompanyId, "", null, null, pageSize, pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            { 
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                listCount = lst.Count;
                BindPage();
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
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
        }
    }
}
