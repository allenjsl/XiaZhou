using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 基础权限
    /// </summary>
    public interface ISysPrivs
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <param name="mode">获取方式 true:获取所有 false：获取一二级模块</param>
        /// <param name="sysId">系统编号</param>
        /// <returns>模块集合</returns>
        IList<MSysPrivs> GetList(bool mode, string sysId);

    }
}
