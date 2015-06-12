/************************************************************
 * 模块名称：短信枚举
 * 功能说明：存放短信中心模块中的枚举
 * 创建人：周文超  2011-9-13 
 * *********************************************************/
namespace EyouSoft.Model.EnumType.SmsStructure
{
    #region MobileType 短信接收号码类型

    /// <summary>
    /// 短信接收号码类型
    /// </summary>
    public enum MobileType
    {
        /// <summary>
        /// 移动,联通手机号码
        /// </summary>
        Mobiel,

        /// <summary>
        /// 小灵通号码
        /// </summary>
        Phs
    }

    #endregion

    #region SendStatus 短信发送状态

    /// <summary>
    /// 短信发送状态
    /// </summary>
    public enum SendStatus
    {
        /// <summary>
        /// 未发送
        /// </summary>
        未发送 = 0,
        /// <summary>
        /// 发送成功
        /// </summary>
        发送成功,
        /// <summary>
        /// 发送失败
        /// </summary>
        发送失败
    }

    #endregion

    #region SendType 短信发送方式

    /// <summary>
    /// 短信发送方式
    /// </summary>
    public enum SendType
    {
        /// <summary>
        /// 直接发送
        /// </summary>
        直接发送 = 0,
        /// <summary>
        /// 定时发送
        /// </summary>
        定时发送
    }

    #endregion

    #region SettingType  短信配置类型

    /// <summary>
    /// 短信配置类型
    /// </summary>
    public enum SettingType
    {
        /// <summary>
        /// 出团通知
        /// </summary>
        出团通知 = 0,
        /// <summary>
        /// 回团通知
        /// </summary>
        回团通知 = 1,
        /// <summary>
        /// 生日提醒
        /// </summary>
        生日提醒 = 2
    }

    /// <summary>
    /// 出团通知插入标签
    /// </summary>
    public enum SettingTypeCTLabel
    {
        /// <summary>
        /// 游客姓名=111
        /// </summary>
        游客姓名 = 111,
        /// <summary>
        /// 线路名称=222
        /// </summary>
        线路名称 = 222,
        /// <summary>
        /// 出团时间=333
        /// </summary>
        出团时间 = 333,
        /// <summary>
        /// 集合方式=444
        /// </summary>
        集合方式 = 444,
        /// <summary>
        /// 本团导游=555
        /// </summary>
        本团导游 = 555,
        /// <summary>
        ///导游电话=666 
        /// </summary>
        导游电话 = 666
    }

    /// <summary>
    /// 回团通知插入标签
    /// </summary>
    public enum SettingTypeHTLabel
    {
        /// <summary>
        /// 游客姓名=111
        /// </summary>
        游客姓名 = 111,
        /// <summary>
        /// 线路名称=222
        /// </summary>
        线路名称 = 222,
        /// <summary>
        /// 出团时间=333
        /// </summary>
        回团时间 = 333
    }

    /// <summary>
    /// 生日提醒插入标签
    /// </summary>
    public enum SettingTypeSRLabel
    {
        /// <summary>
        /// 姓名=111
        /// </summary>
        姓名 = 111,
        /// <summary>
        /// 生日=222
        /// </summary>
        生日 = 222
    }

    #endregion

    #region 出回团提醒短信任务类型

    /// <summary>
    /// 出回团提醒短信任务类型
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 出团提醒
        /// </summary>
        出团提醒 = 0,
        /// <summary>
        /// 回团提醒
        /// </summary>
        回团提醒 = 1
    }

    #endregion

    #region 导入客户管理号码客户类型
    /// <summary>
    /// 导入客户管理号码客户类型
    /// </summary>
    public enum DaoRuKeHuType
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
        个人会员 = 2,
        /// <summary>
        /// 供应商-地接社
        /// </summary>
        地接社 = 100,
        /// <summary>
        /// 供应商-酒店
        /// </summary>
        酒店 = 101,
        /// <summary>
        /// 供应商-餐馆
        /// </summary>
        餐馆 = 102,
        /// <summary>
        /// 供应商-游轮
        /// </summary>
        游轮 = 103,
        /// <summary>
        /// 供应商-车队
        /// </summary>
        车队 = 104,
        /// <summary>
        /// 供应商-票务
        /// </summary>
        票务 = 105,
        /// <summary>
        /// 供应商-景点
        /// </summary>
        景点 = 106,
        /// <summary>
        /// 供应商-购物
        /// </summary>
        购物 = 107,
        /// <summary>
        /// 供应商-其他
        /// </summary>
        其他 = 108,
        /// <summary>
        /// 系统用户
        /// </summary>
        系统用户 = 201
    }
    #endregion
}
