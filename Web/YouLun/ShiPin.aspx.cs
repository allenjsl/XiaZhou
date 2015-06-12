﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.YouLun
{
    public partial class ShiPin : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
            if (Utils.GetQueryStringValue("del") == "1") Del();


        }
        /// <summary>
        /// 删除基础信息
        /// </summary>
        private void Del()
        {
            string id = Utils.GetQueryStringValue("id");
            Response.Clear();
            int result = new EyouSoft.BLL.YlStructure.BJiChuXinXi().DeleteShiPin(SiteUserInfo.CompanyId, id);
            if (result == 1)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "删除失败"));
            }
            Response.End();
        }
        /// <summary>
        /// 初始化列表
        /// </summary>
        protected void PageInit()
        {

            EyouSoft.Model.YlStructure.MShiPinChaXunInfo searCh = new EyouSoft.Model.YlStructure.MShiPinChaXunInfo();
            searCh.GongSiMingCheng = Utils.GetQueryStringValue("gongsiname");
            searCh.XiLieMingCheng = Utils.GetQueryStringValue("xiliename");
            searCh.ChuanZhiMingCheng = Utils.GetQueryStringValue("chuanzhiname");
            pageIndex = UtilsCommons.GetPadingIndex();

            IList<EyouSoft.Model.YlStructure.MShiPinInfo> list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetShiPins(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searCh); ;
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                this.repList.EmptyText = "<tr><td  align=\"center\" colspan=\"6\">暂无数据!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

        }
    }
}
