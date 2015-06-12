using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 系统菜单配置
    /// </summary>
    public interface ISysMenu
    {
        /// <summary>
        /// 修改系统默认菜单名称
        /// </summary>
        /// <param name="item">菜单配置实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(MSysMenu item);
        /// <summary>
        /// 添加系统默认菜单名称
        /// </summary>
        /// <param name="all">菜单集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(IList<MSysMenu> all);
    }
}
