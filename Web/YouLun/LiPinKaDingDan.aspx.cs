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
    /// YL-礼品卡订单管理
    /// </summary>
    public partial class LiPinKaDingDan : EyouSoft.Common.Page.BackPage
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
            var items = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKaDingDans(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MLiPinKaDingDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MLiPinKaDingDanChaXunInfo();
            info.LiPinKaMingCheng = Utils.GetQueryStringValue("txtLiPinKaMingCheng");
            info.JiaoYiHao = Utils.GetQueryStringValue("txtJiaoYiHao");
            info.XiaDanShiJian1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtXiaDanShiJian1"));
            info.XiaDanShiJian2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtXiaDanShiJian2"));

            return info;
        }
        #endregion

        #region protected members
        #endregion
    }
}
