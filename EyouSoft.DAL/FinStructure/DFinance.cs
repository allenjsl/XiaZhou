using System;
using System.Collections.Generic;

namespace EyouSoft.DAL.FinStructure
{
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    using EyouSoft.IDAL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.CrmStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.Toolkit;
    using EyouSoft.Toolkit.DAL;

    using Microsoft.Practices.EnterpriseLibrary.Data;

    using MReconciliation = EyouSoft.Model.FinStructure.MReconciliation;
    using EyouSoft.Model.CrmStructure;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-15
    /// </summary>
    public class DFinance : DALBase, IFinance
    {
        #region dal变量
        EyouSoft.DAL.TourStructure.DTourOrder dTourOrder = new EyouSoft.DAL.TourStructure.DTourOrder();
        #endregion

        #region 构造函数
        /// <summary>
        /// database
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DFinance()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region SQL语句

        private static string SQL_SELECT_KINGDEEPROOF =
                                                    "SELECT  FId,FCompanyId,FTourId,FTourCode,FItemType,FItemId,FDate,FYear,FPeriod,FGroupID,FNumber,FPreparerID,FCheckerID,FApproveID,FCashierID,FHandler,FSettleTypeID,FSettleNo,FQuantity,FMeasureUnitID,FUnitPrice,FReference,FTransDate,FTransNo,FAttachments,FSerialNum,FObjectName,FParameter,FExchangeRate,FPosted,FInternalInd,FCashFlow"
                                                    + "		,XMLDetail=(SELECT FDetailId,FId,FExplanation,FAccountNum,FAccountName,FCurrencyNum,FCurrencyName,FAmountFor,FDebit,FCredit,FEntryID,FItem,FIsExport"
                                                    + "					FROM tbl_FinKingDeeProofDetail"
                                                    + "					WHERE tbl_FinKingDeeProofDetail.FId=tbl_FinKingDeeProof.FId"
                                                    + "					FOR XML RAW,ROOT)"
                                                    + "FROM    tbl_FinKingDeeProof";

        private static readonly string SQL_SELECT_KINGDEESUBJECT = " WITH CTE AS"
                                                                   + " 	("
                                                                   + " 	 SELECT"
                                                                   + " 		A.SubjectId"
                                                                   + " 		,A.CompanyId"
                                                                   + " 		,A.PreSubjectId"
                                                                   + " 		,A.SubjectCd"
                                                                   + " 		,CAST(A.SubjectNm AS NVARCHAR(MAX)) SubjectNm"
                                                                   + " 		,A.SubjectTyp"
                                                                   + " 		,A.MnemonicCd"
                                                                   + " 		,A.ChkId"
                                                                   + " 		,A.ItemId"
                                                                   + " 		,A.UserId"
                                                                   + " 		,A.GuideId"
                                                                   + " 		,PreSubjectNm=(SELECT SubjectNm FROM tbl_FinKingDeeSubject WHERE SubjectId=A.PreSubjectId)"
                                                                   + " 		,XMLItem=CAST(( SELECT ChkId,ChkCate,ChkCd,ChkNm,PreChkNm=ChkNm FROM tbl_FinKingDeeChk WHERE ChkId IN (SELECT [value] FROM fn_split(A.ChkId,',')) FOR XML RAW,ROOT) AS XML)"
                                                                   + " 		,'' ChkItem"
                                                                   + " 	 FROM tbl_FinKingDeeSubject A"
                                                                   + " 	 WHERE "
                                                                   + " 		(SELECT COUNT(*)"
                                                                   + " 		 FROM tbl_FinKingDeeSubject B"
                                                                   + " 		 WHERE B.PreSubjectId=A.SubjectId AND B.IsDeleted='0')=0"
                                                                   + " 		AND A.CompanyId=@CompanyId  AND IsDeleted='0'"
                                                                   + " 	 UNION ALL "
                                                                   + " 	 SELECT"
                                                                   + " 		A.SubjectId"
                                                                   + " 		,A.CompanyId"
                                                                   + " 		,B.PreSubjectId"
                                                                   + " 		,A.SubjectCd"
                                                                   + " 		,CAST(B.SubjectNm + ' - ' + A.SubjectNm AS NVARCHAR(MAX)) SubjectNm"
                                                                   + " 		,A.SubjectTyp"
                                                                   + " 		,A.MnemonicCd "
                                                                   + " 		,A.ChkId"
                                                                   + " 		,A.ItemId"
                                                                   + " 		,A.UserId"
                                                                   + " 		,A.GuideId"
                                                                   + " 		,A.PreSubjectNm"
                                                                   + " 		,A.XMLItem"
                                                                   + " 		,A.ChkItem"
                                                                   + " 	 FROM CTE AS A "
                                                                   + " 	 INNER JOIN tbl_FinKingDeeSubject AS B "
                                                                   + " 	 ON B.SubjectId=A.PreSubjectId"
                                                                   + " 	 ) ";

        const string SQL_SELECT_KINGDEECHK = " WITH CHKCTE AS"
                                            + " 	("
                                            + " 	 SELECT"
                                            + " 		A.ChkId"
                                            + " 		,A.CompanyId"
                                            + " 		,A.PreChkId"
                                            + " 		,A.ChkCd"
                                            + " 		,A.ChkNm"
                                            + " 		,A.MnemonicCd"
                                            + " 		,A.ChkCate"
                                            + " 		,A.ItemId"
                                            + " 		,PreChkNm=(SELECT ChkNm FROM tbl_FinKingDeeChk WHERE ChkId=A.PreChkId)"
                                            + " 	 FROM tbl_FinKingDeeChk A"
                                            + " 	 WHERE "
                                            + " 		(SELECT COUNT(*)"
                                            + " 		 FROM tbl_FinKingDeeChk B"
                                            + " 		 WHERE B.PreChkId=A.ChkId AND B.IsDeleted='0')=0"
                                            + " 		AND A.CompanyId=@CompanyId  AND IsDeleted='0'"
                                            + " 	 UNION ALL "
                                            + " 	 SELECT"
                                            + " 		A.ChkId"
                                            + " 		,A.CompanyId"
                                            + " 		,B.PreChkId"
                                            + " 		,A.ChkCd"
                                            + " 		,A.ChkNm"
                                            + " 		,A.MnemonicCd"
                                            + " 		,A.ChkCate"
                                            + " 		,A.ItemId"
                                            + " 		,A.PreChkNm"
                                            + " 	 FROM CHKCTE AS A "
                                            + " 	 INNER JOIN tbl_FinKingDeeChk AS B "
                                            + " 	 ON B.ChkId=A.PreChkId"
                                            + " 	 ) ";
        #endregion

        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddProfitDistribute(MProfitDistribute mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" INSERT INTO [tbl_FinProfitDistribute]");
            strSql.Append("            ([CompanyId]");
            strSql.Append("            ,[TourId]");
            strSql.Append("            ,[TourCode]");
            strSql.Append("            ,[OrderId]");
            strSql.Append("            ,[OrderCode]");
            strSql.Append("            ,[DistributeType]");
            strSql.Append("            ,[Amount]");
            strSql.Append("            ,[Gross]");
            strSql.Append("            ,[Profit]");
            strSql.Append("            ,[StaffId]");
            strSql.Append("            ,[Staff]");
            strSql.Append("            ,[Remark]");
            strSql.Append("            ,[OperatorId]");
            strSql.Append("            ,[IssueTime])");
            strSql.Append("      VALUES");
            strSql.Append("            (@CompanyId");
            strSql.Append("            ,@TourId");
            strSql.Append("            ,@TourCode");
            strSql.Append("            ,@OrderId");
            strSql.Append("            ,@OrderCode");
            strSql.Append("            ,@DistributeType");
            strSql.Append("            ,@Amount");
            strSql.Append("            ,@Gross");
            strSql.Append("            ,@Profit");
            strSql.Append("            ,@StaffId");
            strSql.Append("            ,@Staff");
            strSql.Append("            ,@Remark");
            strSql.Append("            ,@OperatorId");
            strSql.Append("            ,@IssueTime)");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(dc, "@OrderCode", DbType.String, mdl.OrderCode);
            this._db.AddInParameter(dc, "@DistributeType", DbType.String, mdl.DistributeType);
            this._db.AddInParameter(dc, "@Amount", DbType.Decimal, mdl.Amount);
            this._db.AddInParameter(dc, "@Gross", DbType.Decimal, mdl.Gross);
            this._db.AddInParameter(dc, "@Profit", DbType.Decimal, mdl.Profit);
            this._db.AddInParameter(dc, "@StaffId", DbType.AnsiStringFixedLength, mdl.StaffId);
            this._db.AddInParameter(dc, "@Staff", DbType.String, mdl.Staff);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdProfitDistribute(MProfitDistribute mdl)
        {
            var sql = new StringBuilder();

            sql.Append(" UPDATE  [dbo].[tbl_FinProfitDistribute]");
            sql.Append(" SET     [TourId] = @TourId ,");
            sql.Append("         [TourCode] = @TourCode ,");
            sql.Append("         [OrderId] = @OrderId ,");
            sql.Append("         [OrderCode] = @OrderCode ,");
            sql.Append("         [DistributeType] = @DistributeType ,");
            sql.Append("         [Amount] = @Amount ,");
            sql.Append("         [Gross] = @Gross ,");
            sql.Append("         [Profit] = @Profit ,");
            sql.Append("         [StaffId] = @StaffId ,");
            sql.Append("         [Staff] = @Staff ,");
            sql.Append("         [Remark] = @Remark ");
            sql.Append(" WHERE   Id = @Id");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(dc, "@OrderCode", DbType.String, mdl.OrderCode);
            this._db.AddInParameter(dc, "@DistributeType", DbType.String, mdl.DistributeType);
            this._db.AddInParameter(dc, "@Amount", DbType.Decimal, mdl.Amount);
            this._db.AddInParameter(dc, "@Gross", DbType.Decimal, mdl.Gross);
            this._db.AddInParameter(dc, "@Profit", DbType.Decimal, mdl.Profit);
            this._db.AddInParameter(dc, "@StaffId", DbType.AnsiStringFixedLength, mdl.StaffId);
            this._db.AddInParameter(dc, "@Staff", DbType.String, mdl.Staff);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public bool DelProfitDis(int id, string companyid)
        {
            var sql = "update tbl_FinProfitDistribute set isdeleted='1' where id=@id and companyid=@companyid";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "@id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@companyid", DbType.AnsiStringFixedLength, companyid);
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        public MProfitDistribute GetProfitDistribute(int id, string companId)
        {
            var mdl = new MProfitDistribute();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [DistributeType] ,");
            sql.Append("         [Amount] ,");
            sql.Append("         [Gross] ,");
            sql.Append("         [Profit] ,");
            sql.Append("         [Staff] ,");
            sql.Append("         [Remark] ");
            sql.Append(" FROM    [dbo].[tbl_FinProfitDistribute] where id=@id and companyid=@companyid");

            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@Companyid", DbType.AnsiStringFixedLength, companId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Staff = dr["Staff"].ToString();
                    mdl.DistributeType = dr["DistributeType"].ToString();
                    mdl.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    mdl.Gross = dr.GetDecimal(dr.GetOrdinal("Gross"));
                    mdl.Profit = dr.GetDecimal(dr.GetOrdinal("Profit"));
                    mdl.Remark = dr["Remark"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MProfitDistribute> GetProfitDistribute(string tourId)
        {
            var lst = new List<MProfitDistribute>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [Id] ,");
            sql.Append("         [CompanyId] ,");
            sql.Append("         [TourCode] ,");
            sql.Append("         [OrderCode] ,");
            sql.Append("         [DistributeType] ,");
            sql.Append("         [Amount] ,");
            sql.Append("         [Gross] ,");
            sql.Append("         [Profit] ,");
            sql.Append("         [Staff] ,");
            sql.Append("         [Remark] ");
            sql.Append(" FROM    [dbo].[tbl_FinProfitDistribute]");
            sql.Append(" WHERE   TourId = @TourId and isdeleted='0'");
            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MProfitDistribute
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            CompanyId = dr["CompanyId"].ToString(),
                            TourCode = dr["TourCode"].ToString(),
                            OrderCode = dr["OrderCode"].ToString(),
                            DistributeType = dr["DistributeType"].ToString(),
                            Amount = dr.GetDecimal(dr.GetOrdinal("Amount")),
                            Gross = dr.GetDecimal(dr.GetOrdinal("Gross")),
                            Profit = dr.GetDecimal(dr.GetOrdinal("Profit")),
                            Staff = dr["Staff"].ToString(),
                            Remark = dr["Remark"].ToString(),
                        });
                }
            }
            return lst;
        }

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
        public IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , MReceivableBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            const string table = "tbl_TourOrder";
            var sumfield = new StringBuilder();
            var lst = new List<MReceivableInfo>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            sumfield.Append(" SELECT  ISNULL(SUM(ConfirmMoney),0) SumPrice ,");
            sumfield.Append("         ISNULL(SUM(CheckMoney),0) CheckMoney ,");
            sumfield.Append("         ISNULL(SUM(ReceivedMoney - CheckMoney),0) Unchecked ,");
            sumfield.Append("         ISNULL(SUM(ConfirmMoney - CheckMoney + ReturnMoney),0) Unreceived ,");
            sumfield.Append("         ISNULL(SUM(ReturnMoney),0) ReturnMoney ,");
            sumfield.Append("         ISNULL(SUM(UnChkRtn),0) UnChkRtn ,");
            sumfield.Append("         ISNULL(SUM(Bill),0) Bill");
            sumfield.Append(" FROM    ( SELECT    SumPrice ,");
            sumfield.Append("                     OrderCode ,");
            sumfield.Append("                     BuyCompanyId ,");
            sumfield.Append("                     BuyCompanyName ,");
            sumfield.Append("                     SellerId ,");
            sumfield.Append("                     SellerName ,");
            sumfield.Append("                     IsClean ,");
            sumfield.Append("                     TourId ,");
            sumfield.Append("                     Status ,");
            sumfield.Append("                     IsDelete ,");
            sumfield.Append("                     CompanyId ,");
            sumfield.Append("                     DeptId ,");
            sumfield.Append("                     OrderType ,");
            sumfield.Append("                     CheckMoney ,");
            sumfield.Append("                     ReceivedMoney ,");
            sumfield.Append("                     ConfirmMoney ,");
            sumfield.Append("                     ReturnMoney ,");
            sumfield.Append("                     UnChkRtn = ( SELECT ISNULL(SUM(CollectionRefundAmount),0)");
            sumfield.Append("                                  FROM   tbl_TourOrderSales");
            sumfield.Append("                                  WHERE  OrderId = tbl_tourorder.OrderId");
            sumfield.Append("                                         AND CollectionRefundState = '1'");
            sumfield.Append("                                         AND IsCheck = '0'");
            sumfield.Append("                                ) ,");
            sumfield.Append("                     Bill = ( SELECT ISNULL(SUM(BillAmount),0)");
            sumfield.Append("                              FROM   tbl_FinBill");
            sumfield.Append("                              WHERE  orderId = tbl_tourorder.orderid");
            sumfield.Append("                                     AND isdeleted = '0'");
            sumfield.Append("                                     AND IsApprove = '1'");
            sumfield.Append("                            )");
            sumfield.Append("                     ,Operator,OperatorId,ConfirmMoneyStatus,OrderId ");
            sumfield.Append("           FROM      dbo.tbl_TourOrder");
            sumfield.Append("         ) tbl_TourOrder");

            field.Append(" TourId,OrderId,OrderCode,Adults,Childs,Others,BuyCompanyId,BuyCompanyName,ContactName,ContactTel,SellerId,SellerName,Status,SaveSeatDate");
            field.Append(" ,TourType,Planers=STUFF((SELECT ',' + Planer FROM (SELECT Planer FROM tbl_TourPlaner WHERE TourId=tbl_TourOrder.TourId) AS A FOR XML PATH('')),1, 1,'')");
            field.Append(" ,RouteName=(select RouteName,tourcode from tbl_Tour where TourId=tbl_TourOrder.TourId for xml raw,root)");
            field.Append(" ,SumPrice,ConfirmMoneyStatus,UnChkRtn=(select isnull(sum(CollectionRefundAmount),0) from tbl_TourOrderSales where OrderId=tbl_tourorder.OrderId and CollectionRefundState='1' and IsCheck='0')");
            field.Append(" ,CheckMoney,ReceivedMoney-CheckMoney Unchecked,ConfirmMoney-CheckMoney+ReturnMoney Unreceived,ReturnMoney,ConfirmMoney");
            field.Append(" ,Bill=(select isnull(sum(BillAmount),0) from tbl_FinBill where orderId=tbl_tourorder.orderid and isdeleted='0' and IsApprove='1')");
            field.Append(" ,Operator,OperatorId ");
            field.Append(" ,(SELECT Department FROM tbl_CrmLinkman AS A WHERE A.Id=tbl_TourOrder.ContactDepartId) AS KeHuDeptName ");

