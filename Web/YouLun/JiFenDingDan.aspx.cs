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
    /// YL-积分订单管理
    /// </summary>
    public partial class JiFenDingDan : EyouSoft.Common.Page.BackPage
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
            if (!this.CheckGrant(Privs.游轮管理_积分兑换_栏目))
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
            pageIndex = UtilsCommons.GetPadingIndex();
            var items = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDans(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MWzJiFenDingDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MWzJiFenDingDanChaXunInfo();
            info.ShangPinMingCheng = Utils.GetQueryStringValue("txtShangPinMingCheng");
            info.JiaoYiHao = Utils.GetQueryStringValue("txtJiaoYiHao");
            info.XiaDanShiJian1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtXiaDanShiJian1"));
            info.XiaDanShiJian2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtXiaDanShiJian2"));
            
            return info;
        }
        #endregion

        #region protected members
        /// <summary>
        /// get duihuan fangshi
        /// </summary>
        /// <param name="fangShi"></param>
        /// <returns></returns>
        protected string GetDuiHuanFangShi(object fangShi)
        {
            var _fagnShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)fangShi;
            string s = string.Empty;
            switch (_fagnShi)
            {
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分: s = "积分"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡: s = "积分+礼品卡"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金: s = "积分+现金"; break;
            }

            return s;
        }

        /// <summary>
        /// get jine
        /// </summary>
        /// <param name="fangShi"></param>
        /// <param name="jiFen"></param>
        /// <param name="jinE"></param>
        /// <returns></returns>
        protected string GetJinE(object fangShi, object jiFen, object jinE)
        {
            var _fagnShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)fangShi;
            var _jiFen = (decimal)jiFen;
            var _jinE = (decimal)jinE;

            string s = string.Empty;
            switch (_fagnShi)
            {
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分: s = _jiFen.ToString("F2")+"积分"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡: s = _jiFen.ToString("F2") + "积分+" + _jinE.ToString("F2") + "礼品卡"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金: s = _jiFen.ToString("F2") + "积分+" + _jinE.ToString("F2") + "现金"; break;
            }
            return s;
        }
        #endregion
    }
}
