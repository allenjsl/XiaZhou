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
    /// YL-员工风采新增修改
    /// </summary>
    public partial class FengCaiEdit : EyouSoft.Common.Page.BackPage
    {
        string FengCaiId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            FengCaiId = Utils.GetQueryStringValue("editid");
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
            if (string.IsNullOrEmpty(FengCaiId)) return;

            var info = new EyouSoft.BLL.YlStructure.BWz().GetYuanGongFengCaiInfo(FengCaiId);
            if (info == null) return;

            txtMingCheng.Value = info.MingCheng;

            MFileInfo file = new MFileInfo();
            file.FilePath = info.Filepath;
            var items = new List<MFileInfo>();
            items.Add(file);
            upload1.YuanFiles = items;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo();
            info.CompanyId = CurrentUserCompanyID;
            info.Filepath = null;
            info.IssueTime = DateTime.Now;
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.FengCaiId = FengCaiId;
            info.XiangXiJieShao = string.Empty;

            var items = upload1.Files;
            var items1 = upload1.YuanFiles;

            if (items != null && items.Count > 0)
            {
                info.Filepath = items[0].FilePath;
            }
            else if (items1 != null && items1.Count > 0)
            {
                info.Filepath = items1[0].FilePath;
            }

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(FengCaiId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().InsertYuanGongFengCai(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().UpdateYuanGongFengCai(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
