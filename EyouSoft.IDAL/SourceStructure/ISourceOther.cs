using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 资源管理其他
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ISourceOther
    {

        /// <summary>
        /// 添加其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns></returns>
        int AddOtherModel(Model.SourceStructure.MSourceOther model);

        /// <summary>
        /// 修改其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns></returns>
        int UpdateOtherModel(Model.SourceStructure.MSourceOther model);

        /// <summary>
        /// 获得其他Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceOther GetOtherModel(string SourceId);
    }
}
