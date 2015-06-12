using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PlanStructure
{
    /// <summary>
    /// 描述:业务接口操作保险
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public interface IPlanInsurance
    {
        #region 保险
        /// <summary>
        /// 添加保险信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool AddInsurance(EyouSoft.Model.PlanStructure.MPlanInsurance model);
        /// <summary>
        /// 根据计调ID获取保险实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回保险实体</returns>
        EyouSoft.Model.PlanStructure.MPlanInsurance GetInsuranceModel(string PlanID);
        /// <summary>
        /// 修改保险信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool UpdateInsurance(EyouSoft.Model.PlanStructure.MPlanInsurance model);
        /// <summary>
        /// 根据团队编号获取保险列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        IList<EyouSoft.Model.PlanStructure.MPlanInsurance> GetInsuranceList(string TourId, string CompanyId,bool isAll);
        /// <summary>
        /// 删除签证保险项目
        /// </summary>
        /// <param name="PlanIDs">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        bool DeleteInsurance(string PlanID);
        #endregion
    }
}
