using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.YlStructure
{
    #region 基础信息类型
    /// <summary>
    /// 基础信息类型
    /// </summary>
    public enum JiChuXinXiLeiXing
    {
        /// <summary>
        /// 航线
        /// </summary>
        航线 = 0,
        /// <summary>
        /// 港口
        /// </summary>
        港口,
        /// <summary>
        /// 国籍
        /// </summary>
        国籍,
        /// <summary>
        /// 房型
        /// </summary>
        房型,
        /// <summary>
        /// 宾客类型
        /// </summary>
        宾客类型,
        /// <summary>
        /// 附加产品类型
        /// </summary>
        附加产品类型
    }
    #endregion

    #region 游轮类型
    /// <summary>
    /// 游轮类型
    /// </summary>
    public enum YouLunLeiXing
    {
        /// <summary>
        /// 长江游轮
        /// </summary>
        长江游轮 = 0,
        /// <summary>
        /// 海洋邮轮
        /// </summary>
        海洋邮轮
    }
    #endregion

    #region 游轮星级
    /// <summary>
    /// 游轮星级
    /// </summary>
    public enum XingJi
    {
        /// <summary>
        /// 三星
        /// </summary>
        三星,
        /// <summary>
        /// 四星
        /// </summary>
        四星,
        /// <summary>
        /// 准五星
        /// </summary>
        准五星,
        /// <summary>
        /// 五星
        /// </summary>
        五星,
        /// <summary>
        /// 超五星
        /// </summary>
        超五星
    }
    #endregion

    #region 优惠规则共享方式
    /// <summary>
    /// 优惠规则共享方式
    /// </summary>
    public enum YouHuiGongXiangFangShi
    {
        /// <summary>
        /// 同时享有
        /// </summary>
        同时享有 = 0,
        /// <summary>
        /// 最高金额
        /// </summary>
        最高金额,
        /// <summary>
        /// 最低金额
        /// </summary>
        最低金额
    }
    #endregion

    #region 游轮网站KV KEY
    /// <summary>
    /// 游轮网站KV KEY
    /// </summary>
    public enum WzKvKey
    {
        维诗达简介 = 0,
        联系我们 = 1,
        网站标题 = 2,
        网站关键字 = 3,
        网站描述 = 4,
        网站版权 = 5,
        长江游轮会议简介 = 6,
        长江游轮会议详介 = 7,
        长江游轮小型会议接待 = 8,
        长江游轮小型会议场地选择 = 9,
        长江游轮小型会议服务项目及流程 = 10,
        长江游轮大型会议包租流程 = 11,
        长江游轮大型会议航线介绍 = 12,
        长江游轮大型会议包租价格 = 13,
        长江游轮大型会议会展服务 = 14,
        长江游轮大型会议关切问题 = 15,
        长江游轮商务服务服务流程 = 16,
        长江游轮商务服务会议设施 = 17,
        长江游轮商务服务其它服务 = 18,
        长江游轮商务服务陆地服务 = 19,
        长江游轮费用测算 = 20,
        海洋邮轮会议简介 = 21,
        海洋邮轮会议详介 = 22,
        海洋邮轮小型会议接待 = 23,
        海洋邮轮小型会议场地选择 = 24,
        海洋邮轮小型会议服务项目及流程 = 25,
        海洋邮轮大型会议包租流程 = 26,
        海洋邮轮大型会议航线介绍 = 27,
        海洋邮轮大型会议包租价格 = 28,
        海洋邮轮大型会议会展服务 = 29,
        海洋邮轮大型会议关切问题 = 30,
        海洋邮轮商务服务服务流程 = 31,
        海洋邮轮商务服务会议设施 = 32,
        海洋邮轮商务服务其它服务 = 33,
        海洋邮轮商务服务陆地服务 = 34,
        海洋邮轮费用测算 = 35,
        新手指南_如何订票 = 36,
        新手指南_如何取票 = 37,
        新手指南_游船旅行准备 = 38,
        新手指南_旅行注意事项 = 39,
        会员服务_积分兑换 = 40,
        会员服务_积分使用 = 41,
        会员服务_取消订单 = 42,
        支付方式_在线支付 = 43,
        支付方式_门店支付 = 44,
        支付方式_邮局汇款 = 45,
        支付方式_公司转账 = 46,
        售后服务_售后政策 = 47,
        售后服务_价格保护 = 48,
        售后服务_退款说明 = 49,
        售后服务_取消订单 = 50,
        礼品卡_关于礼品卡 = 51,
        礼品卡_购买流程 = 52,
        礼品卡_规则说明 = 53,
        礼品卡_使用帮助 = 54,
        礼品卡_常见问题 = 55,
        企业文化 = 56,
        长江游轮合同条款 = 57,
        海洋邮轮合同条款 = 58,
        会员服务_退款说明 = 59,
        长江游轮旅客须知 = 60,
        海洋邮轮旅客须知 = 61,
        旅游度假资质=62,
        广告业务=63
    }
    #endregion

    #region 游轮网站友情链接类型
    /// <summary>
    /// 游轮网站友情链接类型
    /// </summary>
    public enum WzYouQingLianJieLeiXing
    {
        /// <summary>
        /// 文本
        /// </summary>
        文本 = 0,
        /// <summary>
        /// 图文
        /// </summary>
        图文,
        /// <summary>
        /// 长江优惠信息
        /// </summary>
        长江优惠信息,
        /// <summary>
        /// 海洋优惠信息
        /// </summary>
        海洋优惠信息
    }
    #endregion

    #region 游轮网站广告位置
    /// <summary>
    /// 游轮网站广告位置
    /// </summary>
    public enum WzGuangGaoWeiZhi
    {
        None,
        长江游轮横幅,
        海洋游轮横幅,
        团购横幅,
        游轮会议横幅,
        游轮会议长江游轮图片,
        游轮会议海洋游轮图片,        
        积分商城首页轮换图片,
        热门关键字,
        登录页左侧大图,
        注册页右侧大图,
        重置登录密码页右侧大图,
        登录页右下图片,
        首页轮播图片,
    }
    #endregion

    #region 游轮网站资讯类型
    /// <summary>
    /// 游轮网站资讯类型
    /// </summary>
    public enum WzZiXunLeiXing
    {
        None = 0,
        长江游轮问题解答,
        会员通知,
        长江游轮攻略,
        海洋邮轮问题解答,
        海洋邮轮攻略
    }
    #endregion

    #region 游轮网站会员状态
    /// <summary>
    /// 游轮网站会员状态
    /// </summary>
    public enum HuiYuanStatus
    {
        可用 = 0,
        停用
    }
    #endregion

    #region 游轮网站会员类型
    /// <summary>
    /// 游轮网站会员类型
    /// </summary>
    public enum HuiYuanLeiXing
    {
        注册会员 = 0,
        直接预订
    }
    #endregion

    #region 游轮网站会员平台类型
    /// <summary>
    /// 游轮网站会员平台类型
    /// </summary>
    public enum HuiYuanPingTaiLeiXing
    {
        游轮网站 = 0
    }
    #endregion

    #region 游轮网站会员常旅客状态
    /// <summary>
    /// 游轮网站会员常旅客状态
    /// </summary>
    public enum HuiYuanChangLvKeStatus
    {
        可用=0,
        停用
    }
    #endregion

    #region 游轮网站会员收藏类型
    /// <summary>
    /// 游轮网站会员收藏类型
    /// </summary>
    public enum HuiYuanShouCangLeiXing
    {
        长江游轮 = 0,
        海洋游轮 = 1,
        积分兑换 = 2,
        团购产品 = 3,
    }
    #endregion

    #region 游轮网站积分兑换商品方式
    /// <summary>
    /// 游轮网站积分兑换商品方式
    /// </summary>
    public enum JiFenDuiHuanFangShi
    {
        积分 = 0,
        积分礼品卡 = 1,
        积分现金 = 2
    }
    #endregion

    #region 发票配送方式
    /// <summary>
    /// 发票配送方式
    /// </summary>
    public enum FaPiaoPeiSongFangShi
    {
        自取=0,
        快递
    }
    #endregion

    #region 游轮航期订单状态
    /// <summary>
    /// 游轮航期订单状态
    /// </summary>
    public enum HangQiDingDanStatus
    {
        未处理 = 0,
        审核中 = 1,
        留位 = 2,
        留位过期 = 3,
        成交 = 4,
        取消 = 5,
        不受理 = 6
    }
    #endregion

    #region 付款状态
    /// <summary>
    /// 付款状态
    /// </summary>
    public enum FuKuanStatus
    {
        未付款=0,
        已付款=1
    }
    #endregion

    #region 航期标签
    /// <summary>
    /// 航期标签
    /// </summary>
    public enum HangQiBiaoQian
    {
        热门=0,
        特价,
        热门推荐,
        特价推荐
    }
    #endregion

    #region 积分商品状态
    /// <summary>
    /// 积分商品状态
    /// </summary>
    public enum JiFenShangPinStatus
    {
        上架=0,
        下架
    }
    #endregion

    #region 积分订单状态
    /// <summary>
    /// 积分订单状态
    /// </summary>
    public enum JiFenDingDanStatus
    {
        未处理 = 0,
        已成交,
        已取消
    }
    #endregion

    #region 礼品卡类型
    /// <summary>
    /// 礼品卡类型
    /// </summary>
    public enum LiPinKaLeiXing
    {
        电子卡 = 0,
        实体卡,
        送礼卡
    }
    #endregion

    #region 礼品卡订单状态
    /// <summary>
    /// 礼品卡订单状态
    /// </summary>
    public enum LiPinKaDingDanStatus
    {
        未处理 = 0,
        已成交,
        已取消
    }
    #endregion

    #region 订单类型
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum DingDanLeiXing
    {
        航期订单 = 0,
        兑换订单 = 1,
        礼品卡订单 = 2
    }
    #endregion

    #region 积分明细类型
    /// <summary>
    /// 积分明细类型
    /// </summary>
    public enum JiFenMxLeiXing
    {
        下单累积积分 = 0,
        下单抵扣积分 = 1,
        积分兑换商品 = 2
    }
    #endregion

    #region 预存款明细类型
    /// <summary>
    /// 预存款明细类型
    /// </summary>
    public enum YuCunKuanMxLeiXing
    {
        电子卡购买 = 0,
        实体卡充值 = 1,
        下单抵扣 = 2,
        兑换商品抵扣 = 3
    }
    #endregion

    #region 在线支付支付方式
    /// <summary>
    /// 在线支付支付方式
    /// </summary>
    public enum ZaiXianZhiFuFangShi
    {
        /// <summary>
        /// 支付宝 = 1
        /// </summary>
        Alipay = 1,
        /// <summary>
        /// 财付通 = 2
        /// </summary>
        Tenpay = 2,
        /// <summary>
        /// 通联支付
        /// </summary>
        AllInPay = 3,
        /// <summary>
        /// 银联支付=4
        /// </summary>
        Unionpay = 4,
        /// <summary>
        /// 快钱支付
        /// </summary>
        Bill99 = 5
    }
    #endregion

    #region 咨询问答类型
    /// <summary>
    /// 咨询问答类型
    /// </summary>
    public enum WenDaLeiXing
    {
        交通 = 0,
        住宿,
        景点,
        餐饮,
        其他
    }
    #endregion

    #region 游客证件类型
    /// <summary>
    /// 游客证件类型
    /// </summary>
    public enum YKZJLeiXing
    {
        请选择=0,
        身份证,
        户口本,
        护照,
        稍后提供
    }
    #endregion

    #region 游轮网站意见反馈类型
    /// <summary>
    /// 游轮网站意见反馈类型
    /// </summary>
    public enum YiJianFanKuiLeiXing
    {
        /// <summary>
        /// 网站改版建议=0
        /// </summary>
        网站改版建议=0,
        /// <summary>
        /// 订购流程=1
        /// </summary>
        订购流程=1
    }
    #endregion
}
