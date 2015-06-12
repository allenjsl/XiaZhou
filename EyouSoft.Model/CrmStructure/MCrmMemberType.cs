using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CrmStructure
{
    /// <summary>
    /// 个人会员类型
    /// </summary>
    [Serializable]
    public class MCrmMemberType
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmMemberType() { }
        #region Model
        private int _typeid;
        private string _name;
        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        #endregion Model

    }
}
