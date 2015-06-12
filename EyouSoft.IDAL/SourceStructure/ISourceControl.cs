using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.EnumType.SourceStructure;

namespace EyouSoft.IDAL.SourceStructure
{
    /// <summary>
    /// 资源预控
    /// </summary>
    public interface ISourceControl
    {
        #region 车辆预控

        /// <summary>
        /// 添加车辆预控
        /// </summary>
        /// <param name="item">车辆预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSueCar(MSourceSueCar item);

        /// <summary>
        /// 获取车辆预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车辆预控信息</returns>
        MSourceSueCar GetModelByCarId(string id, string companyId);
       /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sueId">预控编号</param>
        /// <returns>已使用列表</returns>
        IList<MSueUse> GetCarUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 分页获取车辆预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount, MSourceSueCarSearch search);
        /// <summary>
        /// 分页获取车辆预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourNo">团号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount, string tourNo, string operatorId, MSourceSueCarSearch search);

        #endregion

        #region 酒店预控

        /// <summary>
        /// 添加酒店预控
        /// </summary>
        /// <param name="item">酒店预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSueHotel(MSourceSueHotel item);

        /// <summary>
        /// 添加变更记录
        /// </summary>
        /// <param name="item">变更记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        /// <returns></returns>
        bool AddSueHotelChange(MSourceSueHotelChange item);
        /// <summary>
        /// 获取酒店预控信息
        /// </summary>
        /// <param name="id">酒店编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>酒店预控信息</returns>
        MSourceSueHotel GetModelByHotelId(string id, string companyId);

        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>已使用列表</returns>
        IList<MSueUse> GetHotelUseList(string sueId, string companyId, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 分页获取酒店预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount, MSourceSueHotelSearch search);

        /// <summary>
        /// 分页获取酒店预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount, string tourid, string operatorId, MSourceSueHotelSearch search);

         /// <summary>
        /// 获得酒店预控变更记录
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MSourceSueHotelChange> GetSourceSueChangeList(string sueId, Model.EnumType.SourceStructure.SourceControlCategory Type);

        #endregion

        #region 游轮预控
        /// <summary>
        /// 添加游轮预控
        /// </summary>
        /// <param name="item">游轮预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSueShip(MSourceSueShip item);
       /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>已使用列表</returns>
        IList<MSueUse> GetShipUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获取游轮预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>游轮预控信息</returns>
        MSourceSueShip GetModelByShipId(string id, string companyId);

        /// <summary>
        /// 分页获取游轮预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, MSourceSueShipSearch search);

        /// <summary>
        /// 分页获取游轮预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, string tourid, string operatorId, MSourceSueShipSearch search);
        #endregion

        #region 景点预控

        /// <summary>
        /// 添加景点预控
        /// </summary>
        /// <param name="item">景点预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSueSight(MSourceSueSight item);

        /// <summary>
        /// 分页获取景点预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        IList<MSourceSueSight> GetListSueSight(
            int pageCurrent, int pageSize, ref int pageCount, MSourceSueSightSearch search);

        /// <summary>
        /// 获取景点预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景点预控信息</returns>
        MSourceSueSight GetModelBySightId(string id, string companyId);

        /// <summary>
        /// 分页获取景点预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>景点集合</returns>
        IList<MSourceSueSight> GetListSueSight(
            int pageCurrent,
            int pageSize,
            ref int pageCount,
            string tourid,
            string operatorId,
            MSourceSueSightSearch search);

        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<MSueUse> GetSightUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount);

        #endregion

        #region 其他预控

        /// <summary>
        /// 添加其他预控
        /// </summary>
        /// <param name="item">其他预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSueOther(MSourceSueOther item);

        /// <summary>
        /// 分页获取其他预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        IList<MSourceSueOther> GetListSueOther(
            int pageCurrent, int pageSize, ref int pageCount, MSourceSueOtherSearch search);

        /// <summary>
        /// 获取其他预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>其他预控信息</returns>
        MSourceSueOther GetModelByOtherId(string id, string companyId);

        /// <summary>
        /// 分页获取其他预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>其他集合</returns>
        IList<MSourceSueOther> GetListSueOther(
            int pageCurrent,
            int pageSize,
            ref int pageCount,
            string tourid,
            string operatorId,
            MSourceSueOtherSearch search);

        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<MSueUse> GetOtherUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount);

        #endregion

        /// <summary>
        /// 查询预控剩余数量
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <param name="category">预控类别</param>
        /// <returns>预控剩余数量</returns>
        int SueSurplusNum(string sourceId, SourceControlCategory category);

        /// <summary>
        /// 获得计调安排资源预控列表
        /// </summary>
        /// <param name="searchModel">查询Model</param>
        /// <param name="type">资源预控类型(酒店,车辆,游船)</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MSourceSuePlan> GetSourceSuePlanList(Model.SourceStructure.MSourceSuePlan searchModel, SourceControlCategory type, int pageIndex, int pageSize, ref int recordCount);
    }
}
