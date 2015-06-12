using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.YlWeb
{
    public partial class map : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            InitPage();
        }
        void InitPage()
        {
            var b = new BLL.YlStructure.BJiChuXinXi();
            var xl = b.GetXiLies(YuMingInfo.CompanyId, new Model.YlStructure.MXiLieChaXunInfo() {GongSiLeiXing=EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 });
            if (xl != null && xl.Count > 0) { rptxl.DataSource = xl; rptxl.DataBind(); }
            var gs = b.GetGongSis(YuMingInfo.CompanyId, new Model.YlStructure.MGongSiChaXunInfo() { GongSiLeiXing = Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮 });
            if (gs != null && gs.Count > 0) { rptgs.DataSource = gs; rptgs.DataBind(); }
        }
        protected void rptxl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                var rptxld = (Repeater)e.Item.FindControl("rptxld");
                var xilieid = DataBinder.Eval(e.Item.DataItem, "xilieid");
                var l = new BLL.YlStructure.BJiChuXinXi().GetChuanZhis(YuMingInfo.CompanyId, new Model.YlStructure.MChuanZhiChaXunInfo() { XiLieId = xilieid.ToString() });
                if (l != null && l.Count > 0)
                {
                    rptxld.DataSource = l;
                    rptxld.DataBind();
                }
            }
        }
    }
}
