//游轮网站相关 汪奇志 2014-03-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 游轮网站介绍信息业务实体
    /// <summary>
    /// 游轮网站介绍信息业务实体
    /// </summary>
    public class MWzKvInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// K
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzKvKey K { get; set; }
        /// <summary>
        /// V
        /// </summary>
        public string V { get; set; }
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

    #region 游轮网站域名信息业务实体
    /// <summary>
    /// 游轮网站域名信息业务实体
    /// </summary>
    public class MWzYuMingInfo
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string YuMing { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// ERP域名
        /// </summary>
        public string ErpYuMing { get; set; }
    }
    #endregion

    #region 游轮网站友情链接信息业务实体
    /// <summary>
    /// 游轮网站友情链接信息业务实体
    /// </summary>
    public class MWzYouQingLianJieInfo
    {
        /// <summary>
        /// 链接编号
        /// </summary>
        public string LianJieId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 链接图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 链接类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing LeiXing { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
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

    #region 游轮网站广告信息业务实体
    /// <summary>
    /// 游轮网站广告信息业务实体
    /// </summary>
    public class MWzGuangGaoInfo
    {
        /// <summary>
        /// 广告编号
        /// </summary>
        public string GuangGaoId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 广告位置
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi WeiZhi { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 广告链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string XiangXiNeiRong { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int PaiXuId { get; set; }
    }
    #endregion

    #region 游轮网站资讯信息业务实体
    /// <summary>
    /// 游轮网站资讯信息业务实体
    /// </summary>
    public class MWzZiXunInfo
    {
        /// <summary>
        /// 资讯编号
        /// </summary>
        public string ZiXunId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 资讯类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing LeiXing { get; set; }
        /// <summary>
        /// 资讯标题
        /// </summary>
        public string BiaoTi { get; set; }
        /// <summary>
        /// 资讯内容
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
    }
    #endregion

    #region 游轮网站公司荣誉信息业务实体
    /// <summary>
    /// 游轮网站公司荣誉信息业务实体
    /// </summary>
    public class MWzGongSiRongYuInfo
    {
        /// <summary>
        /// 荣誉编号
        /// </summary>
        public string RongYuId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 荣誉名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 游轮网站员工风采信息业务实体
    /// <summary>
    /// 游轮网站员工风采信息业务实体
    /// </summary>
    public class MWzYuanGongFengCaiInfo
    {
        /// <summary>
        /// 风采编号
        /// </summary>
        public string FengCaiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 风采名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 游轮网站招聘岗位信息业务实体
    /// <summary>
    /// 游轮网站招聘岗位信息业务实体
    /// </summary>
    public class MWzZhaoPinGangWeiInfo
    {
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string GangWeiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 游轮网站会议案例信息业务实体
    /// <summary>
    /// 游轮网站会议案例信息业务实体
    /// </summary>
    public class MWzHuiYiAnLiInfo
    {
        /// <summary>
        /// 案例编号
        /// </summary>
        public string AnLiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
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
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing { get; set; }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 会议价格
        /// </summary>
        public string JiaGe { get; set; }
        /// <summary>
        /// 参会人数
        /// </summary>
        public string RenShu { get; set; }
        /// <summary>
        /// 举办单位
        /// </summary>
        public string DanWei { get; set; }
        /// <summary>
        /// 会议时间
        /// </summary>
        public string ShiJian { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime ShiJian1 { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ShiJian2 { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 会议图片
        /// </summary>
        public string Filepath { get; set; }
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

    #region 游轮网站会议申请信息业务实体
    /// <summary>
    /// 游轮网站会议申请信息业务实体
    /// </summary>
    public partial class MWzHuiYiShenQingInfo
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        public string ShenQingId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 会议规模
        /// </summary>
        public string GuiMo { get; set; }
        /// <summary>
        /// 会议预计时间
        /// </summary>
        public string YuJiShiJian { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LxrXingMing { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string LxrShouJi { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string LxrYouXiang { get; set; }
        /// <summary>
        /// 联系人地址
        /// </summary>
        public string LxrDiZhi { get; set; }
        /// <summary>
        /// 联系人国家编号
        /// </summary>
        public int LxrGuoJiaId { get; set; }
        /// <summary>
        /// 联系人省份编号
        /// </summary>
        public int LxrShengFenId { get; set; }
        /// <summary>
        /// 联系人城市编号
        /// </summary>
        public int LxrChengShiId { get; set; }
        /// <summary>
        /// 联系人县区编号
        /// </summary>
        public int LxrXianQuId { get; set; }
        /// <summary>
        /// 行业名称
        /// </summary>
        public string HangYeMingCheng { get; set; }
        /// <summary>
        /// 行业联系手机
        /// </summary>
        public string HangYeLxShouJi { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 处理备注
        /// </summary>
        public string ChuLiBeiZhu { get; set; }
        /// <summary>
        /// 处理人编号
        /// </summary>
        public string ChuLiOperatorId { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ChuLiShiJian { get; set; }
    }
    #endregion



    #region 友情链接信息查询业务实体
    /// <summary>
    /// 友情链接信息查询业务实体
    /// </summary>
    public class MWzYouQingLianJieChaXunInfo
    {
        /// <summary>
        /// 链接类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing? LeiXing { get; set; }
    }
    #endregion

    #region 游轮网站广告信息查询实体
    /// <summary>
    /// 游轮网站广告信息查询实体
    /// </summary>
    public class MWzGuangGaoChaXunInfo
    {
        /// <summary>
        /// 广告位置
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi? WeiZhi { get; set; }
    }
    #endregion

    #region 游轮网站资讯信息业务实体
    /// <summary>
    /// 游轮网站资讯信息业务实体
    /// </summary>
    public class MWzZiXunChaXunInfo
    {
        /// <summary>
        /// 资讯类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string BiaoTi { get; set; }
    }
    #endregion

    #region 游轮网站公司荣誉信息查询业务实体
    /// <summary>
    /// 游轮网站公司荣誉信息查询业务实体
    /// </summary>
    public class MWzGongSiRongYuChaXunInfo
    {
    }
    #endregion

    #region 游轮网站员工风采信息业务实体
    /// <summary>
    /// 游轮网站员工风采信息业务实体
    /// </summary>
    public class MWzYuanGongFengCaiChaXunInfo
    {
    }
    #endregion

    #region 游轮网站招聘岗位信息查询业务实体
    /// <summary>
    /// 游轮网站招聘岗位信息查询业务实体
    /// </summary>
    public class MWzZhaoPinGangWeiChaXunInfo
    {
    }
    #endregion

    #region 游轮网站会议案例信息查询业务实体
    /// <summary>
    /// 游轮网站会议案例信息查询业务实体
    /// </summary>
    public class MWzHuiYiAnLiChaXunInfo
    {
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 会议时间-起始
        /// </summary>
        public DateTime? ShiJian1 { get; set; }
        /// <summary>
        /// 会议时间-截止
        /// </summary>
        public DateTime? ShiJian2 { get; set; }
    }
    #endregion

    #region 游轮网站会议申请信息查询业务实体
    /// <summary>
    /// 游轮网站会议申请信息查询业务实体
    /// </summary>
    public partial class MWzHuiYiShenQingChaXunInfo
    {
        /// <summary>
        /// 申请时间-起
        /// </summary>
        public DateTime? ShenQingShiJian1 { get; set; }
        /// <summary>
        /// 申请时间-止
        /// </summary>
        public DateTime? ShenQingShiJian2 { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing { get; set; }
    }
    #endregion

    #region 游轮网站意见反馈信息查询
    /// <summary>
    /// 游轮网站意见反馈信息查询
    /// </summary>
    public class MWzYiJianFanKuiChaXun
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 意见反馈类型
        /// </summary>
        public Model.EnumType.YlStructure.YiJianFanKuiLeiXing? LeiXing { get; set; }
    }
    #endregion

    #region 游轮网站意见反馈信息实体
    /// <summary>
    /// 游轮网站意见反馈信息实体
    /// </summary>
    public class MWzYiJianFanKuiInfo : MWzYiJianFanKuiChaXun
    {
        /// <summary>
        /// 意见编号
        /// </summary>
        public string YiJianId { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string RemoteIP { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// 会员编号、操作者编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion
}
