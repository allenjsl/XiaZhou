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
    /// YL-招聘岗位新增修改
    /// </summary>
    public partial class ZhaoPinEdit : EyouSoft.Common.Page.BackPage
    {
        string GangWeiId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            GangWeiId = Utils.GetQueryStringValue("editid");
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_网站介绍_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(GangWeiId)) return;

            var info = new EyouSoft.BLL.YlStructure.BWz().GetZhaoPinGangWeiInfo(GangWeiId);
            if (info == null) return;

            txtMingCheng.Value = info.MingCheng;
            txtJieShao.Value = info.XiangXiJieShao;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo();
            info.CompanyId = CurrentUserCompanyID;
            info.IssueTime = DateTime.Now;
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.GangWeiId = GangWeiId;
            info.XiangXiJieShao = Utils.EditInputText(Request.Form[txtJieShao.UniqueID]);

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(GangWeiId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().InsertZhaoPinGangWei(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().UpdateZhaoPinGangWei(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
