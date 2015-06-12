using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//资源预控
//创建人：郑付杰
//创建时间：2011/9/5
namespace EyouSoft.Model.SourceStructure
{
    #region 
    /// <summary>
    /// 资源预控共有属性
    /// </summary>
    [Serializable]
    public abstract class MSourceControl
    {
        /// <summary>
        /// 预控编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// (酒店/车辆/游轮/景点/其他)编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// (酒店/车辆/游轮/景点/其他)名称
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 预控数量
        /// </summary>
        public int ControlNum { get; set; }

        /// <summary>
        /// 已使用数量
        /// </summary>
        public int AlreadyNum { get; set; }

        /// <summary>
        /// 预控类型
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceControlType SourceControlType { get; set; }

        /// <summary>
        /// 总控共享方式
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.ShareType ShareType { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }

        /// <summary>
        /// 单控团号
        /// </summary>
        public IList<MSourceSueTour> TourNoList { get; set; }

        /// <summary>
        /// 预控人员
        /// </summary>
        public IList<MSourceSueOperator> OperatorList { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 总房价格
        /// </summary>
        public decimal TotalPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId
        {
            get;
            set;
        }
    }
    #endregion

    #region 车辆

    /// <summary>
    /// 车辆预控
    /// </summary>
    [Serializable]
    public class MSourceSueCar : MSourceControl
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueCar() { }

        /// <summary>
        /// 车型编号
        /// </summary>
        public string CarId { get; set; }
      
        /// <summary>
        /// 车型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int DaysNum { get; set; }

        /// <summary>
        /// 预控开始时间
        /// </summary>
        public DateTime? SueStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预控结束时间
        /// </summary>
        public DateTime? SueEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后保留时间
        /// </summary>
        public DateTime? LastTime
        {
            get;
            set;
        }
    }

    #endregion

    #region 酒店

    /// <summary>
    /// 酒店预控
    /// </summary>
    [Serializable]
    public class MSourceSueHotel:MSourceControl
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueHotel() { }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 房型
        /// </summary>
        public string RoomType { get; set; }

        /// <summary>
        /// 预付房款
        /// </summary>
        public decimal Advance { get; set; }

        /// <summary>
        /// 起始有效期
        /// </summary>
        public DateTime PeriodStartTime { get; set; }
        /// <summary>
        /// 结束有效期
        /// </summary>
        public DateTime PeriodEndTime { get; set; }

        /// <summary>
        /// 最后保留日期
        /// </summary>
        public DateTime LastTime
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 酒店/车队/游轮/景点/其他变更
    /// </summary>
    [Serializable]
    public class MSourceSueHotelChange
    {
        /// <summary>
        /// 预控编号
        /// </summary>
        public string SueId { get; set; }

        /// <summary>
        /// 增
        /// </summary>
        public int Plus { get; set; }

        /// <summary>
        /// 减
        /// </summary>
        public int Cut { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 预控类型
        /// </summary>
        public Model.EnumType.SourceStructure.SourceControlCategory Type
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 预控确认单Model(包括酒店预控,车辆预控,游船预控)
    /// </summary>
    [Serializable]
    public class MSourceSueSourceQRD
    {
        /// <summary>
        /// 预控编号
        /// </summary>
        public string SueId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商名称(酒店名称,车队名称,公司名称)
        /// </summary>
        public string SourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 型名称(房型名称,车型名称,游船名称)
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 预控数量
        /// </summary>
        public int ControlNum
        {
            get;
            set;
        }

        /// <summary>
        /// 预控日期
        /// </summary>
        public DateTime ControlTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后保留日期
        /// </summary>
        public DateTime? LastTime
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 预付金额
        /// </summary>
        public decimal AdvancePrice
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }

    #endregion

    #region 游轮
    /// <summary>
    /// 游轮预控
    /// </summary>
    [Serializable]
    public class MSourceSueShip:MSourceControl
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueShip() { }
        /// <summary>
        /// 游船公司
        /// </summary>
        public string ShipCompany { get; set; }

        /// <summary>
        /// 旗下游轮编号
        /// </summary>
        public string SubId { get; set; }

        /// <summary>
        /// 游轮名称
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// 登船日期
        /// </summary>
        public DateTime? GoShipTime
        {
            get;
            set;
        }
        /// <summary>
        /// 航线
        /// </summary>
        public string ShipRoute
        {
            get;
            set;
        }

        /// <summary>
        /// 景点
        /// </summary>
        public string ShipSpot
        {
            get;
            set;
        }

        /// <summary>
        /// 预付金额
        /// </summary>
        public decimal Advance
        {
            get;
            set;
        }

        /// <summary>
        /// 最后保留日期
        /// </summary>
        public DateTime? LastTime
        {
            get;
            set;
        }
    }
    #endregion

    #region 景点
    /// <summary>
    /// 景点预控
    /// </summary>
    [Serializable]
    public class MSourceSueSight : MSourceControl
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueSight() { }

        /// <summary>
        /// 景点编号
        /// </summary>
        public string SpotId { get; set; }
      
