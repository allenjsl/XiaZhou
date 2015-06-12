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
    /// YL-会议申请查看
    /// </summary>
    public partial class HuiYi801 : EyouSoft.Common.Page.BackPage
    {
        string ShenQingId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            ShenQingId = Utils.GetQueryStringValue("editid");
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
            if (string.IsNullOrEmpty(ShenQingId)) return;

            var info = new EyouSoft.BLL.YlStructure.BWz().GetHuiYiShenQingInfo(ShenQingId);
            if (info == null) return;

            ltrGuiMo.Text = info.GuiMo;
            ltrYuJiShiJian.Text = info.YuJiShiJian;
            ltrLeiXing.Text = info.LeiXing.ToString(); ;
            ltrLxrXingMing.Text = info.LxrXingMing;
            ltrLxrShouJi.Text = info.LxrShouJi;
            ltrLxrYouXiang.Text = info.LxrYouXiang;
            ltrLxrDiZhi.Text = info.LxrDiZhi;

            ltrHangYeMingCheng.Text = info.HangYeMingCheng;
            ltrHangYeLianXiFangShi.Text = info.HangYeLxShouJi;

            ltrShenQingShiJian.Text = info.IssueTime.ToString();

            if (info.ChuLiShiJian.HasValue) ltrChuLiShiJian.Text = info.ChuLiShiJian.Value.ToString();
            txtChuLiBeiZhu.Value = info.ChuLiBeiZhu;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            string chuLiBeiZhu = Utils.GetFormValue(txtChuLiBeiZhu.UniqueID);

            int bllRetCode = new EyouSoft.BLL.YlStructure.BWz().HuiYiShenQingChuLi(ShenQingId,SiteUserInfo.UserId,chuLiBeiZhu);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
