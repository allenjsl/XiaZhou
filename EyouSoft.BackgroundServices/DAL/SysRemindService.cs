using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.SmsStructure;
using EyouSoft.Model.SmsStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace EyouSoft.BackgroundServices.DAL
{
    //出团及回团短信提醒 只发确定成交的订单&&游客没退团&&勾中要发短信

    #region 出团提醒服务数据访问

    /// <summary>
    /// 出团提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysLeaveRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysLeaveRemindService
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 出回团提醒查询Sql
        /// </summary>
        private const string SqlSmsTourTimePlanSelect = @" 
        select CompanyId,TourId,LDate,RDate,Gather   
        ,(select TravellerId,CnName,Contact from tbl_TourOrderTraveller where OrderId in 
        (select OrderId from tbl_TourOrder where tbl_TourOrder.TourId = tbl_Tour.TourId and tbl_TourOrder.[Status] = {0} and tbl_TourOrder.IsDelete = '0') 
        and [Status] = {1} and LNotice = '{2}' for xml raw,root('root')) as TravellerInfo
        from tbl_Tour
        where IsDelete = '0' and IsCancel = '0' ";

        /// <summary>
        /// 根据SqlXml构造游客信息实体集合
        /// </summary>
        /// <param name="travellerXml">SqlXml</param>
        /// <returns></returns>
        private IList<MSmsTourTimeTraveller> GetTravellerListByXml(string travellerXml)
        {
            IList<MSmsTourTimeTraveller> list = null;
            if (string.IsNullOrEmpty(travellerXml))
                return list;

            XElement xRoot = XElement.Parse(travellerXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)
                return list;

            list = new List<MSmsTourTimeTraveller>();
            foreach (var t in xRows)
            {
                var model = new MSmsTourTimeTraveller
                {
                    TravellerId = Utils.GetXAttributeValue(t, "TravellerId"),
                    Traveller = Utils.GetXAttributeValue(t, "CnName"),
                    Code = Utils.GetXAttributeValue(t, "Contact")
                };

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 解析导游姓名、导游电话XML，[0]导游姓名，[1]导游电话
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        string[] ParseDaoYouXml(string xml)
        {
            string[] daoyou = new string[] { string.Empty, string.Empty };

            if (string.IsNullOrEmpty(xml)) return daoyou;

            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            if (xrows == null || xrows.Count() == 0) return daoyou;

            foreach (var xrow in xrows)
            {
                daoyou[0] = Utils.GetXAttributeValue(xrow, "SourceName");
                daoyou[1] = Utils.GetXAttributeValue(xrow, "ContactPhone");
                break;
            }

            return daoyou;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysLeaveRemindService()
        {
            _db = SystemStore;
        }

        #region 非ISysLeaveRemindService成员

        /// <summary>
        /// 更新短信当天已发送
        /// </summary>
        /// <param name="type">短信配置类型</param>
        /// <param name="companyId">公司编号集合</param>
        /// <returns></returns>
        public int SetTodaySend(SettingType type, params string[] companyId)
        {
            if (companyId == null || companyId.Length < 1)
                return 0;

            var strSql = new StringBuilder(" update [tbl_SmsSetting] set [IsSend] = @IsSend ");
            strSql.Append(" where [Type] = @Type ");
            if (companyId.Length == 1)
                strSql.AppendFormat(" and [CompanyId] = '{0}' ", companyId[0]);
            else
            {
                string strIds = companyId.Where(t => !string.IsNullOrEmpty(t)).Aggregate(string.Empty, (current, t) => current + "'" + t + "',");
                strSql.AppendFormat(" and [CompanyId] in ({0}) ", strIds);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)type);

            return DbHelper.ExecuteSql(dc, _db);
        }

        /// <summary>
        /// 根据类型获取短信提醒配置，指定点时间等于当前点时间时，更新当天已发送
        /// </summary>
        /// <param name="settingType">提醒配置类型</param>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSettingByType(SettingType settingType)
        {
            var strSql = new StringBuilder(" SELECT [CompanyId],[Type],[Message],[BeforeDay],[Hour],[IsEnable],[IsSend],[OperatorId],[IssueTime] FROM [tbl_SmsSetting] ");
            strSql.Append(" where [IsEnable] = @IsEnable ");
            strSql.Append(" and [IsSend] = @IsSend ");
            strSql.Append(" and [Type] = @Type ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "IsEnable", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)settingType);

            var list = new Queue<MSmsSetting>();
            MSmsSetting model;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                while (dr.Read())
                {
                    int hour = -1;
                    if (!dr.IsDBNull(dr.GetOrdinal("Hour")))
                    {
                        hour = dr.GetInt32(dr.GetOrdinal("Hour"));
                    }

                    if (hour != DateTime.Now.Hour) continue;

                    model = new MSmsSetting();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Type")))
                        model.Type = (SettingType)dr.GetByte(dr.GetOrdinal("Type"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Message")))
                        model.Message = dr.GetString(dr.GetOrdinal("Message"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BeforeDay")))
                        model.BeforeDay = dr.GetInt32(dr.GetOrdinal("BeforeDay"));
                    model.Hour = hour;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")))
                        model.IsEnabled = GetBoolean(dr.GetString(dr.GetOrdinal("IsEnable")));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSend")))
                        model.IsSend = GetBoolean(dr.GetString(dr.GetOrdinal("IsSend")));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    SetTodaySend(settingType, model.CompanyId);
                    list.Enqueue(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <param name="settingType">类型</param>
        /// <returns></returns>
        public IList<MSmsTourTimePlan> GetSmsTourTimePlanByType(string companyId, int beforeDay, SettingType settingType)
        {

            IList<MSmsTourTimePlan> list = new List<MSmsTourTimePlan>();
            if (string.IsNullOrEmpty(companyId))
                return list;

            if (beforeDay < 0) beforeDay = 0;
            DateTime cTime = DateTime.Now.AddDays(beforeDay);

            #region Sql语句构造

            var strSql = new StringBuilder();
            //团队信息
            strSql.Append(" select CompanyId,TourId,LDate,RDate,Gather,SellerId ");
            //游客信息
            strSql.Append(" ,(select TravellerId,CnName,Contact from tbl_TourOrderTraveller where OrderId in ");
            //订单编号条件
            strSql.AppendFormat(
                " (select OrderId from tbl_TourOrder where tbl_TourOrder.TourId = tbl_Tour.TourId and tbl_TourOrder.[Status] = {0} and tbl_TourOrder.IsDelete = '0') ",
                (int)Model.EnumType.TourStructure.OrderStatus.已成交);
            strSql.AppendFormat(" and [Status] = {0}  ", (int)Model.EnumType.TourStructure.TravellerStatus.在团);
            string strTmp = string.Empty;
            string strTmpTour = string.Empty;
            switch (settingType)
            {
                case SettingType.出团通知:
                    strTmp = " and [LNotice] = '1' ";
                    strTmpTour = string.Format(" and year(LDate) = {0} and month(LDate) = {1} and day(LDate) = {2} ",
                                               cTime.Year, cTime.Month, cTime.Day);
                    break;
                case SettingType.回团通知:
                    strTmp = " and [RNotice] = '1' ";
                    strTmpTour = string.Format(" and year(RDate) = {0} and month(RDate) = {1} and day(RDate) = {2} ",
                                               cTime.Year, cTime.Month, cTime.Day);
                    break;
            }
            strSql.Append(strTmp);
            strSql.Append(" for xml raw,root('root')) as TravellerInfo ");

            strSql.Append(" ,RouteName ");
            strSql.Append(" ,(SELECT SourceName,ContactPhone FROM tbl_Plan AS B WHERE B.TourId=tbl_Tour.TourId AND B.IsDelete='0' AND [Status]=4 FOR XML RAW,ROOT('root')) AS DaoYouXml ");

            strSql.Append(" from tbl_Tour where IsDelete = '0' ");
            strSql.Append(strTmpTour);
            strSql.AppendFormat(" and tbl_Tour.CompanyId = '{0}' ", companyId);
            strSql.Append(" and exists (");
            strSql.AppendFormat(" select 1 from tbl_TourOrder where tbl_TourOrder.TourId = tbl_Tour.TourId AND tbl_TourOrder.IsDelete='0' AND tbl_TourOrder.Status={0} ", (int)Model.EnumType.TourStructure.OrderStatus.已成交);
            strSql.Append(" and exists (select 1 from tbl_TourOrderTraveller where tbl_TourOrderTraveller.OrderId = tbl_TourOrder.OrderId and Contact is not null and Contact <> '' ");
            strSql.Append(strTmp);
            strSql.Append(" ) ");
            strSql.Append(" ) ");

            #endregion

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                MSmsTourTimePlan model;
                while (dr.Read())
                {
                    model = new MSmsTourTimePlan();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TourId")))
                        model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                        model.LeaveTime = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RDate")))
                        model.BackTime = dr.GetDateTime(dr.GetOrdinal("RDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Gather")))
                        model.Gather = dr.GetString(dr.GetOrdinal("Gather"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SellerId")))
                        model.SellerId = dr.GetString(dr.GetOrdinal("SellerId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TravellerInfo")))
                        model.Traveller = GetTravellerListByXml(dr.GetString(dr.GetOrdinal("TravellerInfo")));

                    string[] daoyou = ParseDaoYouXml(dr["DaoYouXml"].ToString());

                    model.DaoYouName = daoyou[0];
                    model.DaoYouTelephone = daoyou[1];
                    model.RouteName = dr["RouteName"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        #endregion

        #region ISysLeaveRemindService成员

        /// <summary>
        /// 获取出团提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return GetSmsSettingByType(SettingType.出团通知);
        }

        ///// <summary>
        ///// 更新短信当天已发送
        ///// </summary>
        ///// <param name="companyId">公司编号集合</param>
        ///// <returns></returns>
        //public int SetTodaySend(params string[] companyId)
        //{
        //    if (companyId == null || companyId.Length < 1)
        //        return 0;

        //    return SetTodaySend(SettingType.出团通知, companyId);
        //}

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<Model.SmsStructure.MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay)
        {
            return GetSmsTourTimePlanByType(companyId, beforeDay, SettingType.出团通知);
        }

        #endregion
    }

    #endregion

    #region 回团提醒服务数据访问

    /// <summary>
    /// 回团提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysBackRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysBackRemindService
    {
        #region private member

        /// <summary>
        /// 出团提醒服务数据访问类的实例对象（回团提醒的数据访问通过此对象）
        /// </summary>
        private readonly SysLeaveRemindService _dalLeaveRemind = new SysLeaveRemindService();

        #endregion

        ///// <summary>
        ///// 更新短信当天已发送
        ///// </summary>
        ///// <param name="companyId">公司编号集合</param>
        ///// <returns></returns>
        //public int SetTodaySend(params string[] companyId)
        //{
        //    if (companyId == null || companyId.Length < 1)
        //        return 0;

        //    return _dalLeaveRemind.SetTodaySend(SettingType.回团通知, companyId);
        //}

        /// <summary>
        /// 获取回团提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return _dalLeaveRemind.GetSmsSettingByType(SettingType.回团通知);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay)
        {
            return _dalLeaveRemind.GetSmsTourTimePlanByType(companyId, beforeDay, SettingType.回团通知);
        }
    }

    #endregion

    #region 生日提醒服务数据访问

    /// <summary>
    /// 生日提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysBirthdayRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysBirthdayRemindService
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 出团提醒服务数据访问类的实例对象
        /// </summary>
        private readonly SysLeaveRemindService _dalLeaveRemind = new SysLeaveRemindService();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysBirthdayRemindService()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 获取生日提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return _dalLeaveRemind.GetSmsSettingByType(SettingType.生日提醒);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<MSmsBirthdayRemindPlan> GetSmsBirthdayRemindPlan(string companyId, int beforeDay)
        {
            IList<MSmsBirthdayRemindPlan> list = new List<MSmsBirthdayRemindPlan>();
            if (string.IsNullOrEmpty(companyId))
                return list;

            if (beforeDay < 0) beforeDay = 0;
            DateTime cTime = DateTime.Now.AddDays(beforeDay);

            var strSql = new StringBuilder();
            //客户联系人（含组团社、供应商）
            strSql.Append(" select CompanyId,[Name],MobilePhone,Birthday from tbl_CrmLinkman ");
            strSql.Append(" where MobilePhone is not null and MobilePhone <> '' and Birthday is not null AND Birthday>'1900-01-01' ");
            strSql.AppendFormat(" and month(Birthday) = {0} and day(Birthday) = {1} ", cTime.Month, cTime.Day);

            strSql.Append(" union all  ");

            //内部员工
            strSql.Append(" select CompanyId,[Name],Mobile as MobilePhone,BirthDate AS Birthday from tbl_GovFile ");
            strSql.Append(" where Mobile is not null and Mobile <> '' and BirthDate is not null AND BirthDate>'1900-01-01' ");
            strSql.AppendFormat(" and month(BirthDate) = {0} and day(BirthDate) = {1} ", cTime.Month, cTime.Day);
            strSql.AppendFormat(" and StaffStatus <> {0} ", (int)Model.EnumType.GovStructure.StaffStatus.离职);

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                MSmsBirthdayRemindPlan model;
                while (dr.Read())
                {
                    model = new MSmsBirthdayRemindPlan();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        model.Name = dr.GetString(dr.GetOrdinal("Name"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MobilePhone")))
                        model.MobilePhone = dr.GetString(dr.GetOrdinal("MobilePhone"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Birthday"))) model.Birthday = dr.GetDateTime(dr.GetOrdinal("Birthday"));

                    list.Add(model);
                }
            }

            return list;
        }

        ///// <summary>
        ///// 更新短信当天已发送
        ///// </summary>
        ///// <param name="companyId">公司编号集合</param>
        ///// <returns></returns>
        //public int SetTodaySend(params string[] companyId)
        //{
        //    if (companyId == null || companyId.Length < 1)
        //        return 0;

        //    return _dalLeaveRemind.SetTodaySend(SettingType.生日提醒, companyId);
        //}
    }

    #endregion
}
