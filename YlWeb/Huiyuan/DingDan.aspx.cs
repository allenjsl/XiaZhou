using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.EnumType.YlStructure;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class DingDan : EyouSoft.YlWeb.HuiYuanPage
    {
        protected int pageSize = 10;
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.InitRpt();
            if (Utils.GetQueryStringValue("dotype") == "dp") DianPing();
            if (Utils.GetQueryStringValue("dotype") == "getdp") GetDianPing();

            InitInfo();
        }

        /*void InitRpt()
        {
            var l = new BLL.YlStructure.BHuiYuan().GetHuiYuanJiFenMingXis(this.HuiYuanInfo.HuiYuanId, pageSize, pageIndex, ref recordCount, this.GetChaXun());

            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
            else
            {
                this.phdNoDat.Visible = true;
            }
        }

        MHuiYuanJiFenMxChaXunInfo GetChaXun()
        {
            var m = new MHuiYuanJiFenMxChaXunInfo()
            {
                XiaDanRenId = this.HuiYuanInfo.HuiYuanId
            };
            return m;
        }
        /// <summary>
        /// 支付按钮绑定
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string BindOrderState(object orderId, object Leixing, object FuKuanStatus, object DingDanStatus)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(FuKuanStatus.ToString())) return sb.ToString();
            int OderyPayLeiXing =0;
            
            switch ((HuiYuanShouCangLeiXing)(int)Leixing)
            {
                case HuiYuanShouCangLeiXing.长江游轮:
                    OderyPayLeiXing =(int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单;
                    break;
                case HuiYuanShouCangLeiXing.海洋游轮:
                    OderyPayLeiXing = (int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单;
                    break;
                case HuiYuanShouCangLeiXing.积分兑换:
                    OderyPayLeiXing = (int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单;
                    break;
                case HuiYuanShouCangLeiXing.团购产品:
                    OderyPayLeiXing = (int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单;
                    break;
                default:
                    break;
            }
            int FuKuan = (int)FuKuanStatus;
            int DingDan = (int)DingDanStatus;
            string className = DingDan == (int)EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交 && FuKuan == (int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款 ? "fukuan" : (FuKuan == (int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款 ? "green" : "color_b");
            string url = "href=\"javascript:void(0);\"";
            string name = DingDan == (int)EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交 && FuKuan == (int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款 ? "付款" : DingDanStatus.ToString();
            if (DingDan == (int)EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交 && FuKuan == (int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款)
                url = string.Format("href='/Hangqi/OrderPay.aspx?leixing={0}&orderid={1}&token={2}'", OderyPayLeiXing, orderId.ToString(), HuiYuanInfo.HuiYuanId);

            sb.AppendFormat("<a target='_blank' class='{0}' {1}>{2}</a>", className, url, name);

            return sb.ToString();
        }
        */

        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanDingDanChaXunInfo GetChaXunInfo()
        {
            return null;
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanDingDans(HuiYuanInfo.HuiYuanId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                StringBuilder s = new StringBuilder();
                foreach (var item in items)
                {
                    string s1 = string.Empty;//cp url
                    string s2 = string.Empty;//dingdan status
                    string s3 = string.Empty;//zhifu url
                    string s4 = string.Empty;//dingdan url
                    var dp = string.Empty;//点评
                    decimal jiFen = 0;

                    s3 = string.Format("/Hangqi/OrderPay.aspx?dingdanleixing={0}&dingdanid={1}&token={2}", (int)item.DingDanLeiXing, item.DingDanId, item.HuiYuanId);


                    #region s1
                    if (item.DingDanLeiXing == DingDanLeiXing.航期订单)
                    {
                        if (item.IsTuanGou)
                        {
                            s1 = "/tuangou/tuangouxiangqing.aspx?tuangouid=" + item.TuanGouId;
                        }
                        else
                        {
                            if (item.HQLeiXing.Value == YouLunLeiXing.长江游轮)
                            {
                                s1 = "/hangqi/" + item.CPId+".html";
                            }
                            else if (item.HQLeiXing.Value == YouLunLeiXing.海洋邮轮)
                            {
                                s1 = "/hangqi/HY" + item.CPId+".html";
                            }
                        }

                        jiFen = item.JiFen;
                    }
                    else if (item.DingDanLeiXing == DingDanLeiXing.兑换订单)
                    {
                        s1 = "/jifen/jifeninfo.aspx?id=" + item.CPId;
                        jiFen = 0 - item.JiFen;
                    }
                    #endregion

                    #region s2
                    if (item.FuKuanStatus == FuKuanStatus.未付款)
                    {
                        if (item.DingDanLeiXing == DingDanLeiXing.兑换订单)
                        {
                            switch (item.JFStatus.Value)
                            {
                                case JiFenDingDanStatus.未处理: s2 = "<font>审核中</font>"; break;
                                case JiFenDingDanStatus.已成交: s2 = string.Format("<a class='fukuan' href='{0}'>付款</a>", s3); break;
                                case JiFenDingDanStatus.已取消: s2 = "<font class='color_b'>已取消</font>"; break;
                            }
                        }
                        else if (item.DingDanLeiXing == DingDanLeiXing.航期订单)
                        {
                            switch (item.HQStatus.Value)
                            {
                                case HangQiDingDanStatus.不受理: s2 = "<font>审核中</font>"; break;
                                case HangQiDingDanStatus.成交: s2 = string.Format("<a class='fukuan' href='{0}'>付款</a>", s3); break;
                                case HangQiDingDanStatus.留位:s2 = "<font>审核中</font>"; break;
                                case HangQiDingDanStatus.留位过期: s2 = "<font>审核中</font>"; break;
                                case HangQiDingDanStatus.取消: s2 = "<font class='color_b'>已取消</font>"; break;
                                case HangQiDingDanStatus.审核中: s2 = "<font>审核中</font>"; break;
                                case HangQiDingDanStatus.未处理: s2 = "<font>审核中</font>"; break;
                            }
                            dp = "<span>&nbsp;<a href='javascript:void(0)' class='i_dianping'>【点评】</a></span>";
                        }
                    }
                    else if (item.FuKuanStatus == FuKuanStatus.已付款)
                    {
                        if (item.DingDanLeiXing == DingDanLeiXing.航期订单)
                        {
                            s2 = "<font class='green'>已完成</font>";
                            dp = "<span>&nbsp;<a href='javascript:void(0)' class='i_dianping'>【点评】</a></span>";
                        }
                        else
                        {
                            s2 = "<font class='green'>已完成</font>";
                        }
                    }
                    #endregion

                    #region s4
                    if (item.DingDanLeiXing == DingDanLeiXing.航期订单)
                    {
                        s4 = "/hangqi/dingdanxx.aspx?dingdanleixing=" + (int)item.DingDanLeiXing + "&dingdanid=" + item.DingDanId + "&token=" + item.HuiYuanId;
                    }
                    else if (item.DingDanLeiXing == DingDanLeiXing.兑换订单)
                    {
                        s4 = "/hangqi/jifendingdanxx.aspx?dingdanleixing=" + (int)item.DingDanLeiXing + "&dingdanid=" + item.DingDanId + "&token=" + item.HuiYuanId;
                    }
                    #endregion

                    s.AppendFormat("<tr i_dingdanid='{0}' i_dingdanleixing='{1}'>", item.DingDanId, (int)item.DingDanLeiXing);

                    s.AppendFormat("<td align='left'><a class='blue' target='_blank' href='{1}'>{0}</a> <a class='price_fontred' href='{2}'>【详情】</a>{3}</td>", item.CPName, s1, s4, dp);
                    s.AppendFormat("<td align='center'><b class='price_fontred font14'>{0}</b></td>", item.JinE.ToString("F2"));
                    s.AppendFormat("<td align='center'><b class='font14 font_yellow'>{0}</b></td>", jiFen.ToString("F2"));
                    s.AppendFormat("<td align=\"center\">{0}</td>", s2);

                    s.Append("</tr>");
                }
                ltr0.Text = s.ToString();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
            else
            {
                this.phdNoDat.Visible = true;
            }

        }

        void DianPing()
        {
            var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            EyouSoft.Model.YlStructure.MWzDianPingInfo info = null;

            string txtDianPingId = Utils.GetFormValue("txtDianPingId");
            if (!string.IsNullOrEmpty(txtDianPingId))
            {
                info = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDianPingInfo(txtDianPingId);
            }

            if (info == null)
            {
                info = new MWzDianPingInfo();

                info.ChuanZhiId = string.Empty;
                info.CompanyId = yuMingInfo.CompanyId;
                info.DianPingId = string.Empty;
                info.DingDanId = Utils.GetFormValue("txtDingDanId");
                info.DingDanLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing>(Utils.GetFormValue("txtDingDanLeiXing"), DingDanLeiXing.航期订单);
                info.GongSiId = string.Empty;
                info.GysId = string.Empty;
                info.HangQiId = string.Empty;
                info.IsNiMing = false;
                info.IsShenHe = false;
                info.IssueTime = DateTime.Now;
                info.OperatorId = HuiYuanInfo.HuiYuanId;
                info.RiQiId = string.Empty;
                info.ShenHeOperatorId = string.Empty;
                info.ShenHeTime = null;
                info.XiLieId = string.Empty;
            }

            info.FenShu = Utils.GetDecimal(Utils.GetFormValue("txtFenShu"));
            info.NeiRong = Utils.GetFormValue("txtNeiRong");
            info.BiaoTi = Utils.GetFormValue("txtBiaoTi");
            info.IsShenHe = false;

            if (info.DingDanLeiXing == DingDanLeiXing.航期订单)
            {
                var dingDanInfo = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(info.DingDanId);
                info.HangQiId = dingDanInfo.HangQiId;
            }

            if (string.IsNullOrEmpty(info.DianPingId))
                new EyouSoft.BLL.YlStructure.BHuiYuan().InsertDianPing(info);
            else
                new EyouSoft.BLL.YlStructure.BHuiYuan().UpdateDianPing(info);

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "点评成功"));
        }

        void GetDianPing()
        {
            string dingDanId = Utils.GetFormValue("txtDingDanId");
            var info = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDianPingInfo1(dingDanId);

            if (info == null) Utils.RCWE(UtilsCommons.AjaxReturnJson("0"));

            var d = new { DianPingId = info.DianPingId, BiaoTi = info.BiaoTi, NeiRong = info.NeiRong, FenShu = (int)Math.Ceiling(info.FenShu), IsShenHe = info.IsShenHe };

            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "", d));
        }
    }
}
