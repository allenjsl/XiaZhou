using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.YlStructure
{
    #region 附件信息业务实体
    /// <summary>
    /// 附件信息业务实体
    /// </summary>
    public class MFuJianInfo
    {
        /// <summary>
        /// 附件编号
        /// </summary>
        public int FuJianId { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public int LeiXing { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 附件描述
        /// </summary>
        public string MiaoShu { get; set; }
    }
    #endregion

    #region 游轮基础信息
    /// <summary>
    /// 游轮基础信息业务实体
    /// </summary>
    public class MJiChuXinXiInfo
    {
        /// <summary>
        /// 基础信息编号
        /// </summary>
        public int XinXiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 基础信息类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 基础信息名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 使用场景类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing ChangJingLeiXing { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int PaiXuId { get; set; }
        /// <summary>
        /// 名称别名
        /// </summary>
        public string BieMing { get; set; }
        /// <summary>
        /// 父基础信息编号
        /// </summary>
        public int PXinXiId { get; set; }
    }
    #endregion

    #region 游轮公司信息业务实体
    /// <summary>
    /// 游轮公司信息业务实体
    /// </summary>
    public class MGongSiInfo
    {
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 游轮公司名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 游轮公司类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing { get; set; }
        /// <summary>
        /// 简要介绍
        /// </summary>
        public string JianYaoJieShao { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 游轮公司LOGO
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int PaiXuId { get; set; }
        /// <summary>
        /// 优势
        /// </summary>
        public string YouShi { get; set; }
        /// <summary>
        /// 荣誉
        /// </summary>
        public string RongYu { get; set; }
    }
    #endregion

    #region 游轮系列信息业务实体
    /// <summary>
    /// 游轮系列信息业务实体
    /// </summary>
    public class MXiLieInfo
    {
        /// <summary>
        /// 系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 简要介绍
        /// </summary>
        public string JianYaoJieShao { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 公司名称(OUTPUT)
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing GongSiLeiXing { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int PaiXuId { get; set; }
    }
    #endregion

    #region 游轮船只设施信息业务实体
    /// <summary>
    /// 游轮船只设施信息业务实体
    /// </summary>
    public class MChuanZhiSheShiInfo
    {
        /// <summary>
        /// 设施编号
        /// </summary>
        public string SheShiId { get; set; }
        /// <summary>
        /// 设施名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 设施描述
        /// </summary>
        public string MiaoShu { get; set; }
        /*/// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }*/
        /// <summary>
        /// 设施图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MChuanZhiFangXingFuJian> FuJians { get; set; }
    }
    #endregion

    #region 游轮船只房型信息业务实体
    /// <summary>
    /// 游轮船只房型信息业务实体
    /// </summary>
    public class MChuanZhiFangXingInfo
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public string FangXingId { get; set; }
        /// <summary>
        /// 船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 房型名称编号
        /// </summary>
        public string MingChengId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 房间数量
        /// </summary>
        public string ShuLiang { get; set; }
        /// <summary>
        /// 房间面积
        /// </summary>
        public string MianJi { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 房型结构
        /// </summary>
        public string JieGou { get; set; }
        /// <summary>
        /// 床位配置
        /// </summary>
        public string ChuangWeiPeiZhi { get; set; }
        /// <summary>
        /// 客房设施
        /// </summary>
        public string SheShi { get; set; }
        /// <summary>
        /// 用品介绍
        /// </summary>
        public string YongPin { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string JieShao { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MChuanZhiFangXingFuJian> FuJians { get; set; }
        /// <summary>
        /// 房型图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 房型倍数信息集合
        /// </summary>
        public IList<MChuanZhiFangXingBeiShu> BeiShus { get; set; }
    }

    /// <summary>
    /// 船只房型附件
    /// </summary>
    public class MChuanZhiFangXingFuJian:MFuJianInfo
    {
        public string FangXingId { get; set; }
    }
    #endregion

    #region 游轮船只美食信息业务实体
    /// <summary>
    /// 游轮船只美食信息
    /// </summary>
    public class MChuanZhiMeiShiInfo
    {
        /// <summary>
        /// 美食编号
        /// </summary>
        public string MeiShiId { get; set; }
        /// <summary>
        /// 美食名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 美食描述
        /// </summary>
        public string MiaoShu { get; set; }
        /*/// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }*/
        /// <summary>
        /// 美食图片
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MChuanZhiFangXingFuJian> FuJians { get; set; }
    }
    #endregion

    #region 游轮船只信息业务实体
    /// <summary>
    /// 游轮船只信息业务实体
    /// </summary>
    public class MChuanZhiInfo
    {
        /// <summary>
        /// 船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮公司名称(OUTPUT)
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 游轮系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 系列名称(OUTPUT)
        /// </summary>
        public string XiLieMingCheng { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 船只名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 船只名称-英文
        /// </summary>
        public string MingCheng1 { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.XingJi XingJi { get; set; }
        /// <summary>
        /// 总吨位
        /// </summary>
        public string DunWei { get; set; }
        /// <summary>
        /// 下水日期
        /// </summary>
        public string XiaShuiRiQi { get; set; }
        /// <summary>
        /// 装修日期
        /// </summary>
        public string ZhuangXiuRiQi { get; set; }
        /// <summary>
        /// 载客量
        /// </summary>
        public string ZaiKeLiang { get; set; }
        /// <summary>
        /// 甲板楼层
        /// </summary>
        public string JiaBanLouCeng { get; set; }
        /// <summary>
        /// 客房数量
        /// </summary>
        public string KeFangShuLiang { get; set; }
        /// <summary>
        /// 船员
        /// </summary>
        public string ChuanYuan { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public string ChangDu { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string KuangDu { get; set; }
        /// <summary>
        /// 般籍
        /// </summary>
        public string ChuanJi { get; set; }
        /// <summary>
        /// 吃水
        /// </summary>
        public string ChiShui { get; set; }
        /// <summary>
        /// 最高航速
        /// </summary>
        public string ZuiGaoHangSu { get; set; }
        /// <summary>
        /// 简要介绍
        /// </summary>
        public string JianYaoJieShao { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 船载电话
        /// </summary>
        public string ChuanZaiDianHua { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 平面图信息集合
        /// </summary>
        public IList<MFuJianInfo> PingMianTus { get; set; }
        /// <summary>
        /// 设施信息集合
        /// </summary>
        public IList<MChuanZhiSheShiInfo> SheShis { get; set; }
        /// <summary>
        /// 房型信息集合
        /// </summary>
        public IList<MChuanZhiFangXingInfo> FangXings { get; set; }
        /// <summary>
        /// 美食信息集合
        /// </summary>
        public IList<MChuanZhiMeiShiInfo> MeiShis { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing GongSiLeiXing { get; set; }
        /// <summary>
        /// 房型倍数信息集合
        /// </summary>
        public IList<MChuanZhiFangXingBeiShu> BeiShus { get; set; }
        /// <summary>
        /// 航期编号
        /// </summary>
        public string HangQiId { get; set; }
        /// <summary>
        /// 日期编号
        /// </summary>
        public string RiQiId { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public Model.EnumType.YlStructure.YouLunLeiXing YouLunLeiXing { get; set; }
    }
    #endregion

    #region 游轮船只基础价业务实体
    /// <summary>
    /// 游轮基础价
    /// </summary>
    [Serializable]
    public class MChuanZhiBasePrice
    {
        #region Model
        private string _id;
        private string _chuanzhiid;
        private int _countryid = 0;
        private decimal _baseprice = 0M;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 船只编号
        /// </summary>
        public string ChuanZhiId
        {
            set { _chuanzhiid = value; }
            get { return _chuanzhiid; }
        }
        /// <summary>
        /// 国籍编号
        /// </summary>
        public int CountryId
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 基础价
        /// </summary>
        public decimal BasePrice
        {
            set { _baseprice = value; }
            get { return _baseprice; }
        }
        /// <summary>
        /// 航线编号
        /// </summary>
        public int HangXianId { get; set; }
        #endregion Model

    }
    #endregion

    #region 游轮房型倍数业务实体
    /// <summary>
    /// 游轮房型倍数业务实体
    /// </summary>
    [Serializable]
    public class MChuanZhiFangXingBeiShu
    {
        #region Model
        private string _id;
        private string _fangxingid;
        private int _renyuanleixing;
        private decimal _beishu = 0M;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string FangXingId
        {
            set { _fangxingid = value; }
            get { return _fangxingid; }
        }
        /// <summary>
        /// 人员类型编号
        /// </summary>
        public int RenYuanLeiXing
        {
            set { _renyuanleixing = value; }
            get { return _renyuanleixing; }
        }
        /// <summary>
        /// 占床倍数
        /// </summary>
        public decimal BeiShu
        {
            set { _beishu = value; }
            get { return _beishu; }
        }
        /// <summary>
        /// 不占床倍数
        /// </summary>
        public decimal BeiShu1 { get; set; }
        /// <summary>
        /// 加床倍数
        /// </summary>
        public decimal BeiShu2 { get; set; }
        #endregion Model

    }
    #endregion

    #region 游轮目的地信息业务实体
    /// <summary>
    /// 游轮目的地信息业务实体
    /// </summary>
    public class MMuDiDiInfo
    {
        /// <summary>
        /// 目的地编号
        /// </summary>
        public string MuDiDiId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 目的地名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
    }
    #endregion

    #region 游轮视频信息业务实体
    /// <summary>
    /// 游轮视频信息业务实体
    /// </summary>
    public class MShiPinInfo
    {
        /// <summary>
        /// 视频编号
        /// </summary>
        public string ShiPinId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮公司名称
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 游轮系列编号
        /// </summary>
        public string XiLieId { get; set; }
        /// <summary>
        /// 游轮系列名称
        /// </summary>
        public string XiLieMingCheng { get; set; }
        /// <summary>
        /// 游轮船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
        /// <summary>
        /// 游轮船只名称
        /// </summary>
        public string ChuanZhiMingCheng { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 视频链接
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string XiangXiJieShao { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 视频预览图片
        /// </summary>
        public string ShiPinIMG { get; set; }
    }
    #endregion



    #region 游轮基础信息查询实体
    /// <summary>
    /// 游轮基础信息查询实体
    /// </summary>
    public class MJiChuXinXiChaXunInfo
    {
        /// <summary>
        /// 基础信息类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 游轮类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? YouLunLeiXing { get; set; }
    }
    #endregion

    #region 游轮公司信息查询实体
    /// <summary>
    /// MGongSiChaXunInfo
    /// </summary>
    public class MGongSiChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? GongSiLeiXing { get; set; }
    }
    #endregion

    #region 游轮系列信息查询实体
    /// <summary>
    /// 游轮系列信息查询实体
    /// </summary>
    public class MXiLieChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string XiLieMingCheng { get; set; }
        /// <summary>
        /// 游轮公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 游轮公司类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? GongSiLeiXing { get; set; }
    }
    #endregion

    #region 游轮船只信息查询实体
    /// <summary>
    /// 游轮船只信息查询实体
    /// </summary>
    public class MChuanZhiChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string XiLieMingCheng { get; set; }
        /// <summary>
        /// 船只名称
        /// </summary>
        public string ChuanZhiMingCheng { get; set; }
        /// <summary>
        /// 系列编号
        /// </summary>
        public string XiLieId { get; set; }
    }
    #endregion

    #region 游轮目的地信息查询实体
    /// <summary>
    /// 游轮目的地信息查询实体
    /// </summary>
    public class MMuDiDiChaXunInfo
    {

    }
    #endregion

    #region 游轮视频信息查询实体
    /// <summary>
    /// 游轮视频信息查询实体
    /// </summary>
    public class MShiPinChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiMingCheng { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string XiLieMingCheng { get; set; }
        /// <summary>
        /// 船只名称
        /// </summary>
        public string ChuanZhiMingCheng { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string ShiPinMingCheng { get; set; }
        /// <summary>
        /// 船只编号
        /// </summary>
        public string ChuanZhiId { get; set; }
    }
    #endregion
}
