using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-礼品卡管理
    /// </summary>
    public partial class LiPinKa : EyouSoft.Common.Page.BackPage
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "shanchu") ShanChu();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(Privs.游轮管理_礼品卡管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            int recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKas(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo();
            info.MingCheng = Utils.GetQueryStringValue("txtMingCheng");
            info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing), Utils.GetQueryStringValue("txtLeiXing"));

            return info;
        }

        /// <summary>
        /// shan chu
        /// </summary>
        void ShanChu()
        {
            string txtLiPinKaId = Utils.GetFormValue("txtLiPinKaId");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BLiPinKa().DeleteLiPinKa(SiteUserInfo.CompanyId, txtLiPinKaId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode < 0) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:已经存在订单的商品不可删除。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members

        #endregion
    }
}
