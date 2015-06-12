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
using EyouSoft.Common.Page;

namespace EyouSoft.Web.YouLun
{
    public partial class HuiYuanDianPingEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Common.Utils.GetQueryStringValue("save") == "save") Save();
            InitPage();
        }
        void Save()
        {
            var result = 0;
            if (string.IsNullOrEmpty(Common.Utils.GetQueryStringValue("id")))
            {
                var h = new BLL.YlStructure.BHangQi().GetHangQiInfo(Common.Utils.GetQueryStringValue("hangqiid"));
                if (h != null)
                {
                    result = new BLL.YlStructure.BHuiYuan().InsertDianPing(new Model.YlStructure.MWzDianPingInfo()
                    {
                        DingDanId = string.Empty,
                        CompanyId = h.CompanyId,
                        GysId = h.GysId,
                        GongSiId = h.GongSiId,
                        XiLieId = h.XiLieId,
                        ChuanZhiId = h.ChuanZhiId,
                        HangQiId = h.HangQiId,
                        RiQiId = string.Empty,
                        NeiRong = Common.Utils.GetFormValue(this.txtHuiDa.UniqueID),
                        IssueTime = DateTime.Now,
                        OperatorId = SiteUserInfo.UserId,
                        IsShenHe = Common.Utils.GetFormValue(this.isshenhe.UniqueID) == "1",
                        ShenHeOperatorId = Common.Utils.GetFormValue(this.isshenhe.UniqueID) == "1" ? SiteUserInfo.UserId : string.Empty,
                        ShenHeTime = Common.Utils.GetFormValue(this.isshenhe.UniqueID) == "1" ? DateTime.Now : new DateTime?(),
                        FenShu = this.rdo1.Checked ? 1 : (this.rdo2.Checked ? 2 : (this.rdo3.Checked ? 3 : (this.rdo4.Checked ? 4 : 5))),
                        BiaoTi = Common.Utils.GetFormValue(this.txtbiaoti.UniqueID)
                    });
                }
            }
            else
            {
                string dianpingid = Common.Utils.GetQueryStringValue("id");
                var m = new EyouSoft.BLL.YlStructure.BHuiYuan().GetDianPingInfo(dianpingid);
                if (m == null) Common.Utils.RCWE(Common.UtilsCommons.AjaxReturnJson("0", "操作失败"));
                m.FenShu = this.rdo1.Checked ? 1 : (this.rdo2.Checked ? 2 : (this.rdo3.Checked ? 3 : (this.rdo4.Checked ? 4 : 5)));
                m.BiaoTi = Common.Utils.GetFormValue(this.txtbiaoti.UniqueID);
                m.NeiRong = Common.Utils.GetFormValue(this.txtHuiDa.UniqueID);
                m.IsShenHe = Common.Utils.GetFormValue(this.isshenhe.UniqueID) == "1";
                m.ShenHeOperatorId = m.IsShenHe ? SiteUserInfo.UserId : string.Empty;
                m.ShenHeTime = m.IsShenHe ? DateTime.Now : new DateTime?();
                result = new EyouSoft.BLL.YlStructure.BHuiYuan().UpdateDianPing(m);
            }
            if (result == 1) Common.Utils.RCWE(Common.UtilsCommons.AjaxReturnJson("1", "操作成功"));
            Common.Utils.RCWE(Common.UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        void InitPage()
        {
            var h = new BLL.YlStructure.BHangQi().GetHangQiInfo(Common.Utils.GetQueryStringValue("hangqiid"));
            if (h != null) { this.litmingcheng.Text = h.MingCheng; this.rdo5.Checked = true; }
            var m = new BLL.YlStructure.BHuiYuan().GetDianPingInfo(Common.Utils.GetQueryStringValue("id"));
            if (m != null)
            {
                h = new BLL.YlStructure.BHangQi().GetHangQiInfo(m.HangQiId);
                this.litmingcheng.Text = h != null ? h.MingCheng : string.Empty;
                switch ((int)Math.Ceiling(m.FenShu))
                {
                    case 1:
                        this.rdo1.Checked = true;
                        break;
                    case 2:
                        this.rdo2.Checked = true;
                        break;
                    case 3:
                        this.rdo3.Checked = true;
                        break;
                    case 4:
                        this.rdo4.Checked = true;
                        break;
                    default:
                        this.rdo5.Checked = true;
                        break;
                }
                this.txtbiaoti.Text = m.BiaoTi;
                this.txtHuiDa.Text = m.NeiRong;
                this.isshenhe.SelectedIndex = m.IsShenHe ? 1 : 0;
            }
        }
    }
}
