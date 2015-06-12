// 游轮航期 汪奇志 2014-03-26
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
    /// 游轮航期
    /// </summary>
    public class DHangQi : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IHangQi
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetHangQiInfo = "SELECT * FROM view_YL_HangQi WHERE HangQiId=@HangQiId";
        const string SQL_SELECT_GetFuJiaChanPins = "SELECT * FROM tbl_YL_HangQiFuJiaChanPin WHERE HangQiId=@HangQiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetTuJingChengShis = "SELECT * FROM tbl_YL_HangQiTuJingChengShi WHERE HangQiId=@HangQiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetXingChengs = "SELECT * FROM tbl_YL_HangQiXingCheng WHERE HangQiId=@HangQiId ORDER BY Tian ASC";
        const string SQL_SELECT_GetHangQiJiaGes = "SELECT * FROM tbl_YL_HangQiJiaGe WHERE HangQiId=@HangQiId AND RiQiId=@RiQiId ORDER BY IdentityId ASC";
        //const string SQL_SELECT_GetHangQiJiaGes = "SELECT A.*,(SELECT A1.MingCheng FROM tbl_YL_JiChuXinXi AS A1 WHERE A1.XinXiId=A.FangXingId) AS FangXingName,(SELECT A1.MingCheng FROM tbl_YL_JiChuXinXi AS A1 WHERE A1.XinXiId=A.GuoJiId) AS GuoJiName,(SELECT A1.MingCheng FROM tbl_YL_JiChuXinXi AS A1 WHERE A1.XinXiId=A.BinKeLeiXingId) AS BingKeLeiXingName FROM tbl_YL_HangQiJiaGe AS A WHERE A.HangQiId=@HangQiId AND A.RiQiId=@RiQiId ORDER BY A.IdentityId ASC";
        const string SQL_SELECT_GetHangQiyouHuiGuiZes = "SELECT * FROM tbl_YL_HangQiYouHuiGuiZe WHERE HangQiId=@HangQiId ORDER BY IdentityId ASC;SELECT * FROM tbl_YL_HangQiYouHuiGuiZe1 WHERE HangQiId=@HangQiId";
        const string SQL_INSERT_HangQiLiuLanJiLu_C = "INSERT INTO [tbl_YL_HangQiLiuLanJiLu]([HangQiId],[JiLuId],[YongHuId],[IssueTime])VALUES(@HangQiId,@JiLuId,@YongHuId,@IssueTime)";
        const string SQL_SELECT_GetHangQiFuJians = "SELECT * FROM tbl_YL_HangQiFuJian WHERE HangQiId=@HangQiId";
        const string SQL_SELECT_GetBiaoQians = "SELECT * FROM tbl_YL_HangQiBiaoQian WHERE HangQiId=@HangQiId";
        const string SQL_SELECT_GetTuanGouInfo = "SELECT * FROM view_YL_TuanGou WHERE TuanGouId=@TuanGouId";
        const string SQL_SELECT_GetTuanGouJiaGes = "SELECT * FROM tbl_YL_TuanGouJiaGe WHERE TuanGouId=@TuanGouId";
        const string SQL_SELECT_GetRiQiInfo = "SELECT A.*,(SELECT ISNULL(SUM(A1.RenShu),0) FROM tbl_YL_HangQiDingDan AS A1 WHERE A1.IsDelete='0' AND A1.RiQiId=A.RiQiId AND A1.DingDanStatus IN(0,1,2,4) AND A1.IsTuanGou='0') YouXiaoDingDanRenShu FROM tbl_YL_HangQiRiQi  AS A WHERE A.RiQiId=@RiQiId";
        //更新航期SEO数据
        private const string SQL_UPDATE_HangQiSeoInfo = "update tbl_YL_HangQi set SeoTitle=@SeoTitle,SeoKeyword=@SeoKeyword,SeoDescription=@SeoDescription where HangQiId=@HangQiId";

        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DHangQi()
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
        string CreateFuJiaChanPinXml(IList<EyouSoft.Model.YlStructure.MHangQiFuJiaChanPinInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FuJiaChanPinId=\"{0}\" LeiXingId=\"{1}\" DanJia=\"{2}\">", item.FuJiaChanPinId, item.LeiXingId, item.DanJia);
                    xml.AppendFormat("<XiangMu><![CDATA[{0}]]></XiangMu>", item.XiangMu);
                    xml.AppendFormat("<DanWei><![CDATA[{0}]]></DanWei>", item.DanWei);
                    xml.AppendFormat("<JieShao><![CDATA[{0}]]></JieShao>", item.JieShao);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create xingcheng xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateXingChengXml(IList<EyouSoft.Model.YlStructure.MHangQiXingChengInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            int i = 1;
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info XingChengId=\"{0}\" Tian=\"{1}\">", item.XingChengId, i);
                    xml.AppendFormat("<QuJian1><![CDATA[{0}]]></QuJian1>", item.QuJian1);
                    xml.AppendFormat("<QuJian2><![CDATA[{0}]]></QuJian2>", item.QuJian2);
                    xml.AppendFormat("<ZhuSu><![CDATA[{0}]]></ZhuSu>", item.ZhuSu);
                    xml.AppendFormat("<Zao><![CDATA[{0}]]></Zao>", item.Zao);
                    xml.AppendFormat("<Zhong><![CDATA[{0}]]></Zhong>", item.Zhong);
                    xml.AppendFormat("<Wan><![CDATA[{0}]]></Wan>", item.Wan);
                    xml.AppendFormat("<NeiRong><![CDATA[{0}]]></NeiRong>", item.NeiRong);
                    xml.AppendFormat("<JiaoTongGongJu><![CDATA[{0}]]></JiaoTongGongJu>", item.JiaoTongGongJu);
                    xml.AppendFormat("<BanCi><![CDATA[{0}]]></BanCi>", item.BanCi);
                    xml.AppendFormat("<Filepath><![CDATA[{0}]]></Filepath>", item.Filepath);
                    xml.Append("</info>");

                    i++;
                }
            }
            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// create tujingchengshi xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateTuJingChengShiXml(IList<EyouSoft.Model.YlStructure.MHangQiTuJingChengShiInfo> items)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info ChengShiId=\"{0}\" />", item.ChengShiId);
                }
            }
            xml.Append("</root>");

            return xml.ToString();
        }

        /// <summary>
        /// get fujiachanpins
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiFuJiaChanPinInfo> GetFuJiaChanPins(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiFuJiaChanPinInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiFuJiaChanPinInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetFuJiaChanPins);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiFuJiaChanPinInfo();
                    item.DanJia = rdr.GetDecimal(rdr.GetOrdinal("DanJia"));
                    item.DanWei = rdr["DanWei"].ToString();
                    item.FuJiaChanPinId = rdr["FuJiaChanPinId"].ToString();
                    item.JieShao = rdr["JieShao"].ToString();
                    item.LeiXingId = rdr.GetInt32(rdr.GetOrdinal("LeiXingId"));
                    item.XiangMu = rdr["XiangMu"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get tujingchengshis
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiTuJingChengShiInfo> GetTuJingChengShis(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiTuJingChengShiInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiTuJingChengShiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetTuJingChengShis);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiTuJingChengShiInfo();
                    item.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get xingchengs
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiXingChengInfo> GetXingChengs(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiXingChengInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiXingChengInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetXingChengs);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiXingChengInfo();
                    item.BanCi = rdr["BanCi"].ToString();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.JiaoTongGongJu = rdr["JiaoTongGongJu"].ToString();
                    item.NeiRong = rdr["NeiRong"].ToString();
                    item.QuJian1 = rdr["QuJian1"].ToString();
                    item.QuJian2 = rdr["QuJian2"].ToString();
                    item.Wan = rdr["Wan"].ToString();
                    item.XingChengId = rdr["XingChengId"].ToString();
                    item.Zao = rdr["Zao"].ToString();
                    item.Zhong = rdr["Zhong"].ToString();
                    item.ZhuSu = rdr["ZhuSu"].ToString();
                    item.Tian = rdr.GetInt32(rdr.GetOrdinal("Tian"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// create hangqijiage xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateHangQiJiaGeXml(IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info JiaGeId=\"{0}\"", item.JiaGeId);
                    xml.AppendFormat(" FangXingId=\"{0}\"", item.FangXingId);
                    xml.AppendFormat(" RongNaRenShu=\"{0}\"", item.RongNaRenShu);
                    xml.AppendFormat(" GuoJiId=\"{0}\"", item.GuoJiId);
                    xml.AppendFormat(" BinKeLeiXingId=\"{0}\"", item.BinKeLeiXingId);
                    xml.AppendFormat(" JiaGe1=\"{0}\"", item.JiaGe1);
                    xml.AppendFormat(" JiaGe2=\"{0}\"", item.JiaGe2);
                    xml.AppendFormat(" JiaGe3=\"{0}\"", item.JiaGe3);
                    xml.AppendFormat(" JiaGe4=\"{0}\"", item.JiaGe4);
                    xml.AppendFormat(" FangCha=\"{0}\"", item.FangCha);
                    xml.Append(">");
                    xml.AppendFormat("<LouCeng><![CDATA[{0}]]></LouCeng>", item.LouCeng);
                    xml.AppendFormat("<ShuoMing><![CDATA[{0}]]></ShuoMing>", item.ShuoMing);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// create hangqiriqi xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateHangQiRiQiXml(IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info RiQiId=\"{0}\"", item.RiQiId);
                    xml.AppendFormat(" RiQi=\"{0}\"", item.RiQi);
                    xml.AppendFormat(" RenShu=\"{0}\"", item.RenShu);
                    xml.Append(" />");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// create hangqi youhuiguize xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateHangQiYouHuiGuiZeXml(IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info GuiZeId=\"{0}\"", item.GuiZeId);
                    xml.AppendFormat(" YouXianJi=\"{0}\"", item.YouXianJi);
                    xml.AppendFormat(" IsGongXiang=\"{0}\"", item.IsGongXiang ? "1" : "0");
                    xml.AppendFormat(" JinE=\"{0}\" ", item.JinE);
                    xml.AppendFormat(" FangShi=\"{0}\" ", item.FangShi);
                    xml.Append(" >");
                    xml.AppendFormat("<GuiZe><![CDATA[{0}]]></GuiZe>", item.GuiZe);
                    xml.AppendFormat("<MiaoShu><![CDATA[{0}]]></MiaoShu>", item.MiaoShu);
                    xml.AppendFormat("<MingCheng><![CDATA[{0}]]></MingCheng>", item.MingCheng);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

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
        /// get hangqifujians
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetHangQiFuJians(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHangQiFuJians);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

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

        /// <summary>
        /// get biaoqians
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo> GetBiaoQians(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetBiaoQians);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo();
                    item.BiaoQian = (EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian)rdr.GetByte(rdr.GetOrdinal("BiaoQian"));
                    item.HangQiId = hangQiId;

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// create tuangoujiage xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateTuanGouJiaGeXml(IList<EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info BinKeLeiXingId=\"{0}\" JiaGe=\"{1}\" />", item.BinKeLeiXingId, item.JiaGe);
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// get tuangou jiages
        /// </summary>
        /// <param name="tuanGouId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo> GetTuanGouJiaGes(string tuanGouId)
        {
            IList<EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo> items = new List<EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetTuanGouJiaGes);
            _db.AddInParameter(cmd, "TuanGouId", DbType.AnsiStringFixedLength, tuanGouId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MTuanGouJiaGeInfo();
                    item.BinKeLeiXingId = rdr.GetInt32(rdr.GetOrdinal("BinKeLeiXingId"));
                    item.JiaGe = rdr.GetDecimal(rdr.GetOrdinal("JiaGe"));
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region IHangQi 成员
        /// <summary>
        /// 更新航期SEO数据
        /// </summary>
        /// <param name="HangQiId"></param>
        /// <param name="SeoTitle"></param>
        /// <param name="SeoKeyword"></param>
        /// <param name="SeoDescription"></param>
        /// <returns></returns>
        private bool UpdateHangQiSeo(string HangQiId, string SeoTitle, string SeoKeyword, string SeoDescription)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_HangQiSeoInfo);
            this._db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, HangQiId);
            this._db.AddInParameter(cmd, "SeoTitle", DbType.String, SeoTitle);
            this._db.AddInParameter(cmd, "SeoKeyword", DbType.String, SeoKeyword);
            this._db.AddInParameter(cmd, "SeoDescription", DbType.String, SeoDescription);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 航期新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int HangQi_CU(EyouSoft.Model.YlStructure.MHangQiInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_CU");
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@BianHao", DbType.String, info.BianHao);
            _db.AddInParameter(cmd, "@QiShiJiaGe", DbType.Decimal, info.QiShiJiaGe);
            _db.AddInParameter(cmd, "@QiShiJiaGeShuoMing", DbType.String, info.QiShiJiaGeShuoMing);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@HangXianId", DbType.Int32, info.HangXianId);
            _db.AddInParameter(cmd, "@ChuFaGangKouId", DbType.Int32, info.ChuFaGangKouId);
            _db.AddInParameter(cmd, "@DiDaGangKouId", DbType.Int32, info.DiDaGangKouId);
            _db.AddInParameter(cmd, "@ChanPinTeSe", DbType.String, info.ChanPinTeSe);
            _db.AddInParameter(cmd, "@YouHuiXinXi", DbType.String, info.YouHuiXinXi);
            _db.AddInParameter(cmd, "@TianShu1", DbType.Int32, info.TianShu1);
            _db.AddInParameter(cmd, "@TianShu2", DbType.Int32, info.TianShu2);
            _db.AddInParameter(cmd, "@FeiYongShuoMing", DbType.String, info.FeiYongShuoMing);
            _db.AddInParameter(cmd, "@QianZhengQianZhu", DbType.String, info.QianZhengQianZhu);
            _db.AddInParameter(cmd, "@YuDingXuZhi", DbType.String, info.YuDingXuZhi);
            _db.AddInParameter(cmd, "@YouQingTiShi", DbType.String, info.YouQingTiShi);
            _db.AddInParameter(cmd, "@HangXianXingZhi", DbType.String, info.HangXianXingZhi);
            _db.AddInParameter(cmd, "@KeDiKouJinFen", DbType.Decimal, info.KeDiKouJinFen);
            _db.AddInParameter(cmd, "@JiFenDuiHuanBiLi", DbType.Decimal, info.JiFenDuiHuanBiLi);
            _db.AddInParameter(cmd, "@JiFenLeiJiBiLi", DbType.Decimal, info.JiFenLeiJiBiLi);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@FuJiaChanPinXml", DbType.String, CreateFuJiaChanPinXml(info.FuJiaChanPins));
            _db.AddInParameter(cmd, "@XingChengXml", DbType.String, CreateXingChengXml(info.XingChengs));
            _db.AddInParameter(cmd, "@TuJingChengShiXml", DbType.String, CreateTuJingChengShiXml(info.TuJingChengShis));
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@FuJianXml", DbType.String, CreateFuJianXml(info.FuJians));
            _db.AddInParameter(cmd, "@TuJingChengShi", DbType.String, info.TuJingChengShi);
            _db.AddInParameter(cmd, "@FaPiaoKuaiDiJinE", DbType.Decimal, info.FaPiaoKuaiDiJinE);
            _db.AddInParameter(cmd, "@GongLue", DbType.String, info.GongLue);
            _db.AddInParameter(cmd, "@XiaoLiang", DbType.Int32, info.XiaoLiang1);
            _db.AddInParameter(cmd, "@HaoPing", DbType.Decimal, info.HaoPing);
            _db.AddInParameter(cmd, "@PaiXuId", DbType.Int32, info.PaiXuId);
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
                UpdateHangQiSeo(info.HangQiId,info.SeoTitle,info.SeoKeyword,info.SeoDescription);
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取航期信息
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiInfo GetHangQiInfo(string hangQiId)
        {
            EyouSoft.Model.YlStructure.MHangQiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHangQiInfo);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHangQiInfo();
                    info.SeoTitle = rdr["SeoTitle"].ToString();
                    info.SeoKeyword = rdr["SeoKeyword"].ToString();
                    info.SeoDescription = rdr["SeoDescription"].ToString();
                    info.BianHao = rdr["BianHao"].ToString();
                    info.BiaoQians = null;
                    info.ChanPinTeSe = rdr["ChanPinTeSe"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();
                    info.ChuFaGangKouId = rdr.GetInt32(rdr.GetOrdinal("ChuFaGangKouId"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DiDaGangKouId = rdr.GetInt32(rdr.GetOrdinal("DiDaGangKouId"));
                    info.FeiYongShuoMing = rdr["FeiYongShuoMing"].ToString();
                    info.FuJiaChanPins = null;
                    info.FuJians = null;
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.HangXianId = rdr.GetInt32(rdr.GetOrdinal("HangXianId"));
                    info.HangXianXingZhi = rdr["HangXianXingZhi"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiFenDuiHuanBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenDuiHuanBiLi"));
                    info.JiFenLeiJiBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenLeiJiBiLi"));
                    info.KeDiKouJinFen = rdr.GetInt32(rdr.GetOrdinal("KeDiKouJinFen"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.QianZhengQianZhu = rdr["QianZhengQianZhu"].ToString();
                    info.QiShiJiaGe = rdr.GetDecimal(rdr.GetOrdinal("QiShiJiaGe"));
                    info.QiShiJiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("QiShiJiaGe1"));
                    if (info.QiShiJiaGe == 0) info.QiShiJiaGe = info.QiShiJiaGe1;
                    info.QiShiJiaGeShuoMing = rdr["QiShiJiaGeShuoMing"].ToString();
                    info.TianShu1 = rdr.GetInt32(rdr.GetOrdinal("TianShu1"));
                    info.TianShu2 = rdr.GetInt32(rdr.GetOrdinal("TianShu2"));
                    info.TuJingChengShis = null;
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.XingChengs = null;
                    info.YouHuiXinXi = rdr["YouHuiXinXi"].ToString();
                    info.YouQingTiShi = rdr["YouQingTiShi"].ToString();
                    info.YuDingXuZhi = rdr["YuDingXuZhi"].ToString();
                    info.TuJingChengShi = rdr["TuJingChengShi"].ToString();
                    info.FaPiaoKuaiDiJinE = rdr.GetDecimal(rdr.GetOrdinal("FaPiaoKuaiDiJinE"));
                    info.HangXianMingCheng = rdr["HangXianMingCheng"].ToString();
                    info.ChuFaGangKouMingCheng = rdr["ChuFaGangKouMingCheng"].ToString();
                    info.DiDaGangKouMingCheng = rdr["DiDaGangKouMingCheng"].ToString();
                    info.GongLue = rdr["GongLue"].ToString();
                    info.XiaoLiang1 = rdr.GetInt32(rdr.GetOrdinal("XiaoLiang1"));
                    info.HaoPing = rdr.GetDecimal(rdr.GetOrdinal("HaoPing"));
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                }
            }

            if (info != null)
            {
                info.BiaoQians = GetBiaoQians(info.HangQiId);
                info.FuJiaChanPins = GetFuJiaChanPins(info.HangQiId);
                info.FuJians = GetHangQiFuJians(info.HangQiId);
                info.TuJingChengShis = GetTuJingChengShis(info.HangQiId);
                info.XingChengs = GetXingChengs(info.HangQiId);
                info.RiQis = GetHangQiRiQis(info.HangQiId, null, null, null);
            }

            return info;
        }

        /// <summary>
        /// 航期删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public int HangQi_D(string companyId, string operatorId, string hangQiId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_D");
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
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
        /// 获取航期信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiInfo> GetHangQis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiInfo>();
            string tableName = "view_YL_HangQi";
            string fields = "*";
            string orderByString = "PaiXuId DESC,IsYouXiao DESC,YouXiaoRiQi ASC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.BiaoQian.HasValue)
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_YL_HangQiBiaoQian AS A WHERE A.HangQiId=view_YL_HangQi.HangQiId AND A.BiaoQian={0}) ", (int)chaXun.BiaoQian.Value);
                }
                if (chaXun.ChuFaGangKouId.HasValue)
                {
                    sql.AppendFormat(" AND ChuFaGangKouId={0} ", chaXun.ChuFaGangKouId.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GuanJianZi))
                {
                    sql.AppendFormat("AND (MingCheng LIKE '%{0}%' OR HangXianMingCheng LIKE '%{0}%' OR ChuFaGangKouMingCheng LIKE '%{0}%' OR DiDaGangKouMingCheng LIKE '%{0}%' OR XiLieName LIKE '%{0}%' OR ChuanZhiName LIKE '%{0}%') ", chaXun.GuanJianZi);
                }
                if (chaXun.HangXianId.HasValue)
                {
                    sql.AppendFormat(" AND HangXianId={0} ", chaXun.HangXianId.Value);
                }
                if (chaXun.IsYouXiao.HasValue)
                {
                    sql.AppendFormat(" AND IsYouXiao='{0}' ", chaXun.IsYouXiao.Value ? "1" : "0");
                }
                if (chaXun.JiaGe1.HasValue)
                {
                    sql.AppendFormat(" AND QiShiJiaGe>{0} ", chaXun.JiaGe1.Value);
                }
                if (chaXun.JiaGe2.HasValue)
                {
                    sql.AppendFormat(" AND QiShiJiaGe<{0} ", chaXun.JiaGe2.Value);
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing);
                }
                if (chaXun.RiQi1.HasValue || chaXun.RiQi2.HasValue)
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_YL_HangQiRiQi AS A WHERE A.HangQiId=view_YL_HangQi.HangQiId ");
                    if (chaXun.IsYouXiao.HasValue && chaXun.IsYouXiao.Value)
                        sql.Append(" AND EXISTS(SELECT 1 FROM tbl_YL_HangQiJiaGe AS B WHERE B.RiQiId=A.RiQiId) ");

                    if (chaXun.RiQi1.HasValue)
                    {
                        sql.AppendFormat(" AND A.RiQi>='{0}' ", chaXun.RiQi1.Value);
                    }
                    if (chaXun.RiQi2.HasValue)
                    {
                        sql.AppendFormat(" AND A.RiQi<='{0}' ", chaXun.RiQi2.Value);
                    }
                    sql.AppendFormat(" ) ");
                }
                if (chaXun.TianShu1.HasValue)
                {
                    sql.AppendFormat(" AND TianShu1>={0} ", chaXun.TianShu1.Value);
                }
                if (chaXun.TianShu2.HasValue)
                {
                    sql.AppendFormat(" AND TianShu1<={0} ", chaXun.TianShu2.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieId))
                {
                    sql.AppendFormat(" AND XiLieId='{0}' ", chaXun.XiLieId);
                }
                if (chaXun.IsReXiao.HasValue)
                {
                    if (chaXun.IsReXiao.Value)
                    {
                        orderByString = "XiaoLiang1 DESC,IsYouXiao DESC,YouXiaoRiQi ASC";
                    }
                    else
                    {
                        orderByString = "XiaoLiang1 ASC,IsYouXiao DESC,YouXiaoRiQi ASC";
                    }
                }
                if (!string.IsNullOrEmpty(chaXun.BianHao))
                {
                    sql.AppendFormat(" AND BianHao LIKE '%{0}%' ", chaXun.BianHao);
                }
                if (!string.IsNullOrEmpty(chaXun.MingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.MingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND GysName LIKE '%{0}%' ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiName))
                {
                    sql.AppendFormat(" AND GongSiName LIKE '%{0}%' ", chaXun.GongSiName);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieName))
                {
                    sql.AppendFormat(" AND XiLieName LIKE '%{0}%' ", chaXun.XiLieName);
                }
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiName))
                {
                    sql.AppendFormat(" AND ChuanZhiName LIKE '%{0}%' ", chaXun.ChuanZhiName);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    sql.AppendFormat(" AND GongSiId='{0}' ", chaXun.GongSiId);
                }
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiId))
                {
                    sql.AppendFormat(" AND ChuanZhiId='{0}' ", chaXun.ChuanZhiId);
                }

                #region 多选
                if (chaXun.HX != null && chaXun.HX.Length > 0)
                {
                    sql.AppendFormat(" AND HangXianId IN({0}) ", Utils.GetSqlIn(chaXun.HX));
                }

                if (chaXun.XL != null && chaXun.XL.Length > 0)
                {
                    sql.AppendFormat(" AND XiLieId IN({0}) ", Utils.GetSqlIn(chaXun.XL));
                }

                if (chaXun.GS != null && chaXun.GS.Length > 0)
                {
                    sql.AppendFormat(" AND GongSiId IN({0}) ", Utils.GetSqlIn(chaXun.GS));
                }

                if (chaXun.GK != null && chaXun.GK.Length > 0)
                {
                    sql.AppendFormat(" AND ChuFaGangKouId IN({0}) ", Utils.GetSqlIn(chaXun.GK));
                }

                if (chaXun.TS != null && chaXun.TS.Length > 0)
                {
                    StringBuilder s1 = new StringBuilder();
                    if (chaXun.TS.Contains(1)) s1.AppendFormat(",1,2,3");
                    if (chaXun.TS.Contains(2)) s1.AppendFormat(",4,5");
                    if (chaXun.TS.Contains(3)) s1.AppendFormat(",6,7");
                    if (chaXun.TS.Contains(4)) s1.AppendFormat(",8,9");

                    if (!string.IsNullOrEmpty(s1.ToString()))
                    {
                        sql.AppendFormat(" AND TianShu1 IN({0}) ", s1.ToString().Trim(','));
                    }
                }

                if (chaXun.JG != null && chaXun.JG.Length > 0)
                {
                    StringBuilder s1 = new StringBuilder();

                    if (chaXun.JG.Contains(1)) s1.AppendFormat(" OR (QiShiJiaGe<=1000) ");
                    if (chaXun.JG.Contains(2)) s1.AppendFormat(" OR (QiShiJiaGe>=1000 AND QiShiJiaGe<=2000) ");
                    if (chaXun.JG.Contains(3)) s1.AppendFormat(" OR (QiShiJiaGe>=2000 AND QiShiJiaGe<=3000) ");
                    if (chaXun.JG.Contains(4)) s1.AppendFormat(" OR (QiShiJiaGe>=3000 AND QiShiJiaGe<=4000) ");
                    if (chaXun.JG.Contains(5)) s1.AppendFormat(" OR (QiShiJiaGe>=4000 AND QiShiJiaGe<=5000) ");
                    if (chaXun.JG.Contains(6)) s1.AppendFormat(" OR (QiShiJiaGe>=5000) ");

                    if (!string.IsNullOrEmpty(s1.ToString()))
                    {
                        sql.AppendFormat(" AND ( (1=0) ");
                        sql.AppendFormat(s1.ToString());
                        sql.AppendFormat(" ) ");
                    }
                }

                if (chaXun.YF != null && chaXun.YF.Length > 0)
                {
                    StringBuilder s1 = new StringBuilder();
                    foreach (var yf in chaXun.YF)
                    {
                        var d1 = Utils.GetDateTimeNullable(yf + "-01");
                        if (!d1.HasValue) continue;
                        s1.AppendFormat(" OR(A.RiQi>='{0}' AND A.RiQi<='{1}') ", d1.Value, d1.Value.AddMonths(1).AddDays(-1));
                    }
                    if (!string.IsNullOrEmpty(s1.ToString()))
                    {
                        sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_YL_HangQiRiQi AS A WHERE A.HangQiId=view_YL_HangQi.HangQiId ");
                        if (chaXun.IsYouXiao.HasValue && chaXun.IsYouXiao.Value)
                            sql.Append(" AND EXISTS(SELECT 1 FROM tbl_YL_HangQiJiaGe AS B WHERE B.RiQiId=A.RiQiId) ");
                        sql.Append(" AND (1=0 ");

                        sql.AppendFormat(s1.ToString());
                        sql.AppendFormat(" )) ");
                    }
                }
                #endregion

                switch (chaXun.PaiXu)
                {
                    case 0: break;
                    case 1: orderByString = "XiaoLiang1 ASC,IsYouXiao DESC,YouXiaoRiQi ASC"; break;
                    case 2: orderByString = "XiaoLiang1 DESC,IsYouXiao DESC,YouXiaoRiQi ASC"; break;
                    case 3: orderByString = "QiShiJiaGe ASC,IsYouXiao DESC,YouXiaoRiQi ASC"; break;
                    case 4: orderByString = "QiShiJiaGe DESC,IsYouXiao DESC,YouXiaoRiQi ASC"; break;
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MHangQiInfo();

                    info.BianHao = rdr["BianHao"].ToString();
                    info.BiaoQians = null;
                    info.ChanPinTeSe = rdr["ChanPinTeSe"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();
                    info.ChuFaGangKouId = rdr.GetInt32(rdr.GetOrdinal("ChuFaGangKouId"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DiDaGangKouId = rdr.GetInt32(rdr.GetOrdinal("DiDaGangKouId"));
                    info.FeiYongShuoMing = rdr["FeiYongShuoMing"].ToString();
                    info.FuJiaChanPins = null;
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.HangXianId = rdr.GetInt32(rdr.GetOrdinal("HangXianId"));
                    info.HangXianXingZhi = rdr["HangXianXingZhi"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiFenDuiHuanBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenDuiHuanBiLi"));
                    info.JiFenLeiJiBiLi = rdr.GetDecimal(rdr.GetOrdinal("JiFenLeiJiBiLi"));
                    info.KeDiKouJinFen = rdr.GetInt32(rdr.GetOrdinal("KeDiKouJinFen"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.QianZhengQianZhu = rdr["QianZhengQianZhu"].ToString();
                    info.QiShiJiaGe = rdr.GetDecimal(rdr.GetOrdinal("QiShiJiaGe"));
                    info.QiShiJiaGeShuoMing = rdr["QiShiJiaGeShuoMing"].ToString();
                    info.TianShu1 = rdr.GetInt32(rdr.GetOrdinal("TianShu1"));
                    info.TianShu2 = rdr.GetInt32(rdr.GetOrdinal("TianShu2"));
                    info.TuJingChengShis = null;
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.XingChengs = null;
                    info.YouHuiXinXi = rdr["YouHuiXinXi"].ToString();
                    info.YouQingTiShi = rdr["YouQingTiShi"].ToString();
                    info.YuDingXuZhi = rdr["YuDingXuZhi"].ToString();
                    info.HangXianMingCheng = rdr["HangXianMingCheng"].ToString();
                    info.ChuFaGangKouMingCheng = rdr["ChuFaGangKouMingCheng"].ToString();
                    info.DiDaGangKouMingCheng = rdr["DiDaGangKouMingCheng"].ToString();

                    info.IsYouXiao = rdr["IsYouXiao"].ToString() == "1";
                    info.YouXiaoRiQiId = rdr["YouXiaoRiQiId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YouXiaoRiQi"))) info.YouXiaoRiQi = rdr.GetDateTime(rdr.GetOrdinal("YouXiaoRiQi"));

                    info.YuKongRenShu = rdr.GetInt32(rdr.GetOrdinal("YuKongRenShu"));
                    info.YouXiaoDingDanRenShu = rdr.GetInt32(rdr.GetOrdinal("YouXiaoDingDanRenShu"));
                    info.TuJingChengShi = rdr["TuJingChengShi"].ToString();
                    info.XiaoLiang = rdr.GetInt32(rdr.GetOrdinal("XiaoLiang"));
                    info.XiaoLiang1 = rdr.GetInt32(rdr.GetOrdinal("XiaoLiang1"));
                    info.HaoPing = rdr.GetDecimal(rdr.GetOrdinal("HaoPing"));

                    items.Add(info);
                }
            }

            if (chaXun == null) chaXun = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.FuJians = GetHangQiFuJians(item.HangQiId);
                    item.BiaoQians = GetBiaoQians(item.HangQiId);

                    item.RiQis = GetHangQiRiQis(item.HangQiId, null, null, chaXun.IsYouXiao);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置航期价格
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="items">价格信息</param>
        /// <returns></returns>
        public int SheZhiHangQiJiaGe(string operatorId, string hangQiId, string riQiId, IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> items)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_SheZhiJiaGe");
            _db.AddInParameter(cmd, "@JiaGeXml", DbType.String, CreateHangQiJiaGeXml(items));
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, riQiId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
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
        /// 设置航期出港日期
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="items">出港日期信息</param>
        /// <returns></returns>
        public int SheZhiHangQiRiQi(string operatorId, string hangQiId, IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_SheZhiRiQi");
            _db.AddInParameter(cmd, "@RiQiXml", DbType.String, CreateHangQiRiQiXml(items));
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
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
        /// 获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId, string riQiId)
        {
            IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHangQiJiaGes);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, riQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiJiaGeInfo();

                    item.BinKeLeiXingId = rdr.GetInt32(rdr.GetOrdinal("BinKeLeiXingId"));
                    item.FangCha = rdr.GetDecimal(rdr.GetOrdinal("FangCha"));
                    item.FangXingId = rdr.GetInt32(rdr.GetOrdinal("FangXingId"));
                    item.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    item.HangQiId = hangQiId;
                    item.JiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    item.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    item.JiaGe3 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe3"));
                    item.JiaGe4 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe4"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.LouCeng = rdr["LouCeng"].ToString();
                    item.RiQiId = riQiId;
                    item.RongNaRenShu = rdr.GetInt32(rdr.GetOrdinal("RongNaRenShu"));
                    item.ShuoMing = rdr["ShuoMing"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 批量添加时获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="hangxianId">航线编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId,int hangxianId)
        {
            var items = new List<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo>();
            var s = new StringBuilder();

            s.Append(" SELECT  A.MingChengId ,");
            s.Append("         B.CountryId ,");
            s.Append("         C.RenYuanLeiXing ,");
            s.Append("         B.BasePrice ,");
            s.Append("         C.BeiShu ,");
            s.Append("         C.BeiShu1 ,");
            s.Append("         C.BeiShu2 ,");
            s.Append("         B.BasePrice * C.BeiShu JiaGe ,");
            s.Append("         B.BasePrice * C.BeiShu1 JiaGe1 ,");
            s.Append("         B.BasePrice * C.BeiShu2 JiaGe2");
            s.Append(" FROM    tbl_YL_ChuanZhiFangXing A ,");
            s.Append("         tbl_YL_ChuanZhiBasePrice B ,");
            s.Append("         tbl_YL_ChuanZhiFangXingBeiShu C");
            s.Append(" WHERE   A.ChuanZhiId IN ( SELECT    H.ChuanZhiId");
            s.Append("                           FROM      tbl_YL_HangQi H");
            s.Append("                           WHERE     H.HangQiId = @HangQiId )");
            s.Append("         AND A.ChuanZhiId = B.ChuanZhiId");
            s.Append("         AND A.FangXingId = C.FangXingId");
            s.Append("         AND B.HangXianId = @HangXianId");
            s.Append(" ORDER BY A.IdentityId ,");
            s.Append("         B.IdentityId ,");
            s.Append("         C.IdentityId ");
            //s.Append("         B.BasePrice ,");
            //s.Append("         C.BeiShu");

            DbCommand cmd = _db.GetSqlStringCommand(s.ToString());
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@HangXianId", DbType.Int32, hangxianId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiJiaGeInfo();

                    item.BinKeLeiXingId = rdr.GetInt32(rdr.GetOrdinal("RenYuanLeiXing"));
                    item.FangXingId = rdr.GetInt32(rdr.GetOrdinal("MingChengId"));
                    item.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.HangQiId = hangQiId;
                    item.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe"));
                    item.JiaGe4 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    item.JiaGe3 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    item.BasePrice = rdr.GetDecimal(rdr.GetOrdinal("BasePrice"));
                    item.BeiShu = rdr.GetDecimal(rdr.GetOrdinal("BeiShu"));
                    item.BeiShu1 = rdr.GetDecimal(rdr.GetOrdinal("BeiShu1"));
                    item.BeiShu2 = rdr.GetDecimal(rdr.GetOrdinal("BeiShu2"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取航期出港日期信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="d1">开始日期</param>
        /// <param name="d2">截止日期</param>
        /// <param name="isYouXiao">是否有效出港日期</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> GetHangQiRiQis(string hangQiId, DateTime? d1, DateTime? d2, bool? isYouXiao)
        {
            string sql = " SELECT *,(SELECT COUNT(*) FROM tbl_YL_HangQiJiaGe WHERE tbl_YL_HangQiJiaGe.RiQiId=tbl_YL_HangQiRiQi.RiQiId) AS IsJiaGe  ";

            sql += ",(SELECT ISNULL(MIN(CASE WHEN J.JiaGe1<>0 THEN J.JiaGe1 WHEN J.JiaGe2<>0 THEN J.JiaGe2 WHEN J.JiaGe3<>0 THEN J.JiaGe3 WHEN J.JiaGe4<>0 THEN J.JiaGe4 END),0) FROM dbo.tbl_YL_HangQiJiaGe J WHERE J.HangQiId=@HangQiId AND J.RiQiId=tbl_YL_HangQiRiQi.RiQiId AND J.BinKeLeiXingId IN(SELECT [XinXiId] FROM [tbl_YL_JiChuXinXi] WHERE T1=1)) AS QiShiJiaGe2";//当天最低价格
            //sql += ",(SELECT ISNULL(MIN(CASE WHEN J.JiaGe1<>0 THEN J.JiaGe1 WHEN J.JiaGe2<>0 THEN J.JiaGe2 WHEN J.JiaGe3<>0 THEN J.JiaGe3 WHEN J.JiaGe4<>0 THEN J.JiaGe4 END),0) FROM dbo.tbl_YL_HangQiJiaGe J WHERE J.HangQiId=@HangQiId AND J.RiQiId IN(SELECT R.RiQiId FROM tbl_YL_HangQiRiQi R WHERE R.HangQiId=@HangQiId AND DATEDIFF(MONTH,R.RiQi,tbl_YL_HangQiRiQi.RiQi)=0) AND J.BinKeLeiXingId IN(SELECT [XinXiId] FROM [tbl_YL_JiChuXinXi] WHERE T1=1)) AS QiShiJiaGe2";//当月最低价格

            sql += " FROM tbl_YL_HangQiRiQi WHERE HangQiId=@HangQiId ";
            if (d1.HasValue)
            {
                sql += " AND RiQi>=@D1 ";
            }
            if (d2.HasValue)
            {
                sql += " AND RiQi<=@D2 ";
            }
            if (isYouXiao.HasValue)
            {
                if (isYouXiao.Value)
                {
                    sql += " AND EXISTS(SELECT 1 FROM tbl_YL_HangQiJiaGe WHERE HangQiId=@HangQiId AND tbl_YL_HangQiRiQi.RiQiId=tbl_YL_HangQiJiaGe.RiQiId) ";
                    sql += string.Format(" AND RiQi>'{0}' ", DateTime.Now.AddDays(-1));
                }
                else
                {
                    sql += " AND NOT EXISTS(SELECT 1 FROM tbl_YL_HangQiJiaGe WHERE HangQiId=@HangQiId AND tbl_YL_HangQiRiQi.RiQiId=tbl_YL_HangQiJiaGe.RiQiId) ";
                    sql += string.Format(" AND RiQi<'{0}' ", DateTime.Now.AddDays(1));
                }
            }

            sql += " ORDER BY RiQi ASC ";


            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            if (d1.HasValue)
            {
                _db.AddInParameter(cmd, "D1", DbType.DateTime, d1.Value);
            }
            if (d2.HasValue)
            {
                _db.AddInParameter(cmd, "D2", DbType.DateTime, d2.Value);
            }

            IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiRiQiInfo>();

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiRiQiInfo();
                    item.HangQiId = hangQiId;
                    item.RenShu = rdr.GetInt32(rdr.GetOrdinal("RenShu"));
                    item.RiQi = rdr.GetDateTime(rdr.GetOrdinal("RiQi"));
                    item.RiQiId = rdr["RiQiId"].ToString();
                    item.IsJiaGe = rdr.GetInt32(rdr.GetOrdinal("IsJiaGe")) > 0;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("QiShiJiaGe2"))) item.QiShiJiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("QiShiJiaGe2"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置航期优惠规则
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">优惠共享方式</param>
        /// <param name="items">规则集合</param>
        /// <returns></returns>
        public int SheZhiHangQiYouHuiGuiZe(string operatorId, string hangQiId, EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi, IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_SheZhiYouHuiGuiZe");
            _db.AddInParameter(cmd, "@GuiZeXml", DbType.String, CreateHangQiYouHuiGuiZeXml(items));
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@FangShi", DbType.Byte, fangShi);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
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
        /// 获取航期优惠规则信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">多个共享优惠时的共享方式</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> GetHangQiyouHuiGuiZes(string hangQiId, out EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi)
        {
            fangShi = EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi.同时享有;
            IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHangQiyouHuiGuiZes);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo();
                    item.GuiZe = rdr["GuiZe"].ToString();
                    item.GuiZeId = rdr["GuiZeId"].ToString();
                    item.HangQiId = hangQiId;
                    item.IsGongXiang = rdr["IsGongXiang"].ToString() == "1";
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    item.YouXianJi = rdr.GetInt32(rdr.GetOrdinal("YouXianJi"));
                    item.MingCheng = rdr["MingCheng"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.FangShi = rdr.GetByte(rdr.GetOrdinal("FangShi"));

                    items.Add(item);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    fangShi = (EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi)rdr.GetByte(rdr.GetOrdinal("FangShi"));
                }
            }

            return items;
        }

        /// <summary>
        /// 设置航期标签
        /// </summary>
        /// <param name="info">实体</param>
        /// <param name="fangShi">0:设置 1:取消</param>
        /// <returns></returns>
        public int SheZhiHangQiBiaoQian(EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo info, int fangShi)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_SheZhiBiaoQian");
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@BiaoQian", DbType.Byte, info.BiaoQian);
            _db.AddInParameter(cmd, "@FangShi", DbType.Int32, fangShi);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
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
        /// 写入航期浏览记录信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int HangQiLiuLanJiLu_C(EyouSoft.Model.YlStructure.MHangQiLiuLanJiLuInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_HangQiLiuLanJiLu_C);
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "JiLuId", DbType.AnsiStringFixedLength, info.JiLuId);
            _db.AddInParameter(cmd, "YongHuId", DbType.AnsiStringFixedLength, info.YongHuId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 团购新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int TuanGou_CU(EyouSoft.Model.YlStructure.MTuanGouInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_TuanGou_CU");
            _db.AddInParameter(cmd, "@TuanGouId", DbType.AnsiStringFixedLength, info.TuanGouId);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, info.HangQiId);
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, info.RiQiId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@FengMian", DbType.String, info.FengMian);
            _db.AddInParameter(cmd, "@FangXingId", DbType.Int32, info.FangXingId);
            _db.AddInParameter(cmd, "@GuoJiId", DbType.Int32, info.GuoJiId);
            _db.AddInParameter(cmd, "@YuanJia", DbType.Decimal, info.YuanJia);
            _db.AddInParameter(cmd, "@JieZhiShiJian", DbType.DateTime, info.JieZhiShiJian);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@JiaGeXml", DbType.String, CreateTuanGouJiaGeXml(info.JiaGes));
            _db.AddInParameter(cmd, "@BianHao", DbType.String, info.BianHao);
            _db.AddInParameter(cmd, "@XianJia", DbType.Decimal, info.XianJia);
            _db.AddInParameter(cmd, "@BinKeLeiXing", DbType.String, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "@TuanGouShu", DbType.Int32, info.TuanGouShu);
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
        /// 删除团购信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        public int TuanGou_D(string companyId, string tuanGouId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_TuanGou_D");
            _db.AddInParameter(cmd, "@TuanGouId", DbType.AnsiStringFixedLength, tuanGouId);
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
        /// 获取团购信息
        /// </summary>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MTuanGouInfo GetTuanGouInfo(string tuanGouId)
        {
            EyouSoft.Model.YlStructure.MTuanGouInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetTuanGouInfo);
            _db.AddInParameter(cmd, "TuanGouId", DbType.AnsiStringFixedLength, tuanGouId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MTuanGouInfo();
                    info.FangXingId = rdr.GetInt32(rdr.GetOrdinal("FangXingId"));
                    info.FengMian = rdr["FengMian"].ToString();
                    info.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGes = null;
                    info.JieZhiShiJian = rdr.GetDateTime(rdr.GetOrdinal("JieZhiShiJian"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.RiQi = rdr.GetDateTime(rdr.GetOrdinal("RiQi"));
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.YuanJia = rdr.GetDecimal(rdr.GetOrdinal("YuanJia"));
                    info.BianHao = rdr["BianHao"].ToString();
                    info.XianJia = rdr.GetDecimal(rdr.GetOrdinal("XianJia"));
                    info.HangQiMingCheng = rdr["HangQiMingCheng"].ToString();
                    info.BinKeLeiXing = rdr["BinKeLeiXing"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();
                    info.YiYuDingRenShu = rdr.GetInt32(rdr.GetOrdinal("YiYuDingRenShu"));
                    info.TuanGouShu = rdr.GetInt32(rdr.GetOrdinal("TuanGouShu"));
                }
            }

            if (info != null)
            {
                info.JiaGes = GetTuanGouJiaGes(info.TuanGouId);
            }

            return info;
        }

        /// <summary>
        /// 获取团购集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MTuanGouInfo> GetTuanGous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MTuanGouChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MTuanGouInfo> items = new List<EyouSoft.Model.YlStructure.MTuanGouInfo>();
            string tableName = "view_YL_TuanGou";
            string fields = "*";
            string orderByString = "RiQi DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.ChuFaGangKouId.HasValue)
                {
                    sql.AppendFormat(" AND ChuFaGangKouId={0} ", chaXun.ChuFaGangKouId.Value);
                }
                if (chaXun.HangXianId.HasValue)
                {
                    sql.AppendFormat(" AND HangXianId={0} ", chaXun.HangXianId.Value);
                }
                if (chaXun.IsYouXiao.HasValue)
                {
                    if (chaXun.IsYouXiao.Value)
                    {
                        sql.AppendFormat(" AND RiQi>='{0}' AND JieZhiShiJian>='{0}' ", DateTime.Now);
                    }
                    else
                    {
                        sql.AppendFormat(" AND (RiQi<='{0}' OR JieZhiShiJian<='{0}') ", DateTime.Now);
                    }
                }
                if (chaXun.JiaGe1.HasValue)
                {
                    sql.AppendFormat(" AND XianJia>={0} ", chaXun.JiaGe1.Value);
                }
                if (chaXun.JiaGe2.HasValue)
                {
                    sql.AppendFormat(" AND XianJia<={0} ", chaXun.JiaGe2.Value);
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                if (chaXun.TianShu1.HasValue)
                {
                    sql.AppendFormat(" AND TianShu1>={0} ", chaXun.TianShu1.Value);
                }
                if (chaXun.TianShu2.HasValue)
                {
                    sql.AppendFormat(" AND TianShu1<={0} ", chaXun.TianShu2.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.MingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.MingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.BianHao))
                {
                    sql.AppendFormat(" AND BianHao LIKE '%{0}%' ", chaXun.BianHao);
                }

                switch (chaXun.PaiXu)
                {
                    case 0: orderByString = "RiQi DESC"; break;
                    case 1: orderByString = "RiQi ASC"; break;
                    case 2: orderByString = "XianJia DESC"; break;
                    case 3: orderByString = "XianJia ASC"; break;
                    case 4: orderByString = "XiaoLiang DESC"; break;
                    case 5: orderByString = "XiaoLiang ASC"; break;
                    case 6: orderByString = "PaiXuId DESC"; break;
                    case 7: orderByString = "PaiXuId ASC"; break;
                    case 8: orderByString = "TuanGouShu DESC"; break;
                    case 9: orderByString = "TuanGouShu ASC"; break;
                    default: orderByString = "RiQi DESC"; break;
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MTuanGouInfo();
                    info.FangXingId = rdr.GetInt32(rdr.GetOrdinal("FangXingId"));
                    info.FengMian = rdr["FengMian"].ToString();
                    info.GuoJiId = rdr.GetInt32(rdr.GetOrdinal("GuoJiId"));
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGes = null;
                    info.JieZhiShiJian = rdr.GetDateTime(rdr.GetOrdinal("JieZhiShiJian"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.RiQi = rdr.GetDateTime(rdr.GetOrdinal("RiQi"));
                    info.TuanGouId = rdr["TuanGouId"].ToString();
                    info.YuanJia = rdr.GetDecimal(rdr.GetOrdinal("YuanJia"));
                    info.XianJia = rdr.GetDecimal(rdr.GetOrdinal("XianJia"));

                    info.BianHao = rdr["BianHao"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.XiLieName = rdr["XiLieName"].ToString();
                    info.ChuanZhiName = rdr["ChuanZhiName"].ToString();
                    info.YiYuDingRenShu = rdr.GetInt32(rdr.GetOrdinal("YiYuDingRenShu"));
                    info.TuanGouShu = rdr.GetInt32(rdr.GetOrdinal("TuanGouShu"));

                    items.Add(info);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.JiaGes = GetTuanGouJiaGes(item.TuanGouId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取关联航期集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGuanLianHangQiInfo> GetGuanLianHangQis(string hangQiId)
        {
            IList<EyouSoft.Model.YlStructure.MGuanLianHangQiInfo> items = new List<EyouSoft.Model.YlStructure.MGuanLianHangQiInfo>();
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_GetGuanLianHangQi");
            _db.AddInParameter(cmd, "HangQiId", DbType.AnsiStringFixedLength, hangQiId);

            using (var rdr = DbHelper.RunReaderProcedure(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MGuanLianHangQiInfo();

                    item.HangQiId = rdr["HangQiId"].ToString();
                    item.HangXianMingCheng = rdr["HangXianMingCheng"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置出港日期预控人数
        /// </summary>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="renShu">人数</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int ShenZhiHangQiRiQiRenShu(string riQiId, int renShu, string operatorId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_ShenZhiRiQiRenShu");
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, riQiId);
            _db.AddInParameter(cmd, "@RenShu", DbType.Int32, renShu);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
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
        /// 获取出港日期信息
        /// </summary>
        /// <param name="riQiId">日期编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiRiQiInfo GetRiQiInfo(string riQiId)
        {
            EyouSoft.Model.YlStructure.MHangQiRiQiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetRiQiInfo);
            _db.AddInParameter(cmd, "RiQiId", DbType.AnsiStringFixedLength, riQiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MHangQiRiQiInfo();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.RenShu = rdr.GetInt32(rdr.GetOrdinal("RenShu"));
                    info.RiQi = rdr.GetDateTime(rdr.GetOrdinal("RiQi"));
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.YouXiaoDingDanRenShu = rdr.GetInt32(rdr.GetOrdinal("YouXiaoDingDanRenShu"));
                }
            }

            return info;
        }

        /// <summary>
        /// 取消航期日期，返回1成功，其它失败
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">日期编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int QuXiaoHangQiRiQi(string hangQiId, string riQiId, string operatorId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_HangQi_QuXiaoRiQi");
            _db.AddInParameter(cmd, "@RiQiId", DbType.AnsiStringFixedLength, riQiId);
            _db.AddInParameter(cmd, "@HangQiId", DbType.AnsiStringFixedLength, hangQiId);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);
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
        #endregion
    }
}
