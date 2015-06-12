//计划与订单相关枚举 2011-09-01 PM 曹胡生 创建
namespace EyouSoft.Model.EnumType.TourStructure
{
    #region 散拼计划收客状态
    /// <summary>
    /// 散拼计划收客状态
    /// </summary>
    public enum TourShouKeStatus
    {
        /// <summary>
        /// 报名中 = 0
        /// </summary>
        报名中 = 0,
        /// <summary>
        /// 自动客满 = 1
        /// </summary>
        自动客满 = 1,
        /// <summary>
        /// 自动停收 = 2
        /// </summary>
        自动停收 = 2,
        /// <summary>
        /// 手动客满=3
        /// </summary>
        手动客满 = 3,
        /// <summary>
        /// 手动停收=4
        /// </summary>
        手动停收 = 4
    }
    #endregion

    #region 计划状态
    /// <summary>
    /// 计划状态
    /// </summary>
    public enum TourStatus
    {
        /// <summary>
        /// 销售未派计划(单项业务:操作中) = 0,
        /// </summary>
        销售未派计划 = 0,
        /// <summary>
        /// 计调未接收 = 1
        /// </summary>
        计调未接收 = 1,
        /// <summary>
        /// 计调配置 = 2
        /// </summary>
        计调配置 = 2,
        /// <summary>
        /// 计调配置完毕(单项业务:已落实) = 3
        /// </summary>
        计调配置完毕 = 3,
        /// <summary>
        /// 导游带团 = 4
        /// </summary>
        导游带团 = 4,
        /// <summary>
        /// 导游报帐=5
        /// </summary>
        导游报帐 = 5,
        /// <summary>
        /// 销售未结算=6
        /// </summary>
        销售未结算 = 6,
        /// <summary>
        /// 销售待审 = 7
        /// </summary>
        销售待审 = 7,
        /// <summary>
        /// 计调待审 = 8
        /// </summary>
        计调待审 = 8,
        /// <summary>
        /// 待终审 = 9
        /// </summary>
        待终审 = 9,
        /// <summary>
        /// 财务核算 = 10
        /// </summary>
        财务核算 = 10,
        /// <summary>
        /// 封团 = 11
        /// </summary>
        封团 = 11,
        /// <summary>
        /// 已取消=12
        /// </summary>
        已取消 = 12,
        /// <summary>
        /// 垫付申请=13
        /// </summary>
        垫付申请 = 13,
        /// <summary>
        /// 审核失败=14
        /// </summary>
        审核失败 = 14,
        /// <summary>
        /// 资金超限
        /// </summary>
        资金超限 = 15
    }
    #endregion

    //#region 报账状态
    ///// <summary>
    ///// 报账状态
    ///// </summary>
    //public enum ReimbursementStaus
    //{
    //    /// <summary>
    //    /// 未报账=0
    //    /// </summary>
    //    未报账 = 0,
    //    /// <summary>
    //    /// 导游报帐提交销售
    //    /// </summary>
    //    导游报帐提交销售 = 1,
    //    /// <summary>
    //    /// 销售报账提交计调
    //    /// </summary>
    //    销售报账提交计调 = 2,
    //    /// <summary>
    //    /// 计调报账提交财务
    //    /// </summary>
    //    计调报账提交财务 = 3,
    //    /// <summary>
    //    /// 导游报账提交计调预审
    //    /// </summary>
    //    计调预审 = 4,
    //    /// <summary>
    //    /// 计调终审=7
    //    /// </summary>
    //    计调终审=7,
    //    /// <summary>
    //    /// 计调审核提交财务暂收
    //    /// </summary>
    //    财务暂收 = 5,
    //    /// <summary>
    //    /// 财务终审
    //    /// </summary>
    //    财务终审 = 6,
    //}
    //#endregion

    #region 计划类型
    /// <summary>
    /// 计划类型
    /// </summary>
    public enum TourType
    {
        /// <summary>
        /// 组团团队
        /// </summary>
        组团团队 = 0,
        /// <summary>
        /// 组团散拼
        /// </summary>
        组团散拼 = 1,
        /// <summary>
        /// 地接团队
        /// </summary>
        地接团队 = 2,
        /// <summary>
        /// 地接散拼
        /// </summary>
        地接散拼 = 3,
        /// <summary>
        /// 出境团队
        /// </summary>
        出境团队 = 4,
        /// <summary>
        /// 出境散拼
        /// </summary>
        出境散拼 = 5,
        /// <summary>
        /// 单项服务
        /// </summary>
        单项服务 = 6,
        /// <summary>
        /// 组团散拼短线
        /// </summary>
        组团散拼短线 = 7
    }
    #endregion

