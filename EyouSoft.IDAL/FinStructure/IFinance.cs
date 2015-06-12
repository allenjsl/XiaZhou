using System.Collections.Generic;

namespace EyouSoft.IDAL.FinStructure
{
    using System;

    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.PlanStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;

    using MReconciliation = EyouSoft.Model.FinStructure.MReconciliation;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// </summary>
    public interface IFinance
    {
        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddProfitDistribute(MProfitDistribute mdl);

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdProfitDistribute(MProfitDistribute mdl);

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        bool DelProfitDis(int id, string companyid);

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        MProfitDistribute GetProfitDistribute(int id, string companId);

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MProfitDistribute> GetProfitDistribute(string tourId);

            #endregion

        #region 应收管理

        /// <summary>
        /// 根据应收搜索实体获取应收帐款/已结清账款列表和金额汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应收搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应收帐款/已结清账款列表</returns>
        IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , MReceivableBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);


        /// <summary>
        /// 根据系统公司编号获取当日收款对账列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="tourTypes">团队类型集合</param>
        /// <param name="isShowDistribution">是否同业分销</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="search">搜索实体</param>
        /// <returns>当日收款对账列表</returns>
        IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , string companyId
                                                        , IList<TourType> tourTypes
                                                        , bool isShowDistribution
                                                        , MDayReceivablesChkBase search
                                                        , string operatorId
                                                        , params int[] deptIds);


        ///// <summary>
        ///// 根据订单编号获取金额确认实体
        ///// </summary>
        ///// <param name="type">0：团队 1：散客</param>
        ///// <param name="orderId">订单编号</param>
        ///// <returns>金额确认实体</returns>
        //MIncomeConfirm GetIncomeConfirmByOrderId(int type,string orderId);

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="mdl">订单销售收款/退款的实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetTourOrderSalesCheck(MTourOrderSales mdl);

        /// <summary>
        /// 根据团队编号获取导游实收列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MTourOrderSales> GetTourOrderSalesLstByTourId(string tourId);

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds);

        #endregion

        #region 杂费收支

        /// <summary>
        /// 添加其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl);

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int DelOtherFeeInOut(string companyId, ItemType typ,params int[] ids);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id,string companyId);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量审核
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="auditDeptId">审核人部门编号</param>
        /// <param name="auditId">审核人编号</param>
        /// <param name="audit">审核人</param>
        /// <param name="auditRemark">审核意见</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="status">状态</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int SetOtherFeeInOutAudit(ItemType typ,int auditDeptId, string auditId, string audit, string auditRemark, DateTime auditTime,FinStatus status, params int[] ids);

        /// <summary>
        /// 根据团队编号获取导游报账时添加的其他收入列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MOtherFeeInOut> GetOtherFeeInLst(string tourId);

        /// <summary>
        /// 根据其他（杂费）支出费用编号集合可以批量支付
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="status">状态</param>
        /// <param name="lst">其他（杂费）支出费用编号和支付方式集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int SetOtherFeeOutPay(int accountantDeptId, string accountantId, string accountant, DateTime payTime, FinStatus status, IList<MBatchPay> lst);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用搜索实体获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mSearch">其他（杂费）收入/支出费用搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , ItemType typ
                                                , MOtherFeeInOutBase mSearch
                                                , string operatorId
                                                , params int[] deptIds);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids);


        #endregion

        #region 应付管理

        /// <summary>
        /// 根据应付帐款搜索实体获取应付帐款/已结清账款列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应付帐款/已结清账款列表</returns>
        IList<MPayable> GetPayableLst(int pageSize
                                    , int pageIndex
                                    , ref int recordCount
                                    , ref string xmlSum
                                    , MPayableBase mSearch
                                    , string operatorId
                                    , params int[] deptIds);

        /// <summary>
        /// 根据搜索实体获取当天付款对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>当天付款对账列表</returns>
        IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayRegister mSearch
                                        , string operatorId
                                        , params int[] deptIds);

        /// <summary>
        /// 根据计调编号获取某一个支出项目登记信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>某一个支出项目登记信息</returns>
        MPayRegister GetPayRegisterBaseByPlanId(string planId);

        /// <summary>
        /// 根据计调编号获取某一个计调项目的出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        IList<MRegister> GetPayRegisterLstByPlanId(string planId,bool? isPrepaid);

        ///// <summary>
        ///// 根据团队编号获取计调登记列表实体
        ///// </summary>
        ///// <param name="tourId">团队编号</param>
        ///// <returns>应付账款登记实体</returns>
        //MPayablePayment GetPayablePaymentByTourId(string tourId);

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        int AddRegister(MRegister mdl);

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        int UpdRegister(MRegister mdl);

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        bool DelRegister(string companyId, int registerId);

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表和汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>付款审批列表</returns>
        IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , MPayableApproveBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);

        /// <summary>
        /// 根据登记编号获取登记实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>登记实体</returns>
        MRegister GetRegisterById(string companyId, int registerId);


        /// <summary>
        /// 根据登记编号集合设置登记审核状态
        /// </summary>
        /// <param name="approverId">审核人编号</param>
        /// <param name="approver">审核人</param>
        /// <param name="approveTime">审核时间</param>
        /// <param name="approveRemark">审核意见</param>
        /// <param name="status">审核</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerIds">登记编号集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        int SetRegisterApprove(string approverId
                                , string approver
                                , DateTime approveTime
                                , string approveRemark
                                , FinStatus status
                                , string companyId
                                , params int[] registerIds);

        /// <summary>
        /// 根据登记编号集合设置支付状态
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="lst">批量支付列表</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        int SetRegisterPay(
            int accountantDeptId,
            string accountantId,
            string accountant,
            DateTime payTime,
            string companyId,
            IList<MBatchPay> lst);

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ);

        /// <summary>
        /// 根据团队编号、计调类型、是否确认获取计调成本确认列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <param name="isConfirmed">是否确认</param>
        /// <returns>计调成本确认列表</returns>
        IList<MPlanCostConfirm> GetPlanCostConfirmLst(string tourId, PlanProject typ,bool isConfirmed);

        /// <summary>
        /// 根据计调编号设置计调成本确认
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="costId">成本确认人ID</param>
        /// <param name="costName">成本确认人</param>
        /// <param name="costRemark">成本确认备注</param>
        /// <param name="confirmation">确认金额/结算金额</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetPlanCostConfirmed(
            string planId, string costId, string costName, string costRemark, decimal confirmation);

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdDebit(MDebit mdl);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetDebitApprove(MDebit mdl);

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        MDebit GetDebit(int id);

        /// <summary>
        /// 根据借款搜索实体获取借款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">借款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLst(int pageSize
                                , int pageIndex
                                , ref int recordCount
                                , MDebitBase mSearch
                                , string operatorId
                                , params int[] deptIds);
        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLstByTourId(string tourId,bool isBz);

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLstByPlanId(string planId);

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Pay(MDebit mdl);

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool DeleteDebit(string companyId, int id);

        #endregion

        #region 报销报账

        #endregion

        #region 欠款预警

        /// <summary>
        /// 客户预警
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>客户预警列表</returns>
        IList<MCustomerWarning> GetCustomerWarningLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , string companyId
                                                    , MWarningBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);

        /// <summary>
        /// 根据客户编号获取客户预警列表
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>客户预警列表</returns>
        IList<MCustomerWarning> GetCustomerWarningLstByCrmId(string crmId, string operatorId, params int[] deptIds);

        /// <summary>
        /// 根据客户单位编号获取欠款订单列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>批量销帐订单列表</returns>
        IList<MBatchWriteOffOrder> GetArrearOrderLstByCrmId(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            ref string xmlSum,
            string crmId,
            string operatorId,
            params int[] deptIds);

        /// <summary>
        /// 销售预警
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>客户预警列表</returns>
        IList<MSalesmanWarning> GetSalesmanWarningLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , string companyId
                                                    , MWarningBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);

        /// <summary>
        /// 根据销售员编号获取销售预警列表
        /// </summary>
        /// <param name="sellerId">销售员编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>销售预警列表</returns>
        IList<MSalesmanWarning> GetSalesmanWarningLstBySellerId(
            string sellerId, string operatorId, params int[] deptIds);

        /// <summary>
        /// 根据销售员编号获取已确认垫款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="sellerId">销售员编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>已确认垫款列表</returns>
        IList<MSalesmanWarning> GetSalesmanConfirmLstBySellerId(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            ref string xmlSum,
            string sellerId,
            string operatorId,
            params int[] deptIds);

        /// <summary>
        /// 垫付申请
        /// </summary>
        /// <param name="mdl">垫付申请实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddTransfinite(MTransfinite mdl);

        /// <summary>
        /// 根据垫付编号获取垫付实体
        /// </summary>
        /// <param name="disburseId">垫付编号</param>
        /// <returns>垫付实体</returns>
        MTransfinite GetTransfiniteMdl(string disburseId);

        /// <summary>
        /// 超限审批：根据搜索实体获取超限审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>超限审批列表</returns>
        IList<MTransfinite> GetTransfiniteLst(int pageSize
                                            , int pageIndex
                                            , ref int recordCount
                                            , MChaXianShenPiChaXunInfo mSearch
                                            , string operatorId
                                            , params int[] deptIds);


        /*/// <summary>
        /// 设置超限审批状态
        /// </summary>
        /// <param name="disburseId">主键编号</param>
        /// <param name="companyId">系统公司编号编号</param>
        /// <param name="deptId">审批人部门编号编号</param>
        /// <param name="approverId">审批人编号</param>
        /// <param name="approver">审批人名</param>
        /// <param name="approveTime">审批时间</param>
        /// <param name="approveRemark">审批意见</param>
        /// <param name="status">0：未审批 1：通过 2：未通过</param>
        /// <param name="itemId">报价编号、团队编号、订单编号</param>
        /// <param name="itemType">0:报价成功、1:成团、2:报名</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetTransfiniteChk(string disburseId
                            , string companyId
                            , int deptId
                            , string approverId
                            , string approver
                            , DateTime approveTime
                            , string approveRemark
                            , TransfiniteStatus status, string itemId
                                    , TransfiniteType itemType);*/

        /// <summary>
        /// 超限审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int ChaoXianShenPi(EyouSoft.Model.FinStructure.MChaoXianShenPiInfo info);

        #endregion        

        #region 往来对账

        /// <summary>
        /// 根据往来对账类型和系统公司编号获取往来对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="typ">往来对账类型</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>往来对账列表</returns>
        IList<MReconciliation> GetReconciliationLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , ReconciliationType typ
                                                    , string companyId
                                                    , MAuditBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);


        #endregion

        #region 财务统计

        /// <summary>
        /// 利润统计
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>利润统计列表</returns>
        IList<MProfitStatistics> GetProfitStatisticsLst(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            ref string xmlSum,
            string companyId,
            MProfitStatisticsBase mSearch,
            string operatorId,
            params int[] deptIds);

        /// <summary>
        /// 预算/结算对比表：根据系统公司编号获取预算/结算对比表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>预算/结算对比表</returns>
        IList<MBudgetContrast> GetBudgetContrastLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , string companyId
                                                    , MBudgetContrastBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);


        /// <summary>
        /// 日记账：根据搜索实体获取日记账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>日记账列表</returns>
        IList<MDayRegister> GetDayRegisterLst(int pageSize
                                            , int pageIndex
                                            , ref int recordCount
                                            , ref string xmlSum
                                            , MDayRegisterBase mSearch
                                            , string operatorId
                                            , params int[] deptIds);

        #endregion

        #region 金蝶报表
        
        /// <summary>
        /// 添加或者修改金蝶凭证
        /// </summary>
        /// <param name="mdl">金蝶凭证</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdKingDeeProof(MKingDeeProof mdl);

        /// <summary>
        /// 根据金蝶财务编号获取金蝶凭证实体
        /// </summary>
        /// <param name="fId">金蝶财务编号</param>
        /// <returns>金蝶凭证实体</returns>
        MKingDeeProof GetKingDeeProofMdl(string fId);

        /// <summary>
        /// 根据团队编号获取金蝶凭证列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>金蝶凭证列表</returns>
        IList<MKingDeeProof> GetKingDeeProofLstByTourId(string tourId);

        /// <summary>
        /// 根据金蝶凭证录入类型获取默认详细凭证列表
        /// </summary>
        /// <param name="type">金蝶凭证录入类型</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="itemId">关联编号：订单收款编号、计调付款编号、导游借款编号、杂费收入编号、杂费支出编号、团队编号</param>
        /// <returns>默认详细凭证列表</returns>
        IList<MKingDeeProofDetail> GetDefaultProofLst(DefaultProofType type, string companyId, string itemId);

        /// <summary>
        /// 根据系统公司编号获取金蝶凭证列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>金蝶凭证列表</returns>
        IList<MKingDeeProofDetail> GetKingDeeProofLst(
            int pageSize, int pageIndex, ref int recordCount, string companyId, MKingDeeProofBase mSearch, string operatorId, params int[] deptIds);

        /// <summary>
        /// 根据系统公司编号获取未导出的金蝶凭证列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>金蝶凭证列表</returns>
        IList<MKingDeeProofDetail> GetKingDeeProofLst(string companyId, MKingDeeProofBase mSearch, string operatorId, params int[] deptIds);

        /// <summary>
        /// 设置凭证导出状态
        /// </summary>
        /// <param name="id">凭证详细编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetProofExport(int id);

        /// <summary>
        /// 根据公司编号设置所有凭证的导出状态
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetProofExport(string companyId);

        /// <summary>
        /// 根据金蝶科目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶科目实体</param>
        /// <returns>0：成功 1：失败 2：科目代码已存在</returns>
        int AddOrUpdKingDeeSubject(MKingDeeSubject mdl);

        /// <summary>
        /// 根据金蝶科目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelKingDeeSubject(string companyId, int subjectId);

        /// <summary>
        /// 根据系统公司编号和科目编号判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>True：已使用 False：未使用</returns>
        bool IsSubjectUsed(string companyId, int subjectId);

        /// <summary>
        /// 根据金蝶核算项目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶核算项目实体</param>
        /// <returns>0：成功 1：失败 2：核算项目代码已存在</returns>
        int AddOrUpdKingDeeChk(MKingDeeChk mdl);

        /// <summary>
        /// 根据金蝶核算项目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelKingDeeChk(string companyId, int chkId);

        /// <summary>
        /// 根据系统公司编号和核算项目编号判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>True：已使用 False：未使用</returns>
        bool IsChkItemUsed(string companyId, int chkId);

        /// <summary>
        /// 根据系统公司编号获取金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId);

        /// <summary>
        /// 根据系统公司编号和上级科目编号获取金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preSubjectId">上级科目编号</param>
        /// <returns></returns>
        IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId, int preSubjectId);

        /// <summary>
        /// 根据系统公司编号获取金蝶科目列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        IList<MKingDeeSubject> GetKingDeeSubjectLst(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 根据系统公司编号和科目编号获取金蝶科目实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>科目实体</returns>
        MKingDeeSubject GetKingDeeSubjectMdl(string companyId, int subjectId);

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        IList<MKingDeeChk> GetKingDeeChkLst(string companyId);

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preChkId">上一级核算项目编号</param>
        /// <returns></returns>
        IList<MKingDeeChk> GetKingDeeChkLst(string companyId, string preChkId);

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        IList<MKingDeeChk> GetKingDeeChkLst(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 根据系统公司编号和核算项目编号获取金蝶核算项目实体
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chkId"></param>
        /// <returns></returns>
        MKingDeeChk GetKingDeeChkMdl(string companyId, int chkId);

        /// <summary>
        /// 根据关联类型和关联编号判断是否已财务入帐
        /// </summary>
        /// <param name="itemType">关联类型</param>
        /// <param name="itemId">关联编号</param>
        /// <param name="companyId">系统公司编号编号</param>
        /// <returns>True：已入帐 False：未入账</returns>
        bool IsFinIn(DefaultProofType itemType, string itemId, string companyId);

        #endregion

        #region 固定资产

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns>True：成功 False：失败</returns>
        bool AddAsset(MAsset mdl);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdAsset(MAsset mdl);

        /// <summary>
        /// 根据资产主键集合删除资产
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="ids">资产主键集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        int DelAsset(string companyId,params int[] ids);

        /// <summary>
        /// 根据资产主键编号获取资产实体
        /// </summary>
        /// <param name="id">资产主键编号</param>
        /// <returns>资产实体</returns>
        MAsset GetAsset(int id);

        /// <summary>
        /// 根据资产搜索实体获取固定资产列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>固定资产列表</returns>
        IList<MAsset> GetAssetLst(int pageSize
                                , int pageIndex
                                , ref int recordCount
                                , MAssetBase mSearch
                                , string operatorId
                                , params int[] deptIds);

        #endregion

        #region 发票管理

        /// <summary>
        /// 添加/修改发票
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        bool AddOrUpdBill(MBill mdl);

        /// <summary>
        /// 根据系统公司编号和发票编号删除发票
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelBill(string companyId, int id);

        /// <summary>
        /// 根据发票编号获取发票实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>发票实体</returns>
        MBill GetBillMdl(string companyId, int id);

        /// <summary>
        /// 根据订单编号获取已开票信息集合
        /// </summary>
        /// <param name="orderId">订单编号或杂费收入编号</param>
        /// <returns></returns>
        IList<MBill> GetBillLst(string orderId);

        /// <summary>
        /// 根据选中订单的订单编号获取批量开票列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>批量开票列表</returns>
        IList<MBill> GetBillLst(List<string> orderIds);

        /// <summary>
        /// 根据搜索实体获取发票列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>发票列表</returns>
        IList<MBill> GetBillLst(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            string companyId,
            MBill mSearch,
            string operatorId,
            params int[] deptIds);

        /// <summary>
        /// 发票审核
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <param name="isApprove">是否审核</param>
        /// <param name="approverId">审核人编号</param>
        /// <param name="approver">审核人</param>
        /// <param name="approveRemark">审核意见</param>
        /// <param name="approveTime">审核时间</param>
        /// <param name="billNo">票据号</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetApproveBill(
            string companyId,
            int id,
            bool isApprove,
            string approverId,
            string approver,
            string approveRemark,
            DateTime? approveTime,
            string billNo);

        #endregion
    }
}
