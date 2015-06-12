using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.YlStructure;
using EyouSoft.Common;
using EyouSoft.Model.YlStructure;
using EyouSoft.Model.EnumType.YlStructure;
using System.Text;
using EyouSoft.Model.SSOStructure;
using EyouSoft.YlWeb.Hangqi;

namespace EyouSoft.YlWeb.TuanGou
{
    public partial class TuanGouXiangQing : WzPage
    {
        protected MTuanGouInfo Model;
        /// <summary>
        /// 房型
        /// </summary>
        protected string FangxingStr;
        /// <summary>
        /// 宾客类型
        /// </summary>
        protected string BinkeStr;
        /// <summary>
        /// 登船港口 
        /// </summary>
        protected string Dengchuan_gangkou;
        /// <summary>
        /// 下船港口
        /// </summary>
        protected string Xiachuan_gangkou;
        /// <summary>
        /// 航线性质
        /// </summary>
        protected string Hangxian_XingZhi;
        /// <summary>
        /// 途径城市
        /// </summary>
        protected string TuJingChengShi;
        /// <summary>
        /// 天数1
        /// </summary>
        protected int TianShu1;
        /// <summary>
        /// 天数2
        /// </summary>
        protected int TianShu2;
        /// <summary>
        /// 船只ID
        /// </summary>
        protected string ChuanZhiId;
        /// <summary>
        /// 快递金额
        /// </summary>
        protected decimal KuaiDiJinE;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Utils.GetQueryStringValue("type");
            string tuangouId = Utils.GetQueryStringValue("TuanGouId");
            if (type == "CheckPage") RCWE(SelectProduct());
            if (type == "sava") RCWE(Sava());
            if (type == "address") RCWE(AddressSava());
            if (type == "getdizhi") GetDiZhi();

            if (string.IsNullOrEmpty(tuangouId)) RCWE("请求异常");
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.团购横幅;
            if (!IsPostBack)
            {
                BHangQi bll_hangqi = new BHangQi();
                Model = bll_hangqi.GetTuanGouInfo(tuangouId);
                if (Model == null) RCWE("请求异常");

                EyouSoft.BLL.YlStructure.BJiChuXinXi bll_jichuxinxi = new BJiChuXinXi();
                EyouSoft.BLL.YlStructure.BHuiYuan HuiYuan = new EyouSoft.BLL.YlStructure.BHuiYuan();
                MJiChuXinXiChaXunInfo searchModel = new MJiChuXinXiChaXunInfo();
                Hangqi.Value = Model.HangQiId;
                RiQiID.Value = Model.RiQiId;
                FangXingID.Value = Model.FangXingId.ToString();
                TuanGouId.Value = Model.TuanGouId;
                this.BinkeStr = Model.BinKeLeiXing;


                MHangQiInfo hangQiInfo = bll_hangqi.GetHangQiInfo(Model.HangQiId);
                MChuanZhiInfo chuanZhiInfo = bll_jichuxinxi.GetChuanZhiInfo(hangQiInfo.ChuanZhiId);
                ChuanZhiId = hangQiInfo.ChuanZhiId;
                AddressFree.Value = hangQiInfo.FaPiaoKuaiDiJinE.ToString();
                KuaiDiJinE = hangQiInfo.FaPiaoKuaiDiJinE;
                txt_YL_FeiYong.Text = hangQiInfo.FeiYongShuoMing;
                txt_YL_YuDingXuZhi.Text = hangQiInfo.YuDingXuZhi;
                txt_YL_YouQing.Text = hangQiInfo.YouQingTiShi;
                txt_YL_YouLunGongLue.Text = hangQiInfo.GongLue;
                //this.FangxingStr = string.Join(",", chuanZhiInfo.FangXings.Select(x => x.MingCheng).ToArray());
                this.FangxingStr = JiChuXinXi(Model.FangXingId);

                #region 用户点评

                decimal FenShu = HuiYuan.GetDianPingJunFen(Model.HangQiId);
                int recordCount = 0;
                MWzDianPingChaXunInfo DianPingChaXun = new MWzDianPingChaXunInfo()
                {
                    HangQiId = Model.HangQiId,
                    IsShenHe = true
                };
                var DianPinglist = HuiYuan.GetDianPings(YuMingInfo.CompanyId, 5, 1, ref recordCount, DianPingChaXun);
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
                    HangQiId = Model.HangQiId,
                    IsHuiFu = true
                };
                var WendaList = HuiYuan.GetWenDas(YuMingInfo.CompanyId, 5, 1, ref Count, WenDaChaxun);
                if (WendaList != null && WendaList.Count > 0)
                {
                    rptListZiXunWD.DataSource = WendaList;
                    rptListZiXunWD.DataBind();
                }

