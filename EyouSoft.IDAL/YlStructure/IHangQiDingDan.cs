//航期订单相关interface 汪奇志 2014-03-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// 航期订单相关interface
    /// </summary>
    public interface IHangQiDingDan
    {
        /// <summary>
        /// 写入航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int DingDan_C(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info);
        /// <summary>
        /// 修改航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int DingDan_M(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info);
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHangQiDingDanInfo GetDingDanInfo(string dingDanId);
        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiDingDanInfo> GetDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiDingDanChaXunInfo chaXun);
        /// <summary>
        /// 设置航期订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="liuWeiDaoQiShiJian">留位到期时间</param>
        /// <returns></returns>
        int SheZhiDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus status, DateTime? liuWeiDaoQiShiJian);
        /// <summary>
        /// 设置付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        int SheZhiFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info);
        /// <summary>
        /// 获取航期订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        string GetDingDanId(int identityId);
        /// <summary>
        /// 更新订单游客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="youKes">游客集合</param>
        /// <returns></returns>
        int UpdateDingDanYouKes(string dingDanId, IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> youKes);
        /// <summary>
        /// 更新订单积分，返回1成功，其它失败
        /// </summary>
        /// <param name="dingdanId">订单编号</param>
        /// <param name="dingdanJifen">订单积分</param>
        /// <returns>1：成功 -100：失败 -99：会员可用积分小于0</returns>
        int UpDateDingDanJiFen(string dingdanId, decimal dingdanJifen);
        /// <summary>
        /// 更新订单操作备注
        /// </summary>
        /// <param name="dingdanid">订单编号</param>
        /// <param name="caozuobeizhu">操作备注</param>
        /// <returns>1：成功 0：失败</returns>
        int UpdDingDanCaoZuoBeiZhu(string dingdanid, string caozuobeizhu);
    }
}
