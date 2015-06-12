//预算表信息业务实体 汪奇志 2014-02-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.StatStructure
{
    #region 预算表信息业务实体
    /// <summary>
    /// 预算表信息业务实体
    /// </summary>
    public class MYuSuanBiaoInfo
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime ChuTuanRiQi { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int TianShu { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime HuiTuanRiQi
        {
            get { return ChuTuanRiQi.AddDays(TianShu - 1); }
        }
        /// <summary>
        /// 实收人数
        /// </summary>
        public int ShiShouRenShu { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal ShouRuJinE { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal ZhiChuJinE { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal MaoLi
        {
            get
            {
                return ShouRuJinE - ZhiChuJinE;
            }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string XianLuName { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
    }
    #endregion

    #region 预算表收入信息业务实体
    /// <summary>
    /// 预算表收入信息业务实体
    /// </summary>
    public class MYuSuanBiaoShouRuInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string KeHuName { get; set; }
        /// <summary>
        /// 确认金额(合同金额已确认时确认金额||合同金额未确认时合同金额)
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 确认状态(合同金额是否确认)
        /// </summary>
        public bool QueRenZhuangTai { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime XiaDanTime { get; set; }
        /// <summary>
        /// 实收人数
        /// </summary>
        public int ShiShouRenShu { get; set; }
    }
    #endregion

    #region 预算表支出信息业务实体
    /// <summary>
    /// 预算表支出信息业务实体
    /// </summary>
    public class MYuSuanBiaoZhiChuInfo
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 计调类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject LeiXing { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 费用明细
        /// </summary>
        public string FeiYongMingXi { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.Payment ZhiFuFangShi { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }        
    }
    #endregion

    #region 预算表查询信息业务实体
    /// <summary>
    /// 预算表查询信息业务实体
    /// </summary>
    public class MYuSuanBiaoChaXunInfo
    {
        /// <summary>
        /// 出团日期-起始
        /// </summary>
        public DateTime? ChuTuanSRiQi { get; set; }
        /// <summary>
        /// 出团日期-截止
        /// </summary>
        public DateTime? ChuTuanERiQi { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string XiaoShouYuanId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string XianLuName { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus[] TourStatus { get; set; }
        /// <summary>
        /// 计划销售员部门
        /// </summary>
        public int[] TourSellerDeptIds { get; set; }
    }
    #endregion
}
