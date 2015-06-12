using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;

namespace Web.UserControl
{
    /// <summary>
    /// 预控类型
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-26
    public partial class ControlType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获取酒店预控 单控情况下  预控时间开始控件ClientID
        /// </summary>
        public string ControlDateSClientID
        {
            get { return this.ClientID + "_ControlType_DateS"; }
        }
        /// <summary>
        /// 获取酒店预控 单控情况下  预控时间结束控件ClientID
        /// </summary>
        public string ControlDateEClientID
        {
            get { return this.ClientID + "_ControlType_DateE"; }
        }
        /// <summary>
        /// 获取预控类型
        /// </summary>
        /// 酒店预控:
        /// 单控-出现预控日期(时间段)
        /// 总控-出现预控日期(单个时间)
        /// 其余均无预控日期
        public SourceControlCategory SourceControlCategory
        {
            get;
            set;
        }
    }
}