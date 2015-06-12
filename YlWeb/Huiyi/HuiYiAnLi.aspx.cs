using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.YlStructure;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb.YouLunMeet
{
    public partial class HuiYiAnLi : WzPage
    {
        private IList<MWzHuiYiAnLiInfo> data;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = WzGuangGaoWeiZhi.游轮会议横幅;

            WzKvKey type = (WzKvKey)Utils.GetInt(Utils.GetQueryStringValue("type"));

            if (type > 0)
            {
                GetContent(type);
            }
        }

        private void GetContent(WzKvKey type)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            int recordCount = 0;
            MWzHuiYiAnLiChaXunInfo model = new MWzHuiYiAnLiChaXunInfo();
            if (type == WzKvKey.长江游轮会议详介)  model.LeiXing = YouLunLeiXing.长江游轮;
            if (type == WzKvKey.海洋邮轮会议详介)  model.LeiXing = YouLunLeiXing.海洋邮轮;

            data = bll.GetHuiYiAnLis(YuMingInfo.CompanyId, 100, 1, ref recordCount, model);
            Repeater1.DataSource = data.Select(x => x.ShiJian1.Year).Distinct().OrderByDescending(x => x).Take(5);
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int year = Convert.ToInt32(e.Item.DataItem);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repeater2 = (Repeater)e.Item.FindControl("Repeater2");
                repeater2.DataSource = data.Where(x => x.ShiJian1.Year == year).OrderByDescending(y => y.ShiJian1).Take(6);
                repeater2.DataBind();
            }
        }
    }
}
