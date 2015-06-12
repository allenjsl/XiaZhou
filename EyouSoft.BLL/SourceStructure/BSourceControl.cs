using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.EnumType.SourceStructure;

namespace EyouSoft.BLL.SourceStructure
{
    /// <summary>
    /// 资源预控业务层
    /// 创建者:郑付杰
    /// 创建时间:2011/9/16
    /// </summary>
    public class BSourceControl
    {

        private readonly EyouSoft.IDAL.SourceStructure.ISourceControl dal = 
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceControl>();

        #region 车辆预控
        /// <summary>
        /// 添加车辆预控
        /// </summary>
        /// <param name="item">车辆预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueCar(MSourceSueCar item)
        {
            bool result = false;
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                result = dal.AddSueCar(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加车辆预控，编号为:{0}", item.Id));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取车辆预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车辆预控信息</returns>
        public MSourceSueCar GetModelByCarId(string id, string companyId)
        {
            MSourceSueCar item = null;
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModelByCarId(id, companyId);
            }
            return item;
        }
        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sueId">预控编号</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少</param>

        /// <param name="recordCount">总记录数</param>
        /// <returns>已使用列表</returns>
        public IList<MSueUse> GetCarUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MSueUse> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetCarUseList(companyId,sueId,pageIndex,pageSize,ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 分页获取车辆预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount, 
            MSourceSueCarSearch search)
        {
            IList<MSourceSueCar> list = null;
            if (pageCurrent <= 0)
                pageCurrent = 1;
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueCar(pageCurrent, pageSize, ref pageCount, search);
            }

            return list;
        }

        /// <summary>
        /// 分页获取车辆预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团队编号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueCar> GetListSueCar(int pageCurrent, int pageSize, ref int pageCount,
            string tourid, string operatorId, MSourceSueCarSearch search)
        {
            IList<MSourceSueCar> list = null;
            if (pageCurrent <= 0)
                pageCurrent = 1;
            if (!string.IsNullOrEmpty(tourid) && !string.IsNullOrEmpty(operatorId)
                && search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueCar(pageCurrent, pageSize, ref pageCount, tourid, operatorId, search);
            }

            return list;
        }
        #endregion

