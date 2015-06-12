using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class MComUser
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 客户或供应商编号
        /// </summary>
        public string TourCompanyId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 明文密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// MD5密码
        /// </summary>
        public string MD5Password { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserType UserType { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public char ContactSex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string ContactFax { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// msn
        /// </summary>
        public string MSN { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public string LastLoginTime { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 所属部门名称 
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public string Privs { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string PeopProfile { get; set; }
        /// <summary>
        /// 个人备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserStatus UserStatus { get; set; }
        /// <summary>
        /// 是否是管理员 
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 凭证代码 
        /// </summary>
        public string CertificateCode { get; set; }
        /// <summary>
        /// 欠款额度
        /// </summary>
        public decimal Arrears { get; set; }
        /// <summary>
        /// 监管部门编号
        /// </summary>
        public int DeptIdJG { get; set; }
        /// <summary>
        /// 监管部门名称
        /// </summary>
        public string JGDeptName { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public int OperDeptId { get; set; }
        /// <summary>
        /// 人事档案编号
        /// </summary>
        public string GovFileId { get; set; }
        /// <summary>
        /// 用户在线状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus OnlineStatus { get; set; }
        /// <summary>
        /// 角色名称(OUTPUT)
        /// </summary>
        public string RoleName { get; set; }
    }

    /// <summary>
    /// 用户信息搜索实体
    /// </summary>
    public class MComUserSearch
    {
        /// <summary>
        /// 部门
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserStatus? UserStatus { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int? RoleId { get; set; }
    }

    #region 登录密码信息业务实体
    /// <summary>
    /// 登录密码信息业务实体
    /// </summary>
    [Serializable]
    public class MPasswordInfo
    {
        private readonly EyouSoft.Toolkit.DataProtection.HashCrypto hashcrypto = new EyouSoft.Toolkit.DataProtection.HashCrypto();

        /// <summary>
        /// 明文密码
        /// </summary>
        private string _noEncryptPassword = string.Empty;
        /// <summary>
        /// MD5加密密码
        /// </summary>
        private string _md5Password = string.Empty;

        /// <summary>
        /// default constructor
        /// </summary>
        public MPasswordInfo() { }

        /// <summary>
        /// 获取或设置明文密码
        /// </summary>
        public string NoEncryptPassword
        {
            get
            {
                return _noEncryptPassword;
            }
            set
            {
                this._noEncryptPassword = value;
                this._md5Password = hashcrypto.MD5Encrypt(this._noEncryptPassword);
            }
        }

        /// <summary>
        /// 获取MD5加密密码
        /// </summary>
        public string MD5Password { get { return this._md5Password; } }

        /// <summary>
        /// 设置md5pwd
        /// </summary>
        /// <param name="md5"></param>
        public void SetMD5Pwd(string md5)
        {
            this._md5Password = md5;
        }
    }
    #endregion
}
