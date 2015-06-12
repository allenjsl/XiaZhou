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
    /// 描述:数据操作地接类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanAgency : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanAgency
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanAgency()
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
        #region 地接操作成员方法

        #region 添加地接信息
        /// <summary>
        /// 添加地接信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddAgency(EyouSoft.Model.PlanStructure.MPlanAgency model)
        {
            return AgencyAddUpdate(model, true);
        }
        #endregion

        #region 修改地接信息
        /// <summary>
        /// 修改地接信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateAgency(EyouSoft.Model.PlanStructure.MPlanAgency model)
        {
            return AgencyAddUpdate(model, false);
        }
        #endregion

        #region 根据计调ID获取地接实体
        /// <summary>
        /// 根据计调ID获取地接实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanAgency GetAgencyModel(string PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a." + strPlan + ",LDate,RDate,");
            strSql.Append(" (select Reception from tbl_PlanAgencyDetailed as row where PlanId=a.PlanId for xml path,root('root'))as DetailedXML,");
            strSql.Append(" (select ProjectType,UnitPrice,PeopleNumber,Remarks from tbl_PlanAgencyPrice as row where PlanId=a.PlanId for xml path,root('root'))as PriceXML,");
            strSql.Append(" (select top(1)TravelInfo from tbl_PlanTravel where PlanId=@PlanId )as TravelInfo");
            strSql.Append(" from tbl_Plan as a,tbl_PlanAgency as b ");
            strSql.Append(" where a.PlanId=b.PlanId  and a.PlanId=@PlanID");
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            EyouSoft.Model.PlanStructure.MPlanAgency model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanAgency()
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
                        LDate = reader.IsDBNull(reader.GetOrdinal("LDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LDate")),
                        RDate = reader.IsDBNull(reader.GetOrdinal("RDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("RDate")),
                        PlanAgencyDetailedList = GetDetailedList(reader.IsDBNull(reader.GetOrdinal("DetailedXML")) ? string.Empty : reader["DetailedXML"].ToString()),
                        PlanAgencyPriceList = GetPriceList(reader.IsDBNull(reader.GetOrdinal("PriceXML")) ? string.Empty : reader["PriceXML"].ToString()),
                        TravelInfo = reader.IsDBNull(reader.GetOrdinal("TravelInfo")) ? string.Empty : reader["TravelInfo"].ToString()
                    };
                }
                reader.Close();
            }
            return model;
        }
        #endregion

        #region 根据团队编号获取地接列表
        /// <summary>
        /// 根据团队编号获取地接列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanAgency> GetAgencyList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.PlanId,TourId,SourceId,SourceName,PlanCost,PaymentType,Status,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,IsRebate,LDate,RDate,");
            strSql.Append(" (select ProjectType,UnitPrice,PeopleNumber,Remarks from tbl_PlanAgencyPrice as row where PlanId=a.PlanId for xml path,root('root'))as PriceXML");
            strSql.Append(" from tbl_Plan as a,tbl_PlanAgency as b");
            strSql.Append(" where a.PlanId=b.PlanId and TourId=@TourId and CompanyId=@CompanyId");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanAgency> list = new List<EyouSoft.Model.PlanStructure.MPlanAgency>();
            EyouSoft.Model.PlanStructure.MPlanAgency model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanAgency()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        IsRebate = reader["IsRebate"].ToString() == "1" ? true : false,
                        LDate = reader.IsDBNull(reader.GetOrdinal("LDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LDate")),
                        RDate = reader.IsDBNull(reader.GetOrdinal("RDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("RDate")),
                        PlanAgencyPriceList = GetPriceList(reader.IsDBNull(reader.GetOrdinal("PriceXML")) ? string.Empty : reader["PriceXML"].ToString()),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts"))
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除地接计调项目
        /// <summary>
        /// 删除地接计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteAgency(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanAgency_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 私有方法

        #region 添加or更新
        /// <summary>
        /// 添加or更新
        /// </summary>
        /// <param name="model">地接实体</param>
        /// <param name="isAdd">添加true或者更新false</param>
        /// <returns></returns>
        private bool AgencyAddUpdate(EyouSoft.Model.PlanStructure.MPlanAgency model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanAgency_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanAgency_Update");
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
                db.AddInParameter(cmd, "LDate", DbType.Decimal, model.LDate);
                db.AddInParameter(cmd, "RDate", DbType.Decimal, model.RDate);
                db.AddInParameter(cmd, "AgencyDetailedXML", DbType.String, GetDetailed(model.PlanAgencyDetailedList));
                db.AddInParameter(cmd, "AgencyPriceXML", DbType.String, GetPrice(model.PlanAgencyPriceList));
                db.AddInParameter(cmd, "TravelInfo", DbType.String, model.TravelInfo);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将行程转成XML数据
        /// <summary>
        /// 将行程转成XML数据
        /// </summary>
        /// <param name="list">行程集合</param>
        /// <returns></returns>
        private string GetTravel(IList<EyouSoft.Model.PlanStructure.MPlanTravel> list)
        {
            string strXml = string.Empty;
            if (list != null && list.Count > 0)
            {
                StringBuilder strTravelXml = new StringBuilder();
                strTravelXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strTravelXml.AppendFormat("<Travel TravelId='{0}'", Utils.ReplaceXmlSpecialCharacter(item.TravelId.ToString()));
                    strTravelXml.AppendFormat(" TravelInfo='{0}'", Utils.ReplaceXmlSpecialCharacter(item.TravelInfo.ToString()));
                    strTravelXml.AppendFormat("/>");
                }
                strTravelXml.AppendFormat("</ROOT>");
                strXml = strTravelXml.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将价格转成XML数据
        /// <summary>
        /// 将价格转成XML数据
        /// </summary>
        /// <param name="list">行程集合</param>
        /// <returns></returns>
        private string GetPrice(IList<EyouSoft.Model.PlanStructure.MPlanAgencyPrice> list)
        {
            string strXml = string.Empty;
            if (list != null && list.Count > 0)
            {
                StringBuilder strTravelXml = new StringBuilder();
                strTravelXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strTravelXml.AppendFormat("<AgencyPrice PeopleNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.PeopleNumber.ToString()));
                    strTravelXml.AppendFormat(" ProjectType='{0}'", (int)item.ProjectType);
                    strTravelXml.AppendFormat(" UnitPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.UnitPrice.ToString()));
                    strTravelXml.AppendFormat(" Remarks='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Remarks.ToString()));
                    strTravelXml.AppendFormat("/>");
                }
                strTravelXml.AppendFormat("</ROOT>");
                strXml = strTravelXml.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将行接待明细转成XML数据
        /// <summary>
        /// 将行接待明细转成XML数据
        /// </summary>
        /// <param name="list">行程集合</param>
        /// <returns></returns>
        private string GetDetailed(IList<EyouSoft.Model.PlanStructure.MPlanAgencyDetailed> list)
        {
            string strXml = string.Empty;
            if (list != null && list.Count > 0)
            {
                StringBuilder strTravelXml = new StringBuilder();
                strTravelXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strTravelXml.AppendFormat("<AgencyDetailed Reception='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Reception.ToString()));
                    strTravelXml.AppendFormat("/>");
                }
                strTravelXml.AppendFormat("</ROOT>");
                strXml = strTravelXml.ToString();
            }
            return strXml;

        }
        #endregion

        #region 获取接待明细实体
        /// <summary>
        /// 获取接待明细实体
        /// </summary>
        /// <param name="DetailedXml">接待明细XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanAgencyDetailed> GetDetailedList(string DetailedXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanAgencyDetailed> list = new List<EyouSoft.Model.PlanStructure.MPlanAgencyDetailed>();
            if (!string.IsNullOrEmpty(DetailedXml))
            {
                XElement xem = XElement.Parse(DetailedXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanAgencyDetailed model = new EyouSoft.Model.PlanStructure.MPlanAgencyDetailed();
                    model.Reception = (EyouSoft.Model.EnumType.PlanStructure.PlanAgencyDetailedReception)Utils.GetInt(item.Element("Reception").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取价格实体
        /// <summary>
        /// 获取价格实体
        /// </summary>
        /// <param name="PriceXml">接待价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanAgencyPrice> GetPriceList(string PriceXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanAgencyPrice> list = new List<EyouSoft.Model.PlanStructure.MPlanAgencyPrice>();
            if (!string.IsNullOrEmpty(PriceXml))
            {
                XElement xem = XElement.Parse(PriceXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanAgencyPrice model = new EyouSoft.Model.PlanStructure.MPlanAgencyPrice();
                    model.PeopleNumber = Utils.GetInt(item.Element("PeopleNumber").Value);
                    model.ProjectType = (EyouSoft.Model.EnumType.PlanStructure.PlanAgencyPriceType)Utils.GetInt(item.Element("ProjectType").Value);
                    model.Remarks = item.Element("Remarks").Value;
                    model.UnitPrice = Utils.GetDecimal(item.Element("UnitPrice").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取接待行程实体
        /// <summary>
        /// 获取接待行程实体
        /// </summary>
        /// <param name="TravelXml">接待行程XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanTravel> GetTravelList(string TravelXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanTravel> list = new List<EyouSoft.Model.PlanStructure.MPlanTravel>();
            if (!string.IsNullOrEmpty(TravelXml))
            {
                XElement xem = XElement.Parse(TravelXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanTravel model = new EyouSoft.Model.PlanStructure.MPlanTravel();
                    model.TravelId = Utils.GetInt(item.Element("TravelId").Value);
                    model.TravelInfo = item.Element("TravelInfo").Value;
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
