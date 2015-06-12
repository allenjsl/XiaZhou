using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商餐标实体类
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceDiningCuisine")]
    public class MSourceDiningCuisine
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceDiningCuisine() { }
        #region Model
        private string _sourceid;
       
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId",DbType="char(36)")]
        public string SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 菜系
        /// </summary>
        [Column(Name = "Cuisine", DbType = "tinyint")]
        public Model.EnumType.SourceStructure.SourceCuisine? Cuisine
        {
            get;
            set;
        }

        
        #endregion Model

    }
}
