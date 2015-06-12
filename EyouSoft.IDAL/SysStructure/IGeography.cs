using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 地理接口
    /// 创建者：郑付杰
    /// 创建时间：2011/9/29
    /// </summary>
    public interface IGeography
    { 
        /// <summary>
        /// 获取默认所有国家省份城市县区信息
        /// </summary>
        /// <returns></returns>
        IList<MSysCountry> GetAllList();
    }
}
