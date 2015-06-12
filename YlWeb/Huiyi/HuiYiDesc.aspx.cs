using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyi
{
    public partial class HuiYiDesc : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议横幅;
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"));

            if (type > 0)
            {
                GetContent(type);
            }
        }
        private void GetContent(int type)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            var KyInfo = bll.GetKvInfo(YuMingInfo.CompanyId, (EyouSoft.Model.EnumType.YlStructure.WzKvKey)type);
            if (KyInfo != null)
            {
                if (type == (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介)
                {
                    this.lbhy.Text = KyInfo.V;
                    this.PHCJ.Visible = false;
                }
                else
                {
                    this.lbcj.Text = KyInfo.V;
                    this.phhy.Visible = false;
                }
            }
        }
    }
}
