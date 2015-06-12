using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
//测试账号：1001213884201
//测试密钥：123456

//密钥：XCIMKL3KSIF5Z65K
//账号(MerchantAcctID)：1002352616201

namespace EyouSoft.YlWeb._99bill
{
    public partial class send : System.Web.UI.Page
    {
        EyouSoft.Model.YlStructure.MWzYuMingInfo YuMingInfo = null;

        #region 参数说明
        //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
        public string merchantAcctId = "1002352616201";
        //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
        public string inputCharset = "1";
        //接收支付结果的页面地址，该参数一般置为空即可。
        public string pageUrl = "";
        //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
        public string bgUrl = "http://219.233.173.50:8804/yixiao/rmb/receive.aspx";
        //网关版本，固定值：v2.0,该参数必填。
        public string version = "v2.0";
        //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
        public string language = "1";
        //签名类型,该值为4，代表PKI加密方式,该参数必填。
        public string signType = "4";
        //支付人姓名,可以为空。
        public string payerName = "雪亮";
        //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
        public string payerContactType = "1";
        //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
        public string payerContact = "2532987@qq.com";
        //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
        public string orderId = DateTime.Now.ToString("yyyyMMddHHmmss");
        //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
        public string orderAmount = "1";
        //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
        public string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        //商品名称，可以为空。
        public string productName = "苹果";
        //商品数量，可以为空。
        public string productNum = "5";
        //商品代码，可以为空。
        public string productId = "55558888";
        //商品描述，可以为空。
        public string productDesc = "";
        //扩展字段1，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
        public string ext1 = "";
        //扩展自段2，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
        public string ext2 = "";
        //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10，必填。
        public string payType = "00";
        //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
        public string bankId = "";
        //同一订单禁止重复提交标志，实物购物车填1，虚拟产品用0。1代表只能提交一次，0代表在支付不成功情况下可以再提交。可为空。
        public string redoFlag = "0";
        //快钱合作伙伴的帐户号，即商户编号，可为空。
        public string pid = "";
        // signMsg 签名字符串 不可空，生成加密签名串
        public string signMsg = "";

        /// <summary>
        /// 银行直连 1银行直连
        /// </summary>
        public string BANK_ZL = "0";
        /// <summary>
        /// 银行直连 银行代码
        /// </summary>
        public string BANK_ID = "";
        /// <summary>
        /// 快钱快捷支付 1快捷支付
        /// </summary>
        public string BANK_KJ = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            BANK_ZL = Utils.GetQueryStringValue("bank_zl");
            BANK_ID = Utils.GetQueryStringValue("bank_id");
            BANK_KJ = Utils.GetQueryStringValue("bank_kj");

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
            string strErr = string.Empty;
            string cpName = "";
            string dingDanHao = string.Empty;

            switch (dingDanLeiXiang)
            {
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单:
                    strErr = this.GetHangQiDingDan(dingDanId, ref zhiFuBiaoTi, ref zhiFuMiaoShu, ref zhiFuJinE, ref cpName, ref dingDanHao);
                    break;
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单:
                    strErr = this.GetJiFenDingDan(dingDanId, ref zhiFuBiaoTi, ref zhiFuMiaoShu, ref zhiFuJinE, ref cpName, ref dingDanHao);
                    break;
                default:
                    strErr = "订单类型错误！";
                    break;
            }

            if (!string.IsNullOrEmpty(strErr)) Utils.RCWE(strErr);

            if (zhiFuJinE <= 0) Utils.RCWE("支付金额必须大于0才能支付！");

            Init99bill(dingDanId, zhiFuBiaoTi, zhiFuMiaoShu, zhiFuJinE, cpName,  (int)dingDanLeiXiang,dingDanHao);
        }

        /// <summary>
        /// 构造航期订单支付信息
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="totalfee"></param>
        /// <returns></returns>
        private string GetHangQiDingDan(string dingDanId, ref string zhiFuBiaoTi, ref string zhiFuMiaoShu, ref decimal zhiFuJinE, ref string cpName, ref string dingDanHao)
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
                zhiFuBiaoTi = info.MingCheng + "，订单号：" + info.JiaoYiHao;
                zhiFuMiaoShu = "产品名称：" + info.MingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
            }

            zhiFuJinE += info.JinE;

            cpName = info.MingCheng;
            dingDanHao = info.JiaoYiHao;

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
        private string GetJiFenDingDan(string dingDanId, ref string zhiFuBiaoTi, ref string zhiFuMiaoShu, ref decimal zhiFuJinE, ref string cpName, ref string dingDanHao)
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
                zhiFuBiaoTi = info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao;
                zhiFuMiaoShu = "产品名称：" + info.ShangPinMingCheng + "，订单号：" + info.JiaoYiHao + "，总金额：" + info.JinE.ToString("F2") + " 元";
            }

            zhiFuJinE += info.JinE;

            cpName = info.ShangPinMingCheng;            
            dingDanHao = info.JiaoYiHao;

            return string.Empty;
        }

        private string Init99bill(string dingDanId, string biaoTi, string miaoShu, decimal jinE, string cpName, int dingDanLeiXing, string dingDanHao)
        {
            orderAmount = ((int)Math.Round(jinE * 100, 0)).ToString();
            productName = biaoTi;
            productNum = "1";
            orderId = dingDanHao;
            productDesc = miaoShu;
            productId = string.Empty;
            ext1 = dingDanId;
            ext2 = dingDanLeiXing.ToString();
            payerContactType = string.Empty;
            payerContact = string.Empty;
            payerName = string.Empty;
            bgUrl = "http://" + YuMingInfo.YuMing + "/99bill/receive.aspx";

            if (BANK_ZL == "1" && !string.IsNullOrEmpty(BANK_ID))
            {
                payType = "10";
                bankId = BANK_ID;
            }

            if (BANK_KJ == "1")
            {
                payType = "21";
            }

            //拼接字符串
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
            signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl);
            signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "payerName", payerName);
            signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType);
            signMsgVal = appendParam(signMsgVal, "payerContact", payerContact);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "productName", productName);
            signMsgVal = appendParam(signMsgVal, "productNum", productNum);
            signMsgVal = appendParam(signMsgVal, "productId", productId);
            signMsgVal = appendParam(signMsgVal, "productDesc", productDesc);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            if (payType == "10")
            {
                signMsgVal = appendParam(signMsgVal, "bankId", bankId);
            }
            signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
            signMsgVal = appendParam(signMsgVal, "pid", pid);

            ///PKI加密
            ///编码方式UTF-8 GB2312  用户可以根据自己系统的编码选择对应的加密方式
            ///byte[] OriginalByte=Encoding.GetEncoding("GB2312").GetBytes(OriginalString);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
            X509Certificate2 cert = new X509Certificate2(HttpContext.Current.Server.MapPath("99bill-rsa.pfx"), "123456", X509KeyStorageFlags.MachineKeySet);
            //X509Certificate2 cert = new X509Certificate2(HttpContext.Current.Server.MapPath("tester-rsa.pfx"), "123456", X509KeyStorageFlags.MachineKeySet);
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PrivateKey;
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);
            signMsg = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();

            return string.Empty;
        }

        //功能函数。将变量值不为空的参数组成字符串
        #region 字符串串联函数
        public string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + paramValue;
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }
            return returnStr;
        }
        #endregion
    }
}
