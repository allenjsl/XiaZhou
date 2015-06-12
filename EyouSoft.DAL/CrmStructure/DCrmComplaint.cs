using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using System.Xml;

namespace EyouSoft.DAL.CrmStructure
{
    /// <summary>
    /// 投书管理
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public class DCrmComplaint : EyouSoft.Toolkit.DAL.DALBase, IDAL.CrmStructure.ICrmComplaint
    {
        #region 私有变量
        private Database _db = null;

        /// <summary>
        /// 添加投诉管理的存储过程
        /// </summary>
        private string proc_AddComplaint = "proc_AddComplaint";

        /// <summary>
        /// 查询投诉管理的sql语句
        /// </summary>
        private string sql_Crm_SelectComplaint = "select * from tbl_CrmComplaint where ComplaintsId=@ComplaintId";
        #endregion

        #region dal变量

        #endregion
        #region 构造函数
        public DCrmComplaint()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 添加投诉管理Model
        /// </summary>
        /// <param name="model">投诉管理Model</param>
        /// <returns>返回值 小于0 错误 1:正确</returns>
        public int AddCrmComplaintModel(Model.CrmStructure.MCrmComplaint model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddComplaint);

            _db.AddInParameter(dc, "xmlstring", DbType.Xml, GetComplaintXmlString(model));

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
        /// 获得投诉管理Model
        /// </summary>
        /// <param name="ComplaintId">投诉管理编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmComplaint GetCrmComplaintModel(string ComplaintId)
        {
            Model.CrmStructure.MCrmComplaint complaintModel = new EyouSoft.Model.CrmStructure.MCrmComplaint();
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectComplaint);
            _db.AddInParameter(dc, "ComplaintId", DbType.AnsiStringFixedLength, ComplaintId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    complaintModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    complaintModel.ComplaintsId = ComplaintId;
                    complaintModel.ComplaintsIdentity = !reader.IsDBNull(reader.GetOrdinal("ComplaintsIdentity")) ? (string)reader["ComplaintsIdentity"] : string.Empty;
                    complaintModel.ComplaintsName = !reader.IsDBNull(reader.GetOrdinal("ComplaintsName")) ? (string)reader["ComplaintsName"] : string.Empty;
                    complaintModel.ComplaintsOpinion = !reader.IsDBNull(reader.GetOrdinal("ComplaintsOpinion")) ? (string)reader["ComplaintsOpinion"] : string.Empty;
                    complaintModel.ComplaintsTel = !reader.IsDBNull(reader.GetOrdinal("ComplaintsTel")) ? (string)reader["ComplaintsTel"] : string.Empty;
                    complaintModel.ComplaintsTime = !reader.IsDBNull(reader.GetOrdinal("ComplaintsTime")) ? (DateTime?)reader["ComplaintsTime"] : null;
                    complaintModel.ComplaintsType = !reader.IsDBNull(reader.GetOrdinal("ComplaintsType")) ? (string)reader["ComplaintsType"] : string.Empty;
                    complaintModel.HandleName = !reader.IsDBNull(reader.GetOrdinal("HandleName")) ? (string)reader["HandleName"] : string.Empty;
                    complaintModel.HandleOpinion = !reader.IsDBNull(reader.GetOrdinal("HandleOpinion")) ? (string)reader["HandleOpinion"] : string.Empty;
                    complaintModel.HandleTime = !reader.IsDBNull(reader.GetOrdinal("HandleTime")) ? (DateTime?)reader["HandleTime"] : null;
                    complaintModel.IsHandle = !reader.IsDBNull(reader.GetOrdinal("IsHandle")) ? reader["IsHandle"].ToString() == "0" ? false : true : false;
                    complaintModel.IssueTime = (DateTime)reader["IssueTime"];
                    complaintModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty;
                    complaintModel.TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty;

                }
            }
            return complaintModel;
        }



        /// <summary>
        /// 获得投诉管理显示列表页面上的数据
        /// </summary>
        /// <param name="model">投诉管理列表页面搜索Model</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MComplaintsListModel> GetVisitShowModel(Model.CrmStructure.MComplaintsSearchModel model, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.CrmStructure.MComplaintsListModel> list = new List<Model.CrmStructure.MComplaintsListModel>();
            StringBuilder sbWhereSql = new StringBuilder();
            sbWhereSql.AppendFormat(" CompanyId='{0}' ", model.CompanyId);
            if (!string.IsNullOrEmpty(model.TourCode))
            {
                sbWhereSql.AppendFormat(" and TourCode like '%{0}%' ", model.TourCode);
            }
            if (!string.IsNullOrEmpty(model.RouteName))
            {
                sbWhereSql.AppendFormat(" and RouteName like '%{0}%' ", model.RouteName);
            }
            if (model.StartTime != null)
            {
                sbWhereSql.AppendFormat(" and ComplaintsTime>='{0}'", model.StartTime.ToString());
            }
            if (model.EndTime != null)
            {
                sbWhereSql.AppendFormat(" and ComplaintsTime<='{0}' ", model.EndTime.ToString());
            }
            if (!string.IsNullOrEmpty(model.ComplaintsName))
            {
                sbWhereSql.AppendFormat(" and ComplaintsName like '%{0}%' ", model.ComplaintsName);
            }
            if (!string.IsNullOrEmpty(model.ComplaintsType))
            {
                sbWhereSql.AppendFormat(" and ComplaintsType like '%{0}%' ", model.ComplaintsType);
            }

            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_CrmComplaint", "ComplaintsId", "*", sbWhereSql.ToString(), " IssueTime desc"))
            {
                while (reader.Read())
                {
                    Model.CrmStructure.MComplaintsListModel visitListModel = new EyouSoft.Model.CrmStructure.MComplaintsListModel();
                    visitListModel.CompanyId = model.CompanyId;

                    visitListModel.TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty;
                    visitListModel.TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty;
                    visitListModel.RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty;
                    visitListModel.RouteId = !reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (string)reader["RouteId"] : string.Empty;
                    visitListModel.IsHandle = !reader.IsDBNull(reader.GetOrdinal("IsHandle")) ? reader["IsHandle"].ToString() == "0" ? false : true : false;
                    visitListModel.Id = (int)reader["Id"];
                    visitListModel.HandleTime = !reader.IsDBNull(reader.GetOrdinal("HandleTime")) ? (DateTime?)reader["HandleTime"] : null;
                    visitListModel.HandleResult = !reader.IsDBNull(reader.GetOrdinal("HandleOpinion")) ? (string)reader["HandleOpinion"] : string.Empty;
                    visitListModel.HandleName = !reader.IsDBNull(reader.GetOrdinal("HandleName")) ? (string)reader["HandleName"] : string.Empty;
                    visitListModel.ComplaintsType = !reader.IsDBNull(reader.GetOrdinal("ComplaintsType")) ? (string)reader["ComplaintsType"] : string.Empty;
                    visitListModel.ComplaintsTime = !reader.IsDBNull(reader.GetOrdinal("ComplaintsTime")) ? (DateTime?)reader["ComplaintsTime"] : null;
                    visitListModel.ComplaintsName = !reader.IsDBNull(reader.GetOrdinal("ComplaintsName")) ? (string)reader["ComplaintsName"] : string.Empty;
                    visitListModel.ComplaintsId = !reader.IsDBNull(reader.GetOrdinal("ComplaintsId")) ? (string)reader["ComplaintsId"] : string.Empty;
                    list.Add(visitListModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="complaintsId">投诉编号</param>
        /// <param name="handleName">处理人</param>
        /// <param name="handleTime">处理时间</param>
        /// <param name="handleOpinion">处理意见</param>
        /// <param name="isHandle">是否处理</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetComplaintDeal(string complaintsId, string handleName, DateTime handleTime, string handleOpinion,bool isHandle)
        {
            var sql = new StringBuilder();

            sql.Append(" UPDATE  dbo.tbl_CrmComplaint");
            sql.Append(" SET     IsHandle = @IsHandle ,");
            sql.Append("         HandleName = @HandleName ,");
            sql.Append("         HandleTime = @HandleTime ,");
            sql.Append("         HandleOpinion = @HandleOpinion");
            sql.Append(" WHERE   ComplaintsId = @ComplaintsId");

            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@complaintsId", DbType.AnsiStringFixedLength, complaintsId);
            this._db.AddInParameter(dc, "@handleName", DbType.String, handleName);
            this._db.AddInParameter(dc, "@handleTime", DbType.DateTime, handleTime);
            this._db.AddInParameter(dc, "@handleOpinion", DbType.String, handleOpinion);
            this._db.AddInParameter(dc, "@IsHandle", DbType.AnsiStringFixedLength, isHandle?"1":"0");
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 获得投诉管理每日汇总表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmDayTotalModel> GetComplaintDayTotalList(string CompanyId)
        {
            return null;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获得投诉管理Xml语句
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetComplaintXmlString(Model.CrmStructure.MCrmComplaint model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }
        #endregion
    }
}
