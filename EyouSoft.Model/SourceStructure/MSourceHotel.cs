using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商酒店
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceHotel")]
    public class MSourceHotel
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceHotel() { }
        #region Model

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId", DbType = "char(36)")]
        public string SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// 星级
        /// </summary>
        [Column(Name = "Star", DbType = "tinyint")]
        public Model.EnumType.SourceStructure.HotelStar? Star
        {
            set;
            get;
        }

        /// <summary>
        /// 前台电话
        /// </summary>
        [Column(Name = "ReceptionTel", DbType = "varchar(50)")]
        public string ReceptionTel
        {
            get;
            set;
        }


        /// <summary>
        /// 供应商酒店主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商酒店房型List
        /// </summary>
        public IList<Model.SourceStructure.MSourceHotelRoom> HotelRoomList
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商酒店联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }


        /// <summary>
        /// 图片
        /// </summary>
        public IList<Model.ComStructure.MComAttach> AttachList
        {
            get;
            set;
        }


        #endregion Model

    }
}
