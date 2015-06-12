using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    public partial class HuiYuanDianPing : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;

        protected string LeiXing = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
            if (Utils.GetQueryStringValue("sh") == "1") setState();
        }
        void initPage()
        {
            int recordCount = 0;
            pageIndex = UtilsCommons.GetPadingIndex();
            var list = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDianPings(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, new EyouSoft.Model.YlStructure.MWzDianPingChaXunInfo() { });
            if (list != null && list.Count > 0)
            {
                rptlist.DataSource = list;
                rptlist.DataBind();
                phEmpty.Visible = false;
                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
        }
        void setState()
        {
            int result = new EyouSoft.BLL.YlStructure.BHuiYuan().ShenHeDianPing(SiteUserInfo.CompanyId, SiteUserInfo.UserId, Utils.GetQueryStringValue("id"));
            if (result == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }

        /// <summary>
        /// 获取点评人
        /// </summary>
        /// <returns></returns>
        protected string getDPR(string optid)
        {
            var model = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(optid);
            if (model == null) return "";
            return model.XingMing;
        }
        /// <summary>
        /// 获取航线名称
        /// </summary>
        /// <param name="HXID"></param>
        /// <returns></returns>
        protected string getCPMC(string HXID)
        {
            var model = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(HXID);
            if (model == null) return "";
            return model.MingCheng;
        }

        protected string getOpt(string state, string dpID)
        {
            //if (state == "True") return "已审核";
            return string.Format("<a class=\"shenhe\" href=\"javascript:;\" onclick=\"javascript:pageDataJs.chakan('{0}');\">查看</a>", dpID);

        }

    }
}
