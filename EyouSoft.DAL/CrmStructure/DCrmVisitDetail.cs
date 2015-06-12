using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.CrmStructure
{
    using EyouSoft.Model.CrmStructure;

    /// <summary>
    /// 团队回访电话回访
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class DCrmVisitDetail : Toolkit.DAL.DALBase
    {
        #region 私有变量
        Database _db = null;

        /// <summary>
        /// 查询团队回访电话回访
        /// </summary>
        private string sql_Crm_SelectVisitDetail = "select * from tbl_CrmVisitDetail where VisitId=@VisitId order by SortId asc";
        #endregion

        #region 构造函数
        public DCrmVisitDetail()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得团队回访电话回访的xml语句
        /// </summary>
        /// <param name="model">团队回访电话回访Model</param>
        /// <returns></returns>
        public string GetVisitDetailXmlString(IList<Model.CrmStructure.MCrmVisitDetail> list)
        {
            IList<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得团队回访电话回访Model
        /// </summary>90
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmVisitDetail> GetVisitDetailModel(string tourId,string visitId)
        {
            IList<Model.CrmStructure.MCrmVisitDetail> list = new List<Model.CrmStructure.MCrmVisitDetail>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  P.PlanId ,");
            sql.Append("         P.Type ,");
            sql.Append("         P.SourceId ,");
            sql.Append("         P.SourceName ,");
            sql.Append("         ISNULL(V.Score,0) Score,");
            sql.Append("         V.TotalDesc,V.VisitId");
            sql.Append(" FROM    dbo.tbl_Plan P");
            sql.Append("         LEFT JOIN dbo.tbl_CrmVisitDetail V ON P.PlanId = V.PlanId AND V.VisitId=@VisitId");
            sql.AppendFormat(" WHERE   P.TourId = @TourId AND P.IsDelete=0 AND P.Status={0}",(int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实);

            Model.CrmStructure.MCrmVisitDetail visitDetailModel = new EyouSoft.Model.CrmStructure.MCrmVisitDetail();
            DbCommand dc = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, tourId);
            _db.AddInParameter(dc, "VisitId", DbType.AnsiStringFixedLength, visitId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    list.Add(new MCrmVisitDetail
                        {
                            PlanType = (Model.EnumType.PlanStructure.PlanProject)reader.GetByte(reader.GetOrdinal("Type")),
                            Score = float.Parse(reader["Score"].ToString()),
                            SourceName = !reader.IsDBNull(reader.GetOrdinal("SourceName")) ? (string)reader["SourceName"] : string.Empty,
                            SourceId = !reader.IsDBNull(reader.GetOrdinal("SourceId")) ? (string)reader["SourceId"] : string.Empty,
                            TotalDesc = !reader.IsDBNull(reader.GetOrdinal("TotalDesc")) ? (string)reader["TotalDesc"] : string.Empty,
                            PlanId = !reader.IsDBNull(reader.GetOrdinal("PlanId")) ? (string)reader["PlanId"] : string.Empty,
                            VisitId = reader["VisitId"].ToString(),
                        });
                }
            }
            return list;
        }
        #endregion
    }
}
