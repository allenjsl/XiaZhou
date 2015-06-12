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
    /// 描述:数据操作用餐类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanDining : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanDining
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanDining()
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

        #region 用餐

        #region 添加用餐信息
        /// <summary>
        /// 添加用餐信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddDining(EyouSoft.Model.PlanStructure.MPlanDining model)
        {
            return DiningAddOrUpdate(model, true);
        }
        #endregion

        #region 修改用餐信息
        /// <summary>
        /// 修改用餐信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateDining(EyouSoft.Model.PlanStructure.MPlanDining model)
        {
            return DiningAddOrUpdate(model, false);
        }
        #endregion

        #region 根据计调ID获取用餐实体
        /// <summary>
        /// 根据计调ID获取用餐实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanDining GetDiningModel(string PlanID)
        {
            string strSql = "select a." + strPlan + " , DiningType,DiningTime,IsFullMeal,Breakfast,Lunch,Supper,Frequency,(select AdultPrice,ChildPrice,AdultNumber,ChildNumber from tbl_PlanDiningPrice as row where PlanId=a.PlanId for xml path,root('root'))as DiningPriceXml from tbl_Plan as a,tbl_PlanDining as b where [type]=6 and a.PlanId=b.PlanId and a.PlanId=@PlanId";
            EyouSoft.Model.PlanStructure.MPlanDining model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanDining()
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
                        Breakfast = reader["Breakfast"].ToString() == "1" ? true : false,
                        Lunch = reader["Lunch"].ToString() == "1" ? true : false,
                        IsFullMeal = reader["IsFullMeal"].ToString() == "1" ? true : false,
                        Supper = reader["Supper"].ToString() == "1" ? true : false,
                        Frequency = (int)reader["Frequency"],
                        DiningType = (EyouSoft.Model.EnumType.PlanStructure.PlanDiningType)reader.GetByte(reader.GetOrdinal("DiningType")),
                        DiningTime = reader.IsDBNull(reader.GetOrdinal("DiningTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DiningTime")),
                        PlanDiningPricelist = GetDiningPriceList(reader.IsDBNull(reader.GetOrdinal("DiningPriceXml")) ? string.Empty : reader["DiningPriceXml"].ToString())
                    };
                }
                reader.Close();
            }

            return model;
        }
        #endregion

        #region 根据团队编号获取用餐列表
        /// <summary>
        /// 根据团队编号获取用餐列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanDining> GetDiningList(string TourId, string CompanyId,bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.PlanId,TourId,SourceId,SourceName,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,DiningTime,IsFullMeal,Breakfast,Lunch,Supper,Frequency ,PlanCost,PaymentType,Status, ");
            strSql.Append(" (select AdultPrice,ChildPrice,AdultNumber,ChildNumber from tbl_PlanDiningPrice as row where PlanId=a.PlanId for xml path,root('root'))as DiningPriceXml ");
            strSql.Append(" from  tbl_Plan as a,tbl_PlanDining as b ");
            strSql.Append(" where [type]=6 and a.PlanId=b.PlanId and CompanyId=@CompanyId and TourId=@TourId ");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanDining> list = new List<EyouSoft.Model.PlanStructure.MPlanDining>();
            EyouSoft.Model.PlanStructure.MPlanDining model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanDining()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        DiningTime = reader.GetDateTime(reader.GetOrdinal("DiningTime")),
                        IsFullMeal = reader["IsFullMeal"].ToString() == "1" ? true : false,
                        Breakfast = reader["Breakfast"].ToString() == "1" ? true : false,
                        Lunch = reader["Lunch"].ToString() == "1" ? true : false,
                        Supper = reader["Supper"].ToString() == "1" ? true : false,
                        Frequency = (int)reader["Frequency"],
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        PlanDiningPricelist = GetDiningPriceList(reader.IsDBNull(reader.GetOrdinal("DiningPriceXml")) ? string.Empty : reader["DiningPriceXml"].ToString())
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除用餐计调项目
        /// <summary>
        /// 删除用餐计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteDining(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanDining_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 用餐私有方法

        #region 用餐添加or更新
        /// <summary>
        /// 用餐添加or更新
        /// </summary>
        /// <param name="model">用餐实体</param>
        /// <param name="isAdd">是否添加</param>
        /// <returns></returns>
        private bool DiningAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanDining model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanDining_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanDining_Update");
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
                db.AddInParameter(cmd, "DiningType", DbType.AnsiStringFixedLength, (int)model.DiningType);
                db.AddInParameter(cmd, "DiningTime", DbType.DateTime, model.DiningTime);
                db.AddInParameter(cmd, "IsFullMeal", DbType.String, model.IsFullMeal == true ? 1 : 0);
                db.AddInParameter(cmd, "Breakfast", DbType.String, model.Breakfast == true ? 1 : 0);
                db.AddInParameter(cmd, "Lunch", DbType.String, model.Lunch == true ? 1 : 0);
                db.AddInParameter(cmd, "Supper", DbType.String, model.Supper == true ? 1 : 0);
                db.AddInParameter(cmd, "Frequency", DbType.Int32, model.Frequency);
                db.AddInParameter(cmd, "DiningPriceXml", DbType.String, GetDiningPriceXml(model.PlanDiningPricelist));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将用餐价格实体转成XML
        /// <summary>
        /// 将用餐价格实体转成Xml
        /// </summary>
        /// <param name="list">价格实体</param>
        /// <returns></returns>
        private string GetDiningPriceXml(IList<EyouSoft.Model.PlanStructure.MPlanDiningPrice> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<DiningPrice AdultNumber='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.AdultNumber.ToString()));
                    strRoomXml.AppendFormat(" AdultPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.AdultPrice.ToString()));
                    strRoomXml.AppendFormat(" ChildNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildNumber.ToString()));
                    strRoomXml.AppendFormat(" ChildPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildPrice.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 获取用餐价格实体
        /// <summary>
        /// 获取用餐价格实体
        /// </summary>
        /// <param name="DiningPriceXml">用餐价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanDiningPrice> GetDiningPriceList(string DiningPriceXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanDiningPrice> list = new List<EyouSoft.Model.PlanStructure.MPlanDiningPrice>();
            if (!string.IsNullOrEmpty(DiningPriceXml))
            {
                EyouSoft.Model.PlanStructure.MPlanDiningPrice model = null;
                XElement xem = XElement.Parse(DiningPriceXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanDiningPrice()
                    {
                        AdultNumber = Utils.GetInt(item.Element("AdultNumber").Value),
                        AdultPrice = Utils.GetDecimal(item.Element("AdultPrice").Value),
                        ChildNumber = Utils.GetInt(item.Element("ChildNumber").Value),
                        ChildPrice = Utils.GetDecimal(item.Element("ChildPrice").Value)
                    };
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #endregion

        #endregion
    }
}
