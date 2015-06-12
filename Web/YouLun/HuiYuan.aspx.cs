using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-会员管理
    /// </summary>
    public partial class HuiYuan : EyouSoft.Common.Page.BackPage
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

            LeiXing = ((int)Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing>(Utils.GetQueryStringValue("t"), EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing.注册会员)).ToString();

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "tingyong": SheZhiHuiYuanStatus(EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus.停用); break;
                case "qiyong": SheZhiHuiYuanStatus(EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus.可用); break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(Privs.游轮管理_会员管理_栏目))
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

            var items = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuans(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MHuiYuanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MHuiYuanChaXunInfo();

            info.LianXiFangShi = Utils.GetQueryStringValue("txtLianXiFangShi");
            info.Username = Utils.GetQueryStringValue("txtUsername");
            info.XingMing = Utils.GetQueryStringValue("txtXingMing");
            info.ZhuCeShiJian1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtZhuCeShiJian1"));
            info.ZhuCeShiJian2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtZhuCeShiJian2"));
            info.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing>(Utils.GetQueryStringValue("t"), EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing.注册会员);


            return info;
        }

        /// <summary>
        /// 设置会员状态
        /// </summary>
        /// <param name="status"></param>
        void SheZhiHuiYuanStatus(EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus status)
        {
            string txtHuiYuanId = Utils.GetFormValue("txtHuiYuanId");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BHuiYuan().SheZhiHuiYuanStatus(txtHuiYuanId, status);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// get status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetStatus(object status)
        {
            var _status = (EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus)status;

            if (_status == EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus.可用)
            {
                return "<a href=\"javascript:void(0)\" class=\"i_tingyong\">停用</a>";
            }
            else
            {
                return "<a href=\"javascript:void(0)\" class=\"i_qiyong\">启用</a>";
            }
        }
        #endregion
    }
}
