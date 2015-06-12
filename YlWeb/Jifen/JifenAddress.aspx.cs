using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb.Jifen
{
    public partial class JifenAddress : EyouSoft.YlWeb.WzPage
    {
        protected string CurrentUserCompanyID = "";
        protected MYlHuiYuanInfo m = null;
        protected bool isLogin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUserCompanyID = YuMingInfo.CompanyId;
            isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);
            string type = Utils.GetQueryStringValue("dotype");

            if (type == "address") { RCWE(AddressSava()); }
            if (type == "isaddress") { RCWE(IsAddress()); }
            if (type == "jfsubmit") { RCWE(DingDanSava()); }

            if (!Page.IsPostBack)
            {
                InitInfo();
            }
        }


        protected void InitInfo()
        {
            string id = Utils.GetQueryStringValue("jfid");
            int fangshiId = Utils.GetInt(Utils.GetQueryStringValue("fsid"));
            int Sum = Utils.GetInt(Utils.GetQueryStringValue("jfsum"));

            EyouSoft.BLL.YlStructure.BDuiHuan DuiHuan = new EyouSoft.BLL.YlStructure.BDuiHuan();
            var jiinfo = DuiHuan.GetJiFenShangPinInfo(id);

            if (!isLogin) Response.Redirect("jifeninfo.aspx?id=" + id);
            if (jiinfo == null) Response.Redirect("jifenlist.aspx");
            if (Sum < 1) Response.Redirect("jifenlist.aspx");

            if (isLogin && jiinfo != null && Sum >= 1)
            {
                HidKuaiDiJinE.Value = jiinfo.FaPiaoKuaiDiJinE.ToString("F2");

                #region 地址
                int recordCount = 0;
                EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
                EyouSoft.Model.YlStructure.MHuiYuanDiZhiChaXunInfo chaxun = new MHuiYuanDiZhiChaXunInfo()
                {
                    HuiYuanId = m.HuiYuanId
                };
                var list = bll.GetDiZhis(YuMingInfo.CompanyId, 20, 1, ref recordCount, chaxun);
                rptList.DataSource = list;
                rptList.DataBind();
                #endregion

                #region 积分产品详细
                txtjfName.Text = jiinfo.MingCheng;
                txtJinE.Text = JinEHtml(jiinfo.FangShis, fangshiId, Sum);
                txtSum.Text = Sum.ToString();
                #endregion
            }
            else
                RCWE("异常请求");

        }

        private string JinEHtml(IList<MWzJiFenShangPinFangShiInfo> model, int fangshiId, int Sum)
        {
            StringBuilder sb = new StringBuilder();
            if (model != null && model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    if ((int)model[i].FangShi == fangshiId)
                    {
                        switch (model[i].FangShi)
                        {
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分:
                                sb.AppendFormat("<b class=\"font18 jf_score\">{0}</b>分", model[i].JiFen.ToString("F0"));
                                txtShiFu.Text = "0";
                                txtXiaoji.Text = "0";
                                DingDanJinE.Value = "0";
                                break;
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡:
                                sb.AppendFormat("<b class=\"font18 jf_score\">{0}</b>分 + <b class=\"font_f00 font18\">{1}</b>元礼品卡", model[i].JiFen.ToString("F0"), model[i].JinE.ToString("F2"));
                                txtShiFu.Text = (model[i].JinE * Sum).ToString("C0");
                                txtXiaoji.Text = (model[i].JinE * Sum).ToString("F2");
                                DingDanJinE.Value = (model[i].JinE * Sum).ToString("F2");
                                break;
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金:
                                sb.AppendFormat("<b class=\"font18 jf_score\">{0}</b>分 + <b class=\"font_f00 font18\">{1}</b>元", model[i].JiFen.ToString("F0"), model[i].JinE.ToString("F2"));
                                txtShiFu.Text = (model[i].JinE * Sum).ToString("C0");
                                txtXiaoji.Text = (model[i].JinE * Sum).ToString("F2");
                                DingDanJinE.Value = (model[i].JinE * Sum).ToString("F2");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return sb.ToString();
        }

        #region 私有方法
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
            
            if (DiZhi != null)
            {
                if (!string.IsNullOrEmpty(DiZhi.ToString()))
                {
                    string Ischeck = "";
                    if ((bool)IsMoRen)
                    {
                        Ischeck = "select=\"select\"";
                    }
                    string address = DiZhi.ToString() + "&nbsp;&nbsp;邮编：" + YouBian + "&nbsp;&nbsp;联系电话" + DianHua;
                    sb.AppendFormat("<li> <label><input type=\"radio\" value=\"{0}\" name=\"addressCk\" {1}>{2}</label><a href=\"javascript:;\"></a></li>",//【设为默认地址】
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
        /// 默认地址
        /// </summary>
        /// <returns></returns>
        protected string IsAddress()
        {
            string DiZhiId = Utils.GetQueryStringValue("dz"); int strId = 0; string str = "默认地址设置失败！";
            if (DiZhiId != null)
            {
                if (!string.IsNullOrEmpty(DiZhiId.ToString()))
                {
                    EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
                    strId = bll.SheZhiMoRenDiZhi(m.HuiYuanId, DiZhiId.ToString());
                    str = "默认地址设置成功！";
                }
            }
            return UtilsCommons.AjaxReturnJson(strId > 0 ? "1" : "0", str);
        }
        /// <summary>
        /// 地址添加
        /// </summary>
        /// <returns></returns>
        protected string AddressSava()
        {
            string str = ""; int strId = 0;
            if (m != null)
            {
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
                    HuiYuanId = m.HuiYuanId,
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
                if (str != "")
                {
                    return UtilsCommons.AjaxReturnJson("0", str);
                }
                EyouSoft.BLL.YlStructure.BHuiYuan bll = new EyouSoft.BLL.YlStructure.BHuiYuan();
                strId = bll.InsertDiZhi(model);
                if (strId > 0)
                {
                    return UtilsCommons.AjaxReturnJson("1", "添加成功！", new { DiZhiid = model.DiZhiId });
                    
                }
                else
                    return UtilsCommons.AjaxReturnJson("0", "添加失败", new { DiZhiId = string.Empty });
            }
            return UtilsCommons.AjaxReturnJson(strId > 0 ? "1" : "0", str);
        }

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <returns></returns>
        protected string DingDanSava()
        {
            string id = Utils.GetQueryStringValue("jfid");
            int Sum = Utils.GetInt(Utils.GetQueryStringValue("jfsum"));
            int fangshiId = Utils.GetInt(Utils.GetQueryStringValue("fsid"));
            string dizhi = Utils.GetFormValue(hidAddressId.UniqueID);
            string str = "";
            EyouSoft.BLL.YlStructure.BDuiHuan bll = new EyouSoft.BLL.YlStructure.BDuiHuan();
            var DuiHuan = bll.GetJiFenShangPinInfo(id);

            if (DuiHuan == null) return UtilsCommons.AjaxReturnJson("0", "兑换的积分商品不存在");

            if (DuiHuan.Status == EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus.下架) return UtilsCommons.AjaxReturnJson("0", "商品已下架，不能兑换");

            if (!isLogin) return UtilsCommons.AjaxReturnJson("0", "请登录后再兑换");

            var JfFangShi = DuiHuan.FangShis.Where(n => (int)n.FangShi == fangshiId).ToList();

            #region 赋值
            /// <summary>
            /// 商品编号
            /// </summary>
            string ShangPinId = id;
            /// <summary>
            /// 公司编号
            /// </summary>
            string CompanyId = YuMingInfo.CompanyId;

            /// <summary>
            /// 数量
            /// </summary>
            int ShuLiang = Sum;
            /// <summary>
            /// 兑换方式
            /// </summary>
            EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi FangShi = JfFangShi[0].FangShi;
            
            /// <summary>
            /// 积分
            /// </summary>
            decimal JiFen = JfFangShi[0].JiFen * Sum;
            /// <summary>
            /// 订单状态
            /// </summary>
            EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus DingDanStatus = EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.未处理;
            /// <summary>
            /// 付款状态
            /// </summary>
            EyouSoft.Model.EnumType.YlStructure.FuKuanStatus FuKuanStatus = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款;
            /// <summary>
            /// 下单备注
            /// </summary>
            string XiaDanBeiZhu = "";
            /// <summary>
            /// 预订人姓名
            /// </summary>
            string YuDingRenName = "";
            /// <summary>
            /// 预订人电话
            /// </summary>
            string YuDingRenDianHua = "";
            /// <summary>
            /// 预订人手机
            /// </summary>
            string YuDingRenShouJi = "";
            /// <summary>
            /// 预订人邮箱
            /// </summary>
            string YuDingRenYouXiang = "";
            /// <summary>
            /// 是否需要发票
            /// </summary>
            bool IsXuYaoFaPiao = Utils.GetInt(Utils.GetFormValue(hidIsFapiao.UniqueID)) == 1 ? true : false;
            /// <summary>
            /// 发票抬头
            /// </summary>
            string FaPiaoTaiTou = Utils.GetFormValue("txtFapTitle");
            /// <summary>
            /// 发票明细类型
            /// </summary>
            string FaPiaoLeiXing = "";
            /// <summary>
            /// 发票明细
            /// </summary>
            string FaPiaoMingXi = Utils.GetFormValue("txtFapMingx");
            /// <summary>
            /// 发票配送方式
            /// </summary>
            EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi FaPiaoPeiSongFangShi = EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi.快递;
            /// <summary>
            /// 发票配送地址
            /// </summary>
            string FaPiaoDiZhiId = dizhi;
            /// <summary>
            /// 发票快递费用
            /// </summary>
            decimal FaPiaoKuaiDiJinE = 0;
            if (IsXuYaoFaPiao)
                FaPiaoKuaiDiJinE = DuiHuan.FaPiaoKuaiDiJinE;
            /// <summary>
            /// 下单人编号
            /// </summary>
            string XiaDanRenId = m.HuiYuanId;
            /*******************金额计算***********************************************************/
            /// <summary>
            /// 金额
            /// </summary>
            decimal JinE = JfFangShi[0].JinE * Sum;
            if (IsXuYaoFaPiao)
                JinE = JinE + FaPiaoKuaiDiJinE;

            /******************************************************************************/
            #endregion

            EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo info = new MWzJiFenDingDanInfo()
            {
                ShangPinId = ShangPinId,
                CompanyId = CompanyId,
                ShuLiang = ShuLiang,
                FangShi = FangShi,
                JinE = JinE,
                JiFen = JiFen,
                DingDanStatus = DingDanStatus,
                FuKuanStatus = FuKuanStatus,
                XiaDanBeiZhu = XiaDanBeiZhu,
                YuDingRenName = YuDingRenName,
                YuDingRenDianHua = YuDingRenDianHua,
                YuDingRenShouJi = YuDingRenShouJi,
                YuDingRenYouXiang = YuDingRenYouXiang,
                IsXuYaoFaPiao = IsXuYaoFaPiao,
                FaPiaoTaiTou = FaPiaoTaiTou,
                FaPiaoLeiXing = FaPiaoLeiXing,
                FaPiaoMingXi = FaPiaoMingXi,
                FaPiaoPeiSongFangShi = FaPiaoPeiSongFangShi,
                FaPiaoDiZhiId = FaPiaoDiZhiId,
                FaPiaoKuaiDiJinE = FaPiaoKuaiDiJinE,
                XiaDanRenId = XiaDanRenId
            };

            if (info.JinE <= 0 && info.JiFen <= 0)
                str += "兑换积分产品数量小于1!</br>";
            if (Sum < 1)
                str += "兑换积分产品数量小于1！</br>";
            if (info.IsXuYaoFaPiao&&string.IsNullOrEmpty(info.FaPiaoDiZhiId))
                str += "请选择配送地址！</br>";

            if (!string.IsNullOrEmpty(str)) return UtilsCommons.AjaxReturnJson("0", str);

            decimal keYongJiFen = 0;
            var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.HuiYuanId);
            if (huiYuanInfo != null) keYongJiFen = huiYuanInfo.KeYongJiFen;

            if (keYongJiFen < info.JiFen) return UtilsCommons.AjaxReturnJson("0", "你的可用积分不足！");

            int bllRetCode = bll.InsertJiFenDingDan(info);

            if (bllRetCode == 1)
            {
                var tj = bll.GetJiFenDingDanInfo(info.DingDanId);
                return UtilsCommons.AjaxReturnJson("1", "兑换成功", new { DingDanId = info.DingDanId, FangShi = (int)info.FangShi, JinE = info.JinE, FuKuanState = (int)tj.FuKuanStatus });
            }
            else return UtilsCommons.AjaxReturnJson("0", "兑换失败");
        }
        #endregion
    }
}
