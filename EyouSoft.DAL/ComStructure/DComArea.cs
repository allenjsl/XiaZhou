using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 线路区域
    /// 创建者：郑付杰
    /// 创建时间:2011/9/20
    /// </summary>
    public class DComArea:DALBase,EyouSoft.IDAL.ComStructure.IComArea
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComArea()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComArea 成员
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>返回线路区域编号</returns>
        public int Add(MComArea item)
        {
            StringBuilder xml = new StringBuilder("<item>");
            if (item.Plan != null)
            {
                foreach (MComAreaPlan obj in item.Plan)
                {
                    xml.AppendFormat("<operator>{0}</operator>", obj.OperatorId);
                }
            }
            xml.Append("</item>");
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComArea_Add");
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@AreaName", DbType.AnsiString, item.AreaName);
            this._db.AddInParameter(comm, "@Type", DbType.Byte, (int)item.Type);
            this._db.AddInParameter(comm, "@Operatorid", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, xml.ToString());
            this._db.AddOutParameter(comm, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(comm, _db);
            return Convert.ToInt32(this._db.GetParameterValue(comm, "Result"));
        }
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MComArea item)
        {
            StringBuilder xml = new StringBuilder("<item>");
            if (item.Plan != null)
            {
                foreach (MComAreaPlan obj in item.Plan)
                {
                    xml.AppendFormat("<operator id='{0}'>{1}</operator>", item.AreaId, obj.OperatorId);
                }
            }
            xml.Append("</item>");
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComArea_Update");
            this._db.AddInParameter(comm, "@AreaId", DbType.Int32, item.AreaId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@AreaName", DbType.AnsiString, item.AreaName);
            this._db.AddInParameter(comm, "@Type", DbType.Byte, (int)item.Type);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, xml.ToString());

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 批量删除线路区域
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaids">线路区域编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string companyId,params int[] areaids)
        {
            //string sql = "UPDATE tbl_ComArea SET IsDelete = 1 WHERE CHARINDEX(CAST(AreaId as varchar(15)),@ids,0) > 0  AND CompanyId = @CompanyId";
            string sql = string.Format("UPDATE tbl_ComArea SET IsDelete = '1' WHERE AreaId IN({0}) AND CompanyId = @CompanyId", Utils.GetSqlIdStrByArray(areaids));
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            //this._db.AddInParameter(comm, "@ids", DbType.String, areaIds);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域实体</returns>
        public MComArea GetModel(int areaId, string companyId)
        {
            string sql = "select AreaId,CompanyId,AreaName,[Type],OperatorId,(select OperatorId as PlanerId,(select ContactName from tbl_ComUser where UserId=tbl_ComAreaPlaner.OperatorId) as Planer from tbl_ComAreaPlaner where AreaId=tbl_ComArea.AreaId  for xml raw,root) PlanList from tbl_ComArea where AreaId=@areaId and CompanyId=@CompanyId";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@areaId", DbType.Int32, areaId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    MComArea item = new MComArea()
                    {
                        AreaId = (int)reader["AreaId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        AreaName = reader["AreaName"].ToString(),
                        Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Plan=GetPlanByXml(reader["PlanList"].ToString())
                    };
                    return item;
                }
            }

            return null;
        }
        /// <summary>
        /// 分页获取线路区域信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">搜索实体</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetList(int pageCurrent, int pageSize, ref int pageCount,string companyId,MComAreaSearch model)
        {
            string tableName = "tbl_ComArea";
            string primaryKey = "AreaId";
            string fields = " AreaId,CompanyId,AreaName,[Type],OperatorId,(select OperatorId as PlanerId,(select ContactName from tbl_ComUser where UserId=tbl_ComAreaPlaner.OperatorId) as Planer from tbl_ComAreaPlaner where AreaId=tbl_ComArea.AreaId  for xml raw,root) PlanList ";

            string query = string.Format(" CompanyId = '{0}' and IsDelete='0'", companyId);
            string orderBy = " AreaId DESC";
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.AreaName))
                {
                    query += string.Format(" and AreaName like '%{0}%'", Utils.ToSqlLike(model.AreaName));
                }
                if (model.Type.HasValue)
                {
                    query += string.Format(" and [Type]={0}", (int)model.Type);
                }
            }
            IList<MComArea> list = new List<MComArea>();
            MComArea item = null;
           
            string xmlString = string.Empty;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fields, query, orderBy))
            {
                while (reader.Read())
                {
                    item = new MComArea()
                    {
                        AreaId = (int)reader["AreaId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        AreaName = reader["AreaName"].ToString(),
                        Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Plan = GetPlanByXml(reader["PlanList"].ToString())
                    };
                    //计调员
                  
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据公司编号获取线路区域信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetAreaByCID(string companyId)
        {
            string sql = "SELECT AreaId,AreaName,[Type],OperatorId FROM tbl_ComArea WHERE  IsDelete = '0' AND CompanyId = @companyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComArea> list = new List<MComArea>();
            MComArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComArea()
                    {
                        AreaId = (int)reader["AreaId"],
                        CompanyId = companyId,
                        AreaName = reader["AreaName"].ToString(),
                        Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        OperatorId = reader["OperatorId"].ToString()
                    });
                }
            }

            return list;
        }


        private IList<MComAreaPlan> GetPlanByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MComAreaPlan> list = new List<MComAreaPlan>();
            MComAreaPlan item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MComAreaPlan()
                {
                    Planer = Utils.GetXAttributeValue(xRow, "Planer"),
                    OperatorId = Utils.GetXAttributeValue(xRow, "PlanerId")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
