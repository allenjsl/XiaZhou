using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.CrmStructure
{
    #region 客户类型
    /// <summary>
    /// 客户类型
    /// </summary>
    public enum CrmType
    {
        /// <summary>
        /// 单位直客
        /// </summary>
        单位直客 = 0,
        /// <summary>
        /// 同行客户
        /// </summary>
        同行客户 = 1,
        /// <summary>
        /// 个人会员
        /// </summary>
        个人会员 = 2
    }
    #endregion  

    #region 个人会员状态
    /// <summary>
    /// 个人会员状态
    /// </summary>
    public enum CrmMemberState
    {
        /// <summary>
        /// 普通
        /// </summary>
        普通 = 1,
        /// <summary>
        /// 活跃
        /// </summary>
        活跃 = 2,
        /// <summary>
        /// 休眠
        /// </summary>
        休眠 = 3,
        /// <summary>
        /// 作废
        /// </summary>
        作废 = 4
    }
    #endregion

    #region 团队被访人身份
    /// <summary>
    /// 团队被访人身份
    /// </summary>
    public enum CrmIdentity
    {
        /// <summary>
        /// 游客
        /// </summary>
        游客 = 1,
        /// <summary>
        /// 领队
        /// </summary>
        领队 = 2,
        /// <summary>
        /// 全陪
        /// </summary>
        全陪 = 3        
    }
    #endregion

    #region 团队回访类型
    /// <summary>
    /// 团队回访类型
    /// </summary>
    public enum CrmReturnType
    {
        /// <summary>
        /// 质检回访
        /// </summary>
        质检回访 = 1,
        /// <summary>
        /// 电话回访
        /// </summary>
        电话回访 = 2,
        /// <summary>
        /// 游客意见表
        /// </summary>
        游客意见表=3
    }
    #endregion

    #region 积分增减类别
    /// <summary>
    /// 积分增减类别
    /// </summary>
    public enum JiFenZengJianLeiBie
    {
        /// <summary>
        /// 增加
        /// </summary>
        增加,
        /// <summary>
        /// 减少
        /// </summary>
        减少
    }
    #endregion
}
