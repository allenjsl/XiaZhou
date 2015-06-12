//YL网站会员相关 汪奇志 2014-03-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.YlStructure;

namespace EyouSoft.Model.YlStructure
{
    #region 网站会员信息业务实体
    /// <summary>
    /// 网站会员信息业务实体
    /// </summary>
    public class MHuiYuanInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 会员账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 会员密码（MD5）
        /// </summary>
        public string MD5Password { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender XingBie { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ShouJi { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string YouXiang { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus Status { get; set; }
        /// <summary>
        /// 会员图像
        /// </summary>
        public string TuXiang { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime ZhuCeShiJian { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing LeiXing { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing PingTaiLeiXing { get; set; }
        /// <summary>
        /// 平台OpenID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 可用积分
        /// </summary>
        public decimal KeYongJiFen { get; set; }
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
        /// 联系地址
        /// </summary>
        public string DiZhi { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime ShengRi { get; set; }
        /// <summary>
        /// 可用金额
        /// </summary>
        public decimal KeYongJinE { get; set; }
        /// <summary>
        /// 待付款订单数
        /// </summary>
        public int DaiFuKuanDingDanShu { get; set; }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int ShouCangShu { get; set; }
    }
    #endregion

    #region 游轮网站会员常旅客信息业务实体
    /// <summary>
    /// 游轮网站会员常旅客信息业务实体
    /// </summary>
    public class MHuiYuanChangLvKeInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 旅客编号
        /// </summary>
        public string LvkeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisitorType LeiXing { get; set; }
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
        /// 状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus ZhuangTai { get; set; }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int GuoJiId { get; set; }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
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

    #region 游轮网站会员常用地址信息业务实体
    /// <summary>
    /// 游轮网站会员常用地址信息业务实体
    /// </summary>
    public class MHuiYuanDiZhiInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 地址编号
        /// </summary>
        public string DiZhiId { get; set; }
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
        /// 地址
        /// </summary>
        public string DiZhi { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string YouBian { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否默认地址
        /// </summary>
        public bool IsMoRen { get; set; }
    }
    #endregion

    #region 游轮网站游轮攻略信息业务实体
    /// <summary>
    /// 游轮网站游轮攻略信息业务实体
    /// </summary>
    public class MWzGongLueInfo
    {
        /// <summary>
        /// 攻略编号
        /// </summary>
        public string GongLueId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string BiaoTi { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 游轮船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 出港日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsShenHe { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ShenHeOperatorId { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ShenHeTime { get; set; }
    }
    #endregion

    #region 游轮网站用户点评信息业务实体
    /// <summary>
    /// 游轮网站用户点评信息业务实体
    /// </summary>    
    public class MWzDianPingInfo
    {
        /// <summary>
        /// 点评编号
        /// </summary>
        public string DianPingId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 游轮船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 出港日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 点评内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 点评时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsShenHe { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ShenHeOperatorId { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ShenHeTime { get; set; }
        /// <summary>
        /// 点评分数
        /// </summary>
        public decimal FenShu { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing DingDanLeiXing { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsNiMing { get; set; }
        /// <summary>
        /// 点评标题
        /// </summary>
        public string BiaoTi { get; set; }
    }
    #endregion

    #region 游轮网站咨询问答信息业务实体
    /// <summary>
    /// 游轮网站咨询问答信息业务实体
    /// </summary>
    public partial class MWzWenDaInfo
    {
        /// <summary>
        /// 问答编号
        /// </summary>
        public string WenDaId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 问标题
        /// </summary>
        public string WenBiaoTi { get; set; }
        /// <summary>
        /// 问内容
        /// </summary>
        public string WenNeiRong { get; set; }
        /// <summary>
        /// 问时间
        /// </summary>
        public DateTime WenShiJian { get; set; }
        /// <summary>
        /// 问用户编号
        /// </summary>
        public string WenYongHuId { get; set; }
        /// <summary>
        /// 答用户编号
        /// </summary>
        public string DaOperatorId { get; set; }
        /// <summary>
        /// 答内容
        /// </summary>
        public string DaNeiRong { get; set; }
        /// <summary>
        /// 答时间
        /// </summary>
        public DateTime? DaShiJian { get; set; }
        /// <summary>
        /// 问答分类
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing LeiXing { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsNiMing { get; set; }
    }
    #endregion

    #region 游轮网站会员收藏夹信息业务实体
    /// <summary>
    /// 游轮网站会员收藏夹信息业务实体
    /// </summary>
    public partial class MHuiYuanShouCangJiaInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 收藏编号
        /// </summary>
        public string ShouCangId { get; set; }
        /// <summary>
        /// 收藏类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing LeiXing { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime ShiJian { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string CPName { get; set; }
        /// <summary>
        /// 产品金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsYouXiao { get; set; }
    }
    #endregion




    #region 网站会员信息查询业务实体
    /// <summary>
    /// 网站会员信息查询业务实体
    /// </summary>
    public class MHuiYuanChaXunInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 注册时间-起
        /// </summary>
        public DateTime? ZhuCeShiJian1 { get; set; }
        /// <summary>
        /// 注册时间止
        /// </summary>
        public DateTime? ZhuCeShiJian2 { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LianXiFangShi { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing? LeiXing { get; set; }
    }
    #endregion

    #region 网站会员常旅客信息查询业务实体
    /// <summary>
    /// 网站会员常旅客信息查询业务实体
    /// </summary>
    public class MHuiYuanChangLvKeChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
    }
    #endregion

    #region 游轮网站会员常用地址信息查询业务实体
    /// <summary>
    /// 游轮网站会员常用地址信息查询业务实体
    /// </summary>
    public class MHuiYuanDiZhiChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
    }
    #endregion

    #region 游轮网站游轮攻略信息查询业务实体
    /// <summary>
    /// 游轮网站游轮攻略信息查询业务实体
    /// </summary>
    public class MWzGongLueChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
    }
    #endregion

    #region 游轮网站用户点评信息查询业务实体
    /// <summary>
    /// 游轮网站用户点评信息查询业务实体
    /// </summary>    
    public class MWzDianPingChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool? IsShenHe { get; set; }
    }
    #endregion

    #region 游轮网站咨询问答信息查询业务实体
    /// <summary>
    /// 游轮网站咨询问答信息查询业务实体
    /// </summary>    
    public class MWzWenDaChaXunInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 是否回复
        /// </summary>
        public bool? IsHuiFu { get; set; }
        /// <summary>
        /// 问答类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing? LeiXing { get; set; }
    }
    #endregion

    #region 游轮网站会员收藏夹信息查询业务实体
    /// <summary>
    /// 游轮网站会员收藏夹信息查询业务实体
    /// </summary>
    public partial class MHuiYuanShouCangJiaChaXunInfo
    {
    }
    #endregion

    #region 游轮网站会员积分明细信息
    /// <summary>
    /// 游轮网站会员积分明细信息
    /// </summary>
    public class MHuiYuanJiFenMxInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 明细编号
        /// </summary>
        public string MingXiId { get; set; }
        /// <summary>
        /// 明细类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing LeiXing { get; set; }
        /// <summary>
        /// 积分数量
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 积分时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public string GuanLianId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string CPName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string CPId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 是否团购
        /// </summary>
        public bool IsTuanGou { get; set; }
        /// <summary>
        /// 团购编号
        /// </summary>
        public string TuanGouId { get; set; }
        /// <summary>
        /// 航期类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? HQLeiXing { get; set; }
    }
    #endregion

    #region 游轮网站会员积分明细信息查询
    /// <summary>
    /// 游轮网站会员积分明细信息查询
    /// </summary>
    public class MHuiYuanJiFenMxChaXunInfo
    {
    }
    #endregion

    #region 游轮网站会员预存款明细
    /// <summary>
    /// 游轮网站会员预存款明细
    /// </summary>
    public class MHuiYuanYuCunKuanMxInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 明细编号
        /// </summary>
        public string MingXiId { get; set; }
        /// <summary>
        /// 明细类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YuCunKuanMxLeiXing LeiXing { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 积分时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public string GuanLianId { get; set; }
    }
    #endregion

    #region 游轮网站会员预存款明细查询
    /// <summary>
    /// 游轮网站会员预存款明细查询
    /// </summary>
    public class MHuiYuanYuCunKuanMxChaXunInfo
    {
    }
    #endregion

    #region 会员订单信息业务实体
    /// <summary>
    /// 会员订单信息业务实体
    /// </summary>
    public class MHuiYuanDingDanInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string CPName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string CPId { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 是否团购
        /// </summary>
        public bool IsTuanGou { get; set; }
        /// <summary>
        /// 团购编号
        /// </summary>
        public string TuanGouId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing DingDanLeiXing { get; set; }
        /// <summary>
        /// 航期订单-状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus? HQStatus { get; set; }
        /// <summary>
        /// 积分订单-状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus? JFStatus { get; set; }
        /// <summary>
        /// 获得积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.FuKuanStatus FuKuanStatus { get; set; }
        /// <summary>
        /// 航期类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? HQLeiXing { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
    }
    #endregion

    #region 会员订单查询实体
    /// <summary>
    /// 会员订单查询实体
    /// </summary>
    public class MHuiYuanDingDanChaXunInfo
    {
    }
    #endregion
}
