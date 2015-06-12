using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    /// <summary>
    /// 系统菜单配置
    /// </summary>
    [Serializable]
    public class MSysMenu
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 原始编号
        /// </summary>
        public int OriginalId { get; set; }
        /// <summary>
        /// 修改后的菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public string SortId { get; set; }
        ///// <summary>
        ///// 上级编号
        ///// </summary>
        //public int ParentId { get; set; }
        ///// <summary>
        ///// 基础权限二级模块编号
        ///// </summary>
        //public int PrivsId { get; set; }
        ///// <summary>
        ///// 菜单名称
        ///// </summary>
        //public string Name { get; set; }
        ///// <summary>
        ///// 目标url
        ///// </summary>
        //public string Url { get; set; }
        ///// <summary>
        ///// 排序编号
        ///// </summary>
        //public int SortId { get; set; }
    }
}
