using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
    /// 供应商景点
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceSpot")]
    public class MSourceSpot
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceSpot() { }

        #region Model
        /// <summary>
        /// 供应商景点主体信息Model
        /// </summary>
        public Model.SourceStructure.MSource SourceModel
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商景点联系人List
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

        /// <summary>
        /// 景点价格信息列表
        /// </summary>
        public IList<Model.SourceStructure.MSpotPriceSystemModel> PriceSystemList
        {
            get;
            set;
        }

        #endregion Model
    }

    #region 景点价格体系Model
    /// <summary>
    /// 景点价格体系Model
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SourceSpotPriceSystem")]
    public class MSpotPriceSystemModel
    {
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
        /// 挂牌价
        /// </summary>
        [Column(Name = "PriceGP", DbType = "money")]
        public decimal PriceGP
        {
            get;
            set;
        }

        /// <summary>
        /// 散客价
        /// </summary>
        [Column(Name = "PriceSK", DbType = "money")]
        public decimal PriceSK
        {
            get;
            set;
        }


        /// <summary>
        /// 团队价
        /// </summary>
        [Column(Name = "PriceTD", DbType = "money")]
        public decimal PriceTD
        {
            get;
            set;
        }

        /// <summary>
        /// 儿童价
        /// </summary>
        [Column(Name = "PriceRT", DbType = "money")]
        public decimal PriceRT
        {
            get;
            set;
        }

        /// <summary>
        /// 60-70老人价
        /// </summary>
        [Column(Name = "PriceLR1", DbType = "money")]
        public decimal PriceLR1
        {
            get;
            set;
        }

        /// <summary>
        /// 70老人价
        /// </summary>
        [Column(Name = "PriceLR2", DbType = "money")]
        public decimal PriceLR2
        {
            get;
            set;
        }

        /// <summary>
        /// 学生价
        /// </summary>
        [Column(Name = "PriceXS", DbType = "money")]
        public decimal PriceXS
        {
            get;
            set;
        }


        /// <summary>
        /// 军人价
        /// </summary>
        [Column(Name = "PriceJR", DbType = "money")]
        public decimal PriceJR
        {
            get;
            set;
        }

        /// <summary>
        /// 导游词
        /// </summary>
        [Column(Name = "GuideWord", DbType = "nvarchar(max)")]
        public string GuideWord
        {
            get;
            set;
        }

        /// <summary>
        /// 星级
        /// </summary>
        [Column(Name = "Star", DbType = "tinyint")]
        public Model.EnumType.SourceStructure.SpotStar? Star
        {
            get;
            set;
        }

        /// <summary>
        /// 景点名称
        /// </summary>
        [Column(Name = "SpotName", DbType = "nvarchar(50)")]
        public string SpotName
        {
            get;
            set;
        }

        /// <summary>
        /// 主键(自增列)
        /// </summary>
        [Column(Name = "Id", DbType = "char(36)")]
        public string Id
        {
            get;
            set;
        }
    }
    #endregion
}