        /// <summary>
        /// 景点名称
        /// </summary>
        public string SpotName { get; set; }
        /// <summary>
        /// 预付房款
        /// </summary>
        public decimal Advance { get; set; }
        /// <summary>
        /// 预控开始时间
        /// </summary>
        public DateTime? SueStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预控结束时间
        /// </summary>
        public DateTime? SueEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后保留时间
        /// </summary>
        public DateTime? LastTime
        {
            get;
            set;
        }
    }    
    #endregion

    #region 其他
   /// <summary>
    /// 景点预控
    /// </summary>
    [Serializable]
    public class MSourceSueOther : MSourceControl
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueOther() { }

        /// <summary>
        /// 其他类别编号
        /// </summary>
        public string TypeId { get; set; }
      
        /// <summary>
        /// 其他类别名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 预付房款
        /// </summary>
        public decimal Advance { get; set; }
        /// <summary>
        /// 预控开始时间
        /// </summary>
        public DateTime? SueStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预控结束时间
        /// </summary>
        public DateTime? SueEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后保留时间
        /// </summary>
        public DateTime? LastTime
        {
            get;
            set;
        }
    }    
    #endregion

    #region 单控团号
    /// <summary>
    /// 单控团号
    /// </summary>
    [Serializable]
    public class MSourceSueTour
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueTour(){}
        /// <summary>
        /// 预控类别
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory SourceType { get; set; }

        /// <summary>
        /// 预控编号
        /// </summary>
        public string SueId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
    }
    #endregion

    #region 预控人员
    /// <summary>
    /// 预控人员
    /// </summary>
    [Serializable]
    public class MSourceSueOperator
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueOperator() { }
        /// <summary>
        /// 预控类别
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory SourceType { get; set; }

        /// <summary>
        /// 预控编号
        /// </summary>
        public string SueId { get; set; }

        /// <summary>
        /// 预控人员
        /// </summary>
        public string OperatorId { get; set; }
    }

    #endregion

    #region 预控资源搜索实体
    /// <summary>
    /// 预控资源搜索实体
    /// </summary>
    public abstract class MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSearch() { }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// (酒店/车辆/游轮)名称
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 过期提醒
        /// </summary>
        public bool IsExpiration { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// 预控登记人
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 是否事务提醒
        /// </summary>
        public bool IsTranTip { get; set; }

        /// <summary>
        /// 提前多少天提醒
        /// </summary>
        public int EarlyDays { get; set; }
    }

    /// <summary>
    /// 车辆搜索实体
    /// </summary>
    public class MSourceSueCarSearch:MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueCarSearch() { }
       
        /// <summary>
        /// 车型名称
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int? DaysNum { get; set; }
    }
    /// <summary>
    /// 酒店搜索实体
    /// </summary>
    public class MSourceSueHotelSearch:MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueHotelSearch() { }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 起始有效期
        /// </summary>
        public DateTime? PeriodStartTime { get; set; }
        /// <summary>
        /// 结束有效期
        /// </summary>
        public DateTime? PeriodEndTime { get; set; }
    }
    /// <summary>
    /// 游轮搜索实体
    /// </summary>
    public class MSourceSueShipSearch:MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueShipSearch() { }
        /// <summary>
        /// 游轮公司
        /// </summary>
        public string ShipCompany { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 景点搜索实体
    /// </summary>
    public class MSourceSueSightSearch : MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueSightSearch() { }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string SpotName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 其他搜索实体
    /// </summary>
    public class MSourceSueOtherSearch : MSourceSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSueOtherSearch() { }
        /// <summary>
        /// 支出项目
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            get;
            set;
        }
    }
    #endregion

    #region 预控已使用列表实体
    /// <summary>
    /// 已使用列表
    /// </summary>
    [Serializable]
    public class MSueUse
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSueUse() { }

        /// <summary>
        /// 序号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计调员名称
        /// </summary>
        public string PlanName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId
        {
            get;
            set;
        }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId
        {
            get;
            set;
        }


        /// <summary>
        /// 导游列表
        /// </summary>
        public IList<Model.SourceStructure.MSourceGuide> GuideList
        {
            get;
            set;
        }

        /// <summary>
        /// 游客列表
        /// </summary>
        public IList<Model.TourStructure.MTourOrderTraveller> TravellerList
        {
            get;
            set;
        }

    }
    #endregion

    /// <summary>
    /// 计调安排资源预控列表
    /// </summary>
    [Serializable]
    public class MSourceSuePlan
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsPermission
        {
            get;
            set;
        }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend
        {
            get;
            set;
        }

        /// <summary>
        ///车型,游船名称,房型 
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 控房数量,控船数量,控车数量
        /// </summary>
        public int ControlNum
        {
            get;
            set;
        }

        /// <summary>
        /// 预控开始日期
        /// </summary>
        public DateTime? SueStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 预控结束日期
        /// </summary>
        public DateTime? SueEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 已使用数量
        /// </summary>
        public int UsedNum
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人名称
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人传真
        /// </summary>
        public string ContactFax
        {
            get;
            set;
        }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId
        {
            get;
            set;
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId
        {
            get;
            set;
        }

        /// <summary>
        /// 县区编号
        /// </summary>
        public int CountyId
        {
            get;
            set;
        }
    }
}
