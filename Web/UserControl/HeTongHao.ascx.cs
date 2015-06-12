using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 合同号用户控件
    /// </summary>
    public partial class HeTongHao : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// 合同编号ClientID、UniqueID
        /// </summary>
        public string HeTongIdClientID { get { return ClientID + "_id"; } }
        /// <summary>
        /// 合同号ClientID、UniqueID
        /// </summary>
        public string HeTongCodeClientID { get { return ClientID + "_code"; } }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string HeTongCode { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}