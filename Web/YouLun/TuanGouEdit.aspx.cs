using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Web.YouLun.WUC;
using System.Text;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-团购新增修改
    /// </summary>
    public partial class TuanGouEdit : EyouSoft.Common.Page.BackPage
    {
        string TuanGouId = string.Empty;
        protected string HangQiId = string.Empty;
        protected string HangQiMingCheng = string.Empty;
        protected string RiQiId = string.Empty;
        protected string FangXingId = string.Empty;
        protected string GuoJiId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            TuanGouId = Utils.GetQueryStringValue("editid");

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "baocun": BaoCun(); break;
                case "getriqi": GetRiQi(); break;
                case "getfangxing": GetFangXing(); break;
                case "getbinkeleixing": GetBinKeLeiXing(); break;
                case "getyoulunleixing": GetYouLunLeiXing(); break;
            }

            InitGuoJi();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_团购管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(TuanGouId)) return;

            var info = new EyouSoft.BLL.YlStructure.BHangQi().GetTuanGouInfo(TuanGouId);
            if (info == null) return;

            HangQiId = info.HangQiId;
            HangQiMingCheng = info.HangQiMingCheng;
            RiQiId = info.RiQiId;
            FangXingId = info.FangXingId.ToString();
            GuoJiId = info.GuoJiId.ToString();
            txtMingCheng.Value = info.MingCheng;
            txtYuanJia.Value = info.YuanJia.ToString("F2");
            txtXianJia.Value = info.XianJia.ToString("F2");
            txtJieZhiShiJian.Value = info.JieZhiShiJian.ToString("yyyy-MM-dd");
            txtBKLX.Value = info.BinKeLeiXing;
            txtTuanGouShu.Value = info.TuanGouShu.ToString();

            MFileInfo file = new MFileInfo();
            file.FilePath = info.FengMian;
            var items = new List<MFileInfo>();
            items.Add(file);
            upload1.YuanFiles = items;

            if (info.JiaGes != null && info.JiaGes.Count > 0)
            {
                rpt.DataSource = info.JiaGes;
                rpt.DataBind();

                phJiaGe.Visible = false;
            }
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MTuanGouInfo();
            info.BianHao = string.Empty;
            info.ChuanZhiName = string.Empty;
            info.FangXingId = Utils.GetInt(Utils.GetFormValue("txtFangXing"));
            info.FengMian = string.Empty;
            info.GongSiName = string.Empty;
            info.GuoJiId = Utils.GetInt(Utils.GetFormValue("txtGuoJi"));
            info.GysName = string.Empty;
            info.HangQiId = Utils.GetFormValue(txtHangQiId.UniqueID);
            info.IssueTime = DateTime.Now;
            info.JiaGes = null;
            info.JieZhiShiJian = Utils.GetDateTime(Utils.GetFormValue(txtJieZhiShiJian.UniqueID));
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.RiQi = DateTime.Now;
            info.RiQiId = Utils.GetFormValue("txtRiQi");
            info.TuanGouId = TuanGouId;
            info.XianJia = Utils.GetDecimal(Utils.GetFormValue(txtXianJia.UniqueID));
            info.XiLieName = string.Empty;
            info.YiYuDingRenShu = 0;
            info.YuanJia = Utils.GetDecimal(Utils.GetFormValue(txtYuanJia.UniqueID));
            info.BinKeLeiXing = Utils.GetFormValue(txtBKLX.UniqueID);
            info.TuanGouShu = Utils.GetInt(Utils.GetFormValue(txtTuanGouShu.UniqueID));

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

            info.JiaGes = new List<EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo>();
            string[] txtBinKeLeiXing = Utils.GetFormValues("txtBinKeLeiXing");
            string[] txtJiaGe = Utils.GetFormValues("txtJiaGe");

            if (txtBinKeLeiXing.Length > 0 && txtBinKeLeiXing.Length == txtJiaGe.Length)
            {
                var item1 = new List<int>();

                for (int i = 0; i < txtBinKeLeiXing.Length; i++)
                {
                    var item = new EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo();

                    item.BinKeLeiXingId = Utils.GetInt(txtBinKeLeiXing[i]);
                    item.JiaGe = Utils.GetDecimal(txtJiaGe[i]);

                    if (item1.Contains(item.BinKeLeiXingId)) continue;
                    if (item.BinKeLeiXingId == 0 || item.JiaGe == 0) continue;

                    item1.Add(item.BinKeLeiXingId);

                    info.JiaGes.Add(item);
                }
            }

            if (info.JiaGes.Count == 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "至少要填写一个价格信息"));
            }

            var bllRetCode = 0;

            if (string.IsNullOrEmpty(TuanGouId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BHangQi().InsertTuanGou(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BHangQi().UpdateTuanGou(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// init guoji
        /// </summary>
        void InitGuoJi()
        {
            var chaXun=new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.国籍;
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, chaXun);
            StringBuilder s = new StringBuilder();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat(" <option value=\"{0}\">{1}</option> ", item.XinXiId, item.MingCheng);
                }
            }

            ltrGuoJi.Text = s.ToString();
        }

        /// <summary>
        /// get riqi
        /// </summary>
        void GetRiQi()
        {
            var hangQiId = Utils.GetQueryStringValue("hangqiid");
            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiRiQis(hangQiId, null, null, null);
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<option value=\"\">请选择日期</option>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat(" <option value=\"{0}\">{1}</option> ", item.RiQiId, item.RiQi.ToString("yyyy-MM-dd"));
                }
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "", s.ToString()));
        }

        /// <summary>
        /// get fangxing
        /// </summary>
        void GetFangXing()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<option value=\"\">请选择房型</option>");

            var hangQiId = Utils.GetQueryStringValue("hangqiid");
            var info = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(hangQiId);

            var chaXun = new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.房型;
            chaXun.YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

            if (info != null)
            {
                chaXun.YouLunLeiXing = info.LeiXing;
            }
            
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, chaXun);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat(" <option value=\"{0}\">{1}</option> ", item.XinXiId, item.MingCheng);
                }
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "", s.ToString()));
        }

        /// <summary>
        /// get binkeleixing
        /// </summary>
        void GetBinKeLeiXing()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<option value=\"\">请选择人员类型</option>");

            var hangQiId = Utils.GetQueryStringValue("hangqiid");
            var info = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(hangQiId);

            var chaXun = new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.宾客类型;
            chaXun.YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

            if (info != null)
            {
                chaXun.YouLunLeiXing = info.LeiXing;
            }

            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, chaXun);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat(" <option value=\"{0}\">{1}</option> ", item.XinXiId, item.MingCheng);
                }
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "", s.ToString()));
        }

        void GetYouLunLeiXing()
        {
            var hangQiId = Utils.GetQueryStringValue("hangqiid");
            var info = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(hangQiId);

            if (info != null)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "", ((int)info.LeiXing)).ToString());
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "", "0"));
        }
        #endregion
    }
}
