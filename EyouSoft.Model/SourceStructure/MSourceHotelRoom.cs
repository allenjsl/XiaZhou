using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商酒店房型
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceHotelRoom")]
    public class MSourceHotelRoom
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceHotelRoom() { }
        #region Model
        private string _roomid;
        private string _sourceid;
        private string _typename;
        private decimal _priceqt;
        private decimal _pricewl;
        private decimal _pricesk;
        private decimal _pricedj;
        private decimal _pricepj;
        private decimal _pricewj;
        /// <summary>
        /// 房型编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "RoomId",DbType="char(36)")]
        public string RoomId
        {
            set { _roomid = value; }
            get { return _roomid; }
        }
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId", DbType = "char(36)")]
        public string SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 房型名称
        /// </summary>
        [Column(Name = "TypeName", DbType = "nvarchar(50)")]
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 前台销售价
        /// </summary>
        [Column(Name = "PriceQT", DbType = "money")]
        public decimal PriceQT
        {
            set { _priceqt = value; }
            get { return _priceqt; }
        }
        /// <summary>
        /// 网络价
        /// </summary>
        [Column(Name = "PriceWL", DbType = "money")]
        public decimal PriceWL
        {
            set { _pricewl = value; }
            get { return _pricewl; }
        }
        /// <summary>
        /// 散客价
        /// </summary>
        [Column(Name = "PriceSK", DbType = "money")]
        public decimal PriceSK
        {
            set { _pricesk = value; }
            get { return _pricesk; }
        }
        /// <summary>
        /// 团队价淡季
        /// </summary>
        [Column(Name = "PriceDJ", DbType = "money")]
        public decimal PriceDJ
        {
            set { _pricedj = value; }
            get { return _pricedj; }
        }
        /// <summary>
        /// 团队价平季
        /// </summary>
        [Column(Name = "PricePJ", DbType = "money")]
        public decimal PricePJ
        {
            set { _pricepj = value; }
            get { return _pricepj; }
        }
        /// <summary>
        /// 团队价旺季
        /// </summary>
        [Column(Name = "PriceWJ", DbType = "money")]
        public decimal PriceWJ
        {
            set { _pricewj = value; }
            get { return _pricewj; }
        }
        /// <summary>
        /// 是否含早
        /// </summary>
        [Column(Name = "IsBreakfast", DbType = "char(1)")]
        public bool IsBreakfast
        {
            get;
            set;
        }


        #endregion Model

    }
}
