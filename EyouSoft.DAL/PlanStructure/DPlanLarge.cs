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
    /// 描述:数据操作大交通类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanLarge : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanLarge
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanLarge()
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

        #region 大交通成员操作方法

        #region 添加大交通信息
        /// <summary>
        /// 添加大交通信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddLarge(EyouSoft.Model.PlanStructure.MPlanLarge model)
        {
            bool isOk = false;
            if (model != null)
            {
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.飞机)
                {
                    isOk = AircraftAddOrUpdate(model, true, "proc_PlanLarge_AircraftAdd", 1);
                }
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.火车)
                {
                    isOk = AircraftAddOrUpdate(model, true, "proc_PlanLarge_TrainAdd", 2);
                }
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.汽车)
                {
                    isOk = AircraftAddOrUpdate(model, true, "proc_PlanLarge_CarAdd", 3);
                }
            }
            return isOk;
        }
        #endregion

        #region 修改大交通信息
        /// <summary>
        /// 修改大交通信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateLarge(EyouSoft.Model.PlanStructure.MPlanLarge model)
        {
            bool isOk = false;
            if (model != null)
            {
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.飞机)
                {
                    isOk = AircraftAddOrUpdate(model, false, "proc_PlanLarge_AircraftUpdate", 1);
                }
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.火车)
                {
                    isOk = AircraftAddOrUpdate(model, false, "proc_PlanLarge_TrainUpdate", 2);
                }
                if ((int)model.TripType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanLargeType.汽车)
                {
                    isOk = AircraftAddOrUpdate(model, false, "proc_PlanLarge_CarUpdate", 3);
                }
            }
            return isOk;
        }
        #endregion

        #region 根据计调ID获取大交通实体
        /// <summary>
        /// 根据计调ID获取大交通实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回大交通实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanLarge GetLargeModel(string PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a." + strPlan + ",TripType,IsInsurance,UnitPrice,Quantity,TicketTime, ");
            strSql.Append(" (select Standard,Price,Fee,Quantity,FreeVotes from tbl_PlanLargePrice as row where PlanId=a.PlanId for xml path,root('root')) as LargePriceXML,");
            strSql.Append(" (select DepartureTime,Numbers,Departure, Destination from tbl_PlanLargeTime as row where PlanId=a.PlanId  for xml path,root('root'))as LargeTimeXML,");
            strSql.Append(" (select SeatType,AdultsType,Number,Price,ParValue from tbl_PlanLargeType as row where  PlanId=a.PlanId  for xml path,root('root'))as LargeTypeXML");
            strSql.Append(" from tbl_Plan as a,tbl_PlanLarge as b");
            strSql.Append(" where a.PlanId=b.PlanId and [type]=9 and a.PlanId=@PlanId ");
            EyouSoft.Model.PlanStructure.MPlanLarge model = null;
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanLarge()
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
                        IsInsurance = reader["IsInsurance"].ToString() == "1" ? true : false,
                        Quantity = (int)reader["Quantity"],
                        TicketTime = reader.GetDateTime(reader.GetOrdinal("TicketTime")),
                        TripType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeType)reader.GetByte(reader.GetOrdinal("TripType")),
                        UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        PlanLargePriceList = GetLargePriceList(reader.IsDBNull(reader.GetOrdinal("LargePriceXML")) ? string.Empty : reader["LargePriceXML"].ToString()),
                        PlanLargeTimeList = GetLargeTimeList(reader.IsDBNull(reader.GetOrdinal("LargeTimeXML")) ? string.Empty : reader["LargeTimeXML"].ToString()),
                        PlanLargeTypeList = GetLargeTypeList(reader.IsDBNull(reader.GetOrdinal("LargeTypeXML")) ? string.Empty : reader["LargeTypeXML"].ToString())
                    };
                }
                reader.Close();
            }
            return model;
        }
        #endregion

        #region 根据团队编号获取大交通列表
        /// <summary>
        /// 根据团队编号获取大交通列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <param name="LargeType">类型[飞机，火车，汽车]</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanLarge> GetLargeList(string TourId, string CompanyId, bool isAll, EyouSoft.Model.EnumType.PlanStructure.PlanLargeType LargeType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.PlanId,TourId,SourceId,SourceName,PlanCost,PaymentType,Status,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,TripType,IsInsurance,UnitPrice,Quantity,TicketTime,");
            strSql.Append(" (select Standard,Price,Fee,Quantity,FreeVotes from tbl_PlanLargePrice as row where PlanId=a.PlanId for xml path,root('root')) as LargePriceXML,");
            strSql.Append(" (select DepartureTime,Numbers,Departure, Destination from tbl_PlanLargeTime as row where PlanId=a.PlanId  for xml path,root('root'))as LargeTimeXML,");
            strSql.Append(" (select SeatType,AdultsType,Number,Price,ParValue from tbl_PlanLargeType as row where  PlanId=a.PlanId  for xml path,root('root'))as LargeTypeXML ");
            strSql.Append(" from tbl_Plan as a,tbl_PlanLarge as b");
            strSql.Append(" where   a.PlanId=b.PlanId  and [type]=9 and TourId=@TourId and CompanyId=@CompanyId and TripType=@LargeType");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            db.AddInParameter(cmd, "LargeType", DbType.AnsiStringFixedLength, (int)LargeType);
            IList<EyouSoft.Model.PlanStructure.MPlanLarge> list = new List<EyouSoft.Model.PlanStructure.MPlanLarge>();
            EyouSoft.Model.PlanStructure.MPlanLarge model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanLarge()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader.IsDBNull(reader.GetOrdinal("TourId")) ? string.Empty : reader["TourId"].ToString(),
                        SourceId = reader.IsDBNull(reader.GetOrdinal("SourceId")) ? string.Empty : reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        IsInsurance = reader["IsInsurance"].ToString() == "1" ? true : false,
                        Quantity = (int)reader["Quantity"],
                        TicketTime = reader.GetDateTime(reader.GetOrdinal("TicketTime")),
                        TripType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeType)reader.GetByte(reader.GetOrdinal("TripType")),
                        UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        PlanLargePriceList = GetLargePriceList(reader.IsDBNull(reader.GetOrdinal("LargePriceXML")) ? string.Empty : reader["LargePriceXML"].ToString()),
                        PlanLargeTimeList = GetLargeTimeList(reader.IsDBNull(reader.GetOrdinal("LargeTimeXML")) ? string.Empty : reader["LargeTimeXML"].ToString()),
                        PlanLargeTypeList = GetLargeTypeList(reader.IsDBNull(reader.GetOrdinal("LargeTypeXML")) ? string.Empty : reader["LargeTypeXML"].ToString())
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除大交通保险项目
        /// <summary>
        /// 删除大交通保险项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteLarge(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanLarge_Delete");
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

        #region 添加or更新
        /// <summary>
        /// 添加or更新
        /// </summary>
        /// <param name="model">大交通实体</param>
        /// <param name="isAdd">添加or更新</param>
        /// <param name="ProcName">执行存储过程名</param>
        /// <param name="type">1,飞机 2,火车 3,汽车</param>
        /// <returns></returns>
        private bool AircraftAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanLarge model, bool isAdd, string ProcName, int type)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = db.GetStoredProcCommand(ProcName);
                if (isAdd)
                {

                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
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
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                switch (type)
                {
                    case 1://飞机
                        db.AddInParameter(cmd, "IsInsurance", DbType.String, model.IsInsurance == true ? 1 : 0);
                        db.AddInParameter(cmd, "LargeTime", DbType.String, GetLargeTimeXml(model.PlanLargeTimeList));
                        db.AddInParameter(cmd, "LargeType", DbType.String, GetLargeTypeXml(model.PlanLargeTypeList));
                        break;
                    case 2://火车
                        db.AddInParameter(cmd, "LargeTime", DbType.String, GetLargeTimeXml(model.PlanLargeTimeList));
                        db.AddInParameter(cmd, "LargePrice", DbType.String, GetLargePriceXml(model.PlanLargePriceList));
                        break;
                    case 3://汽车
                        db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, model.UnitPrice);
                        db.AddInParameter(cmd, "Quantity", DbType.Int32, model.Quantity);
                        db.AddInParameter(cmd, "LargeTime", DbType.String, GetLargeTimeXml(model.PlanLargeTimeList));
                        break;
                }
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将大交通价格实体转成Xml
        /// <summary>
        /// 将大交通价格实体转成Xml
        /// </summary>
        /// <param name="list">价格实体</param>
        /// <returns></returns>
        private string GetLargePriceXml(IList<EyouSoft.Model.PlanStructure.MPlanLargePrice> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<LargePrice Fee='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.Fee.ToString()));
                    strRoomXml.AppendFormat(" FreeVotes='{0}'", Utils.ReplaceXmlSpecialCharacter(item.FreeVotes.ToString()));
                    strRoomXml.AppendFormat(" Price='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Price.ToString()));
                    strRoomXml.AppendFormat(" Quantity='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Quantity.ToString()));
                    strRoomXml.AppendFormat(" Standard='{0}'", (int)item.Standard);
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将大交通时间实体转成Xml
        /// <summary>
        /// 将大交通时间实体转成Xml
        /// </summary>
        /// <param name="list">时间实体</param>
        /// <returns></returns>
        private string GetLargeTimeXml(IList<EyouSoft.Model.PlanStructure.MPlanLargeTime> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<LargeTime Departure='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.Departure.ToString()));
                    strRoomXml.AppendFormat(" DepartureTime='{0}'", Utils.ReplaceXmlSpecialCharacter(item.DepartureTime.ToString()));
                    strRoomXml.AppendFormat(" Destination='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Destination.ToString()));
                    strRoomXml.AppendFormat(" Numbers='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Numbers.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将大交通舱位实体转成Xml
        /// <summary>
        /// 将大交通舱位实体转成Xml
        /// </summary>
        /// <param name="list">舱位实体</param>
        /// <returns></returns>
        private string GetLargeTypeXml(IList<EyouSoft.Model.PlanStructure.MPlanLargeType> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<LargeType AdultsType='{0}' ", (int)item.AdultsType);
                    strRoomXml.AppendFormat(" Number='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Number.ToString()));
                    strRoomXml.AppendFormat(" ParValue='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ParValue.ToString()));
                    strRoomXml.AppendFormat(" Price='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Price.ToString()));
                    strRoomXml.AppendFormat(" SeatType='{0}'", (int)item.SeatType);
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 获取大交通价格实体
        /// <summary>
        /// 获取大交通价格实体
        /// </summary>
        /// <param name="LargePriceXml">大交通价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanLargePrice> GetLargePriceList(string LargePriceXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanLargePrice> list = new List<EyouSoft.Model.PlanStructure.MPlanLargePrice>();
            if (!string.IsNullOrEmpty(LargePriceXml))
            {
                EyouSoft.Model.PlanStructure.MPlanLargePrice model = null;
                XElement xem = XElement.Parse(LargePriceXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanLargePrice()
                    {
                        Fee = Utils.GetDecimal(item.Element("Fee").Value),
                        FreeVotes = Utils.GetInt(item.Element("FreeVotes").Value),
                        Price = Utils.GetDecimal(item.Element("Price").Value),
                        Quantity = Utils.GetInt(item.Element("Quantity").Value),
                        Standard = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeStandard)Utils.GetInt(item.Element("Standard").Value)
                    };
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取大交通时间实体
        /// <summary>
        /// 获取大交通时间实体
        /// </summary>
        /// <param name="LargeTimeXml">大交通时间XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanLargeTime> GetLargeTimeList(string LargeTimeXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanLargeTime> list = new List<EyouSoft.Model.PlanStructure.MPlanLargeTime>();
            if (!string.IsNullOrEmpty(LargeTimeXml))
            {
                EyouSoft.Model.PlanStructure.MPlanLargeTime model = null;
                XElement xem = XElement.Parse(LargeTimeXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanLargeTime()
                    {
                        Departure = item.Element("Departure").Value,
                        DepartureTime = item.Element("DepartureTime").Value == string.Empty ? DateTime.MinValue : Utils.GetDateTime(item.Element("DepartureTime").Value),
                        Destination = item.Element("Destination").Value,
                        Numbers = item.Element("Numbers").Value
                    };
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取大交通舱位实体
        /// <summary>
        /// 获取大交通舱位实体
        /// </summary>
        /// <param name="LargeTypeXml">大交通舱位XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanLargeType> GetLargeTypeList(string LargeTypeXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanLargeType> list = new List<EyouSoft.Model.PlanStructure.MPlanLargeType>();
            if (!string.IsNullOrEmpty(LargeTypeXml))
            {
                EyouSoft.Model.PlanStructure.MPlanLargeType model = null;
                XElement xem = XElement.Parse(LargeTypeXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanLargeType()
                    {
                        AdultsType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType)Utils.GetInt(item.Element("AdultsType").Value),
                        Number = Utils.GetInt(item.Element("Number").Value),
                        ParValue = Utils.GetDecimal(item.Element("ParValue").Value),
                        Price = Utils.GetDecimal(item.Element("Price").Value),
                        SeatType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType)Utils.GetInt(item.Element("SeatType").Value)
                    };
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion
        #endregion
    }
}
