using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Security.Membership;
using EyouSoft.Model.SSOStructure;
using System.Text;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class Navhead : System.Web.UI.UserControl
    {
        EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? _LeiXing = null;
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing
        {
            get { return _LeiXing; }
            set { _LeiXing = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            MYlHuiYuanInfo userInfo = null;
            if (YlHuiYuanProvider.IsLogin(out userInfo))
            {
                if (userInfo != null)
                {
                    ltrUserName.Text = userInfo.Username;
                }
                plnLogin.Visible = false;
                plnRegister.Visible = false;
                plnOrder.Visible = true;
                plnLoginOut.Visible = true;
            }

            var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.热门关键字;
            var items=new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(yuMingInfo.CompanyId, 8, 1, ref recordCount,chaXun);

            StringBuilder s = new StringBuilder();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (!string.IsNullOrEmpty(item.Url))
                    {
                        s.AppendFormat("<a href=\"{1}\" target=\"_blank\">{0}</a>", item.MingCheng, item.Url);
                    }
                    else
                    {
                        s.AppendFormat("<a href=\"javascript:void(0)\" class=\"i_rmgjz\">{0}</a>", item.MingCheng);
                    }
                }
            }
            ltrReMen.Text = s.ToString();
        }
    }
}