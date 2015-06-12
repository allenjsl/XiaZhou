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
    /// 描述:数据操作用车类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanCar : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanCar
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanCar()
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
        #region 用车操作成员方法

        #region 添加用车信息
        /// <summary>
        /// 添加用车信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddCar(EyouSoft.Model.PlanStructure.MPlanCar model)
        {
            return CarAddOrUpdate(model, true);
        }
        #endregion

        #region 修改用车信息
        /// <summary>
        /// 修改用车信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateCar(EyouSoft.Model.PlanStructure.MPlanCar model)
        {
            return CarAddOrUpdate(model, false);
        }
        #endregion

        #region 根据计调ID获取用车实体
        /// <summary>
        /// 根据计调ID获取用车实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanCar GetCarModel(string PlanID)
        {
            EyouSoft.Model.PlanStructure.MPlanCar model = null;
            string strCar = "select a." + strPlan + ",(select StartTime, EndTime,StartSeg,EndSeg from tbl_PlanCarTime as row where a.PlanId=PlanId for xml path,root('root')) as CarTimeXml ,(select PricesType, Bridge,FuelCosts,DriversSubsidy,Amount,OtherCosts,Taiban from tbl_PlanCarPrice as row where a.PlanId=PlanId for xml path,root('root')) as CarPriceXml,CarId,VehicleType,Models,DriverPhone,(select top(1)TravelInfo from tbl_PlanTravel where PlanId=@PlanId )as TravelInfo from tbl_Plan as a, tbl_PlanCar as b where a.PlanId=b.PlanId and  [type]=2 and a.PlanId='@PlanID'";
            DbCommand cmd = db.GetSqlStringCommand(strCar);
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanCar()
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
                        PlanCarTimeList = GetCarTimeList(reader.IsDBNull(reader.GetOrdinal("CarTimeXml")) ? string.Empty : reader["CarTimeXml"].ToString()),
                        DriverPhone = reader.IsDBNull(reader.GetOrdinal("DriverPhone")) ? string.Empty : reader["DriverPhone"].ToString(),
                        Models = reader.IsDBNull(reader.GetOrdinal("Models")) ? string.Empty : reader["Models"].ToString(),
                        VehicleType = (EyouSoft.Model.EnumType.PlanStructure.PlanCarType)reader.GetByte(reader.GetOrdinal("VehicleType")),
                        CarId = reader.IsDBNull(reader.GetOrdinal("CarId")) ? string.Empty : reader["CarId"].ToString(),
                        PlanCarPriceList = GetCarPriceList(reader.IsDBNull(reader.GetOrdinal("CarPriceXml")) ? string.Empty : reader["CarPriceXml"].ToString()),
                        TravelInfo = reader.IsDBNull(reader.GetOrdinal("TravelInfo")) ? string.Empty : reader["TravelInfo"].ToString()
                    };
                }
                reader.Close();
            }

            return model;
        }
        #endregion

        #region 根据团队编号获取用车列表
        /// <summary>
        /// 根据团队编号获取用车列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanCar> GetCarList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PlanId,SourceId,SourceName,PlanCost,PaymentType,Status,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,[Type],(select StartTime, EndTime,StartSeg,EndSeg from tbl_PlanCarTime as row where a.PlanId=PlanId for xml path,root('root')) as CarTimeXml,Models,DriverPhone");
            strSql.Append(" from tbl_Plan as a, tbl_PlanCar as b ");
            strSql.Append(" where a.PlanId=b.PlanId and  [type]=2  and CompanyId=@CompanyId  and TourId=@TourId ");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanCar> list = new List<EyouSoft.Model.PlanStructure.MPlanCar>();
            EyouSoft.Model.PlanStructure.MPlanCar model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanCar()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        Type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)reader.GetByte(reader.GetOrdinal("Type")),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        DriverPhone = reader.IsDBNull(reader.GetOrdinal("DriverPhone")) ? string.Empty : reader["DriverPhone"].ToString(),
                        Models = reader.IsDBNull(reader.GetOrdinal("Models")) ? string.Empty : reader["Models"].ToString(),
                        PlanCarTimeList = GetCarTimeList(reader.IsDBNull(reader.GetOrdinal("CarTimeXml")) ? string.Empty : reader["CarTimeXml"].ToString())
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除用车计调项目
        /// <summary>
        /// 删除用车计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteCar(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanCar_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 用车私有方法

        #region 用车添加或者更新
        /// <summary>
        /// 用车添加或者更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isAdd"></param>
        /// <returns></returns>
        private bool CarAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanCar model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanCar_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanCar_Update");
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
                db.AddInParameter(cmd, "SueId", DbType.String, model.SueId);
                db.AddInParameter(cmd, "TotalCosts", DbType.Decimal, model.TotalCosts);
                db.AddInParameter(cmd, "CarId", DbType.String, model.CarId);
                db.AddInParameter(cmd, "VehicleType", DbType.AnsiStringFixedLength, (int)model.VehicleType);
                db.AddInParameter(cmd, "Models", DbType.String, model.Models);
                db.AddInParameter(cmd, "DriverPhone", DbType.String, model.DriverPhone);
                db.AddInParameter(cmd, "CarTimeXml", DbType.String, GetCarTimeXml(model.PlanCarTimeList));
                db.AddInParameter(cmd, "CarPriceXml", DbType.String, GetCarPriceXml(model.PlanCarPriceList));
                db.AddInParameter(cmd, "TravelInfo", DbType.String, model.TravelInfo);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将用车时间转成XML
        /// <summary>
        /// 将用车时间转成XML
        /// </summary>
        /// <param name="list">用车时间实体</param>
        /// <returns></returns>
        private string GetCarTimeXml(IList<EyouSoft.Model.PlanStructure.MPlanCarTime> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<CarTime EndTime='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.EndTime.ToString()));
                    strRoomXml.AppendFormat(" StartTime='{0}'", Utils.ReplaceXmlSpecialCharacter(item.StartTime.ToString()));
                    strRoomXml.AppendFormat(" StartSeg='{0}'", Utils.ReplaceXmlSpecialCharacter(item.StartSeg.ToString()));
                    strRoomXml.AppendFormat(" EndSeg='{0}'", Utils.ReplaceXmlSpecialCharacter(item.EndSeg.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将用车价格转成XML
        /// <summary>
        /// 将用车价格转成XML
        /// </summary>
        /// <param name="list">用车价格实体</param>
        /// <returns></returns>
        private string GetCarPriceXml(IList<EyouSoft.Model.PlanStructure.MPlanCarPrice> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<CarPrice Amount='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.Amount.ToString()));
                    strRoomXml.AppendFormat(" Bridge='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Bridge.ToString()));
                    strRoomXml.AppendFormat(" DriversSubsidy='{0}'", Utils.ReplaceXmlSpecialCharacter(item.DriversSubsidy.ToString()));
                    strRoomXml.AppendFormat(" FuelCosts='{0}'", Utils.ReplaceXmlSpecialCharacter(item.FuelCosts.ToString()));
                    strRoomXml.AppendFormat(" OtherCosts='{0}'", Utils.ReplaceXmlSpecialCharacter(item.OtherCosts.ToString()));
                    strRoomXml.AppendFormat(" PricesType='{0}'", Utils.ReplaceXmlSpecialCharacter(item.PricesType.ToString()));
                    strRoomXml.AppendFormat(" Taiban='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Taiban.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 获取用车时间实体
        /// <summary>
        /// 获取用车时间实体
        /// </summary>
        /// <param name="TimeXml">用车时间XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanCarTime> GetCarTimeList(string TimeXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanCarTime> list = new List<EyouSoft.Model.PlanStructure.MPlanCarTime>();
            if (!string.IsNullOrEmpty(TimeXml))
            {
                XElement xem = XElement.Parse(TimeXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanCarTime model = new EyouSoft.Model.PlanStructure.MPlanCarTime();
                    if (!string.IsNullOrEmpty(item.Element("EndTime").Value))
                    {
                        model.EndTime = Utils.GetDateTime(item.Element("EndTime").Value);
                    }
                    else
                    {
                        model.EndTime = null;
                    }
                    if (!string.IsNullOrEmpty(item.Element("StartTime").Value))
                    {
                        model.StartTime = Utils.GetDateTime(item.Element("StartTime").Value);
                    }
                    else
                    {
                        model.StartTime = null;
                    }
                    model.StartSeg = item.Element("StartSeg").Value;
                    model.EndSeg = item.Element("EndSeg").Value;
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取用车价格实体
        /// <summary>
        /// 获取用车价格实体
        /// </summary>
        /// <param name="TimeXml">用车价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanCarPrice> GetCarPriceList(string TimeXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanCarPrice> list = new List<EyouSoft.Model.PlanStructure.MPlanCarPrice>();
            if (!string.IsNullOrEmpty(TimeXml))
            {
                XElement xem = XElement.Parse(TimeXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanCarPrice model = new EyouSoft.Model.PlanStructure.MPlanCarPrice();
                    model.Amount = Utils.GetDecimal(item.Element("Amount").Value);
                    model.Bridge = Utils.GetDecimal(item.Element("Bridge").Value);
                    model.DriversSubsidy = Utils.GetDecimal(item.Element("DriversSubsidy").Value);
                    model.FuelCosts = Utils.GetDecimal(item.Element("FuelCosts").Value);
                    model.OtherCosts = Utils.GetDecimal(item.Element("OtherCosts").Value);
                    model.PricesType = (EyouSoft.Model.EnumType.PlanStructure.PlanCarPricesType)Utils.GetInt(item.Element("PricesType").Value);
                    model.Taiban = Utils.GetDecimal(item.Element("Taiban").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
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

        #endregion
        #endregion
    }
}
