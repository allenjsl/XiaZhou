using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 创建者  钱琦 时间:2011-9-2
    /// </summary>
    public interface ISourceDining
    {
        /// <summary>
        /// 添加单个供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns></returns>
        int AddDiningModel(Model.SourceStructure.MSourceDining model);

        /// <summary>
        /// 修改供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns></returns>
        int UpdateDiningModel(Model.SourceStructure.MSourceDining model);

        /// <summary>
        /// 获得详细页面上的供应商餐馆Model(包含餐馆菜系,联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceDining GetDiningModel(string SourceId);
    }
}
