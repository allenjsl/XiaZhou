using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排保险
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanInsurance
    {
        EyouSoft.IDAL.PlanStructure.IPlanInsurance dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanInsurance>();
        #region 保险
        /// <summary>
        /// 添加保险信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddInsurance(EyouSoft.Model.PlanStructure.MPlanInsurance model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                model.PlanId = System.Guid.NewGuid().ToString();
                if (dal.AddInsurance(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(model.CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.保险);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("新增保险安排，保险名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据计调ID获取保险实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回保险实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanInsurance GetInsuranceModel(string PlanID)
        {
            return dal.GetInsuranceModel(PlanID);
        }
        /// <summary>
        /// 修改保险信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateInsurance(EyouSoft.Model.PlanStructure.MPlanInsurance model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                if (dal.UpdateInsurance(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(EyouSoft.Security.Membership.UserProvider.GetUserInfo().CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.保险);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("修改保险安排，保险名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据团队编号获取保险列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanInsurance> GetInsuranceList(string TourId, string CompanyId, bool isAll)
        {
            return dal.GetInsuranceList(TourId, CompanyId, isAll);
        }
        /// <summary>
        /// 删除签证保险项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteInsurance(string PlanID)
        {
            if (!string.IsNullOrEmpty(PlanID))
            {
                if (dal.DeleteInsurance(PlanID))
                {
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("删除保险安排，计调编号:{0}", PlanID);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
