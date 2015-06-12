//供应商相关数据访问类接口 汪奇志 2013-04-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GysStructure
{
    /// <summary>
    /// 供应商相关数据访问类接口
    /// </summary>
    public interface IGys
    {
        /// <summary>
        /// 获取供应商选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun);
        /// <summary>
        /// 获取景点选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun);
        /// <summary>
        /// 获取供应商账号信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MGysUserInfo GetGysUserInfo(string gysId);
        /// <summary>
        /// 获取供应商-地接社列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBDiJieSheInfo> GetDiJieShes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-酒店列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBJiuDianInfo> GetJiuDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-餐馆列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBCanGuanInfo> GetCanGuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-景点列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBJingDianInfo> GetJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-游轮列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBYouLunInfo> GetYouLuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-车队列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBCheDuiInfo> GetCheDuis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-票务列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBPiaoWuInfo> GetPiaoWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-购物列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBGouWuInfo> GetGouWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 获取供应商-其他列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="isOnlySelf">数据级浏览权限控制-是否查看自己</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MLBQiTaInfo> GetQiTas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun, bool isOnlySelf, string userId, int[] depts);
        /// <summary>
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        int Delete(string companyId, string gysId);
        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        EyouSoft.Model.EnumType.SourceStructure.SourceType? GetGysLeiXing(string gysId);
        /// <summary>
        /// 获取供应商交易明细信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="heJi">合计信息[0:数量:int][1:数量:decimal][2:结算金额:decimal][3:已支付金额:decimal]</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi);
    }
}
