using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.SysStructure;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 地理数据
    /// 创建者：郑付杰
    /// 创建时间:2011/9/29
    /// </summary>
    public class DGeography:DALBase,EyouSoft.IDAL.SysStructure.IGeography
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetGuoJias = " SELECT * FROM tbl_SysCountry WHERE IsDefault='1' ";
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
        public DGeography()
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
            IList<MSysCity> items = new List<MSysCity>();
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

        #region IGeography 成员
        /// <summary>
        /// 获取默认所有国家省份城市县区信息
        /// </summary>
        /// <returns></returns>
        public IList<MSysCountry>  GetAllList()
        {
            IList<MSysCountry> items = new List<MSysCountry>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGuoJias);

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
        #endregion
    }
}
