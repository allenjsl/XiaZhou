using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.unionpay.upop.sdk;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.unionpay
{
    /// <summary>
    /// 银联支付返回后的结果处理程序
    /// </summary>
    public partial class Unionpay_Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("/unionpay/notify_data.txt"));
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sw.Close();

            // 要使用各种Srv必须先使用LoadConf载入配置
            string strConfigPath = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("UnionpayConfigFilepath");
            UPOPSrv.LoadConf(Server.MapPath(strConfigPath));
            // 使用Post过来的内容构造SrvResponse
            SrvResponse resp = new SrvResponse(Util.NameValueCollection2StrDict(Request.Form));

            #region 取得返回参数
            string respCode = resp.Fields["respCode"];
            string merId = resp.Fields["merId"];
            string orderAmount = resp.Fields["orderAmount"];
            string orderNumber = resp.Fields["orderNumber"];
            string qid = resp.Fields["qid"];
            string respTime = resp.Fields["respTime"];
            string version = resp.Fields["version"];
            string strOrderID = orderNumber.Substring(15, orderNumber.Length - 15);
            string strOrderTpye = orderNumber.Substring(14, 1);

            var dingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)Convert.ToInt32(strOrderTpye);

            string dingDanId = string.Empty;

            if (dingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单)
            {
                dingDanId = new EyouSoft.BLL.YlStructure.BDuiHuan().GetDingDanId(Utils.GetInt(strOrderID));
            }
            else if (dingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单)
            {
                dingDanId = new EyouSoft.BLL.YlStructure.BHangQiDingDan().GetDingDanId(Utils.GetInt(strOrderID));
            }

            #endregion
            // 收到回应后做后续处理
            string strMsg = "支付失败！";
            if (resp.ResponseCode == SrvResponse.RESP_SUCCESS)
            {
                var info = new EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo();//在线支付实体
                var onlinepay = new EyouSoft.BLL.YlStructure.BZaiXianZhiFu();//在线支付BLL
                info.DingDanId = dingDanId;//订单ID
                info.DingDanLeiXing = dingDanLeiXing;//订单类型
                bool ispay = onlinepay.IsZhiFu(info.DingDanId, info.DingDanLeiXing);//获取订单支付状态（成功/失败）

                if (!ispay)
                {
                    info.JiaoYiHao = orderNumber;//流水号
                    info.ApiJiaoYiHao = qid;//支付流水号
                    info.JinE = Utils.GetDecimal(orderAmount);//支付金额
                    info.ZhiFuFangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Unionpay;//支付方式
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

                strMsg = "支付成功";
            }
            //支付接口回调通知
            Response.Write(strMsg);
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Unionpay;
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
            kuanInfo.FangShi = EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi.Unionpay;
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
