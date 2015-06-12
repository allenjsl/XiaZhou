using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 创建者  钱琦 时间:2011-9-2
    /// </summary>
    public interface ISourceMotorcade
    {
        /// <summary>
        /// 添加单个车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns></returns>
        int AddMotorcadeModel(Model.SourceStructure.MSourceMotorcade model);

        /// <summary>
        /// 修改车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns></returns>
        int UpdateMotorcadeModel(Model.SourceStructure.MSourceMotorcade model);

        /// <summary>
        /// 获得车队Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceMotorcade GetMotorcadeModel(string SourceId);
    }
}
