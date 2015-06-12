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
    /// 支付宝返回后的结果处理程序
    /// </summary>
    public partial class Alipay_Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayAPI.Model.Ali.AliPayTradeNotify notify = PayAPI.Ali.Alipay.Create.GetNotifyAsync();//支付宝的返回通知实体

            if (notify.IsTradeSuccess)
            {

                var info = new EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo();//在线支付实体
                var onlinepay = new EyouSoft.BLL.YlStructure.BZaiXianZhiFu();//在线支付BLL
                foreach (var item in notify.OrderInfo.OrderID)
                {
                    info.DingDanId = item;//订单ID
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)Convert.ToInt32(notify.AttachList["OrderType"].Value);//订单类型
                    bool ispay = onlinepay.IsZhiFu(info.DingDanId, info.DingDanLeiXing);//获取订单支付状态（成功/失败）

                    if (!ispay)
                    {
                        info.JiaoYiHao = notify.OutTradeNo;//流水号
                        info.ApiJiaoYiHao = notify.TradeNo;//支付流水号
                        info.JinE = notify.Totalfee;//支付金额
                        info.ZhiFuFangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;//支付方式
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
                }
            }
            //支付接口回调通知
            Response.Write(notify.PayAPICallBackMsg);
            Response.End();
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Alipay;
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
