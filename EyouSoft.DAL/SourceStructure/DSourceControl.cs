using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SourceStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Toolkit.DAL;
using System.Xml;

namespace EyouSoft.DAL.SourceStructure
{
    /// <summary>
    /// 预控资源数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/14
    /// </summary>
    public class DSourceControl : DALBase, EyouSoft.IDAL.SourceStructure.ISourceControl
    {
        #region 变量

        private readonly Database _db = null;
        EyouSoft.DAL.SourceStructure.DSource sourceDal = new EyouSoft.DAL.SourceStructure.DSource();
        EyouSoft.DAL.TourStructure.DTourOrder tourOrderDal = new EyouSoft.DAL.TourStructure.DTourOrder();
        #endregion

        #region 构造函数
        public DSourceControl()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region ISourceControl 成员

        #region 车辆预控
        /// <summary>
        /// 添加车辆预控
        /// </summary>
        /// <param name="item">车辆预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueCar(MSourceSueCar item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_SourceSueCar_Add");
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.Id);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, item.SourceId);
            this._db.AddInParameter(comm, "@carName", DbType.String, item.SourceName);
            this._db.AddInParameter(comm, "@contorlNum", DbType.Int32, item.ControlNum);
            this._db.AddInParameter(comm, "@CarId", DbType.AnsiStringFixedLength, item.CarId);
            this._db.AddInParameter(comm, "@typeName", DbType.String, item.TypeName);
            this._db.AddInParameter(comm, "@daysNum", DbType.Int32, item.DaysNum);
            this._db.AddInParameter(comm, "@controlType", DbType.Byte, (int)item.SourceControlType);
            this._db.AddInParameter(comm, "@shareType", DbType.Byte, (int)item.ShareType);
            this._db.AddInParameter(comm, "@operatorId", DbType.String, item.OperatorId);
            _db.AddInParameter(comm, "@remark", DbType.String, item.Remark);
            _db.AddInParameter(comm, "@unitprice", DbType.Currency, item.UnitPrice);
            _db.AddInParameter(comm, "@totalprice", DbType.Currency, item.TotalPrice);
            _db.AddInParameter(comm, "@SueStartTime", DbType.DateTime, item.SueStartTime);
            _db.AddInParameter(comm, "@SueEndTime", DbType.DateTime, item.SueEndTime);
            _db.AddInParameter(comm, "@lastTime", DbType.DateTime, item.LastTime);
            _db.AddInParameter(comm, "@deptid", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, StructureSourceXml(SourceControlCategory.车辆, item));

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取车辆预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车辆预控信息</returns>
        public MSourceSueCar GetModelByCarId(string id, string companyId)
        {
            string sql = "SELECT * FROM view_SourceSueCar WHERE Id = @id AND CompanyId =@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MSourceSueCar item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MSourceSueCar()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("CarName")) ? string.Empty : reader["CarName"].ToString(),
                        CarId = reader["CarId"].ToString(),
                        TypeName = reader.IsDBNull(reader.GetOrdinal("TypeName")) ? string.Empty : reader["TypeName"].ToString(),
                        ControlNum = (int)reader["ControlNum"],
                        DaysNum = (int)reader["DaysNum"],
                        AlreadyNum = reader.IsDBNull(reader.GetOrdinal("AlreadyNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                        SueStartTime = reader.IsDBNull(reader.GetOrdinal("SueStartTime")) ? null : (DateTime?)reader["SueStartTime"],
                        SueEndTime = reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? null : (DateTime?)reader["SueEndTime"],
                        LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader["LastTime"],
                        UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                        Remark = reader["Remark"].ToString()
                    };
                }
            }

            return item;
        }

        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<MSueUse> GetCarUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" Type = {0} and CompanyId ='{1}' and SueId ='{2}'", (int)PlanProject.用车, companyId, sueId);
            IList<MSueUse> list = new List<MSueUse>();
            MSueUse item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_SueCarUse", "PlanId", "*", sql.ToString(), "IssueTime desc"))
            {
                while (reader.Read())
                {
                    item = new MSueUse()
                        {
                            TourCode = reader["TourCode"].ToString(),
                            RouteName = reader["RouteName"].ToString(),
                            Count = (int)reader["AlreadyNum"],
                            RouteId = (string)reader["RouteId"],
                            GuideList = sourceDal.GetGuideLst(reader["TourId"].ToString())
                        };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取车辆预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount, MSourceSueCarSearch search)
        {
            string tableName = "view_SourceSueCar";
            string fields = "*";
            string primaryKey = "Id";
            string orderBy = "ControlNum DESC";
            StringBuilder query = new StringBuilder();

            #region 构造查询条件
            query.AppendFormat(" CompanyId = '{0}'", search.CompanyId);
            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" AND OperatorId ='{0}'", search.OperatorId);
            }
            if (search.IsTranTip)
            {
                query.Append(" AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0");
                if (search.EarlyDays != 0)
                {
                    query.AppendFormat(" AND datediff(day,getdate(),LastTime)<={0}", search.EarlyDays);
                }
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                query.AppendFormat(" AND CarName like'%{0}%'", search.SourceName);
            }
            if (search.DaysNum != null)
            {
                query.AppendFormat(" AND DaysNum = {0}", search.DaysNum);
            }
            if (!string.IsNullOrEmpty(search.CarType))
            {
                query.AppendFormat(" AND TypeName  like '%{0}%'", search.CarType);
            }
            #endregion

            IList<MSourceSueCar> list = new List<MSourceSueCar>();
            MSourceSueCar item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent,
                ref pageCount, tableName, primaryKey, fields, query.ToString(), orderBy))
            {

                while (reader.Read())
                {
                    list.Add(new MSourceSueCar
                        {
                            Id = reader["Id"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("CarName")) ? string.Empty : reader["CarName"].ToString(),
                            CarId = reader["CarId"].ToString(),
                            TypeName = reader.IsDBNull(reader.GetOrdinal("TypeName")) ? string.Empty : reader["TypeName"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            DaysNum = (int)reader["DaysNum"],
                            AlreadyNum = reader.IsDBNull(reader.GetOrdinal("AlreadyNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                            SueStartTime = reader.IsDBNull(reader.GetOrdinal("SueStartTime")) ? null : (DateTime?)reader["SueStartTime"],
                            SueEndTime = reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? null : (DateTime?)reader["SueEndTime"],
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader["LastTime"]
                        });
                }
            }

            return list;
        }

        /// <summary>
        /// 分页获取车辆预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount,
            string tourid, string operatorId, MSourceSueCarSearch search)
        {
            string tableName = "view_SourceSueCar";
            string fields = "*";
            string primaryKey = "Id";
            string orderBy = "ID";

            #region 构造查询实体
            StringBuilder where = new StringBuilder();
            //总控-共享
            where.Append(" ((ControlType = 2 AND ShareType = 1) ");
            //总控-不共享-用车人
            where.AppendFormat(" or (ControlType = 2 AND ShareType = 2 and exists(select SueId from tbl_SourceSueOperator where [type] = 2 AND OperatorId = '{0}'))", operatorId);
            //单控-团号
            where.AppendFormat(" or (ControlType = 1 and exists(select SueId from tbl_SourceSueTour where [type] = 2 AND TourId = '{0}')))", tourid);
            //剩余数量大于0
            where.AppendFormat(" and ControlNum-AlreadyNum > 0 and CompanyId = '{0}'", search.CompanyId);

            if (search.DistrictId != null && search.DistrictId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CountyId = {0} and SourceId=view_SourceSueCar.SourceId)", search.DistrictId);
            }
            else if (search.CityId != null && search.CityId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CityId = {0} and SourceId=view_SourceSueCar.SourceId)", search.CityId);
            }
            else if (search.ProvinceId != null && search.ProvinceId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where ProvinceId = {0} and SourceId=view_SourceSueCar.SourceId)", search.ProvinceId);
            }

            if (!string.IsNullOrEmpty(search.SourceName))
            {
                where.AppendFormat(" AND CarName like '%{0}%'", search.SourceName);
            }
            #endregion

            IList<MSourceSueCar> list = new List<MSourceSueCar>();
            MSourceSueCar item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent,
                ref pageCount, tableName, primaryKey, fields, where.ToString(), orderBy))
            {

                while (reader.Read())
                {
                    list.Add(
                        item = new MSourceSueCar()
                        {
                            Id = reader["Id"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("CarName")) ? string.Empty : reader["CarName"].ToString(),
                            CarId = reader["CarId"].ToString(),
                            TypeName = reader.IsDBNull(reader.GetOrdinal("TypeName")) ? string.Empty : reader["TypeName"].ToString(),
                            AlreadyNum = reader.IsDBNull(reader.GetOrdinal("AlreadyNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                            CompanyId = reader["CompanyId"].ToString(),
                            ControlNum = reader.IsDBNull(reader.GetOrdinal("ControlNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("ControlNum")),
                            SueStartTime = reader.IsDBNull(reader.GetOrdinal("SueStartTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("SueStartTime")),
                            SueEndTime = reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("SueEndTime")),
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("LastTime"))
                        });
                }
            }

            return list;
        }
        #endregion

        #region 酒店预控
        /// <summary>
        /// 添加酒店预控
        /// </summary>
        /// <param name="item">酒店预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueHotel(MSourceSueHotel item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_SourceSueHotel_Add");
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.Id);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, item.SourceId);
            this._db.AddInParameter(comm, "@hotelName", DbType.String, item.SourceName);
            this._db.AddInParameter(comm, "@roomId", DbType.AnsiStringFixedLength, item.RoomId);
            this._db.AddInParameter(comm, "@roomType", DbType.String, item.RoomType);
            this._db.AddInParameter(comm, "@contorlNum", DbType.Int32, item.ControlNum);
            this._db.AddInParameter(comm, "@advance", DbType.Currency, item.Advance);
            this._db.AddInParameter(comm, "@startTime", DbType.DateTime, item.PeriodStartTime);
            this._db.AddInParameter(comm, "@endTime", DbType.DateTime, item.PeriodEndTime);
            this._db.AddInParameter(comm, "@contorlType", DbType.Byte, (int)item.SourceControlType);
            this._db.AddInParameter(comm, "@shareType", DbType.Byte, (int)item.ShareType);
            this._db.AddInParameter(comm, "@operatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            _db.AddInParameter(comm, "@remark", DbType.String, item.Remark);
            _db.AddInParameter(comm, "@unitprice", DbType.Currency, item.UnitPrice);
            _db.AddInParameter(comm, "@totalprice", DbType.Currency, item.TotalPrice);
            _db.AddInParameter(comm, "@deptid", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@lasttime", DbType.DateTime, item.LastTime);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, StructureSourceXml(SourceControlCategory.酒店, item));

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 添加变更记录
        /// </summary>
        /// <param name="item">变更记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        /// <returns></returns>
        public bool AddSueHotelChange(MSourceSueHotelChange item)
        {
            string sql = string.Empty;
            switch (item.Type)
            {
                case Model.EnumType.SourceStructure.SourceControlCategory.酒店:
                    sql = "UPDATE tbl_SourceSueHotel SET ControlNum = ControlNum + @ControlNum WHERE Id = @Id";
                    break;
                case Model.EnumType.SourceStructure.SourceControlCategory.车辆:
                    sql = "UPDATE tbl_SourceSueCar SET ControlNum = ControlNum + @ControlNum WHERE Id = @Id";
                    break;
                case Model.EnumType.SourceStructure.SourceControlCategory.游轮:
                    sql = "UPDATE tbl_SourceSueShip SET ControlNum = ControlNum + @ControlNum WHERE Id = @Id";
                    break;
                case Model.EnumType.SourceStructure.SourceControlCategory.景点:
                    sql = "UPDATE tbl_SourceSueSight SET ControlNum = ControlNum + @ControlNum WHERE Id = @Id";
                    break;
                case Model.EnumType.SourceStructure.SourceControlCategory.其他:
                    sql = "UPDATE tbl_SourceSueOther SET ControlNum = ControlNum + @ControlNum WHERE Id = @Id";
                    break;
            }
            sql +=
                " INSERT INTO tbl_SourceSueHotelChange(SueId,Plus,Cut,OperatorId,Operator,Type,Remark) VALUES(@Id,@Plus,@Cut,@OperatorId,@Operator,@Type,@Remark)";


            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@ControlNum", DbType.Int32, item.Plus - item.Cut);
            this._db.AddInParameter(comm, "@Id", DbType.AnsiStringFixedLength, item.SueId);
            this._db.AddInParameter(comm, "@Plus", DbType.Int32, item.Plus);
            this._db.AddInParameter(comm, "@Cut", DbType.Int32, item.Cut);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Operator", DbType.String, item.Operator);
            this._db.AddInParameter(comm, "@Type", DbType.Byte, (int)item.Type);
            this._db.AddInParameter(comm, "@Remark", DbType.String, item.Remark);

            int reuslt = DbHelper.ExecuteSqlTrans(comm, this._db);

            return reuslt > 0 ? true : false;
        }
        /// <summary>
        /// 获取酒店预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>酒店预控信息</returns>
        public MSourceSueHotel GetModelByHotelId(string id, string companyId)
        {
            string sql = "SELECT * FROM view_SourceSueHotel WHERE Id = @id AND CompanyId =@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MSourceSueHotel item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MSourceSueHotel()
                    {
                        Id = reader["Id"].ToString(),
                        CompanyId = reader["CompanyId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("HotelName")) ? string.Empty : reader["HotelName"].ToString(),
                        RoomId = reader["RoomId"].ToString(),
                        RoomType = reader["RoomType"].ToString(),
                        ControlNum = (int)reader["ControlNum"],
                        PeriodStartTime = DateTime.Parse(reader["PeriodStartTime"].ToString()),
                        PeriodEndTime = DateTime.Parse(reader["PeriodEndTime"].ToString()),
                        LastTime = DateTime.Parse(reader["LastTime"].ToString()),
                        AlreadyNum = (int)reader["AlreadyNum"],
                        ShareType = (ShareType)reader.GetByte(reader.GetOrdinal("ShareType")),
                        SourceControlType = (SourceControlType)reader.GetByte(reader.GetOrdinal("ControlType")),
                        UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                        Advance = reader.IsDBNull(reader.GetOrdinal("Advance")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Advance")),
                        Remark = reader["Remark"].ToString()
                    };
                }
            }

            return item;
        }
        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>已使用列表</returns>
        public IList<MSueUse> GetHotelUseList(string sueId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("  [Type] = {0} and CompanyId ='{1}' and SueId='{2}'", (int)PlanProject.酒店, companyId, sueId);

            IList<MSueUse> list = new List<MSueUse>();
            MSueUse item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_SueHotelUse", "PlanId", "*", sql.ToString(), "IssueTime desc"))
            {
                while (reader.Read())
                {
                    item = new MSueUse()
                         {
                             TourCode = reader["TourCode"].ToString(),
                             RouteName = reader["RouteName"].ToString(),
                             //LDate = DateTime.Parse(reader["LDate"].ToString()),
                             //SellerName = reader["SellerName"].ToString(),
                             //PlanName = reader["OperatorName"].ToString(),
                             Count = (int)reader["AlreadyNum"],
                             RouteId = (string)reader["RouteId"],
                             Id = (int)reader["Id"]
                         };
                    item.GuideList = sourceDal.GetGuideLst(reader["TourId"].ToString());
                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendFormat("select (select SourceId GuideId,SourceName GuideName from tbl_Plan where Type={0} and TourId='{1}' for xml path,elements,root('item')) Guide from tbl_plan where Type={0} and TourId='{1}'", (int)Model.EnumType.PlanStructure.PlanProject.导游, reader["TourId"].ToString());
                    //DbCommand gDc = _db.GetSqlStringCommand(sb.ToString());
                    //XmlDocument xd = new XmlDocument();
                    //using (IDataReader gReader = DbHelper.ExecuteReader(gDc, _db))
                    //{
                    //    xd = new XmlDocument();
                    //    while (gReader.Read())
                    //    {
                    //        xd = new XmlDocument();
                    //        if (!reader.IsDBNull(reader.GetOrdinal("Guide")))
                    //        {
                    //            xd.LoadXml((string)reader["Guide"]);
                    //        }

                    //        for (int i = 0; i < xd.DocumentElement.ChildNodes.Count; i++)
                    //        {
                    //            XmlElement xe = (XmlElement)xd.DocumentElement.ChildNodes[i];
                    //            Model.SourceStructure.MSourceGuide gModel = sourceDal.GetGuideModel(xe.ChildNodes[0].FirstChild.Value);
                    //            item.GuideList.Add(gModel);
                    //        }
                    //    }
                    //}
                    list.Add(item);
                }
            }

            return list;
        }
        /// <summary>
        /// 分页获取酒店预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount,
            MSourceSueHotelSearch search)
        {
            string tableName = "view_SourceSueHotel";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ControlNum DESC";
            #region 构造查询条件
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}'", search.CompanyId);
            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" AND OperatorId ='{0}'", search.OperatorId);
            }
            if (search.IsTranTip)
            {
                query.Append(" AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0 ");
                if (search.EarlyDays != 0)
                {
                    query.AppendFormat(" AND datediff(day,getdate(),LastTime)<={0}", search.EarlyDays);
                }
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                query.AppendFormat(" AND HotelName like '%{0}%'", search.SourceName);
            }
            if (!string.IsNullOrEmpty(search.RoomType))
            {
                query.AppendFormat(" AND RoomType like'%{0}%'", search.RoomType);
            }
            if (search.PeriodStartTime != null)
            {
                query.AppendFormat(" AND PeriodStartTime >= '{0}'", search.PeriodStartTime);
            }
            if (search.PeriodEndTime != null)
            {
                query.AppendFormat(" AND PeriodEndTime <= '{0}'", search.PeriodEndTime);
            }
            #endregion

            IList<MSourceSueHotel> list = new List<MSourceSueHotel>();
            MSourceSueHotel item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MSourceSueHotel()
                        {
                            Id = reader["Id"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("HotelName")) ? string.Empty : reader["HotelName"].ToString(),
                            RoomId = reader["RoomId"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            PeriodStartTime = DateTime.Parse(reader["PeriodStartTime"].ToString()),
                            PeriodEndTime = DateTime.Parse(reader["PeriodEndTime"].ToString()),
                            LastTime = DateTime.Parse(reader["LastTime"].ToString()),
                            AlreadyNum = (int)reader["AlreadyNum"],
                            ShareType = (ShareType)reader.GetByte(reader.GetOrdinal("ShareType")),
                            SourceControlType = (SourceControlType)reader.GetByte(reader.GetOrdinal("ControlType"))
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 分页获取酒店预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount,
            string tourid, string operatorId, MSourceSueHotelSearch search)
        {
            string tableName = "view_SourceSueHotel";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ID";

            #region 构造查询实体
            StringBuilder where = new StringBuilder();
            //总控-共享
            where.Append(" ((ControlType = 2 AND ShareType = 1)");
            //总控-不共享-用房人
            where.AppendFormat(" or (ControlType = 2 AND ShareType = 2 and exists(select 1 from tbl_SourceSueOperator where [type] = 1 AND OperatorId = '{0}'))", operatorId);
            //单控-团号
            where.AppendFormat(" or (ControlType = 1 AND exists(select 1 from tbl_SourceSueTour where [type] = 1 AND TourId = '{0}')))", tourid);
            //剩余数量大于0
            where.AppendFormat(" and ControlNum - AlreadyNum > 0 AND CompanyId = '{0}'", search.CompanyId);
            if (search.DistrictId != null && search.DistrictId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CountyId = {0} and SourceId=view_SourceSueHotel.SourceId)", search.DistrictId);
            }
            else if (search.CityId != null && search.CityId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CityId = {0} and SourceId=view_SourceSueHotel.SourceId)", search.CityId);
            }
            else if (search.ProvinceId != null && search.ProvinceId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where ProvinceId = {0} and SourceId=view_SourceSueHotel.SourceId)", search.ProvinceId);
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                where.AppendFormat(" AND HotelName like '%{0}%'", search.SourceName);
            }
            #endregion

            IList<MSourceSueHotel> list = new List<MSourceSueHotel>();
            MSourceSueHotel item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, where.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MSourceSueHotel()
                        {
                            Id = reader["Id"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("HotelName")) ? string.Empty : reader["HotelName"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            RoomId = reader["RoomId"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            AlreadyNum = (int)reader["AlreadyNum"],
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LastTime")),
                            PeriodEndTime = reader.IsDBNull(reader.GetOrdinal("PeriodEndTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("PeriodEndTime")),
                            PeriodStartTime = reader.IsDBNull(reader.GetOrdinal("PeriodStartTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("PeriodStartTime"))
                        });
                }
            }

            return list;
        }

        /// <summary>
        /// 获得计调安排资源预控列表
        /// </summary>
        /// <param name="searchModel">查询Model</param>
        /// <param name="type">资源预控类型(酒店,车辆,游船)</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSourceSuePlan> GetSourceSuePlanList(Model.SourceStructure.MSourceSuePlan searchModel, SourceControlCategory type, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.SourceStructure.MSourceSuePlan> list = new List<Model.SourceStructure.MSourceSuePlan>();
            Model.SourceStructure.MSourceSuePlan model = new Model.SourceStructure.MSourceSuePlan();
            string viewName = string.Empty;
            StringBuilder WhereSql = new StringBuilder();
            WhereSql.AppendFormat(" CompanyId='{0}' ", searchModel.CompanyId);
            if (!string.IsNullOrEmpty(searchModel.SourceName))
                WhereSql.AppendFormat(" and SourceName='{0}' ", searchModel.SourceName);
            if (model.ProvinceId != 0)
                WhereSql.AppendFormat(" and ProvinceId={0} ", searchModel.ProvinceId);
            if (model.CityId != 0)
                WhereSql.AppendFormat(" and CityId={0} ", searchModel.CityId);
            if (model.CountyId != 0)
                WhereSql.AppendFormat(" and CountyId={0} ", searchModel.CountyId);
            WhereSql.AppendFormat(" and ContactType={0} ", (int)Model.EnumType.ComStructure.LxrType.供应商);
            switch (type)
            {
                case SourceControlCategory.酒店:
                    viewName = "view_SourceSuePlanHotel";
                    break;
                case SourceControlCategory.车辆:
                    viewName = "view_SourceSuePlanCar";
                    break;
                case SourceControlCategory.游轮:
                    viewName = "view_SourceSuePlanShip";
                    break;
            }
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, viewName, "SourceId", "*", WhereSql.ToString(), "IssueTime desc"))
            {
                while (reader.Read())
                {
                    model.CountyId = !reader.IsDBNull(reader.GetOrdinal("CountyId")) ? (int)reader["CountyId"] : 0;
                    model.IsPermission = !reader.IsDBNull(reader.GetOrdinal("IsPermission")) ? reader["IsPermission"].ToString() == "0" ? false : true : false;
                    model.IsRecommend = !reader.IsDBNull(reader.GetOrdinal("IsRecommend")) ? reader["IsRecommend"].ToString() == "0" ? false : true : false;
                    model.ProvinceId = !reader.IsDBNull(reader.GetOrdinal("ProvinceId")) ? (int)reader["ProvinceId"] : 0;
                    model.SourceId = !reader.IsDBNull(reader.GetOrdinal("CountyId")) ? (string)reader["SourceId"] : string.Empty;
                    model.SourceName = !reader.IsDBNull(reader.GetOrdinal("SourceName")) ? (string)reader["SourceName"] : string.Empty;
                    model.SueEndDate = !reader.IsDBNull(reader.GetOrdinal("SueEndDate")) ? (DateTime?)((DateTime)reader["SueEndDate"]) : null;
                    model.SueStartDate = !reader.IsDBNull(reader.GetOrdinal("SueStartDate")) ? (DateTime?)((DateTime)reader["SueStartDate"]) : null;
                    model.TypeName = !reader.IsDBNull(reader.GetOrdinal("TypeName")) ? (string)reader["TypeName"] : string.Empty;
                    model.UsedNum = !reader.IsDBNull(reader.GetOrdinal("UsedNum")) ? (int)reader["UsedNum"] : 0;
                    model.CompanyId = searchModel.CompanyId;
                    model.ContactFax = !reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? (string)reader["ContactFax"] : string.Empty;
                    model.ContactName = !reader.IsDBNull(reader.GetOrdinal("ContactName")) ? (string)reader["ContactName"] : string.Empty;
                    model.ContactTel = !reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? (string)reader["ContactTel"] : string.Empty;
                    model.ControlNum = !reader.IsDBNull(reader.GetOrdinal("ControlNum")) ? (int)reader["ControlNum"] : 0;
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得预控变更记录
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSourceSueHotelChange> GetSourceSueChangeList(string sueId, Model.EnumType.SourceStructure.SourceControlCategory Type)
        {
            IList<Model.SourceStructure.MSourceSueHotelChange> list = new List<Model.SourceStructure.MSourceSueHotelChange>();
            string sql = string.Format("select * from tbl_SourceSueHotelChange where SueId='{0}' and Type={1} order by IssueTime desc", sueId, (int)Type);
            DbCommand dc = _db.GetSqlStringCommand(sql);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceSueHotelChange model = new Model.SourceStructure.MSourceSueHotelChange();
                    model.Cut = (int)reader["Cut"];
                    model.IssueTime = (DateTime)reader["IssueTime"];
                    model.OperatorId = (string)reader["OperatorId"];
                    model.Operator = reader["Operator"].ToString();
                    model.Plus = (int)reader["Plus"];
                    model.SueId = sueId;
                    model.Remark = reader["Remark"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得预控确认单Model
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <param name="Type">与空类型</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceSueSourceQRD GetSueQRDModel(string sueId, SourceControlCategory Type)
        {
            string sql = string.Empty;
            switch (Type)
            {
                case SourceControlCategory.酒店:
                    sql = string.Format("select HotelName SourceName,RoomType TypeName,TotalPrice,ControlNum,Advance,PeriodStartTime,PeriodEndTime,a.Remark,a.UnitPrice from tbl_SourceSueHotel a  where a.SueId='{0}' ", sueId);
                    break;
                case SourceControlCategory.车辆:
                    sql = string.Format("select CarName SourceName,TypeName TypeName,TotalPrice,TotalPrice Advance,ControlNum,SueStartTime LastTime,SueEndTime PeriodEndTime,a.Remark,a.UnitPrice from tbl_SourceSueCar a  where a.SueId='{0}' ", sueId);
                    break;
                case SourceControlCategory.游轮:
                    sql = string.Format("select ShipCompany SourceName,ShipName TypeName,TotalPrice,Advance,ControlNum,GoShipTime PeriodStartTime,LastTime PeriodEndTime,a.Remark,a.UnitPrice from LastTime a  where a.SueId='{0}' ", sueId);
                    break;
            }
            DbCommand dc = _db.GetSqlStringCommand(sql);
            Model.SourceStructure.MSourceSueSourceQRD model = new Model.SourceStructure.MSourceSueSourceQRD();
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {

                while (reader.Read())
                {
                    model.AdvancePrice = (decimal)reader["Advance"];
                    model.ControlNum = (int)reader["ControlNum"];
                    model.ControlTime = (DateTime)reader["PeriodStartTime"];
                    model.LastTime = (DateTime)reader["PeriodEndTime"];
                    model.Remark = !reader.IsDBNull(reader.GetOrdinal("Remark")) ? (string)reader["Remark"] : string.Empty;
                    model.SourceName = !reader.IsDBNull(reader.GetOrdinal("SourceName")) ? (string)reader["SourceName"] : string.Empty;
                    model.SueId = sueId;
                    model.UnitPrice = (decimal)reader["UnitPrice"];
                    model.TotalPrice = (decimal)reader["TotalPrice"];
                    model.TypeName = !reader.IsDBNull(reader.GetOrdinal("TypeName")) ? (string)reader["TypeName"] : string.Empty;
                }
            }
            return model;
        }

        #endregion

        #region 游轮预控
        /// <summary>
        /// 添加游轮预控
        /// </summary>
        /// <param name="item">游轮预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueShip(MSourceSueShip item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_SourceSueShip_Add");
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.Id);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, item.SourceId);
            this._db.AddInParameter(comm, "@shipName", DbType.String, item.ShipName);
            this._db.AddInParameter(comm, "@shipCompany", DbType.String, item.ShipCompany);
            this._db.AddInParameter(comm, "@SubId", DbType.AnsiStringFixedLength, item.SubId);
            this._db.AddInParameter(comm, "@controlNum", DbType.Int32, item.ControlNum);
            this._db.AddInParameter(comm, "@controlType", DbType.Byte, (int)item.SourceControlType);
            this._db.AddInParameter(comm, "@shareType", DbType.Byte, (int)item.ShareType);
            this._db.AddInParameter(comm, "@operatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            _db.AddInParameter(comm, "@remark", DbType.String, item.Remark);
            _db.AddInParameter(comm, "@unitprice", DbType.Currency, item.UnitPrice);
            _db.AddInParameter(comm, "@totalprice", DbType.Currency, item.TotalPrice);
            _db.AddInParameter(comm, "@goshipdate", DbType.DateTime, item.GoShipTime);
            _db.AddInParameter(comm, "@shiproute", DbType.String, item.ShipRoute);
            _db.AddInParameter(comm, "@shipspot", DbType.String, item.ShipSpot);
            _db.AddInParameter(comm, "@advance", DbType.String, item.Advance);
            _db.AddInParameter(comm, "@lasttime", DbType.DateTime, item.LastTime);
            _db.AddInParameter(comm, "@deptid", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, StructureSourceXml(SourceControlCategory.游轮, item));

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取游轮预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>游轮预控信息</returns>
        public MSourceSueShip GetModelByShipId(string id, string companyId)
        {
            string sql = "SELECT * FROM view_SourceSueShip WHERE Id = @id AND CompanyId =@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MSourceSueShip item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MSourceSueShip()
                    {
                        Id = reader["Id"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SubId = reader["SubId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? string.Empty : reader["ShipName"].ToString(),
                        AlreadyNum = reader.IsDBNull(reader.GetOrdinal("AlreadyNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                        CompanyId = reader["CompanyId"].ToString(),
                        ControlNum = reader.IsDBNull(reader.GetOrdinal("ControlNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("ControlNum")),
                        ShipName = reader["ShipName"].ToString(),
                        GoShipTime = reader.IsDBNull(reader.GetOrdinal("GoShipTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("GoShipTime")),
                        ShipCompany = reader["ShipCompany"].ToString(),
                        LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("LastTime")),
                        UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                        Advance = reader.IsDBNull(reader.GetOrdinal("Advance")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Advance")),
                        Remark = reader["Remark"].ToString()
                    };
                }
            }

            return item;
        }

        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>已使用列表</returns>
        public IList<MSueUse> GetShipUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append("         TourId ,");
            field.Append("         TourCode ,");
            field.Append("         RouteName ,");
            field.Append("         USED = ( SELECT COUNT(*)");
            field.Append("                  FROM   tbl_Plan");
            field.Append("                  WHERE  TourId = tbl_Tour.TourId");
            field.Append("                         AND IsDelete = 0");
            field.AppendFormat("                         AND SueId = '{0}'", sueId);
            field.Append("                )");

            query.AppendFormat("         tbl_Tour.CompanyId = '{0}'", companyId);
            //query.AppendFormat("         AND TourStatus < {0}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消);
            //query.Append("         AND tbl_Tour.IsDelete = 0");
            query.Append("         AND ISNULL(ParentId, '') <> ''");
            query.Append("         AND EXISTS ( SELECT 1");
            query.Append("                      FROM   tbl_Plan");
            query.Append("                      WHERE  TourId = tbl_Tour.TourId");
            query.Append("                             AND IsDelete = 0");
            query.AppendFormat("                             AND SueId = '{0}' )", sueId);

            //StringBuilder sbWhere = new StringBuilder();

            //sbWhere.AppendFormat(" [Type] in ({0},{1}) and CompanyId ='{2}' and SueId='{3}' ", (int)PlanProject.国内游轮, (int)PlanProject.涉外游轮, companyId, sueId);


            IList<MSueUse> list = new List<MSueUse>();
            MSueUse item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_Tour", "TourId", field.ToString(), query.ToString(), "LDate"))
            {
                while (reader.Read())
                {
                    item = new MSueUse()
                        {
                            TourCode = reader["TourCode"].ToString(),
                            RouteName = reader["RouteName"].ToString(),
                            //LDate = DateTime.Parse(reader["LDate"].ToString()),
                            //SellerName = reader["SellerName"].ToString(),
                            //PlanName = reader["OperatorName"].ToString(),
                            Count = (int)reader["Used"],
                            //RouteId = (string)reader["RouteId"],
                            //TourId = (string)reader["TourId"],
                            TravellerList = tourOrderDal.GetTourOrderTravellerByTourId(reader["TourId"].ToString()),
                        };
                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendFormat("select tourid,replace(replace((select orderid,Data from (select a.tourid,a.orderid,b.Data from tbl_tourorder a left join ( select orderid,(select CnName,TravellerId,Contact from tbl_TourOrderTraveller where OrderId=a.OrderId for xml path,elements,root('item')) Data from tbl_TourOrderTraveller a group by a.orderid ) b on a.orderid=b.orderid) a where a.tourid=b.tourid for xml path,elements,root('item')),'&lt;','<'),'&gt;','>') Data from (select a.tourid,a.orderid,b.Data from tbl_tourorder a left join (select orderid,(select CnName,TravellerId,Contact from tbl_TourOrderTraveller where OrderId=a.OrderId for xml path,elements,root('item')) Data from tbl_TourOrderTraveller a group by a.orderid) b on a.orderid=b.orderid) b group by b.tourid where tourid='{0}' ", item.TourId);
                    //DbCommand gDc = _db.GetSqlStringCommand(sb.ToString());
                    //XmlDocument xd = new XmlDocument();
                    //using (IDataReader gReader = DbHelper.ExecuteReader(gDc, _db))
                    //{
                    //    xd = new XmlDocument();
                    //    while (gReader.Read())
                    //    {
                    //        xd = new XmlDocument();
                    //        if (!reader.IsDBNull(reader.GetOrdinal("Data")))
                    //        {
                    //            xd.LoadXml((string)reader["Data"]);
                    //        }

                    //        for (int i = 0; i < xd.DocumentElement.ChildNodes.Count; i++)
                    //        {
                    //            XmlElement xe = (XmlElement)xd.DocumentElement.ChildNodes[i];
                    //            XmlDocument subxd = new XmlDocument();
                    //            if (xe.ChildNodes.Count > 1)
                    //            {
                    //                subxd.LoadXml(xe.ChildNodes[1].FirstChild.Value);
                    //            }
                    //            for (int j = 0; i < subxd.DocumentElement.ChildNodes.Count; j++)
                    //            {
                    //                XmlElement subxe = (XmlElement)subxd.DocumentElement.ChildNodes[0];
                    //                item.TravellerList.Add(tourOrderDal.GetTourOrderTravellerById(subxe.FirstChild.Value));
                    //            }
                    //        }
                    //    }
                    //}
                    list.Add(item);
                }
            }

            return list;
        }
        /// <summary>
        /// 分页获取游轮预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, MSourceSueShipSearch search)
        {
            string tableName = "view_SourceSueShip";
            string primaryKey = "Id";
            string fields = "*";
            string orderBy = "ControlNum DESC";

            #region 构造查询条件
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}'", search.CompanyId);
            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" AND OperatorId ='{0}'", search.OperatorId);
            }
            if (search.IsTranTip)
            {
                query.Append(" AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0 ");
                if (search.EarlyDays != 0)
                {
                    query.AppendFormat(" AND datediff(day,getdate(),LastTime)<={0}", search.EarlyDays);
                }
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                query.AppendFormat(" AND ShipName like '%{0}%'", search.SourceName);
            }
            if (!string.IsNullOrEmpty(search.ShipCompany))
            {
                query.AppendFormat(" AND ShipCompany like '%{0}%'", search.ShipCompany);
            }
            if (search.StartTime != null)
            {
                query.AppendFormat(" and GoShipTime>='{0}' ", search.StartTime.ToString());
            }
            if (search.EndTime != null)
            {
                query.AppendFormat(" and GoShipTime<='{0}' ", search.EndTime.ToString());
            }
            //if (search.RoomType != null)
            //{
            //    query.AppendFormat(" AND RoomType = {0}", (int)search.RoomType);
            //}
            //if (search.ProvinceId != null)
            //{
            //    query.AppendFormat(" AND ProvinceId = {0}", search.ProvinceId);
            //}
            //if (search.CityId != null)
            //{
            //    query.AppendFormat(" AND CityId = {0}", search.CityId);
            //}
            #endregion

            IList<MSourceSueShip> list = new List<MSourceSueShip>();
            MSourceSueShip item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fields, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MSourceSueShip()
                        {
                            Id = reader["Id"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SubId = reader["SubId"].ToString(),
                            ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? string.Empty : reader["ShipName"].ToString(),
                            ShipCompany = reader.IsDBNull(reader.GetOrdinal("ShipCompany")) ? string.Empty : reader["ShipCompany"].ToString(),
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader["LastTime"],
                            ShipRoute = reader.IsDBNull(reader.GetOrdinal("ShipRoute")) ? string.Empty : (string)reader["ShipRoute"],
                            ShipSpot = reader.IsDBNull(reader.GetOrdinal("ShipSpot")) ? string.Empty : (string)reader["ShipSpot"],
                            GoShipTime = reader.IsDBNull(reader.GetOrdinal("GoShipTime")) ? null : (DateTime?)reader["GoShipTime"],
                            ControlNum = (int)reader["ControlNum"],
                            AlreadyNum = (int)reader["AlreadyNum"]

                        });
                }
            }
            return list;
        }
        /// <summary>
        /// 分页获取游轮预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, string tourid,
            string operatorId, MSourceSueShipSearch search)
        {
            string tableName = "view_SourceSueShip";
            string primaryKey = "Id";
            string fields = "*";
            string orderBy = "ID";

            #region 构造查询实体
            StringBuilder where = new StringBuilder();
            //总控-共享
            where.Append("((ControlType = 2 AND ShareType = 1) 	");
            //总控-不共享-用房人
            where.AppendFormat(" or (ControlType = 2 AND ShareType = 2 AND exists(select SueId from tbl_SourceSueOperator where [type] = 3 AND OperatorId = '{0}'))", operatorId);
            //单控-团号
            where.AppendFormat(" or (ControlType = 1 AND exists(select SueId from tbl_SourceSueTour where [type] = 3 AND TourId = '{0}')))", tourid);
            //剩余数量大于0
            where.AppendFormat(" and ControlNum-AlreadyNum > 0 and CompanyId = '{0}'", search.CompanyId);
            if (search.DistrictId != null && search.DistrictId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CountyId = {0} and SourceId=view_SourceSueShip.SourceId)", search.DistrictId);
            }
            else if (search.CityId != null && search.CityId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CityId = {0} and SourceId=view_SourceSueShip.SourceId)", search.CityId);
            }
            else if (search.ProvinceId != null && search.ProvinceId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where ProvinceId = {0} and SourceId=view_SourceSueShip.SourceId)", search.ProvinceId);
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                where.AppendFormat(" AND ShipName like '%{0}%'", search.SourceName);
            }
            #endregion

            IList<MSourceSueShip> list = new List<MSourceSueShip>();
            MSourceSueShip item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fields, where.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MSourceSueShip()
                        {
                            Id = reader["Id"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SubId = reader["SubId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? string.Empty : reader["ShipName"].ToString(),
                            AlreadyNum = reader.IsDBNull(reader.GetOrdinal("AlreadyNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                            CompanyId = reader["CompanyId"].ToString(),
                            ControlNum = reader.IsDBNull(reader.GetOrdinal("ControlNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("ControlNum")),
                            ShipName = reader["ShipName"].ToString(),
                            GoShipTime = reader.IsDBNull(reader.GetOrdinal("GoShipTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("GoShipTime")),
                            ShipCompany = reader["ShipCompany"].ToString(),
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("LastTime"))
                        });
                }
            }
            return list;
        }
        #endregion

        #region 景点预控
        /// <summary>
        /// 添加景点预控
        /// </summary>
        /// <param name="item">景点预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueSight(MSourceSueSight item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_SourceSueSight_Add");
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.Id);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, item.SourceId);
            this._db.AddInParameter(comm, "@SightName", DbType.String, item.SourceName);
            this._db.AddInParameter(comm, "@contorlNum", DbType.Int32, item.ControlNum);
            this._db.AddInParameter(comm, "@SpotId", DbType.AnsiStringFixedLength, item.SpotId);
            this._db.AddInParameter(comm, "@SpotName", DbType.String, item.SpotName);
            this._db.AddInParameter(comm, "@controlType", DbType.Byte, (int)item.SourceControlType);
            this._db.AddInParameter(comm, "@shareType", DbType.Byte, (int)item.ShareType);
            this._db.AddInParameter(comm, "@operatorId", DbType.String, item.OperatorId);
            _db.AddInParameter(comm, "@remark", DbType.String, item.Remark);
            _db.AddInParameter(comm, "@unitprice", DbType.Currency, item.UnitPrice);
            _db.AddInParameter(comm, "@totalprice", DbType.Currency, item.TotalPrice);
            _db.AddInParameter(comm, "@SueStartTime", DbType.DateTime, item.SueStartTime);
            _db.AddInParameter(comm, "@SueEndTime", DbType.DateTime, item.SueEndTime);
            _db.AddInParameter(comm, "@lastTime", DbType.DateTime, item.LastTime);
            _db.AddInParameter(comm, "@deptid", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, StructureSourceXml(SourceControlCategory.景点, item));

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 分页获取景点预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueSight> GetListSueSight(int pageCurrent, int pageSize, ref int pageCount, MSourceSueSightSearch search)
        {
            string tableName = "view_SourceSueSight";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ControlNum DESC";
            #region 构造查询条件
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}'", search.CompanyId);
            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" AND OperatorId ='{0}'", search.OperatorId);
            }
            if (search.IsTranTip)
            {
                query.Append(" AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0 ");
                if (search.EarlyDays != 0)
                {
                    query.AppendFormat(" AND datediff(day,getdate(),LastTime)<={0}", search.EarlyDays);
                }
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                query.AppendFormat(" AND SightName like '%{0}%'", search.SourceName);
            }
            if (!string.IsNullOrEmpty(search.SpotName))
            {
                query.AppendFormat(" AND SpotName like'%{0}%'", search.SpotName);
            }
            if (search.StartTime != null)
            {
                query.AppendFormat(" AND SueStartTime >= '{0}'", search.StartTime);
            }
            if (search.EndTime != null)
            {
                query.AppendFormat(" AND SueEndTime <= '{0}'", search.EndTime);
            }
            #endregion

            IList<MSourceSueSight> list = new List<MSourceSueSight>();
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add( new MSourceSueSight()
                        {
                            Id = reader["Id"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("SightName")) ? string.Empty : reader["SightName"].ToString(),
                            SpotId = reader["SpotId"].ToString(),
                            SpotName = reader["SpotName"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            SueStartTime = DateTime.Parse(reader["SueStartTime"].ToString()),
                            SueEndTime = DateTime.Parse(reader["SueEndTime"].ToString()),
                            LastTime = DateTime.Parse(reader["LastTime"].ToString()),
                            AlreadyNum = (int)reader["AlreadyNum"],
                            ShareType = (ShareType)reader.GetByte(reader.GetOrdinal("ShareType")),
                            SourceControlType = (SourceControlType)reader.GetByte(reader.GetOrdinal("ControlType"))
                        });
                }
            }

            return list;
        }

        /// <summary>
        /// 获取景点预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景点预控信息</returns>
        public MSourceSueSight GetModelBySightId(string id, string companyId)
        {
            string sql = "SELECT * FROM view_SourceSueSight WHERE Id = @id AND CompanyId =@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MSourceSueSight item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MSourceSueSight()
                        {
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName =
                                reader.IsDBNull(reader.GetOrdinal("SightName"))
                                    ? string.Empty
                                    : reader["SightName"].ToString(),
                            SpotId = reader["SpotId"].ToString(),
                            SpotName = 
                                reader.IsDBNull(reader.GetOrdinal("SpotName"))
                                    ? string.Empty
                                    : reader["SpotName"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            AlreadyNum =
                                reader.IsDBNull(reader.GetOrdinal("AlreadyNum"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                            SueStartTime =
                                reader.IsDBNull(reader.GetOrdinal("SueStartTime"))
                                    ? null
                                    : (DateTime?)reader["SueStartTime"],
                            SueEndTime =
                                reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? null : (DateTime?)reader["SueEndTime"],
                            LastTime =
                                reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader["LastTime"],
                            UnitPrice =
                                reader.IsDBNull(reader.GetOrdinal("UnitPrice"))
                                    ? 0
                                    : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            TotalPrice =
                                reader.IsDBNull(reader.GetOrdinal("TotalPrice"))
                                    ? 0
                                    : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            Remark = reader["Remark"].ToString()
                        };
                }
            }

            return item;
        }
        /// <summary>
        /// 分页获取景点预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>景点集合</returns>
        public IList<MSourceSueSight> GetListSueSight(int pageCurrent, int pageSize, ref int pageCount,string tourid, string operatorId, MSourceSueSightSearch search)
        {
            string tableName = "view_SourceSueSight";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ID";

            #region 构造查询实体
            StringBuilder where = new StringBuilder();
            //总控-共享
            where.Append(" ((ControlType = 2 AND ShareType = 1)");
            //总控-不共享-用房人
            where.AppendFormat(" or (ControlType = 2 AND ShareType = 2 and exists(select 1 from tbl_SourceSueOperator where [type] = 1 AND OperatorId = '{0}'))", operatorId);
            //单控-团号
            where.AppendFormat(" or (ControlType = 1 AND exists(select 1 from tbl_SourceSueTour where [type] = 1 AND TourId = '{0}')))", tourid);
            //剩余数量大于0
            where.AppendFormat(" and ControlNum - AlreadyNum > 0 AND CompanyId = '{0}'", search.CompanyId);
            if (search.DistrictId != null && search.DistrictId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CountyId = {0} and SourceId=view_SourceSueSight.SourceId)", search.DistrictId);
            }
            else if (search.CityId != null && search.CityId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CityId = {0} and SourceId=view_SourceSueSight.SourceId)", search.CityId);
            }
            else if (search.ProvinceId != null && search.ProvinceId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where ProvinceId = {0} and SourceId=view_SourceSueSight.SourceId)", search.ProvinceId);
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                where.AppendFormat(" AND SightName like '%{0}%'", search.SourceName);
            }
            #endregion

            var list = new List<MSourceSueSight>();
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, where.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(new MSourceSueSight()
                        {
                            Id = reader["Id"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("SightName")) ? string.Empty : reader["SightName"].ToString(),
                            SpotName = reader["SpotName"].ToString(),
                            SpotId = reader["SpotId"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            AlreadyNum = (int)reader["AlreadyNum"],
                            LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LastTime")),
                            SueStartTime = reader.IsDBNull(reader.GetOrdinal("SueStartTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SueStartTime")),
                            SueEndTime = reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SueEndTime"))
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<MSueUse> GetSightUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" Type = {0} and CompanyId ='{1}' and SueId ='{2}'", (int)PlanProject.景点, companyId, sueId);
            IList<MSueUse> list = new List<MSueUse>();
            MSueUse item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_SueCarUse", "PlanId", "*", sql.ToString(), "IssueTime desc"))
            {
                while (reader.Read())
                {
                    item = new MSueUse()
                    {
                        TourCode = reader["TourCode"].ToString(),
                        RouteName = reader["RouteName"].ToString(),
                        Count = (int)reader["AlreadyNum"],
                        RouteId = (string)reader["RouteId"],
                        GuideList = sourceDal.GetGuideLst(reader["TourId"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 其他预控
        /// <summary>
        /// 添加其他预控
        /// </summary>
        /// <param name="item">其他预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueOther(MSourceSueOther item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_SourceSueOther_Add");
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.Id);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, item.SourceId);
            this._db.AddInParameter(comm, "@OtherName", DbType.String, item.SourceName);
            this._db.AddInParameter(comm, "@contorlNum", DbType.Int32, item.ControlNum);
            this._db.AddInParameter(comm, "@TypeId", DbType.AnsiStringFixedLength, item.TypeId);
            this._db.AddInParameter(comm, "@TypeName", DbType.String, item.TypeName);
            this._db.AddInParameter(comm, "@controlType", DbType.Byte, (int)item.SourceControlType);
            this._db.AddInParameter(comm, "@shareType", DbType.Byte, (int)item.ShareType);
            this._db.AddInParameter(comm, "@operatorId", DbType.String, item.OperatorId);
            _db.AddInParameter(comm, "@remark", DbType.String, item.Remark);
            _db.AddInParameter(comm, "@unitprice", DbType.Currency, item.UnitPrice);
            _db.AddInParameter(comm, "@totalprice", DbType.Currency, item.TotalPrice);
            _db.AddInParameter(comm, "@SueStartTime", DbType.DateTime, item.SueStartTime);
            _db.AddInParameter(comm, "@SueEndTime", DbType.DateTime, item.SueEndTime);
            _db.AddInParameter(comm, "@lastTime", DbType.DateTime, item.LastTime);
            _db.AddInParameter(comm, "@deptid", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@xml", DbType.Xml, StructureSourceXml(SourceControlCategory.其他, item));

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 分页获取其他预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueOther> GetListSueOther(int pageCurrent, int pageSize, ref int pageCount, MSourceSueOtherSearch search)
        {
            string tableName = "view_SourceSueOther";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ControlNum DESC";
            #region 构造查询条件
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}'", search.CompanyId);
            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" AND OperatorId ='{0}'", search.OperatorId);
            }
            if (search.IsTranTip)
            {
                query.Append(" AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0 ");
                if (search.EarlyDays != 0)
                {
                    query.AppendFormat(" AND datediff(day,getdate(),LastTime)<={0}", search.EarlyDays);
                }
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                query.AppendFormat(" AND OtherName like '%{0}%'", search.SourceName);
            }
            if (!string.IsNullOrEmpty(search.TypeName))
            {
                query.AppendFormat(" AND TypeName like'%{0}%'", search.TypeName);
            }
            if (search.StartTime != null)
            {
                query.AppendFormat(" AND SueStartTime >= '{0}'", search.StartTime);
            }
            if (search.EndTime != null)
            {
                query.AppendFormat(" AND SueEndTime <= '{0}'", search.EndTime);
            }
            #endregion

            IList<MSourceSueOther> list = new List<MSourceSueOther>();
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(new MSourceSueOther()
                        {
                            Id = reader["Id"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            SourceId = reader["SourceId"].ToString(),
                            SourceName = reader.IsDBNull(reader.GetOrdinal("OtherName")) ? string.Empty : reader["OtherName"].ToString(),
                            TypeId = reader["TypeId"].ToString(),
                            TypeName = reader["TypeName"].ToString(),
                            ControlNum = (int)reader["ControlNum"],
                            SueStartTime = DateTime.Parse(reader["SueStartTime"].ToString()),
                            SueEndTime = DateTime.Parse(reader["SueEndTime"].ToString()),
                            LastTime = DateTime.Parse(reader["LastTime"].ToString()),
                            AlreadyNum = (int)reader["AlreadyNum"],
                            ShareType = (ShareType)reader.GetByte(reader.GetOrdinal("ShareType")),
                            SourceControlType = (SourceControlType)reader.GetByte(reader.GetOrdinal("ControlType"))
                        });
                }
            }

            return list;
        }

        /// <summary>
        /// 获取其他预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>其他预控信息</returns>
        public MSourceSueOther GetModelByOtherId(string id, string companyId)
        {
            string sql = "SELECT * FROM view_SourceSueOther WHERE Id = @id AND CompanyId =@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MSourceSueOther item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MSourceSueOther()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName =
                            reader.IsDBNull(reader.GetOrdinal("OtherName"))
                                ? string.Empty
                                : reader["OtherName"].ToString(),
                        TypeId = reader["TypeId"].ToString(),
                        TypeName = 
                            reader.IsDBNull(reader.GetOrdinal("TypeName"))
                                ? string.Empty
                                : reader["TypeName"].ToString(),
                        ControlNum = (int)reader["ControlNum"],
                        AlreadyNum =
                            reader.IsDBNull(reader.GetOrdinal("AlreadyNum"))
                                ? 0
                                : reader.GetInt32(reader.GetOrdinal("AlreadyNum")),
                        SueStartTime =
                            reader.IsDBNull(reader.GetOrdinal("SueStartTime"))
                                ? null
                                : (DateTime?)reader["SueStartTime"],
                        SueEndTime =
                            reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? null : (DateTime?)reader["SueEndTime"],
                        LastTime =
                            reader.IsDBNull(reader.GetOrdinal("LastTime")) ? null : (DateTime?)reader["LastTime"],
                        UnitPrice =
                            reader.IsDBNull(reader.GetOrdinal("UnitPrice"))
                                ? 0
                                : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                        TotalPrice =
                            reader.IsDBNull(reader.GetOrdinal("TotalPrice"))
                                ? 0
                                : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                        Remark = reader["Remark"].ToString()
                    };
                }
            }

            return item;
        }
        /// <summary>
        /// 分页获取其他预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>其他集合</returns>
        public IList<MSourceSueOther> GetListSueOther(int pageCurrent, int pageSize, ref int pageCount, string tourid, string operatorId, MSourceSueOtherSearch search)
        {
            string tableName = "view_SourceSueOther";
            string primaryKey = "Id";
            string fileds = "*";
            string orderBy = "ID";

            #region 构造查询实体
            StringBuilder where = new StringBuilder();
            //总控-共享
            where.Append(" ((ControlType = 2 AND ShareType = 1)");
            //总控-不共享-用房人
            where.AppendFormat(" or (ControlType = 2 AND ShareType = 2 and exists(select 1 from tbl_SourceSueOperator where [type] = 1 AND OperatorId = '{0}'))", operatorId);
            //单控-团号
            where.AppendFormat(" or (ControlType = 1 AND exists(select 1 from tbl_SourceSueTour where [type] = 1 AND TourId = '{0}')))", tourid);
            //剩余数量大于0
            where.AppendFormat(" and ControlNum - AlreadyNum > 0 AND CompanyId = '{0}'", search.CompanyId);
            if (search.DistrictId != null && search.DistrictId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CountyId = {0} and SourceId=view_SourceSueOther.SourceId)", search.DistrictId);
            }
            else if (search.CityId != null && search.CityId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where CityId = {0} and SourceId=view_SourceSueOther.SourceId)", search.CityId);
            }
            else if (search.ProvinceId != null && search.ProvinceId != 0)
            {
                where.AppendFormat(" AND exists(select 1 from tbl_Source where ProvinceId = {0} and SourceId=view_SourceSueOther.SourceId)", search.ProvinceId);
            }
            if (!string.IsNullOrEmpty(search.SourceName))
            {
                where.AppendFormat(" AND OtherName like '%{0}%'", search.SourceName);
            }
            #endregion

            var list = new List<MSourceSueOther>();
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, where.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(new MSourceSueOther()
                    {
                        Id = reader["Id"].ToString(),
                        SourceId = reader["SourceId"].ToString(),
                        SourceName = reader.IsDBNull(reader.GetOrdinal("OtherName")) ? string.Empty : reader["OtherName"].ToString(),
                        TypeName = reader["TypeName"].ToString(),
                        TypeId = reader["TypeId"].ToString(),
                        ControlNum = (int)reader["ControlNum"],
                        AlreadyNum = (int)reader["AlreadyNum"],
                        LastTime = reader.IsDBNull(reader.GetOrdinal("LastTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LastTime")),
                        SueStartTime = reader.IsDBNull(reader.GetOrdinal("SueStartTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SueStartTime")),
                        SueEndTime = reader.IsDBNull(reader.GetOrdinal("SueEndTime")) ? System.DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SueEndTime"))
                    });
                }
            }

            return list;
        }
        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<MSueUse> GetOtherUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" Type = {0} and CompanyId ='{1}' and SueId ='{2}'", (int)PlanProject.其它, companyId, sueId);
            IList<MSueUse> list = new List<MSueUse>();
            MSueUse item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_SueCarUse", "PlanId", "*", sql.ToString(), "IssueTime desc"))
            {
                while (reader.Read())
                {
                    item = new MSueUse()
                    {
                        TourCode = reader["TourCode"].ToString(),
                        RouteName = reader["RouteName"].ToString(),
                        Count = (int)reader["AlreadyNum"],
                        RouteId = (string)reader["RouteId"],
                        GuideList = sourceDal.GetGuideLst(reader["TourId"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #endregion

        /// <summary>
        /// 查询预控剩余数量
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <param name="category">预控类别</param>
        /// <returns>预控剩余数量</returns>
        public int SueSurplusNum(string sourceId, SourceControlCategory category)
        {
            string tableName = category == SourceControlCategory.车辆 ? "view_SourceSueCar" : (category == SourceControlCategory.酒店 ? "view_SourceSueHotel" : "view_SourceSueShip");
            string sql = string.Format("SELECT ControlNum-AlreadyNum FROM {0} WHERE SourceId = @sourceId", tableName);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@sourceId", DbType.AnsiStringFixedLength, sourceId);

            int surplus = 0;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    surplus = (int)reader[0];
                }
            }

            return surplus;
        }

        #region 私有函数

        /// <summary>
        /// 构造添加单控团号或总控操作人的xml
        /// </summary>
        /// <param name="category">预控类别</param>
        /// <param name="sc">预控数据</param>
        /// <returns>xml格式文本</returns>
        private string StructureSourceXml(SourceControlCategory category, MSourceControl sc)
        {
            StringBuilder xmlText = new StringBuilder("<item>");
            switch (sc.SourceControlType)
            {
                case SourceControlType.单控:
                    foreach (MSourceSueTour item in sc.TourNoList)
                    {
                        xmlText.AppendFormat("<tour type='{0}'>", (int)category);
                        xmlText.AppendFormat("<sueid>{0}</sueid>", sc.Id);
                        xmlText.AppendFormat("<tourid>{0}</tourid>", item.TourId);
                        xmlText.Append("</tour>");
                    }
                    break;
                case SourceControlType.总控:
                    //总控-共享方式不处理
                    if (sc.ShareType == ShareType.不共享)
                    {
                        foreach (MSourceSueOperator item in sc.OperatorList)
                        {
                            xmlText.AppendFormat("<operator type='{0}'>", (int)category);
                            xmlText.AppendFormat("<sueid>{0}</sueid>", sc.Id);
                            xmlText.AppendFormat("<operatorid>{0}</operatorid>", item.OperatorId);
                            xmlText.Append("</operator>");
                        }
                    }
                    break;
            }
            xmlText.Append("</item>");
            return xmlText.ToString();
        }


        #endregion

    }
}
