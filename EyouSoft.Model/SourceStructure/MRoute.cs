using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 线路库信息
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_Route")]
    public class MRoute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRoute() { }
        #region Model
       
        private string _companyid;
        private int _areaid;
        private string _routename;
        private string _lineintro;
        private int _days;
        private string _operatorid;
        private DateTime _issuetime;
        /// <summary>
        /// 线路编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "RouteId",DbType="char(36)")]
        public string RouteId
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Column(Name = "CompanyId", DbType = "char(36)")]
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 是否共享
        /// </summary>
        [Column(Name = "IsShare", DbType = "char(1)")]
        public bool IsShare
        {
            get;
            set;
        }
        /// <summary>
        /// 线路区域
        /// </summary>
        [Column(Name = "AreaId", DbType = "int")]
        public int AreaId
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        [Column(Name = "RouteName", DbType = "nvarchar(255)")]
        public string RouteName
        {
            set { _routename = value; }
            get { return _routename; }
        }
        /// <summary>
        /// 线路描述
        /// </summary>
        [Column(Name = "LineIntro", DbType = "nvarchar(Max)")]
        public string LineIntro
        {
            set { _lineintro = value; }
            get { return _lineintro; }
        }
        /// <summary>
        /// 旅游天数
        /// </summary>
        [Column(Name = "Days", DbType = "int")]
        public int Days
        {
            set { _days = value; }
            get { return _days; }
        }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column(Name = "OperatorId", DbType = "char(36)")]
        public string OperatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(Name = "IssueTime", DbType = "datetime")]
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }
        /// <summary>
        /// 是否删除 
        /// </summary>
        [Column(Name = "IsDelete", DbType = "char(1)")]
        public bool IsDelete
        {
            get;
            set;
        }


        /// <summary>
        /// 是整团还是分项
        /// </summary>
        [Column(Name = "IsTourOrSubentry", DbType = "char(1)")]
        public bool IsTourOrSubentry
        {
            get;
            set;
        }


        /// <summary>
        /// 服务标准
        /// </summary>
        [Column(Name = "Service", DbType = "nvarchar(255)")]
        public string Service
        {
            get;
            set;
        }


        /// <summary>
        /// 单价
        /// </summary>
        [Column(Name = "OtherPrice", DbType = "money")]
        public decimal OtherPrice
        {
            get;
            set;
        }


        /// <summary>
        /// 成人价
        /// </summary>
        [Column(Name = "AdultPrice", DbType = "money")]
        public decimal AdultPrice
        {
            get;
            set;
        }


        /// <summary>
        /// 儿童价
        /// </summary>
        [Column(Name = "ChildrenPrice", DbType = "money")]
        public decimal ChildrenPrice
        {
            get;
            set;
        }


        /// <summary>
        /// 合计费用
        /// </summary>
        [Column(Name = "TotalPrice", DbType = "money")]
        public decimal TotalPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        [Column(Name = "DeptId", DbType = "int")]
        public int DeptId
        {
            get;
            set;
        }

        /// <summary>
        /// 行程安排
        /// </summary>
        public IList<Model.TourStructure.MPlanBaseInfo> PlanModelList
        {
            get;
            set;
        }

        /// <summary>
        /// 包含项目
        /// </summary>
        public IList<Model.SourceStructure.MRouteStandard> StandardModelList
        {
            get;
            set;
        }

        /// <summary>
        /// 线路服务
        /// </summary>
        public Model.TourStructure.MTourService ServicesModel
        {
            get;
            set;
        }

        /// <summary>
        /// 附件
        /// </summary>
        public Model.ComStructure.MComAttach Attach
        {
            get;
            set;
        }

        /// <summary>
        /// 集合方式
        /// </summary>
        [Column(Name = "SetMode", DbType = "nvarchar(255)")]
        public string SetMode
        {
            get;
            set;
        }

        /// <summary>
        /// 出发交通
        /// </summary>
        [Column(Name = "DepartureTraffic", DbType = "nvarchar(255)")]
        public string DepartureTraffic
        {
            get;
            set;
        }

        /// <summary>
        /// 返程交通
        /// </summary>
        [Column(Name = "ReturnTraffic", DbType = "nvarchar(255)")]
        public string ReturnTraffic
        {
            get;
            set;
        }


        /// <summary>
        /// 签证资料(统一放在附件表中)
        /// </summary>
       
        public IList<Model.ComStructure.MComAttach> VisaInfoList
        {
            get;
            set;
        }

        /// <summary>
        /// 行程特色
        /// </summary>
        [Column(Name = "TripAdvantage", DbType = "nvarchar(255)")]
        public string TripAdvantage
        {
            get;
            set;
        }

        /// <summary>
        /// 价格备注
        /// </summary>
        [Column(Name = "PathRemark", DbType = "nvarchar(255)")]
        public string PathRemark
        {
            get;
            set;
        }

        #endregion Model

    }


    #region 上团数Model
    /// <summary>
    /// 上团数Model
    /// </summary>
    [Serializable]
    public class MTourOnCount
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode
        {
            get;
            set;
        }


        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime StartOutDate
        {
            get;
            set;
        }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId
        {
            get;
            set;
        }


        /// <summary>
        /// 天数
        /// </summary>
        public int DayCount
        {
            get;
            set;
        }


        /// <summary>
        /// 人数
        /// </summary>
        public MPeopleCountModel PeopleCount
        {
            get;
            set;
        }


        /// <summary>
        /// 支出
        /// </summary>
        public decimal PayMoney
        {
            get;
            set;
        }


        /// <summary>
        /// 收入
        /// </summary>
        public decimal IncomeMoney
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
        /// 毛利
        /// </summary>
        public decimal MaoriMoney
        {
            get;
            set;
        }
    }
    #endregion


    #region 收客Model
    /// <summary>
    /// 收客Model
    /// </summary>
    [Serializable]
    public class MAcceptGuestModel
    {
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime ApplyDate
        {
            get;
            set;
        }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId
        {
            get;
            set;
        }

      

        /// <summary>
        /// 人数
        /// </summary>
        public MPeopleCountModel PeopleCount
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
        /// 团号
        /// </summary>
        public string TourCode
        {
            get;
            set;
        }

        /// <summary>
        /// 客源单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }


        /// <summary>
        /// 客源单位名称
        /// </summary>
        public string BuyCompanyName { get; set; }


        /// <summary>
        /// 客源单位联系人
        /// </summary>
        public string ContactName { get; set; }


        /// <summary>
        /// 客源单位联系电话
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// 销售员Model
        /// </summary>
        public Model.ComStructure.MComUser SellerModel
        {
            get;
            set;
        }
    }
    #endregion


    #region 人数Model
    /// <summary>
    /// 人数组成Model
    /// </summary>
    [Serializable]
    public class MPeopleCountModel
    {
        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultCount
        {
            get;
            set;
        }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildrenCount
        {
            get;
            set;
        }


        /// <summary>
        /// 其他人数
        /// </summary>
        public int OtherCount
        {
            get;
            set;
        }
    }
    #endregion


    #region 列表页面显示Model
    /// <summary>
    /// 线路列表显示Model
    /// </summary>
    [Serializable]
    public class MRouteListModel
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 是否共享
        /// </summary>
        public bool IsShare { get; set; }

        /// <summary>
        /// 线路类型名称
        /// </summary>
        public string RouteTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 线路类型编号
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.AreaType? RouteType
        {
            get;
            set;
        }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int RouteAreaId
        {
            get;
            set;
        }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId
        {
            get;
            set;
        }


        /// <summary>
        /// 天数
        /// </summary>
        public int DayCount
        {
            get;
            set;
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }

        /// <summary>
        /// 发布人
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 发布人列表
        /// </summary>
        public IList<Model.ComStructure.MComUser> UserList
        {
            get;
            set;
        }

        /// <summary>
        /// 上团数
        /// </summary>
        public int STCount
        {
            get;
            set;
        }

        /// <summary>
        /// 收客数
        /// </summary>
        public int SKCount
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion
}
