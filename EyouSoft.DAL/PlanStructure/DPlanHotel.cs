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
    /// 描述:数据操作酒店类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanHotel : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanHotel
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanHotel()
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
        #region 酒店操作成员方法

        #region 添加酒店信息
        /// <summary>
        /// 添加酒店信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddHotel(EyouSoft.Model.PlanStructure.MPlanHotel model)
        {
            return HotelAddOrUpdate(model, true);
        }
        #endregion

        #region 修改酒店信息
        /// <summary>
        /// 修改酒店信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateHotel(EyouSoft.Model.PlanStructure.MPlanHotel model)
        {
            return HotelAddOrUpdate(model, false);
        }
        #endregion

        #region 根据计调ID获取酒店实体
        /// <summary>
        /// 根据计调ID获取酒店实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回model</returns>
        public EyouSoft.Model.PlanStructure.MPlanHotel GetHotelModel(string PlanID)
        {
            string strHotel = "select a." + strPlan + ",Days,Star,FreeNumber,FreeAmount,IsMeal,MealPrice,MealNumber,MealFrequency ,(select RoomId,RoomType,UnitPrice,Quantity from tbl_PlanHotelRoom as row  where PlanId=a.PlanId for xml path,root('root')) as HotelRoomXml ,(select CheckTime,LeaveTime,Days from tbl_PlanHotelTime as row  where PlanId=a.PlanId for xml path,root('root')) as HotelTimeXml from tbl_Plan as a,tbl_PlanHotel as b where  a.PlanId=b.PlanId and a.PlanId='@PlanId'";
            EyouSoft.Model.PlanStructure.MPlanHotel model = null;
            DbCommand cmd = db.GetSqlStringCommand(strHotel);
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanHotel()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        Confirmation = reader.GetDecimal(reader.GetOrdinal("Confirmation")),
                        CostId = reader.IsDBNull(reader.GetOrdinal("CostId")) ? string.Empty : reader["CostId"].ToString(),
                        CostRemarks = reader.IsDBNull(reader.GetOrdinal("CostRemarks")) ? string.Empty : reader["CostRemarks"].ToString(),
                        CostTime = reader.IsDBNull(reader.GetOrdinal("CostTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CostTime")),
                        Days = (int)reader["Days"],
                        FinRemark = reader.IsDBNull(reader.GetOrdinal("FinRemark")) ? string.Empty : reader["FinRemark"].ToString(),
                        FreeAmount = reader.GetDecimal(reader.GetOrdinal("FreeAmount")),
                        FreeNumber = (int)reader["FreeNumber"],
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        GuideNotes = reader.IsDBNull(reader.GetOrdinal("GuideNotes")) ? string.Empty : reader["GuideNotes"].ToString(),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        IsMeal = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)reader.GetByte(reader.GetOrdinal("IsMeal")),
                        IsOut = reader["IsOut"].ToString() == "1" ? true : false,
                        IsRebate = reader["IsRebate"].ToString() == "1" ? true : false,
                        MealFrequency = (int)reader["MealFrequency"],
                        MealNumber = (int)reader["MealNumber"],
                        MealPrice = reader.GetDecimal(reader.GetOrdinal("MealPrice")),
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
                        Star = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelStar)reader.GetByte(reader.GetOrdinal("Star")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        SueId = reader.IsDBNull(reader.GetOrdinal("SueId")) ? string.Empty : reader["SueId"].ToString(),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        TourId = reader.IsDBNull(reader.GetOrdinal("TourId")) ? string.Empty : reader["TourId"].ToString(),
                        Type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)reader.GetByte(reader.GetOrdinal("Type")),
                        IssueTime = reader.GetDateTime(reader.GetOrdinal("IssueTime")),
                        AddStatus = (EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus)reader.GetByte(reader.GetOrdinal("AddStatus")),
                        PlanHotelRoomList = GetHotelRoomList(reader.IsDBNull(reader.GetOrdinal("HotelRoomXml")) ? string.Empty : reader["HotelRoomXml"].ToString()),
                        PlanHotelTimeList = GetHotelTimeList(reader.IsDBNull(reader.GetOrdinal("HotelTimeXml")) ? string.Empty : reader["HotelTimeXml"].ToString())
                    };
                }
                reader.Close();
            }

            return model;
        }
        #endregion

        #region 根据团队编号获取酒店列表
        /// <summary>
        /// 根据团队编号获取酒店列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanHotel> GetHotelList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select a.PlanId,TourId,SourceId,[Type],SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,(select * from tbl_PlanHotelTime where a.PlanId=PlanId for xml path,root('root')) as HotelTimeXml, ");
            strSQL.Append("  SourceName,(select * from tbl_PlanHotelRoom where a.PlanId=PlanId for xml path,root('root')) as HotelRoomXml,FreeAmount,IsMeal,");
            strSQL.Append(" PlanCost,PaymentType,Status ");
            strSQL.Append(" from tbl_Plan as a,tbl_PlanHotel as b ");
            strSQL.Append(" where a.PlanId=b.PlanId and  [Type]=1  and TourId='@TourId' and CompanyId='@CompanyId'");
            if (!isAll)
            {
                strSQL.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSQL.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanHotel> list = new List<EyouSoft.Model.PlanStructure.MPlanHotel>();
            EyouSoft.Model.PlanStructure.MPlanHotel model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanHotel()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        Type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)reader.GetByte(reader.GetOrdinal("Type")),
                        PlanHotelTimeList = GetHotelTimeList(reader.IsDBNull(reader.GetOrdinal("HotelTimeXml")) ? string.Empty : reader["HotelTimeXml"].ToString()),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanHotelRoomList = GetHotelRoomList(reader.IsDBNull(reader.GetOrdinal("HotelRoomXml")) ? string.Empty : reader["HotelRoomXml"].ToString()),
                        FreeAmount = reader.GetDecimal(reader.GetOrdinal("FreeAmount")),
                        IsMeal = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)reader.GetByte(reader.GetOrdinal("IsMeal")),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status"))
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            };


            return list;
        }
        #endregion

        #region 删除酒店计调项目
        /// <summary>
        /// 删除酒店计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteHotel(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanHotel_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 酒店私有方法

        #region 将酒店房屋实体转成XML数据
        /// <summary>
        /// 将酒店房屋实体转成XML数据
        /// </summary>
        /// <param name="list">酒店房屋实体</param>
        /// <returns></returns>
        private string GetHotelRoom(IList<EyouSoft.Model.PlanStructure.MPlanHotelRoom> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<HotelRoom RoomType='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.RoomType.ToString()));
                    strRoomXml.AppendFormat(" RoomId='{0}'", Utils.ReplaceXmlSpecialCharacter(item.RoomId.ToString()));
                    strRoomXml.AppendFormat(" PriceType='{0}'", (int)item.PriceType);
                    strRoomXml.AppendFormat(" UnitPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.UnitPrice.ToString()));
                    strRoomXml.AppendFormat(" Quantity='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Quantity.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将酒店入住时间实体转成XML数据
        /// <summary>
        /// 将酒入住时间实体转成XML数据
        /// </summary>
        /// <param name="list">入住时间集合</param>
        /// <returns></returns>
        private string GetHotelTime(IList<EyouSoft.Model.PlanStructure.MPlanHotelTime> list)
        {
            string strXml = string.Empty;
            if (list != null && list.Count > 0)
            {
                StringBuilder strTimeXml = new StringBuilder();
                strTimeXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strTimeXml.AppendFormat("<HotelTime CheckTime='{0}'", Utils.ReplaceXmlSpecialCharacter(item.CheckTime.ToString()));
                    strTimeXml.AppendFormat(" LeaveTime='{0}'", Utils.ReplaceXmlSpecialCharacter(item.LeaveTime.ToString()));
                    strTimeXml.AppendFormat(" Days='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Days.ToString()));
                    strTimeXml.AppendFormat("/>");
                }
                strTimeXml.AppendFormat("</ROOT>");
                strXml = strTimeXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 获取酒店房型实体
        /// <summary>
        /// 获取酒店房型实体
        /// </summary>
        /// <param name="RoomXml">房型XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanHotelRoom> GetHotelRoomList(string RoomXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanHotelRoom> list = new List<EyouSoft.Model.PlanStructure.MPlanHotelRoom>();
            if (!string.IsNullOrEmpty(RoomXml))
            {
                XElement em = XElement.Parse(RoomXml);
                var row = em.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanHotelRoom model = new EyouSoft.Model.PlanStructure.MPlanHotelRoom();
                    model.Quantity = Utils.GetInt(item.Element("Quantity").Value);
                    model.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType)Utils.GetInt(item.Element("Type").Value);
                    model.RoomType = item.Element("RoomType").Value;
                    model.RoomId = item.Element("RoomId").Value;
                    model.UnitPrice = Utils.GetDecimal(item.Element("UnitPrice").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取酒店入住时间实体
        /// <summary>
        /// 获取酒店入住时间实体
        /// </summary>
        /// <param name="TimeXml">入住时间XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanHotelTime> GetHotelTimeList(string TimeXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanHotelTime> list = new List<EyouSoft.Model.PlanStructure.MPlanHotelTime>();
            if (!string.IsNullOrEmpty(TimeXml))
            {
                XElement xem = XElement.Parse(TimeXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanHotelTime model = new EyouSoft.Model.PlanStructure.MPlanHotelTime();
                    if (string.IsNullOrEmpty(item.Element("CheckTime").Value))
                    {
                        model.CheckTime = Utils.GetDateTime(item.Element("CheckTime").Value);
                    }
                    else
                    {
                        model.CheckTime = null;
                    }
                    model.Days = Utils.GetInt(item.Element("Days").Value);
                    if (!string.IsNullOrEmpty(item.Element("LeaveTime").Value))
                    {
                        model.LeaveTime = Utils.GetDateTime(item.Element("LeaveTime").Value);
                    }
                    else
                    {
                        model.LeaveTime = null;
                    }
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 添加或更新数据处理
        /// <summary>
        /// 添加或更新数据处理
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <param name="isAdd">true:添加，false:更新</param>
        /// <returns>true:成功，false:失败</returns>
        private bool HotelAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanHotel model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanHotel_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanHotel_Update");
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
                db.AddInParameter(cmd, "Days", DbType.Int32, model.Days);
                db.AddInParameter(cmd, "Star", DbType.AnsiStringFixedLength, (int)model.Star);
                db.AddInParameter(cmd, "FreeNumber", DbType.Int32, model.FreeNumber);
                db.AddInParameter(cmd, "FreeAmount", DbType.Decimal, model.FreeAmount);
                db.AddInParameter(cmd, "IsMeal", DbType.AnsiStringFixedLength, (int)model.IsMeal);
                db.AddInParameter(cmd, "MealPrice", DbType.Decimal, model.MealPrice);
                db.AddInParameter(cmd, "MealNumber", DbType.Int32, model.MealNumber);
                db.AddInParameter(cmd, "MealFrequency", DbType.Int32, model.MealFrequency);
                db.AddInParameter(cmd, "HotelRoom", DbType.String, GetHotelRoom(model.PlanHotelRoomList));
                db.AddInParameter(cmd, "HotelTime", DbType.String, GetHotelTime(model.PlanHotelTimeList));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #endregion

        #endregion
    }
}