    #region 计划对外报价类型
    /// <summary>
    /// 计划对外报价类型
    /// </summary>
    public enum TourQuoteType
    {
        /// <summary>
        /// 整团
        /// </summary>
        整团 = 0,
        /// <summary>
        /// 分项
        /// </summary>
        分项 = 1
    }
    #endregion

    #region 订单来源类型
    /// <summary>
    /// 订单来源类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 分销商下单=0
        /// </summary>
        分销商下单 = 0,
        /// <summary>
        /// 代客预定
        /// </summary>
        代客预定 = 1,
        /// <summary>
        /// 团队计划
        /// </summary>
        团队计划 = 2,
        /// <summary>
        /// 单项服务
        /// </summary>
        单项服务 = 3,
        /// <summary>
        /// 无计划散客=4
        /// </summary>
        无计划散客 = 4
    }
    #endregion

    #region 订单的收款\退款状态
    /// <summary>
    /// 订单的收款\退款状态
    /// </summary>
    public enum CollectionRefundState
    {
        /// <summary>
        /// 收款 = 0
        /// </summary>
        收款 = 0,
        /// <summary>
        /// 退款 = 1
        /// </summary>
        退款 = 1
    }
    #endregion

    #region 订单状态枚举
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        未处理 = 0,
        /// <summary>
        /// 已留位
        /// </summary>
        已留位 = 1,
        /// <summary>
        /// 留位过期
        /// </summary>
        留位过期 = 2,
        /// <summary>
        /// 不受理
        /// </summary>
        不受理 = 3,
        /// <summary>
        /// 已成交
        /// </summary>
        已成交 = 4,
        /// <summary>
        /// 已取消=5
        /// </summary>
        已取消 = 5,
        /// <summary>
        /// 垫付申请审核
        /// </summary>
        垫付申请审核 = 6,
        /// <summary>
        /// 垫付申请审核成功
        /// </summary>
        垫付申请审核成功 = 7,
        /// <summary>
        /// 垫付申请审核失败
        /// </summary>
        垫付申请审核失败 = 8,
        /// <summary>
        /// 资金超限
        /// </summary>
        资金超限 = 9


    }
    #endregion

    #region  辅助订单状态的枚举（用于供应商、分销商的订单状态）
    /// <summary>
    /// 辅助订单状态的枚举（用于供应商、分销商的订单状态）
    /// </summary>
    public enum GroupOrderStatus
    {
        预留未确认 = 0,
        预留过期 = 1,
        报名未确认 = 2,
        已留位 = 3,
        已确认 = 4,
        预留不受理 = 5,
        报名不受理 = 6,
        已取消 = 7
    }
    #endregion

    #region 订单游客输入方式
    /// <summary>
    /// 订单游客显示类型
    /// </summary>
    public enum CustomerDisplayType
    {
        /// <summary>
        /// 附件方式 = 0
        /// </summary>
        附件方式 = 0,
        /// <summary>
        /// 输入方式 = 1
        /// </summary>
        输入方式 = 1,
    }

    #endregion

    #region 游客类型

    /// <summary>
    /// 游客类型
    /// </summary>
    public enum VisitorType
    {
        /// <summary>
        /// 成人 = 0
        /// </summary>
        成人 = 0,
        /// <summary>
        /// 儿童 = 1
        /// </summary>
        儿童 = 1,
        /// <summary>
        /// 其他人群=2
        /// </summary>
        其他人群 = 2
    }
    #endregion

    #region 游客状态
    /// <summary>
    /// 游客状态
    /// </summary>
    public enum TravellerStatus
    {
        /// <summary>
        /// 在团 = 0
        /// </summary>
        在团 = 0,
        /// <summary>
        /// 退团 = 1
        /// </summary>
        退团 = 1
    }
    #endregion

    #region 游客证件类型枚举
    /// <summary>
    /// 游客证件类型枚举
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 身份证
        /// </summary>
        身份证 = 1,
        /// <summary>
        /// 军官证
        /// </summary>
        军官证 = 2,
        /// <summary>
        /// 台胞证
        /// </summary>
        台胞证 = 3,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        港澳通行证 = 4,
        /// <summary>
        /// 户口本
        /// </summary>
        户口本 = 5,
        /// <summary>
        /// 大陆居民
        /// </summary>
        大陆居民 = 6,
        /// <summary>
        /// 往来港澳通行证
        /// </summary>
        往来港澳通行证 = 7,
        /// <summary>
        /// 往来台湾通行证
        /// </summary>
        往来台湾通行证 = 8,
        /// <summary>
        /// 因私护照
        /// </summary>
        因私护照 = 9
    }
    #endregion

    #region 游客签证状态
    /// <summary>
    /// 游客签证状态
    /// </summary>
    public enum VisaStatus
    {
        /// <summary>
        /// 未知=0
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 材料收集中
        /// </summary>
        材料收集中 = 1,
        /// <summary>
        /// 材料已交待审
        /// </summary>
        材料已交待审 = 2,
        /// <summary>
        /// 签证成功
        /// </summary>
        签证成功 = 3,
        /// <summary>
        /// 拒签
        /// </summary>
        拒签 = 4
    }
    #endregion

    #region 团队计划报价枚举
    /// <summary>
    /// 团队计划报价状态
    /// </summary>
    public enum QuoteState
    {
        /// <summary>
        /// 未处理 = 0
        /// </summary>
        未处理 = 0,
        /// <summary>
        /// 报价成功 = 1
        /// </summary>
        报价成功 = 1,
        /// <summary>
        /// 取消报价 = 2
        /// </summary>
        取消报价 = 2,
        /// <summary>
        /// 垫付申请审核=3
        /// </summary>
        垫付申请审核 = 3,
        /// <summary>
        /// 审核失败=4
        /// </summary>
        审核失败 = 4,
        /// <summary>
        /// 审核成功=5
        /// </summary>
        审核成功 = 5
    }
    #endregion

    #region 模块类型
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 组团=0
        /// </summary>
        组团 = 0,
        /// <summary>
        /// 地接=1
        /// </summary>
        地接 = 1,
        /// <summary>
        /// 出境=2
        /// </summary>
        出境 = 2,
        /// <summary>
        /// 同业分销=3
        /// </summary>
        同业分销 = 3
    }
    #endregion

    #region 判断超限时，人员类型
    /// <summary>
    /// 判断超限时，人员类型
    /// </summary>
    public enum PType
    {
        /// <summary>
        /// 销售员=0
        /// </summary>
        销售员 = 1,
        /// <summary>
        /// 客户单位=1
        /// </summary>
        客户单位 = 2
    }
    #endregion

    #region 订单修改、变更操作时的状态
    /// <summary>
    /// 订单修改、变更操作时的状态
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// 修改 = 0
        /// </summary>
        修改 = 0,
        /// <summary>
        /// 变更 = 1
        /// </summary>
        变更 = 1
    }
    #endregion

    #region 同业分销查询订单类型（用于查询）
    /// <summary>
    /// 同业分销查询订单类型
    /// </summary>
    public enum OrderTypeBySearch
    {
        /// <summary>
        /// 全部订单
        /// </summary>
        全部订单 = 0,
        /// <summary>
        /// 我销售的订单
        /// </summary>
        我销售的订单 = 1,
        /// <summary>
        /// 我操作的订单
        /// </summary>
        我操作的订单 = 2

    }
    #endregion

    #region 供应商计划在分销商显示的发布人
    /// <summary>
    /// 供应商计划在分销商显示的发布人
    /// </summary>
    public enum ShowPublisher
    {
        /// <summary>
        /// 审核人=0
        /// </summary>
        审核人 = 0,
        /// <summary>
        /// 供应商=1
        /// </summary>
        供应商 = 1
    }
    #endregion

    #region 报账列表
    /// <summary>
    /// 报账列表
    /// </summary>
    public enum BZList
    {
        /// <summary>
        /// 导游报账=0
        /// </summary>
        导游报账 = 0,
        /// <summary>
        /// 销售报账=1
        /// </summary>
        销售报账 = 1,
        /// <summary>
        /// 计调报账=2
        /// </summary>
        计调报账 = 2,
        /// <summary>
        /// 计调终审=3
        /// </summary>
        计调终审 = 3,
        /// <summary>
        /// 报销=4
        /// </summary>
        报销 = 4,
        /// <summary>
        /// 单团核算=5
        /// </summary>
        单团核算 = 5,
        /// <summary>
        /// 报账
        /// </summary>
        报账 = 6
    }
    #endregion


    #region 车型变更的类型
    /// <summary>
    /// 车型变更的类型
    /// </summary>
    public enum CarChangeType
    {
        /// <summary>
        /// 车型变更
        /// </summary>
        上车地点变更 = 0,

        /// <summary>
        /// 座次变更
        /// </summary>
        车型座次变更 = 1
    }
    #endregion

}
