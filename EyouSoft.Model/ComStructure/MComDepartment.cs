using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    #region 部门信息
    /// <summary>
    /// 部门信息
    /// </summary>
    [Serializable]
    public class MComDepartment
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 部门主管
        /// </summary>
        public string DepartHead { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        public int PrevDepartId { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 打印页眉
        /// </summary>
        public string PrintHeader { get; set; }
        /// <summary>
        /// 打印页脚
        /// </summary>
        public string PrintFooter { get; set; }
        /// <summary>
        /// 打印模版
        /// </summary>
        public string PrintTemplates { get; set; }
        /// <summary>
        /// 部门公章
        /// </summary>
        public string Seal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 凭证代码
        /// </summary>
        public string CertificateCode { get; set; }
    }
    #endregion

    #region 公司组织机构信息业务实体（用于缓存）
    /// <summary>
    /// 公司组织机构信息业务实体（用于缓存）
    /// </summary>
    /// 汪奇志 2012-03-06
    [Serializable]
    public class MCacheDeptInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCacheDeptInfo() { }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 下级部门集合（含部门编号）
        /// </summary>
        public List<int> Depts { get; set; }
        /// <summary>
        /// 所在一级部门编号
        /// </summary>
        public int FirstId { get; set; }
        /// <summary>
        /// 上级部门编号（一级部门时=0）
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 是否存在下级部门
        /// </summary>
        public bool HasChildren { get; set; }
    }
    #endregion
}
