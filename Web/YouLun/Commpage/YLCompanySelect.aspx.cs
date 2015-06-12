using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.CommonPage
{
    public partial class YLCompanySelect : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;

        protected int listCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
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
            pageIndex = UtilsCommons.GetPadingIndex();

            EyouSoft.Model.YlStructure.MGongSiChaXunInfo searchModel = new EyouSoft.Model.YlStructure.MGongSiChaXunInfo();
            searchModel.GongSiLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("t"));
            if (Utils.GetInt(Utils.GetQueryStringValue("t")) == 255) searchModel.GongSiLeiXing = null;
            searchModel.GongSiMingCheng = Utils.GetQueryStringValue("comName");

            IList<EyouSoft.Model.YlStructure.MGongSiInfo> comps = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSis(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchModel);

            if (comps != null && comps.Count > 0)
            {
                listCount = comps.Count;
                this.rptList.DataSource = comps;
                this.rptList.DataBind();
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
            }

            //绑定分页
            BindPage();
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
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }




        #endregion

    }
}
