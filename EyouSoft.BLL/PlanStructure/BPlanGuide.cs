using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排导游
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanGuide
    {
        EyouSoft.IDAL.PlanStructure.IPlanGuide dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanGuide>();
        #region 导游
        /// <summary>
        /// 添加导游信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddGuide(EyouSoft.Model.PlanStructure.MPlanGuide model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                model.PlanId = System.Guid.NewGuid().ToString();
                if (dal.AddGuide(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(model.CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("新增导游安排，导游名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据计调ID获取导游实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回导游实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanGuide GetGuideModel(string PlanID)
        {
            return dal.GetGuideModel(PlanID);
        }
        /// <summary>
        /// 修改导游信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateGuide(EyouSoft.Model.PlanStructure.MPlanGuide model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                if (dal.UpdateGuide(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(EyouSoft.Security.Membership.UserProvider.GetUserInfo().CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("修改导游安排，导游名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据团队编号获取导游列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanGuide> GetGuideList(string TourId, string CompanyId, bool isAll)
        {
            return dal.GetGuideList(TourId, CompanyId, isAll);
        }
        /// <summary>
        /// 删除导游计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteGuide(string PlanID)
        {
            if (!string.IsNullOrEmpty(PlanID))
            {
                if (dal.DeleteGuide(PlanID))
                {
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("删除导游安排，计调编号:{0}", PlanID);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
