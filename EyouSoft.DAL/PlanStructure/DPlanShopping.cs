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
    /// 描述:数据操作购物类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanShopping : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanShopping
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanShopping()
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

        #region 购物点

        #region 添加购物信息
        /// <summary>
        /// 添加购物信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddShopping(EyouSoft.Model.PlanStructure.MPlanShopping model)
        {
            return ShoppingAddOrUpdate(model, true);
        }
        #endregion

        #region 根据计调ID获取购物实体
        /// <summary>
        /// 根据计调ID获取购物实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanShopping GetShoppingModel(string PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a." + strPlan + ",Headcount,Rebate,ShopTime");
            strSql.Append(" from tbl_Plan as a,tbl_PlanShopping as b");
            strSql.Append(" where a.PlanId=b.PlanId and [Type]=7 and a.PlanId=@PlanId");
            EyouSoft.Model.PlanStructure.MPlanShopping model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanShopping()
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
                        Headcount = (int)reader["Headcount"],
                        Rebate = reader.IsDBNull(reader.GetOrdinal("Rebate")) ? string.Empty : reader["Rebate"].ToString(),
                        ShopTime = reader.IsDBNull(reader.GetOrdinal("ShopTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ShopTime")),
                    };
                }
                reader.Close();
            }
            return model;

        }
        #endregion

        #region 修改购物信息
        /// <summary>
        /// 修改购物信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateShopping(EyouSoft.Model.PlanStructure.MPlanShopping model)
        {
            return ShoppingAddOrUpdate(model, false);
        }
        #endregion

        #region 根据团队编号获取购物列表
        /// <summary>
        /// 根据团队编号获取购物列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanShopping> GetShoppingList(string TourId, string CompanyId,bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.PlanId,TourId,SourceId,SourceName,PlanCost,PaymentType,Status ,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,IsRebate,Rebate,Headcount,ShopTime");
            strSql.Append(" from tbl_Plan as a,tbl_PlanShopping as b ");
            strSql.Append(" where a.PlanId=b.PlanId and [Type]=7 and TourId=@TourId and CompanyId=@CompanyId ");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanShopping> list = new List<EyouSoft.Model.PlanStructure.MPlanShopping>();
            EyouSoft.Model.PlanStructure.MPlanShopping model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanShopping()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader.IsDBNull(reader.GetOrdinal("TourId")) ? string.Empty : reader["TourId"].ToString(),
                        SourceId = reader.IsDBNull(reader.GetOrdinal("SourceId")) ? string.Empty : reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        IsRebate = reader["IsRebate"].ToString() == "1" ? true : false,
                        Rebate = reader.IsDBNull(reader.GetOrdinal("Rebate")) ? string.Empty : reader["Rebate"].ToString(),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        Headcount = (int)reader["Headcount"],
                        ShopTime = reader.GetDateTime(reader.GetOrdinal("ShopTime")),
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除购物计调项目
        /// <summary>
        /// 删除购物计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteShopping(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanShopping_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #endregion

        #region 私有方法

        #region 添加OR更新
        /// <summary>
        /// 添加OR更新
        /// </summary>
        /// <param name="model">购物实体</param>
        /// <param name="isAdd">添加true,更新false</param>
        /// <returns></returns>
        private bool ShoppingAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanShopping model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShopping_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShopping_Update");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                }
                db.AddInParameter(cmd, "SourceId", DbType.String, model.SourceId);
                db.AddInParameter(cmd, "SourceName", DbType.String, model.SourceName);
                db.AddInParameter(cmd, "PlanCost", DbType.Decimal, model.PlanCost);
                db.AddInParameter(cmd, "PaymentType", DbType.AnsiStringFixedLength, (int)model.PaymentType);
                db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, (int)model.Status);
                db.AddInParameter(cmd, "GuideNotes", DbType.String, model.GuideNotes);
                db.AddInParameter(cmd, "Remarks", DbType.String, model.Remarks);
                db.AddInParameter(cmd, "OperatorId", DbType.String, model.OperatorID);
                db.AddInParameter(cmd, "OperatorName", DbType.String, model.OperatorName);
                db.AddInParameter(cmd, "IsRebate", DbType.String, model.IsRebate == true ? 1 : 0);
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                db.AddInParameter(cmd, "Headcount", DbType.Int32, model.Headcount);
                db.AddInParameter(cmd, "Rebate", DbType.String, model.Rebate);
                db.AddInParameter(cmd, "ShopTime", DbType.DateTime, model.ShopTime);
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
