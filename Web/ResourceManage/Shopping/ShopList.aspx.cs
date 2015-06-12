using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace Web.ResourceManage.Shopping
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-08
    /// 说明: 资源管理： 购物
    public partial class ShopList : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        protected string NodataMsg = string.Empty;
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UtilsCommons.IsToXls()) ToXls();

            PowerControl();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.GysStructure.BGys().GetGouWus(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();

                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
            else
            {
                NodataMsg = "<tr class='old'><td colspan='8' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_新增))
            {
                this.shop_add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_修改))
            {
                this.shop_edit.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_删除))
            {
                this.shop_del.Visible = false;
            }
        }

        /// <summary>
        ///to xls
        /// </summary>
        private void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetGouWus(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("所在地\t商店名称\t商品类别\t联系人\t交易次数\t进店人数\n");

            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.ShangPinLeiBie.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.JiaoYiXX.JiaoYiCiShu + "\t");
                s.Append(item.JiaoYiXX.JiaoYiShuLiang + "\n");
            }           

            ResponseToXls(s.ToString());
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MLBChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.GysStructure.MLBChaXunInfo();

            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            info.GysName = Utils.GetQueryStringValue("txtshopname");
            info.GysId = Utils.GetQueryStringValue("gysid");

            return info;
        }
        #endregion
    }
}
