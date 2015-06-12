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
    /// YL-广告管理
    /// </summary>
    public partial class GuangGao : EyouSoft.Common.Page.BackPage
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
            if (!this.CheckGrant(Privs.游轮管理_广告管理_栏目))
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
            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();

            info.WeiZhi = (EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi), Utils.GetQueryStringValue("txtWeiZhi"));

            return info;
        }

        /// <summary>
        /// shan chu
        /// </summary>
        void ShanChu()
        {
            string txtGuangGaoId = Utils.GetFormValue("txtGuangGaoId");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BWz().DeleteGuangGao(SiteUserInfo.CompanyId, txtGuangGaoId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// get lianjie
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="url"></param>
        /// <param name="mingCheng"></param>
        /// <returns></returns>
        protected string GetLianJie(object filepath, object url, object mingCheng)
        {
            string _filepath = string.Empty;
            string _url = string.Empty;
            string target = " target=\"_blank\" ";
            string _mingCheng = "查看";
            if (filepath != null) _filepath = filepath.ToString();
            if (url != null) _url = url.ToString();
            //if (mingCheng != null) _mingCheng = mingCheng.ToString();

            if (string.IsNullOrEmpty(_url))
            {
                _url = "javascript:void(0)";
                target = string.Empty;
            }

            if (!string.IsNullOrEmpty(_filepath))
            {
                return string.Format("<a {2} href=\"{0}\"><img src=\"{1}\" style=\"width:106px;height:47px;margin:4px\" /></a>", _url, _filepath, target);
            }
            else
            {
                return string.Format("<a {2} href=\"{0}\">{1}</a>", _url, _mingCheng, target);
            }
        }
        #endregion
    }
}