                #endregion

                Dengchuan_gangkou = JiChuXinXi(hangQiInfo.ChuFaGangKouId);
                Xiachuan_gangkou = JiChuXinXi(hangQiInfo.DiDaGangKouId);

                this.Hangxian_XingZhi = hangQiInfo.HangXianXingZhi;

                this.TuJingChengShi = hangQiInfo.TuJingChengShi;
                this.TianShu1 = hangQiInfo.TianShu1;
                this.TianShu2 = hangQiInfo.TianShu2;


                JiaGes(Model.JiaGes.Select(x => new { LeiXingId = x.BinKeLeiXingId, MingCheng = JiChuXinXi(x.BinKeLeiXingId), JiaGe = x.JiaGe }).ToArray());

                YouLunInfo(chuanZhiInfo.ChuanZhiId);
                ChuanZhiId = chuanZhiInfo.ChuanZhiId;
                XingChengs(hangQiInfo.XingChengs);

                InsertHangQJiLu(hangQiInfo.HangQiId);

                ltrChangYongDiZhi.Text = GetDiZhiHtml();

                EyouSoft.Model.YlStructure.MWzKvInfo httk = null;

                if (hangQiInfo.LeiXing == YouLunLeiXing.长江游轮)
                    httk = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮合同条款);
                if (hangQiInfo.LeiXing == YouLunLeiXing.海洋邮轮)
                    httk = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮合同条款);
                if (httk != null && !string.IsNullOrEmpty(httk.V))
                {
                    ltrHeTongTiaoKuan.Text = httk.V;
                }
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
                    sb.Append("<img src=\"../images/x01.jpg\">");
                }
            }
            return sb.ToString();
        }

        #region 游轮信息
        /// <summary>
        /// 价格套餐
        /// </summary>
        /// <param name="JiaGes"></param>
        private void JiaGes(IList<object> JiaGes)
        {
            Repeater1.DataSource = JiaGes;
            Repeater1.DataBind();
        }

        /// <summary>
        /// 行程信息
        /// </summary>
        private void XingChengs(IList<MHangQiXingChengInfo> XingChengs)
        {
            rptList_XingCheng.DataSource = XingChengs;
            rptList_XingCheng.DataBind();
        }

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
                    //txt_YL_XingJi.Text = XingJiShow(model.XingJi);
                    if (model.FuJians != null && model.FuJians.Count > 0)
                    {
                        txt_YL_JieShaoImage.Text = "<img width=\"466\" height=\"353\" src=\"" + TuPian.F1(ErpFilepath + model.FuJians[0].Filepath, 466, 353) + "\">";

                    }
                    txt_YL_JScontent.Text = model.JianYaoJieShao;
                    txt_YL_FangXing.Text = FangXing(model.FangXings);
                    txt_YL_MeiShi.Text = MeiShi(model.MeiShis);
                    txt_YL_SheShi.Text = SheShi(model.SheShis);
                    txt_YL_PingMian.Text = PingMian(model.PingMianTus);
                    txt_YL_ShiPin.Text = ShiPing(model.ChuanZhiId);
                }
            }
        }
        /// <summary>
        /// 房形介绍
        /// </summary>
        private string FangXing(IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo> Fangxing)
        {
            StringBuilder sb = new StringBuilder();
            if (Fangxing != null && Fangxing.Count > 0)
            {
                for (int i = 0; i < Fangxing.Count; i++)
                {
                    if (i == 6) break;
                    sb.AppendFormat("<li><a href=\"{2}\" target=\"_blank\"><img src=\"{0}\"><span class=\"name\">{1}</span></a></li>",
                        TuPian.F1(ErpFilepath + Fangxing[i].Filepath,205,137), Fangxing[i].MingCheng, "/Youlun/ChuanZhi.aspx?id=" + ChuanZhiId + "#fangxing");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 美食介绍
        /// </summary>
        private string MeiShi(IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 3) break;
                    sb.AppendFormat("<li><a href=\"{0}\" target=\"_blank\"><img src=\"{1}\"><span class=\"name\">{2}</span><span class=\"title\">{3}</span></a></li>",
                        "/Youlun/ChuanZhi.aspx?id=" + ChuanZhiId + "#meishi", TuPian.F1(ErpFilepath + list[i].Filepath,217,157), list[i].MingCheng, list[i].MiaoShu);
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
                        , "/Youlun/ChuanZhi.aspx?id=" + ChuanZhiId + "#sheshi", TuPian.F1(ErpFilepath + list[i].Filepath, 217, 157), list[i].MiaoShu, list[i].MiaoShu);
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
                            sb.AppendFormat("<div class=\"video_big\"><a target=\"_blank\" href=\"/Youlun/ChuanZhiCJ.aspx?id={0}#youlun\"><img src=\"{1}\"></a></div>", list[i].GongSiId, ErpFilepath + list[i].ShiPinIMG);
                        }
                        else
                        {
                            if (Append)
                            {
                                sb.Append("<div class=\"video_small\"><ul>");
                            }
                            sb.AppendFormat("<li><a target=\"_blank\" href=\"/Youlun/ChuanZhiCJ.aspx?id={0}#youlun\"><img src=\"{1}\"></a></li>", list[i].GongSiId, ErpFilepath + list[i].ShiPinIMG);
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
        /// <summary>
        /// 行程图片
        /// </summary>
        protected string XingChengImage(object obj)
        {
            string str = "";
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                str = string.Format("<img src=\"{0}\">",TuPian.F1( ErpFilepath + obj.ToString(),217,149));
            }
            return str;
        }
        /// <summary>
        /// 写入航期浏览记录信息
        /// </summary>
        private void InsertHangQJiLu(string HangQiId)
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            EyouSoft.Model.YlStructure.MHangQiLiuLanJiLuInfo info = new MHangQiLiuLanJiLuInfo()
            {
                HangQiId = HangQiId
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

        #endregion

        #region 订单提交

        #region 第一部
        private string SelectProduct()
        {
            string str = ""; string strid = "0";

            string json = Utils.GetFormValue("txt");

            if (!string.IsNullOrEmpty(json))
            {
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<MrTuanGouList>(json);
                Page1Info(info);
                if (info.DingdanRenShu > 0)
                {
                    str = "成功"; strid = "1";
                }
                else
                {
                    str = "-请选择游客人数！"; strid = "0";
                    return UtilsCommons.AjaxReturnJson(strid, str);
                }
                return UtilsCommons.AjaxReturnJson(strid, str, info);
            }
            else
            {
                str = "-请选择游客人数！"; strid = "0";
                return UtilsCommons.AjaxReturnJson(strid, str);
            }
        }

        private void Page1Info(MrTuanGouList model)
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            var FromHangqi = bll.GetHangQiInfo(model.HangQiId);
            DateTime Data = DateTime.Now;
            StringBuilder dingdan = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            int Sum = 0;
            string BkHtml = "";
            int num = 1;
            if (model.JiaGes != null && model.JiaGes.Count > 0)
            {
                foreach (var item in model.JiaGes)
                {
                    if (item.RenShu1 > 0)
                    {
                        Sum += item.RenShu1;
                        sb.AppendFormat("<dl><dt>【{0}】</dt>", JiChuXinXi(item.FangXingId));
                        sb.AppendFormat("<dd>{0}{1}人 共计：{2}元</dd>", JiChuXinXi(item.BinKeLeiXingId), item.RenShu1, (item.JiaGe1 * item.RenShu1).ToString("F2"));

                        BkHtml += BingKeHtml(item, ref num);
                        sb.Append("</dl>");
                    }
                }
            }

            var tuanGouInfo = bll.GetTuanGouInfo(model.TuanGouId);

            dingdan.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");
            dingdan.Append("<tbody><tr><th>出发日期</th><th>名称</th><th>项目详情</th><th>金额</th><th>下单时间</th>");
           // dingdan.Append("<th>操作</th>");
            dingdan.Append("</tr><tr>");
            dingdan.AppendFormat("<td align=\"center\" class=\"font14\">{0}</td>", model.ChuFaTime);
            dingdan.AppendFormat("<td valign=\"middle\" class=\"font14 padd20\">{0}<br><a class=\"fontgreen font12\" target=\"_blank\" href=\"tuangouxiangqing.aspx?tuangouid={1}\">【详情】</a></td>", tuanGouInfo.MingCheng, tuanGouInfo.TuanGouId);
            dingdan.AppendFormat("<td valign=\"top\" class=\"font12\">{0}</td>", sb.ToString());
            dingdan.AppendFormat("<td valign=\"middle\" align=\"center\"><b class=\"font20 fontred\">{0}</b><br></td>", (model.DingdanFangXingJinE).ToString("C2"));
            dingdan.AppendFormat("<td valign=\"middle\" align=\"center\">{0}<br>{1}</td>", Data.ToString("yyyy-MM-dd"), Data.ToString("t"));
            //dingdan.Append("<td valign=\"middle\" align=\"center\" class=\"right\"><a class=\"del_btn\" href=\"#\">删除</a></td>");
            dingdan.Append("</tr></tbody></table>");

            #region Page2
            StringBuilder sb2 = new StringBuilder();
            sb2.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");
            sb2.Append("<tbody><tr><th width=\"35%\">名称</th> <th width=\"15%\">出发日期</th><th width=\"35%\">项目详情</th>");
            sb2.Append("<th class=\"right\">金额</th></tr><tr>");
            sb2.AppendFormat("<td class=\"font14 padd20\">{0}</br><a class=\"fontgreen font12\"  target=\"_blank\" href=\"tuangouxiangqing.aspx?tuangouid={1}\">【详情】</a></td>", FromHangqi.MingCheng, tuanGouInfo.TuanGouId);
            sb2.AppendFormat("<td valign=\"middle\" align=\"center\" class=\"font14\">{0}</td>", model.ChuFaTime);
            sb2.AppendFormat("<td valign=\"top\" class=\"font12\">{0} </td>", sb.ToString());
            sb2.AppendFormat("<td valign=\"middle\" align=\"center\" class=\"right\"><b class=\"font20 fontred\">{0}</b></td></tr></tbody></table>", (model.DingdanFangXingJinE).ToString("C2"));
            #endregion


            model.html = dingdan.ToString();
            model.DingdanRenShu = Sum;
            if (model.DingdanRenShu > 0)
                model.DingdanRenjun = model.DingdanFangXingJinE / model.DingdanRenShu;
            else
                model.DingdanRenjun = 0;
            model.html3 = BkHtml;
            model.html2 = sb2.ToString();

        }
        /// <summary>
        /// 宾客列表HTML
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bkint"></param>
        /// <returns></returns>
        private string BingKeHtml(MHangQiDingDanJiaGeInfo model, ref int num)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < model.RenShu1; i++)
            {
                sb.Append("<div class=\"lvke_box\"><div class=\"L_jiao\"></div><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                sb.AppendFormat("<tbody><tr><td valign=\"middle\" align=\"center\" class=\"leftT\"><h3>旅客{0}</h3><span class=\"chenren\">{1}</span><input id=\"bktype\" name=\"bktype\" runat=\"server\" value=\"{2}\" type=\"hidden\" /></td>", num, JiChuXinXi(model.BinKeLeiXingId), model.BinKeLeiXingId);
                sb.Append("<td><div class=\"lvke_Rbox fixed\"><ul class=\"lvke_form\">");
                sb.Append("<li><label><font class=\"font_star\">*</font> 姓名：</label><span><input type=\"text\"  id=\"bkname\" name=\"bkname\" value=\"\" class=\"formsize370 inputbk\" valid=\"required\" errmsg=\"请填写游客姓名！\"></span><span class=\"error\" data-class='yktxsm'>填写说明</span></li>");
                sb.Append("<li><label><font class=\"font_star\">*</font> 证件类型：</label><div style='float:left'>");
                //sb.AppendFormat("<select name=\"sel_zjtype\" onchange=\"PageSet.changeValidByCardType(this);\" class=\"select_style_1\">{0}</select></div><div style='float:left; margin-left:5px;'><input type=\"text\" valid=\"isIdCard\" errmsg=\"请正确填写旅客身份证！\" name=\"bkzj\" id=\"bkzj\" value=\"\" class=\"formsize270 inputbk\"></div><div style='clear:both'></div></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType))));
                sb.AppendFormat("<select name=\"sel_zjtype\" class=\"select_style_1\">{0}</select></div><div style='float:left; margin-left:5px;' data-class='zjhm'><input type=\"text\" errmsg=\"请填写游客身份证！|请正确填写游客身份证！\" name=\"bkzj\" id=\"bkzj\" value=\"请输入证件号码\" class=\"formsize270 inputbk\" style='color:#999'></div><div style='float:left; margin-left:5px;' data-class='sr01'><input type=\"text\" name=\"sr01\" value=\"出生年份\" class=\" formsize100 inputbk\" style='color:#999;margin-right:5px;'><input type=\"text\" name=\"sr02\" value=\"出生月份\" class=\" formsize80 inputbk\" style='color:#999;margin-right:5px;'><input type=\"text\" name=\"sr03\" value=\"出生日期\" class=\" formsize80 inputbk\" style='color:#999;margin-right:5px;'></div><div style='clear:both'></div></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing))));
                //sb.Append("<li><label>证件有效期：</label><span><input type=\"text\" onfocus=\"WdatePicker()\" id=\"bkyxq\" name=\"bkyxq\" value=\"\" class=\" formsize100 inputbk\"></span></li>");
                sb.Append("<li data-class='zjyxq'><label>证件有效期：</label><span><input type=\"text\" name=\"yxq1\" value=\"年份yyyy\" class=\" formsize100 inputbk\"><input type=\"text\" name=\"yxq2\" value=\"月份mm\" class=\" formsize80 inputbk\"><input type=\"text\" name=\"yxq3\" value=\"日期dd\" class=\" formsize80 inputbk\"></span></li>");
                sb.AppendFormat("<li data-class='xb'><label>性别：</label><span><dl class=\"select_style\"><select class='select_style_1' name=\"select_Sex\">{0}</select></dl></span></li>", UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender))));
                //sb.Append("<li><label>出生日期：</label><span><input type=\"text\" onfocus=\"WdatePicker()\" name=\"bkbrithday\" id=\"bkbrithday\" value=\"\" class=\" formsize100 inputbk\"></span></li>");
                sb.Append("<li data-class='sr'><label>出生日期：</label><span><input type=\"text\" name=\"sr1\" value=\"出生年份\" class=\" formsize100 inputbk\"><input type=\"text\" name=\"sr2\" value=\"出生月份\" class=\" formsize80 inputbk\"><input type=\"text\" name=\"sr3\" value=\"出生日期\" class=\" formsize80 inputbk\"></span></li>");
                sb.Append("<li><label>手机号码：</label><span><input id=\"bkphone\" name=\"bkphone\" type=\"text\" value=\"\" class=\"formsize370 inputbk\"></span></li>");
                sb.Append("</ul>");
                sb.Append("<div class=\"lvke_caozuo\"><label><input type=\"checkbox\" id=\"\" class=\"savacontact\" value=\"1\"  name=\"ischeck\" checked='checked'> 保存到常用姓名</label> <input id=\"hd_Ischeck\" name=\"hd_Ischeck\" value=\"1\" type=\"hidden\"/> <a class=\"clearInput\" href=\"javascript:;\">清空</a></div>");
                sb.Append("</div></td></tr></tbody></table></div>");
                num++;
            }
            return sb.ToString();

        }
        #endregion

        #region 第二部
        private string Sava()
        {
            string str = "";
            bool isLogin = false;
            MYlHuiYuanInfo m = null;
            isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            decimal keYongJiFen = 0;
            string feiHuiYuanId = string.Empty;

            string xiaDanRenId = string.Empty;
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

            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<MrTuanGouList>(valuejson);

            if (info == null) return UtilsCommons.AjaxReturnJson("0", "订单提交失败，请重新提交！", new { FeiHuiYuanId = feiHuiYuanId, DingDanId = string.Empty, DingDanStatus = -1 });

            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();

            var HangQiInfo = bll.GetHangQiInfo(info.HangQiId);


            #region 表单赋值
            MHangQiDingDanInfo model = new MHangQiDingDanInfo();
            IList<MHangQiDingDanYouKeInfo> YouKes = null;
            BingKeInfo(ref YouKes, xiaDanRenId);
            IList<MHangQiDingDanJiaGeInfo> JiaGes = info.JiaGes;

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


            model.IsTuanGou = true;
            model.TuanGouId = info.TuanGouId;
            model.FuJiaChanPins = null;
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
            model.YouHuis = null;
            MHangQiDingDanDiKouInfo DiKou = new MHangQiDingDanDiKouInfo();

            model.DiKouInfo = null;

            //******************************产品金额的计算******************************************


            decimal ZongJinE = 0;
            foreach (var item in model.JiaGes)
            {
                ZongJinE += item.JiaGe1 * item.RenShu1;
            }
            if (ZongJinE > 0&&info.DingdanFangXingJinE==ZongJinE)
            {
                if(model.IsXuYaoFaPiao)
                ZongJinE += model.FaPiaoKuaiDiJinE;
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
                            XingBie = model.XingBie
                        };
                        bll.InsertChangLvKe(lvke);
                    }

                }
            }
        }
        #endregion

        #endregion

        #region 配送地址
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

            if (!string.IsNullOrEmpty(str)) return UtilsCommons.AjaxReturnJson("0", str, new { FeiHuiYuanId = "", DiZhiId = "" });

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
        /// 邮寄地址
        /// </summary>
        /// <returns></returns>
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
        #endregion

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

        void GetDiZhi()
        {
            string s = GetDiZhiHtml();

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", s));
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

    public class MrTuanGouList
    {
        public string HangQiId { get; set; }

        public string TuanGouId { get; set; }

        public string ChuFaTime { get; set; }

        public string RiQiId { get; set; }

        public string html { get; set; }

        public string html2 { get; set; }

        public string html3 { get; set; }

        public string html4 { get; set; }

        public decimal DingdanRenjun { get; set; }

        public int DingdanRenShu { get; set; }

        public decimal DingdanFangXingJinE { get; set; }

        public IList<MHangQiDingDanJiaGeInfo> JiaGes { get; set; }
    }
}
