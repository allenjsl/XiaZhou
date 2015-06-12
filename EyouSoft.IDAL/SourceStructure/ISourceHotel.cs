using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 创建者  钱琦 时间:2011-9-2
    /// </summary>
    public interface ISourceHotel
    {
        /// <summary>
        /// 添加供应商酒店Model
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns></returns>
        int AddHotelModel(Model.SourceStructure.MSourceHotel model);

        /// <summary>
        /// 修改供应商酒店Model(包含联系人,价格)
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns></returns>
        int UpdateHotelModel(Model.SourceStructure.MSourceHotel model);

        /// <summary>
        /// 获得酒店详细页面供应商酒店Model(包含联系人列表,房型信息列表)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceHotel GetOneHotelModel(string SourceId);

        /// <summary>
        /// 获得酒店房型列表
        /// </summary>
        /// <param name="sourceId">酒店编号</param>
        /// <returns>酒店房型列表</returns>
        IList<Model.SourceStructure.MSourceHotelRoom> GetRoomModelList(string sourceId);
    }
}
