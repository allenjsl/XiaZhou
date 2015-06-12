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
    /// YL-礼品卡新增修改
    /// </summary>
    public partial class LiPinKaEdit : EyouSoft.Common.Page.BackPage
    {
        string LiPinKaId = string.Empty;
        protected string LeiXing = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            LiPinKaId = Utils.GetQueryStringValue("editid");
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_礼品卡管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(LiPinKaId)) return;

            var info = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKaInfo(LiPinKaId);
            if (info == null) return;

            txtMingCheng.Value = info.MingCheng;
            txtJinE.Value = info.JinE.ToString("F2");
            txtJinE1.Value = info.JinE1.ToString("F2");
            txtMiaoShu.Value = info.MiaoShu;
            txtFaPiaoKuaiDiJinE.Value = info.FaPiaoKuaiDiJinE.ToString("F2");
            LeiXing = ((int)info.LeiXing).ToString();
            txtXuZhi.Value = info.XuZhi;

            MFileInfo file = new MFileInfo();
            file.FilePath = info.FengMian;
            var items = new List<MFileInfo>();
            items.Add(file);
            upload1.YuanFiles = items;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MLiPinKaInfo();

            info.CompanyId = CurrentUserCompanyID;
            info.FaPiaoKuaiDiJinE = Utils.GetDecimal(Utils.GetFormValue(txtFaPiaoKuaiDiJinE.UniqueID));
            info.FengMian = string.Empty;
            info.IssueTime = DateTime.Now;
            info.JinE = Utils.GetDecimal(Utils.GetFormValue(txtJinE.UniqueID));
            info.JinE1 = Utils.GetDecimal(Utils.GetFormValue(txtJinE1.UniqueID));
            info.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing>(Utils.GetFormValue("txtLeiXing"), EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing.电子卡);
            info.LiPinKaId = LiPinKaId;
            info.LiPinKaKuaiDiJinE = 0;            
            info.MiaoShu = Utils.EditInputText(Request.Form[txtMiaoShu.UniqueID]);
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.XuZhi = Utils.EditInputText(Request.Form[txtXuZhi.UniqueID]);
            var items = upload1.Files;
            var items1 = upload1.YuanFiles;

            if (items != null && items.Count > 0)
            {
                info.FengMian = items[0].FilePath;
            }
            else if (items1 != null && items1.Count > 0)
            {
                info.FengMian = items1[0].FilePath;
            }


            int bllRetCode = 0;

            if (string.IsNullOrEmpty(LiPinKaId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BLiPinKa().InsertLiPinKa(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BLiPinKa().UpdateLiPinKa(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
