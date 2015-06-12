using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.SysStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 常用城市
    /// 创建者：郑付杰
    /// 创建时间：2011/9/29
    /// </summary>
    public class DComCity : DALBase, EyouSoft.IDAL.ComStructure.IComCity
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetGuoJias = " SELECT * FROM tbl_SysCountry WHERE CompanyId=@CompanyId OR IsDefault='1' ";
        const string SQL_SELECT_GetShengFens = "SELECT * FROM tbl_SysProvince WHERE CountryId=@GuoJiaId";
        const string SQL_SELECT_GetChengShis = "SELECT * FROM tbl_SysCity WHERE ProvinceId=@ShengFenId";
        const string SQL_SELECT_GetXianQus = "SELECT * FROM tbl_SysCounty WHERE CityId=@ChengShiId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DComCity()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// get shengfens
        /// </summary>
        /// <param name="guoJiaId">guojiaid</param>
        /// <returns></returns>
        IList<MSysProvince> GetShengFens(int guoJiaId)
        {
            IList<MSysProvince> items = new List<MSysProvince>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetShengFens);
            _db.AddInParameter(cmd, "GuoJiaId", DbType.AnsiStringFixedLength, guoJiaId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysProvince();

                    item.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.Name = rdr["Name"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Citys = GetChengShis(item.ProvinceId);
                }
            }

            return items;
        }

        /// <summary>
        /// get chengshis
        /// </summary>
        /// <param name="shengFenId">shengfenid</param>
        /// <returns></returns>
        IList<MSysCity> GetChengShis(int shengFenId)
        {
            IList<MSysCity> items =new List<MSysCity>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChengShis);
            _db.AddInParameter(cmd, "ShengFenId", DbType.AnsiStringFixedLength, shengFenId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysCity();

                    item.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.Name = rdr["Name"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Districts = GetXianQus(item.CityId);
                }
            }

            return items;
        }

        /// <summary>
        /// get xianqus
        /// </summary>
        /// <param name="chengShiId">chengshiid</param>
        /// <returns></returns>
        IList<MSysDistrict> GetXianQus(int chengShiId)
        {
            IList<MSysDistrict> items = new List<MSysDistrict>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetXianQus);
            _db.AddInParameter(cmd, "ChengShiId", DbType.AnsiStringFixedLength, chengShiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysDistrict();

                    item.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.Name = rdr["Name"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region IComCity 成员
        ///// <summary>
        ///// 设置常用城市集合
        ///// </summary>
        ///// <param name="list">常用城市</param>
        ///// <param name="companyId">公司编号</param>
        ///// <returns>true：设置成功 false：设置失败</returns>
        //public bool SetCity(IList<MComCity> list, string companyId)
        //{
        //    StringBuilder xml = new StringBuilder();
        //    xml.Append("<item>");
        //    foreach (MComCity item in list)
        //    {
        //        xml.AppendFormat("<city id='{0}'>{1}</city>", companyId, item.CityId);
        //    }
        //    xml.Append("</item>");


        //    DbCommand comm = this._db.GetStoredProcCommand("proc_ComCity_Update");
        //    this._db.AddInParameter(comm, "@xml", DbType.Xml, xml.ToString());
        //    this._db.AddInParameter(comm, "@companyid", DbType.AnsiStringFixedLength, companyId);

        //    int result = DbHelper.ExecuteSql(comm, this._db);

        //    return result > 0 ? true : false;
        //}
        /// <summary>
        /// 设置常用城市
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：设置成功 false：设置失败</returns>
        public bool SetCity(int cityId, string companyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("if not exists(select id from tbl_ComCity where CityId = {0} and CompanyId = '{1}')", cityId, companyId);
            sql.Append(" begin");
            sql.AppendFormat(" insert into tbl_ComCity(CompanyId,CityId) values('{0}',{1})", companyId, cityId);
            sql.Append(" end else begin");
            sql.AppendFormat(" delete from tbl_ComCity where CityId = {0} and CompanyId = '{1}'", cityId, companyId);
            sql.Append(" end");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取公司常用国家省份城市县区
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MSysCountry> GetAllCity(string companyId)
        {
            IList<MSysCountry> items = new List<MSysCountry>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGuoJias);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysCountry();
                    item.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.Name = rdr["Name"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Provinces = GetShengFens(item.CountryId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取公司常用城市编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>成员城市编号集合</returns>
        public IList<int> GetCityId(string companyId)
        {
            string sql = "SELECT CityId FROM tbl_ComCity WHERE CompanyId = @companyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            IList<int> list = new List<int>();
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add((int)reader["CityId"]);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取省份编号（按省份行政区划代码）
        /// </summary>
        /// <param name="sfxzqhdm">省份行政区划代码</param>
        /// <returns></returns>
        public int GetSFID_SFXZQHDM(string sfxzqhdm)
        {
            string sql = "SELECT ProvinceId FROM tbl_SysProvince WHERE xzqhdm=@xzqhdm";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "xzqhdm", DbType.String, sfxzqhdm);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return -1;
        }

       
        #endregion
    }
}
