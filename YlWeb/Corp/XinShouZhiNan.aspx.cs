using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Corp
{
    /// <summary>
    /// 新手指南
    /// </summary>
    public partial class XinShouZhiNan : WzPage
    {
        protected string OnIndex = "1";

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
            var key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.新手指南_如何订票;
            string s = "如何订票";

            switch (Utils.GetQueryStringValue("s"))
            {
                case "1": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.新手指南_如何订票; s = "如何订票"; OnIndex = "1"; break;
                case "2": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.新手指南_如何取票; s = "如何取票"; OnIndex = "2"; break;
                case "3": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.新手指南_游船旅行准备; s = "游船旅行准备"; OnIndex = "3"; break;
                case "4": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.新手指南_旅行注意事项; s = "旅行注意事项"; OnIndex = "4"; break;
            }

            var info = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(YuMingInfo.CompanyId, key);

            if (info == null) return;

            ltr.Text = info.V;
            ltr1.Text = s;
            this.Title = s;
        }
        #endregion
    }
}
