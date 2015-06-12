// YL积分兑换商品 汪奇志 2014-03-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.YlStructure
{
    /// <summary>
    /// YL积分兑换商品
    /// </summary>
    public class DDuiHuan : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IDuiHuan
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetJiFenShangPinInfo = "SELECT * FROM view_YL_JiFenShangPin WHERE ShangPinId=@ShangPinId";
        const string SQL_SELECT_GetJiFenShangPinFuJians = "SELECT * FROM tbl_YL_WZ_JiFenShangPinFuJian WHERE ShangPinId=@ShangPinId";
        const string SQL_SELECT_GetJiFenShangPinFangShis = "SELECT * FROM tbl_YL_WZ_JiFenShangPinFangShi WHERE ShangPinId=@ShangPinId";
        const string SQL_SELECT_GetJiFenDingDanInfo = "SELECT * FROM view_YL_JiFenDingDan WHERE DingDanId=@DingDanId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DDuiHuan()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create fujian xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFuJianXml(IList<EyouSoft.Model.YlStructure.MFuJianInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info LeiXing=\"{0}\" Filepath=\"{1}\"><MiaoShu><![CDATA[{2}]]></MiaoShu></info>", item.LeiXing
                        , item.Filepath
                        , item.MiaoShu);
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// create jifenshangpinfangshi xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateJiFenShangPinFangShiXml(IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FangShi=\"{0}\" JiFen=\"{1}\" JinE=\"{2}\" />", (int)item.FangShi
                        , item.JiFen
                        , item.JinE);
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// get jifenshangpinfangshis
        /// </summary>
        /// <param name="shangPinId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo> GetJiFenShangPinFangShis(string shangPinId)
        {
            IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo> items = new List<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiFenShangPinFangShis);
            _db.AddInParameter(cmd, "@ShangPinId", DbType.AnsiStringFixedLength, shangPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo();
                    item.FangShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)rdr.GetByte(rdr.GetOrdinal("FangShi"));
                    item.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    items.Add(item);
                }
            }


            return items;
        }

        /// <summary>
        /// get jifenshangpinfujians
        /// </summary>
        /// <param name="shangPinId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetJiFenShangPinFuJians(string shangPinId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiFenShangPinFuJians);
            _db.AddInParameter(cmd, "ShangPinId", DbType.AnsiStringFixedLength, shangPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.FuJianId = rdr.GetInt32(rdr.GetOrdinal("FuJianId"));
                    item.LeiXing = rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region IDuiHuan 成员
        /// <summary>
        /// 积分商品新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int JiFenShangPin_CU(EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiFenShangPin_CU");
            _db.AddInParameter(cmd, "@ShangPinId", DbType.AnsiStringFixedLength, info.ShangPinId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@ShuoMing", DbType.String, info.ShuoMing);
            _db.AddInParameter(cmd, "@XuZhi", DbType.String, info.XuZhi);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@FuJianXml", DbType.String,CreateFuJianXml(info.FuJians));
            _db.AddInParameter(cmd, "@FangShiXml", DbType.String, CreateJiFenShangPinFangShiXml(info.FangShis));
            _db.AddInParameter(cmd, "@PeiSongFangShi", DbType.String, info.PeiSongFangShi);
            _db.AddInParameter(cmd, "@Status", DbType.Byte, info.Status);
            _db.AddInParameter(cmd, "@ShuLiang", DbType.Int32, info.ShuLiang);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@ShangPinJinE", DbType.Decimal, info.ShangPinJinE);
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
        /// 积分商品删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        public int JiFenShangPin_D(string companyId, string shangPinId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiFenShangPin_D");
            _db.AddInParameter(cmd, "@ShangPinId", DbType.AnsiStringFixedLength, shangPinId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
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
        /// 获取积分商品信息
        /// </summary>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo GetJiFenShangPinInfo(string shangPinId)
        {
            EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiFenShangPinInfo);
            _db.AddInParameter(cmd, "@ShangPinId", DbType.AnsiStringFixedLength, shangPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FangShis = null;
                    info.FuJians = null;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ShangPinId = rdr["ShangPinId"].ToString();
                    info.ShuoMing = rdr["ShuoMing"].ToString();
                    info.XuZhi = rdr["XuZhi"].ToString();
                    info.PeiSongFangShi = rdr["PeiSongFangShi"].ToString();
                    info.Status = (EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.ChengJiaoShuLiang = rdr.GetInt32(rdr.GetOrdinal("ChengJiaoShuLiang"));
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.ShangPinJinE = rdr.GetDecimal(rdr.GetOrdinal("ShangPinJinE"));
                }
            }

            if (info != null)
            {
                info.FangShis = GetJiFenShangPinFangShis(info.ShangPinId);
                info.FuJians = GetJiFenShangPinFuJians(info.ShangPinId);
            }

            return info;
        }

        /// <summary>
        /// 获取积分商品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo> GetJiFenShangPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenShangPinChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo> items = new List<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo>();
            string tableName = "view_YL_JiFenShangPin";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                switch (chaXun.PaiXu)
                {
                    case 0: orderByString = "JiFen DESC"; break;
                    case 1: orderByString = "JiFen ASC"; break;
                    case 2: orderByString = "LiPinKaJinE DESC"; break;
                    case 3: orderByString = "LiPinKaJinE ASC"; break;
                    case 4: orderByString = "XianJinJinE DESC"; break;
                    case 5: orderByString = "XianJinJinE ASC"; break;
                    case 6: orderByString = "IssueTime DESC"; break;
                    case 7: orderByString = "IssueTime ASC"; break;
                    default: orderByString = "IssueTime DESC"; break;
                }

                if (!string.IsNullOrEmpty(chaXun.MingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.MingCheng);
                }
                if (chaXun.Status.HasValue)
                {
                    sql.AppendFormat(" AND Status={0} ", (int)chaXun.Status.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FangShis = null;
                    info.FuJians = null;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ShangPinId = rdr["ShangPinId"].ToString();
                    info.ShuoMing = rdr["ShuoMing"].ToString();
                    info.XuZhi = rdr["XuZhi"].ToString();
                    info.Status = (EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.ChengJiaoShuLiang = rdr.GetInt32(rdr.GetOrdinal("ChengJiaoShuLiang"));
                    info.ShangPinJinE = rdr.GetDecimal(rdr.GetOrdinal("ShangPinJinE"));

                    items.Add(info);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.FangShis = GetJiFenShangPinFangShis(item.ShangPinId);
                    item.FuJians = GetJiFenShangPinFuJians(item.ShangPinId);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增积分订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JiFenDingDan_C(EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiFenDingDan_C");
            _db.AddInParameter(cmd, "@ShangPinId", DbType.AnsiStringFixedLength, info.ShangPinId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@JiaoYiHao", DbType.String, info.JiaoYiHao);
            _db.AddInParameter(cmd, "@ShuLiang", DbType.Int32, info.ShuLiang);
            _db.AddInParameter(cmd, "@FangShi", DbType.Byte, info.FangShi);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@JiFen", DbType.Decimal, info.JiFen);
            _db.AddInParameter(cmd, "@DingDanStatus", DbType.Byte, info.DingDanStatus);
            _db.AddInParameter(cmd, "@FuKuanStatus", DbType.Byte, info.FuKuanStatus);
            _db.AddInParameter(cmd, "@XiaDanBeiZhu", DbType.String, info.XiaDanBeiZhu);
            _db.AddInParameter(cmd, "@YuDingRenName", DbType.String, info.YuDingRenName);
            _db.AddInParameter(cmd, "@YuDingRenDianHua", DbType.String, info.YuDingRenDianHua);
            _db.AddInParameter(cmd, "@YuDingRenShouJi", DbType.String, info.YuDingRenShouJi);
            _db.AddInParameter(cmd, "@YuDingRenYouXiang", DbType.String, info.YuDingRenYouXiang);
            _db.AddInParameter(cmd, "@IsXuYaoFaPiao", DbType.AnsiStringFixedLength, info.IsXuYaoFaPiao?"1":"0");
            _db.AddInParameter(cmd, "@FaPiaoTaiTou", DbType.String, info.FaPiaoTaiTou);
            _db.AddInParameter(cmd, "@FaPiaoLeiXing", DbType.String, info.FaPiaoLeiXing);
            _db.AddInParameter(cmd, "@FaPiaoMingXi", DbType.String, info.FaPiaoMingXi);
            _db.AddInParameter(cmd, "@FaPiaoPeiSongFangShi", DbType.Byte, info.FaPiaoPeiSongFangShi);
            _db.AddInParameter(cmd, "@FaPiaoDiZhiId", DbType.AnsiStringFixedLength, info.FaPiaoDiZhiId);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@XiaDanRenId", DbType.AnsiStringFixedLength, info.XiaDanRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@LiPinDiZhiId", DbType.AnsiStringFixedLength, info.LiPinDiZhiId);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

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
        /// 获取积分订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo GetJiFenDingDanInfo(string dingDanId)
        {
            EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiFenDingDanInfo);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                    info.FangShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)rdr.GetByte(rdr.GetOrdinal("FangShi"));
                    info.FaPiaoDiZhiId = rdr["FaPiaoDiZhiId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FaPiaoLeiXing = rdr["FaPiaoLeiXing"].ToString();
                    info.FaPiaoMingXi = rdr["FaPiaoMingXi"].ToString();
                    info.FaPiaoPeiSongFangShi = (EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi)rdr.GetByte(rdr.GetOrdinal("FaPiaoPeiSongFangShi"));
                    info.FaPiaoTaiTou = rdr["FaPiaoTaiTou"].ToString();
                    info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)rdr.GetByte(rdr.GetOrdinal("FuKuanStatus"));
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsXuYaoFaPiao = rdr["IsXuYaoFaPiao"].ToString() == "1";
                    info.JiaoYiHao = rdr["JiaoYiHao"].ToString();
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.ShangPinId = rdr["ShangPinId"].ToString();
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.ShangPinMingCheng = rdr["ShangPinMingCheng"].ToString();
                    info.HuiYuanXingMing = rdr["HuiYuanXingMing"].ToString();
                    info.HuiYuanZhangHao = rdr["HuiYuanZhangHao"].ToString();
                    info.LiPinDiZhiId = rdr["LiPinDiZhiId"].ToString();
                    info.IdentityId = rdr.GetInt32(rdr.GetOrdinal("IdentityId"));
                    info.ShangPinJinE = rdr.GetDecimal(rdr.GetOrdinal("ShangPinJinE"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取积分订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo> GetJiFenDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenDingDanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo> items = new List<EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo>();
            string tableName = "view_YL_JiFenDingDan";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND XiaDanRenId='{0}' ", chaXun.HuiYuanId);
                }
                if (chaXun.DingDanStatus.HasValue)
                {
                    sql.AppendFormat(" AND DingDanStatus={0} ", (int)chaXun.DingDanStatus);
                }
                if (chaXun.FuKuanStatus.HasValue)
                {
                    sql.AppendFormat(" AND FuKuanStatus={0} ", (int)chaXun.FuKuanStatus);
                }
                if (!string.IsNullOrEmpty(chaXun.ShangPinMingCheng))
                {
                    sql.AppendFormat(" AND ShangPinMingCheng LIKE '%{0}%' ", chaXun.ShangPinMingCheng);
                }
                if (chaXun.XiaDanShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>='{0}' ", chaXun.XiaDanShiJian1.Value);
                }
                if (chaXun.XiaDanShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<='{0}' ", chaXun.XiaDanShiJian2.Value.AddDays(1).AddMinutes(-1));
                }
                if (!string.IsNullOrEmpty(chaXun.JiaoYiHao))
                {
                    sql.AppendFormat(" AND JiaoYiHao LIKE '%{0}%' ", chaXun.JiaoYiHao);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                    info.FangShi = (EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi)rdr.GetByte(rdr.GetOrdinal("FangShi"));
                    info.FaPiaoDiZhiId = rdr["FaPiaoDiZhiId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FaPiaoLeiXing = rdr["FaPiaoLeiXing"].ToString();
                    info.FaPiaoMingXi = rdr["FaPiaoMingXi"].ToString();
                    info.FaPiaoPeiSongFangShi = (EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi)rdr.GetByte(rdr.GetOrdinal("FaPiaoPeiSongFangShi"));
                    info.FaPiaoTaiTou = rdr["FaPiaoTaiTou"].ToString();
                    info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)rdr.GetByte(rdr.GetOrdinal("FuKuanStatus"));
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsXuYaoFaPiao = rdr["IsXuYaoFaPiao"].ToString() == "1";
                    info.JiaoYiHao = rdr["JiaoYiHao"].ToString();
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.ShangPinId = rdr["ShangPinId"].ToString();
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.ShangPinMingCheng = rdr["ShangPinMingCheng"].ToString();
                    info.HuiYuanXingMing = rdr["HuiYuanXingMing"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置积分兑换订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        public int SheZhiJiFenDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus status)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiFenDingDan_SheZhiDingDanStatus");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@DingDanStatus", DbType.Byte, status);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

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
        /// 设置积分兑换订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiJiFenDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiFenDingDan_SheZhiFuKuanStatus");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@FuKuanStatus", DbType.Byte, status);
            _db.AddInParameter(cmd, "@FuKuanShiJian", DbType.DateTime, fuKuanShiJian.Value);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@FangShi", DbType.Byte, info.FangShi);
            _db.AddInParameter(cmd, "@BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

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
        /// 获取积分订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        public string GetDingDanId(int identityId)
        {
            string sql = "SELECT DingDanId FROM tbl_YL_WZ_JiFenDingDan WHERE IdentityId=@IdentityId";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "IdentityId", DbType.Int32, identityId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr[0].ToString();
                }
            }

            return string.Empty;
        }
        #endregion
    }
}
