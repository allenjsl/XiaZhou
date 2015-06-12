//YL-在线支付信息业务实体 汪奇志 2014-04-19
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region YL-在线支付信息业务实体
    /// <summary>
    /// YL-在线支付信息业务实体
    /// </summary>
    public class MZaiXianZhiFuInfo
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int IdentityId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing DingDanLeiXing { get; set; }
        /// <summary>
        /// 提供给支付接口的唯一订单号
        /// </summary>
        public string JiaoYiHao { get; set; }
        /// <summary>
        /// 支付接口的支付流水号
        /// </summary>
        public string ApiJiaoYiHao { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 在线支付支付方式
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi ZhiFuFangShi { get; set; }
        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsZhiFu { get; set; }
        /// <summary>
        /// 支付成功时间
        /// </summary>
        public DateTime ZhiFuTime { get; set; }
    }
    #endregion
}
