// 游轮基础信息interface 汪奇志 2014-03-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// 游轮基础信息interface
    /// </summary>
    public interface IJiChuXinXi
    {
        /// <summary>
        /// 写入、修改基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int JiChuXinXi_CU(EyouSoft.Model.YlStructure.MJiChuXinXiInfo info);
        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MJiChuXinXiInfo GetJiChuXinXiInfo(int jiChuXinXiId);
        /// <summary>
        /// 删除基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        int DeleteJiChuXinXi(string companyId, int jiChuXinXiId);
        /// <summary>
        /// 获取基础信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MJiChuXinXiInfo> GetJiChuXinXis(string companyId, EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo chaXun);


        /// <summary>
        /// 写入、修改游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int GongSi_CU(EyouSoft.Model.YlStructure.MGongSiInfo info);
        /// <summary>
        /// 获取游轮公司信息
        /// </summary>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MGongSiInfo GetGongSiInfo(string gongSiId);
        /// <summary>
        /// 删除游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        int DeleteGongSi(string companyId, string gongSiId);
        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun);
        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun);

        /// <summary>
        /// 写入、修改游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int XiLie_CU(EyouSoft.Model.YlStructure.MXiLieInfo info);
        /// <summary>
        /// 获取游轮系列信息
        /// </summary>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MXiLieInfo GetXiLieInfo(string xiLieId);
        /// <summary>
        /// 删除游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        int DeleteXiLie(string companyId, string xiLieId);
        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun);
        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun);

        /// <summary>
        /// 写入、修改游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int ChuanZhi_CU(EyouSoft.Model.YlStructure.MChuanZhiInfo info);
        /// <summary>
        /// 获取游轮船只信息
        /// </summary>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MChuanZhiInfo GetChuanZhiInfo(string chuanZhiId);
        /// <summary>
        /// 根据房型编号获取船只房型附件信息集合
        /// </summary>
        /// <param name="fangxingId">房型编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> GetChuanZhiFangXingFuJian(string fangxingId);
        /// <summary>
        /// get chuanzhi meishis
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> GetChuanZhiMeiShis(string chuanZhiId);
        /// <summary>
        /// get chuanzhi sheshis
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> GetChuanZhiSheShis(string chuanZhiId);
        /// <summary>
        /// 根据出港日期和航线编号获取船只信息
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="riqi">出港日期</param>
        /// <param name="hangxianid">航线编号</param>
        /// <returns>船只列表</returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhiInfo(string companyid, DateTime riqi, int hangxianid);
        /// <summary>
        /// 删除游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        int DeleteChuanZhi(string companyId, string chuanZhiId);
        /// <summary>
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun);
        /// <summary>
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun);

        /*/// <summary>
        /// 写入游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info);
        /// <summary>
        /// 修改游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info);
        /// <summary>
        /// 获取游轮目的地信息
        /// </summary>
        /// <param name="muDiDiId">目的地编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MMuDiDiInfo GetMuDiDiInfo(string muDiDiId);
        /// <summary>
        /// 删除游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="muDiDiId">目的地编号</param>
        /// <returns></returns>
        int DeleteMuDiDi(string companyId, string muDiDiId);
        /// <summary>
        /// 获取游轮目的地信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MMuDiDiInfo> GetMuDiDis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MMuDiDiChaXunInfo chaXun);*/
        

        /// <summary>
        /// 写入游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info);
        /// <summary>
        /// 修改游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info);
        /// <summary>
        /// 获取游轮视频信息
        /// </summary>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MShiPinInfo GetShiPinInfo(string shiPinId);
        /// <summary>
        /// 删除游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        int DeleteShiPin(string companyId, string shiPinId);
        /// <summary>
        /// 获取游轮视频信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MShiPinInfo> GetShiPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MShiPinChaXunInfo chaXun);
        /// <summary>
        /// 根据船只编号获取基础价格列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> GetBasePrice(string chuanzhiId);
        /// <summary>
        /// 根据船只编号和基础价格列表新增、修改
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="l">基础价格列表</param>
        /// <returns></returns>
        int BasePrice(string chuanzhiId, IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> l);
        /// <summary>
        /// 根据船只编号和附件类型新增、修改船只房型、美食、设施附件列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="t">附件类型【0：房型 1：美食 2：设施】</param>
        /// <param name="l">船只房型、美食、设施附件列表</param>
        /// <returns></returns>
        int ChuanZhiFangXingFuJian_M(string chuanzhiId,int t, IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> l);
    }
}
