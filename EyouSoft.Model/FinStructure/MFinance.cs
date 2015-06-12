using System;
using System.Collections.Generic;

//财务管理
//创建者：郑知远
//创建时间：2011-09-01
namespace EyouSoft.Model.FinStructure
{
    using EyouSoft.Model.CrmStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    #region 单团核算基础实体
    /// <summary>
    /// 单团核算基础实体
    /// </summary>
    [Serializable]
    public class MTourCheckBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTourCheckBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 查询条件：出团时间_开始
        /// </summary>
        public string LDateStart { get; set; }

        /// <summary>
        /// 查询条件：出团时间_结束
        /// </summary>
        public string LDateEnd { get; set; }

        /// <summary>
        /// 财务待核算/封团
        /// </summary>
        public TourStatus TourStatus { get; set; }

        /// <summary>
        /// 团队销售员编号
        /// </summary>
        public string SaleId { get; set; }

        /// <summary>
        /// 团队销售员
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId { get; set; }

        /// <summary>
        /// 导游
        /// </summary>
        public string Guide { get; set; }
    }
#endregion

    #region 单团核算信息实体
    /// <summary>
    /// 单团核算信息实体
    /// 创建者：郑知远
    /// 创建时间：2011/09/01
    /// </summary>
    [Serializable]
    public class MTourCheck : MTourCheckBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MTourCheck() { }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 人数（成人+儿童+其他）
        /// </summary>
        public string PeopleS { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 收入
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// 支出
        /// </summary>
        public decimal Outlay { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfit { get{return this.Income - this.Outlay;} }

        /// <summary>
        /// 利润分配
        /// </summary>
        public decimal ProfitDistribute { get; set; }

        /// <summary>
        /// 纯利
        /// </summary>
        public decimal PureProfit { get { return this.GrossProfit - this.ProfitDistribute; } }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfitRate { get{return this.GrossProfit / this.Income;} }

        /// <summary>
        /// 团款收入明细
        /// </summary>
        public IList<MTourIncome>  TourIncomeLst { get; set; }

        /// <summary>
        /// 其他收入明细
        /// </summary>
        public IList<MOtherFeeInOut> OtherIncomeLst { get; set; }

        /// <summary>
        /// 团款支出明细
        /// </summary>
        public IList<MTourOutlay> TourOutlayLst { get; set; }

        /// <summary>
        /// 其他支出明细
        /// </summary>
        public IList<MOtherFeeInOut> OtherOutlayLst { get; set; }

        /// <summary>
        /// 利润分配明细
        /// </summary>
        public IList<MProfitDistribute> ProfitDistributeLst { get; set; }
    }
#endregion

    #region 单团核算_团款收入
    /// <summary>
    /// 单团核算_团款收入
    /// </summary>
    [Serializable]
    public class MTourIncome
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MTourIncome() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 客源单位
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 财务收款
        /// </summary>
        public decimal ReceivedMoney { get; set; }

        /// <summary>
        /// 导游收款
        /// </summary>
        public decimal GuideIncome { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal ConfirmMoney { get; set; }

        /// <summary>
        /// 备注   
        /// </summary>
        public string OrderRemark { get; set; }
    }
#endregion

    #region 单团核算_团款支出
    /// <summary>
    /// 单团核算_团款支出
    /// </summary>
    [Serializable]
    public class MTourOutlay
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MTourOutlay() { }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 支出类别
        /// </summary>
        public string PlanItem { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 财务付款
        /// </summary>
        public decimal PayedAmount { get; set; }

        /// <summary>
        /// 导游现付
        /// </summary>
        public decimal GuidePayed { get; set; }

        /// <summary>
        /// 导游签单
        /// </summary>
        public decimal GuideSign { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
#endregion

    #region 单团核算_利润分配
    /// <summary>
    /// 单团核算_利润分配
    /// </summary>
    [Serializable]
    public class MProfitDistribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MProfitDistribute(){}

        /// <summary>
        /// 利润分配编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 分配类别（操作费、部门提留、业务员提成、其它）
        /// </summary>
        public string DistributeType { get; set; }

        /// <summary>
        /// 分配金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 团队/订单毛利
        /// </summary>
        public decimal Gross { get; set; }

        /// <summary>
        /// 团队/订单净利
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 人员名
        /// </summary>
        public string Staff { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
#endregion  

    #region 应收管理查询/共同实体
    /// <summary>
    /// 应收管理查询/共同实体
    /// </summary>
    [Serializable]
    public class MReceivableBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MReceivableBase() { }
        /// <summary>
        /// 出团启始时间
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? LLDate { get; set; }
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 订单销售员编号
        /// </summary>
        public string SalesmanId { get; set; }
        /// <summary>
        /// 订单销售员
        /// </summary>
        public string Salesman { get; set; }
        /// <summary>
        /// 是否显示在同行分销
        /// </summary>
        public bool? IsShowDistribution { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 是否结清
        /// </summary>
        public bool? IsClean { get; set; }
        /// <summary>
        /// 已收待审核金额
        /// </summary>
        public decimal? UnChecked { get; set; }
        /// <summary>
        /// 未收金额
        /// </summary>
        public decimal? UnReceived { get; set; }
        /// <summary>
        /// 未收款查询条件（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignUnReceived { get; set; }
        /// <summary>
        /// 已收待审查询条件（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignUnChecked { get; set; }
        /// <summary>
        /// 下单人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 合同金额确认状态
        /// </summary>
        public bool? HeTongJinEQueRenStatus { get; set; }
        /// <summary>
        /// 收款人编号
        /// </summary>
        public string ShouKuanRenId { get; set; }
        /// <summary>
        /// 收款人姓名
        /// </summary>
        public string ShluKuanRenName { get; set; }
        /// <summary>
        /// 收款起始时间
        /// </summary>
        public DateTime? ShouKuanSTime { get; set; }
        /// <summary>
        /// 收款截止时间
        /// </summary>
        public DateTime? ShouKuanETime { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string JiDiaoYuanId { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }
    }
#endregion

    #region 应收管理_应收帐款/已结清账款
    /// <summary>
    /// 应收管理_应收帐款/已结清账款
    /// </summary>
    [Serializable]
    public class MReceivableInfo:MReceivableBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MReceivableInfo(){}

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public TourType TourType { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// 辅助订单状态的枚举（用于供应商、分销商的订单状态）
        /// </summary>
        public GroupOrderStatus GroupOrderStatus { get; set; }
        /// <summary>
        /// 留位时间
        /// </summary>
        public DateTime? SaveSeatDate { get; set; }
        /// <summary>
        /// 已退待审
        /// </summary>
        public decimal UnChkRtn { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 其他数
        /// </summary>
        public int Others { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 是否已确认：未确认/已确认
        /// </summary>
        public bool IsConfirmed { get; set; }
        /// <summary>
        /// 应收金额/合同确认金额
        /// </summary>
        public decimal Receivable { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal Received { get; set; }
        /// <summary>
        /// 已退金额
        /// </summary>
        public decimal Returned { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal Bill { get; set; }
        /// <summary>
        /// 计调员
        /// </summary>
        public string Planers { get; set; }
        /// <summary>
        /// 客户部门名称
        /// </summary>
        public string KeHuDeptName { get; set; }
    }
#endregion

    #region 应收管理_应收列表金额汇总
    public class MReceivableSum
    {
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal TotalSumPrice { get; set; }
        /// <summary>
        /// 已收
        /// </summary>
        public decimal TotalReceived { get; set; }
        /// <summary>
        /// 已收待审核
        /// </summary>
        public decimal TotalUnchecked { get; set; }
        /// <summary>
        /// 欠款
        /// </summary>
        public decimal TotalUnReceived { get; set; }
        /// <summary>
        /// 已退
        /// </summary>
        public decimal TotalReturned { get; set; }
        /// <summary>
        /// 已退待审
        /// </summary>
        public decimal TotalUnChkReturn { get; set; }
        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal TotalBill { get; set; }
    }
#endregion

    #region 应收管理_金额确认
    /// <summary>
    /// 应收管理_金额确认
    /// </summary>
    public class MIncomeConfirm
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
        /// 计划对外报价类型
        /// </summary>
        public TourQuoteType OutQuoteType { get; set; }

        /// <summary>
        /// 对外报价整团服务标准
        /// </summary>
        public string ServiceStandard { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }

        /// <summary>
        /// 其它人数
        /// </summary>
        public int Others { get; set; }

        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// 其它人群价格
        /// </summary>
        public decimal OtherPrice { get; set; }

        /// <summary>
        /// 团队合计金额
        /// </summary>
        public decimal TourSumPrice { get; set; }

        /// <summary>
        /// 订单合计金额
        /// </summary>
        public decimal OrderSumPrice { get; set; }

        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        public string ConfirmRemark { get; set; }

        /// <summary>
        /// 结算费用增加
        /// </summary>
        public decimal PeerAddCost { get; set; }

        /// <summary>
        /// 结算费用减少
        /// </summary>
        public decimal PeerReduceCost { get; set; }

        /// <summary>
        /// 增加费用备注
        /// </summary>
        public string AddCostRemark { get; set; }

        /// <summary>
        /// 减少费用备注
        /// </summary>
        public string ReduceCostRemark { get; set; }

        /// <summary>
        /// 团队分项报价列表
        /// </summary>
        public IList<MTourTeamPrice> TourTeamPriceLst { get; set; }
    }
    #endregion

    #region 应收管理_当日收款对账
    /// <summary>
    /// 应收管理_当日收款对账搜索实体
    /// </summary>
    public class MDayReceivablesChkBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SalesmanId { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// 是否在财务管理显示
        /// </summary>
        public bool IsShowInFin { get; set; }
    }
    /// <summary>
    /// 应收管理_当日收款对账
    /// </summary>
    [Serializable]
    public class MDayReceivablesChk : MDayReceivablesChkBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDayReceivablesChk(){}

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 审核状态：未审核/已审核
        /// </summary>
        public string Status { get; set; }
    }
#endregion

    #region 杂费收支管理公用实体
    /// <summary>
    /// 杂费收支管理公用实体
    /// </summary>
    [Serializable]
    public class MOtherFeeInOutBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOtherFeeInOutBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 收/支项目
        /// </summary>
        public string FeeItem { get; set; }

        /// <summary>
        /// 收付款单位编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 收款单位
        /// </summary>
        public string Crm { get; set; }

        /// <summary>
        /// 收/付款人编号
        /// </summary>
        public string DealerId { get; set; }

        /// <summary>
        /// 收/付款人
        /// </summary>
        public string Dealer { get; set; }

        /// <summary>
        /// 查询条件：收/付款时间_开始
        /// </summary>
        public DateTime? DealTimeS { get; set; }

        /// <summary>
        /// 查询条件：收/付款时间_结束
        /// </summary>
        public DateTime? DealTimeE { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public FinStatus Status { get; set; }

    }
#endregion 

    #region 杂费收支管理_杂费收入/杂费支出
    /// <summary>
    /// 杂费收支管理_杂费收入/杂费支出
    /// </summary>
    [Serializable]
    public class MOtherFeeInOut : MOtherFeeInOutBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOtherFeeInOut(){}

        /// <summary>
        /// 其他收支编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 收/付款时间
        /// </summary>
        public DateTime DealTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PayTypeName { get; set; }

        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作者Id
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal Bill { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审核人部门编号
        /// </summary>
        public int AuditDeptId { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public string AuditId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Audit { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditRemark { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// 出纳部门编号
        /// </summary>
        public int AccountantDeptId { get; set; }
        /// <summary>
        /// 出纳编号
        /// </summary>
        public string AccountantId { get; set; }
        /// <summary>
        /// 出纳
        /// </summary>
        public string Accountant { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// 客户单位联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 客户单位联系方式
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 添加状态(1,计调安排时添加 2,导游报账时添加3,销售报账时添加4,计调报账时添加)
        /// </summary>
        public PlanAddStatus IsGuide { get; set; }
    }
#endregion

    #region 应付管理_应收账款共通实体
    /// <summary>
    /// 应付管理_应收账款共通实体
    /// </summary>
    [Serializable]
    public class MPayableBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayableBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 计调类别
        /// </summary>
        public PlanProject? PlanItem { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 单位名称编号/供应商编号（供销商平台用）
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 查询条件：出团时间_开始
        /// </summary>
        public string LDateStart { get; set; }

        /// <summary>
        /// 查询条件：出团时间_结束
        /// </summary>
        public string LDateEnd { get; set; }

        /// <summary>
        /// 查询条件：付款时间_开始
        /// </summary>
        public string PaymentDateS { get; set; }

        /// <summary>
        /// 查询条件：付款时间_结束
        /// </summary>
        public string PaymentDateE { get; set; }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool? IsConfirmed { get; set; }

        /// <summary>
        /// 已付金额比较（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignPaid { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal? Paid { get; set; }

        /// <summary>
        /// 未付金额比较（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignUnpaid { get; set; }

        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal? Unpaid { get; set; }

        /// <summary>
        /// 计划销售员编号
        /// </summary>
        public string SalesmanId { get; set; }

        /// <summary>
        /// 计划销售员
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

        /// <summary>
        /// 是否结清
        /// </summary>
        public bool IsClean { get; set; }

        /// <summary>
        /// 线路区域编号（供应商平台用）
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 是否供应商平台
        /// </summary>
        public bool IsDj { get; set; }
    }
#endregion

    #region 应付管理_应收帐款实体
    /// <summary>
    /// 应付管理_应收帐款实体
    /// </summary>
    [Serializable]
    public class MPayable:MPayableBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayable(){}

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 计划销售员电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 计划销售员手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 计划销售员传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 计划销售员QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal Payable { get; set; }

        /// <summary>
        /// 已登待付
        /// </summary>
        public decimal UnChecked { get; set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        public TourStatus TourStatus { get; set; }

        /// <summary>
        /// 费用明细
        /// </summary>
        public string CostDetail { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public Payment PaymentType { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        public string CostRemarks { get; set; }
    }
#endregion 

    #region 应付管理_应付列表金额汇总
    /// <summary>
    /// 应付管理_应付列表金额汇总
    /// </summary>
    public class MPayableSum
    {
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal TotalPayable { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal TotalPaid { get; set; }
        /// <summary>
        /// 已登待付
        /// </summary>
        public decimal TotalUnchecked { get; set; }
        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal TotalUnpaid { get
        {
            return this.TotalPayable - this.TotalPaid;
        } }
    }
    #endregion

    #region 应付管理_应付账款支出项目
    /// <summary>
    /// 应付管理_应付账款支出项目
    /// </summary>
    [Serializable]
    public class MPayablePlanItem
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayablePlanItem(){}

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set;}

        /// <summary>
        /// 计调项目
        /// </summary>
        public PlanProject PlanItem { get; set; }

        /// <summary>
        /// 是否确认：未确认/已确认
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmedAmount { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal Rest { get{return this.ConfirmedAmount - this.PaidAmount;} }
    }
#endregion 

    #region 应付管理_应付帐款付款
    /// <summary>
    /// 应付管理_应付帐款付款
    /// </summary>
    [Serializable]
    public class MPayablePayment
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayablePayment(){}

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 团队名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// 计调员（张三、李四）
        /// </summary>
        public string Planers { get; set; }

        /// <summary>
        /// 团队状态
        /// </summary>
        public TourStatus TourStatus { get; set; }

        /// <summary>
        /// 团队总成本
        /// </summary>
        public decimal TourCostTotal { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidTotal { get; set; }

        /// <summary>
        /// 支付项目列表
        /// </summary>
        public IList<MPayablePlanItem> PayablePlanItemLst { get; set; }
    }
#endregion

    #region 应付管理_应付帐款登记
    /// <summary>
    /// 应付管理_应付帐款登记
    /// </summary>
    public class MPayRegister
    {
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 计调类型
        /// </summary>
        public PlanProject? PlanTyp { get; set; }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 付款人编号
        /// </summary>
        public string DealerId { get; set; }

        /// <summary>
        /// 付款人
        /// </summary>
        public string Dealer { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

        /// <summary>
        /// 应付金额/结算金额
        /// </summary>
        public decimal Payable { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal Paid { get; set; }

        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal Unpaid { get { return this.Payable - this.Paid; } }

        /// <summary>
        /// 已登记金额（不管审核状态）
        /// </summary>
        public decimal Register { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public Payment PaymentType { get; set; }
    }
#endregion

    #region 应付管理_计调成本
    /// <summary>
    /// 应付管理_计调成本
    /// </summary>
    public class MPlanCostPay
    {
        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal Paid { get; set; }

        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal Unpaid { get; set; }
    }
#endregion

    #region 应付管理_计调成本确认
    /// <summary>
    /// 应付管理_计调成本确认
    /// </summary>
    [Serializable]
    public class MPlanCostConfirm
    {
        /// <summary>
        /// 构成函数
        /// </summary>
        public MPlanCostConfirm(){}

        /// <summary>
        /// 序号
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 支出成本
        /// </summary>
        public decimal Cost { get; set; }
    }
#endregion

    #region 应付管理_批量支付
    /// <summary>
    /// 批量支付实体
    /// </summary>
    public class MBatchPay
    {
        /// <summary>
        /// 登记编号
        /// </summary>
        public int RegisterId { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PaymentType { get; set; }
        /// <summary>
        /// 支付类型名称
        /// </summary>
        public string PaymentTypeName { get; set; }
    }
#endregion

    #region 财务管理_出帐登记
    /// <summary>
    /// 财务管理_出帐登记
    /// </summary>
    [Serializable]
    public class MRegister : MPayRegister
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRegister(){}

        /// <summary>
        /// 出帐登记编号
        /// </summary>
        public int RegisterId { get; set; }

        /// <summary>
        /// 付款人部门编号
        /// </summary>
        public int DealerDeptId { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// 最晚付款时间
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PaymentType { get; set; }

        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PaymentName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproverTime { get; set; }

        /// <summary>
        /// 出纳部门编号
        /// </summary>
        public int AccountantDeptId { get; set; }
        /// <summary>
        /// 出纳编号
        /// </summary>
        public string AccountantId { get; set; }
        /// <summary>
        /// 出纳
        /// </summary>
        public string Accountant { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 状态(销售待确认、财务待审批、账务待支付、账务已支付)
        /// </summary>
        public FinStatus Status { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string ApproveRemark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 是否预付申请登记（非财务登记标识）
        /// </summary>
        public bool IsPrepaid { get; set; }
    }
#endregion

    #region 应付管理_付款审批基础实体
    /// <summary>
    /// 应付管理_付款审批基础实体
    /// </summary>
    [Serializable]
    public class MPayableApproveBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayableApproveBase() { }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        ///计调类型
        /// </summary>
        public PlanProject? PlanTyp { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 供应商单位编号
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 供应商单位
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 请款人编号
        /// </summary>
        public string DealerId { get; set; }

        /// <summary>
        /// 请款人
        /// </summary>
        public string Dealer { get; set; }

        /// <summary>
        /// 查询条件：付款日期_开始
        /// </summary>
        public string PaymentDateS { get; set; }

        /// <summary>
        /// 查询条件：付款日期_结束
        /// </summary>
        public string PaymentDateE { get; set; }

        /// <summary>
        /// 最晚付款日期（开始）
        /// </summary>
        public string DeadlineS { get; set; }

        /// <summary>
        /// 最晚付款日期（结束）
        /// </summary>
        public string DeadlineE { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public FinStatus? Status { get; set; }

        /// <summary>
        /// 是否预付确认
        /// </summary>
        public bool IsPrepaidConfirm { get; set; }
    }
#endregion

    #region 应付管理_付款审批实体
    /// <summary>
    /// 应付管理_付款审批列表
    /// </summary>
    [Serializable]
    public class MPayableApprove:MPayableApproveBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPayableApprove(){}

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 出帐登记编号
        /// </summary>
        public int RegisterId { get; set; } 

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 最迟付款时间
        /// </summary>
        public DateTime? PayExpire { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 是否导游现付的自动登记项
        /// </summary>
        public bool IsDaoYouXianFu { get; set; }
    }
#endregion

    #region 借款管理_基础信息实体
    /// <summary>
    /// 借款管理_基础信息实体
    /// </summary>
    [Serializable]
    public class MDebitBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDebitBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 借款人编号
        /// </summary>
        public string BorrowerId { get; set; }

        /// <summary>
        /// 借款人
        /// </summary>
        public string Borrower { get; set; }

        /// <summary>
        /// 状态[0:待审核 1:待支付 2:已支付]
        /// </summary>
        public FinStatus Status { get; set; }

        /// <summary>
        /// 是否报销完成
        /// </summary>
        public bool IsVerificated{ get; set; }
    }
#endregion

    #region 财务管理_借款管理
    /// <summary>
    /// 财务管理_借款管理
    /// </summary>
    [Serializable]
    public class MDebit:MDebitBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDebit(){}

        /// <summary>
        /// 借款编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set;}

        ///// <summary>
        ///// 计调编号
        ///// </summary>
        //public string PlanId { get; set; }

        /// <summary>
        /// 借款日期
        /// </summary>
        public DateTime BorrowTime { get; set; }

        /// <summary>
        /// 预借金额
        /// </summary>
        public decimal BorrowAmount { get; set; }

        /// <summary>
        /// 借款用途
        /// </summary>
        public string UseFor { get; set; }

        /// <summary>
        /// 审批人编号
        /// </summary>
        public string ApproverId { get; set; }

        /// <summary>
        /// 审批人名
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime ApproveDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Approval { get; set; }

        /// <summary>
        /// 实借金额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 付款人编号
        /// </summary>
        public string LenderId { get; set; }

        /// <summary>
        /// 付款人名
        /// </summary>
        public string Lender { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime LendDate { get; set; }

        /// <summary>
        /// 付款备注
        /// </summary>
        public string LendRemark { get; set; }

        /// <summary>
        /// 预领签单数
        /// </summary>
        public int PreSignNum { get; set; }

        /// <summary>
        /// 实领签单数
        /// </summary>
        public int RelSignNum { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员名
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
#endregion

    #region 欠款预警_基础信息实体
    /// <summary>
    /// 欠款预警_基础信息实体
    /// </summary>
    [Serializable]
    public class MWarningBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MWarningBase(){}

        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 欠款额度
        /// </summary>
        public decimal AmountOwed { get; set; }

        /// <summary>
        /// 拖欠金额（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignArrear { get; set; }

        /// <summary>
        /// 拖欠金额/未收款
        /// </summary>
        public decimal? Arrear { get; set; }

        /// <summary>
        /// 超限金额（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignTransfinite { get; set; }

        /// <summary>
        /// 超限金额
        /// </summary>
        public decimal? Transfinite { get; set; }

        /// <summary>
        /// 超限时间
        /// </summary>
        public DateTime? TransfiniteTime { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
#endregion

    #region 欠款预警_客户预警
    /// <summary>
    /// 欠款预警_客户预警
    /// </summary>
    [Serializable]
    public class MCustomerWarning:MWarningBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCustomerWarning(){}

        /// <summary>
        /// 联系人列表
        /// </summary>
        public IList<MCrmLinkman> Contact { get; set; }

        /// <summary>
        /// 单团账龄期限
        /// </summary>
        public int Deadline { get; set; }
        /// <summary>
        /// 最长超时天数
        /// </summary>
        public int DeadDay { get; set; }
        /// <summary>
        /// 超时团数
        /// </summary>
        public int TourCount { get; set; }
    }
#endregion

    #region 欠款预警_销售预警
    /// <summary>
    /// 欠款预警_销售预警
    /// </summary>
    [Serializable]
    public class MSalesmanWarning:MWarningBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSalesmanWarning(){}
        /// <summary>
        /// 已确认垫款/确定合同金额
        /// </summary>
        public decimal ConfirmAdvances { get; set; }
        /// <summary>
        /// 预收
        /// </summary>
        public decimal PreIncome { get; set; }
        /// <summary>
        /// 支出
        /// </summary>
        public decimal SumPay { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adult { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Child { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
    }
#endregion

    #region 超限审批_基础信息实体
    /// <summary>
    /// 超限审批_基础信息实体
    /// </summary>
    [Serializable]
    public class MTransfiniteBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTransfiniteBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 申请人编号
        /// </summary>
        public string ApplierId { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string Applier { get; set; }

        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Crm { get; set; }

        /// <summary>
        /// 申请时间-开始
        /// </summary>
        public string ApplyTimeS { get; set; }

        /// <summary>
        /// 申请时间-结束
        /// </summary>
        public string ApplyTimeE { get; set; }
    }
#endregion

    #region 超限审批
    /// <summary>
    /// 超限审批
    /// </summary>
    [Serializable]
    public class MTransfinite:MTransfiniteBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTransfinite(){}

        /// <summary>
        /// 垫付编号
        /// </summary>
        public string DisburseId { get; set; }

        /// <summary>
        /// 报价编号/团队编号/订单编号
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 0：报价成功/1：成团/2：报名
        /// </summary>
        public TransfiniteType ItemType { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 垫款金额
        /// </summary>
        public decimal DisburseAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 超限审批状态（0：未审批 1：通过 2：未通过）
        /// </summary>
        public TransfiniteStatus IsApprove { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }

        /// <summary>
        /// 审核人名
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApproveRemark { get; set; }

        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 超限审核时销售员或客户单位超限列表
        /// </summary>
        public IList<MApplyOverrun> AdvanceAppList { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public TourType TourType { get; set; }
    }

    /// <summary>
    /// 超限审批查询信息业务实体
    /// </summary>
    public class MChaXianShenPiChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MChaXianShenPiChaXunInfo() { }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CrmName { get; set; }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public string ShenQingRenId { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ShenQingRenName { get; set; }
        /// <summary>
        /// 申请起始时间
        /// </summary>
        public DateTime? SShenQingTime { get; set; }
        /// <summary>
        /// 申请截止时间
        /// </summary>
        public DateTime? EShenQingTime { get; set; }
        /// <summary>
        /// 申请状态
        /// </summary>
        public TransfiniteStatus? Status { get; set; }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string ShenPiRenId { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ShenPiRenName { get; set; }
        /// <summary>
        /// 审批开始时间
        /// </summary>
        public DateTime? ShenPiSTime { get; set; }
        /// <summary>
        /// 审批截止时间
        /// </summary>
        public DateTime? ShenPiETime { get; set; }
    }
#endregion

    #region 超限审核时销售员或客户单位超限实体
    /// <summary>
    /// 超限审核时销售员或客户单位超限实体
    /// </summary>
    [Serializable]
    public class MApplyOverrun 
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MApplyOverrun() { }

        /// <summary>
        /// 垫付编号
        /// </summary>
        public string DisburseId { get; set; }

        /// <summary>
        ///客户单位或销售员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户单位或销售员编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 客户单位或销售员欠款额度
        /// </summary>
        public decimal AmountOwed { get; set; }

        /// <summary>
        /// 客户单位或销售员超限金额
        /// </summary>
        public decimal Transfinite { get; set; }

        /// <summary>
        /// 客户单位或销售员拖欠金额
        /// </summary>
        public decimal Arrear { get; set; }

        /// <summary>
        /// 客户单位或销售员超限时间
        /// </summary>
        public DateTime? TransfiniteTime { get; set; }

        /// <summary>
        /// 销售员已确认垫款
        /// </summary>
        public decimal ConfirmAdvances { get; set; }

        /// <summary>
        /// 销售员团队预收
        /// </summary>
        public decimal PreIncome { get; set; }

        /// <summary>
        /// 销售员支出
        /// </summary>
        public decimal SumPay { get; set; }

        /// <summary>
        /// 客户单位单团账龄期限
        /// </summary>
        public int Deadline { get; set; }

        /// <summary>
        /// 客户单位最长超时天数
        /// </summary>
        public int DeadDay { get; set; }

        /// <summary>
        /// 超限类型
        /// </summary>
        public OverrunType OverrunType { get; set; }
    }
    #endregion

    #region 批量销帐_订单销帐
    /// <summary>
    /// 绑定订单批量销帐
    /// </summary>
    [Serializable]
    public class MBatchWriteOffOrder
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNum { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNum { get; set; }

        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal Received { get; set; }

        /// <summary>
        /// 未收金额
        /// </summary>
        public decimal Unreceivable { get; set; }
    }
    #endregion

    #region 往来对账_今日收款/付款/应收/应付
    /// <summary>
    /// 往来对账_今日收款/付款/应收/应付-基础
    /// </summary>
    public class MAuditBase
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 客户单位/供应商单位编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 客户单位/供应商单位
        /// </summary>
        public string Crm { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

        /// <summary>
        /// 计调项
        /// </summary>
        public PlanProject? PlanItem { get; set; }

        /// <summary>
        /// 财务金额（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign? SignAmount { get; set; }

        /// <summary>
        /// 财务金额（收款、付款、应收、应付）
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 财务时间（审核、支付、核算）-开始
        /// </summary>
        public string IssueTimeS { get; set; }

        /// <summary>
        /// 财务时间（审核、支付、核算）-结算
        /// </summary>
        public string IssueTimeE { get; set; }
    }
    /// <summary>
    /// 往来对账_今日收款/付款/应收/应付
    /// </summary>
    [Serializable]
    public class MReconciliation : MAuditBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MReconciliation(){}

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 财务人部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 财务人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 财务人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 财务时间（审核、支付、核算）
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
#endregion

    #region 固定资产_基础信息实体
    /// <summary>
    /// 固定资产_基础信息实体
    /// </summary>
    [Serializable]
    public class MAssetBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MAssetBase(){}

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// 使用部门编号
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 使用部门名称
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 管理责任者编号
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// 管理责任者
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// 查询条件：购买开始时间
        /// </summary>
        public string BuyTimeStart { get; set; }

        /// <summary>
        /// 查询条件：购买结束时间
        /// </summary>
        public string BuyTimeEnd { get; set; }
    }
#endregion

    #region 固定资产
    /// <summary>
    /// 固定资产
    /// </summary>
    [Serializable]
    public class MAsset:MAssetBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MAsset(){}

        /// <summary>
        /// 资产编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }

        /// <summary>
        /// 原始价值
        /// </summary>
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// 折旧年限
        /// </summary>
        public decimal DepreciableLife { get; set; }

        /// <summary>
        /// 管理责任者部门编号
        /// </summary>
        public int AdminDeptId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
#endregion

    #region 金蝶凭证基础
    /// <summary>
    /// 金蝶凭证基础
    /// </summary>
    [Serializable]
    public class MKingDeeProofBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKingDeeProofBase(){}

        /// <summary>
        /// 团号
        /// </summary>
        public string FTourCode { get; set; }

        /// <summary>
        /// 查询条件：凭证日期_开始
        /// </summary>
        public string FDateS { get; set; }

        /// <summary>
        /// 查询条件：凭证日期_结束
        /// </summary>
        public string FDateE { get; set; }

        /// <summary>
        /// 凭证日期
        /// </summary>
        public DateTime FDate { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public bool FIsExport { get; set; }

        /// <summary>
        /// 会计年度
        /// </summary>
        public decimal FYear { get; set; }

        /// <summary>
        /// 会计期间
        /// </summary>
        public decimal FPeriod { get; set; }

        /// <summary>
        /// 凭证字
        /// </summary>
        public string FGroupId { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public decimal FNumber { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime FTransDate { get; set; }

        /// <summary>
        /// 制单
        /// </summary>
        public string FPreparerId { get; set; }
    }
#endregion

    #region 金蝶凭证
    /// <summary>
    /// 金蝶凭证
    /// </summary>
    [Serializable]
    public class MKingDeeProof : MKingDeeProofBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKingDeeProof() { }

        /// <summary>
        /// 主键编号
        /// </summary>
        public string FId { get; set; }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string FCompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string FTourId { get; set; }

        /// <summary>
        /// 凭证类型
        /// </summary>
        public DefaultProofType FItemType { get; set; }

        /// <summary>
        /// 关联编号（订单收款编号、计调付款编号、导游借款编号、杂费收入编号、杂费支出编号）
        /// </summary>
        public string FItemId { get; set; }

        /// <summary>
        /// 审核
        /// </summary>
        public string FCheckerId { get; set; }

        /// <summary>
        /// 核准
        /// </summary>
        public string FApproveId { get; set; }

        /// <summary>
        /// 出纳
        /// </summary>
        public string FCashierId { get; set; }

        /// <summary>
        /// 经办
        /// </summary>
        public string FHandler { get; set; }

        /// <summary>
        /// 结算方式
        /// </summary>
        public string FSettleTypeId { get; set; }

        /// <summary>
        /// 结算号
        /// </summary>
        public string FSettleNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal FQuantity { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string FMeasureUnitId { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal FUnitPrice { get; set; }

        /// <summary>
        /// 参考信息
        /// </summary>
        public string FReference { get; set; }

        /// <summary>
        /// 往来业务编号
        /// </summary>
        public string FTransNo { get; set; }

        /// <summary>
        /// 附件数
        /// </summary>
        public decimal FAttachments { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public decimal FSerialNum { get; set; }

        /// <summary>
        /// 系统模块
        /// </summary>
        public string FObjectName { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        public string FParameter { get; set; }

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal FExchangeRate { get; set; }

        /// <summary>
        /// 过账
        /// </summary>
        public decimal FPosted { get; set; }

        /// <summary>
        /// 机制凭证
        /// </summary>
        public string FInternalInd { get; set; }

        /// <summary>
        /// 现金流量
        /// </summary>
        public string FCashFlow { get; set; }

        /// <summary>
        /// 金蝶凭证详细列表
        /// </summary>
        public IList<MKingDeeProofDetail> KingDeeProofDetailLst { get; set; }

        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
    }
#endregion

    #region 金蝶凭证详细
    /// <summary>
    /// 金蝶凭证详细
    /// </summary>
    [Serializable]
    public class MKingDeeProofDetail : MKingDeeProofBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKingDeeProofDetail(){}

        /// <summary>
        /// 详细编号
        /// </summary>
        public int FDetailId { get; set; }

        /// <summary>
        /// 财务编号
        /// </summary>
        public string FId { get; set; }

        /// <summary>
        /// 凭证摘要
        /// </summary>
        public string FExplanation { get; set; }

        /// <summary>
        /// 科目代码
        /// </summary>
        public string FAccountNum { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string FAccountName { get; set; }

        /// <summary>
        /// 核算项目（用逗号隔开的拼接字符串：科目类型-核算项目代码-核算项目名称）
        /// </summary>
        public string FItem { get; set; }

        /// <summary>
        /// 核算项目列表
        /// </summary>
        public IList<MKingDeeChk> FItemList { get; set; }

        /// <summary>
        /// 币别代码
        /// </summary>
        public string FCurrencyNum { get; set; }

        /// <summary>
        /// 币别名称
        /// </summary>
        public string FCurrencyName { get; set; }

        /// <summary>
        /// 原币金额
        /// </summary>
        public decimal FAmountFor { get; set; }

        /// <summary>
        /// 借方
        /// </summary>
        public decimal FDebit { get; set; }

        /// <summary>
        /// 贷方
        /// </summary>
        public decimal FCredit { get; set; }

        /// <summary>
        /// 分录序号
        /// </summary>
        public decimal FEntryId { get; set; }
    }
#endregion

    #region 金蝶科目
    /// <summary>
    /// 金蝶科目
    /// </summary>
    [Serializable]
    public class MKingDeeSubject
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKingDeeSubject(){}

        /// <summary>
        /// 科目编号
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 父级科目编号
        /// </summary>
        public int PreSubjectId { get; set; }

        /// <summary>
        /// 父级科目名称（科目类别）
        /// </summary>
        public string PreSubjectNm { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 科目代码
        /// </summary>
        public string SubjectCd { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectNm { get; set; }

        /// <summary>
        /// 科目类型
        /// </summary>
        public FinanceAccountItem SubjectTyp { get; set; }
        
        /// <summary>
        /// 根据科目类型对应关联编号（客户编号、部门编号、职员编号、计划编号、支付方式编号、供应商编号）
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string MnemonicCd { get; set; }

        /// <summary>
        /// 核算项目编号（多个编号用逗号隔开）
        /// </summary>
        public string ChkId { get; set; }

        /// <summary>
        /// 核算项目名称（核算项目类别 - 核算项目代码 - 核算项目名称），多个用逗号隔开
        /// </summary>
        public string ChkItem { get; set; }

        /// <summary>
        /// 帐号编号（当科目类型为支付方式时，反写公司帐号表）
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 核算项目列表
        /// </summary>
        public IList<MKingDeeChk> ItemList { get; set; }
    }
#endregion

    #region 金蝶核算项目
    /// <summary>
    /// 金蝶核算项目
    /// </summary>
    [Serializable]
    public class MKingDeeChk
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKingDeeChk(){}

        /// <summary>
        /// 核算项目编号
        /// </summary>
        public int ChkId { get; set; }

        /// <summary>
        /// 上级核算项目编号
        /// </summary>
        public int PreChkId { get; set; }

        /// <summary>
        /// 上级核算项目名称（科目类型）
        /// </summary>
        public string PreChkNm { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 核算项目代码
        /// </summary>
        public string ChkCd { get; set; }

        /// <summary>
        /// 核算项目名称
        /// </summary>
        public string ChkNm { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string MnemonicCd { get; set; }

        /// <summary>
        /// 核算项目类别
        /// </summary>
        public FinanceAccountItem ChkCate { get; set; }

        /// <summary>
        /// 根据科目类型对应关联编号（客户编号、部门编号、职员编号、计划编号、支付方式编号、供应商编号）
        /// </summary>
        public string ItemId { get; set; }
    }
#endregion
     
    #region 发票管理
    /// <summary>
    /// 发票管理_基础实体
    /// </summary>
    public class MBillBase
    {
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 开票单位编号
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 开票单位
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 开票金额/未开票金额
        /// </summary>
        public decimal? BillAmount { get; set; }
        /// <summary>
        /// 开票时间-开始
        /// </summary>
        public string BillTimeS { get; set; }
        /// <summary>
        /// 开票时间-结束
        /// </summary>
        public string BillTimeE { get; set; }
    }

    /// <summary>
    /// 发票管理_实体
    /// </summary>
    public class MBill:MBillBase
    {
        /// <summary>
        /// 发票编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户单位联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 客户单位联系方式
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 开票人编号
        /// </summary>
        public string DealerId { get; set; }
        /// <summary>
        /// 开票人
        /// </summary>
        public string Dealer { get; set; }
        /// <summary>
        /// 开票时间
        /// </summary>
        public DateTime BillTime { get; set; }
        /// <summary>
        /// 票据号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsApprove { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string ApproveRemark { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }
        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
    #endregion

    #region 超限审批实体
    /// <summary>
    /// 超限审批实体
    /// </summary>
    public class MChaoXianShenPiInfo
    {
        /// <summary>
        /// 超限申请编号
        /// </summary>
        public string ChaoXianShenQingId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string YiJian { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public TransfiniteStatus Status { get; set; }
        /// <summary>
        /// 订单编号（OUTPUT）
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单金额（OUTPUT）
        /// </summary>
        public decimal OrderJinE { get; set; }
        /// <summary>
        /// 客户单位编号（OUTPUT）
        /// </summary>
        public string CrmId { get; set; }
    }
    #endregion
}
