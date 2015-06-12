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
    /// YL-积分兑换商品管理
    /// </summary>
    public partial class JiFenDuiHuan : EyouSoft.Common.Page.BackPage
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
            var items = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenShangPins(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MWzJiFenShangPinChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MWzJiFenShangPinChaXunInfo();
            info.MingCheng = Utils.GetQueryStringValue("txtMingCheng");
            info.PaiXu = 6;
            info.Status = (EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus), Utils.GetQueryStringValue("txtStatus"));

            return info;
        }

        /// <summary>
        /// shan chu
        /// </summary>
        void ShanChu()
        {
            string txtShangPinId = Utils.GetFormValue("txtShangPinId");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BDuiHuan().DeleteJiFenShangPin(SiteUserInfo.CompanyId, txtShangPinId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode < 0) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:已经存在兑换订单的商品不可删除。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// get shangpin fujian
        /// </summary>
        /// <param name="fujians"></param>
        /// <returns></returns>
        protected string GetShangPinFuJian(object fujians)
        {
            if (fujians == null) return string.Empty;
            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)fujians;
            if (items == null || items.Count == 0) return string.Empty;

            return string.Format("<img src=\"{0}\" style=\"width:124px; height:80px; margin:4px;\" />", items[0].Filepath);
        }

        /// <summary>
        /// get duihuan fangshi
        /// </summary>
        /// <param name="fangshis"></param>
        /// <returns></returns>
        protected string GetDuiHuanFangShi(object fangshis)
        {
            if (fangshis == null) return string.Empty;
            var items = (IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo>)fangshis;

            if (items == null || items.Count == 0) return string.Empty;

            string s = string.Empty;

            foreach (var item in items)
            {
                switch (item.FangShi)
                {
                    case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分: s += "积分：" + item.JiFen.ToString("F2")+"积分<br/>"; break;
                    case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡: s += "积分+礼品卡：" + item.JiFen.ToString("F2") + "积分+" + item.JinE.ToString("F2") + "元<br/>"; break;
                    case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金: s += "积分+现金：" + item.JiFen.ToString("F2") + "积分+" + item.JinE.ToString("F2") + "元<br/>"; break;
                }
            }

            return s;            
        }
        #endregion
    }
}
