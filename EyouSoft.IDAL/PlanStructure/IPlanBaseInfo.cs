using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PlanStructure
{
    /// <summary>
    /// 描述:业务接口操作计调安排
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public interface IPlanBaseInfo
    {
        #region 其他
        /// <summary>
        /// 添加其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool AddOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model);
        /// <summary>
        /// 根据计调ID获取其他实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回其他实体</returns>
        EyouSoft.Model.PlanStructure.MPlanBaseInfo GetOtherModel(string PlanID);
        /// <summary>
        /// 修改其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool UpdateOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model);
        /// <summary>
        /// 根据团队编号获取其他列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>true:成功，false:失败</returns>
        IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> GetOtherList(string TourId, string CompanyId);
        /// <summary>
        /// 删除其他计调项目
        /// </summary>
        /// <param name="PlanIDs">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        bool DeleteOther(string PlanID);
        #endregion
    }
}
