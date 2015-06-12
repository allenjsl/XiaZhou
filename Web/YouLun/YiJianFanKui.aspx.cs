using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    public partial class YiJianFanKui : EyouSoft.Common.Page.BackPage
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
        /// <summary>
        /// 问题类型
        /// </summary>
        protected EyouSoft.Model.EnumType.YlStructure.YiJianFanKuiLeiXing? YiJianFanKuiLeiXing = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "shanchu") ShanChu();

            InitRpt();
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="huiyuanid"></param>
        /// <returns></returns>
        protected string GetHuiYuan(object huiyuanid)
        { 
            var s=string.Empty;
            if(string.IsNullOrEmpty(huiyuanid.ToString()))return s;
            var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(huiyuanid.ToString());
            if (m != null) s = m.XingMing;
            return s;
        }
        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            int recordCount = 0;
            var chaXun = GetChaXunInfo();
            pageIndex = UtilsCommons.GetPadingIndex();
            var items = new EyouSoft.BLL.YlStructure.BWz().GetWZYiJianFanKui(pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzYiJianFanKuiChaXun GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MWzYiJianFanKuiChaXun();
            info.CompanyId = SiteUserInfo.CompanyId;
            info.LeiXing = null;
            return info;
        }

        /// <summary>
        /// shan chu
        /// </summary>
        void ShanChu()
        {
            string txtLianJieId = Utils.GetFormValue("txtLianJieId");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BWz().DelWZYiJianFanKui(txtLianJieId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
