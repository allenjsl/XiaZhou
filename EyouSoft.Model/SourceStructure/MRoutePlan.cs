using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 线路行程安排
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_RoutePlan")]
    public class MRoutePlan
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRoutePlan() { }
        #region Model
        private string _planid;
        private string _routeid;
        private int _days;
        private string _section;
        private string _hotel;

        /// <summary>
        /// 主键编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "PlanId", DbType = "char(36)")]
        public string PlanId
        {
            set { _planid = value; }
            get { return _planid; }
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
        /// 第几天
        /// </summary>
        [Column(Name = "Days", DbType = "int")]
        public int Days
        {
            set { _days = value; }
            get { return _days; }
        }
        /// <summary>
        /// 区间
        /// </summary>
        [Column(Name = "Section", DbType = "nvarchar(255)")]
        public string Section
        {
            set { _section = value; }
            get { return _section; }
        }
        /// <summary>
        /// 交通
        /// </summary>
        [Column(Name = "Traffic", DbType = "nvarchar(255)")]
        public string Traffic
        {
            get;
            set;
        }

        /// <summary>
        /// 住宿
        /// </summary>
        [Column(Name = "Hotel", DbType = "nvarchar(255)")]
        public string Hotel
        {
            set { _hotel = value; }
            get { return _hotel; }
        }
        /// <summary>
        /// 用餐早
        /// </summary>
        [Column(Name = "Breakfast", DbType = "char(1)")]
        public bool Breakfast
        {
            get;
            set;
        }
        /// <summary>
        /// 用餐晚
        /// </summary>
        [Column(Name = "Lunch", DbType = "char(1)")]
        public bool Lunch
        {
            get;
            set;
        }
        /// <summary>
        /// 用餐晚
        /// </summary>
        [Column(Name = "Supper", DbType = "char(1)")]
        public bool Supper
        {
            get;
            set;
        }
        /// <summary>
        /// 行程内容
        /// </summary>
        [Column(Name = "Content", DbType = "nvarchar(Max)")]
        public string Content
        {
            get;set;
        }

        /// <summary>
        /// 图片列表
        /// </summary>
        public IList<Model.ComStructure.MComAttach> PhotoAttachList
        {
            get;
            set;
        }

        /// <summary>
        /// 行程景点
        /// </summary>
        public IList<Model.SourceStructure.MRoutePlanSpot> PlanSpotModel
        {
            get;
            set;
        }
        #endregion Model

    }
}
