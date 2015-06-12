using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排其他
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanBaseInfo
    {
        EyouSoft.IDAL.PlanStructure.IPlanBaseInfo dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanBaseInfo>();
        #region 其他
        /// <summary>
        /// 添加其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model)
        {
            return dal.AddOther(model);
        }
        /// <summary>
        /// 根据计调ID获取其他实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回其他实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanBaseInfo GetOtherModel(string PlanID)
        {
            return dal.GetOtherModel(PlanID);
        }
        /// <summary>
        /// 修改其他信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateOther(EyouSoft.Model.PlanStructure.MPlanBaseInfo model)
        {
            return dal.UpdateOther(model);
        }
        /// <summary>
        /// 根据团队编号获取其他列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> GetOtherList(string TourId, string CompanyId)
        {
            return dal.GetOtherList(TourId, CompanyId);
        }
        /// <summary>
        /// 删除其他计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteOther(string PlanID)
        {
            return dal.DeleteOther(PlanID);
        }
        #endregion
    }
}
