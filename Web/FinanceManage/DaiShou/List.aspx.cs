using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.FinanceManage.DaiShou
{
    public partial class List : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 代收审批权限
        /// </summary>
        protected bool Privs_ShenPi = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            InitRpt();
        }

        #region private members
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_代收管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_代收管理_栏目, true);
                return;
            }

            Privs_ShenPi = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_代收管理_审批);
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            int pageSize = 20;
            int recordCount = 0;
            int pageIndex = UtilsCommons.GetPadingIndex();

            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.FinStructure.BDaiShou().GetDaiShous(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }

            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            paging.Visible = paging.intRecordCount > paging.intPageSize;
            phEmpty.Visible = paging.intRecordCount == 0;
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.FinStructure.MDaiShouChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.FinStructure.MDaiShouChaXunInfo();

            info.OrderCode = Utils.GetQueryStringValue("txtOrderCode");
            info.GysId = Utils.GetQueryStringValue(txtGys.GysIdClientID);
            info.GysName = Utils.GetQueryStringValue(txtGys.GysNameClientID);
            info.Status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.DaiShouStatus), Utils.GetQueryStringValue("txtStatus"));

            return info;
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取操作列HTML
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        protected string GetCaoZuoHtml(object status)
        {
            if (status == null) return string.Empty;
            var _status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)status;

            string s = string.Empty;
            switch (_status)
            {
                case EyouSoft.Model.EnumType.FinStructure.DaiShouStatus.未审批:
                    s += "<a href=\"javascript:void(0)\" class=\"i_chakan\">查看</a>&nbsp;&nbsp;";
                    if (Privs_ShenPi) s += "<a href=\"javascript:void(0)\" class=\"i_shenpi\">审批</a>&nbsp;&nbsp;";
                    break;
                default:
                    s += "<a href=\"javascript:void(0)\" class=\"i_chakan\">查看</a>&nbsp;&nbsp;";
                    break;
            }

            return s;
        }
        #endregion
    }
}
