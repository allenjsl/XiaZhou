using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    using EyouSoft.Model.SourceStructure;

    /// <summary>
    /// 资源管理景点
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ISourceSpot
    {
        /// <summary>
        /// 添加景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns></returns>
        int AddSpotModel(Model.SourceStructure.MSourceSpot model);

        /// <summary>
        /// 修改景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns></returns>
        int UpdateSpotModel(Model.SourceStructure.MSourceSpot model);
        
        /// <summary>
        /// 获得景点信息(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceSpot GetSpotModel(string SourceId);

        /// <summary>
        /// 获得景点价格体系
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MSpotPriceSystemModel> GetSpotPriceSystemModelList(string companyId, string sourceId);
    }
}
