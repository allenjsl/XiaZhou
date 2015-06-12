using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 多选团队状态
    /// </summary>
    public partial class DuoXuanTourStatus : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// 团队状态编号隐藏域ClientID
        /// </summary>
        public string ValueClientID
        {
            get
            {
                return ClientID + "_v";
            }
        }

        /// <summary>
        /// 团队状态名称文本域ClientID
        /// </summary>
        public string TextClientID
        {
            get
            {
                return ClientID + "_t";
            }
        }

        /// <summary>
        /// 选用ClientID
        /// </summary>
        public string AClientID
        {
            get
            {
                return ClientID + "_a";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}