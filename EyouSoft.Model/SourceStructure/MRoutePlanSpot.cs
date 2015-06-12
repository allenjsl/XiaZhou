using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 线路行程景点
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_RoutePlanSpot")]
    public class MRoutePlanSpot
    {
        /// <summary>
        /// 线路行程编号
        /// </summary>
        [Column(Name = "PlanId", DbType = "char(36)")]
        public string PlanId
        {
            get;
            set;
        }

        /// <summary>
        /// 景点编号
        /// </summary>
        [Column(Name = "SpotId", DbType = "char(36)")]
        public string SpotId
        {
            get;
            set;
        }

        /// <summary>
        /// 景点名称
        /// </summary>
        [Column(Name = "SpotName", DbType = "nvarchar(255)")]
        public string SpotName
        {
            get;
            set;
        }
    }
}
