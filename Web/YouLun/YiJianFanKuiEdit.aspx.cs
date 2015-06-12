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
using Web.YouLun.WUC;
using System.Collections.Generic;

namespace EyouSoft.Web.YouLun
{
    public partial class YiJianFanKuiEdit : EyouSoft.Common.Page.BackPage
    {
        protected Model.EnumType.YlStructure.YiJianFanKuiLeiXing LeiXing = Model.EnumType.YlStructure.YiJianFanKuiLeiXing.网站改版建议;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }
        void InitPage()
        {
            var m = new BLL.YlStructure.BWz().GetWZYiJianFanKui(Common.Utils.GetQueryStringValue("editid"));
            if (m == null) return;
            this.LeiXing = m.LeiXing.Value;

            MFileInfo file = new MFileInfo();
            file.FilePath = m.FilePath;
            var items = new List<MFileInfo>();
            items.Add(file);
            this.upload1.YuanFiles = items;

            this.txtMiaoShu.Value = m.MiaoShu;
            this.litRemoteIP.Text = m.RemoteIP;
            this.litIssueTime.Text = m.IssueTime.ToString();
            if (!string.IsNullOrEmpty(m.OperatorId))
            {
                var p = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.OperatorId);
                if (p != null) this.litOperator.Text = p.XingMing;
            }
        }
    }
}
