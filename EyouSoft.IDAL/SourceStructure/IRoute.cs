using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 线路资源
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// 添加线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns></returns>
        int AddRouteModel(Model.SourceStructure.MRoute model);

        /// <summary>
        /// 修改线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns></returns>
        int UpdateRouteModel(Model.SourceStructure.MRoute model);

        /// <summary>
        /// 删除线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        int DeleteRouteModel(string RouteId);

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
        IList<Model.SourceStructure.MRouteListModel> GetRouteShowModel(Model.SourceStructure.MRouteListModel model, bool isOnlySeft, string loginUserId, string DeptId, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获得线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        Model.SourceStructure.MRoute GetRouteModel(string RouteId);

        /// <summary>
        /// 判断线路名称是否存在
        /// </summary>
        /// <param name="RouteName">线路名称</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        bool IsExists(string RouteName, string companyId, int areaId);

        /// <summary>
        /// 获得上团列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MTourOnCount> GetSTModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获得收客列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MAcceptGuestModel> GetSKModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount);
    }
}
