//多选订单状态状态 汪奇志 2013-05-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 多选订单状态状态
    /// </summary>
    public partial class DuoXuanOrderStatus : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// 订单状态编号隐藏域ClientID
        /// </summary>
        public string ZTIdClientID
        {
            get
            {
                return ClientID + "_v";
            }
        }

        /// <summary>
        /// 订单状态名称文本域ClientID
        /// </summary>
        public string ZTNameClientID
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