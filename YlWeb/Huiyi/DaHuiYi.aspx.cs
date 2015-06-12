using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyi
{
    public partial class DaHuiYi : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议横幅;
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"));

            switch (type)
            {
                case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介:
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议包租流程);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议航线介绍);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议包租价格);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议会展服务);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议关切问题);
                    break;
                case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介:
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租流程);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议航线介绍);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租价格);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议会展服务);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议关切问题);
                    break;
                default:
                    break;
            }
        }
        private void GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey dotype)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            var KyInfo = bll.GetKvInfo(YuMingInfo.CompanyId, dotype);
            if (KyInfo != null)
            {
                switch (dotype)
                {
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租流程:
                        this.lbliucheng.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议航线介绍:
                        this.lbjieshao.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租价格:
                        this.lbjiage.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议会展服务:
                        this.lbfuwu.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议关切问题:
                        this.lbwenti.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议包租流程:
                        this.lbliucheng.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议航线介绍:
                        this.lbjieshao.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议包租价格:
                        this.lbjiage.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议会展服务:
                        this.lbfuwu.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮大型会议关切问题:
                        this.lbwenti.Text = KyInfo.V;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
