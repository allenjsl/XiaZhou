using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SourceStructure;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 创建者  钱琦 时间:2011-9-2
    /// </summary>
    public interface ISource
    { 
        /// <summary>
        /// 添加地接社
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddTravelModel(Model.SourceStructure.MSourceTravel model);

        /// <summary>
        /// 修改地接社Model
        /// </summary>
        /// <param name="model">供应商保险Model</param>
        /// <param name="list">联系人列表</param>
        /// <returns></returns>
        int UpdateTravelModel(Model.SourceStructure.MSourceTravel model);

        /// <summary>
        /// 获得地接社Model
        /// </summary>
        /// <param name="SourceId">供应商Model</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceTravel GetTravelModel(string SourceId);

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
        /// <param name="routeAreaList">线路区域集合</param>
        /// <returns></returns>
        int AddAccountToTravel(string sourceId, string companyId, string account, string md5pwd, string pwd, string operatorId, string operatorNm, int operDeptId, IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList);

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
        int UpdateTravelAccountStatus(
            string sourceId,
            Model.EnumType.ComStructure.UserStatus Status,
            string md5pwd,
            string pwd,
            IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList,
            bool isUpdAccount);
    }
}
