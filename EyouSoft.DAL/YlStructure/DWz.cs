// 游轮网站相关 汪奇志 2014-03-27
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
    /// 游轮网站相关
    /// </summary>
    public class DWz : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IWz
    {
        #region static constants
        //static constants
        const string SQL_INSERT_UPDATE_SheZhiKvInfo = "IF EXISTS(SELECT 1 FROM tbl_YL_WZ_KV WHERE CompanyId=@CompanyId AND K=@K) UPDATE tbl_YL_WZ_KV SET V=@V WHERE CompanyId=@CompanyId AND K=@K ELSE INSERT INTO [tbl_YL_WZ_KV]([CompanyId],[K],[V],[OperatorId],[IssueTime])VALUES(@CompanyId,@K,@V,@OperatorId,@IssueTime)";
        const string SQL_SELECT_GetKvInfo = "SELECT * FROM tbl_YL_WZ_KV WHERE CompanyId=@CompanyId AND K=@K";
        const string SQL_SELECT_GetYuMingInfo = "SELECT * FROM tbl_YL_WZ_YuMing WHERE YuMing=@YuMing";
        const string SQL_INSERT_InsertYouQingLianJie = "INSERT INTO [tbl_YL_WZ_YouQingLianJie]([LianJieId],[CompanyId],[MingCheng],[Filepath],[LeiXing],[Url],[IssueTime],[OperatorId]) VALUES(@LianJieId,@CompanyId,@MingCheng,@Filepath,@LeiXing,@Url,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateYouQingLianJie = "UPDATE [tbl_YL_WZ_YouQingLianJie] SET [MingCheng] = @MingCheng,[Filepath] = @Filepath,[LeiXing] = @LeiXing,[Url]=@Url WHERE [LianJieId] = @LianJieId";
        const string SQL_DELETE_DeleteYouQingLianJie = "DELETE FROM tbl_YL_WZ_YouQingLianJie WHERE CompanyId=@CompanyId AND LianJieId=@LianJieId";
        const string SQL_SELECT_GetYouQingLianJieInfo = "SELECT * FROM tbl_YL_WZ_YouQingLianJie WHERE LianJieId=@LianJieId";
        const string SQL_INSERT_InsertGuangGao = "INSERT INTO [tbl_YL_WZ_GuangGao]([GuangGaoId],[CompanyId],[WeiZhi],[MingCheng],[Filepath],[Url],[XiangXiNeiRong],[IssueTime],[OperatorId],[PaiXuId])VALUES(@GuangGaoId,@CompanyId,@WeiZhi,@MingCheng,@Filepath,@Url,@XiangXiNeiRong,@IssueTime,@OperatorId,@PaiXuId)";
        const string SQL_UPDATE_UpdateGuangGao = "UPDATE [tbl_YL_WZ_GuangGao] SET [WeiZhi] = @WeiZhi,[MingCheng] = @MingCheng,[Filepath] = @Filepath,[Url] = @Url,[XiangXiNeiRong] = @XiangXiNeiRong,[PaiXuId]=@PaiXuId WHERE [GuangGaoId] = @GuangGaoId";
        const string SQL_DELETE_DeleteGuangGao = "DELETE FROM [tbl_YL_WZ_GuangGao] WHERE CompanyId=@CompanyId AND GuangGaoId=@GuangGaoId ";
        const string SQL_SELECT_GetGuangGaoInfo = "SELECT * FROM tbl_YL_WZ_GuangGao WHERE GuangGaoId=@GuangGaoId";
        const string SQL_INSERT_InsertZiXun = "INSERT INTO [tbl_YL_WZ_ZiXun]([ZiXunId],[CompanyId],[LeiXing],[BiaoTi],[NeiRong],[IssueTime],[OperatorId])VALUES(@ZiXunId,@CompanyId,@LeiXing,@BiaoTi,@NeiRong,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateZiXun = "UPDATE [tbl_YL_WZ_ZiXun] SET [LeiXing] = @LeiXing,[BiaoTi] = @BiaoTi,[NeiRong] = @NeiRong WHERE [ZiXunId] = @ZiXunId";
        const string SQL_DELETE_DeleteZiXun = "DELETE FROM tbl_YL_WZ_ZiXun WHERE CompanyId=@CompanyId AND ZiXunId=@ZiXunId";
        const string SQL_SELECT_GetZiXunInfo = "SELECT * FROM tbl_YL_WZ_ZiXun WHERE ZiXunId=@ZiXunId";
        const string SQL_INSERT_InsertGongSiRongYu = "INSERT INTO [tbl_YL_WZ_GongSiRongYu]([RongYuId],[CompanyId],[MingCheng],[Filepath],[XiangXiJieShao],[IssueTime],[OperatorId])VALUES(@RongYuId,@CompanyId,@MingCheng,@Filepath,@XiangXiJieShao,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateGongSiRongYu = "UPDATE [tbl_YL_WZ_GongSiRongYu] SET [MingCheng] = @MingCheng,[Filepath] = @Filepath,[XiangXiJieShao] = @XiangXiJieShao WHERE [RongYuId] = @RongYuId";
        const string SQL_DELETE_DeleteGongSiRongYu = "DELETE FROM tbl_YL_WZ_GongSiRongYu WHERE CompanyId=@CompanyId AND RongYuId=@RongYuId";
        const string SQL_SELECT_GetGongSiRongYuInfo = "SELECT * FROM tbl_YL_WZ_GongSiRongYu WHERE RongYuId=@RongYuId";
        const string SQL_INSERT_InsertYuanGongFengCai = "INSERT INTO [tbl_YL_WZ_YuanGongFengCai]([FengCaiId],[CompanyId],[MingCheng],[Filepath],[XiangXiJieShao],[IssueTime],[OperatorId]) VALUES(@FengCaiId,@CompanyId,@MingCheng,@Filepath,@XiangXiJieShao,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateYuanGongFengCai = "UPDATE [tbl_YL_WZ_YuanGongFengCai] SET [MingCheng] = @MingCheng,[Filepath] = @Filepath,[XiangXiJieShao] = @XiangXiJieShao WHERE [FengCaiId] = @FengCaiId";
        const string SQL_DELETE_DeleteYuanGongFengCai = "DELETE FROM tbl_YL_WZ_YuanGongFengCai WHERE CompanyId=@CompanyId AND FengCaiId=@FengCaiId";
        const string SQL_SELECT_GetYuanGongFengCaiInfo = "SELECT * FROM tbl_YL_WZ_YuanGongFengCai WHERE FengCaiId=@FengCaiId";
        const string SQL_INSERT_InsertZhaoPinGangWei = "INSERT INTO [tbl_YL_WZ_ZhaoPinGangWei]([GangWeiId],[CompanyId],[MingCheng],[XiangXiJieShao],[IssueTime],[OperatorId])VALUES(@GangWeiId,@CompanyId,@MingCheng,@XiangXiJieShao,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateZhaoPinGangWei = "UPDATE [tbl_YL_WZ_ZhaoPinGangWei] SET [MingCheng] = @MingCheng,[XiangXiJieShao] = @XiangXiJieShao WHERE [GangWeiId] = @GangWeiId";
        const string SQL_DELETE_DeleteZhaoPinGangWei = "DELETE FROM tbl_YL_WZ_ZhaoPinGangWei WHERE CompanyId=@CompanyId AND GangWeiId=@GangWeiId";
        const string SQL_SELECT_GetZhaoPinGangWeiInfo = "SELECT * FROM tbl_YL_WZ_ZhaoPinGangWei WHERE GangWeiId=@GangWeiId";
        const string SQL_INSERT_InsertHuiYiAnLi = "INSERT INTO [tbl_YL_WZ_HuiYiAnLi]([AnLiId],[CompanyId],[GongSiId],[XiLieId],[ChuanZhiId],[LeiXing],[MingCheng],[JiaGe],[RenShu],[DanWei],[ShiJian],[ShiJian1],[ShiJian2],[NeiRong],[Filepath],[IssueTime],[OperatorId])VALUES(@AnLiId,@CompanyId,@GongSiId,@XiLieId,@ChuanZhiId,@LeiXing,@MingCheng,@JiaGe,@RenShu,@DanWei,@ShiJian,@ShiJian1,@ShiJian2,@NeiRong,@Filepath,@IssueTime,@OperatorId)";
        const string SQL_UPDATE_UpdateHuiYiAnLi = "UPDATE [tbl_YL_WZ_HuiYiAnLi] SET [GongSiId] = @GongSiId,[XiLieId] = @XiLieId,[ChuanZhiId] = @ChuanZhiId,[LeiXing] = @LeiXing,[MingCheng] = @MingCheng,[JiaGe] = @JiaGe,[RenShu] = @RenShu,[DanWei] = @DanWei,[ShiJian] = @ShiJian,[ShiJian1] = @ShiJian1,[ShiJian2] = @ShiJian2,[NeiRong] = @NeiRong,[Filepath] = @Filepath WHERE [AnLiId] = @AnLiId";
        const string SQL_DELETE_DeleteHuiYiAnLi = "DELETE FROM tbl_YL_WZ_HuiYiAnLi WHERE CompanyId=@CompanyId AND AnLiId=@AnLiId";
        const string SQL_SELECT_GetHuiYiAnLiInfo = "SELECT * FROM tbl_YL_WZ_HuiYiAnLi WHERE AnLiId=@AnLiId";
        const string SQL_INSERT_InsertHuiYiShenQing = "INSERT INTO [tbl_YL_WZ_HuiYiShenQing]([ShenQingId],[CompanyId],[GuiMo],[YuJiShiJian],[LeiXing],[LxrXingMing],[LxrShouJi],[LxrYouXiang],[LxrDiZhi],[LxrGuoJiaId],[LxrShengFenId],[LxrChengShiId],[LxrXianQuId],[HangYeMingCheng],[HangYeLxShouJi],[IssueTime])VALUES(@ShenQingId,@CompanyId,@GuiMo,@YuJiShiJian,@LeiXing,@LxrXingMing,@LxrShouJi,@LxrYouXiang,@LxrDiZhi,@LxrGuoJiaId,@LxrShengFenId,@LxrChengShiId,@LxrXianQuId,@HangYeMingCheng,@HangYeLxShouJi,@IssueTime)";
        const string SQL_DELETE_DeleteHuiYiShenQing = "DELETE FROM tbl_YL_WZ_HuiYiShenQing WHERE ShenQingId=@ShenQingId";
        const string SQL_SELECT_GetHuiYiShenQingInfo = "SELECT * FROM tbl_YL_WZ_HuiYiShenQing WHERE ShenQingId=@ShenQingId";
        const string SQL_UPDATE_HuiYiShenQingChuLi = "UPDATE tbl_YL_WZ_HuiYiShenQing SET ChuLiBeiZhu=@ChuLiBeiZhu,ChuLiOperatorId=@ChuLiOperatorId,ChuLiShiJian=@ChuLiShiJian WHERE ShenQingId=@ShenQingId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DWz()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private Model.YlStructure.MWzYiJianFanKuiInfo GetWZYiJianFanKui(IDataReader r)
        {
            return new Model.YlStructure.MWzYiJianFanKuiInfo()
            {
                YiJianId = r["YiJianId"].ToString(),
                CompanyId = r["CompanyId"].ToString(),
                LeiXing = (Model.EnumType.YlStructure.YiJianFanKuiLeiXing)r.GetByte(r.GetOrdinal("LeiXing")),
                FilePath = r["FilePath"].ToString(),
                MiaoShu = r["MiaoShu"].ToString(),
                RemoteIP = r["RemoteIP"].ToString(),
                IssueTime = r.GetDateTime(r.GetOrdinal("IssueTime")),
                Client = r["Client"].ToString(),
                OperatorId = r["OperatorId"].ToString()
            };
        }

        #endregion

        #region IWz 成员
        /// <summary>
        /// 设置网站KV信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiKvInfo(EyouSoft.Model.YlStructure.MWzKvInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_UPDATE_SheZhiKvInfo);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "K", DbType.String, (int)info.K);
            _db.AddInParameter(cmd, "V", DbType.String, info.V);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取网站KV信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="k">key</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzKvInfo GetKvInfo(string companyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey k)
        {
            var info = new EyouSoft.Model.YlStructure.MWzKvInfo();
            info.CompanyId = companyId;
            info.K = k;
            info.V = string.Empty;

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetKvInfo);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength,companyId);
            _db.AddInParameter(cmd, "K", DbType.String, (int)k);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.V = rdr["V"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取网站域名信息
        /// </summary>
        /// <param name="yuMing">域名</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYuMingInfo GetYuMingInfo(string yuMing)
        {
            var info = new EyouSoft.Model.YlStructure.MWzYuMingInfo();
            info.YuMing = yuMing;
            info.CompanyId = string.Empty;

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYuMingInfo);
            _db.AddInParameter(cmd, "YuMing", DbType.String, yuMing);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.ErpYuMing = rdr["ErpYuMing"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 写入友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertYouQingLianJie);
            _db.AddInParameter(cmd, "LianJieId", DbType.AnsiStringFixedLength, info.LianJieId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "Url", DbType.String, info.Url);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateYouQingLianJie);
            _db.AddInParameter(cmd, "LianJieId", DbType.AnsiStringFixedLength, info.LianJieId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "Url", DbType.String, info.Url);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="compamyId">公司编号</param>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        public int DeleteYouQingLianJie(string compamyId, string lianJieId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteYouQingLianJie);

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, compamyId);
            _db.AddInParameter(cmd, "LianJieId", DbType.AnsiStringFixedLength, lianJieId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取友情链接信息
        /// </summary>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo GetYouQingLianJieInfo(string lianJieId)
        {
            EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYouQingLianJieInfo);
            _db.AddInParameter(cmd, "LianJieId", DbType.AnsiStringFixedLength, lianJieId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LianJieId = lianJieId;
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.Url = rdr["Url"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> items = new List<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo>();
            string tableName = "tbl_YL_WZ_YouQingLianJie";
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
                else
                {
                    sql.AppendFormat(" AND LeiXing IN ({0},{1}) ", (int)Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.文本, (int)Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.图文);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                items = GetYouQingLianJies(rdr);
            }

            return items;
        }

        /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId,EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> items = new List<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo>();
            StringBuilder sql = new StringBuilder("select * from tbl_YL_WZ_YouQingLianJie where");
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                else
                {
                    sql.AppendFormat(" AND LeiXing IN ({0},{1}) ", (int)Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.文本, (int)Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.图文);
                }
            }

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,_db))
            {
                items = GetYouQingLianJies(rdr);
            }

            return items;
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(IDataReader rdr)
        {
            IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> items = new List<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo>();
            while (rdr.Read())
            {
                var info = new EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo();
                info.CompanyId = rdr["CompanyId"].ToString();
                info.Filepath = rdr["Filepath"].ToString();
                info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                info.LianJieId = rdr["LianJieId"].ToString();
                info.MingCheng = rdr["MingCheng"].ToString();
                info.OperatorId = rdr["OperatorId"].ToString();
                info.Url = rdr["Url"].ToString();

                items.Add(info);
            }
            return items;
        }

        /// <summary>
        /// 新增广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertGuangGao);
            _db.AddInParameter(cmd, "GuangGaoId", DbType.AnsiStringFixedLength, info.GuangGaoId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "WeiZhi", DbType.Byte, info.WeiZhi);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "Url", DbType.String, info.Url);
            _db.AddInParameter(cmd, "XiangXiNeiRong", DbType.String, info.XiangXiNeiRong);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "PaiXuId", DbType.Int32, info.PaiXuId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateGuangGao);
            _db.AddInParameter(cmd, "GuangGaoId", DbType.AnsiStringFixedLength, info.GuangGaoId);
            _db.AddInParameter(cmd, "WeiZhi", DbType.Byte, info.WeiZhi);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "Url", DbType.String, info.Url);
            _db.AddInParameter(cmd, "XiangXiNeiRong", DbType.String, info.XiangXiNeiRong);
            _db.AddInParameter(cmd, "PaiXuId", DbType.Int32, info.PaiXuId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        public int DeleteGuangGao(string companyId, string guangGaoId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteGuangGao);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "GuangGaoId", DbType.AnsiStringFixedLength, guangGaoId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGuangGaoInfo GetGuangGaoInfo(string guangGaoId)
        {
            EyouSoft.Model.YlStructure.MWzGuangGaoInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGuangGaoInfo);
            _db.AddInParameter(cmd, "GuangGaoId", DbType.AnsiStringFixedLength, guangGaoId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzGuangGaoInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GuangGaoId = rdr["GuangGaoId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.Url = rdr["Url"].ToString();
                    info.WeiZhi = (EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi)rdr.GetByte(rdr.GetOrdinal("WeiZhi"));
                    info.XiangXiNeiRong = rdr["XiangXiNeiRong"].ToString();
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取广告信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGuangGaoInfo> GetGuangGaos(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzGuangGaoInfo> items = new List<EyouSoft.Model.YlStructure.MWzGuangGaoInfo>();
            string tableName = "tbl_YL_WZ_GuangGao";
            string fields = "*";
            string orderByString = "PaiXuId ASC,IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.WeiZhi.HasValue)
                {
                    sql.AppendFormat(" AND WeiZhi={0} ", (int)chaXun.WeiZhi.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzGuangGaoInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GuangGaoId = rdr["GuangGaoId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.Url = rdr["Url"].ToString();
                    info.WeiZhi = (EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi)rdr.GetByte(rdr.GetOrdinal("WeiZhi"));
                    info.XiangXiNeiRong = rdr["XiangXiNeiRong"].ToString();
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertZiXun);
            _db.AddInParameter(cmd, "ZiXunId", DbType.AnsiStringFixedLength, info.ZiXunId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateZiXun);
            _db.AddInParameter(cmd, "ZiXunId", DbType.AnsiStringFixedLength, info.ZiXunId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        public int DeleteZiXun(string companyId, string ziXunId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteZiXun);
            _db.AddInParameter(cmd, "ZiXunId", DbType.AnsiStringFixedLength, ziXunId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength,companyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取资讯信息
        /// </summary>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzZiXunInfo GetZiXunInfo(string ziXunId)
        {
            EyouSoft.Model.YlStructure.MWzZiXunInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetZiXunInfo);
            _db.AddInParameter(cmd, "ZiXunId", DbType.AnsiStringFixedLength, ziXunId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzZiXunInfo();

                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ZiXunId = rdr["ZiXunId"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取资讯信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzZiXunInfo> GetZiXuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzZiXunInfo> items = new List<EyouSoft.Model.YlStructure.MWzZiXunInfo>();
            string tableName = "tbl_YL_WZ_ZiXun";
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
                if (!string.IsNullOrEmpty(chaXun.BiaoTi))
                {
                    sql.AppendFormat(" AND BiaoTi LIKE '%{0}%' ", chaXun.BiaoTi);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzZiXunInfo();

                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ZiXunId = rdr["ZiXunId"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertGongSiRongYu);
            _db.AddInParameter(cmd, "@RongYuId", DbType.AnsiStringFixedLength, info.RongYuId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateGongSiRongYu);
            _db.AddInParameter(cmd, "@RongYuId", DbType.AnsiStringFixedLength, info.RongYuId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        public int DeleteGongSiRongYu(string companyId, string rongYuId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteGongSiRongYu);
            _db.AddInParameter(cmd, "@RongYuId", DbType.AnsiStringFixedLength, rongYuId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取公司荣誉信息
        /// </summary>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo GetGongSiRongYuInfo(string rongYuId)
        {
            EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGongSiRongYuInfo);
            _db.AddInParameter(cmd, "@RongYuId", DbType.AnsiStringFixedLength, rongYuId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RongYuId = rdr["RongYuId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取公司荣誉信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo> GetGongSiRongYus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongSiRongYuChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo> items = new List<EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo>();
            string tableName = "tbl_YL_WZ_GongSiRongYu";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RongYuId = rdr["RongYuId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertYuanGongFengCai);
            _db.AddInParameter(cmd, "@FengCaiId", DbType.AnsiStringFixedLength, info.FengCaiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateYuanGongFengCai);
            _db.AddInParameter(cmd, "@FengCaiId", DbType.AnsiStringFixedLength, info.FengCaiId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        public int DeleteYuanGongFengCai(string companyId, string fengCaiId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteYuanGongFengCai);
            _db.AddInParameter(cmd, "@FengCaiId", DbType.AnsiStringFixedLength, fengCaiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }
        
        /// <summary>
        /// 获取员工风采信息
        /// </summary>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo GetYuanGongFengCaiInfo(string fengCaiId)
        {
            EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYuanGongFengCaiInfo);
            _db.AddInParameter(cmd, "@FengCaiId", DbType.AnsiStringFixedLength, fengCaiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.FengCaiId = rdr["FengCaiId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取员工风采信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo> GetYuanGongFengCais(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYuanGongFengCaiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo> items = new List<EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo>();
            string tableName = "tbl_YL_WZ_YuanGongFengCai";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.FengCaiId = rdr["FengCaiId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertZhaoPinGangWei);
            _db.AddInParameter(cmd, "GangWeiId", DbType.AnsiStringFixedLength, info.GangWeiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateZhaoPinGangWei);
            _db.AddInParameter(cmd, "GangWeiId", DbType.AnsiStringFixedLength, info.GangWeiId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        public int DeleteZhaoPinGangWei(string companyId, string gangWeiId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteZhaoPinGangWei);
            _db.AddInParameter(cmd, "GangWeiId", DbType.AnsiStringFixedLength, gangWeiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取招聘岗位信息
        /// </summary>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo GetZhaoPinGangWeiInfo(string gangWeiId)
        {
            EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetZhaoPinGangWeiInfo);
            _db.AddInParameter(cmd, "GangWeiId", DbType.AnsiStringFixedLength, gangWeiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.GangWeiId = rdr["GangWeiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();

                }
            }

            return info;
        }

        /// <summary>
        /// 获取招聘岗位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo> GetZhaoPinGangWeis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo> items = new List<EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo>();
            string tableName = "tbl_YL_WZ_ZhaoPinGangWei";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.GangWeiId = rdr["GangWeiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertHuiYiAnLi);
            _db.AddInParameter(cmd, "AnLiId", DbType.AnsiStringFixedLength, info.AnLiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "JiaGe", DbType.String, info.JiaGe);
            _db.AddInParameter(cmd, "RenShu", DbType.String, info.RenShu);
            _db.AddInParameter(cmd, "DanWei", DbType.String, info.DanWei);
            _db.AddInParameter(cmd, "ShiJian", DbType.String, info.ShiJian);
            _db.AddInParameter(cmd, "ShiJian1", DbType.DateTime, info.ShiJian1);
            _db.AddInParameter(cmd, "ShiJian2", DbType.DateTime, info.ShiJian2);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateHuiYiAnLi);
            _db.AddInParameter(cmd, "AnLiId", DbType.AnsiStringFixedLength, info.AnLiId);
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "JiaGe", DbType.String, info.JiaGe);
            _db.AddInParameter(cmd, "RenShu", DbType.String, info.RenShu);
            _db.AddInParameter(cmd, "DanWei", DbType.String, info.DanWei);
            _db.AddInParameter(cmd, "ShiJian", DbType.String, info.ShiJian);
            _db.AddInParameter(cmd, "ShiJian1", DbType.DateTime, info.ShiJian1);
            _db.AddInParameter(cmd, "ShiJian2", DbType.DateTime, info.ShiJian2);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "Filepath", DbType.String, info.Filepath);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        public int DeleteHuiYiAnLi(string companyId, string anLiId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteHuiYiAnLi);
            _db.AddInParameter(cmd, "AnLiId", DbType.AnsiStringFixedLength, anLiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取会议案例信息
        /// </summary>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo GetHuiYiAnLiInfo(string anLiId)
        {
            EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHuiYiAnLiInfo);
            _db.AddInParameter(cmd, "AnLiId", DbType.AnsiStringFixedLength, anLiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo();
                    info.AnLiId = rdr["AnLiId"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DanWei = rdr["DanWei"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGe = rdr["JiaGe"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RenShu = rdr["RenShu"].ToString();
                    info.ShiJian = rdr["ShiJian"].ToString();
                    info.ShiJian1 = rdr.GetDateTime(rdr.GetOrdinal("ShiJian1"));
                    info.ShiJian2 = rdr.GetDateTime(rdr.GetOrdinal("ShiJian2"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                }
            }


            return info;
        }

        /// <summary>
        /// 获取会议案例信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo> GetHuiYiAnLis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiAnLiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo> items = new List<EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo>();
            string tableName = "tbl_YL_WZ_HuiYiAnLi";
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
                if (chaXun.ShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND (ShiJian1>='{0}' OR ShiJian2>='{0}') ", chaXun.ShiJian1.Value);
                }
                if (chaXun.ShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND (ShiJian1<='{0}' OR ShiJian2<='{0}' ) ", chaXun.ShiJian2.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo();
                    info.AnLiId = rdr["AnLiId"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DanWei = rdr["DanWei"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGe = rdr["JiaGe"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RenShu = rdr["RenShu"].ToString();
                    info.ShiJian = rdr["ShiJian"].ToString();
                    info.ShiJian1 = rdr.GetDateTime(rdr.GetOrdinal("ShiJian1"));
                    info.ShiJian2 = rdr.GetDateTime(rdr.GetOrdinal("ShiJian2"));
                    info.XiLieId = rdr["XiLieId"].ToString();
                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 写入会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHuiYiShenQing(EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertHuiYiShenQing);
            _db.AddInParameter(cmd, "ShenQingId", DbType.AnsiStringFixedLength, info.ShenQingId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "GuiMo", DbType.String, info.GuiMo);
            _db.AddInParameter(cmd, "YuJiShiJian", DbType.String, info.YuJiShiJian);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "LxrXingMing", DbType.String, info.LxrXingMing);
            _db.AddInParameter(cmd, "LxrShouJi", DbType.String, info.LxrShouJi);
            _db.AddInParameter(cmd, "LxrYouXiang", DbType.String, info.LxrYouXiang);
            _db.AddInParameter(cmd, "LxrDiZhi", DbType.String, info.LxrDiZhi);
            _db.AddInParameter(cmd, "LxrGuoJiaId", DbType.Int32, info.LxrGuoJiaId);
            _db.AddInParameter(cmd, "LxrShengFenId", DbType.Int32, info.LxrShengFenId);
            _db.AddInParameter(cmd, "LxrChengShiId", DbType.Int32, info.LxrChengShiId);
            _db.AddInParameter(cmd, "LxrXianQuId", DbType.Int32, info.LxrXianQuId);
            _db.AddInParameter(cmd, "HangYeMingCheng", DbType.String, info.HangYeMingCheng);
            _db.AddInParameter(cmd, "HangYeLxShouJi", DbType.String, info.HangYeLxShouJi);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        public int DeleteHuiYiShenQing(string companyId, string shenQingId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteHuiYiShenQing);
            _db.AddInParameter(cmd, "ShenQingId", DbType.AnsiStringFixedLength, shenQingId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取会议申请信息
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo GetHuiYiShenQingInfo(string shenQingId)
        {
            EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHuiYiShenQingInfo);
            _db.AddInParameter(cmd, "ShenQingId", DbType.AnsiStringFixedLength, shenQingId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.GuiMo = rdr["GuiMo"].ToString();
                    info.HangYeLxShouJi = rdr["HangYeLxShouJi"].ToString();
                    info.HangYeMingCheng = rdr["HangYeMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LxrChengShiId = rdr.GetInt32(rdr.GetOrdinal("LxrChengShiId"));
                    info.LxrDiZhi = rdr["LxrDiZhi"].ToString();
                    info.LxrGuoJiaId = rdr.GetInt32(rdr.GetOrdinal("LxrGuoJiaId"));
                    info.LxrShengFenId = rdr.GetInt32(rdr.GetOrdinal("LxrShengFenId"));
                    info.LxrShouJi = rdr["LxrShouJi"].ToString();
                    info.LxrXianQuId = rdr.GetInt32(rdr.GetOrdinal("LxrXianQuId"));
                    info.LxrXingMing = rdr["LxrXingMing"].ToString();
                    info.LxrYouXiang = rdr["LxrYouXiang"].ToString();
                    info.ShenQingId = rdr["ShenQingId"].ToString();
                    info.YuJiShiJian = rdr["YuJiShiJian"].ToString();
                    info.ChuLiBeiZhu = rdr["ChuLiBeiZhu"].ToString();
                    info.ChuLiOperatorId = rdr["ChuLiOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuLiShiJian"))) info.ChuLiShiJian = rdr.GetDateTime(rdr.GetOrdinal("ChuLiShiJian"));

                }
            }

            return info;
        }

        /// <summary>
        /// 获取会议申请集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo> GetHuiYiShenQings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiShenQingChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo> items = new List<EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo>();
            string tableName = "tbl_YL_WZ_HuiYiShenQing";
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
                if (chaXun.ShenQingShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>='{0}' ", chaXun.ShenQingShiJian1.Value.AddMinutes(-1));
                }
                if (chaXun.ShenQingShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<='{0}' ", chaXun.ShenQingShiJian2.Value.AddDays(1).AddMinutes(-1));
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.GuiMo = rdr["GuiMo"].ToString();
                    info.HangYeLxShouJi = rdr["HangYeLxShouJi"].ToString();
                    info.HangYeMingCheng = rdr["HangYeMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.LxrChengShiId = rdr.GetInt32(rdr.GetOrdinal("LxrChengShiId"));
                    info.LxrDiZhi = rdr["LxrDiZhi"].ToString();
                    info.LxrGuoJiaId = rdr.GetInt32(rdr.GetOrdinal("LxrGuoJiaId"));
                    info.LxrShengFenId = rdr.GetInt32(rdr.GetOrdinal("LxrShengFenId"));
                    info.LxrShouJi = rdr["LxrShouJi"].ToString();
                    info.LxrXianQuId = rdr.GetInt32(rdr.GetOrdinal("LxrXianQuId"));
                    info.LxrXingMing = rdr["LxrXingMing"].ToString();
                    info.LxrYouXiang = rdr["LxrYouXiang"].ToString();
                    info.ShenQingId = rdr["ShenQingId"].ToString();
                    info.YuJiShiJian = rdr["YuJiShiJian"].ToString();
                    info.ChuLiBeiZhu = rdr["ChuLiBeiZhu"].ToString();
                    info.ChuLiOperatorId = rdr["ChuLiOperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuLiShiJian"))) info.ChuLiShiJian = rdr.GetDateTime(rdr.GetOrdinal("ChuLiShiJian"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 会议申请处理
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="beiZhu">备注</param>
        /// <returns></returns>
        public int HuiYiShenQingChuLi(string shenQingId, string operatorId, string beiZhu)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_HuiYiShenQingChuLi);
            _db.AddInParameter(cmd, "ChuLiBeiZhu", DbType.String, beiZhu);
            _db.AddInParameter(cmd, "ChuLiOperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "ChuLiShiJian", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(cmd, "ShenQingId", DbType.AnsiStringFixedLength, shenQingId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 网站意见反馈新增
        /// </summary>
        /// <param name="m">意见反馈实体</param>
        /// <returns>1：成功 0：失败</returns>
        public int AddWZYiJianFanKui(Model.YlStructure.MWzYiJianFanKuiInfo m)
        {
            var s = new StringBuilder();
            s.Append(" INSERT  INTO [dbo].[tbl_YL_WZ_YiJianFanKui]");
            s.Append("         ( [YiJianId] ,");
            s.Append("           [CompanyId] ,");
            s.Append("           [LeiXing] ,");
            s.Append("           [FilePath] ,");
            s.Append("           [MiaoShu] ,");
            s.Append("           [RemoteIP] ,");
            s.Append("           [IssueTime] ,");
            s.Append("           [OperatorId] ,");
            s.Append("           [Client]");
            s.Append("         )");
            s.Append(" VALUES  ( @YiJianId ,");
            s.Append("           @CompanyId ,");
            s.Append("           @LeiXing ,");
            s.Append("           @FilePath ,");
            s.Append("           @MiaoShu ,");
            s.Append("           @RemoteIP ,");
            s.Append("           @IssueTime ,");
            s.Append("           @OperatorId ,");
            s.Append("           @Client");
            s.Append("         )");

            var cmd = _db.GetSqlStringCommand(s.ToString());
            _db.AddInParameter(cmd, "YiJianId", DbType.AnsiStringFixedLength, m.YiJianId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, m.CompanyId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, (int)m.LeiXing);
            _db.AddInParameter(cmd, "FilePath", DbType.String, m.FilePath);
            _db.AddInParameter(cmd, "MiaoShu", DbType.String, m.MiaoShu);
            _db.AddInParameter(cmd, "RemoteIP", DbType.String, m.RemoteIP);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, m.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, m.OperatorId);
            _db.AddInParameter(cmd, "Client", DbType.String, m.Client);

            return DbHelper.ExecuteSql(cmd, _db);
        }

        /// <summary>
        /// 删除网站意见反馈
        /// </summary>
        /// <param name="yijianId"></param>
        /// <returns>0：失败 1：成功</returns>
        public int DelWZYiJianFanKui(string yijianId)
        {
            var s = new StringBuilder("DELETE FROM [dbo].[tbl_YL_WZ_YiJianFanKui] WHERE YiJianId=@YiJianId");
            var cmd = _db.GetSqlStringCommand(s.ToString());

            _db.AddInParameter(cmd, "YiJianId", DbType.AnsiStringFixedLength, yijianId);

            return DbHelper.ExecuteSql(cmd, _db);
        }

        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="yijianId">意见编号</param>
        /// <returns></returns>
        public Model.YlStructure.MWzYiJianFanKuiInfo GetWZYiJianFanKui(string yijianId)
        {
            Model.YlStructure.MWzYiJianFanKuiInfo m = null;
            var s = new StringBuilder("SELECT * FROM tbl_YL_WZ_YiJianFanKui WHERE YiJianId=@YiJianId");
            var cmd = _db.GetSqlStringCommand(s.ToString());
            _db.AddInParameter(cmd, "YiJianId", DbType.AnsiStringFixedLength, yijianId);

            using (var r = DbHelper.ExecuteReader(cmd, _db))
            {
               while (r.Read())
                { 
                   m = GetWZYiJianFanKui(r);
                }
            }
            return m;
        }

        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<Model.YlStructure.MWzYiJianFanKuiInfo> GetWZYiJianFanKui(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYiJianFanKuiChaXun chaXun)
        {
            var l=new List<Model.YlStructure.MWzYiJianFanKuiInfo>();
            var s = new StringBuilder();

            if (!string.IsNullOrEmpty(chaXun.CompanyId))
            {
                s.AppendFormat(" CompanyId='{0}'", chaXun.CompanyId);
            }
            if (chaXun.LeiXing.HasValue)
            {
                s.AppendFormat(" LeiXing={0}", (int)chaXun.LeiXing.Value);
            }
            using (IDataReader r = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_YL_WZ_YiJianFanKui", "YiJianId", "*", s.ToString(), "IssueTime DESC"))
            {
                while (r.Read())
                {
                    l.Add(GetWZYiJianFanKui(r));
                }
            }
            return l;
        }
        #endregion
    }
}
