//YL-在线支付 汪奇志 2014-04-19
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
    /// //YL-在线支付
    /// </summary>
    public class DZaiXianZhiFu : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IZaiXianZhiFu
    {
        #region static constants
        //static constants
        const string SQL_INSERT_Insert = "INSERT INTO [tbl_YL_ZaiXianZhiFu]([DingDanId],[DingDanLeiXing],[JiaoYiHao],[ApiJiaoYiHao],[JinE],[ZhiFuFangShi],[IsZhiFu],[ZhiFuTime])VALUES(@DingDanId,@DingDanLeiXing,@JiaoYiHao,@ApiJiaoYiHao,@JinE,@ZhiFuFangShi,@IsZhiFu,@ZhiFuTime)";
        const string SQL_SELECCT_IsZhiFu = "SELECT COUNT(*) FROM [tbl_YL_ZaiXianZhiFu] WHERE [DingDanId]=@DingDanId AND [DingDanLeiXing]=@DingDanLeiXing AND IsZhiFu='1'";
        const string SQL_SELECT_GetInfo = "SELECT * FROM [tbl_YL_ZaiXianZhiFu] WHERE [DingDanId]=@DingDanId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DZaiXianZhiFu()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IZaiXainZhiFu 成员
        /// <summary>
        /// 写入在线支付信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_Insert);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "DingDanLeiXing", DbType.Byte, info.DingDanLeiXing);
            _db.AddInParameter(cmd, "JiaoYiHao", DbType.AnsiStringFixedLength, info.JiaoYiHao);
            _db.AddInParameter(cmd, "ApiJiaoYiHao", DbType.String, info.ApiJiaoYiHao);
            _db.AddInParameter(cmd, "JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "ZhiFuFangShi", DbType.Byte, info.ZhiFuFangShi);
            _db.AddInParameter(cmd, "IsZhiFu", DbType.AnsiStringFixedLength, info.IsZhiFu ? "1" : "0");
            _db.AddInParameter(cmd, "ZhiFuTime", DbType.DateTime, info.ZhiFuTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取订单支付状态，返回真已支付，返回假未支付
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="dingDanLeiXing">订单类型</param>
        /// <returns></returns>
        public bool IsZhiFu(string dingDanId, EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing dingDanLeiXing)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECCT_IsZhiFu);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "DingDanLeiXing", DbType.Byte, dingDanLeiXing);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    if (rdr.GetInt32(0) >0) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据订单编号获取在线支付信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo GetInfo(string dingDanId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo info = null;

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo();
                    info.ApiJiaoYiHao = rdr["ApiJiaoYiHao"].ToString();
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanLeiXing = (EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing)rdr.GetByte(rdr.GetOrdinal("DingDanLeiXing"));
                    info.IdentityId = rdr.GetInt32(rdr.GetOrdinal("IdentityId"));
                    info.IsZhiFu = rdr["IsZhiFu"].ToString() == "1";
                    info.JiaoYiHao = rdr["JiaoYiHao"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.ZhiFuFangShi = (EyouSoft.Model.EnumType.YlStructure.ZaiXianZhiFuFangShi)rdr.GetByte(rdr.GetOrdinal("ZhiFuFangShi"));
                    info.ZhiFuTime = rdr.GetDateTime(rdr.GetOrdinal("ZhiFuTime"));
                }
            }

            return info;
        }
        #endregion
    }
}
