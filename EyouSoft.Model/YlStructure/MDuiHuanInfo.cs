//积分兑换相关实体 汪奇志 2014-03-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 游轮网站积分兑换商品信息业务实体
    /// <summary>
    /// 游轮网站积分兑换商品信息业务实体
    /// </summary>
    public class MWzJiFenShangPinInfo
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ShangPinId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 商品说明
        /// </summary>
        public string ShuoMing { get; set; }
        /// <summary>
        /// 兑换须知
        /// </summary>
        public string XuZhi { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 附件集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 游轮网站积分兑换商品兑换方式集合
        /// </summary>
        public IList<MWzJiFenShangPinFangShiInfo> FangShis { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string PeiSongFangShi { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus Status { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 成交数量
        /// </summary>
        public int ChengJiaoShuLiang { get; set; }
        /// <summary>
        /// 发票快递金额
        /// </summary>
        public decimal FaPiaoKuaiDiJinE { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int ShengYuShuLiang { get { return ShuLiang - ChengJiaoShuLiang; } }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ShangPinJinE { get; set; }
    }
    #endregion

    #region 游轮网站积分兑换商品兑换方式信息业务实体
    /// <summary>
    /// 游轮网站积分兑换商品兑换方式信息业务实体
    /// </summary>
    public class MWzJiFenShangPinFangShiInfo
    {
        /// <summary>
        /// 兑换方式
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi FangShi { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal JinE { get; set; }
    }
    #endregion

    #region 游轮网站积分兑换订单信息实体
    /// <summary>
    /// 游轮网站积分兑换订单信息实体
    /// </summary>
    public class MWzJiFenDingDanInfo
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ShangPinId { get; set; }
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
        /// 兑换方式
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi FangShi { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus DingDanStatus { get; set; }
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
        /// 商品名称
        /// </summary>
        public string ShangPinMingCheng { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string HuiYuanXingMing { get; set; }
        /// <summary>
        /// 会员帐号
        /// </summary>
        public string HuiYuanZhangHao { get; set; }
        /// <summary>
        /// 礼品快递地址编号
        /// </summary>
        public string LiPinDiZhiId { get; set; }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ShangPinJinE { get; set; }
        /// <summary>
        /// 自增编号
        /// </summary>
        public int IdentityId { get; set; }
    }
    #endregion


    #region 游轮网站积分兑换商品信息查询业务实体
    /// <summary>
    /// 游轮网站积分兑换商品信息查询业务实体
    /// </summary>
    public class MWzJiFenShangPinChaXunInfo
    {
        /// <summary>
        /// 排序 0:积分DESC 1:积分ASC 2:礼品卡金额DESC 3:礼品卡金额ASC 4:现金金额DESC 5:现金金额ASC 6:发布时间DESC 7:发布时间ASC
        /// </summary>
        public int PaiXu { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus? Status { get; set; }
    }
    #endregion

    #region 游轮网站积分兑换订单信息查询实体
    /// <summary>
    /// 游轮网站积分兑换订单信息查询实体
    /// </summary>
    public class MWzJiFenDingDanChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ShangPinMingCheng { get; set; }
        /// <summary>
        /// 下单时间-起
        /// </summary>
        public DateTime? XiaDanShiJian1 { get; set; }
        /// <summary>
        /// 下单时间-止
        /// </summary>
        public DateTime? XiaDanShiJian2 { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus? DingDanStatus { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus? FuKuanStatus { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string JiaoYiHao { get; set; }
    }
    #endregion
}
