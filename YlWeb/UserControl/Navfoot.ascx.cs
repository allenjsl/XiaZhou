using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class Navfoot : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitInfo();
        }

        void InitInfo()
        {
            var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            var info = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(yuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站版权);
            if (info != null)
            {
                ltr1.Text = info.V;
            }

            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.图文;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetYouQingLianJies(yuMingInfo.CompanyId, 7, 1, ref recordCount, chaXun);

            StringBuilder s = new StringBuilder();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    string url = "javascript:void(0)";
                    string target = string.Empty;
                    if (!string.IsNullOrEmpty(item.Url))
                    {
                        url = item.Url;
                        target = " target=\"_blank\" ";
                    }

                    s.AppendFormat("<li><a href=\"{0}\" {2}><img src=\"{1}\"></a></li>", url, Utils.GetErpFilepath() + item.Filepath, target);
                }
            }

            ltr2.Text = s.ToString();
        }
    }
}