using System;
using System.Collections.Generic;

namespace EyouSoft.BLL.FinStructure
{
    using System.Linq;

    using EyouSoft.Cache.Facade;
    using EyouSoft.Component.Factory;
    using EyouSoft.IDAL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.PlanStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.Toolkit;

    using MReconciliation = EyouSoft.Model.FinStructure.MReconciliation;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// 0.自己添加的数据（默认）
    /// 1.本部浏览：可以查看自己所在部门内用户的
    /// 2.部门浏览：可以查看自己所在部门及下级部门内用户的数据
    /// 3.内部浏览：可以查看自己所在部门的第一层级部门及下级部门内用户的数据
    /// 4.查看全部：可以查看所有用户的数据
    /// </summary>
    public class BFinance:BLLBase
    {
        private readonly IFinance _dal = ComponentFactory.CreateDAL<IFinance>();

        private bool _isOnlySelf;

        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddProfitDistribute(MProfitDistribute mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.TourId) ||string.IsNullOrEmpty(mdl.Staff))
            {
                return false;
            }
            var isOk = this._dal.AddProfitDistribute(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了利润分配数据");
            }
            return isOk;
        }

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdProfitDistribute(MProfitDistribute mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.TourId) || string.IsNullOrEmpty(mdl.Staff))
            {
                return false;
            }
            var isOk = this._dal.UpdProfitDistribute(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了编号：{0}的利润分配数据",mdl.Id));
            }
            return isOk;
        }

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public bool DelProfitDis(int id, string companyid)
        {
            if (id <= 0 || string.IsNullOrEmpty(companyid))
            {
                return false;
            }
            var isOk = this._dal.DelProfitDis(id,companyid);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了编号：{0}的利润分配数据", id));
            }
            return isOk;
        }

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        public MProfitDistribute GetProfitDistribute(int id, string companId)
        {
            return id <= 0 || string.IsNullOrEmpty(companId) ? null : this._dal.GetProfitDistribute(id, companId);
        }

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MProfitDistribute> GetProfitDistribute(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this._dal.GetProfitDistribute(tourId);
        }

        #endregion

        #region 应收管理

        /// <summary>
        /// 根据应收搜索实体获取应收帐款/已结清账款列表和金额汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSum">金额汇总信息<!--<root><row SumPrice="合同金额" CheckMoney="已收" Unchecked="已收待审" Unreceived="欠款" ReturnMoney="已退" UnChkRtn="已退待审" Bill="开票金额"/></root>--></param>
        /// <param name="mSearch">应收搜索实体</param>
        /// <returns>应收帐款/已结清账款列表</returns>
        public IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , ref MReceivableSum mSum
                                                , MReceivableBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId))
            {
                throw new System.Exception("bll error:查询实体为null或string.IsNullOrEmpty(查询实体.CompanyId)==true。");
            }

            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId) || pageSize <= 0 || pageIndex <= 0)
            {
                return null;
            }
            var lst= this._dal.GetReceivableInfoLst(pageSize, pageIndex, ref recordCount,ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应收管理,out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                mSum.TotalSumPrice = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "SumPrice"));
                mSum.TotalReceived = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "CheckMoney"));
                mSum.TotalUnchecked = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Unchecked"));
                mSum.TotalUnReceived = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Unreceived"));
                mSum.TotalReturned = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ReturnMoney"));
                mSum.TotalUnChkReturn = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "UnChkRtn"));
                mSum.TotalBill = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Bill"));
            }
            return lst;
        }

        /// <summary>
        /// 根据系统公司编号获取当日收款对账列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sum">金额汇总信息<!--<root><row CollectionRefundAmount="收款金额"/></root>--></param>
        /// <param name="tourTypes">团队类型集合</param>
        /// <param name="isShowDistribution">是否同业分销</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>当日收款对账列表</returns>
        public IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref decimal sum
                                                                , IList<TourType> tourTypes
                                                                , bool isShowDistribution
                                                                , string companyId
                                                                , MDayReceivablesChkBase search)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(companyId) || pageSize <= 0 || pageIndex <= 0)
            {
                return null;
            }
            var lst= this._dal.GetDayReceivablesChkLst(
                pageSize, pageIndex, ref recordCount, ref xmlSum, companyId, tourTypes, isShowDistribution, search, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应收管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                sum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "CollectionRefundAmount"));
            }
            return lst;
        }

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="mdl">订单销售收款/退款的实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetTourOrderSalesCheck(MTourOrderSales mdl)
        {
            var isOk = mdl != null && !string.IsNullOrEmpty(mdl.Id) && !string.IsNullOrEmpty(mdl.OrderId) && mdl.ApproverDeptId > 0 && this._dal.SetTourOrderSalesCheck(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("更新了收款/退款编号：{0}的财务的审核状态", mdl.Id));
            }

            if (isOk && mdl.IsJiFen == 1)
            {
                decimal jiFenBiLi = 0;
                var setting = EyouSoft.Security.Membership.UserProvider.GetComSetting(mdl.CompanyId);
                if (setting != null) jiFenBiLi = (decimal)setting.IntegralProportion / 100M;

                if (jiFenBiLi > 0)
                {
                    var jiFenInfo = new EyouSoft.Model.CrmStructure.MJiFenInfo();
                    EyouSoft.BLL.CrmStructure.BCrmMember bll = new EyouSoft.BLL.CrmStructure.BCrmMember();
                    jiFenInfo.CrmId = mdl.CrmId;
                    jiFenInfo.IssueTime = System.DateTime.Now;
                    jiFenInfo.JiFen = mdl.OrderJinE * jiFenBiLi;
                    jiFenInfo.JiFenShiJian = System.DateTime.Now;
                    jiFenInfo.OperatorId = mdl.ApproverId;
                    jiFenInfo.Remark = string.Empty;
                    jiFenInfo.ZengJianLeiBie = EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie.增加;
                    jiFenInfo.OrderId = mdl.OrderId;
                    jiFenInfo.OrderJinE = mdl.OrderJinE;
                    jiFenInfo.JiFenBiLi = jiFenBiLi;

                    new EyouSoft.BLL.CrmStructure.BCrmMember().SetJiFen(jiFenInfo);
                }
            }

            return isOk;
        }

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="lst">订单销售收款/退款的实体</param>
        /// <returns>审核失败的收款/退款编号</returns>
        public IList<string> SetTourOrderSalesCheck(IList<MTourOrderSales> lst)
        {
            if (lst==null||lst.Count==0)
            {
                return null;
            }
            return (from m in lst where !this.SetTourOrderSalesCheck(m) select m.Id).ToList();
        }

        /// <summary>
        /// 报销完成根据团队编号对导游实收进行自动审核
        /// </summary>
        /// <param name="approverDeptId"></param>
        /// <param name="approverId"></param>
        /// <param name="approver"></param>
        /// <param name="approveTime"></param>
        /// <param name="tourId"></param>
        /// <returns>审核失败的收款/退款编号</returns>
        public IList<string> SetTourOrderSalesCheck(int approverDeptId, string approverId, string approver, DateTime approveTime,string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                return null;
            }
            var lst = this._dal.GetTourOrderSalesLstByTourId(tourId);
            if (lst != null && lst.Count > 0)
            {
                foreach (var m in lst)
                {
                    m.ApproverDeptId = approverDeptId;
                    m.ApproverId = approverId;
                    m.Approver = approver;
                    m.ApproveTime = approveTime;
                }
            }
            return this.SetTourOrderSalesCheck(lst);
        }

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        public IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds)
        {
            return orderIds == null || orderIds.Length == 0 ? null : this._dal.GetBatchTourOrderSalesCheck(orderIds);
        }

        #endregion

        #region 杂费收支

        /// <summary>
        /// 添加其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.OperatorId))
            {
                return false;
            }
            var isOk= this._dal.AddOtherFeeInOut(typ, mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了杂费收支");
            }

            return isOk;
        }

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            if (mdl == null || mdl.Id<=0 || string.IsNullOrEmpty(mdl.OperatorId))
            {
                return false;
            }
            var isOk= this._dal.UpdOtherFeeInOut(typ, mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费收支编号：{0}的数据",mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int DelOtherFeeInOut(string companyId,ItemType typ, params int[] ids)
        {
            if (ids==null||ids.Length==0||string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            var isOk= this._dal.DelOtherFeeInOut(companyId,typ, ids);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了杂费收支编号：{0}的数据",Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        public MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id,string companyId)
        {
            return id<=0 ? null : this._dal.GetOtherFeeInOut(typ, id,companyId);
        }

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
        public int SetOtherFeeInOutAudit(ItemType typ, int auditDeptId, string auditId, string audit, string auditRemark, DateTime auditTime, FinStatus status, params int[] ids)
        {
            if (ids == null || ids.Length == 0||string.IsNullOrEmpty(auditId)||string.IsNullOrEmpty(audit))
            {
                return 0;
            }
            var isOk = this._dal.SetOtherFeeInOutAudit(typ, auditDeptId, auditId, audit, auditRemark, auditTime,status, ids);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费收支编号：{0}的审核状态",Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 报销完成时根据团队编号自动审核导游报账时添加的其他收入
        /// </summary>
        /// <param name="auditDeptId">审核人部门编号</param>
        /// <param name="auditId">审核人编号</param>
        /// <param name="audit">审核人</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeInAudit(int auditDeptId, string auditId, string audit, DateTime auditTime, string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                return 0;
            }
            var ids = this._dal.GetOtherFeeInLst(tourId).Select(m=>m.Id).ToArray();
            var isOk = this.SetOtherFeeInOutAudit(ItemType.收入, auditDeptId, auditId, audit, "报销完成自动审核", auditTime, FinStatus.账务待支付, ids);

            return isOk;
        }

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
        public int SetOtherFeeOutPay(int accountantDeptId, string accountantId, string accountant, DateTime payTime, FinStatus status, IList<MBatchPay> lst)
        {
            if (lst == null || lst.Count == 0 || string.IsNullOrEmpty(accountantId) || string.IsNullOrEmpty(accountant))
            {
                return 0;
            }
            var isOk = this._dal.SetOtherFeeOutPay(accountantDeptId, accountantId, accountant, payTime,status, lst);
            if (isOk > 0)
            {
                foreach (var m in lst)
                {
                    //添加操作日志
                    SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费支出编号：{0}的支付状态", m.RegisterId));
                }
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用搜索实体获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mSearch">其他（杂费）收入/支出费用搜索实体</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ItemType typ
                                                        , MOtherFeeInOutBase mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId))
            {
                return null;
            }
            return this._dal.GetOtherFeeInOutLst(pageSize, pageIndex, ref recordCount, typ, mSearch, this.LoginUserId, this.GetDataPrivs(typ== ItemType.收入? Menu2.财务管理_杂费收入:Menu2.财务管理_杂费支出, out _isOnlySelf));
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids)
        {
            if (ids==null || ids.Length==0)
            {
                return null;
            }
            return this._dal.GetOtherFeeInOutLst(typ,ids);
        }

        #endregion

        #region 应付管理

        /// <summary>
        /// 根据应付帐款搜索实体获取应付帐款/已结清账款列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSum">金额汇总信息<!--<root><row Confirmation="应付金额" Prepaid="已付金额" UnChecked="已登待付"/></root>--></param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <returns>应付帐款/已结清账款列表</returns>
        public IList<MPayable> GetPayableLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref MPayableSum mSum
                                        , MPayableBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch==null||string.IsNullOrEmpty(mSearch.CompanyId)||pageIndex<=0||pageSize<=0)
            {
                return null;
            }
            var lst= this._dal.GetPayableLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应付管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                mSum.TotalPayable = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Confirmation"));
                mSum.TotalPaid = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Prepaid"));
                mSum.TotalUnchecked = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "UnChecked"));
            }
            return lst;
        }

        /// <summary>
        /// 根据搜索实体获取当天付款对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sum">金额汇总信息<!--<root><row PaymentAmount="请款金额"/></root>--></param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <returns>当天付款对账列表</returns>
        public IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref decimal sum
                                        , MPayRegister mSearch)
        {
            var xmlSum = string.Empty;
            var lst= this._dal.GetTodayPaidLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应付管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                sum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "PaymentAmount"));
            }
            return lst;
        }

        /// <summary>
        /// 根据计调编号集合获取批量出账登记列表
        /// </summary>
        /// <param name="planIds">计调编号集合</param>
        /// <returns>登记列表</returns>
        public IList<MPayRegister> GetPayRegisterBaseByPlanId(params string[] planIds)
        {
            if (planIds == null || planIds.Length==0)
            {
                return null;
            }
            return planIds.Select(planId => this.GetPayRegisterBaseByPlanId(planId)).Where(m=>m.Register<m.Payable).ToList();
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目出账登记基本信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>登记基本信息</returns>
        public MPayRegister GetPayRegisterBaseByPlanId(string planId)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetPayRegisterBaseByPlanId( planId);
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(string planId,bool? isPrepaid)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetPayRegisterLstByPlanId(planId,isPrepaid);
        }

        /// <summary>
        /// 根据计调编号集合获取支出项目出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(params string[] planId)
        {
            var lst = new List<MRegister>();
            if (planId!=null && planId.Length>0)
            {
                foreach (var i in planId)
                {
                    lst.AddRange(this.GetPayRegisterLstByPlanId(i,(bool?)null));
                }
            }
            return lst;
        }

        ///// <summary>
        ///// 根据团队编号获取计调登记列表实体
        ///// </summary>
        ///// <param name="tourId">团队编号</param>
        ///// <returns>应付账款登记实体</returns>
        //public MPayablePayment GetPayablePaymentByTourId(string tourId)
        //{
        //    return string.IsNullOrEmpty(tourId) ? null : this._dal.GetPayablePaymentByTourId(tourId);
        //}

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int AddRegister(MRegister mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.PlanId)||string.IsNullOrEmpty(mdl.Dealer)||string.IsNullOrEmpty(mdl.OperatorId))
            {
                return 0;
            }
            var isOk= this._dal.AddRegister(mdl);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(mdl.IsPrepaid?"新增预付申请":"新增出账登记");
            }

            return isOk;
        }

        /// <summary>
        /// 批量添加多个登记帐款
        /// </summary>
        /// <param name="lst">登记列表</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int AddRegister(IList<MRegister> lst)
        {
            var isOk = 0;
            foreach (var m in lst)
            {
                isOk = this.AddRegister(m);
                if (isOk<=0)
                {
                    break;
                }
            }
             return isOk;
        }

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int UpdRegister(MRegister mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.Dealer) || string.IsNullOrEmpty(mdl.OperatorId)||mdl.RegisterId<=0)
            {
                return 0;
            }
            var isOk = this._dal.UpdRegister(mdl);
            if (isOk==1)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了登记编号：{0}的出账登记",mdl.RegisterId));
            }

            return isOk;
        }

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        public bool DelRegister(string companyId, int registerId)
        {
            if (registerId<=0 || string.IsNullOrEmpty(companyId))
            {
                return false;
            }
            var isOk= this._dal.DelRegister(companyId, registerId);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了登记编号：{0}出账登记",registerId));
            }

            return isOk;
        }

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="paymentAmount">金额汇总信息<!--<root><row PaymentAmount="请款金额"/></root>--></param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <returns>付款审批列表</returns>
        public IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref decimal paymentAmount
                                                        , MPayableApproveBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch==null||string.IsNullOrEmpty(mSearch.CompanyId))
            {
                return null;
            }
            var lst= this._dal.GetMPayableApproveLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应付管理, out _isOnlySelf));

            if (!string.IsNullOrEmpty(xmlSum))
            {
                paymentAmount = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "PaymentAmount"));
            }

            return lst;
        }

        /// <summary>
        /// 根据登记编号获取登记实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>登记实体</returns>
        public MRegister GetRegisterById(string companyId, int registerId)
        {
            return registerId==0 || string.IsNullOrEmpty(companyId) ? null : this._dal.GetRegisterById(companyId, registerId);
        }

        /// <summary>
        /// 根据登记编号集合获取登记列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerIds">登记编号集合</param>
        /// <returns>登记列表</returns>
        public IList<MRegister> GetRegisterById(string companyId, params int[] registerIds)
        {
            if (registerIds == null || registerIds.Length==0)
            {
                return null;
            }
            return registerIds.Select(registerId => this.GetRegisterById(companyId, registerId)).ToList();
        }

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
        public int SetRegisterApprove(string approverId
                                    , string approver
                                    , DateTime approveTime
                                    , string approveRemark
                                    , FinStatus status
                                    , string companyId
                                    , params int[] registerIds)
        {
            var isOk =0;
            if (registerIds==null||registerIds.Length<=0||string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            isOk = this._dal.SetRegisterApprove(approverId, approver, approveTime, approveRemark, status,companyId, registerIds);
            if (isOk > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("修改了出账登记审核状态");
            }
            return isOk;
        }

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
        public int SetRegisterPay(int accountantDeptId
                                , string accountantId
                                , string accountant
                                , DateTime payTime
                                , string companyId
                                , IList<MBatchPay> lst)
        {
            var isOk = 0;
            if (lst == null || lst.Count <= 0 || string.IsNullOrEmpty(accountantId) || string.IsNullOrEmpty(accountant) || accountantDeptId <= 0 || string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            isOk = this._dal.SetRegisterPay(accountantDeptId, accountantId, accountant, payTime, companyId, lst);
            if (isOk > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("修改了出账登记支付状态");
            }

            return isOk;
        }

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        public MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ)
        {
            return this._dal.GetPlanCostPayMdl(tourId, typ);
        }

        /// <summary>
        /// 根据团队编号、计调类型、是否确认获取计调成本确认列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <param name="isConfirmed">是否确认</param>
        /// <returns>计调成本确认列表</returns>
        public IList<MPlanCostConfirm> GetPlanCostConfirmLst(string tourId, PlanProject typ, bool isConfirmed)
        {
            return this._dal.GetPlanCostConfirmLst(tourId, typ, isConfirmed);
        }

        /// <summary>
        /// 根据计调编号设置计调成本确认
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="costId">成本确认人ID</param>
        /// <param name="costName">成本确认人</param>
        /// <param name="costRemark">成本确认备注</param>
        /// <param name="confirmation">确认金额/结算金额</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetPlanCostConfirmed(string planId, string costId, string costName, string costRemark, decimal confirmation)
        {

            var isOk = this._dal.SetPlanCostConfirmed(planId, costId, costName,costRemark,confirmation);
            if (isOk )
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了计调编号：{0} 的成本确认状态", planId));
            }
            return isOk;
        }

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体（Id=0：添加 Id>0：修改）</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdDebit(MDebit mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.TourId)||string.IsNullOrEmpty(mdl.Borrower))
            {
                return false;
            }
            var isOk = this._dal.AddOrUpdDebit(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(
                    "操作员编号：" + this.LoginUserId + (mdl.Id > 0 ? "修改了财务借款编号：" + mdl.Id : "新增了财务借款"));
            }

            return isOk;
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetDebitApprove(MDebit mdl)
        {
            if (mdl == null || mdl.Id<=0||string.IsNullOrEmpty(mdl.CompanyId))
            {
                return false;
            }
            var isOk= this._dal.SetDebitApprove(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了财务借款编号：{0}的审批状态",mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        public MDebit GetDebit(int id)
        {
            return id<=0 ? null : this._dal.GetDebit(id);
        }

        /// <summary>
        /// 根据借款搜索实体获取借款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">借款搜索实体</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MDebitBase mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId)||pageSize<=0||pageIndex<=0)
            {
                return null;
            }
            return this._dal.GetDebitLst(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_借款管理, out _isOnlySelf));
        }

        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByTourId(string tourId,bool isBz)
        {
            return string.IsNullOrEmpty(tourId) ? null: this._dal.GetDebitLstByTourId(tourId,isBz);
        }

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByPlanId(string planId)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetDebitLstByPlanId(planId);
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool PayDebit(MDebit mdl)
        {
            if (mdl == null || mdl.Id <= 0 || string.IsNullOrEmpty(mdl.CompanyId))
            {
                return false;
            }

            var isOk=this._dal.Pay(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了财务借款编号：{0}，实借金额：{1}的支付状态",mdl.Id,mdl.RealAmount));
            }

            return isOk;
        }

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DeleteDebit(string companyId, int id)
        {
            if (string.IsNullOrEmpty(companyId)||id<=0)
            {
                return false;
            }
            var isOk = this._dal.DeleteDebit(companyId, id);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了财务借款编号：{0}的数据", id));
            }

            return isOk;
        }
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
        /// <returns>客户预警列表</returns>
        public IList<MCustomerWarning> GetCustomerWarningLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId
                                                            , MWarningBase mSearch)
        {
            if (string.IsNullOrEmpty(companyId)||pageIndex<=0||pageSize<=0)
            {
                return null;
            }
            return this._dal.GetCustomerWarningLst(pageSize, pageIndex, ref recordCount, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
        }

        /// <summary>
        /// 根据客户编号获取客户预警列表
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <returns>客户预警列表</returns>
        public IList<MCustomerWarning> GetCustomerWarningLstByCrmId(string crmId)
        {
            return string.IsNullOrEmpty(crmId)
                       ? null
                       : this._dal.GetCustomerWarningLstByCrmId(crmId, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
        }

          /// <summary>
        /// 根据客户单位编号获取欠款订单列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="total">金额汇总<!--<root><row ConfirmMoney="应付金额" CheckMoney="已付金额"/></root>--></param>
        /// <param name="crmId">客户单位编号</param>
        /// <returns>欠款订单列表</returns>
        public IList<MBatchWriteOffOrder> GetArrearOrderLstByCrmId(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref MBatchWriteOffOrder total
                                                                , string crmId)
          {
              var xmlSum = string.Empty;
              if (string.IsNullOrEmpty(crmId) || pageIndex <= 0 || pageSize <= 0)
              {
                  return null;
              }
              var lst= this._dal.GetArrearOrderLstByCrmId(pageSize, pageIndex, ref recordCount, ref xmlSum, crmId, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
              if (!string.IsNullOrEmpty(xmlSum))
              {
                  total.Receivable = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ConfirmMoney"));
                  total.Received = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "CheckMoney"));
                  total.Unreceivable = total.Receivable - total.Received + Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ReturnMoney"));
              }
              return lst;
          }
        /// <summary>
        /// 销售预警
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>客户预警列表</returns>
        public IList<MSalesmanWarning> GetSalesmanWarningLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId
                                                            , MWarningBase mSearch)
        {
            if (string.IsNullOrEmpty(companyId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            return this._dal.GetSalesmanWarningLst(pageSize, pageIndex, ref recordCount, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
        }

        /// <summary>
        /// 根据销售员编号获取销售预警列表
        /// </summary>
        /// <param name="sellerId">销售员编号</param>
        /// <returns>销售预警列表</returns>
        public IList<MSalesmanWarning> GetSalesmanWarningLstBySellerId(string sellerId)
        {
            return string.IsNullOrEmpty(sellerId)
                       ? null
                       : this._dal.GetSalesmanWarningLstBySellerId(sellerId, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
        }

        /// <summary>
        /// 根据销售员编号获取已确认垫款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="payableSum">金额汇总"应付金额"</param>
        /// <param name="paidSum">金额汇总"已付金额"</param>
        /// <param name="returnedSum">金额汇总"已退金额"</param>
        /// <param name="sellerId">销售员编号</param>
        /// <returns>已确认垫款列表</returns>
        public IList<MSalesmanWarning> GetSalesmanConfirmLstBySellerId(int pageSize
                                                                    , int pageIndex
                                                                    , ref int recordCount
                                                                    , ref decimal payableSum
                                                                    , ref decimal paidSum
                                                                    , ref decimal returnedSum
                                                                    , string sellerId)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(sellerId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            var lst= this._dal.GetSalesmanConfirmLstBySellerId(pageSize, pageIndex, ref recordCount, ref xmlSum, sellerId, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                payableSum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ConfirmMoney"));
                paidSum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "CheckMoney"));
                returnedSum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ReturnMoney"));
            }
            return lst;
        }

        /// <summary>
        /// 垫付申请
        /// </summary>
        /// <param name="mdl">垫付申请实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddTransfinite(MTransfinite mdl)
        {
            return mdl != null && this._dal.AddTransfinite(mdl);
        }

        /// <summary>
        /// 根据垫付编号获取垫付实体
        /// </summary>
        /// <param name="disburseId">垫付编号</param>
        /// <returns>垫付实体</returns>
        public MTransfinite GetTransfiniteMdl(string disburseId)
        {
            return string.IsNullOrEmpty(disburseId) ? null : this._dal.GetTransfiniteMdl(disburseId);
        }

        /// <summary>
        /// 超限审批：根据搜索实体获取超限审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>超限审批列表</returns>
        public IList<MTransfinite> GetTransfiniteLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , MChaXianShenPiChaXunInfo mSearch)
        {
            if (mSearch==null|| string.IsNullOrEmpty(mSearch.CompanyId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            return this._dal.GetTransfiniteLst(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_欠款预警, out _isOnlySelf));
        }

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
        public bool SetTransfiniteChk(string disburseId
                                    , string companyId
                                    , int deptId
                                    , string approverId
                                    , string approver
                                    , DateTime approveTime
                                    , string approveRemark
                                    , TransfiniteStatus status, string itemId
                                    , TransfiniteType itemType)
        {
            if (string.IsNullOrEmpty(disburseId)||string.IsNullOrEmpty(approverId)||string.IsNullOrEmpty(approver))
            {
                return false;
            }
            var isOk= this._dal.SetTransfiniteChk(disburseId,companyId,deptId, approverId, approver, approveTime, approveRemark, status,itemId,itemType);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了垫付编号：{0}的财务超限审批的状态",disburseId));
            }

            return isOk;
        }*/

        /// <summary>
        /// 超限审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChaoXianShenPi(EyouSoft.Model.FinStructure.MChaoXianShenPiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.ChaoXianShenQingId) 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId)
                || info.Status == TransfiniteStatus.未审批) return 0;

            info.IssueTime = DateTime.Now;
            int dalRetCode = _dal.ChaoXianShenPi(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("超限审批，超限申请编号：" + info.ChaoXianShenQingId + "，审批状态：" + info.Status);
            }

            if (dalRetCode == 1)
            {
                decimal jiFenBiLi = 0;
                var setting = EyouSoft.Security.Membership.UserProvider.GetComSetting(info.CompanyId);
                if (setting != null) jiFenBiLi = (decimal)setting.IntegralProportion / 100M;

                if (jiFenBiLi > 0)
                {
                    var jiFenInfo = new EyouSoft.Model.CrmStructure.MJiFenInfo();
                    EyouSoft.BLL.CrmStructure.BCrmMember bll = new EyouSoft.BLL.CrmStructure.BCrmMember();
                    jiFenInfo.CrmId = info.CrmId;
                    jiFenInfo.IssueTime = System.DateTime.Now;
                    jiFenInfo.JiFen = info.OrderJinE * jiFenBiLi;
                    jiFenInfo.JiFenShiJian = System.DateTime.Now;
                    jiFenInfo.OperatorId = info.OperatorId;
                    jiFenInfo.Remark = string.Empty;
                    jiFenInfo.ZengJianLeiBie = EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie.增加;
                    jiFenInfo.OrderId = info.OrderId;
                    jiFenInfo.OrderJinE = info.OrderJinE;
                    jiFenInfo.JiFenBiLi = jiFenBiLi;

                    new EyouSoft.BLL.CrmStructure.BCrmMember().SetJiFen(jiFenInfo);
                }
            }

            return dalRetCode;
        }

        #endregion        

        #region 往来对账

        /// <summary>
        /// 根据往来对账类型和系统公司编号获取往来对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sum">金额汇总<!--<root><row Amount="金额"/></root>--></param>
        /// <param name="typ">往来对账类型</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>往来对账列表</returns>
        public IList<MReconciliation> GetReconciliationLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref decimal sum
                                                        , ReconciliationType typ
                                                        , string companyId
                                                        , MAuditBase mSearch)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(companyId))
            {
                return null;
            }
             var lst= this._dal.GetReconciliationLst(pageSize, pageIndex, ref recordCount, ref xmlSum, typ, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_往来对账, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                sum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Amount"));
            }
            return lst;
        }

        #endregion

        #region 财务统计

        /// <summary>
        /// 预算/结算对比表：根据系统公司编号获取预算/结算对比表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="total">金额汇总<!--<root><row BudgetIncome="预算收入" BudgetOutgo="预算支出" ClearingIncome="结算收入" ClearingOutgo="结算支出"/></root>--></param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>预算/结算对比表</returns>
        public IList<MBudgetContrast> GetBudgetContrastLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref MBudgetContrast total
                                                        , string companyId
                                                        , MBudgetContrastBase mSearch)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(companyId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            var lst= this._dal.GetBudgetContrastLst(pageSize, pageIndex, ref recordCount, ref xmlSum, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_财务统计, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                total.BudgetIncome = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "BudgetIncome"));
                total.BudgetOutgo = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "BudgetOutgo"));
                total.ClearingIncome = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ClearingIncome"));
                total.ClearingOutgo = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "ClearingOutgo"));
            }
            return lst;
        }

        /// <summary>
        /// 利润统计
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="total">金额汇总<!--<root><row Income="收入" Outlay="支出" PeopleNum="人数"/></root>--></param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>利润统计列表</returns>
        public IList<MProfitStatistics> GetProfitStatisticsLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref MProfitStatistics total
                                                        , string companyId
                                                        , MProfitStatisticsBase mSearch)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(companyId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            var lst= this._dal.GetProfitStatisticsLst(pageSize, pageIndex, ref recordCount, ref xmlSum, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_财务统计, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                total.Income = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Income"));
                total.Outlay = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Outlay"));
                total.PeopleNum = Utils.GetInt(Utils.GetValueFromXmlByAttribute(xmlSum, "PeopleNum"));
            }
            return lst;
        }
        /// <summary>
        /// 日记账：根据搜索实体获取日记账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="total">金额汇总<!--<root><row DebitCash="借方现金" DebitBank="借方银行" LenderCash="贷方现金" LenderBank="贷方银行"/></root>--></param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>日记账列表</returns>
        public IList<MDayRegister> GetDayRegisterLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref MDayRegister total
                                                    , MDayRegisterBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId) || pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            var lst= this._dal.GetDayRegisterLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_财务统计, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                total.DebitCash = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "DebitCash"));
                total.DebitBank = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "DebitBank"));
                total.LenderCash = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "LenderCash"));
                total.LenderBank = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "LenderBank"));
            }
            return lst;
        }
        
        #endregion

        #region 金蝶报表
        /// <summary>
        /// 添加或者修改金蝶凭证
        /// </summary>
        /// <param name="mdl">金蝶凭证</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdKingDeeProof( MKingDeeProof mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.FCompanyId))
            {
                return false;
            }
            //判断报销完成后有没有导游报账信息已入账
            if (mdl.FItemType==DefaultProofType.单团核算&&this.IsFinIn(DefaultProofType.团未完导游先报账,mdl.FItemId,mdl.FCompanyId))
            {
                mdl.FItemType = DefaultProofType.后期收款;
            }
            //判断报销完成财务入账时是否在单团核算时已入帐
            if (mdl.FItemType==DefaultProofType.团未完导游先报账&&this.IsFinIn(DefaultProofType.单团核算,mdl.FItemId,mdl.FCompanyId))
            {
                mdl.FItemType = DefaultProofType.单团核算;
            }
            return this._dal.AddOrUpdKingDeeProof(mdl);
        }

        /// <summary>
        /// 根据金蝶财务编号获取金蝶凭证实体
        /// </summary>
        /// <param name="fId">金蝶财务编号</param>
        /// <returns>金蝶凭证实体</returns>
        public MKingDeeProof GetKingDeeProofMdl(string fId)
        {
            return string.IsNullOrEmpty(fId) ? null : this._dal.GetKingDeeProofMdl(fId);
        }

        /// <summary>
        /// 根据系团队编号获取金蝶凭证实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>金蝶凭证实体</returns>
        public IList<MKingDeeProof> GetKingDeeProofLstByTourId(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this._dal.GetKingDeeProofLstByTourId(tourId);
        }

        /// <summary>
        /// 根据金蝶凭证录入类型获取默认详细凭证列表
        /// </summary>
        /// <param name="type">金蝶凭证录入类型</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="itemId">关联编号：订单收款编号、计调付款编号、导游借款编号、杂费收入编号、杂费支出编号、团队编号</param>
        /// <returns>默认详细凭证列表</returns>
        public IList<MKingDeeProofDetail> GetDefaultProofLst(DefaultProofType type, string companyId, string itemId)
        {
            //单团核算的时候，判断财务报销完成是否已入账
            if (type==DefaultProofType.单团核算 && this.IsFinIn(DefaultProofType.团未完导游先报账,itemId,companyId))
            {
                type = DefaultProofType.后期收款;
            }
            //报销完成时财务入账，判断单团核算是否已入帐
            if (type == DefaultProofType.团未完导游先报账 && this.IsFinIn(DefaultProofType.单团核算, itemId, companyId))
            {
                type = DefaultProofType.单团核算;
            }
            return string.IsNullOrEmpty(companyId) ? null : this._dal.GetDefaultProofLst(type, companyId, itemId);
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶凭证列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>金蝶凭证列表</returns>
        public IList<MKingDeeProofDetail> GetKingDeeProofLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , string companyId
                                                    , MKingDeeProofBase mSearch)
        {
            return string.IsNullOrEmpty(companyId)
                       ? null
                       : this._dal.GetKingDeeProofLst(pageSize, pageIndex, ref recordCount, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_金蝶报表, out _isOnlySelf));
        }

        /// <summary>
        /// 根据系统公司编号获取未导出的金蝶凭证列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>金蝶凭证列表</returns>
        public IList<MKingDeeProofDetail> GetKingDeeProofLst(string companyId, MKingDeeProofBase mSearch)
        {
            return string.IsNullOrEmpty(companyId)
                       ? null
                       : this._dal.GetKingDeeProofLst(companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_金蝶报表, out _isOnlySelf));
        }

        /// <summary>
        /// 批量设置凭证导出状态
        /// </summary>
        /// <param name="ids">凭证详细编号集合</param>
        /// <returns>设置失败凭证详细编号集合</returns>
        public IList<int> SetProofExport(params int[] ids)
        {
            var fail = new List<int>();
            if (ids != null && ids.Length == 0)
            {
                fail.AddRange(ids.Where(id => !this.SetProofExport(id)));
            }
            return fail;
        }

        /// <summary>
        /// 设置凭证导出状态
        /// </summary>
        /// <param name="id">凭证详细编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetProofExport(int id)
        {
            if (id<=0)
            {
                return false;
            }
            var ok = this._dal.SetProofExport(id);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("设置了金蝶详细凭证编号：{0}的导出状态", id));
            }
            return ok;
        }

        /// <summary>
        /// 根据公司编号设置所有凭证的导出状态
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetProofExport(string companyId)
        {
            var ok = this._dal.SetProofExport(companyId);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("设置了系统公司编号：{0}的所有金蝶凭证的导出状态", companyId));
            }
            return ok;
        }

        /// <summary>
        /// 根据系统公司编号获取没有下级的可用金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>金蝶科目列表</returns>
        public IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId)
        {
            IList<MKingDeeSubject> lst;

            var cache = EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeSubject);
            if (cache != null)
            {
                lst = (IList<MKingDeeSubject>)cache;
            }
            else
            {
                lst = string.IsNullOrEmpty(companyId) ? null : this._dal.GetKingDeeSubjectLst(companyId);
                if (lst == null)
                {
                    return null;
                }

                EyouSoftCache.Add(Cache.Tag.TagName.KingDeeSubject, lst);
            }

            return lst;
        }

        /// <summary>
        /// 根据系统公司编号和上级科目编号获取金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preSubjectId">上级科目编号（为0时获取一级科目）</param>
        /// <returns></returns>
        public IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId,int preSubjectId)
        {
            return string.IsNullOrEmpty(companyId) ? null : this._dal.GetKingDeeSubjectLst(companyId,preSubjectId);
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶科目列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        public IList<MKingDeeSubject> GetKingDeeSubjectLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId)
        {
            return string.IsNullOrEmpty(companyId)
                       ? null
                       : this._dal.GetKingDeeSubjectLst(pageSize, pageIndex, ref recordCount, companyId);
        }

        /// <summary>
        /// 根据系统公司编号和科目编号获取金蝶科目实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>科目实体</returns>
        public MKingDeeSubject GetKingDeeSubjectMdl(string companyId, int subjectId)
        {
            return string.IsNullOrEmpty(companyId) || subjectId <= 0
                       ? null
                       : this._dal.GetKingDeeSubjectMdl(companyId, subjectId);
        }

        /// <summary>
        /// 根据系统公司编号获取没有下级的可用金蝶核算项目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>金蝶核算项目列表</returns>
        public IList<MKingDeeChk> GetKingDeeChkLst(string companyId)
        {
            IList<MKingDeeChk> lst;

            var cache = EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeChkItem);
            if (cache != null)
            {
                lst = (IList<MKingDeeChk>)cache;
            }
            else
            {
                lst = string.IsNullOrEmpty(companyId) ? null : this._dal.GetKingDeeChkLst(companyId);
                if (lst == null)
                {
                    return null;
                }

                EyouSoftCache.Add(Cache.Tag.TagName.KingDeeChkItem, lst);
            }

            return lst;
        }

        /// <summary>
        /// 根据系统公司编号和上一级核算项目编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preChkId">上一级核算项目编号（为0时获取一级核算项目）</param>
        /// <returns></returns>
        public IList<MKingDeeChk> GetKingDeeChkLst(string companyId,string preChkId)
        {
            return string.IsNullOrEmpty(companyId) ? null : this._dal.GetKingDeeChkLst(companyId,preChkId);
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IList<MKingDeeChk> GetKingDeeChkLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId)
        {
            return string.IsNullOrEmpty(companyId)
                       ? null
                       : this._dal.GetKingDeeChkLst(pageSize, pageIndex, ref recordCount, companyId);
        }

        /// <summary>
        /// 根据系统公司编号和核算项目编号获取金蝶核算项目实体
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chkId"></param>
        /// <returns></returns>
        public MKingDeeChk GetKingDeeChkMdl(string companyId, int chkId)
        {
            return string.IsNullOrEmpty(companyId) || chkId <= 0 ? null : this._dal.GetKingDeeChkMdl(companyId, chkId);
        }

        /// <summary>
        /// 根据金蝶科目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶科目实体</param>
        /// <returns>0：成功 1：失败 2：科目代码已存在 3：该科目已被使用</returns>
        public int AddOrUpdKingDeeSubject(MKingDeeSubject mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.SubjectCd)||string.IsNullOrEmpty(mdl.SubjectNm))
            {
                return 1;
            }
            if (this.IsSubjectUsed(mdl.CompanyId,mdl.SubjectId)==0)
            {
                return 3;
            }
            var isOk = this._dal.AddOrUpdKingDeeSubject(mdl);
            if (isOk==0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增或修改了金蝶科目代码：{0}的数据", mdl.SubjectCd));
                if (EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeSubject) != null)
                {
                    EyouSoftCache.Remove(Cache.Tag.TagName.KingDeeSubject);
                }
            }

            return isOk;
        }

        /// <summary>
        /// 根据金蝶科目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>0：成功 1：失败 2：已使用</returns>
        public int DelKingDeeSubject(string companyId, int subjectId)
        {
            if (string.IsNullOrEmpty(companyId) || subjectId <= 0)
            {
                return 1;
            }
            if (this._dal.IsSubjectUsed(companyId,subjectId))
            {
                return 2;
            }
            if (!this._dal.DelKingDeeSubject(companyId, subjectId))
            {
                return 1;
            }
            //添加操作日志
            SysStructure.BSysLogHandle.Insert(string.Format("删除了金蝶科目编号：{0}的数据", subjectId));
            if (EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeSubject) != null)
            {
                EyouSoftCache.Remove(Cache.Tag.TagName.KingDeeSubject);
            }
            return 0;
        }

        /// <summary>
        /// 根据金蝶科目编号批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="msg">已使用科目报框</param>
        /// <param name="subjectId">科目编号集合</param>
        /// <returns>0：成功 1：失败 2：已使用</returns>
        public int DelKingDeeSubject(string companyId, ref string msg, params int[] subjectId)
        {
            if (string.IsNullOrEmpty(companyId) || subjectId == null || subjectId.Length <= 0)
            {
                return 1;
            }
            var ok = 0;
            foreach (var i in subjectId)
            {
                ok = DelKingDeeSubject(companyId, i);
                if (ok == 0)
                {
                    continue;
                }
                if (ok == 2)
                {
                    var m = this.GetKingDeeSubjectMdl(companyId,i);
                    msg = m.SubjectCd + "-" + m.SubjectNm + "该科目已被使用不能删除！";
                }
                break;
            }
            return ok;
        }

        /// <summary>
        /// 根据系统公司编号和科目代码判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>0：已使用 1：未使用 2：失败</returns>
        public int IsSubjectUsed(string companyId, int subjectId)
        {
            if (string.IsNullOrEmpty(companyId)||subjectId<=0)
            {
                return 2;
            }
            return this._dal.IsSubjectUsed(companyId, subjectId) ? 0 : 1;
        }

        /// <summary>
        /// 根据金蝶核算项目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶核算项目实体</param>
        /// <returns>0：成功 1：失败 2：核算项目代码已存在 3：父级核算项目已被使用 4：该核算项目已被使用</returns>
        public int AddOrUpdKingDeeChk(MKingDeeChk mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.ChkCd) || string.IsNullOrEmpty(mdl.ChkNm))
            {
                return 1;
            }
            if (this.IsChkItemUsed(mdl.CompanyId,mdl.ChkId)==0)
            {
                return 4;
            }
            //if (this.IsChkItemUsed(mdl.CompanyId,mdl.PreChkId)==0)
            //{
            //    return 3;
            //}
            var  isOk=this._dal.AddOrUpdKingDeeChk(mdl);
            if (isOk==0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增或修改了金蝶核算项目代码：{0}的数据", mdl.ChkCd));
                if (EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeChkItem) != null)
                {
                    EyouSoftCache.Remove(Cache.Tag.TagName.KingDeeChkItem);
                }
            }

            return isOk;
        }

        /// <summary>
        /// 根据金蝶核算项目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>0：成功 1：失败 2：已使用</returns>
        public int DelKingDeeChk(string companyId, int chkId)
        {
            if (string.IsNullOrEmpty(companyId) || chkId <= 0)
            {
                return 1;
            }
            if (this._dal.IsChkItemUsed(companyId, chkId))
            {
                return 2;
            }
            if (!this._dal.DelKingDeeChk(companyId, chkId))
            {
                return 1;
            }
            //添加操作日志
            SysStructure.BSysLogHandle.Insert(string.Format("删除了金蝶核算项目编号：{0}的数据", chkId));
            if (EyouSoftCache.GetCache(Cache.Tag.TagName.KingDeeChkItem) != null)
            {
                EyouSoftCache.Remove(Cache.Tag.TagName.KingDeeChkItem);
            }
            return 0;
        }

        /// <summary>
        /// 根据金蝶核算项目编号批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="msg">已使用核算项目报框</param>
        /// <param name="chkId">核算项目编号集合</param>
        /// <returns>0：成功 1：失败 2：已使用</returns>
        public int DelKingDeeChk(string companyId, ref string msg, params int[] chkId)
        {
            if (string.IsNullOrEmpty(companyId) || chkId == null || chkId.Length <= 0)
            {
                return 1;
            }
            var ok = 0;
            foreach (var i in chkId)
            {
                ok = DelKingDeeChk(companyId, i);
                if (ok == 0)
                {
                    continue;
                }
                if (ok == 2)
                {
                    var m = this.GetKingDeeChkMdl(companyId, i);
                    msg = m.ChkCd + "-" + m.ChkNm + "该核算项目已被使用不能删除！";
                }
                break;
            }
            return ok;
        }

        /// <summary>
        /// 根据系统公司编号和核算项目代码判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>0：已使用 1：未使用 2：失败</returns>
        public int IsChkItemUsed(string companyId, int chkId)
        {
            if (string.IsNullOrEmpty(companyId) || chkId <= 0)
            {
                return 2;
            }
            return this._dal.IsChkItemUsed(companyId, chkId) ? 0 : 1;
        }

        /// <summary>
        /// 根据关联类型和关联编号判断是否已财务入帐
        /// </summary>
        /// <param name="itemType">关联类型</param>
        /// <param name="itemId">关联编号</param>
        /// <param name="companyId">系统公司编号编号</param>
        /// <returns>True：已入帐 False：未入账</returns>
        public bool IsFinIn(DefaultProofType itemType,string itemId,string companyId)
        {
            return !(string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(companyId)) && this._dal.IsFinIn(itemType, itemId, companyId);
        }
        #endregion

        #region 固定资产

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddAsset(MAsset mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.AssetCode)
                ||string.IsNullOrEmpty(mdl.AssetName)||string.IsNullOrEmpty(mdl.Department))
            {
                return false;
            }
            var isOk= this._dal.AddAsset(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了固定资产数据");
            }

            return isOk;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdAsset(MAsset mdl)
        {
            if (mdl == null || mdl.Id<=0 || string.IsNullOrEmpty(mdl.AssetCode)
                || string.IsNullOrEmpty(mdl.AssetName) || string.IsNullOrEmpty(mdl.Department))
            {
                return false;
            }
            var isOk= this._dal.UpdAsset(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了固定资产编号：{0}的数据",mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 根据资产主键集合删除资产
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="ids">资产主键集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        public int DelAsset(string companyId,params int[] ids)
        {
            if (ids==null||ids.Length==0)
            {
                return 0;
            }
            var isOk=this._dal.DelAsset(companyId,ids);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了固定资产编号：{0}的数据",Utils.GetSqlIdStrByList(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据资产主键编号获取资产实体
        /// </summary>
        /// <param name="id">资产主键编号</param>
        /// <returns>资产实体</returns>
        public MAsset GetAsset(int id)
        {
            return id<=0 ? null : this._dal.GetAsset(id);
        }

        /// <summary>
        /// 根据资产搜索实体获取固定资产列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>固定资产列表</returns>
        public IList<MAsset> GetAssetLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MAssetBase mSearch)
        {
            if (mSearch==null||string.IsNullOrEmpty(mSearch.CompanyId)||pageSize<=0||pageIndex<=0)
            {
                return null;
            }
            return this._dal.GetAssetLst(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_固定资产, out _isOnlySelf));
        }

        #endregion

        #region 发票管理
        /// <summary>
        /// 添加/修改发票
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        public bool AddOrUpdBill(MBill mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.SellerId)||string.IsNullOrEmpty(mdl.CustomerId)||string.IsNullOrEmpty(mdl.Customer)||string.IsNullOrEmpty(mdl.DealerId)||string.IsNullOrEmpty(mdl.Dealer)||string.IsNullOrEmpty(mdl.OperatorId)||string.IsNullOrEmpty(mdl.Operator))
            {
                return false;
            }
            var ok=this._dal.AddOrUpdBill(mdl);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(mdl.Id>0?string.Format("修改了编号：{0}的发票数据。", mdl.Id):"新增发票数据。");
            }
            return ok;
        }

        /// <summary>
        /// 批量添加/修改发票
        /// </summary>
        /// <param name="lst">发票列表</param>
        /// <returns>添加/修改失败的发票列表</returns>
        public IList<MBill> AddOrUpdBill(List<MBill> lst)
        {
            if (lst==null||lst.Count==0)
            {
                return null;
            }
            return lst.Where(mdl => !this.AddOrUpdBill(mdl)).ToList();
        }

        /// <summary>
        /// 根据系统公司编号和发票编号删除发票
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelBill(string companyId, int id)
        {
            if (string.IsNullOrEmpty(companyId) || id<=0)
            {
                return false;
            }
            var ok= this._dal.DelBill(companyId, id);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了编号：{0}的发票数据。", id));
            }
            return ok;
        }

        /// <summary>
        /// 根据发票编号获取发票实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>发票实体</returns>
        public MBill GetBillMdl(string companyId, int id)
        {
            if (string.IsNullOrEmpty(companyId) || id <= 0)
            {
                return null;
            }
            return this._dal.GetBillMdl(companyId, id);
        }

        /// <summary>
        /// 根据订单编号获取已开票信息集合
        /// </summary>
        /// <param name="orderId">订单编号或杂费收入编号</param>
        /// <returns></returns>
        public IList<MBill> GetBillLst(string orderId)
        {
            if (string.IsNullOrEmpty(orderId)) return null;

            return _dal.GetBillLst(orderId);
        }

        /// <summary>
        /// 根据搜索实体获取发票列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>发票列表</returns>
        public IList<MBill> GetBillLst(int pageSize, int pageIndex, ref int recordCount, string companyId, MBill mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId) || pageIndex <= 0 || pageSize <= 0) return null;

            return this._dal.GetBillLst(pageSize, pageIndex, ref recordCount, companyId, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_发票管理, out _isOnlySelf));
        }

        /// <summary>
        /// 根据选中订单的订单编号获取批量开票列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>批量开票列表</returns>
        public IList<MBill> GetBillLst(List<string> orderIds)
        {
            return orderIds == null || orderIds.Count == 0 ? null : this._dal.GetBillLst(orderIds);
        }

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
        public bool SetApproveBill(string companyId, int id, bool isApprove, string approverId, string approver, string approveRemark, DateTime? approveTime,string billNo)
        {
            if (string.IsNullOrEmpty(companyId)||id<=0||string.IsNullOrEmpty(approverId)||string.IsNullOrEmpty(approver))
            {
                return false;
            }
            var ok= this._dal.SetApproveBill(companyId, id, isApprove, approverId, approver, approveRemark,  approveTime,billNo);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了发票编号：{0}的审核状态。", id));
            }
            return ok;
        }

        /// <summary>
        /// 批量发票审核
        /// </summary>
        /// <param name="lst">发票列表</param>
        /// <returns></returns>
        public bool SetApproveBill(List<MBill> lst)
        {
            var isOk = false;
            foreach (var m in lst)
            {
                isOk = this.SetApproveBill(m.CompanyId, m.Id, m.IsApprove, m.ApproverId, m.Approver, m.ApproveRemark, m.ApproveTime, m.BillNo);
                if (!isOk)
                {
                    return false;
                }
            }
            return isOk;
        }
        #endregion
    }
}
