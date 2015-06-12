using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
     /// <summary>
        /// 供应商车辆实体类
        /// </summary>
        [Serializable]
        [Table(Name="tbl_SourceCar")]
        public class MSourceCar
        {
            /// <summary>
            /// default constructor
            /// </summary>
            public MSourceCar() { }
            #region Model
            private string _carid;
            private string _sourceid;
            private string _typename;
            private int _seatnumber;
            private string _carnumber;
            private string _driver;
            private string _drivertel;
            private decimal? _insuranceamount;
            
            /// <summary>
            /// 车辆编号
            /// </summary>
            [Column(Name="CarId",IsPrimaryKey=true,DbType="char(36)")]
            public string CarId
            {
                set { _carid = value; }
                get { return _carid; }
            }
            /// <summary>
            /// 供应商编号
            /// </summary>
            [Column(Name="SourceId",DbType="char(36)")]
            public string SourceId
            {
                set { _sourceid = value; }
                get { return _sourceid; }
            }
            /// <summary>
            /// 车型名称
            /// </summary>
            [Column(Name="TypeName",DbType="nvarchar(255)")]
            public string TypeName
            {
                set { _typename = value; }
                get { return _typename; }
            }
            /// <summary>
            /// 座位数
            /// </summary>
            [Column(Name="SeatNumber",DbType="int")]
            public int SeatNumber
            {
                set { _seatnumber = value; }
                get { return _seatnumber; }
            }
            /// <summary>
            /// 车号
            /// </summary>
            [Column(Name="CarNumber",DbType="nvarchar(50)")]
            public string CarNumber
            {
                set { _carnumber = value; }
                get { return _carnumber; }
            }
            /// <summary>
            /// 司机
            /// </summary>
            [Column(Name="Driver",DbType="nvarchar(50)")]
            public string Driver
            {
                set { _driver = value; }
                get { return _driver; }
            }
            /// <summary>
            /// 司机电话
            /// </summary>
            [Column(Name="DriverTel",DbType="nvarchar(50)")]
            public string DriverTel
            {
                set { _drivertel = value; }
                get { return _drivertel; }
            }
            /// <summary>
            /// 是否保险
            /// </summary>
            [Column(Name = "IsInsurance", DbType = "char(1)")]
            public bool IsInsurance
            {
                get;
                set;
            }
            /// <summary>
            /// 保险金额
            /// </summary>
            [Column(Name="InsuranceAmount",DbType="money")]
            public decimal? InsuranceAmount
            {
                set { _insuranceamount = value; }
                get { return _insuranceamount; }
            }
            /// <summary>
            /// 保险期限
            /// </summary>
            [Column(Name="InsuranceLimit",DbType="datetime")]
            public DateTime? InsuranceLimit
            {
                get;set;
            }

            
            #endregion Model

        }

        
}
