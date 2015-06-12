using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商购物
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceShop")]
    public class MSourceShop
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceShop() { }

        #region Model
        private string _sourceid;
       
        
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
        /// 销售商品
        /// </summary>
        [Column(Name = "SellType", DbType = "nvarchar(255)")]
        public string SellType
        {
            get;
            set;
        }


        /// <summary>
        /// 供应商购物主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商联系人List
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList
        {
            get;
            set;
        }
        #endregion Model
    }
}
