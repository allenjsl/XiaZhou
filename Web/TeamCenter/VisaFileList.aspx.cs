using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.TeamCenter
{
    public partial class VisaFileList : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
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
            if (!IsPostBack)
            {
                //初始化
                DataInit();
            }
        }

        #region private members
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = UtilsCommons.GetPadingIndex();

            string quoteId = Utils.GetQueryStringValue("quoteId");
            string tourId = Utils.GetQueryStringValue("tourId");

            if (quoteId != "")
            {
                EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
                EyouSoft.Model.TourStructure.MTourQuoteInfo model = bll.GetQuoteInfo(quoteId);
                if (model != null) {
                    if (model.VisaFileList != null && model.VisaFileList.Count > 0) {
                        this.rptList.DataSource = model.VisaFileList;
                        this.rptList.DataBind();
                    }
                }

                this.ExporPageInfoSelect1.Visible = false;

            }

            if (tourId != "")
            {
                IList<EyouSoft.Model.ComStructure.MComAttach> visaFileList = new EyouSoft.BLL.TourStructure.BTour().GetVisaFileList(tourId, pageSize, pageIndex, ref recordCount);
                if (visaFileList != null && visaFileList.Count > 0)
                {
                    this.rptList.DataSource = visaFileList;
                    this.rptList.DataBind();
                    BindPage();
                    if (visaFileList.Count <= 10)
                    {
                        //绑定分页
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                }
                else
                {
                    this.ExporPageInfoSelect1.Visible = false;
                }

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
        #endregion
    }
}
