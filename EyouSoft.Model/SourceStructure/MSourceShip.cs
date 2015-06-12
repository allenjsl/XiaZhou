using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商游轮信息
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceShip")]
    public class MSourceShip
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceShip() { }

        #region Model
        private string _sourceid;
        private string _telephone;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private string _routes;
        private string _owerroutes;
        /// <summary>
        /// 供应商ID
        /// </summary>
        [Column(Name = "SourceId",DbType="char(36)")]
        public string SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }

        /// <summary>
        /// 船载电话
        /// </summary>
        [Column(Name = "Telephone", DbType = "nvarchar(50)")]
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }


        /// <summary>
        /// 公司传真
        /// </summary>
        [Column(Name = "Fax", DbType = "varchar(50)")]
        public string Fax
        {
            get;
            set;
        }

        /// <summary>
        /// 价格体系
        /// </summary>
        [Column(Name = "PriceSystem", DbType = "nvarchar(255)")]
        public string PriceSystem
        {
            get;
            set;
        }

        /// <summary>
        /// 开航时间开始
        /// </summary>
        [Column(Name = "StartTime", DbType = "datetime")]
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 开行时间结束
        /// </summary>
        [Column(Name = "EndTime", DbType = "datetime")]
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 游览景点去时航线
        /// </summary>
        [Column(Name = "Routes", DbType = "nvarchar(max)")]
        public string Routes
        {
            set { _routes = value; }
            get { return _routes; }
        }
        /// <summary>
        /// 自费景点去时航线
        /// </summary>
        [Column(Name = "OwerRoutes", DbType = "nvarchar(max)")]
        public string OwerRoutes
        {
            set { _owerroutes = value; }
            get { return _owerroutes; }
        }

        /// <summary>
        /// 供应商游轮主体Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商游轮联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }




        /// <summary>
        /// 旗下游轮列表
        /// </summary>
        public IList<Model.SourceStructure.MSourceSubShip> SubShipList
        {
            get;
            set;
        }

        #endregion Model
    }

    #region 游轮旗下游轮
    /// <summary>
    /// 游轮旗下游轮
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceSubShip")]
    public class MSourceSubShip
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
        /// 旗下游轮编号
        /// </summary>
        [Column(IsPrimaryKey=true,Name = "SubId", DbType = "char(36)")]
        public string SubId
        {
            get;
            set;
        }

        /// <summary>
        /// 游船名称
        /// </summary>
        [Column(Name = "ShipName", DbType = "nvarchar(50)")]
        public string ShipName
        {
            get;
            set;
        }

        /// <summary>
        /// 游船星级(三星,四星,五星)
        /// </summary>
        [Column(Name = "ShipStar", DbType = "tinyint")]
        public Model.EnumType.SourceStructure.ShipStar? ShipStar
        {
            get;
            set;
        }


        /// <summary>
        /// 舱位数
        /// </summary>
        [Column(Name = "ShipSpace", DbType = "int")]
        public int ShipSpace
        {
            get;
            set;
        }

        /// <summary>
        /// 船载电话
        /// </summary>
        [Column(Name = "Telephone", DbType = "varchar(50)")]
        public string Telephone
        {
            get;
            set;
        }


        /// <summary>
        /// 联系人
        /// </summary>
        [Column(Name = "ContactName", DbType = "nvarchar(50)")]
        public string ContactName
        {
            get;
            set;
        }

        /// <summary>
        /// 航线
        /// </summary>
        [Column(Name = "ShipRoute", DbType = "nvarchar(255)")]
        public string ShipRoute
        {
            get;
            set;
        }

        /// <summary>
        /// 游船图片附件Model(统一放在附件表)
        /// </summary>
        public Model.ComStructure.MComAttach AttachModel
        {
            get;
            set;
        }

    }
    #endregion

    #region 游船结算金额Model
    /// <summary>
    /// 游船结算金额Model
    /// </summary>
    [Serializable]
    public class MShipClosingCostModel
    {


        /// <summary>
        /// 编号
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
        /// 带团导游列表
        /// </summary>
        public IList<Model.SourceStructure.MSourceGuide> GuideList
        {
            get;
            set;
        }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 交易金额合计
        /// </summary>
        public decimal TradeMoneySum
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
        /// 明细
        /// </summary>
        public string Detailed
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Seller
        {
            get;
            set;
        }

        /// <summary>
        /// 游船名称
        /// </summary>
        public string ShipName
        {
            get;
            set;
        }


        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal UnPaidCost
        {
            get;
            set;
        }

        /// <summary>
        /// 未付金额合计
        /// </summary>
        public decimal UnPaidCostSum
        {
            get;
            set;
        }

        /// <summary>
        /// 费用明细
        /// </summary>
        public string CostDetail
        {
            get;
            set;
        }
        /// <summary>
        /// 交易人数
        /// </summary>
        public decimal DNum { get; set; }
    }
    #endregion
}
