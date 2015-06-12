using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.StatStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.EnumType.StatStructure;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.DAL.StatStructure
{
    /// <summary>
    /// 统计分析(客户,垫款,收支,收入,账龄)
    /// 创建者：郑付杰
    /// 创建时间：2011/10/12
    /// </summary>
    public class DMoneyStatistics
    {
        //private readonly Database _db = null;
        //#region 构造函数
        //public DMoneyStatistics()
        //{
        //    this._db = base.SystemStore;
        //}
        //#endregion

        //#region 客户交汇统计

        ///// <summary>
        ///// 按月统计每家组团社的交易情况
        ///// 根据年度月份、系统公司编号获取客户交汇统计列表
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="year">年度</param>
        ///// <param name="month">月份</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <returns>客户交汇统计列表</returns>
        //public IList<MCustomer> GetCustomerLst(int pageSize
        //                                      , int pageIndex
        //                                      , ref int recordCount
        //                                      , int year
        //                                      , int month
        //                                      , string companyId)
        //{
        //    StringBuilder tablName = new StringBuilder();
        //    tablName.Append("select a.BuyCompanyId,a.TourNum,a.PeopleNum,a.Y,a.S,b.[Name] from (");
        //    tablName.Append(" select a.BuyCompanyId,count(b.TourId)TourNum,sum(isnull(b.PeopleNum,0))PeopleNum,sum(isnull(b.Y,0))Y,sum(isnull(b.S,0))S");
        //    tablName.Append(" from tbl_TourOrder a");
        //    tablName.Append(" left join (");
        //    tablName.Append(" select BuyCompanyId,TourId,sum(Adults)+sum(Childs)+sum(Others) PeopleNum,");
        //    tablName.Append(" sum(ConfirmMoney) Y,sum(CheckMoney) S");
        //    tablName.AppendFormat(" from tbl_TourOrder where CompanyId = '{0}'", companyId);
        //    tablName.AppendFormat(" and year(IssueTime)={0} and month(IssueTime) = {1}", year, month);
        //    tablName.Append(" group by BuyCompanyId,TourId");
        //    tablName.Append(" ) b on a.BuyCompanyId = b.BuyCompanyId");
        //    tablName.Append(" left join tbl_Crm c");
        //    tablName.Append(" on a.BuyCompanyId = c.CrmId");
        //    tablName.AppendFormat(" where c.[Type] = {0} and c.IsDelete = '0' and a.CompanyId = '{1}'", (int)Model.EnumType.CrmStructure.CrmType.组团社, companyId);
        //    tablName.Append(" group by a.BuyCompanyId ) a");
        //    tablName.Append(" left join tbl_Crm b on a.BuyCompanyId = b.CrmId");

        //    IList<MCustomer> list = new List<MCustomer>();
        //    MCustomer item = null;
        //    string fields = "BuyCompanyId,TourNum,PeopleNum,Y,S";
        //    string orderBy = "TourNum DESC";
        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
        //        tablName.ToString(), fields,string.Empty,orderBy,false, string.Empty))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MCustomer()
        //            {
        //                CrmId = reader["BuyCompanyId"].ToString(),
        //                Tours = (int)reader["TourNum"],
        //                Company = reader["Name"].ToString(),
        //                Transaction = (decimal)reader["Y"],
        //                Arrears = (decimal)reader["Y"] - (decimal)reader["S"],
        //                Guests = (int)reader["PeopleNum"]

        //            });
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 根据年度月份、组团社编号、系统公司编号获取团量明细列表
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="year">年度</param>
        ///// <param name="month">月份</param>
        ///// <param name="crmId">组团社编号</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <returns>团量明细列表</returns>
        //public IList<MTourDetail> GetTourDetailByCrmId(int pageSize
        //                                            , int pageIndex
        //                                            , ref int recordCount
        //                                            , int year
        //                                            , int month
        //                                            , string crmId
        //                                            , string companyId)
        //{
        //    StringBuilder tableName = new StringBuilder();
        //    tableName.Append("select b.TourCode,b.RouteName,b.LDate,a.ConfirmMoney,a.CheckMoney,");
        //    tableName.Append(" a.OrderCode,a.Adults+a.Childs+a.Others PeopleNum");
        //    tableName.Append(" from tbl_TourOrder a");
        //    tableName.Append(" left join tbl_Tour b");
        //    tableName.Append(" on a.TourId = b.TourId");
        //    tableName.AppendFormat(" where year(a.IssueTime) = {0} and month(a.IssueTime) = {1}", year, month);
        //    tableName.AppendFormat(" and a.BuyCompanyId = '{0}' and a.CompanyId = '{1}'", crmId, companyId);
        //    string fields = "TourCode,RouteName,LDate,ConfirmMoney,CheckMoney,OrderCode,PeopleNum";
        //    string orderBy = "ConfirmMoney DESC";
        //    IList<MTourDetail> list = new List<MTourDetail>();
        //    MTourDetail item = null;
        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
        //        tableName.ToString(), fields,string.Empty,orderBy,false, string.Empty))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MTourDetail()
        //            {
        //                TourCode = reader["TourCode"].ToString(),
        //                RouteName = reader["RouteName"].ToString(),
        //                LDate = DateTime.Parse(reader["LDate"].ToString()),
        //                OrderCode = reader["OrderCode"].ToString(),
        //                PeopleNum = (int)reader["PeopleNum"],
        //                Incoming = (decimal)reader["ConfirmMoney"],
        //                Arrears = (decimal)reader["ConfirmMoney"] - (decimal)reader["CheckMoney"]
        //            });
        //        }
        //    }

        //    return list;
        //}

        //#endregion

        //#region 垫款余额表

        ///// <summary>
        ///// 根据系统公司编号获取垫款余额列表
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <returns>垫款余额列表</returns>
        //public IList<MAdvance> GetAdvanceLst(int pageSize
        //                                    , int pageIndex
        //                                    , ref int recordCount
        //                                    , string companyId)
        //{
        //    string tableName = "view_Advance_Statistics";
        //    string fields = "CompanyId,ContactName,Arrears,ConfirmMoney,CheckMoney,BorrowAmount,Amount,Confirmation,DisburseAmount";
        //    string query = string.Format(" CompanyId = '{0}'", companyId);
        //    string orderBy = "DisburseAmount-Arrears DESC";
        //    IList<MAdvance> list = new List<MAdvance>();
        //    MAdvance item = null;
        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db,pageSize,pageIndex,ref recordCount,
        //        tableName,string.Empty,fields,query,orderBy))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MAdvance()
        //            {
        //                Salesman = reader["ContactName"].ToString(),
        //                TourPrePaid = (decimal)reader["Amount"],
        //                DisburseAmount = (decimal)reader["DisburseAmount"],
        //                LimitAmount = (decimal)reader["Arrears"],
        //                PreReceived = (decimal)reader["CheckMoney"],
        //                Receivable = (decimal)reader["ConfirmMoney"],
        //                TourBorrow = (decimal)reader["BorrowAmount"],
        //                TourPaid = (decimal)reader["Confirmation"]
        //            });
        //        }
        //    }
        //    return list;
        //}

        //#endregion

        

        //#region 收入成本毛利表

        ///// <summary>
        ///// 根据毛利统计类型、搜索实体获取收入成本毛利列表
        ///// 按区域统计：以个线路区域一行，统计线路区域下所有计划的团队数、收、支
        ///// 按部门统计：一个部门一行，统计一个部门下所有计划的团队数、收、支
        ///// 按类型统计：2行记录（散客、团队）
        ///// 按时间统计：1个月一行记录，默认当前年份（出团日期所在月份）
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="type">毛利统计类型</param>
        ///// <param name="mSearch">搜索实体</param>
        ///// <returns>收入成本毛利列表</returns>
        //public IList<MGrossProfit> GetGrossProfitLst(int pageSize
        //                                            , int pageIndex
        //                                            , ref int recordCount
        //                                            , GrossProfitType type
        //                                            , MGrossProfitBase mSearch)
        //{
        //    IList<MGrossProfit> list = new List<MGrossProfit>();
        //    string tableName = GrossProfitTableName(type, mSearch);
        //    string orderBy = type == GrossProfitType.按时间统计 ? "y,m desc" : "TourNum DESC";
        //    MGrossProfit item = null;
        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db,pageSize,pageIndex,ref recordCount,
        //        tableName,"*",string.Empty,orderBy,false,string.Empty))
        //    {
        //        while (reader.Read())
        //        {
        //            item = new MGrossProfit();
        //            item.TourNum = (int)reader["TourNum"];
        //            item.IncomeTotal = (decimal)reader["SR"];
        //            item.OutgoTotal = (decimal)reader["ZC"];
        //            item.ToursPeoples = (int)reader["PeopleNum"];
        //            item.ProfitDistribute = (decimal)reader["Amount"];
        //            switch (type)
        //            {
        //                case GrossProfitType.按时间统计:
        //                    item.Year = (int)reader["y"];
        //                    item.Month = (int)reader["m"];
        //                    break;
        //                case GrossProfitType.按区域统计:
        //                    item.AreaId = (int)reader["AreaId"];
        //                    item.AreaName = reader.IsDBNull(reader.GetOrdinal("AreaName")) ? string.Empty : reader["AreaName"].ToString();
        //                    break;
        //                case GrossProfitType.按类型统计:
        //                    item.TourType = (int)reader["TourStatus"];
        //                    break;
        //                case GrossProfitType.按部门统计:
        //                    item.DepartmentId = (int)reader["DepartId"];
        //                    item.DepartmentName = reader.IsDBNull(reader.GetOrdinal("DepartName")) ? string.Empty : reader["DepartName"].ToString();
        //                    break;
        //            }
        //            list.Add(item);

        //        }
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// 根据毛利统计类型、搜索实体获取团量明细列表
        ///// 按区域统计：以个线路区域一行，统计线路区域下所有计划的团队数、收、支
        ///// 按部门统计：一个部门一行，统计一个部门下所有计划的团队数、收、支
        ///// 按类型统计：2行记录（散客、团队）
        ///// 按时间统计：1个月一行记录，默认当前年份（出团日期所在月份）
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="type">毛利统计类型</param>
        ///// <param name="mSearch">搜索实体</param>
        ///// <returns>团量明细列表</returns>
        //public IList<MTourDetail> GetTourDetailLstByTyp(int pageSize
        //                                                , int pageIndex
        //                                                , ref int recordCount
        //                                                , GrossProfitType type
        //                                                , MGrossProfitBase mSearch)
        //{
        //    string tableName = TourDetailTableName(type, mSearch);
        //    string fields = " TourId,RouteName,TourCode,LDate,Operator,SR,ZC,PeopleNum";
        //    string orderBy = "LDate DESC";
        //    IList<MTourDetail> list = new List<MTourDetail>();
        //    MTourDetail item = null;

        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
        //        tableName, fields,string.Empty, orderBy,false,string.Empty))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MTourDetail()
        //            {
        //                TourCode = reader.IsDBNull(reader.GetOrdinal("TourCode")) ? string.Empty : reader["TourCode"].ToString(),
        //                LDate = DateTime.Parse(reader["LDate"].ToString()),
        //                Incoming = (decimal)reader["SR"],
        //                Outgoing = (decimal)reader["ZC"],
        //                RouteName = reader.IsDBNull(reader.GetOrdinal("RouteName")) ? string.Empty : reader["RouteName"].ToString(),
        //                PeopleNum = (int)reader["PeopleNum"],
        //                Planer = reader.IsDBNull(reader.GetOrdinal("Operator")) ? string.Empty : reader["Operator"].ToString()
        //            });
        //        }
        //    }
        //    return list;
        //}

        //#endregion

        //#region 账龄分析表

        ///// <summary>
        ///// 根据搜索实体获取账龄分析列表
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="mSearch">搜索实体</param>
        ///// <returns>账龄分析列表</returns>
        //public IList<MAgingAnalysis> GetAgingAnalysisLst(int pageSize
        //                                                , int pageIndex
        //                                                , ref int recordCount
        //                                                , MAgingAnalysisBase mSearch)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select a.SellerId,b.ContactName,a.mMoney,a.date from ( ");
        //    sql.Append(" select a.SellerId,sum(b.ConfirmMoney) - sum(b.CheckMoney) + sum(b.ReturnMoney) mMoney,");
        //    sql.Append(" datediff(day,max(a.IssueTime),getdate()) date");
        //    sql.Append(" from tbl_Tour a");
        //    sql.Append(" left join tbl_TourOrder b");
        //    sql.Append(" on a.SellerId = b.SellerId");
        //    sql.AppendFormat(" where a.IsAllIncome = '1' and a.IsDelete = '0' and b.Status = {0}",(int)OrderStatus.已成交);
        //    #region 查询条件
        //    //团队类型
        //    if (mSearch.TourType != null)
        //    {
        //        sql.AppendFormat(" and a.TourType = {0}", (int)mSearch.TourType);
        //    }
        //    //线路区域
        //    if (mSearch.AreaId > 0)
        //    {
        //        sql.AppendFormat(" and a.AreaId = {0}", (int)mSearch.AreaId);
        //    }
        //    //销售员
        //    if (!string.IsNullOrEmpty(mSearch.SalesmanId))
        //    {
        //        sql.AppendFormat(" and b.SellerId in ({0})", mSearch.SalesmanId);
        //    }
        //    //出团时间-开始
        //    if (mSearch.LDateStart != null)
        //    {
        //        sql.AppendFormat(" and a.LDate >= '{0}'", mSearch.LDateStart);
        //    }
        //    //出团时间-结束
        //    if (mSearch.LDateEnd != null)
        //    {
        //        sql.AppendFormat(" and a.LDate <= '{0}'", mSearch.LDateEnd);
        //    }
        //    #endregion
        //    //公司编号
        //    sql.AppendFormat(" and a.CompanyId = '{0}'", mSearch.CompanyId);
        //    sql.Append(" group by a.SellerId");
        //    sql.Append(" having sum(b.ConfirmMoney) - sum(b.CheckMoney) + sum(b.ReturnMoney) > 0");
        //    sql.Append(" ) a left join tbl_ComUser b");
        //    sql.Append(" on a.SellerId = b.UserId");

        //    string fileds = "SellerId,ContactName,date,mMoney";
        //    string orderBy = "date desc";

        //    IList<MAgingAnalysis> list = new List<MAgingAnalysis>();

        //    MAgingAnalysis item = null;

        //    using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
        //        sql.ToString(), fileds, string.Empty, orderBy, false, string.Empty))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(
        //                item = new MAgingAnalysis()
        //                {
        //                    SalesmanId = reader["SellerId"].ToString(),
        //                    Salesman = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
        //                    ArrearsTime = (int)reader["date"],
        //                    ArrearsTotal = (decimal)reader["mMoney"]
        //                });
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 查看拖欠款明细
        ///// </summary>
        ///// <param name="companyId">公司编号</param>
        ///// <param name="sellerId">销售员编号</param>
        ///// <returns>拖欠款列表明细</returns>
        //public IList<MAgingAnalysis> GetAgingAnalysisLst(string companyId, string sellerId)
        //{
        //    IList<MAgingAnalysis> list = new List<MAgingAnalysis>();
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select b.TourCode,b.RouteName,b.LDate,a.BuyCompanyName,a.Adults+a.Childs+a.Others People,");
        //    sql.Append(" c.ContactName,a.ConfirmMoney,a.CheckMoney");
        //    sql.Append(" from tbl_TourOrder a");
        //    sql.Append(" left join tbl_Tour b");
        //    sql.Append(" on a.TourId = b.TourId");
        //    sql.Append(" left join tbl_ComUser c");
        //    sql.Append(" on a.SellerId = c.UserId");
        //    sql.Append(" where a.SellerId = @sellerId and a.CompanyId = @companyId");
        //    DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
        //    this._db.AddInParameter(comm, "@sellerId", DbType.AnsiStringFixedLength, sellerId);
        //    this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

        //    MAgingAnalysis item = null;
        //    using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MAgingAnalysis()
        //            {
        //                TourCode = reader.IsDBNull(reader.GetOrdinal("TourCode")) ? string.Empty : reader["TourCode"].ToString(),
        //                RouteName = reader.IsDBNull(reader.GetOrdinal("RouteName")) ? string.Empty : reader["RouteName"].ToString(),
        //                LDate = reader.IsDBNull(reader.GetOrdinal("LDate")) ? null : (DateTime?)reader["LDate"],
        //                CompanyName = reader.IsDBNull(reader.GetOrdinal("BuyCompanyName")) ? string.Empty : reader["BuyCompanyName"].ToString(),
        //                PeopleNum = (int)reader["People"],
        //                Salesman = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
        //                TotalNum = (decimal)reader["ConfirmMoney"],
        //                YNum = (decimal)reader["CheckMoney"]
        //            });
        //        }
        //    }

        //    return list;
        //}

        //#endregion

        

        //#region private

        ///// <summary>
        ///// 成本毛利统计-返回sql语句
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="mSearch"></param>
        ///// <returns></returns>
        //private string GrossProfitTableName(GrossProfitType type, MGrossProfitBase mSearch)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    StringBuilder query = new StringBuilder();
            
        //    query.AppendFormat(" where a.CompanyId = '{0}'", mSearch.CompanyId);
        //    #region 条件
        //    //团队类型
        //    if (mSearch.TourType == 1 || mSearch.TourType == 2)
        //    {
        //        query.Append(" and case a.TourStatus when 0 then 1"); //团队1散客2
        //        query.Append(" when 1 then 2");
        //        query.Append(" when 2 then 1");
        //        query.Append(" when 3 then 2");
        //        query.Append(" when 4 then 1");
        //        query.Append(" when 5 then 2");
        //        query.AppendFormat(" when 7 then 2 end  = {0}", mSearch.TourType);
        //    }
        //    if (mSearch.LDateStart != null)
        //    {
        //        query.AppendFormat(" and a.LDate >= '{0}'", mSearch.LDateStart);
        //    }
        //    if (mSearch.LDateEnd != null)
        //    {
        //        query.AppendFormat(" and a.LDate <= '{0}'", mSearch.LDateEnd);
        //    }
        //    if (mSearch.ChkOverTimeStart != null)
        //    {
        //        query.AppendFormat(" and a.ReviewTime >= '{0}'", mSearch.ChkOverTimeStart);
        //    }
        //    if (mSearch.ChkOverTimeEnd != null)
        //    {
        //        query.AppendFormat(" and a.ReviewTime <= '{0}'", mSearch.ChkOverTimeEnd);
        //    }
        //    #endregion
        //    switch (type)
        //    {
        //        case GrossProfitType.按部门统计:
        //            #region
        //            sql.Append("select a.DepartId,a.DepartName,");
        //            sql.Append(" isnull(b.TourNum,0) TourNum,isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum,isnull(b.Amount,0)Amount");
        //            sql.Append(" from tbl_ComDepartment a");
        //            sql.Append(" left join(");
        //            sql.Append(" select d.DepartId,count(a.TourId)TourNum,sum(b.SR)SR,sum(b.ZC)ZC,sum(b.PeopleNum)PeopleNum,");
        //            sql.Append(" sum(b.Amount)Amount");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(" left join tbl_ComUser c on a.OperatorId = c.UserId");
        //            sql.Append(" left join tbl_ComDepartment d on c.DeptId = d.DepartId ");
        //            sql.Append(query.ToString());
        //            sql.Append(" group by d.DepartId");
        //            sql.Append(" )b on a.DepartId = b.DepartId");
        //            sql.AppendFormat(" where a.CompanyId = '{0}'", mSearch.CompanyId);
        //            #endregion
        //            break;
        //        case GrossProfitType.按类型统计:
        //            #region
        //            sql.Append("select case a.TourStatus when 0 then 1");//团队1散客2
        //            sql.Append(" when 1 then 2");
        //            sql.Append(" when 2 then 1");
        //            sql.Append(" when 3 then 2");
        //            sql.Append(" when 4 then 1");
        //            sql.Append(" when 5 then 2");
        //            sql.Append(" when 7 then 2 end as TourStatus,");
        //            sql.Append(" count(a.TourId)TourNum,sum(isnull(b.SR,0))SR,sum(isnull(b.ZC,0))ZC,");
        //            sql.Append(" sum(isnull(b.PeopleNum,0))PeopleNum,sum(isnull(b.Amount,0))Amount");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(" left join tbl_ComUser c on a.OperatorId = c.UserId");
        //            sql.Append(query.ToString());
        //            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
        //            {
        //                sql.AppendFormat(" and a.OperatorId = '{0}'", mSearch.SalesmanId);
        //            }
        //            if (mSearch.DepartmentId > 0)
        //            {
        //                sql.AppendFormat(" and c.DeptId = {0}", mSearch.DepartmentId);
        //            }
        //            sql.AppendFormat("");
        //            sql.Append(" group by a.TourStatus");
        //            #endregion
        //            break;
        //        case GrossProfitType.按区域统计:
        //            #region
        //            sql.Append("select a.AreaId,a.AreaName,");
        //            sql.Append(" isnull(b.TourNum,0) TourNum,isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum,isnull(b.Amount,0)Amount");
        //            sql.Append(" from tbl_ComArea a");
        //            sql.Append(" left join(");
        //            sql.Append(" select a.AreaId,count(a.TourId)TourNum,sum(b.SR)SR,sum(b.ZC)ZC,sum(b.PeopleNum)PeopleNum,");
        //            sql.Append(" sum(b.Amount)Amount");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(query.ToString());
        //            sql.Append(" group by a.AreaId");
        //            sql.Append(" ) b on a.AreaId = b.AreaId");
        //            sql.AppendFormat(" where a.CompanyId = '{0}'", mSearch.CompanyId);

        //            #endregion
        //            break;
        //        case GrossProfitType.按时间统计:
        //            #region
        //            if (mSearch.LDateStart == null)
        //            {
        //                mSearch.LDateStart = DateTime.Now.AddMonths(-DateTime.Now.Month); //默认当前时间1月份
        //            }
        //            if (mSearch.LDateEnd == null)
        //            {
        //                mSearch.LDateEnd = DateTime.Now.AddMonths(12 - DateTime.Now.Month); //默认当前时间12月份
        //            }
        //            sql.AppendFormat("select year(dateadd(month,a.number-1,'{0}')) y,month(dateadd(month,a.number-1,'{0}')) m,", mSearch.LDateStart, mSearch.LDateStart);
        //            sql.Append(" isnull(b.TourNum,0) TourNum,isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum,isnull(b.Amount,0)Amount");
        //            sql.Append(" from master..spt_values a ");
        //            sql.Append(" left join (");
        //            sql.Append(" select month(a.LDate) m,count(a.TourId)TourNum,sum(b.SR)SR,sum(b.ZC)ZC,sum(b.PeopleNum)PeopleNum,");
        //            sql.Append(" sum(b.Amount)Amount");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(query.ToString());
        //            if (mSearch.AreaId > 0)
        //            {
        //                sql.AppendFormat(" and a.AreaId = {0}", mSearch.AreaId);
        //            }
        //            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
        //            {
        //                sql.AppendFormat(" and a.OperatorId = '{0}'", mSearch.SalesmanId);
        //            }
        //            sql.Append(" group by year(a.LDate),month(a.LDate)");
        //            sql.Append(" ) b on a.number = b.m");
        //            sql.AppendFormat(" where a.[type] = 'P' and a.number between 1 and datediff(month,'{0}','{1}')+1", mSearch.LDateStart, mSearch.LDateEnd);
        //            #endregion
        //            break;
        //    }
        //    return sql.ToString();
        //}

        ///// <summary>
        ///// 成本毛利统计-团量列表-返回sql语句
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="mSearch"></param>
        ///// <returns></returns>
        //private string TourDetailTableName(GrossProfitType type, MGrossProfitBase mSearch)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    switch (type)
        //    {
        //        case GrossProfitType.按部门统计:
        //            sql.Append("select a.TourId,a.RouteName,a.TourCode,a.LDate,a.Operator,isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(" left join tbl_ComUser c on a.OperatorId = c.UserId");
        //            sql.AppendFormat(" where c.DeptId = {0} and a.CompanyId = '{0}'", mSearch.DepartmentId, mSearch.CompanyId);
        //            break;
        //        case GrossProfitType.按类型统计:
        //            sql.Append("select a.TourId,a.RouteName,a.TourCode,a.LDate,a.Operator,");
        //            sql.Append(" case a.TourStatus when 0 then 1 ");//团队1散客2
        //            sql.Append(" when 1 then 2");
        //            sql.Append(" when 2 then 1");
        //            sql.Append(" when 3 then 2");
        //            sql.Append(" when 4 then 1");
        //            sql.Append(" when 5 then 2");
        //            sql.Append(" when 7 then 2 end as TourStatus,");
        //            sql.Append(" isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.Append(" left join tbl_ComUser c on a.OperatorId = c.UserId");
        //            sql.AppendFormat(" where a.TourStatus = {0} and a.CompanyId = '{0}'", mSearch.TourType, mSearch.CompanyId);
        //            break;
        //        case GrossProfitType.按区域统计:
        //            sql.Append("select a.TourId,a.RouteName,a.TourCode,a.LDate,a.Operator,isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,");
        //            sql.Append(" isnull(b.PeopleNum,0)PeopleNum");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.AppendFormat(" where a.AreaId = {0} and a.CompanyId = '{0}'", mSearch.AreaId, mSearch.CompanyId);
        //            break;
        //        case GrossProfitType.按时间统计:
        //            sql.Append("select a.TourId,a.RouteName,a.TourCode,a.LDate,a.Operator,");
        //            sql.Append(" isnull(b.SR,0)SR,isnull(b.ZC,0)ZC,isnull(b.PeopleNum,0)PeopleNum");
        //            sql.Append(" from tbl_Tour a");
        //            sql.AppendFormat(" left join fun_Statistics_GrossProfit({0},{1},'{2}') b on a.TourId = b.TourId ", (int)PlanState.已落实, (int)OrderStatus.已成交, mSearch.CompanyId);
        //            sql.AppendFormat(" where year(a.LDate) = {0} and month(a.LDate) = {1} and a.CompanyId = '{2}'", mSearch.Year, mSearch.Month, mSearch.CompanyId);
        //            break;
        //    }
        //    return sql.ToString();
        //}

        ///// <summary>
        ///// 收支对账单-返回sql语句
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="mSearch"></param>
        ///// <returns></returns>
        //private string ReconciliationTableName(ItemType type, MReconciliationBase mSearch)  
        //{
        //    StringBuilder sql = new StringBuilder();
        //    StringBuilder where = new StringBuilder();
        //    #region where
        //    where.AppendFormat(" where a.CompanyId = '{0}'",mSearch.CompanyId);
        //    if (mSearch.TourType == 1 || mSearch.TourType == 2)
        //    {
        //        //--团队1散客2
        //        where.Append(" and case a.TourStatus when 0 then 1");
        //        where.Append(" when 1 then 2");
        //        where.Append(" when 2 then 1");
        //        where.Append(" when 3 then 2");
        //        where.Append(" when 4 then 1");
        //        where.Append(" when 5 then 2");
        //        where.AppendFormat(" when 7 then 2 end  = {0}", mSearch.TourType);
        //    }
        //    if (mSearch.AreaId > 0)
        //    {
        //        where.AppendFormat(" and a.AreaId = {0}", mSearch.AreaId);
        //    }
        //    if (!string.IsNullOrEmpty(mSearch.OperatorId))
        //    {
        //        sql.AppendFormat(" and a.UserId in ({0})", mSearch.OperatorId);
        //    }
        //    if (mSearch.LDateStart != null)
        //    {
        //        where.AppendFormat(" and a.LDate >= '{0}'", mSearch.LDateStart);
        //    }
        //    else if (mSearch.LDateEnd != null)
        //    {
        //        where.AppendFormat(" and a.LDate <= '{0}'", mSearch.LDateEnd);
        //    }
        //    else
        //    {
        //        //默认统计当年数据
        //        where.Append(" and year(a.LDate) = year(getdate())");
        //    }
        //    #endregion
        //    switch (type)
        //    {
        //        case ItemType.收入:
        //            sql.Append(" select a.ContactName,a.UserId,isnull(b.S,0) S,isnull(b.Y,0) Y  from (");
        //            sql.Append(" select d.SellerId,sum(isnull(d.ConfirmMoney,0))S,sum(isnull(d.CheckMoney,0))Y");
        //            sql.Append(" from tbl_TourOrder d");
        //            sql.Append(" left join tbl_Plan c on d.TourId = c.TourId");
        //            sql.Append(" left join tbl_Tour a on d.TourId = a.TourId ");
        //            sql.Append(where.ToString());
        //            sql.AppendFormat(" and a.TourStatus = {0} and c.Status = {1} and c.IsOut = '0' and a.IsDelete = '0'", (int)OrderStatus.已成交, (int)PlanState.已落实);
        //            sql.Append(" group by d.SellerId ) b");
        //            sql.Append(" inner join tbl_ComUser a");
        //            sql.Append(" on b.SellerId = a.UserId");
        //            sql.AppendFormat(" where a.CompanyId = '{0}'", mSearch.CompanyId);
        //            break;
        //        case ItemType.支出:
        //            sql.Append("select a.ContactName,a.UserId,isnull(b.S,0)S,isnull(b.Y,0)Y from (");
        //            sql.Append(" select b.OperatorId,sum(isnull(b.Confirmation,0))S,sum(isnull(b.Prepaid,0))Y");
        //            sql.Append(" from tbl_Plan b");
        //            sql.Append(" left join tbl_Tour a");
        //            sql.Append(" on b.TourId = a.TourId ");
        //            sql.Append(where.ToString());
        //            sql.AppendFormat(" and b.IsOut = '1' and b.Status = {0}", (int)PlanState.已落实);
        //            sql.Append(" group by b.OperatorId ) b");
        //            sql.Append(" inner join tbl_ComUser a");
        //            sql.Append(" on b.OperatorId = a.UserId");
        //            sql.AppendFormat(" where a.CompanyId = '{0}'", mSearch.CompanyId);
        //            break;
        //    }
        //    return sql.ToString();
        //}

        ///// <summary>
        ///// 收支对账单-明细列表-返回sql语句
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="operatorId"></param>
        ///// <returns></returns>
        //private string RestDetailTableName(ItemType type, string operatorId)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    switch (type)
        //    {
        //        case ItemType.支出:
        //            sql.Append("select a.SourceName,a.OperatorName [Name],a.Confirmation Y,a.Prepaid S,");
        //            sql.Append(" b.RouteName,b.TourCode,b.LDate");
        //            sql.Append(" from tbl_Plan a");
        //            sql.Append(" left join tbl_Tour b");
        //            sql.Append(" on a.TourId = b.TourId");
        //            sql.AppendFormat(" where a.IsOut = '1' and b.TourStatus = {0}", (int)PlanState.已落实);
        //            sql.AppendFormat(" and a.OperatorId = '{0}'", operatorId);
        //            break;
        //        case ItemType.收入:
        //            sql.Append("select a.BuyCompanyName as SourceName,a.Adults+a.Childs+a.Others as PeopleNum,");
        //            sql.Append(" a.SellerName [Name],a.ConfirmMoney Y,a.CheckMoney S,b.RouteName,b.TourCode,b.LDate");
        //            sql.Append(" from tbl_TourOrder a");
        //            sql.Append(" left join tbl_Tour b");
        //            sql.Append(" on a.TourId = b.TourId");
        //            sql.AppendFormat(" where a.Status = {0} and a.IsDelete = '0' and a.SellerId = '{1}'", (int)OrderStatus.已成交, operatorId);
        //            break;
        //    }

        //    return sql.ToString();
        //}

        //#endregion
    }
}
