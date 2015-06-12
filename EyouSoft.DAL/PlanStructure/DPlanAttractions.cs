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
    /// 描述:数据操作景点类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlanAttractions : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlanAttractions
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlanAttractions()
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
        #region 景点操作成员方法

        #region 添加景点信息
        /// <summary>
        /// 添加景点信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddAttractions(EyouSoft.Model.PlanStructure.MPlanAttractions model)
        {
            return AttractionsAddOrUpdate(model, true);
        }
        #endregion

        #region 修改景点信息
        /// <summary>
        /// 修改景点信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateAttractions(EyouSoft.Model.PlanStructure.MPlanAttractions model)
        {
            return AttractionsAddOrUpdate(model, false);
        }
        #endregion

        #region 根据计调ID获取景点实体
        /// <summary>
        /// 根据计调ID获取景点实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回景点实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanAttractions GetAttractionsModel(string PlanID)
        {
            string strAttractions = "select a." + strPlan + " ,StartTime,EndTime,StartSeg,EndSeg,(select PriceType,AdultNumber,ChildNumber,AdultPrice,ChildPrice from tbl_PlanAttractionsPrice as row where PlanId=a.PlanId for xml path,root('root')) as PriceXml from tbl_Plan as a,tbl_PlanAttractions as b  where a.PlanId=b.PlanId and  a.PlanId=@PlanId";

            EyouSoft.Model.PlanStructure.MPlanAttractions model = null;
            DbCommand cmd = db.GetSqlStringCommand(strAttractions);
            db.AddInParameter(cmd, "PlanID", DbType.String, PlanID);
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanAttractions()
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
                        LDate = reader.IsDBNull(reader.GetOrdinal("StartTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("StartTime")),
                        RDate = reader.IsDBNull(reader.GetOrdinal("EndTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("EndTime")),
                        StartSeg = reader.IsDBNull(reader.GetOrdinal("StartSeg")) ? string.Empty : reader["StartSeg"].ToString(),
                        EndSeg = reader.IsDBNull(reader.GetOrdinal("EndSeg")) ? string.Empty : reader["EndSeg"].ToString(),
                        PlanAttractionsPriceList = GetAttractionsPriceList(reader.IsDBNull(reader.GetOrdinal("PriceXml")) ? string.Empty : reader["PriceXml"].ToString())
                    };
                }
                reader.Close();
            }
            return model;
        }
        #endregion

        #region 根据团队编号获取景点列表
        /// <summary>
        /// 根据团队编号获取景点列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否查全部</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanAttractions> GetAttractionsList(string TourId, string CompanyId, bool isAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.PlanId,TourId,StartTime,StartSeg,EndSeg,EndTime,SourceId,SourceName,SalePlus,SaleCut,GuidePlus,GuideCut,PlanerPlus,PlanerCut,TotalCosts, ");
            strSql.Append(" (select PriceType,AdultNumber,ChildNumber,AdultPrice,ChildPrice from tbl_PlanAttractionsPrice as row where PlanId=a.PlanId for xml path,root('root'))as PriceXml, ");
            strSql.Append(" (select isnull(sum(AdultNumber),0)+isnull(sum(ChildNumber),0) from tbl_PlanAttractionsPrice where PlanId=a.PlanId ) as TotalPeopleNumber,isnull(PaymentType,0) as PaymentType,PlanCost,Status ");
            strSql.Append(" from tbl_Plan as a,tbl_PlanAttractions as b where a.PlanId=b.PlanId and [Type]=3  and TourId=@TourId and CompanyId=@CompanyId ");
            if (!isAll)
            {
                strSql.Append(" and PaymentType=3 ");
            }
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "TourId", DbType.String, TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, CompanyId);
            IList<EyouSoft.Model.PlanStructure.MPlanAttractions> list = new List<EyouSoft.Model.PlanStructure.MPlanAttractions>();
            EyouSoft.Model.PlanStructure.MPlanAttractions model = null;
            using (IDataReader reader = DbHelper.ExecuteReader(cmd, db))
            {
                while (reader.Read())
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanAttractions()
                    {
                        PlanId = reader["PlanId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("SourceName")) ? string.Empty : reader["SourceName"].ToString(),
                        PlanCost = reader.GetDecimal(reader.GetOrdinal("PlanCost")),
                        Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)reader.GetByte(reader.GetOrdinal("Status")),
                        PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)reader.GetByte(reader.GetOrdinal("PaymentType")),
                        SalePlus = reader.GetDecimal(reader.GetOrdinal("SalePlus")),
                        SaleCut = reader.GetDecimal(reader.GetOrdinal("SaleCut")),
                        GuidePlus = reader.GetDecimal(reader.GetOrdinal("GuidePlus")),
                        GuideCut = reader.GetDecimal(reader.GetOrdinal("GuideCut")),
                        PlanerPlus = reader.GetDecimal(reader.GetOrdinal("PlanerPlus")),
                        PlanerCut = reader.GetDecimal(reader.GetOrdinal("PlanerCut")),
                        TotalCosts = reader.GetDecimal(reader.GetOrdinal("TotalCosts")),
                        StartSeg = reader.IsDBNull(reader.GetOrdinal("StartSeg")) ? string.Empty : reader["StartSeg"].ToString(),
                        EndSeg = reader.IsDBNull(reader.GetOrdinal("EndSeg")) ? string.Empty : reader["EndSeg"].ToString(),
                        LDate = reader.IsDBNull(reader.GetOrdinal("StartTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("StartTime")),
                        RDate = reader.IsDBNull(reader.GetOrdinal("EndTime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("EndTime")),
                        TotalPeopleNumber = (int)reader["TotalPeopleNumber"],
                        PlanAttractionsPriceList = GetAttractionsPriceList(reader.IsDBNull(reader.GetOrdinal("PriceXml")) ? string.Empty : reader["PriceXml"].ToString())
                    };
                    list.Add(model); model = null;
                }
                reader.Close();
            }
            return list;
        }
        #endregion

        #region 删除景点计调项目
        /// <summary>
        /// 删除景点计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteAttractions(string PlanID)
        {
            bool IsResult = false;
            if (!string.IsNullOrEmpty(PlanID))
            {
                DbCommand cmd = db.GetStoredProcCommand("proc_PlanAttractions_Delete");
                db.AddInParameter(cmd, "PlanId", DbType.String, PlanID);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 私有方法

        #region 将景点价格实体转为XML
        /// <summary>
        /// 将景点价格实体转为XML
        /// </summary>
        /// <param name="list">景点价格实体</param>
        /// <returns></returns>
        private string GetAttractionsPrice(IList<EyouSoft.Model.PlanStructure.MPlanAttractionsPrice> list)
        {
            StringBuilder strPriceXml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strPriceXml.AppendFormat("<ROOT>");
                foreach (var item in list)
                {
                    strPriceXml.AppendFormat("<PriceXml PriceType='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.PriceType.ToString()));
                    strPriceXml.AppendFormat(" AdultNumber='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.AdultNumber.ToString()));
                    strPriceXml.AppendFormat(" AdultPrice='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.AdultPrice.ToString()));
                    strPriceXml.AppendFormat(" ChildNumber='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.ChildNumber.ToString()));
                    strPriceXml.AppendFormat(" ChildPrice='{0}' ", Utils.ReplaceXmlSpecialCharacter(item.ChildPrice.ToString()));
                    strPriceXml.AppendFormat("/>");
                }
                strPriceXml.AppendFormat("</ROOT>");
            }

            return strPriceXml.ToString();
        }
        #endregion

        #region 景点添加或者更新
        /// <summary>
        /// 景点添加或者更新
        /// </summary>
        /// <param name="model">景点实体</param>
        /// <param name="isAdd">是否添加</param>
        /// <returns></returns>
        private bool AttractionsAddOrUpdate(EyouSoft.Model.PlanStructure.MPlanAttractions model, bool isAdd)
        {
            bool IsResult = false;
            if (model != null)
            {
                string _price = GetAttractionsPrice(model.PlanAttractionsPriceList);
                DbCommand cmd = null;
                if (isAdd)
                {
                    cmd = db.GetStoredProcCommand("proc_PlanAttractions_Add");
                    db.AddInParameter(cmd, "PlanId", DbType.String, model.PlanId);
                    db.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyId);
                    db.AddInParameter(cmd, "TourId", DbType.String, model.TourId);
                    db.AddInParameter(cmd, "AddStatus", DbType.AnsiStringFixedLength, (int)model.AddStatus);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("proc_PlanAttractions_Update");
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
                db.AddInParameter(cmd, "StartTime", DbType.DateTime, model.LDate);
                db.AddInParameter(cmd, "EndTime", DbType.DateTime, model.RDate);
                db.AddInParameter(cmd, "StartSeg", DbType.DateTime, model.StartSeg);
                db.AddInParameter(cmd, "EndSeg", DbType.DateTime, model.EndSeg);
                db.AddInParameter(cmd, "PriceXml", DbType.String, _price);
                db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
                DbHelper.RunProcedure(cmd, db);
                IsResult = Convert.ToInt32(db.GetParameterValue(cmd, "IsResult")) == 1 ? true : false;
            }
            return IsResult;
        }
        #endregion

        #region 获取景点价格实体
        /// <summary>
        /// 获取景点价格实体
        /// </summary>
        /// <param name="PriceXml">景点价格XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanAttractionsPrice> GetAttractionsPriceList(string PriceXml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanAttractionsPrice> list = new List<EyouSoft.Model.PlanStructure.MPlanAttractionsPrice>();
            if (!string.IsNullOrEmpty(PriceXml))
            {
                XElement xem = XElement.Parse(PriceXml);
                var row = xem.Elements("row");
                foreach (var item in row)
                {
                    EyouSoft.Model.PlanStructure.MPlanAttractionsPrice model = new EyouSoft.Model.PlanStructure.MPlanAttractionsPrice();
                    model.AdultNumber = Utils.GetInt(item.Element("AdultNumber").Value);
                    model.AdultPrice = Utils.GetDecimal(item.Element("AdultPrice").Value);
                    model.ChildNumber = Utils.GetInt(item.Element("ChildNumber").Value);
                    model.ChildPrice = Utils.GetDecimal(item.Element("ChildPrice").Value);
                    model.PriceType = Utils.GetInt(item.Element("PriceType").Value);
                    list.Add(model); model = null;
                }
            }
            return list;
        }
        #endregion

        #endregion 私有方法结束

        #endregion 景点结束
    }
}
