using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using System.Xml;
using EyouSoft.Model.EnumType.CrmStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.DAL.CrmStructure
{
    using EyouSoft.Model.CrmStructure;
    using EyouSoft.Model.EnumType.PlanStructure;

    /// <summary>
    /// 团队回访
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public class DCrmVisit:EyouSoft.Toolkit.DAL.DALBase,IDAL.CrmStructure.ICrmVisit
    {
        #region 私有变量
        private Database _db = null;

        /// <summary>
        /// 添加团队回访的存储过程
        /// </summary>
        private string proc_AddVisit = "proc_AddVisit";

        /// <summary>
        /// 添加团队回访的sql语句
        /// </summary>
        private string sql_Crm_SelectVisit = "select * from tbl_CrmVisit where VisitId=@VisitId";
        #endregion

        #region dal变量
        EyouSoft.DAL.CrmStructure.DCrmVisitDetail visitDetailDal = new DCrmVisitDetail();
        EyouSoft.DAL.TourStructure.DTour tourDal = new EyouSoft.DAL.TourStructure.DTour();
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DCrmVisit()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 添加团队回访Model
        /// </summary>
        /// <param name="model">团队回访Model</param>
        /// <returns>返回值 小于0:错误 1:正确</returns>
        public int AddCrmVisitModel(Model.CrmStructure.MCrmVisit model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddVisit);
            _db.AddInParameter(dc, "xmlVisitString", DbType.Xml, GetVisitXmlString(model));
            _db.AddInParameter(dc, "xmlDetialString", DbType.Xml, visitDetailDal.GetVisitDetailXmlString(model.VisitDetailList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }
            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 修改团队回访Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCrmVisitModel(Model.CrmStructure.MCrmVisit model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddVisit);
            _db.AddInParameter(dc, "VisitId", DbType.AnsiStringFixedLength, model.VisitId);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(dc, "Identity", DbType.Byte, (int)model.Identity);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "Telephone", DbType.AnsiString, model.Telephone);
            _db.AddInParameter(dc, "ReturnType", DbType.Byte, (int)model.ReturnType);
            _db.AddInParameter(dc, "Total", DbType.String, model.Total);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "QualityScore", DbType.Double, model.QualityScore);
            _db.AddInParameter(dc, "xmlDetialString", DbType.Xml, visitDetailDal.GetVisitDetailXmlString(model.VisitDetailList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }
            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return 1;
            }
        }


        /// <summary>
        /// 获得显示在团队回访页面上的列表数据
        /// </summary>
        /// <param name="model">团队回访列表显示Model</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MVisitListModel> GetVisitShowModel(Model.CrmStructure.MVisitListModel model, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.CrmStructure.MVisitListModel> list = new List<Model.CrmStructure.MVisitListModel>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append(" TourId ,");
            field.Append(" TourCode ,");
            field.Append(" RouteId ,");
            field.Append(" RouteName ,");
            field.Append(" RDate ,");
            field.Append(" Customer = STUFF(( SELECT   ',' + BuyCompanyName");
            field.Append("                    FROM     ( SELECT    BuyCompanyName");
            field.Append("                               FROM      dbo.tbl_TourOrder");
            field.Append("                               WHERE     TourId = dbo.tbl_Tour.TourId");
            field.Append("                                         AND IsDelete = '0'");
            field.AppendFormat("                                         AND Status = {0}",(int)OrderStatus.已成交);
            field.Append("                             ) AS A");
            field.Append("                  FOR");
            field.Append("                    XML PATH('')");
            field.Append("                  ), 1, 1, '') ,");
            field.Append(" SellerName ,");
            field.Append(" GuideName = STUFF(( SELECT  ',' + SourceName");
            field.Append("                 FROM    ( SELECT    SourceName");
            field.Append("                           FROM      dbo.tbl_Plan");
            field.Append("                           WHERE     TourId = dbo.tbl_Tour.TourId");
            field.Append("                                     AND IsDelete = '0'");
            field.AppendFormat("                                     AND Type = {0}",(int)PlanProject.导游);
            field.AppendFormat("                                     AND Status = {0}",(int)PlanState.已落实);
            field.Append("                         ) AS A");
            field.Append("               FOR");
            field.Append("                 XML PATH('')");
            field.Append("               ), 1, 1, '') ,");
            field.Append(" Planer = STUFF(( SELECT ',' + Planer");
            field.Append("                  FROM   ( SELECT    Planer");
            field.Append("                           FROM      tbl_TourPlaner");
            field.Append("                           WHERE     TourId = dbo.tbl_Tour.TourId");
            field.Append("                         ) AS A");
            field.Append("                FOR");
            field.Append("                  XML PATH('')");
            field.Append("                ), 1, 1, '') ,");
            field.Append(" TourStatus ,");
            field.Append(" QualityScore = ISNULL(( SELECT  ISNULL(SUM(QualityScore), 0)");
            field.Append("                                 / COUNT(*)");
            field.Append("                         FROM    dbo.tbl_CrmVisit");
            field.Append("                         WHERE   TourId = dbo.tbl_Tour.TourId");
            field.Append("                         HAVING  COUNT(*) > 0");
            field.Append("                       ), 0)");

            query.AppendFormat(" CompanyId = '{0}' AND IsDelete = '0'",model.CompanyId);
            query.AppendFormat(" AND TourStatus BETWEEN {0} AND {1}",(int)TourStatus.计调配置完毕,(int)TourStatus.封团);
            query.Append(" AND ISNULL(ParentId, '') <> ''");
            if (!string.IsNullOrEmpty(model.TourCode))
            {query.AppendFormat(" AND TourCode LIKE '%{0}%'",model.TourCode);}
            if (!string.IsNullOrEmpty(model.RouteName))
            {query.AppendFormat(" AND RouteName LIKE '%{0}%'",model.RouteName);}
            if (startDate != null)
            { query.AppendFormat(" AND RDate >= '{0}'", startDate); }
            if (endDate != null)
            {query.AppendFormat(" AND RDate <= '{0}'",endDate);}
            //if (!string.IsNullOrEmpty(model.UnitName))
            //{query.Append("                     AND BuyCompanyId = '1'");}
            //else 
            if (!string.IsNullOrEmpty(model.UnitName))
            {
                query.Append(" AND EXISTS ( SELECT 1");
                query.Append("              FROM   dbo.tbl_TourOrder");
                query.Append("              WHERE  TourId = dbo.tbl_Tour.TourId");
                query.Append("                     AND IsDelete = '0'");
                query.AppendFormat("                     AND Status = {0}", (int)OrderStatus.已成交);
                query.AppendFormat("                     AND BuyCompanyName LIKE '%{0}%'", model.UnitName);
                query.Append(" )");
            }
            //query.Append("                     AND SourceName LIKE '%%' )");
            if (model.GuideName != null && model.GuideName.Length != 0)
            {
                query.Append(" AND EXISTS ( SELECT 1");
                query.Append("              FROM   dbo.tbl_Plan");
                query.Append("              WHERE  TourId = dbo.tbl_Tour.TourId");
                query.Append("                     AND IsDelete = '0'");
                query.AppendFormat("                     AND Status = {0}", (int)PlanState.已落实);
                query.AppendFormat("                     AND Type = {0}", (int)PlanProject.导游);
                query.Append(" and ");
                for (var i = 0; i < model.GuideName.Length; i++)
                {
                    query.AppendFormat(
                        i == model.GuideName.Length - 1 ? " SourceName like '%{0}%' " : " SourceName like '%{0}%' or",
                        model.GuideName[i]);
                }
                query.Append(" ) ");
            }

            //XmlDocument xml = null;
            using (var reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_Tour", "TourId", field.ToString(), query.ToString(), " IssueTime desc"))
            {
                while (reader.Read())
                {
                    var visitListModel = new EyouSoft.Model.CrmStructure.MVisitListModel { CompanyId = model.CompanyId };
                    if (!reader.IsDBNull(reader.GetOrdinal("GuideName")))
                    {
                        var lst = reader["GuideName"].ToString().Split(',').ToList();
                        //xml = new XmlDocument();
                        //xml.LoadXml(reader["GuideName"].ToString());
                        visitListModel.GuideName = new string[lst.Count];
                        for (var i = 0; i < lst.Count; i++)
                        {
                            visitListModel.GuideName[i] = lst[i];
                        }
                    }
                    visitListModel.PlanerName = reader["Planer"].ToString();
                    visitListModel.RouteId = !reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (string)reader["RouteId"] : string.Empty;
                    visitListModel.RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty;
                    visitListModel.SalesmanName = !reader.IsDBNull(reader.GetOrdinal("SellerName")) ? (string)reader["SellerName"] : string.Empty;
                    visitListModel.RDate = !reader.IsDBNull(reader.GetOrdinal("RDate")) ? (DateTime?)reader["RDate"] : null;
                    visitListModel.TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty;
                    visitListModel.TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty;
                    visitListModel.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)(int.Parse(reader["TourStatus"].ToString()));
                    visitListModel.UnitName = !reader.IsDBNull(reader.GetOrdinal("Customer")) ? reader["Customer"].ToString() : string.Empty;
                    visitListModel.QualityScore = float.Parse( reader["QualityScore"].ToString());
                    //if (!reader.IsDBNull(reader.GetOrdinal("ReturnType")))
                    //{
                    //    visitListModel.ReturnType = (Model.EnumType.CrmStructure.CrmReturnType)((int)reader["ReturnType"]);
                    //}
                    //visitListModel.Total = !reader.IsDBNull(reader.GetOrdinal("Total")) ? (string)reader["Total"] : string.Empty;
                    //visitListModel.Id = (int)reader["Id"];
                    list.Add(visitListModel);
                }
            }
            return list;
        }


        /// <summary>
        /// 获得团队回访每日汇总表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MDayTotalModel> GetDayTotalModelList(string CompanyId, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder sbWhere=new StringBuilder();
            if(startTime==null&&endTime==null)
            {
                sbWhere.Append(" datediff(day,rdate,getdate())=0 ");
            }
            else if(startTime==null&&endTime!=null)
            {
                sbWhere.AppendFormat(" datediff(day,'{0}',rdate)<=0 ", endTime);
            }
            else if(startTime!=null&&endTime==null)
            {
                sbWhere.AppendFormat(" datediff(day,'{0}',rdate)>=0 ", startTime);
            }
            else
            {
                sbWhere.AppendFormat(" datediff(day,rdate,'{0}')<=0 and datediff(day,rdate,'{1}')>=0 ", startTime, endTime);
            }
            IList<Model.CrmStructure.MDayTotalModel> list = new List<Model.CrmStructure.MDayTotalModel>();
            Model.CrmStructure.MDayTotalModel item = new MDayTotalModel();
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_visiedayview", "TourId", "*", sbWhere.ToString(), " rdate desc"))
            {
                XmlDocument xml = null;
                while (reader.Read())
                {
                    
                    //if (!reader.IsDBNull(reader.GetOrdinal("GuideName")))
                    //{
                    //    xml = new XmlDocument();
                    //    xml.LoadXml(reader["GuideName"].ToString());
                    //    item.GuideName = new string[xml.DocumentElement.ChildNodes.Count];
                    //    for (int i = 0; i < xml.DocumentElement.ChildNodes.Count; i++)
                    //    {
                    //        item.GuideName[i] = xml.DocumentElement.ChildNodes[i].FirstChild.Value;
                    //    }
                    //}
                    list.Add(new MDayTotalModel
                        {
                            CompanyId = CompanyId,
                            DateTime = (DateTime)reader["ldate"],
                            Id = (int)reader["Id"],
                            Planer = !reader.IsDBNull(reader.GetOrdinal("OperatorName")) ? (string)reader["OperatorName"] : string.Empty,
                            QualityScore = float.Parse(reader["QualityScore"].ToString()),
                            ReturnType = (Model.EnumType.CrmStructure.CrmReturnType)(reader.GetByte(reader.GetOrdinal("ReturnType"))),
                            RouteId = !reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (string)reader["RouteId"] : string.Empty,
                            RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty,
                            Seller = !reader.IsDBNull(reader.GetOrdinal("SellerName")) ? (string)reader["SellerName"] : string.Empty,
                            TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty,
                            TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty,
                            GuideName = reader["GuideName"].ToString().Split(','),
                });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取回访明细
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="startTime">时间</param>
        /// <returns>回访明细集合</returns>
        public IList<Model.CrmStructure.MCrmVisit> GetCrmVisit(int pageIndex, int pageSize, ref int recordCount,string companyId,string tourId, DateTime? startTime,DateTime? endTime)
        {
            var list = new List<Model.CrmStructure.MCrmVisit>();
            var sql = new StringBuilder();

            sql.AppendFormat(" 	   CompanyId = '{0}' and TourId='{1}'		",companyId,tourId);
            if (startTime.HasValue)
            {
                sql.AppendFormat(" 	        AND CONVERT(VARCHAR(19),IssueTime,111) >= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)		", startTime.Value);
            }
            if (endTime.HasValue)
            {
                sql.AppendFormat(" 	        AND CONVERT(VARCHAR(19),IssueTime,111) <= CONVERT(VARCHAR(19),CAST('{0}' AS DATETIME),111)		", endTime.Value);
            }

            using (var reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_CrmVisit", "VisitId", "VisitId,ReturnType,[Identity],Name,Telephone,QualityScore,Total", sql.ToString(), " IssueTime desc"))
            {
                while (reader.Read())
                {
                    list.Add(new EyouSoft.Model.CrmStructure.MCrmVisit()
                    {
                        VisitId = reader["VisitId"].ToString(),
                        Identity = (CrmIdentity)Enum.Parse(typeof(CrmIdentity), reader["Identity"].ToString()),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                        Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? string.Empty : reader["Telephone"].ToString(),
                        ReturnType = (CrmReturnType)Enum.Parse(typeof(CrmReturnType), reader["ReturnType"].ToString()),
                        QualityScore = float.Parse(reader["QualityScore"].ToString()),
                        Total = reader["Total"].ToString()
                    });
                     
                }
            }
            return list;
        }
        /// <summary>
        /// 获得团队回访Model
        /// </summary>
        /// <param name="visitId">团队回访编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmVisit GetVisitModel(string tourId,string visitId)
        {
            Model.CrmStructure.MCrmVisit visitModel = new EyouSoft.Model.CrmStructure.MCrmVisit();
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectVisit);
            _db.AddInParameter(dc, "VisitId", DbType.AnsiStringFixedLength, visitId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    visitModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    visitModel.Identity = (EyouSoft.Model.EnumType.CrmStructure.CrmIdentity)(int.Parse(reader["Identity"].ToString()));
                    visitModel.IssueTime = (DateTime)reader["IssueTime"];
                    visitModel.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    visitModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty;
                    visitModel.QualityScore = float.Parse(reader["QualityScore"].ToString());
                    visitModel.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? (string)reader["Telephone"] : string.Empty;
                    visitModel.Total = !reader.IsDBNull(reader.GetOrdinal("Total")) ? (string)reader["Total"] : string.Empty;
                    visitModel.TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty;
                    visitModel.ReturnType = (EyouSoft.Model.EnumType.CrmStructure.CrmReturnType)(int.Parse(reader["ReturnType"].ToString()));
                    visitModel.VisitId = visitId;
                }
            }
            visitModel.VisitDetailList = visitDetailDal.GetVisitDetailModel(string.IsNullOrEmpty(tourId) ? visitModel.TourId : tourId, string.IsNullOrEmpty(visitId) ? visitModel.VisitId : visitId);
            return visitModel;
        }

        #endregion

        #region 私有方法

       

        /// <summary>
        /// 获得团队回访Xml语句
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetVisitXmlString(Model.CrmStructure.MCrmVisit model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }
        #endregion
    }
}
