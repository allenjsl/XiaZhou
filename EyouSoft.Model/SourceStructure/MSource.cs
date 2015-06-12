using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    #region 供应商实体类
    /// <summary>
    /// 供应商实体类
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_Source")]
    public class MSource
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSource() { }

        #region 供应商主体
        #region Model
        private string _sourceid;
        private string _name;
        private string _companyid;
        private int _countryid;
        private int _provinceid;
        private int _cityid;
        private int _countyid;
        private Model.EnumType.SourceStructure.SourceType _type;
        private string _address;
        private string _agreementfile;
        private int _tradenum;
        private string _unitpolicy;
        private string _remark;
        private string _desc;
        private string _licensekey;
        private string _userid;
        private string _operatorid;
        private DateTime _issuetime;

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "SourceId", DbType = "char(36)")]
        public string SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column(Name = "Name", DbType = "nvarchar(255)")]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 国家ID
        /// </summary>
        [Column(Name = "CountryId", DbType = "int")]
        public int CountryId
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 省份编号
        /// </summary>
        [Column(Name = "ProvinceId", DbType = "int")]
        public int ProvinceId
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
        /// <summary>
        /// 城市编号
        /// </summary>
        [Column(Name = "CityId", DbType = "int")]
        public int CityId
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
        /// <summary>
        /// 县区ID
        /// </summary>
        [Column(Name = "CountyId", DbType = "int")]
        public int CountyId
        {
            set { _countyid = value; }
            get { return _countyid; }
        }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [Column(Name = "Type", DbType = "tinyint")]
        public Model.EnumType.SourceStructure.SourceType Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        [Column(Name = "Address", DbType = "nvarchar(255)")]
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 是否返佣
        /// </summary>
        [Column(Name = "IsCommission", DbType = "char(1)")]
        public bool IsCommission
        {
            get;
            set;
        }
        /// <summary>
        /// 合作协议
        /// </summary>
        [Column(Name = "AgreementFile", DbType = "nvarchar(255)")]
        public string AgreementFile
        {
            set { _agreementfile = value; }
            get { return _agreementfile; }
        }
        /// <summary>
        /// 交易次数
        /// </summary>
        [Column(Name = "TradeNum", DbType = "int")]
        public int TradeNum
        {
            set { _tradenum = value; }
            get { return _tradenum; }
        }
        /// <summary>
        /// 政策
        /// </summary>
        [Column(Name = "UnitPolicy", DbType = "nvarchar(max)")]
        public string UnitPolicy
        {
            set { _unitpolicy = value; }
            get { return _unitpolicy; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(Name = "Remark", DbType = "nvarchar(max)")]
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 供应商介绍
        /// </summary>
        [Column(Name = "Desc", DbType = "nvarchar(max)")]
        public string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        /// <summary>
        /// 许可证号
        /// </summary>
        [Column(Name = "LicenseKey", DbType = "nvarchar(50)")]
        public string LicenseKey
        {
            set { _licensekey = value; }
            get { return _licensekey; }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Column(Name = "UserId", DbType = "char(36)")]
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
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
        /// 操作人
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


        ///// <summary>
        ///// 返利
        ///// </summary>
        //[Column(Name = "Rebate", DbType = "char(1)")]
        //public string Rebate
        //{
        //    get;
        //    set;
        //}

        #region 2012-2-27
        /// <summary>
        /// 是否推荐
        /// </summary>
        [Column(Name = "IsRecommend", DbType = "char(1)")]
        public bool IsRecommend
        {
            get;
            set;
        }

        /// <summary>
        /// 是否签单
        /// </summary>
        [Column(Name = "IsPermission", DbType = "char(1)")]
        public bool IsPermission
        {
            get;
            set;
        }

        /// <summary>
        /// 最后维护人
        /// </summary>
        [Column(Name = "LastModifierId", DbType = "char(36)")]
        public string LastModifierId
        {
            get;
            set;
        }

        /// <summary>
        /// 最后维护时间
        /// </summary>
        [Column(Name = "LastModifyTime", DbType = "datetime")]
        public DateTime? LastModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否签订合同
        /// </summary>
        [Column(Name = "IsSignContract", DbType = "char(1)")]
        public bool IsSignContract
        {
            get;
            set;
        }

        /// <summary>
        /// 合同号
        /// </summary>
        [Column(Name = "ContractCode",DbType="varchar(50)")]
        public string ContractCode
        {
            get;
            set;
        }

        /// <summary>
        /// 合同期限开始
        /// </summary>
        [Column(Name = "ContractPeriodStart", DbType = "datetime")]
        public DateTime? ContractPeriodStart
        {
            get;
            set;
        }


        /// <summary>
        /// 合同期限结束
        /// </summary>
        [Column(Name = "ContractPeriodEnd", DbType = "datetime")]
        public DateTime? ContractPeriodEnd
        {
            get;
            set;
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        [Column(Name="DeptId",DbType="int")]
        public int DeptId
        {
            get;
            set;
        }

        /// <summary>
        /// 合同附件
        /// </summary>
        public Model.ComStructure.MComAttach ContractAttach
        {
            get;
            set;
        }

        /// <summary>
        /// 是否返单
        /// </summary>
        [Column(Name = "IsBackSingle", DbType = "char(1)")]
        public bool IsBackSingle
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 联系人列表(金蝶那边使用)
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }
        #endregion Model
        #endregion
    }
    #endregion

    #region 地接社线路区域
    /// <summary>
    /// 地接社线路区域
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_TravelRouteArea")]
    public class MSourceTravelRouteArea
    {
        /// <summary>
        /// 地接社编号
        /// </summary>
        [Column(Name = "SourceId",DbType="char(36)")]
        public string SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        [Column(Name = "RouteAreaId", DbType = "int")]
        public int RouteAreaId
        {
            get;
            set;
        }

        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string RouteAreaName
        {
            get;
            set;
        }
    }
    #endregion

    #region 地接社列表未付金额Model
    /// <summary>
    /// 地接社列表未付金额Model
    /// </summary>
    [Serializable]
    public class MTravelUnPaidCostModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id
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
        /// 团队编号
        /// </summary>
        public string TourId
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
        /// 人数组成Model(包含成人数,儿童数)
        /// </summary>
        public Model.SourceStructure.MPeopleCountModel PeopleCountModel
        {
            get;
            set;
        }


        /// <summary>
        /// 结算费用
        /// </summary>
        public decimal ClosingCost
        {
            get;
            set;
        }


        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal UnpaidCost
        {
            get;
            set;
        }

        /// <summary>
        /// 计调员Model
        /// </summary>
        public Model.ComStructure.MComUser PlanerModel
        {
            get;
            set;
        }


        /// <summary>
        /// 导游信息Model
        /// </summary>
        public IList<Model.SourceStructure.MSourceGuide> GuideList
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string Seller
        {
            get;
            set;
        }
    }
    #endregion

    #region 地接社查询页面显示Model
    [Serializable]
    public class MSourceTravel
    {
        /// <summary>
        /// 地接社主体信息Modle
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人列表信息
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商地接社Model(电话,法人代表,特色线路)
        /// </summary>
        public Model.SourceStructure.MSourceTravelInfo SourceTravelModel
        {
            get;
            set;
        }


    }
    #endregion

    #region 供应商地接社信息
    /// <summary>
    /// 供应商地接社信息
    /// </summary>
    [Table(Name = "tbl_SourceTravel")]
    public class MSourceTravelInfo
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId",DbType="char(36)")]
        public string SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// 电话
        /// </summary>
        [Column(Name = "Telephone",DbType="varchar(50)")]
        public string Telephone
        {
            get;
            set;
        }

         /// <summary>
        /// 法人代表
        /// </summary>
        [Column(Name = "LegalRepresentative",DbType="nvarchar(50)")]
        public string LegalRepresentative
        {
            get;
            set;
        }

          /// <summary>
        /// 特色线路
        /// </summary>
        [Column(Name = "Routes",DbType="nvarchar(max)")]
        public string Routes
        {
            get;
            set;
        }
    }
    #endregion

    #region 车队查询页面显示Model
    /// <summary>
    /// 车队Model
    /// </summary>
    [Serializable]
    public class MSourceMotorcade
    {
        /// <summary>
        /// 供应商车队主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商车队车辆信息ModelList
        /// </summary>
        public IList<Model.SourceStructure.MSourceCar> CarList
        {
            get;
            set;
        }


        /// <summary>
        /// 供应商车队联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }
    }
    #endregion

    #region 票务查询页面显示Model
    /// <summary>
    /// 票务显示Model
    /// </summary>
    [Serializable]
    public class MSourceTicket
    {
        /// <summary>
        /// 供应商票务主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商票务联系人ModelList
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }
    }
    #endregion

    #region 其他显示Model
    /// <summary>
    /// 其他Model
    /// </summary>
    [Serializable]
    public class MSourceOther
    {
        /// <summary>
        /// 供应商其他主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }


        /// <summary>
        /// 供应商其他联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }

        /// <summary>
        /// 其他类别列表
        /// </summary>
        public IList<Model.SourceStructure.MSourceOtherType> OtherTypeList
        {
            get;
            set;
        }

    }
    #endregion

    #region 其他类别Model
    /// <summary>
    /// 其他类别Model
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceOther")]
    public class MSourceOtherType
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Column(Name = "OtherId", DbType = "char(36)")]
        public string OtherId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId",DbType="char(36)")]
        public string SouceId
        {
            get;
            set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        [Column(Name = "Price", DbType = "money")]
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 结算价
        /// </summary>
        [Column(Name = "ClosingCost", DbType = "money")]
        public decimal ClosingCost
        {
            get;
            set;
        }

        /// <summary>
        /// 简要描述
        /// </summary>
        [Column(Name = "Desc", DbType = "nvarchar(max)")]
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(Name = "Name", DbType = "nvarchar(50)")]
        public string Name
        {
            get;
            set;
        }
    }
    #endregion

    #region 资源管理列表公共实体
    /// <summary>
    /// 资源管理列表公共实体
    /// </summary>
    [Serializable]
    public class MSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MSourceBase(){}

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend{get;set;}

        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsPermission{get;set;}

        /// <summary>
        /// 是否返佣
        /// </summary>
        public bool IsCommission { get; set; }

        /// <summary>
        /// 是否返单
        /// </summary>
        public bool IsBackSingle { get; set; }

        /// <summary>
        /// 是否签订合同
        /// </summary>
        public bool IsSignContract { get; set; }

        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
#endregion
}
