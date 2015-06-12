using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.YlWeb.Corp
{
    /// <summary>
    /// 员工风采
    /// </summary>
    public partial class FengCai : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "more") More();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            int pageSize = 9;
            int pageIndex = 1;
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzYuanGongFengCaiChaXunInfo();
            var items = new EyouSoft.BLL.YlStructure.BWz().GetYuanGongFengCais(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
        }

        /// <summary>
        /// more
        /// </summary>
        void More()
        {
            int pageSize = 9;
            int pageIndex = UtilsCommons.GetPadingIndex("index");
            int recordCount = 0;

            var chaXun = new EyouSoft.Model.YlStructure.MWzYuanGongFengCaiChaXunInfo();
            var items = new EyouSoft.BLL.YlStructure.BWz().GetYuanGongFengCais(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            int pageCount = (int)Math.Ceiling((double)recordCount / (double)pageSize);

            if (pageIndex > pageCount)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", ""));
            }

            StringBuilder s = new StringBuilder();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<li><a href=\"javascript:void(0)\">");
                    s.AppendFormat("<img src=\"{0}\">", ErpFilepath + item.Filepath);
                    s.AppendFormat("<p>{0}</p>", item.MingCheng);
                    s.AppendFormat("</a></li>");
                }
            }

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", s.ToString()));
        }
        #endregion
    }
}
