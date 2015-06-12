using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.YlStructure;
using System.Text;
using System.Net;
using System.IO;

namespace EyouSoft.YlWeb.ashx
{
    /// <summary>
    /// 公共处理程序
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {
        HttpContext context = null;

        public void ProcessRequest(HttpContext param_context)
        {
            context = param_context;
            context.Response.ContentType = "text/plain";

            string dotype = Utils.GetQueryStringValue("dotype");

            switch (dotype)
            {
                case "mislogin": HuiYuanIsLogin(); break;
                case "hangqiyouhui": HangQiYouHui(); break;
                case "collect": CollectProductc(); break;
                case "dianping": DingPingHtml(); break;
                case "zixun": ZiXunHtml(); break;
                case "wdtw": WenDaTiJiao(); break;
                case "fasongmima": FaSongMiMa(); break;
                case "guanjianzisousuo": GuanJianZiSouSuo(); break;
                default: break;
            }
        }

        #region public members
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 航期优惠游客信息业务实体
        /// </summary>
        public class MYouKeInfo
        {
            /// <summary>
            /// 证件号码
            /// </summary>
            public string zjhm { get; set; }
            /// <summary>
            /// 生日
            /// </summary>
            public string sr { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string xb { get; set; }
            /// <summary>
            /// 证件类型
            /// </summary>
            public string zjlx { get; set; }
            /// <summary>
            /// 生日01
            /// </summary>
            public string sr01 { get; set; }
        }
        #endregion

        #region private members
        /// <summary>
        /// 关键字搜索
        /// </summary>
        void GuanJianZiSouSuo()
        {
            var recordcount = 0;
            var gjz = Utils.GetQueryStringValue("q");
            var lst = new BLL.YlStructure.BHangQi().GetHangQis(EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo().CompanyId, 10, 1, ref recordcount, new MHangQiChaXunInfo() { GuanJianZi = gjz });
            var sb = new StringBuilder();
            var s = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                foreach (var m in lst)
                {
                    if (string.IsNullOrEmpty(s)) if (m.MingCheng.IndexOf(gjz) >= 0) s = m.MingCheng;
                    if (string.IsNullOrEmpty(s)) if (m.HangXianMingCheng.IndexOf(gjz) >= 0) s = m.HangXianMingCheng;
                    if (string.IsNullOrEmpty(s)) if (m.ChuFaGangKouMingCheng.IndexOf(gjz) >= 0) s = m.ChuFaGangKouMingCheng;
                    if (string.IsNullOrEmpty(s)) if (m.DiDaGangKouMingCheng.IndexOf(gjz) >= 0) s = m.DiDaGangKouMingCheng;
                    if (string.IsNullOrEmpty(s)) if (m.XiLieName.IndexOf(gjz) >= 0) s = m.XiLieName;
                    if (string.IsNullOrEmpty(s)) if (m.ChuanZhiName.IndexOf(gjz) >= 0) s = m.ChuanZhiName;
                    if (!string.IsNullOrEmpty(s)) sb.AppendFormat("{0}|{1}\n", s, (int)m.LeiXing);
                }
            }
            Utils.RCWE(sb.ToString());
            //Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(lst));
        }
        /// <summary>
        /// 发送密码
        /// </summary>
        void FaSongMiMa() 
        {
            var shouji = Utils.GetQueryStringValue("shouji");

            if (!string.IsNullOrEmpty(shouji))
            {
                var newMM = EyouSoft.YlWeb.Rand.Number(6,false);
                var b = new EyouSoft.BLL.YlStructure.BHuiYuan();
                var m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
                var huiYuanInfo = new MHuiYuanInfo();
                var pwd = new EyouSoft.Model.ComStructure.MPasswordInfo();
                var mdl = b.GetHuiYuanInfo(m.CompanyId, shouji, 0);
                var r = 0;

                pwd.NoEncryptPassword = newMM;

                huiYuanInfo.HuiYuanId = mdl != null ? mdl.HuiYuanId : string.Empty;
                huiYuanInfo.CompanyId = m.CompanyId;
                huiYuanInfo.Username = shouji;
                huiYuanInfo.MD5Password = pwd.MD5Password;
                huiYuanInfo.ShengRi = DateTime.Now;
                huiYuanInfo.LeiXing = EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing.注册会员;
                huiYuanInfo.ShouJi = shouji;

                if (!string.IsNullOrEmpty(huiYuanInfo.HuiYuanId))
                {
                    r = b.SheZhiHuiYuanMiMa(huiYuanInfo.HuiYuanId, mdl.MD5Password, pwd.MD5Password);
                }
                else
                {
                    r = b.InsertHuiYuan(huiYuanInfo);
                }

                string content = "尊敬的维诗达客户，您好！您的动态密码为"  +newMM + "，用于网站登录【维诗达游轮公司】";
                if (EyouSoft.Toolkit.Utils.ProxySMS(shouji, content))
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "发送成功！"));
                else
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "发送失败！"));

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请输入正确的手机号码！"));
            }
        }
        /// <summary>
        /// 会员是否登录
        /// </summary>
        void HuiYuanIsLogin()
        {
            MYlHuiYuanInfo m;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            var output = new System.Text.StringBuilder();
            output.Append("{");

            output.AppendFormat("\"retCode\":{0}", "1");
            output.AppendFormat(",\"isLogin\":{0}", isLogin ? "true" : "false");
            output.AppendFormat(",\"token\":\"{0}\"", isLogin ? m.HuiYuanId : "");
            if (isLogin)
            {
                output.AppendFormat(",\"u\":\"{0}\"", m.Username);
            }
            else
            {
                output.AppendFormat(",\"u\":\"\"");
            }

            output.Append("}");

            context.Response.Write(output);
        }

        /// <summary>
        /// 航期优惠
        /// </summary>
        void HangQiYouHui()
        {
            string hangQiId = Utils.GetFormValue("txtHangQiId");
            string riQiId = Utils.GetFormValue("txtRiQiId");
            int renShu = Utils.GetInt(Utils.GetFormValue("txtRenShu"));
            decimal jinE = Utils.GetDecimal(Utils.GetFormValue("txtJinE"));
            string youke = Utils.GetFormValue("txtYouKe");
            var youKes = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<MYouKeInfo>>(youke);

            if (string.IsNullOrEmpty(hangQiId)
                || string.IsNullOrEmpty(riQiId)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi;
            var hangQiYouHuiGuiZes = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiyouHuiGuiZes(hangQiId, out fangShi);
            var riQiInfo = new EyouSoft.BLL.YlStructure.BHangQi().GetRiQiInfo(riQiId);

            if (hangQiYouHuiGuiZes == null || hangQiYouHuiGuiZes.Count == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "无优惠"));
            if (riQiInfo == null) Utils.RCWE(UtilsCommons.AjaxReturnJson("-2", "无优惠"));

            IList<MYouHuiInfo> shiJiYouHuis = new List<MYouHuiInfo>();

            foreach (var hangQiYouHuiGuiZe in hangQiYouHuiGuiZes)
            {
                var shiJiYouHui = HangQiYouHui1(hangQiYouHuiGuiZe, riQiInfo, youKes, renShu, jinE);

                if (shiJiYouHui != null) shiJiYouHuis.Add(shiJiYouHui);
            }

            if (shiJiYouHuis == null || shiJiYouHuis.Count == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("-3", "无优惠"));

            var outPutInfo = new MYouHuiOutPutInfo();
            outPutInfo.fangshi = ((int)fangShi).ToString();
            outPutInfo.jine = 0;
            outPutInfo.youhuis = new List<MYouHuiInfo>();

            if (fangShi == EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi.同时享有)
            {
                foreach (var item in shiJiYouHuis)
                {
                    outPutInfo.jine = outPutInfo.jine + item.jine;
                }
                outPutInfo.youhuis = shiJiYouHuis;
            }

            if (fangShi == EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi.最低金额)
            {
                int index = 0; decimal minJinE = shiJiYouHuis[index].jine;

                for (int i = 1; i < shiJiYouHuis.Count; i++)
                {
                    if (minJinE > shiJiYouHuis[i].jine)
                    {
                        minJinE = shiJiYouHuis[i].jine;
                        index = i;
                    }
                }

                outPutInfo.jine = minJinE;
                outPutInfo.youhuis.Add(shiJiYouHuis[index]);
            }

            if (fangShi == EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi.最高金额)
            {
                int index = 0; decimal maxJinE = shiJiYouHuis[index].jine;

                for (int i = 1; i < shiJiYouHuis.Count; i++)
                {
                    if (maxJinE < shiJiYouHuis[i].jine)
                    {
                        maxJinE = shiJiYouHuis[i].jine;
                        index = i;
                    }
                }

                outPutInfo.jine = maxJinE;
                outPutInfo.youhuis.Add(shiJiYouHuis[index]);
            }

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", outPutInfo));
        }

        /// <summary>
        /// 航期优惠-计算单个规则的优惠信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 规则按单计算时，有一个游客满足游客类条件即符合规则
        /// </remarks>
        MYouHuiInfo HangQiYouHui1(EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo guiZeInfo, EyouSoft.Model.YlStructure.MHangQiRiQiInfo riQiInfo, IList<MYouKeInfo> youKes, int renShu, decimal jinE)
        {
            MYouHuiInfo shiJiYouHui = null;
            if (string.IsNullOrEmpty(guiZeInfo.GuiZe)) return null;
            if (guiZeInfo.JinE == 0) return null;
            var guiZeJson = Newtonsoft.Json.JsonConvert.DeserializeObject<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfoJSON>(guiZeInfo.GuiZe);
            if (guiZeJson == null) return null;

            bool b1 = true, b2 = true, b3 = true, b4 = true, b5 = true, b6 = true, b7 = true, b8 = true, b9 = true;

            DateTime dn = DateTime.Now;
            int shiJiJiSuanRenShu = renShu;

            #region 下单时间-提前天
            if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianTiaoJian) && !string.IsNullOrEmpty(guiZeJson.XiaDanShiJianTianShu))
            {
                var b = false;
                int cha = ShiJianChaTian(riQiInfo.RiQi, dn);

                if (guiZeJson.XiaDanShiJianTiaoJian == "1")
                {
                    if (cha > Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                else if (guiZeJson.XiaDanShiJianTiaoJian == "2")
                {
                    if (cha == Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                else if (guiZeJson.XiaDanShiJianTiaoJian == "3")
                {
                    if (cha < Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                b1 = b;
            }
            #endregion

            #region 下单时间-指定日期
            if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianS) && !string.IsNullOrEmpty(guiZeJson.XiaDanShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianS);
                var d2 = Utils.GetDateTime(guiZeJson.XiaDanShiJianE).AddDays(1).AddSeconds(-1);
                if (dn >= d1 && dn <= d2) b = true;
                b2 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianS))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianS);
                if (dn >= d1) b = true;
                b2 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianE).AddDays(1).AddSeconds(-1);
                if (dn <= d1) b = true;
                b2 = b;
            }
            #endregion

            #region 出港日期-指定日期
            if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianS) && !string.IsNullOrEmpty(guiZeJson.ChuGangShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianS);
                var d2 = Utils.GetDateTime(guiZeJson.ChuGangShiJianE).AddDays(1).AddSeconds(-1);
                if (riQiInfo.RiQi >= d1 && riQiInfo.RiQi <= d2) b = true;
                b3 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianS))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianS);
                if (riQiInfo.RiQi >= d1) b = true;
                b3 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianE).AddDays(1).AddSeconds(-1);
                if (riQiInfo.RiQi <= d1) b = true;
                b3 = b;
            }
            #endregion

            #region 人数
            if (!string.IsNullOrEmpty(guiZeJson.RenShuTiaoJian) && !string.IsNullOrEmpty(guiZeJson.RenShu))
            {
                var b = false;

                if (guiZeJson.RenShuTiaoJian == "1")
                {
                    if (renShu > Utils.GetInt(guiZeJson.RenShu)) b = true;
                }
                else if (guiZeJson.RenShuTiaoJian == "2")
                {
                    if (renShu == Utils.GetInt(guiZeJson.RenShu)) b = true;
                }
                else if (guiZeJson.RenShuTiaoJian == "3")
                {
                    if (renShu < Utils.GetInt(guiZeJson.RenShu)) b = true;
                }

                b4 = b;
            }
            #endregion

            #region 游客年龄
            if (!string.IsNullOrEmpty(guiZeJson.YouKeNianLingTiaoJian) && !string.IsNullOrEmpty(guiZeJson.YouKeNianLing))
            {
                int shuliang = 0;

                if (youKes != null && youKes.Count > 0)
                {
                    foreach (var youKe in youKes)
                    {
                        var b = false;
                        DateTime? sr = null;
                        if (youKe.zjlx == "1") sr = Utils.GetDateTimeNullable(getSR(youKe.zjhm));
                        else if (youKe.zjlx == "2") sr = Utils.GetDateTimeNullable(youKe.sr01 + "-01-01");
                        else if (youKe.zjlx == "3") sr = Utils.GetDateTimeNullable(youKe.sr + "-01-01");
                        else if (youKe.zjlx == "4") sr = null;
                        else if (youKe.zjlx == "0") sr = null;
                        else sr = null;

                        if (!sr.HasValue) continue;

                        int nl = ShiJianChaNian(dn, sr.Value);

                        if (guiZeJson.YouKeNianLingTiaoJian == "1")
                        {
                            if (nl > Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                        }
                        else if (guiZeJson.YouKeNianLingTiaoJian == "2")
                        {
                            if (nl == Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                        }
                        else if (guiZeJson.YouKeNianLingTiaoJian == "3")
                        {
                            if (nl < Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                        }

                        if (b) shuliang++;
                    }
                }

                if (shiJiJiSuanRenShu > shuliang) shiJiJiSuanRenShu = shuliang;

                if (shuliang > 0) b5 = true;
                else b5 = false;
            }
            #endregion

            #region 游客区域
            if (!string.IsNullOrEmpty(guiZeJson.YouKeQuYu))
            {
                int shuliang = 0;
                var quyus = guiZeJson.YouKeQuYu.Split(',');

                if (youKes != null && youKes.Count > 0)
                {
                    foreach (var youKe in youKes)
                    {
                        if (string.IsNullOrEmpty(youKe.zjhm)) continue;
                        if (string.IsNullOrEmpty(youKe.zjlx)) continue;
                        if (youKe.zjhm.Length < 2) continue;
                        var zjlx = (EyouSoft.Model.EnumType.TourStructure.CardType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.TourStructure.CardType), youKe.zjlx);
                        if(!zjlx.HasValue)continue;
                        if (zjlx.Value != EyouSoft.Model.EnumType.TourStructure.CardType.身份证 
                            && zjlx.Value != EyouSoft.Model.EnumType.TourStructure.CardType.户口本) continue;

                        int sfid = new EyouSoft.BLL.ComStructure.BComCity().GetSFID_SFXZQHDM(youKe.zjhm.Substring(0, 2));

                        //if (sfid.ToString() == guiZeJson.YouKeQuYu) shuliang++;
                        if (quyus.Contains(sfid.ToString())) shuliang++;
                    }
                }

                if (shiJiJiSuanRenShu > shuliang) shiJiJiSuanRenShu = shuliang;

                if (shuliang > 0) b6 = true;
                else b6 = false;
            }
            #endregion

            #region 是否会员
            if (!string.IsNullOrEmpty(guiZeJson.ShiFouHuiYuan))
            {
                bool b = false;
                MYlHuiYuanInfo m = null;
                bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

                if (isLogin && guiZeJson.ShiFouHuiYuan == "1") b = true;

                if (!isLogin && guiZeJson.ShiFouHuiYuan == "2") b = true;

                b7 = b;
            }
            #endregion

            #region 订单金额
            if (!string.IsNullOrEmpty(guiZeJson.DingDanJinETiaoJian) && !string.IsNullOrEmpty(guiZeJson.DingDanJinE))
            {
                var b = false;

                if (guiZeJson.DingDanJinETiaoJian == "1")
                {
                    if (jinE > Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }
                else if (guiZeJson.DingDanJinETiaoJian == "2")
                {
                    if (jinE == Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }
                else if (guiZeJson.DingDanJinETiaoJian == "3")
                {
                    if (jinE < Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }

                b8 = b;
            }
            #endregion

            #region 游客性别
            if (!string.IsNullOrEmpty(guiZeJson.XingBie))
            {
                int shuliang = 0;

                if (youKes != null && youKes.Count > 0)
                {
                    foreach (var youKe in youKes)
                    {
                        string xb = youKe.xb;
                        if (youKe.zjlx == "1") xb = getXB(youKe.zjhm);
                        else if (youKe.zjlx == "2") xb = "";
                        else if (youKe.zjlx == "3") xb = youKe.xb;
                        else if (youKe.zjlx == "4") xb = "";
                        else if (youKe.zjlx == "0") xb = "";
                        else xb = "";

                        if (xb == guiZeJson.XingBie) shuliang++;
                    }
                }

                if (shiJiJiSuanRenShu > shuliang) shiJiJiSuanRenShu = shuliang;

                if (shuliang > 0) b9 = true;
                else b9 = false;
            }
            #endregion

            if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
            {
                shiJiYouHui = new MYouHuiInfo();
                shiJiYouHui.guize = guiZeInfo.GuiZe;
                shiJiYouHui.jine = 0;
                shiJiYouHui.miaoshu = guiZeInfo.MiaoShu;
                shiJiYouHui.mingcheng = guiZeInfo.MingCheng;

                if (guiZeInfo.FangShi == 0)
                {
                    shiJiYouHui.jine = guiZeInfo.JinE;
                }
                else if (guiZeInfo.FangShi == 1)
                {
                    shiJiYouHui.jine = guiZeInfo.JinE * shiJiJiSuanRenShu;
                }

                return shiJiYouHui;
            }

            return null;
        }

        /// <summary>
        /// 航期优惠-计算单个规则的优惠信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 规则按单计算时，所有游客都要满足游客类条件才符合规则
        /// </remarks>
        MYouHuiInfo HangQiYouHui2(EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo guiZeInfo, EyouSoft.Model.YlStructure.MHangQiRiQiInfo riQiInfo, IList<MYouKeInfo> youKes, int renShu, decimal jinE)
        {
            MYouHuiInfo shiJiYouHui = null;
            if (string.IsNullOrEmpty(guiZeInfo.GuiZe)) return null;
            if (guiZeInfo.JinE == 0) return null;
            var guiZeJson = Newtonsoft.Json.JsonConvert.DeserializeObject<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfoJSON>(guiZeInfo.GuiZe);
            if (guiZeJson == null) return null;

            bool b1 = true, b2 = true, b3 = true, b4 = true, b5 = true, b6 = true, b7 = true, b8 = true, b9 = true;

            DateTime dn = DateTime.Now;
            int shiJiJiSuanRenShu = renShu;

            #region 下单时间-提前天
            if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianTiaoJian) && !string.IsNullOrEmpty(guiZeJson.XiaDanShiJianTianShu))
            {
                var b = false;
                int cha = ShiJianChaTian(riQiInfo.RiQi, dn);

                if (guiZeJson.XiaDanShiJianTiaoJian == "1")
                {
                    if (cha > Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                else if (guiZeJson.XiaDanShiJianTiaoJian == "2")
                {
                    if (cha == Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                else if (guiZeJson.XiaDanShiJianTiaoJian == "3")
                {
                    if (cha < Utils.GetInt(guiZeJson.XiaDanShiJianTianShu)) b = true;
                }
                b1 = b;
            }
            #endregion

            #region 下单时间-指定日期
            if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianS) && !string.IsNullOrEmpty(guiZeJson.XiaDanShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianS);
                var d2 = Utils.GetDateTime(guiZeJson.XiaDanShiJianE).AddDays(1).AddSeconds(-1);
                if (dn >= d1 && dn <= d2) b = true;
                b2 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianS))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianS);
                if (dn >= d1) b = true;
                b2 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.XiaDanShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.XiaDanShiJianE).AddDays(1).AddSeconds(-1);
                if (dn <= d1) b = true;
                b2 = b;
            }
            #endregion

            #region 出港日期-指定日期
            if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianS) && !string.IsNullOrEmpty(guiZeJson.ChuGangShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianS);
                var d2 = Utils.GetDateTime(guiZeJson.ChuGangShiJianE).AddDays(1).AddSeconds(-1);
                if (riQiInfo.RiQi >= d1 && riQiInfo.RiQi <= d2) b = true;
                b3 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianS))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianS);
                if (riQiInfo.RiQi >= d1) b = true;
                b3 = b;
            }
            else if (!string.IsNullOrEmpty(guiZeJson.ChuGangShiJianE))
            {
                var b = false;
                var d1 = Utils.GetDateTime(guiZeJson.ChuGangShiJianE).AddDays(1).AddSeconds(-1);
                if (riQiInfo.RiQi <= d1) b = true;
                b3 = b;
            }
            #endregion

            #region 人数
            if (!string.IsNullOrEmpty(guiZeJson.RenShuTiaoJian) && !string.IsNullOrEmpty(guiZeJson.RenShu))
            {
                var b = false;

                if (guiZeJson.RenShuTiaoJian == "1")
                {
                    if (renShu > Utils.GetInt(guiZeJson.RenShu)) b = true;
                }
                else if (guiZeJson.RenShuTiaoJian == "2")
                {
                    if (renShu == Utils.GetInt(guiZeJson.RenShu)) b = true;
                }
                else if (guiZeJson.RenShuTiaoJian == "3")
                {
                    if (renShu < Utils.GetInt(guiZeJson.RenShu)) b = true;
                }

                b4 = b;
            }
            #endregion

            #region 游客年龄
            if (!string.IsNullOrEmpty(guiZeJson.YouKeNianLingTiaoJian) && !string.IsNullOrEmpty(guiZeJson.YouKeNianLing))
            {
                if (guiZeInfo.FangShi == 0)
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            var b = false;
                            var sr = Utils.GetDateTimeNullable(youKe.sr);
                            if (!sr.HasValue) continue;

                            int nl = ShiJianChaNian(dn, sr.Value);

                            if (guiZeJson.YouKeNianLingTiaoJian == "1")
                            {
                                if (nl > Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }
                            else if (guiZeJson.YouKeNianLingTiaoJian == "2")
                            {
                                if (nl == Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }
                            else if (guiZeJson.YouKeNianLingTiaoJian == "3")
                            {
                                if (nl < Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }

                            if (b) shuliang++;
                        }
                    }

                    if (shuliang != renShu) b5 = false;
                }
                else
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            var b = false;
                            var sr = Utils.GetDateTimeNullable(youKe.sr);
                            if (!sr.HasValue) continue;

                            int nl = ShiJianChaNian(dn, sr.Value);

                            if (guiZeJson.YouKeNianLingTiaoJian == "1")
                            {
                                if (nl > Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }
                            else if (guiZeJson.YouKeNianLingTiaoJian == "2")
                            {
                                if (nl == Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }
                            else if (guiZeJson.YouKeNianLingTiaoJian == "3")
                            {
                                if (nl < Utils.GetInt(guiZeJson.YouKeNianLing)) b = true;
                            }

                            if (b) shuliang++;
                        }
                    }

                    shiJiJiSuanRenShu = shuliang;

                    if (shuliang > 0) b5 = true;
                    else b5 = false;
                }
            }
            #endregion

            #region 游客区域
            if (!string.IsNullOrEmpty(guiZeJson.YouKeQuYu))
            {
                if (guiZeInfo.FangShi == 0)
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            if (string.IsNullOrEmpty(youKe.zjhm)) continue;
                            if (youKe.zjhm.Length < 2) continue;
                            var zjlx = (EyouSoft.Model.EnumType.TourStructure.CardType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.TourStructure.CardType), youKe.zjlx);
                            if (!zjlx.HasValue) continue;
                            if (zjlx.Value != EyouSoft.Model.EnumType.TourStructure.CardType.身份证
                                && zjlx.Value != EyouSoft.Model.EnumType.TourStructure.CardType.户口本) continue;

                            int sfid = new EyouSoft.BLL.ComStructure.BComCity().GetSFID_SFXZQHDM(youKe.zjhm.Substring(0, 2));

                            if (sfid.ToString() == guiZeJson.YouKeQuYu) shuliang++;
                        }
                    }

                    if (shuliang != renShu) b6 = false;
                }
                else
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            if (string.IsNullOrEmpty(youKe.zjhm)) continue;
                            if (youKe.zjhm.Length < 2) continue;

                            int sfid = new EyouSoft.BLL.ComStructure.BComCity().GetSFID_SFXZQHDM(youKe.zjhm.Substring(0, 2));

                            if (sfid.ToString() == guiZeJson.YouKeQuYu) shuliang++;
                        }
                    }

                    if (shiJiJiSuanRenShu > shuliang) shiJiJiSuanRenShu = shuliang;

                    if (shuliang > 0) b6 = true;
                    else b6 = false;
                }
            }
            #endregion

            #region 是否会员
            if (!string.IsNullOrEmpty(guiZeJson.ShiFouHuiYuan))
            {
                bool b = false;
                MYlHuiYuanInfo m = null;
                bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

                if (isLogin && guiZeJson.ShiFouHuiYuan == "1") b = true;

                if (!isLogin && guiZeJson.ShiFouHuiYuan == "2") b = true;

                b7 = b;
            }
            #endregion

            #region 订单金额
            if (!string.IsNullOrEmpty(guiZeJson.DingDanJinETiaoJian) && !string.IsNullOrEmpty(guiZeJson.DingDanJinE))
            {
                var b = false;

                if (guiZeJson.DingDanJinETiaoJian == "1")
                {
                    if (jinE > Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }
                else if (guiZeJson.DingDanJinETiaoJian == "2")
                {
                    if (jinE == Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }
                else if (guiZeJson.DingDanJinETiaoJian == "3")
                {
                    if (jinE < Utils.GetInt(guiZeJson.DingDanJinE)) b = true;
                }

                b8 = b;
            }
            #endregion

            #region 游客性别
            if (!string.IsNullOrEmpty(guiZeJson.XingBie))
            {
                if (guiZeInfo.FangShi == 0)
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            if (youKe.xb == guiZeJson.XingBie) shuliang++;
                        }
                    }

                    if (shuliang != renShu) b9 = false;
                }
                else
                {
                    int shuliang = 0;

                    if (youKes != null && youKes.Count > 0)
                    {
                        foreach (var youKe in youKes)
                        {
                            if (youKe.xb == guiZeJson.XingBie) shuliang++;
                        }
                    }

                    if (shiJiJiSuanRenShu > shuliang) shiJiJiSuanRenShu = shuliang;

                    if (shuliang > 0) b9 = true;
                    else b9 = false;
                }
            }
            #endregion

            if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
            {
                shiJiYouHui = new MYouHuiInfo();
                shiJiYouHui.guize = guiZeInfo.GuiZe;
                shiJiYouHui.jine = 0;
                shiJiYouHui.miaoshu = guiZeInfo.MiaoShu;
                shiJiYouHui.mingcheng = guiZeInfo.MingCheng;

                if (guiZeInfo.FangShi == 0)
                {
                    shiJiYouHui.jine = guiZeInfo.JinE;
                }
                else if (guiZeInfo.FangShi == 1)
                {
                    shiJiYouHui.jine = guiZeInfo.JinE * shiJiJiSuanRenShu;
                }

                return shiJiYouHui;
            }

            return null;
        }

        /// <summary>
        /// 时间差-天 d1-d2
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        int ShiJianChaTian(DateTime d1, DateTime d2)
        {
            TimeSpan ts1 = new TimeSpan(d1.Date.Ticks);
            TimeSpan ts2 = new TimeSpan(d2.Date.Ticks);

            TimeSpan ts = ts1 - ts2;

            return ts.Days;
        }

        /// <summary>
        /// 时间差-年 d1-d2
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        int ShiJianChaNian(DateTime d1, DateTime d2)
        {
            TimeSpan ts1 = new TimeSpan(d1.Date.Ticks);
            TimeSpan ts2 = new TimeSpan(d2.Date.Ticks);

            TimeSpan ts = ts1 - ts2;

            return (int)Math.Ceiling((double)ts.Days / (double)365);
        }

        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        void CollectProductc()
        {
            string collectID = Utils.GetQueryStringValue("cid");
            string LeiXing = Utils.GetQueryStringValue("lxid");
            MYlHuiYuanInfo m;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            if (!isLogin) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未登录不能收藏，请先登录！"));
            if (string.IsNullOrEmpty(collectID) && string.IsNullOrEmpty(LeiXing))
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));
            MHuiYuanShouCangJiaInfo model = new MHuiYuanShouCangJiaInfo()
            {
                HuiYuanId = m.HuiYuanId,
                LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)Utils.GetInt(LeiXing),
                ChanPinId = collectID
            };
            EyouSoft.BLL.YlStructure.BHuiYuan DuiHuan = new EyouSoft.BLL.YlStructure.BHuiYuan();
            int strId = DuiHuan.InsertShouCangJia(model);
            if (strId > 0)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "收藏成功！"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "收藏失败，重新操作！"));
        }

        /// <summary>
        /// 航期点评HTML
        /// </summary>
        void DingPingHtml()
        {
            int pageSize = 15;
            int pageIndex = UtilsCommons.GetPadingIndex("index");
            int recordCount = 0;
            string HangQiId = Utils.GetQueryStringValue("hangqiID");
            MWzDianPingChaXunInfo model = new MWzDianPingChaXunInfo()
            {
                HangQiId = HangQiId,
                IsShenHe = true
            };
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
            var list = bll.GetDianPings(m.CompanyId, pageSize, pageIndex, ref recordCount, model);
            int pageCount = (int)Math.Ceiling((double)recordCount / (double)pageSize);

            if (pageIndex > pageCount)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", ""));
            }
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    var HuiYuan = bll.GetHuiYuanInfo(item.OperatorId);
                    decimal xingxing = item.FenShu;
                    string xing = "";
                    for (int i = 0; i < xingxing; i++)
                    {
                        xing += "<img src=\"/images/x01.jpg\">";
                    }
                    var name = "";
                    if (item.IsNiMing)
                    {
                        name = "匿名";
                    }
                    else
                    {
                        name = HuiYuan != null ? Utils.GetText(HuiYuan.XingMing, 3) + "***" : "***";
                    }
                    sb.Append("<li>");
                    sb.AppendFormat("<h4>{0}{1}</h4>", item.BiaoTi, xing);
                    sb.AppendFormat("<p>{0}</p>", item.NeiRong);
                    sb.AppendFormat("<p class=\"user-txt\">{0}&nbsp;{1}</p>", name, item.IssueTime.ToString("yyyy-MM-dd"));
                }
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", sb.ToString()));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "没有更多了！"));
            }

        }

        /// <summary>
        /// 航期咨询问答HTML
        /// </summary>
        void ZiXunHtml()
        {
            int pageSize = 5;
            int pageIndex = UtilsCommons.GetPadingIndex("index");
            int recordCount = 0;
            string HangQiId = Utils.GetFormValue("txtHangQiId");
            string typeLeixing = Utils.GetFormValue("txtLeiXing");
            MWzWenDaChaXunInfo model = new MWzWenDaChaXunInfo()
            {
                HangQiId = HangQiId,
                IsHuiFu = true
            };
            if (!string.IsNullOrEmpty(typeLeixing) && typeLeixing != "-1")
            {
                model.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing>(typeLeixing, EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing.其他);
            }
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
            var list = bll.GetWenDas(m.CompanyId, pageSize, pageIndex, ref recordCount, model);
            int pageCount = (int)Math.Ceiling((double)recordCount / (double)pageSize);

            if (pageIndex > pageCount)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", ""));
            }
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    var HuiYuan = bll.GetHuiYuanInfo(item.WenYongHuId);
                    var name = "";
                    if (item.IsNiMing)
                    {
                        name = "匿名";
                    }
                    else
                    {
                        name = HuiYuan != null ? Utils.GetText(HuiYuan.XingMing, 3) + "****" : "****";
                    }
                    sb.Append("<li>");
                    sb.Append("<p class=\"question-txt\"><span class=\"icon_qtitle\">咨询内容：</span>");
                    sb.AppendFormat("{0}<span class=\"font_gray\">（{1}&nbsp;&nbsp;{2}）</span></p>",
                        item.WenNeiRong, name,
                        item.WenShiJian.ToString("G"));
                    sb.AppendFormat("<p class=\"answer-txt\"><span class=\"icon_atitle\">客服回复：</span>{0}</p></li>", item.DaNeiRong);

                }

                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", sb.ToString()));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "没有更多了！"));
            }
        }

        /// <summary>
        /// 航期咨询问答提交
        /// </summary>
        void WenDaTiJiao()
        {
            var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            MYlHuiYuanInfo m = null;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            var info = new EyouSoft.Model.YlStructure.MWzWenDaInfo();
            info.CompanyId = yuMingInfo.CompanyId;
            info.DaNeiRong = string.Empty;
            info.DaOperatorId = string.Empty;
            info.DaShiJian = null;
            info.HangQiId = Utils.GetFormValue("txtHangQiId");
            info.IsNiMing = false;
            info.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing>(Utils.GetFormValue("txtLeiXing"), EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing.其他);
            info.WenBiaoTi = string.Empty;
            info.WenDaId = Guid.NewGuid().ToString();
            info.WenNeiRong = Utils.GetFormValue("txtNeiRong");
            info.WenShiJian = DateTime.Now;
            info.WenYongHuId = string.Empty;
            if (isLogin) info.WenYongHuId = m.HuiYuanId;
            else
                info.IsNiMing = true;
            new EyouSoft.BLL.YlStructure.BHuiYuan().InsertWenDa(info);
            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "咨询提交成功"));
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
                    v= "1";
                }
                else
                {
                    v= "0";
                }
            }

            return v;
        }
        #endregion
    }

    #region 优惠信息实体
    /// <summary>
    /// 优惠信息实体
    /// </summary>
    public class MYouHuiInfo
    {
        public string guize { get; set; }
        public string miaoshu { get; set; }
        public string mingcheng { get; set; }
        public decimal jine { get; set; }
    }

    public class MYouHuiOutPutInfo
    {
        public string fangshi { get; set; }
        public decimal jine { get; set; }
        public IList<MYouHuiInfo> youhuis { get; set; }
    }
    #endregion
}
