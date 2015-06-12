using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Hangqi
{
    public partial class JiFenDingDanXX : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dingDanId = Utils.GetQueryStringValue("dingdanid");
            var DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing), Utils.GetQueryStringValue("dingdanleixing"));

            if (!DingDanLeiXing.HasValue 
                || DingDanLeiXing.Value != EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单) Utils.RCWE("请求异常！");


            var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(dingDanId);
            if (info == null) Utils.RCWE("请求异常！");

            string token = Utils.GetQueryStringValue("token");
            if (string.IsNullOrEmpty(token)) Utils.RCWE("请求异常！");

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            if (info.XiaDanRenId != token) RCWE("请求异常");
            if (isLogin && info.XiaDanRenId != huiYuanInfo.HuiYuanId) RCWE("请求异常");

            dingdanhao.Text = info.JiaoYiHao;

            dingdanzhuangtai.Text = info.FuKuanStatus.ToString();
            if (info.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款)
            {
                dingdanzhuangtai.Text = "<span style='color:#ff0000'>未付款</span>";
                if (info.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已成交)
                {
                    ltr01.Text = "<a href=\"orderpay.aspx?dingdanleixing=" + (int)DingDanLeiXing.Value + "&dingdanid=" + dingDanId + "&token=" + token + "\" class=\"fukuan\">付款</a>";
                }
                else if (info.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.未处理)
                {
                    ltr01.Text = "审核中";
                }
                else if (info.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已取消)
                {
                    ltr01.Text = "已取消";
                }
                else
                {
                    ltr01.Text = string.Empty;
                }
            }

            shanpinjine.Text = info.ShangPinJinE.ToString("C2");

            string fangshi = string.Empty;
            if (info.FangShi == EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分)
            {
                fangshi = "积分";
            }
            else if (info.FangShi == EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金)
            {
                fangshi = "积分+现金";
            }
            duihuanfangshi.Text = fangshi;
            duihuanjifen.Text = info.JiFen.ToString("F2");
            duihuanjine.Text = (info.JinE - info.FaPiaoKuaiDiJinE).ToString("C2");
            kuaidifei.Text = info.FaPiaoKuaiDiJinE.ToString("C2");
            shifujine.Text = info.JinE.ToString("C2");
            duihuanshuliang.Text = info.ShuLiang.ToString();

            #region 预订信息
            yudingren.Text = info.YuDingRenName;
            yudingyoujian.Text = info.YuDingRenYouXiang;
            yudingshouji.Text = info.YuDingRenShouJi;
            #endregion

            if (info.IsXuYaoFaPiao)
            {
                #region 发票信息
                fapiaotaitou.Text = info.FaPiaoTaiTou;
                fapiaomingxi.Text = info.FaPiaoMingXi;
                #endregion

                #region 配送方式
                var dizhi = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhiInfo(info.FaPiaoDiZhiId);
                if (dizhi != null)
                {
                    var dizhixiangqing = new EyouSoft.BLL.ComStructure.BComCity().GetCPCD(YuMingInfo.CompanyId, dizhi.GuoJiaId, dizhi.ShengFenId, dizhi.ChengShiId, dizhi.XianQuId);
                    peisongfangshi.Text = string.Format("{0}({1})", dizhixiangqing != null ? dizhixiangqing.CountryName + dizhixiangqing.ProvinceName + dizhixiangqing.CityName + dizhixiangqing.CountyName + dizhi.DiZhi : "", info.FaPiaoPeiSongFangShi);
                }
                #endregion
            }
            else
            {
                phFaPiao.Visible = false;
                phFaPiaoPeiSongFangShi.Visible = false;
            }
        }
    }
}
