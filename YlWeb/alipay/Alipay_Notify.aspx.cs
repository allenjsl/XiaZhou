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
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Net;
using Adpost.Common;

namespace YlWeb.alipay
{
    /// <summary>
    /// ֧�������غ�Ľ���������
    /// </summary>
    public partial class Alipay_Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayAPI.Model.Ali.AliPayTradeNotify notify = PayAPI.Ali.Alipay.Create.GetNotifyAsync();//֧�����ķ���֪ͨʵ��

            if (notify.IsTradeSuccess)
            {

                var info = new EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo();//����֧��ʵ��
                var onlinepay = new EyouSoft.BLL.YlStructure.BZaiXianZhiFu();//����֧��BLL
                foreach (var item in notify.OrderInfo.OrderID)
                {
                    info.DingDanId = item;//����ID
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)Convert.ToInt32(notify.AttachList["OrderType"].Value);//��������
                    bool ispay = onlinepay.IsZhiFu(info.DingDanId, info.DingDanLeiXing);//��ȡ����֧��״̬���ɹ�/ʧ�ܣ�

                    if (!ispay)
                    {
                        info.JiaoYiHao = notify.OutTradeNo;//��ˮ��
                        info.ApiJiaoYiHao = notify.TradeNo;//֧����ˮ��
                        info.JinE = notify.Totalfee;//֧�����
                        info.ZhiFuFangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;//֧����ʽ
                        info.IsZhiFu = true;//�Ƿ���֧��
                        info.ZhiFuTime = DateTime.Now;//֧��ʱ��
                        int bllRetCode = onlinepay.Insert(info);//���֧����¼
                        if (bllRetCode == 1)
                        {
                            //ʵ�������������� 
                            switch (info.DingDanLeiXing)
                            {
                                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.�һ�����:
                                    HandlerJiFenDingDan(info.DingDanId);
                                    break;
                                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.���ڶ���:
                                    HandlerHangQiDingDan(info.DingDanId);
                                    break;
                            }
                        }
                    }
                }
            }
            //֧���ӿڻص�֪ͨ
            Response.Write(notify.PayAPICallBackMsg);
            Response.End();
        }

        #region private members
        /// <summary>
        /// �����ڶ�����Ϣ
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;
            kuanInfo.FuKuanId = string.Empty;
            kuanInfo.IssueTime = DateTime.Now;
            kuanInfo.JinE = info.JinE;
            kuanInfo.OperatorId = info.XiaDanRenId;
            kuanInfo.ShiJian = DateTime.Now;
            kuanInfo.Status = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.�Ѹ���;

            new EyouSoft.BLL.YlStructure.BHangQiDingDan().SheZhiFuKuanStatus(info.DingDanId, info.XiaDanRenId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.�Ѹ���, DateTime.Now, kuanInfo);

        }

        /// <summary>
        /// ������ֶ�����Ϣ
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;
            kuanInfo.FuKuanId = string.Empty;
            kuanInfo.IssueTime = DateTime.Now;
            kuanInfo.JinE = info.JinE;
            kuanInfo.OperatorId = info.XiaDanRenId;
            kuanInfo.ShiJian = DateTime.Now;
            kuanInfo.Status = EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.�Ѹ���;

            new EyouSoft.BLL.YlStructure.BDuiHuan().SheZhiJiFenDingDanFuKuanStatus(info.DingDanId, info.XiaDanRenId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.�Ѹ���, DateTime.Now, kuanInfo);

        }
        #endregion
    }
}