            if (!string.IsNullOrEmpty(mSearch.OrderCode))
            {
                query.AppendFormat(" OrderCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.OrderCode));
            }              
            if (!string.IsNullOrEmpty(mSearch.CustomerId))
            {
                query.AppendFormat(" BuyCompanyId = '{0}' AND", mSearch.CustomerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Customer))
            {
                query.AppendFormat(" BuyCompanyName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Customer));
            }
            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
            {
                query.AppendFormat(" SellerId = '{0}' AND", mSearch.SalesmanId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Salesman))
            {
                query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Salesman));
            }
            if (mSearch.UnChecked.HasValue&&mSearch.SignUnChecked.HasValue)
            {
                query.AppendFormat(" (ReceivedMoney-CheckMoney) {0} {1} AND", GetEqualSign(mSearch.SignUnChecked), mSearch.UnChecked);
            }
            if (mSearch.UnReceived.HasValue&&mSearch.SignUnReceived.HasValue)
            {
                query.AppendFormat(" (ConfirmMoney-CheckMoney+ReturnMoney) {0} {1} AND", GetEqualSign(mSearch.SignUnReceived), mSearch.UnReceived);
            }
            // 订单是否已结清
            if (mSearch.IsClean.HasValue)
            {
                query.AppendFormat(" IsClean = '{0}' AND", mSearch.IsClean.Value ? "1" : "0");
            }
            query.Append(" EXISTS(SELECT 1 FROM tbl_Tour WHERE ");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.RouteName))
            {
                query.AppendFormat(" RouteName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.RouteName));
            }
            if (mSearch.SLDate.HasValue)
            {
                query.AppendFormat(" (LDate >= '{0}') AND", mSearch.SLDate);
            }
            if (mSearch.LLDate.HasValue)
            {
                query.AppendFormat(" (LDate <= '{0}') AND", mSearch.LLDate);
            }
            // 是否同业分销
            if (mSearch.IsShowDistribution.HasValue)
            {
                query.AppendFormat(" IsShowDistribution = '{0}' AND IsCheck = 1 AND (TourType = {1} OR TourType = {2} OR TourType = {3} OR TourType = {4}) AND", mSearch.IsShowDistribution.Value ? "1" : "0", (int)TourType.组团散拼, (int)TourType.地接散拼, (int)TourType.出境散拼,(int) TourType.组团散拼短线);
            }
            query.AppendFormat(" TourId=tbl_TourOrder.TourId AND IsDelete = '0' AND ISNULL(ParentId,'') <> '' AND (TourStatus < {0} OR TourStatus={1} OR TourStatus={2})) AND", (int)TourStatus.已取消, (int)TourStatus.资金超限, (int)TourStatus.垫付申请);
            query.AppendFormat(" (Status = {0} OR Status = {1} OR Status = {2}) AND IsDelete = '0' AND", (int)OrderStatus.已成交, (int)OrderStatus.资金超限, (int)OrderStatus.垫付申请审核);
            query.AppendFormat(" CompanyId = '{0}'", mSearch.CompanyId);
            // 是否同业分销
            if (!mSearch.IsShowDistribution.HasValue)
            {
                query.Append(GetReceivedOrg(operatorId, deptIds));
            }
            if (!string.IsNullOrEmpty(mSearch.OperatorId))
            {
                query.AppendFormat(" AND OperatorId='{0}' ", mSearch.OperatorId);
            }
            else if (!string.IsNullOrEmpty(mSearch.OperatorName))
            {
                query.AppendFormat(" AND Operator LIKE '%{0}%' ", mSearch.OperatorName);
            }
            if (mSearch.HeTongJinEQueRenStatus.HasValue)
            {
                query.AppendFormat(" AND ConfirmMoneyStatus='{0}' ", mSearch.HeTongJinEQueRenStatus.Value ? "1" : "0");
            }
            if (!string.IsNullOrEmpty(mSearch.ShouKuanRenId) || !string.IsNullOrEmpty(mSearch.ShluKuanRenName) || mSearch.ShouKuanETime.HasValue || mSearch.ShouKuanSTime.HasValue)
            {
                query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourOrderSales AS A1 WHERE A1.OrderId=tbl_TourOrder.OrderId ");

                if (!string.IsNullOrEmpty(mSearch.ShouKuanRenId))
                {
                    query.AppendFormat(" AND A1.CollectionRefundOperatorID='{0}' ", mSearch.ShouKuanRenId);
                }
                else if (!string.IsNullOrEmpty(mSearch.ShluKuanRenName))
                {
                    query.AppendFormat(" AND A1.CollectionRefundOperator LIKE '%{0}%' ", mSearch.ShluKuanRenName);
                }

                if (mSearch.ShouKuanETime.HasValue)
                {
                    query.AppendFormat(" AND A1.CollectionRefundDate<='{0}' ", mSearch.ShouKuanETime.Value);
                }
                if (mSearch.ShouKuanSTime.HasValue)
                {
                    query.AppendFormat(" AND A1.CollectionRefundDate>='{0}' ", mSearch.ShouKuanSTime.Value);
                }

                query.Append(")");
            }

            if (!string.IsNullOrEmpty(mSearch.JiDiaoYuanId))
            {
                query.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE tbl_TourPlaner.TourId=tbl_TourOrder.TourId AND tbl_TourPlaner.PlanerId='{0}') ", mSearch.JiDiaoYuanId);
            }
            else if (!string.IsNullOrEmpty(mSearch.JiDiaoYuanName))
            {
                query.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE tbl_TourPlaner.TourId=tbl_TourOrder.TourId AND tbl_TourPlaner.Planer LIKE '%{0}%') ", mSearch.JiDiaoYuanName);
            }

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , table, sumfield.ToString(), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MReceivableInfo
                    {
                        TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType")),
                        TourId = dr.GetString(dr.GetOrdinal("TourId")),
                        OrderId = dr.GetString(dr.GetOrdinal("OrderId")),
                        OrderCode = dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? "" : dr.GetString(dr.GetOrdinal("OrderCode")),
                        Status = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        Adults = dr.GetInt32(dr.GetOrdinal("Adults")),
                        Childs = dr.GetInt32(dr.GetOrdinal("Childs")),
                        Others = dr.GetInt32(dr.GetOrdinal("Others")),
                        CustomerId = dr.GetString(dr.GetOrdinal("BuyCompanyId")),
                        Customer = dr.GetString(dr.GetOrdinal("BuyCompanyName")),
                        Contact = dr.IsDBNull(dr.GetOrdinal("ContactName")) ? "" : dr.GetString(dr.GetOrdinal("ContactName")),
                        Phone = dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? "" : dr.GetString(dr.GetOrdinal("ContactTel")),
                        SalesmanId = dr["SellerId"].ToString(),
                        Salesman = dr.IsDBNull(dr.GetOrdinal("SellerName")) ? "" : dr.GetString(dr.GetOrdinal("SellerName")),
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["RouteName"].ToString(), "RouteName"),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr["RouteName"].ToString(), "tourcode"),
                        TotalAmount = dr.GetDecimal(dr.GetOrdinal("SumPrice")),
                        IsConfirmed = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1",
                        Received = dr.GetDecimal(dr.GetOrdinal("CheckMoney")),
                        Receivable = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")),
                        UnChkRtn = dr.GetDecimal(dr.GetOrdinal("UnChkRtn")),
                        UnChecked = dr.GetDecimal(dr.GetOrdinal("Unchecked")),
                        Planers = dr["Planers"].ToString(),
                        UnReceived = dr.GetDecimal(dr.GetOrdinal("Unreceived")),
                        Returned = dr.GetDecimal(dr.GetOrdinal("ReturnMoney")),
                        Bill = dr.GetDecimal(dr.GetOrdinal("Bill")),
                        SaveSeatDate = dr.IsDBNull(dr.GetOrdinal("SaveSeatDate"))?null:(DateTime?)dr.GetDateTime(dr.GetOrdinal("SaveSeatDate"))
                    };
                    mdl.OperatorName = dr["Operator"].ToString();
                    mdl.GroupOrderStatus = dTourOrder.GetGroupOrderStatus(mdl.Status, mdl.SaveSeatDate);
                    mdl.KeHuDeptName = dr["KeHuDeptName"].ToString();

                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref string xmlSum
                                                                , string companyId
                                                                , IList<TourType> tourTypes
                                                                , bool isShowDistribution
                                                                , MDayReceivablesChkBase search
                                                                , string operatorId
                                                                , params int[] deptIds)
        {
            const string table = "view_TodayReceivedIncome";
            var sumfield = new[] { "CollectionRefundAmount" };
            var lst = new List<MDayReceivablesChk>();
            var query = new StringBuilder();

            if (tourTypes != null && tourTypes.Count > 0)
            {
                query.Append(tourTypes.Aggregate(" TourType IN (", (current, tourType) => current + string.Format("{0},", tourTypes)).TrimEnd(',') + ") AND ");
            }
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.OrderCode))
                {
                    query.AppendFormat(" OrderCode LIKE '%{0}%' AND", Utils.ToSqlLike(search.OrderCode));
                }
                if (!string.IsNullOrEmpty(search.SalesmanId))
                {
                    query.AppendFormat(" SellerId = '{0}' AND", search.SalesmanId);
                }
                else if (!string.IsNullOrEmpty(search.Salesman))
                {
                    query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(search.Salesman));
                }
                if (!string.IsNullOrEmpty(search.CustomerId))
                {
                    query.AppendFormat(" BuyCompanyId = '{0}' AND", search.CustomerId);
                }
                else if (!string.IsNullOrEmpty(search.Customer))
                {
                    query.AppendFormat(" BuyCompanyName LIKE '%{0}%' AND", Utils.ToSqlLike(search.Customer));
                }
                if (search.IsShowInFin)
                {
                    query.Append(" IsCheck = '1' AND");
                }
            }
            if (isShowDistribution)
            {
                query.Append(" IsShowDistribution = '1' AND");
            }
            query.AppendFormat(" CompanyId = '{0}'", companyId);
            query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , table, this.CreateXmlSumByField(sumfield), "TourCode,RouteName,OrderCode,BuyCompanyName,Adults,Childs,SellerName,CollectionRefundAmount,Status"
                , query.ToString()
                , "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MDayReceivablesChk
                        {
                            TourCode = dr["TourCode"].ToString(),
                            RouteName =
                                dr.IsDBNull(dr.GetOrdinal("RouteName")) ? "" : dr.GetString(dr.GetOrdinal("RouteName")),
                            OrderCode =
                                dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? "" : dr.GetString(dr.GetOrdinal("OrderCode")),
                            Customer = dr.GetString(dr.GetOrdinal("BuyCompanyName")),
                            Adults = dr.GetInt32(dr.GetOrdinal("Adults")),
                            Childs = dr.GetInt32(dr.GetOrdinal("Childs")),
                            Salesman =
                                dr.IsDBNull(dr.GetOrdinal("SellerName"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("SellerName")),
                            ReceivableAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount")),
                            Status = dr.GetString(dr.GetOrdinal("Status"))
                        };
                    lst.Add(mdl);
                }
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
            var dc = _db.GetStoredProcCommand("proc_SetTourOrderSalesCheck");
            this._db.AddOutParameter(dc, "OrderId", DbType.AnsiStringFixedLength,36);
            this._db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, mdl.Id);
            this._db.AddInParameter(dc, "ApproverDeptId", DbType.Int32, mdl.ApproverDeptId);
            this._db.AddInParameter(dc, "ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "ApproveTime", DbType.DateTime, mdl.ApproveTime);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);

            this._db.AddOutParameter(dc, "IsJiFen", DbType.Int32, 4);
            this._db.AddOutParameter(dc, "OrderJinE", DbType.Decimal, 8);
            this._db.AddOutParameter(dc, "CrmId", DbType.AnsiStringFixedLength, 36);
            this._db.AddOutParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, 36);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return false;

            int retcode = Convert.ToInt32(_db.GetParameterValue(dc, "Result"));

            if (retcode != 1) return false;

            mdl.CompanyId = _db.GetParameterValue(dc, "CompanyId").ToString();
            mdl.OrderJinE = Convert.ToDecimal(_db.GetParameterValue(dc, "OrderJinE"));
            mdl.CrmId = _db.GetParameterValue(dc, "CrmId").ToString();
            mdl.IsJiFen = Convert.ToInt32(_db.GetParameterValue(dc, "IsJiFen"));
            mdl.OrderId = _db.GetParameterValue(dc, "OrderId").ToString();

            return true;
        }

        /// <summary>
        /// 根据团队编号获取导游实收列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MTourOrderSales> GetTourOrderSalesLstByTourId(string tourId)
        {
            var lst = new List<MTourOrderSales>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  Id ,");
            sql.Append("         OrderId");
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append(" WHERE   IsGuideRealIncome = 1");
            sql.Append("         AND CollectionRefundState = 0");
            sql.Append("         AND IsCheck = 0");
            sql.Append("         AND OrderId IN ( SELECT OrderId");
            sql.Append("                          FROM   dbo.tbl_TourOrder");
            sql.Append("                          WHERE  TourId = @TourId");
            sql.Append("                                 AND IsDelete = 0");
            sql.Append("                                 AND Status = @OrderStatus )");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@OrderStatus", DbType.Byte, (int)OrderStatus.已成交);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MTourOrderSales
                    {
                        Id = dr["Id"].ToString(),
                        OrderId = dr["OrderId"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        public IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds)
        {
            var lst = new List<MTourOrderSales>();

            string cmdText = string.Format("SELECT A.Id,A.CollectionRefundDate,A.CollectionRefundOperator,A.CollectionRefundAmount,A.CollectionRefundMode,A.OrderId,A.Memo,(SELECT [Name] FROM tbl_ComPayment AS B WHERE A.CollectionRefundMode=B.PaymentId) AS ZhiFuFangShiMingCheng,(SELECT A1.OrderCode FROM tbl_TourOrder AS A1 WHERE A1.OrderId=A.OrderId) AS OrderCode FROM tbl_TourOrderSales AS A WHERE A.OrderId IN ({0}) AND A.CollectionRefundState = @ShouTuiType AND A.IsCheck = '0' AND A.IsGuideRealIncome='0' AND A.T=0 ", Utils.GetSqlInExpression(orderIds));
            var cmd = _db.GetSqlStringCommand(cmdText);
            _db.AddInParameter(cmd, "ShouTuiType", DbType.AnsiStringFixedLength, (int)EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款);

            using (var dr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (dr.Read())
                {
                    lst.Add(new MTourOrderSales
                        {
                            Id = dr["Id"].ToString(),
                            CollectionRefundDate = dr.GetDateTime(dr.GetOrdinal("CollectionRefundDate")),
                            CollectionRefundOperator = dr["CollectionRefundOperator"].ToString(),
                            CollectionRefundAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount")),
                            CollectionRefundMode = dr.GetInt32(dr.GetOrdinal("CollectionRefundMode")),
                            Memo = dr["Memo"].ToString(),
                            OrderId = dr["OrderId"].ToString(),
                            CollectionRefundModeName = dr["ZhiFuFangShiMingCheng"].ToString(),
                            OrderCode = dr["OrderCode"].ToString()
                        });
                }
            }
            return lst;
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
            var strSql = new StringBuilder();

            if (mdl.Id == 0)
            {
                strSql.Append(" INSERT  INTO [tbl_FinBill]");
                strSql.Append("         ( [CompanyId] ,");
                strSql.Append("           [TourId] ,");
                strSql.Append("           [TourCode] ,");
                strSql.Append("           [OrderId] ,");
                strSql.Append("           [SellerId] ,");
                strSql.Append("           [SellerName] ,");
                strSql.Append("           [CustomerId] ,");
                strSql.Append("           [Customer] ,");
                strSql.Append("           [ContactName] ,");
                strSql.Append("           [ContactPhone] ,");
                strSql.Append("           [DealerId] ,");
                strSql.Append("           [Dealer] ,");
                strSql.Append("           [BillTime] ,");
                strSql.Append("           [BillAmount] ,");
                strSql.Append("           [BillNo] ,");
                strSql.Append("           [Remark] ,");
                strSql.Append("           [IsApprove] ,");
                strSql.Append("           [ApproverId] ,");
                strSql.Append("           [ApproveRemark] ,");
                strSql.Append("           [Approver] ,");
                strSql.Append("           [ApproveTime] ,");
                strSql.Append("           [DeptId] ,");
                strSql.Append("           [OperatorId] ,");
                strSql.Append("           [Operator] ,");
                strSql.Append("           [IssueTime]");
                strSql.Append("         )");
                strSql.Append(" VALUES  ( @CompanyId ,");
                strSql.Append("           @TourId ,");
                strSql.Append("           @TourCode ,");
                strSql.Append("           @OrderId ,");
                strSql.Append("           @SellerId ,");
                strSql.Append("           @SellerName ,");
                strSql.Append("           @CustomerId ,");
                strSql.Append("           @Customer ,");
                strSql.Append("           @ContactName ,");
                strSql.Append("           @ContactPhone ,");
                strSql.Append("           @DealerId ,");
                strSql.Append("           @Dealer ,");
                strSql.Append("           @BillTime ,");
                strSql.Append("           @BillAmount ,");
                strSql.Append("           @BillNo ,");
                strSql.Append("           @Remark ,");
                strSql.Append("           @IsApprove ,");
                strSql.Append("           @ApproverId ,");
                strSql.Append("           @ApproveRemark ,");
                strSql.Append("           @Approver ,");
                strSql.Append("           @ApproveTime ,");
                strSql.Append("           @DeptId ,");
                strSql.Append("           @OperatorId ,");
                strSql.Append("           @Operator ,");
                strSql.Append("           @IssueTime");
                strSql.Append("         )");
            }
            else
            {
                strSql.Append(" UPDATE  [tbl_FinBill]");
                strSql.Append(" SET");
                //strSql.Append("         [TourId] = @TourId ,");
                //strSql.Append("         [TourCode] = @TourCode ,");
                //strSql.Append("         [OrderId] = @OrderId ,");
                strSql.Append("         [SellerId] = @SellerId ,");
                strSql.Append("         [SellerName] = @SellerName ,");
                strSql.Append("         [CustomerId] = @CustomerId ,");
                strSql.Append("         [Customer] = @Customer ,");
                strSql.Append("         [ContactName] = @ContactName ,");
                strSql.Append("         [ContactPhone] = @ContactPhone ,");
                strSql.Append("         [DealerId] = @DealerId ,");
                strSql.Append("         [Dealer] = @Dealer ,");
                strSql.Append("         [BillTime] = @BillTime ,");
                strSql.Append("         [BillAmount] = @BillAmount ,");
                strSql.Append("         [BillNo] = @BillNo ,");
                strSql.Append("         [Remark] = @Remark,");
                strSql.Append("         [IsApprove] = @IsApprove ,");
                strSql.Append("         [ApproverId] = @ApproverId ,");
                strSql.Append("         [ApproveRemark] = @ApproveRemark ,");
                strSql.Append("         [Approver] = @Approver ,");
                strSql.Append("         [ApproveTime] = @ApproveTime");
                //strSql.Append("         [DeptId] = @DeptId ,");
                //strSql.Append("         [OperatorId] = @OperatorId ,");
                //strSql.Append("         [Operator] = @Operator ,");
                //strSql.Append("         [IssueTime] = @IssueTime");
                strSql.Append(" WHERE   Id = @Id AND [CompanyId] = @CompanyId");
            }

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(cmd, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(cmd, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(cmd, "@SellerId", DbType.AnsiStringFixedLength, mdl.SellerId);
            this._db.AddInParameter(cmd, "@SellerName", DbType.String, mdl.SellerName);
            this._db.AddInParameter(cmd, "@CustomerId", DbType.AnsiStringFixedLength, mdl.CustomerId);
            this._db.AddInParameter(cmd, "@Customer", DbType.String, mdl.Customer);
            this._db.AddInParameter(cmd, "@ContactName", DbType.String, mdl.ContactName);
            this._db.AddInParameter(cmd, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this._db.AddInParameter(cmd, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(cmd, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(cmd, "@BillTime", DbType.DateTime, mdl.BillTime);
            this._db.AddInParameter(cmd, "@BillAmount", DbType.Decimal, mdl.BillAmount);
            this._db.AddInParameter(cmd, "@BillNo", DbType.String, mdl.BillNo);
            this._db.AddInParameter(cmd, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(cmd, "@IsApprove", DbType.AnsiStringFixedLength, mdl.IsApprove ? "1" : "0");
            this._db.AddInParameter(cmd, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(cmd, "@ApproveRemark", DbType.String, mdl.ApproveRemark);
            this._db.AddInParameter(cmd, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(cmd, "@ApproveTime", DbType.DateTime, mdl.ApproveTime);
            this._db.AddInParameter(cmd, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(cmd, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }

        /// <summary>
        /// 根据系统公司编号和发票编号删除发票
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelBill(string companyId, int id)
        {
            var strSql = new StringBuilder("update tbl_finbill set isdeleted = '1' where id=@id and companyid=@companyid");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }

        /// <summary>
        /// 根据发票编号获取发票实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="id">发票编号</param>
        /// <returns>发票实体</returns>
        public MBill GetBillMdl(string companyId, int id)
        {
            var mdl = new MBill();
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            strSql.Append("         [TourCode] ,");
            strSql.Append("         [OrderId] ,");
            strSql.Append("         [SellerId] ,");
            strSql.Append("         [SellerName] ,");
            strSql.Append("         [CustomerId] ,");
            strSql.Append("         [Customer] ,");
            strSql.Append("         [ContactName] ,");
            strSql.Append("         [ContactPhone] ,");
            strSql.Append("         [DealerId] ,");
            strSql.Append("         [Dealer] ,");
            strSql.Append("         [BillTime] ,");
            strSql.Append("         [BillAmount] ,");
            strSql.Append("         [BillNo] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [IsApprove] ,");
            strSql.Append("         [ApproverId] ,");
            strSql.Append("         [ApproveRemark] ,");
            strSql.Append("         [Approver] ,");
            strSql.Append("         [ApproveTime] ,");
            strSql.Append("         [DeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime] ");
            strSql.Append(" FROM    [tbl_FinBill]");
            strSql.Append(" WHERE   Id = @Id");
            strSql.Append("         AND CompanyId = @CompanyId");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.TourCode = dr["TourCode"].ToString();
                    mdl.OrderId = dr["OrderId"].ToString();
                    mdl.SellerId = dr["SellerId"].ToString();
                    mdl.SellerName = dr["SellerName"].ToString();
                    mdl.CustomerId = dr["CustomerId"].ToString();
                    mdl.Customer = dr["Customer"].ToString();
                    mdl.ContactName = dr["ContactName"].ToString();
                    mdl.ContactPhone = dr["ContactPhone"].ToString();
                    mdl.DealerId = dr["DealerId"].ToString();
                    mdl.Dealer = dr["Dealer"].ToString();
                    mdl.BillTime = dr.GetDateTime(dr.GetOrdinal("BillTime"));
                    mdl.BillAmount = dr.GetDecimal(dr.GetOrdinal("BillAmount"));
                    mdl.BillNo = dr["BillNo"].ToString();
                    mdl.Remark = dr["Remark"].ToString();
                    mdl.IsApprove = dr.GetString(dr.GetOrdinal("IsApprove")) == "1";
                    mdl.ApproverId = dr["ApproverId"].ToString();
                    mdl.ApproveRemark = dr["ApproveRemark"].ToString();
                    mdl.Approver = dr["Approver"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ApproveTime")))
                    {
                        mdl.ApproveTime = dr.GetDateTime(dr.GetOrdinal("ApproveTime"));
                    }
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }

            return mdl;
        }

        /// <summary>
        /// 根据订单编号获取已开票信息集合
        /// </summary>
        /// <param name="orderId">订单编号或杂费收入编号</param>
        /// <returns></returns>
        public IList<MBill> GetBillLst(string orderId)
        {
            var lst = new List<MBill>();

            string cmdText = "SELECT  [Id],[TourId],[TourCode] ,[BillTime],[BillAmount],[BillNo],[Remark] ,[IsApprove]  FROM [tbl_FinBill] WHERE IsDeleted='0' AND OrderId=@OrderId ORDER BY BillTime DESC";
            var cmd = this._db.GetSqlStringCommand(cmdText);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MBill
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        TourId = dr["TourId"].ToString(),
                        TourCode = dr["TourCode"].ToString(),
                        BillTime = dr.GetDateTime(dr.GetOrdinal("BillTime")),
                        BillAmount = dr.GetDecimal(dr.GetOrdinal("BillAmount")),
                        BillNo = dr["BillNo"].ToString(),
                        Remark = dr["Remark"].ToString(),
                        IsApprove = dr.GetString(dr.GetOrdinal("IsApprove")) == "1"
                    });
                }
            }

            return lst;
        }

        /// <summary>
        /// 根据选中订单的订单编号获取批量开票列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>批量开票列表</returns>
        public IList<MBill> GetBillLst(List<string> orderIds)
        {
            var lst = new List<MBill>();
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  OrderCode ,OrderId,TourId,(select tourcode from tbl_tour where tourid=tbl_TourOrder.tourid) TourCode,ContactName,ContactTel,SellerId,SellerName,");
            strSql.Append("         BuyCompanyId ,");
            strSql.Append("         BuyCompanyName ,");
            strSql.Append("         GETDATE() BillTime ,");
            strSql.Append("         UnBill = ConfirmMoney");
            strSql.Append("         - ( SELECT  ISNULL(SUM(BillAmount),0)");
            strSql.Append("             FROM    dbo.tbl_FinBill");
            strSql.Append("             WHERE   OrderId = tbl_TourOrder.OrderId");
            strSql.Append("                     AND IsDeleted = '0'");
            strSql.Append("           )");
            strSql.Append(" FROM    tbl_TourOrder");
            strSql.AppendFormat(" WHERE   OrderId IN ( {0})", Utils.GetSqlInExpression(orderIds));
            strSql.Append(" AND ConfirmMoney > ( SELECT ISNULL(SUM(BillAmount),0)");
            strSql.Append("                      FROM   dbo.tbl_FinBill");
            strSql.Append("                      WHERE  OrderId = tbl_TourOrder.OrderId");
            strSql.Append("                             AND IsDeleted = '0'");
            strSql.Append("                    )");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(
                        new MBill
                        {
                            TourId = dr["TourId"].ToString(),
                            TourCode = dr["TourCode"].ToString(),
                            SellerId = dr["SellerId"].ToString(),
                            SellerName = dr["SellerName"].ToString(),
                            ContactName = dr["ContactName"].ToString(),
                            ContactPhone = dr["ContactTel"].ToString(),
                            OrderId = dr["OrderId"].ToString(),
                            OrderCode = dr["OrderCode"].ToString(),
                            CustomerId = dr["BuyCompanyId"].ToString(),
                            Customer = dr["BuyCompanyName"].ToString(),
                            BillTime = dr.GetDateTime(dr.GetOrdinal("BillTime")),
                            BillAmount = dr.GetDecimal(dr.GetOrdinal("UnBill")),
                        });
                }
            }

            return lst;
        }

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
        public IList<MBill> GetBillLst(int pageSize, int pageIndex, ref int recordCount, string companyId, MBill mSearch, string operatorId, params int[] deptIds)
        {
            var lst = new List<MBill>();
            var query = new StringBuilder();

            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.TourCode))
                {
                    query.AppendFormat(" TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(mSearch.CustomerId))
                {
                    query.AppendFormat(" CustomerId = '{0}' AND", mSearch.CustomerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Customer))
                {
                    query.AppendFormat(" Customer LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Customer));
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" SellerId = '{0}' AND", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (mSearch.BillAmount.HasValue)
                {
                    query.AppendFormat(" BillAmount = {0} AND", mSearch.BillAmount);
                }
                if (!string.IsNullOrEmpty(mSearch.BillTimeS))
                {
                    query.AppendFormat(" BillTime >= '{0}' AND", mSearch.BillTimeS);
                }
                if (!string.IsNullOrEmpty(mSearch.BillTimeE))
                {
                    query.AppendFormat(" BillTime <= '{0}' AND", mSearch.BillTimeE);
                }
            }
            query.AppendFormat(" CompanyId = '{0}' AND IsDeleted = '0'", companyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, "tbl_FinBill", "Id", "Id,TourCode,CustomerId,Customer,ContactName,ContactPhone,SellerName,BillAmount,BillTime,IsApprove,Remark,BillNo", query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MBill
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        TourCode = dr["TourCode"].ToString(),
                        CustomerId = dr["CustomerId"].ToString(),
                        Customer = dr["Customer"].ToString(),
                        ContactName = dr["ContactName"].ToString(),
                        ContactPhone = dr["ContactPhone"].ToString(),
                        SellerName = dr["SellerName"].ToString(),
                        BillTime = dr.GetDateTime(dr.GetOrdinal("BillTime")),
                        BillAmount = dr.GetDecimal(dr.GetOrdinal("BillAmount")),
                        IsApprove = dr.GetString(dr.GetOrdinal("IsApprove")) == "1",
                        Remark = dr["Remark"].ToString(),
                        BillNo = dr["BillNo"].ToString()
                    });
                }
            }
            return lst;
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
        public bool SetApproveBill(string companyId, int id, bool isApprove, string approverId, string approver, string approveRemark, DateTime? approveTime, string billNo)
        {
            var strSql = new StringBuilder();

            strSql.Append(" update tbl_finbill set");
            strSql.Append(" isapprove=@isapprove,approverId=@approverid,approver=@approver");
            strSql.Append(" ,approveremark=@approveremark,approvetime=@approvetime,billno=@billno");
            strSql.Append(" where id=@id and companyid=@companyid");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(cmd, "@isApprove", DbType.AnsiStringFixedLength, isApprove ? "1" : "0");
            this._db.AddInParameter(cmd, "@approverId", DbType.AnsiStringFixedLength, approverId);
            this._db.AddInParameter(cmd, "@approver", DbType.String, approver);
            this._db.AddInParameter(cmd, "@approveRemark", DbType.String, approveRemark);
            this._db.AddInParameter(cmd, "@approveTime", DbType.DateTime, approveTime);
            this._db.AddInParameter(cmd, "@billno", DbType.String, billNo);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
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
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" INSERT  INTO [{0}]", table);
            strSql.Append("         ( [CompanyId] ,");
            strSql.Append("           [TourId] ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("           [AuditDeptId] ,");
                strSql.Append("           [TourCode] ,");
                strSql.Append("           [IsGuide] ,");
            }
            strSql.Append("           [DealTime] ,");
            strSql.Append("           [FeeItem] ,");
            strSql.Append("           [PlanId] ,");
            strSql.Append("           [CrmId] ,");
            strSql.Append("           [Crm] ,");
            strSql.Append("           [ContactName] ,");
            strSql.Append("           [ContactPhone] ,");
            strSql.Append("           [DealerId] ,");
            strSql.Append("           [Dealer] ,");
            strSql.Append("           [FeeAmount] ,");
            strSql.Append("           [Remark] ,");
            strSql.Append("           [PayType] ,");
            strSql.Append("           [PayTypeName] ,");
            if (typ == ItemType.支出)
            {
                //strSql.Append("           [AccountantDeptId] ,");
                //strSql.Append("           [AccountantId] ,");
                //strSql.Append("           [Accountant] ,");
                //strSql.Append("           [PayTime] ,");
                strSql.Append("           [SellerId] ,");
                strSql.Append("           [Seller] ,");
            }
            strSql.Append("           [Status] ,");
            strSql.Append("           [AuditId] ,");
            strSql.Append("           [Audit] ,");
            strSql.Append("           [AuditRemark] ,");
            strSql.Append("           [AuditTime] ,");
            strSql.Append("           [DeptId] ,");
            strSql.Append("           [OperatorId] ,");
            strSql.Append("           [Operator] ,");
            strSql.Append("           [IssueTime]");
            strSql.Append("         )");
            strSql.Append(" VALUES  ( @CompanyId ,");
            strSql.Append("           @TourId ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("           @AuditDeptId ,");
                strSql.Append("           @TourCode ,");
                strSql.Append("           @IsGuide ,");
            }
            strSql.Append("           @DealTime ,");
            strSql.Append("           @FeeItem ,");
            strSql.Append("           @PlanId ,");
            strSql.Append("           @CrmId ,");
            strSql.Append("           @Crm ,");
            strSql.Append("           @ContactName ,");
            strSql.Append("           @ContactPhone ,");
            strSql.Append("           @DealerId ,");
            strSql.Append("           @Dealer ,");
            strSql.Append("           @FeeAmount ,");
            strSql.Append("           @Remark ,");
            strSql.Append("           @PayType ,");
            strSql.Append("           @PayTypeName ,");
            if (typ == ItemType.支出)
            {
                //strSql.Append("           @AccountantDeptId ,");
                //strSql.Append("           @AccountantId ,");
                //strSql.Append("           @Accountant ,");
                //strSql.Append("           @PayTime ,");
                strSql.Append("           @SellerId ,");
                strSql.Append("           @Seller ,");
            }
            strSql.Append("           @Status ,");
            strSql.Append("           @AuditId ,");
            strSql.Append("           @Audit ,");
            strSql.Append("           @AuditRemark ,");
            strSql.Append("           @AuditTime ,");
            strSql.Append("           @DeptId ,");
            strSql.Append("           @OperatorId ,");
            strSql.Append("           @Operator ,");
            strSql.Append("           @IssueTime");
            strSql.Append("         )");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@DealTime", DbType.DateTime, mdl.DealTime);
            this._db.AddInParameter(dc, "@FeeItem", DbType.String, mdl.FeeItem);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@CrmId", DbType.AnsiStringFixedLength, mdl.CrmId);
            this._db.AddInParameter(dc, "@Crm", DbType.String, mdl.Crm);
            this._db.AddInParameter(dc, "@ContactName", DbType.String, mdl.ContactName);
            this._db.AddInParameter(dc, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@FeeAmount", DbType.Decimal, mdl.FeeAmount);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@PayType", DbType.Byte, mdl.PayType);
            this._db.AddInParameter(dc, "@PayTypeName", DbType.String, mdl.PayTypeName);
            //this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, mdl.AccountantDeptId);
            //this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, mdl.AccountantId);
            //this._db.AddInParameter(dc, "@Accountant", DbType.String, mdl.Accountant);
            //this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, mdl.PayTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@AuditDeptId", DbType.Int32, mdl.AuditDeptId);
            this._db.AddInParameter(dc, "@AuditId", DbType.AnsiStringFixedLength, mdl.AuditId);
            this._db.AddInParameter(dc, "@Audit", DbType.String, mdl.Audit);
            this._db.AddInParameter(dc, "@AuditRemark", DbType.String, mdl.AuditRemark);
            this._db.AddInParameter(dc, "@AuditTime", DbType.DateTime, mdl.AuditTime);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@SellerId", DbType.AnsiStringFixedLength, mdl.SellerId);
            this._db.AddInParameter(dc, "@Seller", DbType.String, mdl.Seller);
            this._db.AddInParameter(dc, "@IsGuide", DbType.Byte, (int)mdl.IsGuide);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE  [{0}]", table);
            strSql.Append(" SET");
            strSql.Append("         [DealTime] = @DealTime ,");
            strSql.Append("         [FeeItem] = @FeeItem ,");
            strSql.Append("         [PlanId] = @PlanId ,");
            strSql.Append("         [CrmId] = @CrmId ,");
            strSql.Append("         [Crm] = @Crm ,");
            strSql.Append("         [ContactName] = @ContactName ,");
            strSql.Append("         [ContactPhone] = @ContactPhone ,");
            strSql.Append("         [DealerId] = @DealerId ,");
            strSql.Append("         [Dealer] = @Dealer ,");
            strSql.Append("         [FeeAmount] = @FeeAmount ,");
            strSql.Append("         [Remark] = @Remark ,");
            strSql.Append("         [PayType] = @PayType ,");
            strSql.Append("         [PayTypeName] = @PayTypeName ");
            if (typ == ItemType.支出)
            {
                strSql.Append("         ,[SellerId] = @SellerId");
                strSql.Append("         ,[Seller] = @Seller");
            }
            strSql.Append(" WHERE   [Id] = @Id AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.AnsiStringFixedLength, mdl.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@DealTime", DbType.DateTime, mdl.DealTime);
            this._db.AddInParameter(dc, "@FeeItem", DbType.String, mdl.FeeItem);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@CrmId", DbType.AnsiStringFixedLength, mdl.CrmId);
            this._db.AddInParameter(dc, "@Crm", DbType.String, mdl.Crm);
            this._db.AddInParameter(dc, "@ContactName", DbType.String, mdl.ContactName);
            this._db.AddInParameter(dc, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@FeeAmount", DbType.Decimal, mdl.FeeAmount);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@PayType", DbType.Byte, mdl.PayType);
            this._db.AddInParameter(dc, "@PayTypeName", DbType.String, mdl.PayTypeName);
            this._db.AddInParameter(dc, "@SellerId", DbType.AnsiStringFixedLength, mdl.SellerId);
            this._db.AddInParameter(dc, "@Seller", DbType.String, mdl.Seller);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int DelOtherFeeInOut(string companyId, ItemType typ, params int[] ids)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE {0}", table);
            strSql.Append(" SET IsDeleted='1'");
            strSql.Append(" WHERE");
            strSql.Append("     Id IN (" + Utils.GetSqlIdStrByArray(ids) + ")");
            strSql.AppendFormat("     AND CompanyId = '{0}'", companyId);

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        public MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id, string companyId)
        {
            var strSql = new StringBuilder();
            var mdl = new MOtherFeeInOut();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("         [AuditDeptId] ,");
                strSql.Append("         [TourCode] ,");
            }
            strSql.Append("         [DealTime] ,");
            strSql.Append("         [FeeItem] ,");
            strSql.Append("         [PlanId] ,");
            strSql.Append("         [CrmId] ,");
            strSql.Append("         [Crm] ,");
            strSql.Append("         [ContactName] ,");
            strSql.Append("         [ContactPhone] ,");
            strSql.Append("         [DealerId] ,");
            strSql.Append("         [Dealer] ,");
            strSql.Append("         [FeeAmount] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [PayType] ,");
            strSql.Append("         [PayTypeName] ,");
            if (typ == ItemType.支出)
            {
                strSql.Append("         [AccountantDeptId] ,");
                strSql.Append("         [AccountantId] ,");
                strSql.Append("         [Accountant] ,");
                strSql.Append("         [PayTime] ,");
                strSql.Append("         [SellerId] ,");
                strSql.Append("         [Seller] ,");
            }
            strSql.Append("         [Status] ,");
            strSql.Append("         [AuditId] ,");
            strSql.Append("         [Audit] ,");
            strSql.Append("         [AuditRemark] ,");
            strSql.Append("         [AuditTime] ,");
            strSql.Append("         [DeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime]");
            strSql.AppendFormat(" FROM    [{0}]", table);
            strSql.Append(" WHERE   Id = @Id AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.TourId = dr.IsDBNull(dr.GetOrdinal("TourId")) ? "" : dr.GetString(dr.GetOrdinal("TourId"));
                    if (typ == ItemType.收入)
                    {
                        mdl.AuditDeptId = dr.GetInt32(dr.GetOrdinal("AuditDeptId"));
                        mdl.TourCode = dr.IsDBNull(dr.GetOrdinal("TourCode")) ? "" : dr.GetString(dr.GetOrdinal("TourCode"));
                    }
                    mdl.DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime"));
                    mdl.FeeItem = dr.IsDBNull(dr.GetOrdinal("FeeItem")) ? "" : dr.GetString(dr.GetOrdinal("FeeItem"));
                    mdl.PlanId = dr.IsDBNull(dr.GetOrdinal("PlanId")) ? "" : dr.GetString(dr.GetOrdinal("PlanId"));
                    mdl.CrmId = dr.IsDBNull(dr.GetOrdinal("CrmId")) ? "" : dr.GetString(dr.GetOrdinal("CrmId"));
                    mdl.Crm = dr.IsDBNull(dr.GetOrdinal("Crm")) ? "" : dr.GetString(dr.GetOrdinal("Crm"));
                    mdl.ContactName = dr["ContactName"].ToString();
                    mdl.ContactPhone = dr["ContactPhone"].ToString();
                    mdl.DealerId = dr["DealerId"].ToString();
                    mdl.Dealer = dr.IsDBNull(dr.GetOrdinal("Dealer")) ? "" : dr.GetString(dr.GetOrdinal("Dealer"));
                    mdl.FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount"));
                    mdl.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    mdl.PayType = dr.GetInt32(dr.GetOrdinal("PayType"));
                    mdl.PayTypeName = dr["PayTypeName"].ToString();
                    if (typ == ItemType.支出)
                    {
                        mdl.AccountantDeptId = dr.GetInt32(dr.GetOrdinal("AccountantDeptId"));
                        mdl.AccountantId = dr["AccountantId"].ToString();
                        mdl.Accountant = dr["Accountant"].ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("PayTime")))
                        {
                            mdl.PayTime = dr.GetDateTime(dr.GetOrdinal("PayTime"));
                        }
                        mdl.SellerId = dr["SellerId"].ToString();
                        mdl.Seller = dr["Seller"].ToString();
                    }
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.AuditId = dr["AuditId"].ToString();
                    mdl.Audit = dr["Audit"].ToString();
                    mdl.AuditRemark = dr["AuditRemark"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("AuditTime")))
                    {
                        mdl.AuditTime = dr.GetDateTime(dr.GetOrdinal("AuditTime"));
                    }
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.Operator = dr.IsDBNull(dr.GetOrdinal("Operator")) ? "" : dr.GetString(dr.GetOrdinal("Operator"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
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
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE {0}", table);
            strSql.Append(" SET Status=@Status");
            if (typ == ItemType.收入)
            {
                strSql.Append("     ,AuditDeptId=@AuditDeptId");
            }
            strSql.Append("     ,AuditId=@AuditId");
            strSql.Append("     ,Audit=@Audit");
            strSql.Append("     ,AuditRemark=@AuditRemark");
            strSql.Append("     ,AuditTime=@AuditTime");
            strSql.Append(" WHERE");
            strSql.Append("     Id IN (" + Utils.GetSqlIdStrByArray(ids) + ")");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@AuditDeptId", DbType.Int32, auditDeptId);
            this._db.AddInParameter(dc, "@AuditId", DbType.AnsiStringFixedLength, auditId);
            this._db.AddInParameter(dc, "@Audit", DbType.String, audit);
            this._db.AddInParameter(dc, "@AuditRemark", DbType.String, auditRemark);
            this._db.AddInParameter(dc, "@AuditTime", DbType.DateTime, auditTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, status);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据团队编号获取导游报账时添加的其他收入列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MOtherFeeInOut> GetOtherFeeInLst(string tourId)
        {
            var lst = new List<MOtherFeeInOut>();
            var sql = new StringBuilder();

            sql.Append("select Id from tbl_FinOtherInFee where TourId=@TourId and IsGuide<>0 and Status=@Status and IsDeleted=0");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(cmd, "@Status", DbType.Byte, (int)FinStatus.财务待审批);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MOtherFeeInOut
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                    }
                    );
                }
            }
            return lst;
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
            var strSql = new StringBuilder();

            foreach (var m in lst)
            {
                strSql.Append(" UPDATE tbl_FinOtherOutFee");
                strSql.AppendFormat(" SET Status=@Status");
                strSql.Append("     ,AccountantDeptId=@AccountantDeptId");
                strSql.Append("     ,AccountantId=@AccountantId");
                strSql.Append("     ,Accountant=@Accountant");
                strSql.Append("     ,PayTime=@PayTime");
                strSql.AppendFormat("     ,PayType={0}", m.PaymentType);
                strSql.AppendFormat("     ,PayTypeName='{0}'", m.PaymentTypeName);
                strSql.Append(" WHERE");
                strSql.AppendFormat("     Id = {0}", m.RegisterId);
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, accountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, accountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, payTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, status);

            return DbHelper.ExecuteSql(dc, this._db);
        }

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
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ItemType typ
                                                        , MOtherFeeInOutBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            var lst = new List<MOtherFeeInOut>();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append("Id,TourId,DealTime,FeeItem,CrmId,Crm,DealerId,Dealer,OperatorId,Operator,FeeAmount,PayType,PayTypeName,Remark,Status,ContactName,ContactPhone");
            field.Append(typ == ItemType.收入 ? ",TourCode,Bill=(select isnull(sum(BillAmount),0) from tbl_finbill where ISNUMERIC(OrderId)=1 and OrderId=tbl_FinOtherInFee.Id and CustomerId=tbl_FinOtherInFee.CrmId and IsDeleted='0')" : ",Seller");

            if (!string.IsNullOrEmpty(mSearch.FeeItem))
            {
                query.Append("FeeItem LIKE '%" + Utils.ToSqlLike(mSearch.FeeItem) + "%' AND ");
            }
            if (!string.IsNullOrEmpty(mSearch.CrmId))
            {
                query.AppendFormat("CrmId = '{0}' AND ", mSearch.CrmId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Crm))
            {
                query.AppendFormat("Crm LIKE '%{0}%' AND ", Utils.ToSqlLike(mSearch.Crm));
            }
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat("DealerId = '{0}' AND ", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat("Dealer LIKE '%{0}%' AND ", Utils.ToSqlLike(mSearch.Dealer));
            }
            if (mSearch.DealTimeS.HasValue)
            {
                query.AppendFormat("DealTime >= '{0}' AND ", mSearch.DealTimeS.Value);
            }
            if (mSearch.DealTimeE.HasValue)
            {
                query.AppendFormat("DealTime < '{0}' AND ", mSearch.DealTimeE.Value.AddDays(1));
            }
            if ((int)mSearch.Status!=-1)
            {
                query.AppendFormat("Status = {0} AND ",(int)mSearch.Status);
            }
            if (typ == ItemType.收入)
            {
                query.AppendFormat("(IsGuide = 0 OR EXISTS(SELECT 1 FROM tbl_Tour WHERE TourId = tbl_FinOtherInFee.TourId AND IsDelete=0 AND ISNULL(ParentId,'')<>'' AND TourStatus<{0} AND IsSubmit=1)) AND ", (int)TourStatus.已取消);
            }
            query.AppendFormat(" IsDeleted = '0' AND CompanyId = '{0}'", mSearch.CompanyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, table, "Id", field.ToString(), query.ToString(), "DealTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MOtherFeeInOut
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            TourId = dr["TourId"].ToString(),
                            TourCode = typ == ItemType.收入 ? dr["TourCode"].ToString() : "",
                            DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime")),
                            FeeItem = dr["FeeItem"].ToString(),
                            CrmId = dr["CrmId"].ToString(),
                            Crm = dr["Crm"].ToString(),
                            ContactName = dr["ContactName"].ToString(),
                            ContactPhone = dr["ContactPhone"].ToString(),
                            DealerId = dr["DealerId"].ToString(),
                            Dealer = dr["Dealer"].ToString(),
                            FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                            Remark = dr["Remark"].ToString(),
                            PayType = dr.GetInt32(dr.GetOrdinal("PayType")),
                            PayTypeName = dr["PayTypeName"].ToString(),
                            Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                            OperatorId = dr["OperatorId"].ToString(),
                            Operator = dr.GetString(dr.GetOrdinal("Operator")),
                            Seller = typ == ItemType.支出 ? dr["Seller"].ToString() : ""
                        };
                    if (typ == ItemType.收入)
                    {
                        mdl.Bill = dr.GetDecimal(dr.GetOrdinal("Bill"));
                    }
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids)
        {
            var lst = new List<MOtherFeeInOut>();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";
            var sql = new StringBuilder();

            sql.AppendFormat("select Id,DealTime,FeeItem,CrmId,Crm,Dealer,FeeAmount,PayType,PayTypeName,Remark,Status,AuditTime,Audit,AuditRemark from {0}", table);
            sql.AppendFormat(" where Id IN ({0})", Utils.GetSqlIdStrByList(ids));

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MOtherFeeInOut
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime")),
                        FeeItem = dr["FeeItem"].ToString(),
                        CrmId = dr["CrmId"].ToString(),
                        Crm = dr["Crm"].ToString(),
                        Dealer = dr["Dealer"].ToString(),
                        FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                        Remark = dr["Remark"].ToString(),
                        PayType = dr.GetInt32(dr.GetOrdinal("PayType")),
                        PayTypeName = dr["PayTypeName"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        Audit = dr["Audit"].ToString(),
                        AuditRemark = dr["AuditRemark"].ToString()
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("AuditTime")))
                    {
                        mdl.AuditTime = dr.GetDateTime(dr.GetOrdinal("AuditTime"));
                    }
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MPayable> GetPayableLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayableBase mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            const string table = "tbl_Plan";
            var unChecked = string.Format("UnChecked=(select isnull(sum(PaymentAmount),0) from tbl_finregister where planid=tbl_plan.planid and isdeleted='0' and status!={0})", (int)FinStatus.账务已支付);
            var sumfield = new StringBuilder();
            var lst = new List<MPayable>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            sumfield.Append(" SELECT  ISNULL(SUM(Confirmation), 0) Confirmation ,");
            sumfield.Append("         ISNULL(SUM(Prepaid), 0) Prepaid ,");
            sumfield.Append("         ISNULL(SUM(UnChecked), 0) UnChecked");
            sumfield.Append(" FROM    ( SELECT    Confirmation ,");
            sumfield.Append("                     TourId ,");
            sumfield.Append("                     PlanId ,");
            sumfield.Append("                     SourceId ,");
            sumfield.Append("                     SourceName ,");
            sumfield.Append("                     OperatorId ,");
            sumfield.Append("                     OperatorName ,");
            sumfield.Append("                     CostStatus ,IsDelete,");
            sumfield.Append("                     [Type] ,");
            sumfield.Append("                     Status ,");
            sumfield.Append("                     CompanyId ,");
            sumfield.Append("                     Prepaid ,");
            sumfield.AppendFormat("                     {0}", unChecked);
            sumfield.Append("           FROM      dbo.tbl_Plan");
            sumfield.Append("         ) tbl_plan");

            field.Append(" 	[Type]");
            field.Append(" 	,XMLTour=(SELECT T.TourId,T.TourCode,T.RouteName,CONVERT(VARCHAR(19),T.LDate,120) LDate,T.SellerName,U.ContactTel,U.ContactFax,U.ContactMobile,U.QQ,T.TourStatus");
            field.Append(" 			  FROM tbl_Tour T LEFT JOIN tbl_ComUser U ON T.SellerId=U.UserId");
            field.Append(" 			  WHERE T.TourId=tbl_Plan.TourId FOR XML RAW,ROOT)");
            field.Append(" 	,SourceName");
            field.Append(" 	,Num");
            field.Append(" 	,OperatorName");
            field.Append(" 	,CostStatus,CostRemarks");
            field.Append(" 	,CostDetail");
            field.Append(" 	,PaymentType");
            field.Append(" 	,Confirmation");
            field.Append(" 	,Prepaid");
            field.Append(" 	,PlanId");
            field.AppendFormat("  ,{0}", unChecked);

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour ");
            query.Append(" 		   WHERE");
            //线路区域编号
            if (mSearch.AreaId > 0)
            {
                query.AppendFormat(" 				AreaId = {0} AND", mSearch.AreaId);
            }
            ////供应商编号
            //if (!string.IsNullOrEmpty(mSearch.SupplierId))
            //{
            //    query.AppendFormat(" 				SourceId = '{0}' AND", mSearch.SupplierId);
            //}
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
            {
                query.AppendFormat(" 				SellerId = '{0}' AND", mSearch.SalesmanId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Salesman))
            {
                query.AppendFormat(" 				SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Salesman));
            }
            if (!string.IsNullOrEmpty(mSearch.LDateStart))
            {
                query.AppendFormat(" 				LDate >= '{0}' AND", mSearch.LDateStart);
            }
            if (!string.IsNullOrEmpty(mSearch.LDateEnd))
            {
                query.AppendFormat(" 				LDate <= '{0}' AND", mSearch.LDateEnd);
            }
            query.Append(" 				IsDelete='0' AND");
            query.AppendFormat(" 				TourId=tbl_Plan.TourId AND ISNULL(ParentId,'') <> '' AND TourStatus <> {0}) AND", (int)TourStatus.已取消);
            if (!string.IsNullOrEmpty(mSearch.PaymentDateS) || !string.IsNullOrEmpty(mSearch.PaymentDateE))
            {
                query.Append(" 	EXISTS(SELECT 1 FROM tbl_FinRegister");
                query.Append(" 		   WHERE	");
                if (!string.IsNullOrEmpty(mSearch.PaymentDateS))
                {
                    query.AppendFormat(" 				PaymentDate >= '{0}' AND", mSearch.PaymentDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.PaymentDateE))
                {
                    query.AppendFormat(" 				PaymentDate < '{0}' AND", Utils.GetDateTime(mSearch.PaymentDateE).AddDays(1));
                }
                query.Append(" 				PlanId=tbl_Plan.PlanId) AND");
            }
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 	SourceId = '{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 	SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            if (!string.IsNullOrEmpty(mSearch.PlanerId))
            {
                query.AppendFormat(" 	OperatorId = '{0}' AND", mSearch.PlanerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Planer))
            {
                query.AppendFormat(" 	OperatorName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Planer));
            }
            if (mSearch.Paid.HasValue)
            {
                query.AppendFormat(" 	Prepaid {0} {1} AND", GetEqualSign(mSearch.SignPaid), mSearch.Paid.Value);
            }
            if ( mSearch.Unpaid.HasValue)
            {
                query.AppendFormat(" 	Confirmation-Prepaid {0} {1} AND", GetEqualSign(mSearch.SignUnpaid), mSearch.Unpaid.Value);
            }
            if (mSearch.IsClean)
            {
                query.Append(" 	Confirmation = Prepaid AND");
            }
            if (mSearch.IsConfirmed.HasValue)
            {
                query.AppendFormat(" 	CostStatus = '{0}' AND", mSearch.IsConfirmed.Value ? "1" : "0");
            }
            if (mSearch.PlanItem.HasValue)
            {
                query.AppendFormat(" 	[Type]={0} AND", (int)mSearch.PlanItem.Value);
            }
            query.AppendFormat(" IsDelete = 0 AND Status={0} AND CompanyId='{1}' AND [Type]<>{2}", (int)PlanState.已落实, mSearch.CompanyId,(int)PlanProject.购物);
            //供应商平台判断
            if (!mSearch.IsDj)
            {
                query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_Plan.TourId");
                query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
                query.Append(")");
            }

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, sumfield.ToString(), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MPayable
                    {
                        TourId = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourId"),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode"),
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "RouteName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "LDate")),
                        PlanItem = (PlanProject?)dr.GetByte(dr.GetOrdinal("Type")),
                        Supplier = dr["SourceName"].ToString(),
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        Salesman = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SellerName"),
                        Tel = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactTel"),
                        Mobile = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactMobile"),
                        Fax = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactFax"),
                        QQ = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "QQ"),
                        TourStatus = (TourStatus)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourStatus")),
                        Planer = dr["OperatorName"].ToString(),
                        IsConfirmed = dr.GetString(dr.GetOrdinal("CostStatus")) == "1",
                        CostRemarks=dr["CostRemarks"].ToString(),
                        Payable = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                        Paid = dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        Unpaid = dr.GetDecimal(dr.GetOrdinal("Confirmation")) - dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        PlanId = dr["PlanId"].ToString(),
                        UnChecked = dr.GetDecimal(dr.GetOrdinal("UnChecked")),
                        CostDetail = dr["CostDetail"].ToString(),
                        PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayRegister mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            const string table = "tbl_FinRegister";
            var lst = new List<MRegister>();
            var filed = new StringBuilder();
            var query = new StringBuilder();
            var sumfield = new[] { "PaymentAmount" };

            filed.Append(" 	XMLTour=(SELECT TourCode,SellerName FROM tbl_Tour WHERE TourId=tbl_FinRegister.TourId and ParentId<>'' FOR XML RAW,ROOT)");
            filed.Append(" 	,XMLPlan=(SELECT [Type],SourceName,OperatorName FROM tbl_Plan WHERE PlanId=tbl_FinRegister.PlanId FOR XML RAW,ROOT)");
            filed.Append(" 	,Dealer");
            filed.Append(" 	,PaymentAmount");
            filed.Append(" 	,PaymentType");
            filed.Append(" 	,PaymentName=(SELECT Name FROM tbl_ComPayment WHERE PaymentId=PaymentType)");

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour ");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            query.Append(" 				TourId=tbl_FinRegister.TourId and ParentId<>'') AND");
            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Plan ");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 		   SourceId='{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 		   SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            query.Append(" 		   PlanId=tbl_FinRegister.PlanId) AND");
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat(" 	DealerId='{0}' AND", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat(" 	Dealer LIKE '%{0}' AND", Utils.ToSqlLike(mSearch.Dealer));
            }
            query.AppendFormat(" 	Status={0} AND CONVERT(VARCHAR(10),PaymentDate,120)=CONVERT(VARCHAR(10),GETDATE(),120) AND", (int)FinStatus.账务已支付);
            query.AppendFormat(" 	CompanyId='{0}'", mSearch.CompanyId);
            query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_FinRegister.TourId");
            query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
            query.Append(")");

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, CreateXmlSumByField(sumfield), filed.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MRegister
                        {
                            PlanTyp = (PlanProject)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "Type")),
                            TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode"),
                            Supplier = Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "SourceName"),
                            Salesman = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SellerName"),
                            Planer = Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "OperatorName"),
                            Dealer = dr["Dealer"].ToString(),
                            PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                            PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType")),
                            PaymentName = dr["PaymentName"].ToString()
                        });
                }
            }

            return lst;
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目登记基本信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>某一个支出项目登记基本信息</returns>
        public MPayRegister GetPayRegisterBaseByPlanId(string planId)
        {
            var mdl = new MPayRegister();
            var sql = new StringBuilder();

            sql.Append("SELECT TourId,PlanId,[Type],SourceName,Confirmation,Prepaid,PaymentType,Register=(SELECT ISNULL(SUM(PaymentAmount),0) FROM tbl_FinRegister WHERE IsDeleted='0' AND PlanId=tbl_Plan.PlanId) FROM tbl_Plan WHERE PlanId = @PlanId");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@PlanId", DbType.AnsiStringFixedLength, planId);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.PlanTyp = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.Supplier = dr["SourceName"].ToString();
                    mdl.Payable = dr.GetDecimal(dr.GetOrdinal("Confirmation"));
                    mdl.Paid = dr.GetDecimal(dr.GetOrdinal("Prepaid"));
                    mdl.Register = dr.GetDecimal(dr.GetOrdinal("Register"));
                    mdl.PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据计调编号获取某一个计调项目的出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(string planId, bool? isPrepaid)
        {
            var lst = new List<MRegister>();
            var strSql = new StringBuilder();

            strSql.Append(" SELECT");
            strSql.Append(" 	RegisterId");
            strSql.Append(" 	,PaymentDate");
            strSql.Append(" 	,DealerId");
            strSql.Append(" 	,Dealer");
            strSql.Append(" 	,PaymentAmount");
            strSql.Append(" 	,PaymentType");
            strSql.Append(" 	,Deadline");
            strSql.Append(" 	,Remark");
            strSql.Append(" 	,Status");
            strSql.Append(" FROM");
            strSql.Append(" 	tbl_FinRegister");
            strSql.Append(" WHERE");
            strSql.Append(" 	PlanId=@PlanId AND IsDeleted='0'");
            if (isPrepaid.HasValue)
            {
                strSql.Append(" 	AND IsPrepaid=@IsPrepaid");
            }
            strSql.Append(" ORDER BY");
            strSql.Append("     IssueTime DESC");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            if (isPrepaid.HasValue)
            {
                this._db.AddInParameter(dc, "@IsPrepaid", DbType.AnsiStringFixedLength, isPrepaid.Value ? "1" : "0");
            }

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MRegister
                    {
                        RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId")),
                        PaymentDate = dr.IsDBNull(dr.GetOrdinal("PaymentDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PaymentDate")),
                        DealerId = dr["DealerId"].ToString(),
                        Dealer = dr["Dealer"].ToString(),
                        PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                        PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType")),
                        Deadline = dr.IsDBNull(dr.GetOrdinal("Deadline"))?null:(DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline")),
                        Remark = dr["Remark"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"))
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int AddRegister(MRegister mdl)
        {
            var dc = this._db.GetStoredProcCommand("proc_FinRegister_Add");

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@PaymentDate", DbType.DateTime, mdl.PaymentDate);
            this._db.AddInParameter(dc, "@Deadline", DbType.DateTime, mdl.Deadline);
            this._db.AddInParameter(dc, "@DealerDeptId", DbType.Int32, mdl.DealerDeptId);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@PaymentAmount", DbType.Decimal, mdl.PaymentAmount);
            this._db.AddInParameter(dc, "@PaymentType", DbType.Int32, mdl.PaymentType);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, mdl.ApproverTime);
            this._db.AddInParameter(dc, "@ApproveRemark", DbType.String, mdl.ApproveRemark);
            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, mdl.AccountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, mdl.AccountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, mdl.Accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, mdl.PayTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IsDeleted", DbType.AnsiStringFixedLength, mdl.IsDeleted ? "1" : "0");
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@IsPrepaid", DbType.AnsiStringFixedLength, mdl.IsPrepaid ? "1" : "0");
            this._db.AddInParameter(dc, "@IsAuto", DbType.AnsiStringFixedLength, "0");
            this._db.AddInParameter(dc, "@IsGuide", DbType.AnsiStringFixedLength, "0");

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int UpdRegister(MRegister mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" DECLARE @PlanId CHAR(36)");
            strSql.Append(" SELECT  @PlanId = PlanId");
            strSql.Append(" FROM    dbo.tbl_FinRegister");
            strSql.Append(" WHERE   CompanyId = @CompanyId");
            strSql.Append("         AND RegisterId = @RegisterId");
            strSql.Append(" IF ( SELECT ISNULL(SUM(PaymentAmount), 0)");
            strSql.Append("      FROM   tbl_FinRegister");
            strSql.Append("      WHERE  IsDeleted = '0'");
            strSql.Append("             AND PlanId = @PlanId AND RegisterId <> @RegisterId");
            strSql.Append("    ) + @PaymentAmount > ( SELECT    Confirmation");
            strSql.Append("                           FROM      dbo.tbl_Plan");
            strSql.Append("                           WHERE     PlanId = @PlanId");
            strSql.Append("                         ) ");
            strSql.Append("     BEGIN");
            strSql.Append("         SELECT -1");
            strSql.Append("     END");
            strSql.Append(" ELSE ");
            strSql.Append("     BEGIN");
            strSql.Append("         UPDATE  [tbl_FinRegister]");
            strSql.Append("         SET     [PaymentDate] = @PaymentDate ,");
            strSql.Append("                 [Deadline] = @Deadline ,");
            strSql.Append("                 [DealerDeptId] = @DealerDeptId ,");
            strSql.Append("                 [DealerId] = @DealerId ,");
            strSql.Append("                 [Dealer] = @Dealer ,");
            strSql.Append("                 [PaymentAmount] = @PaymentAmount ,");
            strSql.Append("                 [PaymentType] = @PaymentType ,");
            strSql.Append("                 [Remark] = @Remark");
            strSql.Append("         WHERE   CompanyId = @CompanyId");
            strSql.Append("                 AND RegisterId = @RegisterId");
            strSql.Append("     END");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.Int32, mdl.RegisterId);
            this._db.AddInParameter(dc, "@PaymentDate", DbType.DateTime, mdl.PaymentDate);
            this._db.AddInParameter(dc, "@Deadline", DbType.DateTime, mdl.Deadline);
            this._db.AddInParameter(dc, "@DealerDeptId", DbType.Int32, mdl.DealerDeptId);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@PaymentAmount", DbType.Decimal, mdl.PaymentAmount);
            this._db.AddInParameter(dc, "@PaymentType", DbType.Int32, mdl.PaymentType);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        public bool DelRegister(string companyId, int registerId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE [tbl_FinRegister] SET IsDeleted='1'");
            strSql.Append(" WHERE CompanyId = @CompanyId AND RegisterId = @RegisterId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.Int32, registerId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>付款审批列表</returns>
        public IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , MPayableApproveBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            const string table = "tbl_FinRegister";
            var sumfield = new[] { "PaymentAmount" };
            var lst = new List<MPayableApprove>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append(" 	XMLPlan=(SELECT [Type],SourceId,SourceName,OperatorName");
            field.Append(" 			 FROM tbl_Plan WHERE PlanId=tbl_FinRegister.PlanId FOR XML RAW,ROOT)");
            field.Append(" 	,XMLTour=(SELECT TourCode,CONVERT(VARCHAR(19),LDate,120) LDate,SellerName");
            field.Append(" 			 FROM tbl_Tour WHERE TourId=tbl_FinRegister.TourId and ParentId<>'' FOR XML RAW,ROOT)");
            field.Append(" 	,Dealer");
            field.Append(" 	,PaymentAmount");
            field.Append(" 	,Deadline");
            field.Append(" 	,Remark");
            field.Append(" 	,RegisterId");
            field.Append(" 	,Status");
            field.Append(" 	,TourId");
            field.Append(" 	,IsGuide ");

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            query.AppendFormat(" 				TourId=tbl_FinRegister.TourId AND IsDelete = '0' AND ISNULL(ParentId,'') <> '' AND TourStatus < {0}", (int)TourStatus.已取消);
            if (mSearch.IsPrepaidConfirm)
            {
                query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));
            }
            query.Append(" 				) AND");
            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Plan");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 				SourceId='{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 				SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            if (mSearch.PlanTyp.HasValue)
            {
                query.AppendFormat(" 				[Type]={0} AND", (int)mSearch.PlanTyp.Value);
            }
            query.AppendFormat(" 				PlanId=tbl_FinRegister.PlanId AND [Type]<>{0}) AND",(int)PlanProject.购物);
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat(" 	DealerId='{0}' AND", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat(" 	Dealer LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Dealer));
            }
            if (!string.IsNullOrEmpty(mSearch.PaymentDateS))
            {
                query.AppendFormat(" 	PaymentDate>='{0}' AND", mSearch.PaymentDateS);
            }
            if (!string.IsNullOrEmpty(mSearch.PaymentDateE))
            {
                query.AppendFormat(" 	PaymentDate<'{0}' AND",Utils.GetDateTime(mSearch.PaymentDateE).AddDays(1));
            }
            if (!string.IsNullOrEmpty(mSearch.DeadlineS))
            {
                query.AppendFormat(" 	Deadline>='{0}' AND", mSearch.DeadlineS);
            }
            if (!string.IsNullOrEmpty(mSearch.DeadlineE))
            {
                query.AppendFormat(" 	Deadline<'{0}' AND", Utils.GetDateTime(mSearch.DeadlineE).AddDays(1));
            }
            if (mSearch.Status.HasValue)
            {
                query.AppendFormat(" 	Status={0} AND", (int)mSearch.Status.Value);
            }
            else
            {
                query.AppendFormat(" 	Status<>{0} AND", (int)FinStatus.销售待确认);
            }
            //if (mSearch.IsPrepaidConfirm)
            //{
            //    query.Append(" 	IsPrepaid='1' AND");
            //}
            query.AppendFormat(" 	IsDeleted='0' AND CompanyId='{0}'", mSearch.CompanyId);
            if (!mSearch.IsPrepaidConfirm)
            {
                query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_FinRegister.TourId");
                query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
                query.Append(")");
            }
            //else
            //{
            //    query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));
            //}

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MPayableApprove
                    {
                        TourId = dr["TourId"].ToString(),
                        RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId")),
                        PlanTyp = (PlanProject)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "Type")),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "TourCode"),
                        SellerName = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "SellerName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "LDate")),
                        SupplierId = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "SourceId"),
                        Supplier = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "SourceName"),
                        Planer = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "OperatorName"),
                        Dealer = dr["Dealer"].ToString(),
                        PayAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                        PayExpire = dr.IsDBNull(dr.GetOrdinal("Deadline")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline")),
                        Remark = dr.GetString(dr.GetOrdinal("Remark")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        IsDaoYouXianFu = dr["IsGuide"].ToString() == "1"
                    });
                }
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
            var strSql = new StringBuilder();
            var mdl = new MRegister();

            strSql.Append(" SELECT");
            strSql.Append(" 	R.RegisterId");
            strSql.Append(" 	,R.TourId");
            strSql.Append(" 	,R.PaymentDate");
            strSql.Append(" 	,R.Dealer");
            strSql.Append(" 	,R.PaymentAmount");
            strSql.Append(" 	,R.PaymentType");
            strSql.Append(" 	,C.[Name] PaymentName");
            strSql.Append(" 	,R.Deadline");
            strSql.Append(" 	,R.Remark");
            strSql.Append(" 	,R.Status");
            strSql.Append(" 	,R.ApproveTime");
            strSql.Append(" 	,R.Approver");
            strSql.Append(" 	,R.ApproveRemark");
            strSql.Append(" 	,R.Accountant");
            strSql.Append(" 	,R.PayTime");
            strSql.Append(" 	,R.IsPrepaid");
            strSql.Append(" 	,P.[Type]");
            strSql.Append(" 	,P.SourceName");
            strSql.Append(" FROM");
            strSql.Append(" 	tbl_FinRegister R");
            strSql.Append(" LEFT OUTER JOIN");
            strSql.Append("     tbl_Plan P");
            strSql.Append(" ON");
            strSql.Append("     R.PlanId=P.PlanId");
            strSql.Append(" LEFT OUTER JOIN");
            strSql.Append("     tbl_ComPayment C");
            strSql.Append(" ON");
            strSql.Append("     C.PaymentId=R.PaymentType");
            strSql.Append(" WHERE");
            strSql.Append(" 	R.CompanyId=@CompanyId");
            strSql.Append(" 	AND R.RegisterId=@RegisterId");
            strSql.Append(" ORDER BY");
            strSql.Append("     R.IssueTime DESC");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.AnsiStringFixedLength, registerId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.Accountant = dr["Accountant"].ToString();
                    mdl.PayTime = dr.IsDBNull(dr.GetOrdinal("PayTime")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PayTime"));
                    mdl.Approver = dr["Approver"].ToString();
                    mdl.ApproveRemark = dr["ApproveRemark"].ToString();
                    mdl.IsPrepaid = dr.GetString(dr.GetOrdinal("IsPrepaid")) == "1";
                    mdl.RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId"));
                    mdl.PaymentDate = dr.IsDBNull(dr.GetOrdinal("PaymentDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PaymentDate"));
                    mdl.Dealer = dr["Dealer"].ToString();
                    mdl.PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount"));
                    mdl.PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType"));
                    mdl.PaymentName = dr["PaymentName"].ToString();
                    mdl.Deadline = dr.IsDBNull(dr.GetOrdinal("Deadline"))
                                       ? null
                                       : (DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline"));
                    mdl.Remark = dr["Remark"].ToString();
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.ApproverTime = dr.IsDBNull(dr.GetOrdinal("ApproveTime")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("ApproveTime"));
                    mdl.PlanTyp = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.Supplier = dr["SourceName"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据登记编号集合设置登记审核状态
        /// </summary>
        /// <param name="approverId">审核人编号</param>
        /// <param name="approver">审核人</param>
        /// <param name="approveTime">审核时间</param>
        /// <param name="approveRemark">审核意见</param>
        /// <param name="status">状态</param>
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
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinRegister");
            strSql.Append(" SET Status=@Status");
            strSql.Append("  ,ApproverId=@ApproverId");
            strSql.Append("  ,Approver=@Approver");
            strSql.Append("  ,ApproveTime=@ApproveTime");
            strSql.Append("  ,ApproveRemark=@ApproveRemark");
            strSql.Append(" WHERE");
            strSql.Append("     CompanyId = @CompanyId AND");
            strSql.Append("     RegisterId IN (" + Utils.GetSqlIdStrByArray(registerIds) + ")");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)status);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, approverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, approveTime);
            this._db.AddInParameter(dc, "@ApproveRemark", DbType.String, approveRemark);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db);
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
            var strSql = new StringBuilder();

            foreach (var m in lst)
            {
                strSql.Append(" UPDATE tbl_FinRegister");
                strSql.AppendFormat(" SET Status={0}", (int)FinStatus.账务已支付);
                strSql.Append("  ,AccountantDeptId=@AccountantDeptId");
                strSql.Append("  ,AccountantId=@AccountantId");
                strSql.Append("  ,PayTime=@PayTime");
                strSql.Append("  ,Accountant=@Accountant");
                strSql.AppendFormat("  ,PaymentType={0}", m.PaymentType);
                strSql.Append(" WHERE");
                strSql.Append("     CompanyId = @CompanyId AND");
                strSql.AppendFormat("     RegisterId = {0}", m.RegisterId);

                strSql.Append(" UPDATE tbl_Plan");
                strSql.AppendFormat(" SET Prepaid = Prepaid + (SELECT ISNULL(PaymentAmount,0) FROM tbl_FinRegister WHERE CompanyId=@CompanyId AND RegisterId={0}) WHERE tbl_Plan.PlanId = (SELECT PlanId FROM tbl_FinRegister WHERE CompanyId=@CompanyId AND RegisterId={0})", m.RegisterId);
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, accountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, accountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, payTime);

            return DbHelper.ExecuteSqlTrans(dc, this._db);

        }

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        public MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ)
        {
            var strSql = new StringBuilder();
            var mdl = new MPlanCostPay();

            strSql.Append(" SELECT");
            strSql.Append("         ISNULL(SUM(Prepaid),0) Paid,");
            strSql.Append("         ISNULL(SUM(Confirmation)-SUM(Prepaid),0) Unpaid");
            strSql.Append(" FROM    tbl_Plan");
            strSql.Append(" WHERE   TourId = @TourId");
            strSql.Append("         AND Type = @Type");
            strSql.AppendFormat("   AND Status={0}", (int)PlanState.已落实);


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@Type", DbType.Byte, (int)typ);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Paid = dr.GetDecimal(dr.GetOrdinal("Paid"));
                    mdl.Unpaid = dr.GetDecimal(dr.GetOrdinal("Unpaid"));
                }
            }
            return mdl;
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
            var strSql = new StringBuilder();
            var lst = new List<MPlanCostConfirm>();

            strSql.Append(" SELECT");
            strSql.Append("         ROW_NUMBER() OVER(ORDER BY PlanId) AS Num,");
            strSql.Append("         SourceName,");
            strSql.Append("         Confirmation");
            strSql.Append(" FROM    tbl_Plan");
            strSql.Append(" WHERE   TourId = @TourId");
            strSql.Append("         AND Type = @Type");
            strSql.Append("         AND CostStatus=@CostStatus");
            strSql.AppendFormat("   AND Status={0}", (int)PlanState.已落实);


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@Type", DbType.Byte, (int)typ);
            this._db.AddInParameter(dc, "@CostStatus", DbType.AnsiStringFixedLength, isConfirmed ? "1" : "0");

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanCostConfirm
                    {
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        Supplier = dr.IsDBNull(dr.GetOrdinal("SourceName")) ? "" : dr.GetString(dr.GetOrdinal("SourceName")),
                        Cost = dr.GetDecimal(dr.GetOrdinal("Confirmation"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
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
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_Plan");
            strSql.Append(" SET");
            strSql.Append("         CostId=@CostId,");
            strSql.Append("         CostName=@CostName,");
            strSql.Append("         CostStatus='1',");
            //strSql.Append("         Confirmation=@Confirmation,");
            strSql.Append("         CostRemarks=@CostRemark,");
            strSql.Append("         CostTime=GETDATE()");
            strSql.Append(" WHERE   PlanId = @PlanId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            this._db.AddInParameter(dc, "@CostId", DbType.AnsiStringFixedLength, costId);
            this._db.AddInParameter(dc, "@CostName", DbType.AnsiStringFixedLength, costName);
            this._db.AddInParameter(dc, "@CostRemark", DbType.String, costRemark);
            this._db.AddInParameter(dc, "@Confirmation", DbType.Decimal, confirmation);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdDebit(MDebit mdl)
        {
            var strSql = new StringBuilder();

            if (mdl.Id > 0)
            {
                strSql.Append(" UPDATE [tbl_FinDebit]");
                strSql.Append("    SET [BorrowerId] = @BorrowerId");
                strSql.Append("       ,[Borrower] = @Borrower");
                strSql.Append("       ,[BorrowTime] = @BorrowTime");
                strSql.Append("       ,[BorrowAmount] = @BorrowAmount");
                strSql.Append("       ,[UseFor] = @UseFor");
                strSql.Append("       ,[PreSignNum] = @PreSignNum");
                strSql.Append("  WHERE Id=@Id AND CompanyId=@CompanyId");
            }
            else
            {
                strSql.Append(" INSERT  INTO [tbl_FinDebit]");
                strSql.Append("         ( [CompanyId] ,");
                strSql.Append("           [TourId] ,");
                strSql.Append("           [TourCode] ,");
                strSql.Append("           [BorrowerId] ,");
                strSql.Append("           [Borrower] ,");
                strSql.Append("           [BorrowTime] ,");
                strSql.Append("           [BorrowAmount] ,");
                strSql.Append("           [UseFor] ,");
                strSql.Append("           [PreSignNum],");
                strSql.Append("           [OperatorId],");
                strSql.Append("           [Operator],");
                strSql.Append("           [DeptId],");
                strSql.Append("           [IssueTime]");
                strSql.Append("         )");
                strSql.Append(" VALUES  ( @CompanyId ,");
                strSql.Append("           @TourId ,");
                strSql.Append("           @TourCode ,");
                strSql.Append("           @BorrowerId ,");
                strSql.Append("           @Borrower ,");
                strSql.Append("           @BorrowTime ,");
                strSql.Append("           @BorrowAmount ,");
                strSql.Append("           @UseFor ,");
                strSql.Append("           @PreSignNum,");
                strSql.Append("           @OperatorId,");
                strSql.Append("           @Operator,");
                strSql.Append("           @DeptId,");
                strSql.Append("           @IssueTime");
                strSql.Append("         )");
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@BorrowerId", DbType.AnsiStringFixedLength, mdl.BorrowerId);
            this._db.AddInParameter(dc, "@Borrower", DbType.String, mdl.Borrower);
            this._db.AddInParameter(dc, "@BorrowTime", DbType.DateTime, mdl.BorrowTime);
            this._db.AddInParameter(dc, "@BorrowAmount", DbType.Decimal, mdl.BorrowAmount);
            this._db.AddInParameter(dc, "@UseFor", DbType.String, mdl.UseFor);
            this._db.AddInParameter(dc, "@PreSignNum", DbType.Int32, mdl.PreSignNum);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetDebitApprove(MDebit mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE  [tbl_FinDebit]");
            strSql.Append(" SET     ");
            strSql.Append("         [Status] = @Status ,");
            strSql.Append("         [ApproverId] = @ApproverId ,");
            strSql.Append("         [Approver] = @Approver ,");
            strSql.Append("         [ApproveDate] = @ApproveDate ,");
            strSql.Append("         [Approval] = @Approval ,");
            strSql.Append("         [RealAmount] = @RealAmount, ");
            strSql.Append("         [RelSignNum] = @RelSignNum");
            strSql.Append(" WHERE   Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "@ApproveDate", DbType.DateTime, mdl.ApproveDate);
            this._db.AddInParameter(dc, "@Approval", DbType.String, mdl.Approval);
            this._db.AddInParameter(dc, "@RealAmount", DbType.Decimal, mdl.RealAmount);
            this._db.AddInParameter(dc, "@RelSignNum", DbType.Decimal, mdl.RelSignNum);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        public MDebit GetDebit(int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MDebit();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            strSql.Append("         [TourCode] ,");
            strSql.Append("         [BorrowerId] ,");
            strSql.Append("         [Borrower] ,");
            strSql.Append("         [BorrowTime] ,");
            strSql.Append("         [BorrowAmount] ,");
            strSql.Append("         [UseFor] ,");
            strSql.Append("         [ApproverId] ,");
            strSql.Append("         [Approver] ,");
            strSql.Append("         [ApproveDate] ,");
            strSql.Append("         [Approval] ,");
            strSql.Append("         [RealAmount] ,");
            strSql.Append("         [Status] ,");
            strSql.Append("         [LenderId] ,");
            strSql.Append("         [Lender] ,");
            strSql.Append("         [LendDate] ,");
            strSql.Append("         [LendRemark],");
            strSql.Append("         [PreSignNum],");
            strSql.Append("         [RelSignNum],");
            strSql.Append("         [OperatorId],");
            strSql.Append("         [Operator],");
            strSql.Append("         [DeptId],");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [tbl_FinDebit]");
            strSql.Append(" WHERE   Id = @Id");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    mdl.TourCode = dr.IsDBNull(dr.GetOrdinal("TourCode")) ? "" : dr.GetString(dr.GetOrdinal("TourCode"));
                    mdl.BorrowerId = dr.IsDBNull(dr.GetOrdinal("BorrowerId")) ? "" : dr.GetString(dr.GetOrdinal("BorrowerId"));
                    mdl.Borrower = dr.GetString(dr.GetOrdinal("Borrower"));
                    mdl.BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime"));
                    mdl.BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount"));
                    mdl.UseFor = dr.IsDBNull(dr.GetOrdinal("UseFor")) ? "" : dr.GetString(dr.GetOrdinal("UseFor"));
                    mdl.ApproverId = dr.IsDBNull(dr.GetOrdinal("ApproverId")) ? "" : dr.GetString(dr.GetOrdinal("ApproverId"));
                    mdl.Approver = dr.IsDBNull(dr.GetOrdinal("Approver")) ? "" : dr.GetString(dr.GetOrdinal("Approver"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ApproveDate")))
                    {
                        mdl.ApproveDate = dr.GetDateTime(dr.GetOrdinal("ApproveDate"));
                    }
                    mdl.Approval = dr.IsDBNull(dr.GetOrdinal("Approval")) ? "" : dr.GetString(dr.GetOrdinal("Approval"));
                    mdl.RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount"));
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.LenderId = dr["LenderId"].ToString();
                    mdl.Lender = dr["Lender"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("LendDate")))
                    {
                        mdl.LendDate = dr.GetDateTime(dr.GetOrdinal("LendDate"));
                    }
                    mdl.LendRemark = dr.IsDBNull(dr.GetOrdinal("LendRemark")) ? "" : dr.GetString(dr.GetOrdinal("LendRemark"));
                    mdl.PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum"));
                    mdl.RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

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
        public IList<MDebit> GetDebitLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MDebitBase mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            var lst = new List<MDebit>();
            var query = new StringBuilder();

            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.BorrowerId))
            {
                query.AppendFormat(" BorrowerId = '{0}' AND", mSearch.BorrowerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Borrower))
            {
                query.AppendFormat(" Borrower LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Borrower));
            }
            query.AppendFormat(" EXISTS(select 1 from tbl_tour where tourid=tbl_FinDebit.tourid and IsSubmit = {0} and ParentId<>'' and TourStatus<{1}) AND", mSearch.IsVerificated ? "1" : "0",(int)TourStatus.已取消);
            query.AppendFormat(" IsDeleted = '0' AND CompanyId = '{1}'", (int)mSearch.Status, mSearch.CompanyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "tbl_FinDebit", "Id", "[Id],[TourId],[TourCode],[Borrower],[BorrowTime],[BorrowAmount],[Status],[RealAmount],[PreSignNum],[RelSignNum]"
                , query.ToString()
                , "BorrowTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        TourId = dr["TourId"].ToString(),
                        TourCode = dr["TourCode"].ToString(),
                        Borrower = dr["Borrower"].ToString(),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByTourId(string tourId,bool isBz)
        {
            var lst = new List<MDebit>();
            var sql =
                string.Format(
                    "select Id,BorrowerId,Borrower,BorrowTime,BorrowAmount,RealAmount,PreSignNum,RelSignNum,UseFor,Status from tbl_FinDebit where IsDeleted='0' and TourId='{0}' and Status {2} {1}",
                    tourId,
                    (int)FinStatus.账务已支付,
                    isBz ? "=" : "<=");
            var cmd = this._db.GetSqlStringCommand(sql);
            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        BorrowerId = dr["BorrowerId"].ToString(),
                        Borrower = dr.GetString(dr.GetOrdinal("Borrower")),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                        UseFor = dr["UseFor"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                    });
                    //lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByPlanId(string planId)
        {
            var lst = new List<MDebit>();
            var sql =
                string.Format(
                    "select Id,Borrower,BorrowTime,BorrowAmount,RealAmount,PreSignNum,RelSignNum,UseFor,Status from tbl_FinDebit where IsDeleted='0' and PlanId='{0}'", planId);
            var cmd = this._db.GetSqlStringCommand(sql);
            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Borrower = dr.GetString(dr.GetOrdinal("Borrower")),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                        UseFor = dr["UseFor"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Pay(MDebit mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinDebit");
            strSql.Append(" SET Status=@Status,");
            strSql.Append("     LenderId=@LenderId,");
            strSql.Append("     Lender=@Lender,");
            strSql.Append("     LendDate=getdate(),");
            strSql.Append("     LendRemark=@LendRemark");
            strSql.Append(" WHERE");
            strSql.Append("     Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@LenderId", DbType.AnsiStringFixedLength, mdl.LenderId);
            this._db.AddInParameter(dc, "@Lender", DbType.String, mdl.Lender);
            this._db.AddInParameter(dc, "@LendRemark", DbType.String, mdl.LendRemark);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, mdl.Status);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DeleteDebit(string companyId, int id)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinDebit");
            strSql.Append(" SET IsDeleted='1'");
            strSql.Append(" WHERE");
            strSql.Append("     Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

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
        public IList<MCustomerWarning> GetCustomerWarningLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId
                                                            , MWarningBase mSearch
                                                            , string operatorId
                                                            , params int[] deptIds)
        {
            var lst = new List<MCustomerWarning>();
            var query = new StringBuilder();

            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.CrmId))
                {
                    query.AppendFormat(" CrmId = '{0}' AND", mSearch.CrmId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Customer))
                {
                    query.AppendFormat(" Customer LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Customer));
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" SellerId = '{0}' AND", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (mSearch.Arrear.HasValue&&mSearch.SignArrear.HasValue)
                {
                    query.AppendFormat(" Arrear {0} {1} AND", GetEqualSign(mSearch.SignArrear), mSearch.Arrear.Value);
                }
                if (mSearch.Transfinite.HasValue&&mSearch.SignTransfinite.HasValue)
                {
                    query.AppendFormat(" Transfinite {0} {1} AND", GetEqualSign(mSearch.SignTransfinite), mSearch.Transfinite.Value);
                }
            }
            query.AppendFormat(" CompanyId = '{0}' AND (Transfinite>0 or DeadDay>Deadline) ", companyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "view_CustomerWarning", "CrmId", "CrmId,Customer,XMLContact,SellerName,AmountOwed,Arrear,Transfinite,TransfiniteTime,Deadline,DeadDay,TourCount"
                , query.ToString()
                , "TransfiniteTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MCustomerWarning
                    {
                        CrmId = dr["CrmId"].ToString(),
                        Customer = dr.IsDBNull(dr.GetOrdinal("Customer")) ? "" : dr.GetString(dr.GetOrdinal("Customer")),
                        SellerName = dr.IsDBNull(dr.GetOrdinal("SellerName")) ? "" : dr.GetString(dr.GetOrdinal("SellerName")),
                        AmountOwed = dr.GetDecimal(dr.GetOrdinal("AmountOwed")),
                        Transfinite = dr.GetDecimal(dr.GetOrdinal("Transfinite")),
                        Arrear = dr.GetDecimal(dr.GetOrdinal("Arrear")),
                        TransfiniteTime = dr.GetDateTime(dr.GetOrdinal("TransfiniteTime")),
                        Deadline = dr.GetInt32(dr.GetOrdinal("Deadline")),
                        DeadDay = dr.GetInt32(dr.GetOrdinal("DeadDay")),
                        TourCount = dr.GetInt32(dr.GetOrdinal("TourCount")),
                        Contact = GetCrmLinkmanLst(dr["XMLContact"].ToString())
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据客户编号获取客户预警列表
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>客户预警列表</returns>
        public IList<MCustomerWarning> GetCustomerWarningLstByCrmId(string crmId, string operatorId, params int[] deptIds)
        {
            var lst = new List<MCustomerWarning>();
            var sql = new StringBuilder();

            sql.Append(" select Customer,AmountOwed,Arrear,Transfinite,TransfiniteTime,Deadline,DeadDay");
            sql.AppendFormat(" from view_CustomerWarning where CrmId = '{0}' AND (Transfinite>AmountOwed or DeadDay>Deadline) ", crmId);
            sql.Append(GetOrgCondition(operatorId, deptIds));
            sql.Append(" order by TransfiniteTime DESC");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MCustomerWarning
                    {
                        Customer = dr.IsDBNull(dr.GetOrdinal("Customer")) ? "" : dr.GetString(dr.GetOrdinal("Customer")),
                        AmountOwed = dr.GetDecimal(dr.GetOrdinal("AmountOwed")),
                        Transfinite = dr.GetDecimal(dr.GetOrdinal("Transfinite")),
                        Arrear = dr.GetDecimal(dr.GetOrdinal("Arrear")),
                        TransfiniteTime = dr.GetDateTime(dr.GetOrdinal("TransfiniteTime")),
                        Deadline = dr.GetInt32(dr.GetOrdinal("Deadline")),
                        DeadDay = dr.GetInt32(dr.GetOrdinal("DeadDay")),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MBatchWriteOffOrder> GetArrearOrderLstByCrmId(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref string xmlSum
                                                                , string crmId
                                                                , string operatorId
                                                                , params int[] deptIds)
        {
            var lst = new List<MBatchWriteOffOrder>();
            var field = new StringBuilder();
            var query = new StringBuilder();
            var sumfield = new[] { "ConfirmMoney", "CheckMoney", "ReturnMoney" };

            field.Append(" XMLTour=(SELECT TourCode,RouteName,CONVERT(VARCHAR(19),LDate,120) LDate FROM tbl_Tour WHERE TourId = tbl_TourOrder.TourId and ParentId<>'' FOR XML RAW,ROOT)");
            field.Append(" ,Adults,Childs,IssueTime,ConfirmMoney,CheckMoney,ConfirmMoney - CheckMoney+ReturnMoney UnReceived");

            query.AppendFormat(" IsClean='0' AND BuyCompanyId = '{0}' AND Status = {1} and IsDelete='0'", crmId, (int)OrderStatus.已成交);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , "tbl_TourOrder", this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MBatchWriteOffOrder
                    {
                        TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode"),
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "RouteName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "LDate")),
                        AdultNum = dr.GetInt32(dr.GetOrdinal("Adults")),
                        ChildNum = dr.GetInt32(dr.GetOrdinal("Childs")),
                        OrderTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Receivable = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")),
                        Received = dr.GetDecimal(dr.GetOrdinal("CheckMoney")),
                        Unreceivable = dr.GetDecimal(dr.GetOrdinal("UnReceived"))
                    };
                    lst.Add(mdl);
                }
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
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>客户预警列表</returns>
        public IList<MSalesmanWarning> GetSalesmanWarningLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId
                                                            , MWarningBase mSearch
                                                            , string operatorId
                                                            , params int[] deptIds)
        {
            var lst = new List<MSalesmanWarning>();
            var query = new StringBuilder();

            if (mSearch != null)
            {
                if (mSearch.DeptId > 0)
                {
                    query.AppendFormat(" DeptId = {0} AND", mSearch.DeptId);
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" SellerId = '{0}' AND", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (mSearch.Arrear.HasValue)
                {
                    query.AppendFormat(" Arrear {0} {1} AND", GetEqualSign(mSearch.SignArrear), mSearch.Arrear);
                }
                if (mSearch.Transfinite.HasValue)
                {
                    query.AppendFormat(" Transfinite {0} {1} AND", GetEqualSign(mSearch.SignTransfinite), mSearch.Transfinite);
                }
            }
            query.AppendFormat(" CompanyId = '{0}' AND Transfinite>0 ", companyId);
            query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "view_SalesmanWarning", "SellerId", "SellerId,SellerName,Arrear,ConfirmAdvances,PreIncome,SumPay,Transfinite,AmountOwed,TransfiniteTime"
                , query.ToString(), "TransfiniteTime"))
            {
                while (dr.Read())
                {
                    var mdl = new MSalesmanWarning
                    {
                        SellerId = dr["SellerId"].ToString(),
                        SellerName = dr["SellerName"].ToString(),
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("Arrear")))
                    {
                        mdl.Arrear = dr.GetDecimal(dr.GetOrdinal("Arrear"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("ConfirmAdvances")))
                    { mdl.ConfirmAdvances = dr.GetDecimal(dr.GetOrdinal("ConfirmAdvances")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("PreIncome")))
                    { mdl.PreIncome = dr.GetDecimal(dr.GetOrdinal("PreIncome")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("SumPay")))
                    { mdl.SumPay = dr.GetDecimal(dr.GetOrdinal("SumPay")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("Transfinite")))
                    { mdl.Transfinite = dr.GetDecimal(dr.GetOrdinal("Transfinite")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("AmountOwed")))
                    { mdl.AmountOwed = dr.GetDecimal(dr.GetOrdinal("AmountOwed")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("TransfiniteTime")))
                    { mdl.TransfiniteTime = dr.GetDateTime(dr.GetOrdinal("TransfiniteTime")); }

                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据销售员编号获取销售预警列表
        /// </summary>
        /// <param name="sellerId">销售员编号</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>销售预警列表</returns>
        public IList<MSalesmanWarning> GetSalesmanWarningLstBySellerId(string sellerId, string operatorId, params int[] deptIds)
        {
            var lst = new List<MSalesmanWarning>();
            var query = new StringBuilder();

            query.Append("select SellerName,Arrear,ConfirmAdvances,PreIncome,SumPay,Transfinite,AmountOwed,TransfiniteTime");
            query.AppendFormat(" from view_SalesmanWarning where SellerId = '{0}' AND Transfinite>0 ", sellerId);
            query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));
            query.Append(" order by TransfiniteTime");

            var cmd = this._db.GetSqlStringCommand(query.ToString());
            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MSalesmanWarning
                    {
                        SellerName = dr["SellerName"].ToString(),
                        Arrear = dr.GetDecimal(dr.GetOrdinal("Arrear")),
                        ConfirmAdvances = dr.GetDecimal(dr.GetOrdinal("ConfirmAdvances")),
                        PreIncome = dr.GetDecimal(dr.GetOrdinal("PreIncome")),
                        SumPay = dr.GetDecimal(dr.GetOrdinal("SumPay")),
                        Transfinite = dr.GetDecimal(dr.GetOrdinal("Transfinite")),
                        AmountOwed = dr.GetDecimal(dr.GetOrdinal("AmountOwed")),
                        TransfiniteTime = dr.GetDateTime(dr.GetOrdinal("TransfiniteTime"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MSalesmanWarning> GetSalesmanConfirmLstBySellerId(int pageSize
                                                                    , int pageIndex
                                                                    , ref int recordCount
                                                                    , ref string xmlSum
                                                                    , string sellerId
                                                                    , string operatorId
                                                                    , params int[] deptIds)
        {
            var lst = new List<MSalesmanWarning>();
            var field = new StringBuilder();
            var query = new StringBuilder();
            var sumfield = new[] { "ConfirmMoney", "CheckMoney", "ReturnMoney" };

            field.Append(" XMLTour=(SELECT RouteName,CONVERT(VARCHAR(19),LDate,120) LDate FROM tbl_Tour WHERE TourId = tbl_TourOrder.TourId and ParentId<>'' FOR XML RAW,ROOT)");
            field.Append(" ,OrderCode,BuyCompanyName,Adults,Childs,ConfirmMoney,ConfirmMoney - CheckMoney + ReturnMoney UnReceived");

            query.AppendFormat(" IsClean='0' AND SellerId = '{0}' AND Status = {1} and IsDelete='0' AND ConfirmMoneyStatus=1", sellerId, (int)OrderStatus.已成交);
            query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , "tbl_TourOrder", this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MSalesmanWarning
                    {
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "RouteName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "LDate")),
                        OrderCode = dr["OrderCode"].ToString(),
                        Customer = dr["BuyCompanyName"].ToString(),
                        Adult = dr.GetInt32(dr.GetOrdinal("Adults")),
                        Child = dr.GetInt32(dr.GetOrdinal("Childs")),
                        Arrear = dr.GetDecimal(dr.GetOrdinal("UnReceived")),
                        ConfirmAdvances = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")),
                    };
                    lst.Add(mdl);
                }
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
            var strSql = new StringBuilder();

            strSql.Append(" INSERT  INTO [tbl_FinDisburseApply]");
            strSql.Append("         ( [DisburseId] ,");
            strSql.Append("           [CompanyId] ,");
            strSql.Append("           [ItemId] ,");
            strSql.Append("           [ItemType] ,");
            strSql.Append("           [RouteName] ,");
            strSql.Append("           [CrmId] ,");
            strSql.Append("           [Crm] ,");
            strSql.Append("           [ApplierId] ,");
            strSql.Append("           [Applier] ,");
            strSql.Append("           [ApplyTime] ,");
            strSql.Append("           [DisburseAmount] ,");
            strSql.Append("           [Remark] ,");
            strSql.Append("           [IsApprove] ,");
            strSql.Append("           [ApproverId] ,");
            strSql.Append("           [Approver] ,");
            strSql.Append("           [ApproveTime] ,");
            strSql.Append("           [ApproveRemark] ,");
            strSql.Append("           [DeptId] ,");
            strSql.Append("           [OperatorId] ,");
            strSql.Append("           [Operator] ,");
            strSql.Append("           [IssueTime]");
            strSql.Append("         )");
            strSql.Append(" VALUES  ( @DisburseId ,");
            strSql.Append("           @CompanyId ,");
            strSql.Append("           @ItemId ,");
            strSql.Append("           @ItemType ,");
            strSql.Append("           @RouteName ,");
            strSql.Append("           @CrmId ,");
            strSql.Append("           @Crm ,");
            strSql.Append("           @ApplierId ,");
            strSql.Append("           @Applier ,");
            strSql.Append("           @ApplyTime ,");
            strSql.Append("           @DisburseAmount ,");
            strSql.Append("           @Remark ,");
            strSql.Append("           @IsApprove ,");
            strSql.Append("           @ApproverId ,");
            strSql.Append("           @Approver ,");
            strSql.Append("           @ApproveTime ,");
            strSql.Append("           @ApproveRemark ,");
            strSql.Append("           @DeptId ,");
            strSql.Append("           @OperatorId ,");
            strSql.Append("           @Operator ,");
            strSql.Append("           @IssueTime");
            strSql.Append("         )");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@DisburseId", DbType.AnsiString, mdl.DisburseId);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiString, mdl.CompanyId);
            this._db.AddInParameter(cmd, "@ItemId", DbType.AnsiStringFixedLength, mdl.ItemId);
            this._db.AddInParameter(cmd, "@ItemType", DbType.Byte, (int)mdl.ItemType);
            this._db.AddInParameter(cmd, "@RouteName", DbType.String, mdl.RouteName);
            this._db.AddInParameter(cmd, "@CrmId", DbType.AnsiStringFixedLength, mdl.CrmId);
            this._db.AddInParameter(cmd, "@Crm", DbType.String, mdl.Crm);
            this._db.AddInParameter(cmd, "@ApplierId", DbType.AnsiStringFixedLength, mdl.ApplierId);
            this._db.AddInParameter(cmd, "@Applier", DbType.String, mdl.Applier);
            this._db.AddInParameter(cmd, "@ApplyTime", DbType.DateTime, mdl.ApplyTime);
            this._db.AddInParameter(cmd, "@DisburseAmount", DbType.Decimal, mdl.DisburseAmount);
            this._db.AddInParameter(cmd, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(cmd, "@IsApprove", DbType.Byte, mdl.IsApprove);
            this._db.AddInParameter(cmd, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(cmd, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(cmd, "@ApproveTime", DbType.DateTime, mdl.ApproveTime);
            this._db.AddInParameter(cmd, "@ApproveRemark", DbType.String, mdl.ApproveRemark);
            this._db.AddInParameter(cmd, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(cmd, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }

        /// <summary>
        /// 根据垫付编号获取垫付实体
        /// </summary>
        /// <param name="disburseId">垫付编号</param>
        /// <returns>垫付实体</returns>
        public MTransfinite GetTransfiniteMdl(string disburseId)
        {
            var mdl = new MTransfinite();
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  [DisburseId] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [ItemId] ,");
            strSql.Append("         [ItemType] ,");
            strSql.Append("         [RouteName] ,");
            strSql.Append("         [CrmId] ,");
            strSql.Append("         [Crm] ,");
            strSql.Append("         [ApplierId] ,");
            strSql.Append("         [Applier] ,");
            strSql.Append("         [ApplyTime] ,");
            strSql.Append("         [DisburseAmount] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [IsApprove] ,");
            strSql.Append("         [ApproverId] ,");
            strSql.Append("         [Approver] ,");
            strSql.Append("         [ApproveTime] ,");
            strSql.Append("         [ApproveRemark] ,");
            strSql.Append("         [DeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime],");
            strSql.Append("         (SELECT * FROM tbl_FinDisburseApplyOverrun WHERE DisburseId=tbl_FinDisburseApply.DisburseId for xml raw,root) as ApplyOverrunList");
            strSql.Append(" FROM    [tbl_FinDisburseApply]");
            strSql.Append(" WHERE   DisburseId = @DisburseId");

            var cmd = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(cmd, "@DisburseId", DbType.AnsiString, disburseId);
            using (var dr=DbHelper.ExecuteReader(cmd,this._db))
            {
                while (dr.Read())
                {
                    mdl.DisburseId = dr["DisburseId"].ToString();
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.ItemId = dr["ItemId"].ToString();
                    mdl.ItemType = (TransfiniteType)dr.GetByte(dr.GetOrdinal("ItemType"));
                    mdl.RouteName = dr["RouteName"].ToString();
                    mdl.CrmId = dr["CrmId"].ToString();
                    mdl.Crm = dr["Crm"].ToString();
                    mdl.ApplierId = dr["ApplierId"].ToString();
                    mdl.Applier = dr["Applier"].ToString();
                    mdl.ApplyTime = dr.GetDateTime(dr.GetOrdinal("ApplyTime"));
                    mdl.DisburseAmount = dr.GetDecimal(dr.GetOrdinal("DisburseAmount"));
                    mdl.Remark = dr["Remark"].ToString();
                    mdl.IsApprove = (TransfiniteStatus)dr.GetByte(dr.GetOrdinal("IsApprove"));
                    mdl.ApproverId = dr["ApproverId"].ToString();
                    mdl.Approver = dr["Approver"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ApproveTime")))
                    {
                        mdl.ApproveTime = dr.GetDateTime(dr.GetOrdinal("ApproveTime"));
                    }
                    mdl.ApproveRemark = dr["ApproveRemark"].ToString();
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.AdvanceAppList = GetApplyOverrunListByXml(dr["ApplyOverrunList"].ToString());
                }
            }
            return mdl;
        }

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
        public IList<MTransfinite> GetTransfiniteLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , MChaXianShenPiChaXunInfo mSearch
                                                    , string operatorId
                                                    , params int[] deptIds)
        {
            var lst = new List<MTransfinite>();
            var query = new StringBuilder();

            if (!string.IsNullOrEmpty(mSearch.RouteName))
            {
                query.AppendFormat(" RouteName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.RouteName));
            }
            if (!string.IsNullOrEmpty(mSearch.CrmId))
            {
                query.AppendFormat(" CrmId = '{0}' AND", mSearch.CrmId);
            }
            else if (!string.IsNullOrEmpty(mSearch.CrmName))
            {
                query.AppendFormat(" Crm LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.CrmName));
            }
            if (!string.IsNullOrEmpty(mSearch.ShenQingRenId))
            {
                query.AppendFormat(" ApplierId = '{0}' AND", mSearch.ShenQingRenId);
            }
            else if (!string.IsNullOrEmpty(mSearch.ShenQingRenName))
            {
                query.AppendFormat(" Applier LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.ShenQingRenName));
            }
            if (mSearch.SShenQingTime.HasValue)
            {
                query.AppendFormat(" ApplyTime>='{0}' AND ", mSearch.SShenQingTime.Value);
            }
            if (mSearch.EShenQingTime.HasValue)
            {
                query.AppendFormat(" ApplyTime<'{0}' AND ", mSearch.EShenQingTime.Value.AddDays(1));
            }
            if (mSearch.Status.HasValue)
            {
                query.AppendFormat(" IsApprove={0} AND ", (int)mSearch.Status.Value);
            }

            if (!string.IsNullOrEmpty(mSearch.ShenPiRenId))
            {
                query.AppendFormat(" ApproverId='{0}' AND ", mSearch.ShenPiRenId);
            }
            else if (!string.IsNullOrEmpty(mSearch.ShenPiRenName))
            {
                query.AppendFormat(" Approver LIKE '%{0}%' AND ", mSearch.ShenPiRenName);
            }
            if (mSearch.ShenPiSTime.HasValue)
            {
                query.AppendFormat(" ApproveTime>='{0}' AND ", mSearch.ShenPiSTime.Value);
            }
            if (mSearch.ShenPiETime.HasValue)
            {
                query.AppendFormat(" ApproveTime<'{0}' AND ", mSearch.ShenPiETime.Value.AddDays(1));
            }

            query.AppendFormat(" CompanyId = '{0}'", mSearch.CompanyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "tbl_FinDisburseApply", "DisburseId", "DisburseId,RouteName,Applier,DisburseAmount,CrmId,Crm,ItemId,ItemType,IsApprove,TourId,TourType,OrderId,ApplyTime,Approver,ApproveTime"
                , query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MTransfinite
                    {
                        DisburseId = dr["DisburseId"].ToString(),
                        RouteName = dr.IsDBNull(dr.GetOrdinal("RouteName")) ? "" : dr.GetString(dr.GetOrdinal("RouteName")),
                        Applier = dr.IsDBNull(dr.GetOrdinal("Applier")) ? "" : dr.GetString(dr.GetOrdinal("Applier")),
                        DisburseAmount = dr.GetDecimal(dr.GetOrdinal("DisburseAmount")),
                        CrmId = dr["CrmId"].ToString(),
                        Crm = dr["Crm"].ToString(),
                        ItemId = dr["ItemId"].ToString(),
                        ItemType = (TransfiniteType)dr.GetByte(dr.GetOrdinal("ItemType")),
                        IsApprove = (TransfiniteStatus)dr.GetByte(dr.GetOrdinal("IsApprove"))
                    };
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.OrderId = dr["OrderId"].ToString();
                    mdl.TourType = Utils.GetEnumValue<TourType>(dr["TourType"].ToString(), TourType.组团团队);
                    mdl.ApplyTime = dr.GetDateTime(dr.GetOrdinal("ApplyTime"));
                    mdl.Approver = dr["Approver"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ApproveTime"))) mdl.ApproveTime = dr.GetDateTime(dr.GetOrdinal("ApproveTime"));
                    lst.Add(mdl);
                }
            }
            return lst;
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
                                    , TransfiniteStatus status
                                    , string itemId
                                    , TransfiniteType itemType)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE  [tbl_FinDisburseApply]");
            strSql.Append(" SET     [IsApprove] = @IsApprove ,");
            strSql.Append("         [ApproverId] = @ApproverId ,");
            strSql.Append("         [Approver] = @Approver ,");
            strSql.Append("         [ApproveTime] = @ApproveTime ,");
            strSql.Append("         [ApproveRemark] = @ApproveRemark");
            strSql.Append(" WHERE   [DisburseId] = @DisburseId");

            switch (itemType)
            {
                case TransfiniteType.报价成功:
                    //strSql.AppendFormat(" update tbl_quote set QuoteStatus={0} where quoteid=@itemid", status==TransfiniteStatus.通过 ? (int)QuoteState.审核成功 : (int)QuoteState.审核失败);
                    break;
                case TransfiniteType.成团:
                    strSql.AppendFormat(" update tbl_tour set TourStatus={0} where tourid=@itemid", status == TransfiniteStatus.通过 ? (int)TourStatus.销售未派计划 : (int)TourStatus.审核失败);
                    strSql.Append(" INSERT  INTO [dbo].[tbl_TourStatusChange]");
                    strSql.Append("         ( [TourId] ,");
                    strSql.Append("           [CompanyId] ,");
                    strSql.Append("           [TourStatus] ,");
                    strSql.Append("           [Operator] ,");
                    strSql.Append("           [DeptId], ");
                    strSql.Append("           [OperatorId] ");
                    strSql.Append("         )");
                    strSql.Append(" VALUES  ( @ItemId ,");
                    strSql.Append("           @CompanyId ,");
                    strSql.AppendFormat("           {0} ,", status == TransfiniteStatus.通过 ? (int)TourStatus.销售未派计划 : (int)TourStatus.审核失败);
                    strSql.Append("           @Approver ,");
                    strSql.Append("           @DeptId, ");
                    strSql.Append("           @ApproverId ");
                    strSql.Append("         )");
                    strSql.AppendFormat(string.Format(" update tbl_tourorder set Status={0} where tourid=@itemid", status == TransfiniteStatus.通过 ? (int)OrderStatus.已成交 : (int)OrderStatus.垫付申请审核失败));
                    strSql.Append(" EXEC dbo.proc_WeiHuTourPeopleNumber @itemid,@Result = 0 ");
                    break;
                case TransfiniteType.报名:
                    strSql.AppendFormat(" update tbl_tourorder set status={0} where orderid=@itemid", status == TransfiniteStatus.通过 ? (int)OrderStatus.已成交 : (int)OrderStatus.垫付申请审核失败);
                    strSql.Append(" DECLARE @TourId CHAR(36)");
                    strSql.Append(" SET @TourId=(SELECT TourId FROM dbo.tbl_TourOrder WHERE OrderId=@ItemId)");
                    strSql.Append(" EXEC dbo.proc_WeiHuTourPeopleNumber @TourId,@Result = 0");
                    break;
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@DisburseId", DbType.AnsiStringFixedLength, disburseId);
            this._db.AddInParameter(dc, "@IsApprove", DbType.Byte, (int)status);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, approverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, approveTime);
            this._db.AddInParameter(dc, "@ApproveRemark", DbType.String, approveRemark);
            this._db.AddInParameter(dc, "@ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, deptId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSqlTrans(dc, this._db) > 0;
        }*/

        /// <summary>
        /// 超限审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChaoXianShenPi(EyouSoft.Model.FinStructure.MChaoXianShenPiInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_Fin_ChaoXianShenPi");
            _db.AddInParameter(cmd, "ChaoXianShenQingId", DbType.AnsiStringFixedLength, info.ChaoXianShenQingId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "YiJian", DbType.String, info.YiJian);
            _db.AddInParameter(cmd, "Status", DbType.Byte, info.Status);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddOutParameter(cmd, "OrderId", DbType.String, 36);
            _db.AddOutParameter(cmd, "OrderJinE", DbType.Decimal, 8);
            _db.AddOutParameter(cmd, "CrmId", DbType.String, 36);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return sqlExceptionCode;

            int retcode = Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));

            if (retcode != 1) return retcode;

            info.OrderId = _db.GetParameterValue(cmd, "OrderId").ToString();
            info.OrderJinE = Convert.ToDecimal(_db.GetParameterValue(cmd, "OrderJinE"));
            info.CrmId = _db.GetParameterValue(cmd, "CrmId").ToString();

            return retcode;
        }

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
        public IList<MReconciliation> GetReconciliationLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , ReconciliationType typ
                                                        , string companyId
                                                        , MAuditBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            const string @orderby = "IssueTime DESC";
            var lst = new List<MReconciliation>();
            var field = new StringBuilder();
            var query = new StringBuilder();
            var table = string.Empty;
            var sumfield = new[] { "Amount" };

            field.Append("TourCode,RouteName,CrmId,Crm,Contact,Phone,SellerName,Amount,Operator,IssueTime");
            query.AppendFormat("CompanyId = '{0}'", companyId);
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.IssueTimeS))
                {
                    query.AppendFormat(" AND CONVERT(VARCHAR(19),IssueTime,111) >= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)", mSearch.IssueTimeS);
                }
                if (!string.IsNullOrEmpty(mSearch.IssueTimeE))
                {
                    query.AppendFormat(" AND CONVERT(VARCHAR(19),IssueTime,111) <= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)", mSearch.IssueTimeE);
                }
                if (string.IsNullOrEmpty(mSearch.IssueTimeS) && string.IsNullOrEmpty(mSearch.IssueTimeE))
                {
                    query.Append(" AND CONVERT(VARCHAR(10),IssueTime,101)=CONVERT(VARCHAR(10),GETDATE(),101)");
                }
                if (mSearch.Amount.HasValue&&mSearch.SignAmount.HasValue)
                {
                    query.AppendFormat(" AND Amount {0} {1}", GetEqualSign(mSearch.SignAmount), mSearch.Amount.Value);
                }
                if (mSearch.PlanItem.HasValue)
                {
                    query.AppendFormat(" AND Type = {0}", (int)mSearch.PlanItem.Value);
                }
                if (!string.IsNullOrEmpty(mSearch.CrmId))
                {
                    query.AppendFormat(" AND CrmId = '{0}'", mSearch.CrmId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Crm))
                {
                    query.AppendFormat(" AND Crm LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Crm));
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" AND SellerId = '{0}'", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" AND SellerName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (!string.IsNullOrEmpty(mSearch.PlanerId))
                {
                    query.AppendFormat(" AND PlanerId = '{0}'", mSearch.PlanerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Planer))
                {
                    query.AppendFormat(" AND Planer LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Planer));
                }
                if (!string.IsNullOrEmpty(mSearch.TourCode))
                {
                    query.AppendFormat(" AND TourCode LIKE '%{0}%'", Utils.ToSqlLike(mSearch.TourCode));
                }
            }

            switch (typ)
            {
                case ReconciliationType.今日收款:
                    table = "view_TodayReceivedTotal";
                    field.Append(",OrderCode");
                    break;
                case ReconciliationType.今日付款:
                    table = "view_TodayPaidTotal";
                    field.Append(",Type,Planer");
                    break;
                case ReconciliationType.今日应收:
                    table = "view_TodayReceivable";
                    field.Append(",OrderCode");
                    break;
                case ReconciliationType.今日应付:
                    table = "view_TodayPayable";
                    field.Append(",Type,Planer");
                    break;
            }

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), orderby))
            {
                while (dr.Read())
                {
                    var mdl = new MReconciliation
                        {
                            TourCode = dr["TourCode"].ToString(),
                            RouteName = dr["RouteName"].ToString(),
                            CrmId = dr["CrmId"].ToString(),
                            Crm = dr["Crm"].ToString(),
                            Contact = dr["Contact"].ToString(),
                            Phone = dr["Phone"].ToString(),
                            SellerName = dr["SellerName"].ToString(),
                            Amount = dr.GetDecimal(dr.GetOrdinal("Amount")),
                            Operator = dr["Operator"].ToString(),
                            IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                        };
                    if (typ == ReconciliationType.今日付款 || typ == ReconciliationType.今日应付)
                    {
                        mdl.PlanItem = dr.IsDBNull(dr.GetOrdinal("Type"))?null: (PlanProject?)dr.GetByte(dr.GetOrdinal("Type"));
                        mdl.Planer = dr["Planer"].ToString();
                    }
                    else
                    {
                        mdl.OrderCode = dr["OrderCode"].ToString();
                    }
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MProfitStatistics> GetProfitStatisticsLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , string companyId
                                                        , MProfitStatisticsBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            var lst = new List<MProfitStatistics>();
            var query = new StringBuilder();
            var sumfield = new[] { "Income", "Outlay", "PeopleNum" };

            query.AppendFormat(" CompanyId = '{0}'", companyId);
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.LDateS))
                {
                    query.AppendFormat(" AND LDate >= '{0}'", mSearch.LDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.LDateE))
                {
                    query.AppendFormat(" AND LDate <= '{0}'", mSearch.LDateE);
                }
                if (!string.IsNullOrEmpty(mSearch.IssueTimeS))
                {
                    query.AppendFormat(" AND CONVERT(VARCHAR(19),IssueTime,111) >= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)", mSearch.IssueTimeS);
                }
                if (!string.IsNullOrEmpty(mSearch.IssueTimeE))
                {
                    query.AppendFormat(" AND CONVERT(VARCHAR(19),IssueTime,111) <= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)", mSearch.IssueTimeE);
                }
                if (!string.IsNullOrEmpty(mSearch.CrmId))
                {
                    query.AppendFormat(" AND BuyCompanyId = '{0}'", mSearch.CrmId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Crm))
                {
                    query.AppendFormat(" AND BuyCompanyName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Crm));
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" AND SellerId = '{0}'", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" AND SellerName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (!string.IsNullOrEmpty(mSearch.PlanerId))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(PlanerId,',') WHERE [value]='{0}')", mSearch.PlanerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Planer))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(Planer,',') WHERE [value] LIKE '%{0}%')", Utils.ToSqlLike(mSearch.Planer));
                }
                if (!string.IsNullOrEmpty(mSearch.GuideId))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(GuideId,',') WHERE [value]='{0}')", mSearch.GuideId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Guide))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(Guide,',') WHERE [value] LIKE '%{0}%')", Utils.ToSqlLike(mSearch.Guide));
                }
                if (!string.IsNullOrEmpty(mSearch.Code))
                {
                    query.AppendFormat(" AND Code LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Code));
                }
                if (!string.IsNullOrEmpty(mSearch.RouteName))
                {
                    query.AppendFormat(" AND RouteName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.RouteName));
                }
            }
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , "view_ProfitStatistics", this.CreateXmlSumByField(sumfield), "Code,RouteName,BuyCompanyId,BuyCompanyName,ContactName,ContactTel,LDate,PeopleNum,SellerName,Planer,Guide,Income,Outlay,IssueTime"
                , query.ToString(), "LDate"))
            {
                while (dr.Read())
                {
                    var mdl = new MProfitStatistics
                    {
                        Code = dr["Code"].ToString(),
                        RouteName = dr["RouteName"].ToString(),
                        CrmId = dr["BuyCompanyId"].ToString(),
                        Crm = dr["BuyCompanyName"].ToString(),
                        ContactName = dr["ContactName"].ToString(),
                        ContactTel = dr["ContactTel"].ToString(),
                        LDate = dr.GetDateTime(dr.GetOrdinal("LDate")),
                        PeopleNum = dr.GetInt32(dr.GetOrdinal("PeopleNum")),
                        SellerName = dr["SellerName"].ToString(),
                        Planer = dr["Planer"].ToString(),
                        Guide = dr["Guide"].ToString(),
                        Income = dr.GetDecimal(dr.GetOrdinal("Income")),
                        Outlay = dr.GetDecimal(dr.GetOrdinal("Outlay")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MBudgetContrast> GetBudgetContrastLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , string companyId
                                                        , MBudgetContrastBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            var lst = new List<MBudgetContrast>();
            var query = new StringBuilder();
            var sumfield = new[] { "BudgetIncome", "BudgetOutgo", "ClearingIncome", "ClearingOutgo" };

            query.AppendFormat(" CompanyId = '{0}'", companyId);
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    query.AppendFormat(" AND SellerId = '{0}'", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    query.AppendFormat(" AND SellerName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (!string.IsNullOrEmpty(mSearch.LDateS))
                {
                    query.AppendFormat(" AND LDate >= '{0}'", mSearch.LDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.LDateE))
                {
                    query.AppendFormat(" AND LDate <= '{0}'", mSearch.LDateE);
                }
                if (!string.IsNullOrEmpty(mSearch.TourCode))
                {
                    query.AppendFormat(" AND TourCode LIKE '%{0}%'", Utils.ToSqlLike(mSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(mSearch.RouteName))
                {
                    query.AppendFormat(" AND RouteName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.RouteName));
                }
                if (!string.IsNullOrEmpty(mSearch.PlanerId))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(PlanerIds,',') WHERE [value]='{0}')", mSearch.PlanerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Planer))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.fn_split(Planers,',') WHERE [value] LIKE '%{0}%')", Utils.ToSqlLike(mSearch.Planer));
                }
                if (!string.IsNullOrEmpty(mSearch.CrmId))
                {
                    query.AppendFormat(" AND XMLCrm.exist('/root/row[@BuyCompanyId = \"{0}\"]')=1", mSearch.CrmId);
                }
                else if (!string.IsNullOrEmpty(mSearch.Crm))
                {
                    query.AppendFormat(" AND EXISTS(SELECT 1 FROM XMLCrm.nodes('root/row') AS T(Crm) WHERE T.Crm.value('@BuyCompanyName','nvarchar(100)') LIKE '%{0}%')", Utils.ToSqlLike(mSearch.Crm));
                }
            }
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , "view_BudgetContrast", this.CreateXmlSumByField(sumfield), "TourCode,RouteName,LDate,SellerName,XMLCrm,Planers,BudgetIncome,BudgetOutgo,ClearingIncome,ClearingOutgo"
                , query.ToString(), "LDate"))
            {
                while (dr.Read())
                {
                    var mdl = new MBudgetContrast
                    {
                        TourCode = dr.IsDBNull(dr.GetOrdinal("TourCode")) ? string.Empty : dr.GetString(dr.GetOrdinal("TourCode")),
                        RouteName = dr.IsDBNull(dr.GetOrdinal("RouteName")) ? string.Empty : dr.GetString(dr.GetOrdinal("RouteName")),
                        CrmId = Utils.GetValueFromXmlByAttribute(dr["XMLCrm"].ToString(), "BuyCompanyId"),
                        Crm = Utils.GetValueFromXmlByAttribute(dr["XMLCrm"].ToString(), "BuyCompanyName"),
                        Contact = Utils.GetValueFromXmlByAttribute(dr["XMLCrm"].ToString(), "ContactName"),
                        Phone = Utils.GetValueFromXmlByAttribute(dr["XMLCrm"].ToString(), "ContactTel"),
                        LDate = dr.GetDateTime(dr.GetOrdinal("LDate")),
                        SellerName = dr.IsDBNull(dr.GetOrdinal("SellerName")) ? string.Empty : dr.GetString(dr.GetOrdinal("SellerName")),
                        Planer = dr["Planers"].ToString(),
                        BudgetIncome = dr.GetDecimal(dr.GetOrdinal("BudgetIncome")),
                        BudgetOutgo = dr.GetDecimal(dr.GetOrdinal("BudgetOutgo")),
                        ClearingIncome = dr.GetDecimal(dr.GetOrdinal("ClearingIncome")),
                        ClearingOutgo = dr.GetDecimal(dr.GetOrdinal("ClearingOutgo")),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

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
        public IList<MDayRegister> GetDayRegisterLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , MDayRegisterBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds)
        {
            var lst = new List<MDayRegister>();
            var field = new StringBuilder();
            var query = new StringBuilder();
            var sumfield = new[] { "DebitCash", "DebitBank", "LenderCash", "LenderBank" };

            field.Append(" LDate,Summary,DebitCash,DebitBank,LenderCash,LenderBank");

            query.AppendFormat(" CompanyId = '{0}'", mSearch.CompanyId);
            if (mSearch.PaymentId.HasValue)
            {
                query.AppendFormat(" AND PaymentId = {0}", mSearch.PaymentId.Value);
            }
            if (mSearch.Amount.HasValue && mSearch.SignAmount.HasValue)
            {
                query.AppendFormat(" AND Amount {0} {1}", GetEqualSign(mSearch.SignAmount), mSearch.Amount.Value);
            }
            if (!string.IsNullOrEmpty(mSearch.LDateS))
            {
                query.AppendFormat(" AND CONVERT(VARCHAR(10),LDate,101) >= CONVERT(VARCHAR(10),CAST('{0}' AS DATETIME),101)", mSearch.LDateS);
            }
            if (!string.IsNullOrEmpty(mSearch.LDateE))
            {
                query.AppendFormat(" AND CONVERT(VARCHAR(10),LDate,101) <= CONVERT(VARCHAR(10),CAST('{0}' AS DATETIME),101)", mSearch.LDateE);
            }
            if (string.IsNullOrEmpty(mSearch.LDateS) && string.IsNullOrEmpty(mSearch.LDateE))
            {
                query.Append(" AND CONVERT(VARCHAR(10),LDate,101)=CONVERT(VARCHAR(10),GETDATE(),101)");
            }
            if (!string.IsNullOrEmpty(mSearch.Summary))
            {
                query.AppendFormat(" AND Summary LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Summary));
            }
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , "view_DayRegister", this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), "LDate DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MDayRegister
                    {
                        Summary = dr["Summary"].ToString(),
                        DebitCash = dr.GetDecimal(dr.GetOrdinal("DebitCash")),
                        DebitBank = dr.GetDecimal(dr.GetOrdinal("DebitBank")),
                        LenderCash = dr.GetDecimal(dr.GetOrdinal("LenderCash")),
                        LenderBank = dr.GetDecimal(dr.GetOrdinal("LenderBank"))
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                    {
                        mdl.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    }

                    lst.Add(mdl);
                }
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
        public bool AddOrUpdKingDeeProof(MKingDeeProof mdl)
        {
            var xml = new XmlDocument();
            var sbxml = new StringBuilder();

            var dc = this._db.GetStoredProcCommand("proc_FinKingDeeProof_AddOrUpd");

            sbxml.Append("<root>");
            foreach (var i in mdl.KingDeeProofDetailLst)
            {
                sbxml.Append("<row V1=\"" + i.FDetailId + "\" V2=\"" + i.FId + "\" V3=\"" + Utils.ReplaceXmlSpecialCharacter(i.FExplanation) + "\" V4=\"" + i.FAccountNum + "\" V5=\"" + i.FAccountName + "\" V6=\"" + i.FCurrencyNum + "\" V7=\"" + i.FCurrencyName + "\" V8=\"" + i.FAmountFor + "\" V9=\"" + i.FDebit + "\" V10=\"" + i.FCredit + "\" V11=\"" + i.FItem + "\" V12=\"" + i.FEntryId + "\" V13=\"" + (i.FIsExport ? 1 : 0) + "\"/>");
            }
            sbxml.Append("</root>");
            xml.LoadXml(sbxml.ToString());

            this._db.AddInParameter(dc, "@FId", DbType.AnsiStringFixedLength, mdl.FId);
            this._db.AddInParameter(dc, "@FCompanyId", DbType.AnsiStringFixedLength, mdl.FCompanyId);
            this._db.AddInParameter(dc, "@FTourId", DbType.AnsiStringFixedLength, mdl.FTourId);
            this._db.AddInParameter(dc, "@FItemType", DbType.Byte, (int)mdl.FItemType);
            this._db.AddInParameter(dc, "@FItemId", DbType.AnsiStringFixedLength, mdl.FItemId);
            this._db.AddInParameter(dc, "@FTourCode", DbType.String, mdl.FTourCode);
            this._db.AddInParameter(dc, "@FDate", DbType.DateTime, mdl.FDate);
            this._db.AddInParameter(dc, "@FYear", DbType.Decimal, mdl.FYear);
            this._db.AddInParameter(dc, "@FPeriod", DbType.Decimal, mdl.FPeriod);
            this._db.AddInParameter(dc, "@FGroupID", DbType.String, mdl.FGroupId);
            this._db.AddInParameter(dc, "@FNumber", DbType.Decimal, mdl.FNumber);
            this._db.AddInParameter(dc, "@FPreparerID", DbType.String, mdl.FPreparerId);
            this._db.AddInParameter(dc, "@FCheckerID", DbType.String, mdl.FCheckerId);
            this._db.AddInParameter(dc, "@FApproveID", DbType.String, mdl.FApproveId);
            this._db.AddInParameter(dc, "@FCashierID", DbType.String, mdl.FCashierId);
            this._db.AddInParameter(dc, "@FHandler", DbType.String, mdl.FHandler);
            this._db.AddInParameter(dc, "@FSettleTypeID", DbType.String, mdl.FSettleTypeId);
            this._db.AddInParameter(dc, "@FSettleNo", DbType.String, mdl.FSettleNo);
            this._db.AddInParameter(dc, "@FQuantity", DbType.Decimal, mdl.FQuantity);
            this._db.AddInParameter(dc, "@FMeasureUnitID", DbType.String, mdl.FMeasureUnitId);
            this._db.AddInParameter(dc, "@FUnitPrice", DbType.Decimal, mdl.FUnitPrice);
            this._db.AddInParameter(dc, "@FReference", DbType.String, mdl.FReference);
            this._db.AddInParameter(dc, "@FTransDate", DbType.DateTime, mdl.FTransDate);
            this._db.AddInParameter(dc, "@FTransNo", DbType.String, mdl.FTransNo);
            this._db.AddInParameter(dc, "@FAttachments", DbType.Decimal, mdl.FAttachments);
            this._db.AddInParameter(dc, "@FSerialNum", DbType.Decimal, mdl.FSerialNum);
            this._db.AddInParameter(dc, "@FObjectName", DbType.String, mdl.FObjectName);
            this._db.AddInParameter(dc, "@FParameter", DbType.String, mdl.FParameter);
            this._db.AddInParameter(dc, "@FExchangeRate", DbType.Decimal, mdl.FExchangeRate);
            this._db.AddInParameter(dc, "@FPosted", DbType.Decimal, mdl.FPosted);
            this._db.AddInParameter(dc, "@FInternalInd", DbType.String, mdl.FInternalInd);
            this._db.AddInParameter(dc, "@FCashFlow", DbType.String, mdl.FCashFlow);
            this._db.AddInParameter(dc, "@XMLDetail", DbType.Xml, xml.InnerXml);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId",DbType.AnsiStringFixedLength,mdl.OperatorId);

            return DbHelper.RunProcedureWithResult(dc, this._db) == 0;
        }

        /// <summary>
        /// 根据金蝶财务编号获取金蝶凭证实体
        /// </summary>
        /// <param name="fId">金蝶财务编号</param>
        /// <returns>金蝶凭证实体</returns>
        public MKingDeeProof GetKingDeeProofMdl(string fId)
        {
            var strSql = new StringBuilder();

            strSql.Append(SQL_SELECT_KINGDEEPROOF);
            strSql.Append(" WHERE   FId = @FId");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@FId", DbType.AnsiStringFixedLength, fId);

            return GetKingDeeProofMdl(dc, this._db);
        }

        /// <summary>
        /// 根据团队编号获取金蝶凭证实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>金蝶凭证实体</returns>
        public IList<MKingDeeProof> GetKingDeeProofLstByTourId(string tourId)
        {
            var strSql = new StringBuilder();

            strSql.Append(SQL_SELECT_KINGDEEPROOF);
            strSql.Append(" WHERE   FTourId = @FTourId");
            strSql.AppendFormat("   AND EXISTS(SELECT 1 FROM tbl_Tour WHERE TourId=FTourId AND IsDelete='0' AND ISNULL(ParentId,'')<>'' AND TourStatus <> {0})", (int)TourStatus.已取消);

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@FTourId", DbType.AnsiStringFixedLength, tourId);

            return GetKingDeeProofLst(dc, this._db);
        }

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
        public IList<MKingDeeProofDetail> GetKingDeeProofLst(int pageSize
                                                            , int pageIndex
                                                            , ref int recordCount
                                                            , string companyId
                                                            , MKingDeeProofBase mSearch
                                                            , string operatorId
                                                            , params int[] deptIds)
        {
            var lst = new List<MKingDeeProofDetail>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append(" FDate=(SELECT FTourCode,CONVERT(VARCHAR(19),FDate,120) FDate FROM tbl_FinKingDeeProof WHERE tbl_FinKingDeeProof.FId=tbl_FinKingDeeProofDetail.FId FOR XML RAW,ROOT)");
            field.Append(" ,FExplanation,FAccountNum,FAccountName,FDebit,FCredit,FIsExport");

            query.Append(" EXISTS(SELECT 1 FROM tbl_FinKingDeeProof WHERE");
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.FTourCode))
                {
                    query.AppendFormat(" FTourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.FTourCode));
                }
                if (!string.IsNullOrEmpty(mSearch.FDateS))
                {
                    query.AppendFormat(" FDate >= '{0}' AND", mSearch.FDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.FDateE))
                {
                    query.AppendFormat(" FDate <= '{0}' AND", mSearch.FDateE);
                }
            }
            //query.AppendFormat(" EXISTS(SELECT 1 FROM tbl_Tour WHERE TourId=FTourId AND IsDelete='0' AND ISNULL(ParentId,'')<>'' AND TourStatus <> {0}) AND", (int)TourStatus.已取消);
            query.AppendFormat(" tbl_FinKingDeeProof.FId=tbl_FinKingDeeProofDetail.FId AND FCompanyId='{0}'", companyId);
            //query.Append(GetOrgCondition(operatorId, deptIds));
            query.Append(" )");
            if (mSearch != null)
            {
                query.AppendFormat(" AND FIsExport = '{0}'", mSearch.FIsExport ? "1" : "0");
            }

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, "tbl_FinKingDeeProofDetail", "FDetailId", field.ToString(), query.ToString(), "FDetailId DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MKingDeeProofDetail
                        {
                            FTourCode = Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FTourCode"),
                            FDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FDate")),
                            FExplanation = dr["FExplanation"].ToString(),
                            FAccountNum = dr["FAccountNum"].ToString(),
                            FAccountName = dr["FAccountName"].ToString(),
                            FDebit = dr.GetDecimal(dr.GetOrdinal("FDebit")),
                            FCredit = dr.GetDecimal(dr.GetOrdinal("FCredit")),
                            FIsExport = dr["FIsExport"].ToString() == "1",
                        });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据系统公司编号获取未导出的金蝶凭证列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>金蝶凭证列表</returns>
        public IList<MKingDeeProofDetail> GetKingDeeProofLst(string companyId, MKingDeeProofBase mSearch, string operatorId, params int[] deptIds)
        {
            var lst = new List<MKingDeeProofDetail>();
            var sql = new StringBuilder();

            sql.Append(" select FDate=(SELECT FTourCode,CONVERT(VARCHAR(19),FDate,120) FDate,FYear,FPeriod,FGroupID,FNumber,CONVERT(VARCHAR(19),FTransDate,120) FTransDate,FPreparerId,FAmountFor FROM tbl_FinKingDeeProof WHERE tbl_FinKingDeeProof.FId=tbl_FinKingDeeProofDetail.FId FOR XML RAW,ROOT)");
            sql.Append(" ,FExplanation,FAccountNum,FAccountName,FDebit,FCredit,FIsExport,FEntryID,FCurrencyNum,FCurrencyName,FItem");
            sql.Append(" from tbl_FinKingDeeProofDetail");
            sql.Append(" where EXISTS(SELECT 1 FROM tbl_FinKingDeeProof WHERE");
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.FTourCode))
                {
                    sql.AppendFormat(" FTourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.FTourCode));
                }
                if (!string.IsNullOrEmpty(mSearch.FDateS))
                {
                    sql.AppendFormat(" FDate >= '{0}' AND", mSearch.FDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.FDateE))
                {
                    sql.AppendFormat(" FDate <= '{0}' AND", mSearch.FDateE);
                }
            }
            //query.AppendFormat(" EXISTS(SELECT 1 FROM tbl_Tour WHERE TourId=FTourId AND IsDelete='0' AND ISNULL(ParentId,'')<>'' AND TourStatus <> {0}) AND", (int)TourStatus.已取消);
            sql.AppendFormat(" tbl_FinKingDeeProof.FId=tbl_FinKingDeeProofDetail.FId AND FCompanyId='{0}'", companyId);
            //sql.Append(GetOrgCondition(operatorId, deptIds));
            sql.Append(" )");
            sql.Append(" AND FIsExport = '0'");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MKingDeeProofDetail
                    {
                        FTourCode = Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FTourCode"),
                        FDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FDate")),
                        FYear = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FYear")),
                        FPeriod = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FPeriod")),
                        FNumber = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FNumber")),
                        FTransDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FTransDate")),
                        FGroupId = Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FGroupID"),
                        FPreparerId = Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FPreparerId"),
                        FAmountFor = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["FDate"].ToString(), "FAmountFor")),
                        FCurrencyNum = dr["FCurrencyNum"].ToString(),
                        FCurrencyName = dr["FCurrencyName"].ToString(),
                        FExplanation = dr["FExplanation"].ToString(),
                        FAccountNum = dr["FAccountNum"].ToString(),
                        FAccountName = dr["FAccountName"].ToString(),
                        FDebit = dr.GetDecimal(dr.GetOrdinal("FDebit")),
                        FCredit = dr.GetDecimal(dr.GetOrdinal("FCredit")),
                        FIsExport = dr["FIsExport"].ToString() == "1",
                        FEntryId = dr.GetDecimal(dr.GetOrdinal("FEntryID")),
                        FItem = dr["FItem"].ToString()
                    });
                }
            }
            return lst;
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
            var lst = new List<MKingDeeProofDetail>();
            var sql = new StringBuilder();
            var isFinIn = this.IsFinIn(type, itemId, companyId);

            //判断是否已财务入账
            if (!isFinIn && type!=DefaultProofType.杂费收入&&type!=DefaultProofType.杂费支出)
            {
                sql.Append(SQL_SELECT_KINGDEESUBJECT);

                switch (type)
                {
                    case DefaultProofType.订单收款:
                        sql.Append(GetOrderSalesProofSql(this.IsFt(true, itemId, companyId)));
                        break;
                    case DefaultProofType.计调付款:
                        sql.Append(GetPrepaidProofSql(this.IsFt(false, itemId, companyId)));
                        break;
                    case DefaultProofType.导游借款:
                        sql.Append(GetGuideDebitProofSql());
                        break;
                    case DefaultProofType.单团核算:
                        sql.Append(GetTourChkProofSql());
                        break;
                    case DefaultProofType.团未完导游先报账:
                        sql.Append(GetGuideBzProofSql());
                        break;
                    case DefaultProofType.后期收款:
                        sql.Append(GetAfterProofSql());
                        break;
                }
            }
            else
            {
                sql.Append(" SELECT  FExplanation N'摘要' ,");
                sql.Append("         FAccountNum N'科目代码' ,");
                sql.Append("         FAccountName N'科目名称' ,");
                sql.Append("         FItem N'核算科目' ,");
                sql.Append("         FDebit N'借方' ,");
                sql.Append("         FCredit N'贷方' ,");
                sql.Append("         FIsExport ,");
                sql.Append("         FDetailId ,");
                sql.Append("         D.FId ,");
                sql.Append("         XMLItem = ( SELECT ChkId ,");
                sql.Append("                             ChkCd ,");
                sql.Append("                             ChkCate ,");
                sql.Append("                             ChkNm ,");
                sql.Append("                             PreChkNm");
                sql.Append("                     FROM    dbo.view_KingdeeChkItem");
                sql.Append("                     WHERE   D.FItem LIKE '%---' + ChkCd + '---%'");
                sql.Append("                             AND D.FItem LIKE '%---' + ChkNm + '%'");
                sql.Append("                             AND CompanyId = P.FCompanyId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                   )");
                sql.Append(" FROM   dbo.tbl_FinKingDeeProofDetail D");
                sql.Append("        INNER JOIN dbo.tbl_FinKingDeeProof P ON D.FId = P.FId");
                sql.Append("                                                AND P.FItemId = @ItemId");
                sql.Append("                                                AND P.FItemType = @ItemType");
                sql.Append("                                                AND P.FCompanyId = @CompanyId");
            }

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(dc, "@ItemType", DbType.Byte, type);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MKingDeeProofDetail
                        {
                            FExplanation = dr["摘要"].ToString(),
                            FAccountNum = dr["科目代码"].ToString(),
                            FAccountName = dr["科目名称"].ToString(),
                            FItem = dr["核算科目"].ToString(),
                            FDebit = dr.GetDecimal(dr.GetOrdinal("借方")),
                            FCredit = dr.GetDecimal(dr.GetOrdinal("贷方")),
                            FIsExport = !isFinIn ? false : dr["FIsExport"].ToString() == "1",
                            FDetailId = !isFinIn ? 0 : dr.GetInt32(dr.GetOrdinal("FDetailId")),
                            FId = !isFinIn ? string.Empty : dr["FId"].ToString(),
                            FItemList = GetKingDeeChkLstByXml(dr["XMLItem"].ToString())
                        });
                }
            }

            return lst;
        }

        /// <summary>
        /// 设置凭证导出状态
        /// </summary>
        /// <param name="id">凭证详细编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetProofExport(int id)
        {
            var sql = string.Format("UPDATE tbl_FinKingDeeProofDetail SET FIsExport='1' WHERE FDetailId = {0}", id);
            var dc = this._db.GetSqlStringCommand(sql);
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据公司编号设置所有凭证的导出状态
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetProofExport(string companyId)
        {
            var sql = "UPDATE tbl_FinKingDeeProofDetail SET FIsExport='1' WHERE FId IN (SELECT FId FROM dbo.tbl_FinKingDeeProof WHERE FId=dbo.tbl_FinKingDeeProofDetail.FId AND FCompanyId=@CompanyId)";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.ExecuteSqlTrans(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        public IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId)
        {
            var strSql = new StringBuilder();

            strSql.Append(SQL_SELECT_KINGDEESUBJECT);
            strSql.Append(" SELECT * FROM CTE WHERE PreSubjectId=0");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return GetKingDeeSubjectLst(dc, this._db); ;
        }

        /// <summary>
        /// 根据系统公司编号和上级科目编号获取金蝶科目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preSubjectId">上级科目编号</param>
        /// <returns></returns>
        public IList<MKingDeeSubject> GetKingDeeSubjectLst(string companyId, int preSubjectId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  [SubjectId] ,");
            strSql.Append("         [PreSubjectId] ,");
            strSql.Append("         [SubjectCd] ,");
            strSql.Append("         [SubjectTyp] ,");
            strSql.Append("         [MnemonicCd] ,");
            strSql.Append("         [SubjectNm] ,");
            strSql.Append("         [ChkId],'' AS ChkItem,'' AS XMLItem,PreSubjectNm=(SELECT SubjectNm FROM tbl_FinKingDeeSubject S WHERE S.SubjectId=tbl_FinKingDeeSubject.PreSubjectId)");
            strSql.Append(" FROM    [dbo].[tbl_FinKingDeeSubject]");
            strSql.Append(" WHERE   CompanyId = @CompanyId");
            strSql.Append("         AND PreSubjectId = @PreSubjectId");
            strSql.Append("         AND IsDeleted = '0'");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@PreSubjectId", DbType.Int32, preSubjectId);

            return GetKingDeeSubjectLst(dc, this._db); ;
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
            var lst = new List<MKingDeeSubject>();

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, "view_KingdeeSubject", "SubjectId", "SubjectId,SubjectCd,SubjectTyp,MnemonicCd,SubjectNm,ChkItem,PreSubjectNm", "CompanyId = '" + companyId + "'", "SubjectCd"))
            {
                while (dr.Read())
                {
                    lst.Add(new MKingDeeSubject
                    {
                        SubjectId = dr.GetInt32(dr.GetOrdinal("SubjectId")),
                        SubjectCd = dr["SubjectCd"].ToString(),
                        SubjectTyp = (FinanceAccountItem)dr.GetByte(dr.GetOrdinal("SubjectTyp")),
                        MnemonicCd = dr["MnemonicCd"].ToString(),
                        SubjectNm = dr["SubjectNm"].ToString(),
                        ChkItem = dr["ChkItem"].ToString(),
                        PreSubjectNm = dr["PreSubjectNm"].ToString()
                    });
                }
            }

            return lst;
        }

        /// <summary>
        /// 根据系统公司编号和科目编号获取金蝶科目实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>科目实体</returns>
        public MKingDeeSubject GetKingDeeSubjectMdl(string companyId, int subjectId)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  [SubjectId] ,");
            sql.Append("         [PreSubjectId] ,");
            sql.Append("         [SubjectCd] ,");
            sql.Append("         [SubjectTyp] ,");
            sql.Append("         [MnemonicCd] ,");
            sql.Append("         [SubjectNm] ,");
            sql.Append("         [ChkId] ,");
            sql.Append("         ChkItem = STUFF(( SELECT    ',' + ChkNm");
            sql.Append("               FROM      ( SELECT    ChkNm");
            sql.Append("                           FROM      dbo.tbl_FinKingDeeChk");
            sql.Append("                           WHERE     ChkId IN (");
            sql.Append("                                     SELECT  [value]");
            sql.Append("                                     FROM    dbo.fn_split(dbo.tbl_FinKingDeeSubject.ChkId,");
            sql.Append("                                                   ',') )");
            sql.Append("                         ) AS A");
            sql.Append("             FOR");
            sql.Append("               XML PATH('')");
            sql.Append("             ), 1, 1, '') ,");
            sql.Append("         XMLItem = (SELECT ChkCate,ChkCd,ChkNm FROM dbo.tbl_FinKingDeeChk WHERE ChkId IN (SELECT [value] FROM dbo.fn_split(dbo.tbl_FinKingDeeSubject.ChkId,',')) FOR XML RAW,ROOT) ,");
            sql.Append("         PreSubjectNm = ( SELECT SubjectNm");
            sql.Append("              FROM   tbl_FinKingDeeSubject S");
            sql.Append("              WHERE  S.SubjectId = tbl_FinKingDeeSubject.PreSubjectId");
            sql.Append("            )");
            sql.Append(" FROM    [dbo].[tbl_FinKingDeeSubject]");
            sql.Append(" WHERE   CompanyId = @CompanyId");
            sql.Append("         AND SubjectId = @SubjectId");
            sql.Append("         AND IsDeleted = '0'");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@SubjectId", DbType.Int32, subjectId);

            return GetKingDeeSubjectMdl(dc, this._db); ;
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IList<MKingDeeChk> GetKingDeeChkLst(string companyId)
        {
            var strSql = new StringBuilder();

            strSql.Append(SQL_SELECT_KINGDEECHK);
            strSql.Append(" SELECT * FROM CHKCTE WHERE PreChkId=0");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return GetKingDeeChkLst(dc, this._db); ;
        }

        /// <summary>
        /// 根据系统公司编号获取金蝶核算项目列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="preChkId">上一级核算项目编号</param>
        /// <returns></returns>
        public IList<MKingDeeChk> GetKingDeeChkLst(string companyId, string preChkId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  [ChkId] ,");
            strSql.Append("         [ChkCate] ,");
            strSql.Append("         [PreChkId] ,");
            strSql.Append("         [ChkCd] ,");
            strSql.Append("         [ChkNm] ,");
            strSql.Append("         [MnemonicCd] ,");
            strSql.Append("         [ItemId] ,");
            strSql.Append("         PreChkNm = ( SELECT C.ChkNm");
            strSql.Append("                      FROM   dbo.tbl_FinKingDeeChk C");
            strSql.Append("                      WHERE  C.ChkId = dbo.tbl_FinKingDeeChk.PreChkId");
            strSql.Append("                    )");
            strSql.Append(" FROM    [dbo].[tbl_FinKingDeeChk]");
            strSql.Append(" WHERE   CompanyId = @CompanyId");
            strSql.Append("         AND PreChkId = @PreChkId");
            strSql.Append("         AND IsDeleted = '0'");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@PreChkId", DbType.Int32, preChkId);

            return GetKingDeeChkLst(dc, this._db); ;
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
            var lst = new List<MKingDeeChk>();

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, "view_KingdeeChkItem", "ChkId", "ChkId,ChkCd,ChkCate,MnemonicCd,ChkNm,PreChkNm", "CompanyId = '" + companyId + "'", "ChkCd"))
            {
                while (dr.Read())
                {
                    lst.Add(new MKingDeeChk
                    {
                        ChkId = dr.GetInt32(dr.GetOrdinal("ChkId")),
                        ChkCd = dr["ChkCd"].ToString(),
                        ChkCate = (FinanceAccountItem)dr.GetByte(dr.GetOrdinal("ChkCate")),
                        MnemonicCd = dr["MnemonicCd"].ToString(),
                        ChkNm = dr["ChkNm"].ToString(),
                        PreChkNm = dr["PreChkNm"].ToString()
                    });
                }
            }

            return lst;
        }

        /// <summary>
        /// 根据系统公司编号和核算项目编号获取金蝶核算项目实体
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chkId"></param>
        /// <returns></returns>
        public MKingDeeChk GetKingDeeChkMdl(string companyId, int chkId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" SELECT  [ChkId] ,");
            strSql.Append("         [ChkCate] ,");
            strSql.Append("         [PreChkId] ,");
            strSql.Append("         [ChkCd] ,");
            strSql.Append("         [ChkNm] ,");
            strSql.Append("         [MnemonicCd] ,");
            strSql.Append("         [ItemId] ,");
            strSql.Append("         PreChkNm = ( SELECT C.ChkNm");
            strSql.Append("                      FROM   dbo.tbl_FinKingDeeChk C");
            strSql.Append("                      WHERE  C.ChkId = dbo.tbl_FinKingDeeChk.PreChkId");
            strSql.Append("                    )");
            strSql.Append(" FROM    [dbo].[tbl_FinKingDeeChk]");
            strSql.Append(" WHERE   CompanyId = @CompanyId");
            strSql.Append("         AND ChkId = @ChkId");
            strSql.Append("         AND IsDeleted = '0'");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@ChkId", DbType.Int32, chkId);

            return GetKingDeeChkMdl(dc, this._db); ;
        }

        /// <summary>
        /// 根据金蝶科目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶科目实体</param>
        /// <returns>0：成功 1：失败 2：科目代码已存在</returns>
        public int AddOrUpdKingDeeSubject(MKingDeeSubject mdl)
        {
            var dc = this._db.GetStoredProcCommand("proc_FinKingDeeSubject_AddOrUpd");

            this._db.AddInParameter(dc, "@SubjectId", DbType.Int32, mdl.SubjectId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@PreSubjectId", DbType.Int32, mdl.PreSubjectId);
            this._db.AddInParameter(dc, "@SubjectCd", DbType.String, mdl.SubjectCd);
            this._db.AddInParameter(dc, "@SubjectTyp", DbType.Byte, (int)mdl.SubjectTyp);
            this._db.AddInParameter(dc, "@MnemonicCd", DbType.String, mdl.MnemonicCd);
            this._db.AddInParameter(dc, "@SubjectNm", DbType.String, mdl.SubjectNm);
            this._db.AddInParameter(dc, "@ChkId", DbType.String, mdl.ChkId);
            this._db.AddInParameter(dc, "@AccountId", DbType.Int32, mdl.AccountId);
            this._db.AddInParameter(dc, "@ItemId", DbType.AnsiStringFixedLength, mdl.ItemId);

            return DbHelper.RunProcedureWithResult(dc, this._db);
        }

        /// <summary>
        /// 根据金蝶科目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelKingDeeSubject(string companyId, int subjectId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinKingDeeSubject SET IsDeleted='1'");
            strSql.Append(" WHERE SubjectId = @SubjectId AND CompanyId=@CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@SubjectId", DbType.Int32, subjectId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据系统公司编号和科目编号判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="subjectId">科目编号</param>
        /// <returns>True：已使用 False：未使用</returns>
        public bool IsSubjectUsed(string companyId, int subjectId)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT TOP 1 D.FDetailId");
            sql.Append(" FROM tbl_FinKingDeeProofDetail D");
            sql.Append(" INNER JOIN tbl_FinKingDeeProof M");
            sql.Append(" ON D.FId = M.FId AND M.FCompanyId = @CompanyId");
            sql.Append(" INNER JOIN tbl_FinKingDeeSubject S");
            sql.Append(" ON D.FAccountNum = S.SubjectCd AND M.FCompanyId = S.CompanyId AND S.SubjectId = @SubjectId");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@SubjectId", DbType.Int32, subjectId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.Exists(dc, this._db);
        }

        /// <summary>
        /// 根据金蝶核算项目实体添加或修改
        /// </summary>
        /// <param name="mdl">金蝶核算项目实体</param>
        /// <returns>0：成功 1：失败 2：核算项目代码已存在</returns>
        public int AddOrUpdKingDeeChk(MKingDeeChk mdl)
        {
            var dc = this._db.GetStoredProcCommand("proc_FinKingDeeChk_AddOrUpd");

            this._db.AddInParameter(dc, "@ChkId", DbType.Int32, mdl.ChkId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@ChkCate", DbType.Byte, (int)mdl.ChkCate);
            this._db.AddInParameter(dc, "@PreChkId", DbType.Int32, mdl.PreChkId);
            this._db.AddInParameter(dc, "@ChkCd", DbType.String, mdl.ChkCd);
            this._db.AddInParameter(dc, "@ChkNm", DbType.String, mdl.ChkNm);
            this._db.AddInParameter(dc, "@MnemonicCd", DbType.String, mdl.MnemonicCd);
            this._db.AddInParameter(dc, "@ItemId", DbType.AnsiStringFixedLength, mdl.ItemId);

            return DbHelper.RunProcedureWithResult(dc, this._db);
        }

        /// <summary>
        /// 根据金蝶核算项目编号删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelKingDeeChk(string companyId, int chkId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinKingDeeChk SET IsDeleted='1'");
            strSql.Append(" WHERE ChkId = @ChkId AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@ChkId", DbType.Int32, chkId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据系统公司编号和核算项目编号判断是否已使用
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="chkId">核算项目编号</param>
        /// <returns>True：已使用 False：未使用</returns>
        public bool IsChkItemUsed(string companyId, int chkId)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT TOP 1 SubjectId");
            sql.Append(" FROM tbl_FinKingDeeSubject");
            sql.Append(" WHERE EXISTS(SELECT 1 FROM dbo.fn_split(ChkId,',') WHERE [value]= @ChkId AND CompanyId=@CompanyId)");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@ChkId", DbType.String, chkId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.Exists(dc, this._db);
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
            var sql = new StringBuilder();

            sql.Append(" SELECT 1 FROM tbl_FinKingDeeProof WHERE FItemId=@FItemId AND FItemType=@FItemType AND FCompanyId=@FCompanyId");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@FItemType", DbType.Byte, itemType);
            this._db.AddInParameter(dc, "@FItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(dc, "@FCompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.Exists(dc, this._db);
        }

        /// <summary>
        /// 根据是否订单收款和关联编号判断是否已封团
        /// </summary>
        /// <param name="isOrder">是否收款</param>
        /// <param name="itemId">收款编号/出账编号</param>
        /// <param name="companyId">系统公司编号编号</param>
        /// <returns>True：已封团 False：未封团</returns>
        public bool IsFt(bool isOrder,string itemId, string companyId)
        {
            var sql = new StringBuilder();

            if (isOrder)
            {
                sql.Append(" SELECT  1");
                sql.Append(" FROM    dbo.tbl_Tour");
                sql.AppendFormat(" WHERE   TourStatus = {0}", (int)TourStatus.封团);
                sql.Append("         AND TourId = ( SELECT   TourId");
                sql.Append("                        FROM     dbo.tbl_TourOrder");
                sql.Append("                        WHERE    OrderId = ( SELECT  OrderId");
                sql.Append("                                             FROM    dbo.tbl_TourOrderSales");
                sql.Append("                                             WHERE   Id = @ItemId");
                sql.Append("                                           )");
                sql.Append("                      )");
            }
            else
            {
                sql.Append(" SELECT  1");
                sql.Append(" FROM    dbo.tbl_Tour");
                sql.AppendFormat(" WHERE   TourStatus = {0}", (int)TourStatus.封团);
                sql.Append("         AND TourId = ( SELECT   TourId");
                sql.Append("                        FROM     dbo.tbl_FinRegister");
                sql.Append("                        WHERE    RegisterId = @ItemId");
                sql.Append("                                 AND CompanyId = @CompanyId");
                sql.Append("                      )");
            }

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@ItemId", isOrder ? DbType.AnsiStringFixedLength : DbType.Int32, itemId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.Exists(dc, this._db);
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
            var strSql = new StringBuilder();

            strSql.Append(" INSERT  INTO [tbl_FinAssets]");
            strSql.Append("         ( [CompanyId] ,");
            strSql.Append("           [AssetCode] ,");
            strSql.Append("           [AssetName] ,");
            strSql.Append("           [BuyTime] ,");
            strSql.Append("           [BuyPrice] ,");
            strSql.Append("           [DepreciableLife] ,");
            strSql.Append("           [DepartmentId] ,");
            strSql.Append("           [Department] ,");
            strSql.Append("           [Remark] ,");
            strSql.Append("           [AdminDeptId] ,");
            strSql.Append("           [AdminId] ,");
            strSql.Append("           [Admin] ,");
            strSql.Append("           [DeptId] ,");
            strSql.Append("           [OperatorId] ,");
            strSql.Append("           [Operator] ,");
            strSql.Append("           [IssueTime]");
            strSql.Append("         )");
            strSql.Append(" VALUES  ( @CompanyId ,");
            strSql.Append("           @AssetCode ,");
            strSql.Append("           @AssetName ,");
            strSql.Append("           @BuyTime ,");
            strSql.Append("           @BuyPrice ,");
            strSql.Append("           @DepreciableLife ,");
            strSql.Append("           @DepartmentId ,");
            strSql.Append("           @Department ,");
            strSql.Append("           @Remark ,");
            strSql.Append("           @AdminDeptId ,");
            strSql.Append("           @AdminId ,");
            strSql.Append("           @Admin ,");
            strSql.Append("           @DeptId ,");
            strSql.Append("           @OperatorId ,");
            strSql.Append("           @Operator ,");
            strSql.Append("           @IssueTime");
            strSql.Append("         )");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@AssetCode", DbType.String, mdl.AssetCode);
            this._db.AddInParameter(dc, "@AssetName", DbType.String, mdl.AssetName);
            this._db.AddInParameter(dc, "@BuyTime", DbType.DateTime, mdl.BuyTime);
            this._db.AddInParameter(dc, "@BuyPrice", DbType.Decimal, mdl.BuyPrice);
            this._db.AddInParameter(dc, "@DepreciableLife", DbType.Decimal, mdl.DepreciableLife);
            this._db.AddInParameter(dc, "@DepartmentId", DbType.Int32, mdl.DepartmentId);
            this._db.AddInParameter(dc, "@Department", DbType.String, mdl.Department);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@AdminDeptId", DbType.Int32, mdl.AdminDeptId);
            this._db.AddInParameter(dc, "@AdminId", DbType.AnsiStringFixedLength, mdl.AdminId);
            this._db.AddInParameter(dc, "@Admin", DbType.String, mdl.Admin);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdAsset(MAsset mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE  [tbl_FinAssets]");
            strSql.Append(" SET     [AssetCode] = @AssetCode ,");
            strSql.Append("         [AssetName] = @AssetName ,");
            strSql.Append("         [BuyTime] = @BuyTime ,");
            strSql.Append("         [BuyPrice] = @BuyPrice ,");
            strSql.Append("         [DepreciableLife] = @DepreciableLife ,");
            strSql.Append("         [DepartmentId] = @DepartmentId ,");
            strSql.Append("         [Department] = @Department ,");
            strSql.Append("         [Remark] = @Remark ,");
            strSql.Append("         [AdminDeptId] = @AdminDeptId ,");
            strSql.Append("         [AdminId] = @AdminId ,");
            strSql.Append("         [Admin] = @Admin ,");
            strSql.Append("         [DeptId] = @DeptId ,");
            strSql.Append("         [OperatorId] = @OperatorId ,");
            strSql.Append("         [Operator] = @Operator ,");
            strSql.Append("         [IssueTime] = @IssueTime");
            strSql.Append(" WHERE   Id = @Id AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@AssetCode", DbType.String, mdl.AssetCode);
            this._db.AddInParameter(dc, "@AssetName", DbType.String, mdl.AssetName);
            this._db.AddInParameter(dc, "@BuyTime", DbType.DateTime, mdl.BuyTime);
            this._db.AddInParameter(dc, "@BuyPrice", DbType.Decimal, mdl.BuyPrice);
            this._db.AddInParameter(dc, "@DepreciableLife", DbType.Decimal, mdl.DepreciableLife);
            this._db.AddInParameter(dc, "@DepartmentId", DbType.Int32, mdl.DepartmentId);
            this._db.AddInParameter(dc, "@Department", DbType.String, mdl.Department);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@AdminDeptId", DbType.Int32, mdl.AdminDeptId);
            this._db.AddInParameter(dc, "@AdminId", DbType.AnsiStringFixedLength, mdl.AdminId);
            this._db.AddInParameter(dc, "@Admin", DbType.String, mdl.Admin);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据资产主键集合删除资产
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="ids">资产主键集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        public int DelAsset(string companyId, params int[] ids)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinAssets");
            strSql.Append(" SET IsDeleted='1'");
            strSql.Append(" WHERE");
            strSql.Append("     Id IN (" + Utils.GetSqlIdStrByArray(ids) + ")");
            strSql.Append("     AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据资产主键编号获取资产实体
        /// </summary>
        /// <param name="id">资产主键编号</param>
        /// <returns>资产实体</returns>
        public MAsset GetAsset(int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MAsset();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [AssetCode] ,");
            strSql.Append("         [AssetName] ,");
            strSql.Append("         [BuyTime] ,");
            strSql.Append("         [BuyPrice] ,");
            strSql.Append("         [DepreciableLife] ,");
            strSql.Append("         [DepartmentId] ,");
            strSql.Append("         [Department] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [AdminDeptId] ,");
            strSql.Append("         [AdminId] ,");
            strSql.Append("         [Admin] ,");
            strSql.Append("         [DeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [tbl_FinAssets]");
            strSql.Append(" WHERE   Id = @Id");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.AssetCode = dr.GetString(dr.GetOrdinal("AssetCode"));
                    mdl.AssetName = dr.GetString(dr.GetOrdinal("AssetName"));
                    mdl.BuyTime = dr.GetDateTime(dr.GetOrdinal("BuyTime"));
                    mdl.BuyPrice = dr.GetDecimal(dr.GetOrdinal("BuyPrice"));
                    mdl.DepreciableLife = dr.GetDecimal(dr.GetOrdinal("DepreciableLife"));
                    mdl.DepartmentId = dr.GetInt32(dr.GetOrdinal("DepartmentId"));
                    mdl.Department = dr.GetString(dr.GetOrdinal("Department"));
                    mdl.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    mdl.AdminDeptId = dr.GetInt32(dr.GetOrdinal("AdminDeptId"));
                    mdl.AdminId = dr.GetString(dr.GetOrdinal("AdminId"));
                    mdl.Admin = dr.GetString(dr.GetOrdinal("Admin"));
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.Operator = dr.GetString(dr.GetOrdinal("Operator"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

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
        public IList<MAsset> GetAssetLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MAssetBase mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            var lst = new List<MAsset>();
            var query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}' AND IsDeleted = '0'", mSearch.CompanyId);
            if (mSearch.DepartmentId > 0)
            {
                query.AppendFormat(" AND DepartmentId = {0}", mSearch.DepartmentId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Department))
            {
                query.AppendFormat(" AND Department LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Department));
            }
            if (!string.IsNullOrEmpty(mSearch.AdminId))
            {
                query.AppendFormat(" AND AdminId = '{0}'", mSearch.AdminId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Admin))
            {
                query.AppendFormat(" AND Admin LIKE '%{0}%'", Utils.ToSqlLike(mSearch.Admin));
            }
            if (!string.IsNullOrEmpty(mSearch.AssetCode))
            {
                query.AppendFormat(" AND AssetCode LIKE '%{0}%'", Utils.ToSqlLike(mSearch.AssetCode));
            }
            if (!string.IsNullOrEmpty(mSearch.AssetName))
            {
                query.AppendFormat(" AND AssetName LIKE '%{0}%'", Utils.ToSqlLike(mSearch.AssetName));
            }
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "tbl_FinAssets", "Id", "[Id],[AssetCode],[AssetName],[BuyTime],[Department],[BuyPrice],[DepreciableLife],[Admin]"
                , query.ToString(), "BuyTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MAsset
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        AssetCode = dr.GetString(dr.GetOrdinal("AssetCode")),
                        AssetName = dr.GetString(dr.GetOrdinal("AssetName")),
                        BuyTime = dr.GetDateTime(dr.GetOrdinal("BuyTime")),
                        Department = dr.GetString(dr.GetOrdinal("Department")),
                        BuyPrice = dr.GetDecimal(dr.GetOrdinal("BuyPrice")),
                        DepreciableLife = dr.GetDecimal(dr.GetOrdinal("DepreciableLife")),
                        Admin = dr["Admin"].ToString()
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 获取核算项目列表
        /// </summary>
        /// <param name="xml">核算项目XML</param>
        /// <returns>核算项目列表</returns>
        private static IList<MKingDeeChk> GetKingDeeChkLstByXml(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MKingDeeChk
            {
                ChkCate = (FinanceAccountItem)Utils.GetInt(Utils.GetXAttributeValue(i, "ChkCate")),
                ChkCd = Utils.GetXAttributeValue(i, "ChkCd"),
                ChkId = Utils.GetInt(Utils.GetXAttributeValue(i, "ChkId")),
                ChkNm = Utils.GetXAttributeValue(i, "ChkNm"),
                PreChkNm = string.IsNullOrEmpty(Utils.GetXAttributeValue(i, "PreChkNm")) ? Utils.GetXAttributeValue(i, "ChkNm") : Utils.GetXAttributeValue(i, "PreChkNm"),
            }).ToList();
        }

        /// <summary>
        /// 根据当前销售员编号和部门编号集合获取销售收款和应收管理的浏览权限
        /// </summary>
        /// <param name="sellerId">销售员编号</param>
        /// <param name="deptIds">部门集合</param>
        /// <returns>组织机构浏览条件</returns>
        protected string GetReceivedOrg(string sellerId, ICollection<int> deptIds)
        {
            var str = string.Empty;

            if (!string.IsNullOrEmpty(sellerId) && deptIds != null)
            {
                str = string.Format(" AND (SellerId = '{0}'", sellerId);

                if (deptIds.Count > 0 && !deptIds.Contains(-1))
                {
                    str = str + deptIds.Aggregate(" OR DeptId IN (", (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")";
                    str = str + string.Format(" OR EXISTS(SELECT 1 FROM tbl_Tour T WHERE T.TourId=tbl_TourOrder.TourId AND (T.SellerId = '{0}' {1}))", sellerId, deptIds.Aggregate(" OR T.DeptId IN (", (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")", (int)OrderType.分销商下单,(int)OrderType.代客预定);
                }
                str = str + ")";
            }

            return str;
        }

        /// <summary>
        /// 根据大于等于、等于、小于等于转换
        /// </summary>
        /// <param name="sign">大于等于、等于、小于等于</param>
        /// <returns>转换结果</returns>
        private static string GetEqualSign(EqualSign? sign)
        {
            if (!sign.HasValue) return "=";
            return Utils.GetSqlBiJiaoYunSuanFu((int)sign.Value);
        }

        /// <summary>
        /// 根据xml获取联系人列表
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>联系人列表</returns>
        private static IList<MCrmLinkman> GetCrmLinkmanLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MCrmLinkman
            {
                Name = Utils.GetXAttributeValue(i, "Name"),
                MobilePhone = Utils.GetXAttributeValue(i, "MobilePhone"),
                Telephone = Utils.GetXAttributeValue(i, "Telephone"),
                Fax = Utils.GetXAttributeValue(i, "Fax"),
                Type = (LxrType)Utils.GetInt(Utils.GetXAttributeValue(i, "Type")),
            }).ToList();
        }

        /// <summary>
        /// 根据ＸＭＬ获到销售员与客户单位的超限信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MApplyOverrun> GetApplyOverrunListByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MApplyOverrun> list = new List<MApplyOverrun>();
            MApplyOverrun item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MApplyOverrun()
                {
                    AmountOwed = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "AmountOwed")),
                    Name = Utils.GetXAttributeValue(xRow, "Name"),
                    Arrear = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Arrear")),
                    PreIncome = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PreIncome")),
                    SumPay = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SumPay")),
                    Transfinite = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Transfinite")),
                    ConfirmAdvances = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ConfirmAdvances")),
                    DeadDay = Utils.GetInt(Utils.GetXAttributeValue(xRow, "DeadDay")),
                    Deadline = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Deadline")),
                    TransfiniteTime = string.IsNullOrEmpty(Utils.GetXAttributeValue(xRow, "TransfiniteTime")) ? null : (DateTime?)Utils.GetDateTime(Utils.GetXAttributeValue(xRow, "TransfiniteTime")),
                    DisburseId = Utils.GetXAttributeValue(xRow, "DisburseId"),
                    Id = Utils.GetXAttributeValue(xRow, "Id"),
                    OverrunType = (OverrunType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "OverrunType"))
                };
                list.Add(item);
            }
            return list;
        }


        /// <summary>
        /// 根据xml获取金蝶凭证详细列表
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>金蝶凭证详细列表</returns>
        private static IList<MKingDeeProofDetail> GetKingDeeProofDetailLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MKingDeeProofDetail
                {
                    FDetailId = Utils.GetInt(Utils.GetXAttributeValue(i, "FDetailId")),
                    FId = Utils.GetXAttributeValue(i, "FId"),
                    FExplanation = Utils.GetXAttributeValue(i, "FExplanation"),
                    FAccountNum = Utils.GetXAttributeValue(i, "FAccountNum"),
                    FAccountName = Utils.GetXAttributeValue(i, "FAccountName"),
                    FCurrencyNum = Utils.GetXAttributeValue(i, "FCurrencyNum"),
                    FCurrencyName = Utils.GetXAttributeValue(i, "FCurrencyName"),
                    FAmountFor = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FAmountFor")),
                    FDebit = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FDebit")),
                    FCredit = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FCredit")),
                    FItem = Utils.GetXAttributeValue(i, "FItem"),
                    FEntryId = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FEntryId")),
                    FIsExport = Utils.GetXAttributeValue(i, "FIsExport") == "1",
                }).ToList();
        }

        /// <summary>
        /// 根据查询语句返回金蝶凭证实体
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶凭证实体</returns>
        private static MKingDeeProof GetKingDeeProofMdl(System.Data.Common.DbCommand dc, Database db)
        {
            var mdl = new MKingDeeProof();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    mdl = GetKingDeeProofMdl(dr);
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据查询语句返回金蝶凭证列表
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶凭证列表</returns>
        private static IList<MKingDeeProof> GetKingDeeProofLst(System.Data.Common.DbCommand dc, Database db)
        {
            var lst = new List<MKingDeeProof>();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    lst.Add(GetKingDeeProofMdl(dr));
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据查询语句返回金蝶科目实体
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶科目实体</returns>
        private static MKingDeeSubject GetKingDeeSubjectMdl(System.Data.Common.DbCommand dc, Database db)
        {
            var mdl = new MKingDeeSubject();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    mdl = GetKingDeeSubjectMdl(dr);
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据查询语句返回金蝶科目列表
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶科目列表</returns>
        private static IList<MKingDeeSubject> GetKingDeeSubjectLst(System.Data.Common.DbCommand dc, Database db)
        {
            var lst = new List<MKingDeeSubject>();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    lst.Add(GetKingDeeSubjectMdl(dr));
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据查询语句返回金蝶核算项目实体
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶核算项目实体</returns>
        private static MKingDeeChk GetKingDeeChkMdl(System.Data.Common.DbCommand dc, Database db)
        {
            var mdl = new MKingDeeChk();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    mdl = GetKingDeeChkMdl(dr);
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据查询语句返回金蝶核算项目列表
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>金蝶核算项目列表</returns>
        private static IList<MKingDeeChk> GetKingDeeChkLst(System.Data.Common.DbCommand dc, Database db)
        {
            var lst = new List<MKingDeeChk>();
            using (var dr = DbHelper.ExecuteReader(dc, db))
            {
                while (dr.Read())
                {
                    lst.Add(GetKingDeeChkMdl(dr));
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据IDataReader返回金蝶凭证实体
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>金蝶凭证实体</returns>
        private static MKingDeeProof GetKingDeeProofMdl(IDataReader dr)
        {
            return new MKingDeeProof
                {
                    FId = dr.GetString(dr.GetOrdinal("FId")),
                    FCompanyId = dr.GetString(dr.GetOrdinal("FCompanyId")),
                    FTourId = dr.GetString(dr.GetOrdinal("FTourId")),
                    FTourCode = dr.IsDBNull(dr.GetOrdinal("FTourCode")) ? "" : dr.GetString(dr.GetOrdinal("FTourCode")),
                    FItemType = (DefaultProofType)dr.GetByte(dr.GetOrdinal("FItemType")),
                    FItemId = dr["FItemId"].ToString(),
                    FDate = dr.GetDateTime(dr.GetOrdinal("FDate")),
                    FYear = dr.GetDecimal(dr.GetOrdinal("FYear")),
                    FPeriod = dr.GetDecimal(dr.GetOrdinal("FPeriod")),
                    FGroupId = dr.IsDBNull(dr.GetOrdinal("FGroupId")) ? "" : dr.GetString(dr.GetOrdinal("FGroupId")),
                    FNumber = dr.GetInt32(dr.GetOrdinal("FNumber")),
                    FPreparerId = dr.IsDBNull(dr.GetOrdinal("FPreparerId")) ? "" : dr.GetString(dr.GetOrdinal("FPreparerId")),
                    FCheckerId = dr.IsDBNull(dr.GetOrdinal("FCheckerId")) ? "" : dr.GetString(dr.GetOrdinal("FCheckerId")),
                    FApproveId = dr.IsDBNull(dr.GetOrdinal("FApproveId")) ? "" : dr.GetString(dr.GetOrdinal("FApproveId")),
                    FCashierId = dr.IsDBNull(dr.GetOrdinal("FCashierId")) ? "" : dr.GetString(dr.GetOrdinal("FCashierId")),
                    FHandler = dr.IsDBNull(dr.GetOrdinal("FHandler")) ? "" : dr.GetString(dr.GetOrdinal("FHandler")),
                    FSettleTypeId = dr.IsDBNull(dr.GetOrdinal("FSettleTypeId")) ? "" : dr.GetString(dr.GetOrdinal("FSettleTypeId")),
                    FSettleNo = dr.IsDBNull(dr.GetOrdinal("FSettleNo")) ? "" : dr.GetString(dr.GetOrdinal("FSettleNo")),
                    FQuantity = dr.GetDecimal(dr.GetOrdinal("FQuantity")),
                    FMeasureUnitId = dr.IsDBNull(dr.GetOrdinal("FMeasureUnitId")) ? "" : dr.GetString(dr.GetOrdinal("FMeasureUnitId")),
                    FUnitPrice = dr.GetDecimal(dr.GetOrdinal("FUnitPrice")),
                    FReference = dr.IsDBNull(dr.GetOrdinal("FReference")) ? "" : dr.GetString(dr.GetOrdinal("FReference")),
                    FTransDate = dr.GetDateTime(dr.GetOrdinal("FTransDate")),
                    FTransNo = dr.IsDBNull(dr.GetOrdinal("FTransNo")) ? "" : dr.GetString(dr.GetOrdinal("FTransNo")),
                    FAttachments = dr.GetDecimal(dr.GetOrdinal("FAttachments")),
                    FSerialNum = dr.GetDecimal(dr.GetOrdinal("FSerialNum")),
                    FObjectName = dr.IsDBNull(dr.GetOrdinal("FObjectName")) ? "" : dr.GetString(dr.GetOrdinal("FObjectName")),
                    FParameter = dr.IsDBNull(dr.GetOrdinal("FParameter")) ? "" : dr.GetString(dr.GetOrdinal("FParameter")),
                    FExchangeRate = dr.GetDecimal(dr.GetOrdinal("FExchangeRate")),
                    FPosted = dr.GetDecimal(dr.GetOrdinal("FPosted")),
                    FInternalInd = dr.IsDBNull(dr.GetOrdinal("FInternalInd")) ? "" : dr.GetString(dr.GetOrdinal("FInternalInd")),
                    FCashFlow = dr.IsDBNull(dr.GetOrdinal("FCashFlow")) ? "" : dr.GetString(dr.GetOrdinal("FCashFlow")),
                    KingDeeProofDetailLst = GetKingDeeProofDetailLst(dr.GetString(dr.GetOrdinal("XMLDetail")))
                };
        }

        /// <summary>
        /// 根据IDataReader返回金蝶科目实体
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>金蝶科目实体</returns>
        private static MKingDeeSubject GetKingDeeSubjectMdl(IDataReader dr)
        {
            return new MKingDeeSubject
                {
                    SubjectId = dr.GetInt32(dr.GetOrdinal("SubjectId")),
                    SubjectCd = dr.GetString(dr.GetOrdinal("SubjectCd")),
                    SubjectNm = dr.GetString(dr.GetOrdinal("SubjectNm")),
                    SubjectTyp = (FinanceAccountItem)dr.GetByte(dr.GetOrdinal("SubjectTyp")),
                    MnemonicCd = dr["MnemonicCd"].ToString(),
                    ChkId = dr.IsDBNull(dr.GetOrdinal("ChkId")) ? "" : dr.GetString(dr.GetOrdinal("ChkId")),
                    ChkItem = dr.IsDBNull(dr.GetOrdinal("ChkItem")) ? "" : dr.GetString(dr.GetOrdinal("ChkItem")),
                    ItemList = GetKingDeeChkLstByXml(dr["XMLItem"].ToString()),
                    PreSubjectNm = dr["PreSubjectNm"].ToString()
                };
        }

        /// <summary>
        /// 根据IDataReader返回金蝶核算项目实体
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>金蝶核算项目实体</returns>
        private static MKingDeeChk GetKingDeeChkMdl(IDataReader dr)
        {
            return new MKingDeeChk
            {
                ChkId = dr.GetInt32(dr.GetOrdinal("ChkId")),
                ChkCd = dr.GetString(dr.GetOrdinal("ChkCd")),
                MnemonicCd = dr["MnemonicCd"].ToString(),
                ChkNm = dr.GetString(dr.GetOrdinal("ChkNm")),
                ChkCate = (FinanceAccountItem)dr.GetByte(dr.GetOrdinal("ChkCate")),
                PreChkId = dr.GetInt32(dr.GetOrdinal("PreChkId")),
                PreChkNm = dr["PreChkNm"].ToString(),
                ItemId = dr["ItemId"].ToString()
            };
        }

        /// <summary>
        /// 获取订单收款默认凭证sql语句
        /// </summary>
        /// <param name="isFt">是否封团</param>
        /// <returns></returns>
        private static string GetOrderSalesProofSql(bool isFt)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  BuyCompanyName + '销售现收 ' + CAST(CollectionRefundAmount AS NVARCHAR(max)) N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            if (!isFt)
            {
                sql.Append("         CollectionRefundAmount N'借方' ,");
                sql.Append("         0 N'贷方'");
            }
            else
            {
                sql.Append("         0 N'借方' ,");
                sql.Append("         CollectionRefundAmount N'贷方'");
            }
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append("         INNER JOIN dbo.tbl_TourOrder ON dbo.tbl_TourOrderSales.OrderId = dbo.tbl_TourOrder.OrderId");
            if (!isFt)
            {
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_TourOrder.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.PreSubjectId = 0");
                sql.Append("         				  AND CASE WHEN ISNULL(CTE.ChkId,'')=''");
                sql.Append("         						 THEN CASE WHEN");
                sql.Append("                                            CASE WHEN EXISTS ( SELECT  1");
                sql.Append("                                                               FROM    dbo.tbl_ComPayment");
                sql.Append("                                                               WHERE   dbo.tbl_TourOrderSales.CollectionRefundMode = dbo.tbl_ComPayment.PaymentId");
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.ItemType = {0}", (int)ItemType.收入);
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.SourceType = {0} )", (int)SourceType.现金);
                sql.Append("                                                        THEN CASE WHEN CTE.SubjectNm LIKE '现金%' THEN 1 ELSE 0 END");
                sql.Append("                                                        ELSE CASE WHEN CTE.ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS NVARCHAR(MAX)) THEN 1 ELSE 0 END");
                sql.Append("                                            END = 1");
                sql.Append("                                      THEN 1 ELSE 0 END");
                sql.Append("                                  ELSE CASE WHEN");
                sql.Append("                                            EXISTS( SELECT 1");
                sql.Append("                                            		FROM dbo.tbl_ComSetting");
                sql.Append("                                            		WHERE dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                            				AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.订单收款_借);
                sql.Append("                                            				AND CTE.SubjectCd LIKE '' + dbo.tbl_ComSetting.[Value] + '%')");
                sql.Append("                                            THEN 1 ELSE 0 END");
                sql.Append("                              END = 1");
            }
            else
            {
                sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应收帐款_借);
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.SubjectCd LIKE ''");
                sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
                sql.Append("                          AND CTE.PreSubjectId = 0");
            }
            sql.Append(" WHERE   dbo.tbl_TourOrderSales.Id = @ItemId AND dbo.tbl_TourOrderSales.IsCheck='1' AND dbo.tbl_TourOrderSales.CollectionRefundState='0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            if (!isFt)
            {
                sql.Append("         0 ,");
                sql.Append("         CollectionRefundAmount");
            }
            else
            {
                sql.Append("         CollectionRefundAmount ,");
                sql.Append("         0");
            }
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append("         INNER JOIN dbo.tbl_TourOrder ON dbo.tbl_TourOrderSales.OrderId = dbo.tbl_TourOrder.OrderId");
            if (!isFt)
            {
                sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.订单收款_贷);
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.SubjectCd LIKE ''");
                sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
                sql.Append("                          AND CTE.PreSubjectId = 0");
            }
            else
            {
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_TourOrder.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.PreSubjectId = 0");
                sql.Append("         				  AND CASE WHEN ISNULL(CTE.ChkId,'')=''");
                sql.Append("         						 THEN CASE WHEN");
                sql.Append("                                            CASE WHEN EXISTS ( SELECT  1");
                sql.Append("                                                               FROM    dbo.tbl_ComPayment");
                sql.Append("                                                               WHERE   dbo.tbl_TourOrderSales.CollectionRefundMode = dbo.tbl_ComPayment.PaymentId");
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.ItemType = {0}", (int)ItemType.收入);
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.SourceType = {0} )", (int)SourceType.现金);
                sql.Append("                                                        THEN CASE WHEN CTE.SubjectNm LIKE '现金%' THEN 1 ELSE 0 END");
                sql.Append("                                                        ELSE CASE WHEN CTE.ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS NVARCHAR(MAX)) THEN 1 ELSE 0 END");
                sql.Append("                                            END = 1");
                sql.Append("                                      THEN 1 ELSE 0 END");
                sql.Append("                                  ELSE CASE WHEN");
                sql.Append("                                            EXISTS( SELECT 1");
                sql.Append("                                            		FROM dbo.tbl_ComSetting");
                sql.Append("                                            		WHERE dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                            				AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.订单收款_借);
                sql.Append("                                            				AND CTE.SubjectCd LIKE '' + dbo.tbl_ComSetting.[Value] + '%')");
                sql.Append("                                            THEN 1 ELSE 0 END");
                sql.Append("                              END = 1");
            }
            sql.Append(" WHERE   dbo.tbl_TourOrderSales.Id = @ItemId AND dbo.tbl_TourOrderSales.IsCheck='1' AND dbo.tbl_TourOrderSales.CollectionRefundState='0'");

            return sql.ToString();
        }

        /// <summary>
        /// 获取计调预付默认凭证sql语句
        /// </summary>
        /// <param name="isFt">是否封团</param>
        /// <returns></returns>
        private static string GetPrepaidProofSql(bool isFt)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  SourceName + ' 预付 ' + CAST(PaymentAmount AS NVARCHAR(max)) N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            if(!isFt)
            {
                sql.Append("         0 N'借方' ,");
                sql.Append("         PaymentAmount N'贷方'");
            }
            else
            {
                sql.Append("         PaymentAmount N'借方' ,");
                sql.Append("         0 N'贷方'");
            }
            sql.Append(" FROM    dbo.tbl_FinRegister");
            if (!isFt)
            {
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_FinRegister.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.PreSubjectId = 0");
                sql.Append("         				  AND CASE WHEN ISNULL(CTE.ChkId,'')=''");
                sql.Append("         						 THEN CASE WHEN");
                sql.Append("                                            CASE WHEN EXISTS ( SELECT  1");
                sql.Append("                                                               FROM    dbo.tbl_ComPayment");
                sql.Append("                                                               WHERE   dbo.tbl_FinRegister.PaymentType = dbo.tbl_ComPayment.PaymentId");
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.ItemType = {0}", (int)ItemType.支出);
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.SourceType = {0} )", (int)SourceType.现金);
                sql.Append("                                                        THEN CASE WHEN CTE.SubjectNm LIKE '现金%' THEN 1 ELSE 0 END");
                sql.Append("                                                        ELSE CASE WHEN CTE.ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS NVARCHAR(MAX)) THEN 1 ELSE 0 END");
                sql.Append("                                            END = 1");
                sql.Append("                                      THEN 1 ELSE 0 END");
                sql.Append("                                  ELSE CASE WHEN");
                sql.Append("                                            EXISTS( SELECT 1");
                sql.Append("                                            		FROM dbo.tbl_ComSetting");
                sql.Append("                                            		WHERE dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                            				AND dbo.tbl_ComSetting.[Key] = '{0}'",(int)SysConfigDefaultSubject.计调预付款_贷);
                sql.Append("                                            				AND CTE.SubjectCd LIKE '' + dbo.tbl_ComSetting.[Value] + '%')");
                sql.Append("                                            THEN 1 ELSE 0 END");
                sql.Append("                              END = 1");
            }
            else
            {
                sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应付账款_贷);
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.SubjectCd LIKE ''");
                sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
                sql.Append("                          AND CASE WHEN ISNULL(CTE.ChkId,'')<>''");
                sql.Append("                          		  THEN CASE WHEN EXISTS(SELECT 1 FROM dbo.view_KingdeeChkItem AS V");
                sql.Append("                          								WHERE CTE.XMLItem.exist('/root/row[@ChkCate = sql:column(\"V.ChkCate\")]')=1");
                sql.Append("                          									  AND CASE V.ChkCate");
                sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
                sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
                sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
                sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
                sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
                sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
                sql.Append("                          											ELSE 0");
                sql.Append("                          										  END = 1) THEN 1 ELSE 0 END");
                sql.Append("                          		  ELSE CASE CTE.SubjectTyp");
                sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
                sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
                sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
                sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
                sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
                sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
                sql.Append("                          			        ELSE 0");
                sql.Append("                          			   END");
                sql.Append("                          	 END = 1");
                sql.Append("                          AND CTE.PreSubjectId = 0");
            }
            sql.Append(" WHERE   dbo.tbl_FinRegister.RegisterId = @ItemId");
            sql.AppendFormat("         AND dbo.tbl_FinRegister.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinRegister.IsDeleted = '0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  SourceName + ' 预付 ' + CAST(PaymentAmount AS NVARCHAR(max)) N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            if (!isFt)
            {
                sql.Append("         PaymentAmount N'借方' ,");
                sql.Append("         0 N'贷方'");
            }
            else
            {
                sql.Append("         0 N'借方' ,");
                sql.Append("         PaymentAmount N'贷方'");
            }
            sql.Append(" FROM    dbo.tbl_FinRegister");
            if (!isFt)
            {
                sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.计调预付款_借);
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.SubjectCd LIKE ''");
                sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
                sql.Append("                          AND CTE.PreSubjectId = 0");
            }
            else
            {
                sql.Append("         LEFT JOIN CTE ON dbo.tbl_FinRegister.CompanyId = CTE.CompanyId");
                sql.Append("                          AND CTE.PreSubjectId = 0");
                sql.Append("         				  AND CASE WHEN ISNULL(CTE.ChkId,'')=''");
                sql.Append("         						 THEN CASE WHEN");
                sql.Append("                                            CASE WHEN EXISTS ( SELECT  1");
                sql.Append("                                                               FROM    dbo.tbl_ComPayment");
                sql.Append("                                                               WHERE   dbo.tbl_FinRegister.PaymentType = dbo.tbl_ComPayment.PaymentId");
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.ItemType = {0}", (int)ItemType.支出);
                sql.AppendFormat("                                                                 AND dbo.tbl_ComPayment.SourceType = {0} )", (int)SourceType.现金);
                sql.Append("                                                        THEN CASE WHEN CTE.SubjectNm LIKE '现金%' THEN 1 ELSE 0 END");
                sql.Append("                                                        ELSE CASE WHEN CTE.ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS NVARCHAR(MAX)) THEN 1 ELSE 0 END");
                sql.Append("                                            END = 1");
                sql.Append("                                      THEN 1 ELSE 0 END");
                sql.Append("                                  ELSE CASE WHEN");
                sql.Append("                                            EXISTS( SELECT 1");
                sql.Append("                                            		FROM dbo.tbl_ComSetting");
                sql.Append("                                            		WHERE dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
                sql.AppendFormat("                                            				AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.计调预付款_贷);
                sql.Append("                                            				AND CTE.SubjectCd LIKE '' + dbo.tbl_ComSetting.[Value] + '%')");
                sql.Append("                                            THEN 1 ELSE 0 END");
                sql.Append("                              END = 1");
            }
            sql.Append(" WHERE   dbo.tbl_FinRegister.RegisterId = @ItemId");
            sql.AppendFormat("         AND dbo.tbl_FinRegister.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinRegister.IsDeleted = '0'");

            return sql.ToString();
        }

        /// <summary>
        /// 获取导游借款默认凭证sql语句
        /// </summary>
        /// <returns></returns>
        private static string GetGuideDebitProofSql()
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  Borrower + ' 借款 ' + CAST(RealAmount AS NVARCHAR(max)) N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem ,");
            sql.Append("         0 N'借方' ,");
            sql.Append("         RealAmount N'贷方'");
            sql.Append(" FROM    dbo.tbl_FinDebit");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinDebit.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.导游备用金_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_FinDebit.Id = @ItemId");
            sql.AppendFormat("         AND dbo.tbl_FinDebit.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinDebit.IsDeleted = '0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN GuideId = dbo.tbl_FinDebit.BorrowerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinDebit.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         RealAmount ,");
            sql.Append(" 0");
            sql.Append(" FROM    dbo.tbl_FinDebit");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinDebit.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.导游备用金_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_FinDebit.Id = @ItemId");
            sql.AppendFormat("         AND dbo.tbl_FinDebit.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinDebit.IsDeleted = '0'");

            return sql.ToString();
        }

        /// <summary>
        /// 获取导游提前报账默认凭证sql语句
        /// </summary>
        /// <returns></returns>
        private static string GetGuideBzProofSql()
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  '' N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         ISNULL(Confirmation,0) N'借方' ,");
            sql.Append("         0 N'贷方'");
            sql.Append(" FROM    dbo.tbl_Plan ");
            sql.Append(" INNER JOIN dbo.tbl_Tour ON dbo.tbl_Plan.TourId = dbo.tbl_Tour.TourId");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Plan.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.团未完导游先报账_预付账款_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE dbo.tbl_Plan.TourId=@ItemId");
            sql.Append(" 	   AND dbo.tbl_Plan.IsDelete = '0'");
            sql.AppendFormat(" 	   AND dbo.tbl_Plan.Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat(" 	   AND dbo.tbl_Plan.PaymentType={0}", (int)Payment.导游现付);
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         ISNULL(GuideRealIncome,0)");
            sql.Append(" FROM    dbo.tbl_TourOrder");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.团未完导游先报账_预收账款_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   tbl_TourOrder.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_TourOrder.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_TourOrder.Status = {0}", (int)OrderStatus.已成交);
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN GuideId = dbo.tbl_FinDebit.BorrowerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinDebit.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         ISNULL(RealAmount,0)");
            sql.Append(" FROM    dbo.tbl_FinDebit");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinDebit.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.团未完导游先报账_团队借款_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append("         AND IsDeleted = '0'");
            sql.AppendFormat("         AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem ,");
            sql.Append("         CASE WHEN ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 )<0 THEN ");
            sql.Append("         ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) + ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 ) ");
            sql.Append("        - ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) ELSE 0 END,");

            sql.Append("         CASE WHEN ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 )>=0 THEN ");
            sql.Append("         ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 ) ELSE 0 END");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.团未完导游先报账_现金_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");

            return sql.ToString();
        }

        /// <summary>
        /// 获取单团核算默认凭证sql语句
        /// </summary>
        /// <returns></returns>
        private static string GetTourChkProofSql()
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  TourCode N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 N'借方' ,");
            sql.Append("         ( SELECT    ISNULL(SUM(ConfirmMoney), 0)");
            sql.Append("           FROM      dbo.tbl_TourOrder");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("                     AND ConfirmMoneyStatus = '1'");
            sql.Append("         ) + ( SELECT    ISNULL(SUM(FeeAmount), 0)");
            sql.Append("               FROM      dbo.tbl_FinOtherInFee");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDeleted = '0'");
            sql.Append("                         AND IsGuide = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)FinStatus.账务待支付);
            sql.Append("             ) N'贷方'");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_主营业务收入_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  Borrower + ' 借款 ' + CAST(RealAmount AS NVARCHAR(max)) ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN GuideId = dbo.tbl_FinDebit.BorrowerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinDebit.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         RealAmount");
            sql.Append(" FROM    dbo.tbl_FinDebit");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinDebit.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_团队借款_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append("         AND IsDeleted = '0'");
            sql.AppendFormat("         AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  SourceName + ' 预付 ' + CAST(dbo.tbl_FinRegister.PaymentAmount AS NVARCHAR(max)) ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         dbo.tbl_FinRegister.PaymentAmount");
            sql.Append(" FROM    dbo.tbl_FinRegister");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_团队预支_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_FinRegister.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_FinRegister.IsDeleted = '0'");
            sql.AppendFormat("         AND dbo.tbl_FinRegister.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinRegister.IsGuide = '0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         Confirmation - Prepaid");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Plan.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应付账款_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CASE WHEN ISNULL(CTE.ChkId,'')<>''");
            sql.Append("                          		  THEN CASE WHEN EXISTS(SELECT 1 FROM dbo.view_KingdeeChkItem AS V");
            sql.Append("                          								WHERE CTE.XMLItem.exist('/root/row[@ChkCate = sql:column(\"V.ChkCate\")]')=1");
            sql.Append("                          									  AND CASE V.ChkCate");
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END",(int)FinanceAccountItem.供应商);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("                          											ELSE 0");
            sql.Append("                          										  END = 1) THEN 1 ELSE 0 END");
            sql.Append("                          		  ELSE CASE CTE.SubjectTyp");
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("                          			        ELSE 0");
            sql.Append("                          			   END");
            sql.Append("                          	 END = 1");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_Plan.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_Plan.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_Plan.Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("         AND dbo.tbl_Plan.PaymentType <> {0}", (int)Payment.导游现付);
            sql.Append("         AND dbo.tbl_Plan.Confirmation <> dbo.tbl_Plan.Prepaid");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem ,");
            sql.Append("         CASE WHEN ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 )<0 THEN ");
            sql.Append("         ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) + ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 )");
            sql.Append("         -( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) ELSE 0 END,");

            sql.Append("         CASE WHEN ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 )>=0 THEN ");
            sql.Append("         ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("                     AND PaymentType = {0}", (int)Payment.导游现付);
            sql.Append("         ) - ( SELECT    ISNULL(SUM(GuideRealIncome), 0)");
            sql.Append("               FROM      dbo.tbl_TourOrder");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDelete = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("             ) - ( SELECT    ISNULL(SUM(BorrowAmount), 0)");
            sql.Append("                   FROM      dbo.tbl_FinDebit");
            sql.Append("                   WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                             AND IsDeleted = '0'");
            sql.AppendFormat("                             AND Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("                 ) ELSE 0 END");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_现金_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  TourCode ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.Append("         ) ,");
            sql.Append(" 0");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_主营业务成本_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  BuyCompanyName + '销售现收 ' + CAST(CollectionRefundAmount AS NVARCHAR(max)) ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         CollectionRefundAmount ,");
            sql.Append(" 0");
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append("         INNER JOIN dbo.tbl_TourOrder ON dbo.tbl_TourOrderSales.OrderId = dbo.tbl_TourOrder.OrderId");
            sql.Append("                                         AND dbo.tbl_TourOrder.TourId = @ItemId");
            sql.Append("                                         AND dbo.tbl_TourOrder.IsDelete = '0'");
            sql.AppendFormat("                                         AND dbo.tbl_TourOrder.Status = {0}", (int)OrderStatus.已成交);
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_预收账款_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_TourOrderSales.IsCheck = 1");
            sql.Append("         AND dbo.tbl_TourOrderSales.IsGuideRealIncome = 0");
            sql.Append("         AND dbo.tbl_TourOrderSales.CollectionRefundState = 0");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '%' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         ConfirmMoney - CheckMoney + ReturnMoney ,");
            sql.Append(" 0");
            sql.Append(" FROM    dbo.tbl_TourOrder");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应收帐款_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   tbl_TourOrder.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_TourOrder.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_TourOrder.Status = {0}", (int)OrderStatus.已成交);
            sql.Append("         AND dbo.tbl_TourOrder.IsClean = '0'");

            return sql.ToString();
        }

        /// <summary>
        /// 获取后期收款默认凭证Sql语句
        /// </summary>
        /// <returns></returns>
        private static string GetAfterProofSql()
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT  '' N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_TourOrderSales.CollectionRefundMode AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         CollectionRefundAmount N'借方' ,");
            sql.Append("         0 N'贷方'");
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append("         INNER JOIN dbo.tbl_TourOrder ON dbo.tbl_TourOrderSales.OrderId = dbo.tbl_TourOrder.OrderId");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_预收账款_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_TourOrder.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_TourOrderSales.IsCheck = '1'");
            sql.Append("         AND dbo.tbl_TourOrderSales.CollectionRefundState = '0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.BuyCompanyId THEN 1 ELSE 0 END", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_TourOrder.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_TourOrder.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         dbo.tbl_TourOrder.ConfirmMoney - dbo.tbl_TourOrder.CheckMoney");
            sql.Append("         + dbo.tbl_TourOrder.ReturnMoney ,");
            sql.Append("         0");
            sql.Append(" FROM    dbo.tbl_TourOrder");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_TourOrder.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应收帐款_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   tbl_TourOrder.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_TourOrder.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_TourOrder.Status = {0}", (int)OrderStatus.已成交);
            sql.Append("         AND ( dbo.tbl_TourOrder.IsClean = '0'");
            sql.Append("               OR dbo.tbl_TourOrder.ConfirmMoney - dbo.tbl_TourOrder.CheckMoney");
            sql.Append("               + dbo.tbl_TourOrder.ReturnMoney > 0");
            sql.Append("             )");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  TourCode ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         ( SELECT    ISNULL(SUM(Confirmation), 0)");
            sql.Append("           FROM      dbo.tbl_Plan");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
            sql.Append("         ) ,");
            sql.Append(" 0");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_主营业务成本_借);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         ( SELECT    ISNULL(SUM(ConfirmMoney), 0)");
            sql.Append("           FROM      dbo.tbl_TourOrder");
            sql.Append("           WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                     AND IsDelete = '0'");
            sql.AppendFormat("                     AND Status = {0}", (int)OrderStatus.已成交);
            sql.Append("                     AND ConfirmMoneyStatus = '1'");
            sql.Append("         ) + ( SELECT    ISNULL(SUM(FeeAmount), 0)");
            sql.Append("               FROM      dbo.tbl_FinOtherInFee");
            sql.Append("               WHERE     TourId = dbo.tbl_Tour.TourId");
            sql.Append("                         AND IsDeleted = '0'");
            sql.AppendFormat("                         AND Status = {0}", (int)FinStatus.账务待支付);
            sql.Append("                         AND IsGuide = '0'");
            sql.Append("             )");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Tour.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_主营业务收入_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   TourId = @ItemId");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_FinRegister.DealerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_FinRegister.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = CAST(dbo.tbl_FinRegister.PaymentType AS CHAR(36)) THEN 1 ELSE 0 END", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         ISNULL(PaymentAmount, 0)");
            sql.Append(" FROM    dbo.tbl_FinRegister");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_FinRegister.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_团队预支_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_FinRegister.TourId = @ItemId");
            sql.AppendFormat("         AND dbo.tbl_FinRegister.Status = {0}", (int)FinStatus.账务已支付);
            sql.Append("         AND dbo.tbl_FinRegister.IsDeleted = '0'");
            sql.Append("         AND dbo.tbl_FinRegister.IsGuide = '0'");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' ,");
            sql.Append("         SubjectCd ,");
            sql.Append("         SubjectNm ,");
            sql.Append("         ChkItem ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 ,");
            sql.Append("         Confirmation - Prepaid");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Plan.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_应付账款_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CASE WHEN ISNULL(CTE.ChkId,'')<>''");
            sql.Append("                          		  THEN CASE WHEN EXISTS(SELECT 1 FROM dbo.view_KingdeeChkItem AS V");
            sql.Append("                          								WHERE CTE.XMLItem.exist('/root/row[@ChkCate = sql:column(\"V.ChkCate\")]')=1");
            sql.Append("                          									  AND CASE V.ChkCate");
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("                          											WHEN {0} THEN CASE WHEN V.ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("                          											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("                          											ELSE 0");
            sql.Append("                          										  END = 1) THEN 1 ELSE 0 END");
            sql.Append("                          		  ELSE CASE CTE.SubjectTyp");
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.UserId = dbo.tbl_Plan.CostId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("                          			WHEN {0} THEN CASE WHEN CTE.ItemId = dbo.tbl_Plan.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("                          			WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("                          			        ELSE 0");
            sql.Append("                          			   END");
            sql.Append("                          	 END = 1");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_Plan.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_Plan.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_Plan.Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("         AND dbo.tbl_Plan.PaymentType <> {0}", (int)Payment.导游现付);
            sql.Append("         AND dbo.tbl_Plan.Confirmation <> dbo.tbl_Plan.Prepaid");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT  '' N'摘要' ,");
            sql.Append("         SubjectCd N'科目代码' ,");
            sql.Append("         SubjectNm N'科目名称' ,");
            sql.Append("         ChkItem N'核算科目' ,");
            sql.Append("         XMLItem = CASE WHEN ISNULL(CTE.ChkId, '') <> ''");
            sql.Append("                        THEN ( SELECT    ISNULL(ChkId,'') ChkId,");
            sql.Append("                                         ISNULL(ChkCate,Tbl.Col.value('@ChkCate', 'TINYINT')) ChkCate,");
            sql.Append("                                         ISNULL(ChkCd,'') ChkCd,");
            sql.Append("                                         ISNULL(ChkNm,'') ChkNm,");
            sql.Append("                                         ISNULL(PreChkNm,Tbl.Col.value('@PreChkNm', 'NVARCHAR(255)')) PreChkNm");
            sql.Append("                               FROM      CTE.XMLItem.nodes('root/row') Tbl(Col)");
            sql.Append("                                         LEFT JOIN dbo.view_KingdeeChkItem ON");
            sql.Append("         									Tbl.Col.value('@ChkCate','TINYINT') = dbo.view_KingdeeChkItem.ChkCate");
            sql.Append("                                             AND dbo.view_KingdeeChkItem.ChkCd LIKE '' + Tbl.Col.value('@ChkCd','NVARCHAR(50)') + '%'");
            sql.Append("                                             AND CASE Tbl.Col.value('@ChkCate','TINYINT')");
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.客户);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.部门);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN UserId = dbo.tbl_Tour.SellerId THEN 1 ELSE 0 END", (int)FinanceAccountItem.职员);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Plan.SourceId THEN 1 ELSE 0 END", (int)FinanceAccountItem.供应商);
            sql.AppendFormat("         											WHEN {0} THEN CASE WHEN ItemId = dbo.tbl_Tour.TourId THEN 1 ELSE 0 END", (int)FinanceAccountItem.团号);
            sql.AppendFormat("         											WHEN {0} THEN 0", (int)FinanceAccountItem.支付方式);
            sql.Append("         											ELSE 0");
            sql.Append("         										END = 1");
            sql.Append("                                FOR");
            sql.Append("                                    XML RAW ,");
            sql.Append("                                        ROOT");
            sql.Append("                                )");
            sql.Append("                        ELSE CTE.XMLItem");
            sql.Append("                   END ,");
            sql.Append("         0 N'借方' ,");
            sql.Append("         ISNULL(Confirmation, 0) N'贷方'");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append("         INNER JOIN dbo.tbl_Tour ON dbo.tbl_Plan.TourId = dbo.tbl_Tour.TourId");
            sql.Append("         LEFT JOIN dbo.tbl_ComSetting ON dbo.tbl_Plan.CompanyId = dbo.tbl_ComSetting.CompanyId");
            sql.AppendFormat("                                         AND dbo.tbl_ComSetting.[Key] = '{0}'", (int)SysConfigDefaultSubject.单团核算_团队支出_贷);
            sql.Append("         LEFT JOIN CTE ON dbo.tbl_ComSetting.CompanyId = CTE.CompanyId");
            sql.Append("                          AND CTE.SubjectCd LIKE ''");
            sql.Append("                          + dbo.tbl_ComSetting.[Value] + '%'");
            sql.Append("                          AND CTE.PreSubjectId = 0");
            sql.Append(" WHERE   dbo.tbl_Plan.TourId = @ItemId");
            sql.Append("         AND dbo.tbl_Plan.IsDelete = '0'");
            sql.AppendFormat("         AND dbo.tbl_Plan.Status = {0}", (int)PlanState.已落实);
            sql.AppendFormat("         AND dbo.tbl_Plan.PaymentType = {0}", (int)Payment.导游现付);

            return sql.ToString();
        }
        #endregion
    }
}
