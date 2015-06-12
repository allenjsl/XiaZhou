//游轮航期相关 汪奇志 2014-03-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 游轮航期信息业务实体
    /// <summary>
    /// 游轮航期信息业务实体
    /// </summary>
    public class MHangQiInfo
    {
        /// <summary>
        /// Seo Title
        /// </summary>
        public string SeoTitle { get; set; }
        /// <summary>
        /// Seo Keywords
        /// </summary>
        public string SeoKeyword { get; set; }
        /// <summary>
        /// Seo Description
        /// </summary>
        public string SeoDescription { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string BianHao { get; set; }
        /// <summary>
        /// 起始价格【近三个月内同航期的最低价】
        /// 新增时使用此字段 输出时为近三个月内同航期的最低价
        /// </summary>
        public decimal QiShiJiaGe { get; set; }
        /// <summary>
        /// 起始价格 输出时为录入的起始价格
        /// </summary>
        public decimal QiShiJiaGe1 { get; set; }
        /// <summary>
        /// 起始价格说明
        /// </summary>
        public string QiShiJiaGeShuoMing { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称(OUTPUT)
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮公司名称(OUTPUT)
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 游轮系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 游轮系列名称(OUTPUT)
        /// </summary>
        public string XiLieName { get; set; }
        /// <summary>
        /// 游轮船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 游轮船只名称(OUTPUT)
        /// </summary>
        public string ChuanZhiName { get; set; }
        /// <summary>
        /// 游轮航线编号
        /// </summary>
        public int HangXianId { get; set; }
        /// <summary>
        /// 出发港口编号
        /// </summary>
        public int ChuFaGangKouId { get; set; }
        /// <summary>
        /// 抵达港口编号
        /// </summary>
        public int DiDaGangKouId { get; set; }
        /// <summary>
        /// 产品特色
        /// </summary>
        public string ChanPinTeSe { get; set; }
        /// <summary>
        /// 优惠信息
        /// </summary>
        public string YouHuiXinXi { get; set; }
        /// <summary>
        /// 行程天数天
        /// </summary>
        public int TianShu1 { get; set; }
        /// <summary>
        /// 行程天数晚
        /// </summary>
        public int TianShu2 { get; set; }
        /// <summary>
        /// 费用说明
        /// </summary>
        public string FeiYongShuoMing { get; set; }
        /// <summary>
        /// 签证签注
        /// </summary>
        public string QianZhengQianZhu { get; set; }
        /// <summary>
        /// 预订须知
        /// </summary>
        public string YuDingXuZhi { get; set; }
        /// <summary>
        /// 友情提示
        /// </summary>
        public string YouQingTiShi { get; set; }
        /// <summary>
        /// 航线性质
        /// </summary>
        public string HangXianXingZhi { get; set; }
        /// <summary>
        /// 最多可抵扣积分
        /// </summary>
        public int KeDiKouJinFen { get; set; }
        /// <summary>
        /// 积分兑换比例
        /// </summary>
        public decimal JiFenDuiHuanBiLi { get; set; }
        /// <summary>
        /// 积分累积比例
        /// </summary>
        public decimal JiFenLeiJiBiLi { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 附加产品信息集合
        /// </summary>
        public IList<MHangQiFuJiaChanPinInfo> FuJiaChanPins { get; set; }
        /// <summary>
        /// 行程信息集合
        /// </summary>
        public IList<MHangQiXingChengInfo> XingChengs { get; set; }
        /// <summary>
        /// 途经城市信息集合
        /// </summary>
        public IList<MHangQiTuJingChengShiInfo> TuJingChengShis { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<EyouSoft.Model.YlStructure.MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 出港时间集合
        /// </summary>
        public IList<MHangQiRiQiInfo> RiQis { get; set; }
        /// <summary>
        /// 标签集合
        /// </summary>
        public IList<MHangQiBiaoQianInfo> BiaoQians { get; set; }
        /// <summary>
        /// 途经城市
        /// </summary>
        public string TuJingChengShi { get; set; }
        /// <summary>
        /// 发票快递费用
        /// </summary>
        public decimal FaPiaoKuaiDiJinE { get; set; }

        /// <summary>
        /// 航线名称
        /// </summary>
        public string HangXianMingCheng { get; set; }
        /// <summary>
        /// 出发港口名称
        /// </summary>
        public string ChuFaGangKouMingCheng { get; set; }
        /// <summary>
        /// 抵达港口名称
        /// </summary>
        public string DiDaGangKouMingCheng { get; set; }
        /// <summary>
        /// 是否含有效日期及价格
        /// </summary>
        public bool IsYouXiao { get; set; }
        /// <summary>
        /// 有效日期编号
        /// </summary>
        public string YouXiaoRiQiId { get; set; }
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime? YouXiaoRiQi { get; set; }
        /// <summary>
        /// 预控人数
        /// </summary>
        public int YuKongRenShu { get; set; }
        /// <summary>
        /// 有效订单人数
        /// </summary>
        public int YouXiaoDingDanRenShu { get; set; }
        /// <summary>
        /// 游轮攻略
        /// </summary>
        public string GongLue { get; set; }
        /// <summary>
        /// 销量[真实统计]
        /// </summary>
        public int XiaoLiang { get; set; }
        /// <summary>
        /// 销量[可以维护]
        /// </summary>
        public int XiaoLiang1 { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        public decimal HaoPing { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int PaiXuId { get; set; }
    }
    #endregion

    #region 游轮航期附加产品信息业务实体
    /// <summary>
    /// 游轮航期附加产品信息业务实体
    /// </summary>
    public class MHangQiFuJiaChanPinInfo
    {
        /// <summary>
        /// 附加产品编号
        /// </summary>
        public string FuJiaChanPinId { get; set; }
        /// <summary>
        /// 服务类型编号
        /// </summary>
        public int LeiXingId { get; set; }
        /// <summary>
        /// 服务项目
        /// </summary>
        public string XiangMu { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DanJia { get; set; }
        /// <summary>
        /// 计价单位
        /// </summary>
        public string DanWei { get; set; }
        /// <summary>
        /// 产品介绍
        /// </summary>
        public string JieShao { get; set; }
    }
    #endregion

    #region 游轮航期行程信息业务实体
    /// <summary>
    /// 游轮航期行程信息业务实体
    /// </summary>
    public class MHangQiXingChengInfo
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        public string XingChengId { get; set; }
        /// <summary>
        /// 起始区间
        /// </summary>
        public string QuJian1 { get; set; }
        /// <summary>
        /// 抵达区间
        /// </summary>
        public string QuJian2 { get; set; }
        /// <summary>
        /// 住宿
        /// </summary>
        public string ZhuSu { get; set; }
        /// <summary>
        /// 早
        /// </summary>
        public string Zao { get; set; }
        /// <summary>
        /// 中
        /// </summary>
        public string Zhong { get; set; }
        /// <summary>
        /// 晚
        /// </summary>
        public string Wan { get; set; }
        /// <summary>
        /// 行程内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        public string JiaoTongGongJu { get; set; }
        /// <summary>
        /// 班次号码
        /// </summary>
        public string BanCi { get; set; }
        /// <summary>
        /// 行程图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 第几天
        /// </summary>
        public int Tian { get; set; }
    }
    #endregion

    #region 游轮航期途经城市信息业务实体
    /// <summary>
    /// 游轮航期途经城市信息业务实体
    /// </summary>
    public class MHangQiTuJingChengShiInfo
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int ChengShiId { get; set; }
    }
    #endregion

    #region 游轮航期价格信息业务实体
    /// <summary>
    /// 游轮航期价格信息业务实体
    /// </summary>
    public class MHangQiJiaGeInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 出港日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 价格编号
        /// </summary>
        public string JiaGeId { get; set; }
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 最底容纳人数
        /// </summary>
        public int RongNaRenShu { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int GuoJiId { get; set; }
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe1 { get; set; }
        /// <summary>
        /// 占床价格
        /// </summary>
        public decimal JiaGe2 { get; set; }
        /// <summary>
        /// 加床价格
        /// </summary>
        public decimal JiaGe3 { get; set; }
        /// <summary>
        /// 不占床价格
        /// </summary>
        public decimal JiaGe4 { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal FangCha { get; set; }
        /// <summary>
        /// 价格说明
        /// </summary>
        public string ShuoMing { get; set; }
        /// <summary>
        /// 船只基础价
        /// </summary>
        public decimal BasePrice { get; set; }
        /// <summary>
        /// 船只房型占床倍数
        /// </summary>
        public decimal BeiShu { get; set; }
        /// <summary>
        /// 船只房型不占床倍数
        /// </summary>
        public decimal BeiShu1 { get; set; }
        /// <summary>
        /// 船只房型占加床倍数
        /// </summary>
        public decimal BeiShu2 { get; set; }

        /*/// <summary>
        /// 房型名称
        /// </summary>
        public string FangXingName { get; set; }
        /// <summary>
        /// 国籍名称
        /// </summary>
        public string GuoJiName { get; set; }
        /// <summary>
        /// 宾客类型名称
        /// </summary>
        public string BinKeLeiXingName { get; set; }*/
    }
    #endregion

    #region 游轮航期出港日期信息业务实体
    /// <summary>
    /// 游轮航期出港日期信息业务实体
    /// </summary>
    public class MHangQiRiQiInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 出港日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 出港日期
        /// </summary>
        public DateTime RiQi { get; set; }
        /// <summary>
        /// 预控人数
        /// </summary>
        public int RenShu { get; set; }
        /// <summary>
        /// 是否设定价格信息
        /// </summary>
        public bool IsJiaGe { get; set; }
        /// <summary>
        /// 有效订单人数
        /// </summary>
        public int YouXiaoDingDanRenShu { get; set; }
        /// <summary>
        /// 当月最低价格(OUTPUT WZ日历)
        /// </summary>
        public decimal QiShiJiaGe2 { get; set; }
    }
    #endregion

    #region 游轮航期优惠规则信息业务实体
    /// <summary>
    /// 游轮航期优惠规则信息业务实体
    /// </summary>
    public class MHangQiYouHuiGuiZeInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 规则编号
        /// </summary>
        public string GuiZeId { get; set; }
        /// <summary>
        /// 规则
        /// </summary>
        public string GuiZe { get; set; }
        /// <summary>
        /// 规则描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int YouXianJi { get; set; }
        /// <summary>
        /// 是否共享
        /// </summary>
        public bool IsGongXiang { get; set; }
        /// <summary>
        /// 优惠名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 优惠方式 0:按订单 1:按人
        /// </summary>
        public int FangShi { get; set; }
    }
    #endregion

    #region 游轮航期优惠规则JSON实体
    /// <summary>
    /// 游轮航期优惠规则JSON实体
    /// </summary>
    public class MHangQiYouHuiGuiZeInfoJSON
    {
        /// <summary>
        /// 下单时间条件
        /// </summary>
        public string XiaDanShiJianTiaoJian { get; set; }
        /// <summary>
        /// 下单时间提前天数
        /// </summary>
        public string XiaDanShiJianTianShu { get; set; }
        /// <summary>
        /// 下单时间区间开始时间
        /// </summary>
        public string XiaDanShiJianS { get; set; }
        /// <summary>
        /// 下单时间区间结束时间
        /// </summary>
        public string XiaDanShiJianE { get; set; }
        /// <summary>
        /// 出港开始时间
        /// </summary>
        public string ChuGangShiJianS { get; set; }
        /// <summary>
        /// 出港结束时间
        /// </summary>
        public string ChuGangShiJianE { get; set; }
        /// <summary>
        /// 人数条件
        /// </summary>
        public string RenShuTiaoJian { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public string RenShu { get; set; }
        /// <summary>
        /// 游客年龄条件
        /// </summary>
        public string YouKeNianLingTiaoJian { get; set; }
        /// <summary>
        /// 游客年龄
        /// </summary>
        public string YouKeNianLing { get; set; }
        /// <summary>
        /// 游客区域
        /// </summary>
        public string YouKeQuYu { get; set; }
        /// <summary>
        /// 是否会员
        /// </summary>
        public string ShiFouHuiYuan { get; set; }
        /// <summary>
        /// 订单金额条件
        /// </summary>
        public string DingDanJinETiaoJian { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string DingDanJinE { get; set; }
         /// <summary>
        /// 游客性别-0男1女
        /// </summary>
        public string  XingBie { get; set; }


    }
    #endregion

    #region 游轮航期标签信息业务实体
    /// <summary>
    /// 游轮航期标签信息业务实体
    /// </summary>
    public class MHangQiBiaoQianInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian BiaoQian { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 游轮航期浏览记录信息业务实体
    /// <summary>
    /// 游轮航期浏览记录信息业务实体
    /// </summary>
    public class MHangQiLiuLanJiLuInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 记录编号
        /// </summary>
        public string JiLuId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string YongHuId { get; set; }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 航期信息查询业务实体
    /// <summary>
    /// 航期信息查询业务实体
    /// </summary>
    public class MHangQiChaXunInfo
    {
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 航线编号
        /// </summary>
        public int? HangXianId { get; set; }
        /// <summary>
        /// 系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 出发港口编号
        /// </summary>
        public int? ChuFaGangKouId { get; set; }
        /// <summary>
        /// 出港时间-起始
        /// </summary>
        public DateTime? RiQi1 { get; set; }
        /// <summary>
        /// 出港时间-截止
        /// </summary>
        public DateTime? RiQi2 { get; set; }
        /// <summary>
        /// 起始价格-低
        /// </summary>
        public decimal? JiaGe1 { get; set; }
        /// <summary>
        /// 起始价格-高
        /// </summary>
        public decimal? JiaGe2 { get; set; }
        /// <summary>
        /// 天数-低
        /// </summary>
        public int? TianShu1 { get; set; }
        /// <summary>
        /// 天数-高
        /// </summary>
        public int? TianShu2 { get; set; }
        /// <summary>
        /// 航期标签
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian? BiaoQian { get; set; }
        /// <summary>
        /// 是否有效产品
        /// </summary>
        public bool? IsYouXiao { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string GuanJianZi { get; set; }
        /// <summary>
        /// 是否热销
        /// </summary>
        public bool? IsReXiao { get; set; }
        /// <summary>
        /// 航期名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string BianHao { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 游轮公司名称
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 游轮系列名称
        /// </summary>
        public string XiLieName { get; set; }
        /// <summary>
        /// 游轮船只名称
        /// </summary>
        public string ChuanZhiName { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }

        #region 多选查询
        /// <summary>
        /// 航线
        /// </summary>
        public int[] HX { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public string[] XL { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string[] GS { get; set; }
        /// <summary>
        /// 出发港口
        /// </summary>
        public int[] GK { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int[] TS { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int[] JG { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string[] YF { get; set; }

        /// <summary>
        /// 0:默认 1:销量ASC 2:销量DESC 3:价格ASC 4:价格DESC
        /// </summary>
        public int PaiXu { get; set; }
        #endregion
    }
    #endregion

    #region 游轮航期团购信息业务实体
    /// <summary>
    /// 游轮航期团购信息业务实体
    /// </summary>
    public class MTuanGouInfo
    {
        /// <summary>
        /// 团购编号
        /// </summary>
        public string TuanGouId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 出港日期
        /// </summary>
        public DateTime RiQi { get; set; }
        /// <summary>
        /// 团购名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string FengMian { get; set; }
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int GuoJiId { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal YuanJia { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime JieZhiShiJian { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 价格集合
        /// </summary>
        public IList<MTuanGouJiaGeInfo> JiaGes { get; set; }
        /// <summary>
        /// 团购编号
        /// </summary>
        public string BianHao { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 游轮公司名称
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string XiLieName { get; set; }
        /// <summary>
        /// 船只名称
        /// </summary>
        public string ChuanZhiName { get; set; }
        /// <summary>
        /// 已预订人数
        /// </summary>
        public int YiYuDingRenShu { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public decimal XianJia { get; set; }
        /// <summary>
        /// 宾客类型
        /// </summary>
        public string BinKeLeiXing { get; set; }
        /// <summary>
        /// 航期名称
        /// </summary>
        public string HangQiMingCheng { get; set; }
        /// <summary>
        /// 团购数【可维护】
        /// </summary>
        public int TuanGouShu { get; set; }
    }
    #endregion

    #region 游轮航期团购价格组成信息业务实体
    /// <summary>
    /// 游轮航期团购价格组成信息业务实体
    /// </summary>
    public class MTuanGouJiaGeInfo
    {
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
    }
    #endregion

    #region 游轮航期团购信息查询业务实体
    /// <summary>
    /// 游轮航期团购信息查询业务实体
    /// </summary>
    public class MTuanGouChaXunInfo
    {
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 航线编号
        /// </summary>
        public int? HangXianId { get; set; }
        /// <summary>
        /// 出发港口编号
        /// </summary>
        public int? ChuFaGangKouId { get; set; }
        /// <summary>
        /// 天数-低
        /// </summary>
        public int? TianShu1 { get; set; }
        /// <summary>
        /// 天数-高
        /// </summary>
        public int? TianShu2 { get; set; }
        /// <summary>
        /// 价格-低
        /// </summary>
        public decimal? JiaGe1 { get; set; }
        /// <summary>
        /// 价格-高
        /// </summary>
        public decimal? JiaGe2 { get; set; }
        /// <summary>
        /// 排序方式 0:出港时间DESC 1:出港时间ASC 2:价格DESC 3:价格ASC 4:销量DESC 5:销量ASC 6:航期排序DESC 7:航期排序ASC 8:团购数DESC 9:团购数ASC
        /// </summary>
        public int PaiXu { get; set; }
        /// <summary>
        /// 是否有效团购
        /// </summary>
        public bool? IsYouXiao { get; set; }
        /// <summary>
        /// 团购名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 团购编号
        /// </summary>
        public string BianHao { get; set; }
    }
    #endregion

    #region 航期关联航线信息实体
    /// <summary>
    /// 航期关联航线信息实体
    /// </summary>
    public class MGuanLianHangQiInfo
    {
        /// <summary>
        /// 航线名称
        /// </summary>
        public string HangXianMingCheng { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
    }
    #endregion

    #region 海洋邮轮价格信息
    /// <summary>
    /// 海洋邮轮价格信息
    /// </summary>
    public class MHYJiaGeInfo1
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 最底容纳人数
        /// </summary>
        public int RongNaRenShu { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal FangCha { get; set; }
        /// <summary>
        /// 价格说明
        /// </summary>
        public string ShuoMing { get; set; }
        /// <summary>
        /// 人员类型、价格集合
        /// </summary>
        public IList<MHYJiaGeInfo2> JiaGes { get; set; }
    }

    /// <summary>
    /// 海洋邮轮价格信息
    /// </summary>
    public class MHYJiaGeInfo2
    {
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
    }
    #endregion
}
