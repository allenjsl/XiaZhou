﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace Web.ResourceManage.scenic_spots
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-26
    /// 说明: 资源管理： 车队
    public partial class scenic_spotsList : BackPage
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
        private void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetJingDians(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

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
                this.lbEmptyMsg.Text = "<tr class='old'><td colspan='11' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_新增))
            {
                this.spot_add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_修改))
            {
                this.spot_edit.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_删除))
            {
                this.spot_del.Visible = false;
            }

        }

        /// <summary>
        /// to xls
        /// </summary>
        private void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            
            var chaXun = GetChaXunInfo();
            int _recordCount = 0;
            var items = new EyouSoft.BLL.GysStructure.BGys().GetJingDians(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("所在地\t景点名称\t联系人\t价格\t返单\t交易次数\t交易人数\t结算金额\t未付金额\t质检均分\n");

            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.JiaGeGP1.ToString("F2") + "\t");
                s.Append(item.IsFanDan ? "是" : "否");
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
            info.GysName=Utils.GetQueryStringValue("txtscenicName");
            info.GysId = Utils.GetQueryStringValue("gysid");

            return info;
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取景点信息浮动HTML
        /// </summary>
        /// <param name="jingDians"></param>
        /// <returns></returns>
        protected string GetJingDianFuDongHtml(object jingDians)
        {
            IList<EyouSoft.Model.GysStructure.MJingDianJingDianInfo> items = (IList<EyouSoft.Model.GysStructure.MJingDianJingDianInfo>)jingDians;
            StringBuilder s = new StringBuilder();
            if (items != null && items.Count > 0)
            {
                s.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>景点名称</th><th align='center'>挂牌价格</th><th align='center'>散客价</th><th align='center'>团队价</th><th align='center'>儿童价</th><th align='center'>60-70老人价</th><th align='center'>70以上老人价</th><th align='center'>学生价</th><th align='center'>军人价</th></tr>");
                foreach (var item in items)
                {
                    s.Append("<tr><td align='center'>" + item.Name + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeGP, ProviderToMoney) + "</td><td align='center' >" + UtilsCommons.GetMoneyString(item.JiaGeSK, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeTD, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeET, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeLR67, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeLR7, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeXS, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(item.JiaGeJR, ProviderToMoney) + "</td></tr>");
                }
                s.Append("</table>");
            }
            return s.ToString();
        }
        #endregion
    }
}
