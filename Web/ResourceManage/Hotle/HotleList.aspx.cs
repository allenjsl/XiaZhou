using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace Web.ResourceManage.Hotle
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-22
    /// 说明: 资源管理： 餐馆
    public partial class HotleList : BackPage
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
        /// <summary>
        /// 餐馆列表为空的提示信息
        /// </summary>
        protected string NodataMsg = string.Empty;
        /// <summary>
        /// 菜系的列表
        /// </summary>
        protected string Cuisinelist = string.Empty;
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
        private void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetCanGuans(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

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
                NodataMsg = "<tr class='old'><td colspan='7' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_新增))
            {
                this.phForAdd.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_修改))
            {
                this.phForUpdate.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_删除))
            {
                this.phForDel.Visible = false;
            }
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MLBChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.GysStructure.MLBChaXunInfo();
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            info.GysName = Utils.GetQueryStringValue("HotleName");
            info.CanGuanCaiXi = (EyouSoft.Model.EnumType.SourceStructure.SourceCuisine?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SourceStructure.SourceCuisine), Utils.GetQueryStringValue("ddlCuisine"));
            info.GysId = Utils.GetQueryStringValue("gysid");

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var chaXun = GetChaXunInfo();
            int _recordCount = 0;
            var items = new EyouSoft.BLL.GysStructure.BGys().GetCanGuans(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("所在地\t餐厅名称\t菜系\t联系人\t餐标区间\t交易次数\t交易人数\t结算金额\t未付金额\t质检均分\n");
            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.CaiXiNames + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.CanBiao+ "\t");
                s.Append(item.JiaoYiXX.JiaoYiCiShu + "\t");
                s.Append(item.JiaoYiXX.JiaoYiShuLiang + "\t");
                s.Append(item.JiaoYiXX.JieSuanJinE + "\t");
                s.Append(item.JiaoYiXX.WeiZhiFuJinE + "\t");
                s.Append(item.JiaoYiXX.ZhiJianJunFen + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
