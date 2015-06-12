using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 线路包含项目
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_RouteStandard")]
    public class MRouteStandard
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRouteStandard() { }

        #region Model
        private string _routeid;
        private string _standard;

        /// <summary>
        /// 分项项目编号
        /// </summary>
        [Column(Name = "StandardId", IsPrimaryKey = true, DbType = "char(36)")]
        public string StandardId
        {
            get;
            set;
        }

        /// <summary>
        /// 线路编号
        /// </summary>
        [Column(Name = "RouteId", DbType = "char(36)")]
        public string RouteId
        {
            set { _routeid = value; }
            get { return _routeid; }
        }
        /// <summary>
        /// 接待标准
        /// </summary>
        [Column(Name = "Standard", DbType = "varchar(255)")]
        public string Standard
        {
            set { _standard = value; }
            get { return _standard; }
        }
        /// <summary>
        /// 接待标准类型
        /// </summary>
        [Column(Name = "Type", DbType = "tinyint")]
        public Model.EnumType.ComStructure.ContainProjectType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        [Column(Name = "Unit", DbType = "tinyint")]
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit Unit
        {
            get;
            set;
        }

        /// <summary>
        /// 单位报价
        /// </summary>
        [Column(Name = "UnitPrice", DbType = "money")]
        public decimal UnitPrice
        {
            get;
            set;
        }
        #endregion Model

    }
}
