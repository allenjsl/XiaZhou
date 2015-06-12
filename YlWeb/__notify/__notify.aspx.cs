using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.__notify
{
    /// <summary>
    /// zhifu notify test
    /// </summary>
    public partial class __notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dingDanId = Utils.GetQueryStringValue("dingdanid");
            var dingDanLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing>(Utils.GetQueryStringValue("dingdanleixing"), EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单);

            if (string.IsNullOrEmpty(dingDanId)) return;

            switch (dingDanLeiXing)
            {
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单:
                    HandlerJiFenDingDan(dingDanId);
                    break;
                case EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单:
                    HandlerHangQiDingDan(dingDanId);
                    break;
            }

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
