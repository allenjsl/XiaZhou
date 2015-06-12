using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 部门信息
    /// 创建者：郑付杰
    /// 创建时间：2011/9/19
    /// </summary>
    public class DComDepartment : DALBase, EyouSoft.IDAL.ComStructure.IComDepartment
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComDepartment()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComDepartment 成员
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Add(MComDepartment item)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO tbl_ComDepartment(CompanyId,DepartName,DepartHead,PrevDepartId,Contact,");
            sql.Append("Fax,PrintHeader,PrintFooter,PrintTemplates,Seal,Remarks,OperatorId,CertificateCode) VALUES(@CompanyId,@DepartName,");
            sql.Append("@DepartHead,@PrevDepartId,@Contact,@Fax,@PrintHeader,@PrintFooter,@PrintTemplates,@Seal,@Remarks,@OperatorId,@CertificateCode)");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@DepartName", DbType.String, item.DepartName);
            this._db.AddInParameter(comm, "@DepartHead", DbType.String, item.DepartHead);
            this._db.AddInParameter(comm, "@PrevDepartId", DbType.Int32, item.PrevDepartId);
            this._db.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._db.AddInParameter(comm, "@Fax", DbType.String, item.Fax);
            this._db.AddInParameter(comm, "@PrintHeader", DbType.String, item.PrintHeader);
            this._db.AddInParameter(comm, "@PrintFooter", DbType.String, item.PrintFooter);
            this._db.AddInParameter(comm, "@PrintTemplates", DbType.String, item.PrintTemplates);
            this._db.AddInParameter(comm, "@Seal", DbType.String, item.Seal);
            this._db.AddInParameter(comm, "@Remarks", DbType.String, item.Remarks);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@CertificateCode", DbType.AnsiString, item.CertificateCode);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(MComDepartment item)
        {
            StringBuilder sql = new StringBuilder("UPDATE tbl_ComDepartment SET DepartName = @DepartName,DepartHead=@DepartHead,Contact=@Contact,");
            sql.Append("Fax=@Fax,PrintHeader=@PrintHeader,PrintFooter=@PrintFooter,PrintTemplates=@PrintTemplates,Seal=@Seal,Remarks=@Remarks,OperatorId=@OperatorId ");
            sql.Append("WHERE DepartId = @DepartId AND CompanyId = @CompanyId");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@DepartName", DbType.String, item.DepartName);
            this._db.AddInParameter(comm, "@DepartHead", DbType.String, item.DepartHead);
            this._db.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._db.AddInParameter(comm, "@Fax", DbType.String, item.Fax);
            this._db.AddInParameter(comm, "@PrintHeader", DbType.String, item.PrintHeader);
            this._db.AddInParameter(comm, "@PrintFooter", DbType.String, item.PrintFooter);
            this._db.AddInParameter(comm, "@PrintTemplates", DbType.String, item.PrintTemplates);
            this._db.AddInParameter(comm, "@Seal", DbType.String, item.Seal);
            this._db.AddInParameter(comm, "@Remarks", DbType.String, item.Remarks);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@DepartId", DbType.Int32, item.DepartId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;

        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1:部门已添加用户 2:该部门已添加下级部门 3:删除成功 4:删除失败		</returns>
        public int Delete(int departId, string companyId)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComDepartment_Delete");
            this._db.AddInParameter(comm, "@departId", DbType.Int32, departId);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.RunProcedureWithResult(comm, this._db);
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门实体</returns>
        public MComDepartment GetModel(int departId,string companyId)
        {
            string sql = "SELECT DepartId,CompanyId,DepartName,DepartHead,PrevDepartId,Contact,Fax,PrintHeader,PrintFooter,PrintTemplates,Seal,Remarks,OperatorId,CertificateCode FROM tbl_ComDepartment WHERE DepartId = @DepartId AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@DepartId", DbType.Int32, departId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                if (reader.Read())
                {
                    return new MComDepartment()
                    {
                        DepartId = (int)reader["DepartId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        DepartName = reader.IsDBNull(reader.GetOrdinal("DepartName")) ? string.Empty : reader["DepartName"].ToString(),
                        DepartHead = reader.IsDBNull(reader.GetOrdinal("DepartHead")) ? string.Empty : reader["DepartHead"].ToString(),
                        PrevDepartId = (int)reader["PrevDepartId"],
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? string.Empty : reader["Contact"].ToString(),
                        Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? string.Empty : reader["Fax"].ToString(),
                        PrintHeader = reader.IsDBNull(reader.GetOrdinal("PrintHeader")) ? string.Empty : reader["PrintHeader"].ToString(),
                        PrintFooter = reader.IsDBNull(reader.GetOrdinal("PrintFooter")) ? string.Empty : reader["PrintFooter"].ToString(),
                        PrintTemplates = reader.IsDBNull(reader.GetOrdinal("PrintTemplates")) ? string.Empty : reader["PrintTemplates"].ToString(),
                        Seal = reader.IsDBNull(reader.GetOrdinal("Seal")) ? string.Empty : reader["Seal"].ToString(),
                        OperatorId = reader["OperatorId"].ToString(),
                        Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? string.Empty : reader["Remarks"].ToString(),
                        CertificateCode = reader.IsDBNull(reader.GetOrdinal("CertificateCode")) ? string.Empty : reader["CertificateCode"].ToString()
                    };
                }
            }

            return null;
        }
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        ///  <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MComDepartment> GetList(string companyId)
        {
            string sql = "SELECT DepartId,DepartName,PrevDepartId FROM tbl_ComDepartment WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComDepartment> list = new List<MComDepartment>();
            MComDepartment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComDepartment
                        {
                            DepartId = (int)reader["DepartId"],
                            DepartName =
                                reader.IsDBNull(reader.GetOrdinal("DepartName"))
                                    ? string.Empty
                                    : reader["DepartName"].ToString(),
                            PrevDepartId = (int)reader["PrevDepartId"]
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 获取部门下的所有子部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门集合</returns>
        public IList<MComDepartment> GetList(string departId, string companyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("with depart( DepartId,DepartName,PrevDepartId)");
            sql.Append(" as");
            sql.Append(" (");
            sql.Append(" select DepartId,DepartName,PrevDepartId");
            sql.AppendFormat(" from tbl_ComDepartment where DepartId = {0} and CompanyId = '{1}'", departId, companyId);
            sql.Append(" union all");
            sql.Append(" select a.DepartId,a.DepartName,a.PrevDepartId ");
            sql.Append(" from tbl_ComDepartment a ");
            sql.Append(" inner join depart b on a.PrevDepartId = b.DepartId");
            sql.Append(" )");
            sql.AppendFormat(" select * from depart where DepartId <> {0}", departId);

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            IList<MComDepartment> list = new List<MComDepartment>();

            MComDepartment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComDepartment
                    {
                        DepartId = (int)reader["DepartId"],
                        DepartName =
                            reader.IsDBNull(reader.GetOrdinal("DepartName"))
                                ? string.Empty
                                : reader["DepartName"].ToString(),
                        PrevDepartId = (int)reader["PrevDepartId"]
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 根据上级部门编号判断是否存在子部门
        /// </summary>
        /// <param name="prevDepartId">上级部门编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExistSubDept(int prevDepartId)
        {
            var sql = new StringBuilder("select DepartId from tbl_ComDepartment where PrevDepartId=@PrevDepartId");
            var comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@PrevDepartId", DbType.Int32, prevDepartId);

            return DbHelper.Exists(comm, this._db);
        }

        #endregion
    }
}