        #region 酒店预控
        /// <summary>
        /// 添加酒店预控
        /// </summary>
        /// <param name="item">酒店预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueHotel(MSourceSueHotel item)
        {
            bool result = false;
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                result = dal.AddSueHotel(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加酒店预控，编号为:{0}", item.Id));
                }
            }
            return result;
        }
        /// <summary>
        /// 添加变更记录
        /// </summary>
        /// <param name="item">变更记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        /// <returns></returns>
        public bool AddSueHotelChange(MSourceSueHotelChange item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.AddSueHotelChange(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加酒店变更记录，编号为:{0}", item.SueId));
                }
            }

            return result;
        }
        /// <summary>
        /// 获取酒店预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>酒店预控信息</returns>
        public MSourceSueHotel GetModelByHotelId(string id, string companyId)
        {
            MSourceSueHotel item = null;
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModelByHotelId(id, companyId);
            }
            return item;
        }
        /// <summary>
        /// 已使用列表
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>已使用列表</returns>
        public IList<MSueUse> GetHotelUseList(string sueId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MSueUse> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetHotelUseList(sueId,companyId,pageIndex,pageSize,ref recordCount);
            }
            return list;
        }
        /// <summary>
        /// 分页获取酒店预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount,
            MSourceSueHotelSearch search)
        {
            IList<MSourceSueHotel> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueHotel(pageCurrent, pageSize, ref pageCount, search);
            }

            return list;
        }
        /// <summary>
        /// 分页获取酒店预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">团号</param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueHotel> GetListSueHotel(int pageCurrent, int pageSize, ref int pageCount,
            string tourid, string operatorId, MSourceSueHotelSearch search)
        {
            IList<MSourceSueHotel> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (!string.IsNullOrEmpty(tourid) && !string.IsNullOrEmpty(operatorId)
                && search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueHotel(pageCurrent, pageSize, ref pageCount, tourid, operatorId, search);
            }

            return list;
        }

         /// <summary>
        /// 获得酒店预控变更记录
        /// </summary>
        /// <param name="sueId">预控编号</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSourceSueHotelChange> GetSourceSueChangeList(string sueId, Model.EnumType.SourceStructure.SourceControlCategory Type)
        {
            return dal.GetSourceSueChangeList(sueId, Type);
        }

        #endregion

        #region 游轮预控
        /// <summary>
        /// 添加游轮预控
        /// </summary>
        /// <param name="item">游轮预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueShip(MSourceSueShip item)
        {
            bool result = false;
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                result = dal.AddSueShip(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加游轮预控，编号为:{0}", item.Id));
                }
            }

            return result;
        }

         /// <summary>
        /// 获取游轮预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>游轮预控信息</returns>
        public MSourceSueShip GetModelByShipId(string id, string companyId)
         {
             MSourceSueShip item = null;
             if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(companyId))
             {
                 item = dal.GetModelByShipId(id, companyId);
             }
             return item;
         }
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
        public IList<MSueUse> GetShipUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MSueUse> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetShipUseList(companyId,sueId,pageIndex,pageSize,ref recordCount);
            }
            return list;
        }
        /// <summary>
        /// 分页获取游轮预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, 
            MSourceSueShipSearch search)
        {
            IList<MSourceSueShip> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueShip(pageCurrent, pageSize, ref pageCount, search);
            }

            return list;
        }
        /// <summary>
        /// 分页获取游轮预控信息(计调安排时调用)
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="tourid">计划编号param>
        /// <param name="operatorId">计调员</param>
        /// <param name="search">搜索实体</param>
        /// <returns>车辆集合</returns>
        public IList<MSourceSueShip> GetListSueShip(int pageCurrent, int pageSize, ref int pageCount, 
            string tourid, string operatorId, MSourceSueShipSearch search)
        {
            IList<MSourceSueShip> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (!string.IsNullOrEmpty(tourid) && !string.IsNullOrEmpty(operatorId)
                && search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueShip(pageCurrent, pageSize, ref pageCount, tourid, operatorId, search);
            }
            return list;
        }
        #endregion

        #region 景点预控
        /// <summary>
        /// 添加景点预控
        /// </summary>
        /// <param name="item">景点预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueSight(MSourceSueSight item)
        {
            bool result = false;
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                result = dal.AddSueSight(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加景点预控，编号为:{0}", item.Id));
                }
            }
            return result;
        }

        /// <summary>
        /// 分页获取景点预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueSight> GetListSueSight(int pageCurrent, int pageSize, ref int pageCount, MSourceSueSightSearch search)
        {
            IList<MSourceSueSight> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueSight(pageCurrent, pageSize, ref pageCount, search);
            }

            return list;
        }

        /// <summary>
        /// 获取景点预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景点预控信息</returns>
        public MSourceSueSight GetModelBySightId(string id, string companyId)
        {
            MSourceSueSight item = null;
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModelBySightId(id, companyId);
            }
            return item;
        }
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
        public IList<MSourceSueSight> GetListSueSight(int pageCurrent, int pageSize, ref int pageCount, string tourid, string operatorId, MSourceSueSightSearch search)
        {
            IList<MSourceSueSight> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (!string.IsNullOrEmpty(tourid) && !string.IsNullOrEmpty(operatorId) && search != null
                && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueSight(pageCurrent, pageSize, ref pageCount, tourid, operatorId, search);
            }

            return list;
        }
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
        public IList<MSueUse> GetSightUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MSueUse> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetSightUseList(companyId, sueId, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        #endregion

        #region 其他预控
        /// <summary>
        /// 添加其他预控
        /// </summary>
        /// <param name="item">其他预控实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSueOther(MSourceSueOther item)
        {
            bool result = false;
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                result = dal.AddSueOther(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加其他预控，编号为:{0}", item.Id));
                }
            }
            return result;
        }

        /// <summary>
        /// 分页获取其他预控信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>集合</returns>
        public IList<MSourceSueOther> GetListSueOther(int pageCurrent, int pageSize, ref int pageCount, MSourceSueOtherSearch search)
        {
            IList<MSourceSueOther> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueOther(pageCurrent, pageSize, ref pageCount, search);
            }

            return list;
        }

        /// <summary>
        /// 获取其他预控信息
        /// </summary>
        /// <param name="id">预控编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>其他预控信息</returns>
        public MSourceSueOther GetModelByOtherId(string id, string companyId)
        {
            MSourceSueOther item = null;
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModelByOtherId(id, companyId);
            }
            return item;
        }
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
        public IList<MSourceSueOther> GetListSueOther(int pageCurrent, int pageSize, ref int pageCount, string tourid, string operatorId, MSourceSueOtherSearch search)
        {
            IList<MSourceSueOther> list = null;
            if (pageCurrent <= 0)
            {
                pageCurrent = 1;
            }
            if (!string.IsNullOrEmpty(tourid) && !string.IsNullOrEmpty(operatorId) && search != null
                && !string.IsNullOrEmpty(search.CompanyId))
            {
                list = dal.GetListSueOther(pageCurrent, pageSize, ref pageCount, tourid, operatorId, search);
            }

            return list;
        }
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
        public IList<MSueUse> GetOtherUseList(string companyId, string sueId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MSueUse> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetOtherUseList(companyId, sueId, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 查询预控剩余数量
        /// </summary>
        /// <param name="sourceId">供应商编号</param>
        /// <param name="category">预控类别</param>
        /// <returns>预控剩余数量</returns>
        public int SueSurplusNum(string sourceId, SourceControlCategory category)
        {
            int surplus = 0;
            if (!string.IsNullOrEmpty(sourceId))
            {
                surplus = dal.SueSurplusNum(sourceId, category);
            }
            return surplus;
        }

        /// <summary>
        /// 获得计调安排资源预控列表
        /// </summary>
        /// <param name="searchModel">查询Model</param>
        /// <param name="type">资源预控类型(酒店,车辆,游船)</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSourceSuePlan> GetSourceSuePlanList(Model.SourceStructure.MSourceSuePlan searchModel, SourceControlCategory type, int pageIndex, int pageSize, ref int recordCount)
        {
            if (searchModel == null)
                return null;
            if (string.IsNullOrEmpty(searchModel.CompanyId))
                return null;
            return dal.GetSourceSuePlanList(searchModel, type, pageIndex, pageSize, ref recordCount);
        }
    }
}
