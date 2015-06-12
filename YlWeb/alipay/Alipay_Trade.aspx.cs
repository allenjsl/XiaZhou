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
    /// ֧����֧����ҳ��
    /// </summary>
    public partial class Alipay_Trade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dingDanId = Request.QueryString["dingdanid"];  //��Ϊ�������ID,���Զ��ŷָ�     
            var dingDanLeiXiang = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing>(Utils.GetQueryStringValue("DingDanLeiXing"), EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.���ڶ���);//��������

            if (string.IsNullOrEmpty(dingDanId)) Utils.RCWE("���������");

            string token = Utils.GetQueryStringValue("token");
            if (string.IsNullOrEmpty(token)) Utils.RCWE("���������");

            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            if (isLogin && huiYuanInfo.HuiYuanId != token) Utils.RCWE("���������");

            string[] dingDanIds = dingDanId.Split(',');

            decimal totalfee = 0.0m;
            string subject = "";  //����
            string body = ""; //����
            var orderList = new List<string>();

            string strErr;

            switch (dingDanLeiXiang)
            {
                case  EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.���ڶ���:
                    strErr = this.GetHangQiDingDan(dingDanIds, ref subject, ref body, ref totalfee, orderList);
                    break;
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.�һ�����:
                    strErr = this.GetJiFenDingDan(dingDanIds, ref subject, ref body, ref totalfee, orderList);
                    break;
                default:
                    strErr = "�������ʹ���";
                    break;
            }

            if (!string.IsNullOrEmpty(strErr)) Utils.RCWE(strErr);

            if (totalfee <= 0) Utils.RCWE("֧�����������0����֧����");

            if (dingDanIds.Length > 1)  //��������1ʱ����ʡ�Ժ���ʾ
            {
                subject += "......";
                body += "......";
            }

            //��ʼ֧��
            string url = this.InitAliPay(orderList, subject, body, totalfee, (int)dingDanLeiXiang);
            if (string.IsNullOrEmpty(url)) Utils.RCWE("��ʼ��֧�����ӿ�ʧ��");

            Response.Redirect(url);                
        }

        /// <summary>
        /// ���캽�ڶ���֧����Ϣ
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
            if (arrstrOrderId == null || arrstrOrderId.Length <= 0 || arrstrOrderId.Length > 1) return "Ҫ֧���Ķ��������ڣ�";

            if (orderList == null) orderList = new List<string>();

            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            foreach (string strId in arrstrOrderId)
            {
                if (string.IsNullOrEmpty(strId)) continue;

                var info = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanInfo(strId);

                if (info == null) continue;

                if (info.XiaDanRenId != token) Utils.RCWE("���������");

                if (isLogin)
                {
                    if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("���������");
                }

                if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.δ����) Utils.RCWE("���������");
                if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.�ɽ�) Utils.RCWE("���������");

                if (string.IsNullOrEmpty(subject))
                {
                    subject = "��Ʒ���ƣ�" + info.MingCheng + "�������ţ�" + info.JiaoYiHao;
                    body = "��Ʒ���ƣ�" + info.MingCheng + "�������ţ�" + info.JiaoYiHao + "���ܽ�" + info.JinE.ToString("F2") + " Ԫ";
                }

                totalfee += info.JinE;

                orderList.Add(strId);  //����ID
            }

            return string.Empty;
        }

        /// <summary>
        /// ������ֶ���֧����Ϣ
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
            if (arrstrOrderId == null || arrstrOrderId.Length <= 0) return "Ҫ֧���Ķ��������ڣ�";

            if (orderList == null) orderList = new List<string>();

            string token = Utils.GetQueryStringValue("token");
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo huiYuanInfo;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out huiYuanInfo);

            foreach (string strId in arrstrOrderId)
            {
                if (string.IsNullOrEmpty(strId)) continue;

                var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenDingDanInfo(strId);

                if (info == null) continue;

                if (info.XiaDanRenId != token) Utils.RCWE("���������");

                if (isLogin)
                {
                    if (info.XiaDanRenId != huiYuanInfo.HuiYuanId) Utils.RCWE("���������");
                }

                if (info.FuKuanStatus != EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.δ����) continue;
                if (info.DingDanStatus != EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.�ѳɽ�) continue;

                if (string.IsNullOrEmpty(subject))
                {
                    subject = "��Ʒ���ƣ�" + info.ShangPinMingCheng + "�������ţ�" + info.JiaoYiHao;
                    body = "��Ʒ���ƣ�" + info.ShangPinMingCheng + "�������ţ�" + info.JiaoYiHao + "���ܽ�" + info.JinE.ToString("F2") + " Ԫ";
                }

                totalfee += info.JinE;

                orderList.Add(strId);  //����ID
            }

            return string.Empty;
        }

        /// <summary>
        /// ��ʼ��֧����֧��url
        /// </summary>
        /// <param name="orderList">����idlist</param>
        /// <param name="Subject">����</param>
        /// <param name="Body">����</param>
        /// <param name="Totalfee">�ܽ��</param>
        /// <param name="OrderType">��������</param>
        /// <returns></returns>
        private string InitAliPay(List<string> orderList, string Subject, string Body, decimal Totalfee, int OrderType)
        {
            PayAPI.Model.Ali.AliPayTrade trade = new PayAPI.Model.Ali.AliPayTrade();
            trade.OrderInfo.OrderID = orderList;
            trade.OrderInfo.Subject = Subject;
            trade.OrderInfo.Body = Body;
            trade.Totalfee = Totalfee;
            trade.IsRoyalty = false;
            trade.RoyaltyType = PayAPI.Model.Ali.RoyaltyType.ƽ������;

            //PayAPI.Model.Ali.Royalty roy1 = new PayAPI.Model.Ali.Royalty();
            //roy1.Account = PayAPI.Ali.Core.AliPaySystem.Account;  //�����˺�
            //if ((decimal)PayAPI.Ali.Core.AliPaySystem.ServiceFeePercent == 0)
            //    roy1.Price = trade.Totalfee * 0.005m;
            //else
            //    roy1.Price = trade.Totalfee * (decimal)PayAPI.Ali.Core.AliPaySystem.ServiceFeePercent;
            //roy1.Remark = "��ȡ��������";
            //trade.RoyaltyList.Add(roy1);
            trade.SellerAccount = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("AlipayAccount");   //�����˺�           
            trade.ShowUrl = "";  //չʾҳ��
            PayAPI.Model.Attach attach = new PayAPI.Model.Attach();
            attach.Key = "OrderType";
            attach.Value = OrderType.ToString();
            trade.AttachList.Add(attach);
            //����url
            return PayAPI.Ali.Alipay.Create.Create_url(trade);
        }
    }
}
