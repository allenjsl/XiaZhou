using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb.Corp
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public partial class LianXi : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitInfo();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.联系我们);

            if (info == null) return;

            ltr.Text = info.V;
        }
        #endregion
    }
}
