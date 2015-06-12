//YL积分兑换商品interface 汪奇志 2014-03-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// YL积分兑换商品interface
    /// </summary>
    public interface IDuiHuan
    {
        /// <summary>
        /// 积分商品新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int JiFenShangPin_CU(EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo info);
        /// <summary>
        /// 积分商品删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        int JiFenShangPin_D(string companyId, string shangPinId);
        /// <summary>
        /// 获取积分商品信息
        /// </summary>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo GetJiFenShangPinInfo(string shangPinId);
        /// <summary>
        /// 获取积分商品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo> GetJiFenShangPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenShangPinChaXunInfo chaXun);

        /// <summary>
        /// 新增积分订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int JiFenDingDan_C(EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo info);
        /// <summary>
        /// 获取积分订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo GetJiFenDingDanInfo(string dingDanId);
        /// <summary>
        /// 获取积分订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo> GetJiFenDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenDingDanChaXunInfo chaXun);
        /// <summary>
        /// 设置积分兑换订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        int SheZhiJiFenDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus status);
        /// <summary>
        /// 设置积分兑换订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        int SheZhiJiFenDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info);
        /// <summary>
        /// 获取积分订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        string GetDingDanId(int identityId);
    }
}
