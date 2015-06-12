using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GysStructure
{
    #region 供应商交易明细汇总信息业务实体
    /// <summary>
    /// 供应商交易明细汇总信息业务实体
    /// </summary>
    public class MJiaoYiXXInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiaoYiXXInfo() { }
        /// <summary>
        /// 交易次数
        /// </summary>
        public int JiaoYiCiShu { get; set; }
        /// <summary>
        /// 交易数量
        /// </summary>
        public int JiaoYiShuLiang { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal JieSuanJinE { get; set; }
        /// <summary>
        /// 已支付金额
        /// </summary>
        public decimal YiZhiFuJinE { get; set; }
        /// <summary>
        /// 质检均分
        /// </summary>
        public decimal ZhiJianJunFen { get; set; }
        /// <summary>
        /// 未支付金额
        /// </summary>
        public decimal WeiZhiFuJinE { get { return JieSuanJinE - YiZhiFuJinE; } }
        /// <summary>
        /// 交易数量（decimal）
        /// </summary>
        public decimal DJiaoYiShuLiang { get; set; }
    }
    #endregion

    #region 供应商联系人信息业务实体
    /// <summary>
    /// 供应商联系人信息业务实体
    /// </summary>
    public class MLxrInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLxrInfo() { }
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string LxrId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string ZhiWu { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 电子信箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
    }
    #endregion

    #region 供应商账号线路区域信息集合
    /// <summary>
    /// 供应商账号线路区域信息集合
    /// </summary>
    public class MGysUserAreaInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGysUserAreaInfo() { }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
    }
    #endregion

    #region 供应商账号信息业务实体
    /// <summary>
    /// 供应商账号信息业务实体
    /// </summary>
    public class MGysUserInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGysUserInfo() { }
        /// <summary>
        /// 账号状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserStatus Status { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 线路区域信息集合
        /// </summary>
        public IList<MGysUserAreaInfo> Areas { get; set; }
    }
    #endregion

    #region 供应商-酒店房型信息业务实体
    /// <summary>
    /// 供应商-酒店房型信息业务实体
    /// </summary>
    public class MJiuDianFangXingInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiuDianFangXingInfo() { }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string FangXingId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string FangXingName { get; set; }
        /// <summary>
        /// 前台销售价
        /// </summary>
        public decimal JiaGeQT { get; set; }
        /// <summary>
        /// 网络价
        /// </summary>
        public decimal JiaGeWL { get; set; }
        /// <summary>
        /// 散客价
        /// </summary>
        public decimal JiaGeSK { get; set; }
        /// <summary>
        /// 团队价淡季
        /// </summary>
        public decimal JiaGeDJ { get; set; }
        /// <summary>
        /// 团队价平季
        /// </summary>
        public decimal JiaGePJ { get; set; }
        /// <summary>
        /// 团队价旺季
        /// </summary>
        public decimal JiaGeWJ { get; set; }
        /// <summary>
        /// 是否含早
        /// </summary>
        public bool IsHanZao { get; set; }
    }
    #endregion

    #region 供应商-景点景点信息业务实体
    /// <summary>
    /// 供应商-景点景点信息业务实体
    /// </summary>
    public class MJingDianJingDianInfo
    {
        /// <summary>
        /// 景点编号
        /// </summary>
        public string JingDianId { get; set; }
        /// <summary>
        /// 挂牌价
        /// </summary>
        public decimal JiaGeGP { get; set; }
        /// <summary>
        /// 散客价
        /// </summary>
        public decimal JiaGeSK { get; set; }
        /// <summary>
        /// 团队价
        /// </summary>
        public decimal JiaGeTD { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal JiaGeET { get; set; }
        /// <summary>
        /// 60-70老人价
        /// </summary>
        public decimal JiaGeLR67 { get; set; }
        /// <summary>
        /// 70老人价
        /// </summary>
        public decimal JiaGeLR7 { get; set; }
        /// <summary>
        /// 学生价
        /// </summary>
        public decimal JiaGeXS { get; set; }
        /// <summary>
        /// 军人价
        /// </summary>
        public decimal JiaGeJR { get; set; }
        /// <summary>
        /// 导游词
        /// </summary>
        public string DaoYouCi { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public Model.EnumType.SourceStructure.SpotStar XingJi { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Name { get; set; }
    }
    #endregion

    #region 供应商-游轮游船信息业务实体
    /// <summary>
    /// 供应商-游轮游船信息业务实体
    /// </summary>
    public class MYouLunYouChuanInfo
    {
        /// <summary>
        /// 游船编号
        /// </summary>
        public string YouChuanId { get; set; }
        /// <summary>
        /// 游船名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 游船星级(三星,四星,五星)
        /// </summary>
        public Model.EnumType.SourceStructure.ShipStar XingJi { get; set; }
        /// <summary>
        /// 舱位数
        /// </summary>
        public int CangWeiShu { get; set; }
        /// <summary>
        /// 船载电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LxrName { get; set; }
        /// <summary>
        /// 航线
        /// </summary>
        public string HangXian { get; set; }
        /// <summary>
        /// 游船附件
        /// </summary>
        public MFuJianInfo FuJian { get; set; }
    }
    #endregion

    #region 供应商交易明细信息业务实体
    /// <summary>
    /// 供应商交易明细信息业务实体
    /// </summary>
    public class MJiaoYiMingXiInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiaoYiMingXiInfo() { }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouname { get; set; }
        /// <summary>
        /// 交易数量（int）
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 交易数量（DECIMAL）
        /// </summary>
        public decimal DShuLiang { get; set; }
        /// <summary>
        /// 费用明细
        /// </summary>
        public string FeiYongMingXi { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 已支付金额
        /// </summary>
        public decimal YiZhiFuJinE { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 安排类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject AnPaiLeiXing { get; set; }
        /// <summary>
        /// 安排添加类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus AnPaiTianJiaLeiXing { get; set; }
        /// <summary>
        /// 计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }

        /// <summary>
        /// 未支付金额
        /// </summary>
        public decimal WeiZhiFuJinE
        {
            get
            {
                return JinE - YiZhiFuJinE;
            }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.Payment ZhiFuFangShi { get; set; }
    }
    #endregion 供应商交易明细信息业务实体

    #region 供应商交易明细查询信息业务实体
    /// <summary>
    /// 供应商交易明细查询信息业务实体
    /// </summary>
    public class MJiaoYiMingXiChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiaoYiMingXiChaXunInfo() { }
        /// <summary>
        /// 出团开始时间
        /// </summary>
        public DateTime? LSDate { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? LEDate { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType? GysLeiXing { get; set; }
    }
    #endregion

    #region 供应商-车队车型信息业务实体
    /// <summary>
    /// 供应商-车队车型信息业务实体
    /// </summary>
    public class MCheDuiCheXingInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCheDuiCheXingInfo() { }
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CheXingId { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public int ZuoWeiShu { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string CheHao { get; set; }
        /// <summary>
        /// 司机姓名
        /// </summary>
        public string SiJiName { get; set; }
        /// <summary>
        /// 司机电话
        /// </summary>
        public string SiJiTelephone { get; set; }
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool IsBaoXian { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public decimal BaoXianJinE { get; set; }
        /// <summary>
        /// 保险期限
        /// </summary>
        public DateTime? BaoXianQiXian { get; set; }
    }
    #endregion

    #region 供应商合同信息业务实体
    /// <summary>
    /// 供应商合同信息业务实体
    /// </summary>
    public class MHeTongInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MHeTongInfo() { }
        /// <summary>
        /// 是否签订合同
        /// </summary>
        public bool IsQianDingHeTong { get; set; }
        /// <summary>
        /// 有效期起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 有效期截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        public string FilePath { get; set; }
    }
    #endregion

    #region 供应商附件信息业务实体
    /// <summary>
    /// 供应商附件信息业务实体
    /// </summary>
    public class MFuJianInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MFuJianInfo() { }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FilePath { get; set; }
    }
    #endregion

    #region 供应商-游轮信息业务实体
    /// <summary>
    /// 供应商-游轮信息业务实体
    /// </summary>
    public class MYouLunInfo
    {
        /// <summary>
        /// 公司电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 价格体系
        /// </summary>
        public string JiaGeTiXi { get; set; }
        /// <summary>
        /// 开航起始时间
        /// </summary>
        public DateTime? KaiHangSTime { get; set; }
        /// <summary>
        /// 开航截止时间
        /// </summary>
        public DateTime? KaiHangETime { get; set; }
        /// <summary>
        /// 浏览景点
        /// </summary>
        public string JingDian { get; set; }
        /// <summary>
        /// 自费景点
        /// </summary>
        public string ZiFeiJingDian { get; set; }
    }
    #endregion

    #region 供应商信息业务实体
    /// <summary>
    /// 供应商信息业务实体
    /// </summary>
    public class MGysInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGysInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType LeiXing { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 国家省份城市县区
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string XuKeZhengCode { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string FaRenName { get; set; }
        /// <summary>
        /// 法人电话
        /// </summary>
        public string FaRenTelephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string JianJie { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 是否返利
        /// </summary>
        public bool IsFanLi { get; set; }
        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsQianDan { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 是否返单
        /// </summary>
        public bool IsFanDan { get; set; }
        /// <summary>
        /// 政策
        /// </summary>
        public string ZhengCe { get; set; }
        /// <summary>
        /// 合作协议路径
        /// </summary>
        public string HeZuoXieYiFilePaht { get; set; }
        /// <summary>
        /// 合同信息
        /// </summary>
        public MHeTongInfo HeTong { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<MLxrInfo> Lxrs { get; set; }
        /// <summary>
        /// 酒店-房型信息集合
        /// </summary>
        public IList<MJiuDianFangXingInfo> FangXings { get; set; }
        /// <summary>
        /// 景点-景点信息集合
        /// </summary>
        public IList<MJingDianJingDianInfo> JingDians { get; set; }
        /// <summary>
        /// 游轮-游船信息集合
        /// </summary>
        public IList<MYouLunYouChuanInfo> YouChuans { get; set; }
        /// <summary>
        /// 车队-车型信息集合
        /// </summary>
        public IList<MCheDuiCheXingInfo> CheXings { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 餐馆-餐标
        /// </summary>
        public string CanGuanCanBiao { get; set; }
        /// <summary>
        /// 餐馆-菜系
        /// </summary>
        public IList<EyouSoft.Model.EnumType.SourceStructure.SourceCuisine> CaiXis { get; set; }
        /// <summary>
        /// 酒店前台电话
        /// </summary>
        public string JiuDianQianTaiTelephone { get; set; }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.HotelStar JiuDianXingJi { get; set; }
        /// <summary>
        /// 游轮信息
        /// </summary>
        public MYouLunInfo YouLun { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 最后操作人编号(OUTPUT)
        /// </summary>
        public string LatestOperatorId { get; set; }
        /// <summary>
        /// 最后操作人姓名(OUTPUT)
        /// </summary>
        public string LatestOperatorName { get; set; }
        /// <summary>
        /// 最后操作人时间(OUTPUT)
        /// </summary>
        public DateTime LatestTime { get; set; }
    }
    #endregion
}
