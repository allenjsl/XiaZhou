//供应商所有枚举类型 创建者 钱琦 时间 2011-9-1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.SourceStructure
{
    #region 供应商类型

    /// <summary>
    /// 供应商类型
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 地接社
        /// </summary>
        地接社 = 0,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 1,
        /// <summary>
        /// 餐馆
        /// </summary>
        餐馆 = 2,
        /// <summary>
        /// 游轮
        /// </summary>
        游轮 = 3,
        /// <summary>
        /// 车队
        /// </summary>
        车队 = 4,
        /// <summary>
        /// 票务
        /// </summary>
        票务 = 5,
        /// <summary>
        /// 景点
        /// </summary>
        景点 = 6,
        /// <summary>
        /// 购物
        /// </summary>
        购物 = 7,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 8,
        //保险=9//要删除的

    }
    #endregion

    #region 供应商菜系
    /// <summary>
    /// 供应商菜系
    /// </summary>
    public enum SourceCuisine
    {
        /// <summary>
        /// 川菜
        /// </summary>
        川菜 = 0,
        /// <summary>
        /// 鲁菜
        /// </summary>
        鲁菜 = 1,
        /// <summary>
        /// 粤菜
        /// </summary>
        粤菜 = 2,
        /// <summary>
        /// 闽菜
        /// </summary>
        闽菜 = 3,
        /// <summary>
        /// 苏菜
        /// </summary>
        苏菜 = 4,
        /// <summary>
        /// 浙菜
        /// </summary>
        浙菜 = 5,
        /// <summary>
        /// 湘菜
        /// </summary>
        湘菜 = 6,
        /// <summary>
        /// 徽菜
        /// </summary>
        徽菜 = 7,
        /// <summary>
        /// 未选择
        /// </summary>
        未选择=-1
    }
    #endregion

    #region 导游级别枚举
    /// <summary>
    /// 导游级别
    /// </summary>
    public enum GuideLevel
    {
        /// <summary>
        /// 初级
        /// </summary>
        初级 = 0,
        /// <summary>
        /// 中级
        /// </summary>
        中级 = 1,
        /// <summary>
        /// 高级
        /// </summary>
        高级 = 2,
        /// <summary>
        /// 见习
        /// </summary>
        见习 = 3,
        /// <summary>
        /// 实习
        /// </summary>
        实习 = 4
    }
    #endregion

    #region 导游证挂靠单位枚举
    /// <summary>
    /// 导游证挂靠单位
    /// </summary>
    public enum AnchoredCom
    {
        /// <summary>
        /// 本社
        /// </summary>
        本社 = 1,
        /// <summary>
        /// 导管中心
        /// </summary>
        导管中心 = 2,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 3
    }
    #endregion

    #region 导游类别枚举
    /// <summary>
    /// 导游类别
    /// </summary>
    public enum GuideCategory
    {
        /// <summary>
        /// 领队
        /// </summary>
        领队 = 0,
        /// <summary>
        /// 全陪
        /// </summary>
        全陪,
        /// <summary>
        /// 地陪
        /// </summary>
        地陪,
        /// <summary>
        /// 景区导游
        /// </summary>
        景区导游
    }
    #endregion

    #region 酒店星级
    /// <summary>
    /// 酒店星级
    /// </summary>
    public enum HotelStar
    {
        /// <summary>
        /// 未选择
        /// </summary>
        未选择=0,
        /// <summary>
        /// 一星
        /// </summary>
        一星 = 1,
        /// <summary>
        /// 两星
        /// </summary>
        两星 = 2,
        /// <summary>
        /// 准三星
        /// </summary>
        准三星 = 3,
        /// <summary>
        /// 三星
        /// </summary>
        三星 = 4,
        /// <summary>
        /// 准四星
        /// </summary>
        准四星 = 5,
        /// <summary>
        /// 四星
        /// </summary>
        四星 = 6,
        /// <summary>
        /// 准五星
        /// </summary>
        准五星 = 7,
        /// <summary>
        /// 五星
        /// </summary>
        五星 = 8
    }
    #endregion

    #region 景点星级
    /// <summary>
    /// 景点星级
    /// </summary>
    public enum SpotStar
    {
        /// <summary>
        /// A
        /// </summary>
        A = 0,
        /// <summary>
        /// AA
        /// </summary>
        AA = 1,
        /// <summary>
        /// AAA
        /// </summary>
        AAA = 2,
        /// <summary>
        /// AAAA
        /// </summary>
        AAAA = 3,
        /// <summary>
        /// AAAAA
        /// </summary>
        AAAAA = 4,
        /// <summary>
        /// 未选择
        /// </summary>
        未选择=5
    }
    #endregion

    #region 用餐类型
    /// <summary>
    /// 用餐类型
    /// </summary>
    public enum DiningType
    {
        /// <summary>
        /// 早餐
        /// </summary>
        早餐 = 0,
        /// <summary>
        /// 中餐
        /// </summary>
        中餐 = 1,
        /// <summary>
        /// 晚餐
        /// </summary>
        晚餐 = 2
    }
    #endregion

    //#region 用餐人类型
    ///// <summary>
    ///// 用餐人类别
    ///// </summary>
    //public enum DiningPeopleType
    //{
    //    /// <summary>
    //    /// 成人
    //    /// </summary>
    //    成人=0,
    //    /// <summary>
    //    /// 儿童
    //    /// </summary>
    //    儿童
    //}
    //#endregion

    #region 资源预控类型
    /// <summary>
    /// 资源预控类型
    /// </summary>
    public enum SourceControlType
    {
        /// <summary>
        /// 单控
        /// </summary>
        单控 = 1,
        /// <summary>
        /// 总控
        /// </summary>
        总控 = 2
    }
    #endregion

    #region 总控共享方式
    /// <summary>
    /// 总控共享方式
    /// </summary>
    public enum ShareType
    {
        /// <summary>
        /// 共享
        /// </summary>
        共享 = 1,
        /// <summary>
        /// 不共享
        /// </summary>
        不共享 = 2
    }
    #endregion

    #region 预控类别

    /// <summary>
    /// 预控类别
    /// </summary>
    public enum SourceControlCategory
    {
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 1,
        /// <summary>
        /// 车辆
        /// </summary>
        车辆 = 2,
        /// <summary>
        /// 游轮
        /// </summary>
        游轮 = 3,
        /// <summary>
        /// 景点=4
        /// </summary>
        景点=4,
        /// <summary>
        /// 其他=5
        /// </summary>
        其他=5
    }

    #endregion

   

    #region 游船星级
    /// <summary>
    /// 游船星级
    /// </summary>
    public enum ShipStar
    {
        /// <summary>
        /// none
        /// </summary>
        未选择 = 0,
        /// <summary>
        /// 三星
        /// </summary>
        三星,
        /// <summary>
        /// 四星
        /// </summary>
        四星,
        /// <summary>
        /// 五星
        /// </summary>
        五星
    }
    #endregion
}
