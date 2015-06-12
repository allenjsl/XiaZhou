using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;
using EyouSoft.Cache.Tag;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 系统默认城市
    /// 创建者：郑付杰
    /// 创建时间：2011/9/29
    /// </summary>
    public class BGeography
    {
        private readonly EyouSoft.IDAL.SysStructure.IGeography dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.IGeography>();

        /// <summary>
        /// 获取国内省份城市信息
        /// </summary>
        /// <returns>城市集合</returns>
        public MSysCountry GetList()
        {
            return GetAllList().Single(c => c.CountryId == 1);
        }

        /// <summary>
        /// 获取默认所有国家省份城市县区信息
        /// </summary>
        /// <returns></returns>
        public IList<MSysCountry> GetAllList()
        {
            IList<MSysCountry> list = null;
            //缓存
            if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(TagName.SysCity) != null)
            {
                list = (IList<MSysCountry>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(TagName.SysCity);
            }
            else
            {
                list = dal.GetAllList();
                if (list.Count > 0)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(TagName.SysCity, list);
                }
            }

            return list;
        }

    }
}
