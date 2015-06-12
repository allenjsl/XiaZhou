using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit;
using EyouSoft.Model.EnumType.YlStructure;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 游轮网站会员登录
    /// </summary>
    public class DYlHuiYuanLogin : DALBase, IYlHuiYuanLogin
    {
        #region static constants
        //static constants
        private string SQL_SELECT_Login = "SELECT *,0 AS DaiFuKuanDingDanShu,0 AS ShouCangShu FROM tbl_YL_WZ_HuiYuan WHERE CompanyId=@CompanyId AND Username=@Username AND MD5Password=@MD5Password";
        private string SQL_SELECT_Login1 = "SELECT *,0 AS DaiFuKuanDingDanShu,0 AS ShouCangShu FROM tbl_YL_WZ_HuiYuan WHERE HuiYuanId=@HuiYuanId";
        const string SQL_SELECT_GetLatestLoginTime = "SELECT TOP(1) * FROM tbl_YL_WZ_HuiYuanDengLuLiShi WHERE HuiYuanId=@HuiYuanId";
        const string SQL_INSERT_LoginLogwr = "INSERT INTO [tbl_YL_WZ_HuiYuanDengLuLiShi]([JiLuId],[HuiYuanId],[ShiJian],[CompanyId],[Ip],[Client],[LeiXing])VALUES(@JiLuId,@HuiYuanId,@ShiJian,@CompanyId,@Ip,@Client,@LeiXing)";
        const string SQL_SELECT_GetYuMingInfo = "SELECT * FROM tbl_YL_WZ_YuMing WHERE YuMing=@YuMing";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DYlHuiYuanLogin()
        {
            _db = SystemStore;
        }
        #endregion        

        #region private members
        /// <summary>
        /// get latestlogintime
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="huiYuanId"></param>
        /// <returns></returns>
        DateTime? GetLatestLoginTime(string companyId, string huiYuanId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetLatestLoginTime);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetDateTime(rdr.GetOrdinal("ShiJian"));
                }
            }

            return null;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        EyouSoft.Model.SSOStructure.MYlHuiYuanInfo ReadHuiYuanInfo(DbCommand cmd)
        {
            EyouSoft.Model.SSOStructure.MYlHuiYuanInfo info = null;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.SSOStructure.MYlHuiYuanInfo();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DianHua = rdr["DianHua"].ToString();
                    info.HuiYuanId = rdr["HuiYuanId"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.OpenID = rdr["OpenID"].ToString();
                    info.PingTaiLeiXing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing)rdr.GetByte(rdr.GetOrdinal("PingTaiLeiXing"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.Username = rdr["Username"].ToString();
                    info.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("XingBie"));
                    info.XingMing = rdr["XingMing"].ToString();
                    info.YouXiang = rdr["YouXiang"].ToString();
                    info.ZhuCeShiJian = rdr.GetDateTime(rdr.GetOrdinal("ZhuCeShiJian"));                    
                }
            }

            if (info != null)
            {
                info.LatestLoginTime = GetLatestLoginTime(info.CompanyId, info.HuiYuanId);
            }

            return info;
        }
        #endregion

        #region IYlHuiYuanLogin 成员
        /// <summary>
        /// 会员登录，根据系统公司编号、用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        public EyouSoft.Model.SSOStructure.MYlHuiYuanInfo Login(string companyId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Login);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "Username", DbType.String, username);
            _db.AddInParameter(cmd, "MD5Password", DbType.String, pwd.MD5Password);

            return ReadHuiYuanInfo(cmd);
        }

        /// <summary>
        /// 会员登录，根据用户编号获取用户信息
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SSOStructure.MYlHuiYuanInfo Login(string huiYuanId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Login1);
            _db.AddInParameter(cmd, "HuiYuanId", DbType.AnsiStringFixedLength, huiYuanId);

            return ReadHuiYuanInfo(cmd);
        }

        /// <summary>
        /// 写会员登录日志
        /// </summary>
        /// <param name="info">登录会员信息</param>
        public void LoginLogwr(EyouSoft.Model.SSOStructure.MYlHuiYuanInfo info, byte leiXing)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_LoginLogwr);
            _db.AddInParameter(cmd, "JiLuId", DbType.String, Guid.NewGuid().ToString());
            _db.AddInParameter(cmd, "HuiYuanId", DbType.String, info.HuiYuanId);
            _db.AddInParameter(cmd, "ShiJian", DbType.String, info.LoginTime);
            _db.AddInParameter(cmd, "CompanyId", DbType.String, info.CompanyId);
            _db.AddInParameter(cmd, "Ip", DbType.String, Utils.GetRemoteIP());
            _db.AddInParameter(cmd, "Client", DbType.String, new EyouSoft.Toolkit.BrowserInfo().ToJsonString());
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, leiXing);

            DbHelper.ExecuteSql(cmd, _db);
        }

        /// <summary>
        /// 获取域名信息
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
        #endregion
    }
}
