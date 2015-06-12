//航期订单相关 汪奇志 2014-03-30
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
    /// 航期订单相关
    /// </summary>
    public class DHangQiDingDan : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IHangQiDingDan
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetDingDanInfo = "SELECT * FROM view_YL_HangQiDingDan WHERE DingDanId=@DingDanId";
        const string SQL_SELECT_GetDiKouInfo = "SELECT * FROM tbl_YL_HangQiDingDanDiKou WHERE DingDanId=@DingDanId";
        const string SQL_SELECT_GetFuJiaChanPins = "SELECT * FROM tbl_YL_HangQiDingDanFuJiaChanPin WHERE DingDanId=@DingDanId";
        const string SQL_SELECT_GetJiaGes = "SELECT *,(SELECT J.MingCheng FROM dbo.tbl_YL_JiChuXinXi J WHERE J.XinXiId=FangXingId) AS FangXing FROM tbl_YL_HangQiDingDanJiaGe WHERE DingDanId=@DingDanId";
        const string SQL_SELECT_GetYouHuis = "SELECT * FROM tbl_YL_HangQiDingDanYouHui WHERE DingDanId=@DingDanId";
        const string SQL_SELECT_GetYouKes = "SELECT * FROM tbl_YL_HangQiDingDanYouKe WHERE DingDanId=@DingDanId ORDER BY IdentityId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DHangQiDingDan()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create fujiachanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFuJiaChanPinXml(IList<EyouSoft.Model.YlStructure.MHangQiDingDanFuJiaChanPinInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FuJiaChanPinId=\"{0}\" DanJia=\"{1}\" ShuLiang=\"{2}\" JinE=\"{3}\" />", item.FuJiaChanPinId
                        , item.DanJia
                        , item.ShuLiang
                        , item.JinE);
                }
            }

            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create youke xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateYouKeXml(IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info YouKeId=\"{0}\" ", item.YouKeId);
                    xml.AppendFormat(" LeiXing=\"{0}\" ", (int)item.LeiXing);
                    xml.AppendFormat(" LeiXingId=\"{0}\" ", item.LeiXingId);
                    xml.AppendFormat(" ZhengJianLeiXing=\"{0}\" ", (int)item.ZhengJianLeiXing);
                    if (item.ZhengJianYouXiaoQi.HasValue)
                    {
                        xml.AppendFormat(" ZhengJianYouXiaoQi=\"{0}\" ", item.ZhengJianYouXiaoQi.Value);
                    }
                    if (item.ChuShengRiQi.HasValue)
                    {
                        xml.AppendFormat(" ChuShengRiQi=\"{0}\" ", item.ChuShengRiQi.Value);
                    }
                    xml.AppendFormat(" GuoJiaId=\"{0}\" ", item.GuoJiaId);
                    xml.AppendFormat(" ShengFenId=\"{0}\" ", item.ShengFenId);
                    xml.AppendFormat(" ChengShiId=\"{0}\" ", item.ChengShiId);
                    xml.AppendFormat(" XianQuId=\"{0}\" ", item.XianQuId);
                    xml.AppendFormat(" XingBie=\"{0}\" ", (int)item.XingBie);
                    xml.AppendFormat(">");
                    xml.AppendFormat("<XingMing><![CDATA[{0}]]></XingMing>", item.XingMing);
                    xml.AppendFormat("<ZhengJianHaoMa><![CDATA[{0}]]></ZhengJianHaoMa>", item.ZhengJianHaoMa);
                    xml.AppendFormat("<DianHua><![CDATA[{0}]]></DianHua>", item.DianHua);
                    xml.AppendFormat("<ShouJi><![CDATA[{0}]]></ShouJi>", item.ShouJi);
                    xml.AppendFormat("<YXQ1><![CDATA[{0}]]></YXQ1>", item.YXQ1);
                    xml.AppendFormat("<YXQ2><![CDATA[{0}]]></YXQ2>", item.YXQ2);
                    xml.AppendFormat("<YXQ3><![CDATA[{0}]]></YXQ3>", item.YXQ3);
                    xml.AppendFormat("<SR1><![CDATA[{0}]]></SR1>", item.SR1);
                    xml.AppendFormat("<SR2><![CDATA[{0}]]></SR2>", item.SR2);
                    xml.AppendFormat("<SR3><![CDATA[{0}]]></SR3>", item.SR3);
                    xml.AppendFormat("</info>");
                }
            }

            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create jiage xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateJiaGeXml(IList<EyouSoft.Model.YlStructure.MHangQiDingDanJiaGeInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FangXingId=\"{0}\" ", item.FangXingId);
                    xml.AppendFormat(" GuoJiId=\"{0}\" ", item.GuoJiId);
                    xml.AppendFormat(" BinKeLeiXingId=\"{0}\" ", item.BinKeLeiXingId);
                    xml.AppendFormat(" RongNaRenShu=\"{0}\" ", item.RongNaRenShu);
                    xml.AppendFormat(" JiaGe1=\"{0}\" ", item.JiaGe1);
                    xml.AppendFormat(" JiaGe2=\"{0}\" ", item.JiaGe2);
                    xml.AppendFormat(" JiaGe3=\"{0}\" ", item.JiaGe3);
                    xml.AppendFormat(" JiaGe4=\"{0}\" ", item.JiaGe4);
                    xml.AppendFormat(" RenShu1=\"{0}\" ", item.RenShu1);
                    xml.AppendFormat(" RenShu2=\"{0}\" ", item.RenShu2);
                    xml.AppendFormat(" RenShu3=\"{0}\" ", item.RenShu3);
                    xml.AppendFormat(" RenShu4=\"{0}\" ", item.RenShu4);
                    xml.AppendFormat(" FangCha=\"{0}\" ", item.FangCha);
                    xml.AppendFormat(">");
                    xml.AppendFormat("<LouCeng><![CDATA[{0}]]></LouCeng>", item.LouCeng);
                    xml.AppendFormat("</info>");
                }
            }

            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create youhui xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateYouHuiXml(IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouHuiInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info JinE=\"{0}\" ", item.JinE);
                    xml.AppendFormat(" YouXianJi=\"{0}\" ", item.YouXianJi);
                    xml.AppendFormat(" IsGongXiang=\"{0}\" ", item.IsGongXiang ? "1" : "0");
                    xml.AppendFormat(">");
                    xml.AppendFormat("<MiaoShu><![CDATA[{0}]]></MiaoShu>", item.MiaoShu);
                    xml.AppendFormat("<GuiZe><![CDATA[{0}]]></GuiZe>", item.GuiZe);
                    xml.AppendFormat("<MingCheng><![CDATA[{0}]]></MingCheng>", item.MingCheng);
                    xml.AppendFormat("</info>");
                }
            }

            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create dikou xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateDiKouXml(EyouSoft.Model.YlStructure.MHangQiDingDanDiKouInfo info)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");

            if (info != null)
            {
                xml.AppendFormat("<info JiFen=\"{0}\" ", info.JiFen);
                xml.AppendFormat(" JiFenBiLi=\"{0}\" ", info.JiFenBiLi);
                xml.AppendFormat(" JinFenJinE=\"{0}\" ", info.JinFenJinE);
                xml.AppendFormat(" LiPinKaJinE=\"{0}\" ", info.LiPinKaJinE);
                xml.AppendFormat(">");
                xml.AppendFormat("</info>");
            }

            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// get dikou info
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHangQiDingDanDiKouInfo GetDiKouInfo(string dingDanId)
        {
            EyouSoft.Model.YlStructure.MHangQiDingDanDiKouInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDiKouInfo);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHangQiDingDanDiKouInfo();
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    info.JiFenBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenBiLi"));
                    info.JinFenJinE = rdr.GetDecimal(rdr.GetOrdinal("JinFenJinE"));
                    info.LiPinKaJinE = rdr.GetDecimal(rdr.GetOrdinal("LiPinKaJinE"));
                }
            }

            return info;
        }

        /// <summary>
        /// get fujiachanpins
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiDingDanFuJiaChanPinInfo> GetFuJiaChanPins(string dingDanId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiDingDanFuJiaChanPinInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiDingDanFuJiaChanPinInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetFuJiaChanPins);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiDingDanFuJiaChanPinInfo();

                    item.DanJia = rdr.GetDecimal(rdr.GetOrdinal("DanJia"));
                    item.FuJiaChanPinId = rdr["FuJiaChanPinId"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));

                    item.LeiXingId = rdr.GetInt32(rdr.GetOrdinal("LeiXingId"));
                    item.XiangMu = rdr["XiangMu"].ToString();
                    item.DanWei = rdr["DanWei"].ToString();
                    item.JieShao = rdr["JieShao"].ToString();

                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// get jiages
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiDingDanJiaGeInfo> GetJiaGes(string dingDanId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiDingDanJiaGeInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiDingDanJiaGeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiaGes);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiDingDanJiaGeInfo();
                    item.BinKeLeiXingId = rdr.GetInt32(rdr.GetOrdinal("BinKeLeiXingId"));
                    item.FangCha = rdr.GetDecimal(rdr.GetOrdinal("FangCha"));
                    item.FangXingId = rdr.GetInt32(rdr.GetOrdinal("FangXingId"));
                    item.FangXing = rdr["FangXing"].ToString();
                    item.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    item.JiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    item.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    item.JiaGe3 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe3"));
                    item.JiaGe4 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe4"));
                    item.RenShu1 = rdr.GetInt32(rdr.GetOrdinal("RenShu1"));
                    item.RenShu2 = rdr.GetInt32(rdr.GetOrdinal("RenShu2"));
                    item.RenShu3 = rdr.GetInt32(rdr.GetOrdinal("RenShu3"));
                    item.RenShu4 = rdr.GetInt32(rdr.GetOrdinal("RenShu4"));
                    item.RongNaRenShu = rdr.GetInt32(rdr.GetOrdinal("RongNaRenShu"));
                    item.LouCeng = rdr["LouCeng"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get youhuis
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouHuiInfo> GetYouHuis(string dingDanId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouHuiInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiDingDanYouHuiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYouHuis);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiDingDanYouHuiInfo();
                    item.GuiZe = rdr["GuiZe"].ToString();
                    item.IsGongXiang = rdr["IsGongXiang"].ToString() == "1";
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    item.YouXianJi = rdr.GetInt32(rdr.GetOrdinal("YouXianJi"));
                    item.MingCheng = rdr["MingCheng"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get youkes
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> GetYouKes(string dingDanId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYouKes);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo();
                    item.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuShengRiQi"))) item.ChuShengRiQi = rdr.GetDateTime(rdr.GetOrdinal("ChuShengRiQi"));
                    item.DianHua = rdr["DianHua"].ToString();
                    item.GuoJiaId = rdr.GetInt32(rdr.GetOrdinal("GuoJiaId"));
                    item.LeiXing = (EyouSoft.Model.EnumType.TourStructure.VisitorType)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    item.LeiXingId = rdr.GetInt32(rdr.GetOrdinal("LeiXingId"));
                    item.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    item.ShouJi = rdr["ShouJi"].ToString();
                    item.XianQuId = rdr.GetInt32(rdr.GetOrdinal("XianQuId"));
                    item.XingMing = rdr["XingMing"].ToString();
                    item.YouKeId = rdr["YouKeId"].ToString();
                    item.ZhengJianHaoMa = rdr["ZhengJianHaoMa"].ToString();
                    item.ZhengJianLeiXing = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)rdr.GetByte(rdr.GetOrdinal("ZhengJianLeiXing"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhengJianYouXiaoQi"))) item.ZhengJianYouXiaoQi = rdr.GetDateTime(rdr.GetOrdinal("ZhengJianYouXiaoQi"));
                    item.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("XingBie"));
                    item.YXQ1 = rdr["YXQ1"].ToString();
                    item.YXQ2 = rdr["YXQ2"].ToString();
                    item.YXQ3=rdr["YXQ3"].ToString();
                    item.SR1 = rdr["SR1"].ToString();
                    item.SR2 = rdr["SR2"].ToString();
                    item.SR3 = rdr["SR3"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion


        #region IHangQiDingDan 成员
        /// <summary>
        /// 写入航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int DingDan_C(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_C");

            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, info.RiQiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@JiaoYiHao", DbType.String, info.JiaoYiHao);
            _db.AddInParameter(cmd, "@RenShu", DbType.Int32, info.RenShu);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@DingDanStatus", DbType.Byte, info.DingDanStatus);
            _db.AddInParameter(cmd, "@FuKuanStatus", DbType.Byte, info.FuKuanStatus);
            _db.AddInParameter(cmd, "@LiuWeiDaoQiShiJian", DbType.DateTime, info.LiuWeiDaoQiShiJian);
            _db.AddInParameter(cmd, "@XiaDanBeiZhu", DbType.String, info.XiaDanBeiZhu);
            _db.AddInParameter(cmd, "@YuDingRenName", DbType.String, info.YuDingRenName);
            _db.AddInParameter(cmd, "@YuDingRenDianHua", DbType.String, info.YuDingRenDianHua);
            _db.AddInParameter(cmd, "@YuDingRenShouJi", DbType.String, info.YuDingRenShouJi);
            _db.AddInParameter(cmd, "@YuDingRenYouXiang", DbType.String, info.YuDingRenYouXiang);
            _db.AddInParameter(cmd, "@IsXuYaoFaPiao", DbType.AnsiStringFixedLength, info.IsXuYaoFaPiao ? "1" : "0");
            _db.AddInParameter(cmd, "@FaPiaoTaiTou", DbType.String, info.FaPiaoTaiTou);
            _db.AddInParameter(cmd, "@FaPiaoLeiXing", DbType.String, info.FaPiaoLeiXing);
            _db.AddInParameter(cmd, "@FaPiaoMingXi", DbType.String, info.FaPiaoMingXi);
            _db.AddInParameter(cmd, "@FaPiaoPeiSongFangShi", DbType.Byte, info.FaPiaoPeiSongFangShi);
            _db.AddInParameter(cmd, "@FaPiaoDiZhiId", DbType.AnsiStringFixedLength, info.FaPiaoDiZhiId);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@XiaDanRenId", DbType.AnsiStringFixedLength, info.XiaDanRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@IsTuanGou", DbType.AnsiStringFixedLength, info.IsTuanGou ? "1" : "0");
            _db.AddInParameter(cmd, "@TuanGouId", DbType.AnsiStringFixedLength, info.TuanGouId);
            _db.AddInParameter(cmd, "@FuJiaChanPinXml", DbType.String, CreateFuJiaChanPinXml(info.FuJiaChanPins));
            _db.AddInParameter(cmd, "@YouKeXml", DbType.String, CreateYouKeXml(info.YouKes));
            _db.AddInParameter(cmd, "@JiaGeXml", DbType.String, CreateJiaGeXml(info.JiaGes));
            _db.AddInParameter(cmd, "@YouHuiXml", DbType.String, CreateYouHuiXml(info.YouHuis));
            _db.AddInParameter(cmd, "@DiKouXml", DbType.String, CreateDiKouXml(info.DiKouInfo));
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
        /// 修改航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int DingDan_M(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_M");

            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, info.RiQiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@JiaoYiHao", DbType.String, info.JiaoYiHao);
            _db.AddInParameter(cmd, "@RenShu", DbType.Int32, info.RenShu);
            _db.AddInParameter(cmd, "@JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "@DingDanStatus", DbType.Byte, info.DingDanStatus);
            _db.AddInParameter(cmd, "@FuKuanStatus", DbType.Byte, info.FuKuanStatus);
            //_db.AddInParameter(cmd, "@LiuWeiDaoQiShiJian", DbType.DateTime, info.LiuWeiDaoQiShiJian);
            _db.AddInParameter(cmd, "@XiaDanBeiZhu", DbType.String, info.XiaDanBeiZhu);
            _db.AddInParameter(cmd, "@YuDingRenName", DbType.String, info.YuDingRenName);
            _db.AddInParameter(cmd, "@YuDingRenDianHua", DbType.String, info.YuDingRenDianHua);
            _db.AddInParameter(cmd, "@YuDingRenShouJi", DbType.String, info.YuDingRenShouJi);
            _db.AddInParameter(cmd, "@YuDingRenYouXiang", DbType.String, info.YuDingRenYouXiang);
            _db.AddInParameter(cmd, "@IsXuYaoFaPiao", DbType.AnsiStringFixedLength, info.IsXuYaoFaPiao ? "1" : "0");
            _db.AddInParameter(cmd, "@FaPiaoTaiTou", DbType.String, info.FaPiaoTaiTou);
            _db.AddInParameter(cmd, "@FaPiaoLeiXing", DbType.String, info.FaPiaoLeiXing);
            _db.AddInParameter(cmd, "@FaPiaoMingXi", DbType.String, info.FaPiaoMingXi);
            _db.AddInParameter(cmd, "@FaPiaoPeiSongFangShi", DbType.Byte, info.FaPiaoPeiSongFangShi);
            _db.AddInParameter(cmd, "@FaPiaoDiZhiId", DbType.AnsiStringFixedLength, info.FaPiaoDiZhiId);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@XiaDanRenId", DbType.AnsiStringFixedLength, info.XiaDanRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@IsTuanGou", DbType.AnsiStringFixedLength, info.IsTuanGou ? "1" : "0");
            _db.AddInParameter(cmd, "@TuanGouId", DbType.AnsiStringFixedLength, info.TuanGouId);
            _db.AddInParameter(cmd, "@FuJiaChanPinXml", DbType.String, CreateFuJiaChanPinXml(info.FuJiaChanPins));
            _db.AddInParameter(cmd, "@YouKeXml", DbType.String, CreateYouKeXml(info.YouKes));
            _db.AddInParameter(cmd, "@JiaGeXml", DbType.String, CreateJiaGeXml(info.JiaGes));
            _db.AddInParameter(cmd, "@YouHuiXml", DbType.String, CreateYouHuiXml(info.YouHuis));
            _db.AddInParameter(cmd, "@DiKouXml", DbType.String, CreateDiKouXml(info.DiKouInfo));
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
        /// 获取订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiDingDanInfo GetDingDanInfo(string dingDanId)
        {
            EyouSoft.Model.YlStructure.MHangQiDingDanInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDingDanInfo);
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHangQiDingDanInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DiKouInfo = null;
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                    info.FaPiaoDiZhiId = rdr["FaPiaoDiZhiId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FaPiaoLeiXing = rdr["FaPiaoLeiXing"].ToString();
                    info.FaPiaoMingXi = rdr["FaPiaoMingXi"].ToString();
                    info.FaPiaoPeiSongFangShi = (EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi)rdr.GetByte(rdr.GetOrdinal("FaPiaoPeiSongFangShi"));
                    info.FaPiaoTaiTou = rdr["FaPiaoTaiTou"].ToString();
                    info.FuJiaChanPins = null;
                    info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)rdr.GetByte(rdr.GetOrdinal("FuKuanStatus"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsTuanGou = rdr["IsTuanGou"].ToString() == "1";
                    info.IsXuYaoFaPiao = rdr["IsXuYaoFaPiao"].ToString() == "1";
                    info.JiaGes = null;
                    info.JiaoYiHao = rdr["JiaoYiHao"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.LiuWeiDaoQiShiJian = rdr.GetDateTime(rdr.GetOrdinal("LiuWeiDaoQiShiJian"));
                    info.RenShu = rdr.GetInt32(rdr.GetOrdinal("RenShu"));
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YouHuis = null;
                    info.YouKes = null;
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.IdentityId = rdr.GetInt32(rdr.GetOrdinal("IdentityId"));
                    info.YouLunLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.GysName = rdr["GysName"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();
                    info.HangXian = rdr["HangXian"].ToString();
                    info.BianHao = rdr["BianHao"].ToString();
                    info.TianShu1 = rdr.GetInt32(rdr.GetOrdinal("TianShu1"));
                    info.TianShu2 = rdr.GetInt32(rdr.GetOrdinal("TianShu2"));

                    info.RiQi = rdr.GetDateTime(rdr.GetOrdinal("RiQi"));
                    info.DingDanJiFen = rdr.GetDecimal(rdr.GetOrdinal("DingDanJiFen"));
                    info.CaoZuoBeiZhu = rdr["CaoZuoBeiZhu"].ToString();
                }
            }

            if (info != null)
            {
                info.DiKouInfo = GetDiKouInfo(info.DingDanId);
                info.FuJiaChanPins = GetFuJiaChanPins(info.DingDanId);
                info.JiaGes = GetJiaGes(info.DingDanId);
                info.YouHuis = GetYouHuis(info.DingDanId);
                info.YouKes = GetYouKes(info.DingDanId);
            }

            return info;
        }

        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiDingDanInfo> GetDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiDingDanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiDingDanInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiDingDanInfo>();
            string tableName = "view_YL_HangQiDingDan";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.HuiYuanId))
                {
                    sql.AppendFormat(" AND XiaDanRenId='{0}' ", chaXun.HuiYuanId);
                }
                if (!string.IsNullOrEmpty(chaXun.DingDanHao))
                {
                    sql.AppendFormat(" AND JiaoYiHao LIKE '%{0}%' ", chaXun.DingDanHao);
                }
                if (chaXun.DingDanStatus.HasValue)
                {
                    sql.AppendFormat(" AND DingDanStatus={0} ", (int)chaXun.DingDanStatus.Value);
                }
                if (chaXun.FuKuanStatus.HasValue)
                {
                    sql.AppendFormat(" AND FuKuanStatus={0} ", (int)chaXun.FuKuanStatus.Value);
                }
                if (chaXun.XiaDanShiJian1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>='{0}' ", chaXun.XiaDanShiJian1.Value);
                }
                if (chaXun.XiaDanShiJian2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<='{0}' ", chaXun.XiaDanShiJian2.Value.AddDays(1).AddMinutes(-1));
                }
                if (chaXun.DingDanLeiXing.HasValue)
                {
                    switch (chaXun.DingDanLeiXing)
                    {
                        case 0: sql.AppendFormat(" AND LeiXing=0 AND IsTuanGou='0' "); break;
                        case 1: sql.AppendFormat(" AND LeiXing=1 AND IsTuanGou='0' "); break;
                        case 2: sql.AppendFormat(" AND IsTuanGou='1' "); break;
                    }
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHangQiDingDanInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DiKouInfo = null;
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.DingDanStatus = (EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus)rdr.GetByte(rdr.GetOrdinal("DingDanStatus"));
                    info.FaPiaoDiZhiId = rdr["FaPiaoDiZhiId"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.FaPiaoLeiXing = rdr["FaPiaoLeiXing"].ToString();
                    info.FaPiaoMingXi = rdr["FaPiaoMingXi"].ToString();
                    info.FaPiaoPeiSongFangShi = (EyouSoft.Model.EnumType.YlStructure.FaPiaoPeiSongFangShi)rdr.GetByte(rdr.GetOrdinal("FaPiaoPeiSongFangShi"));
                    info.FaPiaoTaiTou = rdr["FaPiaoTaiTou"].ToString();
                    info.FuJiaChanPins = null;
                    info.FuKuanStatus = (EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)rdr.GetByte(rdr.GetOrdinal("FuKuanStatus"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsTuanGou = rdr["IsTuanGou"].ToString() == "1";
                    info.IsXuYaoFaPiao = rdr["IsXuYaoFaPiao"].ToString() == "1";
                    info.JiaGes = null;
                    info.JiaoYiHao = rdr["JiaoYiHao"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.LiuWeiDaoQiShiJian = rdr.GetDateTime(rdr.GetOrdinal("LiuWeiDaoQiShiJian"));
                    info.RenShu = rdr.GetInt32(rdr.GetOrdinal("RenShu"));
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.XiaDanBeiZhu = rdr["XiaDanBeiZhu"].ToString();
                    info.XiaDanRenId = rdr["XiaDanRenId"].ToString();
                    info.YouHuis = null;
                    info.YouKes = null;
                    info.YuDingRenDianHua = rdr["YuDingRenDianHua"].ToString();
                    info.YuDingRenName = rdr["YuDingRenName"].ToString();
                    info.YuDingRenShouJi = rdr["YuDingRenShouJi"].ToString();
                    info.YuDingRenYouXiang = rdr["YuDingRenYouXiang"].ToString();
                    info.JiFenLeiJiBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenLeiJiBiLi"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();

                    items.Add(info);
                }
            }
            return items;
        }

        /// <summary>
        /// 设置航期订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="liuWeiDaoQiShiJian">留位到期时间</param>
        /// <returns></returns>
        public int SheZhiDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus status, DateTime? liuWeiDaoQiShiJian)
        {
            if (!liuWeiDaoQiShiJian.HasValue) liuWeiDaoQiShiJian = DateTime.Now;
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_SheZhiDingDanStatus");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@DingDanStatus", DbType.Byte, status);
            _db.AddInParameter(cmd, "@LiuWeiDaoQiShiJian", DbType.DateTime, liuWeiDaoQiShiJian);
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
        /// 设置付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_SheZhiFuKuanStatus");
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
        /// 获取航期订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        public string GetDingDanId(int identityId)
        {
            string sql = "SELECT DingDanId FROM tbl_YL_HangQiDingDan WHERE IdentityId=@IdentityId";
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

        /// <summary>
        /// 更新订单游客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="youKes">游客集合</param>
        /// <returns></returns>
        public int UpdateDingDanYouKes(string dingDanId, IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> youKes)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_YouKes_U");

            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "@YouKeXml", DbType.String, CreateYouKeXml(youKes));
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
        /// 更新订单积分，返回1成功，其它失败
        /// </summary>
        /// <param name="dingdanId">订单编号</param>
        /// <param name="dingdanJifen">订单积分</param>
        /// <returns>1：成功 -100：失败 -99：会员可用积分小于0</returns>
        public int UpDateDingDanJiFen(string dingdanId, decimal dingdanJifen)
        {
            var cmd = _db.GetStoredProcCommand("proc_YL_HangQiDingDan_DingDanJiFen_U");

            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingdanId);
            _db.AddInParameter(cmd, "@DingDanJiFen", DbType.Decimal, dingdanJifen);
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
        /// 更新订单操作备注
        /// </summary>
        /// <param name="dingdanid">订单编号</param>
        /// <param name="caozuobeizhu">操作备注</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdDingDanCaoZuoBeiZhu(string dingdanid, string caozuobeizhu)
        {
            var sql = "update tbl_yl_hangqidingdan set caozuobeizhu=@caozuobeizhu where dingdanid=@dingdanid";
            var cmd = _db.GetSqlStringCommand(sql);

            _db.AddInParameter(cmd, "@dingdanid", DbType.AnsiStringFixedLength, dingdanid);
            _db.AddInParameter(cmd, "@caozuobeizhu", DbType.String, caozuobeizhu);

            return DbHelper.ExecuteSql(cmd, _db);
        }
        #endregion
    }
}
