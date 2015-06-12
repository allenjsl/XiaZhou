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
    /// YL-礼品卡订单查看
    /// </summary>
    public partial class LiPinKaDingDanEdit : EyouSoft.Common.Page.BackPage
    {
        string DingDanId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            DingDanId = Utils.GetQueryStringValue("editid");
            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "baocun": BaoCun(); break;
                case "quxiao": QuXiao(); break;
            }
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
            if (string.IsNullOrEmpty(DingDanId)) return;

            var info = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKaDingDanInfo(DingDanId);
            if (info == null) return;

            ltrDingDanHao.Text = info.JiaoYiHao;
            ltrLiPinKaMingCheng.Text = info.LiPinKaMingCheng;
            ltrLiPinKaJinE.Text = info.JinE1.ToString("F2");
            ltrLiPinKaLeiXing.Text = info.LiPinKaLeiXing.ToString();
            ltrHuiYuanXingMing.Text = info.HuiYuanXingMing;
            ltrShuLiang.Text = info.ShuLiang.ToString();
            ltrJinE.Text = info.JinE.ToString("F2");            
            ltrDingDanStatus.Text = info.DingDanStatus.ToString();
            ltrFuKuanStatus.Text = info.FuKuanStatus.ToString();
            if (info.IsXuYaoFaPiao)
            {
                ltrFaPiao.Text = "发票抬头：" + info.FaPiaoTaiTou + "<br/>发票明细：" + info.FaPiaoMingXi;

                if (info.FaPiaoPeiSongFangShi == EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi.快递)
                {
                    if (!string.IsNullOrEmpty(info.FaPiaoDiZhiId))
                    {
                        var diZhiInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhiInfo(info.FaPiaoDiZhiId);
                        if (diZhiInfo != null)
                        {
                            var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                            var CPCD = citybll.GetCPCD(CurrentUserCompanyID, diZhiInfo.GuoJiaId, diZhiInfo.ShengFenId, diZhiInfo.ChengShiId, diZhiInfo.XianQuId);
                            string s = string.Empty;
                            if (CPCD != null)
                            {
                                s = CPCD.CountryName + "-" + CPCD.ProvinceName + "-" + CPCD.CityName + "-" + CPCD.CountyName + "&nbsp;";
                            }

                            ltrPeiSongFangShi.Text = "快递<br/>地址：" + s + diZhiInfo.DiZhi + "&nbsp;邮编：" + diZhiInfo.YouBian + "<br/>收件人：" + diZhiInfo.XingMing + "&nbsp;&nbsp;" + diZhiInfo.DianHua;
                        }
                    }
                }
                else
                {
                    ltrPeiSongFangShi.Text = "自取";
                }
            }
            else
            {
                ltrFaPiao.Text = "无需发票";
            }

            ltrXiaDanShiJian.Text = info.IssueTime.ToString();
            ltrZengYu.Text = info.ZengYu;

            if (!string.IsNullOrEmpty(info.SlrDiZhiId))
            {
                var diZhiInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhiInfo(info.SlrDiZhiId);
                if (diZhiInfo != null)
                {
                    ltrSlrXingMing.Text = diZhiInfo.XingMing;
                    var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                    var CPCD = citybll.GetCPCD(CurrentUserCompanyID, diZhiInfo.GuoJiaId, diZhiInfo.ShengFenId, diZhiInfo.ChengShiId, diZhiInfo.XianQuId);
                    string s = string.Empty;
                    if (CPCD != null)
                    {
                        s = CPCD.CountryName + "-" + CPCD.ProvinceName + "-" + CPCD.CityName + "-" + CPCD.CountyName + "&nbsp;";
                    }

                    ltrSlrDiZhi.Text = "地址：" + s + diZhiInfo.DiZhi + "&nbsp;邮编：" + diZhiInfo.YouBian + "<br/>收件人：" + diZhiInfo.XingMing + "&nbsp;&nbsp;" + diZhiInfo.DianHua;
                }
            }

            if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus.已取消
                && info.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款)
            {
                phQuXiao.Visible = true;
            }
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {

        }

        /// <summary>
        /// quxiao
        /// </summary>
        void QuXiao()
        {
            var bllRetCode = new EyouSoft.BLL.YlStructure.BLiPinKa().SheZhiLiPinKaDingDanStatus(DingDanId, SiteUserInfo.UserId, EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus.已取消);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
