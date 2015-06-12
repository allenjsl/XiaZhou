using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb.Corp
{
    /// <summary>
    /// 简介
    /// </summary>
    public partial class JianJie : WzPage
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
            var info = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, (EyouSoft.Model.EnumType.YlStructure.WzKvKey)EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("k")));

            if (info == null) return;

            ltr.Text = info.V;
        }
        #endregion
    }
}
