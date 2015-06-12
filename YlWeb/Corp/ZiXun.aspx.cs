using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Corp
{
    public partial class ZiXun : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo();

            chaXun.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing>(Utils.GetQueryStringValue("t"), EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答);

            if (chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答 || chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略)
            {
                BangZhu1.LeiXing1 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答;
                BangZhu1.LeiXing2 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略;
            }

            if (chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮问题解答 || chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略)
            {
                BangZhu1.LeiXing1 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮问题解答;
                BangZhu1.LeiXing2 = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略;
            }

            string title = chaXun.LeiXing.Value.ToString();

            if (chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答 || chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮问题解答)
            {
                title = "问题解答";
            }

            if (chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略)
            {
                title = "游轮攻略";
            }

            if (chaXun.LeiXing == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略)
            {
                title = "邮轮攻略";
            }

            Title = title;
            ltr0.Text = title;

            var items = new EyouSoft.BLL.YlStructure.BWz().GetZiXuns(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
        }
    }
}
