// YL-网站会员相关 汪奇志 2014-03-28
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
    /// YL-网站会员相关
    /// </summary>
    public class DHuiYuan : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IHuiYuan
    {
        #region static constants
        //static constants
        const string SQL_SELECT_IsExistsUsername = "SELECT COUNT(*) FROM tbl_YL_WZ_HuiYuan WHERE CompanyId=@CompanyId AND Username=@Username";
        const string SQL_SELECT_GetHuiYuanInfo = "SELECT * FROM tbl_YL_WZ_HuiYuan WHERE HuiYuanId=@HuiYuanId";
        const string SQL_INSERT_InsertChangLvKe = "INSERT INTO [tbl_YL_WZ_HuiYuanChangLvKe]([HuiYuanId],[LvkeId],[XingMing],[LeiXing],[ZhengJianLeiXing],[ZhengJianHaoMa],[ZhengJianYouXiaoQi],[ChuShengRiQi],[DianHua],[ShouJi],[GuoJiaId],[ShengFenId],[ChengShiId],[XianQuId],[ZhuangTai],[GuoJiId],[GuoJi],[IssueTime],[YXQ1],[YXQ2],[YXQ3],[SR1],[SR2],[SR3])VALUES(@HuiYuanId,@LvkeId,@XingMing,@LeiXing,@ZhengJianLeiXing,@ZhengJianHaoMa,@ZhengJianYouXiaoQi,@ChuShengRiQi,@DianHua,@ShouJi,@GuoJiaId,@ShengFenId,@ChengShiId,@XianQuId,@ZhuangTai,@GuoJiId,@GuoJi,@IssueTime,@YXQ1,@YXQ2,@YXQ3,@SR1,@SR2,@SR3)";
        const string SQL_UPDATE_UpdateChangLvKe = "UPDATE [tbl_YL_WZ_HuiYuanChangLvKe] SET [XingMing] = @XingMing,[LeiXing] = @LeiXing,[ZhengJianLeiXing] = @ZhengJianLeiXing,[ZhengJianHaoMa] = @ZhengJianHaoMa,[ZhengJianYouXiaoQi] = @ZhengJianYouXiaoQi,[ChuShengRiQi] = @ChuShengRiQi,[DianHua] = @DianHua,[ShouJi] = @ShouJi,[GuoJiaId] = @GuoJiaId,[ShengFenId] = @ShengFenId,[ChengShiId] = @ChengShiId,[XianQuId] = @XianQuId,[ZhuangTai] = @ZhuangTai,[GuoJiId] = @GuoJiId,[GuoJi] = @GuoJi WHERE [LvkeId] = @LvkeId";
        const string SQL_DELETE_DeleteChangLvKe = "DELETE FROM tbl_YL_WZ_HuiYuanChangLvKe WHERE LvKeId=@LvKeId";
        const string SQL_SELECT_GetChangLvKeInfo = "SELECT * FROM tbl_YL_WZ_HuiYuanChangLvKe WHERE LvKeId=@LvKeId";
        const string SQL_INSERT_InsertDiZhi = "INSERT INTO [tbl_YL_WZ_HuiYuanDiZhi]([HuiYuanId],[DiZhiId],[GuoJiaId],[ShengFenId],[ChengShiId],[XianQuId],[DiZhi],[YouBian],[XingMing],[DianHua],[IssueTime],[IsMoRen],[IsXianShi])VALUES(@HuiYuanId,@DiZhiId,@GuoJiaId,@ShengFenId,@ChengShiId,@XianQuId,@DiZhi,@YouBian,@XingMing,@DianHua,@IssueTime,@IsMoRen,'1')";
        const string SQL_UPDATE_UpdateDiZhi = "UPDATE [tbl_YL_WZ_HuiYuanDiZhi]SET[GuoJiaId] = @GuoJiaId,[ShengFenId] = @ShengFenId,[ChengShiId] = @ChengShiId,[XianQuId] = @XianQuId,[DiZhi] = @DiZhi,[YouBian] = @YouBian,[XingMing] = @XingMing,[DianHua] = @DianHua WHERE [DiZhiId] = @DiZhiId";
        const string SQL_DELETE_DeleteDiZhi = "DELETE FROM tbl_YL_WZ_HuiYuanDiZhi WHERE DiZhiId=@DiZhiId";
        const string SQL_SELECT_GetDiZhiInfo = "SELECT * FROM tbl_YL_WZ_HuiYuanDiZhi WHERE DiZhiId=@DiZhiId";
        const string SQL_UPDATE_SheZhiMoRenDiZhi = "UPDATE tbl_YL_WZ_HuiYuanDiZhi SET IsMoRen='0' WHERE HuiYuanId=@HuiYuanId;UPDATE tbl_YL_WZ_HuiYuanDiZhi SET IsMoRen='1' WHERE HuiYuanId=@HuiYuanId AND DiZhiId=@DiZhiId ";
        const string SQL_UPDATE_SheZhiChangLvKeStatus = "UPDATE tbl_YL_WZ_HuiYuanChangLvKe SET ZhuangTai=@ZhuangTai WHERE HuiYuanId=@HuiYuanId AND LvKeId=@LvKeId";
        const string SQL_INSERT_InsertGongLue = "INSERT INTO [tbl_YL_WZ_GongLue]([GongLueId],[CompanyId],[BiaoTi],[NeiRong],[IssueTime],[OperatorId],[DingDanId],[GysId],[GongSiId],[XiLieId],[ChuanZhiId],[HangQiId],[RiQiId],[IsShenHe],[ShenHeOperatorId],[ShenHeTime])VALUES(@GongLueId,@CompanyId,@BiaoTi,@NeiRong,@IssueTime,@OperatorId,@DingDanId,@GysId,@GongSiId,@XiLieId,@ChuanZhiId,@HangQiId,@RiQiId,@IsShenHe,@ShenHeOperatorId,@ShenHeTime)";
        const string SQL_UPDATE_UpdateGongLue = "UPDATE [tbl_YL_WZ_GongLue] SET [BiaoTi] = @BiaoTi,[NeiRong] = @NeiRong,[IsShenHe]=@IsShenHe WHERE [GongLueId] = @GongLueId";
        const string SQL_DELETE_DeleteGongLue = "DELETE FROM tbl_YL_WZ_GongLue WHERE GognLueId=@GognLueId";
        const string SQL_SELECT_GetGongLueInfo = "SELECT * FROM tbl_YL_WZ_GongLue WHERE GognLueId=@GognLueId";
        const string SQL_UPDATE_ShenHeGongLue = "UPDATE tbl_YL_WZ_GongLue SET IsShenHe='1',ShenHeOperatorId=@ShenHeOperatorId,ShenHeTime=@ShenHeTime WHERE GongLueId=@GongLueId ";
        const string SQL_INSERT_InsertDianPing = "INSERT INTO [tbl_YL_WZ_DianPing]([DianPingId],[DingDanId],[CompanyId],[GysId],[GongSiId],[XiLieId],[ChuanZhiId],[HangQiId],[RiQiId],[NeiRong],[IssueTime],[OperatorId],[IsShenHe],[ShenHeOperatorId],[ShenHeTime],[FenShu],[DingDanLeiXing],[IsNiMing],[BiaoTi])VALUES(@DianPingId,@DingDanId,@CompanyId,@GysId,@GongSiId,@XiLieId,@ChuanZhiId,@HangQiId,@RiQiId,@NeiRong,@IssueTime,@OperatorId,@IsShenHe,@ShenHeOperatorId,@ShenHeTime,@FenShu,@DingDanLeiXing,@IsNiMing,@BiaoTi)";
        const string SQL_UPDATE_UpdateDianPing = "UPDATE [tbl_YL_WZ_DianPing] SET [NeiRong] = @NeiRong,[IsShenHe] = @IsShenHe,[FenShu]=@FenShu,[IsNiMing]=@IsNiMing,[BiaoTi]=@BiaoTi,[ShenHeOperatorId]=@ShenHeOperatorId,[ShenHeTime]=@ShenHeTime WHERE [DianPingId] = @DianPingId";
        const string SQL_DELETE_DeleteDianPing = "DELETE FROM tbl_YL_WZ_DianPing WHERE DianPingId=@DianPingId";
        const string SQL_SELECT_GetDianPingInfo = "SELECT * FROM tbl_YL_WZ_DianPing WHERE DianPingId=@DianPingId";
        const string SQL_SELECT_GetDianPingInfo1 = "SELECT * FROM tbl_YL_WZ_DianPing WHERE DingDanId=@DingDanId";
        const string SQL_UPDATE_ShenHeDianPing = "UPDATE tbl_YL_WZ_DianPing SET IsShenHe='1',ShenHeOperatorId=@ShenHeOperatorId,ShenHeTime=@ShenHeTime WHERE DianPingId=@DianPingId ";
        const string SQL_INSERT_InsertWenDa = "INSERT INTO [tbl_YL_WZ_WenDa]([WenDaId],[CompanyId],[HangQiId],[WenBiaoTi],[WenNeiRong],[WenShiJian],[WenYongHuId],[DaOperatorId],[DaNeiRong],[DaShiJian],[LeiXing],[IsNiMing])VALUES(@WenDaId,@CompanyId,@HangQiId,@WenBiaoTi,@WenNeiRong,@WenShiJian,@WenYongHuId,@DaOperatorId,@DaNeiRong,@DaShiJian,@LeiXing,@IsNiMing)";
        const string SQL_UPDATE_UpdateWenDa = "UPDATE [tbl_YL_WZ_WenDa] SET [WenBiaoTi] = @WenBiaoTi,[WenNeiRong] = @WenNeiRong,[DaOperatorId]=NULL,[LeiXing]=@LeiXing,[IsNiMing]=@IsNiMing WHERE [WenDaId] = @WenDaId";
        const string SQL_DELETE_DeleteWenDa = "DELETE FROM tbl_YL_WZ_WenDa WHERE WenDaId=@WenDaId";
        const string SQL_SELECT_GetWenDaInfo = "SELECT * FROM tbl_YL_WZ_WenDa WHERE WenDaId=@WenDaId";
        const string SQL_UPDATE_HuiFuWenDa = "UPDATE [tbl_YL_WZ_WenDa] SET [DaOperatorId] = @DaOperatorId,[DaNeiRong] = @DaNeiRong,[DaShiJian] = @DaShiJian WHERE [WenDaId] = @WenDaId";
        const string SQL_INSERT_InsertShouCangJia = "INSERT INTO [tbl_YL_WZ_HuiYuanShouCangJia]([HuiYuanId],[ShouCangId],[LeiXing],[ChanPinId],[ShiJian])VALUES(@HuiYuanId,@ShouCangId,@LeiXing,@ChanPinId,@ShiJian)";
        const string SQL_DELETE_DeleteShouCangJia = "DELETE FROM tbl_YL_WZ_HuiYuanShouCangJia WHERE ShouCangId=@ShouCangId";
        const string SQL_SELECT_GetShouCangJiaInfo = "SELECT * FROM tbl_YL_WZ_HuiYuanShouCangJia WHERE ShouCangId=@ShouCangId";
        const string SQL_UPDATE_SheZhiHuiYuanMiMa = "UPDATE tbl_YL_WZ_HuiYuan SET MD5Password=@MD5 WHERE HuiYuanId=@HuiYuanId AND MD5Password=@YuanMD5";
        const string SQL_UPDATE_SheZhiHuiYuanStatus = "UPDATE tbl_YL_WZ_HuiYuan SET Status=@Status WHERE HuiYuanId=@HuiYuanId";
        const string SQL_UPDATE_SetHuiYuanMiMa = "UPDATE tbl_YL_WZ_HuiYuan SET MD5Password=@MD5 WHERE HuiYuanId=@HuiYuanId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DHuiYuan()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// get daifukuan dingdan shu
        /// </summary>
        /// <param name="huiYuanId"></param>
        /// <returns></returns>
        int GetDaiFuKuanDingDanShu(string huiYuanId)
        {
            string sql = "SELECT COUNT(*) FROM view_YL_HuiYuanDingDan WHERE HuiYuanId=@HuiYuanId AND FuKuanStatus=@FuKuanStatus AND (0=1 OR (DingDanLeiXing=@DingDanLeiXing1 AND DingDanStatus=@DingDanStatus1) OR (DingDanLeiXing=@DingDanLeiXing2 AND DingDanStatus=@DingDanStatus2))";
            DbCommand cmd = _db.GetSqlStringCommand(sql);

            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "FuKuanStatus", DbType.Byte, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.未付款);
            _db.AddInParameter(cmd, "DingDanLeiXing1", DbType.Byte, EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单);
            _db.AddInParameter(cmd, "DingDanStatus1", DbType.Byte, EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交);
            _db.AddInParameter(cmd, "DingDanLeiXing2", DbType.Byte, EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单);
            _db.AddInParameter(cmd, "DingDanStatus2", DbType.Byte, EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已成交);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }

        /// <summary>
        /// get daifukuan dingdan shu
        /// </summary>
        /// <param name="huiYuanId"></param>
        /// <returns></returns>
        int GetShouCangShu(string huiYuanId)
        {
            string sql = "SELECT COUNT(*) FROM tbl_YL_WZ_HuiYuanShouCangJia WHERE HuiYuanId=@HuiYuanId";
            DbCommand cmd = _db.GetSqlStringCommand(sql);

            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }
        #endregion

        #region IHuiYuan 成员
        /// <summary>
        /// 是否存在相同的用户名
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool IsExistsUsername(string companyId, string username)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsUsername);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "Username", DbType.String, username);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// 新增、修改会员信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int HuiYuan_CU(EyouSoft.Model.YlStructure.MHuiYuanInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HuiYuan_CU");
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, info.HuiYuanId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@Username", DbType.String, info.Username);
            _db.AddInParameter(cmd, "@MD5Password", DbType.String, info.MD5Password);
            _db.AddInParameter(cmd, "@XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "@XingBie", DbType.Byte, info.XingBie);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@YouXiang", DbType.String, info.YouXiang);
            _db.AddInParameter(cmd, "@Status", DbType.Byte, info.Status);
            _db.AddInParameter(cmd, "@TuXiang", DbType.String, info.TuXiang);
            _db.AddInParameter(cmd, "@ZhuCeShiJian", DbType.DateTime, info.ZhuCeShiJian);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@PingTaiLeiXing", DbType.Byte, info.PingTaiLeiXing);
            _db.AddInParameter(cmd, "@OpenID", DbType.String, info.OpenID);
            _db.AddInParameter(cmd, "@KeYongJiFen", DbType.Decimal, info.KeYongJiFen);
            _db.AddInParameter(cmd, "@GuoJiaId", DbType.Int32, info.GuoJiaId);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@XianQuId", DbType.Int32, info.XianQuId);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@GuoJi", DbType.String, info.GuoJi);
            _db.AddInParameter(cmd, "@ShengRi", DbType.DateTime, info.ShengRi);
            _db.AddInParameter(cmd, "@KeYongJinE", DbType.Decimal, info.KeYongJinE);
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
        /// 获取会员信息
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string huiYuanId)
        {
            EyouSoft.Model.YlStructure.MHuiYuanInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHuiYuanInfo);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);

            this.GetHuiYuanInfo(cmd, ref info);

            return info;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名或者注册邮箱</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string companyId,string username,int fx)
        {
            EyouSoft.Model.YlStructure.MHuiYuanInfo info = null;
            string sql = " SELECT * FROM tbl_YL_WZ_HuiYuan WHERE companyid=@companyid ";
            if (fx == 0)
            {
                sql += " and (username=@username)";
            }
            else if (fx == 1)
            {
                sql += "  and (username=@username or youxiang=@username) ";
            }
            else
            {
                return null;
            }
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "companyid", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "username", DbType.String, username);

            this.GetHuiYuanInfo(cmd, ref info);

            return info;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="info"></param>
        private void GetHuiYuanInfo(DbCommand cmd,ref EyouSoft.Model.YlStructure.MHuiYuanInfo info)
        {
            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHuiYuanInfo();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.GuoJi = rdr["GuoJi"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.KeYongJiFen = rdr.GetDecimal(rdr.GetOrdinal("KeYongJiFen"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MD5Password = rdr["MD5Password"].ToString();
                    info.OpenID = rdr["OpenID"].ToString();
                    info.PingTaiLeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing)rdr.GetByte(rdr.GetOrdinal("PingTaiLeiXing"));
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.ShengRi = rdr.GetDateTime(rdr.GetOrdinal("ShengRi"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.TuXiang = rdr["TuXiang"].ToString();
                    info.Username = rdr["Username"].ToString();
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("XingBie"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.YouXiang = rdr["YouXiang"].ToString();
                    info.ZhuCeShiJian = rdr.GetDateTime(rdr.GetOrdinal("ZhuCeShiJian"));
                    info.KeYongJinE = rdr.GetDecimal(rdr.GetOrdinal("KeYongJinE"));
                }
            }

            if (info != null)
            {
                info.ShouCangShu = GetShouCangShu(info.HuiYuanId);
                info.DaiFuKuanDingDanShu = GetDaiFuKuanDingDanShu(info.HuiYuanId);
            }
        }

        /// <summary>
        /// 获取会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanInfo> GetHuiYuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanInfo>();
            string tableName = "tbl_YL_WZ_HuiYuan";
            string fields = "*";
            string orderByString = "ZhuCeShiJian DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.LianXiFangShi))
                {
                    sql.AppendFormat(" AND (DianHua LIKE '%{0}%' OR ShouJi LIKE '%{0}%') ", chaXun.LianXiFangShi);
                }
                if (!string.IsNullOrEmpty(chaXun.Username))
                {
                    sql.AppendFormat(" AND Username LIKE '%{0}%' ", chaXun.Username);
                }
                if (!string.IsNullOrEmpty(chaXun.XingMing))
                {
                    sql.AppendFormat(" AND XingMing LIKE '%{0}%' ", chaXun.XingMing);
                }
                if (chaXun.ZhuCeShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND ZhuCeShiJian>='{0}' ", chaXun.ZhuCeShiJian1.Value.AddSeconds(-1));
                }
                if (chaXun.ZhuCeShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND ZhuCeShiJian<='{0}' ", chaXun.ZhuCeShiJian2.Value.AddDays(1).AddSeconds(-1));
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanInfo();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.GuoJi = rdr["GuoJi"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.KeYongJiFen = rdr.GetDecimal(rdr.GetOrdinal("KeYongJiFen"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    //info.MD5Password=
                    info.OpenID = rdr["OpenID"].ToString();
                    info.PingTaiLeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing)rdr.GetByte(rdr.GetOrdinal("PingTaiLeiXing"));
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.ShengRi = rdr.GetDateTime(rdr.GetOrdinal("ShengRi"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.TuXiang = rdr["TuXiang"].ToString();
                    info.Username = rdr["Username"].ToString();
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("XingBie"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.YouXiang = rdr["YouXiang"].ToString();
                    info.ZhuCeShiJian = rdr.GetDateTime(rdr.GetOrdinal("ZhuCeShiJian"));
                    info.KeYongJinE = rdr.GetDecimal(rdr.GetOrdinal("KeYongJinE"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 写入常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertChangLvKe);
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, info.HuiYuanId);
            _db.AddInParameter(cmd, "@LvkeId", DbType.AnsiStringFixedLength, info.LvkeId);
            _db.AddInParameter(cmd, "@XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@ZhengJianLeiXing", DbType.Byte, info.ZhengJianLeiXing);
            _db.AddInParameter(cmd, "@ZhengJianHaoMa", DbType.String, info.ZhengJianHaoMa);
            _db.AddInParameter(cmd, "@ZhengJianYouXiaoQi", DbType.DateTime, info.ZhengJianYouXiaoQi);
            _db.AddInParameter(cmd, "@ChuShengRiQi", DbType.DateTime, info.ChuShengRiQi);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@GuoJiaId", DbType.Int32, info.GuoJiaId);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@XianQuId", DbType.Int32, info.XianQuId);
            _db.AddInParameter(cmd, "@ZhuangTai", DbType.Byte, info.ZhuangTai);
            _db.AddInParameter(cmd, "@GuoJiId", DbType.Int32, info.GuoJiId);
            _db.AddInParameter(cmd, "@GuoJi", DbType.String, info.GuoJi);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@YXQ1", DbType.String, info.YXQ1);
            _db.AddInParameter(cmd, "@YXQ2", DbType.String, info.YXQ2);
            _db.AddInParameter(cmd, "@YXQ3", DbType.String, info.YXQ3);
            _db.AddInParameter(cmd, "@SR1", DbType.String, info.SR1);
            _db.AddInParameter(cmd, "@SR2", DbType.String, info.SR2);
            _db.AddInParameter(cmd, "@SR3", DbType.String, info.SR3);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateChangLvKe);
            _db.AddInParameter(cmd, "@LvkeId", DbType.AnsiStringFixedLength, info.LvkeId);
            _db.AddInParameter(cmd, "@XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@ZhengJianLeiXing", DbType.Byte, info.ZhengJianLeiXing);
            _db.AddInParameter(cmd, "@ZhengJianHaoMa", DbType.String, info.ZhengJianHaoMa);
            _db.AddInParameter(cmd, "@ZhengJianYouXiaoQi", DbType.DateTime, info.ZhengJianYouXiaoQi);
            _db.AddInParameter(cmd, "@ChuShengRiQi", DbType.DateTime, info.ChuShengRiQi);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@GuoJiaId", DbType.Int32, info.GuoJiaId);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@XianQuId", DbType.Int32, info.XianQuId);
            _db.AddInParameter(cmd, "@ZhuangTai", DbType.Byte, info.ZhuangTai);
            _db.AddInParameter(cmd, "@GuoJiId", DbType.Int32, info.GuoJiId);
            _db.AddInParameter(cmd, "@GuoJi", DbType.String, info.GuoJi);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        public int DeleteChangLvKe(string companyId, string huiYuanId, string lvkeId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteChangLvKe);
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "@LvkeId", DbType.AnsiStringFixedLength, lvkeId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取常旅客信息
        /// </summary>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo GetChangLvKeInfo(string lvkeId)
        {
            EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChangLvKeInfo);
            _db.AddInParameter(cmd, "@LvkeId", DbType.AnsiStringFixedLength, lvkeId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuShengRiQi"))) info.ChuShengRiQi = rdr.GetDateTime(rdr.GetOrdinal("ChuShengRiQi"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.GuoJi = rdr["GuoJi"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.TourStructure.VisitorType)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LvkeId = rdr["LvKeId"].ToString();
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.ZhengJianHaoMa = rdr["ZhengJianHaoMa"].ToString();
                    info.ZhengJianLeiXing = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)rdr.GetByte(rdr.GetOrdinal("ZhengJianLeiXing"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhengJianYouXiaoQi"))) info.ZhengJianYouXiaoQi = rdr.GetDateTime(rdr.GetOrdinal("ZhengJianYouXiaoQi"));
                    info.ZhuangTai = (EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus)rdr.GetByte(rdr.GetOrdinal("ZhuangTai"));
                    info.YXQ1 = rdr["YXQ1"].ToString();
                    info.YXQ2 = rdr["YXQ2"].ToString();
                    info.YXQ3 = rdr["YXQ3"].ToString();
                    info.SR1 = rdr["SR1"].ToString();
                    info.SR2 = rdr["SR2"].ToString();
                    info.SR3 = rdr["SR3"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取常旅客集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo> GetChangLvKes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChangLvKeChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo>();
            string tableName = "tbl_YL_WZ_HuiYuanChangLvKe";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" 1=1 ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND HuiYuanId='{0}' ", chaXun.HuiYuanId);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuShengRiQi"))) info.ChuShengRiQi = rdr.GetDateTime(rdr.GetOrdinal("ChuShengRiQi"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.GuoJi = rdr["GuoJi"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.TourStructure.VisitorType)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LvkeId = rdr["LvKeId"].ToString();
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.ZhengJianHaoMa = rdr["ZhengJianHaoMa"].ToString();
                    info.ZhengJianLeiXing = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)rdr.GetByte(rdr.GetOrdinal("ZhengJianLeiXing"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhengJianYouXiaoQi"))) info.ZhengJianYouXiaoQi = rdr.GetDateTime(rdr.GetOrdinal("ZhengJianYouXiaoQi"));
                    info.ZhuangTai = (EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus)rdr.GetByte(rdr.GetOrdinal("ZhuangTai"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 写入地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertDiZhi);
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, info.HuiYuanId);
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, info.DiZhiId);
            _db.AddInParameter(cmd, "@GuoJiaId", DbType.Int32, info.GuoJiaId);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@XianQuId", DbType.Int32, info.XianQuId);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@YouBian", DbType.String, info.YouBian);
            _db.AddInParameter(cmd, "@XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@IsMoRen", DbType.AnsiStringFixedLength, info.IsMoRen ? "1" : "0");

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateDiZhi);
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, info.DiZhiId);
            _db.AddInParameter(cmd, "@GuoJiaId", DbType.Int32, info.GuoJiaId);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@XianQuId", DbType.Int32, info.XianQuId);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@YouBian", DbType.String, info.YouBian);
            _db.AddInParameter(cmd, "@XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int DeleteDiZhi(string companyId, string huiYuanId, string diZhiId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteDiZhi);
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, diZhiId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo GetDiZhiInfo(string diZhiId)
        {
            EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDiZhiInfo);
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, diZhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo();

                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.DiZhiId = rdr["DiZhiId"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.YouBian = rdr["YouBian"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取地址集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo> GetDiZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDiZhiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo>();
            string tableName = "tbl_YL_WZ_HuiYuanDiZhi";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat(" CompanyId='{0}' AND IsXianShi='1' ", companyId);
            sql.Append(" IsXianShi='1' ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND HuiYuanId='{0}' ", chaXun.HuiYuanId);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo();

                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.DiZhiId = rdr["DiZhiId"].ToString();
                    info.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.YouBian = rdr["YouBian"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置会员默认地址，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int SheZhiMoRenDiZhi(string huiYuanId, string diZhiId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SheZhiMoRenDiZhi);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "DiZhiId", DbType.AnsiStringFixedLength, diZhiId);

            return DbHelper.ExecuteSql(cmd, _db) > 0 ? 1 : -100;
        }

        /// <summary>
        /// 设置会员常旅客状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvKeId">常旅客编号</param>
        /// <param name="status">常旅客状态</param>
        /// <returns></returns>
        public int SheZhiChangLvKeStatus(string huiYuanId, string lvKeId, EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus status)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SheZhiChangLvKeStatus);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "LvKeId", DbType.AnsiStringFixedLength, lvKeId);
            _db.AddInParameter(cmd, "ZhuangTai", DbType.Byte, status);

            return DbHelper.ExecuteSql(cmd, _db) > 0 ? 1 : -100;
        }

        /// <summary>
        /// 写入游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertGongLue);
            _db.AddInParameter(cmd, "@GongLueId", DbType.AnsiStringFixedLength, info.GongLueId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, info.RiQiId);
            _db.AddInParameter(cmd, "@IsShenHe", DbType.AnsiStringFixedLength, info.IsShenHe ? "1" : "0");
            _db.AddInParameter(cmd, "@ShenHeOperatorId", DbType.AnsiStringFixedLength, info.ShenHeOperatorId);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, DBNull.Value);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateGongLue);
            _db.AddInParameter(cmd, "@GongLueId", DbType.AnsiStringFixedLength, info.GongLueId);
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@IsShenHe", DbType.AnsiStringFixedLength, info.IsShenHe ? "1" : "0");

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public int DeleteGongLue(string companyId, string huiYuanId, string gongLueId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteGongLue);
            _db.AddInParameter(cmd, "@GongLueId", DbType.AnsiStringFixedLength, gongLueId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取游轮攻略信息
        /// </summary>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGongLueInfo GetGongLueInfo(string gongLueId)
        {
            EyouSoft.Model.YlStructure.MWzGongLueInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGongLueInfo);
            _db.AddInParameter(cmd, "@GongLueId", DbType.AnsiStringFixedLength, gongLueId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzGongLueInfo();
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GongLueId = rdr["GongLueId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IsShenHe = rdr["IsShenHe"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.ShenHeOperatorId = rdr["ShenHeOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShenHeTime"))) info.ShenHeTime = rdr.GetDateTime(rdr.GetOrdinal("ShenHeTime"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取游轮攻略集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGongLueInfo> GetGongLues(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongLueChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzGongLueInfo> items = new List<EyouSoft.Model.YlStructure.MWzGongLueInfo>();
            string tableName = "tbl_YL_WZ_GongLue";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND OperatorId='{0}' ", chaXun.HuiYuanId);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzGongLueInfo();
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GongLueId = rdr["GongLueId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IsShenHe = rdr["IsShenHe"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.ShenHeOperatorId = rdr["ShenHeOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShenHeTime"))) info.ShenHeTime = rdr.GetDateTime(rdr.GetOrdinal("ShenHeTime"));
                    info.XiLieId = rdr["XiLieId"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 审核游轮攻略
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public int ShenHeGongLue(string companyId, string operatorId, string gongLueId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_ShenHeGongLue);
            _db.AddInParameter(cmd, "@GongLueId", DbType.AnsiStringFixedLength, gongLueId);
            _db.AddInParameter(cmd, "@ShenHeOperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, DateTime.Now);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 写入用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertDianPing);
            _db.AddInParameter(cmd, "@DianPingId", DbType.AnsiStringFixedLength, info.DianPingId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, info.RiQiId);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@IsShenHe", DbType.AnsiStringFixedLength, info.IsShenHe ? "1" : "0");
            _db.AddInParameter(cmd, "@ShenHeOperatorId", DbType.AnsiStringFixedLength, info.ShenHeOperatorId);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, info.ShenHeTime);
            _db.AddInParameter(cmd, "@FenShu", DbType.Decimal, info.FenShu);
            _db.AddInParameter(cmd, "@DingDanLeiXing", DbType.Byte, info.DingDanLeiXing);
            _db.AddInParameter(cmd, "@IsNiMing", DbType.AnsiStringFixedLength, info.IsNiMing ? "1" : "0");
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateDianPing);
            _db.AddInParameter(cmd, "@DianPingId", DbType.AnsiStringFixedLength, info.DianPingId);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@IsShenHe", DbType.AnsiStringFixedLength, info.IsShenHe ? "1" : "0");
            _db.AddInParameter(cmd, "@FenShu", DbType.Decimal, info.FenShu);
            _db.AddInParameter(cmd, "@IsNiMing", DbType.AnsiStringFixedLength, info.IsNiMing ? "1" : "0");
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "@ShenHeOperatorId", DbType.AnsiStringFixedLength, info.ShenHeOperatorId);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, info.ShenHeTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public int DeleteDianPing(string companyId, string huiYuanId, string dianPingId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteDianPing);
            _db.AddInParameter(cmd, "@DianPingId", DbType.AnsiStringFixedLength, dianPingId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取用户点评信息
        /// </summary>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo(string dianPingId)
        {
            EyouSoft.Model.YlStructure.MWzDianPingInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDianPingInfo);
            _db.AddInParameter(cmd, "@DianPingId", DbType.AnsiStringFixedLength, dianPingId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzDianPingInfo();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianPingId = rdr["DianPingId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IsShenHe = rdr["IsShenHe"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.ShenHeOperatorId = rdr["ShenHeOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IssueTime"))) info.ShenHeTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.FenShu = rdr.GetDecimal(rdr.GetOrdinal("FenShu"));
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)rdr.GetByte(rdr.GetOrdinal("DingDanLeiXing"));
                    info.IsNiMing = rdr["IsNiMing"].ToString() == "1";
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取用户点评集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzDianPingInfo> GetDianPings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzDianPingChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzDianPingInfo> items = new List<EyouSoft.Model.YlStructure.MWzDianPingInfo>();
            string tableName = "tbl_YL_WZ_DianPing";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND OperatorId='{0}' ", chaXun.HuiYuanId);
                }
                if (!string.IsNullOrEmpty(chaXun.HangQiId))
                {
                    sql.AppendFormat(" AND HangQiId='{0}' ", chaXun.HangQiId);
                }
                if (chaXun.IsShenHe.HasValue)
                {
                    sql.AppendFormat(" AND IsShenHe='{0}' ", chaXun.IsShenHe.Value ? "1" : "0");
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzDianPingInfo();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianPingId = rdr["DianPingId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IsShenHe = rdr["IsShenHe"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.ShenHeOperatorId = rdr["ShenHeOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IssueTime"))) info.ShenHeTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.FenShu = rdr.GetDecimal(rdr.GetOrdinal("FenShu"));
                    info.IsNiMing = rdr["IsNiMing"].ToString() == "1";
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)rdr.GetByte(rdr.GetOrdinal("DingDanLeiXing"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 审核用户点评
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public int ShenHeDianPing(string companyId, string operatorId, string dianPingId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_ShenHeDianPing);
            _db.AddInParameter(cmd, "@DianPingId", DbType.AnsiStringFixedLength, dianPingId);
            _db.AddInParameter(cmd, "@ShenHeOperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, DateTime.Now);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 写入咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertWenDa);
            _db.AddInParameter(cmd, "@WenDaId", DbType.AnsiStringFixedLength, info.WenDaId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@WenBiaoTi", DbType.String, info.WenBiaoTi);
            _db.AddInParameter(cmd, "@WenNeiRong", DbType.String, info.WenNeiRong);
            _db.AddInParameter(cmd, "@WenShiJian", DbType.DateTime, info.WenShiJian);
            _db.AddInParameter(cmd, "@WenYongHuId", DbType.AnsiStringFixedLength, info.WenYongHuId);
            _db.AddInParameter(cmd, "@DaOperatorId", DbType.AnsiStringFixedLength, DBNull.Value);
            _db.AddInParameter(cmd, "@DaNeiRong", DbType.String, DBNull.Value);
            _db.AddInParameter(cmd, "@DaShiJian", DbType.DateTime, DBNull.Value);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@IsNiMing", DbType.AnsiStringFixedLength, info.IsNiMing ? "1" : "0");

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateWenDa);
            _db.AddInParameter(cmd, "@WenDaId", DbType.AnsiStringFixedLength, info.WenDaId);
            _db.AddInParameter(cmd, "@WenBiaoTi", DbType.String, info.WenBiaoTi);
            _db.AddInParameter(cmd, "@WenNeiRong", DbType.String, info.WenNeiRong);
            _db.AddInParameter(cmd, "@IsNiMing", DbType.AnsiStringFixedLength, info.IsNiMing ? "1" : "0");
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        public int DeleteWenDa(string companyId, string huiYuanId, string wenDaId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteWenDa);
            _db.AddInParameter(cmd, "@WenDaId", DbType.AnsiStringFixedLength, wenDaId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取咨询问答信息
        /// </summary>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzWenDaInfo GetWenDaInfo(string wenDaId)
        {
            EyouSoft.Model.YlStructure.MWzWenDaInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetWenDaInfo);
            _db.AddInParameter(cmd, "@WenDaId", DbType.AnsiStringFixedLength, wenDaId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzWenDaInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DaNeiRong = rdr["DaNeiRong"].ToString();
                    info.DaOperatorId = rdr["DaOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DaShiJian"))) info.DaShiJian = rdr.GetDateTime(rdr.GetOrdinal("DaShiJian"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.WenBiaoTi = rdr["WenBiaoTi"].ToString();
                    info.WenDaId = rdr["WenDaId"].ToString();
                    info.WenNeiRong = rdr["WenNeiRong"].ToString();
                    info.WenShiJian = rdr.GetDateTime(rdr.GetOrdinal("WenShiJian"));
                    info.WenYongHuId = rdr["WenYongHuId"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.IsNiMing = rdr["IsNiMing"].ToString() == "1";
                }
            }

            return info;
        }

        /// <summary>
        /// 获取咨询问答集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzWenDaInfo> GetWenDas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzWenDaChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzWenDaInfo> items = new List<EyouSoft.Model.YlStructure.MWzWenDaInfo>();
            string tableName = "tbl_YL_WZ_WenDa";
            string fields = "*";
            string orderByString = "WenShiJian DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND WenYongHuId='{0}' ", chaXun.HuiYuanId);
                }
                if (!string.IsNullOrEmpty(chaXun.HangQiId))
                {
                    sql.AppendFormat(" AND HangQiId='{0}' ", chaXun.HangQiId);
                }
                if (chaXun.IsHuiFu.HasValue)
                {
                    if (chaXun.IsHuiFu.Value)
                    {
                        sql.AppendFormat(" AND DaOperatorId IS NOT NULL ");
                    }
                    else
                    {
                        sql.AppendFormat(" AND DaOperatorId IS NULL ");
                    }
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzWenDaInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DaNeiRong = rdr["DaNeiRong"].ToString();
                    info.DaOperatorId = rdr["DaOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DaShiJian"))) info.DaShiJian = rdr.GetDateTime(rdr.GetOrdinal("DaShiJian"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.WenBiaoTi = rdr["WenBiaoTi"].ToString();
                    info.WenDaId = rdr["WenDaId"].ToString();
                    info.WenNeiRong = rdr["WenNeiRong"].ToString();
                    info.WenShiJian = rdr.GetDateTime(rdr.GetOrdinal("WenShiJian"));
                    info.WenYongHuId = rdr["WenYongHuId"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WenDaLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.IsNiMing = rdr["IsNiMing"].ToString() == "1";

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 回复咨询问答，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int HuiFuWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_HuiFuWenDa);
            _db.AddInParameter(cmd, "@WenDaId", DbType.AnsiStringFixedLength, info.WenDaId);
            _db.AddInParameter(cmd, "@DaOperatorId", DbType.AnsiStringFixedLength, info.DaOperatorId);
            _db.AddInParameter(cmd, "@DaNeiRong", DbType.String, info.DaNeiRong);
            _db.AddInParameter(cmd, "@DaShiJian", DbType.DateTime, info.DaShiJian.Value);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 写入收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShouCangJia(EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertShouCangJia);
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, info.HuiYuanId);
            _db.AddInParameter(cmd, "@ShouCangId", DbType.AnsiStringFixedLength, info.ShouCangId);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, info.ChanPinId);
            _db.AddInParameter(cmd, "@ShiJian", DbType.DateTime, info.ShiJian);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        public int DeleteShouCangJia(string companyId, string huiYuanId, string shouCangId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteShouCangJia);
            _db.AddInParameter(cmd, "@ShouCangId", DbType.AnsiStringFixedLength, shouCangId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取收藏夹信息
        /// </summary>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo GetShouCangJiaInfo(string shouCangId)
        {
            EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetShouCangJiaInfo);
            _db.AddInParameter(cmd, "@ShouCangId", DbType.AnsiStringFixedLength, shouCangId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo();

                    info.ChanPinId = rdr["ChanPinId"].ToString();
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.ShiJian = rdr.GetDateTime(rdr.GetOrdinal("ShiJian"));
                    info.ShouCangId = rdr["ShouCangId"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取收藏夹集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo> GetShouCangJias(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo>();
            string tableName = "view_YL_HuiYuanShouCangJia";
            string fields = "*";
            string orderByString = "ShiJian DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" HuiYuanId='{0}' ", huiYuanId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo();

                    info.ChanPinId = rdr["ChanPinId"].ToString();
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.ShiJian = rdr.GetDateTime(rdr.GetOrdinal("ShiJian"));
                    info.ShouCangId = rdr["ShouCangId"].ToString();

                    info.CPName = rdr["CPName"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    if (info.LeiXing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.海洋游轮 || info.LeiXing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.团购产品 || info.LeiXing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.长江游轮)
                    {
                        info.JiFen = info.JinE * rdr.GetDecimal(rdr.GetOrdinal("JiFenDuiHuanBiLi"));
                    }
                    else if (info.LeiXing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.积分兑换)
                    {
                        info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("SuoXuJiFen"));
                    }

                    info.IsYouXiao = rdr["IsYouXiao"].ToString() == "1";

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置会员密码，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="yuanmd5">原密码MD5</param>
        /// <param name="md5">新密码MD5</param>
        /// <returns></returns>
        public int SheZhiHuiYuanMiMa(string huiYuanId, string yuanmd5, string md5)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SheZhiHuiYuanMiMa);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "MD5", DbType.String, md5);
            _db.AddInParameter(cmd, "YuanMD5", DbType.String, yuanmd5);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 设置会员状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int SheZhiHuiYuanStatus(string huiYuanId, EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus status)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SheZhiHuiYuanStatus);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "Status", DbType.Byte, status);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 设置会员头像
        /// </summary>
        /// <param name="huiyuanid">会员编号</param>
        /// <param name="src">头像链接</param>
        /// <returns></returns>
        public int SheZhiHuiYuanTouXiang(string huiyuanid, string src)
        {
            var cmd = _db.GetSqlStringCommand("update tbl_YL_WZ_HuiYuan set tuxiang=@touxiang where huiyuanid=@huiyuanid");
            _db.AddInParameter(cmd, "huiyuanid", DbType.AnsiStringFixedLength, huiyuanid);
            _db.AddInParameter(cmd, "touxiang", DbType.String, src);

            return DbHelper.ExecuteSql(cmd, _db);
        }

        /// <summary>
        /// 获取会员积分明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo> GetHuiYuanJiFenMxs(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanJiFenMxChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo>();

            string tableName = "view_YL_HuiYuanJiFen";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" HuiYuanId='{0}' ", huiYuanId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo();

                    info.GuanLianId = rdr["GuanLianId"].ToString();
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiFen = rdr.IsDBNull(rdr.GetOrdinal("JiFen")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingXiId = rdr["MingXiId"].ToString();
                    info.CPName = rdr["CPName"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.IsTuanGou = rdr["IsTuanGou"].ToString() == "1";
                    info.CPId = rdr["CPId"].ToString();

                    if (info.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing.下单抵扣积分 || info.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing.下单累积积分)
                    {
                        info.HQLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("HangQiLeiXing"));
                    }

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取会员预存款明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo> GetHuiYuanYuCunKuanMxs(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo>();

            string tableName = "tbl_YL_WZ_HuiYuanYuCunKuan";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" HuiYuanId='{0}' ", huiYuanId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo();

                    info.GuanLianId = rdr["GuanLianId"].ToString();
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YuCunKuanMxLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingXiId = rdr["MingXiId"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取会员订单集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo> GetHuiYuanDingDans(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDingDanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo> items = new List<EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo>();

            string tableName = "view_YL_HuiYuanDingDan";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" HuiYuanId='{0}' ", huiYuanId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo();

                    info.CPId = rdr["CPId"].ToString();
                    info.CPName = rdr["CPName"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)rdr.GetByte(rdr.GetOrdinal("DingDanLeiXing"));

                    if (info.DingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单)
                    {
                        info.HQStatus = (EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                        info.HQLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("HangQiLeiXing"));
                    }
                    else if (info.DingDanLeiXing == EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单)
                    {
                        info.JFStatus = (EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                    }

                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsTuanGou = rdr["IsTuanGou"].ToString() == "1";
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)rdr.GetByte(rdr.GetOrdinal("FuKuanStatus"));
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取点评均分
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public Decimal GetDianPingJunFen(string hangQiId)
        {
            string sql = "SELECT AVG(FenShu) AS JunFen FROM tbl_YL_WZ_DianPing  WHERE HangQiId=@HangQiId AND IsShenHe='1'";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(0)) return rdr.GetDecimal(0);
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取用户点评信息，按订单编号获取
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo1(string dingDanId)
        {
            EyouSoft.Model.YlStructure.MWzDianPingInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDianPingInfo1);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzDianPingInfo();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianPingId = rdr["DianPingId"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IsShenHe = rdr["IsShenHe"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.ShenHeOperatorId = rdr["ShenHeOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IssueTime"))) info.ShenHeTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.FenShu = rdr.GetDecimal(rdr.GetOrdinal("FenShu"));
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)rdr.GetByte(rdr.GetOrdinal("DingDanLeiXing"));
                    info.IsNiMing = rdr["IsNiMing"].ToString() == "1";
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 设置会员账号
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public int SetHuiYuanUsername(string huiYuanId, string username)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HuiYuan_SheZhiYongHuMing");
            _db.AddInParameter(cmd, "@HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "@Username", DbType.String, username);
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
        /// 设置会员密码
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="md5pwd">md5密码</param>
        /// <returns></returns>
        public int SetHuiYuanMiMa(string huiYuanId, string md5pwd)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SetHuiYuanMiMa);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);
            _db.AddInParameter(cmd, "MD5", DbType.String, md5pwd);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }
        #endregion
    }
}
