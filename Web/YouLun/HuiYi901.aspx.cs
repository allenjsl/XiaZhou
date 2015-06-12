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
    /// YL-会议案例新增修改
    /// </summary>
    public partial class HuiYi901 : EyouSoft.Common.Page.BackPage
    {
        string AnLiId = string.Empty;
        protected string LeiXing = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            AnLiId = Utils.GetQueryStringValue("editid");
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_会议管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(AnLiId)) return;

            var info = new EyouSoft.BLL.YlStructure.BWz().GetHuiYiAnLiInfo(AnLiId);
            if (info == null) return;

            txtMingCheng.Value = info.MingCheng;
            txtShiJian1.Value = info.ShiJian1.ToString("yyyy-MM-dd");
            txtShiJian2.Value = info.ShiJian2.ToString("yyyy-MM-dd");
            LeiXing = ((int)info.LeiXing).ToString();

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
            var info = new EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo();

            info.AnLiId = AnLiId;
            info.ChuanZhiId = string.Empty;
            info.CompanyId = CurrentUserCompanyID;
            info.DanWei = string.Empty;
            info.Filepath = null;
            info.GongSiId = string.Empty;
            info.IssueTime = DateTime.Now;
            info.JiaGe = string.Empty;
            info.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing>(Utils.GetFormValue("txtLeiXing"), EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮);
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.NeiRong = string.Empty;
            info.OperatorId = SiteUserInfo.UserId;
            info.RenShu = string.Empty;
            info.ShiJian = string.Empty;
            info.ShiJian1 = Utils.GetDateTime(Utils.GetFormValue(txtShiJian1.UniqueID), DateTime.Now);
            info.ShiJian2 = Utils.GetDateTime(Utils.GetFormValue(txtShiJian2.UniqueID), DateTime.Now);
            info.XiLieId = string.Empty;

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

            if (string.IsNullOrEmpty(AnLiId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().InsertHuiYiAnLi(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BWz().UpdateHuiYiAnLi(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
