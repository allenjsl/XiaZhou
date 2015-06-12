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
    /// 投诉意见Dal
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class DCrmComplaintDetail:EyouSoft.Toolkit.DAL.DALBase
    {
        #region 私有变量
        private Database _db = null;

        /// <summary>
        /// 查询投诉意见列表的sql语句
        /// </summary>
        private string sql_Crm_SelectComplaintDetail = "select * from tbl_CrmComplaintDetail where ComplaintsId=@ComplaintId";
        #endregion

        #region 构造函数
        public DCrmComplaintDetail()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得投诉意见列表
        /// </summary>
        /// <param name="complaintId">投诉编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmComplaintDetail> GetCrmComplaintDetailModelList(string complaintId)
        {
            IList<Model.CrmStructure.MCrmComplaintDetail> list = new List<Model.CrmStructure.MCrmComplaintDetail>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectComplaintDetail);
            _db.AddInParameter(dc, "ComplaintId", DbType.AnsiStringFixedLength, complaintId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc,_db))
            {
                while (reader.Read())
                {
                    Model.CrmStructure.MCrmComplaintDetail complaintDetailModel = new EyouSoft.Model.CrmStructure.MCrmComplaintDetail();
                    complaintDetailModel.ComplaintId = complaintId;
                    //complaintDetailModel.ComplaintsType = (EyouSoft.Model.EnumType.CrmStructure.CrmComplaintsType)(int.Parse(reader["ComplaintsType"].ToString()));
                    complaintDetailModel.Opinion = !reader.IsDBNull(reader.GetOrdinal("Opinion")) ? (string)reader["Opinion"] : string.Empty;
                    list.Add(complaintDetailModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得投诉意见xml语句
        /// </summary>
        /// <param name="list">投诉意见列表</param>
        /// <returns></returns>
        public string GetComplaintDetailXmlString(IList<Model.CrmStructure.MCrmComplaintDetail> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }
        #endregion

    }
}
