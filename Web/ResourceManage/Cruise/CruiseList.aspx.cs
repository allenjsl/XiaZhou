using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using System.Text;

namespace Web.ResourceManage.Cruise
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-28
    /// 说明: 资源管理： 游轮
    public partial class CruiseList : BackPage
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
            var items = new EyouSoft.BLL.GysStructure.BGys().GetYouLuns(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

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
                this.litMsgEmpty.Text = "<tr class='old'><td colspan='9' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_新增))
            {
                this.phForAdd.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_修改))
            {
                this.phForUpdate.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_删除))
            {
                this.phForDel.Visible = false;
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
            var items = new EyouSoft.BLL.GysStructure.BGys().GetYouLuns(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("所在地\t邮轮公司名称\t联系人\t游船名\t交易次数\t交易人数\t结算金额\t未付金额\t质检均分\n");

            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.YouChuanName1 + "\t");
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

            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            info.GysName = Utils.GetQueryStringValue("txtCruiseCompany");
            info.YouLunYouChuanName = Utils.GetQueryStringValue("txtCruiseName");
            info.GysId = Utils.GetQueryStringValue("gysid");

            return info;
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取游船浮动信息HTML
        /// </summary>
        /// <param name="youChuans"></param>
        /// <returns></returns>
        protected string GetYouChuanFuDongHTML(object youChuans)
        {
            var items = (IList<EyouSoft.Model.GysStructure.MYouLunYouChuanInfo>)youChuans;
            StringBuilder s = new StringBuilder();

            s.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>船名</th><th align='center'>星级</th><th align='center'>仓位数</th><th align='center'>船载电话</th><th align='center'>船载联系人</th><th align='center'>航线</th></tr>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.Append("<tr><td align='center'>" + item.Name + "</td><td align='center'>" + item.XingJi + "</td><td align='center' >" + item.CangWeiShu + "</td><td align='center'>" + item.Telephone + "</td><td align='center'>" + item.LxrName + "</td><td align='center'>" + item.HangXian + "</td></tr>");
                }
            }
            s.Append("</table>");

            return s.ToString();
        }
        #endregion
    }
}
