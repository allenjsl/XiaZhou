//航期订单相关业务实体 汪奇志 2014-03-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 游轮航期订单信息业务实体
    /// <summary>
    /// 游轮航期订单信息业务实体
    /// </summary>
    public class MHangQiDingDanInfo
    {
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId{ get; set; }
        /// <summary>
        /// 出港日期编号
        /// </summary>
        public string RiQiId{ get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId{ get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId{ get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string JiaoYiHao{ get; set; }
        /// <summary>
        /// 订单人数
        /// </summary>
        public int RenShu{ get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal JinE{ get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus DingDanStatus { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus FuKuanStatus { get; set; }
        /// <summary>
        /// 留位截止时间
        /// </summary>
        public DateTime LiuWeiDaoQiShiJian{ get; set; }
        /// <summary>
        /// 下单备注
        /// </summary>
        public string XiaDanBeiZhu{ get; set; }
        /// <summary>
        /// 预订人姓名
        /// </summary>
        public string YuDingRenName{ get; set; }
        /// <summary>
        /// 预订人电话
        /// </summary>
        public string YuDingRenDianHua{ get; set; }
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
        /// 是否团购订单
        /// </summary>
        public bool IsTuanGou { get; set; }
        /// <summary>
        /// 团购编号
        /// </summary>
        public string TuanGouId { get; set; }
        /// <summary>
        /// 附加产品集合
        /// </summary>
        public IList<MHangQiDingDanFuJiaChanPinInfo> FuJiaChanPins { get; set; }
        /// <summary>
        /// 游客集合
        /// </summary>
        public IList<MHangQiDingDanYouKeInfo> YouKes { get; set; }
        /// <summary>
        /// 价格集合
        /// </summary>
        public IList<MHangQiDingDanJiaGeInfo> JiaGes { get; set; }
        /// <summary>
        /// 优惠集合
        /// </summary>
        public IList<MHangQiDingDanYouHuiInfo> YouHuis { get; set; }
        /// <summary>
        /// 抵扣信息
        /// </summary>
        public MHangQiDingDanDiKouInfo DiKouInfo { get; set; }        
        /// <summary>
        /// 积分累积比例
        /// </summary>
        public decimal JiFenLeiJiBiLi { get; set; }
        /// <summary>
        /// 订单积分
        /// </summary>
        public decimal DingDanJiFen { get; set; }
        /// <summary>
        /// 操作备注
        /// </summary>
        public string CaoZuoBeiZhu { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string MingCheng { get; set; }
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
        /// 航线
        /// </summary>
        public string HangXian { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string BianHao { get; set; }
        /// <summary>
        /// 几天
        /// </summary>
        public int TianShu1 { get; set; }
        /// <summary>
        /// 几晚
        /// </summary>
        public int TianShu2 { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing YouLunLeiXing { get; set; }
        /// <summary>
        /// 自增编号
        /// </summary>
        public int IdentityId { get; set; }

        /// <summary>
        /// 海洋邮轮价格集合
        /// </summary>
        public IList<MHYDingDanJiaGeInfo1> HYJiaGes
        {
            get
            {
                if (YouLunLeiXing != EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮 && !IsTuanGou) return null;
                if (JiaGes == null || JiaGes.Count == 0) return null;

                List<EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo1> items = new List<EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo1>();

                foreach (var item in JiaGes)
                {
                    var item1 = items.FindLast(tmp =>
                    {
                        if (tmp.FangXingId == item.FangXingId
                            && tmp.LouCeng == item.LouCeng) return true;
                        return false;
                    });

                    item1 = item1 ?? new EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo1();

                    if (item1.FangXingId == 0)
                    {
                        item1.FangCha = item.FangCha;
                        item1.FangXingId = item.FangXingId;
                        item1.JiaGes = new List<EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo2>();
                        item1.LouCeng = item.LouCeng;
                        item1.RongNaRenShu = item.RongNaRenShu;

                        item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo2()
                        {
                            BinKeLeiXingId = item.BinKeLeiXingId,
                            JiaGe = item.JiaGe1,
                            RenShu = item.RenShu1
                        });

                        items.Add(item1);
                    }
                    else
                    {
                        item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MHYDingDanJiaGeInfo2()
                        {
                            BinKeLeiXingId = item.BinKeLeiXingId,
                            JiaGe = item.JiaGe1,
                            RenShu = item.RenShu1
                        });
                    }
                }

                return items;
            }
        }
        /// <summary>
        /// 长江邮轮价格集合
        /// </summary>
        public IList<MCJDingDanJiaGeInfo1> CJJiaGes
        {
            get
            {
                if (YouLunLeiXing != EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 && !IsTuanGou) return null;

                List<EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo1> items = new List<EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo1>();

                foreach (var item in JiaGes)
                {
                    var item1 = items.FindLast(tmp =>
                    {
                        if (tmp.GuoJiId == item.GuoJiId
                            && tmp.FangXingId == item.FangXingId) return true;
                        return false;
                    });

                    item1 = item1 ?? new EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo1();

                    if (item1.FangXingId == 0)
                    {
                        item1.FangXingId = item.FangXingId;
                        item1.JiaGes = new List<EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo2>();
                        item1.GuoJiId = item.GuoJiId;

                        item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo2()
                        {
                            BinKeLeiXingId = item.BinKeLeiXingId,
                            JiaGe1=item.JiaGe1,
                            JiaGe2=item.JiaGe2,
                            JiaGe3=item.JiaGe3,
                            JiaGe4=item.JiaGe4,
                            RenShu1=item.RenShu1,
                            RenShu2 = item.RenShu2,
                            RenShu3 = item.RenShu3,
                            RenShu4 = item.RenShu4
                        });

                        items.Add(item1);
                    }
                    else
                    {
                        item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MCJDingDanJiaGeInfo2()
                        {
                            BinKeLeiXingId = item.BinKeLeiXingId,
                            JiaGe1 = item.JiaGe1,
                            JiaGe2 = item.JiaGe2,
                            JiaGe3 = item.JiaGe3,
                            JiaGe4 = item.JiaGe4,
                            RenShu1 = item.RenShu1,
                            RenShu2 = item.RenShu2,
                            RenShu3 = item.RenShu3,
                            RenShu4 = item.RenShu4
                        });
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// 团购订单价格集合
        /// </summary>
        public IList<MTGDingDanJiaGeInfo> TGJiaGes
        {
            get
            {
                if (!IsTuanGou) return null;
                if (JiaGes != null && JiaGes.Count == 0) return null;
                IList<MTGDingDanJiaGeInfo> items = new List<MTGDingDanJiaGeInfo>();
                foreach (var item in JiaGes)
                {
                    var item1 = new MTGDingDanJiaGeInfo();
                    item1.BinKeLeiXingId = item.BinKeLeiXingId;
                    item1.JiaGe = item.JiaGe1;
                    item1.RenShu = item.RenShu1;
                    items.Add(item1);
                }
                return items;
            }

        }
        /// <summary>
        /// 出港日期
        /// </summary>
        public DateTime RiQi { get; set; }
    }
    #endregion

    #region 游轮航期订单附加产品信息业务实体
    /// <summary>
    /// 游轮航期订单附加产品信息业务实体
    /// </summary>
    public class MHangQiDingDanFuJiaChanPinInfo
    {
        /// <summary>
        /// 附加产品编号
        /// </summary>
        public string FuJiaChanPinId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DanJia { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 附加产品类型编号(OUTPUT)
        /// </summary>
        public int LeiXingId { get; set; }
        /// <summary>
        /// 服务项目(OUTPUT)
        /// </summary>
        public string XiangMu { get; set; }
        /// <summary>
        /// 计价单位(OUTPUT)
        /// </summary>
        public string DanWei { get; set; }
        /// <summary>
        /// 产品介绍(OUTPUT)
        /// </summary>
        public string JieShao { get; set; }
    }
    #endregion

    #region 游轮航期订单游客信息业务实体
    /// <summary>
    /// 游轮航期订单游客信息业务实体
    /// </summary>
    public class MHangQiDingDanYouKeInfo
    {
        /// <summary>
        /// 游客编号
        /// </summary>
        public string YouKeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisitorType LeiXing { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public int LeiXingId { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing ZhengJianLeiXing { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string ZhengJianHaoMa { get; set; }
        /// <summary>
        /// 证件有效期
        /// </summary>
        public DateTime? ZhengJianYouXiaoQi { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? ChuShengRiQi { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ShouJi { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int GuoJiaId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ShengFenId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int ChengShiId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int XianQuId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender XingBie { get; set; }

        public string YXQ1 { get; set; }
        public string YXQ2 { get; set; }
        public string YXQ3 { get; set; }
        public string SR1 { get; set; }
        public string SR2 { get; set; }
        public string SR3 { get; set; }
    }
    #endregion

    #region 游轮航期订单价格组成信息业务实体
    /// <summary>
    /// 游轮航期订单价格组成信息业务实体
    /// </summary>
    public class MHangQiDingDanJiaGeInfo
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 房型
        /// </summary>
        public string FangXing { get; set; }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int GuoJiId { get; set; }
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 最低容纳人数
        /// </summary>
        public int RongNaRenShu { get; set; }
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
        /// 人数
        /// </summary>
        public int RenShu1 { get; set; }
        /// <summary>
        /// 占床人数
        /// </summary>
        public int RenShu2 { get; set; }
        /// <summary>
        /// 加床人数
        /// </summary>
        public int RenShu3 { get; set; }
        /// <summary>
        /// 不占床人数
        /// </summary>
        public int RenShu4 { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal FangCha { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string LouCeng { get; set; }
    }
    #endregion

    #region 游轮航期订单优惠信息业务实体
    /// <summary>
    /// 游轮航期订单优惠信息业务实体
    /// </summary>
    public class MHangQiDingDanYouHuiInfo
    {
        /// <summary>
        /// 优惠规则
        /// </summary>
        public string GuiZe { get; set; }
        /// <summary>
        /// 规则描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal JinE { get; set; }
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
    }
    #endregion

    #region 游轮航期订单积分礼品卡抵扣金额信息业务实体
    /// <summary>
    /// 游轮航期订单积分礼品卡抵扣金额信息业务实体
    /// </summary>
    public class MHangQiDingDanDiKouInfo
    {
        /// <summary>
        /// 抵扣积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 积分比例
        /// </summary>
        public decimal JiFenBiLi { get; set; }
        /// <summary>
        /// 抵扣金额
        /// </summary>
        public decimal JinFenJinE { get; set; }
        /// <summary>
        /// 礼品卡抵扣金额
        /// </summary>
        public decimal LiPinKaJinE { get; set; }
    }
    #endregion

    #region 游轮航期团购订单价格组成信息业务实体
    /// <summary>
    /// 游轮航期团购订单价格组成信息业务实体
    /// </summary>
    public partial class MTGDingDanJiaGeInfo
    {
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int RenShu { get; set; }
    }
    #endregion

    #region 游轮订单款项信息业务实体
    /// <summary>
    /// 游轮订单款项信息业务实体
    /// </summary>
    public partial class MHangQiDingDanKuanInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 付款编号
        /// </summary>
        public string FuKuanId { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi FangShi { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus Status { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime ShiJian { get; set; }
        /// <summary>
        /// 付款备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion


    #region 航期订单信息查询业务实体
    /// <summary>
    /// 航期订单信息查询业务实体
    /// </summary>
    public class MHangQiDingDanChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string DingDanHao { get; set; }
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
        public EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus? DingDanStatus { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus? FuKuanStatus { get; set; }
        /// <summary>
        /// 订单类型 0：长江 1:海洋 2：团购
        /// </summary>
        public int? DingDanLeiXing { get; set; }
    }
    #endregion

    #region 海洋邮轮价格信息
    /// <summary>
    /// 海洋邮轮订单价格信息
    /// </summary>
    public class MHYDingDanJiaGeInfo1
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
        /// 人员类型、人数、价格集合
        /// </summary>
        public IList<MHYDingDanJiaGeInfo2> JiaGes { get; set; }
        /// <summary>
        /// 人数：各人员类型人数小计
        /// </summary>
        public int RenShu
        {
            get
            {
                if (JiaGes == null || JiaGes.Count == 0) return 0;
                int renShu = 0;
                foreach (var item in JiaGes)
                {
                    renShu += item.RenShu;
                }
                return renShu;
            }
        }
        /// <summary>
        /// 金额：各人员类型价格小计
        /// </summary>
        public decimal JinE
        {
            get
            {
                if (RenShu == 0) return 0;
                decimal jinE = 0;

                foreach (var item in JiaGes)
                {
                    jinE += item.RenShu * item.JiaGe;
                }

                if (RongNaRenShu > 0)
                {
                    int _m = RenShu % RongNaRenShu;

                    if (_m > 0)
                    {
                        int buFangChaRenShu = RongNaRenShu - _m;
                        decimal fangChaJinE = buFangChaRenShu * FangCha;
                        jinE += fangChaJinE;
                    }
                }

                return jinE;
            }
        }
    }

    /// <summary>
    /// 海洋邮轮订单价格信息
    /// </summary>
    public class MHYDingDanJiaGeInfo2
    {
        /// <summary>
        /// 宾客类型编号
        /// </summary>
        public int BinKeLeiXingId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int RenShu { get; set; }
    }
    #endregion

    #region 长江游轮订单价格信息
    /// <summary>
    /// 长江游轮订单价格信息
    /// </summary>
    public class MCJDingDanJiaGeInfo1
    {
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int GuoJiId { get; set; }
        /// <summary>
        /// 房型编号
        /// </summary>
        public int FangXingId { get; set; }
        /// <summary>
        /// 价格集合
        /// </summary>
        public IList<MCJDingDanJiaGeInfo2> JiaGes { get; set; }
    }

    /// <summary>
    /// 长江游轮订单价格信息
    /// </summary>
    public class MCJDingDanJiaGeInfo2
    {
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
        /// 人数
        /// </summary>
        public int RenShu1 { get; set; }
        /// <summary>
        /// 占床人数
        /// </summary>
        public int RenShu2 { get; set; }
        /// <summary>
        /// 加床人数
        /// </summary>
        public int RenShu3 { get; set; }
        /// <summary>
        /// 不占床人数
        /// </summary>
        public int RenShu4 { get; set; }
    }
    #endregion
}
