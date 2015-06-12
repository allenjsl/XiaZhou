﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyi
{
    public partial class XiaoHuiYi : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议横幅;
            int type = Utils.GetInt(Utils.GetQueryStringValue("type"));

            switch (type)
            {
                case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介:
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议接待);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议场地选择);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议服务项目及流程);
                    break;
                case (int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介:
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议接待);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议场地选择);
                    GetContent(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议服务项目及流程);
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
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议接待:
                        this.lbjiedai.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议场地选择:
                        this.lbxuanze.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议服务项目及流程:
                        this.lbliucheng.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议接待:
                        this.lbjiedai.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议场地选择:
                        this.lbxuanze.Text = KyInfo.V;
                        break;
                    case EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮小型会议服务项目及流程:
                        this.lbliucheng.Text = KyInfo.V;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
