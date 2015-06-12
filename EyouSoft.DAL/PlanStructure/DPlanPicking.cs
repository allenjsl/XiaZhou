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
    /// 描述:数据操作领料类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanPicking : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanPicking
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanPicking()
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

        #region 领料

        #region 添加领料信息
        /// <summary>
        /// 添加领料信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddPicking(EyouSoft.Model.PlanStructure.MPlanPicking model)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanPicking_Add");
                db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                db.AddInParameter(cmd, "PlanCost", DbType.Decimal, model.PlanCost);
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, (int)model.Status);
                db.AddInParameter(cmd, "GuideNotes", DbType.String, model.GuideNotes);
                db.AddInParameter(cmd, "Remarks", DbType.String, model.Remarks);
                db.AddInParameter(cmd, "OperatorId", DbType.String, model.OperatorID);
                db.AddInParameter(cmd, "OperatorName", DbType.String, model.OperatorName);
                db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, model.UnitPrice);
                db.AddInParameter(cmd, "Quantity", DbType.Int32, model.Quantity);
                db.AddInParameter(cmd, "UserId", DbType.String, model.UserId);
                db.AddInParameter(cmd, "PickingName", DbType.String, model.PickingName);
                db.AddInParameter(cmd, "PickingContentXML", DbType.String, GetPickingContent(model.Content));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 根据计调ID获取领料实体
        /// <summary>
        /// 根据计调ID获取领料实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanPicking GetPickingModel(string PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a." + strPlan + ",UnitPrice,Quantity,UserId,PickingName, ");
            strSql.Append("(select GoodId,[Content] as Contents from tbl_PlanPickingContent as row where PlanId=a.PlanId for xml path,root('root'))as PickingContent  ");
            strSql.Append("from tbl_Plan as a ,tbl_PlanPicking as b ");
            strSql.Append("where [type]=8 and a.PlanId=b.PlanId and a.PlanId=@PlanId");
            EyouSoft.Model.PlanStructure.MPlanPicking model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanPicking()
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
                        PickingName = reader.IsDBNull(reader.GetOrdinal("PickingName")) ? string.Empty : reader["PickingName"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? string.Empty : reader["UserId"].ToString(),
                        Content = GetPickingContentList(reader.IsDBNull(reader.GetOrdinal("PickingContent")) ? string.Empty : reader["PickingContent"].ToString())
                    };
                }
            }
            return model;
        }
        #endregion

        #region 修改领料信息
        /// <summary>
        /// 修改领料信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdatePicking(EyouSoft.Model.PlanStructure.MPlanPicking model)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanPicking_Update");
                db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                db.AddInParameter(cmd, "PlanCost", DbType.Decimal, model.PlanCost);
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, (int)model.Status);
                db.AddInParameter(cmd, "GuideNotes", DbType.String, model.GuideNotes);
                db.AddInParameter(cmd, "Remarks", DbType.String, model.Remarks);
                db.AddInParameter(cmd, "OperatorId", DbType.String, model.OperatorID);
                db.AddInParameter(cmd, "OperatorName", DbType.String, model.OperatorName);
                db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, model.UnitPrice);
                db.AddInParameter(cmd, "Quantity", DbType.Int32, model.Quantity);
                db.AddInParameter(cmd, "UserId", DbType.String, model.UserId);
                db.AddInParameter(cmd, "PickingName", DbType.String, model.PickingName);
                db.AddInParameter(cmd, "PickingContentXML", DbType.String, GetPickingContent(model.Content));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 根据团队编号获取领料列表
        /// <summary>
        /// 根据团队编号获取领料列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanPicking> GetPickingList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a." + strPlan + ",UnitPrice,Quantity,UserId,PickingName, ");
            strSql.Append("(select GoodId,[Content] as Contents from tbl_PlanPickingContent as row where PlanId=a.PlanId for xml path,root('root'))as PickingContent  ");
            strSql.Append("from tbl_Plan as a ,tbl_PlanPicking as b ");
            strSql.Append("where [type]=8 and a.PlanId=b.PlanId and TourId=@TourId and CompanyId=@CompanyId");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            IList<EyouSoft.Model.PlanStructure.MPlanPicking> list = new List<EyouSoft.Model.PlanStructure.MPlanPicking>();
            EyouSoft.Model.PlanStructure.MPlanPicking model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanPicking()
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
                        PickingName = reader.IsDBNull(reader.GetOrdinal("PickingName")) ? string.Empty : reader["PickingName"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? string.Empty : reader["UserId"].ToString(),
                        Content = GetPickingContentList(reader.IsDBNull(reader.GetOrdinal("PickingContent")) ? string.Empty : reader["PickingContent"].ToString())
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }

            return list;
        }
        #endregion

        #region 删除领料计调项目
        /// <summary>
        /// 删除领料计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeletePicking(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanPicking_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将领料内容转实体成XML
        /// <summary>
        /// 将领料内容转实体成XML
        /// </summary>
        /// <param name="list">领料实体</param>
        /// <returns></returns>
        private string GetPickingContent(IList<EyouSoft.Model.PlanStructure.MPlanPickingContent> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<PickingContent GoodId='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.GoodId.ToString()));
                    strRoomXml.AppendFormat(" Content='{0}'", Utils.ReplaceXmlSpecialCharacter(item.PickingContent.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 获取领料内容实体
        /// <summary>
        /// 获取领料内容实体
        /// </summary>
        /// <param name="PickingContentXml">领料内容实体XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanPickingContent> GetPickingContentList(string PickingContentXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanPickingContent> list = new List<EyouSoft.Model.PlanStructure.MPlanPickingContent>();
            if (!string.IsNullOrEmpty(PickingContentXml))
            {
                XElement xem = XElement.Parse(PickingContentXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanPickingContent model = new EyouSoft.Model.PlanStructure.MPlanPickingContent();
                    model.GoodId = item.Element("GoodId").Value;
                    model.PickingContent = item.Element("Contents").Value;
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #endregion
    }
}
