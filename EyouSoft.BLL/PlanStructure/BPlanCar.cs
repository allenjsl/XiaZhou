using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排用车
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanCar
    {
        EyouSoft.IDAL.PlanStructure.IPlanCar dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanCar>();

        #region 用车
        /// <summary>
        /// 添加用车信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddCar(EyouSoft.Model.PlanStructure.MPlanCar model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                model.PlanId = System.Guid.NewGuid().ToString();
                if (dal.AddCar(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(model.CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("新增用车安排，用车名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据计调ID获取用车实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanCar GetCarModel(string PlanID)
        {
            return dal.GetCarModel(PlanID);
        }
        /// <summary>
        /// 修改用车信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateCar(EyouSoft.Model.PlanStructure.MPlanCar model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                if (dal.UpdateCar(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(EyouSoft.Security.Membership.UserProvider.GetUserInfo().CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("修改用车安排，用车名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据团队编号获取用车列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanCar> GetCarList(string TourId, string CompanyId, bool isAll)
        {
            return dal.GetCarList(TourId, CompanyId, isAll);
        }
        /// <summary>
        /// 删除用车计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteCar(string PlanID)
        {
            if (!string.IsNullOrEmpty(PlanID))
            {
                if (dal.DeleteCar(PlanID))
                {
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("删除用车安排，计调编号:{0}", PlanID);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
