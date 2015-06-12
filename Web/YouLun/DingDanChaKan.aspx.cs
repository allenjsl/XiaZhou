using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.YlStructure;
using System.Text;

namespace EyouSoft.Web.YouLun
{
    public partial class DingDanChaKan : BackPage
    {
        protected string dingdanzhuangtai = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "baocunyk") BaoCunYK();
            if (Utils.GetQueryStringValue("dotype") == "SaveDingDanJiFen") SaveDingDanJiFen();
            initPage();
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
        }
        /// <summary>
        /// 设置状态
        /// </summary>
        void BaoCun()
        {
            string id = Utils.GetQueryStringValue("id");
            EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus state = (EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus)Utils.GetInt(Utils.GetFormValue("dingdanState"));

            MHangQiDingDanInfo model = new MHangQiDingDanInfo() { DingDanId = id, JinE = Utils.GetDecimal(Utils.GetFormValue(this.litZongJinE.UniqueID)), DingDanStatus = state, XiaDanRenId = SiteUserInfo.UserId,DingDanJiFen=Utils.GetDecimal(Utils.GetFormValue(txtDingDanJiFen.UniqueID)) };
            var b = new EyouSoft.BLL.YlStructure.BHangQiDingDan();
            var result = b.UpdateDingDan(model);

            if (result == 1)
                result = b.SheZhiDingDanStatus(id, SiteUserInfo.UserId, state, null);
            if (result == 1) { SavaCaoZuoBeiZhu(); RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功")); }
            RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
        }

        /// <summary>
        /// 更新订单积分
        /// </summary>
        void SaveDingDanJiFen()
        {
            var r = new BLL.YlStructure.BHangQiDingDan().UpDateDingDanJiFen(Utils.GetQueryStringValue("id"), Utils.GetDecimal(Utils.GetFormValue("txtDingDanJiFen")));

            switch (r)
            {
                case -99:
                    RCWE(UtilsCommons.AjaxReturnJson("0", "会员可用积分小于0"));
                    break;
                case -100:
                case 0:
                    RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
                    break;
                case 1:
                    SavaCaoZuoBeiZhu();
                    RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
                    break;
            }
        }

        /// <summary>
        /// 更新操作备注
        /// </summary>
        /// <returns></returns>
        int SavaCaoZuoBeiZhu() 
        {
            return new BLL.YlStructure.BHangQiDingDan().UpdDingDanCaoZuoBeiZhu(Utils.GetQueryStringValue("id"), Utils.GetFormValue(txtCaoZuoBeiZhu.UniqueID));
        }
        
        /// <summary>
        /// 初始化页面
        /// </summary>
        void initPage()
        {
            string id = Utils.GetQueryStringValue("id");
            MHangQiDingDanInfo model = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(id);
            if (model == null) return;

            fukuanzhuangtai.Text = model.FuKuanStatus.ToString();
            if (model.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款)
            {
                var zxzfinfo = new EyouSoft.BLL.YlStructure.BZaiXianZhiFu().GetInfo(model.DingDanId);
                if (zxzfinfo != null)
                {
                    switch (zxzfinfo.ZhiFuFangShi)
                    {
                        case   EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay:
                            fukuanzhuangtai.Text+="【支付宝】";
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Bill99:
                            fukuanzhuangtai.Text += "【快钱】";
                            break;
                        default: break;
                    }
                }
            }

            litZongJinE.Text = model.JinE.ToString("F2");
            txtDingDanJiFen.Text = model.DingDanJiFen.ToString("F2");
            if (model.DiKouInfo != null)
                litDiKouJinE.Text = model.DiKouInfo.JinFenJinE.ToString("F2");
            else
                litDiKouJinE.Text = "0.00";
            dingdanzhuangtai = ((int)(model.DingDanStatus)).ToString();
            litKuaiDiJinE.Text = model.FaPiaoKuaiDiJinE.ToString("F2");

            selectDDL.Text = getSelectState(((int)model.DingDanStatus).ToString());
            txtCaoZuoBeiZhu.Value = model.CaoZuoBeiZhu;

            #region  游轮信息

            gongyingshang.Text = model.GysName;
            youlungongsi.Text = model.GongSiName;
            youlunxilie.Text = model.XiLieName;
            youlunchuanzhi.Text = model.ChuanZhiName;

            var hangqi = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(model.HangQiId);//获取航期

            if (hangqi != null)
            {
                chufagangkou.Text = hangqi.ChuFaGangKouMingCheng;
                didagangkou.Text = hangqi.DiDaGangKouMingCheng;
                xingchengtianshu.Text = string.Format("{0}天{1}晚", hangqi.TianShu1, hangqi.TianShu2);
                chanpinmingcheng.Text = hangqi.MingCheng;
                chanpinbianhao.Text = hangqi.BianHao;
            }
            #endregion

            #region 价格信息
            jiagexingxi.Text = GetFangXingString(model);
            #endregion

            #region 附加产品
            if (model.FuJiaChanPins != null && model.FuJiaChanPins.Count > 0)
            {
                rptfujia.DataSource = model.FuJiaChanPins;
                rptfujia.DataBind();
                PlaceHolder2.Visible = false;
            }
            #endregion

            #region 优惠信息
            if (model.YouHuis != null && model.YouHuis.Count > 0)
            {
                rptyouhuis.DataSource = model.YouHuis;
                rptyouhuis.DataBind();
            }

            #endregion

            #region 游客信息
            if (model.YouKes != null && model.YouKes.Count > 0)
            {
                rptyouke.DataSource = model.YouKes;
                rptyouke.DataBind();
                PlaceHolder3.Visible = false;
            }
            #endregion

            #region 预订信息
            yudingxingming.Text = model.YuDingRenName;
            //yudingzhanghao.Text=model.yudingz
            yudingshijian.Text = model.IssueTime.ToString();
            yudinglianxidianhua.Text = model.YuDingRenDianHua;
            yudinglianxishouji.Text = model.YuDingRenShouJi;
            yudinglianxiyouxiang.Text = model.YuDingRenYouXiang;
            xiadanbeizhu.Text = model.XiaDanBeiZhu;
            #endregion

            #region 发票信息
            if (model.IsXuYaoFaPiao)
            {
                var dizhi = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhiInfo(model.FaPiaoDiZhiId);
                if (dizhi != null)
                {
                    var dizhixiangqing = new EyouSoft.BLL.ComStructure.BComCity().GetCPCD(SiteUserInfo.CompanyId, dizhi.GuoJiaId, dizhi.ShengFenId, dizhi.ChengShiId, dizhi.XianQuId);
                    //youjidizhi.Text = string.Format("{0}({1})", dizhixiangqing != null ? dizhixiangqing.CountryName + dizhixiangqing.ProvinceName + dizhixiangqing.CityName + dizhixiangqing.CountyName + dizhi.DiZhi : "", model.FaPiaoPeiSongFangShi);
                    if (dizhixiangqing != null)
                    {
                        youjidizhi.Text = dizhixiangqing.ProvinceName + "&nbsp;" + dizhixiangqing.CityName + "&nbsp;" + dizhixiangqing.CountyName + "&nbsp;" + dizhi.DiZhi;
                        shoujianrenxingming.Text = dizhi.XingMing;
                        shoujianrendianhua.Text = dizhi.DianHua;
                    }
                }

                ltrFaPiaoTaiTou.Text = model.FaPiaoTaiTou;
                ltrFaPiaoMingXi.Text = model.FaPiaoMingXi;

                phFaPiao1.Visible = true;
                ltrShiFouXuYaoFaPiao.Text = "需要发票";
            }
            else
            {
                ltrShiFouXuYaoFaPiao.Text = "不需要发票";
            }            
            #endregion

            chufariqi.Text = model.RiQi.ToString("yyyy-MM-dd");

            if (model.DingDanStatus == EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.取消)
            {
                ltrCaoZuo.Text = "订单已取消";
            }
            else
            {
                if (model.FuKuanStatus == EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款)
                {
                    ltrCaoZuo.Text = "订单已付款";
                }
                else
                {
                    ltrCaoZuo.Text = "<a id=\"btnSave\" href=\"javascript:;\"><s class=\"baochun\"></s>保 存</a> ";
                }
            }
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

        /// <summary>
        /// 获取小计
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        protected string getXJ(object i, object j)
        {
            decimal danjia = Utils.GetDecimal(i.ToString());
            int shuliang = Utils.GetInt(j.ToString());
            return (danjia * shuliang).ToString("F2");
        }

        /// <summary>
        /// 获取价格信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string GetFangXingString(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info)
        {
            if (info == null) return string.Empty;
            System.Text.StringBuilder s = new System.Text.StringBuilder();


            if (info.IsTuanGou)
            {
                if (info.TGJiaGes != null && info.TGJiaGes.Count > 0)
                {
                    foreach (var item in info.TGJiaGes)
                    {
                        s.AppendFormat("{0}，{1}人，小计{2}<br/>", getGYSName(item.BinKeLeiXingId.ToString()), item.RenShu, (item.JiaGe * item.RenShu).ToString("F2"));
                    }
                }
                return s.ToString();
            }

            if (info.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
            {
                if (info.CJJiaGes == null || info.CJJiaGes.Count == 0) return string.Empty;

                foreach (var item in info.CJJiaGes)
                {
                    if (item.JiaGes != null && item.JiaGes.Count > 0)
                    {
                        foreach (var item1 in item.JiaGes)
                        {
                            s.AppendFormat("国籍：{0}，房型：{1}", getGYSName(item.GuoJiId.ToString()), getGYSName(item.FangXingId.ToString()));
                            s.AppendFormat("，{0}", getGYSName(item1.BinKeLeiXingId.ToString()));
                            if (item1.RenShu1 > 0)
                            {
                                s.AppendFormat("，人数：{0}", item1.RenShu1);
                            }
                            if (item1.RenShu2 > 0)
                            {
                                s.AppendFormat("，占床：{0}人", item1.RenShu2);
                            }
                            if (item1.RenShu3 > 0)
                            {
                                s.AppendFormat("，加床：{0}人", item1.RenShu3);
                            }
                            if (item1.RenShu4 > 0)
                            {
                                s.AppendFormat("，不占床：{0}人", item1.RenShu4);
                            }

                            s.AppendFormat("，小计{0}", (item1.RenShu1 * item1.JiaGe1 + item1.RenShu2 * item1.JiaGe2 + item1.RenShu3 * item1.JiaGe3 + item1.RenShu4 * item1.JiaGe4).ToString("F2"));
                            s.AppendFormat("<br/>");
                        }
                    }
                }

                return s.ToString();
            }


            if (info.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
            {
                if (info.HYJiaGes == null || info.HYJiaGes.Count == 0) return string.Empty;

                foreach (var item in info.HYJiaGes)
                {
                    s.AppendFormat("房型：{0}，楼层：{1}，", getGYSName(item.FangXingId.ToString()), item.LouCeng);
                    if (item.JiaGes != null && item.JiaGes.Count > 0)
                    {
                        foreach (var item1 in item.JiaGes)
                        {
                            s.AppendFormat("，{0} {1}人", getGYSName(item1.BinKeLeiXingId.ToString()), item1.RenShu);
                        }
                    }
                    s.AppendFormat("，小计{0}", item.JinE.ToString("F2"));
                    s.AppendFormat("<br/>");
                }

                return s.ToString();
            }

            return string.Empty;
        }

        string getSelectState(string select)
        {
            if (select == ((int)(EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.取消)).ToString()) return EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.取消.ToString();
            System.Text.StringBuilder strbu = new System.Text.StringBuilder();
            strbu.Append("<select id=\"dingdanState\" name=\"dingdanState\" class=\"inputselect\" style=\"width: 120px;\">");
            strbu.Append(EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus), new string[] { "2", "3", "6" }), select));
            strbu.Append("</select>");

            return strbu.ToString();
        }

        protected string GetYK_ZJYXQ(object zjlx, object yxq, object yxq1, object yxq2, object yxq3)
        {
            EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing _zjlx = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)zjlx;
            string _yxq = string.Empty;
            string _yxq1 = string.Empty;
            string _yxq2 = string.Empty;
            string _yxq3 = string.Empty;
            if (yxq != null) _yxq = yxq.ToString();
            if (yxq1 != null) _yxq1 = yxq1.ToString();
            if (yxq2 != null) _yxq2 = yxq2.ToString();
            if (yxq3 != null) _yxq3 = yxq3.ToString();

            if (string.IsNullOrEmpty(_yxq) 
                && string.IsNullOrEmpty(_yxq1) 
                && string.IsNullOrEmpty(_yxq2) 
                && string.IsNullOrEmpty(_yxq3)) return string.Empty;

            if (!string.IsNullOrEmpty(_yxq)) return Utils.GetDateTime(_yxq).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(_yxq1))
            {
                if (string.IsNullOrEmpty(_yxq2)) _yxq2 = "01";
                if (string.IsNullOrEmpty(_yxq3)) _yxq3 = "01";

                var d = Utils.GetDateTimeNullable(_yxq1 + "-" + _yxq2 + "-" + _yxq3);
                if (d.HasValue) return d.Value.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }

        protected string GetYK_CSRQ(object zjlx, object sr, object sr1, object sr2, object sr3)
        {
            EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing _zjlx = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)zjlx;
            string _sr = string.Empty;
            string _sr1 = string.Empty;
            string _sr2 = string.Empty;
            string _sr3 = string.Empty;
            if (sr != null) _sr = sr.ToString();
            if (sr1 != null) _sr1 = sr1.ToString();
            if (sr2 != null) _sr2 = sr2.ToString();
            if (sr3 != null) _sr3 = sr3.ToString();

            if (string.IsNullOrEmpty(_sr)
                && string.IsNullOrEmpty(_sr1)
                && string.IsNullOrEmpty(_sr2)
                && string.IsNullOrEmpty(_sr3)) return string.Empty;

            if (!string.IsNullOrEmpty(_sr))
            {
                var d = Utils.GetDateTimeNullable(_sr);
                if (d.HasValue) return d.Value.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(_sr1))
            {
                if (string.IsNullOrEmpty(_sr2)) _sr2 = "01";
                if (string.IsNullOrEmpty(_sr3)) _sr3 = "01";

                var d = Utils.GetDateTimeNullable(_sr1 + "-" + _sr2 + "-" + _sr3);
                if (d.HasValue) return d.Value.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }

        void BaoCunYK()
        {
            string dingdanid = Utils.GetQueryStringValue("id");
            string[] yk_youkeid = Utils.GetFormValues("yk_youkeid");
            string[] yk_xingming = Utils.GetFormValues("yk_xingming");
            string[] yk_xingbie = Utils.GetFormValues("yk_xingbie");
            string[] yk_zhengjianleixing = Utils.GetFormValues("yk_zhengjianleixing");
            string[] yk_zhengjianhaoma = Utils.GetFormValues("yk_zhengjianhaoma");
            string[] yk_zhengjianyouxiaoqi = Utils.GetFormValues("yk_zhengjianyouxiaoqi");
            string[] yk_chushengriqi = Utils.GetFormValues("yk_chushengriqi");
            string[] yk_shouji = Utils.GetFormValues("yk_shouji");

            IList<MHangQiDingDanYouKeInfo> items = new List<MHangQiDingDanYouKeInfo>();

            for (int i = 0; i < yk_youkeid.Length; i++)
            {
                var item = new MHangQiDingDanYouKeInfo();

                item.YouKeId = yk_youkeid[i];
                item.XingMing = yk_xingming[i];
                item.XingBie = Utils.GetEnumValue(yk_xingbie[i], EyouSoft.Model.EnumType.GovStructure.Gender.男);
                item.ZhengJianLeiXing = Utils.GetEnumValue(yk_zhengjianleixing[i], EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.请选择);
                item.ZhengJianHaoMa = yk_zhengjianhaoma[i];
                item.ZhengJianYouXiaoQi = Utils.GetDateTimeNullable(yk_zhengjianyouxiaoqi[i]);
                item.ChuShengRiQi = Utils.GetDateTimeNullable(yk_chushengriqi[i]);
                item.ShouJi = yk_shouji[i];

                if (item.ZhengJianYouXiaoQi.HasValue)
                {
                    item.YXQ1 = item.ZhengJianYouXiaoQi.Value.Year.ToString();
                    item.YXQ2 = item.ZhengJianYouXiaoQi.Value.Month.ToString();
                    item.YXQ3 = item.ZhengJianYouXiaoQi.Value.Day.ToString();
                }

                if (item.ChuShengRiQi.HasValue)
                {
                    item.SR1 = item.ChuShengRiQi.Value.Year.ToString();
                    item.SR2 = item.ChuShengRiQi.Value.Month.ToString();
                    item.SR3 = item.ChuShengRiQi.Value.Day.ToString();
                }

                if (item.ZhengJianLeiXing == EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.身份证)
                {
                    item.ChuShengRiQi = Utils.GetDateTimeNullable(getSR(item.ZhengJianHaoMa));
                    item.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(getXB(item.ZhengJianHaoMa));
                }

                items.Add(item);
            }

            int bllRetCode = new EyouSoft.BLL.YlStructure.BHangQiDingDan().UpdateDingDanYouKes(dingdanid, items);

            if (bllRetCode == 1) { SavaCaoZuoBeiZhu(); RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功")); }
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        string getSR(string hm)
        {
            if (hm.Length == 18)
            {
                return hm.Substring(6, 4) + "-" + hm.Substring(10, 2) + "-" + hm.Substring(12, 2);
            }

            if (hm.Length == 15)
            {
                return "19" + hm.Substring(6, 2) + "-" + hm.Substring(8, 2) + "-" + hm.Substring(10, 2);
            }

            return string.Empty;
        }

        string getXB(string hm)
        {
            string s = string.Empty;
            string v = string.Empty;
            if (hm.Length == 18)
            {
                s = hm.Substring(14, 3);
            }
            if (hm.Length == 15)
            {
                s = hm.Substring(12, 3);
            }

            if (!string.IsNullOrEmpty(s))
            {
                if (Utils.GetInt(s) % 2 == 0)
                {
                    v = "1";
                }
                else
                {
                    v = "0";
                }
            }

            return v;
        }
    }
}
