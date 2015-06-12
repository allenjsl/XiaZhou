//预算表interface 汪奇志 2014-02-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.StatStructure
{
    /// <summary>
    /// 预算表interface
    /// </summary>
    public interface IYuSuanBiao
    {
        /// <summary>
        /// 获取预算表信息信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计 [0:int:实收人数] [1:decimal:收入合计] [2:decimal:支出合计]</param>
        /// <returns></returns>
        IList<EyouSoft.Model.StatStructure.MYuSuanBiaoInfo> GetYuSuanBiaos(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.StatStructure.MYuSuanBiaoChaXunInfo chaXun, out object[] heJi);
        /// <summary>
        /// 获取预算表收入信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.StatStructure.MYuSuanBiaoShouRuInfo> GetShouRus(string tourId);
        /// <summary>
        /// 获取预算表支出信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.StatStructure.MYuSuanBiaoZhiChuInfo> GetZhiChus(string tourId);
    }
}
