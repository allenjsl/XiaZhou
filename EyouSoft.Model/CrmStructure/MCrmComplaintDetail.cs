using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CrmStructure
{
    /// <summary>
    /// 实体类tbl_CrmComplaintDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MCrmComplaintDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmComplaintDetail() { }

        #region Model
        private string _complaintid;
        
        private string _opinion;
        /// <summary>
        /// 投诉编号
        /// </summary>
        public string ComplaintId
        {
            set { _complaintid = value; }
            get { return _complaintid; }
        }
       
        /// <summary>
        /// 投诉意见
        /// </summary>
        public string Opinion
        {
            set { _opinion = value; }
            get { return _opinion; }
        }
        #endregion Model

    }
}
