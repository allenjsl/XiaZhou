using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using EyouSoft.Common;

namespace EyouSoft.YlWeb._99bill
{
    public partial class receive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //人民币网关账号，该账号为11位人民币网关商户编号+01,该值与提交时相同。
            string merchantAcctId = Request.QueryString["merchantAcctId"].ToString();
            //网关版本，固定值：v2.0,该值与提交时相同。
            string version = Request.QueryString["version"].ToString();
            //语言种类，1代表中文显示，2代表英文显示。默认为1,该值与提交时相同。
            string language = Request.QueryString["language"].ToString();
            //签名类型,该值为4，代表PKI加密方式,该值与提交时相同。
            string signType = Request.QueryString["signType"].ToString();
            //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10,该值与提交时相同。
            string payType = Request.QueryString["payType"].ToString();
            //银行代码，如果payType为00，该值为空；如果payType为10,该值与提交时相同。
            string bankId = Request.QueryString["bankId"].ToString();
            //商户订单号，,该值与提交时相同。
            string orderId = Request.QueryString["orderId"].ToString();
            //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101,该值与提交时相同。
            string orderTime = Request.QueryString["orderTime"].ToString();
            //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试,该值与支付时相同。
            string orderAmount = Request.QueryString["orderAmount"].ToString();
            // 快钱交易号，商户每一笔交易都会在快钱生成一个交易号。
            string dealId = Request.QueryString["dealId"].ToString();
            //银行交易号 ，快钱交易在银行支付时对应的交易号，如果不是通过银行卡支付，则为空
            string bankDealId = Request.QueryString["bankDealId"].ToString();
            //快钱交易时间，快钱对交易进行处理的时间,格式：yyyyMMddHHmmss，如：20071117020101
            string dealTime = Request.QueryString["dealTime"].ToString();
            //商户实际支付金额 以分为单位。比方10元，提交时金额应为1000。该金额代表商户快钱账户最终收到的金额。
            string payAmount = Request.QueryString["payAmount"].ToString();
            //费用，快钱收取商户的手续费，单位为分。
            string fee = Request.QueryString["fee"].ToString();
            //扩展字段1，该值与提交时相同。
            string ext1 = Request.QueryString["ext1"].ToString();
            //扩展字段2，该值与提交时相同。
            string ext2 = Request.QueryString["ext2"].ToString();
            //处理结果， 10支付成功，11 支付失败，00订单申请成功，01 订单申请失败
            string payResult = Request.QueryString["payResult"].ToString();
            //错误代码 ，请参照《人民币网关接口文档》最后部分的详细解释。
            string errCode = Request.QueryString["errCode"].ToString();
            //签名字符串 
            string signMsg = Request.QueryString["signMsg"].ToString();
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            signMsgVal = appendParam(signMsgVal, "bankId", bankId);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "dealId", dealId);
            signMsgVal = appendParam(signMsgVal, "bankDealId", bankDealId);
            signMsgVal = appendParam(signMsgVal, "dealTime", dealTime);
            signMsgVal = appendParam(signMsgVal, "payAmount", payAmount);
            signMsgVal = appendParam(signMsgVal, "fee", fee);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payResult", payResult);
            signMsgVal = appendParam(signMsgVal, "errCode", errCode);

            ///UTF-8编码  GB2312编码  用户可以根据自己网站的编码格式来选择加密的编码方式
            ///byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
            byte[] SignatureByte = Convert.FromBase64String(signMsg);
            X509Certificate2 cert = new X509Certificate2(Server.MapPath("99bill.cert.rsa.20140728.cer"), "");
            //X509Certificate2 cert = new X509Certificate2(Server.MapPath("99bill[1].cert.rsa.20140803.cer"), "");
            //X509Certificate2 cert = new X509Certificate2(Server.MapPath("99bill.cert.rsa.20340630.cer"), "");
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PublicKey.Key;
            rsapri.ImportCspBlob(rsapri.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);

            if (f.VerifySignature(result, SignatureByte))
            {
                string rurl = string.Empty;
                var YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
                rurl = "http://" + YuMingInfo.YuMing;

                //逻辑处理  写入数据库
                if (payResult == "10")
                {
                    //此处做商户逻辑处理
                    var info = new EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo();//在线支付实体
                    var onlinepay = new EyouSoft.BLL.YlStructure.BZaiXianZhiFu();//在线支付BLL

                    info.DingDanId = ext1;//订单ID
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)Convert.ToInt32(ext2);//订单类型
                    bool ispay = onlinepay.IsZhiFu(info.DingDanId, info.DingDanLeiXing);//获取订单支付状态（成功/失败）

                    if (info.DingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单)
                    {
                        var dingdaninfo = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(info.DingDanId);
                        if (dingdaninfo != null)
                        {
                            rurl += "/hangqi/dingdanxx.aspx?dingdanid=" + info.DingDanId + "&dingdanleixing=" + (int)info.DingDanLeiXing + "&token=" + dingdaninfo.XiaDanRenId;
                        }
                    }
                    else if (info.DingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单)
                    {
                        var dingdaninfo = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(info.DingDanId);
                        rurl += "/hangqi/JiFenDingDanXX.aspx?dingdanid=" + info.DingDanId + "&dingdanleixing=" + (int)info.DingDanLeiXing + "&token=" + dingdaninfo.XiaDanRenId;
                    }

                    if (!ispay)
                    {
                        info.JiaoYiHao = orderId;//流水号
                        info.ApiJiaoYiHao = dealId;//支付流水号
                        info.JinE = Utils.GetDecimal(orderAmount) / 100M;//支付金额
                        info.ZhiFuFangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Bill99;//支付方式
                        info.IsZhiFu = true;//是否已支付
                        info.ZhiFuTime = DateTime.Now;//支付时间
                        int bllRetCode = onlinepay.Insert(info);//添加支付记录
                        if (bllRetCode == 1)
                        {
                            //实现其它操作处理 
                            switch (info.DingDanLeiXing)
                            {
                                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单:
                                    HandlerJiFenDingDan(info.DingDanId);
                                    break;
                                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单:
                                    HandlerHangQiDingDan(info.DingDanId);
                                    break;
                            }
                        }
                    }                    

                    //以下是我们快钱设置的show页面，商户需要自己定义该页面。
                    Response.Write("<result>1</result>" + "<redirecturl>" + rurl + "</redirecturl>");
                }
                else
                {
                    //以下是我们快钱设置的show页面，商户需要自己定义该页面。
                    Response.Write("<result>1</result>" + "<redirecturl>" + rurl + "</redirecturl>");
                    //Response.Write("signMsgVal=" + "(" + signMsgVal + ")");
                    //Response.Write("</br>" + "signMsg =" + signMsg);
                    //Response.Write("</br>" + "错误");
                }
            }
            else
            {
                Response.Write("signMsgVal=" + "(" + signMsgVal + ")");
                Response.Write("</br>" + "signMsg =" + signMsg);
                Response.Write("</br>" + "错误");
            }
        }

        //功能函数。将变量值不为空的参数组成字符串
        String appendParam(String returnStr, String paramId, String paramValue)
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

        #region private members
        /// <summary>
        /// 处理航期订单信息
        /// </summary>
        /// <param name="orderId"></param>
        private void HandlerHangQiDingDan(string orderId)
        {
            if (string.IsNullOrEmpty(orderId)) return;

            var info = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(orderId);

            if (info == null) return;

            var kuanInfo = new EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo();
            kuanInfo.BeiZhu = string.Empty;
            kuanInfo.DingDanId = info.DingDanId;
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Bill99;
            kuanInfo.FuKuanId = string.Empty;
            kuanInfo.IssueTime = DateTime.Now;
            kuanInfo.JinE = info.JinE;
            kuanInfo.OperatorId = info.XiaDanRenId;
            kuanInfo.ShiJian = DateTime.Now;
            kuanInfo.Status = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款;

            new EyouSoft.BLL.YlStructure.BHangQiDingDan().SheZhiFuKuanStatus(info.DingDanId, info.XiaDanRenId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款, DateTime.Now, kuanInfo);

        }

        /// <summary>
        /// 处理积分订单信息
        /// </summary>
        /// <param name="orderId"></param>
        private void HandlerJiFenDingDan(string orderId)
        {
            if (string.IsNullOrEmpty(orderId)) return;

            var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(orderId);

            if (info == null) return;

            var kuanInfo = new EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo();
            kuanInfo.BeiZhu = string.Empty;
            kuanInfo.DingDanId = info.DingDanId;
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Bill99;
            kuanInfo.FuKuanId = string.Empty;
            kuanInfo.IssueTime = DateTime.Now;
            kuanInfo.JinE = info.JinE;
            kuanInfo.OperatorId = info.XiaDanRenId;
            kuanInfo.ShiJian = DateTime.Now;
            kuanInfo.Status = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款;

            new EyouSoft.BLL.YlStructure.BDuiHuan().SheZhiJiFenDingDanFuKuanStatus(info.DingDanId, info.XiaDanRenId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款, DateTime.Now, kuanInfo);

        }
        #endregion
    }
}
