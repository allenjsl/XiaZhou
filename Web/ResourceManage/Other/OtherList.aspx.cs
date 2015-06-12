using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace Web.ResourceManage.Other
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-09
    /// 说明: 资源管理： 其它
    public partial class OtherList : BackPage
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
            var items = new EyouSoft.BLL.GysStructure.BGys().GetQiTas(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();

                this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect2.intPageSize = pageSize;
                this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect2.intRecordCount = recordCount;
            }
            else
            {
                NodataMsg = "<tr class='old'><td colspan='10' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_栏目, false);
                return;
            }

            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_新增))
            {
                this.other_add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_修改))
            {
                this.other_edit.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_删除))
            {
                this.other_del.Visible = false;
            }
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetQiTas(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("所在地\t单位名称\t联系人\t交易次数\t交易人数\t结算金额\t未付金额\t质检均分\n");

            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.JiaoYiXX.JiaoYiCiShu + "\t");
                s.Append(item.JiaoYiXX.JiaoYiShuLiang + "\t");
                s.Append(item.JiaoYiXX.JieSuanJinE + "\t");
                s.Append(item.JiaoYiXX.WeiZhiFuJinE + "\t");
                s.Append(item.JiaoYiXX.ZhiJianJunFen + "\n");
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
             info.GysName = Utils.GetQueryStringValue("txtUnitName");
             info.GysId = Utils.GetQueryStringValue("gysid");
             return info;
         }
        #endregion
    }
}
