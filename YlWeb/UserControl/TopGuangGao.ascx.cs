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
    public partial class TopGuangGao : System.Web.UI.UserControl
    {
        EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi _WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.长江游轮横幅;
        public EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi WeiZhi
        {
            get { return _WeiZhi; }
            set { this._WeiZhi = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            chaXun.WeiZhi = WeiZhi;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(YuMingInfo.CompanyId, 8, 1, ref recordCount, chaXun);

            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();

            if (items != null && items.Count > 0)
            {
                int j = 0;
                foreach (var item in items)
                {
                    string url = item.Url;
                    string target = "target=\"_blank\"";
                    string style = "";

                    if (j++ == 0) style = " style=\"display:block;\" ";

                    if (string.IsNullOrEmpty(url))
                    {
                        url = "javascript:void(0)";
                        target = string.Empty;
                    }

                    s1.AppendFormat("<li class=\"bg_img\" data-src='{0}' {1}>", item.Url, style);
                    s1.AppendFormat("<div class=\"banner_con\" style=\"background-image: url({0});\" time=\"5000\">", Utils.GetErpFilepath() + item.Filepath);
                    //if (!string.IsNullOrEmpty(item.Url))
                    //{
                    //    s1.AppendFormat("<a href='{0}' target='_blank' style='display:block;width:100%;height:100%;position:relative;z-index:100'></a>", item.Url);
                    //}
                    s1.Append("</div>");
                    s1.Append("</li>");
                }

                for (int i = 0; i < items.Count; i++)
                {
                    string _class = "play_but_disc";
                    if (i == 0) _class = "play_but_disc current";
                    s2.AppendFormat("<span class=\"{1}\" rel=\"{0}\"></span>", i + 1, _class);
                }
            }

            ltr1.Text = s1.ToString();
            ltr2.Text = s2.ToString();
        }
    }
}