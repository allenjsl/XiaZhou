//供应商列表相关信息业务实体
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GysStructure
{
    #region 供应商列表公共信息业务实体
    /// <summary>
    /// 供应商列表公共信息业务实体
    /// </summary>
    public class MLBInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 国家省份城市县区
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 交易明细
        /// </summary>
        public MJiaoYiXXInfo JiaoYiXX { get; set; }
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
        /// 联系人姓名
        /// </summary>
        public string LxrName
        {
            get
            {
                if (Lxrs != null && Lxrs.Count > 0)
                {
                    return Lxrs[0].Name;
                }

                return string.Empty;
            }
        }
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 供应商查询信息业务实体
    /// <summary>
    /// 供应商查询信息业务实体
    /// </summary>
    public class MLBChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBChaXunInfo() { }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 酒店-星级
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.HotelStar? JiuDianXingJi { get; set; }
        /// <summary>
        /// 餐馆菜系
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.SourceCuisine? CanGuanCaiXi { get; set; }
        /// <summary>
        /// 游轮-游船名称
        /// </summary>
        public string YouLunYouChuanName { get; set; }
        /// <summary>
        /// 车队-车型名称
        /// </summary>
        public string CheDuiCheXingName { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
    }
    #endregion    

    #region 供应商-地接社列表信息业务实体
    /// <summary>
    /// 供应商-地接社列表信息业务实体
    /// </summary>
    public class MLBDiJieSheInfo : MLBInfo
    {
        /// <summary>
        /// 供应商账号信息实体
        /// </summary>
        public MGysUserInfo UserInfo { get; set; }
    }
    #endregion

    #region 供应商-酒店列表信息业务实体
    /// <summary>
    /// 供应商-酒店列表信息业务实体
    /// </summary>
    public class MLBJiuDianInfo : MLBInfo
    {
        /// <summary>
        /// 星级
        /// </summary>
        public EyouSoft.Model.EnumType.SourceStructure.HotelStar XingJi { get; set; }
        /// <summary>
        /// 前台电话
        /// </summary>
        public string QianTaiTelephone { get; set; }
        /// <summary>
        /// 房型信息集合
        /// </summary>
        public IList<MJiuDianFangXingInfo> FangXings { get; set; }
        /// <summary>
        /// 团队价平季-最小值
        /// </summary>
        public decimal MinJiaGePJ
        {
            get
            {
                decimal? _min = null;
                if (FangXings == null || FangXings.Count == 0) return 0;

                foreach (var item in FangXings)
                {
                    if (!_min.HasValue) { _min = item.JiaGePJ; continue; }
                    if (item.JiaGePJ < _min) _min = item.JiaGePJ;
                }

                if (!_min.HasValue) _min = 0;
                return _min.Value;
            }
        }
        /// <summary>
        /// 团队价平季-最大值
        /// </summary>
        public decimal MaxJiaGePJ
        {
            get
            {
                decimal? _max = null;
                if (FangXings == null || FangXings.Count == 0) return 0;

                foreach (var item in FangXings)
                {
                    if (!_max.HasValue) { _max = item.JiaGePJ; continue; }
                    if (item.JiaGePJ > _max) _max = item.JiaGePJ;
                }

                if (!_max.HasValue) _max = 0;
                return _max.Value;
            }
        }
    }
    #endregion

    #region 供应商-餐馆列表信息业务实体
    /// <summary>
    /// 供应商-餐馆列表信息业务实体
    /// </summary>
    public class MLBCanGuanInfo : MLBInfo
    {
        /// <summary>
        /// 餐标
        /// </summary>
        public string CanBiao { get; set; }
        /// <summary>
        /// 菜系
        /// </summary>
        public IList<EyouSoft.Model.EnumType.SourceStructure.SourceCuisine> CaiXis { get; set; }
        /// <summary>
        /// 获取菜系名称，逗号间隔
        /// </summary>
        public string CaiXiNames
        {
            get
            {
                if (CaiXis == null || CaiXis.Count == 0) return string.Empty;

                string s = string.Empty;
                foreach (var item in CaiXis)
                {
                    if (item == EyouSoft.Model.EnumType.SourceStructure.SourceCuisine.未选择) continue;
                    s += item.ToString() + ",";
                }

                if (!string.IsNullOrEmpty(s)) s = s.TrimEnd(',');

                return s;
            }
        }
    }
    #endregion

    #region 供应商-景点列表信息业务实体
    /// <summary>
    /// 供应商-景点列表信息业务实体
    /// </summary>
    public class MLBJingDianInfo : MLBInfo
    {
        /// <summary>
        /// 是否返单
        /// </summary>
        public bool IsFanDan { get; set; }
        /// <summary>
        /// 景点信息集合
        /// </summary>
        public IList<MJingDianJingDianInfo> JingDians { get; set; }
        /// <summary>
        /// 第一个景点挂牌价格
        /// </summary>
        public decimal JiaGeGP1
        {
            get
            {
                if (JingDians == null || JingDians.Count == 0) return 0;

                return JingDians[0].JiaGeGP;
            }
        }
    }
    #endregion

    #region 供应商-游轮列表信息业务实体
    /// <summary>
    /// 供应商-游轮列表信息业务实体
    /// </summary>
    public class MLBYouLunInfo : MLBInfo
    {
        /// <summary>
        /// 游船信息集合
        /// </summary>
        public IList<MYouLunYouChuanInfo> YouChuans { get; set; }
        /// <summary>
        /// 第一个游船名称
        /// </summary>
        public string YouChuanName1
        {
            get
            {
                if (YouChuans == null || YouChuans.Count == 0) return string.Empty;
                return YouChuans[0].Name;
            }
        }
    }
    #endregion

    #region 供应商-车队列表信息业务实体
    /// <summary>
    /// 供应商-车队列表信息业务实体
    /// </summary>
    public class MLBCheDuiInfo : MLBInfo
    {
    }
    #endregion

    #region 供应商-票务列表信息业务实体
    /// <summary>
    /// 供应商-票务列表信息业务实体
    /// </summary>
    public class MLBPiaoWuInfo : MLBInfo
    {
        /// <summary>
        /// 政策
        /// </summary>
        public string ZhengCe { get; set; }
    }
    #endregion

    #region 供应商-购物列表信息业务实体
    /// <summary>
    /// 供应商-购物列表信息业务实体
    /// </summary>
    public class MLBGouWuInfo : MLBInfo
    {
        /// <summary>
        /// 商品类别
        /// </summary>
        public string ShangPinLeiBie { get; set; }
    }
    #endregion

    #region 供应商-其他列表信息业务实体
    /// <summary>
    /// 供应商-其他列表信息业务实体
    /// </summary>
    public class MLBQiTaInfo : MLBInfo
    {

    }
    #endregion
}
