using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 资源管理票务
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ISourceTicket
    {
        /// <summary>
        /// 添加票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns></returns>
        int AddTicketModel(Model.SourceStructure.MSourceTicket model);

        /// <summary>
        /// 修改票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns></returns>
        int UpdateTicketModel(Model.SourceStructure.MSourceTicket model);
        
        /// <summary>
        /// 获得票务Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceTicket GetTicketModel(string SourceId);
    }
}
