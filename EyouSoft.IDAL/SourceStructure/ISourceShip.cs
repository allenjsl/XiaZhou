using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 创建者  钱琦 时间:2011-9-2
    /// </summary>
    public interface ISourceShip
    {
        /// <summary>
        /// 添加单个游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns></returns>
        int AddShipModel(Model.SourceStructure.MSourceShip model);

        /// <summary>
        /// 修改游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns></returns>
        int UpdateShipModel(Model.SourceStructure.MSourceShip model);

        /// <summary>
        /// 获得详细页面显示游轮Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceShip GetShipModel(string SourceId);
    }
}
