using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PlanStructure
{
    /// <summary>
    /// 描述:业务接口操作领料
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public interface IPlanPicking
    {

        #region 领料
        /// <summary>
        /// 添加领料信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool AddPicking(EyouSoft.Model.PlanStructure.MPlanPicking model);
        /// <summary>
        /// 根据计调ID获取领料实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        EyouSoft.Model.PlanStructure.MPlanPicking GetPickingModel(string PlanID);
        /// <summary>
        /// 修改领料信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool UpdatePicking(EyouSoft.Model.PlanStructure.MPlanPicking model);
        /// <summary>
        /// 根据团队编号获取领料列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        IList<EyouSoft.Model.PlanStructure.MPlanPicking> GetPickingList(string TourId, string CompanyId,bool isAll);
        /// <summary>
        /// 删除领料计调项目
        /// </summary>
        /// <param name="PlanIDs">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        bool DeletePicking(string PlanID);
        #endregion
    }
}
