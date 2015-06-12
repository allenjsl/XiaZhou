using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.unionpay.upop.sdk;
using System.Text.RegularExpressions;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.unionpay
{
    public partial class Unionpay_Trade : System.Web.UI.Page
    {
        EyouSoft.Model.YlStructure.MWzYuMingInfo YuMingInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            string dingDanId = Request.QueryString["dingdanid"];
            var dingDanLeiXiang = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing>(Utils.GetQueryStringValue("DingDanLeiXing"), EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单);//订单类型

            if (string.IsNullOrEmpty(dingDanId)) Utils.RCWE("错误的请求");

            string token = Utils.GetQueryStringValue("token");
            if (string.IsNullOrEmpty(token)) Utils.RCWE("错误的请求");

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            if (isLogin && huiYuanInfo.HuiYuanId != token) Utils.RCWE("错误的请求");

            decimal zhiFuJinE = 0.0m;
            string zhiFuBiaoTi = "";  //标题
            string zhiFuMiaoShu = ""; //描述
            string strErr=string.Empty;
            string cpName = "";
            string cpUrl ="";
            int dingDanIdentityId = 0;

            switch (dingDanLeiXiang)
            {
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单:
                    strErr = this.GetHangQiDingDan(dingDanId, ref zhiFuBiaoTi, ref zhiFuMiaoShu, ref zhiFuJinE, ref cpName, ref cpUrl, ref dingDanIdentityId);
                    break;
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单:
                    strErr = this.GetJiFenDingDan(dingDanId, ref zhiFuBiaoTi, ref zhiFuMiaoShu, ref zhiFuJinE, ref cpName, ref cpUrl, ref dingDanIdentityId);
                    break;
                default:
                    strErr = "订单类型错误！";
                    break;
            }

            if (!string.IsNullOrEmpty(strErr)) Utils.RCWE(strErr);
            if (dingDanIdentityId == 0) Utils.RCWE("订单编号错误！");

            if (zhiFuJinE <= 0) Utils.RCWE("支付金额必须大于0才能支付！");

            string s = InitUnionpay(dingDanId, zhiFuBiaoTi, zhiFuMiaoShu, zhiFuJinE, cpName, cpUrl, (int)dingDanLeiXiang, dingDanIdentityId);
            
            if(string.IsNullOrEmpty(s)) Utils.RCWE("初始化银联接口失败");

            Response.Write(s);
        }

        /// <summary>
        /// 构造航期订单支付信息
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="totalfee"></param>
        /// <returns></returns>
        private string GetHangQiDingDan(string dingDanId, ref string zhiFuBiaoTi, ref string zhiFuMiaoShu, ref decimal zhiFuJinE, ref string cpName, ref string cpUrl, ref int dingDanIdentityId)
        {
            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            var info = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(dingDanId);

            if (info == null) Utils.RCWE("错误的请求");

            if (info.XiaDanRenId != token) Utils.RCWE("错误的请求");

            if (isLogin)
            {
                if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("错误的请求");
            }

            if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款) Utils.RCWE("错误的请求");
            if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交) Utils.RCWE("错误的请求");

            if (string.IsNullOrEmpty(zhiFuBiaoTi))
            {
                zhiFuBiaoTi = "产品名称：" + info.MingCheng + "，订单号：" + info.JiaoYiHao;
                zhiFuMiaoShu = "产品名称：" + info.MingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
            }
            
            if (info.IsTuanGou)
            {
                cpUrl += "/tuangou/tuangouxiangqing.aspx?tuangouid=" + info.TuanGouId;
            }
            else
            {
                if (info.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                {
                    cpUrl += "/hangqi/haiyanginfo.aspx?id=" + info.HangQiId;
                }
                else if (info.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                {
                    cpUrl += "/hangqi/hangqiinfo.aspx?id=" + info.HangQiId;
                }
            }

            zhiFuJinE += info.JinE;

            cpName = info.MingCheng;
            cpUrl = "http://" + YuMingInfo.YuMing;
            dingDanIdentityId = info.IdentityId;


            return string.Empty;
        }

        /// <summary>
        /// 构造积分订单支付信息
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="totalfee"></param>
        /// <returns></returns>
        private string GetJiFenDingDan(string dingDanId, ref string zhiFuBiaoTi, ref string zhiFuMiaoShu, ref decimal zhiFuJinE, ref string cpName, ref string cpUrl, ref int dingDanIdentityId)
        {
            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(dingDanId);

            if (info == null) Utils.RCWE("错误的请求");

            if (info.XiaDanRenId != token) Utils.RCWE("错误的请求");

            if (isLogin)
            {
                if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("错误的请求");
            }

            if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款) Utils.RCWE("错误的请求");
            if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已成交) Utils.RCWE("错误的请求");

            if (string.IsNullOrEmpty(zhiFuBiaoTi))
            {
                zhiFuBiaoTi = "产品名称：" + info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao;
                zhiFuMiaoShu = "产品名称：" + info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
            }

            zhiFuJinE += info.JinE;

            cpName = info.ShangPinMingCheng;
            cpUrl = "http://" + YuMingInfo.YuMing + "/jifen/jifeninfo.aspx?id=" + info.ShangPinId;
            dingDanIdentityId = info.IdentityId;

            return string.Empty;
        }

        /// <summary>
        /// 初始化银联支付url 
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="biaoTi">标题</param>
        /// <param name="miaoShu">描述</param>
        /// <param name="jinE">总金额</param>
        /// <param name="cpName">产品名称</param>
        /// <param name="cpUrl">产品URL</param>
        /// <param name="dingDanLeiXing">订单类型</param>
        /// <returns></returns>
        private string InitUnionpay(string dingDanId, string biaoTi, string miaoShu, decimal jinE,string cpName,string cpUrl,int dingDanLeiXing,int dingDanIdentityId)
        {
            string returnValue = string.Empty;

            if (jinE > 10000000)  //支付金额不能超过10000000.00RMB
            {
                Response.Write("支付金额不能超过10000000.00");
                Response.End();
            }

            // 用户IP
            string ip = HttpContext.Current.Request.UserHostAddress;
            // 交易类型，前台只支持CONSUME 和 PRE_AUTH
            string strTransType = UPOPSrv.TransType.CONSUME;

            cpName = Regex.Replace(cpName, @"[~!@#\$%\^&\*\(\)\+=\|\\\}\]\{\[:“”，。！（）‘’''"";<,>\?\/" + "\"]+", " ");
            // 商品单价，分为单位
            string commodityUnitPrice = Convert.ToInt32(jinE * 100).ToString();
            // 商品数量
            string shuLiang = "1";
            // 订单号，必须唯一
            //string orderID = dingDanId + "_" + dingDanLeiXing;
            // 交易金额
            string orderAmount = Convert.ToInt32(jinE * 100).ToString();
            // 币种
            string orderCurrency = UPOPSrv.CURRENCY_CNY;
            // 前台回调URL
            string returnUrl = "http://" + YuMingInfo.YuMing + EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("UnionpayReturnUrl");
            // 后台回调URL（前台请求时可为空）
            string notifyUrl = "http://" + YuMingInfo.YuMing + EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("UnionpayNotifyUrl");
            // 要使用各种Srv必须先使用LoadConf载入配置
            string configFilepath = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("UnionpayConfigFilepath");
            UPOPSrv.LoadConf(Server.MapPath(configFilepath));
            // 使用Dictionary保存参数
            System.Collections.Generic.Dictionary<string, string> param = new System.Collections.Generic.Dictionary<string, string>();

            // 订单号，必须唯一
            string orderID1 = DateTime.Now.ToString("yyyyMMddHHmmss") + dingDanLeiXing.ToString() + dingDanIdentityId.ToString();

            // 填写参数
            param["transType"] = strTransType;
            param["commodityUrl"] = cpUrl;
            param["commodityName"] = cpName.Trim();
            param["commodityUnitPrice"] = commodityUnitPrice;
            param["commodityQuantity"] = shuLiang;
            param["orderNumber"] = orderID1;
            param["orderAmount"] = orderAmount;
            param["orderCurrency"] = orderCurrency;
            // 交易时间
            param["orderTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");
            param["customerIp"] = ip;
            param["frontEndUrl"] = returnUrl;
            param["backEndUrl"] = notifyUrl;
            // 创建前台交易服务对象
            FrontPaySrv srv = new FrontPaySrv(param);

            // 将前台交易服务对象产生的Html文档写入页面，从而引导用户浏览器重定向
            Response.ContentEncoding = srv.Charset; // 指定输出编码
            returnValue = srv.CreateHtml();

            return returnValue;
        }
    }
}
