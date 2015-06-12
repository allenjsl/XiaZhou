//供应商相关数据访问类 汪奇志 2013-04-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.IDAL.GysStructure;
using EyouSoft.Toolkit;
using System.Xml.Linq;

namespace EyouSoft.DAL.GysStructure
{
    /// <summary>
    /// 供应商相关数据访问类
    /// </summary>
    public class DGys : DALBase, IGys
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetGysLxrs = "SELECT * FROM tbl_CrmLinkman WHERE TypeId=@GysId AND [Type]=@LxrType ORDER BY SortId ASC";
        const string SQL_SELECT_GetGysUserInfo = "SELECT UserId,Username,UserStatus FROM tbl_ComUser WHERE UserId=@UserId AND IsDelete='0'";
        const string SQL_SELECT_GetJiuDianFangXings = "SELECT * FROM tbl_SourceHotelRoom WHERE SourceId=@GysId ORDER BY SortId ASC";
        const string SQL_SELECT_GetJingDianJingDians = "SELECT * FROM tbl_SourceSpotPriceSystem WHERE SourceId=@GysId ORDER BY SortId ASC";
        const string SQL_SELECT_GetYouLunYouChuans = "SELECT * FROM tbl_SourceSubShip WHERE SourceId=@GysId";
        const string SQL_SELECT_GetGysLeiXing = "SELECT [Type] FROM tbl_Source WHERE SourceId=@GysId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DGys()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// 获取供应商联系人信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLxrInfo> GetGysLxrs(string gysId)
        {
            IList<EyouSoft.Model.GysStructure.MLxrInfo> items = new List<EyouSoft.Model.GysStructure.MLxrInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGysLxrs);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.供应商);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item =new EyouSoft.Model.GysStructure.MLxrInfo();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday"))) item.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    item.Email = rdr["EMail"].ToString();
                    item.Fax = rdr["Fax"].ToString();
                    item.LxrId = rdr["Id"].ToString();
                    item.Mobile = rdr["MobilePhone"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.Telephone = rdr["Telephone"].ToString();
                    item.ZhiWu = rdr["Post"].ToString();
                    
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商交易明细汇总信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MJiaoYiXXInfo GetJiaoYiXX(string gysId)
        {
            var info = new EyouSoft.Model.GysStructure.MJiaoYiXXInfo();
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_GetGysJiaoYiXX");
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.RunReaderProcedure(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.JiaoYiCiShu = rdr.GetInt32(rdr.GetOrdinal("JiaoYiCiShu"));
                    info.JiaoYiShuLiang = rdr.GetInt32(rdr.GetOrdinal("JiaoYiShuLiang"));
                    info.JieSuanJinE = rdr.GetDecimal(rdr.GetOrdinal("JieSuanJinE"));
                    info.YiZhiFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinE"));
                    info.ZhiJianJunFen = rdr.GetDecimal(rdr.GetOrdinal("ZhiJianJunFen"));
                    info.DJiaoYiShuLiang = rdr.GetDecimal(rdr.GetOrdinal("DJiaoYiShuLiang"));
                }
            }

            return info;
        }
        /// <summary>
        /// 初始化供应商用户信息业务实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MGysUserInfo GetGysUserInfo1(string userId)
        {
            var info= new EyouSoft.Model.GysStructure.MGysUserInfo();
            info.Username = string.Empty;
            info.UserId = string.Empty;
            info.Status = EyouSoft.Model.EnumType.ComStructure.UserStatus.未启用;

            if (string.IsNullOrEmpty(userId)) return info;

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGysUserInfo);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);

            using (IDataReader rdr= DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserId"));
                    info.Status = (EyouSoft.Model.EnumType.ComStructure.UserStatus)rdr.GetByte(rdr.GetOrdinal("UserStatus"));
                    info.Username = rdr["UserName"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取酒店房型信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MJiuDianFangXingInfo> GetJiuDianFangXings(string gysId)
        {
            IList<EyouSoft.Model.GysStructure.MJiuDianFangXingInfo> items = new List<EyouSoft.Model.GysStructure.MJiuDianFangXingInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiuDianFangXings);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MJiuDianFangXingInfo();
                    item.FangXingId = rdr.GetString(rdr.GetOrdinal("RoomId"));
                    item.FangXingName = rdr["TypeName"].ToString();
                    item.IsHanZao = rdr["IsBreakfast"].ToString() == "1";
                    item.JiaGeDJ = rdr.GetDecimal(rdr.GetOrdinal("PriceDJ"));
                    item.JiaGePJ = rdr.GetDecimal(rdr.GetOrdinal("PricePJ"));
                    item.JiaGeQT = rdr.GetDecimal(rdr.GetOrdinal("PriceQT"));
                    item.JiaGeSK = rdr.GetDecimal(rdr.GetOrdinal("PriceSK"));
                    item.JiaGeWJ = rdr.GetDecimal(rdr.GetOrdinal("PriceWJ"));
                    item.JiaGeWL = rdr.GetDecimal(rdr.GetOrdinal("PriceWL"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取景点景点信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MJingDianJingDianInfo> GetJingDianJingDians(string gysId)
        {
            IList<EyouSoft.Model.GysStructure.MJingDianJingDianInfo> items = new List<EyouSoft.Model.GysStructure.MJingDianJingDianInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJingDianJingDians);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MJingDianJingDianInfo();

                    item.DaoYouCi = rdr["GuideWord"].ToString();
                    item.JiaGeET = rdr.GetDecimal(rdr.GetOrdinal("PriceRT"));
                    item.JiaGeGP = rdr.GetDecimal(rdr.GetOrdinal("PriceGP"));
                    item.JiaGeJR = rdr.GetDecimal(rdr.GetOrdinal("PriceJR"));
                    item.JiaGeLR67 = rdr.GetDecimal(rdr.GetOrdinal("PriceLR1"));
                    item.JiaGeLR7 = rdr.GetDecimal(rdr.GetOrdinal("PriceLR2"));
                    item.JiaGeSK = rdr.GetDecimal(rdr.GetOrdinal("PriceSK"));
                    item.JiaGeTD = rdr.GetDecimal(rdr.GetOrdinal("PriceTD"));
                    item.JiaGeXS = rdr.GetDecimal(rdr.GetOrdinal("PriceXS"));
                    item.JingDianId = rdr.GetString(rdr.GetOrdinal("Id"));
                    item.Name = rdr["Name"].ToString();
                    item.XingJi = (EyouSoft.Model.EnumType.SourceStructure.SpotStar)rdr.GetByte(rdr.GetOrdinal("Star"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取游轮游船信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MYouLunYouChuanInfo> GetYouLunYouChuans(string gysId)
        {
            IList<EyouSoft.Model.GysStructure.MYouLunYouChuanInfo> items = new List<EyouSoft.Model.GysStructure.MYouLunYouChuanInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYouLunYouChuans);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MYouLunYouChuanInfo();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShipSpace"))) item.CangWeiShu = rdr.GetInt32(rdr.GetOrdinal("ShipSpace"));
                    item.FuJian = null;
                    item.HangXian = rdr["ShipRoute"].ToString();
                    item.LxrName = rdr["ContactName"].ToString();
                    item.Name = rdr["ShipName"].ToString();
                    item.Telephone = rdr["Telephone"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShipStar"))) item.XingJi = (EyouSoft.Model.EnumType.SourceStructure.ShipStar)rdr.GetByte(rdr.GetOrdinal("ShipStar"));
                    item.YouChuanId = rdr.GetString(rdr.GetOrdinal("SubId"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取选用的查询SQL
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        string GetXuanYongSQL(string companyId, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun)
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            if (chaXun != null)
            {
                if (chaXun.GysLeiXing.HasValue)
                {
                    s.AppendFormat(" AND Type={0} ", (int)chaXun.GysLeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    s.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.JingDianName))
                {
                    s.AppendFormat(" AND JingDianName LIKE '%{0}%' ", chaXun.JingDianName);
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    s.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (chaXun.CityId.HasValue)
                {
                    s.AppendFormat(" AND CityId={0} ", chaXun.CityId.Value);
                }
                if (chaXun.CountryId.HasValue)
                {
                    s.AppendFormat(" AND CountryId={0} ", chaXun.CountryId.Value);
                }
                if (chaXun.DistrictId.HasValue)
                {
                    s.AppendFormat(" AND CountyId={0} ", chaXun.DistrictId.Value);
                }
            }

            return s.ToString();
        }

        /// <summary>
        /// 获取供应商列表查询条件SQL
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysLeiXing">供应商类型</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        string GetGysLBSQL(string companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType gysLeiXing, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            s.AppendFormat(" AND Type={0} ", (int)gysLeiXing);
            if (isOnlySelf)
            {
                s.AppendFormat(" AND OperatorId='{0}' ", userId);
            }
            else if (depts != null && depts.Length > 0)
            {
                s.AppendFormat(" AND (DeptId IN({0}) OR OperatorId='{1}') ", Utils.GetSqlIn<int>(depts), userId);
            }
            if (chaXun != null)
            {
                if (chaXun.CityId.HasValue)
                {
                    s.AppendFormat(" AND CityId={0} ", chaXun.CityId.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    if (gysLeiXing != EyouSoft.Model.EnumType.SourceStructure.SourceType.景点)
                        s.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.GysName);
                    else
                        s.AppendFormat(" AND (Name LIKE '%{0}%' OR EXISTS (SELECT 1 FROM tbl_SourceSpotPriceSystem AS A WHERE A.SourceId=tbl_Source.SourceId AND A.Name LIKE '%{0}%') ) ", chaXun.GysName);
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    s.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (chaXun.JiuDianXingJi.HasValue)
                {
                    s.AppendFormat(" AND JiuDianXingJi={0} ", (int)chaXun.JiuDianXingJi.Value);
                }
                if (chaXun.CanGuanCaiXi.HasValue)
                {
                    s.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_SourceDiningCuisine AS A WHERE A.SourceId=tbl_Source.SourceId AND A.Cuisine={0}) ", (int)chaXun.CanGuanCaiXi.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.YouLunYouChuanName))
                {
                    s.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_SourceSubShip AS A WHERE A.SourceId=tbl_Source.SourceId AND A.ShipName LIKE '%{0}%') ", chaXun.YouLunYouChuanName);
                }
                if (!string.IsNullOrEmpty(chaXun.CheDuiCheXingName))
                {
                    s.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_SourceCar AS A WHERE A.SourceId=tbl_Source.SourceId AND A.TypeName LIKE '%{0}%') ", chaXun.CheDuiCheXingName);
                }
                if (!string.IsNullOrEmpty(chaXun.GysId))
                {
                    s.AppendFormat(" AND SourceId='{0}' ", chaXun.GysId);
                }
            }
            return s.ToString();
        }
        #endregion

        #region IGys 成员
        /// <summary>
        /// 获取供应商选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.GysStructure.MXuanYongInfo> items = new List<EyouSoft.Model.GysStructure.MXuanYongInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,Type,IsCommission,IsRecommend,IsPermission";

            if (chaXun != null && chaXun.GysLeiXing.HasValue && chaXun.GysLeiXing.Value == EyouSoft.Model.EnumType.SourceStructure.SourceType.酒店)
            {
                fields += ",(SELECT A1.ReceptionTel FROM tbl_SourceHotel AS A1 WHERE A1.SourceId=tbl_Source.SourceId) AS JiuDianQianTaiTelephone";
            }
            else
            {
                fields += ",'' AS JiuDianQianTaiTelephone";
            }

            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetXuanYongSQL(companyId, chaXun);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MXuanYongInfo();
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysLeiXing = (EyouSoft.Model.EnumType.SourceStructure.SourceType)rdr.GetByte(rdr.GetOrdinal("Type"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiuDianQianTaiTelephone = rdr["JiuDianQianTaiTelephone"].ToString();
                    items.Add(item);
                }
            }

            if (chaXun != null && chaXun.IsLxr && items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取景点选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.GysStructure.MXuanYongJingDianInfo> items = new List<EyouSoft.Model.GysStructure.MXuanYongJingDianInfo>();

            string tableName = "view_Gys_JingDianJingDian";
            string fields = "*";
            string orderByString = "SortId ASC";
            string sumString = string.Empty;
            string sql = GetXuanYongSQL(companyId, chaXun);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MXuanYongJingDianInfo();

                    item.JingDianId = rdr.GetString(rdr.GetOrdinal("JingDianId"));
                    item.MiaoShu = rdr["JingDianMiaoShu"].ToString();
                    item.Name = rdr["JingDianName"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商账号信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.GysStructure.MGysUserInfo GetGysUserInfo(string gysId)
        {
            EyouSoft.Model.GysStructure.MGysUserInfo info = null;
            var sql = new StringBuilder();

            sql.Append(" SELECT  B.UserName ,B.Password,B.UserId FROM dbo.tbl_Source AS A INNER JOIN tbl_ComUser AS B ON A.UserId = B.UserId WHERE A.SourceId = @GysId; ");
            sql.Append(" SELECT A.RouteAreaId,B.AreaName FROM tbl_TravelRouteArea AS A LEFT JOIN tbl_ComArea AS B ON A.RouteAreaId=B.AreaId AND B.IsDelete='0' WHERE A.SourceId=@GysId; ");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            using (var rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.GysStructure.MGysUserInfo();
                    info.Username = rdr["UserName"].ToString();
                    info.Pwd = rdr["Password"].ToString();
                    info.Areas = new List<EyouSoft.Model.GysStructure.MGysUserAreaInfo>();
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserId"));
                }

                if (info != null && rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        var item = new EyouSoft.Model.GysStructure.MGysUserAreaInfo();

                        item.AreaId = rdr.GetInt32(rdr.GetOrdinal("RouteAreaId"));
                        item.AreaName = rdr["AreaName"].ToString();

                        info.Areas.Add(item);
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// 获取供应商-地接社列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBDiJieSheInfo> GetDiJieShes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBDiJieSheInfo> items = new List<EyouSoft.Model.GysStructure.MLBDiJieSheInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.地接社, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBDiJieSheInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.UserInfo = new EyouSoft.Model.GysStructure.MGysUserInfo();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.UserInfo.UserId = rdr["UserId"].ToString().Trim();
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                    item.UserInfo = GetGysUserInfo1(item.UserInfo.UserId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-酒店列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBJiuDianInfo> GetJiuDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBJiuDianInfo> items = new List<EyouSoft.Model.GysStructure.MLBJiuDianInfo>();
            string tableName = "view_Gys_JiuDian";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId,JiuDianXingJi,QianTaiTelephone";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.酒店, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBJiuDianInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";                   
                    item.JiaoYiXX = null;
                    item.Lxrs = null;                    
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    item.FangXings = null;
                    item.XingJi = (EyouSoft.Model.EnumType.SourceStructure.HotelStar)rdr.GetByte(rdr.GetOrdinal("JiuDianXingJi"));
                    item.QianTaiTelephone = rdr["QianTaiTelephone"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                    item.FangXings = GetJiuDianFangXings(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-餐馆列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBCanGuanInfo> GetCanGuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBCanGuanInfo> items = new List<EyouSoft.Model.GysStructure.MLBCanGuanInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            fields += ",(SELECT A.DiningStandard FROM tbl_SourceDining AS A WHERE A.SourceId=tbl_Source.SourceId) AS CanBiao";
            fields += ",(SELECT A.Cuisine FROM tbl_SourceDiningCuisine AS A WHERE A.SourceId=tbl_Source.SourceId FOR XML RAW,ROOT('root')) AS CaiXiXML";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.餐馆, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBCanGuanInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    item.CanBiao = rdr["CanBiao"].ToString();
                    item.CaiXis = new List<EyouSoft.Model.EnumType.SourceStructure.SourceCuisine>();
                    string xml = rdr["CaiXiXML"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        var xroot = XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");
                        foreach (var xrow in xrows)
                        {
                            item.CaiXis.Add(Utils.GetEnumValue<EyouSoft.Model.EnumType.SourceStructure.SourceCuisine>(Utils.GetXAttributeValue(xrow, "Cuisine"), EyouSoft.Model.EnumType.SourceStructure.SourceCuisine.未选择));
                        }
                    }

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-景点列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBJingDianInfo> GetJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBJingDianInfo> items = new List<EyouSoft.Model.GysStructure.MLBJingDianInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId,IsBackSingle";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.景点, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBJingDianInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    item.IsFanDan = rdr.GetString(rdr.GetOrdinal("IsBackSingle")) == "1";

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                    item.JingDians = GetJingDianJingDians(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-游轮列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBYouLunInfo> GetYouLuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBYouLunInfo> items = new List<EyouSoft.Model.GysStructure.MLBYouLunInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.游轮, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBYouLunInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                    item.YouChuans = GetYouLunYouChuans(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-车队列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBCheDuiInfo> GetCheDuis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBCheDuiInfo> items = new List<EyouSoft.Model.GysStructure.MLBCheDuiInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.车队, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBCheDuiInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-票务列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBPiaoWuInfo> GetPiaoWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBPiaoWuInfo> items = new List<EyouSoft.Model.GysStructure.MLBPiaoWuInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId,UnitPolicy";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.票务, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBPiaoWuInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    item.ZhengCe = rdr["UnitPolicy"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-购物列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBGouWuInfo> GetGouWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBGouWuInfo> items = new List<EyouSoft.Model.GysStructure.MLBGouWuInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            fields += ",(SELECT A.SellType FROM tbl_SourceShop AS A WHERE A.SourceId=tbl_Source.SourceId) AS ShangPinLeiBie";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.购物, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBGouWuInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    item.ShangPinLeiBie = rdr["ShangPinLeiBie"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-其他列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBQiTaInfo> GetQiTas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts)
        {
            IList<EyouSoft.Model.GysStructure.MLBQiTaInfo> items = new List<EyouSoft.Model.GysStructure.MLBQiTaInfo>();
            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsCommission,IsRecommend,IsPermission,UserId,OperatorId";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            string sql = GetGysLBSQL(companyId, EyouSoft.Model.EnumType.SourceStructure.SourceType.其他, chaXun, isOnlySelf, userId, depts);

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql, orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MLBQiTaInfo();
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsFanLi = rdr.GetString(rdr.GetOrdinal("IsCommission")) == "1";
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsPermission")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsRecommend")) == "1";
                    item.JiaoYiXX = null;
                    item.Lxrs = null;
                    item.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetGysLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_Delete");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType? GetGysLeiXing(string gysId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGysLeiXing);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return (EyouSoft.Model.EnumType.SourceStructure.SourceType)rdr.GetByte(0);
                }
            }

            return null;
        }

        /// <summary>
        /// 获取供应商交易明细信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="heJi">合计信息[0:数量:int][1:数量:decimal][2:结算金额:decimal][3:已支付金额:decimal]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi)
        {
            heJi = new object[] { 0, 0M, 0M, 0M };
            IList<EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo> items = new List<EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo>();
            string tableName = "view_Gys_JiaoYiMingXi";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = "SUM(ShuLiang) AS ShuLiangHeJi,SUM(DShuLiang) AS DShuLiangHeJi,SUM(JinE) AS JinEHeJi,SUM(YiZhiFuJinE) AS YiZhiFuJinEHeJi";
            StringBuilder sql = new StringBuilder();

            #region sql
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (gysId != "all")
            {
                sql.AppendFormat(" AND GysId='{0}' ", gysId);
            }
            else if (chaXun.GysLeiXing.HasValue)
            {
                sql.AppendFormat(" AND GysLeiXing={0} ", (int)chaXun.GysLeiXing.Value);
            }
            else
            {
                sql.AppendFormat(" AND GysId='{0}' ", "00000000-0000-0000-0000-000000000000");
            }

            if (chaXun != null)
            {
                if (chaXun.LEDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.LEDate.Value.AddDays(1));
                }
                if (chaXun.LSDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.LSDate.Value.AddDays(-1));
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo();

                    item.AnPaiLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)rdr.GetByte(rdr.GetOrdinal("AnPaiLeiXing"));
                    item.AnPaiTianJiaLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus)rdr.GetByte(rdr.GetOrdinal("AnPaiTianJiaLeiXing"));
                    item.DaoYouname = string.Empty;
                    item.DShuLiang = rdr.GetDecimal(rdr.GetOrdinal("DShuLiang"));
                    item.FeiYongMingXi = rdr["FeiYongMingXi"].ToString();
                    item.JiDiaoYuanName = rdr["JiDiaoYuanName"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    item.XiaoShouYuanName = rdr["XiaoShouYuanName"].ToString();
                    item.YiZhiFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinE"));
                    item.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.GysName = rdr["GysName"].ToString();

                    string xml = rdr["DaoYouXml"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        var xroot = XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");
                        foreach (var xrow in xrows)
                        {
                            string _name = Utils.GetXAttributeValue(xrow, "Name") + ",";
                            if (item.DaoYouname.IndexOf(_name) > -1) continue;

                            item.DaoYouname = item.DaoYouname + _name;
                        }
                        if (!string.IsNullOrEmpty(item.DaoYouname)) item.DaoYouname = item.DaoYouname.TrimEnd(',');
                    }                    

                    if (item.AnPaiTianJiaLeiXing != EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调报账时添加)
                    {
                        xml = rdr["JiDiaoYuanXml"].ToString();

                        if (!string.IsNullOrEmpty(xml))
                        {
                            item.JiDiaoYuanName = string.Empty;
                            var xroot = XElement.Parse(xml);
                            var xrows = Utils.GetXElements(xroot, "row");
                            foreach (var xrow in xrows)
                            {
                                string _name = Utils.GetXAttributeValue(xrow, "Name") + ",";
                                if (item.JiDiaoYuanName.IndexOf(_name) > -1) continue;

                                item.JiDiaoYuanName = item.JiDiaoYuanName + _name;
                            }
                            if (!string.IsNullOrEmpty(item.JiDiaoYuanName)) item.JiDiaoYuanName = item.JiDiaoYuanName.TrimEnd(',');
                        }
                    }

                    item.ZhiFuFangShi = (EyouSoft.Model.EnumType.PlanStructure.Payment)rdr.GetByte(rdr.GetOrdinal("PaymentType"));

                    items.Add(item);
                }

                rdr.NextResult();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShuLiangHeJi"))) heJi[0] = rdr.GetInt32(rdr.GetOrdinal("ShuLiangHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DShuLiangHeJi"))) heJi[1] = rdr.GetDecimal(rdr.GetOrdinal("DShuLiangHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JinEHeJi"))) heJi[2] = rdr.GetDecimal(rdr.GetOrdinal("JinEHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YiZhiFuJinEHeJi"))) heJi[3] = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinEHeJi"));
                }
            }

            return items;
        }
        #endregion
    }
}
