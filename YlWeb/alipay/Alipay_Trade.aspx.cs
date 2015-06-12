using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;

namespace YlWeb.alipay
{
    /// <summary>
    /// 支付宝支付的页面
    /// </summary>
    public partial class Alipay_Trade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dingDanId = Request.QueryString["dingdanid"];  //若为多个订单ID,则以逗号分隔     
            var dingDanLeiXiang = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing>(Utils.GetQueryStringValue("DingDanLeiXing"), EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单);//订单类型

            if (string.IsNullOrEmpty(dingDanId)) Utils.RCWE("错误的请求");

            string token = Utils.GetQueryStringValue("token");
            if (string.IsNullOrEmpty(token)) Utils.RCWE("错误的请求");

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            if (isLogin && huiYuanInfo.HuiYuanId != token) Utils.RCWE("错误的请求");

            string[] dingDanIds = dingDanId.Split(',');

            decimal totalfee = 0.0m;
            string subject = "";  //标题
            string body = ""; //描述
            var orderList = new List<string>();

            string strErr;

            switch (dingDanLeiXiang)
            {
                case  EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单:
                    strErr = this.GetHangQiDingDan(dingDanIds, ref subject, ref body, ref totalfee, orderList);
                    break;
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单:
                    strErr = this.GetJiFenDingDan(dingDanIds, ref subject, ref body, ref totalfee, orderList);
                    break;
                default:
                    strErr = "订单类型错误！";
                    break;
            }

            if (!string.IsNullOrEmpty(strErr)) Utils.RCWE(strErr);

            if (totalfee <= 0) Utils.RCWE("支付金额必须大于0才能支付！");

            if (dingDanIds.Length > 1)  //数量大于1时，已省略号显示
            {
                subject += "......";
                body += "......";
            }

            //开始支付
            string url = this.InitAliPay(orderList, subject, body, totalfee, (int)dingDanLeiXiang);
            if (string.IsNullOrEmpty(url)) Utils.RCWE("初始化支付宝接口失败");

            Response.Redirect(url);                
        }

        /// <summary>
        /// 构造航期订单支付信息
        /// </summary>
        /// <param name="arrstrOrderId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="totalfee"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        private string GetHangQiDingDan(string[] arrstrOrderId, ref string subject, ref string body, ref decimal totalfee
            , List<string> orderList)
        {
            if (arrstrOrderId == null || arrstrOrderId.Length <= 0 || arrstrOrderId.Length > 1) return "要支付的订单不存在！";

            if (orderList == null) orderList = new List<string>();

            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            foreach (string strId in arrstrOrderId)
            {
                if (string.IsNullOrEmpty(strId)) continue;

                var info = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(strId);

                if (info == null) continue;

                if (info.XiaDanRenId != token) Utils.RCWE("错误的请求");

                if (isLogin)
                {
                    if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("错误的请求");
                }

                if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款) Utils.RCWE("错误的请求");
                if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交) Utils.RCWE("错误的请求");

                if (string.IsNullOrEmpty(subject))
                {
                    subject = "产品名称：" + info.MingCheng + "，订单号：" + info.JiaoYiHao;
                    body = "产品名称：" + info.MingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
                }

                totalfee += info.JinE;

                orderList.Add(strId);  //订单ID
            }

            return string.Empty;
        }

        /// <summary>
        /// 构造积分订单支付信息
        /// </summary>
        /// <param name="arrstrOrderId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="totalfee"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        private string GetJiFenDingDan(string[] arrstrOrderId, ref string subject, ref string body, ref decimal totalfee
            , List<string> orderList)
        {
            if (arrstrOrderId == null || arrstrOrderId.Length <= 0) return "要支付的订单不存在！";

            if (orderList == null) orderList = new List<string>();

            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            foreach (string strId in arrstrOrderId)
            {
                if (string.IsNullOrEmpty(strId)) continue;

                var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(strId);

                if (info == null) continue;

                if (info.XiaDanRenId != token) Utils.RCWE("错误的请求");

                if (isLogin)
                {
                    if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("错误的请求");
                }

                if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款) continue;
                if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已成交) continue;

                if (string.IsNullOrEmpty(subject))
                {
                    subject = "产品名称：" + info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao;
                    body = "产品名称：" + info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
                }

                totalfee += info.JinE;

                orderList.Add(strId);  //订单ID
            }

            return string.Empty;
        }

        /// <summary>
        /// 初始化支付宝支付url
        /// </summary>
        /// <param name="orderList">订单idlist</param>
        /// <param name="Subject">标题</param>
        /// <param name="Body">描述</param>
        /// <param name="Totalfee">总金额</param>
        /// <param name="OrderType">订单类型</param>
        /// <returns></returns>
        private string InitAliPay(List<string> orderList, string Subject, string Body, decimal Totalfee, int OrderType)
        {
            PayAPI.Model.Ali.AliPayTrade trade = new PayAPI.Model.Ali.AliPayTrade();
            trade.OrderInfo.OrderID = orderList;
            trade.OrderInfo.Subject = Subject;
            trade.OrderInfo.Body = Body;
            trade.Totalfee = Totalfee;
            trade.IsRoyalty = false;
            trade.RoyaltyType = PayAPI.Model.Ali.RoyaltyType.平级分润;

            //PayAPI.Model.Ali.Royalty roy1 = new PayAPI.Model.Ali.Royalty();
            //roy1.Account = PayAPI.Ali.Core.AliPaySystem.Account;  //分润账号
            //if ((decimal)PayAPI.Ali.Core.AliPaySystem.ServiceFeePercent == 0)
            //    roy1.Price = trade.Totalfee * 0.005m;
            //else
            //    roy1.Price = trade.Totalfee * (decimal)PayAPI.Ali.Core.AliPaySystem.ServiceFeePercent;
            //roy1.Remark = "收取的手续费";
            //trade.RoyaltyList.Add(roy1);
            trade.SellerAccount = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("AlipayAccount");   //卖家账号           
            trade.ShowUrl = "";  //展示页面
            PayAPI.Model.Attach attach = new PayAPI.Model.Attach();
            attach.Key = "OrderType";
            attach.Value = OrderType.ToString();
            trade.AttachList.Add(attach);
            //构造url
            return PayAPI.Ali.Alipay.Create.Create_url(trade);
        }
    }
}
