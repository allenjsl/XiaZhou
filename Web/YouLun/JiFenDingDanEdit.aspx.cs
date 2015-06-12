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
    /// YL-积分兑换订单查看
    /// </summary>
    public partial class JiFenDingDanEdit : EyouSoft.Common.Page.BackPage
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
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_积分兑换_栏目))
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

            var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(DingDanId);
            if (info == null) return;

            ltrDingDanHao.Text = info.JiaoYiHao;
            ltrShangPinMingCheng.Text = info.ShangPinMingCheng;
            ltrHuiYuanXingMing.Text = info.HuiYuanZhangHao;
            ltrShuLiang.Text = info.ShuLiang.ToString();
            ltrFangShi.Text = GetDuiHuanFangShi(info.FangShi);
            ltrJinE.Text = GetJinE(info.FangShi, info.JiFen, info.JinE);
            ltrDingDanStatus.Text = info.DingDanStatus.ToString();
            ltrFuKuanStatus.Text = info.FuKuanStatus.ToString();
            if (info.IsXuYaoFaPiao)
            {
                ltrFaPiao.Text = "发票抬头：" + info.FaPiaoTaiTou + "<br/>发票明细：" + info.FaPiaoMingXi;
            }
            else
            {
                ltrFaPiao.Text = "无需发票";
            }

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

            ltrXiaDanShiJian.Text = info.IssueTime.ToString();

            if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已取消 
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
        /// get duihuan fangshi
        /// </summary>
        /// <param name="fangShi"></param>
        /// <returns></returns>
        string GetDuiHuanFangShi(object fangShi)
        {
            var _fagnShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)fangShi;
            string s = string.Empty;
            switch (_fagnShi)
            {
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分: s = "积分"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡: s = "积分+礼品卡"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金: s = "积分+现金"; break;
            }

            return s;
        }

        /// <summary>
        /// get jine
        /// </summary>
        /// <param name="fangShi"></param>
        /// <param name="jiFen"></param>
        /// <param name="jinE"></param>
        /// <returns></returns>
        string GetJinE(object fangShi, object jiFen, object jinE)
        {
            var _fagnShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)fangShi;
            var _jiFen = (decimal)jiFen;
            var _jinE = (decimal)jinE;

            string s = string.Empty;
            switch (_fagnShi)
            {
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分: s = _jiFen.ToString("F2") + "积分"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡: s = _jiFen.ToString("F2") + "积分+" + _jinE.ToString("F2") + "礼品卡"; break;
                case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金: s = _jiFen.ToString("F2") + "积分+" + _jinE.ToString("F2") + "现金"; break;
            }
            return s;
        }

        /// <summary>
        /// quxiao
        /// </summary>
        void QuXiao()
        {
            var bllRetCode = new EyouSoft.BLL.YlStructure.BDuiHuan().SheZhiJiFenDingDanStatus(DingDanId, SiteUserInfo.UserId, EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已取消);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
