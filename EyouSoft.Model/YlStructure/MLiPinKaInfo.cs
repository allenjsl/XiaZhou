//YL-礼品卡相关 汪奇志 2014-04-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 游轮网站礼品卡信息
    /// <summary>
    /// 游轮网站礼品卡信息
    /// </summary>
    public class MLiPinKaInfo
    {
        /// <summary>
        /// 礼品卡编号
        /// </summary>
        public string LiPinKaId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 卡片金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 购买金额
        /// </summary>
        public decimal JinE1 { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string FengMian { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing LeiXing { get; set; }
        /// <summary>
        /// 发票快递费用
        /// </summary>
        public decimal FaPiaoKuaiDiJinE { get; set; }
        /// <summary>
        /// 礼品卡快递费用
        /// </summary>
        public decimal LiPinKaKuaiDiJinE { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 购买须知
        /// </summary>
        public string XuZhi { get; set; }
    }
    #endregion

    #region 游轮网站礼品卡卡片信息
    /*/// <summary>
    /// 游轮网站礼品卡卡片信息
    /// </summary>
    public class MLiPinKaKaPianInfo
    {
        /// <summary>
        /// 卡片编号
        /// </summary>
        public string KaPianId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string KaHao { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string YanZhengMa { get; set; }
    }*/
    #endregion

    #region 游轮网站礼品卡信息查询实体
    /// <summary>
    /// 游轮网站礼品卡信息查询实体
    /// </summary>
    public class MLiPinKaChaXunInfo
    {
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 礼品卡类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing? LeiXing { get; set; }
    }
    #endregion


    #region 游轮网站礼品卡订单信息
    /// <summary>
    /// 游轮网站礼品卡订单信息
    /// </summary>
    public class MLiPinKaDingDanInfo
    {
        /// <summary>
        /// 礼品卡编号
        /// </summary>
        public string LiPinKaId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string JiaoYiHao { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 礼品卡金额
        /// </summary>
        public decimal JinE1 { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus DingDanStatus { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus FuKuanStatus { get; set; }
        /// <summary>
        /// 下单备注
        /// </summary>
        public string XiaDanBeiZhu { get; set; }
        /// <summary>
        /// 预订人姓名
        /// </summary>
        public string YuDingRenName { get; set; }
        /// <summary>
        /// 预订人电话
        /// </summary>
        public string YuDingRenDianHua { get; set; }
        /// <summary>
        /// 预订人手机
        /// </summary>
        public string YuDingRenShouJi { get; set; }
        /// <summary>
        /// 预订人邮箱
        /// </summary>
        public string YuDingRenYouXiang { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public bool IsXuYaoFaPiao { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string FaPiaoTaiTou { get; set; }
        /// <summary>
        /// 发票明细类型
        /// </summary>
        public string FaPiaoLeiXing { get; set; }
        /// <summary>
        /// 发票明细
        /// </summary>
        public string FaPiaoMingXi { get; set; }
        /// <summary>
        /// 发票配送方式
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi FaPiaoPeiSongFangShi { get; set; }
        /// <summary>
        /// 发票配送地址
        /// </summary>
        public string FaPiaoDiZhiId { get; set; }
        /// <summary>
        /// 发票快递费用
        /// </summary>
        public decimal FaPiaoKuaiDiJinE { get; set; }
        /// <summary>
        /// 下单人编号
        /// </summary>
        public string XiaDanRenId { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 赠语
        /// </summary>
        public string ZengYu { get; set; }
        /// <summary>
        /// 收礼人地址编号
        /// </summary>
        public string SlrDiZhiId { get; set; }
        /// <summary>
        /// 礼品卡快递费用
        /// </summary>
        public decimal LiPinKaKuaiDiJinE { get; set; }
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string LiPinKaMingCheng { get; set; }
        /// <summary>
        /// 礼品卡类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing LiPinKaLeiXing { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string HuiYuanXingMing { get; set; }
    }
    #endregion

    #region 游轮网站礼品卡订单查询实体
    /// <summary>
    /// 游轮网站礼品卡订单查询实体
    /// </summary>
    public class MLiPinKaDingDanChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string LiPinKaMingCheng { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string JiaoYiHao { get; set; }
        /// <summary>
        /// 礼品卡类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing? LiPinKaLeiXing { get; set; }
        /// <summary>
        /// 下单时间-起
        /// </summary>
        public DateTime? XiaDanShiJian1 { get; set; }
        /// <summary>
        /// 下单时间-止
        /// </summary>
        public DateTime? XiaDanShiJian2 { get; set; }
    }
    #endregion
}
