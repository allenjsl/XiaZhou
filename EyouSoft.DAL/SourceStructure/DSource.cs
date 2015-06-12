using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit.DAL;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Toolkit;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.DAL.SourceStructure
{
    /// <summary>
    /// 创建者 钱琦 时间:2011-10-1
    /// 资源管理DAL
    /// 修改导游：邵权江 2012-03-09
    /// 增加获得导游排班 导游上团统计 邵权江 2011-10-08
    /// </summary>
    public class DSource : EyouSoft.Toolkit.DAL.DALBase, IDAL.SourceStructure.ISource, IDAL.SourceStructure.ISourceHotel, IDAL.SourceStructure.ISourceSpot, IDAL.SourceStructure.ISourceDining, IDAL.SourceStructure.ISourceShip, IDAL.SourceStructure.ISourceShop, IDAL.SourceStructure.ISourceMotorcade, IDAL.SourceStructure.ISourceTicket, IDAL.SourceStructure.ISourceGuide, IDAL.SourceStructure.IRoute, IDAL.SourceStructure.ISourceOther
    {
        #region 私有变量
        private Database _db = null;
        #region 字符串变量
        /// <summary>
        /// 添加地接社的存储过程
        /// </summary>
        private string proc_AddTravel = "proc_AddTravel";
        /// <summary>
        /// 修改地接社的存储过程
        /// </summary>
        private string proc_Source_UpdateTravel = "proc_Source_UpdateTravel";
        /// <summary>
        /// 修改供应商的存储过程
        /// </summary>
        private string proc_Source_UpdateSource = "proc_Source_UpdateSource";
        /// <summary>
        /// 添加酒店的存储过程
        /// </summary>
        private string proc_AddHotel = "proc_AddHotel";
        /// <summary>
        /// 修改酒店的存储过程
        /// </summary>
        private string proc_Source_UpdateHotel = "proc_Source_UpdateHotel";
        /// <summary>
        /// 添加景点的存储过程
        /// </summary>
        private string proc_AddSpot = "proc_AddSpot";
        /// <summary>
        /// 修改景点的存储过程
        /// </summary>，
        private string proc_Source_UpdateSpot = "proc_Source_UpdateSpot";
        /// <summary>
        /// 添加餐馆的存储过程
        /// </summary>
        private string proc_AddDining = "proc_AddDining";
        /// <summary>
        /// 修改餐馆的存储过程
        /// </summary>
        private string proc_Source_UpdateDining = "proc_Source_UpdateDining";
        /// <summary>
        /// 添加游轮的存储过程
        /// </summary>
        private string proc_AddShip = "proc_AddShip";
        /// <summary>
        /// 修改游轮的存储过程
        /// </summary>
        private string proc_Source_UpdateShip = "proc_Source_UpdateShip";
        /// <summary>
        /// 添加购物点的存储过程
        /// </summary>
        private string proc_AddShop = "proc_AddShop";
        /// <summary>
        /// 修改购物点的存储过程
        /// </summary>
        private string proc_Source_UpdateShop = "proc_Source_UpdateShop";
        /// <summary>
        /// 添加车队的存储过程
        /// </summary>
        private string proc_AddMotorcade = "proc_AddMotorcade";
        /// <summary>
        /// 修改车队的存储过程
        /// </summary>
        private string proc_Source_UpdateMotorcade = "proc_Source_UpdateMotorcade";
        /// <summary>
        /// 添加导游的存储过程
        /// </summary>
        private string proc_AddSourceGuide = "proc_AddSourceGuide";
        /// <summary>
        /// 修改导游的存储过程
        /// </summary>
        private string proc_Source_UpdateGuide = "proc_Source_UpdateGuide";
        /// <summary>
        /// 添加线路的存储过程
        /// </summary>
        private string proc_AddRoute = "proc_AddRoute";
        /// <summary>
        /// 修改线路的存储过程
        /// </summary>
        private string proc_Source_UpdateRoute = "proc_Source_UpdateRoute";
        /// <summary>
        /// 查询供应商的sql语句
        /// </summary>
        private string sql_Source_SelectSource = "select * from tbl_Source where SourceId=@SourceId";
        /// <summary>
        /// 查询供应商酒店表sql语句
        /// </summary>
        private string sql_Source_Hotel = "select * from tbl_SourceHotel where SourceId=@SourceId";
        /// <summary>
        /// 查询供应商酒店房型表sql语句
        /// </summary>
        private string sql_Source_HotelRoom = "select * from tbl_SourceHotelRoom where SourceId=@SourceId order by SortId asc";
        /// <summary>
        /// 查询供应商餐馆表的sql语句
        /// </summary>
        private string sql_Source_Dining = "select * from tbl_SourceDining where SourceId=@SourceId";
        /// <summary>
        /// 查询供应商餐馆菜系表的sql语句
        /// </summary>
        private string sql_Source_DiningCuisine = "select * from tbl_SourceDiningCuisine where SourceId=@SourceId order by SortId asc";
        /// <summary>
        /// 查询供应商游轮表的sql语句
        /// </summary>
        private string sql_Source_Ship = "select * from tbl_SourceShip where SourceId=@SourceId";
        /// <summary>
        /// 查询供应商购物点表的sql语句
        /// </summary>
        private string sql_Source_Shop = "select * from tbl_SourceShop where SourceId=@SourceId";
        /// <summary>
        /// 查询供应商车辆表的sql语句
        /// </summary>
        private string sql_Source_Car = "select * from tbl_SourceCar where SourceId=@SourceId order by SortId asc";
        /// <summary>
        /// 查询导游的sql语句
        /// </summary>
        private string sql_Source_Guide = "select *,(SELECT TOP 1 DepartId FROM tbl_GovFile WHERE GuideId=@GuideId) AS DepartId,(SELECT TOP 1 DepartName FROM tbl_ComDepartment WHERE DepartId IN (SELECT DepartId FROM tbl_GovFile WHERE GuideId=@GuideId)) AS DepartName,(SELECT GuideId,Category FROM tbl_SourceGuideCategory WHERE GuideId=tbl_SourceGuide.GuideId FOR XML RAW,ROOT('ROOT'))AS GuideCategoryXML from tbl_SourceGuide where GuideId=@GuideId";
        /// <summary>
        /// 查询线路库表的sql语句
        /// </summary>
        private string sql_Source_SelectRoute = "select * from tbl_Route where RouteId=@RouteId";
        /// <summary>
        /// 查询线路服务表的sql语句
        /// </summary>
        private string sql_Source_SelectRouteServices = "select * from tbl_RouteServices where RouteId=@RouteId";
        /// <summary>
        /// 查询线路包含项目表的sql语句
        /// </summary>
        private string sql_Source_SelectRouteStandard = "select * from tbl_RouteStandard where RouteId=@RouteId order by SortId asc";
        /// <summary>
        /// 查询线路行程计划表的sql语句
        /// </summary>
        private string sql_Source_SelectRoutePlan = "select * from tbl_RoutePlan where RouteId=@RouteId order by Days asc";
        /// <summary>
        /// 查询线路行程景点表的sql语句
        /// </summary>
        private string sql_Source_SelectRoutePlanSpot = "select * from tbl_RoutePlanSpot where PlanId=@PlanId order by SortId asc";
        /// <summary>
        /// 判断是否存在线路名称
        /// </summary>
        private string sql_Source_IsExistsRouteName = "select RouteId from tbl_Route where CompanyId=@CompanyId and RouteName=@RouteName and AreaId=@AreaId";
        #endregion
        #endregion

        #region dal变量
        EyouSoft.DAL.CrmStructure.DCrmLinkman linmanDal = new EyouSoft.DAL.CrmStructure.DCrmLinkman();
        EyouSoft.DAL.ComStructure.DComUser userDal = new EyouSoft.DAL.ComStructure.DComUser();
        EyouSoft.DAL.ComStructure.DComAttach attachDal = new EyouSoft.DAL.ComStructure.DComAttach();
        #endregion

        #region 构造函数
        public DSource()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法
        #region 地接社

        /// <summary>
        /// 添加地接社
        /// </summary>
        /// <param name="model">地接社Model</param>
        /// <returns>返回值 -1:存在相同地接社名称 -2:存在相同的用户名 0:事务回滚 1:正确</returns>
        public int AddTravelModel(Model.SourceStructure.MSourceTravel model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddTravel);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);

            if (model.SourceModel.ContractAttach != null)
            {
                IList<Model.ComStructure.MComAttach> attachList = new List<Model.ComStructure.MComAttach>();
                attachList.Add(model.SourceModel.ContractAttach);
                _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(attachList));
            }
            else
                _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, null);
            Model.ComStructure.MComAttach xyAttach = new Model.ComStructure.MComAttach();
            xyAttach.FilePath = model.SourceModel.AgreementFile;
            xyAttach.Name = System.IO.Path.GetFileName(xyAttach.FilePath);
            xyAttach.ItemId = model.SourceModel.SourceId;
            xyAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合作协议;

            _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, null);
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlTravelString", DbType.Xml, GetSourceTravelXmlString(model.SourceTravelModel));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改地接社Model
        /// </summary>
        /// <param name="model">供应商保险Model</param>
        /// <returns>返回值 -1:地接社名称已经存在 -2:用户名已经存在 0:事务回滚 1:正确</returns>
        public int UpdateTravelModel(Model.SourceStructure.MSourceTravel model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateTravel);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "Telephone", DbType.String, model.SourceTravelModel.Telephone);
            _db.AddInParameter(dc, "LegalRepresentative", DbType.String, model.SourceTravelModel.LegalRepresentative);
            _db.AddInParameter(dc, "Routes", DbType.String, model.SourceTravelModel.Routes);

            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "AttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "XAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合作协议);
            if (model.SourceModel.ContractAttach != null)
            {
                IList<Model.ComStructure.MComAttach> attachList = new List<Model.ComStructure.MComAttach>();
                attachList.Add(model.SourceModel.ContractAttach);
                _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(attachList));
            }
            else
                _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, null);
            Model.ComStructure.MComAttach xyAttach = new Model.ComStructure.MComAttach();
            xyAttach.FilePath = model.SourceModel.AgreementFile;
            xyAttach.Name = System.IO.Path.GetFileName(xyAttach.FilePath);
            xyAttach.ItemId = model.SourceModel.SourceId;
            xyAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合作协议;
            _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, null);
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 给地接社分配账户
        /// </summary>
        /// <param name="sourceId">地接社编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="account">账户</param>
        /// <param name="md5pwd">md5密码</param>
        /// <param name="pwd">明文密码</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="operatorNm">操作者姓名</param>
        /// <param name="operDeptId">操作者部门编号</param>
        /// <param name="routeAreaList">线路区域列表</param>
        /// <returns></returns>
        public int AddAccountToTravel(string sourceId, string companyId, string account, string md5pwd, string pwd, string operatorId, string operatorNm, int operDeptId, IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_Source_AddAccountToTravel");
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "sourceId", DbType.AnsiStringFixedLength, sourceId);
            _db.AddInParameter(dc, "companyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(dc, "account", DbType.AnsiString, account);
            _db.AddInParameter(dc, "md5pwd", DbType.AnsiString, md5pwd);
            _db.AddInParameter(dc, "pwd", DbType.AnsiString, pwd);
            _db.AddInParameter(dc, "operatorId", DbType.AnsiStringFixedLength, operatorId);
            _db.AddInParameter(dc, "operatorNm", DbType.String, operatorNm);
            _db.AddInParameter(dc, "operDeptId", DbType.Int32, operDeptId);
            _db.AddInParameter(dc, "userType", DbType.Byte, (int)Model.EnumType.ComStructure.UserType.供应商);
            _db.AddInParameter(dc, "userId", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            _db.AddInParameter(dc, "xmlAreaString", DbType.Xml, GetTravelRouteAreaXmlString(routeAreaList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }



        /// <summary>
        /// 修改地接社帐号
        /// </summary>
        /// <param name="sourceId">地接社编号</param>
        /// <param name="Status">帐号状态</param>
        /// <param name="md5pwd">md5密码</param>
        /// <param name="pwd">明文密码</param>
        /// <param name="routeAreaList">线路区域列表</param>
        /// <param name="isUpdAccount">是否修改帐号信息 0：单纯修改状态 1：修改密码和线路区域</param>
        /// <returns>0:失败 1:成功</returns>
        public int UpdateTravelAccountStatus(string sourceId, Model.EnumType.ComStructure.UserStatus Status, string md5pwd, string pwd, IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList, bool isUpdAccount)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_UpdateTravelAccountStatus");
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "sourceId", DbType.AnsiStringFixedLength, sourceId);
            _db.AddInParameter(dc, "Status", DbType.Byte, (int)Status);
            _db.AddInParameter(dc, "md5pwd", DbType.AnsiString, md5pwd);
            _db.AddInParameter(dc, "pwd", DbType.AnsiString, pwd);
            _db.AddInParameter(dc, "xmlAreaString", DbType.Xml, GetTravelRouteAreaXmlString(routeAreaList));
            _db.AddInParameter(dc, "IsUpdAccount", DbType.AnsiStringFixedLength, isUpdAccount ? "1" : "0");

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得地接社Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns>地接社Model</returns>
        public Model.SourceStructure.MSourceTravel GetTravelModel(string SourceId)
        {
            Model.SourceStructure.MSourceTravel traverModel = new EyouSoft.Model.SourceStructure.MSourceTravel();
            traverModel.SourceModel = GetSourceModel(SourceId);
            traverModel.LinkManList = linmanDal.GetLinkManModelList(traverModel.SourceModel.CompanyId, traverModel.SourceModel.SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);

            traverModel.SourceTravelModel = GetSourceTravelInfoModel(SourceId);

            return traverModel;
        }
        #endregion

        #region 酒店
        /// <summary>
        /// 添加供应商酒店Model
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns>返回值 -1:已经存在相同的酒店名称 -2:该酒店已经存在相同的酒店房型  0:事务回滚 1:正确</returns>
        public int AddHotelModel(Model.SourceStructure.MSourceHotel model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddHotel);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.AttachList));
            _db.AddInParameter(dc, "xmlCAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlHotelString", DbType.Xml, GetHotelXmlString(model));
            _db.AddInParameter(dc, "xmlRoomTypeString", DbType.Xml, GetRoomXmlString(model.HotelRoomList));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改供应商酒店Model(包含联系人,价格)
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns>返回值 -1:酒店名称已经存在 -2:已经存在相同的酒店房型名称  0:事务回滚 1:正确</returns>
        public int UpdateHotelModel(Model.SourceStructure.MSourceHotel model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateHotel);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "Star", DbType.Byte, (int)model.Star);
            _db.AddInParameter(dc, "ReceptionTel", DbType.String, model.ReceptionTel);
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "AttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.AttachList));
            _db.AddInParameter(dc, "xmlCAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlRoomType", DbType.Xml, GetRoomXmlString(model.HotelRoomList));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));

            var linq =
                        from entity in model.HotelRoomList
                        group entity by new { TypeName = entity.TypeName } into entityGroup
                        where entityGroup.Count() > 1
                        select entityGroup;
            if (linq.ToList().Count > 0)
            {
                return -2;
            }

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得酒店详细页面供应商酒店Model(包含联系人列表,房型信息列表)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns>供应商酒店Model</returns>
        public Model.SourceStructure.MSourceHotel GetOneHotelModel(string SourceId)
        {
            Model.SourceStructure.MSourceHotel hotelModel = new EyouSoft.Model.SourceStructure.MSourceHotel();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Hotel);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    hotelModel.ReceptionTel = !reader.IsDBNull(reader.GetOrdinal("ReceptionTel")) ? (string)reader["ReceptionTel"] : string.Empty;
                    hotelModel.SourceId = SourceId;
                    hotelModel.Star = (EyouSoft.Model.EnumType.SourceStructure.HotelStar)(int.Parse(reader["Star"].ToString()));
                }
            }
            hotelModel.SourceModel = GetSourceModel(SourceId);
            hotelModel.LinkManList = linmanDal.GetLinkManModelList(hotelModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            hotelModel.HotelRoomList = GetRoomModelList(SourceId);
            hotelModel.AttachList = attachDal.GetModelList(SourceId, EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商);
            return hotelModel;
        }

        #endregion

        #region 景点
        /// <summary>
        /// 添加景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns>返回值 -1:已经存在相同的景点名称 -2:已经存在相同的附件名称 0:事务回滚 1:正确</returns>
        public int AddSpotModel(Model.SourceStructure.MSourceSpot model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddSpot);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.AttachList));
            _db.AddInParameter(dc, "xmlCAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));

            _db.AddInParameter(dc, "xmlPriceSystemString", DbType.Xml, GetSpotPriceSystemXmlString(model.PriceSystemList));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns>返回值 -1:景点名称已经存在 -2:已经存在相同的附件名称 0:事务回滚 1:正确</returns>
        public int UpdateSpotModel(Model.SourceStructure.MSourceSpot model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateSpot);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);
            _db.AddInParameter(dc, "IsBackSingle", DbType.AnsiStringFixedLength, model.SourceModel.IsBackSingle ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.String, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "AttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商);
            _db.AddInParameter(dc, "xmlPriceSystemString", DbType.Xml, GetSpotPriceSystemXmlString(model.PriceSystemList));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.AttachList));
            _db.AddInParameter(dc, "xmlCAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得景点信息(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceSpot GetSpotModel(string SourceId)
        {
            Model.SourceStructure.MSourceSpot spotModel = new EyouSoft.Model.SourceStructure.MSourceSpot();

            spotModel.SourceModel = GetSourceModel(SourceId);
            spotModel.LinkManList = linmanDal.GetLinkManModelList(spotModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            spotModel.AttachList = attachDal.GetModelList(SourceId, EyouSoft.Model.EnumType.ComStructure.AttachItemType.供应商);
            spotModel.PriceSystemList = GetSpotPriceSystemList(SourceId);
            return spotModel;
        }

        /// <summary>
        /// 获得景点价格体系
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSpotPriceSystemModel> GetSpotPriceSystemModelList(string companyId, string sourceId)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(sourceId))
            {
                sql = string.Format("select * from tbl_SourceSpotPriceSystem where SourceId='{0}' ", sourceId);
            }
            else
            {
                sql = string.Format("select a.* from tbl_SourceSpotPriceSystem a join tbl_Source b on a.SourceId=b.SourceId where Type={0} and CompanyId='{1}' ", (int)Model.EnumType.SourceStructure.SourceType.景点, companyId);
            }
            DbCommand dc = _db.GetSqlStringCommand(sql);
            IList<Model.SourceStructure.MSpotPriceSystemModel> list = new List<Model.SourceStructure.MSpotPriceSystemModel>();
            Model.SourceStructure.MSpotPriceSystemModel model = new Model.SourceStructure.MSpotPriceSystemModel();
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    model.GuideWord = !reader.IsDBNull(reader.GetOrdinal("GuideWord")) ? (string)reader["GuideWord"] : string.Empty;
                    model.SpotName = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    model.PriceGP = !reader.IsDBNull(reader.GetOrdinal("PriceGP")) ? (decimal)reader["PriceGP"] : 0m;
                    model.PriceJR = !reader.IsDBNull(reader.GetOrdinal("PriceJR")) ? (decimal)reader["PriceJR"] : 0m;
                    model.PriceLR1 = !reader.IsDBNull(reader.GetOrdinal("PriceLR1")) ? (decimal)reader["PriceLR1"] : 0m;
                    model.PriceLR2 = !reader.IsDBNull(reader.GetOrdinal("PriceLR2")) ? (decimal)reader["PriceLR2"] : 0m;
                    model.PriceRT = !reader.IsDBNull(reader.GetOrdinal("PriceRT")) ? (decimal)reader["PriceRT"] : 0m;
                    model.PriceSK = !reader.IsDBNull(reader.GetOrdinal("PriceSK")) ? (decimal)reader["PriceSK"] : 0m;
                    model.PriceTD = !reader.IsDBNull(reader.GetOrdinal("PriceTD")) ? (decimal)reader["PriceTD"] : 0m;
                    model.PriceXS = !reader.IsDBNull(reader.GetOrdinal("PriceXS")) ? (decimal)reader["PriceXS"] : 0m;
                    model.Id = (string)reader["Id"];
                    model.SourceId = sourceId;
                    model.Star = (Model.EnumType.SourceStructure.SpotStar)((int)reader["Star"]);
                    list.Add(model);
                }
            }
            return list;

        }
        #endregion

        #region 餐馆
        /// <summary>
        /// 添加单个供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns>返回值 -1:已经存在该名称的餐馆 -2:该餐馆已经存在相同的菜系 0:事务回滚 1:正确</returns>
        public int AddDiningModel(Model.SourceStructure.MSourceDining model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddDining);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlDiningString", DbType.Xml, GetDiningXmlString(model));
            _db.AddInParameter(dc, "xmlCuisinesString", DbType.Xml, GetCuisineXmlString(model.DiningCuisineList));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns>返回值 -1:已经存在相同的餐馆名称 -2:该菜馆已经存在相同的菜系 0:事务回滚 -1:正确</returns>
        public int UpdateDiningModel(Model.SourceStructure.MSourceDining model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateDining);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "DiningStandard", DbType.AnsiString, model.DiningStandard);
            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlCuisine", DbType.Xml, GetCuisineXmlString(model.DiningCuisineList));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得详细页面上的供应商餐馆Model(包含餐馆菜系,联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceDining GetDiningModel(string SourceId)
        {
            Model.SourceStructure.MSourceDining diningModel = new EyouSoft.Model.SourceStructure.MSourceDining();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Dining);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    diningModel.DiningStandard = !reader.IsDBNull(reader.GetOrdinal("DiningStandard")) ? reader["DiningStandard"].ToString() : string.Empty;
                    diningModel.SourceId = SourceId;
                }
            }
            diningModel.SourceModel = GetSourceModel(SourceId);
            diningModel.LinkManList = linmanDal.GetLinkManModelList(diningModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            diningModel.DiningCuisineList = GetDiningCuisineModelList(SourceId);
            return diningModel;
        }
        #endregion

        #region 游轮
        /// <summary>
        /// 添加单个游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns>返回值 -1:已经存在相同的游轮名称 0:事务回滚 1:正确</returns>
        public int AddShipModel(Model.SourceStructure.MSourceShip model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddShip);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlShipString", DbType.Xml, GetShipXmlString(model));
            _db.AddInParameter(dc, "xmlSubShipString", DbType.Xml, GetShipSubShipXmlString(model.SubShipList));
            _db.AddInParameter(dc, "xmlPAttachString", DbType.Xml, GetSubShipAttachXmlString(model.SubShipList));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns>返回值 -1:已经存在相同的游轮名称 0:事务回滚 1:正确</returns>
        public int UpdateShipModel(Model.SourceStructure.MSourceShip model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateShip);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "Telephone", DbType.String, model.Telephone);
            _db.AddInParameter(dc, "StartTime", DbType.DateTime, model.StartTime);
            _db.AddInParameter(dc, "EndTime", DbType.DateTime, model.EndTime);
            _db.AddInParameter(dc, "Routes", DbType.String, model.Routes);
            _db.AddInParameter(dc, "OwerRoutes", DbType.String, model.OwerRoutes);
            _db.AddInParameter(dc, "Fax", DbType.String, model.Fax);
            _db.AddInParameter(dc, "PriceSystem", DbType.String, model.PriceSystem);
            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "PAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.游轮旗下游轮图片);
            _db.AddInParameter(dc, "xmlSubShipString", DbType.Xml, GetShipSubShipXmlString(model.SubShipList));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlPAttachString", DbType.Xml, GetSubShipAttachXmlString(model.SubShipList));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得详细页面显示游轮Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceShip GetShipModel(string SourceId)
        {
            Model.SourceStructure.MSourceShip shipModel = new EyouSoft.Model.SourceStructure.MSourceShip();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Ship);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    shipModel.OwerRoutes = !reader.IsDBNull(reader.GetOrdinal("OwerRoutes")) ? reader["OwerRoutes"].ToString() : string.Empty;
                    shipModel.Routes = !reader.IsDBNull(reader.GetOrdinal("Routes")) ? reader["Routes"].ToString() : string.Empty;
                    shipModel.Fax = !reader.IsDBNull(reader.GetOrdinal("Fax")) ? reader["Fax"].ToString() : string.Empty;
                    shipModel.PriceSystem = !reader.IsDBNull(reader.GetOrdinal("PriceSystem")) ? reader["PriceSystem"].ToString() : string.Empty;
                    shipModel.SourceId = SourceId;
                    shipModel.StartTime = (DateTime?)reader["StartTime"];
                    shipModel.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? reader["Telephone"].ToString() : string.Empty;
                    shipModel.EndTime = (DateTime?)reader["EndTime"];
                }
            }
            shipModel.SourceModel = GetSourceModel(SourceId);
            shipModel.LinkManList = linmanDal.GetLinkManModelList(shipModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            shipModel.SubShipList = GetShipSubShip(SourceId);
            return shipModel;
        }
        #endregion

        #region 购物
        /// <summary>
        /// 添加购物Model
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <returns>返回值 -1:已经存在相同的购物点名称 0:事务回滚 1:正确</returns>
        public int AddShopModel(Model.SourceStructure.MSourceShop model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddShop);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlShopString", DbType.Xml, GetShopXmlString(model));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改购物Model
        /// </summary>
        /// <param name="model">供应商购物Model</param>
        /// <returns>返回值 -1:已经存在相同的购物点名称 0:事务回滚 1:正确</returns>
        public int UpdateShopModel(Model.SourceStructure.MSourceShop model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateShop);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "SellType", DbType.String, model.SellType);
            _db.AddInParameter(dc, "IsBackSingle", DbType.AnsiStringFixedLength, model.SourceModel.IsBackSingle ? "1" : "0");
            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得购物Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceShop GetShopModel(string SourceId)
        {
            Model.SourceStructure.MSourceShop shopModel = new EyouSoft.Model.SourceStructure.MSourceShop();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Shop);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    shopModel.SourceId = SourceId;
                    shopModel.SellType = !reader.IsDBNull(reader.GetOrdinal("SellType")) ? reader["SellType"].ToString() : string.Empty;
                }
            }
            shopModel.SourceModel = GetSourceModel(SourceId);
            shopModel.LinkManList = linmanDal.GetLinkManModelList(shopModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            return shopModel;
        }
        #endregion

        #region 车队
        /// <summary>
        /// 添加单个车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns>返回值 -1:已经存在相同的车队名称 0:事务回滚 1:正确</returns>
        public int AddMotorcadeModel(Model.SourceStructure.MSourceMotorcade model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddMotorcade);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlCarString", DbType.Xml, GetCarXmlString(model.CarList));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns>返回值 -1:已经存在相同的车队名称 0:事务回滚 1:正确</returns>
        public int UpdateMotorcadeModel(Model.SourceStructure.MSourceMotorcade model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateMotorcade);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlCar", DbType.Xml, GetCarXmlString(model.CarList));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得车队Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceMotorcade GetMotorcadeModel(string SourceId)
        {
            Model.SourceStructure.MSourceMotorcade motorcadeModel = new EyouSoft.Model.SourceStructure.MSourceMotorcade();
            motorcadeModel.SourceModel = GetSourceModel(SourceId);
            motorcadeModel.CarList = GetCarModelList(SourceId);
            motorcadeModel.LinkManList = linmanDal.GetLinkManModelList(motorcadeModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            return motorcadeModel;
        }
        #endregion

        #region 票务
        /// <summary>
        /// 添加票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns>返回值 -1:已经存在相同的票务名称 0:事务回滚 1:正确</returns>
        public int AddTicketModel(Model.SourceStructure.MSourceTicket model)
        {
            return AddSourceModel(model.SourceModel, model.LinkManList);
        }

        /// <summary>
        /// 修改票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns>返回值 -1:已经存在相同的票务名称 0:事务回滚 1:正确</returns>
        public int UpdateTicketModel(Model.SourceStructure.MSourceTicket model)
        {
            return UpdateSourceModel(model.SourceModel, model.LinkManList);
        }

        /// <summary>
        /// 获得票务Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceTicket GetTicketModel(string SourceId)
        {
            Model.SourceStructure.MSourceTicket ticketModel = new EyouSoft.Model.SourceStructure.MSourceTicket();
            ticketModel.SourceModel = GetSourceModel(SourceId);
            ticketModel.LinkManList = linmanDal.GetLinkManModelList(ticketModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            return ticketModel;
        }
        #endregion

        #region 其他
        /// <summary>
        /// 添加其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns>返回值 -1:已经存在相同的其他名称 0:事务回滚 1:正确</returns>
        public int AddOtherModel(Model.SourceStructure.MSourceOther model)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_AddOther");
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model.SourceModel));
            _db.AddInParameter(dc, "xmlOtherString", DbType.Xml, GetOtherXmlString(model.OtherTypeList));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns>返回值 -1:已经存在相同的其他名称 0:事务回滚 1:正确</returns>
        public int UpdateOtherModel(Model.SourceStructure.MSourceOther model)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_Source_UpdateOther");
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceModel.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.SourceModel.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.SourceModel.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.SourceModel.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.SourceModel.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.SourceModel.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.SourceModel.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.SourceModel.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.SourceModel.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.SourceModel.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.SourceModel.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.SourceModel.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.SourceModel.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.SourceModel.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.SourceModel.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.SourceModel.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.SourceModel.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.SourceModel.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.SourceModel.IssueTime);

            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.SourceModel.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.SourceModel.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.SourceModel.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.SourceModel.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.SourceModel.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.SourceModel.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.SourceModel.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.SourceModel.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.SourceModel.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.SourceModel.ContractAttach));
            _db.AddInParameter(dc, "xmlOther", DbType.Xml, GetOtherXmlString(model.OtherTypeList));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(model.LinkManList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得其他Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceOther GetOtherModel(string SourceId)
        {
            Model.SourceStructure.MSourceOther otherModel = new EyouSoft.Model.SourceStructure.MSourceOther();
            otherModel.SourceModel = GetSourceModel(SourceId);
            otherModel.LinkManList = linmanDal.GetLinkManModelList(otherModel.SourceModel.CompanyId, SourceId, (int)Model.EnumType.ComStructure.LxrType.供应商);
            otherModel.OtherTypeList = GetSourceOtherList(SourceId);
            return otherModel;
        }
        #endregion

        #region 导游
        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="GuideId">供应商编号GuideId,新增GuideId=""</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns></returns>
        public bool ExistsIDNumber(string IDNumber, string GuideId, string CompanyID)
        {
            string StrSql = " SELECT count(GuideId) FROM tbl_SourceGuide WHERE CompanyID=@CompanyID AND IDNumber=@IDNumber ";
            if (!string.IsNullOrEmpty(GuideId))
            {
                StrSql += " AND GuideId<>@GuideId ";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (!string.IsNullOrEmpty(GuideId))
            {
                this._db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, GuideId);
            }
            this._db.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            this._db.AddInParameter(dc, "IDNumber", DbType.String, IDNumber);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 添加导游信息
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns>返回值 -1:存在相同的用户名 -2:存在相同的身份证号 -3:存在相同的导游证号 小于0:错误 1:正确</returns>
        public int AddGuideModel(Model.SourceStructure.MSourceGuide model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddSourceGuide);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, model.GuideId);
            _db.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "Gender", DbType.Byte, (int)model.Gender);
            _db.AddInParameter(dc, "IDNumber", DbType.AnsiString, model.IDNumber);
            _db.AddInParameter(dc, "GuideNumber", DbType.AnsiString, model.GuideNumber);
            _db.AddInParameter(dc, "LeaderNumber", DbType.AnsiString, model.LeaderNumber);
            _db.AddInParameter(dc, "AnchoredCom", DbType.Byte, (int)model.AnchoredCom);
            _db.AddInParameter(dc, "AnnualReview", DbType.AnsiStringFixedLength, model.AnnualReview == true ? "1" : "0");//年审状态(0未审，,1:已审)
            _db.AddInParameter(dc, "Language", DbType.String, model.Language);
            _db.AddInParameter(dc, "Level", DbType.Byte, (int)model.Level);
            _db.AddInParameter(dc, "Mobile", DbType.AnsiString, model.Mobile);
            _db.AddInParameter(dc, "QQ", DbType.AnsiString, model.QQ);
            _db.AddInParameter(dc, "Msn", DbType.AnsiString, model.Msn);
            _db.AddInParameter(dc, "Email", DbType.AnsiString, model.Email);
            _db.AddInParameter(dc, "HomeTel", DbType.AnsiString, model.HomeTel);
            _db.AddInParameter(dc, "Address", DbType.String, model.Address);
            _db.AddInParameter(dc, "Photo", DbType.AnsiString, model.Photo);
            _db.AddInParameter(dc, "Character", DbType.String, model.Character);
            _db.AddInParameter(dc, "TourExperience", DbType.String, model.TourExperience);
            _db.AddInParameter(dc, "CustomerReviews", DbType.String, model.CustomerReviews);
            _db.AddInParameter(dc, "Specialty", DbType.String, model.Specialty);
            _db.AddInParameter(dc, "Remarks", DbType.String, model.Remarks);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.DeptId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "MaintainId", DbType.AnsiStringFixedLength, model.MaintainId);
            _db.AddInParameter(dc, "MaintainTime", DbType.DateTime, model.MaintainTime);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserModel.UserId);
            _db.AddInParameter(dc, "GovFileId", DbType.AnsiStringFixedLength, model.GovFileId);
            _db.AddInParameter(dc, "UserName", DbType.String, model.UserModel.UserName);
            _db.AddInParameter(dc, "Password", DbType.AnsiString, model.UserModel.Password);
            _db.AddInParameter(dc, "MD5Password", DbType.AnsiString, model.UserModel.MD5Password);
            _db.AddInParameter(dc, "GovDeptId", DbType.Int32, model.UserModel.DeptId);
            _db.AddInParameter(dc, "Operator", DbType.String, model.UserModel.Operator);
            _db.AddInParameter(dc, "SourceGuideCategoryXML", DbType.Xml, CreateSourceGuideCategoryListXML(model.SourceGuideCategoryList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改导游Model
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns>返回值 -1:存在相同的用户名 -2:存在相同的身份证号 -3:存在相同的导游证号 小于0:错误 1:正确</returns>
        public int UpdateGuideModel(Model.SourceStructure.MSourceGuide model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateGuide);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, model.GuideId);
            _db.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "Gender", DbType.Byte, (int)model.Gender);
            _db.AddInParameter(dc, "IDNumber", DbType.AnsiString, model.IDNumber);
            _db.AddInParameter(dc, "GuideNumber", DbType.AnsiString, model.GuideNumber);
            _db.AddInParameter(dc, "LeaderNumber", DbType.AnsiString, model.LeaderNumber);
            _db.AddInParameter(dc, "AnchoredCom", DbType.Byte, (int)model.AnchoredCom);
            _db.AddInParameter(dc, "AnnualReview", DbType.AnsiStringFixedLength, model.AnnualReview == true ? "1" : "0");//年审状态(0未审，,1:已审)
            _db.AddInParameter(dc, "Language", DbType.String, model.Language);
            _db.AddInParameter(dc, "Level", DbType.Byte, (int)model.Level);
            _db.AddInParameter(dc, "Mobile", DbType.AnsiString, model.Mobile);
            _db.AddInParameter(dc, "QQ", DbType.AnsiString, model.QQ);
            _db.AddInParameter(dc, "Msn", DbType.AnsiString, model.Msn);
            _db.AddInParameter(dc, "Email", DbType.AnsiString, model.Email);
            _db.AddInParameter(dc, "HomeTel", DbType.AnsiString, model.HomeTel);
            _db.AddInParameter(dc, "Address", DbType.String, model.Address);
            _db.AddInParameter(dc, "Photo", DbType.AnsiString, model.Photo);
            _db.AddInParameter(dc, "Character", DbType.String, model.Character);
            _db.AddInParameter(dc, "TourExperience", DbType.String, model.TourExperience);
            _db.AddInParameter(dc, "CustomerReviews", DbType.String, model.CustomerReviews);
            _db.AddInParameter(dc, "Specialty", DbType.String, model.Specialty);
            _db.AddInParameter(dc, "Remarks", DbType.String, model.Remarks);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.DeptId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "MaintainId", DbType.AnsiStringFixedLength, model.MaintainId);
            _db.AddInParameter(dc, "MaintainTime", DbType.DateTime, model.MaintainTime);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserModel.UserId);
            _db.AddInParameter(dc, "GovFileId", DbType.AnsiStringFixedLength, model.GovFileId);
            _db.AddInParameter(dc, "UserName", DbType.String, model.UserModel.UserName);
            _db.AddInParameter(dc, "Password", DbType.AnsiString, model.UserModel.Password);
            _db.AddInParameter(dc, "MD5Password", DbType.AnsiString, model.UserModel.MD5Password);
            _db.AddInParameter(dc, "GovDeptId", DbType.Int32, model.UserModel.DeptId);
            _db.AddInParameter(dc, "Operator", DbType.String, model.UserModel.Operator);
            _db.AddInParameter(dc, "SourceGuideCategoryXML", DbType.Xml, CreateSourceGuideCategoryListXML(model.SourceGuideCategoryList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /*/// <summary>
        /// 删除导游Model
        /// </summary>
        /// <param name="GuideId">供应商编号</param>
        /// <returns></returns>
        //public int DeleteGuideModel(string GuideId)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    GuideId = "'" + GuideId + "'";
        //    sql.AppendFormat(sql_Source_DeleteGuide, GuideId.ToString());
        //    DbCommand dc = _db.GetSqlStringCommand(sql.ToString());
        //    return DbHelper.ExecuteSql(dc, _db);
        //}*/

        /// <summary>
        /// 批量删除导游Model
        /// </summary>
        /// <param name="guideIdList">供应商编号List</param>
        /// <returns>负值或0：失败； 正值1：成功；  正值2：导游有安排</returns>
        public int DeleteGuideModel(params string[] guideIdList)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < guideIdList.Length; i++)
            {
                sId.AppendFormat("{0},", guideIdList[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_SourceGuide_Delete");
            this._db.AddInParameter(dc, "GuideIdList", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 获得显示在列表页面上面的导游信息列表
        /// </summary>
        /// <param name="Searchmodel">导游列表查询Model</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideListModel> GetGuideShowModel(Model.SourceStructure.MGuideSearchModel Searchmodel, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.SourceStructure.MGuideListModel> list = new List<Model.SourceStructure.MGuideListModel>();
            StringBuilder sbWhereSql = new StringBuilder();
            sbWhereSql.AppendFormat(" CompanyId='{0}'", CompanyId);
            if (Searchmodel != null)
            {
                if (!string.IsNullOrEmpty(Searchmodel.Name))
                {
                    sbWhereSql.AppendFormat(" and Name like '%{0}%'", Searchmodel.Name);
                }
                if (Searchmodel.Gender != null && (int)Searchmodel.Gender >= 0)
                {
                    sbWhereSql.AppendFormat(" and Gender={0} ", (int)Searchmodel.Gender);
                }
                if (!string.IsNullOrEmpty(Searchmodel.Language))
                {
                    sbWhereSql.AppendFormat(" and Language like '%{0}%' ", Searchmodel.Language);
                }
                if (Searchmodel.Level != null && (int)Searchmodel.Level >= 0)
                {
                    sbWhereSql.AppendFormat(" and Level={0} ", (int)Searchmodel.Level);

                }
                if (Searchmodel.GuideCategory != null && (int)Searchmodel.GuideCategory >= 0)
                {
                    sbWhereSql.AppendFormat(" AND CAST(GuideCategoryXML AS XML).exist('/ROOT/row/@Category[.=\"{0}\"]') = 1", (int)Searchmodel.GuideCategory);
                }
            }
            sbWhereSql.Append(" and IsDelete='0'");
            string fields = " GuideId,CompanyId,Name,Gender,Language,Level,Mobile,Email,OperatorId,IssueTime,IsDelete,AnchoredCom,AnnualReview,TourExperience,GuideCategoryXML,GroupCount,DayCount  ";
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_SourceGuidePlan", "GuideId", fields, sbWhereSql.ToString(), "  IssueTime desc"))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MGuideListModel guideListModel = new EyouSoft.Model.SourceStructure.MGuideListModel();
                    guideListModel.Gender = (Model.EnumType.GovStructure.Gender)(int.Parse(reader["Gender"].ToString()));
                    guideListModel.GuideId = !reader.IsDBNull(reader.GetOrdinal("GuideId")) ? (string)reader["GuideId"] : string.Empty;
                    guideListModel.Language = !reader.IsDBNull(reader.GetOrdinal("Language")) ? (string)reader["Language"] : string.Empty;
                    guideListModel.Level = (Model.EnumType.SourceStructure.GuideLevel)(int.Parse(reader["Level"].ToString()));
                    guideListModel.Mobile = !reader.IsDBNull(reader.GetOrdinal("Mobile")) ? (string)reader["Mobile"] : string.Empty;
                    guideListModel.Email = !reader.IsDBNull(reader.GetOrdinal("Email")) ? (string)reader["Email"] : string.Empty;
                    guideListModel.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    guideListModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty;
                    guideListModel.Counts = !reader.IsDBNull(reader.GetOrdinal("GroupCount")) ? reader.GetInt32(reader.GetOrdinal("GroupCount")) : 0;
                    guideListModel.Days = !reader.IsDBNull(reader.GetOrdinal("DayCount")) ? reader.GetInt32(reader.GetOrdinal("DayCount")) : 0;
                    if (!reader.IsDBNull(reader.GetOrdinal("GuideCategoryXML")))
                    {
                        guideListModel.SourceGuideCategoryList = this.GetGuideCategoryList(reader["GuideCategoryXML"].ToString());
                    }
                    guideListModel.AnchoredCom = !reader.IsDBNull(reader.GetOrdinal("AnchoredCom")) ? (Model.EnumType.SourceStructure.AnchoredCom)(int.Parse(reader["AnchoredCom"].ToString())) : Model.EnumType.SourceStructure.AnchoredCom.其他;
                    if (!reader.IsDBNull(reader.GetOrdinal("AnnualReview")))
                    {
                        guideListModel.AnnualReview = (string)reader["AnnualReview"] == "1" ? true : false;
                    }
                    guideListModel.TourExperience = !reader.IsDBNull(reader.GetOrdinal("TourExperience")) ? (string)reader["TourExperience"] : "";

                    list.Add(guideListModel);
                }
            }

            return list;
        }

        /// <summary>
        /// 获得导游Model
        /// </summary>
        /// <param name="GuideId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceGuide GetGuideModel(string GuideId)
        {
            Model.SourceStructure.MSourceGuide guideModel = new EyouSoft.Model.SourceStructure.MSourceGuide();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Guide);
            _db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, GuideId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    guideModel.Address = !reader.IsDBNull(reader.GetOrdinal("Address")) ? (string)reader["Address"] : string.Empty;
                    guideModel.Msn = !reader.IsDBNull(reader.GetOrdinal("Msn")) ? (string)reader["Msn"] : string.Empty;
                    guideModel.Character = !reader.IsDBNull(reader.GetOrdinal("Character")) ? (string)reader["Character"] : string.Empty;
                    guideModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    guideModel.CustomerReviews = !reader.IsDBNull(reader.GetOrdinal("CustomerReviews")) ? (string)reader["CustomerReviews"] : string.Empty;
                    guideModel.Email = !reader.IsDBNull(reader.GetOrdinal("Email")) ? (string)reader["Email"] : string.Empty;
                    guideModel.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)(int.Parse(reader["Gender"].ToString()));
                    guideModel.AnchoredCom = !reader.IsDBNull(reader.GetOrdinal("AnchoredCom")) ? (Model.EnumType.SourceStructure.AnchoredCom)(int.Parse(reader["AnchoredCom"].ToString())) : Model.EnumType.SourceStructure.AnchoredCom.其他;
                    if (!reader.IsDBNull(reader.GetOrdinal("GuideCategoryXML")))
                    {
                        guideModel.SourceGuideCategoryList = this.GetGuideCategoryList(reader["GuideCategoryXML"].ToString());
                    }
                    guideModel.GuideId = reader["GuideId"].ToString();
                    guideModel.GuideNumber = !reader.IsDBNull(reader.GetOrdinal("GuideNumber")) ? (string)reader["GuideNumber"] : string.Empty;
                    guideModel.LeaderNumber = !reader.IsDBNull(reader.GetOrdinal("LeaderNumber")) ? (string)reader["LeaderNumber"] : string.Empty;
                    guideModel.HomeTel = !reader.IsDBNull(reader.GetOrdinal("HomeTel")) ? (string)reader["HomeTel"] : string.Empty;
                    guideModel.IDNumber = !reader.IsDBNull(reader.GetOrdinal("IDNumber")) ? (string)reader["IDNumber"] : string.Empty;
                    if (!reader.IsDBNull(reader.GetOrdinal("AnnualReview")))
                    {
                        guideModel.AnnualReview = (string)reader["AnnualReview"] == "1" ? true : false;
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("AnnualReview")))
                    {
                        guideModel.IsDelete = (string)reader["IsDelete"] == "1" ? true : false;
                    }
                    guideModel.IssueTime = (DateTime)reader["IssueTime"];
                    guideModel.Language = !reader.IsDBNull(reader.GetOrdinal("Language")) ? (string)reader["Language"] : string.Empty;
                    guideModel.Level = (EyouSoft.Model.EnumType.SourceStructure.GuideLevel)(int.Parse(reader["Level"].ToString()));
                    guideModel.Mobile = !reader.IsDBNull(reader.GetOrdinal("Mobile")) ? (string)reader["Mobile"] : string.Empty;
                    guideModel.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    guideModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty; ;
                    guideModel.Photo = !reader.IsDBNull(reader.GetOrdinal("Photo")) ? (string)reader["Photo"] : string.Empty;
                    guideModel.QQ = !reader.IsDBNull(reader.GetOrdinal("QQ")) ? (string)reader["QQ"] : string.Empty;
                    guideModel.Remarks = !reader.IsDBNull(reader.GetOrdinal("Remarks")) ? (string)reader["Remarks"] : string.Empty;
                    guideModel.Specialty = !reader.IsDBNull(reader.GetOrdinal("Specialty")) ? (string)reader["Specialty"] : string.Empty;
                    guideModel.TourExperience = !reader.IsDBNull(reader.GetOrdinal("TourExperience")) ? (string)reader["TourExperience"] : string.Empty;
                    guideModel.UserId = !reader.IsDBNull(reader.GetOrdinal("UserId")) ? (string)reader["UserId"] : string.Empty;
                    guideModel.MaintainId = !reader.IsDBNull(reader.GetOrdinal("MaintainId")) ? (string)reader["MaintainId"] : string.Empty;
                    guideModel.GovFileId = !reader.IsDBNull(reader.GetOrdinal("GovFileId")) ? (string)reader["GovFileId"] : string.Empty;
                    //EyouSoft.Model.ComStructure.MComUser mod = new MComUser();
                    guideModel.UserModel = new MComUser();
                    guideModel.UserModel.DeptId = !reader.IsDBNull(reader.GetOrdinal("DepartId")) ? (int)reader["DepartId"] : 0;
                    guideModel.UserModel.DeptName = !reader.IsDBNull(reader.GetOrdinal("DepartName")) ? (string)reader["DepartName"] : string.Empty; ;
                    if (!reader.IsDBNull(reader.GetOrdinal("MaintainTime")))
                    {
                        guideModel.MaintainTime = (DateTime)reader["MaintainTime"];
                    }
                }
            }
            guideModel.UserModel = userDal.GetModel(guideModel.UserId, guideModel.CompanyId);
            return guideModel;
        }

        /// <summary>
        /// 获得导游上团统计信息列表
        /// </summary>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="TimeBegin">查询开始时间</param>
        /// <param name="TimeEnd">查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideListGroup> GetGuideListGroup(string GuideName, DateTime? TimeBegin, DateTime? TimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuideListGroup> ResultList = null;
            DbCommand dc = this._db.GetStoredProcCommand("proc_SelectGuideSTTJ");
            this._db.AddInParameter(dc, "OnTimeStart", DbType.DateTime, TimeBegin);
            this._db.AddInParameter(dc, "OnTimeEnd", DbType.DateTime, TimeEnd);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null) && int.Parse(Result.ToString()) > 0)
            {
                string tableName = "view_GuideSTTJ";
                string identityColumnName = "GuideId";
                string fields = " GuideId,CompanyID,Name,IssueTime,TCount,DCount,CAST(GuideJF AS decimal(18,4)) GuideJF  ";
                string query = string.Format(" CompanyId='{0}'", CompanyId);
                if (!string.IsNullOrEmpty(GuideName))
                {
                    query = query + string.Format(" AND Name like  '%{0}%'", GuideName);
                }
                string orderByString = " IssueTime DESC";
                using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identityColumnName, fields, query, orderByString))
                {
                    ResultList = new List<EyouSoft.Model.SourceStructure.MGuideListGroup>();
                    EyouSoft.Model.SourceStructure.MGuideListGroup model = null;
                    while (dr.Read())
                    {
                        model = new EyouSoft.Model.SourceStructure.MGuideListGroup();
                        model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                        model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyID")) ? dr.GetString(dr.GetOrdinal("CompanyID")) : "";
                        model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                        model.GroupCount = !dr.IsDBNull(dr.GetOrdinal("TCount")) ? dr.GetInt32(dr.GetOrdinal("TCount")) : 0;
                        model.DayCount = !dr.IsDBNull(dr.GetOrdinal("DCount")) ? dr.GetInt32(dr.GetOrdinal("DCount")) : 0;
                        model.Score = !dr.IsDBNull(dr.GetOrdinal("GuideJF")) ? dr.GetDecimal((dr.GetOrdinal("GuideJF"))) : 0;
                        ResultList.Add(model);
                        model = null;
                    }
                };
            }
            return ResultList;
        }

        /// <summary>
        /// 获得导游带团详细信息列表
        /// </summary>
        /// <param name="GuideId">导游编号</param>
        /// <param name="TimeBegin">上团查询开始时间</param>
        /// <param name="TimeEnd">上团查询结束时间</param>
        /// <param name="LTimeBegin">出团查询开始时间</param>
        /// <param name="LTimeEnd">出团查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideTourList> GetGuideTourList(string GuideId, DateTime? TimeBegin, DateTime? TimeEnd, DateTime? LTimeBegin, DateTime? LTimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuideTourList> ResultList = null;
            string tableName = "view_SourceGuideGroupInfo";
            string identityColumnName = "SourceId";
            string fields = " CompanyId,SourceId,PlanId,TourId,TourCode,RouteName,StartDate,EndDate,DayCount,LDate,RealPeopleNumber  ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (!string.IsNullOrEmpty(GuideId))
            {
                query = query + string.Format(" AND SourceId = '{0}' ", GuideId);
            }
            if (TimeBegin != null)
            {
                query = query + string.Format(" AND StartDate>='{0}' ", TimeBegin);
            }
            if (TimeEnd != null)
            {
                query = query + string.Format(" AND StartDate<'{0}' ", TimeEnd.Value.AddDays(1));
            }
            if (LTimeBegin != null)
            {
                query = query + string.Format(" AND LDate>='{0}' ", LTimeBegin);
            }
            if (LTimeEnd != null)
            {
                query = query + string.Format(" AND LDate<'{0}' ", LTimeEnd.Value.AddDays(1));
            }
            string orderByString = " LDate DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.SourceStructure.MGuideTourList>();
                EyouSoft.Model.SourceStructure.MGuideTourList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SourceStructure.MGuideTourList();
                    model.TourId = !dr.IsDBNull(dr.GetOrdinal("TourId")) ? dr.GetString(dr.GetOrdinal("TourId")) : "";
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : "";
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                    {
                        model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    }
                    model.DayCount = !dr.IsDBNull(dr.GetOrdinal("DayCount")) ? dr.GetInt32(dr.GetOrdinal("DayCount")) : 0;
                    model.RealPeopleNumber = !dr.IsDBNull(dr.GetOrdinal("RealPeopleNumber")) ? dr.GetInt32(dr.GetOrdinal("RealPeopleNumber")) : 0;
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuidePlanWork> ResultList = null;
            string DateNow = "";
            string DateNext = "";
            if (NextTimeStart != null || NextTimeEnd != null)
            {
                DateNow = null;
                DateNext = null;
                if (NextTimeStart != null && NextTimeEnd != null && NextTimeEnd.Value.Subtract(NextTimeStart.Value).Days > 34)
                {
                    NextTimeEnd = NextTimeStart.Value.AddDays(34);
                }
                if (NextTimeStart != null && NextTimeEnd == null)
                {
                    NextTimeEnd = NextTimeStart.Value.AddDays(34);
                }
                if (NextTimeStart == null && NextTimeEnd != null)
                {
                    NextTimeStart = NextTimeEnd.Value.AddDays(-34);
                }
            }
            else
            {
                int monthNow = DateTime.Now.Month;
                int yearNow = DateTime.Now.Year;
                if (month > 0)
                {
                    monthNow = month;
                    if (year > 0)
                    {
                        yearNow = year;
                    }
                }
                if (monthNow == 1 || monthNow == 3 || monthNow == 5 || monthNow == 7 || monthNow == 8 || monthNow == 10 || monthNow == 12)
                {
                    DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                    DateNext = DateTime.Parse(DateNow).AddDays(30).ToString("yyyy-MM-dd");
                    DateNow = DateTime.Parse(DateNow).AddDays(-4).ToString("yyyy-MM-dd");
                }
                if (monthNow == 2)
                {
                    if ((yearNow % 100 == 0) && (yearNow % 400 == 0) || (yearNow % 100 != 0) && (yearNow % 4 == 0))
                    {
                        DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                        DateNext = DateTime.Parse(DateNow).AddDays(28).ToString("yyyy-MM-dd");
                        DateNow = DateTime.Parse(DateNow).AddDays(-6).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                        DateNext = DateTime.Parse(DateNow).AddDays(27).ToString("yyyy-MM-dd");
                        DateNow = DateTime.Parse(DateNow).AddDays(-7).ToString("yyyy-MM-dd");
                    }
                }
                if (monthNow == 4 || monthNow == 6 || monthNow == 9 || monthNow == 11)
                {
                    DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                    DateNext = DateTime.Parse(DateNow).AddDays(29).ToString("yyyy-MM-dd");
                    DateNow = DateTime.Parse(DateNow).AddDays(-5).ToString("yyyy-MM-dd");
                }
            }
            DbCommand dc = this._db.GetStoredProcCommand("proc_SelectGuideDYPB");
            this._db.AddInParameter(dc, "OnTimeStart", DbType.String, DateNow);
            this._db.AddInParameter(dc, "OnTimeEnd", DbType.String, DateNext);
            this._db.AddInParameter(dc, "NextTimeStart", DbType.String, NextTimeStart != null ? NextTimeStart.Value.ToString("yyyy-MM-dd") : "");
            this._db.AddInParameter(dc, "NextTimeEnd", DbType.String, NextTimeStart != null ? NextTimeEnd.Value.ToString("yyyy-MM-dd") : "");
            this._db.AddInParameter(dc, "NextLocation", DbType.String, Location);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null) && int.Parse(Result.ToString()) > 0)
            {
                string tableName = "view_GuideDYPB";
                string identityColumnName = "GuideId";
                string fields = " GuideId,CompanyId,Name,StaffStatus,IssueTime,TypeXML  ";
                string query = string.Format(" CompanyId='{0}' AND (StaffStatus<>2 OR TypeXML IS NOT NULL)", CompanyId);
                if (!string.IsNullOrEmpty(GuideName))
                {
                    query = query + string.Format(" AND Name like  '%{0}%' ", GuideName);
                }
                string orderByString = " IssueTime DESC";
                using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identityColumnName, fields, query, orderByString))
                {
                    ResultList = new List<EyouSoft.Model.SourceStructure.MGuidePlanWork>();
                    EyouSoft.Model.SourceStructure.MGuidePlanWork model = null;
                    while (dr.Read())
                    {
                        model = new EyouSoft.Model.SourceStructure.MGuidePlanWork();
                        model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                        model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyID")) ? dr.GetString(dr.GetOrdinal("CompanyID")) : "";
                        model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                        model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("StaffStatus")).ToString());
                        model.TypeList = this.GetTypeList(dr["TypeXML"].ToString());
                        ResultList.Add(model);
                        model = null;
                    }
                };
            }
            return ResultList;
        }

        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date)
        {
            IList<EyouSoft.Model.SourceStructure.MGuidePlanWork> ResultList = null;
            DbCommand dc = _db.GetSqlStringCommand("select * from view_SourceGuidePlanWork where CompanyId=@CompanyId and GuideId=@GuideId and (datediff(dd,@date,StartDate)=0 or datediff(dd,@date,ENDdate)=0 or (@date>StartDate and @date<ENDdate))");
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            _db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, GuideId);
            _db.AddInParameter(dc, "date", DbType.DateTime, date);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, _db))
            {
                ResultList = new List<EyouSoft.Model.SourceStructure.MGuidePlanWork>();
                EyouSoft.Model.SourceStructure.MGuidePlanWork model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SourceStructure.MGuidePlanWork();
                    model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                    model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? dr.GetString(dr.GetOrdinal("CompanyId")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.PlanId = !dr.IsDBNull(dr.GetOrdinal("PlanId")) ? dr.GetString(dr.GetOrdinal("PlanId")) : "";
                    model.TourId = !dr.IsDBNull(dr.GetOrdinal("TourId")) ? dr.GetString(dr.GetOrdinal("TourId")) : "";
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : "";
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                    {
                        model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("RDate")))
                    {
                        model.RDate = dr.GetDateTime(dr.GetOrdinal("RDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("StartDate")))
                    {
                        model.OnTime = dr.GetDateTime(dr.GetOrdinal("StartDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDdate")))
                    {
                        model.NextTime = dr.GetDateTime(dr.GetOrdinal("ENDdate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }

                    model.TourDays = !dr.IsDBNull(dr.GetOrdinal("TourDays")) ? dr.GetInt32(dr.GetOrdinal("TourDays")) : 0;
                    model.OnLocation = !dr.IsDBNull(dr.GetOrdinal("OnLocation")) ? dr.GetString(dr.GetOrdinal("OnLocation")) : "";
                    model.NextLocation = !dr.IsDBNull(dr.GetOrdinal("NextLocation")) ? dr.GetString(dr.GetOrdinal("NextLocation")) : "";
                    model.GuideDays = !dr.IsDBNull(dr.GetOrdinal("GuideDays")) ? dr.GetInt32(dr.GetOrdinal("GuideDays")) : 0;
                    model.PlanCost = !dr.IsDBNull(dr.GetOrdinal("Confirmation")) ? dr.GetDecimal(dr.GetOrdinal("Confirmation")) : 0;
                    model.PlanState = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), dr.GetByte(dr.GetOrdinal("Status")).ToString());
                    ResultList.Add(model);
                    model = null;
                }
            }
            return ResultList;
        }

        #endregion

        #region 线路
        /// <summary>
        /// 添加线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns>返回值 -1:已经存在相同的线路名称 -2;同一个行程已经存在相同的景点 -3:已经存在相同的附件名称 0:事务回滚 1:正确</returns>
        public int AddRouteModel(Model.SourceStructure.MRoute model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_AddRoute);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);

            _db.AddInParameter(dc, "xmlRouteString", DbType.Xml, GetRouteXmlString(model));
            _db.AddInParameter(dc, "xmlPlanString", DbType.Xml, GetRoutePlanXmlString(model.PlanModelList));
            _db.AddInParameter(dc, "xmlStandardString", DbType.Xml, GetRouteStandardXmlString(model.StandardModelList));
            _db.AddInParameter(dc, "xmlServicesString", DbType.Xml, GetRouteServicesXmlString(model.ServicesModel));
            _db.AddInParameter(dc, "xmlSpotString", DbType.Xml, GetPlanSpotXmlString(model.PlanModelList));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.Attach));
            _db.AddInParameter(dc, "xmlQAttachString", DbType.Xml, GetAttachXmlString(model.VisaInfoList));
            _db.AddInParameter(dc, "xmlPAttachString", DbType.Xml, GetRoutePlanPhotoXmlString(model.PlanModelList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns>返回值 -1:已经存在相同的线路名称 -2;同一个行程已经存在相同的景点 -3:已经存在相同的附件名称 0:事务回滚 1:正确</returns>
        public int UpdateRouteModel(Model.SourceStructure.MRoute model)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateRoute);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, model.RouteId);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "IsShare", DbType.AnsiStringFixedLength, model.IsShare ? "1" : "0");
            _db.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
            _db.AddInParameter(dc, "LineIntro", DbType.String, model.LineIntro);
            _db.AddInParameter(dc, "Days", DbType.Int32, model.Days);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "IsTourOrSubentry", DbType.AnsiStringFixedLength, model.IsTourOrSubentry ? "1" : "0");
            _db.AddInParameter(dc, "Service", DbType.String, model.Service);
            _db.AddInParameter(dc, "OtherPrice", DbType.Currency, model.OtherPrice);
            _db.AddInParameter(dc, "AdultPrice", DbType.Currency, model.AdultPrice);
            _db.AddInParameter(dc, "ChildrenPrice", DbType.Currency, model.ChildrenPrice);
            _db.AddInParameter(dc, "TotalPrice", DbType.Currency, model.TotalPrice);
            _db.AddInParameter(dc, "PathRemark", DbType.String, model.PathRemark);
            _db.AddInParameter(dc, "Excluding", DbType.String, model.ServicesModel.NoNeedItem);
            _db.AddInParameter(dc, "Shopping", DbType.String, model.ServicesModel.ShoppingItem);
            _db.AddInParameter(dc, "Children", DbType.String, model.ServicesModel.ChildServiceItem);
            _db.AddInParameter(dc, "Chargeable", DbType.String, model.ServicesModel.OwnExpense);
            _db.AddInParameter(dc, "Note", DbType.String, model.ServicesModel.NeedAttention);
            _db.AddInParameter(dc, "WarmPrompt", DbType.String, model.ServicesModel.WarmRemind);
            _db.AddInParameter(dc, "Internal", DbType.String, model.ServicesModel.InsiderInfor);

            _db.AddInParameter(dc, "SetMode", DbType.String, model.SetMode);
            _db.AddInParameter(dc, "DepartureTraffic", DbType.String, model.DepartureTraffic);
            _db.AddInParameter(dc, "ReturnTraffic", DbType.String, model.ReturnTraffic);
            _db.AddInParameter(dc, "TripAdvantage", DbType.String, model.TripAdvantage);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.DeptId);
            _db.AddInParameter(dc, "xmlRoutePlan", DbType.Xml, GetRoutePlanXmlString(model.PlanModelList));
            _db.AddInParameter(dc, "xmlRoutePlanSpot", DbType.Xml, GetPlanSpotXmlString(model.PlanModelList));
            _db.AddInParameter(dc, "xmlRouteStandard", DbType.Xml, GetRouteStandardXmlString(model.StandardModelList));
            _db.AddInParameter(dc, "AttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路附件);
            _db.AddInParameter(dc, "QAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路签证资料);
            _db.AddInParameter(dc, "PAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路行程图片);
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.Attach));
            _db.AddInParameter(dc, "xmlQAttachString", DbType.Xml, GetAttachXmlString(model.VisaInfoList));
            _db.AddInParameter(dc, "xmlPAttachString", DbType.Xml, GetRoutePlanPhotoXmlString(model.PlanModelList));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 删除线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        public int DeleteRouteModel(string RouteId)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_Source_DeleteRoute");
            Model.SourceStructure.MRoute routeModel = GetRouteModel(RouteId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, routeModel.OperatorId);
            _db.AddInParameter(dc, "RouteId", DbType.String, RouteId);
            _db.AddInParameter(dc, "AttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路附件);
            _db.AddInParameter(dc, "QAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路签证资料);
            _db.AddInParameter(dc, "PAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.线路行程图片);
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }
            if (sqlExceptionCode < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 获得显示在线路列表上的数据
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <param name="isOnlySeft">是否显示关于自己</param>
        /// <param name="loginUserId">登录用户编号</param>
        /// <param name="DeptId">部门编号</param>
        /// <param name="startDate">开始发布日期</param>
        /// <param name="endDate">结束发布日期</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MRouteListModel> GetRouteShowModel(Model.SourceStructure.MRouteListModel model, bool isOnlySeft, string loginUserId, string DeptId, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.SourceStructure.MRouteListModel> list = new List<Model.SourceStructure.MRouteListModel>();
            StringBuilder sbWhereSql = new StringBuilder();
            sbWhereSql.AppendFormat(" CompanyId='{0}'", model.CompanyId);
            //if (model.RouteType != null)
            //{
            //    sbWhereSql.AppendFormat(" and Type={0} ", (int)model.RouteType);
            //}
            if (model.RouteAreaId != 0)
            {
                sbWhereSql.AppendFormat(" and AreaId={0} ", model.RouteAreaId);
            }
            if (model.DayCount != 0)
            {
                sbWhereSql.AppendFormat(" and Days={0} ", model.DayCount);
            }

            if (startDate != null)
            {
                sbWhereSql.AppendFormat(" and IssueTime>='{0}' ", startDate.ToString());
            }
            if (endDate != null)
            {
                sbWhereSql.AppendFormat(" and IssueTime<'{0}' ", endDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(model.OperatorName))
            {
                sbWhereSql.AppendFormat(" and ContactName like '%{0}%' ", model.OperatorName);
            }
            if (!string.IsNullOrEmpty(model.RouteName))
            {
                sbWhereSql.AppendFormat(" and RouteName like '%{0}%'", model.RouteName);
            }
            //if (isOnlySeft)
            //{
            //    sbWhereSql.AppendFormat(" and OperatorId='{0}' ", loginUserId);
            //}
            //else if (!string.IsNullOrEmpty(DeptId))
            //{
            //    sbWhereSql.AppendFormat(" and DeptId in ({0}) and (IsShare=1 or OperatorId='{1}')", DeptId, loginUserId);
            //}
            //else
            //{
            //    sbWhereSql.AppendFormat(" and (IsShare=1 or OperatorId='{0}')", loginUserId);
            //}
            sbWhereSql.AppendFormat(" and IsDelete='0' and (IsShare=1 or OperatorId='{0}')", loginUserId);

            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_RouteAreaTourOrderUser", "RouteId", "*", sbWhereSql.ToString(), "  IssueTime desc"))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MRouteListModel routeListModel = new EyouSoft.Model.SourceStructure.MRouteListModel();
                    routeListModel.Id = (int)reader["Id"];
                    routeListModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    routeListModel.DayCount = (int)reader["Days"];
                    routeListModel.IssueTime = (DateTime)reader["IssueTime"];
                    routeListModel.OperatorName = !reader.IsDBNull(reader.GetOrdinal("ContactName")) ? (string)reader["ContactName"] : string.Empty;
                    routeListModel.RouteAreaId = (int)reader["AreaId"];
                    routeListModel.RouteId = !reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (string)reader["RouteId"] : string.Empty;
                    routeListModel.RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty;
                    routeListModel.RouteType = (EyouSoft.Model.EnumType.ComStructure.AreaType)(int.Parse(reader["Type"].ToString()));
                    routeListModel.RouteTypeName = !reader.IsDBNull(reader.GetOrdinal("AreaName")) ? (string)reader["AreaName"] : string.Empty;
                    routeListModel.SKCount = (int)reader["SKCount"];
                    routeListModel.STCount = (int)reader["STCount"];
                    Model.ComStructure.MComUser userModel = userDal.GetModel((!reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty), routeListModel.CompanyId);
                    routeListModel.UserList = new List<Model.ComStructure.MComUser>();
                    routeListModel.UserList.Add(userModel);
                    routeListModel.OperatorId = reader["OperatorId"].ToString();
                    routeListModel.IsShare = reader["IsShare"].ToString() == "1";
                    list.Add(routeListModel);
                }
            }

            return list;
        }

        /// <summary>
        /// 获得线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MRoute GetRouteModel(string RouteId)
        {
            Model.SourceStructure.MRoute routeModel = new EyouSoft.Model.SourceStructure.MRoute();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectRoute);
            _db.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, RouteId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    routeModel.AdultPrice = !reader.IsDBNull(reader.GetOrdinal("AdultPrice")) ? (decimal)reader["AdultPrice"] : 0;
                    routeModel.AreaId = (int)reader["AreaId"];
                    routeModel.ChildrenPrice = !reader.IsDBNull(reader.GetOrdinal("ChildrenPrice")) ? (decimal)reader["ChildrenPrice"] : 0;
                    routeModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    routeModel.Days = (int)reader["Days"];
                    routeModel.IsDelete = !reader.IsDBNull(reader.GetOrdinal("IsDelete")) ? reader["IsDelete"].ToString() == "0" ? false : true : false;
                    routeModel.IsShare = !reader.IsDBNull(reader.GetOrdinal("IsShare")) ? reader["IsShare"].ToString() == "0" ? false : true : false;
                    routeModel.IssueTime = (DateTime)reader["IssueTime"];
                    routeModel.IsTourOrSubentry = !reader.IsDBNull(reader.GetOrdinal("IsTourOrSubentry")) ? reader["IsTourOrSubentry"].ToString() == "0" ? false : true : false;
                    routeModel.LineIntro = !reader.IsDBNull(reader.GetOrdinal("LineIntro")) ? (string)reader["LineIntro"] : string.Empty;
                    routeModel.SetMode = !reader.IsDBNull(reader.GetOrdinal("SetMode")) ? (string)reader["SetMode"] : string.Empty;
                    routeModel.DepartureTraffic = !reader.IsDBNull(reader.GetOrdinal("DepartureTraffic")) ? (string)reader["DepartureTraffic"] : string.Empty;
                    routeModel.ReturnTraffic = !reader.IsDBNull(reader.GetOrdinal("ReturnTraffic")) ? (string)reader["ReturnTraffic"] : string.Empty;
                    routeModel.TripAdvantage = !reader.IsDBNull(reader.GetOrdinal("TripAdvantage")) ? (string)reader["TripAdvantage"] : string.Empty;

                    routeModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty;
                    routeModel.RouteId = RouteId;
                    routeModel.RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty;
                    routeModel.Service = !reader.IsDBNull(reader.GetOrdinal("Service")) ? (string)reader["Service"] : string.Empty;
                    routeModel.TotalPrice = !reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? (decimal)reader["TotalPrice"] : 0;
                    routeModel.OtherPrice = !reader.IsDBNull(reader.GetOrdinal("OtherPrice")) ? (decimal)reader["OtherPrice"] : 0;
                    routeModel.DeptId = (int)reader["DeptId"];
                    routeModel.PathRemark = !reader.IsDBNull(reader.GetOrdinal("PathRemark")) ?reader.GetString( reader.GetOrdinal("PathRemark")) : "";
                    routeModel.Attach = attachDal.GetModel(routeModel.RouteId, Model.EnumType.ComStructure.AttachItemType.线路附件);
                    routeModel.VisaInfoList = attachDal.GetModelList(routeModel.RouteId, Model.EnumType.ComStructure.AttachItemType.线路签证资料);
                }
            }
            routeModel.PlanModelList = GetRoutePlanModelList(RouteId);
            routeModel.ServicesModel = GetRouteServiceModel(RouteId);
            routeModel.StandardModelList = GetRouteStandardModelList(RouteId);
            return routeModel;
        }

        /// <summary>
        /// 判断线路名称是否存在
        /// </summary>
        /// <param name="RouteName">线路名称</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        public bool IsExists(string RouteName, string companyId, int areaId)
        {
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_IsExistsRouteName);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(dc, "RouteName", DbType.String, RouteName);
            _db.AddInParameter(dc, "AreaId", DbType.Int32, areaId);
            if (DbHelper.ExecuteReader(dc, _db).Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获得上团列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MTourOnCount> GetSTModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.SourceStructure.MTourOnCount> list = new List<Model.SourceStructure.MTourOnCount>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append("         TourCode ,");
            field.Append("         TourId ,");
            field.Append("         RouteName ,");
            field.Append("         RouteId ,");
            field.Append("         LDate ,");
            field.Append("         TourDays ,");
            field.Append("         XMLTourOrder = ( SELECT ISNULL(SUM(Adults), 0) Adults ,");
            field.Append("                                 ISNULL(SUM(Childs), 0) Childs ,");
            field.Append("                                 ISNULL(SUM(Others), 0) Others ,");
            field.Append("                                 ISNULL(SUM(ConfirmMoney), 0) ConfirmMoney");
            field.Append("                          FROM   dbo.tbl_TourOrder");
            field.Append("                          WHERE  TourId = dbo.tbl_Tour.TourId");
            field.Append("                                 AND IsDelete = '0'");
            field.AppendFormat("                                 AND Status = {0}", (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交);
            field.Append("                        FOR");
            field.Append("                          XML RAW ,");
            field.Append("                              ROOT");
            field.Append("                        ) ,");
            field.Append("         Confirmation = ( SELECT ISNULL(SUM(Confirmation), 0)");
            field.Append("                          FROM   tbl_plan");
            field.Append("                          WHERE  tourid = tbl_tour.tourid");
            field.Append("                                 AND isdelete = '0'");
            field.AppendFormat("                                 AND Status = {0}", (int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实);
            field.Append("                        ) ,");
            field.Append("         IssueTime ,");
            field.Append("         CompanyId");

            query.AppendFormat("         RouteId = '{0}'", routeId);
            query.AppendFormat("         AND CompanyId = '{0}'", companyId);
            query.Append("         AND IsDelete = '0'");
            query.Append("         AND ISNULL(ParentId, '') <> ''");
            query.AppendFormat("         AND TourStatus <> {0}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消);

            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_Tour", "TourId", field.ToString(), query.ToString(), "  IssueTime desc"))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MTourOnCount stTourModel = new EyouSoft.Model.SourceStructure.MTourOnCount();
                    stTourModel.DayCount = (int)reader["TourDays"];
                    stTourModel.IncomeMoney = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(reader["XMLTourOrder"].ToString(), "ConfirmMoney"));
                    stTourModel.PayMoney = reader.IsDBNull(reader.GetOrdinal("Confirmation")) ? 0 : (decimal)reader["Confirmation"];
                    Model.SourceStructure.MPeopleCountModel peopleCountModel = new EyouSoft.Model.SourceStructure.MPeopleCountModel();
                    peopleCountModel.AdultCount = Utils.GetInt(Utils.GetValueFromXmlByAttribute(reader["XMLTourOrder"].ToString(), "Adults"));
                    peopleCountModel.ChildrenCount = Utils.GetInt(Utils.GetValueFromXmlByAttribute(reader["XMLTourOrder"].ToString(), "Childs"));
                    peopleCountModel.OtherCount = Utils.GetInt(Utils.GetValueFromXmlByAttribute(reader["XMLTourOrder"].ToString(), "Others"));
                    stTourModel.PeopleCount = peopleCountModel;
                    if (!reader.IsDBNull(reader.GetOrdinal("LDate")))
                    {
                        stTourModel.StartOutDate = (DateTime)reader["LDate"];
                    }
                    stTourModel.TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty;
                    stTourModel.RouteName = !reader.IsDBNull(reader.GetOrdinal("RouteName")) ? (string)reader["RouteName"] : string.Empty;
                    stTourModel.RouteId = !reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (string)reader["RouteId"] : string.Empty;
                    stTourModel.TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty;
                    stTourModel.MaoriMoney = stTourModel.IncomeMoney - stTourModel.PayMoney;
                    list.Add(stTourModel);
                }
            }

            return list;
        }

        /// <summary>
        /// 获得收客列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MAcceptGuestModel> GetSKModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<Model.SourceStructure.MAcceptGuestModel> list = new List<Model.SourceStructure.MAcceptGuestModel>();
            StringBuilder sbWhereSql = new StringBuilder();
            sbWhereSql.AppendFormat(" CompanyId='{0}' ", companyId);
            sbWhereSql.AppendFormat(" and RouteId='{0}' ", routeId);
            using (IDataReader reader = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "view_PlanTourOrderSK", "Id", "*", sbWhereSql.ToString(), "  IssueTime desc"))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MAcceptGuestModel skTourModel = new EyouSoft.Model.SourceStructure.MAcceptGuestModel();
                    skTourModel.ApplyDate = (DateTime)reader["IssueTime"];
                    skTourModel.OrderCode = !reader.IsDBNull(reader.GetOrdinal("OrderCode")) ? (string)reader["OrderCode"] : string.Empty;
                    Model.ComStructure.MComUser sellerModel = userDal.GetModel((!reader.IsDBNull(reader.GetOrdinal("SellerId")) ? (string)reader["SellerId"] : string.Empty), companyId);

                    skTourModel.SellerModel = sellerModel;
                    Model.SourceStructure.MPeopleCountModel peopleCountModel = new EyouSoft.Model.SourceStructure.MPeopleCountModel();
                    peopleCountModel.AdultCount = (int)reader["Adults"];
                    peopleCountModel.ChildrenCount = (int)reader["Childs"];
                    peopleCountModel.OtherCount = (int)reader["Others"];
                    skTourModel.PeopleCount = peopleCountModel;
                    skTourModel.OrderId = !reader.IsDBNull(reader.GetOrdinal("OrderId")) ? (string)reader["OrderId"] : string.Empty;
                    skTourModel.TourId = !reader.IsDBNull(reader.GetOrdinal("TourId")) ? (string)reader["TourId"] : string.Empty;
                    skTourModel.TourCode = !reader.IsDBNull(reader.GetOrdinal("TourCode")) ? (string)reader["TourCode"] : string.Empty;
                    skTourModel.BuyCompanyId = reader["BuyCompanyId"].ToString();
                    skTourModel.BuyCompanyName = reader["BuyCompanyName"].ToString();
                    skTourModel.ContactName = reader["ContactName"].ToString();
                    skTourModel.ContactTel = reader["ContactTel"].ToString();
                    list.Add(skTourModel);
                }
            }

            return list;
        }
        #endregion

        #endregion

        #region 私有方法
        #region 供应商
        /// <summary>
        /// 创建供应商Xml语句
        /// </summary>
        /// <param name="model">供应商实体</param>
        /// <returns></returns>
        private string GetSourceXmlString(Model.SourceStructure.MSource model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }


        #endregion

        #region 酒店
        /// <summary>
        /// 获得酒店Xml语句
        /// </summary>
        /// <param name="model">酒店对象</param>
        /// <returns></returns>
        private string GetHotelXmlString(Model.SourceStructure.MSourceHotel model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得酒店房型Xml语句
        /// </summary>
        /// <param name="list">房型对象列表</param>
        /// <returns></returns>
        private string GetRoomXmlString(IList<Model.SourceStructure.MSourceHotelRoom> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得酒店房型列表
        /// </summary>
        /// <param name="sourceId">酒店编号</param>
        /// <returns>酒店房型列表</returns>
        public IList<Model.SourceStructure.MSourceHotelRoom> GetRoomModelList(string sourceId)
        {
            IList<Model.SourceStructure.MSourceHotelRoom> list = new List<Model.SourceStructure.MSourceHotelRoom>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_HotelRoom);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, sourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceHotelRoom roomModel = new EyouSoft.Model.SourceStructure.MSourceHotelRoom();
                    roomModel.IsBreakfast = !reader.IsDBNull(reader.GetOrdinal("IsBreakfast")) ? reader["IsBreakfast"].ToString() == "0" ? false : true : false;
                    roomModel.PriceDJ = decimal.Parse(reader["PriceDJ"].ToString());
                    roomModel.PricePJ = decimal.Parse(reader["PricePJ"].ToString());
                    roomModel.PriceQT = decimal.Parse(reader["PriceQT"].ToString());
                    roomModel.PriceSK = decimal.Parse(reader["PriceSK"].ToString());
                    roomModel.PriceWJ = decimal.Parse(reader["PriceWJ"].ToString());
                    roomModel.PriceWL = decimal.Parse(reader["PriceWL"].ToString());
                    roomModel.RoomId = !reader.IsDBNull(reader.GetOrdinal("RoomId")) ? (string)reader["RoomId"] : string.Empty;
                    roomModel.SourceId = sourceId;
                    roomModel.TypeName = !reader.IsDBNull(reader.GetOrdinal("TypeName")) ? (string)reader["TypeName"] : string.Empty;
                    list.Add(roomModel);
                }
            }
            return list;
        }
        #endregion

        #region 景点
        /// <summary>
        /// 获得景点Xml语句
        /// </summary>
        /// <param name="model">景点对象</param>
        /// <returns></returns>
        private string GetSpotXmlString(Model.SourceStructure.MSourceSpot model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得景点价格体系xml语句
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetSpotPriceSystemXmlString(IList<Model.SourceStructure.MSpotPriceSystemModel> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得景点价格体系列表
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MSpotPriceSystemModel> GetSpotPriceSystemList(string sourceId)
        {
            DbCommand dc = _db.GetSqlStringCommand(string.Format("select * from tbl_SourceSpotPriceSystem where sourceId='{0}' order by SortId asc ", sourceId));
            IList<Model.SourceStructure.MSpotPriceSystemModel> list = new List<Model.SourceStructure.MSpotPriceSystemModel>();
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    list.Add(new MSpotPriceSystemModel()
                        {
                            GuideWord = !reader.IsDBNull(reader.GetOrdinal("GuideWord")) ? (string)reader["GuideWord"] : string.Empty,
                            SpotName = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty,
                            PriceGP = !reader.IsDBNull(reader.GetOrdinal("PriceGP")) ? (decimal)reader["PriceGP"] : 0m,
                            PriceJR = !reader.IsDBNull(reader.GetOrdinal("PriceJR")) ? (decimal)reader["PriceJR"] : 0m,
                            PriceLR1 = !reader.IsDBNull(reader.GetOrdinal("PriceLR1")) ? (decimal)reader["PriceLR1"] : 0m,
                            PriceLR2 = !reader.IsDBNull(reader.GetOrdinal("PriceLR2")) ? (decimal)reader["PriceLR2"] : 0m,
                            PriceRT = !reader.IsDBNull(reader.GetOrdinal("PriceRT")) ? (decimal)reader["PriceRT"] : 0m,
                            PriceSK = !reader.IsDBNull(reader.GetOrdinal("PriceSK")) ? (decimal)reader["PriceSK"] : 0m,
                            PriceTD = !reader.IsDBNull(reader.GetOrdinal("PriceTD")) ? (decimal)reader["PriceTD"] : 0m,
                            PriceXS = !reader.IsDBNull(reader.GetOrdinal("PriceXS")) ? (decimal)reader["PriceXS"] : 0m,
                            Id = (string)reader["Id"],
                            SourceId = sourceId,
                            Star = (Model.EnumType.SourceStructure.SpotStar)reader.GetByte(reader.GetOrdinal("Star")),
                        });
                }
            }
            return list;
        }
        #endregion

        #region 游轮
        /// <summary>
        /// 获得游轮Xml语句
        /// </summary>
        /// <param name="model">游轮对象</param>
        /// <returns></returns>
        private string GetShipXmlString(Model.SourceStructure.MSourceShip model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得游轮旗下游轮xml语句
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetShipSubShipXmlString(IList<Model.SourceStructure.MSourceSubShip> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得游轮旗下游轮集合
        /// </summary>
        /// <param name="sourceId">游轮编号</param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MSourceSubShip> GetShipSubShip(string sourceId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from tbl_SourceSubShip where sourceid='{0}' order by SortId asc", sourceId);
            IList<Model.SourceStructure.MSourceSubShip> list = new List<Model.SourceStructure.MSourceSubShip>();
            DbCommand dc = _db.GetSqlStringCommand(sb.ToString());
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceSubShip model = new Model.SourceStructure.MSourceSubShip();
                    model.ContactName = !reader.IsDBNull(reader.GetOrdinal("ContactName")) ? (string)reader["ContactName"] : string.Empty;
                    model.ShipName = !reader.IsDBNull(reader.GetOrdinal("ShipName")) ? (string)reader["ShipName"] : string.Empty;
                    model.ShipRoute = !reader.IsDBNull(reader.GetOrdinal("ShipRoute")) ? (string)reader["ShipRoute"] : string.Empty;
                    model.ShipSpace = (int)reader["ShipSpace"];
                    model.ShipStar = (Model.EnumType.SourceStructure.ShipStar)reader.GetByte(reader.GetOrdinal("ShipStar"));
                    model.SourceId = sourceId;
                    model.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? (string)reader["Telephone"] : string.Empty;
                    model.SubId = !reader.IsDBNull(reader.GetOrdinal("SubId")) ? (string)reader["SubId"] : string.Empty;
                    model.AttachModel = attachDal.GetModel(model.SubId, Model.EnumType.ComStructure.AttachItemType.游轮旗下游轮图片);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取游轮旗下游轮图片附件xml语句
        /// </summary>
        /// <param name="list">游轮旗下游轮集合</param>
        /// <returns></returns>
        private string GetSubShipAttachXmlString(IList<Model.SourceStructure.MSourceSubShip> list)
        {
            IList<Model.ComStructure.MComAttach> attachList = new List<Model.ComStructure.MComAttach>();
            for (int i = 0; i < list.Count; i++)
            {
                attachList.Add(list[i].AttachModel);
            }
            List<object> l = new List<object>();
            for (int i = 0; i < attachList.Count; i++)
            {
                l.Add(attachList[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }
        #endregion

        #region 购物
        /// <summary>
        /// 获得购物点Xml语句
        /// </summary>
        /// <param name="model">购物点对象</param>
        /// <returns></returns>
        private string GetShopXmlString(Model.SourceStructure.MSourceShop model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        #endregion

        #region 车辆
        /// <summary>
        /// 获得车辆Xml语句
        /// </summary>
        /// <param name="list">车辆对象列表</param>
        /// <returns></returns>
        private string GetCarXmlString(IList<Model.SourceStructure.MSourceCar> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得车辆列表信息
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MSourceCar> GetCarModelList(string sourceId)
        {
            IList<Model.SourceStructure.MSourceCar> list = new List<Model.SourceStructure.MSourceCar>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_Car);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, sourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceCar carModel = new EyouSoft.Model.SourceStructure.MSourceCar();
                    carModel.CarId = !reader.IsDBNull(reader.GetOrdinal("CarId")) ? (string)reader["CarId"] : string.Empty;
                    carModel.CarNumber = !reader.IsDBNull(reader.GetOrdinal("CarNumber")) ? (string)reader["CarNumber"] : string.Empty;
                    carModel.Driver = !reader.IsDBNull(reader.GetOrdinal("Driver")) ? (string)reader["Driver"] : string.Empty; reader["Driver"].ToString();
                    carModel.DriverTel = !reader.IsDBNull(reader.GetOrdinal("DriverTel")) ? (string)reader["DriverTel"] : string.Empty;
                    carModel.InsuranceAmount = (decimal?)reader["InsuranceAmount"];
                    carModel.InsuranceLimit = (DateTime)reader["InsuranceLimit"];
                    carModel.IsInsurance = !reader.IsDBNull(reader.GetOrdinal("IsInsurance")) ? reader["IsInsurance"].ToString() == "0" ? false : true : false;
                    carModel.SeatNumber = (int)reader["SeatNumber"];
                    carModel.SourceId = sourceId;
                    carModel.TypeName = !reader.IsDBNull(reader.GetOrdinal("TypeName")) ? (string)reader["TypeName"] : string.Empty;
                    list.Add(carModel);
                }
            }
            return list;
        }
        #endregion

        #region 餐馆
        /// <summary>
        /// 获得餐馆Xml语句
        /// </summary>
        /// <param name="model">餐馆对象</param>
        /// <returns></returns>
        private string GetDiningXmlString(Model.SourceStructure.MSourceDining model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得菜系Xml语句
        /// </summary>
        /// <param name="list">菜系列表</param>
        /// <returns></returns>
        private string GetCuisineXmlString(IList<Model.SourceStructure.MSourceDiningCuisine> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得餐馆菜系列表
        /// </summary>
        /// <param name="sourceId">餐馆编号</param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MSourceDiningCuisine> GetDiningCuisineModelList(string sourceId)
        {
            IList<Model.SourceStructure.MSourceDiningCuisine> list = new List<Model.SourceStructure.MSourceDiningCuisine>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_DiningCuisine);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, sourceId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceDiningCuisine cuisineModel = new EyouSoft.Model.SourceStructure.MSourceDiningCuisine();
                    cuisineModel.Cuisine = (EyouSoft.Model.EnumType.SourceStructure.SourceCuisine)(int.Parse(reader["Cuisine"].ToString()));
                    cuisineModel.SourceId = reader["SourceId"].ToString();
                    list.Add(cuisineModel);
                }
            }
            return list;
        }
        #endregion

        #region 导游
        /// <summary>
        /// 获得导游Xml语句
        /// </summary>
        /// <param name="model">导游对象</param>
        /// <returns></returns>
        private string GetGuideXmlString(Model.SourceStructure.MSourceGuide model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 创建导游类别XML
        /// </summary>
        /// <param name="Lists">导游类别集合</param>
        /// <returns></returns>
        private string CreateSourceGuideCategoryListXML(IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.SourceStructure.MSourceGuideCategory model in list)
            {
                StrBuild.AppendFormat("<SourceGuideCategory GuideId=\"{0}\"", model.GuideId);
                StrBuild.AppendFormat(" GuideCategory=\"{0}\" />", (int)model.GuideCategory);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 生成导游类别集合List
        /// </summary>
        /// <param name="GuideCategoryXML">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory> GetGuideCategoryList(string GuideCategoryXML)
        {
            if (string.IsNullOrEmpty(GuideCategoryXML)) return null;
            IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory> ResultList = null;
            ResultList = new List<EyouSoft.Model.SourceStructure.MSourceGuideCategory>();
            XElement root = XElement.Parse(GuideCategoryXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.SourceStructure.MSourceGuideCategory model = new EyouSoft.Model.SourceStructure.MSourceGuideCategory()
                {
                    GuideId = tmp1.Attribute("GuideId").Value,
                    GuideCategory = (EyouSoft.Model.EnumType.SourceStructure.GuideCategory)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory), tmp1.Attribute("Category").Value)
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成导游安排类型集合List
        /// </summary>
        /// <param name="TypeXML">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SourceStructure.MGuidePlanWorkType> GetTypeList(string TypeXML)
        {
            if (string.IsNullOrEmpty(TypeXML)) return null;
            IList<EyouSoft.Model.SourceStructure.MGuidePlanWorkType> ResultList = null;
            ResultList = new List<EyouSoft.Model.SourceStructure.MGuidePlanWorkType>();
            XElement root = XElement.Parse(TypeXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                if (tmp1.Attribute("StartDate") != null && tmp1.Attribute("EndDate") != null && tmp1.Attribute("StartDate").Value.Trim() != "" && tmp1.Attribute("EndDate").Value.Trim() != "")
                {
                    EyouSoft.Model.SourceStructure.MGuidePlanWorkType model = new EyouSoft.Model.SourceStructure.MGuidePlanWorkType();
                    model.OnTime = Convert.ToDateTime(tmp1.Attribute("StartDate").Value);
                    model.NextTime = Convert.ToDateTime(tmp1.Attribute("EndDate").Value);
                    model.Type = tmp1.Attribute("Type").Value;
                    ResultList.Add(model);
                    model = null;
                }
            }
            return ResultList;
        }

        #endregion

        #region 线路
        /// <summary>
        /// 获得线路Xml语句
        /// </summary>
        /// <param name="model">线路对象</param>
        /// <returns></returns>
        private string GetRouteXmlString(Model.SourceStructure.MRoute model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得线路服务Xml语句
        /// </summary>
        /// <param name="model">线路服务列表</param>
        /// <returns></returns>
        private string GetRouteServicesXmlString(Model.TourStructure.MTourService model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得线路行程Xml语句
        /// </summary>
        /// <param name="list">线路行程对象列表</param>
        /// <returns></returns>
        private string GetRoutePlanXmlString(IList<Model.TourStructure.MPlanBaseInfo> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得分项项目Xml语句
        /// </summary>
        /// <param name="list">线路分项项目对象列表</param>
        /// <returns></returns>
        private string GetRouteStandardXmlString(IList<Model.SourceStructure.MRouteStandard> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得线路行程景点Xml语句
        /// </summary>
        /// <param name="list">行程计划列表</param>
        /// <returns></returns>
        private string GetPlanSpotXmlString(IList<Model.TourStructure.MPlanBaseInfo> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].TourPlanSpot.Count; j++)
                {
                    l.Add(list[i].TourPlanSpot[j]);
                }
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得线路服务Model
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        private Model.TourStructure.MTourService GetRouteServiceModel(string routeId)
        {
            Model.TourStructure.MTourService servicesModel = new EyouSoft.Model.TourStructure.MTourService();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectRouteServices);
            _db.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    servicesModel.OwnExpense = !reader.IsDBNull(reader.GetOrdinal("Chargeable")) ? (string)reader["Chargeable"] : string.Empty;
                    servicesModel.ChildServiceItem = !reader.IsDBNull(reader.GetOrdinal("Children")) ? (string)reader["Children"] : string.Empty;
                    servicesModel.NoNeedItem = !reader.IsDBNull(reader.GetOrdinal("Excluding")) ? (string)reader["Excluding"] : string.Empty;
                    servicesModel.InsiderInfor = !reader.IsDBNull(reader.GetOrdinal("Internal")) ? (string)reader["Internal"] : string.Empty;
                    servicesModel.NeedAttention = !reader.IsDBNull(reader.GetOrdinal("Note")) ? (string)reader["Note"] : string.Empty;
                    servicesModel.WarmRemind = !reader.IsDBNull(reader.GetOrdinal("WarmPrompt")) ? (string)reader["WarmPrompt"] : string.Empty;
                    servicesModel.ShoppingItem = !reader.IsDBNull(reader.GetOrdinal("Shopping")) ? (string)reader["Shopping"] : string.Empty;
                    servicesModel.ItemId = routeId;
                }
            }
            return servicesModel;
        }

        /// <summary>
        /// 获得线路包含项目列表
        /// </summary>
        /// <param name="routeId">线路Model</param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MRouteStandard> GetRouteStandardModelList(string routeId)
        {
            IList<Model.SourceStructure.MRouteStandard> list = new List<Model.SourceStructure.MRouteStandard>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectRouteStandard);
            _db.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MRouteStandard standardModel = new EyouSoft.Model.SourceStructure.MRouteStandard();
                    standardModel.RouteId = routeId;
                    standardModel.Standard = !reader.IsDBNull(reader.GetOrdinal("Standard")) ? (string)reader["Standard"] : string.Empty;
                    standardModel.Type = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)(int.Parse(reader["Type"].ToString()));
                    standardModel.Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)(int.Parse(reader["Unit"].ToString()));
                    standardModel.UnitPrice = (decimal)reader["UnitPrice"];
                    standardModel.StandardId = !reader.IsDBNull(reader.GetOrdinal("StandardId")) ? (string)reader["StandardId"] : string.Empty;
                    list.Add(standardModel);
                }
            }
            return list;
        }


        /// <summary>
        /// 获得线路行程计划列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        private IList<Model.TourStructure.MPlanBaseInfo> GetRoutePlanModelList(string routeId)
        {
            IList<Model.TourStructure.MPlanBaseInfo> list = new List<Model.TourStructure.MPlanBaseInfo>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectRoutePlan);
            _db.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.TourStructure.MPlanBaseInfo planModel = new EyouSoft.Model.TourStructure.MPlanBaseInfo();
                    planModel.ItemId = routeId;
                    planModel.Breakfast = !reader.IsDBNull(reader.GetOrdinal("Breakfast")) ? reader["Breakfast"].ToString() == "0" ? false : true : false;
                    planModel.Content = !reader.IsDBNull(reader.GetOrdinal("Content")) ? (string)reader["Content"] : string.Empty;
                    planModel.Days = (int)reader["Days"];
                    planModel.HotelId = !reader.IsDBNull(reader.GetOrdinal("HotelId")) ? (string)reader["HotelId"] : string.Empty;
                    planModel.Hotel = !reader.IsDBNull(reader.GetOrdinal("Hotel")) ? (string)reader["Hotel"] : string.Empty;
                    planModel.Lunch = !reader.IsDBNull(reader.GetOrdinal("Lunch")) ? reader["Lunch"].ToString() == "0" ? false : true : false;
                    //planModel.Pic = !reader.IsDBNull(reader.GetOrdinal("Pic")) ? (string)reader["Pic"] : string.Empty;
                    //planModel.PhotoAttachList = attachDal.GetModelList(planModel.PlanId, Model.EnumType.ComStructure.AttachItemType.线路行程图片);
                    planModel.Traffic = !reader.IsDBNull(reader.GetOrdinal("Traffic")) ? (string)reader["Traffic"] : string.Empty;
                    planModel.PlanId = !reader.IsDBNull(reader.GetOrdinal("PlanId")) ? (string)reader["PlanId"] : string.Empty;
                    var mdl = attachDal.GetModel(planModel.PlanId, Model.EnumType.ComStructure.AttachItemType.线路行程图片);
                    planModel.FilePath = mdl == null ? string.Empty : mdl.FilePath;
                    planModel.Section = !reader.IsDBNull(reader.GetOrdinal("Section")) ? (string)reader["Section"] : string.Empty;
                    planModel.Supper = !reader.IsDBNull(reader.GetOrdinal("Supper")) ? reader["Supper"].ToString() == "0" ? false : true : false;
                    planModel.TourPlanSpot = GetRoutePlanSpotModelList(planModel.PlanId);
                    list.Add(planModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取线路行程图片xml语句
        /// </summary>
        /// <param name="planList">线路行程集合</param>
        /// <returns></returns>
        private string GetRoutePlanPhotoXmlString(IList<Model.TourStructure.MPlanBaseInfo> planList)
        {
            IList<Model.ComStructure.MComAttach> list = new List<Model.ComStructure.MComAttach>();
            for (int i = 0; i < planList.Count; i++)
            {
                Model.ComStructure.MComAttach model = new Model.ComStructure.MComAttach();
                model.Downloads = 0;
                if (!string.IsNullOrEmpty(planList[i].FilePath))
                {
                    if (planList[i].FilePath.Contains('|'))
                    {
                        model.FilePath = planList[i].FilePath.Split('|')[1];
                        model.Name = planList[i].FilePath.Split('|')[0];
                    }
                    else
                    {
                        model.FilePath = planList[i].FilePath;
                        model.Name = System.IO.Path.GetFileName(model.FilePath);
                    }
                }

                model.ItemId = planList[i].PlanId;
                model.ItemType = Model.EnumType.ComStructure.AttachItemType.线路行程图片;
                model.Size = 0;
                list.Add(model);
            }

            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }



        /// <summary>
        /// 获得线路行程景点列表
        /// </summary>
        /// <param name="planId">线路行程计划编号</param>
        /// <returns></returns>
        private IList<Model.TourStructure.MTourPlanSpot> GetRoutePlanSpotModelList(string planId)
        {
            IList<Model.TourStructure.MTourPlanSpot> list = new List<Model.TourStructure.MTourPlanSpot>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectRoutePlanSpot);
            _db.AddInParameter(dc, "PlanId", DbType.AnsiStringFixedLength, planId);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.TourStructure.MTourPlanSpot planSpotModel = new EyouSoft.Model.TourStructure.MTourPlanSpot();
                    planSpotModel.PlanId = planId;
                    planSpotModel.SpotId = !reader.IsDBNull(reader.GetOrdinal("SpotId")) ? (string)reader["SpotId"] : string.Empty;
                    planSpotModel.SpotName = !reader.IsDBNull(reader.GetOrdinal("SpotName")) ? (string)reader["SpotName"] : string.Empty;
                    list.Add(planSpotModel);
                }
            }
            return list;
        }

        #endregion

        #region 供应商

        /// <summary>
        /// 添加供应商(票务,保险,其他)
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <param name="list">联系人列表</param>
        /// <returns>返回值 -1:供应商名称存在 0:事务回滚 1:正确</returns>
        private int AddSourceModel(Model.SourceStructure.MSource model, IList<Model.CrmStructure.MCrmLinkman> list)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_AddTicket");
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "xmlSourceString", DbType.Xml, GetSourceXmlString(model));
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.ContractAttach));
            if (model.Type == Model.EnumType.SourceStructure.SourceType.票务)
            {
                Model.ComStructure.MComAttach xyAttach = new Model.ComStructure.MComAttach();
                xyAttach.FilePath = model.AgreementFile;
                xyAttach.Name = System.IO.Path.GetFileName(xyAttach.FilePath);
                xyAttach.ItemId = model.SourceId;
                xyAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合作协议;
                _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, GetAttachXmlString(xyAttach));
            }
            else
            {
                _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, "Null");
            }
            _db.AddInParameter(dc, "xmlLinkmanString", DbType.Xml, linmanDal.GetLinkManXmlString(list));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 修改供应商公有信息(票务,保险,其他)
        /// </summary>
        /// <param name="model">供应商公有信息</param>
        /// <param name="list">联系人列表</param>
        /// <returns>返回值 -1:供应商名称存在 0:事务回滚 1:正确</returns>
        private int UpdateSourceModel(Model.SourceStructure.MSource model, IList<Model.CrmStructure.MCrmLinkman> list)
        {
            DbCommand dc = _db.GetStoredProcCommand(proc_Source_UpdateSource);
            _db.AddOutParameter(dc, "result", DbType.Int32, 4);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, model.SourceId);
            _db.AddInParameter(dc, "Name", DbType.String, model.Name);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "CountryId", DbType.Int32, model.CountryId);
            _db.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            _db.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            _db.AddInParameter(dc, "CountyId", DbType.Int32, model.CountyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, model.Type);
            _db.AddInParameter(dc, "Address", DbType.String, model.Address);
            _db.AddInParameter(dc, "IsCommission", DbType.AnsiStringFixedLength, model.IsCommission ? "1" : "0");
            _db.AddInParameter(dc, "AgreementFile", DbType.String, model.AgreementFile);
            _db.AddInParameter(dc, "TradeNum", DbType.Int32, model.TradeNum);
            _db.AddInParameter(dc, "UnitPolicy", DbType.String, model.UnitPolicy);
            _db.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            _db.AddInParameter(dc, "Desc", DbType.String, model.Desc);
            _db.AddInParameter(dc, "LicenseKey", DbType.String, model.LicenseKey);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserId);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);

            _db.AddInParameter(dc, "IsPermission", DbType.AnsiStringFixedLength, model.IsPermission ? "1" : "0");
            _db.AddInParameter(dc, "IsRecommend", DbType.AnsiStringFixedLength, model.IsRecommend ? "1" : "0");
            _db.AddInParameter(dc, "LastModifierId", DbType.AnsiStringFixedLength, model.LastModifierId);
            _db.AddInParameter(dc, "LastModifyTime", DbType.DateTime, model.LastModifyTime);
            _db.AddInParameter(dc, "IsSignContract", DbType.AnsiStringFixedLength, model.IsSignContract ? "1" : "0");
            _db.AddInParameter(dc, "ContractCode", DbType.AnsiStringFixedLength, model.ContractCode);
            _db.AddInParameter(dc, "ContractPeriodStart", DbType.DateTime, model.ContractPeriodStart);
            _db.AddInParameter(dc, "ContractPeriodEnd", DbType.DateTime, model.ContractPeriodEnd);
            _db.AddInParameter(dc, "DeptId", DbType.Int32, model.DeptId);
            _db.AddInParameter(dc, "CAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合同附件);

            if (model.Type == Model.EnumType.SourceStructure.SourceType.票务)
            {
                _db.AddInParameter(dc, "XAttachType", DbType.Byte, (int)Model.EnumType.ComStructure.AttachItemType.供应商合作协议);

                Model.ComStructure.MComAttach xyAttach = new Model.ComStructure.MComAttach();
                xyAttach.FilePath = model.AgreementFile;
                xyAttach.Name = System.IO.Path.GetFileName(xyAttach.FilePath);
                xyAttach.ItemId = model.SourceId;
                xyAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合作协议;
                _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, GetAttachXmlString(xyAttach));
            }
            else
            {
                _db.AddInParameter(dc, "XAttachType", DbType.Byte, 0);
                _db.AddInParameter(dc, "xmlXAttachString", DbType.Xml, "Null");
            }
            _db.AddInParameter(dc, "xmlAttachString", DbType.Xml, GetAttachXmlString(model.ContractAttach));
            _db.AddInParameter(dc, "xmlLinkman", DbType.Xml, linmanDal.GetLinkManXmlString(list));
            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(dc, _db);
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
                return Convert.ToInt32(_db.GetParameterValue(dc, "result"));
            }
        }

        /// <summary>
        /// 获得供应商信息
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns>供应商Model</returns>
        private Model.SourceStructure.MSource GetSourceModel(string SourceId)
        {
            Model.SourceStructure.MSource sourceModel = new EyouSoft.Model.SourceStructure.MSource();
            DbCommand dc = _db.GetSqlStringCommand(sql_Source_SelectSource);
            _db.AddInParameter(dc, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            using (IDataReader reader = DbHelper.RunReaderProcedure(dc, _db))
            {
                while (reader.Read())
                {
                    sourceModel.Address = !reader.IsDBNull(reader.GetOrdinal("Address")) ? (string)reader["Address"] : string.Empty;
                    sourceModel.AgreementFile = !reader.IsDBNull(reader.GetOrdinal("AgreementFile")) ? (string)reader["AgreementFile"] : string.Empty;
                    sourceModel.CityId = int.Parse(reader["CityId"].ToString());
                    sourceModel.CompanyId = reader["CompanyId"].ToString();
                    sourceModel.CountryId = int.Parse(reader["CountryId"].ToString());
                    sourceModel.CountyId = int.Parse(reader["CountyId"].ToString());
                    sourceModel.Desc = !reader.IsDBNull(reader.GetOrdinal("Desc")) ? (string)reader["Desc"] : string.Empty;
                    sourceModel.IsPermission = !reader.IsDBNull(reader.GetOrdinal("IsPermission")) ? reader["IsPermission"].ToString() == "0" ? false : true : false;
                    sourceModel.IsDelete = !reader.IsDBNull(reader.GetOrdinal("IsDelete")) ? reader["IsDelete"].ToString() == "0" ? false : true : false;
                    sourceModel.IssueTime = DateTime.Parse(reader["IssueTime"].ToString());
                    sourceModel.LicenseKey = !reader.IsDBNull(reader.GetOrdinal("LicenseKey")) ? (string)reader["LicenseKey"] : string.Empty;
                    sourceModel.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    sourceModel.OperatorId = !reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? (string)reader["OperatorId"] : string.Empty;
                    sourceModel.ProvinceId = int.Parse(reader["ProvinceId"].ToString());

                    sourceModel.Remark = !reader.IsDBNull(reader.GetOrdinal("Remark")) ? (string)reader["Remark"] : string.Empty;
                    sourceModel.SourceId = !reader.IsDBNull(reader.GetOrdinal("SourceId")) ? (string)reader["SourceId"] : string.Empty;
                    sourceModel.TradeNum = int.Parse(reader["TradeNum"].ToString());
                    sourceModel.Type = (EyouSoft.Model.EnumType.SourceStructure.SourceType)(int.Parse((reader["Type"].ToString())));
                    sourceModel.UnitPolicy = !reader.IsDBNull(reader.GetOrdinal("UnitPolicy")) ? (string)reader["UnitPolicy"] : string.Empty;
                    sourceModel.IsRecommend = !reader.IsDBNull(reader.GetOrdinal("IsRecommend")) ? reader["IsRecommend"].ToString() == "0" ? false : true : false;
                    sourceModel.IsCommission = !reader.IsDBNull(reader.GetOrdinal("IsCommission")) ? reader["IsCommission"].ToString() == "0" ? false : true : false;
                    sourceModel.IsBackSingle = !reader.IsDBNull(reader.GetOrdinal("IsBackSingle")) ? reader["IsBackSingle"].ToString() == "0" ? false : true : false;
                    sourceModel.LastModifierId = !reader.IsDBNull(reader.GetOrdinal("LastModifierId")) ? (string)reader["LastModifierId"] : string.Empty;
                    sourceModel.LastModifyTime = !reader.IsDBNull(reader.GetOrdinal("LastModifyTime")) ? (DateTime?)reader["LastModifyTime"] : null;
                    sourceModel.IsSignContract = !reader.IsDBNull(reader.GetOrdinal("IsSignContract")) ? reader["IsSignContract"].ToString() == "0" ? false : true : false;
                    sourceModel.ContractCode = !reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? (string)reader["ContractCode"] : string.Empty;
                    sourceModel.ContractPeriodStart = !reader.IsDBNull(reader.GetOrdinal("ContractPeriodStart")) ? (DateTime?)reader["ContractPeriodStart"] : null;
                    sourceModel.ContractPeriodEnd = !reader.IsDBNull(reader.GetOrdinal("ContractPeriodEnd")) ? (DateTime?)reader["ContractPeriodEnd"] : null;
                    sourceModel.UserId = !reader.IsDBNull(reader.GetOrdinal("UserId")) ? (string)reader["UserId"] : string.Empty;
                    sourceModel.DeptId = !reader.IsDBNull(reader.GetOrdinal("DeptId")) ? (int)reader["DeptId"] : 0;
                }
            }
            sourceModel.ContractAttach = attachDal.GetModel(SourceId, Model.EnumType.ComStructure.AttachItemType.供应商合同附件);
            return sourceModel;
        }
        #endregion

        #region 地接社
        /// <summary>
        /// 获得地接社线路区域xml语句
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetTravelRouteAreaXmlString(IList<Model.SourceStructure.MSourceTravelRouteArea> list)
        {
            List<object> l = new List<object>();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    l.Add(list[i]);
                }
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得供应商地接社xml语句
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetSourceTravelXmlString(Model.SourceStructure.MSourceTravelInfo model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }

        /// <summary>
        /// 获得供应商地接社Model
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        private Model.SourceStructure.MSourceTravelInfo GetSourceTravelInfoModel(string sourceId)
        {
            Model.SourceStructure.MSourceTravelInfo model = new Model.SourceStructure.MSourceTravelInfo();
            string sql = string.Format("select * from tbl_SourceTravel where sourceId='{0}'", sourceId);
            DbCommand dc = _db.GetSqlStringCommand(sql);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    model.SourceId = sourceId;
                    model.LegalRepresentative = !reader.IsDBNull(reader.GetOrdinal("LegalRepresentative")) ? (string)reader["LegalRepresentative"] : string.Empty;
                    model.Routes = !reader.IsDBNull(reader.GetOrdinal("Routes")) ? (string)reader["Routes"] : string.Empty;
                    model.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? (string)reader["Telephone"] : string.Empty;

                }
            }
            return model;
        }

        /// <summary>
        /// 根据团队编号获取计调已落实的导游列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MSourceGuide> GetGuideLst(string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                return null;
            }
            var lst = new List<MSourceGuide>();
            var sb = new StringBuilder();

            sb.AppendFormat("select (select SourceId GuideId,SourceName GuideName from tbl_Plan where Type={0} and TourId='{1}' and IsDelete=0 and status={2} for xml path,elements,root('item')) Guide", (int)PlanProject.导游, tourId, (int)PlanState.已落实);

            var gDc = _db.GetSqlStringCommand(sb.ToString());

            using (var gReader = DbHelper.ExecuteReader(gDc, _db))
            {
                while (gReader.Read())
                {
                    var xd = new XmlDocument();
                    if (gReader.IsDBNull(gReader.GetOrdinal("Guide")))
                    {
                        continue;
                    }
                    xd.LoadXml((string)gReader["Guide"]);
                    if (xd.DocumentElement == null)
                    {
                        continue;
                    }
                    for (var i = 0; i < xd.DocumentElement.ChildNodes.Count; i++)
                    {
                        var xe = (XmlElement)xd.DocumentElement.ChildNodes[i];
                        if (xe["GuideId"] == null)
                        {
                            continue;
                        }
                        var xmlElement = xe["GuideId"];
                        if (xmlElement == null
                            || (xmlElement.FirstChild == null || string.IsNullOrEmpty(xmlElement.FirstChild.Value)))
                        {
                            continue;
                        }
                        var element = xe["GuideId"];
                        if (element != null)
                        {
                            lst.Add(this.GetGuideModel(element.FirstChild.Value));
                        }
                    }
                }
            }
            return lst;
        }
        #endregion

        #region 其他
        /// <summary>
        /// 获得tbl_sourceOther表的xml语句
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetOtherXmlString(IList<Model.SourceStructure.MSourceOtherType> list)
        {
            List<object> l = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                l.Add(list[i]);
            }
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }

        /// <summary>
        /// 获得其他类别列表
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        private IList<Model.SourceStructure.MSourceOtherType> GetSourceOtherList(string sourceId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from tbl_SourceOther where SourceId='{0}' order by SortId asc", sourceId);
            DbCommand dc = _db.GetSqlStringCommand(sb.ToString());
            IList<Model.SourceStructure.MSourceOtherType> list = new List<Model.SourceStructure.MSourceOtherType>();
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    Model.SourceStructure.MSourceOtherType model = new Model.SourceStructure.MSourceOtherType();
                    model.SouceId = sourceId;
                    model.Price = (decimal)reader["Price"];
                    model.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    model.Desc = !reader.IsDBNull(reader.GetOrdinal("Desc")) ? (string)reader["Desc"] : string.Empty;
                    model.ClosingCost = (decimal)reader["ClosingCost"];
                    model.OtherId = (string)reader["OtherId"];
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 附件
        /// <summary>
        /// 获得附件xml语句
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetAttachXmlString(IList<Model.ComStructure.MComAttach> list)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            IList<object> l = list.Cast<object>().ToList();
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(l);
        }


        /// <summary>
        /// 获得附件XML语句
        /// </summary>
        /// <param name="model">附件Model</param>
        /// <returns></returns>
        public string GetAttachXmlString(Model.ComStructure.MComAttach model)
        {
            return EyouSoft.Toolkit.Function.ReflectionHelp.GetModelXmlString(model);
        }
        #endregion
        #endregion
    }
}

