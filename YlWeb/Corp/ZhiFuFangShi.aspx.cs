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
    /// 支付方式
    /// </summary>
    public partial class ZhiFuFangShi : WzPage
    {
        protected string OnIndex = "1";

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack) {
                InitInfo();
            }
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.支付方式_在线支付;
            string s = "在线支付";

            switch (Utils.GetQueryStringValue("s"))
            {
                case "1": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.支付方式_在线支付; s = "在线支付"; OnIndex = "1"; break;
                case "2": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.支付方式_门店支付; s = "门店支付"; OnIndex = "2"; break;
                case "3": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.支付方式_邮局汇款; s = "淘宝支付"; OnIndex = "3"; break;
                case "4": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.支付方式_公司转账; s = "银行转账"; OnIndex = "4"; break;
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
