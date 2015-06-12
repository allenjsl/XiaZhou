//预算表 汪奇志 2014-02-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.StatStructure
{
    /// <summary>
    /// 预算表
    /// </summary>
    public class DYuSuanBiao : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.StatStructure.IYuSuanBiao
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetShouRus = "SELECT OrderId,OrderCode,SellerName,BuyCompanyName,ConfirmMoney,ConfirmMoneyStatus,IssueTime,(Adults+Childs) AS ShiShouRenShu FROM tbl_TourOrder WHERE TourId=@TourId AND [Status]=4 ORDER BY IssueTime";
        const string SQL_SELECT_GetZhiChus = "SELECT PlanId,SourceName,CostDetail,Confirmation,PaymentType,[Type],OperatorName FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND [Status]=4 ORDER BY [Type]";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DYuSuanBiao()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IYuSuanBiao 成员
        /// <summary>
        /// 获取预算表信息信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计 [0:int:实收人数] [1:decimal:收入合计] [2:decimal:支出合计]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.StatStructure.MYuSuanBiaoInfo> GetYuSuanBiaos(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.StatStructure.MYuSuanBiaoChaXunInfo chaXun, out object[] heJi)
        {
            heJi = new object[] { 0, 0M, 0M };
            IList<EyouSoft.Model.StatStructure.MYuSuanBiaoInfo> items = new List<EyouSoft.Model.StatStructure.MYuSuanBiaoInfo>();

            string tableName = "view_TongJi_YuSuanBiao";
            string fields = "*";
            string paiXuString = "LDate DESC";
            string heJiString = "SUM(ShiShouRenShu) AS ShiShouRenShu,SUM(ShouRuJinE) AS ShouRuJinE,SUM(ZhiChuJinE) AS ZhiChuJinE";
            StringBuilder sql = new StringBuilder();

            #region sql
            sql.AppendFormat(" CompanyId='{0}' AND TourStatus IN(0,1,2,3,4,5,6,7,8,9,10,13,15) ", companyId);

            if (depts != null && depts.Length == 1 && depts[0] == -1)//查看自己
            {
                sql.AppendFormat(" AND SellerId='{0}' ", userId);
            }
            else
            {
                if (depts != null && depts.Length > 0)
                {
                    sql.AppendFormat(" AND( DeptId IN({0}) ", Utils.GetSqlIn<int>(depts));

                    if (!string.IsNullOrEmpty(userId))
                    {
                        sql.AppendFormat(" OR SellerId='{0}' ", userId);
                    }

                    sql.Append(" ) ");
                }
            }

            if (chaXun != null)
            {
                if (chaXun.ChuTuanERiQi.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ChuTuanERiQi.Value.AddDays(1));
                }
                if (chaXun.ChuTuanSRiQi.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.ChuTuanSRiQi.Value.AddDays(-1));
                }
                if (!string.IsNullOrEmpty(chaXun.XiaoShouYuanId))
                {
                    sql.AppendFormat(" AND SellerId='{0}' ", chaXun.XiaoShouYuanId);
                }
                else if (!string.IsNullOrEmpty(chaXun.XiaoShouYuanName))
                {
                    sql.AppendFormat(" AND SellerName LIKE '%{0}%' ", chaXun.XiaoShouYuanName);
                }
                if (!string.IsNullOrEmpty(chaXun.TourCode))
                {
                    sql.AppendFormat(" AND TourCode LIKE '%{0}%' ", chaXun.TourCode);
                }
                if (!string.IsNullOrEmpty(chaXun.XianLuName))
                {
                    sql.AppendFormat(" AND RouteName LIKE '%{0}%' ", chaXun.XianLuName);
                }
                if (chaXun.TourStatus != null && chaXun.TourStatus.Length > 0)
                {
                    sql.AppendFormat(" AND TourStatus IN({0})", Utils.GetSqlIn<EyouSoft.Model.EnumType.TourStructure.TourStatus>(chaXun.TourStatus));
                }
                if (chaXun.TourSellerDeptIds != null && chaXun.TourSellerDeptIds.Length > 0)
                {
                    sql.AppendFormat(" AND DeptId IN({0})", Utils.GetSqlIn<int>(chaXun.TourSellerDeptIds));
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), paiXuString, heJiString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.StatStructure.MYuSuanBiaoInfo();

                    item.ChuTuanRiQi = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    //item.HuiTuanRiQi=
                    item.JiDiaoYuanName = rdr["JiDiaoYuanName"].ToString();
                    //item.MaoLi=
                    item.ShiShouRenShu = rdr.GetInt32(rdr.GetOrdinal("ShiShouRenShu"));
                    item.ShouRuJinE = rdr.GetDecimal(rdr.GetOrdinal("ShouRuJinE"));
                    item.TianShu = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.TourId = rdr["TourId"].ToString();
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    item.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.XianLuName = rdr["RouteName"].ToString();
                    item.XiaoShouYuanName = rdr["SellerName"].ToString();
                    item.ZhiChuJinE = rdr.GetDecimal(rdr.GetOrdinal("ZhiChuJinE"));     

                    items.Add(item);
                }

                rdr.NextResult();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShiShouRenShu"))) heJi[0] = rdr.GetInt32(rdr.GetOrdinal("ShiShouRenShu"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShouRuJinE"))) heJi[1] = rdr.GetDecimal(rdr.GetOrdinal("ShouRuJinE"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhiChuJinE"))) heJi[2] = rdr.GetDecimal(rdr.GetOrdinal("ZhiChuJinE"));
                }
            }

            return items;
        }

        /// <summary>
        /// 获取预算表收入信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.StatStructure.MYuSuanBiaoShouRuInfo> GetShouRus(string tourId)
        {
            IList<EyouSoft.Model.StatStructure.MYuSuanBiaoShouRuInfo> items = new List<EyouSoft.Model.StatStructure.MYuSuanBiaoShouRuInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetShouRus);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item =new EyouSoft.Model.StatStructure.MYuSuanBiaoShouRuInfo();

                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney"));
                    item.KeHuName = rdr["BuyCompanyName"].ToString();
                    item.OrderCode = rdr["OrderCode"].ToString();
                    item.OrderId = rdr["OrderId"].ToString();
                    item.XiaDanTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.XiaoShouYuanName = rdr["SellerName"].ToString();
                    item.QueRenZhuangTai = rdr["ConfirmMoneyStatus"].ToString() == "1";
                    item.ShiShouRenShu = rdr.GetInt32(rdr.GetOrdinal("ShiShouRenShu"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取预算表支出信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.StatStructure.MYuSuanBiaoZhiChuInfo> GetZhiChus(string tourId)
        {
            IList<EyouSoft.Model.StatStructure.MYuSuanBiaoZhiChuInfo> items = new List<EyouSoft.Model.StatStructure.MYuSuanBiaoZhiChuInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetZhiChus);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.StatStructure.MYuSuanBiaoZhiChuInfo();

                    item.FeiYongMingXi = rdr["CostDetail"].ToString();
                    item.GysName = rdr["SourceName"].ToString();
                    item.JiDiaoYuanName = rdr["OperatorName"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("Confirmation"));
                    item.LeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)rdr.GetByte(rdr.GetOrdinal("Type"));
                    item.PlanId = rdr["PlanId"].ToString();
                    item.ZhiFuFangShi = (EyouSoft.Model.EnumType.PlanStructure.Payment)rdr.GetByte(rdr.GetOrdinal("PaymentType"));

                    items.Add(item);
                }
            }

            return items;            
        }
        #endregion
    }
}
