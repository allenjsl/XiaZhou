using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Web.YouLun.WUC;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-资讯新增修改
    /// </summary>
    public partial class ZiXunEdit : EyouSoft.Common.Page.BackPage
    {
        string ZiXunId = string.Empty;

        protected string LeiXing = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            ZiXunId = Utils.GetQueryStringValue("editid");
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_资讯管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(ZiXunId)) return;

            var info = new EyouSoft.BLL.YlStructure.BWz().GetZiXunInfo(ZiXunId);
            if (info == null) return;

            txtBiaoTi.Value = info.BiaoTi;
            txtNeiRong.Value = info.NeiRong;
            LeiXing = ((int)info.LeiXing).ToString();
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MWzZiXunInfo();
            info.CompanyId = CurrentUserCompanyID;
            info.IssueTime = DateTime.Now;
            info.BiaoTi = Utils.GetFormValue(txtBiaoTi.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.ZiXunId = ZiXunId;
            info.NeiRong = Utils.GetYlEditorText(Request.Form[txtNeiRong.UniqueID]);
            info.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing>(Utils.GetFormValue("txtLeiXing"), EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.None);

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(ZiXunId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().InsertZiXun(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().UpdateZiXun(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
