using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using System.Xml.Linq;


namespace EyouSoft.DAL.PlanStructure
{
    /// <summary>
    /// 描述:数据操作其他类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanBaseInfo : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanBaseInfo
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanBaseInfo()
        {
            this.db = base.SystemStore;
        }
        #endregion

        #region sql语句
        /// <summary>
        /// 计调安排表字段
        /// </summary>
        string strPlan = "PlanId,CompanyId,TourId,SourceId,SourceName,[Type],PlanCost,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,PaymentType,Status,GuideNotes,Remarks,IsOut,SueId,CostId,CostTime,Confirmation,CostRemarks,OperatorId,IssueTime,OperatorName,Prepaid,IsRebate,FinRemark,AddStatus";
        #endregion
        #region 其他操作成员方法

        #region 添加其他信息
        /// <summary>
        /// 添加其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model)
        {
            return OtherAddOrUpdate(model, true);
        }
        #endregion

        #region 根据计调ID获取其他实体
        /// <summary>
        /// 根据计调ID获取其他实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回其他实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanBaseInfo GetOtherModel(string PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select " + strPlan + "");
            strSql.Append(" from tbl_Plan");
            strSql.Append(" where [Type]=13 and PlanId=@PlanId");
            EyouSoft.Model.PlanStructure.MPlanBaseInfo model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanBaseInfo()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        Confirmation = reader.GetDecimal(reader.GetOrdinal("Confirmation")),
                        CostId = reader.IsDBNull(reader.GetOrdinal("CostId")) ? string.Empty : reader["CostId"].ToString(),
                        CostRemarks = reader.IsDBNull(reader.GetOrdinal("CostRemarks")) ? string.Empty : reader["CostRemarks"].ToString(),
                        CostTime = reader.IsDBNull(reader.GetOrdinal("CostTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CostTime")),
                        FinRemark = reader.IsDBNull(reader.GetOrdinal("FinRemark")) ? string.Empty : reader["FinRemark"].ToString(),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        GuideNotes = reader.IsDBNull(reader.GetOrdinal("GuideNotes")) ? string.Empty : reader["GuideNotes"].ToString(),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        IsOut = reader["IsOut"].ToString() == "1" ? true : false,
                        IsRebate = reader["IsRebate"].ToString() == "1" ? true : false,
                        OperatorID = reader.IsDBNull(reader.GetOrdinal("OperatorID")) ? string.Empty : reader["OperatorID"].ToString(),
                        OperatorName = reader.IsDBNull(reader.GetOrdinal("OperatorName")) ? string.Empty : reader["OperatorName"].ToString(),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanId = reader["PlanId"].ToString(),
                        Prepaid = reader.GetDecimal(reader.GetOrdinal("Prepaid")),
                        Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? string.Empty : reader["Remarks"].ToString(),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SourceId = reader.IsDBNull(reader.GetOrdinal("SourceId")) ? string.Empty : reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        AddStatus = (EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus)reader.GetByte(reader.GetOrdinal("AddStatus")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        SueId = reader.IsDBNull(reader.GetOrdinal("SueId")) ? string.Empty : reader["SueId"].ToString(),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        TourId = reader.IsDBNull(reader.GetOrdinal("TourId")) ? string.Empty : reader["TourId"].ToString(),
                        Type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)reader.GetByte(reader.GetOrdinal("Type")),
                        IssueTime = reader.GetDateTime(reader.GetOrdinal("IssueTime")),
                    };
                }
                reader.Close();
            }
            return model;
        }
        #endregion

        #region 修改其他信息
        /// <summary>
        /// 修改其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model)
        {
            return OtherAddOrUpdate(model, false);
        }
        #endregion

        #region 根据团队编号获取其他列表
        /// <summary>
        /// 根据团队编号获取其他列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> GetOtherList(string TourId, string CompanyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PlanId,TourId,SourceId,SourceName,PlanCost,PaymentType,Status");
            strSql.Append(" from tbl_Plan");
            strSql.Append(" where [Type]=13 and  TourId=@TourId and CompanyId=@CompanyId");
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> list = new List<EyouSoft.Model.PlanStructure.MPlanBaseInfo>();
            EyouSoft.Model.PlanStructure.MPlanBaseInfo model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanBaseInfo()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader.IsDBNull(reader.GetOrdinal("TourId")) ? string.Empty : reader["TourId"].ToString(),
                        SourceId = reader.IsDBNull(reader.GetOrdinal("SourceId")) ? string.Empty : reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除其他计调项目
        /// <summary>
        /// 删除其他计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteOther(string PlanID)
        {
            return false;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 添加or更新
        /// </summary>
        /// <param name="model">导游实体</param>
        /// <param name="isAdd">添加true,更新false</param>
        /// <returns></returns>
        private bool OtherAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanBaseInfo model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanOther_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, System.Guid.NewGuid().ToString());
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanOther_Update");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                }
                db.AddInParameter(cmd, "SourceId", DbType.String, model.SourceId);
                db.AddInParameter(cmd, "SourceName", DbType.String, model.SourceName);
                db.AddInParameter(cmd, "PlanCost", DbType.Decimal, model.PlanCost);
                db.AddInParameter(cmd, "PaymentType", DbType.AnsiStringFixedLength, model.PaymentType);
                db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, model.Status);
                db.AddInParameter(cmd, "GuideNotes", DbType.String, model.GuideNotes);
                db.AddInParameter(cmd, "Remarks", DbType.String, model.Remarks);
                db.AddInParameter(cmd, "OperatorId", DbType.String, model.OperatorID);
                db.AddInParameter(cmd, "OperatorName", DbType.String, model.OperatorName);
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #endregion

    }
}
