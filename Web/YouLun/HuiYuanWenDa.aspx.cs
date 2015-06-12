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
    public partial class HuiYuanWenDa : BackPage
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
        }
        void initPage()
        {
            int recordCount = 0;
            pageIndex = UtilsCommons.GetPadingIndex();
            var list = new EyouSoft.BLL.YlStructure.BHuiYuan().GetWenDas(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, new EyouSoft.Model.YlStructure.MWzWenDaChaXunInfo() { });
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
        /// <summary>
        /// 回复问答
        /// </summary>
        /// <param name="wdid"></param>
        /// <returns></returns>
        protected string getOpt(object obj, string wdid)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return string.Format(" <a class=\"huifu\" href=\"javascript:;\" onclick=\"javascript:pageDataJs.HuiFu('{0}');\">回复</a>", wdid);
            return string.Format(" <a class=\"huifu\" href=\"javascript:;\" onclick=\"javascript:pageDataJs.HuiFu('{0}');\">查看</a>", wdid);

        }

    }
}
/*
 BHuiYuan.InsertWenDa()  咨询问答写入
BHuiYuan.GetWenDas()  获取问答集合
 */