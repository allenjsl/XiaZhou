using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GysStructure
{
    #region 供应商选用信息业务实体
    /// <summary>
    /// 供应商选用信息业务实体
    /// </summary>
    public class MXuanYongInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType GysLeiXing { get; set; }
        /// <summary>
        /// 是否返利
        /// </summary>
        public bool IsFanLi { get; set; }
        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsQianDan { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<MLxrInfo> Lxrs { get; set; }
        /// <summary>
        /// 酒店前台电话
        /// </summary>
        public string JiuDianQianTaiTelephone { get; set; }
    }
    #endregion

    #region 供应商选用查询信息业务实体
    /// <summary>
    /// 供应商选用查询信息业务实体
    /// </summary>
    public class MXuanYongChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongChaXunInfo() { }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType? GysLeiXing { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// 景点-景点名称
        /// </summary>
        public string JingDianName { get; set; }
        /// <summary>
        /// 是否包含联系人信息
        /// </summary>
        public bool IsLxr { get; set; }
    }
    #endregion

    #region 景点选用信息业务实体
    /// <summary>
    /// 景点选用信息业务实体
    /// </summary>
    public class MXuanYongJingDianInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongJingDianInfo() { }
        /// <summary>
        /// 景点编号
        /// </summary>
        public string JingDianId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 景点描述
        /// </summary>
        public string MiaoShu { get; set; }
    }
    #endregion
}
