using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排签证
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanVisa
    {
        EyouSoft.IDAL.PlanStructure.IPlanVisa dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanVisa>();
        #region 签证
        /// <summary>
        /// 添加签证信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddVisa(EyouSoft.Model.PlanStructure.MPlanVisa model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                model.PlanId = System.Guid.NewGuid().ToString();
                if (dal.AddVisa(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(model.CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.签证);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("新增签证安排，签证名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据计调ID获取签证实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回签证实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanVisa GetVisaModel(string PlanID)
        {
            return dal.GetVisaModel(PlanID);
        }
        /// <summary>
        /// 修改签证信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateVisa(EyouSoft.Model.PlanStructure.MPlanVisa model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                if (dal.UpdateVisa(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(EyouSoft.Security.Membership.UserProvider.GetUserInfo().CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.签证);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("修改签证安排，签证名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据团队编号获取签证列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanVisa> GetVisaList(string TourId, string CompanyId, bool isAll)
        {
            return dal.GetVisaList(TourId, CompanyId, isAll);
        }
        /// <summary>
        /// 删除签证计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteVisa(string PlanID)
        {
            if (!string.IsNullOrEmpty(PlanID))
            {
                if (dal.DeleteVisa(PlanID))
                {
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("删除签证安排，计调编号:{0}", PlanID);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
