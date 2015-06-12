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
    /// 会员服务
    /// </summary>
    public partial class HuiYuanFuWu : WzPage
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
            var key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.会员服务_积分兑换;
            string s = "积分兑换";

            switch (Utils.GetQueryStringValue("s"))
            {
                case "1": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.会员服务_积分兑换; s = "积分兑换"; OnIndex = "1"; break;
                case "2": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.会员服务_积分使用; s = "积分使用"; OnIndex = "2"; break;
                case "3": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.会员服务_取消订单; s = "取消订单"; OnIndex = "3"; break;
                case "4": key = EyouSoft.Model.EnumType.YlStructure.WzKvKey.会员服务_退款说明; s = "退款说明"; OnIndex = "4"; break;
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
