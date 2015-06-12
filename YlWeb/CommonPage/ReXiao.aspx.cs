using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.CommonPage
{
    public partial class ReXiao : EyouSoft.YlWeb.WzPage
    {
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing), Utils.GetQueryStringValue("lx"));
            if (!LeiXing.HasValue) LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

            InitRX();
        }

        void InitRX()
        {
            var chaXun = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();
            chaXun.IsYouXiao = true;
            chaXun.IsReXiao = true;
            chaXun.LeiXing = LeiXing;

            int recordCount = 0;
            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(YuMingInfo.CompanyId, 5, 1, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                rptRX.DataSource = items;
                rptRX.DataBind();
            }
        }

        void InitRX1()
        {
            var chaXun = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();
            chaXun.IsYouXiao = true;
            chaXun.IsReXiao = true;
            chaXun.LeiXing = LeiXing;

            int recordCount = 0;
            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(YuMingInfo.CompanyId, 10, 1, ref recordCount, chaXun);
            StringBuilder s = new StringBuilder();
            if (items != null && items.Count > 0)
            {
                int i = 1;
                foreach (var item in items)
                {
                    s.AppendFormat("<li><em>{0}</em>", i);
                    s.AppendFormat("<a href=\"{0}{1}.html\">{2}</a>", GetHqUrl(item.LeiXing), item.HangQiId, item.MingCheng);
                    s.AppendFormat("<p>{0}/人起</p>", item.QiShiJiaGe.ToString("C2"));
                    s.AppendFormat("</li>");
                    i++;
                }
            }

            RCWE(s.ToString());
        }

        protected string GetHqUrl(object leiXing)
        {
            string url = "/hangqi/";
            var _leiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)leiXing;
            if (_leiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                url = "/hangqi/HY";

            return url;
        }
    }
}
