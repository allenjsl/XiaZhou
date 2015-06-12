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
    /// 描述:数据操作游轮类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanShip : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanShip
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanShip()
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
        #region 游轮操作成员方法

        #region 添加游轮信息
        /// <summary>
        /// 添加游轮信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddShip(EyouSoft.Model.PlanStructure.MPlanShip model)
        {
            bool isOK = false;
            if (model != null)
            {
                if ((int)model.CategoryType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanShipType.涉外游轮)
                {
                    isOK = ShipForeignAddOrUpdate(model, true);
                }
                if ((int)model.CategoryType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanShipType.国内游轮)
                {
                    isOK = ShipChinaAddOrUpdate(model, true);
                }
            }
            return isOK;
        }
        #endregion

        #region 修改游轮信息
        /// <summary>
        /// 修改游轮信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateShip(EyouSoft.Model.PlanStructure.MPlanShip model)
        {
            bool isOK = false;
            if (model != null)
            {
                if ((int)model.CategoryType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanShipType.涉外游轮)
                {
                    isOK = ShipForeignAddOrUpdate(model, false);
                }
                if ((int)model.CategoryType == (int)EyouSoft.Model.EnumType.PlanStructure.PlanShipType.国内游轮)
                {
                    isOK = ShipChinaAddOrUpdate(model, false);
                }
            }
            return isOK;
        }
        #endregion

        #region 根据计调ID获取游轮实体
        /// <summary>
        /// 根据计调ID获取游轮实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanShip GetShipModel(string PlanID)
        {
            string strShip = "select a." + strPlan + " , ShipName,ShipCalls,LineId,RoomType,[Floor],Remark,IsMeal,IsTip,AdultPrice,ChildPrice,CategoryType,(select CrowdType,AdultNumber,AdultPrice,ChildNumber,ChildPrice,ChildNoOccupancy,ChildNoOccupancyPrice,BabyNumber,BabyNumberPrice from tbl_PlanShipCrowd as row where a.planid=planid for xml path,root('root'))as ShipCrowdXml ,(select ID,ProjectId,Price,Payment from tbl_PlanShipOwnCost as row where a.planid=planid for xml path,root('root'))as ShipOwnCostXml ,(select PriceType,Grade,AdultNumber,AdultPrice,ChildNumber,ChildPrice from tbl_PlanShipPrice as row where a.planid=planid for xml path,root('root'))as ShipPriceXml from tbl_Plan as a ,tbl_PlanShip as b where [type]=4 and a.planid=b.planid and a.planid=@PlanID";
            DbCommand cmd = db.GetSqlStringCommand(strShip);
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            EyouSoft.Model.PlanStructure.MPlanShip model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanShip()
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
                        CategoryType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipType)reader.GetByte(reader.GetOrdinal("CategoryType")),
                        Floor = (EyouSoft.Model.EnumType.PlanStructure.PlanShipFloor)reader.GetByte(reader.GetOrdinal("Floor")),
                        IsMeal = reader["IsMeal"].ToString() == "1" ? true : false,
                        IsTip = reader["IsTip"].ToString() == "1" ? true : false,
                        LineId = (int)reader["LineId"],
                        PlanShipCrowdList = GetShipCrowdList(reader.IsDBNull(reader.GetOrdinal("ShipCrowdXml")) ? string.Empty : reader["ShipCrowdXml"].ToString()),
                        PlanShipOwnCostList = GetShipOwnCostList(reader.IsDBNull(reader.GetOrdinal("ShipOwnCostXml")) ? string.Empty : reader["ShipOwnCostXml"].ToString()),
                        PlanShipPriceList = GetShipPriceList(reader.IsDBNull(reader.GetOrdinal("ShipPriceXml")) ? string.Empty : reader["ShipPriceXml"].ToString()),
                        Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? string.Empty : reader["Remark"].ToString(),
                        RoomType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType)reader.GetByte(reader.GetOrdinal("RoomType")),
                        ShipCalls = reader.IsDBNull(reader.GetOrdinal("ShipCalls")) ? string.Empty : reader["ShipCalls"].ToString(),
                        ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? string.Empty : reader["ShipName"].ToString(),
                    };
                }
                reader.Close();
            }
            return model;
        }
        #endregion

        #region 根据团队编号获取游轮列表
        /// <summary>
        /// 根据团队编号获取游轮列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanShip> GetShipList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PlanId,TourId,SourceId,SourceName,ShipName,PlanCost,PaymentType,Status,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts,SueId ");
            strSql.Append("from tbl_Plan as a,tbl_PlanShip as b where a.planid=b.planid and [type]=4 and TourId=@TourId and CompanyId=@CompanyId");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanShip> list = new List<EyouSoft.Model.PlanStructure.MPlanShip>();
            EyouSoft.Model.PlanStructure.MPlanShip model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanShip()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader.IsDBNull(reader.GetOrdinal("SourceId")) ? string.Empty : reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? string.Empty : reader["ShipName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        SueId = reader.IsDBNull(reader.GetOrdinal("SueId")) ? string.Empty : reader["SueId"].ToString(),
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

        #region 删除游轮计调项目
        /// <summary>
        /// 删除游轮计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <param name="type">true 国内,false 涉外</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteShip(string PlanID, bool type)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = null;
                if (type)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ChinaDelete");
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ForeignDelete");
                }
                if (cmd != null)
                {
                    db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                    db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                    DbHelper.RunProcedure(cmd, db);
                    IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
                }
            }
            return IsResult;
        }
        #endregion

        #region 游轮私有方法开始

        #region 涉外游轮添加或者更新
        /// <summary>
        /// 涉外游轮添加或者更新
        /// </summary>
        /// <param name="model">游轮实体</param>
        /// <param name="isAdd">添加OR更新</param>
        /// <returns></returns>
        private bool ShipForeignAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanShip model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ForeignAdd");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ForeignUpdate");
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
                db.AddInParameter(cmd, "ShipName", DbType.String, model.ShipName);
                db.AddInParameter(cmd, "ShipCalls", DbType.String, model.ShipCalls);
                db.AddInParameter(cmd, "LineId", DbType.Int32, model.LineId);
                db.AddInParameter(cmd, "RoomType", DbType.AnsiStringFixedLength, (int)model.RoomType);
                db.AddInParameter(cmd, "Floor", DbType.AnsiStringFixedLength, (int)model.Floor);
                db.AddInParameter(cmd, "Remark", DbType.String, model.Remark);
                db.AddInParameter(cmd, "IsMeal", DbType.String, model.IsMeal == true ? 1 : 0);
                db.AddInParameter(cmd, "IsTip", DbType.String, model.IsTip == true ? 1 : 0);
                db.AddInParameter(cmd, "ShipOwnCostXML", DbType.String, GetShipOwnCostXml(model.PlanShipOwnCostList));
                db.AddInParameter(cmd, "ShipCrowdXML", DbType.String, GetShipCrowdXml(model.PlanShipCrowdList));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 国内游轮添加或者更新
        /// <summary>
        /// 国内游轮添加或者更新
        /// </summary>
        /// <param name="model">游轮实体</param>
        /// <param name="isAdd">添加OR更新</param>
        /// <returns></returns>
        private bool ShipChinaAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanShip model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ChinaAdd");
                    db.AddInParameter(cmd, "PlanId", DbType.String, System.Guid.NewGuid().ToString());
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanShip_ChinaUpdate");
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
                db.AddInParameter(cmd, "ShipName", DbType.String, model.ShipName);
                db.AddInParameter(cmd, "LineId", DbType.Int32, model.LineId);
                db.AddInParameter(cmd, "ShipOwnCostXML", DbType.String, GetShipOwnCostXml(model.PlanShipOwnCostList));
                db.AddInParameter(cmd, "ShipPriceXml", DbType.String, GetShipPriceXml(model.PlanShipPriceList));
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 将游轮自费项目转成XML
        /// <summary>
        /// 将游轮自费项目转成XML
        /// </summary>
        /// <param name="list">游轮自费项目实体</param>
        /// <returns></returns>
        private string GetShipOwnCostXml(IList<EyouSoft.Model.PlanStructure.MPlanShipOwnCost> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<ShipOwnCost ID='{0}' ", System.Guid.NewGuid().ToString());
                    strRoomXml.AppendFormat(" Payment='{0}'", (int)item.Payment);
                    strRoomXml.AppendFormat(" Price='{0}'", Utils.ReplaceXmlSpecialCharacter(item.Price.ToString()));
                    strRoomXml.AppendFormat(" ProjectId='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ProjectId.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将游轮人群转成XML
        /// <summary>
        /// 将游轮人群转成XML
        /// </summary>
        /// <param name="list">将游轮人群实体</param>
        /// <returns></returns>
        private string GetShipCrowdXml(IList<EyouSoft.Model.PlanStructure.MPlanShipCrowd> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<ShipCrowd CrowdType='{0}' ", (int)item.CrowdType);
                    strRoomXml.AppendFormat(" AdultNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.AdultNumber.ToString()));
                    strRoomXml.AppendFormat(" AdultPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.AdultPrice.ToString()));
                    strRoomXml.AppendFormat(" BabyNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.BabyNumber.ToString()));
                    strRoomXml.AppendFormat(" ChildNoOccupancy='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildNoOccupancy.ToString()));
                    strRoomXml.AppendFormat(" ChildNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildNumber.ToString()));
                    strRoomXml.AppendFormat(" ChildPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildPrice.ToString()));
                    strRoomXml.AppendFormat(" ChildNoOccupancyPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.ChildNoOccupancyPrice.ToString()));
                    strRoomXml.AppendFormat(" BabyNumberPrice='{0}'", Utils.ReplaceXmlSpecialCharacter(item.BabyNumberPrice.ToString()));
                    strRoomXml.AppendFormat("/>");
                }
                strRoomXml.AppendFormat("</ROOT>");
                strXml = strRoomXml.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将游轮价格转成XML
        /// <summary>
        /// 将游轮价格转成XML
        /// </summary>
        /// <param name="list">将游轮价格实体</param>
        /// <returns></returns>
        private string GetShipPriceXml(IList<EyouSoft.Model.PlanStructure.MPlanShipPrice> list)
        {
            string strXml = string.Empty;
            StringBuilder strRoomXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strRoomXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strRoomXml.AppendFormat("<ShipPrice Grade='{0}' ", (int)item.Grade);
                    strRoomXml.AppendFormat(" PriceType='{0}'", (int)item.PriceType);
                    strRoomXml.AppendFormat(" AdultNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(item.AdultNumber.ToString()));
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

        #region 获取游轮自费项目实体
        /// <summary>
        /// 获取游轮自费项目实体
        /// </summary>
        /// <param name="ShipOwnCostXml">游轮自费项目XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanShipOwnCost> GetShipOwnCostList(string ShipOwnCostXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanShipOwnCost> list = new List<EyouSoft.Model.PlanStructure.MPlanShipOwnCost>();
            if (!string.IsNullOrEmpty(ShipOwnCostXml))
            {
                XElement xem = XElement.Parse(ShipOwnCostXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanShipOwnCost model = new EyouSoft.Model.PlanStructure.MPlanShipOwnCost();
                    model.Payment = (EyouSoft.Model.EnumType.PlanStructure.PlanShipOwnCostPanment)Utils.GetInt(item.Element("Payment").Value);
                    model.Price = Utils.GetDecimal(item.Element("Price").Value);
                    model.ProjectId = item.Element("ProjectId").Value;
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 获取游轮人群实体
        /// <summary>
        /// 获取游轮人群实体
        /// </summary>
        /// <param name="ShipCrowdXml">游轮人群XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanShipCrowd> GetShipCrowdList(string ShipCrowdXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanShipCrowd> list = new List<EyouSoft.Model.PlanStructure.MPlanShipCrowd>();
            if (!string.IsNullOrEmpty(ShipCrowdXml))
            {
                XElement xem = XElement.Parse(ShipCrowdXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanShipCrowd model = new EyouSoft.Model.PlanStructure.MPlanShipCrowd();
                    model.AdultNumber = Utils.GetInt(item.Element("AdultNumber").Value);
                    model.AdultPrice = Utils.GetDecimal(item.Element("AdultPrice").Value);
                    model.BabyNumber = Utils.GetInt(item.Element("BabyNumber").Value);
                    model.BabyNumberPrice = Utils.GetDecimal(item.Element("BabyNumberPrice").Value);
                    model.ChildNoOccupancy = Utils.GetInt(item.Element("ChildNoOccupancy").Value);
                    model.ChildNoOccupancyPrice = Utils.GetDecimal(item.Element("ChildNoOccupancyPrice").Value);
                    model.ChildNumber = Utils.GetInt(item.Element("ChildNumber").Value);
                    model.ChildPrice = Utils.GetDecimal(item.Element("ChildPrice").Value);
                    model.CrowdType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipCrowdType)Utils.GetInt(item.Element("CrowdType").Value);

                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #region 将游轮价格实体
        /// <summary>
        /// 将游轮价格实体
        /// </summary>
        /// <param name="ShipPriceXml">游轮价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanShipPrice> GetShipPriceList(string ShipPriceXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanShipPrice> list = new List<EyouSoft.Model.PlanStructure.MPlanShipPrice>();
            if (!string.IsNullOrEmpty(ShipPriceXml))
            {
                XElement xem = XElement.Parse(ShipPriceXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanShipPrice model = new EyouSoft.Model.PlanStructure.MPlanShipPrice();
                    model.Grade = (EyouSoft.Model.EnumType.PlanStructure.PlanShipGrade)Utils.GetInt(item.Element("Grade").Value);
                    model.AdultNumber = Utils.GetInt(item.Element("AdultNumber").Value);
                    model.AdultPrice = Utils.GetDecimal(item.Element("AdultPrice").Value);
                    model.ChildNumber = Utils.GetInt(item.Element("ChildNumber").Value);
                    model.ChildPrice = Utils.GetDecimal(item.Element("ChildPrice").Value);
                    model.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipPriceType)Utils.GetInt(item.Element("PriceType").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #endregion 游轮私有方法结束

        #endregion
    }
}
