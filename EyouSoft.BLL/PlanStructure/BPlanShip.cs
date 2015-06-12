using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 描述:业务逻辑层计调安排游轮
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlanShip
    {
        EyouSoft.IDAL.PlanStructure.IPlanShip dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlanShip>();
        #region 游轮
        /// <summary>
        /// 添加游轮信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool AddShip(EyouSoft.Model.PlanStructure.MPlanShip model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                model.PlanId = System.Guid.NewGuid().ToString();
                if (dal.AddShip(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(EyouSoft.Security.Membership.UserProvider.GetUserInfo().CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.游轮);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("新增游轮安排，游轮名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据计调ID获取游轮实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回用车实体</returns>
        public EyouSoft.Model.PlanStructure.MPlanShip GetShipModel(string PlanID)
        {
            return dal.GetShipModel(PlanID);
        }
        /// <summary>
        /// 修改游轮信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        public bool UpdateShip(EyouSoft.Model.PlanStructure.MPlanShip model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.TourId))
                {
                    return false;
                }
                if (dal.UpdateShip(model))
                {
                    new EyouSoft.BLL.PlanStructure.BPlan().UpdatePlanStatus(model.CompanyId, model.TourId, EyouSoft.Model.EnumType.PlanStructure.PlanProject.游轮);
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("修改游轮安排，游轮名称:{0}，支出金额:{1}，计调编号:{2}", model.SourceName, model.PlanCost, model.PlanId);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据团队编号获取Ship列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        public IList<EyouSoft.Model.PlanStructure.MPlanShip> GetShipList(string TourId, string CompanyId, bool isAll)
        {
            return dal.GetShipList(TourId, CompanyId, isAll);
        }
        /// <summary>
        /// 删除游轮计调项目
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <param name="type">true 国内,false 涉外</param>
        /// <returns>true:成功，false:失败</returns>
        public bool DeleteShip(string PlanID, bool type)
        {
            if (!string.IsNullOrEmpty(PlanID))
            {
                if (dal.DeleteShip(PlanID, type))
                {
                    StringBuilder strLog = new StringBuilder();
                    strLog.AppendFormat("删除游轮安排，计调编号:{0}", PlanID);
                    BSysLogHandle.Insert(strLog.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
