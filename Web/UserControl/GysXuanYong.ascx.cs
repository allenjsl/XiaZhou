using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.UserControl
{
    public partial class GysXuanYong : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// 供应商编号ClientID
        /// </summary>
        public string GysIdClientID
        {
            get
            {
                return ClientID + "_GYSID";
            }
        }

        /// <summary>
        /// 供应商名称ClientID
        /// </summary>
        public string GysNameClientID
        {
            get
            {
                return ClientID + "_GYSNAME";
            }
        }

        /// <summary>
        /// 选用ClientID
        /// </summary>
        public string AClientID
        {
            get
            {
                return ClientID + "_A";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}