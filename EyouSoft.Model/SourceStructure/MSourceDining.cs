using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商餐馆
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceDining")]
    public class MSourceDining
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceDining() { }
        #region Model
        private string _sourceid;
        private string _diningstandard;
        /// <summary>
        /// 供应商ID
        /// </summary>
        [Column(Name = "SourceId", DbType = "char(36)")]
        public string SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 餐标
        /// </summary>
        [Column(Name = "DiningStandard", DbType = "varchar(255)")]
        public string DiningStandard
        {
            set { _diningstandard = value; }
            get { return _diningstandard; }
        }

        /// <summary>
        /// 是否系统默认导游自定
        /// </summary>
        [Column(Name = "IsSystem", DbType = "char(1)")]
        public bool IsSystem
        {
            get;
            set;
        }
        /// <summary>
        /// 供应商餐馆主体Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商餐馆联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商餐馆菜系List
        /// </summary>
        public IList<Model.SourceStructure.MSourceDiningCuisine> DiningCuisineList
        {
            get;
            set;
        }

        #endregion Model

    }

    #region 用餐信息Model
    /// <summary>
    /// 用餐信息Model
    /// </summary>
    [Serializable]
    public class MDiningInfoModel
    {
        /// <summary>
        /// 用餐时间
        /// </summary>
        public DateTime DiningTime
        {
            get;
            set;
        }


        /// <summary>
        /// 用餐人类别(成人,儿童)
        /// </summary>
        public Model.EnumType.PlanStructure.PlanLargeAdultsType DingingPeopleType
        {
            get;
            set;
        }

        ///// <summary>
        ///// 用餐详细信息(包括,用餐类别,用餐次数,用餐人数,用餐价格)
        ///// </summary>
        //public IList<Model.SourceStructure.MDiningInfo2Model> DiningInfoList
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 用餐类别(早,中,晚)
        /// </summary>
        public Model.EnumType.SourceStructure.DiningType? DiningType
        {
            get;
            set;
        }

        /// <summary>
        /// 用餐次数
        /// </summary>
        public int Frequency
        {
            get;
            set;
        }

        /// <summary>
        /// 用餐人数
        /// </summary>
        public int PeopleCount
        {
            get;
            set;
        }

        /// <summary>
        /// 用餐价格
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

    }
    #endregion
}
