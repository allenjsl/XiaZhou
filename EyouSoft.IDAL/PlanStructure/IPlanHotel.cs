using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PlanStructure
{
    /// <summary>
    /// 描述:业务接口操作酒店
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public interface IPlanHotel
    {
        #region 酒店
        /// <summary>
        /// 添加酒店信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool AddHotel(EyouSoft.Model.PlanStructure.MPlanHotel model);
        /// <summary>
        /// 根据计调ID获取酒店实体
        /// </summary>
        /// <param name="PlanID">计调ID</param>
        /// <returns>返回model</returns>
        EyouSoft.Model.PlanStructure.MPlanHotel GetHotelModel(string PlanID);
        /// <summary>
        /// 修改酒店信息
        /// </summary>
        /// <param name="model">信息实体</param>
        /// <returns>true:成功，false:失败</returns>
        bool UpdateHotel(EyouSoft.Model.PlanStructure.MPlanHotel model);
        /// <summary>
        /// 根据团队编号获取酒店列表
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="isAll">是否显示所有计调项目(注意:在报帐的页面根据,系统配置是否显示全部[true],否则只显示导游现付[false])</param>
        /// <returns>true:成功，false:失败</returns>
        IList<EyouSoft.Model.PlanStructure.MPlanHotel> GetHotelList(string TourId, string CompanyId,bool isAll);
        /// <summary>
        /// 删除酒店计调项目
        /// </summary>
        /// <param name="PlanIDs">计调ID</param>
        /// <returns>true:成功，false:失败</returns>
        bool DeleteHotel(string PlanID);
        #endregion
    }
}
