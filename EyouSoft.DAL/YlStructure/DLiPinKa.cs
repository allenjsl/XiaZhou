//YL礼品卡 汪奇志 2014-03-29
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
    /// YL礼品卡
    /// </summary>
    public class DLiPinKa : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.ILiPinKa
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetLiPinKaInfo = "SELECT * FROM tbl_YL_WZ_LiPinKa WHERE LiPinKaId=@LiPinKaId";
        const string SQL_SELECT_GetLiPinKaDingDanInfo = "SELECT * FROM view_YL_LiPinKaDingDan WHERE DingDanId=@DingDanId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DLiPinKa()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members

        #endregion

        #region ILiPinKa 成员
        /// <summary>
        /// 礼品卡新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int LiPinKa_CU(EyouSoft.Model.YlStructure.MLiPinKaInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_LiPinKa_CU");
            _db.AddInParameter(cmd, "@LiPinKaId", DbType.AnsiStringFixedLength, info.LiPinKaId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@JinE1", DbType.Decimal, info.JinE1);
            _db.AddInParameter(cmd, "@FengMian", DbType.String, info.FengMian);
            _db.AddInParameter(cmd, "@MiaoShu", DbType.String, info.MiaoShu);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@LiPinKaKuaiDiJinE", DbType.Decimal, info.LiPinKaKuaiDiJinE);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@XuZhi", DbType.String, info.XuZhi);
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
        /// 礼品卡删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        public int LiPinKa_D(string companyId, string liPinKaId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_LiPinKa_D");
            _db.AddInParameter(cmd, "@LiPinKaId", DbType.AnsiStringFixedLength, liPinKaId);
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
        /// 获取礼品卡信息
        /// </summary>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MLiPinKaInfo GetLiPinKaInfo(string liPinKaId)
        {
            EyouSoft.Model.YlStructure.MLiPinKaInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetLiPinKaInfo);
            _db.AddInParameter(cmd, "@LiPinKaId", DbType.AnsiStringFixedLength, liPinKaId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MLiPinKaInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FengMian = rdr["FengMian"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.JinE1 = rdr.GetDecimal(rdr.GetOrdinal("JinE1"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LiPinKaId = rdr["LiPinKaId"].ToString();
                    info.LiPinKaKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("LiPinKaKuaiDiJinE"));
                    info.MiaoShu = rdr["MiaoShu"].ToString();
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XuZhi = rdr["XuZhi"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取礼品卡集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MLiPinKaInfo> GetLiPinKas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MLiPinKaInfo> items = new List<EyouSoft.Model.YlStructure.MLiPinKaInfo>();
            string tableName = "tbl_YL_WZ_LiPinKa";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.MingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.MingCheng);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MLiPinKaInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FengMian = rdr["FengMian"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.JinE1 = rdr.GetDecimal(rdr.GetOrdinal("JinE1"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LiPinKaId = rdr["LiPinKaId"].ToString();
                    info.LiPinKaKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("LiPinKaKuaiDiJinE"));
                    info.MiaoShu = rdr["MiaoShu"].ToString();
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增礼品卡订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int LiPinKaDingDan_C(EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_LiPinKaDingDan_C");

            _db.AddInParameter(cmd, "@LiPinKaId", DbType.AnsiStringFixedLength, info.LiPinKaId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@JiaoYiHao", DbType.String, info.JiaoYiHao);
            _db.AddInParameter(cmd, "@ShuLiang", DbType.Int32, info.ShuLiang);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@JinE1", DbType.Decimal, info.JinE1);
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
            _db.AddInParameter(cmd, "@ZengYu", DbType.String, info.ZengYu);
            _db.AddInParameter(cmd, "@SlrDiZhiId", DbType.AnsiStringFixedLength, info.SlrDiZhiId);
            _db.AddInParameter(cmd, "@LiPinKaKuaiDiJinE", DbType.Decimal, info.LiPinKaKuaiDiJinE);
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
        /// 获取礼品卡订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo GetLiPinKaDingDanInfo(string dingDanId)
        {
            EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo info = null;

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetLiPinKaDingDanInfo);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
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
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.JinE1 = rdr.GetDecimal(rdr.GetOrdinal("JinE1"));
                    info.LiPinKaId = rdr["LiPinKaId"].ToString();
                    info.LiPinKaKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("LiPinKaKuaiDiJinE"));
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.SlrDiZhiId = rdr["SlrDiZhiId"].ToString();
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.ZengYu = rdr["ZengYu"].ToString();

                    info.LiPinKaLeiXing = (EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing)rdr.GetByte(rdr.GetOrdinal("LiPinKaLeiXing"));
                    info.LiPinKaMingCheng = rdr["LiPinKaMingCheng"].ToString();
                    info.HuiYuanXingMing = rdr["HuiYuanXingMing"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取礼品卡订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo> GetLiPinKaDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaDingDanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo> items = new List<EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo>();
            string tableName = "view_YL_LiPinKaDingDan";
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
                if (!string.IsNullOrEmpty(chaXun.JiaoYiHao))
                {
                    sql.AppendFormat(" AND JiaoYiHao LIKE '%{0}%' ", chaXun.JiaoYiHao);
                }
                if (!string.IsNullOrEmpty(chaXun.LiPinKaMingCheng))
                {
                    sql.AppendFormat(" AND LiPinKaMingCheng LIKE '%{0}%' ", chaXun.LiPinKaMingCheng);
                }
                if (chaXun.LiPinKaLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LiPinKaLeiXing={0} ", (int)chaXun.LiPinKaLeiXing.Value);
                }
                if (chaXun.XiaDanShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>='{0}' ", chaXun.XiaDanShiJian1.Value);
                }
                if (chaXun.XiaDanShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<='{0}' ", chaXun.XiaDanShiJian2.Value.AddDays(1).AddMinutes(-1));
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
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
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.JinE1 = rdr.GetDecimal(rdr.GetOrdinal("JinE1"));
                    info.LiPinKaId = rdr["LiPinKaId"].ToString();
                    info.LiPinKaKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("LiPinKaKuaiDiJinE"));
                    info.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    info.SlrDiZhiId = rdr["SlrDiZhiId"].ToString();
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.ZengYu = rdr["ZengYu"].ToString();

                    info.LiPinKaLeiXing = (EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing)rdr.GetByte(rdr.GetOrdinal("LiPinKaLeiXing"));
                    info.LiPinKaMingCheng = rdr["LiPinKaMingCheng"].ToString();
                    info.HuiYuanXingMing = rdr["HuiYuanXingMing"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置礼品卡订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        public int SheZhiLiPinKaDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus status)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_LiPinKaDingDan_SheZhiDingDanStatus");
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
        /// 设置礼品卡订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiLiPinKaDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_LiPinKaDingDan_SheZhiFuKuanStatus");
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
        #endregion
    }
}
