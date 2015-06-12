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
    /// <summary>
    /// 个人会员类型dal
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public class DCrmMemberType : EyouSoft.Toolkit.DAL.DALBase,IDAL.CrmStructure.ICrmMemberType
    {
        #region 私有变量
        private Database _db = null;
        #endregion

        /// <summary>
        /// 添加个人会员类型的存储过程
        /// </summary>
        private string proc_AddCrmMemberType = "proc_AddCrmMemberType";

        /// <summary>
        /// 修改个人会员类型的存储过程
        /// </summary>
        private string proc_Crm_UpdateMemberType = "proc_Crm_UpdateMemberType";

        /// <summary>
        /// 删除个人会员类型的sql语句
        /// </summary>
        private string sql_Crm_DeleteMemberType = "delete from tbl_CrmMemberType where TypeId in (@TypeId)";

        /// <summary>
        /// 查询所有的个人会员类型的sql语句
        /// </summary>
        private string sql_Crm_SelectAllMemberType = "select * from tbl_CrmMemberType where CompanyId=@CompanyId";

        /// <summary>
        /// 查询个人会员类型的sql语句
        /// </summary>
        private string sql_Crm_SelectMemberType = "select * from tbl_CrmMemberType where TypeId=@TypeId";

        /// <summary>
        /// 判断是否存在相同的个人会员类型名称
        /// </summary>
        private string sql_Crm_IsExistsMemberTypeName = "select TypeId from tbl_CrmMemberType where Name=@Name and CompanyId=@CompanyId";

        #region 构造函数
        public DCrmMemberType()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 添加个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns>返回值 -1:已经存在相同的类型名称 1:正确</returns>
        public int AddCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddCrmMemberType);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns>返回值 -1:已经存在相同的类型名称 1:正确</returns>
        public int UpdateCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Crm_UpdateMemberType);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, model.TypeId);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }



        /// <summary>
        /// 删除个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型编号</param>
        /// <returns></returns>
        public int DeleteCrmMemberTypeModel(int TypeId)
        {
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_DeleteMemberType);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, TypeId);
            return DbHelper.ExecuteSql(dc,_db);
        }


        /// <summary>
        /// 批量删除个人会员类型Model
        /// </summary>
        /// <param name="typeIdList">个人会员类型编号List</param>
        /// <returns></returns>
        public int DeleteCrmMemberTypeModel(params int[] typeIdList)
        {
            StringBuilder sbTypeId = new StringBuilder();
            for (int i = 0; i < typeIdList.Length; i++)
            {
                sbTypeId.AppendFormat("{0},", typeIdList[i]);
            }
            sbTypeId.Remove(sbTypeId.Length - 1, 1);
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_DeleteMemberType);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, sbTypeId.ToString());
            return DbHelper.ExecuteSql(dc,_db);
        }


        /// <summary>
        /// 获得所有个人会员类型Model
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmMemberType> GetCrmMerberTypeModelList(string CompanyId)
        {
            IList<Model.CrmStructure.MCrmMemberType> list = new List<Model.CrmStructure.MCrmMemberType>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectAllMemberType);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc,_db))
            {
                while (reader.Read())
                {
                    Model.CrmStructure.MCrmMemberType memberTpeModel = new EyouSoft.Model.CrmStructure.MCrmMemberType();
                    memberTpeModel.TypeId = (int)reader["TypeId"];
                    memberTpeModel.Name = (string)reader["Name"];
                    memberTpeModel.CompanyId = CompanyId;
                    list.Add(memberTpeModel);
                }
            }
            return list;
        }


        /// <summary>
        /// 获得个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型Model</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmMemberType GetCrmMerberTypeModel(int TypeId)
        {
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectMemberType);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, TypeId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc,_db))
            {
                Model.CrmStructure.MCrmMemberType memberTpeModel = new EyouSoft.Model.CrmStructure.MCrmMemberType();
                memberTpeModel.TypeId = TypeId;
                memberTpeModel.Name = (string)reader["Name"];
                memberTpeModel.CompanyId = (string)reader["CompanyId"];
                return memberTpeModel;
            }
        }


        /// <summary>
        /// 判断是否存在相同的个人会员类型名称
        /// </summary>
        /// <param name="TypeName">个人会员类型名称</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool IsExistsSameMemberType(string TypeName,string companyId)
        {
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_IsExistsMemberTypeName);
            _db.AddInParameter(dc, "Name", DbType.String, TypeName);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            if (DbHelper.ExecuteReader(dc,_db).Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
