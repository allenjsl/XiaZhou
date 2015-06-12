using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyi
{
    public partial class YouLunFeiYong : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议横幅;
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"));

            if (type > 0)
            {
                switch (type)
                {
                    case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介:
                        GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮费用测算);
                        break;
                    case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介:
                        GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮费用测算);
                        break;
                    default:
                        break;
                }
            }
        }
        private void GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey type)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            var KyInfo = bll.GetKvInfo(YuMingInfo.CompanyId, type);
            if (KyInfo != null)
            {
                switch (type)
                {
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮费用测算:
                        this.lbcj.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮费用测算:
                        this.lbhy.Text = KyInfo.V;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
