//YL-礼品卡相关-interface 汪奇志 2014-04-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// YL-礼品卡相关-interface
    /// </summary>
    public interface ILiPinKa
    {
        /// <summary>
        /// 礼品卡新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int LiPinKa_CU(EyouSoft.Model.YlStructure.MLiPinKaInfo info);
        /// <summary>
        /// 礼品卡删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        int LiPinKa_D(string companyId, string liPinKaId);
        /// <summary>
        /// 获取礼品卡信息
        /// </summary>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MLiPinKaInfo GetLiPinKaInfo(string liPinKaId);
        /// <summary>
        /// 获取礼品卡集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MLiPinKaInfo> GetLiPinKas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo chaXun);

        /// <summary>
        /// 新增礼品卡订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int LiPinKaDingDan_C(EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo info);
        /// <summary>
        /// 获取礼品卡订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo GetLiPinKaDingDanInfo(string dingDanId);
        /// <summary>
        /// 获取礼品卡订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo> GetLiPinKaDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaDingDanChaXunInfo chaXun);
        /// <summary>
        /// 设置礼品卡订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        int SheZhiLiPinKaDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus status);
        /// <summary>
        /// 设置礼品卡订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        int SheZhiLiPinKaDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info);
    }
}
