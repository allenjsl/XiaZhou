using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 资源管理购物
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ISourceShop
    {
        /// <summary>
        /// 添加购物Model
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <returns></returns>
        int AddShopModel(Model.SourceStructure.MSourceShop model);

        /// <summary>
        /// 修改购物Model
        /// </summary>
        /// <param name="model">供应商购物Model</param>
        /// <returns></returns>
        int UpdateShopModel(Model.SourceStructure.MSourceShop model);

        /// <summary>
        /// 获得购物Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceShop GetShopModel(string SourceId);
    }
}
