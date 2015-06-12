using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Model.YlStructure;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization.Json;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Hangqi
{
    public partial class HaiYangInfo : EyouSoft.YlWeb.WzPage
    {
        /// <summary>
        /// 航期ID
        /// </summary>
        protected string HangQiId = "";

        protected string ScriptJson = "";

        protected string QishiMoney = "";

        protected EyouSoft.Model.YlStructure.MHangQiInfo FromHangqi = null;
        protected DateTime RiQi1 = DateTime.Today;

        protected void Page_Load(object sender, EventArgs e)
        {
            HangQiId = Utils.GetQueryStringValue("id");
            string type = Utils.GetQueryStringValue("type");
            if (type == "selectHtml") RCWE(SelectHtml(""));
            if (type == "CheckPage") RCWE(SelectProduct());
            if (type == "CheckBk") RCWE(DingDanFuJiaChanPinHtml());
            if (type == "sava") RCWE(Sava());
            if (type == "jifen") RCWE(KeYouJiFenCheck());
            if (type == "address") RCWE(AddressSava());
            if (type == "getdizhi") GetDiZhi();

            if (string.IsNullOrEmpty(HangQiId)) RCWE("请求异常");
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.海洋游轮横幅;
            if (!Page.IsPostBack)
            {
                InitInfo();
                ltrChangYongDiZhi.Text = GetDiZhiHtml();
            }
        }

        private void InitInfo()
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            EyouSoft.BLL.YlStructure.BHuiYuan HuiYuan = new EyouSoft.BLL.YlStructure.BHuiYuan();
            var model = bll.GetHangQiInfo(HangQiId);
            if (model == null) RCWE("请求异常");

            if (model.LeiXing != EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                Response.Redirect("/Hangqi/" + model.HangQiId+".html", true);

            FromHangqi = model;

            Master.SEOWebTitle = model.SeoTitle;
            Master.SEOKeywords = model.SeoKeyword;
            Master.SEODescription = model.SeoDescription;


            Hangqi.Value = model.HangQiId;
            txt_YL_DengChuan.Text = model.ChuFaGangKouMingCheng;
            txt_YL_XiaChuan.Text = model.DiDaGangKouMingCheng;
            txt_YL_TuJingChengShi.Text = model.TuJingChengShi;
            txt_YL_ChanPinBianHao.Text = model.BianHao;
            //txt_YL_ChanPinTeSe.Text = model.ChanPinTeSe;
            txt_YL_Jiage.Text = model.QiShiJiaGe.ToString("C2");
            QishiMoney = model.QiShiJiaGe.ToString("C2");
            txt_YL_Name.Text = "&nbsp;&nbsp;" + model.MingCheng;
            txt_YL_XingZhi.Text = model.HangXianXingZhi;
            txt_YL_Title.Text = model.ChuanZhiName + "\"" + model.XiLieName + "\"&nbsp;&nbsp;" + model.MingCheng;
            if (string.IsNullOrEmpty(model.YouHuiXinXi)) phYouHui.Visible = false;
            txt_YL_YouHui.Text = model.YouHuiXinXi;
            txt_YL_JSName.Text = model.ChuanZhiName;
            txt_YL_QianZheng.Text = model.QianZhengQianZhu;
            YouLunInfo(model.ChuanZhiId);
            //行程安排
            rptList_XingCheng.DataSource = model.XingChengs;
            rptList_XingCheng.DataBind();
            txt_YL_FeiYong.Text = model.FeiYongShuoMing;
            txt_YL_YuDingXuZhi.Text = model.YuDingXuZhi;
            txt_YL_YouQing.Text = model.YouQingTiShi;
            txt_YL_YouLunGongLue.Text = model.GongLue;
            string HangXian = "";
            txt_YL_XingChengTianShu.Text = model.TianShu1.ToString() + "天" + model.TianShu2.ToString() + "晚";
            FuJiaChanPinInfo(model.FuJiaChanPins);
            #region 航期的日期
            IList<MHangQiRiQiInfo> RiQis = bll.GetHangQiRiQis(HangQiId, DateTime.Now.AddDays(-1), null, true);
            HangXianJson(RiQis);

            if (RiQis != null && RiQis.Count > 0)
            {
                var riqiid = Utils.GetQueryStringValue("riqiid");
                var riqis = new List<MHangQiRiQiInfo>();
                if (!string.IsNullOrEmpty(riqiid))
                {
                    riqis = RiQis.Where(m => m.RiQiId == riqiid).ToList();
                }
                if (riqis != null && riqis.Count > 0)
                    RiQi1 = riqis[0].RiQi;
                else
                    RiQi1 = RiQis[0].RiQi;
            }
            #endregion
            InsertHangQJiLu();
            AddressFree.Value = model.FaPiaoKuaiDiJinE.ToString();

            #region 用户点评

            decimal FenShu = HuiYuan.GetDianPingJunFen(HangQiId);
            int recordCount = 0;
            MWzDianPingChaXunInfo DianPingChaXun = new MWzDianPingChaXunInfo()
            {
                HangQiId = HangQiId,
                IsShenHe = true
            };
            var DianPinglist = HuiYuan.GetDianPings(YuMingInfo.CompanyId, 15, 1, ref recordCount, DianPingChaXun);
            if (DianPinglist != null && DianPinglist.Count > 0)
            {
                rptListDianPing.DataSource = DianPinglist;
                rptListDianPing.DataBind();
            }
            string DianPingHtml = "";
            if (FenShu > 0)
            {
                DianPingHtml += "<em>" + FenShu.ToString("F2") + "</em>分";
            }
            else
            {
                DianPingHtml += "<em>0</em>分";
            }
            if (recordCount > 0)
            {
                DianPingHtml += "（共" + recordCount + "人点评）";
            }
            else
            {
                DianPingHtml += "（暂无点评）";
            }
            litYongHuDingPing.Text = DianPingHtml;
            #endregion

            #region 咨询问答
            int Count = 0;
            MWzWenDaChaXunInfo WenDaChaxun = new MWzWenDaChaXunInfo()
            {
                HangQiId = HangQiId,
                IsHuiFu = true
            };
            var WendaList = HuiYuan.GetWenDas(YuMingInfo.CompanyId, 5, 1, ref Count, WenDaChaxun);
            if (WendaList != null && WendaList.Count > 0)
            {
                rptListZiXunWD.DataSource = WendaList;
                rptListZiXunWD.DataBind();
            }

            #endregion

            var httk = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮合同条款);
            if (httk != null && !string.IsNullOrEmpty(httk.V))
            {
                ltrHeTongTiaoKuan.Text = httk.V;
            }
        }
        /// <summary>
        /// 会员名称处理
        /// </summary>
        /// <param name="HuiYuanID"></param>
        /// <param name="IsNiMing"></param>
        /// <returns></returns>
        protected string HuiYuanMingCheng(object HuiYuanID, object IsNiMing)
        {
            if ((bool)IsNiMing)
            {
                return "匿名";
            }
            else
            {
                EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
                var HuiYuan = bll.GetHuiYuanInfo(HuiYuanID.ToString());
                var name = HuiYuan != null ? Utils.GetText(HuiYuan.XingMing, 3) + "****" : "***";
                return name;
            }
        }

        /// <summary>
        /// 咨询问答类型
        /// </summary>
        /// <returns></returns>
        protected string ZiXunLeiXing()
        {
            StringBuilder sb = new StringBuilder();
            var list = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing));
            sb.Append("<a href=\"javascript:;\" data-lx=\"-1\" class=\"select\">全部</a>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sb.AppendFormat("<a href=\"javascript:;\" data-lx=\"{0}\" class=\"\">{1}</a>", list[i].Value, list[i].Text);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 点评分数
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string DianPingFenShu(object values)
        {
            StringBuilder sb = new StringBuilder();
            string name = values.ToString();
            decimal Number = Utils.GetDecimal(values.ToString());
            if (Number > 0)
            {
                for (int i = 0; i < Number; i++)
                {
                    sb.Append("<img src=\"/images/x01.jpg\">");
                }
            }
            return sb.ToString();
        }

        #region 私有方法

        #region 游轮信息
        /// <summary>
        /// 游轮信息
        /// </summary>
        private void YouLunInfo(string ChuanZhiId)
        {
            if (!string.IsNullOrEmpty(ChuanZhiId))
            {
                EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
                var model = bll.GetChuanZhiInfo(ChuanZhiId);
                if (model != null)
                {
                    txt_YL_ChanZhiMingCheng.Text = model.MingCheng;
                    txt_YL_GongSi.Text = model.GongSiMingCheng; //XingJiShow(model.XingJi);
                    if (model.FuJians != null && model.FuJians.Count > 0)
                    {
                        txt_YL_JieShaoImage.Text = "<img width=\"466\" height=\"353\" src=\"" + TuPian.F1(ErpFilepath + model.FuJians[0].Filepath, 466, 353) + "\">";

                    }
                    txt_YL_JScontent.Text = Utils.GetText(model.JianYaoJieShao, 250);
                    txt_YL_FangXing.Text = FangXing(model.FangXings);
                    txt_YL_MeiShi.Text = MeiShi(model.MeiShis);
                    txt_YL_SheShi.Text = SheShi(model.SheShis);
                    txt_YL_PingMian.Text = PingMian(model.PingMianTus);
                    txt_YL_ShiPin.Text = ShiPing(model.ChuanZhiId);
                }
            }
        }
        /// <summary>
        /// 星级设置
        /// </summary>
        /// <param name="XingJi"></param>
        /// <returns></returns>
        private string XingJiShow(EyouSoft.Model.EnumType.YlStructure.XingJi XingJi)
        {
            string xj = "";
            switch (XingJi)
            {
                case EyouSoft.Model.EnumType.YlStructure.XingJi.三星:
                    xj = "★★★";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.XingJi.四星:
                    xj = "★★★★";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.XingJi.准五星:
                    xj = "★★★★☆";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.XingJi.五星:
                    xj = "★★★★★";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.XingJi.超五星:
                    xj = "★★★★★★";
                    break;
                default:
                    break;
            }
            return xj;
        }
        /// <summary>
        /// 房形介绍
        /// </summary>
        /// <param name="Fangxing"></param>
        /// <returns></returns>
        private string FangXing(IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo> Fangxing)
        {
            StringBuilder sb = new StringBuilder();
            if (Fangxing != null && Fangxing.Count > 0)
            {
                for (int i = 0; i < Fangxing.Count; i++)
                {
                    if (i == 6) break;
                    sb.AppendFormat("<li><a href=\"{2}\" target=\"_blank\"><img src=\"{0}\"><span class=\"name\">{1}</span></a></li>",
                        TuPian.F1(ErpFilepath + Fangxing[i].Filepath,205,137), Fangxing[i].MingCheng, "/Youlun/ChuanZhi.aspx?id=" + FromHangqi.ChuanZhiId + "#fangxing");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 美食介绍
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string MeiShi(IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 3) break;
                    sb.AppendFormat("<li><a href=\"{0}\" target=\"_blank\"><img src=\"{1}\"><span class=\"name\">{2}</span><span class=\"title\">{3}</span></a></li>",
                        "/Youlun/ChuanZhi.aspx?id=" + FromHangqi.ChuanZhiId + "#meishi", TuPian.F1(ErpFilepath + list[i].Filepath, 217, 157), list[i].MingCheng, list[i].MiaoShu);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 娱乐设施
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string SheShi(IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 3) break;
                    sb.AppendFormat("<li><a href=\"{0}\" target=\"_blank\"><img src=\"{1}\"><span class=\"name\">{2}</span><span class=\"title\">{3}</span></a></li>"
                        , "/Youlun/ChuanZhi.aspx?id=" + FromHangqi.ChuanZhiId + "#sheshi", TuPian.F1(ErpFilepath + list[i].Filepath, 217, 157), list[i].MiaoShu, list[i].MiaoShu);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 平面图
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string PingMian(IList<MFuJianInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sb.AppendFormat("<img src=\"{0}\">", TuPian.F1(ErpFilepath + list[0].Filepath, 674, 284));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 视频
        /// </summary>
        /// <param name="GongSiMingCheng"></param>
        /// <returns></returns>
        private string ShiPing(string ChuanZhiID)
        {
            StringBuilder sb = new StringBuilder();
            bool Append = true;
            if (!string.IsNullOrEmpty(ChuanZhiID))
            {
                int recordCount = 0;
                EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
                EyouSoft.Model.YlStructure.MShiPinChaXunInfo model = new MShiPinChaXunInfo()
                {
                    ChuanZhiId = ChuanZhiID
                };
                var list = bll.GetShiPins(YuMingInfo.CompanyId, 5, 1, ref recordCount, model);
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == 0)
                        {
                            sb.AppendFormat("<div class=\"video_big\"><a target=\"_blank\" href=\"/Youlun/ChuanZhi.aspx?id={0}#youlun\"><img src=\"{1}\"></a></div>", list[i].GongSiId, ErpFilepath + list[i].ShiPinIMG);
                        }
                        else
                        {
                            if (Append)
                            {
                                sb.Append("<div class=\"video_small\"><ul>");
                            }
                            sb.AppendFormat("<li><a target=\"_blank\" href=\"/Youlun/ChuanZhi.aspx?id={0}#youlun\"><img src=\"{1}\"></a></li>", list[i].GongSiId, ErpFilepath + list[i].ShiPinIMG);
                            if (Append)
                            {
                                sb.Append("</ul><div class=\"clear\"></div></div>");
                            }
                            Append = false;
                        }
                    }

                }
            }
            return sb.ToString();
        }
        #endregion
        /// <summary>
        /// 航期的日期
        /// </summary>
        /// <param name="RiQis"></param>
        private void HangXianJson(IList<MHangQiRiQiInfo> RiQis)
        {
            IsoDateTimeConverter isDate = new IsoDateTimeConverter();
            isDate.DateTimeFormat = "yyyy-MM-dd";
            if (RiQis != null && RiQis.Count > 0)
            {
                string scripts = string.Format("var Tour={0};", Newtonsoft.Json.JsonConvert.SerializeObject(RiQis.Where(n => n.RiQi >= DateTime.Now.AddDays(-1)), isDate));
                ScriptJson = scripts;
            }
        }

        /// <summary>
        /// 行程图片
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string XingChengImage(object obj)
        {
            string str = "";
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                str = string.Format("<img src=\"{0}\">", TuPian.F1(ErpFilepath + obj.ToString(), 217, 149));
            }
            return str;
        }
        /// <summary>
        /// 写入航期浏览记录信息
        /// </summary>
        private void InsertHangQJiLu()
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            EyouSoft.Model.YlStructure.MHangQiLiuLanJiLuInfo info = new MHangQiLiuLanJiLuInfo()
            {
                HangQiId = Utils.GetQueryStringValue("id")
            };
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo m = null;
            bool isLogin = false;
            isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            if (isLogin)
            {
                info.YongHuId = m.HuiYuanId;
            }
            bll.InsertHangQiLiuLanJiLu(info);

        }

        /// <summary>
        /// 获取房型HTML
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string SelectHtml(string dateTime)
        {
            string html = "";
            string hangqiId = Utils.GetQueryStringValue("id");
            string date = "";
            if (string.IsNullOrEmpty(dateTime))
            {
                date = Utils.GetQueryStringValue("date");
            }
            else
            {
                date = dateTime;
            }
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            var list = bll.GetHYJiaGes(hangqiId, date);
            StringBuilder sb = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                sb.Append("<div class=\"fx_Content\" data-id='fx'><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                sb.Append("<tbody><tr><th width=\"124\" valign=\"middle\" align=\"center\">房型</th><th width=\"88\" valign=\"middle\" align=\"center\">最低容纳人数</th>");
                sb.Append("<th>楼层</th><th>报名人数</th><th>价格说明</th></tr>");
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr data-fx='1' class='i_jiage' i_rnrs='{0}' i_fx='{1}' i_fc='{2}' i_lc='{3}'>", item.RongNaRenShu, item.FangXingId, item.FangCha, item.LouCeng);
                    sb.AppendFormat("<td width=\"124\" valign=\"middle\" align=\"center\">{0}</td>", JiChuXinXi(item.FangXingId));
                    sb.AppendFormat("<td width=\"74\" valign=\"middle\" align=\"center\">{0}人</td>", item.RongNaRenShu);
                    sb.AppendFormat("<td align=\"center\">{0}层</td>", item.LouCeng);
                    sb.Append("<td align=\"left\" class=\"padd10 font_gray\"><ul class=\"numbox\">");
                    foreach (var itemJiaGe in item.JiaGes)
                    {
                        sb.Append("<li>");
                        sb.AppendFormat("{0}<b class=\"font14 font_yellow\">{1}</b>/人", JiChuXinXi(itemJiaGe.BinKeLeiXingId), itemJiaGe.JiaGe.ToString("C2"));
                        sb.AppendFormat("<span><a class=\"jian\" data-m=\"{0}\" href=\"javascript:;\"><img src=\"/images/num_l.jpg\"></a>", itemJiaGe.JiaGe.ToString());
                        sb.AppendFormat("<input type=\"text\" data-fx=\"{0}\" data-rn=\"{1}\" data-m=\"{2}\" data-bk=\"{3}\" data-fc=\"{4}\" value=\"0\" name=\"n4Tab2_Content_1\" readonly='readonly'>",
                            item.FangXingId, item.RongNaRenShu, itemJiaGe.JiaGe.ToString(), itemJiaGe.BinKeLeiXingId, item.FangCha.ToString());
                        sb.AppendFormat("<a class=\"jia\" href=\"javascript:;\" data-m=\"{0}\"><img src=\"/images/num_r.jpg\"></a></span>人</li>", itemJiaGe.JiaGe.ToString());
                    }
                    sb.AppendFormat("</ul><td class=\"padd10 font_gray\">{0}</td>", item.ShuoMing);
                    sb.Append("</tr>");
                }

                if (list.Count > 3)
                {
                    sb.Append("<tr><td valign='middle' align='left' colspan='5'><div class='guoji_more'><a href='javascript:void(0)' data-fs='+' data-id='fangxingmore'><font style='color:#cc3939;'>+</font> 点击查看更多房型</a></div></td></tr>");
                }

                sb.Append("</tbody></table></div>");
                html += sb.ToString();
            }
            else { html += "<liclass=\"normal\">暂无数据</li></ul></div>"; }
            return UtilsCommons.AjaxReturnJson("1", "加载成功！", html);
        }
        /// <summary>
        /// 基础信息
        /// </summary>
        /// <returns></returns>
        protected string JiChuXinXi(int id)
        {
            EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
            var list = bll.GetJiChuXinXiInfo(id);
            if (list != null)
                return list.MingCheng;
            else
                return "";

        }
        /// <summary>
        /// 去除重复数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type">1：房形|2：国家</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> removeDuplicate(IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> list, int type)
        {

            List<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> strlist = null;
            if (list != null && list.Count > 0)
            {
                strlist = new List<MHangQiJiaGeInfo>();
                foreach (var item in list)
                {
                    strlist.Add(item);
                }
                for (int i = 0; i < strlist.Count - 1; i++)
                {
                    for (int j = strlist.Count - 1; j > i; j--)
                    {
                        if (type == 1)
                        {
                            if (strlist[i].FangXingId == strlist[j].FangXingId)
                            {
                                strlist.Remove(strlist[j]);
                            }
                        }
                        else if (type == 2)
                        {
                            if (strlist[i].GuoJiId == strlist[j].GuoJiId)
                            {
                                strlist.Remove(strlist[j]);
                            }
                        }
                    }
                }
            }

            return strlist;
        }
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        private string GetDprList(EyouSoft.Model.YlStructure.MHangQiInfo m, ref string HangXian)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<option value=\"{0}\">{1}</option>", m.HangQiId, JiChuXinXi(m.ChuFaGangKouId) + "→" + JiChuXinXi(m.DiDaGangKouId) + "(" + m.TianShu1 + "天)");
            HangXian += JiChuXinXi(m.ChuFaGangKouId) + "→" + JiChuXinXi(m.DiDaGangKouId) + "(" + m.TianShu1 + "天)" + "&nbsp;&nbsp;";
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            var list = bll.GetGuanLianHangQis(m.HangQiId);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var model = bll.GetHangQiInfo(list[i].HangQiId);
                    if (model != null)
                    {
                        sb.AppendFormat("<option value=\"{0}\">{1}</option>", model.HangQiId, JiChuXinXi(model.ChuFaGangKouId) + "→" + JiChuXinXi(model.DiDaGangKouId) + "(" + model.TianShu1 + "天)");
                        HangXian += JiChuXinXi(model.ChuFaGangKouId) + "→" + JiChuXinXi(model.DiDaGangKouId) + "(" + model.TianShu1 + "天)" + "&nbsp;&nbsp;";
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 提交订单
        private string SelectProduct()
        {
            MrHaiYangList obj = null;
            string str = ""; string strid = "0";
            string json = Utils.GetFormValue("txt");
            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            if (string.IsNullOrEmpty(json))
            {
                str = "-请选择游客人数！"; strid = "0";
                return UtilsCommons.AjaxReturnJson(strid, str);
            }
            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<MrHaiYangList>(json);

            Page1Info(info, ref obj);
            if (obj != null && obj.DingdanRenShu > 0)
            {
                obj.KeYongJiFen = 0;
                if (isLogin)
                {
                    var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.HuiYuanId);
                    if (huiYuanInfo != null) obj.KeYongJiFen = huiYuanInfo.KeYongJiFen;
                }
                str = "成功"; strid = "1";
            }
            else
            {
                str = "-请选择游客人数！"; strid = "0";
                return UtilsCommons.AjaxReturnJson(strid, str);
            }

            return UtilsCommons.AjaxReturnJson(strid, str, obj);

        }

        private void Page1Info(MrHaiYangList model, ref MrHaiYangList obj)
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            FromHangqi = bll.GetHangQiInfo(model.HangQiId);
            DateTime Data = DateTime.Now;
            StringBuilder dingdan = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            int Sum = 0;
            string BkHtml = "";
            List<MrHaiYangFangXIng> list = new List<MrHaiYangFangXIng>();
            MrHaiYangFangXIng FangXing = null;
            int bkint = 1;
            if (model.FangXing != null && model.FangXing.Count > 0)
            {
                foreach (var item in model.FangXing)
                {
                    int _renShu = 0;

                    if (item.JiaGes == null || item.JiaGes.Count == 0) continue;
                    foreach (var item1 in item.JiaGes)
                    {
                        _renShu += item1.RenShu;
                    }
                    if (_renShu == 0) continue;


                    string louCeng = string.Empty;
                    //if (!string.IsNullOrEmpty(item.LouCeng)) louCeng = "楼层：" + item.LouCeng;

                    sb.AppendFormat("<dl><dt>【{0}】{1}</dt>", JiChuXinXi(item.FangXingId), louCeng);

                    foreach (var item1 in item.JiaGes)
                    {
                        if (item1.RenShu > 0)
                        {
                            FangXing = new MrHaiYangFangXIng();
                            FangXing.FangCha = item.FangCha;
                            FangXing.FangXingId = item.FangXingId;
                            FangXing.JiaGes = item.JiaGes;
                            FangXing.LouCeng = item.LouCeng;
                            FangXing.RongNaRenShu = item.RongNaRenShu;
                            list.Add(FangXing);

                            Sum += item1.RenShu;
                            sb.AppendFormat("<dd>{0}{1}人 共计：{2}元</dd>", JiChuXinXi(item1.BinKeLeiXingId), item1.RenShu, (item1.JiaGe * item1.RenShu).ToString("F2"));
                            BkHtml += BingKeHtml(item1, ref bkint);
                        }
                    }

                    if (_renShu > 0 && item.RongNaRenShu > 0)
                    {
                        int _m = _renShu % item.RongNaRenShu;

                        decimal fangChaJinE = 0;
                        if (_m > 0)
                        {
                            int buFangChaRenShu = item.RongNaRenShu - _m;
                            fangChaJinE = buFangChaRenShu * item.FangCha;
                        }

                        if (fangChaJinE > 0)
                        {
                            sb.AppendFormat("<dd>补房差：{0:F2}元</dd><dd></dd>", fangChaJinE);
                        }
                    }

                    sb.Append("</dl>");
                }
            }

            string shengYu = "<9张";
            var riQiInfo = new EyouSoft.BLL.YlStructure.BHangQi().GetRiQiInfo(model.RiQiId);
            if (riQiInfo != null)
            {
                int shengYuRenShu = riQiInfo.RenShu - riQiInfo.YouXiaoDingDanRenShu;

                if (shengYuRenShu > 9) shengYu = ">9张";
            }

            dingdan.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");
            dingdan.Append("<tbody><tr><th>出发日期</th><th>名称</th><th>项目详情</th><th>金额</th><th>下单时间</th>");
            //dingdan.Append("<th>操作</th>");
            dingdan.Append("</tr><tr>");
            dingdan.AppendFormat("<td align=\"center\" class=\"font14\">{0}</td>", model.ChuFaTime);
            dingdan.AppendFormat("<td valign=\"middle\" class=\"font14 padd20\">{0}<a class=\"fontgreen font12\" target=\"_blank\" href=\"haiyanginfo.aspx?id={1}\">【详情】</a></td>", FromHangqi.MingCheng, FromHangqi.HangQiId);
            dingdan.AppendFormat("<td valign=\"top\" class=\"font12\">{0}</td>", sb.ToString());
            dingdan.AppendFormat("<td valign=\"middle\" align=\"center\"><b class=\"font20 fontred\">{0}</b><br></td>", (model.DingdanFangXingJinE).ToString("C2"));
            dingdan.AppendFormat("<td valign=\"middle\" align=\"center\">{0}<br>{1}</td>", Data.ToString("yyyy-MM-dd"), Data.ToString("t"), shengYu);
            //dingdan.Append("<td valign=\"middle\" align=\"center\" class=\"right\"><a class=\"del_btn\" href=\"#\">删除</a></td>");
            dingdan.Append("</tr></tbody></table>");

            #region Page2
            StringBuilder sb2 = new StringBuilder();
            sb2.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");
            sb2.Append("<tbody><tr><th width=\"35%\">名称</th> <th width=\"15%\">出发日期</th><th width=\"35%\">项目详情</th>");
            sb2.Append("<th class=\"right\">金额</th></tr><tr>");
            sb2.AppendFormat("<td class=\"font14 padd20\">{0}<a class=\"fontgreen font12\"  target=\"_blank\" href=\"haiyanginfo.aspx?id={1}\">【详情】</a></td>", FromHangqi.MingCheng, FromHangqi.HangQiId);
            sb2.AppendFormat("<td valign=\"middle\" align=\"center\" class=\"font14\">{0}</td>", model.ChuFaTime);
            sb2.AppendFormat("<td valign=\"top\" class=\"font12\">{0} </td>", sb.ToString());
            sb2.AppendFormat("<td valign=\"middle\" align=\"center\" class=\"right\"><b class=\"font20 fontred\">{0}</b></td></tr></tbody></table>", (model.DingdanFangXingJinE).ToString("C2"));

            #endregion


            if (Sum > 0)
            {
                obj = new MrHaiYangList();
                obj.DingdanFangXingJinE = model.DingdanFangXingJinE;
                obj.html = dingdan.ToString();
                obj.DingdanRenShu = Sum;
                obj.DingdanRenjun = (obj.DingdanFangXingJinE / obj.DingdanRenShu).ToString();
                obj.html3 = BkHtml;
                obj.html2 = sb2.ToString();
                obj.DingdanRenShu = Sum;
                obj.HangQiId = model.HangQiId;
                obj.RiQiId = model.RiQiId;
                obj.FangXing = list;
            }

        }

        private void FuJiaChanPinInfo(IList<MHangQiFuJiaChanPinInfo> FuJiaChanPins)
        {
            var newFuJiaChanPin = removeFuJiaChanPin(FuJiaChanPins, 1);
            rptList_FuJian.DataSource = newFuJiaChanPin;
            rptList_FuJian.DataBind();

        }

        protected void InitFuJiaChanPinDetail(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptDetailList = (Repeater)e.Item.FindControl("rptList_FujiaDetail");
                MHangQiFuJiaChanPinInfo model = (MHangQiFuJiaChanPinInfo)e.Item.DataItem;
                var list = FromHangqi.FuJiaChanPins.Where(n => n.LeiXingId == model.LeiXingId);
                rptDetailList.DataSource = list;
                rptDetailList.DataBind();
            }
        }

        /// <summary>
        /// 去除重复数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<MrHaiYangJiaGe> removelist(List<MrHaiYangJiaGe> list, int type)
        {

            List<MrHaiYangJiaGe> strlist = null;
            if (list != null && list.Count > 0)
            {
                strlist = new List<MrHaiYangJiaGe>();
                foreach (var item in list)
                {
                    strlist.Add(item);
                }
                for (int i = 0; i < strlist.Count - 1; i++)
                {
                    for (int j = strlist.Count - 1; j > i; j--)
                    {
                        if (type == 1)
                        {

                        }
                        else if (type == 2)
                        {
                            if (strlist[i].BinKeLeiXingId == strlist[j].BinKeLeiXingId)
                            {
                                strlist.Remove(strlist[j]);
                            }
                        }
                    }
                }
            }

            return strlist;
        }


        private List<MHangQiFuJiaChanPinInfo> removeFuJiaChanPin(IList<MHangQiFuJiaChanPinInfo> list, int type)
        {
            List<MHangQiFuJiaChanPinInfo> strlist = null;
            if (list != null && list.Count > 0)
            {
                strlist = new List<MHangQiFuJiaChanPinInfo>();
                foreach (var item in list)
                {
                    strlist.Add(item);
                }
                for (int i = 0; i < strlist.Count - 1; i++)
                {
                    for (int j = strlist.Count - 1; j > i; j--)
                    {
                        if (type == 1)
                        {
                            if (strlist[i].LeiXingId == strlist[j].LeiXingId)
                            {
                                strlist.Remove(strlist[j]);
                            }
                        }
                    }
                }
            }

            return strlist;
        }

        /// <summary>
        /// 宾客列表HTML
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bkint"></param>
        /// <returns></returns>
        private string BingKeHtml(MrHaiYangJiaGe model, ref int bkint)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < model.RenShu; i++)
            {
                sb.Append("<div class=\"lvke_box\"><div class=\"L_jiao\"></div><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                sb.AppendFormat("<tbody><tr><td valign=\"middle\" align=\"center\" class=\"leftT\"><h3>旅客{0}</h3><span class=\"chenren\">{1}</span><input id=\"bktype\" name=\"bktype\" runat=\"server\" value=\"{2}\" type=\"hidden\" /></td>", bkint, JiChuXinXi(model.BinKeLeiXingId), model.BinKeLeiXingId);
                sb.Append("<td><div class=\"lvke_Rbox fixed\"><ul class=\"lvke_form\">");
                sb.Append("<li><label><font class=\"font_star\">*</font> 姓名：</label><span><input type=\"text\"  id=\"bkname\" name=\"bkname\" value=\"\" class=\"formsize370 inputbk\" valid=\"required\" errmsg=\"请填写游客姓名！\"></span><span class=\"error\" data-class='yktxsm'>填写说明</span></li>");
                sb.Append("<li><label><font class=\"font_star\">*</font> 证件类型：</label><div style='float:left'>");
                //sb.AppendFormat("<select name=\"sel_zjtype\" class=\"zjtype select_style_1\">{0}</select></div><div style='float:left; margin-left:5px;'><input type=\"text\" errmsg=\"请正确填写游客身份证！\" name=\"bkzj\" id=\"bkzj\" value=\"\" class=\"formsize270 inputbk\"></div><div style='clear:both'></div></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType), new string[] { "0" }), "", "", "请选择"));
                sb.AppendFormat("<select name=\"sel_zjtype\" class=\"select_style_1\">{0}</select></div><div style='float:left; margin-left:5px;' data-class='zjhm'><input type=\"text\" errmsg=\"请填写游客身份证！|请正确填写游客身份证！\" name=\"bkzj\" id=\"bkzj\" value=\"请输入证件号码\" class=\"formsize270 inputbk\" style='color:#999'></div><div style='float:left; margin-left:5px;' data-class='sr01'><input type=\"text\" name=\"sr01\" value=\"出生年份\" class=\" formsize100 inputbk\" style='color:#999;margin-right:5px;'><input type=\"text\" name=\"sr02\" value=\"出生月份\" class=\" formsize80 inputbk\" style='color:#999;margin-right:5px;'><input type=\"text\" name=\"sr03\" value=\"出生日期\" class=\" formsize80 inputbk\" style='color:#999;margin-right:5px;'></div><div style='clear:both'></div></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing))));
                //sb.Append("<li><label>证件有效期：</label><span><input type=\"text\" onfocus=\"WdatePicker()\" id=\"bkyxq\" name=\"bkyxq\" value=\"\" class=\" formsize100 inputbk\"></span></li>");
                sb.Append("<li data-class='zjyxq'><label>证件有效期：</label><span><input type=\"text\" name=\"yxq1\" value=\"年份yyyy\" class=\" formsize100 inputbk\"><input type=\"text\" name=\"yxq2\" value=\"月份mm\" class=\" formsize80 inputbk\"><input type=\"text\" name=\"yxq3\" value=\"日期dd\" class=\" formsize80 inputbk\"></span></li>");
                sb.AppendFormat("<li data-class='xb'><label>性别：</label><span><dl class=\"select_style\"><select name=\"select_Sex\" class='select_style_1'>{0}</select></dl></span></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), new string[] { "2" }), "", "", "请选择"));
                //sb.Append("<li><label>出生日期：</label><span><input type=\"text\" onfocus=\"WdatePicker()\" name=\"bkbrithday\" id=\"bkbrithday\" value=\"\" class=\" formsize100 inputbk\"></span></li>");
                sb.Append("<li data-class='sr'><label>出生日期：</label><span><input type=\"text\" name=\"sr1\" value=\"出生年份\" class=\" formsize100 inputbk\"><input type=\"text\" name=\"sr2\" value=\"出生月份\" class=\" formsize80 inputbk\"><input type=\"text\" name=\"sr3\" value=\"出生日期\" class=\" formsize80 inputbk\"></span></li>");
                sb.Append("<li><label>手机号码：</label><span><input id=\"bkphone\" errmsg=\"请正确填写游客手机号码!\" name=\"bkphone\" type=\"text\" value=\"\" class=\"formsize370 inputbk\"></span></li>");
                sb.Append("</ul>");
                sb.Append("<div class=\"lvke_caozuo\"><label><input type=\"checkbox\" id=\"\" class=\"savacontact\" value=\"1\"  name=\"ischeck\" checked='checked'> 保存到常用姓名</label> <input id=\"hd_Ischeck\" name=\"hd_Ischeck\" value=\"1\" type=\"hidden\"/> <a class=\"clearInput\" href=\"javascript:;\">清空</a></div>");
                sb.Append("</div></td></tr></tbody></table></div>");
                bkint++;
            }
            return sb.ToString();

        }

        private string DingDanFuJiaChanPinHtml()
        {
            string strid = " 0"; string str = "";

            string json = Utils.GetFormValue("jsonHtml");
            StringBuilder sb = new StringBuilder();
            List<MHangQiDingDanFuJiaChanPinInfo> dingdanlist = null;
            if (!string.IsNullOrEmpty(json))
            {
                List<MHangQiFuJiaChanPinInfo> list = new List<MHangQiFuJiaChanPinInfo>();
                dingdanlist = new List<MHangQiDingDanFuJiaChanPinInfo>();
                JsonObject jsonObj = (JsonObject)JsonConvert.Import(json);
                JsonArray jagroup = (JsonArray)jsonObj["options"];
                MHangQiFuJiaChanPinInfo fujian = null;
                MHangQiDingDanFuJiaChanPinInfo dingdan = null;
                foreach (JsonObject item in jagroup)
                {
                    fujian = new MHangQiFuJiaChanPinInfo();
                    fujian.FuJiaChanPinId = item["fj"].ToString();
                    fujian.LeiXingId = Utils.GetInt(item["lx"].ToString());
                    if (Utils.GetInt(item["sl"].ToString()) > 0)
                    {
                        dingdan = new MHangQiDingDanFuJiaChanPinInfo();
                        dingdan.FuJiaChanPinId = item["fj"].ToString();
                        dingdan.DanJia = Utils.GetDecimal(item["dj"].ToString());
                        dingdan.ShuLiang = Utils.GetInt(item["sl"].ToString());
                        dingdan.JinE = Utils.GetDecimal(item["jn"].ToString());
                        dingdanlist.Add(dingdan);
                        list.Add(fujian);
                    }
                }
                if (list != null && list.Count > 0)
                {
                    EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
                    var fujia = bll.GetHangQiInfo(jsonObj["hangqiid"].ToString()).FuJiaChanPins;

                    var newlist = removeFuJiaChanPin(list, 1);

                    sb.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" class=\"borderB\">");
                    sb.Append("<tbody><tr><th width=\"35%\" height=\"100\">类型</th><th width=\"15%\">项目名称");
                    sb.Append("</th><th width=\"35%\">项目详情</th><th class=\"right\">金额</th></tr>");
                    StringBuilder tr = new StringBuilder();
                    for (int i = 0; i < newlist.Count; i++)
                    {
                        var xl = list.Where(n => n.LeiXingId == newlist[i].LeiXingId).ToList();
                        for (int x = 0; x < xl.Count; x++)
                        {
                            var fujiantr = fujia.Where(n => n.FuJiaChanPinId == xl[x].FuJiaChanPinId).ToList();
                            tr.Append("<tr>");
                            if (x == 0)
                            {
                                tr.AppendFormat("<td valign=\"middle\" align=\"center\" rowspan=\"{0}\" class=\"font14\">", xl.Count);
                                tr.AppendFormat("{0}</td>", JiChuXinXi(newlist[i].LeiXingId));
                            }
                            tr.AppendFormat("<td align=\"center\">{0}</td>", fujiantr[0].XiangMu);
                            tr.AppendFormat("<td><font class=\"fontgreen padd10\">{0}</font></td>", fujiantr[0].JieShao);
                            tr.AppendFormat("<td align=\"center\" class=\"right\"><b class=\"font16 fontred\">{0}</b></td></tr>"
                                , dingdanlist.Where(n => n.FuJiaChanPinId == xl[x].FuJiaChanPinId).ToList()[0].JinE.ToString("C2"));
                        }

                        sb.Append(tr.ToString());

                        tr = new StringBuilder();
                    }
                    sb.Append("</tbody></table>");

                }
            }

            MrHangQiList obj = new MrHangQiList();
            //if (!string.IsNullOrEmpty(sb.ToString()))
            //{
            strid = "1"; str = "成功";
            obj.FuJiaChanPin = dingdanlist;
            obj.html4 = sb.ToString();
            //}
            return UtilsCommons.AjaxReturnJson(strid, str, obj != null ? obj : null);
        }

        private string KeYouJiFenCheck()
        {
            decimal value = Utils.GetDecimal(Utils.GetFormValue("txtDingDanJiFen"));
            string HangQiid = Utils.GetFormValue("txtHangQiid");
            string str = "积分计算失败！"; int strid = 0;
            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            var HangQiInfo = bll.GetHangQiInfo(HangQiid);

            if (!isLogin)
            {
                return UtilsCommons.AjaxReturnJson("0", "你没有积分可以抵扣");
            }

            decimal keYongJiFen = 0;
            var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.HuiYuanId);
            if (huiYuanInfo != null) keYongJiFen = huiYuanInfo.KeYongJiFen;

            if (value > keYongJiFen)
            {
                str = "-积分超出了可用积分！";
            }
            else
            {
                if (value > HangQiInfo.KeDiKouJinFen)
                {
                    str = "-积分超出了最多可抵用积分！";
                }
                else
                {
                    strid = 1;
                    str = (value * HangQiInfo.JiFenDuiHuanBiLi).ToString();
                }
            }

            return UtilsCommons.AjaxReturnJson(strid.ToString(), str);
        }
        #endregion

        #region 订单
        private string Sava()
        {
            string str = "";
            bool isLogin = false;
            MYlHuiYuanInfo m = null;
            isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            decimal keYongJiFen = 0;

            string xiaDanRenId = string.Empty;
            string feiHuiYuanId = string.Empty;
            if (isLogin)
            {
                xiaDanRenId = m.HuiYuanId;
                var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.HuiYuanId);
                if (huiYuanInfo != null) keYongJiFen = huiYuanInfo.KeYongJiFen;
            }
            else
            {
                var feiHuiYuanInfo = GetFeiHuiYuanInfo();
                xiaDanRenId = feiHuiYuanInfo.id;
                feiHuiYuanId = feiHuiYuanInfo.id;
            }

            string valuejson = Utils.GetFormValue(hd_page1json.UniqueID);

            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<MrHaiYangList>(valuejson);

            if (info == null)
            {
                return UtilsCommons.AjaxReturnJson("0", "订单提交失败，请重新提交！", new { FeiHuiYuanId = feiHuiYuanId, DingDanId = string.Empty, DingDanStatus = -1 });
            }

            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            var HangQiInfo = bll.GetHangQiInfo(info.HangQiId);

            #region 表单赋值
            MHangQiDingDanInfo model = new MHangQiDingDanInfo();

            IList<MHangQiDingDanYouKeInfo> YouKes = null;
            BingKeInfo(ref YouKes, xiaDanRenId);
            IList<MHangQiDingDanJiaGeInfo> JiaGes = Fangxing(info.FangXing);

            if (string.IsNullOrEmpty(info.HangQiId))
                str += "-请重新选择订单！</br>";
            else model.HangQiId = info.HangQiId;
            if (string.IsNullOrEmpty(info.RiQiId))
                str += "-请选择出发日期</br>";
            else
                model.RiQiId = info.RiQiId;
            model.CompanyId = YuMingInfo.CompanyId;
            if (info.DingdanRenShu > 0)
                model.RenShu = info.DingdanRenShu;
            else
                str += "-请添加出发人数</br>";


            model.DingDanStatus = EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.未处理;
            model.FuKuanStatus = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款;
            string YuDingRenName = Utils.GetFormValue("txtYudingName");
            if (!string.IsNullOrEmpty(YuDingRenName))
            {
                model.YuDingRenName = YuDingRenName;
            }
            else
                str += "-请填写预订人姓名</br>";
            string YuDingRenDianHua = Utils.GetFormValue("txtYudingtell");
            string YuDingRenShouJi = Utils.GetFormValue("txtYudingphone");
            if (string.IsNullOrEmpty(YuDingRenDianHua) && string.IsNullOrEmpty(YuDingRenShouJi))
            {
                str += "-手机号码和联系电话至少填写一项</br>";
            }
            else
            {
                model.YuDingRenShouJi = YuDingRenShouJi;
                model.YuDingRenDianHua = YuDingRenDianHua;
            }
            string YuDingRenYouXiang = Utils.GetFormValue("txtYudingEmail");
            if (!string.IsNullOrEmpty(YuDingRenYouXiang))
                model.YuDingRenYouXiang = YuDingRenYouXiang;
            //else
            //    str += "-请填写预订人电子邮件</br>";
            model.IsXuYaoFaPiao = Utils.GetInt(Utils.GetFormValue("IscheckFaPiao")) == 1 ? true : false;
            model.FaPiaoTaiTou = Utils.GetFormValue("txtFapiaoTitle");
            //model.FaPiaoMingXi = Utils.GetFormValue("txtFapiaoMingxi");
            model.FaPiaoLeiXing = Utils.GetFormValue("txt_fapiao_mxlx");
            model.FaPiaoMingXi = GetFaPiaoMXLX(model.FaPiaoLeiXing);

            //***********************发票配送方式*******************
            model.FaPiaoPeiSongFangShi = EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi.快递;
            if (model.IsXuYaoFaPiao)
                model.FaPiaoDiZhiId = Utils.GetFormValue(hidAddressId.UniqueID);
            else
                model.FaPiaoDiZhiId = "";
            if (model.IsXuYaoFaPiao)
                model.FaPiaoKuaiDiJinE = HangQiInfo.FaPiaoKuaiDiJinE;
            else
                model.FaPiaoKuaiDiJinE = 0;
            model.XiaDanRenId = xiaDanRenId;
            model.IssueTime = DateTime.Now;
            model.IsTuanGou = false;
            model.FuJiaChanPins = info.HaiYangFuJiaChanPin;
            if (YouKes != null && YouKes.Count > 0 && YouKes.Count == info.DingdanRenShu)
                model.YouKes = YouKes;
            else
                str += "-请填写游客信息</br>";
            if (JiaGes != null && JiaGes.Count > 0)
                model.JiaGes = JiaGes;
            else
                str += "-请选择房型</br>";
            //********************************优惠信息**********************
            MHangQiDingDanYouHuiInfo YouHui = new MHangQiDingDanYouHuiInfo();
            decimal youHuiJInE;
            model.YouHuis = GetDingDanYouHuis(out youHuiJInE);

            MHangQiDingDanDiKouInfo DiKou = new MHangQiDingDanDiKouInfo();
            decimal DingDanJiFen = Utils.GetDecimal(Utils.GetFormValue("txtDingDanJiFen"));
            if (DingDanJiFen > 0)
            {
                if (DingDanJiFen > keYongJiFen)
                { str += "-积分超出可用积分</br>"; }
                else
                {

                    if (DingDanJiFen > HangQiInfo.KeDiKouJinFen)
                    {
                        str = "-积分超出了最多可抵用积分！";
                    }
                    else
                    {
                        DiKou.JiFenBiLi = HangQiInfo.JiFenDuiHuanBiLi;
                        DiKou.JiFen = DingDanJiFen;
                        DiKou.JinFenJinE = HangQiInfo.JiFenDuiHuanBiLi * DingDanJiFen;
                        DiKou.LiPinKaJinE = 0;
                    }
                }
            }
            else
            {
                DiKou.JiFenBiLi = HangQiInfo.JiFenDuiHuanBiLi;
                DiKou.JiFen = 0;
                DiKou.JinFenJinE = 0;
                DiKou.LiPinKaJinE = 0;
            }
            model.DiKouInfo = DiKou;

            //******************************产品金额的计算******************************************
            decimal FuJiaChanPinJinE = 0;
            if (info.HaiYangFuJiaChanPin != null && info.HaiYangFuJiaChanPin.Count > 0)
            {
                for (int i = 0; i < info.HaiYangFuJiaChanPin.Count; i++)
                {
                    FuJiaChanPinJinE += info.HaiYangFuJiaChanPin[i].JinE;
                }
            }
            decimal ZongJinE = info.DingdanFangXingJinE;
            if (ZongJinE > 0)
            {
                ZongJinE += FuJiaChanPinJinE;

                ZongJinE += model.FaPiaoKuaiDiJinE;

                ZongJinE = ZongJinE - DiKou.JinFenJinE;

                ZongJinE = ZongJinE - youHuiJInE;
            }
            else
            { str += "-请重新选择订单！"; }
            model.JinE = ZongJinE;
            //************************************************************************************

            model.JiFenLeiJiBiLi = HangQiInfo.JiFenLeiJiBiLi;
            model.MingCheng = HangQiInfo.MingCheng;
            model.GysName = HangQiInfo.GysName;
            model.GongSiName = HangQiInfo.GongSiName;
            model.XiLieName = HangQiInfo.XiLieName;
            model.ChuanZhiName = HangQiInfo.ChuanZhiName;
            model.XiaDanBeiZhu = Utils.GetFormValue("txtXiaDanBeiZhu");
            #endregion

            if (!string.IsNullOrEmpty(str))
            {
                return UtilsCommons.AjaxReturnJson("0", str, new { FeiHuiYuanId = feiHuiYuanId, DingDanId = string.Empty, DingDanStatus = -1 });
            }

            EyouSoft.BLL.YlStructure.BHangQiDingDan DingDan_Bll = new EyouSoft.BLL.YlStructure.BHangQiDingDan();
            int num = DingDan_Bll.InsertDingDan(model);

            if (num != 1) return UtilsCommons.AjaxReturnJson("0", "订单提交失败，请重新提交！", new { FeiHuiYuanId = feiHuiYuanId, DingDanId = string.Empty, DingDanStatus = -1 });

            var tj = DingDan_Bll.GetDingDanInfo(model.DingDanId);

            return UtilsCommons.AjaxReturnJson("1", "下单成功", new { FeiHuiYuanId = feiHuiYuanId, DingDanId = model.DingDanId, DingDanStatus = (int)tj.DingDanStatus });
        }

        private void BingKeInfo(ref IList<MHangQiDingDanYouKeInfo> YouKes, string huiYuanId)
        {
            string[] YoukeName = HttpContext.Current.Request.Form.GetValues("bkname");
            string[] YoukeZhengjianType = HttpContext.Current.Request.Form.GetValues("sel_zjtype");
            string[] YoukeZhengjian = HttpContext.Current.Request.Form.GetValues("bkzj");
            //string[] YoukeYXQ = HttpContext.Current.Request.Form.GetValues("bkyxq");
            string[] YoukeSex = HttpContext.Current.Request.Form.GetValues("select_Sex");
            //string[] YoukeBrithDay = HttpContext.Current.Request.Form.GetValues("bkbrithday");
            string[] YoukePhone = HttpContext.Current.Request.Form.GetValues("bkphone");
            string[] YoukeType = HttpContext.Current.Request.Form.GetValues("bktype");
            string[] YoukeIsSave = HttpContext.Current.Request.Form.GetValues("hd_Ischeck");

            var yxq1 = HttpContext.Current.Request.Form.GetValues("yxq1");
            var yxq2 = HttpContext.Current.Request.Form.GetValues("yxq2");
            var yxq3 = HttpContext.Current.Request.Form.GetValues("yxq3");

            var sr1 = HttpContext.Current.Request.Form.GetValues("sr1");
            var sr2 = HttpContext.Current.Request.Form.GetValues("sr2");
            var sr3 = HttpContext.Current.Request.Form.GetValues("sr3");

            var sr01 = HttpContext.Current.Request.Form.GetValues("sr01");
            var sr02 = HttpContext.Current.Request.Form.GetValues("sr02");
            var sr03 = HttpContext.Current.Request.Form.GetValues("sr03");

            if (YoukeName.Length > 0 && YoukeZhengjian.Length > 0)
            {
                YouKes = new List<MHangQiDingDanYouKeInfo>();
                for (int i = 0; i < YoukeName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(YoukeName[i]))
                    {
                        MHangQiDingDanYouKeInfo model = new MHangQiDingDanYouKeInfo();
                        model.XingMing = YoukeName[i];
                        model.LeiXingId = Utils.GetInt(YoukeType[i]);
                        model.ZhengJianLeiXing = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)Utils.GetInt(YoukeZhengjianType[i]);
                        model.ZhengJianHaoMa = YoukeZhengjian[i];
                        if (model.ZhengJianHaoMa == "请输入证件号码") model.ZhengJianHaoMa = string.Empty;
                        //model.ZhengJianYouXiaoQi = Utils.GetDateTimeNullable(YoukeYXQ[i]);
                        //model.ChuShengRiQi = Utils.GetDateTimeNullable(YoukeBrithDay[i]);
                        model.ShouJi = YoukePhone[i];
                        if (model.ShouJi == "请至少输入一位出行旅客的手机号码") model.ShouJi = string.Empty;
                        model.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(YoukeSex[i]);

                        model.YXQ1 = yxq1[i];
                        model.YXQ2 = yxq2[i];
                        model.YXQ3 = yxq3[i];

                        if (model.YXQ1 == "年份yyyy") model.YXQ1 = string.Empty;
                        if (model.YXQ2 == "月份mm") model.YXQ2 = string.Empty;
                        if (model.YXQ3 == "日期dd") model.YXQ3 = string.Empty;

                        if (model.ZhengJianLeiXing == EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.身份证)
                        {
                            model.ChuShengRiQi = Utils.GetDateTimeNullable(getSR(model.ZhengJianHaoMa));
                            model.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(getXB(model.ZhengJianHaoMa));
                        }

                        if (model.ZhengJianLeiXing == EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.护照)
                        {
                            model.SR1 = sr1[i];
                            model.SR2 = sr2[i];
                            model.SR3 = sr3[i];
                        }

                        if (model.ZhengJianLeiXing == EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing.户口本)
                        {
                            model.SR1 = sr01[i];
                            model.SR2 = sr02[i];
                            model.SR3 = sr03[i];
                        }

                        if (model.SR1 == "出生年份") model.SR1 = string.Empty;
                        if (model.SR2 == "出生月份") model.SR2 = string.Empty;
                        if (model.SR3 == "出生日期") model.SR3 = string.Empty;

                        YouKes.Add(model);
                        if (YoukeIsSave[i] == "1")
                        {
                            EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
                            EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo lvke = new MHuiYuanChangLvKeInfo()
                            {
                                HuiYuanId = huiYuanId,
                                XingMing = model.XingMing,
                                ZhengJianLeiXing = model.ZhengJianLeiXing,
                                ZhengJianHaoMa = model.ZhengJianHaoMa,
                                ZhengJianYouXiaoQi = model.ZhengJianYouXiaoQi,
                                ChuShengRiQi = model.ChuShengRiQi,
                                ShouJi = model.ShouJi,
                                XingBie = model.XingBie,
                                SR1 = model.SR1,
                                SR2 = model.SR2,
                                SR3 = model.SR3,
                                YXQ1 = model.YXQ1,
                                YXQ2 = model.YXQ2,
                                YXQ3 = model.YXQ3
                            };
                            bll.InsertChangLvKe(lvke);
                        }
                    }
                }
            }
        }

        private IList<MHangQiDingDanJiaGeInfo> Fangxing(IList<MrHaiYangFangXIng> list)
        {
            IList<MHangQiDingDanJiaGeInfo> Jiage = null;
            if (list != null && list.Count > 0)
            {
                Jiage = new List<MHangQiDingDanJiaGeInfo>();
                foreach (var item in list)
                {
                    if (item.FangXingId > 0)
                    {
                        foreach (var item1 in item.JiaGes)
                        {
                            if (item1.RenShu > 0)
                            {
                                MHangQiDingDanJiaGeInfo model = new MHangQiDingDanJiaGeInfo();
                                model.BinKeLeiXingId = item1.BinKeLeiXingId;
                                model.FangCha = item.FangCha;
                                model.FangXingId = item.FangXingId;
                                model.GuoJiId = 0;
                                model.JiaGe1 = item1.JiaGe;
                                model.LouCeng = item.LouCeng;
                                model.RongNaRenShu = item.RongNaRenShu;
                                model.RenShu1 = item1.RenShu;
                                Jiage.Add(model);
                            }
                        }
                    }
                }
            }
            return Jiage;
        }
        #endregion

        /// <summary>
        /// 地址HTML
        /// </summary>
        /// <param name="DiZhiId"></param>
        /// <param name="DiZhi"></param>
        /// <param name="YouBian"></param>
        /// <param name="DianHua"></param>
        /// <param name="IsMoRen"></param>
        /// <returns></returns>
        protected string AddressHtml(object DiZhiId, object DiZhi, object YouBian, object DianHua, object IsMoRen)
        {
            StringBuilder sb = new StringBuilder();
            string Ischeck = "";
            if (DiZhi != null)
            {
                if (!string.IsNullOrEmpty(DiZhi.ToString()))
                {
                    if ((bool)IsMoRen)
                    {
                        Ischeck = "select=\"select\"";
                    }
                    string address = DiZhi.ToString() + "&nbsp;&nbsp;邮编：" + YouBian + "&nbsp;&nbsp;联系电话" + DianHua;
                    sb.AppendFormat("<li> <input type=\"radio\" value=\"{0}\" name=\"addressCk\" {1}>{2}<a href=\"javascript:;\"></a></li>",//【设为默认地址】
                         DiZhiId, Ischeck, address);
                }
            }
            if (string.IsNullOrEmpty(sb.ToString()))
            {
                sb.Append("暂无常用地址，请添加常用地址");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 地址添加
        /// </summary>
        /// <returns></returns>
        protected string AddressSava()
        {
            string str = "";
            bool isLogin = false;
            MYlHuiYuanInfo m = null;
            isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            #region 赋值
            string Name = Utils.GetFormValue("txtName");
            string Tell = Utils.GetFormValue("txtTell");
            int province = Utils.GetInt(Utils.GetFormValue("sel_province"));
            int city = Utils.GetInt(Utils.GetFormValue("sel_city"));
            int country = Utils.GetInt(Utils.GetFormValue("sel_country"));
            string Adress = Utils.GetFormValue("txtAdress");
            string Zip = Utils.GetFormValue("txtZip");
            MHuiYuanDiZhiInfo model = new MHuiYuanDiZhiInfo()
            {
                ShengFenId = province,
                ChengShiId = city,
                XianQuId = country,
                DiZhi = Adress,
                YouBian = Zip,
                XingMing = Name,
                DianHua = Tell
            };
            #endregion

            #region 判断
            if (string.IsNullOrEmpty(model.XingMing))
                str = "-收件人不能为空！</br>";
            if (string.IsNullOrEmpty(model.DianHua))
                str += "-联系电话不能为空！</br>";
            if (model.ShengFenId == 0)
                str += "-请选择省份！</br>";
            if (model.ChengShiId == 0)
                str += "-请选择城市！</br>";
            if (model.XianQuId == 0)
                str += "-请选择县区！</br>";
            if (string.IsNullOrEmpty(model.DiZhi))
                str += "-邮寄地址不能为空！</br>";
            if (string.IsNullOrEmpty(model.YouBian))
                str += "-邮政编码！</br>";
            #endregion

            if (!string.IsNullOrEmpty(str)) return UtilsCommons.AjaxReturnJson("0", str, new { FeiHuiYuanId = string.Empty, DiZhiId = string.Empty });

            string feiHuiYuanId = string.Empty;
            if (isLogin) model.HuiYuanId = m.HuiYuanId;
            else
            {
                var feiHuiYuanInfo = GetFeiHuiYuanInfo();
                model.HuiYuanId = feiHuiYuanId = feiHuiYuanInfo.id;
            }

            var bllRetCode = new EyouSoft.BLL.YlStructure.BHuiYuan().InsertDiZhi(model);
            if (bllRetCode == 1)
            {
                return UtilsCommons.AjaxReturnJson("1", model.DiZhiId, new { FeiHuiYuanId = feiHuiYuanId, DiZhiid = model.DiZhiId });
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("0", "添加失败", new { FeiHuiYuanId = feiHuiYuanId, DiZhiId = string.Empty });
            }
        }

        /// <summary>
        /// get feihuiyun
        /// </summary>
        /// <returns></returns>
        MFeiHuiYuanInfo GetFeiHuiYuanInfo()
        {
            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            if (isLogin) return new MFeiHuiYuanInfo() { id = m.HuiYuanId };

            MFeiHuiYuanInfo info = new MFeiHuiYuanInfo();
            string s = Utils.GetFormValue("txtFeiHuiYuanInfo");
            if (string.IsNullOrEmpty(s)) return info;

            info = Newtonsoft.Json.JsonConvert.DeserializeObject<MFeiHuiYuanInfo>(s);

            info = info ?? new MFeiHuiYuanInfo();

            if (!string.IsNullOrEmpty(info.id)) return info;

            //if (string.IsNullOrEmpty(info.sj) || string.IsNullOrEmpty(info.xm)) return info;

            string guid = Guid.NewGuid().ToString();
            var pwd = new EyouSoft.Model.ComStructure.MPasswordInfo();
            pwd.NoEncryptPassword = guid;

            MHuiYuanInfo huiYuanInfo = new MHuiYuanInfo();
            huiYuanInfo.CompanyId = YuMingInfo.CompanyId;
            huiYuanInfo.Username = guid;
            huiYuanInfo.MD5Password = pwd.MD5Password;
            huiYuanInfo.YouXiang = "";
            huiYuanInfo.ShengRi = DateTime.Now;
            huiYuanInfo.LeiXing = EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing.直接预订;
            huiYuanInfo.XingMing = info.xm;
            huiYuanInfo.ShouJi = info.sj;

            int bllRetCode = new EyouSoft.BLL.YlStructure.BHuiYuan().InsertHuiYuan(huiYuanInfo);

            if (bllRetCode == 1)
            {
                info.id = huiYuanInfo.HuiYuanId;

                MYlHuiYuanInfo outuserinfo = null;
                EyouSoft.Security.Membership.YlHuiYuanProvider.Login(huiYuanInfo.CompanyId, huiYuanInfo.Username, pwd, out outuserinfo, 2);
            }

            return info;
        }

        string GetDiZhiHtml()
        {
            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            if (!isLogin) return string.Empty;

            EyouSoft.Model.YlStructure.MHuiYuanDiZhiChaXunInfo chaxun = new MHuiYuanDiZhiChaXunInfo()
            {
                HuiYuanId = m.HuiYuanId
            };
            int recordCount = 0;

            StringBuilder s = new StringBuilder();
            var items = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDiZhis(YuMingInfo.CompanyId, 20, 1, ref recordCount, chaxun);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    string Ischeck = "";

                    if ((bool)item.IsMoRen)
                    {
                        Ischeck = "select=\"select\"";
                    }
                    string address = item.DiZhi + "&nbsp;&nbsp;邮编：" + item.YouBian + "&nbsp;&nbsp;联系电话" + item.DianHua;
                    s.AppendFormat("<li> <label><input type=\"radio\" value=\"{0}\" name=\"addressCk\" {1}>{2}</label><a href=\"javascript:;\"></a></li>",
                         item.DiZhiId, Ischeck, address);
                }
            }

            return s.ToString();

        }

        void GetDiZhi()
        {
            string s = GetDiZhiHtml();

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", s));
        }

        IList<MHangQiDingDanYouHuiInfo> GetDingDanYouHuis(out decimal youHuiJinE)
        {
            youHuiJinE = 0;
            IList<MHangQiDingDanYouHuiInfo> items = new List<MHangQiDingDanYouHuiInfo>();
            string s = Utils.GetFormValue("txtYouHuiInfo");
            if (string.IsNullOrEmpty(s)) return null;

            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<EyouSoft.YlWeb.ashx.MYouHuiOutPutInfo>(s);
            if (info == null) return null;
            if (info.youhuis == null || info.youhuis.Count == 0) return null;

            foreach (var item in info.youhuis)
            {
                var item1 = new MHangQiDingDanYouHuiInfo();

                item1.GuiZe = item.guize;
                item1.JinE = item.jine;
                item1.MiaoShu = item.miaoshu;
                item1.MingCheng = item.mingcheng;

                youHuiJinE += item.jine;

                items.Add(item1);
            }

            return items;
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

        string GetFaPiaoMXLX(string lx)
        {
            string s = string.Empty;
            switch (lx)
            {
                case "1": s = "旅游业-船票"; break;
                case "2": s = "旅游业-综合服务费"; break;
                case "3": s = "旅游业-旅游费"; break;
            }
            return s;
        }
    }

    public class MrHaiYangFangXIng
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 最底容纳人数
        /// </summary>
        public int RongNaRenShu { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal FangCha { get; set; }
        /// <summary>
        /// 价格说明
        /// </summary>
        public string ShuoMing { get; set; }
        /// <summary>
        /// 价格、人数、宾客类型集合
        /// </summary>
        public IList<MrHaiYangJiaGe> JiaGes { get; set; }
    }

    public class MrHaiYangJiaGe
    {
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int RenShu { get; set; }
    }

    public class MrHaiYangList
    {

        public string HangQiId { get; set; }

        public decimal DingdanFangXingJinE { get; set; }

        public string ChuFaTime { get; set; }

        public IList<MrHaiYangFangXIng> FangXing { get; set; }

        public IList<MHangQiDingDanFuJiaChanPinInfo> HaiYangFuJiaChanPin { get; set; }

        public string RiQiId { get; set; }

        public string html { get; set; }

        public string html2 { get; set; }

        public string html3 { get; set; }

        public string html4 { get; set; }

        public string DingdanRenjun { get; set; }

        public int DingdanRenShu { get; set; }

        public decimal KeYongJiFen { get; set; }

    }
}
