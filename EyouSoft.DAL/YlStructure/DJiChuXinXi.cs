// 游轮基础信息 汪奇志 2014-03-24
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
    /// 游轮基础信息
    /// </summary>
    public class DJiChuXinXi : EyouSoft.Toolkit.DAL.DALBase, IDAL.YlStructure.IJiChuXinXi
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetJiChuXinXiInfo = "SELECT * FROM [tbl_YL_JiChuXinXi] WHERE XinXiId=@XinXiId";
        const string SQL_SELECT_GetGongSiInfo = "SELECT * FROM [tbl_YL_GongSi] WHERE GongSiId=@GongSiId";
        const string SQL_SELECT_GetGongSiFuJians = "SELECT * FROM [tbl_YL_GongSiFuJian]  WHERE GongSiId=@GongSiId";
        const string SQL_SELECT_GetXiLieInfo = "SELECT * FROM view_YL_XiLie WHERE XiLieId=@XiLieId";
        const string SQL_SELECT_GetXiLieFuJians = "SELECT * FROM [tbl_YL_XiLieFuJian]  WHERE XiLieId=@XiLieId";
        const string SQL_SELECT_GetChuanZhiInfo = "SELECT * FROM view_YL_ChuanZhi WHERE ChuanZhiId=@ChuanZhiId";
        const string SQL_SELECT_GetChuanZhiFangXings = "SELECT * FROM tbl_YL_ChuanZhiFangXing WHERE ChuanZhiId=@ChuanZhiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetChuanZhiMeiShis = "SELECT * FROM tbl_YL_ChuanZhiMeiShi WHERE ChuanZhiId=@ChuanZhiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetChuanZhiSheShis = "SELECT * FROM tbl_YL_ChuanZhiSheShi WHERE ChuanZhiId=@ChuanZhiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetChuanZhiFuJians = "SELECT * FROM tbl_YL_ChuanZhiFuJian WHERE ChuanZhiId=@ChuanZhiId";
        const string SQL_SELECT_GetChuanZhiPingMianTus = "SELECT * FROM tbl_YL_ChuanZhiPingMianTu WHERE ChuanZhiId=@ChuanZhiId";
        const string SQL_SELECT_GetChuanZhiFangXingFuJian = "SELECT * FROM tbl_YL_ChuanZhiFangXingFuJian WHERE FangXingId=@FangXingId ORDER BY FuJianId";

        const string SQL_INSERT_InsertShiPin = "INSERT INTO [tbl_YL_ShiPin]([ShiPinId],[CompanyId],[GongSiId],[XiLieId],[ChuanZhiId],[MingCheng],[Filepath],[XiangXiJieShao],[IssueTime],[OperatorId],[Filepath1])VALUES(@ShiPinId,@CompanyId,@GongSiId,@XiLieId,@ChuanZhiId,@MingCheng,@Filepath,@XiangXiJieShao,@IssueTime,@OperatorId,@Filepath1)";
        const string SQL_UPDATE_UpdateShiPin = "UPDATE [tbl_YL_ShiPin] SET [GongSiId] = @GongSiId,[XiLieId] = @XiLieId,[ChuanZhiId] = @ChuanZhiId,[MingCheng] = @MingCheng,[Filepath] = @Filepath,[XiangXiJieShao] = @XiangXiJieShao,[Filepath1]=@Filepath1 WHERE [ShiPinId] = @ShiPinId";
        const string SQL_SELECT_GetShiPinInfo = "SELECT * FROM view_YL_ShiPin WHERE ShiPinId=@ShiPinId";
        const string SQL_DELETE_DeleteShiPin = "DELETE FROM tbl_YL_ShiPin WHERE ShiPinId=@ShiPinId";
        const string SQL_SELECT_GetBasePrice = "SELECT * FROM tbl_YL_ChuanZhiBasePrice WHERE ChuanZhiId=@ChuanZhiId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetFangXingBeiShu = "SELECT * FROM tbl_YL_ChuanZhiFangXingBeiShu WHERE FangXingId=@FangXingId ORDER BY IdentityId ASC";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DJiChuXinXi()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// 创建船只房型附件xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateChuanZhiFangXingFuJianXml(IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FuJianId=\"{0}\" FangXingId=\"{1}\" LeiXing=\"{2}\" Filepath=\"{3}\" MiaoShu=\"{4}\"></info>", item.FuJianId
                        , item.FangXingId
                        , item.LeiXing, item.Filepath, Utils.ReplaceXmlSpecialCharacter(item.MiaoShu));
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }
        /// <summary>
        /// create baseprice xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateBasePriceXml(IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info Id=\"{0}\" ChuanZhiId=\"{1}\" CountryId=\"{2}\" BasePrice=\"{3}\" HangXianId=\"{4}\"></info>", item.Id
                        , item.ChuanZhiId
                        , item.CountryId, item.BasePrice, item.HangXianId);
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }
        /// <summary>
        /// create fangxingbeishu xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFangXingBeiShuXml(IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingBeiShu> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info Id=\"{0}\" FangXingId=\"{1}\" RenYuanLeiXing=\"{2}\" BeiShu=\"{3}\" BeiShu1=\"{4}\" BeiShu2=\"{5}\"></info>", item.Id
                        , item.FangXingId
                        , item.RenYuanLeiXing, item.BeiShu, item.BeiShu1, item.BeiShu2);
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
        /// get fangxingbeishu
        /// </summary>
        /// <param name="fangxingid"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingBeiShu> GetFangXingBeiShus(string fangxingid)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingBeiShu> items = new List<EyouSoft.Model.YlStructure.MChuanZhiFangXingBeiShu>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetFangXingBeiShu);
            _db.AddInParameter(cmd, "FangXingId", DbType.AnsiStringFixedLength, fangxingid);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiFangXingBeiShu();
                    item.Id = rdr["Id"].ToString();
                    item.FangXingId = rdr["FangXingId"].ToString();
                    item.RenYuanLeiXing = rdr.GetInt32(rdr.GetOrdinal("RenYuanLeiXing"));
                    item.BeiShu = rdr.GetDecimal(rdr.GetOrdinal("BeiShu"));
                    item.BeiShu1 = rdr.GetDecimal(rdr.GetOrdinal("BeiShu1"));
                    item.BeiShu2 = rdr.GetDecimal(rdr.GetOrdinal("BeiShu2"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get gongsi fujian
        /// </summary>
        /// <param name="gongSiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetGongSiFuJians(string gongSiId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGongSiFuJians);
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, gongSiId);

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
        /// get xilie fujian
        /// </summary>
        /// <param name="xiLieId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetXiLieFuJians(string xiLieId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetXiLieFuJians);
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, xiLieId);

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
        /// create fangxing xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFangXingXml(IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info FangXingId=\"{0}\" Filepath=\"{1}\" MingChengId=\"{2}\">", item.FangXingId, item.Filepath,item.MingChengId);
                    xml.AppendFormat("<MingCheng><![CDATA[{0}]]></MingCheng>", item.MingCheng);
                    xml.AppendFormat("<ShuLiang><![CDATA[{0}]]></ShuLiang>", item.ShuLiang);
                    xml.AppendFormat("<MianJi><![CDATA[{0}]]></MianJi>", item.MianJi);
                    xml.AppendFormat("<LouCeng><![CDATA[{0}]]></LouCeng>", item.LouCeng);
                    xml.AppendFormat("<JieGou><![CDATA[{0}]]></JieGou>", item.JieGou);
                    xml.AppendFormat("<ChuangWeiPeiZhi><![CDATA[{0}]]></ChuangWeiPeiZhi>", item.ChuangWeiPeiZhi);
                    xml.AppendFormat("<SheShi><![CDATA[{0}]]></SheShi>", item.SheShi);
                    xml.AppendFormat("<YongPin><![CDATA[{0}]]></YongPin>", item.YongPin);
                    xml.AppendFormat("<JieShao><![CDATA[{0}]]></JieShao>", item.JieShao);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// create meishi xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateMeiShiXml(IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info MeiShiId=\"{0}\" Filepath=\"{1}\">", item.MeiShiId, item.Filepath);
                    xml.AppendFormat("<MingCheng><![CDATA[{0}]]></MingCheng>", item.MingCheng);
                    xml.AppendFormat("<MiaoShu><![CDATA[{0}]]></MiaoShu>", item.MiaoShu);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// create sheshi xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateSheShiXml(IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> items)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    xml.AppendFormat("<info SheShiId=\"{0}\" Filepath=\"{1}\">", item.SheShiId, item.Filepath);
                    xml.AppendFormat("<MingCheng><![CDATA[{0}]]></MingCheng>", item.MingCheng);
                    xml.AppendFormat("<MiaoShu><![CDATA[{0}]]></MiaoShu>", item.MiaoShu);
                    xml.Append("</info>");
                }
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// get chuanzhi fangxings
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo> GetChuanZhiFangXings(string chuanZhiId)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiFangXings);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiFangXingInfo();
                    item.ChuangWeiPeiZhi = rdr["ChuangWeiPeiZhi"].ToString();
                    item.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    item.FangXingId = rdr["FangXingId"].ToString();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.JieGou = rdr["JieGou"].ToString();
                    item.JieShao = rdr["JieShao"].ToString();
                    item.LouCeng = rdr["LouCeng"].ToString();
                    item.MianJi = rdr["MianJi"].ToString();
                    item.MingChengId = rdr["MingChengId"].ToString();
                    item.MingCheng = rdr["MingCheng"].ToString();
                    item.SheShi = rdr["SheShi"].ToString();
                    item.ShuLiang = rdr["ShuLiang"].ToString();
                    item.YongPin = rdr["YongPin"].ToString();
                    item.BeiShus = GetFangXingBeiShus(item.FangXingId);
                    item.FuJians = GetChuanZhiFangXingFuJian(item.FangXingId);
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get chuanzhi meishis
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> GetChuanZhiMeiShis(string chuanZhiId)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiMeiShis);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.MeiShiId = rdr["MeiShiId"].ToString();
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    item.MingCheng = rdr["MingCheng"].ToString();
                    item.FuJians = GetChuanZhiFangXingFuJian(item.MeiShiId);
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get chuanzhi sheshis
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> GetChuanZhiSheShis(string chuanZhiId)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiSheShis);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.SheShiId = rdr["SheShiId"].ToString();
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    item.MingCheng = rdr["MingCheng"].ToString();
                    item.FuJians = GetChuanZhiFangXingFuJian(item.SheShiId);
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get chuanzhi fujians
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetChuanZhiFuJians(string chuanZhiId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiFuJians);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

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
        /// get chuanzhi pingmiantus
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MFuJianInfo> GetChuanZhiPingMianTus(string chuanZhiId)
        {
            IList<EyouSoft.Model.YlStructure.MFuJianInfo> items = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiPingMianTus);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

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
        /// 根据房型编号获取船只房型附件信息集合
        /// </summary>
        /// <param name="fangxingId">房型编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> GetChuanZhiFangXingFuJian(string fangxingId)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> items = new List<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiFangXingFuJian);
            _db.AddInParameter(cmd, "FangXingId", DbType.AnsiStringFixedLength, fangxingId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.FuJianId = rdr.GetInt32(rdr.GetOrdinal("FuJianId"));
                    item.LeiXing = rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    item.MiaoShu = rdr["MiaoShu"].ToString();
                    item.FangXingId = rdr["FangXingId"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region IJiChuXinXi 成员
        /// <summary>
        /// 写入、修改基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JiChuXinXi_CU(EyouSoft.Model.YlStructure.MJiChuXinXiInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiChuXinXi_CU");
            _db.AddInParameter(cmd, "XinXiId", DbType.Int32, info.XinXiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, (byte)info.LeiXing);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "ChangJingLeiXing", DbType.Byte, info.ChangJingLeiXing);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddOutParameter(cmd, "RetXinXiId", DbType.Int32, 4);
            _db.AddInParameter(cmd, "PaiXuId", DbType.Int32, info.PaiXuId);
            _db.AddInParameter(cmd, "BieMing", DbType.String, info.BieMing);
            _db.AddInParameter(cmd, "PXinXiId", DbType.Int32, info.PXinXiId);

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
                if (info.XinXiId == 0) info.XinXiId = Utils.GetInt(_db.GetParameterValue(cmd, "RetXinXiId").ToString());
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MJiChuXinXiInfo GetJiChuXinXiInfo(int jiChuXinXiId)
        {
            EyouSoft.Model.YlStructure.MJiChuXinXiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiChuXinXiInfo);
            _db.AddInParameter(cmd, "XinXiId", DbType.Int32, jiChuXinXiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MJiChuXinXiInfo();
                    info.ChangJingLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("ChangJingLeiXing"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XinXiId = jiChuXinXiId;
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                    info.BieMing = rdr["BieMing"].ToString();
                    info.PXinXiId = rdr.GetInt32(rdr.GetOrdinal("PXinXiId"));
                }
            }

            return info;
        }

        /// <summary>
        /// 删除基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        public int DeleteJiChuXinXi(string companyId, int jiChuXinXiId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_JiChuXinXi_D");
            _db.AddInParameter(cmd, "XinXiId", DbType.Int32, jiChuXinXiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
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
        /// 获取基础信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MJiChuXinXiInfo> GetJiChuXinXis(string companyId, EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo chaXun)
        {
            #region sql
            string sql = string.Format("SELECT * FROM [tbl_YL_JiChuXinXi] WHERE IsDelete='0' AND CompanyId='{0}' ", companyId);
            if (chaXun != null)
            {
                sql += string.Format(" AND LeiXing={0} ", (int)chaXun.LeiXing);

                if (chaXun.YouLunLeiXing.HasValue)
                {
                    sql += string.Format(" AND ChangJingLeiXing={0} ", (int)chaXun.YouLunLeiXing.Value);
                }
            }
            sql += " ORDER BY PaiXuId ASC, XinXiId ASC ";
            #endregion

            IList<EyouSoft.Model.YlStructure.MJiChuXinXiInfo> items = new List<EyouSoft.Model.YlStructure.MJiChuXinXiInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(sql);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MJiChuXinXiInfo();
                    info.ChangJingLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("ChangJingLeiXing"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XinXiId = rdr.GetInt32(rdr.GetOrdinal("XinXiId"));
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                    info.BieMing = rdr["BieMing"].ToString();
                    info.PXinXiId = rdr.GetInt32(rdr.GetOrdinal("PXinXiId"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 写入、修改游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int GongSi_CU(EyouSoft.Model.YlStructure.MGongSiInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_GongSi_CU");
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, (byte)info.LeiXing);
            _db.AddInParameter(cmd, "JianYaoJieShao", DbType.String, info.JianYaoJieShao);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "Logo", DbType.String, info.Logo);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "FuJianXML", DbType.String, CreateFuJianXml(info.FuJians));
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "PaiXuId", DbType.Int32, info.PaiXuId);
            _db.AddInParameter(cmd, "YouShi", DbType.String, info.YouShi);
            _db.AddInParameter(cmd, "RongYu", DbType.String, info.RongYu);

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
        /// 获取游轮公司信息
        /// </summary>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MGongSiInfo GetGongSiInfo(string gongSiId)
        {
            EyouSoft.Model.YlStructure.MGongSiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGongSiInfo);
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, gongSiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MGongSiInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FuJians = null;
                    info.GongSiId = gongSiId;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                    info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    info.Logo = rdr["Logo"].ToString();
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                    info.YouShi = rdr["YouShi"].ToString();
                    info.RongYu = rdr["RongYu"].ToString();

                }
            }

            if (info != null) info.FuJians = GetGongSiFuJians(gongSiId);

            return info;
        }

        /// <summary>
        /// 删除游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        public int DeleteGongSi(string companyId, string gongSiId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_GongSi_D");
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, gongSiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
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
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MGongSiInfo> items = new List<EyouSoft.Model.YlStructure.MGongSiInfo>();

            string tableName = "tbl_YL_GongSi";
            string fields = "*";
            string orderByString = "PaiXuId ASC,IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.GongSiLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.GongSiLeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                items=GetGongSis(rdr);

            }

            return items;
        }

        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MGongSiInfo> items = new List<EyouSoft.Model.YlStructure.MGongSiInfo>();

            string tableName = "tbl_YL_GongSi";
            string fields = "*";
            string orderByString = "PaiXuId ASC,IssueTime DESC";
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select {2} from {1} where CompanyId='{0}' AND IsDelete='0' ", companyId,tableName,fields);

            if (chaXun != null)
            {
                if (chaXun.GongSiLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.GongSiLeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
            }
            sql.AppendFormat(" order by {0}", orderByString);
            var cmd = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,_db))
            {
                items = GetGongSis(rdr);

            }

            return items;
        }

        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(IDataReader rdr)
        {
            IList<EyouSoft.Model.YlStructure.MGongSiInfo> items = new List<EyouSoft.Model.YlStructure.MGongSiInfo>();
            while (rdr.Read())
            {
                var info = new EyouSoft.Model.YlStructure.MGongSiInfo();

                info.CompanyId = rdr["CompanyId"].ToString();
                info.FuJians = null;
                info.GongSiId = rdr["GongSiId"].ToString();
                info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                info.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                info.Logo = rdr["Logo"].ToString();
                info.MingCheng = rdr["MingCheng"].ToString();
                info.OperatorId = rdr["OperatorId"].ToString();
                info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));


                items.Add(info);
            }
            return items;
        }

        /// <summary>
        /// 写入、修改游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int XiLie_CU(EyouSoft.Model.YlStructure.MXiLieInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_XiLie_CU");
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "Companyid", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "JianYaoJieShao", DbType.String, info.JianYaoJieShao);
            _db.AddInParameter(cmd, "XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "FuJianXml", DbType.AnsiStringFixedLength, CreateFuJianXml(info.FuJians));
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "PaiXuId", DbType.Int32, info.PaiXuId);

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
        /// 获取游轮系列信息
        /// </summary>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MXiLieInfo GetXiLieInfo(string xiLieId)
        {
            EyouSoft.Model.YlStructure.MXiLieInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetXiLieInfo);
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, xiLieId);
            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MXiLieInfo();

                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.FuJians = null;
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                    info.XiLieId = xiLieId;
                    info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));
                }
            }

            if (info != null) info.FuJians = GetXiLieFuJians(xiLieId);

            return info;
        }

        /// <summary>
        /// 删除游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        public int DeleteXiLie(string companyId, string xiLieId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_XiLie_D");
            _db.AddInParameter(cmd, "XiLieId", DbType.AnsiStringFixedLength, xiLieId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
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
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MXiLieInfo> items = new List<EyouSoft.Model.YlStructure.MXiLieInfo>();

            string tableName = "view_YL_XiLie";
            string fields = "*";
            string orderByString = "PaiXuId ASC,IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND GongSiMingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.XiLieMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    sql.AppendFormat(" AND GongSiId='{0}' ", chaXun.GongSiId);
                }
                if (chaXun.GongSiLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND GongSiLeiXing={0} ", (int)chaXun.GongSiLeiXing);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                items = GetXiLies(rdr);
            }

            return items;
        }

        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MXiLieInfo> items = new List<EyouSoft.Model.YlStructure.MXiLieInfo>();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from view_YL_XiLie where CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND GongSiMingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.XiLieMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    sql.AppendFormat(" AND GongSiId='{0}' ", chaXun.GongSiId);
                }
                if (chaXun.GongSiLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND GongSiLeiXing={0} ", (int)chaXun.GongSiLeiXing);
                }
            }
            sql.Append(" order by PaiXuId ASC,IssueTime DESC");
            var cmd = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,_db))
            {
                items = GetXiLies(rdr);
            }

            return items;
        }

        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(IDataReader rdr)
        {
            IList<EyouSoft.Model.YlStructure.MXiLieInfo> items = new List<EyouSoft.Model.YlStructure.MXiLieInfo>();
            while (rdr.Read())
            {
                var info = new EyouSoft.Model.YlStructure.MXiLieInfo();

                info.CompanyId = rdr["CompanyId"].ToString();
                info.FuJians = null;
                info.GongSiId = rdr["GongSiId"].ToString();
                info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                info.MingCheng = rdr["MingCheng"].ToString();
                info.OperatorId = rdr["OperatorId"].ToString();
                info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                info.XiLieId = rdr["XiLieId"].ToString();
                info.GongSiLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("GongSiLeiXing"));
                info.PaiXuId = rdr.GetInt32(rdr.GetOrdinal("PaiXuId"));

                items.Add(info);
            }
            return items;
        }

        /// <summary>
        /// 写入、修改游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChuanZhi_CU(EyouSoft.Model.YlStructure.MChuanZhiInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_ChuanZhi_CU");
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@MingCheng1", DbType.String, info.MingCheng1);
            _db.AddInParameter(cmd, "@XingJi", DbType.Byte, info.XingJi);
            _db.AddInParameter(cmd, "@DunWei", DbType.String, info.DunWei);
            _db.AddInParameter(cmd, "@XiaShuiRiQi", DbType.String, info.XiaShuiRiQi);
            _db.AddInParameter(cmd, "@ZhuangXiuRiQi", DbType.String, info.ZhuangXiuRiQi);
            _db.AddInParameter(cmd, "@ZaiKeLiang", DbType.String, info.ZaiKeLiang);
            _db.AddInParameter(cmd, "@JiaBanLouCeng", DbType.String, info.JiaBanLouCeng);
            _db.AddInParameter(cmd, "@KeFangShuLiang", DbType.String, info.KeFangShuLiang);
            _db.AddInParameter(cmd, "@ChuanYuan", DbType.String, info.ChuanYuan);
            _db.AddInParameter(cmd, "@ChangDu", DbType.String, info.ChangDu);
            _db.AddInParameter(cmd, "@KuangDu", DbType.String, info.KuangDu);
            _db.AddInParameter(cmd, "@ChuanJi", DbType.String, info.ChuanJi);
            _db.AddInParameter(cmd, "@ChiShui", DbType.String, info.ChiShui);
            _db.AddInParameter(cmd, "@ZuiGaoHangSu", DbType.String, info.ZuiGaoHangSu);
            _db.AddInParameter(cmd, "@JianYaoJieShao", DbType.String, info.JianYaoJieShao);
            _db.AddInParameter(cmd, "@XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@ChuanZaiDianHua", DbType.String, info.ChuanZaiDianHua);
            _db.AddInParameter(cmd, "@ChuanZhiFuJianXml", DbType.String, CreateFuJianXml(info.FuJians));
            _db.AddInParameter(cmd, "@FangXingXml", DbType.String, CreateFangXingXml(info.FangXings));
            _db.AddInParameter(cmd, "@MeiShiXML", DbType.String, CreateMeiShiXml(info.MeiShis));
            _db.AddInParameter(cmd, "@PingMianTuXml", DbType.String, CreateFuJianXml(info.PingMianTus));
            _db.AddInParameter(cmd, "@SheShiXml", DbType.String, CreateSheShiXml(info.SheShis));
            _db.AddInParameter(cmd, "@XML", DbType.String, CreateFangXingBeiShuXml(info.BeiShus));
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
        /// 获取游轮船只信息
        /// </summary>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MChuanZhiInfo GetChuanZhiInfo(string chuanZhiId)
        {
            EyouSoft.Model.YlStructure.MChuanZhiInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetChuanZhiInfo);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MChuanZhiInfo();

                    info.ChangDu = rdr["ChangDu"].ToString();
                    info.ChiShui = rdr["ChiShui"].ToString();
                    info.ChuanJi = rdr["ChuanJi"].ToString();
                    info.ChuanYuan = rdr["ChuanYuan"].ToString();
                    info.ChuanZaiDianHua = rdr["ChuanZaiDianHua"].ToString();
                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DunWei = rdr["DunWei"].ToString();
                    info.FangXings = null;
                    info.FuJians = null;
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaBanLouCeng = rdr["JiaBanLouCeng"].ToString();
                    info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                    info.KeFangShuLiang = rdr["KeFangShuLiang"].ToString();
                    info.KuangDu = rdr["KuangDu"].ToString();
                    info.MeiShis = null;
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.MingCheng1 = rdr["MingCheng1"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.PingMianTus = null;
                    info.SheShis = null;
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                    info.XiaShuiRiQi = rdr["XiaShuiRiQi"].ToString();
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.XiLieMingCheng = rdr["XiLieMingCheng"].ToString();
                    info.XingJi = (EyouSoft.Model.EnumType.YlStructure.XingJi)rdr.GetByte(rdr.GetOrdinal("XingJi"));
                    info.ZaiKeLiang = rdr["ZaiKeLiang"].ToString();
                    info.ZhuangXiuRiQi = rdr["ZhuangXiuRiQi"].ToString();
                    info.ZuiGaoHangSu = rdr["ZuiGaoHangSu"].ToString();
                    info.GongSiLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("GongSiLeiXing"));
                }
            }

            if (info != null)
            {
                info.FangXings = GetChuanZhiFangXings(info.ChuanZhiId);
                info.FuJians = GetChuanZhiFuJians(info.ChuanZhiId);
                info.MeiShis = GetChuanZhiMeiShis(info.ChuanZhiId);
                info.PingMianTus = GetChuanZhiPingMianTus(info.ChuanZhiId);
                info.SheShis = GetChuanZhiSheShis(info.ChuanZhiId);
                if (info.FangXings != null)
                {
                    info.BeiShus = new List<Model.YlStructure.MChuanZhiFangXingBeiShu>();
                    foreach (var mf in info.FangXings)
                    {
                        foreach (var mb in mf.BeiShus)
                        {
                            info.BeiShus.Add(mb);
                        }
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// 根据出港日期和航线编号获取船只信息
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="riqi">出港日期</param>
        /// <param name="hangxianid">航线编号</param>
        /// <returns>船只列表</returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhiInfo(string companyid, DateTime riqi, int hangxianid)
        {
            var l = new List<EyouSoft.Model.YlStructure.MChuanZhiInfo>();
            EyouSoft.Model.YlStructure.MChuanZhiInfo info = null;
            var s =new StringBuilder();

            s.Append(" SELECT  C.MingCheng ,");
            s.Append("         H.HangQiId ,");
            s.Append("         H.LeiXing ,");
            s.Append("         T.RiQiId");
            s.Append(" FROM    dbo.view_YL_ChuanZhi C");
            s.Append("         INNER JOIN dbo.view_YL_HangQi H ON C.ChuanZhiId = H.ChuanZhiId AND H.IsYouXiao=1");
            s.Append("         INNER JOIN dbo.tbl_YL_HangQiRiQi T ON H.HangQiId = T.HangQiId");
            s.Append(" WHERE   C.CompanyId = @CompanyId");
            s.Append("         AND H.HangXianId = @HangXianId");
            s.Append("         AND T.RiQi = @RiQi");
            s.Append("         AND C.IsDelete = 0");

            DbCommand cmd = _db.GetSqlStringCommand(s.ToString());
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyid);
            _db.AddInParameter(cmd, "RiQi", DbType.DateTime, riqi);
            _db.AddInParameter(cmd, "HangXianId", DbType.Int32, hangxianid);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MChuanZhiInfo();

                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.HangQiId = rdr["HangQiId"].ToString();
                    info.RiQiId = rdr["RiQiId"].ToString();
                    info.YouLunLeiXing = (Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));

                    l.Add(info);
                }
            }

            return l;
        }

        /// <summary>
        /// 删除游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        public int DeleteChuanZhi(string companyId, string chuanZhiId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_ChuanZhi_D");
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanZhiId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
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
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiInfo>();

            string tableName = "view_YL_ChuanZhi";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.ChuanZhiMingCheng); 
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND GongSiMingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieMingCheng))
                {
                    sql.AppendFormat(" AND XiLieMingCheng LIKE '%{0}%' ", chaXun.XiLieMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieId))
                {
                    sql.AppendFormat(" AND XiLieId='{0}' ", chaXun.XiLieId);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                items = GetChuanZhis(rdr);
            }

            return items;
        }

        /// <summary>
        /// 获取船只列表
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(IDataReader rdr)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiInfo>();

            while (rdr.Read())
            {
                var info = new EyouSoft.Model.YlStructure.MChuanZhiInfo();

                info.ChangDu = rdr["ChangDu"].ToString();
                info.ChiShui = rdr["ChiShui"].ToString();
                info.ChuanJi = rdr["ChuanJi"].ToString();
                info.ChuanYuan = rdr["ChuanYuan"].ToString();
                info.ChuanZaiDianHua = rdr["ChuanZaiDianHua"].ToString();
                info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                info.CompanyId = rdr["CompanyId"].ToString();
                info.DunWei = rdr["DunWei"].ToString();
                info.FangXings = null;
                info.FuJians = null;
                info.GongSiId = rdr["GongSiId"].ToString();
                info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                info.JiaBanLouCeng = rdr["JiaBanLouCeng"].ToString();
                info.JianYaoJieShao = rdr["JianYaoJieShao"].ToString();
                info.KeFangShuLiang = rdr["KeFangShuLiang"].ToString();
                info.KuangDu = rdr["KuangDu"].ToString();
                info.MeiShis = null;
                info.MingCheng = rdr["MingCheng"].ToString();
                info.MingCheng1 = rdr["MingCheng1"].ToString();
                info.OperatorId = rdr["OperatorId"].ToString();
                info.PingMianTus = null;
                info.SheShis = null;
                info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                info.XiaShuiRiQi = rdr["XiaShuiRiQi"].ToString();
                info.XiLieId = rdr["XiLieId"].ToString();
                info.XiLieMingCheng = rdr["XiLieMingCheng"].ToString();
                info.XingJi = (EyouSoft.Model.EnumType.YlStructure.XingJi)rdr.GetByte(rdr.GetOrdinal("XingJi"));
                info.ZaiKeLiang = rdr["ZaiKeLiang"].ToString();
                info.ZhuangXiuRiQi = rdr["ZhuangXiuRiQi"].ToString();
                info.ZuiGaoHangSu = rdr["ZuiGaoHangSu"].ToString();
                info.GongSiLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)rdr.GetByte(rdr.GetOrdinal("GongSiLeiXing"));

                if (info != null)
                {
                    info.FangXings = GetChuanZhiFangXings(info.ChuanZhiId);
                    info.FuJians = GetChuanZhiFuJians(info.ChuanZhiId);
                    info.MeiShis = GetChuanZhiMeiShis(info.ChuanZhiId);
                    info.PingMianTus = GetChuanZhiPingMianTus(info.ChuanZhiId);
                    info.SheShis = GetChuanZhiSheShis(info.ChuanZhiId);
                }

                items.Add(info);
            }

            return items;
        }

        /// <summary>
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> items = new List<EyouSoft.Model.YlStructure.MChuanZhiInfo>();
            StringBuilder sql = new StringBuilder("select * from view_YL_ChuanZhi where");
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.ChuanZhiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND GongSiMingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieMingCheng))
                {
                    sql.AppendFormat(" AND XiLieMingCheng LIKE '%{0}%' ", chaXun.XiLieMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieId))
                {
                    sql.AppendFormat(" AND XiLieId='{0}' ", chaXun.XiLieId);
                }
            }

            sql.Append(" order by IssueTime DESC");

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,_db))
            {
                items = GetChuanZhis(rdr);                
            }

            return items;
        }

        /*
        public int InsertMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info)
        {
            throw new NotImplementedException();
        }

        public int UpdateMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info)
        {
            throw new NotImplementedException();
        }

        public EyouSoft.Model.YlStructure.MMuDiDiInfo GetMuDiDiInfo(string muDiDiId)
        {
            throw new NotImplementedException();
        }

        public int DeleteMuDiDi(string companyId, string muDiDiId)
        {
            throw new NotImplementedException();
        }

        public IList<EyouSoft.Model.YlStructure.MMuDiDiInfo> GetMuDiDis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MMuDiDiChaXunInfo chaXun)
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// 写入游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertShiPin);
            _db.AddInParameter(cmd, "@ShiPinId", DbType.AnsiStringFixedLength, info.ShiPinId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "@XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Filepath1", DbType.String, info.ShiPinIMG);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateShiPin);
            _db.AddInParameter(cmd, "@ShiPinId", DbType.AnsiStringFixedLength, info.ShiPinId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@XiLieId", DbType.AnsiStringFixedLength, info.XiLieId);
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, info.ChuanZhiId);
            _db.AddInParameter(cmd, "@MingCheng", DbType.String, info.MingCheng);
            _db.AddInParameter(cmd, "@Filepath", DbType.String, info.Filepath);
            _db.AddInParameter(cmd, "@XiangXiJieShao", DbType.String, info.XiangXiJieShao);
            _db.AddInParameter(cmd, "Filepath1", DbType.String, info.ShiPinIMG);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取游轮视频信息
        /// </summary>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MShiPinInfo GetShiPinInfo(string shiPinId)
        {
            EyouSoft.Model.YlStructure.MShiPinInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetShiPinInfo);
            _db.AddInParameter(cmd, "ShiPinId", DbType.AnsiStringFixedLength, shiPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.YlStructure.MShiPinInfo();

                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.ChuanZhiMingCheng = rdr["ChuanZhiMingCheng"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ShiPinId = rdr["ShiPinId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.XiLieMingCheng = rdr["XiLieMingCheng"].ToString();
                    info.ShiPinIMG = rdr["Filepath1"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 删除游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        public int DeleteShiPin(string companyId, string shiPinId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteShiPin);
            _db.AddInParameter(cmd, "ShiPinId", DbType.AnsiStringFixedLength, shiPinId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取游轮视频信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MShiPinInfo> GetShiPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MShiPinChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.YlStructure.MShiPinInfo> items = new List<EyouSoft.Model.YlStructure.MShiPinInfo>();

            string tableName = "view_YL_ShiPin";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiMingCheng))
                {
                    sql.AppendFormat(" AND ChuanZhiMingCheng LIKE '%{0}%' ", chaXun.ChuanZhiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiMingCheng))
                {
                    sql.AppendFormat(" AND GongSiMingCheng LIKE '%{0}%' ", chaXun.GongSiMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.XiLieMingCheng))
                {
                    sql.AppendFormat(" AND XiLieMingCheng LIKE '%{0}%' ", chaXun.XiLieMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.ShiPinMingCheng))
                {
                    sql.AppendFormat(" AND MingCheng LIKE '%{0}%' ", chaXun.ShiPinMingCheng);
                }
                if (!string.IsNullOrEmpty(chaXun.ChuanZhiId))
                {
                    sql.AppendFormat(" AND ChuanZhiId='{0}' ", chaXun.ChuanZhiId);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.YlStructure.MShiPinInfo();

                    info.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    info.ChuanZhiMingCheng = rdr["ChuanZhiMingCheng"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.Filepath = rdr["Filepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiMingCheng = rdr["GongSiMingCheng"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MingCheng = rdr["MingCheng"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.ShiPinId = rdr["ShiPinId"].ToString();
                    info.XiangXiJieShao = rdr["XiangXiJieShao"].ToString();
                    info.XiLieId = rdr["XiLieId"].ToString();
                    info.XiLieMingCheng = rdr["XiLieMingCheng"].ToString();
                    info.ShiPinIMG = rdr["Filepath1"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 根据船只编号获取基础价格列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> GetBasePrice(string chuanzhiId)
        {
            IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> items = new List<EyouSoft.Model.YlStructure.MChuanZhiBasePrice>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetBasePrice);
            _db.AddInParameter(cmd, "ChuanZhiId", DbType.AnsiStringFixedLength, chuanzhiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.YlStructure.MChuanZhiBasePrice();
                    item.Id = rdr["Id"].ToString();
                    item.ChuanZhiId = rdr["ChuanZhiId"].ToString();
                    item.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.BasePrice = rdr.GetDecimal(rdr.GetOrdinal("BasePrice"));
                    item.HangXianId = rdr.GetInt32(rdr.GetOrdinal("HangXianId"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 根据船只编号和基础价格列表新增、修改
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="l">基础价格列表</param>
        /// <returns></returns>
        public int BasePrice(string chuanzhiId,IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> l)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_ChuanZhiBasePrice");
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, chuanzhiId);
            _db.AddInParameter(cmd, "@XML", DbType.String, CreateBasePriceXml(l));
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
        /// 根据船只编号和附件类型新增、修改船只房型、美食、设施附件列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="t">附件类型【0：房型 1：美食 2：设施】</param>
        /// <param name="l">船只房型、美食、设施附件列表</param>
        /// <returns></returns>
        public int ChuanZhiFangXingFuJian_M(string chuanzhiId,int t, IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> l)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_YL_ChuanZhiFangXingFuJian");
            _db.AddInParameter(cmd, "@ChuanZhiId", DbType.AnsiStringFixedLength, chuanzhiId);
            _db.AddInParameter(cmd, "@T", DbType.Int32, t);
            _db.AddInParameter(cmd, "@XML", DbType.String, CreateChuanZhiFangXingFuJianXml(l));
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
