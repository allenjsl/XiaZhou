using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 线路服务
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_RouteServices")]
    public class MRouteServices
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRouteServices() { }
        #region Model
        private string _routeid;
        private string _excluding;
        private string _shopping;
        private string _children;
        private string _chargeable;
        private string _note;
        private string _warmprompt;
        private string _internal;
        /// <summary>
        /// 线路库ID
        /// </summary>
        [Column(Name = "RouteId", DbType = "char(36)")]
        public string RouteId
        {
            set { _routeid = value; }
            get { return _routeid; }
        }
        /// <summary>
        /// 不含项目
        /// </summary>
        [Column(Name = "Excluding", DbType = "nvarchar(Max)")]
        public string Excluding
        {
            set { _excluding = value; }
            get { return _excluding; }
        }
        /// <summary>
        /// 购物安排
        /// </summary>
        [Column(Name = "Shopping", DbType = "nvarchar(Max)")]
        public string Shopping
        {
            set { _shopping = value; }
            get { return _shopping; }
        }
        /// <summary>
        /// 儿童安排
        /// </summary>
        [Column(Name = "Children", DbType = "nvarchar(Max)")]
        public string Children
        {
            set { _children = value; }
            get { return _children; }
        }
        /// <summary>
        /// 自费项目
        /// </summary>
        [Column(Name = "Chargeable", DbType = "nvarchar(Max)")]
        public string Chargeable
        {
            set { _chargeable = value; }
            get { return _chargeable; }
        }
        /// <summary>
        /// 注意事项
        /// </summary>
        [Column(Name = "Note", DbType = "nvarchar(Max)")]
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 温馨提示
        /// </summary>
        [Column(Name = "WarmPrompt", DbType = "nvarchar(Max)")]
        public string WarmPrompt
        {
            set { _warmprompt = value; }
            get { return _warmprompt; }
        }
        /// <summary>
        /// 内部消息
        /// </summary>
        [Column(Name = "Internal", DbType = "nvarchar(Max)")]
        public string Internal
        {
            set { _internal = value; }
            get { return _internal; }
        }
        #endregion Model

    }
}
