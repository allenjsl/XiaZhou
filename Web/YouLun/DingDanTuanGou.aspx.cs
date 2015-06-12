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
    public partial class HangQiTuanGou : EyouSoft.Common.Page.BackPage
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

        protected string LeiXing = string.Empty;
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
            if (!this.CheckGrant(Privs.游轮管理_订单管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int recordCount = 0;
            pageIndex = UtilsCommons.GetPadingIndex();
            var items = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDans(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MHangQiDingDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MHangQiDingDanChaXunInfo();

            info.DingDanHao = Utils.GetQueryStringValue("dingdanhao");
            info.XiaDanShiJian1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sxiadan"));
            info.XiaDanShiJian2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("exiadan"));
            info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus), Utils.GetQueryStringValue("dingdanzhuangtai"));
            info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.FuKuanStatus), Utils.GetQueryStringValue("fukuanzhuangtai"));
            info.DingDanLeiXing = 2;

            return info;
        }


        #endregion


    }
}
