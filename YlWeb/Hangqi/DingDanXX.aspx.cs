using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.YlWeb.Hangqi
{
    public partial class DingDanXX : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
        }

        void initPage()
        {
            string dingDanId = Utils.GetQueryStringValue("dingdanid");
            var DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing), Utils.GetQueryStringValue("dingdanleixing"));

            if (!DingDanLeiXing.HasValue
                || DingDanLeiXing.Value != EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单) Utils.RCWE("请求异常！");

            MHangQiDingDanInfo model = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(dingDanId);
            if (model == null) Utils.RCWE("请求异常！");

            string token = Utils.GetQueryStringValue("token");
            if (string.IsNullOrEmpty(token)) Utils.RCWE("请求异常！");

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            if (model.XiaDanRenId != token) RCWE("请求异常");
            if (isLogin && model.XiaDanRenId != huiYuanInfo.HuiYuanId) RCWE("请求异常");

            dingdanhao.Text = model.JiaoYiHao;

            dingdanzhuangtai.Text = model.FuKuanStatus.ToString();
            if (model.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款)
            {
                dingdanzhuangtai.Text = "<span style='color:#ff0000'>未付款</span>";

                if (model.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交)
                    ltr01.Text = "<a href=\"orderpay.aspx?dingdanleixing=" + (int)DingDanLeiXing.Value + "&dingdanid=" + dingDanId + "&token=" + token + "\" class=\"fukuan\">付款</a>";
                else if (model.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.审核中 || model.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.未处理)
                {
                    ltr01.Text = "审核中";
                }
                else if (model.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.取消)
                {
                    ltr01.Text = "已取消";
                }
                else
                {
                    ltr01.Text = string.Empty;
                }
            }
            else
            {
                ltr01.Text = string.Format("<a href=\"/Print/QueRenDan.aspx?DingDanId={0}\" target=\"_blank\" class=\"print-btn\">打印订单</a> <a href=\"/Print/XingChengDan.aspx?HangQiId={1}\" target=\"_blank\" class=\"print-btn\">打印行程单</a>", dingDanId, model.HangQiId);
            }

            chanpinjine.Text = "";
            decimal jinE = 0;
            #region  产品金额


            if (model.IsTuanGou)
            {
                if (model.TGJiaGes != null && model.TGJiaGes.Count > 0)
                {
                    foreach (var item in model.TGJiaGes)
                    {
                        jinE += item.RenShu * item.JiaGe;
                    }
                }
            }
            else
            {
                if (model.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                {
                    if (model.JiaGes != null && model.JiaGes.Count > 0)
                    {
                        foreach (var item in model.JiaGes)
                        {
                            jinE += item.RenShu1 * item.JiaGe1 + item.RenShu2 * item.JiaGe2 + item.RenShu3 * item.JiaGe3 + item.RenShu4 * item.JiaGe4;
                        }
                    }
                }
                else if (model.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                {
                    if (model.HYJiaGes != null && model.HYJiaGes.Count > 0)
                    {
                        foreach (var item in model.HYJiaGes)
                        {
                            jinE += item.JinE;
                        }
                    }
                }
            }
            #endregion
            chanpinjine.Text = jinE.ToString("C2");


            if (model.DiKouInfo != null)
            {
                shiyongjifen.Text = string.Format("-{0}", model.DiKouInfo.JinFenJinE.ToString("C2"));
            }
            else
            {
                JiFenTr.Visible = false;
            }
            shifujine.Text = model.JinE.ToString("C2");

            #region 附加产品
            decimal sumFJ = 0M;
            if (model.FuJiaChanPins != null && model.FuJiaChanPins.Count > 0)
            {
                for (int i = 0; i < model.FuJiaChanPins.Count; i++)
                {
                    sumFJ += model.FuJiaChanPins[i].JinE;
                }
            }
            fujiachanpinjie.Text = string.Format("{0}", sumFJ.ToString("C2"));
            #endregion

            kuaidifei.Text = model.FaPiaoKuaiDiJinE.ToString("C2");





            var hangqi = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(model.HangQiId);
            var riqi = new EyouSoft.BLL.YlStructure.BHangQi().GetRiQiInfo(model.RiQiId);
            var chuanzhi = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(hangqi.ChuanZhiId);

            if (hangqi != null)
            {
                dengchuanxinxi.Text = string.Format("<tr><td align=\"center\" valign=\"middle\">{0}</td><td align=\"center\">{1}</td><td align=\"center\" valign=\"middle\" class=\"font12\">{2}</td><td align=\"center\" valign=\"middle\">{3}</td></tr>", riqi != null ? riqi.RiQi.ToString() : "", hangqi.ChuFaGangKouMingCheng, hangqi.DiDaGangKouMingCheng, chuanzhi != null && model.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款 ? chuanzhi.ChuanZaiDianHua : "");
            }

            dingdanxinxi.Text += string.Format(" <tr>");
            dingdanxinxi.Text += string.Format(" <td align=\"middle\" class=\"padd20\">{0}</td>", model.MingCheng);
            dingdanxinxi.Text += string.Format(" <td align=\"center\">{0}</td>", riqi != null ? riqi.RiQi.ToString("yyyy-MM-dd") : "");
            dingdanxinxi.Text += string.Format(" <td align=\"center\" valign=\"top\" class=\"font12\">");


            #region 价格信息
            if (model.JiaGes != null && model.JiaGes.Count > 0)
            {
                for (int i = 0; i < model.JiaGes.Count; i++)
                {
                    dingdanxinxi.Text += string.Format(" <dl> <dt>【{0}】</dt><dd> {2} {1}人</dd></dl>", getGYSName(model.JiaGes[i].FangXingId.ToString()), (model.JiaGes[i].RenShu1 + model.JiaGes[i].RenShu2 + model.JiaGes[i].RenShu3 + model.JiaGes[i].RenShu4), getGYSName(model.JiaGes[i].BinKeLeiXingId.ToString()));
                }
            }

            #endregion



            dingdanxinxi.Text += string.Format("</dl></td>");
            dingdanxinxi.Text += string.Format("<td align=\"center\" valign=\"middle\">");

            if (model.FuJiaChanPins != null && model.FuJiaChanPins.Count > 0)
            {
                for (int i = 0; i < model.FuJiaChanPins.Count; i++)
                {
                    dingdanxinxi.Text += string.Format("{0}</br>", getGYSName(model.FuJiaChanPins[i].LeiXingId.ToString()));

                }

            }

            dingdanxinxi.Text += string.Format("</td></tr>");




            #region  游轮信息



            if (hangqi != null)
            {
            }
            #endregion

            #region 优惠信息
            decimal sumYH = 0M;
            if (model.YouHuis != null && model.YouHuis.Count > 0)
            {
                for (int i = 0; i < model.YouHuis.Count; i++)
                {
                    sumYH += model.YouHuis[i].JinE;
                }
            }
            youhuijine.Text = string.Format("-{0}", sumYH.ToString("C2"));
            #endregion

            #region 游客信息
            if (model.YouKes != null && model.YouKes.Count > 0)
            {
                rptyoukes.DataSource = model.YouKes;
                rptyoukes.DataBind();
            }
            #endregion

            #region 预订信息
            yudingren.Text = model.YuDingRenName;
            yudingyoujian.Text = model.YuDingRenYouXiang;
            yudingshouji.Text = model.YuDingRenShouJi;
            xiadanbeizhu.Text = model.XiaDanBeiZhu;
            #endregion

            if (model.IsXuYaoFaPiao)
            {
                #region 发票信息
                fapiaotaitou.Text = model.FaPiaoTaiTou;
                fapiaomingxi.Text = model.FaPiaoMingXi;
                #endregion

                #region 配送方式
                var dizhi = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhiInfo(model.FaPiaoDiZhiId);
                if (dizhi != null)
                {
                    var dizhixiangqing = new EyouSoft.BLL.ComStructure.BComCity().GetCPCD(YuMingInfo.CompanyId, dizhi.GuoJiaId, dizhi.ShengFenId, dizhi.ChengShiId, dizhi.XianQuId);
                    peisongfangshi.Text = string.Format("{0}({1})", dizhixiangqing != null ? dizhixiangqing.CountryName + dizhixiangqing.ProvinceName + dizhixiangqing.CityName + dizhixiangqing.CountyName + dizhi.DiZhi : "", model.FaPiaoPeiSongFangShi);
                }
                #endregion
            }
            else
            {
                phFaPiao.Visible = false;
                phFaPiaoPeiSongFangShi.Visible = false;
            }

            #region 旅客须知
            EyouSoft.Model.YlStructure.MWzKvInfo lkxz = null;

            if (hangqi.LeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                lkxz = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮旅客须知);
            if (hangqi.LeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                lkxz = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮旅客须知);
            if (lkxz != null && !string.IsNullOrEmpty(lkxz.V))
            {
                ltrLvKeXuZhi.Text = lkxz.V;
            }
            #endregion
        }

        /// <summary>
        /// 返回基础信息名称
        /// </summary>
        /// <param name="jcxxID"></param>
        /// <returns></returns>
        protected string getGYSName(string jcxxID)
        {
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXiInfo(Utils.GetInt(jcxxID));
            if (model == null) return "";
            return model.MingCheng;
        }

        protected void rptyoukes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex < 0) return;

            var info = (EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo)e.Item.DataItem;

            var ltrYK_ZJYXQ = (Literal)e.Item.FindControl("ltrYK_ZJYXQ");
            var ltrYK_CSRQ = (Literal)e.Item.FindControl("ltrYK_CSRQ");
            var ltrYK_XB = (Literal)e.Item.FindControl("ltrYK_XB");

            switch (info.ZhengJianLeiXing)
            {
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.户口本:
                    ltrYK_CSRQ.Text = info.SR1 + "-" + info.SR2 + "-" + info.SR3;
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.护照:
                    ltrYK_CSRQ.Text = info.SR1 + "-" + info.SR2 + "-" + info.SR3;
                    ltrYK_ZJYXQ.Text = info.YXQ1 + "-" + info.YXQ2 + "-" + info.YXQ3;
                    ltrYK_XB.Text = info.XingBie.ToString();
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.请选择:
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.稍后提供: 
                    break;
                case EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.身份证:
                    if (info.ChuShengRiQi.HasValue) ltrYK_CSRQ.Text = info.ChuShengRiQi.Value.ToString("yyyy-MM-dd");
                    ltrYK_XB.Text = info.XingBie.ToString();
                    break;
            }

        }
    }
}
